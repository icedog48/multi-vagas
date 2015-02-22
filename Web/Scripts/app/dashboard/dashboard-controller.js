(function () {
    var dashboardController = function ($scope, $state, authService) {        

        $scope.logout = function () {
            authService.logout();

            $state.go("login");
        };
    };

    angular.module("dashboard").controller("dashboardController", ["$scope", "$state", "authService", dashboardController]);
}());