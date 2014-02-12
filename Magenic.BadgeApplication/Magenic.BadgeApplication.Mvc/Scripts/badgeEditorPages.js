function checkAllBoxes(element) {
    var allChecked = $(element).is(':checked');
    $('.table tbody [type="checkbox"]').prop('checked', allChecked);
}

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