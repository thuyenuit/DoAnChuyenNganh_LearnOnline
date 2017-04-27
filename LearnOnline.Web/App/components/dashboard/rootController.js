//(function (app) {
//    app.controller('rootController', rootController);

//    rootController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService']; // khởi tạo

//    function rootController($scope, apiService, $ngBootbox, notificationService) {

//        $scope.lang = '';

//        $scope.defaultLanguage = defaultLanguage;
//        function defaultLanguage() {
//            var url = 'api/dashboard/defaultlanguage';
//            apiService.get(url, null, function (result) {
//                $scope.lang = result.data;
//            }, function () {

//            })
//        }
//        $scope.defaultLanguage();

//        $scope.setLanguage = function () {
//            SetLanguage();
//        }

//        function SetLanguage() {

//            var consfigs = {
//                params: {
//                    lgID: $scope.lang
//                }
//            }

//            var url = 'api/dashboard/setlanguage';
//            apiService.get(url, consfigs, function (result) {
//                $scope.lang = result.data;
//            }, function () {

//            })
//        }
//    }
//})(angular.module('uit'));