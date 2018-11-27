$(function () {
    $(".circle-img").css("clip-path", function() {
        return "circle(" + $(this).width() / 2 + "px at center)";
    });
});