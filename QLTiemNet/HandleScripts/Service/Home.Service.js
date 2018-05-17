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

    function getAllInformationComputer(Id, UserId) {

        var parameter = {
            Id: Id,
            UserId: UserId
        }

        var request = $http.post('Home/getAllInformationComputer', parameter);

        return request.then(function (response) {
            return response.data;
        },
            function (err) {
                console.log(err);
                return err;
            }); 
    }

    function getStatus() {

        var request = $http.get('Home/getStatus');

        return request.then(function (response) {
            return response.data;
        },
            function (err) {
                console.log(err);
                return err;
            }); 
    }

    function getTimeRemaining(Id) {

        var parameter = {
            Id:Id
        }
        var request = $http.post('Users/getTimeRemaining', parameter);

        return request.then(function (response) {
            return response.data;
        },
            function (err) {
                console.log(err);
                return err;
            });
    }

    return {
        getAllInformationComputer: getAllInformationComputer,
        getAllComputer: getAllComputer,
        getStatus: getStatus,
        getTimeRemaining: getTimeRemaining
    }
}