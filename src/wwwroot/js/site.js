function getDataSets(labels, data, backgroundColors, borderColors, fill, tension, borderWidth) {
    return labels.map((label, i) => ({
        label,
        data: data[i],
        borderColor: borderColors[i],
        backgroundColor: backgroundColors[i],
        fill,
        tension: tension,
        borderWidth: borderWidth
    }));
}

function drawChartBase(period, chartName, chartTitle, rawDates, datasets, type) {
    var ctx = document.getElementById(chartName).getContext('2d');
    var labels = rawDates.map(date => moment(date).toDate());
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
        type: type,
        options: options,
        data: {
            labels: labels,
            datasets: datasets
        }        
    }).update();
}

function drawMinMaxChartInternal(period, chartName, chartTitle, rawDates, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    var datasets = getDataSets(["Max","Mittel","Min"],[maxYs,averageYs,minYs],[maxYsColors,averageYsColor,minYsColor],['rgba(233, 241, 221, 1)','rgba(140, 173, 88, 1)','rgba(132, 133, 135, 1)'],false,0,5);
    drawChartBase(period, chartName, chartTitle, rawDates, datasets, 'line');
}
function drawMinMaxChart(chartName, chartTitle, rawDates, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    drawMinMaxChartInternal('day', chartName, chartTitle, rawDates, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}
function drawMinMaxChartDay(chartName, chartTitle, rawDates, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    drawMinMaxChartInternal('day', chartName, chartTitle, rawDates, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}
function drawMinMaxChartWeek(chartName, chartTitle, rawDates, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    drawMinMaxChartInternal('week', chartName, chartTitle, rawDates, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}
function drawMinMaxChartMonth(chartName, chartTitle, rawDates, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    drawMinMaxChartInternal('month', chartName, chartTitle, rawDates, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}
 function drawMinMaxChartYear(chartName, chartTitle, rawDates, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    drawMinMaxChartInternal('year', chartName, chartTitle, rawDates, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor);
}

function drawStartEndChartInternal(period, chartName, chartTitle, rawDates, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    var datasets = getDataSets(["Start","Delta","End"],[startYs,deltaYs,endYs],[startYsColors,deltaYsColor,endYsColor],['rgba(233, 241, 221, 1)','rgba(140, 173, 88, 1)','rgba(132, 133, 135, 1)'],false,0,5);
    drawChartBase(period, chartName, chartTitle, rawDates, datasets, 'line');
}
function drawStartEndChart(chartName, chartTitle, rawDates, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    drawStartEndChartInternal('day', chartName, chartTitle, rawDates, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}
 function drawStartEndChartDay(chartName, chartTitle, rawDates, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    drawStartEndChartInternal('day', chartName, chartTitle, rawDates, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}
 function drawStartEndChartWeek(chartName, chartTitle, rawDates, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    drawStartEndChartInternal('week', chartName, chartTitle, rawDates, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}
 function drawStartEndChartMonth(chartName, chartTitle, rawDates, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    drawStartEndChartInternal('month', chartName, chartTitle, rawDates, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}
 function drawStartEndChartYear(chartName, chartTitle, rawDates, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    drawStartEndChartInternal('year', chartName, chartTitle, rawDates, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor);
}

function drawBarChartInternal(period, chartName, chartTitle, rawDates, Ys, YsColors) {
    var datasets = getDataSets(["Mittel"],[Ys],[YsColors],['rgba(140, 173, 88, 1)'],false,0,0);
    drawChartBase(period, chartName, chartTitle, rawDates, datasets, 'bar');
}
function drawBarChartDay(chartName, chartTitle, rawDates, Ys, YsColors) {
    drawBarChartInternal('day', chartName, chartTitle, rawDates, Ys, YsColors);
}
function drawBarChartWeek(chartName, chartTitle, rawDates, Ys, YsColors) {
    drawBarChartInternal('week', chartName, chartTitle, rawDates, Ys, YsColors);
}
function drawBarChartMonth(chartName, chartTitle, rawDates, Ys, YsColors) {
    drawBarChartInternal('month', chartName, chartTitle, rawDates, Ys, YsColors);
}
function drawBarChartYear(chartName, chartTitle, rawDates, Ys, YsColors) {
    drawBarChartInternal('year', chartName, chartTitle, rawDates, Ys, YsColors);
}
function drawBarChartTotal(chartName, chartTitle, rawDates, Ys, YsColors) {
    drawBarChartInternal('total', chartName, chartTitle, rawDates, Ys, YsColors);
}