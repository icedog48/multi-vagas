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

            'listarTiposPagamento': {
                method: 'GET',
                isArray: true,
                headers: { 'Authorization': 'token' },
                url: 'api/movimentacoes/tipospagamento',
            },

            'filtrar': {
                method: 'POST',
                isArray: true,
                headers: { 'Authorization': 'token' },
                url: '/api/movimentacoes/filtrar'
            },

            'filtrarPorPeriodo': {
                method: 'POST',
                isArray: true,
                headers: { 'Authorization': 'token' },
                url: '/api/movimentacoes/periodo'
            },

            'filtrarPorCategoria': {
                method: 'POST',
                isArray: true,
                headers: { 'Authorization': 'token' },
                url: '/api/movimentacoes/categoria'
            },

            'filtrarPorEstadia': {
                method: 'POST',
                isArray: true,
                headers: { 'Authorization': 'token' },
                url: '/api/movimentacoes/estadia'
            },

            'remove': {
                method: 'DELETE',
                headers: { 'Authorization': 'token' }
            },

            'registrarEntrada': {
                url: 'api/movimentacoes/registrarentrada',
                headers: { 'Authorization': 'token' },
                method: 'POST'
            },

            'registrarEntradaExpressa': {
                url: 'api/movimentacoes/registrarentradaexpressa',
                headers: { 'Authorization': 'token' },
                method: 'POST'
            },

            'atualizarVaga': {
                method: 'POST',
                headers: { 'Authorization': 'token' },
                url: 'api/movimentacoes/atualizarvaga',
            },

            'registrarSaida': {
                url: 'api/movimentacoes/registrarsaida/:id',
                headers: { 'Authorization': 'token' },
                method: 'PUT'
            },

            'prepararSaida': {
                url: 'api/movimentacoes/prepararsaida/:movimentacao',
                headers: { 'Authorization': 'token' },
                method: 'GET'
            },
            
        });

        return resource;
    };

    angular.module("movimentacao").service("Movimentacao", ["$resource", "authService", Movimentacao]);

}());