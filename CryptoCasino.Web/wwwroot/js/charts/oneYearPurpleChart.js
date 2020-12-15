// General configuration for the charts with Line gradientStroke
function oneYearPurple(chartMonths, chartValueFirstWindow, chartValueSecondWindow, chartValueThirdWindow) {
    gradientChartOptionsConfiguration = {
        maintainAspectRatio: false,
        legend: {
            display: false
        },

        tooltips: {
            backgroundColor: '#fff',
            titleFontColor: '#333',
            bodyFontColor: '#666',
            bodySpacing: 4,
            xPadding: 12,
            mode: "nearest",
            intersect: 0,
            position: "nearest"
        },
        responsive: true,
        scales: {
            yAxes: [{
                barPercentage: 1.6,
                gridLines: {
                    drawBorder: false,
                    color: 'rgba(29,140,248,0.0)',
                    zeroLineColor: "transparent",
                },
                ticks: {
                    suggestedMin: 1,
                    suggestedMax: Math.max(...chartValueFirstWindow),
                    padding: 20,
                    fontColor: "#9a9a9a"
                }
            }],

            xAxes: [{
                barPercentage: 1.6,
                gridLines: {
                    drawBorder: false,
                    color: 'rgba(220,53,69,0.1)',
                    zeroLineColor: "transparent",
                },
                ticks: {
                    padding: 20,
                    fontColor: "#9a9a9a"
                }
            }]
        }
    };

    var chart_labels = chartMonths;
    var chart_data = chartValueFirstWindow;

    var ctx = document.getElementById("oneYearChart").getContext('2d');

    var gradientStroke = ctx.createLinearGradient(0, 230, 0, 50);

    gradientStroke.addColorStop(1, 'rgba(72,72,176,0.1)');
    gradientStroke.addColorStop(0.4, 'rgba(72,72,176,0.0)');
    gradientStroke.addColorStop(0, 'rgba(119,52,169,0)'); //purple colors
    var config = {
        type: 'line',

        data: {
            labels: chart_labels,

            datasets: [{
                label: "Total",
                fill: true,
                backgroundColor: gradientStroke,
                borderColor: '#d346b1',
                borderWidth: 2,
                borderDash: [],
                borderDashOffset: 0.0,
                pointBackgroundColor: '#d346b1',
                pointBorderColor: 'rgba(255,255,255,0)',
                pointHoverBackgroundColor: '#d346b1',
                pointBorderWidth: 20,
                pointHoverRadius: 4,
                pointHoverBorderWidth: 15,
                pointRadius: 4,
                data: chart_data,
            }]
        },
        options: gradientChartOptionsConfiguration
    };
    var myChartData = new Chart(ctx, config);
    //Start up diagram
    $("#0").click(function () {
        var data = myChartData.config.data;
        data.datasets[0].data = chart_data;
        data.labels = chart_labels;
        myChartData.update();
    });
    //Second window diagram
    $("#1").click(function () {
        var chart_data = chartValueSecondWindow;
        var data = myChartData.config.data;
        data.datasets[0].data = chart_data;
        data.labels = chart_labels;
        myChartData.update();
    });
    //Third window diagram
    $("#2").click(function () {
        var chart_data = chartValueThirdWindow;
        var data = myChartData.config.data;
        data.datasets[0].data = chart_data;
        data.labels = chart_labels;
        myChartData.update();
    });
}

