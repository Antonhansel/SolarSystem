'use strict';

// set up ======================================================================
// get all the tools we need
var morgan = require('morgan');
var path = require('path');
var express = require('express');
var app = express.createServer();
app.listen(process.env.PORT || 4567);
var io = require('socket.io').listen(app);

// configuration ===============================================================
app.use(morgan('dev'));
app.set('view engine', 'ejs');

// to get local files
app.use(express.static(path.join(__dirname, 'public')));

app.get('/', function(req, res) {
  res.render('menu.ejs', {
    layout: false,
  });
});


// require socket.io
io.on('connection', function(socket) {
  console.log("New connection");

  socket.on("connection", function() {
    socket.emit("test", {status: "connection"});
  });

  socket.on("move", function(data) {
    console.log(data);
    socket.broadcast.emit("move", data);
  });

  socket.on("code", function(data) {
    socket.channel = data.code;
  });

  socket.on("data", function(data) {
    socket.broadcast.emit("data", {
      channel: socket.channel,
      message: data.message
    });
  });

  socket.on('disconnect', function() {
    socket.broadcast.emit("connectionLost", {
      channel: socket.channel,
    });
  });
});
