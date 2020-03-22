const express = require('express');
const router = express.Router();
const inviteSerivce = require('./invites.service');


module.exports = router;
router.post('/createInvite/:id', createInvite);
router.post('/acceptInvite/:id', acceptInvite);
router.post('/declineInvite/:id', declineInvite);

function createInvite(req, res, next) {
    inviteSerivce.createInvite(req.user.sub, req.params.id)
        .then(invite => invite ? res.json(invite) : res.status(400).json({message: 'could not create invite'}))
        .catch(err => next(err));
}


function acceptInvite(req, res, next) {
    inviteSerivce.editInvite(req.user.sub, req.params.id, true)
        .then(() => res.json({}))
        .catch(err => next(err));
}

function declineInvite(req, res, next) {
    inviteSerivce.editInvite(req.user.sub, req.params.id, false)
        .then(() => res.json({}))
        .catch(err => next(err));
}