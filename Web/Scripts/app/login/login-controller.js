(function () {
    var loginController = function ($scope, authService, $rootScope, $state) {

        $scope.credentials = {
            Login: '',
            Senha: ''
        };

        $scope.login = function (credentials) {
            authService.login(credentials).then(function (user) {
                $scope.setCurrentUser(user);
                
                $state.go("estacionamento_list");

            });
        };
    };

    angular.module("login").controller("loginController", ["$scope", "authService", "$rootScope", "$state", loginController]);
}());