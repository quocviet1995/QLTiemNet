app.factory("LoginAdminService", LoginAdminService);

function LoginAdminService($http, $filter) {

    function login( user, password) {
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
        login : login
    }
}