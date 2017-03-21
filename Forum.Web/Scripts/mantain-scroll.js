var xPos, yPos;
var prm = Sys.WebForms.PageRequestManager.getInstance();

prm.add_beginRequest(BeginRequestHandler);
prm.add_endRequest(EndRequestHandler);

function BeginRequestHandler(sender, args) {
    xPos = $get('scrollDiv').scrollLeft;
    yPos = $get('scrollDiv').scrollTop;
}
function EndRequestHandler(sender, args) {
    $get('scrollDiv').scrollLeft = xPos;
    $get('scrollDiv').scrollTop = yPos;
}