$(function () {

    var chat = $.connection.chatHub;

    var app = new Vue({
        el: "#app",
        data: {
            type: "client",
            isStarted : false,
            username: null,
            message: null,
            messages: [],
            chatId: null
        },
        methods: {
            messageReceived: function (username, message) {
                this.messages.push({ username: username, message: message });
            },
            sendMessage: function () {
                chat.server.sendMessage(this.chatId, this.username, this.message);
                this.message = null;
            },
            startReceiving: function () {
                this.isStarted = true;
                $.connection.hub.start().done(() => {
                    chat.server.joinChat(this.chatId);    
                });
            }
        }
    });

    chat.client.messageReceived = app.messageReceived;

});

