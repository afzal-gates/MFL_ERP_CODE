(function () {
    'use strict';
    angular.module('multitex.planning').controller('GmtLineLoadingEventOffsetModalController', ['$q', 'config', 'PlanningDataService', '$scope', '$modalInstance', '$modal', 'data', 'Dialog', GmtLineLoadingEventOffsetModalController]);
    function GmtLineLoadingEventOffsetModalController($q, config, PlanningDataService, $scope, $modalInstance, $modal, data, Dialog) {

        $scope.data = data;
        $scope.showSplash = false;
        getPlansData();

        $scope.cancel = function () {
            $modalInstance.dismiss(0);
        };

        function getPlansData() {
            return PlanningDataService.getDataByUrl('/GmtLineLoad/getProdPlanDataByLine?pOption=3022&pHR_PROD_LINE_ID=' + data.HR_PROD_LINE_ID + '&pSTART_DT=' + data.SEW_END_DT)
            .then(function (res) {
                $scope.others_allocations = res.map(function (o, k) {
                    if (k == 0) {
                        o['IS_SELECTED'] = 'Y';
                    } else {
                        o['IS_SELECTED'] = 'N';
                    }
                    
                    return o;
                });
            });
        };

        $scope.onSelectAll = function (is_selectted) {
            angular.forEach($scope.others_allocations, function (val, key) {
                if (!(val.GMT_PLN_STRIPE_PHASE_ID == 2 || val.GMT_PLN_STRIPE_PHASE_ID == 5)) {
                    val['IS_SELECTED'] = is_selectted;
                }
            }); 
        }

        $scope.SaveData = function (data, valid) {
            if (!valid || data.PLN_OFFSET == 0) { return; }

            Dialog.confirm('Offseting Plan by '+data.PLN_OFFSET+'Hrs ?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                 .then(function () {
                         var data2save = Object.assign({}, data);
                         data2save['pOption'] = 1009;
                         data2save['XML'] = config.xmlStringShort($scope.others_allocations.filter(function (o) { return o.IS_SELECTED === 'Y'; }).map(function (x) {
                             return {
                                 GMT_PLN_LINE_LOAD_ID: x.GMT_PLN_LINE_LOAD_ID
                             }
                         })
                         );
                         $scope.showSplash = true;
                         return PlanningDataService.saveDataByUrl(data2save, '/GmtLineLoad/updateEventForTuning').then(function (res) {
                             if (res.success === false) {
                                 vm.errors = res.errors;
                             }
                             else {
                                 res['data'] = angular.fromJson(res.jsonStr);

                                 if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                     $scope.showSplash = false;
                                     $modalInstance.close();
                                 }
                                 config.appToastMsg(res.data.PMSG);
                             }
                         });
                 });
        };
    }

})();