(function () {

    var APP_CONFIG = {
        templateBaseUrl: "Scripts/app/"
    };

    var USER_ROLES = {
        equipeMultivagas: 'equipe_multivagas', // Cadastra estacionamentos
        admin: 'admin', // Gerencia estacionamentos
        funcionario: 'funcionario', // Registra entrada e saida de clientes
        cliente: 'cliente' // Consulta e reserva vagas
    };

    var httpInterceptor = function ($q, $rootScope) {
        var numLoadings = 0;

        return {
            request: function (config) {

                numLoadings++;

                // Show loader
                $rootScope.$broadcast("loader_show");
                return config || $q.when(config)

            },
            response: function (response) {

                if ((--numLoadings) === 0) {
                    // Hide loader
                    $rootScope.$broadcast("loader_hide");
                }

                return response || $q.when(response);

            },
            responseError: function (response) {

                if (!(--numLoadings)) {
                    // Hide loader
                    $rootScope.$broadcast("loader_hide");
                }

                return $q.reject(response);
            }
        };
    };

    var config = function ($httpProvider) {
        $httpProvider.interceptors.push('httpInterceptor');
    };

    var loaderDirective = function ($rootScope) {
        return function ($scope, element, attrs) {
            $scope.$on("loader_show", function () {
                return element.show();
            });
            return $scope.$on("loader_hide", function () {
                return element.hide();
            });
        };
    };

    angular.module("shared", [])
        .constant("APP_CONFIG", APP_CONFIG)
        .constant("USER_ROLES", USER_ROLES)
        .config(["$httpProvider", config])
        .factory("httpInterceptor", ["$q", "$rootScope", httpInterceptor])
        .directive("loader", ["$rootScope", loaderDirective]);
    ;
}());