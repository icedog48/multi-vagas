(function () {
    var estacionamentoController = function ($scope, $stateParams, Estacionamento, $state) {

        $scope.estacionamentos = Estacionamento.query();

        if (typeof ($stateParams.id) !== 'undefined') {
            
            Estacionamento.get({ id: $stateParams.id }).$promise.then(function (data) {
                $scope.estacionamento = new Estacionamento(data);
            }, function (errResponse) {
                console.log(errResponse);

                $state.go("estacionamento_list");
            });
            
        } else {
            $scope.estacionamento = new Estacionamento();
        }

        $scope.novoEstacionamento = function () {
            $state.go("estacionamento_add");
        }

        $scope.cadastrar = function () {
            $scope.estacionamento.$save()
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
    };

    angular.module("estacionamento").controller("estacionamentoController", ["$scope", "$stateParams", "Estacionamento", "$state", estacionamentoController]);
}());