(function () {

    var Cliente = function ($resource, authService) {

        var resource = $resource('/api/clientes/:id', { id: '@id' }, {
            'registrar': {
                method: 'POST',
                headers: { 'Authorization': 'token' }
            },
        });

        return resource;
    };

    angular.module("login").service("Cliente", ["$resource", "authService", Cliente]);

}());