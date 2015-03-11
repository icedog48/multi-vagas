(function () {    

    var config = function ($stateProvider, APP_CONFIG, USER_ROLES, $resourceProvider) {

        $stateProvider
            .state("estacionamento_list", {
                parent: 'dashboard',
                url: "/estacionamentos",
                templateUrl: APP_CONFIG.templateBaseUrl + "estacionamento/lista.html",
                controller: 'listaController',
                roles: [USER_ROLES.equipeMultivagas, USER_ROLES.admin]
            });

        $stateProvider
            .state("estacionamento_edit", {
                parent: 'dashboard',
                url: "/estacionamentos/:id",
                templateUrl: APP_CONFIG.templateBaseUrl + "estacionamento/formulario.html",
                controller: 'formularioController',
                roles: [USER_ROLES.equipeMultivagas, USER_ROLES.admin]
            });

        $stateProvider
            .state("estacionamento_add", {
                parent: 'dashboard',
                url: "/estacionamento",
                templateUrl: APP_CONFIG.templateBaseUrl + "estacionamento/formulario.html",
                controller: 'formularioController',
                roles: [USER_ROLES.equipeMultivagas, USER_ROLES.admin]
            });

        $resourceProvider.defaults.stripTrailingSlashes = false;
    };

    angular.module("estacionamento", ["shared", "ui.router", "ngResource"])
        .config(["$stateProvider", "APP_CONFIG", "USER_ROLES", "$resourceProvider", config])
}());