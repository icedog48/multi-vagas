(function () {

    var mainController = function ($scope, authService, USER_ROLES) {

        $scope.currentUser = null;
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
                  $state.transitionTo('estacionamento_publico_list', null, { 'reload': true });
              }]
        });
    };

    var runFunction = function ($rootScope, $state, authService, $location) {

        $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
            
            var authorizedRoles = toState.roles || [];

            var autenticado = authService.isAuthenticated();

            var autorizado = authService.isAuthorized(authorizedRoles);            

            if (!(autenticado || autorizado)) {
                event.preventDefault();

                //$state.go('estacionamento_publico_list');
                $state.transitionTo('estacionamento_publico_list', null, { 'reload': true });
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
        "ui.router",
        "frapontillo.bootstrap-switch",
        "ui.bootstrap"
    ])

    .config(["$stateProvider", "APP_CONFIG", "USER_ROLES", config])

    .controller("mainController", ["$scope", "authService", "USER_ROLES", mainController])

    

    .run(["$rootScope", "$state", "authService", "$location", runFunction]);

}());