(function($, google) {
    if (google) {
        google.load("visualization",
            "1.0",
            {
                packages: ["corechart"],
                callback: function() {
                    getStatistic();
                }
            });
    }

    var selectors = {
        userName: "[data-name-input]",
        startDate: "[data-date-input]"
    };

    function getStatistic() {
        $("[data-statistic]")
            .on("click",
                function(event) {
                    event.preventDefault();
                    var jsonData;

                    var sendModel = {
                        url: $(this).attr("data-url"),
                        userNameValue: $(selectors.userName).val(),
                        startDateValue: $(selectors.startDate).val()
                    };

                    $.ajax({
                            url: sendModel.url,
                            type: "get",
                            data: {
                                userName: sendModel.userNameValue,
                                startDate: sendModel.startDateValue
                            }
                        })
                        .done(function(jsonString) {
                            jsonData = JSON.parse(jsonString);

                            if (jsonData == null) {
                                cleanStatistic();
                            } else {
                                showTicketDates(jsonData);
                                showPrices(jsonData);
                                showColors(jsonData);
                                //showTable(jsonData);
                            }
                        })
                        .fail(function(error) { console.log(error) });
                });
    }

    function cleanStatistic() {
        $("[data-chartDate], " +
                "[data-chartStatus], " +
                "[data-chartPriority]")
            .html("");

        $("[data-is-statistic-container]").html("<h2>No data</h2>");
            //.html($("[data-NotFound]").html());
    }

    function showTable(jsonData) {
        var statisticTableTemplateHtml = $("[data-is-table-statistic]").html();
        var statisticTableContainer = $("[data-is-statistic-container]");

        var mustacheToHtml = Mustache.to_html(statisticTableTemplateHtml, jsonData);

        //var mustasheToHtml = (function (statisticTableTemplateHtml, jsonData) {
        //    Mustache.to_html(statisticTableTemplateHtml, jsonData);
        //})(statisticTableTemplateHtml, jsonData);

        statisticTableContainer.html(mustacheToHtml);
    }

    function showTicketDates(jsonData) {
        var data;
        var chart;
        var chartDiv;

        var options = {
            "title": "Report by date",
            "vAxis": { "title": "Count" },
            "hAxis": { "title": "Date" },
            "legend": "none",
            "colors": ["#ff4000"]
        };

        data = new google.visualization.DataTable();
        data.addColumn("string", "Date");
        data.addColumn("number", "Count");
        setData(data, jsonData.DateCountDictionary);
        chartDiv = $("[data-chartDate]")[0];
        chart = new google.visualization.AreaChart(chartDiv);
        chart.draw(data, options);
    }

    function showColors(jsonData) {
        var data;
        var chart;
        var chartDiv;

        var options = {
            "title": "Report by status",
            "height": 400,
            "pieHole": 0.4
        };

        data = new google.visualization.DataTable();
        data.addColumn("string", "Status");
        data.addColumn("number", "Count");
        setData(data, jsonData.ColorCountDictionary);
        chartDiv = $("[data-chartStatus]")[0];
        chart = new google.visualization.PieChart(chartDiv);
        chart.draw(data, options);
    }

    function showPrices(jsonData) {
        var data;
        var chart;
        var chartDiv;

        var options = {
            "title": "Report by priority",
            "height": 400
        };

        data = new google.visualization.DataTable();
        data.addColumn("string", "Priority");
        data.addColumn("number", "Count");
        setData(data, jsonData.PriceCountDictionary);
        chartDiv = $("[data-chartPriority]")[0];
        chart = new google.visualization.PieChart(chartDiv);
        chart.draw(data, options);
    }

    function setData(data, dictionary) {
        for (var propertyName in dictionary) {
            data.addRow(
                [propertyName, dictionary[propertyName]]
            );
        }
    }
})(jQuery, google);