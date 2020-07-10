$(document).ready(function () {
    var actions = {
        listAction: $('#ListAction').val(),
        updateAction: $('#UpdateAction').val()
    }

    //Prepare jtable plugin
    $('#PermissonsTable').jtable({
        title: 'Employee Permissions',
        sorting: true,
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
                list: true,
                edit: true
            },
            LastName: {
                title: 'Last Name',
                list: true,
                edit: true
            },
            PermissionId: {
                title: 'Permission Name',
                list: true,
                edit: true,
                options: { 1: 'User', 2: 'Administrator', 3: 'Manager' },
                type: 'combobox',
            }
        },
        formCreated: function (event, data) {
            data.form.find('input[name="FirstName"]').attr("disabled", "disabled");
            data.form.find('input[name="LastName"]').attr("disabled", "disabled");
        }
    });

    //Load person list from server
    $('#PermissonsTable').jtable('load');
});
