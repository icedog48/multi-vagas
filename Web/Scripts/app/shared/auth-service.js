(function () {

    var authService = function (sessionService, $http, USER_ROLES) {

        var me = { };

        me.login = function (credentials) {
            var data = "grant_type=password&username=" + credentials.Email + "&password=" + credentials.Senha;

            return $http
                      .post('/token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                      .then(function (response) {

                          sessionService.create(response.data.access_token, response.data.Usuario, JSON.parse(response.data.Permissoes), response.data.AlterarSenha);

                          return sessionService;
                       });
        };

        me.isAuthenticated = function () {
            return !!sessionService.user;
        };


        //TODO: Refatorar esse metodo
        me.isAuthorized = function (authorizedRoles) {

            if (!angular.isArray(authorizedRoles)) {
                authorizedRoles = [authorizedRoles];
            }

            //Caso nao existam roles espeficicando acesso, esta autorizado.
            if (authorizedRoles.length == 0) return true;

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