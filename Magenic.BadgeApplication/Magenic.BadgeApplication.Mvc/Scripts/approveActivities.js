function refreshPreviousActivitiesList() {
    var listUrl = $('#UrlToList').val();
    $('#activitiesForApproval').load(listUrl);
}

function approveActivity(element, activityId) {
    var approvalUrl = $('#UrlToApprove').val();
    $.post(approvalUrl, { SubmissionId: activityId }, function (data) {
        refreshPreviousActivitiesList();
    });
}

function rejectActivity(element, activityId) {
    var rejectionUrl = $('#UrlToReject').val();
    $.post(rejectionUrl, { SubmissionId: activityId }, function (data) {
        refreshPreviousActivitiesList();
    });
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
                alert('Internel Server Error.');
            } else if (status == 'parsererror') {
                alert('Error.\nParsing JSON Request failed.');
            } else if (status == 'timeout') {
                alert('Request Time out.');
            } else {
                alert('Unknow Error.\n' + x.responseText);
            }
        }
    });
});