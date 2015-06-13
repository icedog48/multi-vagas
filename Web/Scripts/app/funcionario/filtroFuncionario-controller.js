(function () {
    var filtroFuncionarioController = function ($scope, Funcionario, Estacionamento) {
        $scope.estacionamentos = Estacionamento.query();

        $scope.funcionarios = Funcionario.query();

        $scope.filtroFuncionario = { Estacionamento: null, Descricao: ''};

        $scope.filtrarFuncionarios = function (filtroFuncionario) {
            
            var filtro = {
                Estacionamento: filtroFuncionario.Estacionamento.Id,
                Matricula: filtroFuncionario.Matricula,
                Nome: filtroFuncionario.Nome
            };
            
            Funcionario.filtrar(filtro).$promise.then(function (data) {
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