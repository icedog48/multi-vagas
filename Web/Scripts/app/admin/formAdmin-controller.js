(function () {
    var formAdminController = function ($scope, $stateParams, Admin, $state, Estacionamento) {

        var novoCadastro = (typeof ($stateParams.id) == 'undefined');

        var admin = {};

        var carregarDados = function (adminId) {
            Admin.get({ id: adminId }).$promise.then(function (data) {                
                $scope.admin = new Admin(data);
            }, function (errResponse) {
                alert('Registro não encontado.');

                $state.go('admin_list');
            });
        };

        var mensagemSucesso = function () {
            alert("Operação realizada com sucesso.");

            $state.go("admin_list");
        };

        var cadastrar = function (admin) {
            Admin.add(admin).$promise.then(function (data) {
                mensagemSucesso();
            });
        };

        var atualizar = function (admin) {
            Admin.update(admin).$promise.then(function (data) {
                mensagemSucesso();
            });
        };

        if (novoCadastro) {
            $scope.admin = new Admin();
        } else {
            carregarDados($stateParams.id);
        }

        $scope.novoCadastro = novoCadastro;

        Estacionamento.query().$promise.then(function (data) {
            $scope.estacionamentos = data;
        });

        $scope.salvar = function (admin)
        {
            if ($scope.novoCadastro) {
                cadastrar(admin);
            } else {
                atualizar(admin)
            }
        };
    };

    angular.module("admin").controller("formAdminController", ["$scope", "$stateParams", "Admin", "$state", "Estacionamento", formAdminController]);
}());