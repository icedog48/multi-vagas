(function () {
    var config = function ($stateProvider, USER_ROLES) {

        $stateProvider
            .state("dashboard", {
                abstract: true,
                url: "/dashboard",
                templateUrl: "Scripts/app/dashboard/dashboard.html",
                controller: 'dashboardController',
                roles: [USER_ROLES.equipeMultivagas, USER_ROLES.admin, USER_ROLES.funcionario, USER_ROLES.cliente]
            });

    };

    angular.module("dashboard",
        [
             "shared",
             "ui.router",
             "estacionamento"
        ])

        .config(["$stateProvider", "USER_ROLES", config]);
}());