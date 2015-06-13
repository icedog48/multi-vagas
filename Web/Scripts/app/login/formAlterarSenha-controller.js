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

        var showErrorMessage = function (errResponse) {
            if (errResponse.status == 400) {
                alert(errResponse.data.Message);
            } else {
                console.log(errResponse);

                alert("Ocorreu um erro inesperado. Por favor, contacte o administrador.");
            }
        };

        var alterarSenha = function (cliente) {

            cliente.NomeUsuario = sessionService.user;

            if (cliente.Senha == cliente.ConfirmacaoSenha) {
                Usuario.alterarSenha(cliente).$promise.then(function (data) {
                    $scope.currentUser.alterarSenha = false;

                    mensagemSucesso();
                }, function (errResponse) {
                    showErrorMessage(errResponse);
                });
            } else {
                alert("O campo senha e o campo confirmação de senha possuem valores diferentes.");
            }
        }

        $scope.cliente = {};
        $scope.alterarSenha = alterarSenha;
    };

    angular.module("login").controller("formAlterarSenhaController", ["$scope", "Usuario", "sessionService", "authService", "$state", formAlterarSenhaController]);
}());