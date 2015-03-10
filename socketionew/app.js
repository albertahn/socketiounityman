var app = require('http').createServer(handler)
, io = require('socket.io').listen(app)
, fs = require('fs')
app.listen(3000);
function handler (req, res) {
fs.readFile(__dirname + '/index.html',
function (err, data) {
if (err) {
res.writeHead(500);
return res.end('Error loading index.html');
}
res.writeHead(200);
res.end(data);
});
}


var usernames = {};

// rooms which are currently available in chat
var rooms = ['room1','room2','room3'];


io.sockets.on('connection', function (socket) {
    
   

socket.on('adduser', function(username){
    
    
    socket.username = username;
		// store the room name in the socket session for this client
		/*socket.room = 'room1';
		// add the client's username to the global list
		usernames[username] = username;
		// send client to room 1
		socket.join('room1');
		// echo to client they've connected
		socket.emit('updatechat', 'SERVER', 'you have connected to room1');
		// echo to room 1 that a person has connected to their room
		socket.broadcast.to('room1').emit('updatechat', 'SERVER', username + ' has connected to this room');
		socket.emit('updaterooms', rooms, 'room1');
                */
                io.sockets.in(socket.room).emit('new message', socket.username, "hi there this is "+socket.username);
    
});
    
    
    // when the client emits 'sendchat', this listens and executes
	/*socket.on('sendchat', function (data) {
		// we tell the client to execute 'updatechat' with 2 parameters
		io.sockets.in(socket.room).emit('updatechat', socket.username, data);
               
                //emit to all the data
                //io.sockets.emit('new message', data);
                
console.log(data);
                
	});*/
    
socket.emit('news', { hello: 'world' });


socket.on('my other event', function (data) {
    
    
    
console.log(data);
});


socket.on('send message', function(data){
    
		io.sockets.emit('new message', socket.username, data);
                io.sockets.in(socket.room).emit('new message', socket.username, data);
                
	});
        
        
});