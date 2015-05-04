(function () {
    var categoriaVagasController = function ($scope, $modalInstance, categoriaVaga) {

        $scope.ok = function () {
            $modalInstance.close($scope.selected.item);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

    };

    angular.module("estacionamento").controller("categoriaVagasController", ["$scope", "$modalInstance", categoriaVagasController]);
}());