(function () {

    var Funcionario = function ($resource) {

        var resource = $resource('/api/funcionarios/:id', { id: '@id' }, {

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
        });

        return resource;
    };

    angular.module("funcionario").service("Funcionario", ["$resource", Funcionario]);

}());