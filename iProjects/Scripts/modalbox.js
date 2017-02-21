//The script read the modalbox element and define the
//modal on it using modal method and calls its "show state"
$(function () {
    var modelbox = function () {
        $("#modalboxtime").modal("show");
    };
    $("#showmodalbox").click(modelbox);
});
    //The script read the modalbox element and define the
    //modal on it using modal method and calls its "show state"
    $(function () {
        var modelbox = function () {
            $("#modalboxstatistics").modal("show");
        };
        $("#showmodalboxstatistic").click(modelbox);
    });