(function () {
    var formReservaVagaController = function ($scope, $stateParams, Vaga, $state, $filter) {

        var mensagemSucesso = function () {
            alert("Operação realizada com sucesso.");

            $state.go("estacionamento_reserva_list");
        };

        var showErrorMessage = function (errCode) {
            alert("Erro inesperado.");
        };

        var salvar = function (reserva) {

            Vaga.reservarVaga(reserva).$promise.then(function (response) {
                mensagemSucesso();
            }, function (errResponse) {
                showErrorMessage(errResponse.data.Message);
            });

        };

        $scope.categoriasVaga = Vaga.categoriasVagaEstacionamento($stateParams.id);

        $scope.reserva = {};
        $scope.reserva.Data = new Date();

        $scope.salvar = salvar;
    };

    angular.module("vagas").controller("formReservaVagaController", ["$scope", "$stateParams", "Vaga", "$state", "$filter", formReservaVagaController]);
}());