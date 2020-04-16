let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/Users")
        .build();

    connection.on("NewUser", function (username) {
        document.getElementById("usersLoggedIn").innerHTML += "New user : " + "<p id = " + username + ">" + username + "</p>";
    });

    
    connection.on("UserDisconnected", function (username) {
        document.getElementById(username).remove();
    });


    connection.start()
        .catch(err => console.error(err.toString()));
};

setupConnection();

document.getElementById("submit").addEventListener("click", e => {
    e.preventDefault();
    const product = document.getElementById("product").value;
    const size = document.getElementById("size").value;

    fetch("/Coffee",
        {
            method: "POST",
            body: JSON.stringify({ product, size }),
            headers: {
                'content-type': 'application/json'
            }
        })
        .then(response => response.text())
        .then(id => connection.invoke("GetUpdateForOrder", parseInt(id)));
});