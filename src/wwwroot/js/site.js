;function drawMinMaxChartInternal (period, chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    var ctx = document.getElementById(chartName).getContext('2d');
    var data = {
        labels: labels,
        datasets: [{
            label: "Max",
            borderWidth: 5,
            data: maxYs,
            fill: false,
            tension: 0,
            borderColor: 'rgba(233, 241, 221, 1)',
            backgroundColor: maxYsColors,
        }, {
            label: "Mittel",
            borderWidth: 5,
            data: averageYs,
            fill: false,
            tension: 0,
            borderColor: 'rgba(140, 173, 88, 1)',
            backgroundColor: averageYsColor,
        }, {
            label: "Min",
            borderWidth: 5,
            data: minYs,
            fill: false,
            tension: 0,
            borderColor: 'rgba(132, 133, 135, 1)',
            backgroundColor: minYsColor,
        }]
    };
    var xAxes = [{
        type: 'time',
        time: {
            parser: 'YYYY-MM-DD HH:mm:ss',
            unit: period
        },
        ticks: {
            min: 0,
            beginAtZero: false
        },
        gridLines: {
            display: false
        }
    }];
    if(period === 'week'){
        xAxes = [{
            gridLines: {
                display: false
            }
        }];
    }
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
            xAxes: xAxes
        }
    };

    new Chart(ctx, {
        options: options,
        data: data,
        type: 'line'
    });
};
;function drawMinMaxChart (chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    drawMinMaxChartInternal('day', chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
};
;function drawMinMaxChartDay (chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    drawMinMaxChartInternal('day', chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
};
;function drawMinMaxChartWeek (chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    drawMinMaxChartInternal('week', chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}
;function drawMinMaxChartMonth (chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    drawMinMaxChartInternal('month', chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}
;function drawMinMaxChartYear (chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    drawMinMaxChartInternal('year', chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}

;function drawStartEndChartInternal (period, chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    var ctx = document.getElementById(chartName).getContext('2d');
    var data = {
        labels: labels,
        datasets: [{
            label: "Start",
            borderWidth: 5,
            data: startYs,
            fill: false,
            tension: 0,
            borderColor: startYsColors[0],
            backgroundColor: startYsColors,
        }, {
            label: "Delta",
            borderWidth: 5,
            data: deltaYs,
            fill: false,
            tension: 0,
            borderColor: 'rgba(140, 173, 88, 1)',
            backgroundColor: deltaYsColor,
        }, {
            label: "End",
            borderWidth: 5,
            data: endYs,
            fill: false,
            tension: 0,
            borderColor: 'rgba(132, 133, 135, 1)',
            backgroundColor: endYsColor,
        }]
    };
    var xAxes = [{
        type: 'time',
        time: {
            parser: 'YYYY-MM-DD HH:mm:ss',
            unit: period
        },
        ticks: {
            min: 0,
            beginAtZero: false
        },
        gridLines: {
            display: false
        }
    }];
    if(period === 'week'){
        xAxes = [{
            gridLines: {
              display: false
            }
        }];
    }
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
                    color: "rgba(176, 177, 177, 1)"
                }
            }],
            xAxes: xAxes
        }
    };

    new Chart(ctx, {
        options: options,
        data: data,
        type: 'line'
    });
};

;function drawStartEndChart (chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    drawStartEndChartInternal('day', chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}
;function drawStartEndChartDay (chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    drawStartEndChartInternal('day', chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}
;function drawStartEndChartWeek (chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    drawStartEndChartInternal('week', chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}
;function drawStartEndChartMonth (chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    drawStartEndChartInternal('month', chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}
;function drawStartEndChartYear (chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    drawStartEndChartInternal('year', chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}

function drawBarChartInternal(period, chartName, chartTitle, xAxisLabel, Ys, YsColors) {
    var ctx = document.getElementById(chartName).getContext('2d');
    var data = 
    {
        labels: xAxisLabel,
        datasets: [{
            backgroundColor: YsColors,
            data: Ys
        }]
    };

    var xAxes = [{
        type: 'time',
        time: {
            parser: 'YYYY-MM-DD HH:mm:ss',
            unit: period
        },
        ticks: {
            min: 0,
            beginAtZero: false
        },
        gridLines: {
            display: false
        }
    }];
    if(period === 'week'){
        xAxes = [{
            gridLines: {
              display: false
            }
        }];
    }
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
                    color: "rgba(176, 177, 177, 1)"
                }
            }],
            xAxes: xAxes
        }
    };

    new Chart(ctx, {
        options: options,
        data: data,
        type: 'bar'
    });
};
function drawBarChartDay(chartName, chartTitle, xAxisLabel, Ys, YsColors) {
    drawBarChartInternal('day',chartName, chartTitle, xAxisLabel, Ys, YsColors);
}
function drawBarChartWeek(chartName, chartTitle, xAxisLabel, Ys, YsColors) {
    drawBarChartInternal('week', chartName, chartTitle, xAxisLabel, Ys, YsColors);
}
function drawBarChartMonth(chartName, chartTitle, xAxisLabel, Ys, YsColors) {
    drawBarChartInternal('month', chartName, chartTitle, xAxisLabel, Ys, YsColors);
}
function drawBarChartYear(chartName, chartTitle, xAxisLabel, Ys, YsColors) {
    drawBarChartInternal('year', chartName, chartTitle, xAxisLabel, Ys, YsColors);
}