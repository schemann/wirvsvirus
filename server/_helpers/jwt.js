const expressJwt = require('express-jwt');
const config = require('../config.json');
const userSerivce = require('../users/user.service');

module.exports = jwt;

function jwt() {
    const secret = config.secret;
    return expressJwt({secret, isRevoked}).unless ({
        path: [
            '/api/users/authenticate',
            '/api/users/register'
        ]
    });
}

async function isRevoked(req, payload, done) {
    const user = await userSerivce.getById(payload.sub);

    if(!user) {
        return done(null, true);
    }

    done();
}