const config = require('../config.json');
const db = require('../_helpers/db');
const userService = require('../users/user.service');
const Invite = db.Invite;

module.exports = {
    createInvite,
    editInvite,
}

async function createInvite(ownersID, inviteId) {
    console.log(ownersID);
    console.log(inviteId);
    
    const owner = await userService.getById(ownersID);

    const invitee = await userService.getById(inviteId);

    console.log(owner);
    console.log(invitee);
    if (owner === null || invitee === null) {
        throw 'it is not possible to create an invited';
    }

    if (owner.friendList.includes(inviteId)) {
        throw 'You are already friends';
    }

    if (await Invite.findOne({
        ownerId: ownersID,
        inviteeId: inviteId
    }) !== null ) {
        throw 'already sent an invite';
    }

    const invite = new Invite({
        ownerName: owner.username,
        ownerId: owner.id,
        inviteeName: invitee.username,
        inviteeId: invitee.id,
    });

    await invite.save();
    await owner.createdInvites.push(invite.id);
    await invitee.openInvites.push(
        {name: owner.username, invitedId: invite.id}
    );

    await owner.save();
    await invitee.save();

    return await invite;
}

async function editInvite(userId, inviteid, accepted) {
    const invite = await Invite.findById(inviteid);
    const invitee = await userService.getById(userId);

    if (!invite) {
        throw 'no invite for you';
    }

    if (invite.inviteeId !== invitee.id) {
        throw 'no invite for you';
    }

    const inviter = await userService.getById(invite.ownerId);
    if(!inviter || !inviter.createdInvites.includes(inviteid)) {
        throw 'no invite for you'
    }

    invitee.openInvites = invitee.openInvites.filter( obj => obj.invitedId !== inviteid);
    inviter.createdInvites = inviter.createdInvites.filter(obj => obj !== inviteid);

    if (accepted === true) {
        invitee.friendList.push(inviter.id);
        inviter.friendList.push(invitee.id);
    }
    await Invite.findByIdAndRemove(inviteid);
    await invitee.save();
    await inviter.save();

}
