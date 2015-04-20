(function () {
    var formRegistrarEntradaController = function ($scope, $state, Vaga, Movimentacao) {

        var categoriasVaga = Vaga.categoriasVaga();

        var listarVagas = function (categoriaVaga) {
            $scope.vagasDisponiveis = Vaga.vagasDisponiveis({ id: categoriaVaga });
        }

        var registrar = function (movimentacao) {
            Movimentacao.registrarEntrada(movimentacao).$promise.then(function (response) {

            }, function (errResponse) {

            });
        };

        $scope.vagasDisponiveis = [];
        $scope.categoriasVaga = categoriasVaga;
        $scope.listarVagas = listarVagas;
        $scope.registrar = registrar;
    };

    angular.module("movimentacao").controller("formRegistrarEntradaController", ["$scope", "$state", "Vaga", "Movimentacao", formRegistrarEntradaController]);
}());