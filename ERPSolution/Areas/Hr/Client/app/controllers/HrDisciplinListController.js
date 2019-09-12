(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrDisciplinListController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'HrService', 'entityService', HrDisciplinListController]);

    function HrDisciplinListController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, HrService, entityService) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.dtFormat = config.appDateFormat;
        vm.timeFormat = config.appTimeFormat;
        vm.showSplash = true;
        
        vm.insert = true;
        vm.today = new Date();

        vm.editRecord = function (dataItem) {
            vm.insert = false;

            //$routeParams = dataItem;
            //$location.path('/OfficeEntry');

            //console.log($routeParams);
            vm.form = dataItem;
            //console.log(dataItem);

            return $state.go('DisciplinAction', { pDisciplinActionObj: dataItem });
        };

        //console.log($stateParams.dataSource);
        vm.gridOptions = {
            autoBind: true,
            dataSource: $stateParams.dataSource,
            filterable: {
                mode: "row"
            },
            selectable: "row",
            sortable: true,
            pageSize: 10,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [
                { field: "DISP_ACT_REF_NO", title: "Action Ref.#", type: "string", width: "150px" },
                { field: "EMPLOYEE_CODE", title: "Employee Code", type: "string", width: "150px" },
                { field: "MONTH_YEAR_NAME", title: "Period", type: "string", width: "100px" },
                { field: "DA_TYPE_NAME_EN", title: "Action Type", type: "string", width: "200px" },
                { field: "LK_DATA_NAME_EN", title: "Action Reason", type: "string", width: "200px" },
                {
                    title: "Action",
                    template: function () {

                        return "<a class='btn btn-xs blue' ng-click='vm.editRecord(dataItem)'  title='Edit'><i class='fa fa-edit'></i>Edit</a>";
                        //return "<a ng-click='vm.editRecord(dataItem)' title='Edit'><i class='fa fa-edit'></i></a>";
                    },
                    width: "50px"
                }

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