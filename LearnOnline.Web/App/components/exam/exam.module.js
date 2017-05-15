(function () {
    angular.module('uit.exam',
        ['uit.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('exams', {
            url: '/exams',
            templateUrl: "/app/components/exam/examListView.html",
            controller: "examListController"
        })
        .state('exam-import', {
            url: '/exam-import',
            templateUrl: "/app/components/exam/examImportView.html",
            controller: "examImportController"
        });

    }
})();