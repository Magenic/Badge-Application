$(document).ready(function () {
    var actions = $('#CanDelete').val() === 'True'
        ?    
        {
            listAction: $('#ListAction').val(),
            deleteAction: $('#DeleteAction').val(),
        }
        :
        {
            listAction: $('#ListAction').val(),
        };

    //Prepare jtable plugin
    $('#earned-badges-tbl').jtable({
        title: 'Earned Badges',
        sorting: true,
        defaultSorting: 'Name ASC',
        paging: true,
        actions: actions,
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
                title: 'Paid Out',
                sorting: false
            },
            AwardAmount: {
                title: 'Award Amount',
                sorting: false
            }
        }
    });

    //Load person list from server
    $('#earned-badges-tbl').jtable('load');
});
