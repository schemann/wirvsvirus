const config = require('../config.json');
const mongoose = require('mongoose');

mongoose.connect(process.MONGODB_URL || config.conntectionString, {useCreateIndex: true, useNewUrlParser: true});
mongoose.Promise = global.Promise;

module.exports = {
    User: require('../users/user.model')
};

