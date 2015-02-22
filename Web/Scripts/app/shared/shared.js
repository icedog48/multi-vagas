angular.module("shared", [])
    .constant("APP_CONFIG", {
        templateBaseUrl: "Scripts/app/"
    })
    .constant("USER_ROLES", {
        equipeMultivagas: 'equipe-multivagas', // Cadastra estacionamentos
        admin: 'admin', // Gerencia estacionamentos
        funcionario: 'funcionario', // Registra entrada e saida de clientes
        cliente: 'cliente' // Consulta e reserva vagas
    })
    .constant('AUTH_EVENTS', {
        loginSuccess: 'auth-login-success',
        loginFailed: 'auth-login-failed',
        logoutSuccess: 'auth-logout-success',
        sessionTimeout: 'auth-session-timeout',
        notAuthenticated: 'auth-not-authenticated',
        notAuthorized: 'auth-not-authorized'
    });