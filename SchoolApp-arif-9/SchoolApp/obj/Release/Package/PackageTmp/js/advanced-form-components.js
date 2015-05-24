//date picker start

    if (top.location != location) {
        top.location.href = document.location.href ;
    }
    $(function(){
        window.prettyPrint && prettyPrint();
        $('.default-date-picker').datepicker({
            format: 'mm/dd/yyyy'
        });
        $('.dpYears').datepicker();
        $('.dpMonths').datepicker();
    });