(function () {
    'use strict';

    angular.module('multitex.security.directives')
      .directive('sameAs', function () {
          return {
              require: 'ngModel',
              link: function (scope, elem, attrs, ngModel) {
                  ngModel.$parsers.unshift(validate);

                  // Force-trigger the parsing pipeline.
                  scope.$watch(attrs.sameAs, function () {
                      ngModel.$setViewValue(ngModel.$viewValue);
                  });

                  function validate(value) {
                      var isValid = scope.$eval(attrs.sameAs) == value;

                      ngModel.$setValidity('same-as', isValid);

                      return isValid ? value : undefined;
                  }
              }
          };
      });


})();







(function () {

    'use strict';
    angular.module('multitex.security.directives')
     .directive('usernameAvailableValidator', ['$http', '$q', function ($http, $q) {
        return {
            require: 'ngModel',
            link: function ($scope, element, attrs, ngModel) {
                ngModel.$asyncValidators.usernameAvailable = function (modelValue, viewValue) {
                    return $http.get('/Security/ScUser/CheckValidUserID?Username=' + angular.uppercase(viewValue)).then(function (res) {
                        if (res.data) {
                            return $q.reject();
                        }
                        return true;
                    });
                };
            }
        }
    }]);

})();







