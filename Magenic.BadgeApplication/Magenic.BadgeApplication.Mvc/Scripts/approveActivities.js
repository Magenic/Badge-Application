function refreshPreviousActivitiesList() {
    var listUrl = $('#UrlToList').val();
    listUrl = listUrl.substr(0, listUrl.length - 4)  + $('#showAdminView').prop('checked');
    $('#activitiesForApproval').load(listUrl);
}

function approveActivity(element, activityId) {
    disableAnchors(element);
    var approvalUrl = $('#UrlToApprove').val();
    $.post(approvalUrl, { SubmissionId: activityId }, function (data) {
        refreshPreviousActivitiesList();
    });
}

function rejectActivity(element, activityId) {
    disableAnchors(element);
    var rejectionUrl = $('#UrlToReject').val();
    $.post(rejectionUrl, { SubmissionId: activityId }, function (data) {
        refreshPreviousActivitiesList();
    });
}

function disableAnchors(anchor) {
    var parent = $(anchor).parent();
    parent.addClass('not-allowed');

    var anchors = parent.find('a.btn');
    anchors.attr('disabled', true);

    var spinner = $(anchor).find('.fa.fa-spinner.fa-spin');
    $(spinner).removeClass('hide');
}

// TODO: maybe this should be in a global script?
$(document).ready(function () {
    $.ajaxSetup({
        error: function (xhr, status, err) {
            if (xhr.status === 0) {
                alert('You are offline!!\n Please Check Your Network.');
            } else if (xhr.status === 404) {
                alert('Requested URL not found.');
            } else if (xhr.status === 400) {
                alert(xhr.responseText);
            } else if (xhr.status === 500) {
                alert('Internal Server Error.');
            } else if (status == 'parsererror') {
                alert('Error.\nParsing JSON Request failed.');
            } else if (status == 'timeout') {
                alert('Request Time out.');
            } else {
                alert('Unknown Error.\n' + x.responseText);
            }
        }
    });
});
