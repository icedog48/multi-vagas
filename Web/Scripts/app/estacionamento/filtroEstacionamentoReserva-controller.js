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

        var reservarVaga = function (estacionamento) {
            if (estacionamento.VagasDisponiveis <= 0) {
                alert('O estacionamento selecionado não possui vagas disponíveis. Por favor selecione outro estacionamento.');
            } else {
                $state.go('reservar_vaga', { id: estacionamento.Id });
            }
        };

        filtrarEstacionamentos($scope.filtroEstacionamento);

        $scope.filtrarEstacionamentos = filtrarEstacionamentos;
        $scope.reservarVaga = reservarVaga;
    };

    angular.module("estacionamento").controller("filtroEstacionamentoReservaController", ["$scope", "Estacionamento", "$state", filtroEstacionamentoReservaController]);
}());