const config = require('../config.json');
const jwt = require('jsonwebtoken');
const bcrypt = require('bcryptjs');
const db = require('../_helpers/db');
const distance = require('gps-distance');
const User = db.User;

module.exports = {
    authenticate,
    getAll,
    getById,
    create,
    update,
    setHomeBase,
    pingHomeBase,
    delete: _delete
};

async function authenticate({username, password}) {
    const user = await User.findOne({username});
    if (user && bcrypt.compareSync(password, user.hash)) {
        const {hash, ...userWithoutHash} = user.toObject();
        const token = jwt.sign({sub: user.id}, config.secret);
        return {...userWithoutHash,
            token
        }; 
    }
}

async function getAll() {
    return await User.find().select('-hash');
}

async function getById(id) {
    return await User.findById(id)
    .select('-hash')
    .select('-homeBaseSetDate');
}

async function setHomeBase({longitude, latitude}, id) {
    const user = await User.findById(id);
    console.log(user);
    if (user === null) {
        throw 'User does not exsits';
    }

    if (user.homeBase.longitude !== null && user.homeBase.latitude !== null) {
        throw 'Homebase is already set';
    }
    user.currentXP = user.currentXP + 100;
    user.homeBase = {longitude, latitude};
    user.homeBaseSetDate = Date.now();
    user.lastHomePingDate = Date.now();
    await user.save();
    return await User.findById(id).select('-hash');
}

async function pingHomeBase({longitude, latitude}, id) {
    const user = await User.findById(id);
    if (user === null) {
        throw 'User does not exsits';
    }
    if (user.homeBase.longitude === null || user.homeBase.latitude === null) {
        throw 'No Homebase set';
    }

    const dist = Math.abs(distance(latitude, longitude, user.homeBase.latitude, user.homeBase.longitude));

    if (dist <= 0.2) {
        user.currentXP += 100;
    }
    await user.save();
    return await User.findById(id).select('-hash');
}

async function create(userParam) {
    if (await User.findOne({username: userParam.username})) {
        throw '"Username "' + userParam.username + '" is already taken';
    }

    const user = new User(userParam);

    if (userParam.password) {
        user.hash = bcrypt.hashSync(userParam.password, 10)
    }

    await user.save()
}

async function update(id, userParam) {
    const user = await User.findById(id);

    if (!user) throw 'User not found';
    if (user.username !== userParam.username && await User.findOne({username: userParam.username})) {
        throw '"Username "' + userParam.username + '" is already taken';
    }

    if (userParam.password) {
        userParam.hash = bcrypt.hashSync(userParam.password, 10);
    }

    Object.assign(user, userParam);

    await user.save();
}

async function _delete(id) {
    await User.findByIdAndRemove(id);
}

