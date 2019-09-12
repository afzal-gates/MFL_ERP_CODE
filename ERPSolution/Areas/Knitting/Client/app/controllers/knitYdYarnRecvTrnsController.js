(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitYdYrnRecvTrnsController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'cur_date_server', '$filter', 'Dialog', 'FormData', '$modal', '$window', KnitYdYrnRecvTrnsController]);
    function KnitYdYrnRecvTrnsController($q, config, KnittingDataService, $stateParams, $state, $scope, cur_date_server, $filter, Dialog, FormData, $modal, $window) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        $scope.form = {};
        $scope.alerts = [];
        activate()
       
        vm.showSplash = false;
        vm.params = $stateParams;


        function activate() {
            var promise = [ getMouList(), getSupplierList()];
            return $q.all(promise).then(function () {
                
            });
        }

        vm.fabProdOrderDs = new kendo.data.DataSource({
            data: FormData.orders
        });

        vm.onFabProdOrderBound = function (item, e) {
            var ds = e.sender.dataSource.data();
            if (ds.length == 1) {
                e.sender.value(ds[0].MC_FAB_PROD_ORD_H_ID);
                item['MC_FAB_PROD_ORD_H_ID'] = ds[0].MC_FAB_PROD_ORD_H_ID;
            }
        };

        function getSupplierList() {
           return KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
                vm.supplierListDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        vm.dtFormat = config.appDateFormat;
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.CHALAN_DT_OPEN = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.CHALAN_DT_OPENED = true;
        };

        vm.addKnitPlan = function (pMC_FAB_PROD_ORD_H_ID, pKNT_SC_PRG_ISS_ID, pTR_PARTY_ID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ExColourListModal.html',
                controller: 'JobCardDashboardYdRecvTrnsModalController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    Params: function () {
                        return {

                            pMC_FAB_PROD_ORD_H_ID: pMC_FAB_PROD_ORD_H_ID,

                            pKNT_SC_PRG_ISS_ID: pKNT_SC_PRG_ISS_ID,
                            pSCM_SUPPLIER_ID: pTR_PARTY_ID,

                            pRF_FAB_PROD_CAT_ID: null,
                            LK_COL_TYPE_ID_LST: '360',
                            DEFAULT_CAT_ID: '2',
                            RF_FAB_PROD_CAT_ID_LST: '1,2,3,4,5,6'
                        }
                    }
                }
            });
            modalInstance.result.then(function (data) {
                //if (data.KNT_SC_PRG_ISS_ID) {
                //    $scope.form['KNT_SC_PRG_ISS_ID'] = data.KNT_SC_PRG_ISS_ID;
                //    vm.printReportYd(data.KNT_SC_PRG_ISS_ID);
                //}
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });


        }



        if (Object.keys(FormData).length > 0) {
            $scope.form = angular.copy(FormData);
            $scope.form['IS_FINALIZED_V'] = FormData.IS_FINALIZED;
        } else {
            $scope.form = {
                PACK_MOU_ID : 29,
                QTY_MOU_ID : 3,
                KNT_YD_RCV_H_ID : -1,
                details: [],
                SCM_STORE_ID: 2,               
                IS_FINALIZED_V: 'N',
                IS_TEMP_CHALLAN: 'N',
                IS_TRANSFER: 'Y',
                CHALAN_DT : new Date()
            }
        }

        vm.OnRequestYarnBatch = function (p) {
            return KnittingDataService.getDataByUrl('/KnitYdProgram/SearchApprovedBatchNo?pYD_BATCH_NO=' + p).then(function (res) {
                return res;
            })
        };

        vm.onRequestChallanNo = function (p) {
            return KnittingDataService.getDataByUrl('/KnitYdProgram/SearchApprovedBatchNo?pYD_BATCH_NO=' + p + '&pOption=3005').then(function (res) {
                return res;
            })
        };


        function getMouList() {
            return KnittingDataService.getDataByFullUrl('/api/common/MOUList/N').then(function (res) {
                vm.rfMouDs = new kendo.data.DataSource({
                    data: res
                });
            });
        };


        vm.onChaneProgramRef = function (e) {
            var PrgRef = e.sender.dataItem(e.sender.item);

            console.log(PrgRef);

            if (PrgRef.KNT_YD_PRG_H_ID) {
                $scope.form['SCM_SUPPLIER_ID'] = PrgRef.SCM_SUPPLIER_ID;
                $scope.form['SUP_TRD_NAME_EN'] = PrgRef.SUP_TRD_NAME_EN;
                $scope.form['MC_FAB_PROD_ORD_H_LST'] = PrgRef.MC_FAB_PROD_ORD_H_LST;

                KnittingDataService.getDataByUrl('/KnitYdProgram/getYdYrnRecvD?KNT_YD_PRG_H_ID=' + PrgRef.KNT_YD_PRG_H_ID).then(function (res) {
                    $scope.form['details'] = res;
                });


                KnittingDataService.getDataByUrl('/KnitYdProgram/getOrderListByProgram?pMC_FAB_PROD_ORD_H_LST=' + PrgRef.MC_FAB_PROD_ORD_H_LST).then(function (res) {
                    vm.fabProdOrderDs = new kendo.data.DataSource({
                        data: res
                    });
                });
            } else {
                $scope.form['SCM_SUPPLIER_ID'] = '';
                $scope.form['SUP_TRD_NAME_EN'] = '';
                $scope.form['MC_FAB_PROD_ORD_H_LST'] = '';
                vm.fabProdOrderDs = new kendo.data.DataSource({
                    data: []
                });
            }


        };


        //$scope.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST #</h5><p> #: data.FAB_PROD_CAT_SNAME||"" #</p></span>';
        //$scope.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST # (#: data.FAB_PROD_CAT_SNAME||"" #)</h5></span>';

        //vm.Yarntemplate = '<span><span style="padding:0px;margin:0px;">#: data.ITEM_NAME_EN # #if (data.PCT_RATIO>0)  { #<span class="label label-sm label-danger" style="padding:0px;margin:0px;">Suggested</span># } # </span>';
        //vm.value_template = '<span><h5 style="padding:0px;margin:0px;"><b>#: data.YRN_LOT_NO # </b> (#: data.STK_BALANCE # #: data.MOU_CODE #)</h5></span>';

        vm.printDeliveryChallan = function (pKNT_SC_PRG_ISS_ID) {
            if (!pKNT_SC_PRG_ISS_ID) { return; }

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = {
                REPORT_CODE: 'RPT-3582',
                KNT_SC_PRG_ISS_ID: pKNT_SC_PRG_ISS_ID,
                KNT_YD_RCV_H_ID: pKNT_YD_RCV_H_ID,
                IS_YD: 'Y'
            };

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
        vm.printScProgram = function (pKNT_SC_PRG_ISS_ID) {
            if (!pKNT_SC_PRG_ISS_ID) { return; }
            var url = '/Knitting/Knit/KntScProgYd/#/KntScProgYd?pKNT_SC_PRG_ISS_ID=' + pKNT_SC_PRG_ISS_ID
            var opt = 'location=yes,height=570,width=' + ($window.outerWidth - 300) + ',scrollbars=yes,status=yes';
            $window.open(url, "_blank", opt);
        }

        $scope.KntYdProgramDs = {
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);

                    var url = "/KnitYdProgram/getYdProgramDropDownData";
                   
                    if (params.filter) {
                        url += '?pPRG_REF_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                    } else {
                        url += '?pPRG_REF_NO';
                        url += '&pKNT_YD_PRG_H_ID=' + (Object.keys(FormData).length > 0 ? FormData.KNT_YD_PRG_H_ID : '');
                    }

                    return KnittingDataService.getDataByUrl(url).then(function (res) {
                        e.success(res);
                   
                    });
                }
            },
            serverFiltering: true
        };

        vm.submitData = function (dataOri, isValid) {

            var details = [];
            var data = angular.copy(dataOri);

            //alert('x');
            console.log(dataOri);
            console.log(isValid);


            if (!data.KNT_YD_PRG_H_ID) {
                $scope.YdYrnRevForm.KNT_YD_PRG_H_ID.$setValidity('required', false);
                return;
            }

            //if (!data.SCM_STORE_ID) {
            //    $scope.YdYrnRevForm.SCM_STORE_ID.$setValidity('required', false);
            //    return;
            //}
            
            if (!isValid)
                return;


            angular.forEach(data.details, function (val, key) {
                if (val.RCV_YD_QTY > 0) {
                    details.push({
                        KNT_YD_RCV_D_ID: val.KNT_YD_RCV_D_ID,
                        YRN_COLOR_ID: val.YRN_COLOR_ID,
                        YARN_ITEM_ID: val.YARN_ITEM_ID,

                        RCV_YD_QTY: val.RCV_YD_QTY,
                        QTY_MOU_ID: 3,
                        RCV_YD_QTY_ADJ: (val.RCV_YD_QTY - val.RCV_YD_QTY_ORI),

                        YD_BATCH_NO: val.YD_BATCH_NO,
                        RCV_CONE_QTY: val.RCV_CONE_QTY,

                        RCV_PACK_QTY: val.RCV_PACK_QTY,
                        PACK_MOU_ID: val.PACK_MOU_ID,
                        RCV_GYRN_QTY: val.RCV_GYRN_QTY,
                        REMARKS: val.REMARKS
                    });
                }
            });

            

            if (details.length == 0) {
                return;
            } else {
                var d2save = {
                    KNT_YD_RCV_H_ID: data.KNT_YD_RCV_H_ID || -1,
                    KNT_YD_PRG_H_ID: data.KNT_YD_PRG_H_ID,


                    SCM_SUPPLIER_ID: data.SCM_SUPPLIER_ID,
                    SCM_STORE_ID: data.SCM_STORE_ID,
                    CL_WO_REF_NO: data.CL_WO_REF_NO,

                    CHALAN_NO: data.CHALAN_NO,
                    INVOICE_NO: data.INVOICE_NO,
                    GATE_PASS_NO: data.GATE_PASS_NO,
                    VEHICLE_NO: data.VEHICLE_NO,
                    CARRIER_NAME: data.CARRIER_NAME,
                    IS_FINALIZED: data.IS_FINALIZED_V,

                    MC_FAB_PROD_ORD_H_ID: data.MC_FAB_PROD_ORD_H_ID,
                    TR_PARTY_ID : data.TR_PARTY_ID,
                    KNT_SC_PRG_ISS_ID: data.KNT_SC_PRG_ISS_ID,
                    IS_TRANSFER: data.IS_TRANSFER || 'Y',
                    IS_PASS: data.IS_PASS,
                    IS_TEMP_CHALLAN: data.IS_TEMP_CHALLAN,
                    IS_RECV_QTY_CHANGE: _.some(data.details, function (o) { return (o.RCV_YD_QTY - o.RCV_YD_QTY_ORI) != 0; }) ? 'Y' : 'N',
                    REMARKS: data.REMARKS,
                    CHALAN_DT: $filter('date')(data.CHALAN_DT, 'dd-MMM-yyyy'),
                    XML: config.xmlStringShort(details),
                    RF_ACTN_STATUS_ID: data.RF_ACTN_STATUS_ID_INIT
                };

                console.log(d2save);

                return KnittingDataService.saveDataByUrl(d2save, '/KnitYdProgram/SaveYdYrnRecv').then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                            var CurStateParam = angular.extend(vm.params, { pKNT_YD_RCV_H_ID: parseInt(res.data.OP_KNT_YD_RCV_H_ID) });
                            $state.go('KnitYdYrnRecvTrns',CurStateParam, { reload: true, inherit: false });
                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }
        vm.onNewMode = function () {
            var params = angular.extend({}, $stateParams);
            delete params['pKNT_YD_RCV_H_ID'];
            console.log(params);
            $state.go($state.current, params, { reload: true, inherit:false });

        };
    }

})();