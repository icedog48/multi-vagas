(function () {    

    var config = function ($stateProvider, APP_CONFIG, USER_ROLES) {
        $stateProvider
            .state("estacionamentos", {
                parent: 'dashboard',
                url: "/estacionamentos",
                templateUrl: APP_CONFIG.templateBaseUrl + "estacionamento/lista.html",
                controller: 'estacionamentoController',
                roles: [USER_ROLES.equipeMultivagas, USER_ROLES.admin]
            });

        $stateProvider
            .state("estacionamento", {
                parent: 'dashboard',
                url: "/estacionamento/:id",
                templateUrl: APP_CONFIG.templateBaseUrl + "estacionamento/edit.html",
                controller: 'estacionamentoController',
                roles: [USER_ROLES.equipeMultivagas, USER_ROLES.admin]
            });
    };

    angular.module("estacionamento", ["shared", "ui.router"])
        .config(["$stateProvider", "APP_CONFIG", "USER_ROLES", config])
}());