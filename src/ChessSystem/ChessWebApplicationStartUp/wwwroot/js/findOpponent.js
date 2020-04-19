let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/OnlineUsers")
        .build();

    connection.on("NewUser", function (user) {
        document.getElementById("usersLoggedIn").innerHTML +=
            `<li id="${user.userId}linkForInvitation" class="list-group-item d-flex justify-content-between align-items-center">
                    ${user.username}
                    <span onclick="InviteUserToPlay('${user.userId}')" class="badge badge-primary badge-pill">Challenge</span>
             </li>`;
    });


    connection.on("UserDisconnected", function (user) {
        document.getElementById(user.userId).remove();
    });

    connection.on("HandleInvitation", function (user) {
        alert(user + "is inviting you to play")
    });

   

    connection.start()
        .catch(err => console.error(err.toString()));
};

setupConnection();

function InviteUserToPlay(InvitedId) {
    connection.invoke("InvitedUser", InvitedId)
}