(function () {

    var APP_CONFIG = {
        templateBaseUrl: "Scripts/app/"
    };

    var USER_ROLES = {
        equipeMultivagas: 'equipe_multivagas', // Cadastra estacionamentos
        admin: 'administrador', // Gerencia estacionamentos
        funcionario: 'funcionario', // Registra entrada e saida de clientes
        usuario: 'usuario' // Consulta e reserva vagas
    };

    var httpInterceptor = function ($q, $rootScope, sessionService) {
        var numLoadings = 0;

        return {
            request: function (config) {

                numLoadings++;                

                $rootScope.$broadcast("loader_show");

                if (config.headers.Authorization === 'token') {
                    config.headers.Authorization = 'Bearer ' + sessionService.token;
                }

                return config || $q.when(config)

            },
            response: function (config) {
                
                numLoadings--;                

                if ((numLoadings) === 0) {
                    $rootScope.$broadcast("loader_hide");
                }

                return config || $q.when(config);

            },
            responseError: function (config) {

                if (!(--numLoadings)) {
                    $rootScope.$broadcast("loader_hide");
                }

                return $q.reject(config);
            }
        };
    };

    var config = function ($httpProvider) {
        $httpProvider.interceptors.push('httpInterceptor');
    };

    var loaderDirective = function ($rootScope) {

        return {
            restrict: 'EA',
            link: function (scope, element) {

                var shownType = element.css('display');

                function hideElement() {
                    element.css('display', 'none');
                };                
            
                scope.$on('loader_show', function () {
                    element.css('display', shownType);
                });

                scope.$on('loader_hide', hideElement);
                                
                hideElement();                
            }
        };
    };

    angular.module("shared", [])
        .constant("APP_CONFIG", APP_CONFIG)
        .constant("USER_ROLES", USER_ROLES)
        .config(["$httpProvider", config])
        .directive("loader", ["$rootScope", loaderDirective])
        .factory("httpInterceptor", ["$q", "$rootScope", "sessionService", httpInterceptor])
    ;
}());