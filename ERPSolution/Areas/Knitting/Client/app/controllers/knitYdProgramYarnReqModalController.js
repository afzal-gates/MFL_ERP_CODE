(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitYdProgramYarnReqModalController', ['$q', 'config', 'KnittingDataService', '$scope', '$modalInstance', '$modal', 'YdPrgData', 'YrnStrReq', 'Dialog', KnitYdProgramYarnReqModalController]);
    function KnitYdProgramYarnReqModalController($q, config, KnittingDataService, $scope, $modalInstance, $modal, YdPrgData, YrnStrReq, Dialog) {

        var vm = this;
        $scope.showSplash = true;

        $scope.ydPrgData = YdPrgData;
        $scope.yrnStrReq = YrnStrReq;

        console.log($scope.ydPrgData);
        console.log($scope.yrnStrReq);

        $scope.yrnList4Req = [];
        
        activate();
        
        $scope.template = '<span><h5 style="padding:0px;margin:0px;"><b>#: data.YRN_LOT_NO #</b></h5><p> Stock: #: data.STK_BALANCE # #: data.MOU_CODE #</p></span>';
        $scope.Yarntemplate = '<span><span style="padding:0px;margin:0px;">#: data.ITEM_NAME_EN # #if (data.PCT_RATIO>0)  { #<span class="label label-sm label-danger" style="padding:0px;margin:0px;">Suggested</span># } # </span>';
        $scope.value_template = '<span><h5 style="padding:0px;margin:0px;"><b>#: data.YRN_LOT_NO # </b> (#: data.STK_BALANCE # #: data.MOU_CODE #)</h5></span>';



        function activate() {
            var promise = [getYrnRqdList4Yd()];
            return $q.all(promise).then(function () {
                $scope.showSplash = false;
            });
        }

        function getYrnRqdList4Yd() {
            
            return KnittingDataService.getDataByFullUrl("/api/Knit/KnitYdProgram/GetYrnRqdList4Yd?pKNT_YD_PRG_H_ID=" + (YdPrgData.KNT_YD_PRG_H_ID || 0) + "&pKNT_YRN_STR_REQ_H_ID=" + (YrnStrReq.KNT_YRN_STR_REQ_H_ID || 0)).then(function (res) {
                angular.forEach(res, function (val, key) {
                    val['brandListDs'] = new kendo.data.DataSource({
                        data: val['itemsBrand']
                    });

                    $scope.yrnList4Req.push(val);
                });

            }, function (err) {
                console.log(err);
            });
        };


        $scope.onChangeBrnad = function (item, e, base, index) {
            var objBrand = e.sender.dataItem(e.sender.item);
            console.log(objBrand);

            if (objBrand.RF_BRAND_ID) {
                return KnittingDataService.getDataByFullUrl('/api/Knit/KnitPlan/getYarnLot?pRF_BRAND_ID=' + objBrand.RF_BRAND_ID + '&pYARN_ITEM_ID=' + item.YARN_ITEM_ID).then(function (res) {
                    item['yarnLotListDs'] = new kendo.data.DataSource({
                        data: res
                    });


                    //if (index == 0) {
                    //    angular.forEach($scope.form.col_reqs, function (val, key) {
                    //        if (!val.RF_BRAND_ID) {
                    //            val['RF_BRAND_ID'] = objBrand.RF_BRAND_ID;;
                    //            val['yarnLotListDs'] = new kendo.data.DataSource({
                    //                data: res
                    //            });
                    //        }
                    //    });
                    //}

                });

            } else {
                item.yarnLotListDs = new kendo.data.DataSource({
                    data: []
                });
            }
        }
        $scope.onBoundBrnad = function (item, base, index) {
            if (item.YARN_ITEM_ID && item.RF_BRAND_ID) {
                return KnittingDataService.getDataByFullUrl('/api/Knit/KnitPlan/getYarnLot?pRF_BRAND_ID=' + item.RF_BRAND_ID + '&pYARN_ITEM_ID=' + item.YARN_ITEM_ID).then(function (res) {
                    item['yarnLotListDs'] = new kendo.data.DataSource({
                        data: res
                    });

                    //if (base) {
                    //    angular.forEach($scope.datas, function (val, key) {
                    //        if (val['items'][index]['KNT_PLAN_D_ID'] < 0) {
                    //            val['items'][index]['RF_BRAND_ID'] = item.RF_BRAND_ID;
                    //            val['items'][index]['yarnLotListDs'] = new kendo.data.DataSource({
                    //                data: res
                    //            });
                    //        }
                    //    });
                    //}

                });
            }
        }


        //vm.onChangeBrnad = function (item) {

        //    console.log(item);

        //    return KnittingDataService.getDataByFullUrl('/api/KnitPlan/getYarnLot?&pRF_BRAND_ID=' + item.RF_BRAND_ID + '&pYARN_ITEM_ID=' + item.YARN_ITEM_ID).then(function (res) {
        //        item['yarnLotListDs'] = new kendo.data.DataSource({
        //            data: res
        //        });

        //    });
        //}


        $scope.cancel = function () {
            $modalInstance.dismiss();
        };

        $scope.createRequisition = function () {
            var submitReqData = {};
            var vMsg = 'Do you want to save?';


            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                submitReqData['KNT_YD_PRG_H_ID'] = YdPrgData.KNT_YD_PRG_H_ID;
                //submitReqData['RCV_DT'] = $filter('date')(vm.form.RCV_DT, vm.dtFormat);
                
                submitReqData.YDP_YRN_REQ_XML = KnittingDataService.xmlStringShort($scope.yrnList4Req.map(function (ob) {
                    return {
                        KNT_YRN_STR_REQ_D_ID: ob.KNT_YRN_STR_REQ_D_ID, KNT_YRN_STR_REQ_H_ID: ob.KNT_YRN_STR_REQ_H_ID, MC_FAB_PROD_ORD_H_ID: ob.MC_FAB_PROD_ORD_H_ID, KNT_YDP_D_COL_ID: ob.KNT_YDP_D_COL_ID,
                        YARN_ITEM_ID: ob.YARN_ITEM_ID, RF_BRAND_ID: ob.RF_BRAND_ID, KNT_YRN_LOT_ID: ob.KNT_YRN_LOT_ID, RQD_YD_QTY: ob.RQD_YD_QTY, RQD_YRN_QTY: ob.RQD_YRN_QTY,
                        RQD_CONE_QTY: ob.RQD_CONE_QTY
                    };
                }));

                console.log(submitReqData);
                //return;

                return KnittingDataService.saveDataByFullUrl(submitReqData, "/api/Knit/KnitYdProgram/CreateRequisition").then(function (res) {
                    
                    $scope.errors = undefined;
                    if (res.success === false) {
                        $scope.errors = [];
                        $scope.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            //vm.form.KNT_CLCF_STR_RCV_H_ID = res['data'].PKNT_CLCF_STR_RCV_H_ID_RTN;                            
                            //$state.go('KntCollarCuffStrRcvH', { pKNT_CLCF_STR_RCV_H_ID: res['data'].PKNT_CLCF_STR_RCV_H_ID_RTN }, { notify: false });

                            //vm.isSaved = true;

                            $modalInstance.close(submitReqData);
                        };

                        config.appToastMsg(res.data.PMSG);
                    }

                }, function (err) {
                    console.log(err);
                });

            });
        }


    }

})();