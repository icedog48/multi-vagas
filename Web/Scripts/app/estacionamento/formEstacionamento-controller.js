(function () {
    var formEstacionamentoController = function ($scope, Estacionamento, $state, $stateParams, $modal, Usuario) {

        $scope.novoCadastro = (typeof ($stateParams.id) == 'undefined');

        if ($scope.novoCadastro) {
            $scope.estacionamento = new Estacionamento();
        } else {
            Estacionamento.get({ id: $stateParams.id }).$promise.then(function (data) {
                $scope.estacionamento = new Estacionamento(data);
            }, function (errResponse) {
                alert('Estacionamento não encontrado.');

                $state.go('estacionamento_list');
            });
        }

        $scope.cadastrar = function (estacionamento) {
            Estacionamento.add(estacionamento).$promise
                .then(function (response) {
                    alert('Operação realizada com sucesso');

                    $state.go('estacionamento_list');
                })
                .catch(function (errResponse) {
                    console.log(errResponse);
                });
        };

        $scope.atualizar = function (estacionamento) {
            Estacionamento.update({ id: estacionamento.Id }, estacionamento).$promise
                .then(function (response) {
                    alert('Operação realizada com sucesso!');

                    $state.go('estacionamento_list');
                })
                .catch(function (errResponse) {
                    console.log(errResponse);
                });
        };

        $scope.listar = function () {
            $state.go("estacionamento_list");
        };

        $scope.salvar = function (estacionamento) {
            if ($scope.novoCadastro) {
                $scope.cadastrar(estacionamento);
            } else {
                $scope.atualizar(estacionamento);
            }
        };

        var limparUsuario = function () {
            var login = $scope.estacionamento.Usuario.Login;

            $scope.estacionamento.Usuario = {};
            $scope.estacionamento.Usuario.Login = login;
            $scope.estacionamento.Usuario.Email = "";
        }

        $scope.carregarUsuario = function () {
            Estacionamento.verificaLogin({ login: $scope.estacionamento.Usuario.Login }).$promise
                .then(function (data) {
                    if (typeof (data.Id) == "undefined") {
                        limparUsuario();
                    } else {
                        $scope.estacionamento.Usuario = new Usuario(data);
                    }                    
                }, function (err) {
                    limparUsuario();

                    $scope.frmEstacionamento.Login.$invalid = true;

                    alert(err.data.Message);
                });
        }
    };

    angular.module("estacionamento").controller("formEstacionamentoController", ["$scope", "Estacionamento", "$state", "$stateParams", "$modal", "Usuario", formEstacionamentoController]);
}());