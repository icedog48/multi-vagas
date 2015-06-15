﻿(function () {
    var filtroMovimentacaoPorCategoriaController = function ($scope, Movimentacao, Estacionamento, printHelper, Vaga) {
        
        var listarCategoriaVaga = function (estacionamento) {
            $scope.filtro.CategoriaVaga = null;

            if (estacionamento) {
                $scope.categoriasVaga = Vaga.categoriasVagaEstacionamento({ id: estacionamento.Id });
            } else {
                $scope.categoriasVaga = [];                
            }
            
        };

        var filtrar = function (filtro) {

            var obj = {
                Estacionamento: (filtro.Estacionamento) ? filtro.Estacionamento.Id : null,
                Categoria: (filtro.CategoriaVaga) ? filtro.CategoriaVaga : null
            };

            Movimentacao.filtrarPorCategoria(obj).$promise.then(function (data) {
                $scope.movimentacoes = data;
            }, function (err) {
                console.log(err);
            });

        };

        var printDiv = function (divName) {
            var templateUrl = 'Scripts/app/relatorios/movimentacaoPorCategoria-template.html';

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

        $scope.filtrar = filtrar;
        $scope.filtrar($scope.filtro);
        $scope.printDiv = printDiv;
    };

    angular.module("relatorios").controller("filtroMovimentacaoPorCategoriaController", ["$scope", "Movimentacao", "Estacionamento", "printHelper", "Vaga", filtroMovimentacaoPorCategoriaController]);
}());