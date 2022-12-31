function recreateCanvas(chartName) {
    document.getElementById(chartName).remove();
    let canvas = document.createElement('canvas');
    canvas.setAttribute('id', chartName);
    canvas.setAttribute('class', 'canvas-style');
    document.getElementById(chartName + "Chart").appendChild(canvas);
}

; function drawMinMaxChartInternal(period, chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    recreateCanvas(chartName)

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
    var x = {
        offset: true,
        type: 'time',
        time: {
            parser: 'YYYY-MM-DD HH:mm:ss',
            unit: period,
            isoWeekday: true,
            displayFormats: {
                year: 'YYYY',
                month: 'YYYY-MM',
                week: 'K[W] W',
                day: 'MM-DD-YY'
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

    return new Chart(ctx, {
        options: options,
        data: data,
        type: 'line'
    });
};
; function drawMinMaxChart(chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    return drawMinMaxChartInternal('day', chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}
function drawMinMaxChartDay(chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    return drawMinMaxChartInternal('day', chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}
function drawMinMaxChartWeek(chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    return drawMinMaxChartInternal('week', chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}
function drawMinMaxChartMonth(chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    return drawMinMaxChartInternal('month', chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}
 function drawMinMaxChartYear(chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    return drawMinMaxChartInternal('year', chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}

function drawStartEndChartInternal(period, chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    recreateCanvas(chartName)

    var ctx = document.getElementById(chartName).getContext('2d');
    var data = {
        labels: labels,
        datasets: [{
            label: "Start",
            borderWidth: 5,
            data: startYs,
            fill: false,
            tension: 0,
            borderColor: 'rgba(233, 241, 221, 1)',
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
    var x = {
        offset: true,
        type: 'time',
        time: {
            parser: 'YYYY-MM-DD HH:mm:ss',
            unit: period,
            isoWeekday: true,
            displayFormats: {
                year: 'YYYY',
                month: 'YYYY-MM',
                week: 'W',
                day: 'MM-DD-YY'
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

    return new Chart(ctx, {
        options: options,
        data: data,
        type: 'line'
    });
};
; function drawStartEndChart(chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    return drawStartEndChartInternal('day', chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}
; function drawStartEndChartDay(chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    return drawStartEndChartInternal('day', chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}
; function drawStartEndChartWeek(chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    return drawStartEndChartInternal('week', chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}
; function drawStartEndChartMonth(chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    return drawStartEndChartInternal('month', chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}
; function drawStartEndChartYear(chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    return drawStartEndChartInternal('year', chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}

function drawBarChartInternal(period, chartName, chartTitle, xAxisLabel, Ys, YsColors) {
    recreateCanvas(chartName)

    var ctx = document.getElementById(chartName).getContext('2d');
    var data = {
        labels: xAxisLabel,
        datasets: [{
            label: "Mittel",
            backgroundColor: YsColors,
            data: Ys
        }]
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
                month: 'YYYY-MM',
                week: 'W',
                day: 'MM-DD-YY'
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

    return new Chart(ctx, {
        options: options,
        data: data,
        type: 'bar'
    });
};
function drawBarChartDay(chartName, chartTitle, xAxisLabel, Ys, YsColors) {
    return drawBarChartInternal('day', chartName, chartTitle, xAxisLabel, Ys, YsColors);
}
function drawBarChartWeek(chartName, chartTitle, xAxisLabel, Ys, YsColors) {
    return drawBarChartInternal('week', chartName, chartTitle, xAxisLabel, Ys, YsColors);
}
function drawBarChartMonth(chartName, chartTitle, xAxisLabel, Ys, YsColors) {
    return drawBarChartInternal('month', chartName, chartTitle, xAxisLabel, Ys, YsColors);
}
function drawBarChartYear(chartName, chartTitle, xAxisLabel, Ys, YsColors) {
    return drawBarChartInternal('year', chartName, chartTitle, xAxisLabel, Ys, YsColors);
}
function drawBarChartTotal(chartName, chartTitle, xAxisLabel, Ys, YsColors) {
    return drawBarChartInternal('total', chartName, chartTitle, xAxisLabel, Ys, YsColors);
}