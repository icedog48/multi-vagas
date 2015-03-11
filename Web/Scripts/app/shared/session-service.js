(function () {

    var sessionService = function ($window) {

        var me = typeof ($window.sessionStorage.user) !== 'undefined' && JSON.parse($window.sessionStorage.user) || {};

        me.create = function (sessionId, userId, userRoles) {
            me.id = sessionId;
            me.userId = userId;
            me.userRoles = userRoles;

            $window.sessionStorage.user = JSON.stringify(me);
        };

        me.destroy = function () {
            me.id = null;
            me.userId = null;
            me.userRoles = null;

            $window.sessionStorage.removeItem("user");
        };

        return me;
    };

    angular.module("shared").service("sessionService", ["$window", sessionService]);

}());