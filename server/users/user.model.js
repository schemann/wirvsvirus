const mongoose = require('mongoose');
const Schema = mongoose.Schema;


const userSchema = new Schema({
    username: {type: String, unique: true, required: true},
    hash: {type: String, require: true},
    createdDate: {type: Date, default: Date.now},
    homeBase: {longitude: {type: Number, default: null}, latitude: {type: Number, default: null}},
    homeBaseSetDate: {type: Date, default: null},
    lastHomePingDate: {type: Date, default: null},
    currentLevel: {type: Number, default: 0},
    currentXP: {type: Number, default: 0},
    friendList: {type: [String], default: []},
    equimentList: {type: [String], default: []},
    currentEquimentList: {type: [String], default: []}

});
userSchema.set('toJSON', {virtuals: true});

module.exports = mongoose.model('User', userSchema);