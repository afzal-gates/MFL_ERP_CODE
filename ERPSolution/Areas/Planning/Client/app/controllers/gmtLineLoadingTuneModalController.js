(function () {
    'use strict';
    angular.module('multitex.planning').controller('GmtLineLoadingTuneModalController', ['$q', 'config', 'PlanningDataService', '$scope', '$modalInstance', '$modal', 'data', 'ResDept', 'Dialog', GmtLineLoadingTuneModalController]);
    function GmtLineLoadingTuneModalController($q, config, PlanningDataService, $scope, $modalInstance, $modal, data, ResDept, Dialog) {

        $scope.data = data;
        $scope.IS_REFRESH_DISABLED = false;
        $scope.showSplash = false;
        //$scope.ResDeptDs = new kendo.data.DataSource({
        //    data: ResDept
        //});
        $scope.ResDeptDs = ResDept;
        $scope.cancel = function () {
            $modalInstance.dismiss(0);
        };
        loadResources();
        var date = new Date();
        $scope.today = new Date(date.getFullYear(),date.getMonth(), date.getDay(), date.getHours());
      

        $scope.datePickerOptions = {
            parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
            format: "dd-MMM-yyyy hh:mm tt"
        };

        $scope.openTNATasks = function (data) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Planning/Pln/_TnaDataListModal',
                controller: 'GmtLineLoadTnaDatasModalController',
                size: 'lg',
                windowClass: 'app-modal-window',

                resolve: {
                    data: function () {
                        return PlanningDataService.getDataByUrl('/GmtLineLoad/getTnaDatas?pGMT_PLN_LINE_LOAD_ID=' + data.GMT_PLN_LINE_LOAD_ID);
                    },
                    summery: function () {
                        return PlanningDataService.getDataByUrl('/GmtLineLoad/GetCpSummeryData?pGMT_PLN_LINE_LOAD_ID=' + data.GMT_PLN_LINE_LOAD_ID);
                    },
                    allocation: function () {
                        return data;
                    }
                }
            });
            modalInstance.result.then(function (dta) {
                getRefreshTuningData(data.GMT_PLN_LINE_LOAD_ID);
            }, function () {
                getRefreshTuningData(data.GMT_PLN_LINE_LOAD_ID);
            });
        }

        $scope.tuning = function (GMT_PLN_LINE_LOAD_ID) {
            $modalInstance.close(GMT_PLN_LINE_LOAD_ID);
        };


        $scope.holidayTypeList = {
            optionLabel: "Select",
            dataSource: {
                transport: {
                    read: {
                        url: "/Hr/HrYearlyCalander/DayTypeListData",
                        dataType: "json"
                    }
                }
            },
            dataTextField: "DAY_TYPE_NAME_EN",
            dataValueField: "HR_DAY_TYPE_ID"
        };

        $scope.$watchGroup(['data.PLAN_OP', 'data.PLAN_WH', 'data.PLAN_EFFICIENCY', 'data.PLAN_HP'], function (newVal, oldVal) {

            if (oldVal && (newVal[0] != oldVal[0])) {
                angular.forEach($scope.data.details, function (val, key) {
                    if ( val.STS_FLAG == 'U' && val.IS_INDIV_CHANGE == 'N') {
                        val['PLAN_OP'] = newVal[0];
                        val['PLAN_HP'] = newVal[3];
                        val['OT_MP'] = newVal[0] + newVal[3];
                        val['PLAN_MP'] = newVal[0] + newVal[3];
                        val['IS_INDIV_CHANGE'] = 'N';
                    }
                });
            }
            if (oldVal && (newVal[1] != oldVal[1])) {
                angular.forEach($scope.data.details, function (val, key) {
                    if (val.IS_LC_APPLIED == 'N' && val.STS_FLAG == 'U' && val.IS_INDIV_CHANGE == 'N') {
                        val['OT_HR'] = (newVal[1] > 8) ? (newVal[1] - 8) : 0;
                        val['IS_INDIV_CHANGE'] = 'N';
                    }
                });
            }
            if (oldVal && (newVal[2] != oldVal[2])) {
                angular.forEach($scope.data.details, function (val, key) {
                    if (val.IS_LC_APPLIED == 'N' && val.STS_FLAG == 'U' && val.IS_INDIV_CHANGE == 'N') {
                        val['RF_PLAN_EFF'] = newVal[2];
                        val['IS_INDIV_CHANGE'] = 'N';
                    }
                });
            }
        });


        function getRefreshTuningData(pGMT_PLN_LINE_LOAD_ID) {
            return PlanningDataService.getDataByUrl('/GmtLineLoad/getEventDataForTuning?pGMT_PLN_LINE_LOAD_ID=' + pGMT_PLN_LINE_LOAD_ID)
              .then(function (res) {
                  $scope.data = res;
              });
        };

        $scope.findSewingEndDate2 = function (item) {
            $scope.showSplash = true;
            var url = '/GmtLineLoad/getForcastLineLoadDataById2?pGMT_PLN_LINE_LOAD_ID=' + item.GMT_PLN_LINE_LOAD_ID + '&pIS_DYNAMIC_END=Y';
            return PlanningDataService.getDataByUrl(url).then(function (res) {
                $scope.showSplash = false;
                $scope.data['SEW_START_DT'] = res.SEW_START_DT;
            });
        }


        $scope.findSewingEndDate = function (item) {
            $scope.showSplash = true;
            var url = '/GmtLineLoad/getForcastLineLoadDataById?pGMT_PLN_LINE_LOAD_ID=' + item.GMT_PLN_LINE_LOAD_ID + '&pIS_DYNAMIC_START=Y';
            return PlanningDataService.getDataByUrl(url).then(function (res) {
                $scope.showSplash = false;
                $scope.data['SEW_END_DT'] = res.SEW_END_DT;
            });
        }


        $scope.template = '<span><h5 style="padding:0px;margin:0px;color:#=data.LINE_LBL_COLOR#;">#: data.name # [#:data.TTL_ACTV_POINT#]</h5</span>';
        //$scope.template = '<span><h6 style="padding:0px;margin:0px;color:#=data.LINE_LBL_COLOR#;">#: data.name #</h6><h6><p> #: data.LINE_CRITICALITY#</p></h6></span>';
        $scope.valueTemplate = '<span><h5 style="padding:0px;margin:0px;color:#=data.LINE_LBL_COLOR#;">#: data.name # [#:data.TTL_ACTV_POINT#]</h5></span>';
        function loadResources() {
            return PlanningDataService.getDataByUrl('/GmtLineLoad/getResourceDataDD').then(function (res) {
                $scope.resourceDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }


        $scope.deletePlanData = function (data, IsMain) {
            Dialog.confirm('<h5 style="margin:0px;"> Line #: <b> ' + data.LINE_CODE + ' </b> &nbsp;  Allocation # : <b> ' + data.ALLOCATED_QTY + '</b></h5', 'Deleting Plan...Are you sure?', ['Yes', ' No'])
                 .then(function () {
                     return PlanningDataService.saveDataByUrl(data, '/GmtLineLoad/DeletePlanData').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 if (IsMain) {
                                     $modalInstance.dismiss(1);
                                 } else {
                                     getRefreshTuningData($scope.data.GMT_PLN_LINE_LOAD_ID);
                                 }
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     });
                 });
        };


        


        $scope.UpdateComments = function (data) {
            
            var data2save = Object.assign({}, data);
            data2save.pOption = 1010;
            return PlanningDataService.saveDataByUrl(data2save, '/GmtLineLoad/updateEventForTuning').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        getRefreshTuningData(data2save.GMT_PLN_LINE_LOAD_ID);
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            });

        };


        $scope.SaveData = function (data, valid) {

            if (!valid) { return;}

            var data2save = Object.assign({}, data);

            data2save['SEW_START_DT'] = moment(data2save.SEW_START_DT).format();
            data2save['SEW_END_DT'] = moment(data2save.SEW_END_DT).format();

            data2save['PLAN_MP'] = (data.PLAN_OP + (data.PLAN_HP || 0));
            data2save['ALLOCATED_QTY'] = (data.ALLOCATED_QTY - (data.DE_ALLOCATED_QTY || 0));

            data2save['XML'] =config.xmlStringShort(data.details.map(function (x) {
                        return {
                            GMT_PLN_LINE_LOAD_D_ID: x.GMT_PLN_LINE_LOAD_D_ID,
                            OT_HR: x.OT_HR,
                            GEN_MP: x.GEN_MP,
                            OT_MP: x.GEN_MP,
                            PLAN_OP: x.PLAN_OP,
                            PLAN_HP: x.PLAN_HP,
                            RF_RESP_DEPT_ID: (x.OT_HR > 5 ? x.RF_RESP_DEPT_ID : ''), 
                            REASON_OF_EOT: x.REASON_OF_EOT,
                            RF_PLAN_EFF: x.RF_PLAN_EFF,
                            IS_INDIV_CHANGE: x.IS_INDIV_CHANGE
                        }
                    })
            );

            data2save['MERGE_XML'] = config.xmlStringShort(data.merges.map(function (x) {
                        return {
                            GMT_PLN_LINE_LD_MRG_ID: (x.GMT_PLN_LINE_LD_MRG_ID || -1),
                            MRG_PROD_LINE_ID: x.MRG_PROD_LINE_ID
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
                        getRefreshTuningData(data2save.GMT_PLN_LINE_LOAD_ID);
                        $scope.showSplash = false;
                        $scope.IS_REFRESH_DISABLED = false;
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            });

        };



    }

})();