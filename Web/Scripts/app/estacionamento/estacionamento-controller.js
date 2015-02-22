(function () {
    var estacionamentoController = function ($scope, $stateParams) {

        $scope.estacionamentos = ["Estacionamento 1", "Estacionamento 2"];

        $scope.estacionamento = $stateParams.id;

        
    };

    angular.module("estacionamento").controller("estacionamentoController", ["$scope", "$stateParams", estacionamentoController]);
}());