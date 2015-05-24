(function () {
    var filtroEstacionamentoReserva = function ($scope, Estacionamento, $state) {
        $scope.filtroEstacionamento = { };

        var filtrarEstacionamentos = function (filtroEstacionamento) {
            Estacionamento.filtrar(filtroEstacionamento).$promise.then(function (data) {
                $scope.estacionamentos = data;
            }, function (err) {
                console.log(err);
            });

        };

        filtrarEstacionamentos($scope.filtroEstacionamento);

        $scope.filtrarEstacionamentos = filtrarEstacionamentos;
    };

    angular.module("estacionamento").controller("filtroEstacionamentoReserva", ["$scope", "Estacionamento", "$state", filtroEstacionamentoReserva]);
}());