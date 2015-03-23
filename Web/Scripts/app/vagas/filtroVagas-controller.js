(function () {
    var filtroVagasController = function ($scope, Vaga, Estacionamento) {
        $scope.estacionamentos = Estacionamento.query();

        $scope.vagas = Vaga.query();

        $scope.filtroVagas = { Estacionamento: null, Descricao: ''};

        $scope.filtrarVagas = function (filtroVagas) {

            filtroVagas.Estacionamento = filtroVagas.Estacionamento.Id;

            Vaga.filtrar(filtroVagas).$promise.then(function (data) {
                $scope.vagas = data;
            });
        };

        $scope.excluirVaga = function (vaga) {

            if (confirm('Deseja realmente excluir ?')) {

                var vagaId = vaga.Id;

                Vaga.remove({ id: vagaId }).$promise.then(function () {
                    $scope.vagas.forEach(function (vaga, index) {
                            if (vaga.Id == vagaId) {
                                $scope.vagas.splice(index, 1);
                            }
                        });
                    });
            };
        };

    };

    angular.module("vagas").controller("filtroVagasController", ["$scope", "Vaga", "Estacionamento", filtroVagasController]);
}());