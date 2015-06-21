(function () {
    var filtroMovimentacaoPorPeriodoController = function ($scope, Movimentacao, Estacionamento, printHelper) {

        var filtrar = function (filtro) {

            var obj = {
                Estacionamento: (filtro.Estacionamento) ? filtro.Estacionamento.Id : null,
                DataInicial: filtro.DataInicial,
                DataFinal: filtro.DataFinal
            };

            Movimentacao.filtrarPorPeriodo(obj).$promise.then(function (data) {
                $scope.movimentacoes = data;
            }, function (err) {
                console.log(err);
            });

        };

        var printDiv = function (divName) {
            var templateUrl = 'Scripts/app/relatorios/movimentacaoPorPeriodo-template.html';

            var data = {
                movimentacoes: $scope.movimentacoes,
                filtro: $scope.filtro
            };

            return printHelper.printTemplate(templateUrl, data);
        };

        var calcularFaturamentoTotal = function (movimentacoes) {
            var faturamentoTotal = 0;

            if (movimentacoes.length > 0) {
                movimentacoes.forEach(function (movimentacao) {
                    faturamentoTotal += parseFloat(movimentacao.ValorPago) || 0;
                });
            }

            return faturamentoTotal;
        };

        $scope.estacionamentos = Estacionamento.query();
        $scope.movimentacoes = [];

        $scope.filtro = {};
        $scope.filtro.DataInicial = new Date();
        $scope.filtro.DataFinal = new Date();

        $scope.calcularFaturamentoTotal = calcularFaturamentoTotal;

        $scope.filtrar = filtrar;
        $scope.filtrar($scope.filtro);
        $scope.printDiv = printDiv;
    };

    angular.module("relatorios").controller("filtroMovimentacaoPorPeriodoController", ["$scope", "Movimentacao", "Estacionamento", "printHelper", filtroMovimentacaoPorPeriodoController]);
}());