(function () {

    var Usuario = function ($resource, authService) {

        var resource = $resource('/api/Usuarios/:id', { id: '@id' }, {

            'update': {
                method: 'PUT',
                headers: { 'Authorization': 'token' }
            },

            'query': {
                method: 'GET',
                isArray: true,
                headers: { 'Authorization': 'token' }
            },

            'add': {
                method: 'POST',
                headers: { 'Authorization': 'token' }
            },

            'get': {
                method: 'GET',
                headers: { 'Authorization': 'token' }
            },

            'filtrar': {
                method: 'POST',
                isArray: true,
                headers: { 'Authorization': 'token' },
                url: '/api/Usuarios/filtrar'
            },
            'remove': {
                method: 'DELETE',
                headers: { 'Authorization': 'token' }
            },
            'getByLogin': {
                method: 'GET',
                headers: { 'Authorization': 'token' },
                url: '/api/Usuarios/getByLogin/:login'
            },
        });

        return resource;
    };

    angular.module("login").service("Usuario", ["$resource", "authService", Usuario]);

}());