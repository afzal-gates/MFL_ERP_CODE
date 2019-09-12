(function () {
    'use strict';

    angular.module('multitex.admin').controller('LookupTableController', ['logger', 'config', '$q', '$scope', '$http', LookupTableController]);

    function LookupTableController(logger, config, $q, $scope, $http) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.showSplash = true;
        
        vm.fromDate = null;
        vm.toDate = null;

        vm.insert = true;
        
        vm.reset = function () {
            vm.form = { IS_ACTIVE: 'Y' };
        };
        
        vm.submit = function (isValid, form, insert) {
            //alert(insert);
            //alert('No');
            //console.log(form);

            if (!isValid) return;

            //alert('ok');            

            //submit the data to the server
            if (insert) {

                $http.post('/Hr/Admin/LookupTables/AddNew', form).success(function (data, status, headers, config) {

                    if (data != '') {
                        if (data.substr(0, 9) == 'MULTI-001') {
                            toastr.success(data.substr(9), "MultiTEX");
                            vm.reset();
                            //vm.TRAN_MODE = 1;
                        }
                        if (data.substr(0, 9) == 'MULTI-002') {
                            toastr.info(data.substr(9), "MultiTEX");
                        }
                        if (data.substr(0, 9) == 'MULTI-003') {
                            toastr.warning(data.substr(9), "MultiTEX");
                        }
                        if (data.substr(0, 9) == 'MULTI-005') {
                            toastr.error(data.substr(9), "MultiTEX");
                        }

                    }
                }).
                error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });

            }
            else {

                $http.post('/Hr/Admin/LookupTables/Edit', form).success(function (data, status, headers, config) {

                    if (data != '') {
                        if (data.substr(0, 9) == 'MULTI-001') {
                            toastr.success(data.substr(9), "MultiTEX");                            
                            //vm.reset();
                            //vm.TRAN_MODE = 1;
                            
                        }
                        if (data.substr(0, 9) == 'MULTI-002') {
                            toastr.info(data.substr(9), "MultiTEX");
                        }
                        if (data.substr(0, 9) == 'MULTI-003') {
                            toastr.warning(data.substr(9), "MultiTEX");
                        }
                        if (data.substr(0, 9) == 'MULTI-005') {
                            toastr.error(data.substr(9), "MultiTEX");
                        }

                    }
                }).
                error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });
                
            }

            return;

        };



        //function getTreeData() {
        //    return $http.get('/Admin/HrManagementType/HrManagementTypeData').then(function (res) {
        //       return vm.thingsOptions = {
        //           dataSource: res.data
        //       };
        //    });
        //}
             
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