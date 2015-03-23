(function () {
    var formFuncionarioController = function ($scope, $stateParams, Funcionario, $state, Estacionamento, Perfil) {

        var novoCadastro = (typeof ($stateParams.id) == 'undefined');

        var funcionario = {};

        var carregarDados = function (funcionarioId) {
            Funcionario.get({ id: funcionarioId }).$promise.then(function (data) {                
                $scope.funcionario = new Funcionario(data);

                $scope.funcionario.DataAdmissao = new Date($scope.funcionario.DataAdmissao);

            }, function (errResponse) {
                alert('Registro não encontado.');

                $state.go('funcionario_list');
            });
        };

        var mensagemSucesso = function () {
            alert("Operação realizada com sucesso.");

            $state.go("funcionario_list");
        };

        var cadastrar = function (funcionario) {
            Funcionario.add(funcionario).$promise.then(function (data) {
                mensagemSucesso();
            });
        };

        var atualizar = function (funcionario) {
            Funcionario.update({id: funcionario.Id}, funcionario).$promise.then(function (data) {
                mensagemSucesso();
            });
        };

        if (novoCadastro) {
            $scope.funcionario = new Funcionario();
        } else {
            carregarDados($stateParams.id);
        }

        $scope.novoCadastro = novoCadastro;

        Estacionamento.query().$promise.then(function (data) {
            $scope.estacionamentos = data;
        });

        Perfil.perfisFuncionario().$promise.then(function (data) {
            $scope.perfis = data;
        });

        $scope.salvar = function (funcionario)
        {
            if ($scope.novoCadastro) {
                cadastrar(funcionario);
            } else {
                atualizar(funcionario)
            }
        };
    };

    angular.module("funcionario").controller("formFuncionarioController", ["$scope", "$stateParams", "Funcionario", "$state", "Estacionamento", "Perfil", formFuncionarioController]);
}());