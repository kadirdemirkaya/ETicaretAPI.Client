var connection = new signalR.HubConnectionBuilder().withUrl("/communicationCustomerMessage").build();

connection.on("sendMessage", function (message) {
    console.log("incoming message:", message);
});

connection.start()
    .then(function () {
        console.log("SignalR connection started.");
    })
    .catch(function (err) {
        console.error(err.toString());
    });
