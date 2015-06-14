(function () {

    var mainController = function ($scope, authService, USER_ROLES, sessionService) {

        if (authService.isAuthenticated()) { //Caso o usuario esteja autenticado, recupera os dados da sessao
            $scope.currentUser = sessionService;
        }
        else {
            $scope.currentUser = null;
        }

        $scope.userRoles = USER_ROLES;
        $scope.isAuthorized = authService.isAuthorized;

        $scope.setCurrentUser = function (user) {
            $scope.currentUser = user;
        };

        $scope.isAuthenticated = function () {
            return $scope.currentUser != null;
        }
    }

    var config = function ($stateProvider, APP_CONFIG, USER_ROLES) {

        $stateProvider.state("otherwise", {
            url: "*path",
            template: "",
            controller: [
                      '$state',
              function ($state) {
                  $state.go('movimentacao_list');
              }]
        });
    };

    var runFunction = function ($rootScope, $state, authService, $location, USER_ROLES) {

        $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
            
            var authorizedRoles = toState.roles || [];

            var autenticado = authService.isAuthenticated();

            var autorizado = authService.isAuthorized(authorizedRoles);

            if (!autenticado && (toState.name != "estacionamento_publico_list" && toState.name != "login" && toState.name != "registrar")) {
                event.preventDefault();

                $state.go('estacionamento_publico_list');
            }

            if (autenticado && !autorizado) {
                event.preventDefault();                

                if (authService.isAuthorized([USER_ROLES.equipeMultivagas, USER_ROLES.admin])) {
                    $state.go('estacionamento_list');
                } else if (authService.isAuthorized(USER_ROLES.funcionario)) {
                    $state.go('movimentacao_list');
                } else if (authService.isAuthorized(USER_ROLES.usuario)) {
                    $state.go('estacionamento_reserva_list');
                } else {
                    $state.go('estacionamento_publico_list');
                }
            }
        });
    };


    //MODULOS
    angular.module("multi-vagas",
    [
        "login",
        "shared",
        "dashboard",
        "estacionamento",
        "funcionario",
        "movimentacao",
        "relatorios",
        "ui.router",
        "frapontillo.bootstrap-switch",
        "ui.bootstrap",
        "ui.utils.masks"
    ])

    .config(["$stateProvider", "APP_CONFIG", "USER_ROLES", config])

    .controller("mainController", ["$scope", "authService", "USER_ROLES", "sessionService", mainController])

    .run(["$rootScope", "$state", "authService", "$location", "USER_ROLES", runFunction]);

}());