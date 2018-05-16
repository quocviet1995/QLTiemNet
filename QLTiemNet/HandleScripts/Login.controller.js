app.controller("LoginCtrl", ['$scope', 'LoginService', '$interval', function ($scope, LoginService, $interval) {
    $scope.Login = function () {
        var user = $scope.user;
        var password = $scope.password;
        LoginService.login(user, password).then(function (result) {
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