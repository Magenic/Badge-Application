$(document).ready(function () {
    var canEditRequiresApproval = $('#CanEditRequiresApproval').val() === 'true';
    var canEditEntryType = $('#CanEditEntryType').val() === 'true';

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
                values: { 'false': 'Approval not required', 'true': 'Approval required' },
                create: canEditRequiresApproval,
                edit: canEditRequiresApproval
            },
            EntryType: {
                title: 'Allowed Role',
                type: 'combobox',
                defaultValue: '1',
                options: { 1: 'All Users', 2: 'Managers Only', 3: 'Administrators' },
                list: false,
                create: canEditEntryType,
                edit: canEditEntryType
            }
        }
    });

    //Load person list from server
    $('#ActivitiesTable').jtable('load');
});
