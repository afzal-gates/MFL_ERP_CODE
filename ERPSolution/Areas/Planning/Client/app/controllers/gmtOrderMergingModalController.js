(function () {
    'use strict';
    angular.module('multitex.planning').controller('GmtOrderMergingModalController', ['$q', 'config', 'PlanningDataService', '$scope', '$modalInstance', '$modal', 'data', 'Dialog', GmtOrderMergingModalController]);
    function GmtOrderMergingModalController($q, config, PlanningDataService, $scope, $modalInstance, $modal, data, Dialog) {
        $scope.data = data;
        $scope.showSplash = false;
        getOrderPlnList();
        $scope.cancel = function () {
            $modalInstance.dismiss(0);
        };

        function getOrderPlnList() {
            return PlanningDataService.getDataByUrl('/GmtLineLoad/getOrderItmPlanList?pMC_STYLE_H_ID=' + data.MC_STYLE_H_ID + '&pMC_STYLE_D_ITEM_ID=' + data.MC_STYLE_D_ITEM_ID
                + '&pMC_ORDER_ITEM_PLN_ID=' + data.MC_ORDER_ITEM_PLN_ID)
            .then(function (res) {
                $scope.OrderPlanList = res;
            });
        };

        $scope.onSelectAll = function (is_selectted) {
            angular.forEach($scope.OrderPlanList, function (val, key) {
                val['IS_SELECTED'] = is_selectted;
            }); 
        }

        $scope.SaveData = function (dataList) {
            if (dataList.filter(function (o) { return o.IS_SELECTED == 'Y'; }).length == 0) {
                config.appToastMsg('MULTI-003Opps! You did not select any Order');
                return;
            }

            Dialog.confirm('Merging Orders?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
              .then(function () {
                  var data2save = {};
                  data2save['pOption'] = 1001;
                  data2save['MC_ORDER_ITEM_PLN_ID'] = data.MC_ORDER_ITEM_PLN_ID;
                  data2save['XML'] = config.xmlStringShort(dataList.filter(function (o) { return o.IS_SELECTED == 'Y'; }).map(function (x) {
                      return {
                          MC_ORDER_ITEM_PLN_ID: x.MC_ORDER_ITEM_PLN_ID,
                          ORDER_QTY: x.ORDER_QTY
                      }
                  }));

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