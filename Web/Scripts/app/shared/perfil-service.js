(function () {

    var Perfil = function ($resource) {

        var resource = $resource('/api/perfis/:id', { id: '@id' }, {

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
                url: '/api/funcionario/filtrar'
            },
            'remove': {
                method: 'DELETE',
                headers: { 'Authorization': 'token' }
            },
            'perfisFuncionario': {
                method: 'GET',
                isArray: true,
                headers: { 'Authorization': 'token' },
                url: '/api/perfis/perfisfuncionario'
            },
        });

        return resource;
    };

    angular.module("shared").service("Perfil", ["$resource", Perfil]);

}());