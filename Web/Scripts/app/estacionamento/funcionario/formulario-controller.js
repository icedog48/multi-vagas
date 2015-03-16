(function () {
    var funcionarioController = function ($scope, $modalInstance, funcionario) {

        $scope.ok = function () {
            $modalInstance.close($scope.selected.item);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

    };

    angular.module("estacionamento").controller("funcionarioController", ["$scope", "$modalInstance", funcionarioController]);
}());