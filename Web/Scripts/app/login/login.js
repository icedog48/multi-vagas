(function () {
    var config = function ($routeProvider, $locationProvider, APP_CONFIG, USER_ROLES) {
        $routeProvider
            .when("/login", {
                templateUrl:  APP_CONFIG.templateBaseUrl + "login/login-form.html",
                controller: "loginController",
                authorizedRoles: [USER_ROLES.any]
            });

        $locationProvider.html5Mode(true);
    };

    angular.module("login", ["shared", "ngRoute"])
        .config(["$routeProvider", "$locationProvider", "APP_CONFIG", "USER_ROLES", config]);
}());