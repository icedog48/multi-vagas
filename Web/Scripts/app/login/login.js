(function () {
    var config = function ($stateProvider, APP_CONFIG, USER_ROLES) {

        $stateProvider.state("login", {
            url: "/login",
            templateUrl: APP_CONFIG.templateBaseUrl + "login/login-form.html",
            controller: 'loginController'
        });


        $stateProvider.state("registrar", {
            url: "/registrar",
            templateUrl: APP_CONFIG.templateBaseUrl + "login/formRegistrarCliente.html",
            controller: 'formRegistrarClienteController'
        });

        $stateProvider.state("alterar_senha", {
            url: "/alterarsenha",
            templateUrl: APP_CONFIG.templateBaseUrl + "login/formAlterarSenha.html",
            controller: 'formAlterarSenhaController',
            roles: [USER_ROLES.equipeMultivagas, USER_ROLES.admin, USER_ROLES.funcionario, USER_ROLES.usuario]
        });

    };

    angular.module("login",
        [
             "shared",
             "ui.router"
        ])

        .config(["$stateProvider", "APP_CONFIG", "USER_ROLES", config]);
}());