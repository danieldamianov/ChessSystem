let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/OnlineUsers")
        .build();

    connection.on("NewUser", function (user) {
        document.getElementById("usersLoggedIn").innerHTML +=
            `<li id="${user.userId}linkForInvitation" class="list-group-item d-flex justify-content-between align-items-center">
                    ${user.username}
                    <span onclick="InviteUserToPlay('${user.userId}','${user.username}')" class="badge badge-primary badge-pill">Challenge</span>
             </li>`;
    });


    connection.on("UserDisconnected", function (user) {
        document.getElementById(user.userId + "linkForInvitation").remove();
    });

    connection.on("StartGameAsBlack", function (user) {
        post(`/Game/Play`, { WhitePlayerId: user.whitePlayerId, BlackPlayerId: user.blackPlayerId, PlayerColor: "black" })
    });

    connection.on("StartGameAsWhite", function (user) {
        post(`/Game/Play`, { WhitePlayerId: user.whitePlayerId, BlackPlayerId: user.blackPlayerId, PlayerColor: "white" })
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

/**
 * sends a request to the specified url from a form. this will change the window location.
 * @param {string} path the path to send the post request to
 * @param {object} params the paramiters to add to the url
 * @param {string} [method=post] the method to use on the form
 */

function post(path, params, method = 'post') {

    // The rest of this code assumes you are not using a library.
    // It can be made less wordy if you use one.
    const form = document.createElement('form');
    form.method = method;
    form.action = path;

    for (const key in params) {
        if (params.hasOwnProperty(key)) {
            const hiddenField = document.createElement('input');
            hiddenField.type = 'hidden';
            hiddenField.name = key;
            hiddenField.value = params[key];

            form.appendChild(hiddenField);
        }
    }

    document.body.appendChild(form);
    form.submit();
}

function AcceptGame(opponentId) {
    connection.invoke("AcceptGame", opponentId);
}

function InviteUserToPlay(InvitedId, invitedUsername) {
    connection.invoke("InviteUserToPlay", InvitedId)

    document.getElementById("usersThatCurrentUserHasInvited").innerHTML +=
        `<li class="list-group-item d-flex justify-content-between align-items-center">
                    You successfully invited ${invitedUsername} to play chess against you.
                    Please wait here until the user accepts your invitation.
                    When that happens, you will automatically be redirected to the game page and the game will start.
        </li>`;
}