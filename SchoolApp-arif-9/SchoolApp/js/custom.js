$(document).ready(function () {
    $(".editbtn").bind("click", function () {
        var val = $(this).val();
        //alert(val);
        var mydata = { teacherid: val };
       // $('#myModal').on('shown.bs.modal', function() {
            $.ajax({
                type: "POST",
                url: '@Url.Action("GetTheDetails")',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(mydata),
                dataType: "json",
                success: function (data) {
                    if (data != false) {
                        // alert(data.email);
                        $(data).each(function (key, value) {
                            $("#st").append(value.email);
                        });
                    } else {
                        //$("#srcid").append('<div class="alert alert-info" style="margin-top:10px">Not Found!</div>');
                    }

                },
            });
            return false;
        //});
        
    });
});