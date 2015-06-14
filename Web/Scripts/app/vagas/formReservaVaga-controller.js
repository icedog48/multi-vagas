(function () {
    var formReservaVagaController = function ($scope, $stateParams, Vaga, $state, $filter) {

        var mensagemSucesso = function () {
            alert("Operação realizada com sucesso.");

            $state.go("estacionamento_reserva_list");
        };

        var showErrorMessage = function (errResponse) {
            if (errResponse.status == 400) {
                alert(errResponse.data.Message);
            } else {
                console.log(errResponse);

                alert("Ocorreu um erro inesperado. Por favor, contacte o administrador.");
            }
        };

        var salvar = function (reserva) {

            Vaga.reservarVaga(reserva).$promise.then(function (response) {
                mensagemSucesso();
            }, function (errResponse) {
                showErrorMessage(errResponse);
            });

        };

        $scope.categoriasVaga = Vaga.categoriasVagaEstacionamento($stateParams.id);

        $scope.reserva = {};
        $scope.reserva.Data = new Date();

        $scope.salvar = salvar;

        $scope.$watch('reserva.CartaoCredito.Numero', function (newValue, oldValue) {

            if (typeof (newValue) !== 'undefined') {

                var numberPattern = /^\d+$/;

                var isValid = true;

                for (var i = 0; i < newValue.length; i++) {                    
                    isValid = numberPattern.test(newValue.charAt(i));

                    if (!isValid) break;
                }

                if (isValid) {
                    $scope.reserva.CartaoCredito.Numero = newValue.toUpperCase();
                } else {
                    $scope.reserva.CartaoCredito.Numero = oldValue ? oldValue.toUpperCase() : '';
                }
            }
        }, true);
    };

    angular.module("vagas").controller("formReservaVagaController", ["$scope", "$stateParams", "Vaga", "$state", "$filter", formReservaVagaController]);
}());