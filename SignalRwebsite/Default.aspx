<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SignalRwebsite.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>
    <script src="Scripts/jquery.signalR-2.2.2.min.js"></script>
    <script src="signalr/hubs"></script>
    <script src="https://unpkg.com/vue"></script>
    <script src="logic.js"></script>

</head>
<body>
    <div id="app">
        <div v-if="!isStarted">
            <select v-model="type">
                <option>client</option>
                <option>approver</option>
            </select>
            <input type="number" v-model="chatId" placeholder="Chat Id" v-if="type == 'client'" />
            <input v-model="username" placeholder="username">
            <button v-on:click="startReceiving">Start</button>
        </div>
        <div v-if="isStarted">
            Name: {{ username }}, Role: {{ type }},
            ChatId: {{ chatId }}
        </div>
        <div v-if="isStarted && type == 'client'">
            Send message: <br />
            <input v-model="message" placeholder="Message">
            <button v-on:click="sendMessage">Send</button>
        </div>
        <div v-if="isStarted">
            <div v-for="msg in messages" style="margin: 4px;">
                {{ msg.Name }}: {{ msg.Message }}
                <button v-if="type == 'approver'" v-on:click="approveMessage(msg)">Approve</button>
            </div>
        </div>
    </div>
</body>
</html>
