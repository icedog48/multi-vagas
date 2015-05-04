(function () {
    var formularioController = function ($scope, Estacionamento, $state, $stateParams, $modal) {

        $scope.novoCadastro = (typeof ($stateParams.id) == 'undefined');

        
    };

    angular.module("estacionamento").controller("formularioController", ["$scope", "Estacionamento", "$state", "$stateParams", "$modal", formularioController]);
}());