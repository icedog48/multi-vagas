(function () {
    var formRegistrarEntradaController = function ($scope, $state, Vaga, Movimentacao, $stateParams, $filter, printHelper) {

        var edicao = (typeof ($stateParams.id) != 'undefined');

        var categoriasVaga = Vaga.categoriasVaga();

        var mensagemSucesso = function () {
            alert("Operação realizada com sucesso.");

            $state.go("movimentacao_list");
        };

        var showErrorMessage = function (errResponse) {
            if (errResponse.status == 400) {
                alert(errResponse.data.Message);
            } else {
                console.log(errResponse);

                alert("Ocorreu um erro inesperado. Por favor, contacte o administrador.");
            }
        };

        var listarVagas = function (categoriaVaga) {
            Vaga.vagasDisponiveis({ id: categoriaVaga }).$promise.then(function (data) {
                $scope.vagasDisponiveis = data;
            });
        };

        var emitirTicket = function (entrada) {
            var templateUrl = 'Scripts/app/movimentacao/ticket-template.html';

            var data = {
                movimentacao: entrada
            };

            return printHelper.printTemplate(templateUrl, data);

        };
        var registrar = function (movimentacao) {
            Movimentacao.registrarEntrada(movimentacao).$promise.then(function (response) {

                if (confirm("Operação realizada com sucesso. Deseja imprimir o ticket de acesso ?")) {
                    emitirTicket(response);
                }

                $state.go("movimentacao_list");

            }, function (errResponse) {
                showErrorMessage(errResponse);
            });
        };

        var atualizar = function (movimentacao) {
            Movimentacao.atualizarVaga({id: movimentacao.Id}, movimentacao).$promise.then(function (response) {
                mensagemSucesso();
            }, function (errResponse) {
                showErrorMessage(errResponse);
            });
        };

        var salvar = function (movimentacao) {
            if (edicao) {
                atualizar(movimentacao);
            } else {
                registrar(movimentacao);
            }
        };

        var carregarDados = function (movimentacaoId) {
            Movimentacao.get({ id: movimentacaoId }).$promise.then(function (data) {
                var movimentacao = new Movimentacao(data);

                listarVagas(movimentacao.CategoriaVaga);

                $scope.movimentacao = movimentacao;
                $scope.movimentacao.Entrada = $filter('date')($scope.movimentacao.Entrada, 'dd/MM/yyyy HH:mm');
                
                Vaga.get({ id: movimentacao.Vaga }).$promise.then(function (data) {
                    $scope.vagaAtual = data.CategoriaVaga + " - " + data.Codigo;
                });
            }, function (errResponse) {
                alert('Registro não encontado.');

                $state.go('movimentacao_list');
            });
        };

        if (edicao) {
            carregarDados($stateParams.id);            
        } else {
            $scope.movimentacao = {};
            $scope.movimentacao.Entrada = $filter('date')(new Date(), 'dd/MM/yyyy HH:mm');;

            $scope.vagasDisponiveis = [];
        }

        $scope.categoriasVaga = categoriasVaga;
        $scope.listarVagas = listarVagas;
        $scope.edicao = edicao;
        $scope.salvar = salvar;

        $scope.$watch('movimentacao.Placa', function (newValue, oldValue) {

            if (typeof (newValue) !== 'undefined') {

                var numberPattern = /^\d+$/;
                var letterPattern = /^[a-zA-Z]+$/;

                var isValid = true;

                for (var i = 0; i < newValue.length; i++) {
                    if (i < 3) {
                        isValid = letterPattern.test(newValue.charAt(i));
                    } else {
                        isValid = numberPattern.test(newValue.charAt(i));
                    }

                    if (!isValid) break;
                }

                if (isValid) {
                    $scope.movimentacao.Placa = newValue.toUpperCase();
                } else {
                    $scope.movimentacao.Placa = oldValue ? oldValue.toUpperCase() : '';
                }
            }
        }, true);
    };

    angular.module("movimentacao").controller("formRegistrarEntradaController", ["$scope", "$state", "Vaga", "Movimentacao", "$stateParams", "$filter", "printHelper", formRegistrarEntradaController]);
}());