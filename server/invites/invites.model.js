const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const inviteSchema = new Schema({
    ownerName: {type: String, required: true},
    ownerId: {type: String, required: true},
    inviteeName: {type: String, required: true},
    inviteeId: {type: String, required: true}
})

inviteSchema.set('toJSON', {virtuals: true});

module.exports = mongoose.model('Invite', inviteSchema);