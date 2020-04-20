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

    connection.on("StartGameAsBlack", function (user) {
        window.location.replace(`/Game/Play?WhitePlayerId=${user.whitePlayerId}&BlackPlayerId=${user.blackPlayerId}&PlayerColor=black`);
    });

    connection.on("StartGameAsWhite", function (user) {
        window.location.replace(`/Game/Play?WhitePlayerId=${user.whitePlayerId}&BlackPlayerId=${user.blackPlayerId}&PlayerColor=white`);
    });


    connection.on("HandleInvitation", function (user) {
        document.getElementById("usersInvitingCurrentUserToPlay").innerHTML +=
            `<li id="${user.userId}linkForAcceptingGame" class="list-group-item d-flex justify-content-between align-items-center">
                    ${user.username}
                    <span onclick="AcceptGame('${user.userId}')" class="badge badge-primary badge-pill">Accept</span>
             </li>`;
    });

    connection.start()
        .catch(err => console.error(err.toString()));
};

setupConnection();

function AcceptGame(opponentId) {
    connection.invoke("AcceptGame", opponentId);
}

function InviteUserToPlay(InvitedId) {
    connection.invoke("InviteUserToPlay", InvitedId)
}