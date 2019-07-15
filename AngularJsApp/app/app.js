var DemoApp = angular.module('DemoApp', ['dx']);

DemoApp.controller('DemoController', function DemoController($scope) {
    var collapsed = false;
    $scope.dataGridOptions = {
        dataSource: {
            store: {
                type: "odata",
                url: "https://stockmanagementwebapi.azurewebsites.net/api/requestheader",
                beforeSend: function (request) {
                    request.params.category = 1;
                }
            }
        },
        paging: {
            pageSize: 10
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [10, 25, 50, 100]
        },
        remoteOperations: false,
        searchPanel: {
            visible: true,
            highlightCaseSensitive: true
        },
        groupPanel: { visible: true },
        grouping: {
            autoExpandAll: false
        },
        allowColumnReordering: true,
        rowAlternationEnabled: true,
        showBorders: true,
        columns: [
            {
                dataField: "RequestHeaderNumber",
                dataType: "string"
            }, 
            {
                dataField: "PoNumber",
                dataType: "string"
            },

        ],
        onContentReady: function (e) {
            if (!collapsed) {
                collapsed = true;
            }
        }
    };

    $scope.customizeTooltip = function (pointsInfo) {
        return { text: parseInt(pointsInfo.originalValue) + "%" };
    };
});
