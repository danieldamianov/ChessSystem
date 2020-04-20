let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/Game/Communication")
        .build();

   
    connection.on("OpponentHasMadeMove", function (initialPositionHorizontal,
        initialPositionVertical,
        targetPositionHorizontal,
        targetPositionVertical,
        figureType,
        figureColor
    ) {
        
    });


    connection.start()
        .catch(err => console.error(err.toString()));
};

setupConnection();

function SendMove(initialPositionHorizontal,
    initialPositionVertical,
    targetPositionHorizontal,
    targetPositionVertical,
    figureType,
    figureColor) {
    connection.invoke("UserHasMadeMove", initialPositionHorizontal,
        initialPositionVertical,
        targetPositionHorizontal,
        targetPositionVertical,
        figureType,
        figureColor);
}
