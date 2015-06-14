(function () {
    var formRegistrarClienteController = function ($scope, Cliente, $state) {
        var mensagemSucesso = function () {
            alert("Operação realizada com sucesso.");

            $state.go('login');
        };

        var showErrorMessage = function (errResponse) {
            if (errResponse.status == 400) {
                alert(errResponse.data.Message);
            } else {
                console.log(errResponse);

                alert("Ocorreu um erro inesperado. Por favor, contacte o administrador.");
            }
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