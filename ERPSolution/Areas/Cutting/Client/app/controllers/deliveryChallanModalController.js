
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('DeliveryChallanModalController', ['$q', 'config', 'CuttingDataService', '$modalInstance', '$scope', 'BundleDatas', 'Dialog', DeliveryChallanModalController]);
    function DeliveryChallanModalController($q, config, CuttingDataService, $modalInstance, $scope, BundleDatas, Dialog) {



        $scope.BundleDatas = BundleDatas;
        $scope.GMT_CUT_PRN_DELV_CHL_H_LST = null;

        //getServiceData();
        getSupplierCatData();
        getSupplierList();
        getGmtPartList();
        
       

        $scope.challanList = [{
            GMT_CUT_PRN_DELV_CHL_H_ID: -1, RF_GARM_PART_LST: '', LK_BNDL_MVM_RSN_ID: '', LK_SUPPLIER_CAT: 479, SCM_SUPPLIER_ID: 562, IS_TAG: 'N', IS_FINALIZED : 'N'
        }];

        $scope.DsService = new kendo.data.DataSource({
            data: [
                 { LOOKUP_DATA_ID: 692, LK_DATA_NAME_EN: 'Printing' },
                 { LOOKUP_DATA_ID: 693, LK_DATA_NAME_EN: 'Embroidery' },
                 { LOOKUP_DATA_ID: 694, LK_DATA_NAME_EN: 'HeatSeal' },
                 { LOOKUP_DATA_ID: 695, LK_DATA_NAME_EN: 'Printing+Embroidery' }
            ]
        });


        $scope.addNew = function () {
            $scope.challanList.push({
                GMT_CUT_PRN_DELV_CHL_H_ID: -1, RF_GARM_PART_LST: '', LK_BNDL_MVM_RSN_ID: '', LK_SUPPLIER_CAT: 479, SCM_SUPPLIER_ID: 562, IS_TAG: 'N', IS_FINALIZED : 'N'
            })
        }



        $scope.printDelivChallan = function (challan) {

            var url = '/api/Cutting/CutReport/PreviewReportRDLC';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');
            challan.REPORT_CODE = 'RPT-4515';

            var params = angular.copy(challan);

            console.log(params);

            for (var i in params) {
                if (params.hasOwnProperty(i)) {

                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    input.value = params[i];
                    form.appendChild(input);
                }
            }

            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);

        };


        

        $scope.cancel = function () {
            $modalInstance.dismiss();
        }

        $scope.onClose = function () {
            $modalInstance.close();
        };

        function IsEqual (oblist1, oblist2) {
            for (var i = 0; i < oblist1.length; i++) {
                if (oblist1[i].RF_GARM_PART_LST === oblist2[i].RF_GARM_PART_LST &&
               oblist1[i].LK_SUPPLIER_CAT === oblist2[i].LK_SUPPLIER_CAT &&
               oblist1[i].SCM_SUPPLIER_ID === oblist2[i].SCM_SUPPLIER_ID &&
               oblist1[i].CHALAN_NO === oblist2[i].CHALAN_NO &&
               oblist1[i].NO_OF_BAG === oblist2[i].NO_OF_BAG &&
               oblist1[i].GATE_PASS_NO === oblist2[i].GATE_PASS_NO &&
               oblist1[i].VEHICLE_NO === oblist2[i].VEHICLE_NO) {
                  return true;
              } 
    
            }
            return false;
        }

        function getPrintDeliveryChallan(pGMT_CUT_PRN_DELV_CHL_H_LST) {  
            return CuttingDataService.getDataByUrl('/GmtCutBank/getPrintDeliveryChallan?pOption=3000&pGMT_CUT_PRN_DELV_CHL_H_LST=' + pGMT_CUT_PRN_DELV_CHL_H_LST)
               .then(function (res) {
                   $scope.challanList = res;
                   $scope.isLocked = _.some(res, function (o) { return o.IS_FINALIZED === 'Y'; })
               });
        }
            
        //function getServiceData() {
        //    return CuttingDataService.getDataByFullUrl('/api/common/LookupListData/142').then(function (res) {
                
        //        $scope.DsService = new kendo.data.DataSource({
        //            data: res.filter(function (o) { return [692, 693, 694,695].indexOf(o.LOOKUP_DATA_ID) > -1; })

        //        });
        //    });
        //}

        function getGmtPartList() {
            return CuttingDataService.getDataByFullUrl('/api/common/GmtPartList?pRF_GARM_PART_LST=' + BundleDatas.RF_GARM_PART_LST).then(function (res) {
                $scope.PartDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        function getSupplierCatData() {
            return CuttingDataService.getDataByFullUrl('/api/common/LookupListData/89').then(function (res) {
                
                $scope.DsSupCat = new kendo.data.DataSource({
                    data: res
                });

            });
        }

        function getSupplierList() {
            return CuttingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=717').then(function (res) {
                
                $scope.DsSup = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        $scope.submitData = function (data, valid) {
            if (!valid) {
                return;
            }

            
            return CuttingDataService.saveDataByUrl({
                XML: config.xmlStringShort(data.map(function (obj) {
                     return {
                      GMT_CUT_PRN_DELV_CHL_H_ID: obj.GMT_CUT_PRN_DELV_CHL_H_ID, RF_GARM_PART_LST : obj.RF_GARM_PART_LST.join(','),LK_BNDL_MVM_RSN_ID : obj.LK_BNDL_MVM_RSN_ID, 
                      LK_SUPPLIER_CAT: obj.LK_SUPPLIER_CAT, SCM_SUPPLIER_ID: obj.SCM_SUPPLIER_ID, IS_TAG: obj.IS_TAG, NO_OF_BAG: obj.NO_OF_BAG, GATE_PASS_NO: obj.GATE_PASS_NO,
                      VEHICLE_NO: obj.VEHICLE_NO
                       }
                    })
                )
                ,
                GMT_CUT_PNL_BNK_LST: BundleDatas.GMT_CUT_PNL_BNK_LST,
                pOption: 1003

            }, '/GmtCutBank/SaveAndOrDelete').then(function (res) {
                if (res.success === false) {
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        
                        $scope.GMT_CUT_PRN_DELV_CHL_H_LST = res.data.OP_GMT_CUT_PRN_DELV_CHL_H_LST;

                        getPrintDeliveryChallan(res.data.OP_GMT_CUT_PRN_DELV_CHL_H_LST);
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            })

        }
        $scope.FinalizeAndDeliver = function (data, valid) {
            if (!valid) {
                return;
            }

            if (data.every(function (o) { return parseInt(o.GMT_CUT_PRN_DELV_CHL_H_ID) > 1 && parseInt(o.SCM_SUPPLIER_ID) > 1; })) {

                 Dialog.confirm('<h5 style="margin:0px;">Finalizing and Delivering Challan...</h5', 'Are you sure?', ['Yes', ' No'])
                         .then(function () {
                             return CuttingDataService.saveDataByUrl({
                                 XML: config.xmlStringShort(data.map(function (obj) {
                                     return {
                                         GMT_CUT_PRN_DELV_CHL_H_ID: obj.GMT_CUT_PRN_DELV_CHL_H_ID, RF_GARM_PART_LST : obj.RF_GARM_PART_LST.join(','),LK_BNDL_MVM_RSN_ID : obj.LK_BNDL_MVM_RSN_ID, 
                                         LK_SUPPLIER_CAT: obj.LK_SUPPLIER_CAT, SCM_SUPPLIER_ID: obj.SCM_SUPPLIER_ID, IS_TAG: 'N', NO_OF_BAG: obj.NO_OF_BAG, GATE_PASS_NO: obj.GATE_PASS_NO,
                                         VEHICLE_NO: obj.VEHICLE_NO
                                               }
                                     })
                                 ),
                                 GMT_CUT_PRN_DELV_CHL_H_LST: $scope.GMT_CUT_PRN_DELV_CHL_H_LST,
                                 IS_FINALIZED: 'Y',
                                 GMT_CUT_PNL_BNK_LST: BundleDatas.GMT_CUT_PNL_BNK_LST,

                             }, '/GmtCutBank/SavePrintEmbrDelChallan').then(function (res) {
                                 if (res.success === false) {
                                 }
                                 else {
                                     res['data'] = angular.fromJson(res.jsonStr);
                                     if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                         getPrintDeliveryChallan($scope.GMT_CUT_PRN_DELV_CHL_H_LST);
                                     }
                                     config.appToastMsg(res.data.PMSG);
                                 }
                             })
                 });


            } else {
                config.appToastMsg('MULTI-002Please Complete Challan Data Properly');
            }


        }

    }
})();
