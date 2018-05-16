app.factory("HomeService", HomeService);

function HomeService($http, $filter) {

    function getAllComputer(status) {

        var parameter = {
            status : status,
        }

        var request = $http.post('Home/getAllComputer', parameter);

        return request.then(function (response) {
            return response.data;
        },
            function (err) {
                console.log(err);
                return err;
            });
    }

    function login(user, password) {
        var parameter = {
            userName: user,
            password: password,
        };

        var request = $http.post('Login', parameter);

        return request.then(function (response) {
            return response.data;
        },
            function (err) {
                console.log(err);
                return err;
            });
    }

    return {
        login: login,
        getAllComputer: getAllComputer
    }
}