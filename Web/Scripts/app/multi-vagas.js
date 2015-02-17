(function () {

    var mainController = function ($scope, authService, USER_ROLES) {

        $scope.currentUser = null;
        $scope.userRoles = USER_ROLES;
        $scope.isAuthorized = authService.isAuthorized;

        $scope.setCurrentUser = function (user) {
            $scope.currentUser = user;
        };
    }

    var config = function ($routeProvider, $locationProvider, APP_CONFIG, USER_ROLES) {
        $routeProvider
            .when("/", {
                templateUrl: APP_CONFIG.templateBaseUrl + "home/home.html",
                authorizedRoles: [USER_ROLES.admin]
            });

        $locationProvider.html5Mode(true);
    };

    var runFunction = function ($rootScope, AUTH_EVENTS, authService, $location) {

        $rootScope.$on('$routeChangeStart', function (event, next) {

            var authorizedRoles = next.authorizedRoles;

            if (!authService.isAuthorized(authorizedRoles)) {
                event.preventDefault();

                if (authService.isAuthenticated()) {
                    // user is not allowed
                    $rootScope.$broadcast(AUTH_EVENTS.notAuthorized);

                    $location.path('/')

                } else {
                    // user is not logged in
                    $rootScope.$broadcast(AUTH_EVENTS.notAuthenticated);

                    $location.path('/login')
                }
            }
        });
    };

    angular.module("multi-vagas",
    [
        "ngRoute",
        "login",
        "shared"
    ])

    .config(["$routeProvider", "$locationProvider", "APP_CONFIG", "USER_ROLES", config])

    .controller("mainController", ["$scope", "authService", "USER_ROLES", mainController])

    .run(["$rootScope", "AUTH_EVENTS", "authService", "$location", runFunction]);

}());