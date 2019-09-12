"use strict";
(function () {
    angular.module("multitex.knitting")
           .factory("entityService", ["akFileUploaderService", function (akFileUploaderService) {
               var saveTutorial = function (tutorial, url, antiForgeryToken) {
                   return akFileUploaderService.saveModel(tutorial, url, antiForgeryToken);
               };
               return {
                   saveTutorial: saveTutorial
               };
           }]);
})();