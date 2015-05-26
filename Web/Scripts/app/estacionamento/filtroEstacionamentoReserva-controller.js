(function () {
    var filtroEstacionamentoReservaController = function ($scope, Estacionamento, $state) {
        $scope.filtroEstacionamento = { };

        var filtrarEstacionamentos = function (filtroEstacionamento) {

            console.log(filtroEstacionamento);

            Estacionamento.filtrar(filtroEstacionamento).$promise.then(function (data) {
                $scope.estacionamentos = data;
            }, function (err) {
                console.log(err);
            });

        };

        filtrarEstacionamentos($scope.filtroEstacionamento);

        $scope.filtrarEstacionamentos = filtrarEstacionamentos;
    };

    angular.module("estacionamento").controller("filtroEstacionamentoReservaController", ["$scope", "Estacionamento", "$state", filtroEstacionamentoReservaController]);
}());