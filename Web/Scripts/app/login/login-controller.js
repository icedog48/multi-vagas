(function () {
    var loginController = function ($scope, authService, $rootScope, AUTH_EVENTS, $state) {

        $scope.credentials = {
            username: '',
            password: ''
        };

        $scope.login = function (credentials) {

            authService.login(credentials).then(function (user) {
                $scope.setCurrentUser(user);
                
                $state.go("estacionamentos");

            });
        };
    };

    angular.module("login").controller("loginController", ["$scope", "authService", "$rootScope", "AUTH_EVENTS", "$state", loginController]);
}());