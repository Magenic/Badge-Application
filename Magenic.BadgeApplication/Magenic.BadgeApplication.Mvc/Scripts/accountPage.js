$(document).ready(function () {
    // TODO: can we get this to work with knockout?
    bindSlider();
});

function bindSlider() {
    $(".slider").slider()
        .on("slideStop", function (e) {
            $(this).closest('form').submit();
        });
}