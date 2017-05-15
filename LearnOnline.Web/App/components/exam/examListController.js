(function (app) {

    app.controller('examListController', examListController);

    examListController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService', '$filter', '$state']; // khởi tạo

    function examListController($scope, apiService, $ngBootbox, notificationService, $filter, $state) {
        $scope.listExam = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;

        $scope.keyword = '';
        //$scope.subjectId = 0;
        //$scope.levelId = 0;

        $scope.options = [
         { name: 5, value: 5 },
         { name: 15, value: 15 },
         { name: 50, value: 50 },
         { name: 100, value: 100 }];
        $scope.valueShow = $scope.options[0].value;

        $scope.changeShow = function () {
            getListExam();
        }

        $scope.search = function () {
            getListExam();
        }
           
        $scope.getListExam = getListExam;
        function getListExam(page) {
            if ($scope.subjectId === undefined || $scope.subjectId === null)
                $scope.subjectId = -1;
            if ($scope.levelId === undefined || $scope.levelId === null)
                $scope.levelId = -1;
            page = page || 0;
            var consfig = {
                params: {
                    keyword: $scope.keyword,
                    subjectId: $scope.subjectId,
                    levelId: $scope.levelId,
                    page: page,
                    pageSize: $scope.valueShow
                }
            }

            var url = 'api/exam/getall';
            $scope.loading = true;
            apiService.get(url, consfig, function (result) {
                $scope.listExam = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;

            }, function () {
                console.log('Load danh sách thất bại!');
                $scope.loading = false;
            })
        }
        $scope.getListExam();

        $scope.listSubject = {};
        function loadSubjects() {
            apiService.get('api/other/getallsubjects', null, function (result) {
                $scope.listSubject = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }
        loadSubjects();

        $scope.listLevels = {};
        function loadLevel() {
            apiService.get('api/other/getalllevels', null, function (result) {
                $scope.listLevels = result.data;
            }, function () {
                notificationService.displayError('Load danh sách cấp độ thất bại!');
                //console.log('Cannot get list parent');
            });
        }
        loadLevel();

        // xóa multi
        $scope.selectAll = selectAll;
        $scope.deleteMulti = deleteMulti;
        function deleteMulti() {
            $ngBootbox.confirm('Bạn có muốn chắc xóa những mục đã chọn?').then(function () {
                var listId = [];
                $.each($scope.selected, function (i, item) {
                    listId.push(item.ID);
                });

                var consfigs = {
                    params: {
                        jsonlistId: JSON.stringify(listId)
                    }
                }
                apiService.del('api/exam/deletemulti', consfigs, function (result) {
                    if (result.status === 400)
                        notificationService.displayError('Xóa không thành công! Vui lòng kiểm tra lại.');
                    else {
                        notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi');
                        getListExam();
                    }

                }, function () {
                    notificationService.displayError('Xóa không thành công! Vui lòng kiểm tra lại.');
                })
            });
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll == false) {
                angular.forEach($scope.listExam, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            }
            else {
                angular.forEach($scope.listExam, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }
        // Lắng nghe sự thay đổi của requestLicenses,
        // co 2 tham so: 1 - lang nghe ten bien requestLicenses
        //               2 - function (new, old) va filter nhung gia tri moi la true thi vao danh sach da dc checked
        $scope.$watch("listExam", function (ne, old) {
            var checked = $filter("filter")(ne, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDeleteMulti').removeAttr('disabled');
            } else {
                $('#btnDeleteMulti').attr('disabled', 'disabled');
            }
        }, true);
    }

})(angular.module('uit.exam'));