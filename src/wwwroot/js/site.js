function drawChart (chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    var ctx = document.getElementById(chartName).getContext('2d');
    var data = {
        labels: labels,
        datasets: [{
            label: "Max",
            borderWidth: 5,
            data: maxYs,
            fill: false,
            borderColor: 'rgba(233, 241, 221, 1)',
            backgroundColor: maxYsColors,
        }, {
            label: "Mittel",
            borderWidth: 5,
            data: minYs,
            fill: false,
            borderColor: 'rgba(140, 173, 88, 1)',
            backgroundColor: averageYsColor,
        }, {
            label: "Min",
            borderWidth: 5,
            data: averageYs,
            fill: false,
            borderColor: 'rgba(132, 133, 135, 1)',
            backgroundColor: minYsColor,
        }]
    };

    var options = {
        title: {
            display: true,
            text: chartTitle
        },
        maintainAspectRatio: false,
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: false
                },
                gridLines: {
                    display: true,
                    color: "rgba(rgba(176, 177, 177, 1))"
                }
            }],
            xAxes: [{
                type: 'time',
                time: {
                    parser: 'YYYY-MM-DD HH:mm:ss',
                    unit: 'day'
                },
                ticks: {
                    min: 0,
                    beginAtZero: false
                },
                gridLines: {
                    display: false
                }
            }]
        }
    };

    new Chart(ctx, {
        options: options,
        data: data,
        type: 'line'
    });
};


function drawBarChart(chartName, xAxisLabel, labels, Ys, YsColors) {
    var ctx = document.getElementById(chartName).getContext('2d');
    var data = {
        labels: labels,
        datasets: [{
            label: xAxisLabel,
            borderWidth: 5,
            data: Ys,
            fill: false,
            borderColor: 'rgba(140, 173, 88, 1)',
            backgroundColor: YsColors,
        }]
    };

    var options = {
        maintainAspectRatio: false,
        scales: {
            yAxes: [{
                ticks: {
                    min: 0,
                    beginAtZero: false
                },
                gridLines: {
                    display: true,
                    color: "rgba(rgba(171, 177, 177, 1))"
                }
            }],
            xAxes: [{
                type: 'time',
                time: {
                    parser: 'YYYY-MM-DD HH:mm:ss',
                    unit: 'day'
                },
                ticks: {
                    min: 0,
                    beginAtZero: false
                },
                gridLines: {
                    display: false
                }
            }]
        }
    };

    var myChart = new Chart(ctx, {
        options: options,
        data: data,
        type: 'bar'
    });
};