(function () {
    var filtroEstacionamentoController = function ($scope, Estacionamento, $state) {
        $scope.filtroEstacionamento = {};

        $scope.estacionamentos = Estacionamento.query();

        $scope.filtrarEstacionamentos = function (filtroEstacionamento) {
            Estacionamento.filtrar(filtroEstacionamento).$promise.then(function (data) {                
                $scope.estacionamentos = data;
            }, function (err) {
                console.log(err);
            });
        };

        $scope.novoEstacionamento = function () {
            $state.go("estacionamento_add");
        };        

        $scope.excluirEstacionamento = function (estacionamento) {

            if (confirm('Deseja realmente excluir ?')) {

                var estacionamentoId = estacionamento.Id;

                Estacionamento.remove({ id: estacionamentoId }).$promise
                    .then(function () {
                        $scope.estacionamentos.forEach(function (estacionamento, index) {

                            if (estacionamento.Id == estacionamentoId) {

                                console.log(estacionamento);

                                $scope.estacionamentos.splice(index, 1);
                            }
                        });

                        $state.go("estacionamento_list");
                    });
            };
        };
    };

    angular.module("estacionamento").controller("filtroEstacionamentoController", ["$scope", "Estacionamento", "$state", filtroEstacionamentoController]);
}());