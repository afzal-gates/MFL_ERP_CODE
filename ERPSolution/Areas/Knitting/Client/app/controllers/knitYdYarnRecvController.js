(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitYdYrnRecvController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'cur_date_server', '$filter', 'Dialog', 'FormData',  KnitYdYrnRecvController]);
    function KnitYdYrnRecvController($q, config, KnittingDataService, $stateParams, $state, $scope, cur_date_server, $filter, Dialog, FormData) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        $scope.form = {};
        $scope.alerts = [];
        activate()
       
        vm.showSplash = false;
        vm.params = $stateParams;

        function activate() {
            var promise = [getScmStoreData(), getMouList()];
            return $q.all(promise).then(function () {
                
            });
        }

        //$state.go($state.current.data.stateTo, { ConMode: $state.current.data.ConMode, stateBack: $state.current.data.stateBack });

        vm.fabProdOrderDs = new kendo.data.DataSource({
            data: FormData.orders
        });

        vm.onFabProdOrderBound = function (item, e) {
            var ds = e.sender.dataSource.data();
            if (ds.length == 1) {
                e.sender.value(ds[0].MC_FAB_PROD_ORD_H_ID);
                item['MC_FAB_PROD_ORD_H_ID'] = ds[0].MC_FAB_PROD_ORD_H_ID;

                //KnittingDataService.getDataByUrl('/KnitYdProgram/getYdYrnRecvD?KNT_YD_PRG_H_ID=' + item.KNT_YD_PRG_H_ID + '&pKNT_YD_RCV_H_ID=-1&pMC_FAB_PROD_ORD_H_ID=' + ds[0].MC_FAB_PROD_ORD_H_ID).then(function (res) {
                //    $scope.form['details'] = res;
                //});
            }
        };

        vm.onFabProdOrderChange = function (item, e) {
            var ordRef = e.sender.dataItem(e.sender.item);
            if (ordRef.MC_FAB_PROD_ORD_H_ID) {
                KnittingDataService.getDataByUrl('/KnitYdProgram/getYdYrnRecvD?KNT_YD_PRG_H_ID=' + item.KNT_YD_PRG_H_ID + '&pKNT_YD_RCV_H_ID=-1&pMC_FAB_PROD_ORD_H_ID=' + ordRef.MC_FAB_PROD_ORD_H_ID).then(function (res) {
                    $scope.form['details'] = res;
                });
            } else {
                $scope.form['details'] = [];
            }
        };

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

        vm.onRequestChallanNo = function (p) {
            return KnittingDataService.getDataByUrl('/KnitYdProgram/SearchApprovedBatchNo?pYD_BATCH_NO=' + p + '&pOption=3005').then(function (res) {
                return res;
            })
        };



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
                CHALAN_DT: new Date(),
                IS_TEMP_CHALLAN: 'N',
            }
        }

        vm.OnRequestYarnBatch = function (p) {
            return KnittingDataService.getDataByUrl('/KnitYdProgram/SearchApprovedBatchNo?pYD_BATCH_NO=' + p).then(function (res) {
                return res;
            })
        };

        function getScmStoreData() {
            return KnittingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=2').then(function (res) {
                vm.ScmStoreDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        function getMouList() {
            return KnittingDataService.getDataByFullUrl('/api/common/MOUList/N').then(function (res) {
                vm.rfMouDs = new kendo.data.DataSource({
                    data: res
                });
            });
        };


        vm.onChaneProgramRef = function (e) {
            var PrgRef = e.sender.dataItem(e.sender.item);
            if (PrgRef.KNT_YD_PRG_H_ID) {
                $scope.form['SCM_SUPPLIER_ID'] = PrgRef.SCM_SUPPLIER_ID;
                $scope.form['SUP_TRD_NAME_EN'] = PrgRef.SUP_TRD_NAME_EN;

                KnittingDataService.getDataByUrl('/KnitYdProgram/getOrderListByProgram?pMC_FAB_PROD_ORD_H_LST=' + PrgRef.MC_FAB_PROD_ORD_H_LST).then(function (res) {
                    vm.fabProdOrderDs = new kendo.data.DataSource({
                        data: res
                    });
                });



            } else {
                $scope.form['SCM_SUPPLIER_ID'] = '';
                $scope.form['SUP_TRD_NAME_EN'] = '';
                vm.fabProdOrderDs = new kendo.data.DataSource({
                    data: []
                });
            }


        };


        //$scope.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST #</h5><p> #: data.FAB_PROD_CAT_SNAME||"" #</p></span>';
        //$scope.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST # (#: data.FAB_PROD_CAT_SNAME||"" #)</h5></span>';

        //vm.Yarntemplate = '<span><span style="padding:0px;margin:0px;">#: data.ITEM_NAME_EN # #if (data.PCT_RATIO>0)  { #<span class="label label-sm label-danger" style="padding:0px;margin:0px;">Suggested</span># } # </span>';
        //vm.value_template = '<span><h5 style="padding:0px;margin:0px;"><b>#: data.YRN_LOT_NO # </b> (#: data.STK_BALANCE # #: data.MOU_CODE #)</h5></span>';



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

        vm.gotoFabricProductionOrder = function (pMC_FAB_PROD_ORD_H_ID, pRF_FAB_PROD_CAT_ID) {
            var url = '/Knitting/Knit/FabProdKnitOrder?a=204/#/FabProdKnitOrderYD?pMC_FAB_PROD_ORD_H_ID=' + pMC_FAB_PROD_ORD_H_ID + '&pRF_FAB_PROD_CAT_ID=' + pRF_FAB_PROD_CAT_ID;
            var a = document.createElement('a');
            a.href = url;
            a.target = '_blank';
            document.body.appendChild(a);
            a.click();
        };

        

        vm.submitData = function (dataOri, isValid) {

            var details = [];
            var data = angular.copy(dataOri);
            console.log(data.details);

            if (!data.KNT_YD_PRG_H_ID) {
                $scope.YdYrnRevForm.KNT_YD_PRG_H_ID.$setValidity('required', false);
                return;
            }

            if (!data.SCM_STORE_ID) {
                $scope.YdYrnRevForm.SCM_STORE_ID.$setValidity('required', false);
                return;
            }
            
            if (!isValid)
                return;


            angular.forEach(data.details, function (val, key) {
                if (val.RCV_YD_QTY > 0) {
                    details.push({
                        KNT_YD_RCV_D_ID: val.KNT_YD_RCV_D_ID,
                        YRN_COLOR_ID: val.YRN_COLOR_ID,
                        YARN_ITEM_ID: val.YARN_ITEM_ID,

                        RCV_YD_QTY: val.RCV_YD_QTY,
                        RCV_YD_QTY_ADJ: (val.RCV_YD_QTY - val.RCV_YD_QTY_ORI),

                        QTY_MOU_ID: 3,

                        YD_BATCH_NO: val.YD_BATCH_NO,
                        RCV_CONE_QTY: val.RCV_CONE_QTY,

                        RCV_PACK_QTY: val.RCV_PACK_QTY,
                        PACK_MOU_ID: val.PACK_MOU_ID,
                        RCV_GYRN_QTY: val.RCV_GYRN_QTY,
                        REMARKS: val.REMARKS
                    });
                }
            });

            

            if (details.length == 0 && data.RF_ACTN_STATUS_ID_INIT == 20) {
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
                    REMARKS : data.REMARKS,
                    CHALAN_DT: $filter('date')(data.CHALAN_DT, 'dd-MMM-yyyy'),
                    XML: config.xmlStringShort(details),
                    MC_FAB_PROD_ORD_H_ID: data.MC_FAB_PROD_ORD_H_ID,
                    IS_TRANSFER: 'N',
                    IS_PASS: data.IS_PASS,
                    IS_TEMP_CHALLAN: data.IS_TEMP_CHALLAN,
                    RF_ACTN_STATUS_ID: data.RF_ACTN_STATUS_ID_INIT,
                    IS_RECV_QTY_CHANGE: 'N'
                };

                return KnittingDataService.saveDataByUrl(d2save, '/KnitYdProgram/SaveYdYrnRecv').then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            var CurStateParam = angular.extend(vm.params, { pKNT_YD_RCV_H_ID: parseInt(res.data.OP_KNT_YD_RCV_H_ID) });
                            $state.go('KnitYdYrnRecv', CurStateParam, { reload: true, inherit: false });                            
                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }

        //vm.printProgramRpt = function (pKNT_YD_PRG_H_ID) {
        //    var form = document.createElement("form");
        //    form.setAttribute("method", "post");
        //    form.setAttribute("action", '/api/knit/KntReport/PreviewReport');
        //    form.setAttribute("target", '_blank');

        //    var params = angular.extend({ REPORT_CODE: 'RPT-3528' }, {
        //        KNT_YD_PRG_H_ID: pKNT_YD_PRG_H_ID
        //    });

        //    for (var i in params) {
        //        if (params.hasOwnProperty(i)) {

        //            var input = document.createElement('input');
        //            input.type = 'hidden';
        //            input.name = i;
        //            input.value = params[i];
        //            form.appendChild(input);
        //        }
        //    }

        //    document.body.appendChild(form);
        //    form.submit();
        //    document.body.removeChild(form);
        //};
    }

})();