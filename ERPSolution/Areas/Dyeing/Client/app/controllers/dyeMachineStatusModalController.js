(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('dyeMachineStatusModalController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'Dialog', 'DateShareService', '$modalInstance', dyeMachineStatusModalController]);
    function dyeMachineStatusModalController($q, config, DyeingDataService, $stateParams, $state, $scope, Dialog, DateShareService, $modalInstance) {

        var vm = this;
        vm.showSplash = true;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        console.log($stateParams);

        vm.obGrid = [];
        vm.selectedRow = {};

        activate();
        function activate() {
            var promise = [loadMachineList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
                vm.form = formData.MC_BYR_ACC_ID ? formData : { IS_ACTIVE: 'Y', IS_RECHECK: null, RECHECK: false, RE_CHK_NO: 0 };
            });
        }


        function loadMachineList() {

            return vm.machineList = new kendo.data.DataSource({
                pageSize: 40,
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/DyeMachine/SelectAll').then(function (res) {
                            console.log(res);
                            var list = _.filter(res, function (x) { return x.LK_MAC_STATUS_ID == 502 });
                            e.success(list);
                        });
                    }
                }
            });
        }


        vm.gridOptions = {

            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains"
                    }
                }
            },
            //pageable: {
            //    refresh: true,
            //    pageSizes: true,
            //    buttonCount: 5
            //},
            columns: [
                { field: "DYE_MACHINE_ID", hidden: true },
                { field: "DYE_MC_TYPE_ID", hidden: true },
                { field: "C_ORIGIN_ID", hidden: true },
                { field: "CAP_MOU_ID", hidden: true },
                { field: "LK_MAC_STATUS_ID", hidden: true },
                { field: "RF_LOCATION_ID", hidden: true },
                { field: "RF_BRAND_ID", hidden: true },
                { field: "MAX_TEMP_C", hidden: true },
                { field: "IS_SMP_BLK", hidden: true },

                { field: "DYE_MACHINE_NO", title: "Machine No", type: "string", width: "10%", template: "<span style='color:#: MAC_PROD_STS_COLOR #'>#: DYE_MACHINE_NO #</span>" },
                { field: "D_PRD_CAPACITY", title: "Capacity", type: "string", width: "10%", template: "<span style='color:#: MAC_PROD_STS_COLOR #'>#: D_PRD_CAPACITY # (Kg)</span>" },
                { field: "MAC_PROD_STS_EN_NAME", title: "Current Status", type: "string", width: "10%", template: "<span style='color:#: MAC_PROD_STS_COLOR #'>#: MAC_PROD_STS_EN_NAME #</span>" },
                { field: "DM_ACTN_MODE_NAME", title: "Action Mode", type: "string", width: "10%" },
                { field: "ACTN_REF_NO", title: "Referance No", type: "string", width: "10%" },
                { field: "ACTN_TIME", title: "Start Time", type: "date", template: "#= kendo.toString(kendo.parseDate(ACTN_TIME),'dd-MMM-yyyy hh:mm tt') #", width: "10%" },
                { field: "MAC_PROD_STS_COLOR", title: "Color Code", type: "string", width: "10%" },
                { field: "DYE_TYPE_NAME_EN", title: "NPT Reason", type: "string", width: "10%" },

            ]
        };

        function getColor(colorCode, value) {
            alert(colorCode);
            return "<b style='color:'" + colorCode + "';'>" + value + "</b>";
        }
        vm.cancel = function (data) {
            $modalInstance.close(data);
        }



        function loadMachineList2() {

            DyeingDataService.getDataByFullUrl('/api/dye/DyeMachine/SelectAll').then(function (res) {
                vm.machineList = res;
            });
        }

    }
})();

