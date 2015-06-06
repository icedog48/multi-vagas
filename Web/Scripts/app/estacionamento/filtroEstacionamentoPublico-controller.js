(function () {
    var filtroEstacionamentoPublicoController = function ($scope, Estacionamento, $state) {
        $scope.filtroEstacionamento = {};

        $scope.filtrarEstacionamentos = function (filtroEstacionamento) {
            Estacionamento.filtrar(filtroEstacionamento).$promise.then(function (data) {
                $scope.estacionamentos = data;
            }, function (err) {
                console.log(err);
            });

        };

        $scope.filtrarEstacionamentos($scope.filtrarEstacionamentos);
    };

    angular.module("estacionamento").controller("filtroEstacionamentoPublicoController", ["$scope", "Estacionamento", "$state", filtroEstacionamentoPublicoController]);
}());