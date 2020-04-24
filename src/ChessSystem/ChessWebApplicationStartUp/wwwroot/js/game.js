let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/Game/Communication")
        .build();
    connection.serverTimeoutInMilliseconds = 100000; // 100 second
   
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

function SendMove(opponentId,
    initialPositionHorizontal,
    initialPositionVertical,
    targetPositionHorizontal,
    targetPositionVertical,
    figureType,
    figureColor) {
    connection.invoke("UserHasMadeMove",
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

