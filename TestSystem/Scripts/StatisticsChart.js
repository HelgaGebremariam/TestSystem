$(document).ready(function () {
    $("#btnLoadChart").click(function () {
        alert("1");
        ////var categoryId = $("#ddlCategory").val();
        $("#dvChart").load('@(Url.Action("SequenceStatistics", "Generator"))');
        //alert("2");
    });
});