(function () {    

    var config = function ($stateProvider, USER_ROLES, $resourceProvider) {

        $stateProvider
            .state("admin_list", {
                parent: 'dashboard',
                url: "/admin",
                templateUrl: "Scripts/app/admin/filtroAdmin.html",
                controller: 'filtroAdminController',
                roles: [USER_ROLES.equipeMultivagas, USER_ROLES.admin]
            });

        $stateProvider
            .state("admin_edit", {
                parent: 'dashboard',
                url: "/admin/:id",
                templateUrl: "Scripts/app/admin/formAdmin.html",
                controller: 'formAdminController',
                roles: [USER_ROLES.equipeMultivagas, USER_ROLES.admin]
            });

        $stateProvider
            .state("admin_add", {
                parent: 'dashboard',
                url: "/admin",
                templateUrl: "Scripts/app/admin/formAdmin.html",
                controller: 'formAdminController',
                roles: [USER_ROLES.equipeMultivagas, USER_ROLES.admin]
            });

        $resourceProvider.defaults.stripTrailingSlashes = false;
    };

    angular.module("admin", ["shared", "ui.router", "ngResource", "dashboard", "estacionamento"])
        .config(["$stateProvider", "USER_ROLES", "$resourceProvider", config])
}());