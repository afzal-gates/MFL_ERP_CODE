(function () {
    'use strict';

    angular
        .module('multitex.hr.directives')
        .directive('dateLowerThan', [dateLowerThan]);

    angular
        .module('multitex.hr.directives')
        .directive('dateGraterThan', [dateGraterThan]);

        
    function dateLowerThan() {

        var directive = {
            require: 'ngModel',
            link: link,
            restrict: 'A'
        };
        return directive;

        function link($scope, element, attrs, ngModel) {
            //console.log(attrs);
            
            ngModel.$validators.dateLowerThanCheck = function (value) {
                console.log('LT= ' + value);
                //var vToDate = Date.parse(value);
                
                var status = true;
                attrs.$observe('dateLowerThan', function (value1) {
                    console.log('LF= ' + value1);
                    //var vFromDate = Date.parse(value);

                    //if (vFromDate < vToDate) {
                    //    status = false;
                    //}
                });

                return status;
            };
        };
    };



    function dateGraterThan() {

        var directive = {
            require: 'ngModel',
            link: link,
            restrict: 'A'
        };
        return directive;

        function link($scope, element, attrs, ngModel) {
            //console.log(attributes);
            
            ngModel.$validators.dateGraterThanCheck = function (value) {
                //console.log(value);
                //console.log('GT= ' + value);
                var vToDate = Date.parse(value);

                var status = true;                
                $scope.$watch(attrs.dateGraterThan, function (value1) {
                    //console.log('GF= ' + value1);
                    //alert(value);
                    var vFromDate = Date.parse(value1);                    
                    
                    if (vFromDate < vToDate) {
                        status = false;
                        alert(status);
                    }
                });

                return status;
            };
                        

            //attributes.$observe('anotherParam', function (value) {
            //    console.log(value);
            //});



        };
    };



    ///////////////////////////////////////////////////


    //angular
    //    .module('multitex.hr.widgets')
    //    .directive('dateCompaire', [dateCompaire]);


    //function dateCompaire() {

    //    var directive = {
    //        require: 'ngModel',
    //        link: link,
    //        restrict: 'A'
    //    };
    //    return directive;

    //    function link($scope, element, attrs, ngModel) {
           
    //        ngModel.$validators.dateCompaireCheck = function (value) {
    //            console.log('v=' + value);

    //            attrs.$observe('dateCompaire', function (value) {
    //                console.log('a=' + value);
    //            });
    //        };

            

    //    };
    //};



    //////////////////////////////////////////////////





})();