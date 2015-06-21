(function () {
    var filtroMovimentacaoPorEstadiaController = function ($scope, Movimentacao, Estacionamento, printHelper, Vaga) {

        var listarCategoriaVaga = function (estacionamento) {
            $scope.categoriasVaga = Vaga.categoriasVagaEstacionamento({ id: estacionamento.Id});
        };

        var filtrar = function (filtro) {
            
            var obj = {
                Estacionamento: (filtro.Estacionamento) ? filtro.Estacionamento.Id : null,
                HoraInicio: filtro.HoraInicio,
                HoraFim: filtro.HoraFim
            };

            Movimentacao.filtrarPorEstadia(obj).$promise.then(function (data) {
                $scope.movimentacoes = data;
            }, function (err) {
                console.log(err);
            });

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

        var printDiv = function (divName) {
            var templateUrl = 'Scripts/app/relatorios/movimentacaoPorEstadia-template.html';

            var data = {
                movimentacoes: $scope.movimentacoes,
                filtro: $scope.filtro
            };

            return printHelper.printTemplate(templateUrl, data);
        };

        $scope.listarCategoriaVaga = listarCategoriaVaga;
        $scope.estacionamentos = Estacionamento.query();
        $scope.categoriasVaga = { };

        $scope.filtro = {};
        $scope.filtro.DataInicial = new Date();
        $scope.filtro.DataFinal = new Date();
        $scope.calcularFaturamentoTotal = calcularFaturamentoTotal;

        $scope.filtrar = filtrar;
        $scope.filtrar($scope.filtro);
        $scope.printDiv = printDiv;
    };

    angular.module("relatorios").controller("filtroMovimentacaoPorEstadiaController", ["$scope", "Movimentacao", "Estacionamento", "printHelper", "Vaga", filtroMovimentacaoPorEstadiaController]);
}());