(function () {

    var Estacionamento = function ($resource) {
        return $resource('/api/estacionamentos/:id', { id: '@id' }, {
            'update': { method: 'PUT' }
        });
    };

    angular.module("estacionamento").service("Estacionamento", ["$resource", Estacionamento]);

}());