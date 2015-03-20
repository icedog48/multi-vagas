(function () {
    var filtroAdminController = function ($scope, Admin, Estacionamento) {
        $scope.estacionamentos = Estacionamento.query();

        $scope.admins = Admin.query();

        $scope.filtroAdmin = { Estacionamento: null, Descricao: ''};

        $scope.filtrarAdmins = function (filtroAdmin) {
            Admin.filtrar(filtroAdmin).$promise.then(function (data) {
                $scope.admins = data;
            });
        };

        $scope.excluirAdmin = function (admin) {

            if (confirm('Deseja realmente excluir ?')) {

                var adminId = admin.Id;

                Admin.remove({ id: adminId }).$promise.then(function () {
                    $scope.admins.forEach(function (admin, index) {
                            if (admin.Id == adminId) {
                                $scope.admins.splice(index, 1);
                            }
                        });
                    });
            };
        };

    };

    angular.module("admin").controller("filtroAdminController", ["$scope", "Admin", "Estacionamento", filtroAdminController]);
}());