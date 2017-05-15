(function (app) {

    app.controller('examImportController', examImportController);

    examImportController.$inject = ['$scope', '$http', 'apiService', 'notificationService', '$filter', '$state']; // khởi tạo

    function examImportController($scope, $http, apiService, notificationService, $filter, $state) {
        $scope.listSubject = {};
        function loadSubjects() {
            apiService.get('api/other/getallsubjects', null, function (result) {
                //alert('ok');
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

        $scope.files = [];
        $scope.$on("fileSelected", function (event, args) {
            $scope.$apply(function () {
                //add the file object to the scope's files collection
                $scope.files.push(args.file);
            });
        });

        $scope.subjectId = 0;
        $scope.levelId = 0;

        $scope.ImportExam = ImportExam;
        function ImportExam() {
            //alert('ok nhé');
            //authenticationService.setHeader();
            $http({
                method: 'POST',
                url: "/api/exam/import",
                headers: { 'Content-Type': undefined },
                transformRequest: function (data) {
                    var formData = new FormData();
                    formData.append("subjectId", angular.toJson(data.subjectId));
                    formData.append("levelId", angular.toJson(data.levelId));

                    for (var i = 0; i < data.files.length; i++) {
                        formData.append("file" + i, data.files[i]);
                    }
                    return formData;
                },
                //Create an object that contains the model and files which will be transformed
                // in the above transformRequest method
                data: { subjectId: $scope.subjectId, levelId: $scope.levelId, files: $scope.files }
            }).then(function (result, status, headers, config) {
                 notificationService.displaySuccess(result.data);
               // alert('ok nhé');
                $state.go('exams');
            },
            function (data, status, headers, config) {
               // alert('thất bại rồi');
                notificationService.displayError(data);
            });
        }
    }

})(angular.module('uit.exam'));