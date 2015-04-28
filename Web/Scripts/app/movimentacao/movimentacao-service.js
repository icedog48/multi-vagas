(function () {

    var Movimentacao = function ($resource, authService) {

        var resource = $resource('/api/movimentacoes/:id', { id: '@id' }, {

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
                url: '/api/movimentacoes/filtrar'
            },

            'remove': {
                method: 'DELETE',
                headers: { 'Authorization': 'token' }
            },

            'registrarEntrada': {
                url: 'api/movimentacoes/registrarEntrada',
                headers: { 'Authorization': 'token' },
                method: 'POST'
            },

            'atualizarVaga': {
                method: 'PUT',
                headers: { 'Authorization': 'token' }
            },
        });

        return resource;
    };

    angular.module("movimentacao").service("Movimentacao", ["$resource", "authService", Movimentacao]);

}());