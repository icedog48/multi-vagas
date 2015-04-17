(function () {    

    var config = function ($stateProvider, USER_ROLES, $resourceProvider) {

        $stateProvider
            .state("registrar_entrada", {
                parent: 'dashboard',
                url: "/movimentacao/entrada",
                templateUrl: "Scripts/app/movimentacao/formRegistrarEntrada.html",
                controller: 'formRegistrarEntradaController',
                roles: [USER_ROLES.funcionario]
            });

        $resourceProvider.defaults.stripTrailingSlashes = false;
    };

    angular.module("movimentacao",
        [
            "shared",
            "ui.router",
            "ngResource",
            "estacionamento"]
        )
        .config(["$stateProvider", "USER_ROLES", "$resourceProvider", config])
}());