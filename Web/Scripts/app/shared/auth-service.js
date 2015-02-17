(function () {

    var authService = function (sessionService, $http, USER_ROLES) {

        this.login = function (credentials) {

        };

        authService.isAuthenticated = function () {
            return !!sessionService.userId;
        };

        authService.isAuthorized = function (authorizedRoles) {

            if (!angular.isArray(authorizedRoles)) {
                authorizedRoles = [authorizedRoles];
            }

            return (authService.isAuthenticated() && (authorizedRoles.indexOf(sessionService.userRole) !== -1)) || (authorizedRoles.indexOf(USER_ROLES.any) !== -1);
        };

        return authService;

    };

    angular.module("shared").service("authService", ["sessionService", "$http", "USER_ROLES", authService]);

}());