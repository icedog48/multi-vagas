(function () {

    var Admin = function ($resource) {

        var resource = $resource('/api/admins/:id', { id: '@id' }, {

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
                url: '/api/admin/filtrar'
            },
            'remove': {
                method: 'DELETE',
                headers: { 'Authorization': 'token' }
            },
        });

        return resource;
    };

    angular.module("admin").service("Admin", ["$resource", Admin]);

}());