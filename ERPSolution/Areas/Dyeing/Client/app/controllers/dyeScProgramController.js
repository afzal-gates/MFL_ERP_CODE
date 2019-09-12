(function () {
    'use strict';
    angular.module('multitex.knitting').controller('DyeScProgramController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'Dialog', '$modal', DyeScProgramController]);
    function DyeScProgramController($q, config, DyeingDataService, $stateParams, $state, $scope, Dialog, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.form = {};

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getBatchPlan($stateParams.pDYE_SC_PRG_ISS_ID)];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.params = $stateParams;


        function getBatchPlan(pDYE_SC_PRG_ISS_ID) {
            if (!pDYE_SC_PRG_ISS_ID) { return; }
            DyeingDataService.getDataByUrl('/DyeBatch/GetBatchPlanDataByScProg?pDYE_SC_PRG_ISS_ID=' + pDYE_SC_PRG_ISS_ID).then(function (res) {
                vm.dyePrograms = res;
            });
        }

        vm.SchedulePlanData = {
            optionLabel: "--Select Program--",
            filter: "contains",
            autoBind: true,
            template: '<span><h4 style="padding:0px;margin:0px;"><b>#: data.PRG_ISS_NO # || #: data.SUP_TRD_NAME_EN #</b></h4><p> #= kendo.toString(kendo.parseDate(data.SC_PRG_DT),"dd/MM/yyyy HH:mm") #</p></span>',
            valueTemplate: '<span><h4 style="padding:0px;margin:0px;"><b>#: data.PRG_ISS_NO #||#: data.SUP_TRD_NAME_EN #</b><small>(#= kendo.toString(kendo.parseDate(data.SC_PRG_DT),"dd/MM/yyyy HH:mm") #)</small></h4></span>',
            dataSource: {
                transport: {
                    read: function (e) {


                        var url = "/DyeScProgram/QueryDyeScPrgIss";

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '?pPRG_ISS_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '?pPRG_ISS_NO';
                        }
                        url += '&pDYE_SC_PRG_ISS_ID=' + ($stateParams.pDYE_SC_PRG_ISS_ID||'');
                        url += "&pOption=3002"

                        return DyeingDataService.getDataByUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true,
            },
            change: function (e) {
                var itm = e.sender.dataItem(e.item);
                
                if (itm.DYE_SC_PRG_ISS_ID) {                    
                    $state.go('DyeScProgram', { pDYE_SC_PRG_ISS_ID: itm.DYE_SC_PRG_ISS_ID }, { reload: true });
                } else {                    
                    $state.go('DyeScProgram', {}, { reload: true, inherit:false });
                };
                
            },
            dataBound: function (e) {

                var ds = e.sender.dataSource.data();                
                if ($stateParams.pDYE_SC_PRG_ISS_ID) {
                    e.sender.value($stateParams.pDYE_SC_PRG_ISS_ID);
                    vm.ProgramData = _.find(ds, function (o) {
                        return o.DYE_SC_PRG_ISS_ID === parseInt($stateParams.pDYE_SC_PRG_ISS_ID);
                    });
                } 
            },
            dataTextField: "PRG_ISS_NO",
            dataValueField: "DYE_SC_PRG_ISS_ID"
        };



        vm.onPrintBooking = function () {
            var url = '/api/Dye/DyeReport/PreviewReport';
            

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.DYE_SC_PRG_ISS_ID = vm.params.pDYE_SC_PRG_ISS_ID;
            vm.form.REPORT_CODE = 'RPT-4029';
            var params = angular.copy(vm.form);

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
        }

        vm.onNewPrograAdd = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'DyeScProgramModal.html',
                controller: function ($scope, config, DyeingDataService, $modalInstance, supplierListDs) {
                    $scope.form = {};
                    $scope.today = new Date();
                    $scope.dtFormat = config.appDateFormat;

                    $scope.dateOptions = {
                        formatYear: 'yy',
                        startingDay: 6
                    };

                    $scope.form['SC_PRG_DT'] = new Date();
                    
                    $scope.form['LK_SC_PRG_STATUS_ID'] = 537;
                    $scope.form['DYE_SC_PRG_ISS_ID'] = -1;

                    $scope.supplierListDs = supplierListDs;


                    $scope.RevisionDate_LNopen = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.RevisionDate_LNopened = true;
                    }

                    $scope.EXP_DELV_DTopen = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.EXP_DELV_DTopened = true;
                    }



                    $scope.save = function (data) {
                        return DyeingDataService.saveDataByUrl(data, '/DyeScProgram/SaveDyeScPrgIss').then(function (res) {
                            if (res.success === false) {
                            }
                            else {
                                res['data'] = angular.fromJson(res.json);

                                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                    $modalInstance.close({
                                        DYE_SC_PRG_ISS_ID: parseInt(res.data.OP_DYE_SC_PRG_ISS_ID || 0)
                                    });
                                }
                                config.appToastMsg(res.data.PMSG);
                            }
                        });
                    };

                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }
                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    supplierListDs: function () {
                        return DyeingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=579').then(function (res) {
                            return new kendo.data.DataSource({
                                data: res
                            });
                        });
                    }
                }
            });

            modalInstance.result.then(function (dta) {
                $state.go('DyeScProgram', { pDYE_SC_PRG_ISS_ID: dta.DYE_SC_PRG_ISS_ID }, { reload: true });

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };

        vm.openprogramModal = function (data) {
            data.RF_FAB_PROD_CAT_ID = 2;
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Dyeing/Dye/_DyeBatchProgramModal?ViewName=_DyeBatchProgramModalScP',
                controller: 'DyeBatchProgramModalScPController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    formData: function () {
                        return data;
                    }
                }
            });

            modalInstance.result.then(function (dta) {
                $state.reload();
            }, function () {
                $state.reload();
            });
        };

        vm.onRequirementClick = function (itm) {
            var url;

            if (!itm.hasOwnProperty('requirements')) {
                itm['requirements'] = [];
            }

            if (itm.IS_OPEN_REQ && itm.requirements.length == 0) {
                url = '/DyeBatch/GetDataFabReqCalScP?pMC_FAB_PROD_ORD_H_LST=' + itm.MC_FAB_PROD_ORD_H_ID + (itm.LNK_ORD_H_ID_LST ? (',' + itm.LNK_ORD_H_ID_LST) : '');
                url += '&pDYE_BATCH_PLAN_ID=' + itm.DYE_BATCH_PLAN_ID;
                url += '&pDYE_SC_PRG_ISS_ID=' + $stateParams.pDYE_SC_PRG_ISS_ID;
                url += '&pFAB_COLOR_ID=' + itm.FAB_COLOR_ID;
                url += '&pRF_FAB_TYPE_ID=' + itm.RF_FAB_TYPE_ID;
                url += '&pRQD_PRD_QTY=' + itm.LEFT_TO_REQ;
                url += "&pINV_ITEM_CAT_ID=" + itm.INV_ITEM_CAT_ID;

                return DyeingDataService.getDataByUrl(url).then(function (res) {
                    itm['requirements'] = res;
                    vm.disabledEdit = false;
                });
            }
        };


        vm.remove = function (data) {
            Dialog.confirm('Removing Program ID# <b>' + data.DYE_BATCH_PLAN_ID + '</b><br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     return DyeingDataService.saveDataByUrl({
                         DYE_BATCH_PLAN_ID: data.DYE_BATCH_PLAN_ID
                     }, '/DyeBatch/Delete').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 getBatchPlan($stateParams.pDYE_SC_PRG_ISS_ID);
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });
                 });
        }

        vm.getDfProcessTypeData = function (itm) {
            if (!itm.hasOwnProperty('df_data')) {
                itm['df_data'] = [];
            }

            if (itm.IS_OPEN_DF_PRC && itm.df_data.length == 0) {
                return DyeingDataService.getDataByUrl('/DyeBatchPlan/getDFProcessData?pDYE_BATCH_PLAN_ID=' + itm.DYE_BATCH_PLAN_ID).then(function (res) {
                    itm['df_data'] = res;
                });
            };
        };

        vm.submitData = function (dataOri, isValid) {

            //if (!isValid)
            //    return;
            var DfProcessData = [];
            var data2save = [];

            angular.forEach(dataOri, function (val1, key1) {


                if (val1.IS_SELECTED_BT) {


                    if (val1.df_data && val1.df_data.length > 0) {
                        var DfDataD = [];

                        angular.forEach(val1.df_data, function (val, key) {
                            angular.forEach(val.items, function (v, k) {
                                if (v.IS_SELECTED == "Y") {
                                    DfDataD.push({
                                        DYE_BT_RQD_PROC_ID: v.DYE_BT_RQD_PROC_ID,
                                        DF_PROC_TYPE_ID: v.DF_PROC_TYPE_ID
                                    });
                                }
                            })
                        });

                        DfProcessData.push({
                            DYE_BATCH_PLAN_ID: val1.DYE_BATCH_PLAN_ID,
                            DF_XML_D: config.xmlStringShortNoTag(DfDataD)

                        });


                    }

                    var parent = _.filter(val1.requirements, function (o) {
                        return o.MC_FAB_PROD_ORD_H_ID == val1.MC_FAB_PROD_ORD_H_ID;
                    });
                    angular.forEach(parent, function (oo) {

                        data2save.push({
                            DYE_BATCH_PLAN_ID: val1.DYE_BATCH_PLAN_ID,
                            ACT_LOAD_TIME: val1.LOAD_TIME,
                            ACT_UN_LOAD_TIME: val1.UN_LOAD_TIME,
                            FAB_COLOR_ID: val1.FAB_COLOR_ID,
                            LK_DYE_MTHD_ID: val1.LK_DYE_MTHD_ID,
                            RF_FAB_TYPE_ID: (oo.RF_FAB_TYPE_ID || ''),

                            RF_FIB_COMP_ID: (oo.RF_FIB_COMP_ID || ''),
                            MC_STYLE_H_ID: oo.MC_STYLE_H_ID,

                            INV_ITEM_CAT_ID: oo.INV_ITEM_CAT_ID,

                            ACT_BATCH_QTY: oo.INV_ITEM_CAT_ID == 34 ?
                                 _.sumBy(oo.non_col_cu_avail, function (f) { return (f.ASSIGN_QTY || 0) })

                                 :
                                 (oo.RQD_QTY_KG || 0),

                            DYE_BATCH_NO: _.filter(parent, function (x) { return x.DYE_BATCH_NO.length > 0 })[0].DYE_BATCH_NO,
                            DYE_LOT_NO: _.filter(parent, function (x) { return x.DYE_LOT_NO.length > 0 })[0].DYE_LOT_NO,

                            MC_FAB_PROD_ORD_H_ID: oo.MC_FAB_PROD_ORD_H_ID,
                            DYE_BT_CARD_H_ID: oo.DYE_BT_CARD_H_ID,
                            LK_FBR_GRP_ID: (oo.LK_FBR_GRP_ID || ''),
                            DYE_BT_CARD_GRP_ID: (oo.DYE_BT_CARD_GRP_ID || ''),


                            DYE_BT_CARD_D_TRMS_ID: (oo.DYE_BT_CARD_D_TRMS_ID || ''),
                            RQD_QTY_YDS: (oo.RQD_QTY_YDS || 0),
                            RQD_QTY_KG: (oo.RQD_QTY_KG || 0),

                            NON_COL_CUF: config.xmlStringShortNoTag(oo.non_col_cu_avail.filter(function (f) { return (f.ASSIGN_QTY || 0) > 0 }).map(function (o) {
                                return {
                                    DYE_BT_CARD_D_FAB_ID: o.DYE_BT_CARD_D_FAB_ID,

                                    INV_ORD_GFAB_STK_ID: o.INV_ORD_GFAB_STK_ID,

                                    KNT_STYL_FAB_ITEM_ID: o.KNT_STYL_FAB_ITEM_ID,
                                    RQD_QTY: o.RCV_ROLL_WT,
                                    QTY_MOU_ID: 3,
                                    ASSIGN_QTY: o.ASSIGN_QTY,
                                    KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID
                                }
                            })),
                            COL_CUF: config.xmlStringShortNoTag(oo.col_cu_avail.filter(function (f) { return (f.ASSIGN_QTY || 0) > 0 }).map(function (o) {
                                return {
                                    KNT_CLCF_STYL_ITEM_ID: o.KNT_CLCF_STYL_ITEM_ID,
                                    DYE_BT_CARD_D_CLCF_ID: o.DYE_BT_CARD_D_CLCF_ID,
                                    PRD_QTY: o.PRD_QTY,
                                    QTY_MOU_ID: 1,
                                    ASSIGN_QTY: o.ASSIGN_QTY
                                }
                            })),
                        });
                    });
                }

            });
            if (data2save.length == 0 && _.sumBy(data2save,function (o){ return (o.ACT_BATCH_QTY||0) })) {
                return;
            } else {
                Dialog.confirm('Saving Batch data...<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     return DyeingDataService.saveDataByUrl({
                         XML: config.xmlStringShort(data2save),
                         XML_DF: config.xmlStringShort(DfProcessData)

                     }, '/DyeBatch/Save').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);

                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 getBatchPlan($stateParams.pDYE_SC_PRG_ISS_ID)
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });
                 });
            }




        }
        vm.createRequisition = function (dataOri) {
            var data2save = [];

            angular.forEach(dataOri, function (val1, key1) {
                    if (val1.IS_SELECTED_BT) {

                        if (data2save.indexOf({
                            DYE_BATCH_PLAN_ID: val1.DYE_BATCH_PLAN_ID
                        }) < 0) {
                            data2save.push({
                                DYE_BATCH_PLAN_ID: val1.DYE_BATCH_PLAN_ID,
                                INV_ITEM_CAT_ID: (val1.INV_ITEM_CAT_ID || 34)
                            });
                        }
                    }
                });

            if (data2save.length == 0) {
                return;
            } else {
                Dialog.confirm('Creating Grey Fabric Req for Batch?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     return DyeingDataService.saveDataByUrl({
                         XML: config.xmlStringShort(data2save),
                         RF_REQ_TYPE_ID: 20,// 16 S/C Program
                         PREFIX: 'RO-',
                         RF_ACTN_TYPE_ID: 18 // 6 Sample,5 Bulk
                     }, '/DyeBatch/CreateRequisition').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);

                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 //proceed with Sucess
                                 getBatchPlan($stateParams.pDYE_SC_PRG_ISS_ID);
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });
                 });
            }
        }

        vm.cancel = function () {
            getBatchPlan($stateParams.pDYE_SC_PRG_ISS_ID);
        };


    }

})();