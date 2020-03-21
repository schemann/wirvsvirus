const config = require('../config.json');
const db = require('../_helpers/db');
const userService = require('../users/user.service');
const uuidv4 = require('uuid');
const Invite = db.Invite;

module.exports = {
    createInvite,
    acceptInvite,
}

async function createInvite(ownersID, inviteId) {
    console.log(ownersID);
    console.log(inviteId);
    
    const owner = await userService.getById(ownersID);

    const invitee = await userService.getById(inviteId);

    console.log(owner);
    console.log(invitee);
    if (owner === null && invitee === null) {
        throw 'it is not possible to create an invited';
    }

    const invite = new Invite({
        uuid: uuidv4.v4(),
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

async function acceptInvite(userId, inviteid) {
    const invite = await Invite.findById(inviteid);
    const invitee = await userService.getById(userId);

    if (invite.inviteeId !== invitee.id) {
        throw 'no invite for you';
    }

    const inviter = await userService.getById(invite.ownerId);
    if(!inviter || !inviter.createdInvites.includes(inviteid)) {
        throw 'no invite for you'
    }

    invitee.openInvites = invitee.openInvites.filter( obj => obj.invitedId !== inviteid);
    inviter.createdInvites = inviter.createdInvites.filter(obj => obj !== inviteid);

    invitee.friendList.push(inviter.id);
    inviter.friendList.push(invitee.id);

    await invitee.save();
    await inviter.save();

}