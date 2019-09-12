(function () {
    'use strict';
    angular.module('multitex.planning').controller('GmtLineLoadTnaDatasModalController', ['$q', 'config', 'PlanningDataService', '$scope', '$modalInstance', 'data', 'allocation', 'Dialog', 'summery', GmtLineLoadTnaDatasModalController]);
    function GmtLineLoadTnaDatasModalController($q, config, PlanningDataService, $scope, $modalInstance, data, allocation, Dialog, summery) {
       

        $scope.data = data;
        $scope.allocation = allocation;

        $scope.summery = summery;
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        console.log(summery);
        $scope.onSelectAll = function (pG_IS_SD_RD) {
            angular.forEach($scope.data, function (val, key) {
                val['IS_SD_RD'] = pG_IS_SD_RD;
            });
        };

        function getTnaDatas(pGMT_PLN_LINE_LOAD_ID) {
            return PlanningDataService.getDataByUrl('/GmtLineLoad/getTnaDatas?pGMT_PLN_LINE_LOAD_ID=' + pGMT_PLN_LINE_LOAD_ID)
            .then(function (res) {
                $scope.data = res;
                getTnaSummery(pGMT_PLN_LINE_LOAD_ID);
            })
        }

        function getTnaSummery(pGMT_PLN_LINE_LOAD_ID) {
            return PlanningDataService.getDataByUrl('/GmtLineLoad/GetCpSummeryData?pGMT_PLN_LINE_LOAD_ID=' + pGMT_PLN_LINE_LOAD_ID)
            .then(function (res) {
                $scope.summery = res;
            });
        };

        function RecursiveStDaysUpdate(data, pPRIOR_TNA_TASK_D_ID, pDAYS2BeAdj) {
            console.log('Recursive Call');
            var idx = data.findIndex(function (o) { return o.MC_TNA_TASK_D_ID === pPRIOR_TNA_TASK_D_ID });
            if (idx > -1) {
                data[idx]['STD_DAYS'] = (data[idx]['STD_DAYS_ORI'] + pDAYS2BeAdj)
                if (data[idx]['PRIOR_TNA_TASK_D_ID'] > 0) {
                    RecursiveStDaysUpdate(data, data[idx]['PRIOR_TNA_TASK_D_ID'], pDAYS2BeAdj);
                };
            }
        }

        $scope.onChangeStdDays = function (item) {
            var DAYS2BeAdj = (item.STD_DAYS - item.STD_DAYS_ORI)
            angular.forEach($scope.data, function (val, key) {
                if (item.GMT_PLN_LINE_LD_CP_ID != val.GMT_PLN_LINE_LD_CP_ID && item.PRIOR_TNA_TASK_D_ID > 0) {
                    RecursiveStDaysUpdate($scope.data, item.PRIOR_TNA_TASK_D_ID, DAYS2BeAdj);
                }
            });
        };
 
        $scope.SaveData = function (data, valid) {
            if (!valid) {
                return;
            }

            var data2save = Object.assign({});

            data2save['GMT_PLN_LINE_LOAD_ID'] = allocation.GMT_PLN_LINE_LOAD_ID;
            data2save['XML'] = config.xmlStringShort(data.map(function (x) {
                return {
                    GMT_PLN_LINE_LD_CP_ID: x.GMT_PLN_LINE_LD_CP_ID,
                    PRIOR_TNA_TASK_D_ID: x.PRIOR_TNA_TASK_D_ID,
                    MC_TNA_TASK_D_ID: x.MC_TNA_TASK_D_ID,
                    STD_DAYS: (x.STD_DAYS - x.STD_DAYS_ORI),
                    IS_SD_RD: x.IS_SD_RD
                }
               })
            );


             Dialog.confirm('Do you really want to change data?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
             .then(function () {
                 return PlanningDataService.saveDataByUrl(data2save, '/GmtLineLoad/UpdateLineLoadCpData').then(function (res) {
                     if (res.success === false) {
                         vm.errors = res.errors;
                     }
                     else {
                         res['data'] = angular.fromJson(res.jsonStr);

                         if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                             getTnaDatas(allocation.GMT_PLN_LINE_LOAD_ID);
                         }
                         config.appToastMsg(res.data.PMSG);
                     }
                 });
             });

            


        }
    }

})();