$(document).ready(function () {
    var actions = {
        listAction: $('#ListAction').val(),
        updateAction: $('#UpdateAction').val()
    }

    //Prepare jtable plugin
    $('#PermissonsTable').jtable({
        title: 'Employee Permissions',
        sorting: false,
        defaultSorting: 'FirstName ASC',
        paging: true,
        actions: actions,
        fields: {
            EmployeePermissionId: {
                key: true,
                create: false,
                edit: false,
                list: false
            },
            FirstName: {
                title: 'First Name',
                edit: false
            },
            LastName: {
                title: 'Last Name',
                edit: false
            },
            PermissionId: {
                title: 'Permission Name',
                list: true,
                edit: true,
                options: { 1: 'User', 2: 'Administrator', 3: 'Manager' },
                type: 'combobox',
            }
        }
    });

    //Load person list from server
    $('#PermissonsTable').jtable('load');
});
