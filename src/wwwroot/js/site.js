Date.prototype.getWeek = function () {
    // Copy date so don't modify original
    var d = new Date(Date.UTC(this.getFullYear(), this.getMonth(), this.getDate()));
    // Set to nearest Thursday: current date + 4 - current day number
    // Make Sunday's day number 7
    d.setUTCDate(d.getUTCDate() + 4 - (d.getUTCDay() || 7));
    // Get first day of year
    var yearStart = new Date(Date.UTC(d.getUTCFullYear(), 0, 1));
    // Calculate full weeks to nearest Thursday
    return Math.ceil((((d - yearStart) / 86400000) + 1) / 7);
}

function recreateCanvas(chartName) {
    document.getElementById(chartName).remove();
    let canvas = document.createElement('canvas');
    canvas.setAttribute('id', chartName);
    canvas.setAttribute('class', 'canvas-style');
    document.getElementById(chartName + "Chart").appendChild(canvas);
}

function getXOptions(period) {
    const commonOptions = {
        offset: true,
        type: 'time',
        time: {
            parser: 'YYYY-MM-DD HH:mm:ss',
            unit: period,
            isoWeekday: true,
            displayFormats: {
                month: 'YYYY-MM'
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
    if (period === 'week') {
        return {
            ...commonOptions,
            ticks: {
                ...commonOptions.ticks,
                callback: function (value) {
                    return "KW " + new Date(value).getWeek();
                }
            }
        };
    }
    if (period === 'total') {
        return {
            grid: {
                display: false
            },
            offset: true,
            display: true,
            ticks: {
                major: {
                    enabled: false
                }
            }
        };
    }
    return commonOptions;
}

function getYOptions(displayTitle) {

    var y = {
        display: true,
        title: {
            display: displayTitle
        },
        grid: {
            display: true,
            color: "rgba(176, 177, 177, 0.5)"
        }
    };
    return y;
};

function getPluginsOptions(displayLegend, chartTitle) {

    var plugins = {
        title: {
            display: true,
            text: chartTitle
        },
        legend: {
            display: displayLegend
        }
    };
    return plugins;
};

function getOptions(displayLegend, period, displayTitle, displayLegend, chartTitle) {
    var x = getXOptions(period, 'YYYY-MM');
    var y = getYOptions(displayTitle);
    var plugins = getPluginsOptions(displayLegend, chartTitle);
    var options = {
        maintainAspectRatio: false,
        plugins: plugins,
        scales: {
            x: x,
            y: y
        },
        legend: {
            display: displayLegend
        }
    }
    return options;
};

function getChart(ctx, displayLegend, period, displayTitle, displayLegend, chartTitle, data, chartType) {
    return new Chart(ctx, {
        options: getOptions(displayLegend, period, displayTitle, displayLegend, chartTitle),
        data: data,
        type: chartType
    });
};

function drawMinMaxChartInternal(period, chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
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
    return getChart(ctx, true, period, true, true, chartTitle, data, 'line');
};

; function drawMinMaxChart(chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    return drawMinMaxChartInternal('day', chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
};
; function drawMinMaxChartDay(chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    return drawMinMaxChartInternal('day', chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
};
; function drawMinMaxChartWeek(chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    return drawMinMaxChartInternal('week', chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}
; function drawMinMaxChartMonth(chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    return drawMinMaxChartInternal('month', chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}
; function drawMinMaxChartYear(chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    return drawMinMaxChartInternal('year', chartName, chartTitle, labels, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}

; function drawStartEndChartInternal(period, chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
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
    return getChart(ctx, true, period, false, true, chartTitle, data, 'line');
};

function drawStartEndChart(chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    return drawStartEndChartInternal('day', chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
};

function drawStartEndChartDay(chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    return drawStartEndChartInternal('day', chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
};

function drawStartEndChartWeek(chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    return drawStartEndChartInternal('week', chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
};

function drawStartEndChartMonth(chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    return drawStartEndChartInternal('month', chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}

function drawStartEndChartYear(chartName, chartTitle, labels, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
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
    return getChart(ctx, false, period, true, false, chartTitle, data, 'bar');
};

function drawBarChartDay(chartName, chartTitle, xAxisLabel, Ys, YsColors) {
    return drawBarChartInternal('day', chartName, chartTitle, xAxisLabel, Ys, YsColors);
};

function drawBarChartWeek(chartName, chartTitle, xAxisLabel, Ys, YsColors) {
    return drawBarChartInternal('week', chartName, chartTitle, xAxisLabel, Ys, YsColors);
};

function drawBarChartMonth(chartName, chartTitle, xAxisLabel, Ys, YsColors) {
    return drawBarChartInternal('month', chartName, chartTitle, xAxisLabel, Ys, YsColors);
};

function drawBarChartYear(chartName, chartTitle, xAxisLabel, Ys, YsColors) {
    return drawBarChartInternal('year', chartName, chartTitle, xAxisLabel, Ys, YsColors);
};

function drawBarChartTotal(chartName, chartTitle, xAxisLabel, Ys, YsColors) {
    return drawBarChartInternal('total', chartName, chartTitle, xAxisLabel, Ys, YsColors);
};