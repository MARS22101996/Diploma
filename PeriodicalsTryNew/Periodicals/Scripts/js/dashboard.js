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
        startDate: "[#start-date-input]"
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
                        startDateValue: $("#start-date-input").val()
                    };

                    $.ajax({
                            url: sendModel.url,
                            type: "get",
                            data: {
                                year: sendModel.userNameValue,
                                month: sendModel.startDateValue
                            }
                        })
                        .done(function(jsonString) {
                            jsonData = JSON.parse(jsonString);

                            if (jsonData == null) {
                                cleanStatistic();
                            } else {
                                showPrices(jsonData);
                                showColors(jsonData);
                                showTable(jsonData);
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
    }

    function showTable(jsonData) {
        var statisticTableTemplateHtml = $("[data-is-table-statistic]").html();
        var statisticTableContainer = $("[data-is-statistic-container]");

        var mustacheToHtml = Mustache.to_html(statisticTableTemplateHtml, jsonData);

        statisticTableContainer.html(mustacheToHtml);
    }

    function showColors(jsonData) {
        var data;
        var chart;
        var chartDiv;

        var options = {
            "title": "Отчет по видам",
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
            "title": "Отчет по ценам",
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