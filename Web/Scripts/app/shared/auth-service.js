(function () {

    var authService = function (sessionService, $http, USER_ROLES) {

        var me = { };

        me.login = function (credentials) {
            //return $http
            //          .post('/login', credentials)
            //          .then(function (res) {
            //              Session.create(res.data.id, res.data.user.id,
            //                             res.data.user.role);
            //              return res.data.user;
            //          });

            return {
                then: function (sucessCallback, errorCallback) {
                    var user = {
                        id: 1,
                        username: 'Teste'
                    };

                    sessionService.create(1, 1, USER_ROLES.equipeMultivagas);

                    sucessCallback(user);

                    
                }
            }
        };

        me.isAuthenticated = function () {
            return !!sessionService.userId;
        };

        me.isAuthorized = function (authorizedRoles) {

            if (!angular.isArray(authorizedRoles)) {
                authorizedRoles = [authorizedRoles];
            }

            return (me.isAuthenticated() && (authorizedRoles.indexOf(sessionService.userRole) !== -1));
        };

        me.logout = function () {
            sessionService.destroy();
        };

        return me;

    };

    angular.module("shared").service("authService", ["sessionService", "$http", "USER_ROLES", authService]);

}());