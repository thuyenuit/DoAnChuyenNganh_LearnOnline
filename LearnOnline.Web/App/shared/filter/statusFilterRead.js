(function (app) {
    app.filter('statusFilterRead', function () {
        return function (input) {
            if (input == true)
                return 'Watched';
            else
                return 'Unwatch';
        }
    })
})(angular.module('uit.common'));