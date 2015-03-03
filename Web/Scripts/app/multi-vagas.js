(function () {

    var mainController = function ($scope, authService, USER_ROLES) {

        $scope.currentUser = null;
        $scope.userRoles = USER_ROLES;
        $scope.isAuthorized = authService.isAuthorized;

        $scope.setCurrentUser = function (user) {
            $scope.currentUser = user;
        };
    }

    var config = function ($stateProvider, APP_CONFIG, USER_ROLES) {

        $stateProvider.state("otherwise", {
            url: "*path",
            template: "",
            controller: [
                      '$state',
              function ($state) {                  
                  $state.go('estacionamento_list')
              }]
        });
    };

    var runFunction = function ($rootScope, $state, AUTH_EVENTS, authService, $location) {

        $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
            
            var authorizedRoles = toState.roles;

            if (!authService.isAuthorized(authorizedRoles) && (typeof (authorizedRoles) !== 'undefined')) {
                
                event.preventDefault();

                if (authService.isAuthenticated()) {
                    $state.go('estacionamento_list');
                } else {
                    $state.go('login');
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
        "ui.router",
        "frapontillo.bootstrap-switch"
    ])

    .config(["$stateProvider", "APP_CONFIG", "USER_ROLES", config])

    .controller("mainController", ["$scope", "authService", "USER_ROLES", mainController])

    .run(["$rootScope", "$state", "AUTH_EVENTS", "authService", "$location", runFunction]);

}());