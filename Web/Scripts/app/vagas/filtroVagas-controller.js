(function () {
    var filtroVagasController = function ($scope, CategoriaVaga, Estacionamento) {
        $scope.estacionamentos = Estacionamento.query();

        $scope.vagas = CategoriaVaga.query();

        $scope.filtroVagas = { Estacionamento: null, Descricao: ''};

        $scope.filtrarVagas = function (filtroVagas) {
            CategoriaVaga.filtrar(filtroVagas).$promise.then(function (data) {
                $scope.vagas = data;
            });
        };

        $scope.excluirVaga = function (vaga) {

            if (confirm('Deseja realmente excluir ?')) {

                var vagaId = vaga.Id;

                CategoriaVaga.remove({ id: vagaId }).$promise.then(function () {
                    $scope.vagas.forEach(function (vaga, index) {
                            if (vaga.Id == vagaId) {
                                $scope.vagas.splice(index, 1);
                            }
                        });
                    });
            };
        };

    };

    angular.module("vagas").controller("filtroVagasController", ["$scope", "CategoriaVaga", "Estacionamento", filtroVagasController]);
}());