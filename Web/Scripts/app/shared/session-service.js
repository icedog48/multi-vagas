(function () {

    var sessionService = function ($window) {

        var me = typeof ($window.sessionStorage.user) !== 'undefined' && JSON.parse($window.sessionStorage.user) || {};

        me.create = function (token, user, userRoles) {
            me.token = token;
            me.user = user;
            me.userRoles = userRoles;

            $window.sessionStorage.user = JSON.stringify(me);
        };

        me.destroy = function () {
            me.token = null;
            me.user = null;
            me.userRoles = null;

            $window.sessionStorage.removeItem("user");
        };

        return me;
    };

    angular.module("shared").service("sessionService", ["$window", sessionService]);

}());