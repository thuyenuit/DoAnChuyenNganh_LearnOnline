(function () {
    angular.module('uit.student',
        ['uit.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('students', {
            url: '/students',
            templateUrl: "/app/components/user/student/studentListView.html",
            controller: "studentListController"
        });

    }
})();