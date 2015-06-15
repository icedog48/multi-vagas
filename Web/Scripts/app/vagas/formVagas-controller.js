(function () {
    var formVagasController = function ($scope, $stateParams, CategoriaVaga, $state, Estacionamento) {

        var novoCadastro = (typeof ($stateParams.id) == 'undefined');

        var categoriaVaga = {};

        var carregarDados = function (vagaId) {
            CategoriaVaga.get({ id: vagaId }).$promise.then(function (data) {
                $scope.categoriaVaga = new CategoriaVaga(data);
            }, function (errResponse) {
                alert('Vaga não encontrada.');

                $state.go('vagas_list');
            });
        };

        var mensagemSucesso = function () {
            alert("Operação realizada com sucesso.");

            $state.go("vagas_list");
        };

        var mensagemErro = function (errResponse) {
            console.log(errResponse);

            if (errResponse.status == 500) {
                alert(errResponse.data.Message);
            } else {
                alert("Ocorreu um erro inesperado. Por favor, contacte o administrador.");
            }
        }

        var cadastrar = function (categoriaVaga) {
            CategoriaVaga.add(categoriaVaga).$promise.then(function (data) {
                mensagemSucesso();
            }, function (errResponse) {
                mensagemErro(errResponse);
            });
        };

        var atualizar = function (categoriaVaga) {
            CategoriaVaga.update({ id: categoriaVaga.Id }, categoriaVaga).$promise.then(function (data) {
                mensagemSucesso();
            }, function (errResponse) {
                mensagemErro(errResponse);
            });
        };

        if (novoCadastro) {
            $scope.categoriaVaga = new CategoriaVaga();
        } else {
            carregarDados($stateParams.id);
        }

        $scope.novoCadastro = novoCadastro;
        $scope.naoPodeAlterar = !novoCadastro;

        Estacionamento.query().$promise.then(function (data) {
            $scope.estacionamentos = data;
        });

        $scope.salvar = function (categoriaVagas)
        {
            if ($scope.novoCadastro) {
                cadastrar(categoriaVagas);
            } else {
                atualizar(categoriaVagas)
            }
        };

       
    };

    angular.module("vagas").controller("formVagasController", ["$scope", "$stateParams", "CategoriaVaga", "$state", "Estacionamento", formVagasController]);
}());