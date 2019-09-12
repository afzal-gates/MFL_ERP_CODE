﻿(function () {
    'use strict';

    angular.module('multitex.admin').controller('DesigGroupController', ['logger', 'config', '$q', '$scope', '$http', DesigGroupController]);

    function DesigGroupController(logger, config, $q, $scope, $http) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.showSplash = true;
        
        vm.fromDate = null;
        vm.toDate = null;

        vm.enable = false;
       
       
        function getTreeData() {
            return $http.get('/Hr/Admin/HrDesigGroup/HrDesigGroupData').then(function (res) {
               return vm.thingsOptions = {
                   dataSource: res.data
               };
            });
        }
             
        function activate(){
            var promise = getTreeData();
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        //function getData(){
        //     return AdminService.getData().then(function (data) {
        //         //vm.schedulerOptions.dataSource.data = data;
        //         //return vm.schedulerOptions.dataSource.data;
           
        //    });
        //}

 
        

            
        
    }

    

})();