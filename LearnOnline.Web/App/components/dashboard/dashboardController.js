(function (app) {

    app.controller('dashboardController', dashboardController);

    dashboardController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService', '$filter', '$state']; // khởi tạo

    function dashboardController($scope, apiService, $ngBootbox, notificationService, $filter, $state) {
        $scope.loading = 'Đang tải dữ liệu...';
        $scope.loading2 = 'Đang tải dữ liệu...';
        $scope.requestLicenses = [];
        $scope.licenseInfo = [];
        $scope.totalcount = 0;
        $scope.getListRequestLicenseToday = getListRequestLicenseToday;      

        $scope.totalCountCustomer = totalCountCustomer;
        function totalCountCustomer() {
            var url = 'api/dashboard/totalcustomer';
            apiService.get(url, null, function (result) {
                $scope.totalcount = result.data;             
            }, function () {

            })
        }
       

        function getListRequestLicenseToday() {
            var url = 'api/dashboard/getrequestlicensetoday';
            $scope.loading = true;
            apiService.get(url, null, function (result) {
                $scope.requestLicenses = result.data;         
                $scope.loading = false;
            }, function () {
                Console.log('Load danh sách yêu cầu License thất bại!');
                $scope.loading = false;
            })
        }

        $scope.getGetLicenseNotified = getGetLicenseNotified;
        function getGetLicenseNotified() {
            var url = 'api/dashboard/getlicensenotified';
            $scope.loading2 = true;
            apiService.get(url, null, function (result) {
                $scope.licenseInfo = result.data;
                $scope.loading2 = false;

            }, function () {
                Console.log('Load danh sách thất bại!');
                $scope.loading2 = false;
            })
        }
        //$scope.totalCountCustomer();
       // $scope.getGetLicenseNotified();
       // $scope.getListRequestLicenseToday();
    }

    // share value view 1 to view 2
    app.factory('Data', function () {
        return { valueView: '' };
    });
})(angular.module('uit'));