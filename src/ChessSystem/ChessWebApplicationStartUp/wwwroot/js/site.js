let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/OnlineUsers")
        .build();

    connection.on("NewUser", function (user) {
        document.getElementById("usersLoggedIn").innerHTML +=
            `<li id="${user.userId}" class="list-group-item d-flex justify-content-between align-items-center">
                    ${user.username}
                    <span class="badge badge-primary badge-pill">Challenge</span>
             </li>`;
    });

    
    connection.on("UserDisconnected", function (user) {
        document.getElementById(user.userId).remove();
    });

    connection.start()
        .catch(err => console.error(err.toString()));
};

setupConnection();