(function () {
    angular.module('uit.teacheradmin',
        ['uit.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('users', {
            url: '/users',
            templateUrl: "/app/components/user/teacher_admin/teacherAdminListView.html",
            controller: "teacherAdminListController"
            })
         .state('adduser', {
             url: '/adduser',
             templateUrl: "/app/components/user/teacher_admin/teacherAdminAddView.html",
             controller: "teacherAdminAddController"
         });

    }
})();