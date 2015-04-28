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

        $stateProvider
            .state("registrar_saida", {
                parent: 'dashboard',
                url: "/movimentacao/saida",
                templateUrl: "Scripts/app/movimentacao/formRegistrarSaida.html",
                controller: 'formRegistrarSaidaController',
                roles: [USER_ROLES.funcionario]
            });

        $stateProvider
            .state("editar_entrada", {
                parent: 'dashboard',
                url: "/movimentacoes/:id",
                templateUrl: "Scripts/app/movimentacao/formRegistrarEntrada.html",
                controller: 'formRegistrarEntradaController',
                roles: [USER_ROLES.funcionario]
            });

        $stateProvider
            .state("movimentacao_list", {
                parent: 'dashboard',
                url: "/movimentacoes",
                templateUrl: "Scripts/app/movimentacao/filtroMovimentacao.html",
                controller: 'filtroMovimentacaoController',
                roles: [USER_ROLES.funcionario]
            });

        $resourceProvider.defaults.stripTrailingSlashes = false;
    };

    angular.module("movimentacao",
        [
            "shared",
            "ui.router",
            "ngResource",
            "estacionamento"
        ])
        .config(["$stateProvider", "USER_ROLES", "$resourceProvider", config])
}());