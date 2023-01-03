const LINE_CHART_COLORS = [
    'rgba(233, 241, 221, 1)',
    'rgba(140, 173, 88, 1)',
    'rgba(132, 133, 135, 1)'
];

const X_AXIS_COLOR = "rgba(176, 177, 177, 0.5)";

function createDataSet(label, data, borderColor, backgroundColor, fill, tension, borderWidth) {
    return {
        label,
        data,
        borderColor,
        backgroundColor,
        fill,
        tension,
        borderWidth
    };
}

function createDataSets(labels, data, borderColors, backgroundColors, fill, tension, borderWidth) {
    return labels.map((label, i) =>
        createDataSet(
            label,
            data[i],
            borderColors[i],
            backgroundColors[i],
            fill,
            tension,
            borderWidth
        )
    );
}

function createChart(ctx, config) {
    return new Chart(ctx, config);
}

function drawChartBase(unit, chartName, chartTitle, rawDates, datasets, type) {
    if (typeof document.getElementById(chartName) === "undefined") {
        return;
    }
    var canvas = document.getElementById(chartName);
    var ctx = canvas.getContext('2d');
    var labels;
    if (unit === 'total') {
        labels = rawDates;
    } else {
        labels = rawDates.map(date => moment(date).toDate());
    }
    var x = {
        offset: true,
        type: 'time',
        time: {
            parser: 'YYYY-MM-DD HH:mm:ss',
            unit: unit,
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
    if (unit === 'total') {
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
                    color: X_AXIS_COLOR
                }
            }
        }
    };
    var config = {
        type: type,
        options: options,
        data: {
            labels: labels,
            datasets: datasets
        }
    };
    return createChart(ctx, config);
}

function drawMaxAvgMinChart(unit, chartName, chartTitle, rawDates, maxYs, maxYsColors, averageYs, averageYsColor, minYs, minYsColor) {
    var datasets = createDataSets(["Max", "Mittel", "Min"], [maxYs, averageYs, minYs], [maxYsColors, averageYsColor, minYsColor], LINE_CHART_COLORS, false, 0, 5);
    return drawChartBase(unit, chartName, chartTitle, rawDates, datasets, 'line');
}

function drawStartDeltaEndChart(unit, chartName, chartTitle, rawDates, startYs, startYsColors, deltaYs, deltaYsColor, endYs, endYsColor) {
    var datasets = createDataSets(["Start", "Delta", "End"], [startYs, deltaYs, endYs], [startYsColors, deltaYsColor, endYsColor], LINE_CHART_COLORS, false, 0, 5);
    return drawChartBase(unit, chartName, chartTitle, rawDates, datasets, 'line');
}

function drawBarChart(unit, chartName, chartTitle, rawDates, Ys, YsColors) {
    var datasets = createDataSets(["Mittel"], [Ys], [YsColors], [LINE_CHART_COLORS[1]], false, 0, 0);
    return drawChartBase(unit, chartName, chartTitle, rawDates, datasets, 'bar');
}

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