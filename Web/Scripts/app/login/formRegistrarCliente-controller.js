(function () {
    var formRegistrarClienteController = function ($scope, Cliente, $state) {
        var mensagemSucesso = function () {
            alert("Operação realizada com sucesso.");

            $state.go('login');
        };

        var showErrorMessage = function (errCode) {
            alert("Erro inesperado.");
        };

        var registrar = function (cliente) {
            Cliente.registrar(cliente).$promise.then(function (data) {
                mensagemSucesso();
            }, function (errResponse) {
                showErrorMessage(errResponse);
            });
        }

        $scope.registrar = registrar;
    };

    angular.module("login").controller("formRegistrarClienteController", ["$scope", "Cliente", "$state", formRegistrarClienteController]);
}());