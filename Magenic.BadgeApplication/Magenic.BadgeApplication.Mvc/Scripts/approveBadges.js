function refreshPreviousBadgesList() {
    var listUrl = $('#UrlToList').val();
    $('#badgesForApproval').load(listUrl);
}

function approveBadge(element, activityId) {
    var approvalUrl = $('#UrlToApprove').val();
    $.post(approvalUrl, { badgeId: activityId }, function (data) {
        refreshPreviousBadgesList();
    });
}

function rejectBadge(element, activityId) {
    var rejectionUrl = $('#UrlToReject').val();
    $.post(rejectionUrl, { badgeId: activityId }, function (data) {
        refreshPreviousBadgesList();
    });
}