(function () {

    var authService = function (sessionService, $http, USER_ROLES) {

        var me = { };

        me.login = function (credentials) {
            return $http
                      .post('/api/account', credentials)
                      .then(function (res) {
                          sessionService.create(1, res.data.Id, res.data.Permissoes);

                          return res.data;
                      });
        };

        me.isAuthenticated = function () {
            return !!sessionService.userId;
        };


        //TODO: Refatorar esse metodo
        me.isAuthorized = function (authorizedRoles) {

            if (!angular.isArray(authorizedRoles)) {
                authorizedRoles = [authorizedRoles];
            }

            var hasRole = false;

            authorizedRoles.forEach(function (role)
            {
                if (!hasRole && sessionService.userRoles != null) {
                    hasRole = sessionService.userRoles.indexOf(role) !== -1;
                }
            });

            return (me.isAuthenticated() && hasRole);
        };

        me.logout = function () {
            sessionService.destroy();
        };

        return me;

    };

    angular.module("shared").service("authService", ["sessionService", "$http", "USER_ROLES", authService]);

}());