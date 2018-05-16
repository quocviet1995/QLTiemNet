app.controller("HomeCtrl", ['$scope', 'HomeService', '$interval', function ($scope, HomeService, $interval) {

    $scope.init = function () {
        $scope.status = 0;
        HomeService.getAllComputer($scope.status).then(function (result) {
            if (result != null) {
                console.log(result);
            }
        })
    }

    $scope.init();


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