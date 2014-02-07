function removeCorporateBadgeValue() {
    $('#NotBoundType').prop('checked', false);
}

$(document).ready(function () {
    $(':file').filestyle();
    $('.datepicker').datepicker();

    $('.datepicker').each(function () {
        var startingValue = $(this).val();
        $(this).val(startingValue.substring(0, startingValue.indexOf(' ')));
    });

    $('.selectpicker').selectpicker();
});