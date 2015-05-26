(function () {

    var Vaga = function ($resource) {

        var resource = $resource('/api/vagas/:id', { id: '@id' }, {
            'get': {
                method: 'GET',
                isArray: false,
                headers: { 'Authorization': 'token' }
            },

            'categoriasVaga': {
                method: 'GET',
                isArray: true,
                headers: { 'Authorization': 'token' },
                url: '/api/vagas/categorias'
            },

            'vagasDisponiveis' : {
                method: 'GET',
                isArray: true,
                headers: { 'Authorization': 'token' },
                url: '/api/vagas/disponiveis/:id'
            },

            'reservarVaga': {
                method: 'POST',
                headers: { 'Authorization': 'token' },
                url: '/api/vagas/reservar'
            },

            'categoriasVagaEstacionamento': {
                method: 'GET',
                isArray: true,
                headers: { 'Authorization': 'token' },
                url: '/api/vagas/categorias/:id'
            },
        });

        return resource;
    };

    angular.module("vagas").service("Vaga", ["$resource", Vaga]);

}());