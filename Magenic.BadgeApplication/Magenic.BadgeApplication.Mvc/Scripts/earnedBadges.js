$(document).ready(function () {
    var canEditRequiresApproval = $('#CanEditRequiresApproval').val() === 'true';
    var canEditEntryType = $('#CanEditEntryType').val() === 'true';

    //Prepare jtable plugin
    $('#earned-badges-tbl').jtable({
        title: 'Earned Badges',
        paging: true,
        actions: {
            listAction: '/BadgeManager/EarnedBadgesList',
            deleteAction: '/BadgeManager/DeleteEarnedBadge',
        },
        fields: {
            BadgeAwardId: {
                key: true,
                create: false,
                edit: false,
                list: false
            },
            Name: {
                title: 'Badge Name'
            },
            BadgeEffectiveEnd: {
                title: 'Effective End Date',
                type: 'date',
                displayFormat: 'mm/dd/yy'
            },
            EmployeeName: {
                title: 'Employee Name'
            },
            AwardDate: {
                title: 'Award Date',
                type: 'date',
                displayFormat: 'mm/dd/yy'
            },
            PaidOut: {
                title: 'Paid Out'
            },
            AwardAmount: {
                title: 'Award Amount'
            }
        }
    });

    //Load person list from server
    $('#earned-badges-tbl').jtable('load');
});
