/*
Created by Alex Klibisz, aklibisz@utk.edu
February 2015
*/

var a;
a = angular.module("dualmultiselect", []), a.directive("dualmultiselect", [function () {
    return {
        restrict: 'E',
        scope: {
            options: '='           
        },
        controller: function ($scope) {
            $scope.transfer = function (from, to, index) {
                if (index >= 0) {
                    to.push(from[index]);
                    from.splice(index, 1);
                } else {
                    for (var i = 0; i < from.length; i++) {
                        to.push(from[i]);
                    }
                    from.length = 0;
                }
            };
        },
        template: '<fieldset class="custom-fieldset">' +
                    '<legend class="custom-legend" >{{options.legendTitle}}</legend>' +
                    '<div class="dualmultiselect">' +
                        '<div class="row" ng-if="options.showHelpMessage">' +
                            '<div class="col-lg-12 col-md-12 col-sm-12">' +
                                '<h4>{{options.title}}<small>&nbsp;{{options.helpMessage}}</small> </h4>' +
                            '</div>' +
                        '</div>' +

                        '<div class="row">'+ 
                            '<div class="col-lg-10 col-md-10 col-sm-10">'+                                
                                '<input class="form-control" placeholder="{{options.filterPlaceHolder}}" ng-model="searchTerm" />' +                                
                            '</div>' +
                            '<div class="col-lg-2 col-md-2 col-sm-2">' +
                                '<button type="button" class="btn btn-xs blue" ng-if="options.addButton" ng-click="options.clickAdd()"><i class="fa fa-plus"></i> {{options.addButtonCaption}}</button>' +
                            '</div>' +
                        '</div>'+
                        
                        '<div class="row">' +
                            '<div class="col-lg-5 col-md-5 col-sm-5">' +
                                '<label>{{options.labelAll}}</label>'+                                     
                                '<div class="pool">'+ 
                                    '<ul>'+ 
                                        '<li ng-repeat="item in options.items | filter: searchTerm | orderBy: options.orderProperty"> <a href="" ng-click="transfer(options.items, options.selectedItems, options.items.indexOf(item))">{{item.name}}&nbsp;&rArr; </a> </li>'+
                                    '</ul>'+ 
                                '</div>'+                                     
                            '</div>' +

                            '<div class="col-lg-2 col-md-2 col-sm-2">' +
                                '<label>&nbsp;</label>' +
                                '<label>&nbsp;</label>' +
                                '<label>&nbsp;</label>' +
                                '<label>&nbsp;</label>' +
                                '<label>&nbsp;</label>' +
                                '<label>&nbsp;</label>' +
                                '<label>&nbsp;</label>' +
                                '<p>&nbsp;</p>' +
                                '<button type="button" class="btn btn-default btn-xs" ng-click="transfer(options.items, options.selectedItems, -1)"> >> </button>' +
                                '<button type="button" class="btn btn-default btn-xs" ng-click="transfer(options.selectedItems, options.items, -1)"> << </button>' +
                            '</div>'+

                            '<div class="col-lg-5 col-md-5 col-sm-5">' +
                                '<label>{{options.labelSelected}}</label>'+                                 
                                '<div class="pool">'+ 
                                    '<ul>'+ 
                                        '<li ng-repeat="item in options.selectedItems | orderBy: options.orderProperty"> <a href="" ng-click="transfer(options.selectedItems, options.items, options.selectedItems.indexOf(item))"> &lArr;&nbsp;{{item.name}}</a> </li>'+
                                    '</ul>'+ 
                                '</div>'+
                            '</div>'+
                        '</div>'+

                    '</div>' +
                   '</fieldset>'
    };
}]);

//