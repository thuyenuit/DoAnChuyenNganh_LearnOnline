/// <reference path="C:\Users\PC\Desktop\New version\ASACreateLicense\ASACreateLicense.Web\Assets/admin/libs/angular/angular.min.js" />
(function (app) {

    app.controller('teacherAdminAddController', teacherAdminAddController);
    teacherAdminAddController.$inject = ['$scope', '$state', 'apiService', 'notificationService', '$window', 'commonService']; // khởi tạo

    function teacherAdminAddController($scope, $state, apiService, notificationService, $window, commonService) {
        $scope.userinfo = {};
        $scope.testUsername = '';

        // load tỉnh/tp
        $scope.LoadProvinces = LoadProvinces;
        $scope.listCustomerType = {};
        function LoadProvinces() {
            var url = 'api/other/getallprovince';
            apiService.get(url, null, function (result) {
               
                $scope.listCustomerType = result.data;
            }, function () {
                notificationService.displayError('Load danh sách Tỉnh/TP thất bại');
            })
        }
        $scope.LoadProvinces();

        $scope.gioitinh = false;
        $scope.myGender = function () {
            $scope.gioitinh = true;
            $scope.userinfo.Sex = $scope.gioitinh;        
        }

        $scope.myGenderNu = function () {
            $scope.gioitinh = false;
            $scope.userinfo.Sex = $scope.gioitinh;
        }

        $scope.ChangeProvince = function () {          
            if ($scope.userinfo.ProvincesID != undefined) {
                var consfig = {
                    params: {
                        provinceId: $scope.userinfo.ProvincesID
                    }
                }

                var url = 'api/other/getbyprovince';
                apiService.get(url, consfig, function (result) {
                    $scope.listDistrict = result.data;
                }, function () {
                    notificationService.displayError('Load danh sách Quận/Huyện thất bại');
                })
            }
            else {
                $scope.listDistrict = [];
            } 
        }

        // Nhóm người dùng
        $scope.LoadUserGroup = LoadUserGroup;
        $scope.listUserGroup = {};
        function LoadUserGroup() {
            var url = 'api/other/getusergroupnostudent';
            apiService.get(url, null, function (result) {
                $scope.listUserGroup = result.data;
            }, function () {
                notificationService.displayError('Load nhóm người dùng thất bại');
            })
        }
        $scope.LoadUserGroup();

        //AddUser      
        $scope.AddUser = AddUser;
        function AddUser() {

            //var checkUsername = CheckUsername();
            CheckUsername();

            alert($scope.testUsername);
            if ($scope.testUsername === false)
            {
                var url = 'api/user/create';
                apiService.post(url, $scope.userinfo, function (result) {
                    notificationService.displaySuccess('Đã thêm người dùng thành công');
                    $state.go('users');
                }, function (error) {
                    notificationService.displayError('Thêm người dùng thất bại');
                })
            }
            else
                notificationService.displayError('Tên tài khoản đã được sử dụng!');
        }

        //
        $scope.myFuncUsername = function () {
            $scope.testUsername = false;
            $scope.userinfo.UserName = commonService.replaceCharacter($scope.userinfo.UserName);
        };
        // check tài khoản bị trùng
        function CheckUsername(testUsername) {
            $scope.testUsername = false;
            var url = 'api/user/selectbyusername';
            var consfig = {
                params: {
                    username: $scope.userinfo.UserName
                }
            }
            apiService.get(url, consfig, function (result) {
                if (result.data === true) {
                    $scope.testUsername = true;                  
                }
                else {
                    $scope.testUsername = false;
                }
                 
            }, function (error) {
                $scope.testUsername = false;
            })

           if ($scope.testUsername == true) // đã bị trùng tài khoản
               return false;

           return true;
        }




    }
})(angular.module('uit.teacheradmin'));
