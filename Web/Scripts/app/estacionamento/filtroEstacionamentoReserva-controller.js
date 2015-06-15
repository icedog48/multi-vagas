(function () {
    var filtroEstacionamentoReservaController = function ($scope, Estacionamento, $state) {
        $scope.filtroEstacionamento = { };

        var filtrarEstacionamentos = function (filtroEstacionamento) {
            if (!filtroEstacionamento) {
                filtroEstacionamento = { };
            }

            filtroEstacionamento.PermiteReserva = true;

            Estacionamento.filtrar(filtroEstacionamento).$promise.then(function (data) {
                $scope.estacionamentos = data;

                console.log($scope.estacionamentos);
            }, function (err) {
                console.log(err);
            });

        };

        filtrarEstacionamentos($scope.filtroEstacionamento);

        $scope.filtrarEstacionamentos = filtrarEstacionamentos;
    };

    angular.module("estacionamento").controller("filtroEstacionamentoReservaController", ["$scope", "Estacionamento", "$state", filtroEstacionamentoReservaController]);
}());