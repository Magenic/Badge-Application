$(document).ready(function () {
    var canEditRequiresApproval = $('#CanEditRequiresApproval').val() === 'true';

    //Prepare jtable plugin
    $('#ActivitiesTable').jtable({
        title: 'Activity Management',
        paging: true,
        actions: {
            listAction: $('#ListAction').val(),
            createAction: $('#CreateAction').val(),
            updateAction: $('#UpdateAction').val(),
            deleteAction: $('#DeleteAction').val()
        },
        fields: {
            Id: {
                key: true,
                create: false,
                edit: false,
                list: false
            },
            Name: {
                title: 'Name'
            },
            Description: {
                title: 'Description',
                type: 'textarea'
            },
            RequiresApproval: {
                title: 'Requires Approval',
                type: 'checkbox',
                defaultValue: false,
                create: canEditRequiresApproval,
                edit: canEditRequiresApproval,
            }
        }
    });

    //Load person list from server
    $('#ActivitiesTable').jtable('load');
});
