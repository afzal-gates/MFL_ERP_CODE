(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrTaRawDataListController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'HrService', HrTaRawDataListController]);

    function HrTaRawDataListController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, HrService) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.dtFormat = config.appDateFormat;
        vm.timeFormat = config.appTimeFormat;
        vm.showSplash = true;
        
        vm.insert = true;
        vm.today = new Date();
        
        
        
        vm.PunchDataList = {
            //optionLabel: "Select",
            //template: '<span class="text-primary">#= EMPLOYEE_CODE #</span> :  #= EMP_FULL_NAME_EN #',
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        HrService.getPunchData().then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 10
            },
            filterable: {
                mode: "row"
            },
            selectable: "row",
            sortable: true,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [
                { field: "EMPLOYEE_CODE", title: "Employee Code", type: "string", width: "150px" },
                { field: "EMP_FULL_NAME_EN", title: "Employee Name", type: "string", width: "150px" },
                { field: "TA_DEVICE_CODE", title: "Device Code", type: "string", width: "100px" },
                { field: "TA_PROXI_ID", title: "Proxi ID", type: "string", width: "150px" },
                { field: "PUNCH_DATE", title: "Date", type: "date", format: "{0:" + vm.dtFormat + "}", width: "100px" },
                { field: "PUNCH_TIME", title: "Time", type: "date", format: "{0:" + vm.timeFormat + "}", width: "100px" },
                //,                
            ]
        };
        

        

        function activate(){
            var promise = [];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

       
            
        
    }

    

})();