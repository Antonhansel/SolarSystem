'use strict';


// require socket.io
var io = require('socket.io')();
io.on('connection', function(socket) {

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

// Get the port from the heroku env
io.listen(process.env.PORT || 3001);
