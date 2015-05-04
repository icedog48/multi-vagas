(function () {
    var formRegistrarEntradaController = function ($scope, $state, Vaga, Movimentacao, $stateParams) {

        var edicao = (typeof ($stateParams.id) != 'undefined');

        var categoriasVaga = Vaga.categoriasVaga();

        var mensagemSucesso = function () {
            alert("Operação realizada com sucesso.");

            $state.go("movimentacao_list");
        };

        var showErrorMessage = function (errCode) {
            alert("Erro inesperado.");
        }

        var listarVagas = function (categoriaVaga) {
            Vaga.vagasDisponiveis({ id: categoriaVaga }).$promise.then(function (data) {
                $scope.vagasDisponiveis = data;
            });
        }

        var registrar = function (movimentacao) {
            Movimentacao.registrarEntrada(movimentacao).$promise.then(function (response) {
                mensagemSucesso();
            }, function (errResponse) {
                showErrorMessage(errResponse.data.Message);
            });
        };

        var atualizar = function (movimentacao) {
            Movimentacao.atualizarVaga({id: movimentacao.Id}, movimentacao).$promise.then(function (response) {
                mensagemSucesso();
            }, function (errResponse) {
                showErrorMessage(errResponse.data.Message);
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
                
                Vaga.get({ id: movimentacao.Vaga }).$promise.then(function (data) {
                    console.log(data);

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
            $scope.vagasDisponiveis = [];
        }

        $scope.categoriasVaga = categoriasVaga;
        $scope.listarVagas = listarVagas;
        $scope.edicao = edicao;
        $scope.salvar = salvar;
    };

    angular.module("movimentacao").controller("formRegistrarEntradaController", ["$scope", "$state", "Vaga", "Movimentacao", "$stateParams", formRegistrarEntradaController]);
}());