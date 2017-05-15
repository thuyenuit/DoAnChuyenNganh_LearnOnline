/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('uit',
        ['uit.common',
        'uit.student',
        'uit.teacheradmin',
        'uit.exam']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
         .state('dashboard', {
             url: "/dashboard",
             templateUrl: "/app/components/dashboard/dashboardView.html",
             controller: "dashboardController"
         })   
        $urlRouterProvider.otherwise('/dashboard');
    }
})();
