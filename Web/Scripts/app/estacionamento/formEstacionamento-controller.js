(function () {
    var formEstacionamentoController = function ($scope, Estacionamento, $state, $stateParams, $modal, Usuario) {

        $scope.novoCadastro = (typeof ($stateParams.id) == 'undefined');

        if ($scope.novoCadastro) {
            $scope.estacionamento = new Estacionamento();
        } else {
            Estacionamento.get({ id: $stateParams.id }).$promise.then(function (data) {
                $scope.estacionamento = new Estacionamento(data);
            }, function (errResponse) {
                alert('Estacionamento não encontrado.');

                $state.go('estacionamento_list');
            });
        }

        $scope.cadastrar = function (estacionamento) {
            Estacionamento.add(estacionamento).$promise
                .then(function (response) {
                    alert('Operação realizada com sucesso');

                    $state.go('estacionamento_list');
                })
                .catch(function (errResponse) {
                    showErrorMessage(errResponse);
                });
        };

        $scope.atualizar = function (estacionamento) {
            Estacionamento.update({ id: estacionamento.Id }, estacionamento).$promise
                .then(function (response) {
                    alert('Operação realizada com sucesso!');

                    $state.go('estacionamento_list');
                })
                .catch(function (errResponse) {
                    showErrorMessage(errResponse);
                });
        };

        $scope.listar = function () {
            $state.go("estacionamento_list");
        };

        $scope.salvar = function (estacionamento) {
            if ($scope.novoCadastro) {
                $scope.cadastrar(estacionamento);
            } else {
                $scope.atualizar(estacionamento);
            }
        };

        var limparUsuario = function () {
            var NomeUsuario = $scope.estacionamento.Usuario.NomeUsuario;

            $scope.estacionamento.Usuario = {};
            $scope.estacionamento.Usuario.NomeUsuario = "";
            $scope.estacionamento.Usuario.Email = "";
        }

        var showErrorMessage = function (errResponse) {
            if (errResponse.status == 400) {
                alert(errResponse.data.Message);
            } else {
                console.log(errResponse);

                alert("Ocorreu um erro inesperado. Por favor, contacte o administrador.");
            }
        };
    };

    angular.module("estacionamento").controller("formEstacionamentoController", ["$scope", "Estacionamento", "$state", "$stateParams", "$modal", "Usuario", formEstacionamentoController]);
}());