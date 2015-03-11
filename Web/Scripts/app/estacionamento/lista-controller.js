(function () {
    var listaController = function ($scope, Estacionamento, $state) {

        $scope.estacionamentos = Estacionamento.query();

        $scope.novoEstacionamento = function () {
            $state.go("estacionamento_add");
        };
    };

    angular.module("estacionamento").controller("listaController", ["$scope", "Estacionamento", "$state", listaController]);
}());