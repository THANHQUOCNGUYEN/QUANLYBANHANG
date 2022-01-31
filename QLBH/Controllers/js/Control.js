$(function () {
    $("#txttest").datepicker({
        dateFormat: "dd/mm/yy"
    });
});

$(function () {
    $("#search").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/SanPhams/getname/",
                data: "{'txttim':'" + request.term + "'}",
                datatype: "json",
                type: "POST",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
            });
        },
        minLength: 1
    });
});

setTimeout(function () {
    $('#msgAlert').fadeOut('slow');
}, 2000);
