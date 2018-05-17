app.controller("HomeCtrl", ['$scope', 'HomeService', '$interval', function ($scope, HomeService, $interval) {

    $scope.computerInformation = null;
    $scope.data = [];
    $scope.AllStatus = [];
    $scope.status = "0";
    $scope.TimeRemaining = 0;

    $scope.init = function () {

        HomeService.getAllComputer($scope.status).then(function (result) {
            if (result != null) {
                $scope.data = result;
            }
        })
    }

    $scope.Search = function () {
        HomeService.getAllComputer($scope.status).then(function (result) {
            if (result != null) {
                $scope.data = result;
            }
        })
    }

    $scope.getStatus = function () {

        HomeService.getStatus().then(function (result) {
            if (result != null) {
                $scope.AllStatus = result;
            }
        })
    }

    $scope.getTimeRemaining = function () {
        var Id = $("#UserId").val();
        if (Id != null) {
            HomeService.getTimeRemaining(Id).then(function (result) {
                if (result != null) {
                    $scope.TimeRemaining = result;
                }
            })
        }
    }

    $interval(function () {
        $scope.init();
        $scope.getTimeRemaining();
    },1000*30)

    $scope.init();
    $scope.getStatus();
    $scope.getTimeRemaining();

    $scope.getAllInformationComputer = function (Id, UserId) {

        HomeService.getAllInformationComputer(Id, UserId).then(function (result) {
            if (result != null) {
                $scope.computerInformation = result[0];
            }
        })
    }

}]);