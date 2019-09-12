(function () {
    'use strict';
    angular.module('multitex.planning').controller('GmtPlanChangeModalController', ['$q', 'config', 'PlanningDataService', '$scope', '$modalInstance', '$modal', 'data', 'Dialog', 'ResDept', GmtPlanChangeModalController]);
    function GmtPlanChangeModalController($q, config, PlanningDataService, $scope, $modalInstance, $modal, data, Dialog, ResDept) {
        $scope.data = data;
        $scope.ResDeptDs = ResDept;
        $scope.showSplash = false;
        getPlansData();
        getStyleChangeData();
        $scope.cancel = function () {
            $modalInstance.dismiss(0);
        };


        function getPlansData() {
            return PlanningDataService.getDataByUrl('/GmtLineLoad/getProdPlanDataByLine?pOption=3022&pHR_PROD_LINE_ID=' + data.HR_PROD_LINE_ID + '&pSTART_DT=' + data.SEW_END_DT)
            .then(function (res) {
                $scope.others_allocations = res;
            });
        };

        function getStyleChangeData() {
            return PlanningDataService.getDataByUrl('/GmtLineLoad/GetStyleChangeLogDataById?pGMT_PLN_LINE_LOAD_ID=' + data.GMT_PLN_LINE_LOAD_ID + '&pGMT_PLN_STYL_CNGE_LOG_ID=' + data.GMT_PLN_STYL_CNGE_LOG_ID)
            .then(function (res) {
                $scope.FormData = res;
            });
        }

        $scope.onSelectAll = function (is_selectted) {
            angular.forEach($scope.others_allocations, function (val, key) {
                if ( val.GMT_PLN_STRIPE_PHASE_ID == 2 ) {
                    val['IS_SELECTED'] = is_selectted;
                }
            }); 
        }


        $scope.$watch('FormData.items', function (newVal, oldVal) {

            if (newVal && newVal.length > 0) {
                var ttl = newVal.reduce(function (ttl, val) {
                    return ttl + (val.PCT_SHARE || 0);
                }, 0);


                var isDeptColDup = _.some( _.map(_.groupBy(newVal, function (doc) {
                    return doc.RF_RESP_DEPT_ID;
                }), function (grouped) {
                    return grouped.length;
                }), function (o) { return o > 1 });

                if (!isDeptColDup) {
                    $scope.GmtPlanChangeForm['PLN_CHANE_NO'].$setValidity('DeptDup', true);

                } else {
                    $scope.GmtPlanChangeForm['PLN_CHANE_NO'].$setValidity('DeptDup', false);
                }

                if (ttl == 100) {
                    $scope.GmtPlanChangeForm['PLN_CHANE_NO'].$setValidity('incorrect', true);
                    
                } else {
                    $scope.GmtPlanChangeForm['PLN_CHANE_NO'].$setValidity('incorrect', false);
                }
            }
         

        }, true)

        $scope.SaveData = function (data, valid) {
            if (!valid) { return; }

            Dialog.confirm('Marking as Plan Change?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
              .then(function () {
                  var data2save = Object.assign({}, data);

                  data2save['pOption'] = 1000;
                  data2save['XML'] = config.xmlStringShort(data2save.items.map(function (x) {
                      return {
                          GMT_PLN_STYL_CNGE_RES_ID: x.GMT_PLN_STYL_CNGE_RES_ID,
                          RF_RESP_DEPT_ID: x.RF_RESP_DEPT_ID,
                          PCT_SHARE: x.PCT_SHARE,
                          PLAN_CNGE_REASON: x.PLAN_CNGE_REASON
                      }
                  }));

                  data2save['OTHER_ALLOC_XML'] = config.xmlStringShort($scope.others_allocations.filter(function (o) { return o.IS_SELECTED === 'Y'; }).map(function (x) {
                      return {
                          GMT_PLN_LINE_LOAD_ID: x.GMT_PLN_LINE_LOAD_ID
                      }
                  })
                  );

                  return PlanningDataService.saveDataByUrl(data2save, '/GmtLineLoad/SavePlanChangeData').then(function (res) {
                      if (res.success === false) {
                          vm.errors = res.errors;
                      }
                      else {
                          res['data'] = angular.fromJson(res.jsonStr);
                          if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                              $modalInstance.close();
                          }
                          config.appToastMsg(res.data.PMSG);
                      }
                  });

              });

          

        };
    }

})();