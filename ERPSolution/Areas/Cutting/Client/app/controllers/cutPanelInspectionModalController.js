
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutPanelInspectionModalController', ['$q', 'config', 'CuttingDataService', '$modalInstance', '$scope', 'BundleCaradList', 'Dialog', CutPanelInspectionModalController]);
    function CutPanelInspectionModalController($q, config, CuttingDataService, $modalInstance, $scope, BundleCaradList, Dialog) {

        $scope.cancel = function () {
            $modalInstance.dismiss();
        }
        $scope.SL = 1;

        $scope.selectedDefect = null;

        $scope.MAX_SL = _.maxBy(BundleCaradList, function (o) { return o.SL; }).SL;
        $scope.BundleCaradList = BundleCaradList;
        $scope.CurrentBundle = $scope.BundleCaradList[0];

        function setCurrentBundle() {
            var idx = $scope.BundleCaradList.findIndex(function (o) { return o.SL === $scope.SL });
            if (idx > -1) {
                $scope.selectedDefect = null;
                $scope.CurrentBundle = $scope.BundleCaradList[idx];
            }
        };

        $scope.setTotalPnlRej = function() {
            $scope.CurrentBundle.NO_OF_PANEL_REJ = _.sumBy($scope.CurrentBundle.defects, function (o) { return (o.NO_OF_DFCT||0); });
        };

        $scope.onTrimDigit = function () {
            var data = Object.assign({}, $scope.selectedDefect);
            $scope.selectedDefect.NO_OF_DFCT = parseInt(data.NO_OF_DFCT.toString().substring(0, (data.NO_OF_DFCT.toString().length - 1)) || 0);
            $scope.setTotalPnlRej();
        };

        $scope.setSelected = function (itm, list) {

            angular.forEach(list, function (o) {
                o['SELECTED'] = false;
            });

            itm['SELECTED'] = true;
            $scope.selectedDefect = itm;
        };

        $scope.addOne = function (item, val) {
            if (item.SELECTED) {
                item.NO_OF_DFCT = item.NO_OF_DFCT + val;
                $scope.setTotalPnlRej();
            }
        }

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
            
            var count = data.filter(function (ob) { return (ob.NO_OF_PANEL_REJ || 0) == 0; }).length;
            var msg = count == 0 ? 'Saving data...' : 'Still ' + count + ' data is pending to update. Do you want to save?';
            var pXML;


          Dialog.confirm('<h5 style="margin:0px;">' + msg + '</h5', 'Are You Sure?', ['Yes', ' No'])
          .then(function () {
              pXML = config.xmlStringShort(data.filter(function (ob) { return (ob.NO_OF_PANEL_REJ || 0) > 0; }).map(function (ob) {
                  return {
                      GMT_CUT_PNL_INSPTN_H_ID: ob.GMT_CUT_PNL_INSPTN_H_ID,
                      NO_OF_PANEL_REJ: ob.NO_OF_PANEL_REJ,
                      XML_D: config.xmlStringShortNoTag(ob.defects.filter(function (o) { return o.NO_OF_DFCT > 0; }).map(function (o) {
                          return {
                              GMT_CUT_PNL_INSPTN_D_ID: o.GMT_CUT_PNL_INSPTN_D_ID,
                              RF_FB_DFCT_TYPE_ID: o.RF_FB_DFCT_TYPE_ID,
                              NO_OF_DFCT: o.NO_OF_DFCT
                          }
                      })
                      )
                  }
              })
                  );

              console.log(pXML);

              return CuttingDataService.saveDataByUrl({pXML : pXML} , '/CutCutPnlInspect/SaveDefectData').then(function (res) {
                  res['data'] = angular.fromJson(res.jsonStr);
                  if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                      $modalInstance.close();
                  }
              });

              });
        }
      
    }

})();
