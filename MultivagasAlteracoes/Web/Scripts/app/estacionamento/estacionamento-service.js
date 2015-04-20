(function () {

    var Estacionamento = function ($resource, authService) {

        var resource = $resource('/api/estacionamentos/:id', { id: '@id' }, {

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
                url: '/api/estacionamentos/filtrar'
            },
            'remove': {
                method: 'DELETE',
                headers: { 'Authorization': 'token' }
            },
            'verificaLogin': {
                url: 'api/estacionamentos/verficalogin/:login',
                headers: { 'Authorization': 'token' },
                method: 'GET'
            },
        });

        return resource;
    };

    angular.module("estacionamento").service("Estacionamento", ["$resource", "authService", Estacionamento]);

}());