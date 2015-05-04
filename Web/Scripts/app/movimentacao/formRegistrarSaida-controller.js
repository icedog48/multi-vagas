(function () {
    var formRegistrarSaidaController = function ($scope, $state, Vaga, Movimentacao, $stateParams, Estacionamento) {

        var categoriasVaga = Vaga.categoriasVaga();

        var mensagemSucesso = function () {
            alert("Operação realizada com sucesso.");

            $state.go("movimentacao_list");
        };

        var showErrorMessage = function (errCode) {
            alert("Erro inesperado.");
        }

        var listarVagas = function (categoriaVaga) {
            Vaga.vagasDisponiveis({ id: categoriaVaga }).$promise.then(function (data) {
                $scope.vagasDisponiveis = data;
            });
        }

        var salvar = function (movimentacao) {
            
        };

        carregarDados($stateParams.id);

        $scope.categoriasVaga = categoriasVaga;
        $scope.listarVagas = listarVagas;
        $scope.salvar = salvar;
    };

    angular.module("movimentacao").controller("formRegistrarSaidaController", ["$scope", "$state", "Vaga", "Movimentacao", "$stateParams", "Estacionamento", formRegistrarSaidaController]);
}());