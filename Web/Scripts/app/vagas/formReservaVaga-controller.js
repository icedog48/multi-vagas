(function () {
    var formReservaVagaController = function ($scope, $stateParams, Vaga, $state, $filter, printHelper) {

        var mensagemSucesso = function () {
            alert("Operação realizada com sucesso. Por favor imprima seu ticket de acesso.");

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

        var emitirTicket = function (entrada) {
            var templateUrl = 'Scripts/app/movimentacao/ticket-template.html';

            var data = {
                movimentacao: entrada
            };

            return printHelper.printTemplate(templateUrl, data);
        };

        var salvar = function (reserva) {
            
            var objReserva = JSON.parse(JSON.stringify(reserva));
            objReserva.CategoriaVaga = reserva.CategoriaVaga.Id;

            Vaga.reservarVaga(objReserva).$promise.then(function (response) {

                mensagemSucesso();

                emitirTicket(response);

            }, function (errResponse) {
                showErrorMessage(errResponse);
            });

        };

        var calcularValorAPagar = function (categoriaVaga) {
            console.log(categoriaVaga);

            var valorAPagar = categoriaVaga.ValorHora * 8;

            $scope.reserva.ValorAPagar = valorAPagar;
            $scope.reserva.ValorAPagarFormatado = $filter('currency')(valorAPagar, "R$ ");
        };
        
        $scope.categoriasVaga = Vaga.categoriasVagaEstacionamento({ id: $stateParams.id });

        $scope.reserva = {};
        $scope.reserva.Data = new Date();

        $scope.salvar = salvar;
        $scope.calcularValorAPagar = calcularValorAPagar;

        $scope.$watch('reserva.Placa', function (newValue, oldValue) {

            if (typeof (newValue) !== 'undefined') {

                var numberPattern = /^\d+$/;
                var letterPattern = /^[a-zA-Z]+$/;

                var isValid = true;

                for (var i = 0; i < newValue.length; i++) {
                    if (i < 3) {
                        isValid = letterPattern.test(newValue.charAt(i));
                    } else {
                        isValid = numberPattern.test(newValue.charAt(i));
                    }

                    if (!isValid) break;
                }

                if (isValid) {
                    $scope.reserva.Placa = newValue.toUpperCase();
                } else {
                    $scope.reserva.Placa = oldValue ? oldValue.toUpperCase() : '';
                }
            }
        }, true);
    };

    angular.module("vagas").controller("formReservaVagaController", ["$scope", "$stateParams", "Vaga", "$state", "$filter", "printHelper", formReservaVagaController]);
}());