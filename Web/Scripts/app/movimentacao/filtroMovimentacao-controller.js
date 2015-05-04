(function () {
    var filtroMovimentacaoController = function ($scope, Movimentacao, $state) {

        $scope.filtroMovimentacao = {};

        $scope.filtrar = function (filtroMovimentacao) {
            Movimentacao.filtrar(filtroMovimentacao).$promise.then(function (data) {
                $scope.movimentacoes = data;
            }, function (err) {
                console.log(err);
            });

        };

        $scope.novaMovimentacao = function () {
            $state.go("registrar_entrada");
        };        

        $scope.atualizar = function () {

        };

        $scope.filtrar($scope.filtroMovimentacao);
    };

    angular.module("movimentacao").controller("filtroMovimentacaoController", ["$scope", "Movimentacao", "$state", filtroMovimentacaoController]);
}());