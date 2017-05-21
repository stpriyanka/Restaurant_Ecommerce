/// <reference path="jquery-3.1.1.min.js" />
/// <reference path="jquery.signalR-2.2.2.min.js" />

var getName = function () {
	//var name1 = document.querySelector('#start1').value;
	document.getElementById('start1').style.visibility = 'hidden';
	document.getElementById('startButton').style.visibility = 'hidden';
}


$(function () {
	console.log($.connection);

	var time = new Date();
	var currentTime = time.getHours() + ":" + time.getMinutes() + ":" + time.getSeconds();
	console.log(time.getHours() + ":" + time.getMinutes() + ":" + time.getSeconds());


	// Declare a proxy to reference the hub.
	var chat = $.connection.chatHub;

	console.log('Now connected, connection ID=' + $.connection.hub.id);

	// Create a function that the hub can call to broadcast messages.
	chat.client.broadcastMessage = function (name, message) {
		// Html encode display name and message.
		document.getElementById('start1').style.visibility = 'hidden';
		var encodedName = name;
		var encodedMsg = $('<div />').text(message).html();

		// Add the message to the page.
		$('#discussion').append('<li><strong>' + encodedName
			+ '</strong>:&nbsp;&nbsp;' + encodedMsg + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;time:'+'&nbsp;&nbsp;' + currentTime + '</li>');

		return;
	}
	// Get the user name and store it to prepend to messages.
	// Set initial focus to message input box.
	$('#message').focus();
	// Start the connection.
	$.connection.hub.start().done(function () {
		$('#sendmessage').click(function () {
			var name1 = document.querySelector('#start1').value;
			if (!name1) {
				name1 = "Guest";
			}
			// Call the Send method on the hub.
			chat.server.send(name1, $('#message').val());
			// Clear text box and reset focus for next comment.
			$('#message').val('').focus();
		});
	});
});