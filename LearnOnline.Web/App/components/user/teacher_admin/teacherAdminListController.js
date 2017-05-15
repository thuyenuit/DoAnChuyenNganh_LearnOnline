(function (app) {

    app.controller('teacherAdminListController', teacherAdminListController);

    teacherAdminListController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService', '$filter', '$state']; // khởi tạo

    function teacherAdminListController($scope, apiService, $ngBootbox, notificationService, $filter, $state) {
        $scope.liststudent = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;

        $scope.getListStudent = getListStudent;
        function getListStudent(page) {
            page = page || 0;
            var consfig = {
                params: {
                    keyword: '',
                    page: page,
                    pageSize: 5
                }
            }

            var url = 'api/user/getallteacheradmin';
            $scope.loading = true;
            apiService.get(url, consfig, function (result) {
                $scope.liststudent = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;

            }, function () {
                console.log('Load danh sách thất bại!');
                $scope.loading = false;
            })
        }
        $scope.getListStudent();
    }

})(angular.module('uit.teacheradmin'));