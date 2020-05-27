function setAwardValue () {
    var name = $('#ActivityType')[0][$('#ActivityType')[0].selectedIndex].innerHTML;
    $.get("/Activities/MaxAwardValue", { BadgeName: name }, function (data) {
        var obj = JSON.parse(data);
        $('#AwardAmount').val(obj.minval);
        if (obj.maxval) {
            $('#AwardSection').show();
        }
        else {
            $('#AwardSection').hide();
        }
    });
}
$('#ActivityType').change(setAwardValue);
$('#ActivityType').ready(setAwardValue);
