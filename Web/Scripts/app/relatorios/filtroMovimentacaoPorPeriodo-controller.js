(function () {
    var filtroMovimentacaoPorPeriodoController = function ($scope, Movimentacao, Estacionamento, printHelper) {

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

        var filtrar = function (filtro) {

            Movimentacao.filtrarPorPeriodo(filtro).$promise.then(function (data) {
                
                $scope.movimentacoes = data;
            }, function (err) {
                console.log(err);
            });

        };

        var printDiv = function (divName) {

            //var content = document.getElementById(divName).innerHTML;
            //var templateUrl = 'Scripts/app/relatorios/report-template.html';

            //return printHelper.printContent(templateUrl, content);

            var templateUrl = 'Scripts/app/relatorios/movimentacaoPorPeriodo-template.html';

            var data = {
                movimentacoes: $scope.movimentacoes,
                filtro: $scope.filtro
            };

            return printHelper.printTemplate(templateUrl, data);
        };

        $scope.estacionamentos = Estacionamento.query();

        $scope.filtro = {};
        $scope.filtro.DataInicial = new Date();
        $scope.filtro.DataFinal = new Date();

        $scope.filtrar = filtrar;
        $scope.filtrar($scope.filtro);
        $scope.printDiv = printDiv;
    };

    angular.module("relatorios").controller("filtroMovimentacaoPorPeriodoController", ["$scope", "Movimentacao", "Estacionamento", "printHelper", filtroMovimentacaoPorPeriodoController]);
}());