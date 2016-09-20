angular.module("mmHomeApp")
    .factory('authenticationService', ['$window', '$q', '$http',
        function ($window, $q, $http) {
            var userInfo;

            function login(userName, password) {
                var deferred = $q.defer();

                $http.post("/api/User", {
                    userName: userName,
                    password: password
                }).then(function (result) {
                    if (result.data.User.UserName) {
                        userInfo = {
                            keyId: result.data.User.KeyId,
                            userName: result.data.User.UserName
                        };
                        $window.sessionStorage["userInfo"] = JSON.stringify(userInfo);
                        deferred.resolve(userInfo);
                    } else {
                        deferred.reject(result.status);
                    }
                }, function (error) {
                    deferred.reject(error);
                });
                return deferred.promise;
            }

            function getUserInfo() {
                return userInfo;
            };

            return {
                login: login,
                getUserInfo: getUserInfo
            };
        }
    ]);