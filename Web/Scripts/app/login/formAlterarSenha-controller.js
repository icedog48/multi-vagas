(function () {
    var formAlterarSenhaController = function ($scope, Usuario, sessionService, authService, $state) {

        var mensagemSucesso = function () {
            alert("Operação realizada com sucesso.");

            if (authService.isAuthorized($scope.userRoles.funcionario)) {
                $state.go('movimentacao_list');
            } else {
                $state.go('estacionamento_list');
            }
        };

        var showErrorMessage = function (errCode) {
            alert("Erro inesperado.");
        };

        var alterarSenha = function (cliente) {

            cliente.Login = sessionService.user ;

            Usuario.alterarSenha(cliente).$promise.then(function (data) {
                $scope.currentUser.alterarSenha = false;

                mensagemSucesso();
            }, function (errResponse) {
                showErrorMessage(errResponse);
            });
        }

        $scope.alterarSenha = alterarSenha;
    };

    angular.module("login").controller("formAlterarSenhaController", ["$scope", "Usuario", "sessionService", "authService", "$state", formAlterarSenhaController]);
}());