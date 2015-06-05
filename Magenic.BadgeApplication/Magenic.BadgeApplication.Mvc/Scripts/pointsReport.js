
function PointsReport() {
    $.ajax({
        url: UrlSettings.ApproveActivitiesUrl,
        data: {
            displayAll: $('#displayAll').prop('checked')
            //    id: id
        },
        contentType: "application/json; charset=utf-8",
        //data will be your return content (the rendered html from your Partial View)
        success: function (data) {
            //Outputs your data into a specific <div> element (This can be your original TreeView container)
            $('#displayThreshold').html(data);
        },
        failure: function (data) {
            alert($('#displayAll').val());
        }

    });

}