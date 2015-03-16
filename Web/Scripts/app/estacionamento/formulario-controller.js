(function () {
    var formularioController = function ($scope, Estacionamento, $state, $stateParams, $modal) {

        var novoCadastro = (typeof ($stateParams.id) == 'undefined');

        if (novoCadastro) {
            $scope.estacionamento = new Estacionamento();
        } else {
            Estacionamento.get({ id: $stateParams.id }).$promise.then(function (data) {
                $scope.estacionamento = new Estacionamento(data);
            }, function (errResponse) {
                alert('Estacionamento não encontrado');

                $state.go('estacionamento_list');
            });
        }

        $scope.cadastrar = function () {
            Estacionamento.add($scope.estacionamento).$promise
                .then(function (response) {
                    alert('Operação realizada com sucesso');

                    $state.go('estacionamento_list');
                })
                .catch(function (errResponse) {
                    console.log(errResponse);
                });
        };

        $scope.atualizar = function () {
            Estacionamento.update( {id: $scope.estacionamento.Id}, $scope.estacionamento).$promise
                .then(function (response) {
                    alert('Operação realizada com sucesso');

                    $state.go('estacionamento_list');
                })
                .catch(function (errResponse) {
                    console.log(errResponse);
                });
        };

        $scope.listar = function () {
            $state.go("estacionamento_list");
        };

        $scope.excluir = function () {

            if (confirm('Deseja realmente excluir ?')) {

                var estacionamentoId = $scope.estacionamento.Id;

                $scope.estacionamento.$delete({ id: estacionamentoId })
                    .then(function () {
                        $scope.estacionamentos.forEach(function (estacionamento, index) {
                            
                            if (estacionamento.Id == estacionamentoId) {

                                console.log(estacionamento);

                                $scope.estacionamentos.splice(index, 1);
                            }
                        });

                        $state.go("estacionamento_list");
                    });
                    
            }
        };

        $scope.salvar = function () {

            if (typeof ($scope.estacionamento.Id) !== 'undefined') {
                $scope.atualizar();
            } else {
                $scope.cadastrar();
            }
        };

        $scope.IsInvalid = function (formName, field) {
            if (typeof ($scope[formName][field]) == 'undefined') throw "Field without name property: " + field;

            var isInvalid = { invalid_field: $scope[formName][field].$dirty && $scope[formName][field].$invalid };

            return isInvalid;
        }

        $scope.novaCategoria = function () {

            var modalInstance = $modal.open({
                templateUrl: 'Scripts/app/estacionamento/categoria-vagas/formulario.html',
                controller: 'categoriaVagasController',
                backdrop: 'static',
                size: 'lg',
                resolve: {
                    categoriaVaga: null
                }
            });

        };

        $scope.novoFuncionario = function () {

            var modalInstance = $modal.open({
                templateUrl: 'Scripts/app/estacionamento/funcionario/formulario.html',
                controller: 'funcionarioController',
                backdrop: 'static',
                size: 'lg',
                resolve: {
                    funcionario: null
                }
            });

        };
    };

    angular.module("estacionamento").controller("formularioController", ["$scope", "Estacionamento", "$state", "$stateParams", "$modal", formularioController]);
}());