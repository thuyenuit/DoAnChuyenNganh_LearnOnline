/// <reference path="/Assets/admin/libs/angular/angular.js" />


(function (app) {
    app.factory('apiService', apiService);

    apiService.$inject = ['$http', 'notificationService'];
    function apiService($http, notificationService) {
        return {
            get: get,
            post: post,
            put: put,
            del: del
        }

        function get(url, params, success, failed) {
            $http.get(url, params).then(function (result) {
                success(result);
            }, function (error) {          
                failed(error);
            })
        }

        function post(url, data, success, failed) {
            $http.post(url, data).then(function (result) {

                success(result);
            }, function (error) {

                if (error.status === 401)
                {
                    notificationService.displayError('Vui lòng đăng nhập!');
                }

                failed(error);
            })
        }

        function put(url, data, success, failure) {
            $http.put(url, data).then(function (result) {
                success(result);
            }, function (error) {
                console.log(error.status)
                if (error.status === 401) {
                    notificationService.displayError('Vui lòng đăng nhập!');
                }
                else if (failure != null) {
                    failure(error);
                }

            });
        }

        function del(url, data, success, failure) {
            $http.delete(url, data).then(function (result) {
                success(result);
            }, function (error) {
                console.log(error.status)
                if (error.status === 401) {
                    notificationService.displayError('Vui lòng đăng nhập!');
                }
                else if (failure != null) {
                    failure(error);
                }

            });
        }
    }
})(angular.module('uit.common'));