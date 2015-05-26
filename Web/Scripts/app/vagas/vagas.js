(function () {    

    var config = function ($stateProvider, USER_ROLES, $resourceProvider) {

        $stateProvider
            .state("vagas_list", {
                parent: 'dashboard',
                url: "/vagas",
                templateUrl: "Scripts/app/vagas/filtroVagas.html",
                controller: 'filtroVagasController',
                roles: [USER_ROLES.equipeMultivagas, USER_ROLES.admin]
            });

        $stateProvider
            .state("vagas_edit", {
                parent: 'dashboard',
                url: "/vagas/:id",
                templateUrl: "Scripts/app/vagas/formVagas.html",
                controller: 'formVagasController',
                roles: [USER_ROLES.equipeMultivagas, USER_ROLES.admin]
            });

        $stateProvider
            .state("vagas_add", {
                parent: 'dashboard',
                url: "/vaga",
                templateUrl: "Scripts/app/vagas/formVagas.html",
                controller: 'formVagasController',
                roles: [USER_ROLES.equipeMultivagas, USER_ROLES.admin]
            });


        $stateProvider
           .state("reservar_vaga", {
               parent: 'dashboard',
               url: "/vagas/reservar",
               templateUrl: "Scripts/app/vagas/formReservaVaga.html",
               controller: 'formReservaVagaController',
               roles: [USER_ROLES.usuario]
           });


        $resourceProvider.defaults.stripTrailingSlashes = false;
    };

    angular.module("vagas", ["shared", "ui.router", "ngResource", "dashboard", "estacionamento"])
        .config(["$stateProvider", "USER_ROLES", "$resourceProvider", config])
}());