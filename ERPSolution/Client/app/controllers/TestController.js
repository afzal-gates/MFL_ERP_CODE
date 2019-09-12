(function () {
    'use strict';

    angular.module('multitex.hr').controller('ScUserController', ['logger', 'config', '$q', '$scope', '$http', ScUserController]);

    function ScUserController(logger, config, $q, $scope, $http) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.showSplash = true;
        
        vm.submit = function (isValid, form, insert) {

            //console.log(insert);
            alert('hello');

            if (!isValid) return;

            alert('ok');   
        }



        function activate(){
            var promise = [];
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