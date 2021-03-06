﻿(function () {    

    var config = function ($stateProvider, USER_ROLES, $resourceProvider) {

        $stateProvider
            .state("estacionamento_publico_list", {
                parent: 'dashboard',
                url: "/estacionamentos/publico",
                templateUrl: "Scripts/app/estacionamento/filtroEstacionamentoPublico.html",
                controller: 'filtroEstacionamentoPublicoController',
                roles: []
            });

        $stateProvider
            .state("estacionamento_reserva_list", {
                parent: 'dashboard',
                url: "/estacionamentos/reserva",
                templateUrl: "Scripts/app/estacionamento/filtroEstacionamentoReserva.html",
                controller: 'filtroEstacionamentoReservaController',
                roles: [USER_ROLES.usuario]
            });

        $stateProvider
            .state("estacionamento_list", {
                parent: 'dashboard',
                url: "/estacionamentos",
                templateUrl: "Scripts/app/estacionamento/filtroEstacionamento.html",
                controller: 'filtroEstacionamentoController',
                roles: [USER_ROLES.equipeMultivagas, USER_ROLES.admin]
            });

        $stateProvider
            .state("estacionamento_edit", {
                parent: 'dashboard',
                url: "/estacionamentos/:id",
                templateUrl: "Scripts/app/estacionamento/formEstacionamento.html",
                controller: 'formEstacionamentoController',
                roles: [USER_ROLES.equipeMultivagas, USER_ROLES.admin]
            });

        $stateProvider
            .state("estacionamento_add", {
                parent: 'dashboard',
                url: "/estacionamento",
                templateUrl: "Scripts/app/estacionamento/formEstacionamento.html",
                controller: 'formEstacionamentoController',
                roles: [USER_ROLES.equipeMultivagas, USER_ROLES.admin]
            });

        $resourceProvider.defaults.stripTrailingSlashes = false;
    };

    angular.module("estacionamento", ["shared", "ui.router", "ngResource", "vagas"])
        .config(["$stateProvider", "USER_ROLES", "$resourceProvider", config])
}());