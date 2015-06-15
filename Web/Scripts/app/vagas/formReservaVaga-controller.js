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

        console.log($stateParams);
        
        $scope.categoriasVaga = Vaga.categoriasVagaEstacionamento({ id: $stateParams.id });

        $scope.reserva = {};
        $scope.reserva.Data = new Date();

        $scope.salvar = salvar;
    };

    angular.module("vagas").controller("formReservaVagaController", ["$scope", "$stateParams", "Vaga", "$state", "$filter", formReservaVagaController]);
}());