(function () {
    var config = function ($stateProvider, APP_CONFIG) {

        $stateProvider.state("login", {
            url: "/login",
            templateUrl: APP_CONFIG.templateBaseUrl + "login/login-form.html",
            controller: 'loginController'
        });


        $stateProvider.state("registrar", {
            url: "/registrar",
            templateUrl: APP_CONFIG.templateBaseUrl + "login/formRegistrar.html",
            controller: 'formRegistrarController'
        });

    };

    angular.module("login",
        [
             "shared",
             "ui.router"
        ])

        .config(["$stateProvider", "APP_CONFIG", config]);
}());