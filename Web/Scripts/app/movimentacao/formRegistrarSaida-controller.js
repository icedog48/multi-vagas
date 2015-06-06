(function () {
    var formRegistrarSaidaController = function ($scope, $state, Vaga, Movimentacao, $stateParams, Estacionamento, $filter) {

        var categoriasVaga = Vaga.categoriasVaga();

        var mensagemSucesso = function () {
            alert("Operação realizada com sucesso.");

            $state.go("movimentacao_list");
        };

        var showErrorMessage = function (errCode) {
            alert("Erro inesperado.");
        };

        var listarVagas = function (categoriaVaga) {
            Vaga.vagasDisponiveis({ id: categoriaVaga }).$promise.then(function (data) {
                $scope.vagasDisponiveis = data;
            });
        };

        var salvar = function (movimentacao) {

            var saida = {
                Id: movimentacao.Id,
                ValorPago: movimentacao.ValorPago,
                TipoPagamento: movimentacao.TipoPagamento
            };

            Movimentacao.registrarSaida({ id: movimentacao.Id }, saida).$promise.then(function (response) {
                mensagemSucesso();
            }, function (errResponse) {
                showErrorMessage(errResponse.data.Message);
            });
        };

        var carregarDados = function (movimentacaoId) {
            Movimentacao.prepararSaida({ movimentacao: movimentacaoId }).$promise.then(function (data) {

                $scope.movimentacao = new Movimentacao(data);
                $scope.movimentacao.Entrada = $filter('date')($scope.movimentacao.Entrada, 'dd/MM/yyyy HH:mm');

            }, function (errResponse) {
                alert('Registro não encontado.');

                $state.go('movimentacao_list');
            });

            Movimentacao.listarTiposPagamento().$promise.then(function (data) {
                $scope.tiposPagamento = data;
            });
        };

        carregarDados($stateParams.id);

        $scope.salvar = salvar;
    };

    angular.module("movimentacao").controller("formRegistrarSaidaController", ["$scope", "$state", "Vaga", "Movimentacao", "$stateParams", "Estacionamento", "$filter", formRegistrarSaidaController]);
}());