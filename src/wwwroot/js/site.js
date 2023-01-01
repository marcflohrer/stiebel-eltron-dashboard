function getDataSets(labels, data, backgroundColors, borderColors, fill, tension, borderWith) {
    return labels.map((label, i) => ({
        label,
        data: data[i],
        borderColor: borderColors[i],
        backgroundColor: backgroundColors[i],
        fill,
        tension: tension,
        borderWidth: borderWith
    }));
}

function drawMinMaxChartInternal(period, chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    var ctx = document.getElementById(chartName).getContext('2d');
    var dataSets = getDataSets(["Max","Mittel","Min"],[maxYs,averageYs,minYs],[maxYsColors,averageYsColor,minYsColor],['rgba(233, 241, 221, 1)','rgba(140, 173, 88, 1)','rgba(132, 133, 135, 1)'],false,0,5);
    var x = {
        offset: true,
        type: 'time',
        time: {
            parser: 'YYYY-MM-DD HH:mm:ss',
            unit: period,
            isoWeekday: true,
            displayFormats: {
                year: 'YYYY',
                month: 'MMM YY',
                week: 'K[W] W',
                day: 'MMM D'
            }
        },
        display: true,
        grid: {
            display: false
        },
        ticks: {
            major: {
                enabled: false
            }
        }
    };
    var options = {
        maintainAspectRatio: false,
        plugins: {
            title: {
                display: true,
                text: chartTitle
            },
            legend: {
                display: true
            }
        },
        scales: {
            x: x,
            y: {
                display: true,
                title: {
                    display: true
                },
                grid: {
                    display: true,
                    color: "rgba(176, 177, 177, 0.5)"
                }
            }
        }
    };

    new Chart(ctx, {
        options: options,
        data: {
            labels: labels,
            datasets: dataSets
        },
        type: 'line'
    }).update();
}
function drawMinMaxChart(chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    drawMinMaxChartInternal('day', chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}
function drawMinMaxChartDay(chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    drawMinMaxChartInternal('day', chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}
function drawMinMaxChartWeek(chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    drawMinMaxChartInternal('week', chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}
function drawMinMaxChartMonth(chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    drawMinMaxChartInternal('month', chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}
 function drawMinMaxChartYear(chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    drawMinMaxChartInternal('year', chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}

function drawStartEndChartInternal(period, chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    var ctx = document.getElementById(chartName).getContext('2d');
    var dataSets = getDataSets(["Start","Delta","End"],[startYs,deltaYs,endYs],[startYsColors,deltaYsColor,endYsColor],['rgba(233, 241, 221, 1)','rgba(140, 173, 88, 1)','rgba(132, 133, 135, 1)'],false,0,5);
    var data = {
        labels: labels,
        datasets: dataSets
    };
    var x = {
        offset: true,
        type: 'time',
        time: {
            parser: 'YYYY-MM-DD HH:mm:ss',
            unit: period,
            isoWeekday: true,
            displayFormats: {
                year: 'YYYY',
                month: 'MMM YY',
                week: 'K[W] W',
                day: 'MMM D'
            }
        },
        display: true,
        grid: {
            display: false
        },
        ticks: {
            major: {
                enabled: false
            }
        }
    };
    var options = {
        maintainAspectRatio: false,
        plugins: {
            title: {
                display: true,
                text: chartTitle
            },
            legend: {
                display: true
            }
        },
        scales: {
            x: x,
            y: {
                display: true,
                title: {
                    display: false
                },
                grid: {
                    display: true,
                    color: "rgba(176, 177, 177, 0.5)"
                }
            }
        }
    };

    new Chart(ctx, {
        options: options,
        data: data,
        type: 'line'
    }).update();
}
function drawStartEndChart(chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    drawStartEndChartInternal('day', chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}
 function drawStartEndChartDay(chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    drawStartEndChartInternal('day', chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}
 function drawStartEndChartWeek(chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    drawStartEndChartInternal('week', chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}
 function drawStartEndChartMonth(chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    drawStartEndChartInternal('month', chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}
 function drawStartEndChartYear(chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    drawStartEndChartInternal('year', chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}

function drawBarChartInternal(period, chartName, chartTitle, xAxisLabel, Ys, YsColors) {
    var ctx = document.getElementById(chartName).getContext('2d');
    var dataSets = getDataSets(["Mittel"],[Ys],[YsColors],['rgba(140, 173, 88, 1)'],false,0,0);
    var data = {
        labels: xAxisLabel,
        datasets: dataSets
    };
    var x = {
        offset: true,
        type: 'time',
        time: {
            parser: 'YYYY-MM-DD HH:mm:ss',
            unit: period,
            isoWeekday: true,
            displayFormats: {
                year: 'YYYY',
                month: 'MMM YY',
                week: 'K[W] W',
                day: 'MMM D'
            }
        },
        display: true,
        grid: {
            display: false
        },
        ticks: {
            major: {
                enabled: false
            }
        }
    };
    if (period === 'total') {
        x = {
            offset: true,
            grid: {
                display: false
            },
            display: true,
            ticks: {
                major: {
                    enabled: false
                }
            }
        };
    }
    var options = {
        maintainAspectRatio: false,
        plugins: {
            title: {
                display: true,
                text: chartTitle
            },
            legend: {
                display: false
            }
        },
        scales: {
            x: x,
            y: {
                display: true,
                title: {
                    display: true
                },
                grid: {
                    display: true,
                    color: "rgba(176, 177, 177, 0.5)"
                }
            }
        },
        legend: {
            display: false
        }
    };

    new Chart(ctx, {
        options: options,
        data: data,
        type: 'bar'
    }).update();
}
function drawBarChartDay(chartName, chartTitle, xAxisLabel, Ys, YsColors) {
    drawBarChartInternal('day', chartName, chartTitle, xAxisLabel, Ys, YsColors);
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
function drawBarChartTotal(chartName, chartTitle, xAxisLabel, Ys, YsColors) {
    drawBarChartInternal('total', chartName, chartTitle, xAxisLabel, Ys, YsColors);
}