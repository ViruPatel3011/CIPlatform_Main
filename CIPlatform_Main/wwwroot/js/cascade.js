$(document).ready(function () {
    function GetCountry() {
        $.ajax({
            url: '/Home/Country',
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#Country').append('<Option value=' + data.country_id + '>' + data.name + '</Option>');
                })
            }
        })
    }
    GetCountry();
    $('#Country').change(function () {
 
        var id = $(this).find(":selected").text();
        /*alert(id);*/
        console.log(id);
        $('#City').empty();
        $('#City').append('<Option>City </Option>');
        $.ajax({
            url: '/Home/City?id=' + id,
            success: function (result) {
                $.each(result, function (i, data) {
                    $("#City").append('<Option value=' + data.city_id + '>' + data.name + '</Option>')
                });
            }
        });
    });
})


