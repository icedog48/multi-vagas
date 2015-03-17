(function () {
    var listaController = function ($scope, Estacionamento, $state) {
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

        $scope.atualizarEstacionamento = function (estacionamento) {
            $state.go("estacionamento_edit", { id: estacionamento.Id, notify: false });
        };

        $scope.excluriEstacionamento = function (estacionamento) {
            
        };
    };

    angular.module("estacionamento").controller("listaController", ["$scope", "Estacionamento", "$state", listaController]);
}());