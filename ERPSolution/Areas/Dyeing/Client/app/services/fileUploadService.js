﻿(function () {
    "use strict"
    angular.module("multitex.purchase")
        .factory("FileUploadService", ["$q", "$http",
               function ($q, $http) {
                   var baseUrl = "";
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

                   var save = function (data, url) {
                       var deferred = $q.defer();
                       $http({
                           url: baseUrl + url,
                           method: "POST",
                           data: getModelAsFormData(data),
                           transformRequest: angular.identity,
                           headers: { 'Content-Type': undefined }
                       }).success(function (result) {
                           deferred.resolve(result);
                       }).error(function (result, status) {
                           deferred.reject(status);
                       });
                       return deferred.promise;
                   };
                   return {
                       save: save
                   }

               }]);

})(window, document);