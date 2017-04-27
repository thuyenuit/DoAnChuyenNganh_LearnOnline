(function (app) {
    app.filter('statusFilter', function () {
        return function (input) {
            if (input == false)
                return 'Đã hủy';
            else
                return 'Đang hoạt động';
        }     
    })
})(angular.module('uit.common'));