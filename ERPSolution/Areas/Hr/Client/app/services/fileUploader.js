﻿(function () {
    "use strict"
    angular.module("multitex.hr", [])
        .factory("akFileUploaderService", ["$q", "$http",
               function ($q, $http) {
                   var getModelAsFormData = function (data) {
                       var dataAsFormData = new FormData();
                       angular.forEach(data, function (value, key) {                           
                           if (angular.isDate(value)) {                               
                               value = new Date(value).toDateString('yyyy-MMM-dd');                               
                           }
                           dataAsFormData.append(key, value);
                       });
                       return dataAsFormData;
                   };

                   var saveModel = function (data, url, antiForgeryToken) {
                       //console.log(getModelAsFormData(data));
                       var deferred = $q.defer();
                       $http({
                           url: url,
                           method: "POST",
                           data: getModelAsFormData(data),
                           transformRequest: angular.identity,
                           headers: { 'Content-Type': undefined, "RequestVerificationToken": antiForgeryToken }
                       }).success(function (result) {
                           deferred.resolve(result);
                       }).error(function (result, status) {
                           deferred.reject(status);
                       });
                       return deferred.promise;
                   };

                   return {
                       saveModel: saveModel
                   }

               }])
        .directive("akFileModel", ["$parse",
                function ($parse) {
                    return {
                        restrict: "A",
                        link: function (scope, element, attrs) {
                            var model = $parse(attrs.akFileModel);
                            var modelSetter = model.assign;
                            element.bind("change", function () {
                                scope.$apply(function () {
                                    modelSetter(scope, element[0].files[0]);
                                });
                            });
                        }
                    };
                }]);
})(window,document);