(function () {    

    var config = function ($stateProvider, USER_ROLES, $resourceProvider) {

        $stateProvider
            .state("filrar_movimentacoes_por_periodo", {
                parent: 'dashboard',
                url: "/movimentacoes/periodo",
                templateUrl: "Scripts/app/relatorios/filtroMovimentacaoPorPeriodo.html",
                controller: 'filtroMovimentacaoPorPeriodoController',
                roles: [USER_ROLES.admin]
            });

        $stateProvider
            .state("filrar_movimentacoes_por_estadia", {
                parent: 'dashboard',
                url: "/movimentacoes/estadia",
                templateUrl: "Scripts/app/relatorios/filtroMovimentacaoPorEstadia.html",
                controller: 'filtroMovimentacaoPorEstadiaController',
                roles: [USER_ROLES.admin]
            });

        $stateProvider
            .state("filrar_movimentacoes_por_categoria", {
                parent: 'dashboard',
                url: "/movimentacoes/categoria",
                templateUrl: "Scripts/app/relatorios/filtroMovimentacaoPorCategoria.html",
                controller: 'filtroMovimentacaoPorCategoriaController',
                roles: [USER_ROLES.admin]
            });

        $resourceProvider.defaults.stripTrailingSlashes = false;
    };

    var printHelper = function ($http, $rootScope, $compile, $timeout) {
        var me = { };

        var printPage = function (reportPage) {

            var popupWin = null;

            if (navigator.userAgent.toLowerCase().indexOf('chrome') > -1) {

                popupWin = window.open('', '_blank', 'width=600,height=600,scrollbars=no,menubar=no,toolbar=no,location=no,status=no,titlebar=no');
                popupWin.window.focus();
                popupWin.document.write(reportPage);

                popupWin.onbeforeunload = function (event) {
                    popupWin.close();

                    return '.\n';
                };

                popupWin.onabort = function (event) {
                    popupWin.document.close();
                    popupWin.close();
                };
            } else {

                popupWin = window.open('', '_blank', 'width=800,height=600');
                popupWin.document.open();
                popupWin.document.write(reportPage);
                popupWin.document.close();
            }

            popupWin.document.close();

            return true;
        };

        var printContent = function (templateUrl, content) {

            $http.get(templateUrl).success(function (template) {
                var page = template.replace("{{content}}", content);

                return printPage(page);
            });
        };

        var preparePage = function (html) {

            var htmlContent =  "<!doctype html>" +
                               "<html>" +
                                    '<head>' +
                                       '<link href="/Content/bootstrap/css/bootstrap.css" rel="stylesheet">' +
		                               '<link href="/Content/dashgum/css/font-awesome.css" rel="stylesheet">' +
		                               '<link href="/Content/style.css" rel="stylesheet">' +
		                               '<link href="/Content/style-responsive.css" rel="stylesheet">' +
                                    '</head>' +
                                    '<body onload="window.print()">' +
                                        '<div class="reward-body">' +
                                            html +
                                        '</div>' +
                                    '</body>' +
                               "</html>";

            return htmlContent;

        };

        var printTemplate = function (templateUrl, data) {

            $http.get(templateUrl).success(function (template) {

                var printScope = angular.extend($rootScope.$new(), data);

                var element = $compile($('<div>' + template + '</div>'))(printScope);

                var waitForRenderAndPrint = function () {
                    if (printScope.$$phase || $http.pendingRequests.length) {
                        $timeout(waitForRenderAndPrint);
                    } else {
                        var page = preparePage(element.html());

                        printPage(page);

                        printScope.$destroy(); // To avoid memory leaks from scope create by $rootScope.$new()
                    }
                }

                waitForRenderAndPrint();
            });

        };

        me.printContent = printContent;
        me.printTemplate = printTemplate;

        return me;
    };

    angular.module("relatorios",
        [
            "shared",
            "ui.router",
            "ngResource"
        ])
        .config(["$stateProvider", "USER_ROLES", "$resourceProvider", config])
        .factory("printHelper", ["$http", "$rootScope", "$compile", "$timeout", printHelper])
}());