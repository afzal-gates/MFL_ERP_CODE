
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutRejectedPrintEmbrModalController', ['$q', 'config', 'CuttingDataService', '$modalInstance', '$scope', 'BundleCaradList', 'Dialog', CutRejectedPrintEmbrModalController]);
    function CutRejectedPrintEmbrModalController($q, config, CuttingDataService, $modalInstance, $scope, BundleCaradList, Dialog) {

        $scope.cancel = function () {
            $modalInstance.dismiss();
        }
        $scope.SL = 1;

        $scope.selectedDefect = null;

        $scope.MAX_SL = _.maxBy(BundleCaradList, function (o) { return o.SL; }).SL;
        $scope.BundleCaradList = BundleCaradList;
        $scope.CurrentBundle = BundleCaradList[0];
        


        function setCurrentBundle() {
            var idx = $scope.BundleCaradList.findIndex(function (o) { return o.SL === $scope.SL });
            if (idx > -1) {
                $scope.selectedDefect = null;
                $scope.CurrentBundle = $scope.BundleCaradList[idx];
            }
        };

        $scope.onChange = function (item) {
            item['PAIRED_QTY'] = _.min(item.details.map(function (x) { return x.QTY_IN_BNDL - x.REJECT_QTY - x.REJECT_QTY_FAB - x.SHORT_QTY + x.SURPLS_QTY + (x.ADJUST_QTY || 0) }));
            item['MAX_QTY'] = _.max(item.details.map(function (x) { return x.QTY_IN_BNDL - x.REJECT_QTY - x.REJECT_QTY_FAB  - x.SHORT_QTY + x.SURPLS_QTY + (x.ADJUST_QTY || 0) }));
            item['IS_CHANGE'] = true;

            //setTimeout(function () {
            //    angular.forEach(item.details, function (val) {
            //        val['PAIRED_QTY'] = item.PAIRED_QTY + (val.ADJUST_QTY || 0);
            //    });
            //}, 200)

        };

        $scope.onChange(BundleCaradList[0]);


        $scope.prev = function () {
            if ($scope.SL < 2) { return;}
            $scope.SL--;
            setCurrentBundle();
        }
        $scope.next = function () {
            if ( $scope.SL > $scope.MAX_SL) { return; }
            $scope.SL++;
            setCurrentBundle();
        }

        $scope.save = function (data) {
            
            var count = data.filter(function (ob) { return !ob.IS_CHANGE; }).length;
            var msg = count == 0 ? 'Saving data...' : 'Still ' + count + ' data is pending to update. Do you want to save?';
            var pXML;


          Dialog.confirm('<h5 style="margin:0px;">' + msg + '</h5', 'Are You Sure?', ['Yes', ' No'])
          .then(function () {
              pXML = config.xmlStringShort(data.filter(function (ob) { return ob.IS_CHANGE; }).map(function (ob) {
                  return {
                      GMT_CUT_PRN_RCV_CHL_D_ID: ob.GMT_CUT_PRN_RCV_CHL_D_ID,
                      XML_D: config.xmlStringShortNoTag(ob.details.filter(function (o) { return o.IS_ACTIVE; }).map(function (o) {
                                      return {
                                          RCV_QTY: o.QTY_IN_BNDL - o.REJECT_QTY - o.REJECT_QTY_FAB  - o.SHORT_QTY + o.SURPLS_QTY,
                                          SHORT_QTY: o.SHORT_QTY,
                                          SURPLS_QTY: o.SURPLS_QTY,
                                          REJECT_QTY: o.REJECT_QTY,
                                          REJECT_QTY_FAB: o.REJECT_QTY_FAB,
                                          ADJUST_QTY: o.ADJUST_QTY,
                                          PAIRED_QTY: ob.PAIRED_QTY
                                      }
                                  })
                                  )
                              }
                          })
                );

              return CuttingDataService.saveDataByUrl({pXML : pXML, pOption: 1007} , '/CutCutPnlInspect/SaveDefectData').then(function (res) {
                  res['data'] = angular.fromJson(res.jsonStr);
                  if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                      $modalInstance.close();
                  }
              });

              });
        }
      
    }

})();
