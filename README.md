# ServiceFabricSelfHostedWebAPIwithAngualrJS
A Service Fabric type of Self-Hosted stateless WebAPI is a Console application. This tutorial gives you method to add a AngualrJS frontend UI to WebAPI Console application.

This method works the best when you want to provide a simple configuration/monitor UI for your Web API. 
If you want to build a full ASP.NET MVC application, you should consider the ASP.NET Core  service type. <br/>
More details from here: <br/>
[https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-add-a-web-frontend](https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-add-a-web-frontend)
[http://stackoverflow.com/questions/34707087/web-api-and-web-ui-in-same-application-service](http://stackoverflow.com/questions/34707087/web-api-and-web-ui-in-same-application-servic)

## Bringing in static HTML pages

As a starting point, let’s bring in some static HTML pages to the Web API service.
1.	Create a new Service Fabric application with a Stateless Web API service.
2.	Under the service project, create a new wwwroot folder and a simple index.html page under it.
3.	Change the property of index.html to set its Copy to Output Directory property to  Copy Always (with Build Action as Content).
4.	Add a reference to the Microsoft.Owin.FileSystems NuGet package and the Microsoft.Owin.StaticFiles NuGet package.
5.	Modify the startup.cs file to add routings for static files:
```js
var fileSystem = new PhysicalFileSystem(@".\wwwroot");
var options = new FileServerOptions
{
    EnableDefaultFiles = true,
    FileSystem = fileSystem,
    RequestPath = PathString.Empty
};
options.DefaultFilesOptions.DefaultFileNames = new[] { "index.html" };
options.StaticFileOptions.FileSystem = fileSystem;
options.StaticFileOptions.ServeUnknownFileTypes = true;
options.EnableDirectoryBrowsing = true;
config.MapHttpAttributeRoutes();
appBuilder.UseFileServer(options);
```
6.	Build and deploy the application. You should be able to access the above index.html page by navigating to the root folder (such as http://localhost:<port>/)
## Adding jQuery, Angular, and Bootstrap etc.
Now, you can continue to bring in JavaScript libraries such as jQuery and Angular to build up your UI.
1.	Modify the index.html file:
```js
<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title></title>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <script src="//ajax.googleapis.com/ajax/libs/angularjs/1.4.3/angular.min.js"></script>
    <script src="//ajax.googleapis.com/ajax/libs/angularjs/1.4.3/angular-route.min.js"></script>
    <script src="//ajax.googleapis.com/ajax/libs/angularjs/1.4.3/angular-resource.min.js"></script>
    <script src="//angular-ui.github.io/bootstrap/ui-bootstrap-tpls-0.13.2.js"></script>
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
    <script src="app.js"></script>
</head>
<body>
    <div ng-app="testApp">
       <div ng-view></div>
    </div>
</body>
</html>
```
2.	Add an app.js file to the wwwroot folder:
```js
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
```
3.	Add a new Views folder to the wwwroot folder. Create a testPage1.html page under the Views folder:
```js
<h1>This is test page with message {{message}}</h1>
```
4. Change the property of app.js testPage1.html to set its Copy to Output Directory property to  Copy Always (with Build Action as Content).

5. Deploy the app again. You should be able to access the Angular route by navigating to http://localhost:<port>/#/ngtest.

## Reference
Below are some useful links, you will need them before your real action.<br/><br/>
http://haishibai.blogspot.com/2016/11/tutorial-adding-ui-to-service-fabric.html
