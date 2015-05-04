(function () {
    var config = function ($stateProvider, APP_CONFIG, USER_ROLES) {

        $stateProvider.state("login", {
            url: "/login",
            templateUrl: APP_CONFIG.templateBaseUrl + "login/login-form.html",
            controller: 'loginController'
        });

    };

    angular.module("login",
        [
             "shared",
             "ui.router"
        ])

        .config(["$stateProvider", "APP_CONFIG", "USER_ROLES", config]);
}());