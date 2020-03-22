const express = require('express');
const router = express.Router();
const userService = require('./user.service');

module.exports = router;

router.post('/authenticate', authenticate);
router.post('/register', register);
router.post('/setHomeBase', setHomeBase)
router.post('/pingHomeBase', pingHomeBase);
router.get('/getAll', getAll);
router.get('/current', getCurrent);
router.get('/:id', getById);
router.put('/:id', update);
router.post('/removeFriend/:id', removeFriend);


function authenticate(req, res, next) {
    userService.authenticate(req.body)
        .then(user => user ? res.json(user) : res.status(400).json({message: 'Username or password is incorrect'}))
        .catch(err => next(err));
}

function setHomeBase(req, res, next) {
    userService.setHomeBase(req.body, req.user.sub, req.query.override)
        .then(user => user ? res.json(user): res.status(422).json({message: 'Homebase already set'}))
        .catch(err => next(err));
}

function register(req, res, next) {
    console.log(req.body);
    userService.create(req.body) 
        .then(users => res.json({}))
        .catch(err => next(err));
}

function getAll(req, res, next) {
    userService.getAll()
        .then(users => res.json(users))
        .catch(err => next(err));
}

function pingHomeBase(req, res, next) {
    userService.pingHomeBase(req.body, req.user.sub)
        .then(user => user ? res.json(user) : res.status(422).json({}))
        .catch(err => next(err));
}

function getCurrent(req, res, next) {
    userService.getById(req.user.sub)
        .then(user => user ? res.json(user): res.sendStatus(404))
        .catch(err => next(err));
}

function getById(req, res, next) {
    userService.getById(req.params.id)
        .then(user => user ? res.json(user): res.sendStatus(404))
        .catch(err => next(err));
}

function update(req, res, next) {
    userService.update(req.params.id, req.body)
        .then(() => res.json({}))
        .catch(err => res.json({}));
}

function removeFriend(req,  res, next) {
    userService.removeFriend(req.user.sub, req.params.id)
        .then(() => res.json({}))
        .catch(err => next(err));
}

function _delete(req, res, next) {
    return;
}