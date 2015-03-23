(function () {
    var formVagasController = function ($scope, $stateParams, Vaga, $state, Estacionamento) {

        var novoCadastro = (typeof ($stateParams.id) == 'undefined');

        var categoriaVaga = {};

        var carregarDados = function (vagaId) {
            Vaga.get({ id: vagaId }).$promise.then(function (data) {                
                $scope.categoriaVaga = new Vaga(data);
            }, function (errResponse) {
                alert('Vaga não encontrada.');

                $state.go('vagas_list');
            });
        };

        var mensagemSucesso = function () {
            alert("Operação realizada com sucesso.");

            $state.go("vagas_list");
        };

        var cadastrar = function (categoriaVaga) {
            Vaga.add(categoriaVaga).$promise.then(function (data) {
                mensagemSucesso();
            });
        };

        var atualizar = function (categoriaVaga) {
            Vaga.update(categoriaVaga).$promise.then(function (data) {
                mensagemSucesso();
            });
        };

        if (novoCadastro) {
            $scope.categoriaVaga = new Vaga();
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
            categoriaVagas.Estacionamento = categoriaVagas.Estacionamento.Id;

            if ($scope.novoCadastro) {
                cadastrar(categoriaVagas);
            } else {
                atualizar(categoriaVagas)
            }
        };
    };

    angular.module("vagas").controller("formVagasController", ["$scope", "$stateParams", "Vaga", "$state", "Estacionamento", formVagasController]);
}());