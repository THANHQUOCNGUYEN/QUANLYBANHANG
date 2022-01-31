var app = angular.module('appChart', ['chart.js']);
app.controller('revenueStasticController', revenueStasticController);
revenueStasticController.$inject = ['$scope'];
function revenueStasticController($scope) {
    $scope.labels = [];

    $scope.data = [];
    $scope.staticChart = staticChart;
    function staticChart() {
        $.ajax({
            url: '/Admin/staticRevenue',
            dataType: 'json',
            type: 'post',
            data: {
                fromdate: $('#fromdate').val(),
                todate: $('#todate').val()
            }
            ,
            success: function (res) {
                $scope.datachart = res;
                var chartdata = [];
                var labels = [];
                var benifits = [];
                $.each(res, function (i, item) {
                    labels.push(item.name);
                    chartdata.push(item.count);
                });
                benifits.push(chartdata);
                $scope.$apply(function () {
                    $scope.labels = labels;

                    $scope.data = benifits;
                });
            }
        });
    }

}