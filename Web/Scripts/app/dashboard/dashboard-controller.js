(function () {
    var dashboardController = function ($scope, $state, authService) {        

        $scope.logout = function () {
            authService.logout();

            //TODO: Descobrir uma forma melhor de redirecionar para a lista publica fazendo reload da dashboard.
            window.location.reload();
        };

    };

    angular.module("dashboard").controller("dashboardController", ["$scope", "$state", "authService", dashboardController]);
}());