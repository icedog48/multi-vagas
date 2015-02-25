(function () {
    var estacionamentoController = function ($scope, $stateParams) {

        $scope.estacionamentos = ["Estacionamento 1", "Estacionamento 2"];

        $scope.estacionamento = {
            Id: $stateParams.id,
            RazaoSocial: $stateParams.id
        };
    };

    angular.module("estacionamento").controller("estacionamentoController", ["$scope", "$stateParams", estacionamentoController]);
}());