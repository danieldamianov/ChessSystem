let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/Users")
        .build();

    connection.on("NewUser", function (user) {
        document.getElementById("usersLoggedIn").innerHTML += `<p id="${user.userId}">New user: ${user.username}</p>`;
    });

    
    connection.on("UserDisconnected", function (user) {
        document.getElementById(user.userId).remove();
    });

    connection.on("UserAlreadyOnline", function (user) {
        alert("user already connected");
        window.location.href = "/";
    });

    


    connection.start()
        .catch(err => console.error(err.toString()));
};

setupConnection();