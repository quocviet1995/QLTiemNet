app.factory("ToolService", ToolService);

function ToolService($http, $filter) {

    function getAllComputerStatusEmpty() {

        var request = $http.get('getAllComputerStatusEmpty');

        return request.then(function (response) {
            return response.data;
        },
            function (err) {
                console.log(err);
                return err;
            });
    }

    function UserRegisterComputer(user, password, ComputerId) {
        var parameter = {
            userName: user,
            password: password,
            ComputerId: ComputerId
        };

        var request = $http.post('UserRegisterComputer', parameter);

        return request.then(function (response) {
            return response.data;
        },
            function (err) {
                console.log(err);
                return err;
            });
    }

    function GetUserComputerActive() {

        var request = $http.get('GetUserComputerActive');

        return request.then(function (response) {
            return response.data;
        },
            function (err) {
                console.log(err);
                return err;
            });
    }

    function UserLogoutComputer(ComputerId, UserId) {
        var parameter = {
            ComputerId: ComputerId,
            UserId: UserId
        };

        var request = $http.post('UserLogoutComputer', parameter);

        return request.then(function (response) {
            return response.data;
        },
            function (err) {
                console.log(err);
                return err;
            });
    }

    return {
        UserRegisterComputer: UserRegisterComputer,
        getAllComputerStatusEmpty: getAllComputerStatusEmpty,
        GetUserComputerActive: GetUserComputerActive,
        UserLogoutComputer: UserLogoutComputer
    }
}