(function () {    

    var config = function ($stateProvider, USER_ROLES, $resourceProvider) {

        $stateProvider
            .state("funcionario_list", {
                parent: 'dashboard',
                url: "/funcionario",
                templateUrl: "Scripts/app/funcionario/filtroFuncionario.html",
                controller: 'filtroFuncionarioController',
                roles: [USER_ROLES.admin]
            });

        $stateProvider
            .state("funcionario_edit", {
                parent: 'dashboard',
                url: "/funcionario/:id",
                templateUrl: "Scripts/app/funcionario/formFuncionario.html",
                controller: 'formFuncionarioController',
                roles: [USER_ROLES.admin]
            });

        $stateProvider
            .state("funcionario_add", {
                parent: 'dashboard',
                url: "/funcionario",
                templateUrl: "Scripts/app/funcionario/formFuncionario.html",
                controller: 'formFuncionarioController',
                roles: [USER_ROLES.admin]
            });

        $resourceProvider.defaults.stripTrailingSlashes = false;
    };

    angular.module("funcionario", ["shared", "ui.router", "ngResource", "dashboard", "estacionamento"])
        .config(["$stateProvider", "USER_ROLES", "$resourceProvider", config])
}());