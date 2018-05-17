app.controller("ToolCtrl", ['$scope', 'ToolService', '$interval', function ($scope, ToolService, $interval) {

    $scope.AllComputerStatusEmpty = [];
    $scope.UserComputerActive = [];
    $scope.userId = -1;

    $scope.getAllComputerStatusEmpty = function () {

        ToolService.getAllComputerStatusEmpty().then(function (result) {
            if (result != null) {
                $scope.AllComputerStatusEmpty = result;
            }
        })
    }

    $scope.getAllComputerStatusEmpty();

    $scope.GetUserComputerActive = function () {

        ToolService.GetUserComputerActive().then(function (result) {
            if (result != null) {
                console.log(result);
                $scope.UserComputerActive = result;
            }
        })
    }

    $scope.GetUserComputerActive();
    $interval(function () {
        $scope.getAllComputerStatusEmpty();
        $scope.GetUserComputerActive();
    }, 1000 * 30)


    $scope.UserRegisterComputer = function () {
        var user = $scope.user;
        var password = $scope.password;
        var userId = $scope.userId;
        ToolService.UserRegisterComputer(user, password, userId).then(function (result) {
            if (result != null) {
                if (result.error == 0) {
                    swal(
                        result.message,
                        '',
                        'success'
                    )
                    $scope.GetUserComputerActive();
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

    $scope.UserLogoutComputer = function (ComputerId, UserId) {

        ToolService.UserLogoutComputer(ComputerId, UserId).then(function (result) {
            if (result != null) {
                if (result.error == 0) {
                    swal(
                        result.message,
                        '',
                        'success'
                    )
                    $scope.GetUserComputerActive();
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