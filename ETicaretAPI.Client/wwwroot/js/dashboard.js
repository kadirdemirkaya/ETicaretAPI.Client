"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/productHub").build();

connection.on("receiveProduct", function (productsForGraph) {
    var formattedData = formatCategoryProductData(productsForGraph);
    BindProductsToGraph(formattedData);
});

function formatCategoryProductData(productsForGraph) {
    var formattedData = [];

    $.each(productsForGraph, function (index, item) {
        var formattedItem = {
            categoryName: item.category,
            totalProduct: item.products
        };
        formattedData.push(formattedItem);
    });

    return formattedData;
}

function BindProductsToGraph(productsForGraph) {
    var labels = [];
    var data = [];

    $.each(productsForGraph, function (index, item) {
        labels.push(item.categoryName);
        data.push(item.totalProduct);
    });

    DestroyCanvasIfExists('canvasProudcts');

    const context = $('#canvasProudcts');
    const myChart = new Chart(context, {
        type: 'doughnut',
        data: {
            labels: labels,
            datasets: [{
                label: '# of Products',
                data: data,
                backgroundColor: backgroundColors,
                borderColor: borderColors,
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}
