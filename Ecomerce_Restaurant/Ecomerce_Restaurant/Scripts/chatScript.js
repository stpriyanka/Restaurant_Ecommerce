﻿/// <reference path="jquery-3.1.1.min.js" />
/// <reference path="jquery.signalR-2.2.2.min.js" />


$(function () {

		console.log($.connection);
		// Declare a proxy to reference the hub.
		var chat = $.connection.chatHub;

		console.log('Now connected, connection ID=' + $.connection.hub.id);

		// Create a function that the hub can call to broadcast messages.
		chat.client.broadcastMessage = function (name, message) {
			// Html encode display name and message.
			var encodedName = $('<div />').text(name).html();
			var encodedMsg = $('<div />').text(message).html();
			// Add the message to the page.
			$('#discussion').append('<li><strong>' + encodedName
				+ '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
			console.log(encodedName + ' ' + encodedMsg);

		};
		// Get the user name and store it to prepend to messages.
		$('#displayname').val(prompt('Enter your name:', ''));
		// Set initial focus to message input box.
		$('#message').focus();
		// Start the connection.
		$.connection.hub.start().done(function () {
			$('#sendmessage').click(function () {
				// Call the Send method on the hub.
				chat.server.send($('#displayname').val(), $('#message').val());
				// Clear text box and reset focus for next comment.
				$('#message').val('').focus();
			});
		});
	});
