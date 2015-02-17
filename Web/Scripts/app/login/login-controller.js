(function () {
    var loginController = function ($scope, authService) {

        $scope.credentials = {
            username: '',
            password: ''
        };

        $scope.login = function (credentials) {

            //authService.login(credentials).then(function (user) {
            //    $rootScope.$broadcast(AUTH_EVENTS.loginSuccess);
            //    $scope.setCurrentUser(user);
            //}, function () {
            //    $rootScope.$broadcast(AUTH_EVENTS.loginFailed);
            //});
        };
    };

    angular.module("login").controller("loginController", ["$scope", "authService", loginController]);
}());