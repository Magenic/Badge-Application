
//Handles the toggle button (whether or not to display all users -
// or just those above a certain threshold).
function PointsReport() {
    $.ajax({
        url: UrlSettings.ApproveActivitiesUrl,
        cache: false,
        data: {
            displayAll: $('#displayAll').prop('checked')
        },
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#displayThreshold').html(data);
        },
        failure: function (data) {
            alert($('#displayAll').val());
        }

    });
}

(function () {

    $('body').on('click', '.modal-link', function (e) {
        e.preventDefault();
        $(this).attr('data-target', '#modal-container');
        $(this).attr('data-toggle', 'modal');
    });
    $('body').on('click', '.modal-close-btn', function () {
        $('#modal-container').modal('hide');
    });
    $('#modal-container').on('hidden.bs.modal', function () {
        $(this).removeData('bs.modal');
    });
    $('#CancelModal').on('click', function () {
        return false;
    });

})();

(function () {
    $('#approve-btn').click(function () {
        $('#modal-container').modal('hide');
    });
})();




