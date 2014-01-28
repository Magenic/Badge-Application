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