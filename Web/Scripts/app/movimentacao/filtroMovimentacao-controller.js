(function () {
    var filtroMovimentacaoController = function ($scope, Movimentacao, $state) {

        var mensagemSucesso = function () {
            alert("Operação realizada com sucesso.");

            $scope.filtrar($scope.filtroMovimentacao);
        };

        var registrarEntradaExpressa = function () {

            Movimentacao.registrarEntradaExpressa().$promise.then(function (response) {
                mensagemSucesso();
            }, function (errResponse) {
                showErrorMessage(errResponse.data.Message);
            });

        };

        var filtrar = function (filtroMovimentacao) {
            Movimentacao.filtrar(filtroMovimentacao).$promise.then(function (data) {
                $scope.movimentacoes = data;
            }, function (err) {
                console.log(err);
            });
        };

        $scope.filtroMovimentacao = { };

        $scope.filtrar = filtrar;

        $scope.filtrar($scope.filtroMovimentacao);

        $scope.registrarEntradaExpressa = registrarEntradaExpressa;
    };

    angular.module("movimentacao").controller("filtroMovimentacaoController", ["$scope", "Movimentacao", "$state", filtroMovimentacaoController]);
}());