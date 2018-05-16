app.controller("LoginAdminCtrl", ['$scope', 'LoginAdminService', '$interval', function ($scope, LoginAdminService, $interval) {
    $scope.LoginAdmin = function () {
        var user = $scope.user;
        var password = $scope.password;
        LoginAdminService.login(user, password).then(function (result) {
            if (result != null) {
                if (result.error == 0) {
                    swal(
                        result.message,
                        '',
                        'success'
                    )
                    window.location.href = "Index";
                } else {
                    swal(
                        result.message,
                        '',
                        'error'
                    )
                }
            }
        })
    }
}]);