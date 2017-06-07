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
            sendMessage: function () {
                chat.server.sendMessage(this.chatId, this.username, this.message);
                this.message = null;
            },
            startReceiving: function () {
                this.isStarted = true;
                if (this.type == "approver") this.chatId = "-100";
                $.connection.hub.start().done(() => {
                    chat.server.joinChat(this.chatId).then(() => {
                        chat.server.getChat(this.chatId);
                    });    
                });
            },
            getChat: function(messages) {
                this.messages = messages;
            },
            approveMessage : function(message) {
                chat.server.approveMessage(message);
            }
        }
    });

    chat.client.messageReceived = app.messageReceived;
    chat.client.getChat = app.getChat;

});

