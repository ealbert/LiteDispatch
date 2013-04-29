$(document).ready(function () {
    $("#detail_up").click(function () {
        $("#featured_detail").slideUp("slow");
        $("#detail_up").fadeOut("slow", function () {
            $("#detail_down").fadeIn("slow");
        });
    });
    $("#detail_down").click(function () {
        $("#featured_detail").slideDown("slow");
        $("#detail_down").fadeOut("slow", function () {
            $("#detail_up").fadeIn("slow");
        });
    });
    $("#detail_up").hide();
    $("#featured_detail").hide();
});