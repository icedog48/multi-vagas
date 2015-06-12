(function () {
    var formFuncionarioController = function ($scope, $stateParams, Funcionario, $state, Estacionamento, Perfil, $filter) {

        var novoCadastro = (typeof ($stateParams.id) == 'undefined');

        var funcionario = {};

        var carregarDados = function (funcionarioId) {
            Funcionario.get({ id: funcionarioId }).$promise.then(function (data) {

                data.HoraInicio = new Date(Date.parse(data.HoraInicio));
                data.HoraSaida = new Date(Date.parse(data.HoraSaida));

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

        var mensagemErro = function (errResponse) {
            if (errResponse.status == 500) {
                alert(errResponse.data.Message);
            } else {
                console.log(errResponse);

                alert("Ocorreu um erro inesperado. Por favor, contacte o administrador.");
            }
        }

        var cadastrar = function (funcionario) {
            Funcionario.add(funcionario).$promise.then(function (data) {
                mensagemSucesso();
            }, function (errResponse) {
                mensagemErro(errResponse);
            });
        };

        var atualizar = function (funcionario) {
            Funcionario.update({id: funcionario.Id}, funcionario).$promise.then(function (data) {
                mensagemSucesso();
            }, function (errResponse) {
                mensagemErro(errResponse);
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

        $scope.salvar = function (funcionario)
        {
            if ($scope.novoCadastro) {
                cadastrar(funcionario);
            } else {
                atualizar(funcionario)
            }
        };

        var inputNumber = function (newValue, oldValue) {
            if (typeof (newValue) !== 'undefined') {

                var numberPattern = /^\d+$/;

                var isValid = true;

                for (var i = 0; i < newValue.length; i++) {

                    isValid = numberPattern.test(newValue.charAt(i));

                    if (!isValid) break;
                }

                if (isValid) {
                    $scope.funcionario.Matricula = newValue;
                } else {
                    $scope.funcionario.Matricula = oldValue ? oldValue : '';
                }
            }
        };

        var inputCpf = function (newValue, oldValue) {
            var isValid = true;

            if (typeof (newValue) !== 'undefined') {
                isValid = newValue.length == 11;
            }

            $scope.cpfInvalido = !isValid;
        };

        $scope.$watch('funcionario.Matricula', inputNumber, true);
        $scope.$watch('funcionario.CPF', inputCpf, true);
    };

    angular.module("funcionario").controller("formFuncionarioController", ["$scope", "$stateParams", "Funcionario", "$state", "Estacionamento", "Perfil", "$filter", formFuncionarioController]);
}());