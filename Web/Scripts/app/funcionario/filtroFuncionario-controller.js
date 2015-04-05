(function () {
    var filtroFuncionarioController = function ($scope, Funcionario, Estacionamento) {
        $scope.estacionamentos = Estacionamento.query();

        $scope.funcionarios = Funcionario.query();

        $scope.filtroFuncionario = { Estacionamento: null, Descricao: ''};

        $scope.filtrarFuncionarios = function (filtroFuncionario) {
            Funcionario.filtrar(filtroFuncionario).$promise.then(function (data) {
                $scope.funcionarios = data;
            });
        };

        $scope.excluirFuncionario = function (funcionario) {

            if (confirm('Deseja realmente excluir ?')) {

                var funcionarioId = funcionario.Id;

                Funcionario.remove({ id: funcionarioId }).$promise.then(function () {
                    $scope.funcionarios.forEach(function (funcionario, index) {
                            if (funcionario.Id == funcionarioId) {
                                $scope.funcionarios.splice(index, 1);
                            }
                        });
                    });
            };
        };

    };

    angular.module("funcionario").controller("filtroFuncionarioController", ["$scope", "Funcionario", "Estacionamento", filtroFuncionarioController]);
}());