(function($) {

    $(function() {
        $("[data-autocomplete-source]")
            .each(function() {
                var target = $(this);
                target.autocomplete({ source: target.attr("data-autocomplete-source") });
            });

        $("#start-date-input")
            .datepicker({
                dateFormat: "yy-mm-dd",
                //minDate: "-30D",
                maxDate: "-3D"
            });

        $("[data-datepicker]")
            .datepicker({
                dateFormat: "yy-mm-dd"
            });
    });

})(jQuery);