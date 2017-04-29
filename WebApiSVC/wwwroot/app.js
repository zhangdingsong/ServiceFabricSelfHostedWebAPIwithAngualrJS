var testApp = angular.module("testApp", ['ngRoute']);

testApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
    when('/ngtest', {
        templateUrl: '/views/testPage1.html', controller: 'TestController'
    });
}]);

testApp.controller('TestController', function ($scope) {
    $scope.message = "This is a message from the test controller";
});