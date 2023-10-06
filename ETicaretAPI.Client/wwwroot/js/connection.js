var connection = new signalR.HubConnectionBuilder()
    .withUrl('https://localhost:7107/connectionHub')
    .withAutomaticReconnect([1000, 2000, 3000, 4000, 2000, 2000, 2000, 2000, 2000, 2000, 2000, 2000, 2000, 2000, 2000])
    .build();

var myDiv = document.getElementById('durum');
async function start() {
    try {
        await connection.start();
        console.log('ConnectionHub connected')
    } catch (e) {
        console.log('ConnectionHub disconnected', +e.message)
        setTimeout(() => start(), 2000);
    }
}
connection.onreconnected(connectionId => {
    myDiv.style.color = 'white';
    myDiv.style.backgroundColor = 'blue';
    myDiv.innerHTML = 'connection established !';
    setTimeout(function () {
        myDiv.style.display = 'none';
    }, 2000);
});
connection.onreconnecting(error => {
    myDiv.style.color = 'white';
    myDiv.style.backgroundColor = 'blue';
    myDiv.innerHTML = 'establishing connection...';
    setTimeout(function () {
        myDiv.style.display = 'none';
    }, 2000);
});
connection.onclose(connectionId => {
    myDiv.style.color = 'white';
    myDiv.style.backgroundColor = 'blue';
    myDiv.innerHTML = 'connection disconnected or not connected !!!';
    setTimeout(function () {
        myDiv.style.display = 'none';
    }, 2000);
});
start();