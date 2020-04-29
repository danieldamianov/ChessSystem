let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/Game/Communication")
        .build();
    connection.serverTimeoutInMilliseconds = 100000; // 100 second

    connection.on("OpponentHasMadeCastlingMove", function
        (opponentId,
            kingPositionHorizontal,
            kingPositionVertical,
            rookPositionHorizontal,
            rookPositionVertical,
            figureColor) {
        DotNet.invokeMethodAsync('ChessWebApplicationStartUp', 'OpponentHasMadeCastlingMove',
            opponentId,
            kingPositionHorizontal,
            kingPositionVertical,
            rookPositionHorizontal,
            rookPositionVertical,
            figureColor);
    });

    connection.on("OpponentHasMadeMove", function
        (opponentId,
        initialPositionHorizontal,
        initialPositionVertical,
        targetPositionHorizontal,
        targetPositionVertical,
        figureType,
        figureColor
    ) {
        DotNet.invokeMethodAsync('ChessWebApplicationStartUp', 'OpponentHasMadeMove',
            opponentId,
            initialPositionHorizontal,
            initialPositionVertical,
            targetPositionHorizontal,
            targetPositionVertical,
            figureType,
            figureColor);
                                  
    });


    connection.start()
        .catch(err => console.error(err.toString()));
};

setupConnection();

function SendCastlingMove(
    userThatHasMadeCastlingId,
    opponentId,
    kingPositionHorizontal,
    kingPositionVertical,
    rookPositionHorizontal,
    rookPositionVertical,
    figureColor) {
    connection.invoke("UserHasMadeCastlingMove",
        userThatHasMadeCastlingId,
        opponentId,
        kingPositionHorizontal,
        kingPositionVertical,
        rookPositionHorizontal,
        rookPositionVertical,
        figureColor
    )
}

function SendNormalMove(
    userThatHasMadeTheMove,
    opponentId,
    initialPositionHorizontal,
    initialPositionVertical,
    targetPositionHorizontal,
    targetPositionVertical,
    figureType,
    figureColor) {
    connection.invoke("UserHasMadeMove",
        userThatHasMadeTheMove,
        opponentId,
        initialPositionHorizontal,
        initialPositionVertical,
        targetPositionHorizontal,
        targetPositionVertical,
        figureType,
        figureColor);
}

function enableButtons(buttons) {
    
    buttons.forEach(element => { var id = `${element.horizontal}${element.vertical}field`; document.getElementById(id).disabled = false });
}

function disableButtons(buttons) {

    buttons.forEach(element => { var id = `${element.horizontal}${element.vertical}field`; document.getElementById(id).disabled = true  });
}

