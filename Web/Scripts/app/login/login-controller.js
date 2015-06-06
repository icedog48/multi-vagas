(function () {
    var loginController = function ($scope, authService, $rootScope, $state) {

        $scope.credentials = {
            Login: '',
            Senha: ''
        };

        $scope.login = function (credentials) {
            authService.login(credentials).then(function (user) {
                console.log(user);

                $scope.setCurrentUser(user);

                if (user.AlterarSenha) {
                    $state.go('alterar_senha');
                } else if (authService.isAuthorized($scope.userRoles.funcionario)) {
                    $state.go('movimentacao_list');
                } else if (authService.isAuthorized($scope.userRoles.usuario)) {
                    $state.go('estacionamento_reserva_list');
                } else {
                    $state.go('estacionamento_list');
                }

            }, function (err) {

                if (err.data.error == "invalid_grant") {
                    alert("Usuário ou senha incorreta.");
                }

            });
        };
    };

    angular.module("login").controller("loginController", ["$scope", "authService", "$rootScope", "$state", loginController]);
}());