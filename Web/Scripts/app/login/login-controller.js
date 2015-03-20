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

            }, function (err) {

                if (err.data.error == "invalid_grant") {
                    alert("Usuário ou senha incorreta.");
                }

            });
        };
    };

    angular.module("login").controller("loginController", ["$scope", "authService", "$rootScope", "$state", loginController]);
}());