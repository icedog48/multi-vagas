(function () {    

    var config = function ($stateProvider, USER_ROLES, $resourceProvider) {

        $stateProvider
            .state("estacionamento_list", {
                parent: 'dashboard',
                url: "/estacionamentos",
                templateUrl: "Scripts/app/estacionamento/lista.html",
                controller: 'listaController',
                roles: [USER_ROLES.equipeMultivagas, USER_ROLES.admin]
            });

        $stateProvider
            .state("estacionamento_edit", {
                parent: 'dashboard',
                url: "/estacionamentos/:id",
                templateUrl: "Scripts/app/estacionamento/formulario.html",
                controller: 'formularioController',
                roles: [USER_ROLES.equipeMultivagas, USER_ROLES.admin]
            });

        $stateProvider
            .state("estacionamento_add", {
                parent: 'dashboard',
                url: "/estacionamento",
                templateUrl: "Scripts/app/estacionamento/formulario.html",
                controller: 'formularioController',
                roles: [USER_ROLES.equipeMultivagas, USER_ROLES.admin]
            });

        $resourceProvider.defaults.stripTrailingSlashes = false;
    };

    angular.module("estacionamento", ["shared", "ui.router", "ngResource"])
        .config(["$stateProvider", "USER_ROLES", "$resourceProvider", config])
}());