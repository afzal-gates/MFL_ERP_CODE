//=============== Start for Sub-contract Program Header =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntSubContProgHController', ['$q', 'config', 'formData', 'Dialog', 'KnittingDataService', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', KntSubContProgHController]);
    function KntSubContProgHController($q, config, formData, Dialog, KnittingDataService, $stateParams, $state, $scope, $filter, commonDataService) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();// $filter('date')(new Date(), config.appDateFormat);
        //vm.kntScPrgList = [];

        var key = 'KNT_SC_PRG_RCV_ID';
        vm.form = { KNT_SC_PRG_RCV_ID: -1, SCM_SUPPLIER_ID: -1 };
        $scope.form = { KNT_SC_PRG_RCV_ID: -1, SCM_SUPPLIER_ID: -1 };

        $scope.challanList = [];

        vm.yarnReceivState = ($state.current.name === 'KntSubContProgH.YarnReceive') ? true : false;
        vm.fabColorKPState = ($state.current.name === 'KntSubContProgH.FabColorKP') ? true : false;
        

        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [GetSupplierList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;

                vm.form = formData[key] ? formData : { KNT_SC_PRG_RCV_ID: -1, SC_PRG_DT: vm.today };
                $scope.form = vm.form;
            });
        }
        
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        $scope.SC_PRG_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.SC_PRG_DT_LNopened = true;
        }

        $scope.$watch('vm.form', function (newVal, oldVal) {

            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    $scope.form = vm.form;
                    $scope.form.SC_PRG_DT = $filter('date')(vm.form.SC_PRG_DT, vm.dtFormat);
                }
            }
        }, true);

        vm.goState = function () {
            if (vm.form.KNT_SC_PRG_RCV_ID < 1) {
                vm.fabColorKPState = true;
                vm.yarnReceivState = false;

                $state.go('KntSubContProgH.FabColorKP');
            }
            else {
                vm.fabColorKPState = false;
                vm.yarnReceivState = true;

                $state.go('KntSubContProgH.YarnReceive');
            }
        }

        function GetSupplierList() {
            vm.supplierOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SUP_TRD_NAME_EN",
                dataValueField: "SCM_SUPPLIER_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    $stateParams.pSCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                },
                dataBound: function (e) {
                    if ($stateParams.pSCM_SUPPLIER_ID != null && $stateParams.pSCM_SUPPLIER_ID > 0) {
                        vm.form.SCM_SUPPLIER_ID = $stateParams.pSCM_SUPPLIER_ID;
                    }
                }
            };

            return vm.supplierDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });            
        };

        $scope.mainYrnRcvGridOption = {
            height: 350,
            sortable: true,
            //pageable: true,
            selectable: "row",
            navigatable: true,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains",
                        startswith: "Starts With",
                        eq: "Is Equal To"
                    }
                }
            },
            //dataBound: function () {
            //    this.expandRow(this.tbody.find("tr.k-master-row").first());
            //},
            columns: [
                { field: "KNT_SC_YRN_RCV_H_ID", title: "KNT_SC_YRN_RCV_H_ID", type: "number" },
                { field: "CHALAN_NO", title: "Challan#", type: "string", width: "240px" },
                { field: "CHALAN_DT", title: "Date", type: "date", format: "{0:d-MMM-yyyy}", width: "140px" },
                { field: "STORE_NAME_EN", title: "Store", width: "260px" },
                { field: "REMARKS", title: "Remarks" },
                {
                    title: "Action",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editYrnRcvHdrRow(dataItem)'><i class='fa fa-edit'></i></button>";
                    },
                    width: "100px"
                }
            ]
        };                

        $scope.mainYrnRcvGridDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    //e.success([]);
                    return KnittingDataService.getDataByFullUrl('/api/knit/KntScRcv/GetScYrnRcvHdrListByPrgID?pKNT_SC_PRG_RCV_ID=' + ($stateParams.pKNT_SC_PRG_RCV_ID || 0)).then(function (res) {

                        if (res.length > 0) {
                            e.success(res);
                        }
                        else {                            
                            e.success([]);
                        }

                    }, function (err) {
                        console.log(err);
                    });
                }
            },            
            schema: {
                model: {
                    id: "KNT_SC_YRN_RCV_H_ID",
                    fields: {
                        CHALAN_DT: { type: "date", editable: false }                                          
                    }
                }
            }
        });

        $scope.detailYrnRcvGridOption = function (dtlDataItem) {
            //console.log(dtlDataItem);
            dtlDataItem.QTY_MOU_ID = 3;
            
            return {
                dataSource: {
                    transport: {
                        read: function (e) {
                            //e.success([]);
                            return KnittingDataService.getDataByFullUrl('/api/knit/KntScRcv/GetScYrnRcvDtlListByHrdID?pKNT_SC_YRN_RCV_H_ID=' + (dtlDataItem.KNT_SC_YRN_RCV_H_ID || 0)).then(function (res) {

                                if (res.length > 0) {
                                    
                                    e.success(res.map(function (o) {
                                        o['PARENT_DS'] = dtlDataItem;
                                        return o;
                                    }));
                                }
                                else {
                                    e.success([]);
                                }

                            }, function (err) {
                                console.log(err);
                            });
                        },
                        parameterMap: function (options, operation) {
                            if (operation !== "read" && options.models) {
                                return { models: kendo.stringify(options.models) };
                            }
                        }
                    },
                    serverPaging: true,
                    serverSorting: true,
                    serverFiltering: true,
                    //pageSize: 5,
                    //filter: { field: "KNT_SC_YRN_RCV_H_ID", operator: "eq", value: dtlDataItem.KNT_SC_YRN_RCV_H_ID }
                },
                scrollable: false,
                sortable: true,
                //pageable: true,
                columns: [
                    { field: "ITEM_NAME_EN", title: "Yarn Item", width: "30%" },
                    { field: "YRN_COLR_GRP_NAME", title: "Color Group", width: "10%" },
                    { field: "BRAND_NAME_EN", title: "Brand", width: "20%" },
                    { field: "YRN_LOT_NO", title: "Lot#", width: "5%" },                    
                    { field: "CTN_RCV_QTY", title: "Pac.Qty", width: "5%" },
                    { field: "PACK_MOU_CODE", title: "Pac.Unit", width: "8%" },
                    { field: "QTY_PER_CTN", title: "Qty/Pac", width: "5%" },
                    { field: "RCV_QTY", title: "Rcv.Qty", width: "5%" },
                    { field: "QTY_MOU_CODE", title: "Rcv.Unit", width: "7%" },
                    {
                        title: "Action",
                        template: function () {
                            return "<button type='button' class='btn btn-xs blue' ng-click='vm.editYrnRcvDtlRow(dataItem.PARENT_DS,dataItem)'><i class='fa fa-edit'></i></button>&nbsp;<button ng-click='vm.removeItem(dataItem)' class='btn btn-xs red'><i class='fa fa-remove'></i></button>";
                        },
                        width: "5%"
                    }                    
                ]
            };
        };


        $scope.fabOrderGridOption = {
            height: 200,
            sortable: true,
            selectable: "row",
            navigatable: true,
            columns: [
                //{ field: "SC_BUYER_NAME", title: "Buyer", type: "string", width: "10%" },
                { field: "SC_WO_REF_NO", title: "Order Ref#", type: "string", width: "15%" },
                { field: "COLOR_NAME_EN", title: "Color", width: "10%" },
                { field: "FAB_TYPE_SNAME", title: "Fab.Type", width: "10%" },
                { field: "FIB_COMP_NAME", title: "Fib.Composition", width: "15%" },
                { field: "FIN_DIA_N_DIA_TYPE", title: "Fin.Dia", width: "10%" },
                { field: "FIN_GSM", title: "F.Gsm", width: "5%" },
                { field: "NET_FFAB_QTY_NAME", title: "Qty", width: "10%" },                
                { field: "SC_START_DT", title: "Start Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                { field: "SC_END_DT", title: "End Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                {
                    title: "Action",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editRow(dataItem)'><i class='fa fa-edit'></i></button>&nbsp;<button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ng-disabled='dataItem.MC_FAB_PROD_ORD_D_ID>0?true:false' ><i class='fa fa-remove'></i></button>";
                    },
                    width: "5%"
                }
            ]
        };

        $scope.fabOrderGridDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    //e.success([]);
                    return KnittingDataService.getDataByFullUrl('/api/knit/KntScRcv/GetFabOrdListByKntScPrgID?pKNT_SC_PRG_RCV_ID=' + ($stateParams.pKNT_SC_PRG_RCV_ID || 0)).then(function (res) {

                        if (res.length > 0) {
                            angular.forEach(res, function (val, key) {
                                val['SC_START_DT'] = $filter('date')(val['SC_START_DT'], vm.dtFormat);
                                val['SC_END_DT'] = $filter('date')(val['SC_END_DT'], vm.dtFormat);
                            });

                            e.success(res);
                        }
                        else {
                            e.success([]);
                        }

                    }, function (err) {
                        console.log(err);
                    });
                }
            },
            schema: {
                model: {
                    id: "MC_FAB_PROD_ORD_D_ID",
                    fields: {
                        //SC_START_DT: { type: "date", editable: false },
                        //SC_END_DT: { type: "date", editable: false }
                    }
                }
            }
        });
        

        vm.isProgAuto = function () {
            vm.form.PRG_RCV_NO = "";
        }

        vm.backToList = function () {
            $state.go('KntSubContProgList', { pSCM_SUPPLIER_ID: vm.form.SCM_SUPPLIER_ID });
        }
        
        vm.batchSave = function (pIS_FINALIZE) {
           
            var submitData = angular.copy(vm.form);
            submitData['IS_FINALIZE'] = pIS_FINALIZE;
            submitData['KNT_SC_YRN_RCV_H_XML'] = "";
            submitData['KNT_SC_YRN_RCV_D_XML'] = "";
            submitData['FAB_PROD_D_XML'] = "";
            submitData['FAB_PROD_YRN_XML'] = "";
            
            var fabOrdGridData = $scope.fabOrderGridDataSource.data();
            var mainGridData = $scope.challanList;// $scope.mainYrnRcvGridDataSource.data();
                        
            var yrnRcvHdrIndex = 0;
            var yrnRcvDtlIndex = 0;
            var yrnRcvHdrData = [];
            var yrnRcvDtlData = [];

            var fabDtlIndex = 0;
            //var fabDtlYrnIndex = 0;
            var fabDtlData = [];
            var fabDtlYrnData = [];

            
            angular.forEach(fabOrdGridData, function (val, key) {
                fabDtlIndex = fabDtlIndex + 1;
                var fabData = {
                    FAB_DTL_INDEX: fabDtlIndex, MC_FAB_PROD_ORD_D_ID: val['MC_FAB_PROD_ORD_D_ID'], MC_FAB_PROD_ORD_H_ID: val['MC_FAB_PROD_ORD_H_ID'], 
                    DIA_MOU_ID: val['DIA_MOU_ID'], FIN_DIA: val['FIN_DIA'], LK_DIA_TYPE_ID: val['LK_DIA_TYPE_ID'], FAB_COLOR_ID: val['FAB_COLOR_ID'],
                    RF_FIB_COMP_ID: val['RF_FIB_COMP_ID'], FIN_GSM: val['FIN_GSM'], KNT_MC_DIA_ID: val['KNT_MC_DIA_ID'], LK_MC_GG_ID: val['LK_MC_GG_ID'],
                    NET_FFAB_QTY: val['NET_FFAB_QTY'], PROD_RATE: val['PROD_RATE'], QTY_MOU_ID: val['QTY_MOU_ID'], RF_FAB_TYPE_ID: val['RF_FAB_TYPE_ID'],
                    SCM_SC_WO_REF_ID: val['SCM_SC_WO_REF_ID'], SC_START_DT: val['SC_START_DT'], SC_END_DT: val['SC_END_DT'], SC_WO_REF_NO: val['SC_WO_REF_NO'],
                    SC_PRG_SN: val['SC_PRG_SN'], IS_SC_PRG_SN_AUTO: val['IS_SC_PRG_SN_AUTO'], LK_YD_TYPE_ID: val['LK_YD_TYPE_ID'], LK_FEDER_TYPE_ID: val['LK_FEDER_TYPE_ID']
                };

                fabDtlData.push(fabData);
                //var dtlGridData = val['yarnList']; //val['ChildGrid'].dataSource.data();
                //console.log(dtlGridData);

                //fabDtlYrnIndex = 0;
                angular.forEach(val['fabProdYrn'], function (val1, key1) {
                    //fabDtlYrnIndex = fabDtlYrnIndex + 1;
                    val1['FAB_DTL_INDEX'] = fabDtlIndex;
                    fabDtlYrnData.push(val1);
                });
            });
                        
            angular.forEach(mainGridData, function (val, key) {
                yrnRcvHdrIndex = yrnRcvHdrIndex + 1;
                var hdrData = {
                    YRN_RCV_INDEX: yrnRcvHdrIndex, KNT_SC_YRN_RCV_H_ID: val['KNT_SC_YRN_RCV_H_ID'], KNT_SC_PRG_RCV_ID: val['KNT_SC_PRG_RCV_ID'],
                    CHALAN_DT: val['CHALAN_DT'], CHALAN_NO: val['CHALAN_NO'], SCM_STORE_ID: val['SCM_STORE_ID'], REMARKS: val['REMARKS']
                };

                yrnRcvHdrData.push(hdrData);
                var dtlGridData = val['yarnList']; //val['ChildGrid'].dataSource.data();
                console.log(dtlGridData);

                yrnRcvDtlIndex = 0;
                angular.forEach(dtlGridData, function (val1, key1) {
                    yrnRcvDtlIndex = yrnRcvDtlIndex + 1;
                    val1['YRN_RCV_INDEX'] = yrnRcvHdrIndex;
                    yrnRcvDtlData.push(val1);
                });
            });
            
            //console.log(yrnRcvHdrData);
            //console.log(yrnRcvDtlData);

            


            submitData.KNT_SC_YRN_RCV_H_XML = KnittingDataService.xmlStringShort(yrnRcvHdrData.map(function (ob) {
                return ob;
            }));

            submitData.KNT_SC_YRN_RCV_D_XML = KnittingDataService.xmlStringShort(yrnRcvDtlData.map(function (ob) {
                return ob;
            }));
            
            submitData.FAB_PROD_D_XML = KnittingDataService.xmlStringShort(fabDtlData.map(function (ob) {
                return ob;
            }));

            submitData.FAB_PROD_YRN_XML = KnittingDataService.xmlStringShort(fabDtlYrnData.map(function (ob) {
                ob['BRAND_NAME_EN'] = '';

                return ob;
            }));

            console.log(submitData);
            console.log(fabOrdGridData);
            //return;

            var vMsg = 'Do you want save?';
            if (pIS_FINALIZE == 'Y') {
                vMsg = 'After finalize, the program is lock.</br></br> <b>Do you want to save and finalize?</b>';
            }

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/KntScRcv/BatchSave').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                //$scope.fabOrderGridDataSource.read();
                                //vm.getChallanList();
                                if (res.data.PKNT_SC_PRG_RCV_ID_RTN != 0) {
                                    $stateParams.pKNT_SC_PRG_RCV_ID = res.data.PKNT_SC_PRG_RCV_ID_RTN;
                                    vm.form.KNT_SC_PRG_RCV_ID = res.data.PKNT_SC_PRG_RCV_ID_RTN;
                                    vm.form.PRG_RCV_NO = res.data.PPRG_RCV_NO_RTN;
                                    vm.form.LK_SC_PRG_STATUS_ID = res.data.PLK_SC_PRG_STATUS_ID_RTN;

                                    if (vm.yarnReceivState == true) {
                                        $state.go('KntSubContProgH.YarnReceive', { pKNT_SC_PRG_RCV_ID: res.data.PKNT_SC_PRG_RCV_ID_RTN }, { reload: 'KntSubContProgH.YarnReceive' });
                                    }
                                    else {
                                        $state.go('KntSubContProgH.FabColorKP', { pKNT_SC_PRG_RCV_ID: res.data.PKNT_SC_PRG_RCV_ID_RTN }, { notify: false });
                                        
                                        $scope.fabOrderGridDataSource.read();
                                    }

                                }

                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
           
        };

        vm.cancel = function () {
            $state.go('KntSubContProgH.FabColorKP', { pKNT_SC_PRG_RCV_ID: 0, pSCM_SUPPLIER_ID: $stateParams.pSCM_SUPPLIER_ID });
        };

        vm.printLedger = function () {
            var url = '/api/Knit/KntReport/PreviewReport';
            
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');
           
            vm.form.REPORT_CODE = 'RPT-3514';
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
        };

        $scope.addData = function (dataList) {
            $scope.challanList = [];
            angular.forEach(dataList, function (val,key) {
                $scope.challanList.push(val);
            });           
        }

        
    }


})();
//=============== End for Sub-contract Program Header =================





//=============== Start for Yarn Receive =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntSubContProgYrnRcvController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', '$filter', 'Dialog', 'commonDataService', 'formData', KntSubContProgYrnRcvController]);
    function KntSubContProgYrnRcvController($q, config, KnittingDataService, $stateParams, $state, $scope, $filter, Dialog, commonDataService, formData) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.isYrnAddToGrid = 'Y';

        if (formData.length > 0) {
            $scope.$parent.challanList = [];
            angular.forEach(formData, function (val, key) {
                val['yarnList'] = [];
                val['CHALAN_DT'] = $filter('date')(val['CHALAN_DT'], config.appDateFormat);
                $scope.$parent.challanList.push(val);
            });
        }

        vm.yrnRcvForm = { YRN_RCV_INDEX: 0, KNT_SC_YRN_RCV_H_ID: -1, KNT_SC_PRG_RCV_ID: 0, CHALAN_NO: '', CHALAN_DT: vm.today, REMARKS: '' };
        $scope.dataItem = { QTY_MOU_ID: 3, QTY_MOU_CODE: 'Kg' };
        //vm.yrnDtl = { QTY_MOU_CODE: 'Kg' };
        
        
        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getPrgOrdList(), getStoreList(), getYarnItemList(), /*getYarnColorGroupList(), getCatgBrandList(),*/ GetMOUList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        };

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.CHALAN_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.CHALAN_DT_LNopened = true;
        };

        //$scope.emit = function () {
        //    // Controller 1 $scope, and all parent $scopes (including $rootScope) 
        //    // will see this event
        //    $scope.$emit('eventX', { data: {test:'ok'}});
        //};
        
        function getPrgOrdList() {
            vm.scPrgOrdListOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SC_WO_REF_NO",
                dataValueField: "SCM_SC_WO_REF_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    $scope.dataItem.SC_WO_REF_NO = item.SC_WO_REF_NO;

                    return vm.yarnItemDataSource = new kendo.data.DataSource({
                        transport: {
                            read: function (e) {
                                KnittingDataService.getDataByFullUrl('/api/knit/KntScRcv/GetYrnByScOrdRefID?&pSCM_SC_WO_REF_ID=' + item.SCM_SC_WO_REF_ID).then(function (res) {
                                    e.success(res);
                                });
                            }
                        }
                    });
                }
            };

            return vm.scPrgOrdListDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                     
                        var url = '/api/knit/KntScRcv/GetScOrdRefByPrgID?&pKNT_SC_PRG_RCV_ID=' + (($stateParams.pKNT_SC_PRG_RCV_ID == null) ? 0 : $stateParams.pKNT_SC_PRG_RCV_ID);
                        
                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {                            
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getYarnItemList() {
            vm.yarnItemOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,                
                dataTextField: "YR_SPEC_DESC",
                dataValueField: "YARN_ITEM_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);                    
                    $scope.dataItem.YR_SPEC_DESC = item.YR_SPEC_DESC;
                    $scope.dataItem.KNT_YRN_LOT_ID = item.KNT_YRN_LOT_ID;
                    $scope.dataItem.LK_YRN_COLR_GRP_ID = item.LK_YRN_COLR_GRP_ID;
                    $scope.dataItem.RF_BRAND_ID = item.RF_BRAND_ID;
                }
            };            
        };

        function getYarnColorGroupList() {
            vm.colorGrpOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    $scope.dataItem.YRN_COLR_GRP_NAME = item.LK_DATA_NAME_EN;
                }
            };

            return vm.yarnColorGroupDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.LookupListData(93).then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getCatgBrandList() {
            vm.catBrandOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "BRAND_NAME_EN",
                dataValueField: "RF_BRAND_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    $scope.dataItem.BRAND_NAME_EN = item.BRAND_NAME_EN;
                }
            };

            return vm.categoryBrandDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                            var list = _.filter(res, { 'INV_ITEM_CAT_ID': 2 });
                            e.success(list);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function GetMOUList() {
            vm.packMouOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    $scope.dataItem.PACK_MOU_CODE = item.MOU_CODE;
                }
            };

            vm.qtyMouOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    $scope.dataItem.QTY_MOU_CODE = item.MOU_CODE;
                }
            };

            return KnittingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                //e.success(res);
                vm.packMouDataSource = new kendo.data.DataSource({
                    data: res
                });

                vm.qtyMouDataSource = new kendo.data.DataSource({
                    data: res
                });

            }, function (err) {
                console.log(err);
            });

            //return vm.mouDataSource = new kendo.data.DataSource({
            //    transport: {
            //        read: function (e) {
            //            KnittingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
            //                e.success(res);

            //            }, function (err) {
            //                console.log(err);
            //            });
            //        }
            //    }
            //});
        };

        function getStoreList() {

            return vm.storeList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=2').then(function (res) {
                                //KnittingDataService.LookupListData(92).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.yrnRcvForm.STORE_NAME_EN = item.STORE_NAME_EN;
                }
            };
        };

        $scope.$watch('$parent.challanList', function (newVal, oldVal) {
            if (!_.isEqual(newVal, oldVal)) {
                $scope.addData($scope.$parent.challanList);
            }
        }, true);

        vm.addChallan = function (data) {
            //$scope.$parent.mainYrnRcvGridDataSource.insert(data);

            var challanData = angular.copy(data);
            challanData['CHALAN_DT'] = moment(challanData.CHALAN_DT).format("DD-MMM-YYYY");
            challanData['yarnList'] = [];
            $scope.$parent.challanList.push(challanData);


            vm.yrnRcvForm.KNT_SC_YRN_RCV_H_ID = -1;
            vm.yrnRcvForm.CHALAN_NO = '';
            vm.yrnRcvForm.REMARKS = '';

            vm.isAddToGrid = 'Y';
        };

        vm.editChallan = function (index, dataList) {
            //console.log(index);
            //console.log(dataList);
            
            vm.yrnRcvForm = angular.copy(dataList[index]);
            vm.yrnRcvForm['yrnRcvHdrIndex'] = index;
            vm.isAddToGrid = 'N';
        };

        vm.updateChallan = function (data) {
            var yrnRcvFormCopy = angular.copy(data);
            yrnRcvFormCopy['CHALAN_DT'] = $filter('date')(yrnRcvFormCopy['CHALAN_DT'], config.appDateFormat);

            console.log(yrnRcvFormCopy);
            $scope.$parent.challanList[data.yrnRcvHdrIndex] = yrnRcvFormCopy;

            vm.yrnRcvForm.KNT_SC_YRN_RCV_H_ID = -1;
            vm.yrnRcvForm.CHALAN_NO = '';
            vm.yrnRcvForm.REMARKS = '';

            vm.isAddToGrid = 'Y';
        };

        vm.removeChallan = function (index, removeFrom) {
            removeFrom.splice(index, 1);
        };

        vm.makeActive = function (items, $index) {

            angular.forEach(items, function (val, key) {
                if ($index != key) {
                    val['IS_ACTIVE'] = false;
                }

            });
            items[$index]['IS_ACTIVE'] = !items[$index]['IS_ACTIVE'];


            console.log(items[$index]);
            if (items[$index]['yarnList'].length < 1) {
                return KnittingDataService.getDataByFullUrl('/api/knit/KntScRcv/GetScYrnRcvDtlListByHrdID?pKNT_SC_YRN_RCV_H_ID=' + items[$index]['KNT_SC_YRN_RCV_H_ID']).then(function (res) {
                    angular.forEach(res, function (val, key) {
                        items[$index]['yarnList'].push(val);
                    });
                }, function (err) {
                    console.log(err);
                });
            };
        };

        //vm.getYarnDtl = function () {

        //};

        vm.packageType = function (LK_LOC_SRC_TYPE_ID) {

            if (LK_LOC_SRC_TYPE_ID == 452)
                vm.formItem.CTN_BOSTA = "BOSTA";
            else if (LK_LOC_SRC_TYPE_ID == 453)
                vm.formItem.CTN_BOSTA = "CARTOON";
        };

        vm.TotalReceiveQty = function (data) {
            if (data.CTN_RCV_QTY > 0 && data.QTY_PER_CTN > 0)
                data.RCV_QTY = data.CTN_RCV_QTY * data.QTY_PER_CTN;
            else
                data.RCV_QTY = 0;
        };

        vm.addYarn = function (data, itmChln) {
            
            var dataCopy = angular.copy(data);

            var item = {
                KNT_SC_YRN_RCV_D_ID: -1,
                KNT_SC_YRN_RCV_H_ID: 0,
                SCM_SC_WO_REF_ID: dataCopy.SCM_SC_WO_REF_ID,
                SC_WO_REF_NO: dataCopy.SC_WO_REF_NO,
                YARN_ITEM_ID: dataCopy.YARN_ITEM_ID,
                KNT_YRN_LOT_ID: dataCopy.KNT_YRN_LOT_ID,
                YR_SPEC_DESC: dataCopy.YR_SPEC_DESC,
                LK_YRN_COLR_GRP_ID: dataCopy.LK_YRN_COLR_GRP_ID,
                YRN_COLR_GRP_NAME: dataCopy.YRN_COLR_GRP_NAME,
                RF_BRAND_ID: dataCopy.RF_BRAND_ID,
                BRAND_NAME_EN: dataCopy.BRAND_NAME_EN,
                YRN_LOT_NO: dataCopy.YRN_LOT_NO,
                CTN_RCV_QTY: dataCopy.CTN_RCV_QTY,
                QTY_PER_CTN: dataCopy.QTY_PER_CTN,
                RCV_QTY: dataCopy.RCV_QTY,
                PARENT_DS: dataCopy,
                QTY_MOU_ID: 3,
                QTY_MOU_CODE: dataCopy.QTY_MOU_CODE,
                PACK_MOU_ID: dataCopy.PACK_MOU_ID,
                PACK_MOU_CODE: dataCopy.PACK_MOU_CODE,
                RQD_FAB_QTY: dataCopy.RQD_FAB_QTY,
                CUMU_RCV_YRN_QTY: dataCopy.CUMU_RCV_YRN_QTY,
                BAL_YRN_QTY: dataCopy.BAL_YRN_QTY
            }
            //var d = angular.copy(data);
            //data.ChildGrid.dataSource.add(item);
            itmChln.yarnList.push(item);

            $scope.dataItem.CTN_RCV_QTY = null;
            $scope.dataItem.QTY_PER_CTN = null;
            $scope.dataItem.RQD_FAB_QTY = null;
            $scope.dataItem.CUMU_RCV_YRN_QTY = null;
            $scope.dataItem.CUMU_RCV_YRN_QTY = null;

            vm.isYrnAddToGrid = 'Y';
            console.log(itmChln.yarnList);
        };

        vm.getYrnSummeryData = function (data, itmChln) {
            var dataCopy = angular.copy(data);
            console.log(dataCopy);

            return KnittingDataService.getDataByFullUrl('/api/knit/KntScRcv/GetYrnSummery?&pSCM_SC_WO_REF_ID=' + dataCopy.SCM_SC_WO_REF_ID +
                '&pYARN_ITEM_ID=' + dataCopy.YARN_ITEM_ID + '&pKNT_YRN_LOT_ID=' + dataCopy.KNT_YRN_LOT_ID + //'&pLK_YFAB_PART_ID=' + dataCopy.LK_YFAB_PART_ID +
                '&pKNT_SC_YRN_RCV_D_ID=' + dataCopy.KNT_SC_YRN_RCV_D_ID + '&pCHALAN_DT=' + itmChln.CHALAN_DT).then(function (res) {

                $scope.dataItem.RQD_FAB_QTY = res.RQD_FAB_QTY;
                $scope.dataItem.CUMU_RCV_YRN_QTY = res.CUMU_RCV_YRN_QTY;
                
                vm.getYrnBalQty();
            });            
        };

        vm.getYrnBalQty = function () {
            $scope.dataItem.BAL_YRN_QTY = $scope.dataItem.RQD_FAB_QTY - ($scope.dataItem.CUMU_RCV_YRN_QTY + $scope.dataItem.RCV_QTY);
        };

        vm.editYarn = function (index, dataList) {
            $scope.dataItem = angular.copy(dataList[index]);
            console.log($scope.dataItem);

            return vm.yarnItemDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/KntScRcv/GetYrnByScOrdRefID?&pSCM_SC_WO_REF_ID=' + $scope.dataItem.SCM_SC_WO_REF_ID).then(function (res) {
                            e.success(res);

                            $scope.dataItem['yrnRcvDtlIndex'] = index;
                            vm.isYrnAddToGrid = 'N';
                        });
                    }
                }
            });
                        
        };

        vm.updateYarn = function (data, itmChln) {
            var dataCopy = angular.copy(data);
            console.log(dataCopy);

            itmChln.yarnList[data.yrnRcvDtlIndex] = dataCopy;

            $scope.dataItem.CTN_RCV_QTY = null;
            $scope.dataItem.QTY_PER_CTN = null;

            vm.isYrnAddToGrid = 'Y';
            console.log(itmChln.yarnList);
        };

        function findGridIndex(index, KNT_SC_YRN_RCV_H_ID) {
            var dataList = $scope.$parent.mainYrnRcvGridDataSource.data();

            if (index > -1) {
                return index;
            } else {

                return _.findIndex(dataList, function (obj) {
                    return parseInt(obj.KNT_SC_YRN_RCV_H_ID) == parseInt(KNT_SC_YRN_RCV_H_ID);
                });
            }

        };

        vm.updateHrdGridIndex = 0;
        vm.editYrnRcvHdrRow = function (dataItem) {
            var index = $scope.$parent.mainYrnRcvGridDataSource.indexOf(dataItem);
            //alert('index:' + index);

            vm.updateHrdGridIndex = findGridIndex(index, dataItem.KNT_SC_YRN_RCV_H_ID);
            //alert(vm.updateHrdGridIndex);

            console.log(dataItem);
            var yrnRcvFormCopy = { KNT_SC_YRN_RCV_H_ID: dataItem.KNT_SC_YRN_RCV_H_ID, KNT_SC_PRG_RCV_ID: dataItem.KNT_SC_PRG_RCV_ID,
                CHALAN_NO: dataItem.CHALAN_NO, CHALAN_DT: dataItem.CHALAN_DT, SCM_STORE_ID: dataItem.SCM_STORE_ID, REMARKS: dataItem.REMARKS,
                STORE_NAME_EN: dataItem.STORE_NAME_EN, uid: dataItem.uid
            }; //angular.copy(dataItem);
            vm.yrnRcvForm = yrnRcvFormCopy;

            vm.isAddToGrid = 'N';
        };        

        vm.editYrnRcvDtlRow = function (parent, dataItem) {
            console.log(dataItem);

            parent['KNT_SC_YRN_RCV_D_ID'] = dataItem.KNT_SC_YRN_RCV_D_ID;
            parent['KNT_SC_YRN_RCV_H_ID'] = dataItem.KNT_SC_YRN_RCV_H_ID;
            parent['YARN_ITEM_ID'] = dataItem.YARN_ITEM_ID;
            parent['LK_YRN_COLR_GRP_ID'] = dataItem.LK_YRN_COLR_GRP_ID;
            parent['RF_BRAND_ID'] = dataItem.RF_BRAND_ID;
            parent['YRN_LOT_NO'] = dataItem.YRN_LOT_NO;
            parent['CTN_RCV_QTY'] = dataItem.CTN_RCV_QTY;
            parent['PACK_MOU_ID'] = dataItem.PACK_MOU_ID;
            parent['QTY_PER_CTN'] = dataItem.QTY_PER_CTN;
            parent['RCV_QTY'] = dataItem.RCV_QTY;
            parent['QTY_MOU_ID'] = dataItem.QTY_MOU_ID;

            vm.isYrnAddToGrid = 'N';
        };       
       
    }


})();
//=============== End for Yarn Receive =================







//=============== Start for Fabric Color & KP =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntSubContProgFabColorKPController', ['$q', 'config', 'Dialog', 'KnittingDataService', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', '$modal', KntSubContProgFabColorKPController]);
    function KntSubContProgFabColorKPController($q, config, Dialog, KnittingDataService, $stateParams, $state, $scope, $filter, commonDataService, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        var yrnListData = [];
        var brandListData = [];
        var fabPartListData = [];
        var colGrpListData = [];

        //vm.fabOrder = { FIN_DIA_N_DIA_TYPE: '', DIA_MOU_ID: 23, LK_DIA_TYPE_ID: 327, DIA_MOU_CODE: 'Open', QTY_MOU_ID: 3, QTY_MOU_CODE: 'Kg' };
        vm.fabOrder = { MC_FAB_PROD_ORD_D_ID: -1, MC_FAB_PROD_ORD_H_ID: -1, FIN_DIA_N_DIA_TYPE: '', DIA_MOU_CODE: 'Open', QTY_MOU_CODE: 'Kg', IS_SC_PRG_SN_AUTO: 'Y' };

        //vm.yarnReceivState = ($state.current.name === 'KntSubContProgH.YarnReceive') ? true : false;
        //vm.kpState = ($state.current.name === 'KntSubContProgH.KPReceive') ? true : false;


        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getFabTypeList(), getFabricCompList(), getMOUList(), getDiaTypeList(), getColorList(), getMcDiaList(), getMcGgList(), getScByrOrdList(),
                getYarnItemList(), getCatgBrandList(), getFabPartList(), getYarnColorGroupList(), getFeederType(), getYDType()
            ];
            return $q.all(promise).then(function () {
                vm.showSplash = false;                
            });
        }

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.SC_START_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.SC_START_DT_LNopened = true;
        }

        vm.SC_END_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.SC_END_DT_LNopened = true;
        }

        $scope.$watch('$parent.form.SCM_SUPPLIER_ID', function (newVal, oldVal) {

            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    vm.scByrOrdListDataSource.read();
                }
            }
        }, true);

        function getScByrOrdList() {
            vm.scByrOrdListOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SC_WO_REF_NO",
                dataValueField: "SCM_SC_WO_REF_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.fabOrder.SC_WO_REF_NO = item.SC_WO_REF_NO;

                    if (item.SCM_SC_WO_REF_ID < 0) {
                        var modalInstance = $modal.open({
                            animation: true,
                            templateUrl: 'SubContraOrderRef.html',
                            controller: function ($modalInstance, $scope, supID, supOrderList) {
                                var vm = this;
                                vm.errors = null;
                                vm.AopBaseColName = '';
                                vm.colTypeName = '';
                                vm.Title = $state.current.Title || '';                                
                                vm.form = { SCM_SC_WO_REF_ID: 0, SCM_SUPPLIER_ID: supID, SC_WO_REF_NO: '' };


                                activate()
                                vm.showSplash = true;
                                function activate() {
                                    var promise = [GetSupplierList(), GetSupplierWiseOrderList()];
                                    return $q.all(promise).then(function () {
                                        vm.showSplash = false;

                                    });
                                }


                                function GetSupplierWiseOrderList() {
                                    vm.scOrderGridGridOption = {
                                        height: 300,
                                        sortable: true,
                                        scrollable: {
                                            virtual: true,
                                            //scrollable:true
                                        },
                                        pageable: false,
                                        editable: false,
                                        selectable: "row",
                                        navigatable: true,
                                        filterable: {
                                            extra: false,
                                            operators: {
                                                string: {
                                                    contains: "Contains",
                                                    startswith: "Starts With",
                                                    eq: "Is Equal To"
                                                }
                                            }
                                        },
                                        columns: [
                                            { field: "SC_BUYER_NAME", title: "Buyer", type: "string", width: "10%" },
                                            { field: "SC_ORDER_NO", title: "Order#", type: "string", width: "10%" },
                                            { field: "SC_STYLE_NO", title: "Style#", width: "10%" },
                                            { field: "SC_WO_REF_NO", title: "Order Ref#", width: "10%" },
                                            {
                                                title: "Action",
                                                template: function () {
                                                    return "<button type='button' class='btn btn-xs blue' ng-click='vm.edit(dataItem)'><i class='fa fa-edit'></i></button>";
                                                },
                                                width: "4%"
                                            }
                                        ]
                                    };


                                    vm.scOrderGridDataSource = new kendo.data.DataSource({
                                        serverPaging: true,
                                        serverFiltering: true,
                                        schema: {
                                            data: "data",
                                            total: "total",
                                            model: {
                                                id: "SCM_SC_WO_REF_ID"//,
                                                //fields: {
                                                //    SC_PRG_DT: { type: "date", editable: false }
                                                //}
                                            }
                                        },
                                        pageSize: 10,
                                        transport: {
                                            read: function (e) {
                                                var webapi = new kendo.data.transports.webapi({});
                                                var params = webapi.parameterMap(e.data);
                                                var url = '/api/knit/KntScRcv/GetScOrdRefByPartyID?&pSCM_SUPPLIER_ID=' + ((vm.form.SCM_SUPPLIER_ID == null) ? 0 : vm.form.SCM_SUPPLIER_ID);

                                                url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                                                url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                                                return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                                                    e.success(res);
                                                }, function (err) {
                                                    console.log(err);
                                                });
                                            }
                                        }
                                    });
                                }

                                function GetSupplierList() {
                                    vm.supplierOption = {
                                        optionLabel: "-- Select --",
                                        filter: "contains",
                                        autoBind: true,
                                        dataTextField: "SUP_TRD_NAME_EN",
                                        dataValueField: "SCM_SUPPLIER_ID",
                                        select: function (e) {
                                            var item = this.dataItem(e.item);
                                            vm.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;                                            
                                            vm.scOrderGridDataSource.read();
                                            //console.log(item);
                                            //$stateParams.pSCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                                        },
                                        dataBound: function (e) {
                                            if (supID != null && supID > 0) {
                                                vm.form.SCM_SUPPLIER_ID = supID;
                                            }
                                        }
                                    };

                                    return vm.supplierDataSource = new kendo.data.DataSource({
                                        transport: {
                                            read: function (e) {
                                                KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
                                                    e.success(res);
                                                });
                                            }
                                        }
                                    });
                                };
                                                                
                                vm.tranCancel = function () {
                                    var item = angular.copy(vm.form);
                                    vm.form = { SCM_SC_WO_REF_ID: 0, SCM_SUPPLIER_ID: item.SCM_SUPPLIER_ID, SC_WO_REF_NO: '' };
                                }

                                vm.genWoRef = function () {
                                    vm.form.SC_WO_REF_NO = vm.form.SC_ORDER_NO + '-' + vm.form.SC_BUYER_NAME + '-' + vm.form.SC_STYLE_NO;
                                }

                                vm.edit = function (dataItem) {
                                    vm.form = angular.copy(dataItem);
                                }

                                vm.submitData = function (dataOri, token) {
                                    var data = angular.copy(dataOri);
                                    
                                    return KnittingDataService.saveDataByFullUrl(data, '/api/knit/KntScRcv/SaveScOrdRef', token).then(function (res) {
                                        if (res.success === false) {
                                            vm.errors = res.errors;
                                        }
                                        else {
                                            res['data'] = angular.fromJson(res.jsonStr);
                                            if (res.data.V_MC_COLOR_ID != 0 && res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                                dataOri['SCM_SC_WO_REF_ID'] = parseInt(res.data.PSCM_SC_WO_REF_ID_RTN);
                                                vm.scOrderGridDataSource.read();

                                                config.appToastMsg(res.data.PMSG);
                                                //$modalInstance.close(data);
                                            }
                                        }
                                    }).catch(function (message) {
                                        exception.catcher('XHR loading Failded')(message);
                                    });
                                }

                                vm.cancel = function (data) {
                                    //data['COL_TYPE_NAME'] = vm.colTypeName;
                                    $modalInstance.close(data);
                                };
                            },
                            controllerAs: 'vm',
                            size: 'lg',
                            windowClass: 'large-Modal',
                            resolve: {
                                supID: function () {
                                    return $scope.$parent.form.SCM_SUPPLIER_ID; //KnittingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll');
                                },
                                supOrderList: function (KnittingDataService) {
                                    return []; //KnittingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll');
                                }
                            }
                        });

                        modalInstance.result.then(function (result) {
                            console.log(result);

                            if (result.SCM_SC_WO_REF_ID && result.SCM_SC_WO_REF_ID > 0) {
                                $scope.$parent.form.SCM_SUPPLIER_ID = result.SCM_SUPPLIER_ID;

                                vm.scByrOrdListDataSource.read().then(function () {
                                    vm.fabOrder['SCM_SC_WO_REF_ID'] = result.SCM_SC_WO_REF_ID;
                                    vm.fabOrder['SC_WO_REF_NO'] = result.SC_WO_REF_NO;
                                });
                            }
                        }, function () {
                            console.log('Modal dismissed at: ' + new Date());
                        });
                    }
                }
            };

            return vm.scByrOrdListDataSource = new kendo.data.DataSource({
                serverFiltering: true,                
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KntScRcv/GetScOrdRefByPartyID?&pageNumber=1&pageSize=10&pSCM_SUPPLIER_ID=' + (($scope.$parent.form.SCM_SUPPLIER_ID == null) ? 0 : $scope.$parent.form.SCM_SUPPLIER_ID);
                        
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);                        
                        console.log(url);
                        
                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);
                       
                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            console.log(res.data);
                            //e.success([]);
                            e.success([{ 'SC_WO_REF_NO': '---New Order---', SCM_SC_WO_REF_ID: -1 }].concat(res.data));                          
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getFabTypeList() {
            vm.fabTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FAB_TYPE_SNAME",
                dataValueField: "RF_FAB_TYPE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.fabOrder.IS_FBP_VISIBLE = item.IS_FBP_VISIBLE;
                    vm.fabOrder.FAB_TYPE_SNAME = item.FAB_TYPE_SNAME;
                }
            };

            return vm.fabTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/Common/FabricTypeList?pINV_ITEM_CAT_ID=34').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getFabricCompList() {
            vm.fibCompOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FIB_COMP_NAME",
                dataValueField: "RF_FIB_COMP_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.fabOrder.IS_ELA_MXD = item.IS_ELA_MXD;
                    vm.fabOrder.FIB_COMP_NAME = item.FIB_COMP_NAME;
                    
                    if (item.IS_ELA_MXD != 'Y') {
                        vm.fabOrder.LK_FEDER_TYPE_ID = null;
                        vm.feederTypeDataSource.read();
                    }

                    console.log(item);

                    if (this.dataItem(e.item).RF_FIB_COMP_ID == -1) {
                        var modalInstance = $modal.open({
                            animation: true,
                            templateUrl: 'FabricCompositionGeneration.html',
                            controller: function (FiberCompList, FiberTypeList, FiberCompGroup, $modalInstance, $scope, $timeout, config) {
                                var vm = this;
                                vm.form = {};
                                vm.errors = [];
                                vm.FiberCompositionTypeList = FiberTypeList.map(function (o) {
                                    return {
                                        LOOKUP_DATA_ID: o.LOOKUP_DATA_ID,
                                        LK_DATA_NAME_EN: o.LK_DATA_NAME_EN
                                    }
                                });

                                vm.form['FIB_COMP_NAME'] = '';
                                vm.formDisabled = false;


                                vm.FeederTypeList = {
                                    optionLabel: "--Fiber Group--",
                                    filter: "contains",
                                    autoBind: true,
                                    dataSource: {
                                        transport: {
                                            read: function (e) {
                                                e.success(FiberCompGroup);
                                            }
                                        }
                                    },
                                    select: function (e) {
                                        var FabGroupOb = [];
                                        var FIB_TYPE_ID_LST = this.dataItem(e.item).FIB_TYPE_ID_LST;
                                        if (FIB_TYPE_ID_LST) {
                                            FIB_TYPE_ID_LST.split(',').forEach(function (val) {

                                                FabGroupOb.push({
                                                    LOOKUP_DATA_ID: parseInt(val),
                                                    LK_DATA_NAME_EN: _.filter(FiberTypeList, function (o) {
                                                        return o.LOOKUP_DATA_ID == parseInt(val)
                                                    })[0].LK_DATA_NAME_EN
                                                });
                                            });
                                        }
                                        vm.form['LK_FIB_TYPE_LST'] = FabGroupOb;
                                    },
                                    dataTextField: "RF_FIB_COMP_GRP_NAME",
                                    dataValueField: "RF_FIB_COMP_GRP_ID"
                                };
                                


                                $scope.$watch('vm.form.LK_FIB_TYPE_LST', function (newValOri, oldVal) {
                                    if (newValOri && newValOri.length > 0) {
                                        var newVal = angular.copy(newValOri);
                                        vm.form['FIB_COMP_NAME'] = '';

                                        newVal.map(function (o) {
                                            o.PERCENT = o.PERCENT || 0;
                                            return o;
                                        });

                                        var pecentValue = _.sumBy(newVal, function (o) {
                                            return o.PERCENT;
                                        });

                                        var isFillUpData = _.every(newVal, function (o) {
                                            return o.PERCENT > 0;
                                        })


                                        //if (isFillUpData) {
                                        //    var dataToBeCheck = [];
                                        //    FiberCompList.forEach(function (o) {
                                        //        dataToBeCheck.push(config.parseXmlString(o.LK_FIB_TYPE_LST).map(function (ob) {
                                        //            return {
                                        //                LOOKUP_DATA_ID: parseInt(ob.LOOKUP_DATA_ID),
                                        //                PERCENT: parseInt(ob.PERCENT)
                                        //            }
                                        //        }));

                                        //    });

                                        //    var isAvailable = _.some(dataToBeCheck, function (o) {

                                        //        return angular.equals(o, newVal.map(function (ob) {
                                        //            return {
                                        //                LOOKUP_DATA_ID: parseInt(ob.LOOKUP_DATA_ID),
                                        //                PERCENT: parseInt(ob.PERCENT)
                                        //            }
                                        //        }));

                                        //    });




                                        //}

                                        //if (isAvailable && isFillUpData) {

                                        //    $timeout(function () {
                                        //        vm.errors = [['Duplicate Composition is not allowed. Please try another']];
                                        //        vm.formDisabled = true;
                                        //    })

                                        //};

                                        if (pecentValue < 100 && isFillUpData) {
                                            vm.errors = [['Wrong Percent Value !!!. Percent Value must be 100 in total.']];
                                            vm.formDisabled = true;
                                        };


                                        if (pecentValue == 100 && isFillUpData) {

                                            vm.formDisabled = false;
                                            vm.errors = [];

                                        };

                                        if (pecentValue > 100 && isFillUpData) {
                                            vm.formDisabled = true;
                                            vm.errors = [['Wrong Percent Value !!!. Percent Value must be 100 in total.']];
                                        };


                                        newVal.forEach(function (val, key) {
                                            vm.form['FIB_COMP_NAME'] += ' ' + val.PERCENT + '% ' + val.LK_DATA_NAME_EN;
                                        });
                                        vm.form['FIB_COMP_NAME'] = vm.form['FIB_COMP_NAME'].replace('null', '').trim();
                                    } else {
                                        vm.form['FIB_COMP_NAME'] = '';
                                    }

                                }, true);


                                vm.cancel = function (data) {
                                    $modalInstance.close(data);
                                };
                                vm.submitData = function (dataOri, token) {
                                    var data = angular.copy(dataOri);


                                    data['IS_ELA_MXD'] = (
                                        _.some(data.LK_FIB_TYPE_LST, function (o) {
                                            return o.LOOKUP_DATA_ID == 375;
                                        })
                                        ) ? 'Y' : 'N';


                                    data['LK_FIB_TYPE_LST'] = config.xmlStringLong(
                                             data.LK_FIB_TYPE_LST.map(function (o) {
                                                 return {
                                                     LOOKUP_DATA_ID: o.LOOKUP_DATA_ID,
                                                     PERCENT: o.PERCENT
                                                 }
                                             })
                                         );

                                    return KnittingDataService.saveDataByFullUrl(data, '/api/mrc/StyleDItemFab/SaveFiberComposition', token).then(function (res) {
                                        if (res.success === false) {
                                            vm.errors = res.errors;
                                        }
                                        else {
                                            res['data'] = angular.fromJson(res.jsonStr);
                                            config.appToastMsg(res.data.PMSG);

                                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                                vm.form['RF_FIB_COMP_ID'] = parseInt(res.data.V_RF_FIB_COMP_ID);
                                            }
                                        }
                                    }).catch(function (message) {
                                        exception.catcher('XHR loading Failded')(message);
                                    });

                                };


                            },
                            controllerAs: 'vm',
                            size: 'lg',
                            windowClass: 'large-Modal',
                            resolve: {
                                FiberCompList: function () {
                                    return vm.fibCompDataSource.data();
                                },
                                FiberTypeList: function () {
                                    return KnittingDataService.LookupListData(76);
                                },
                                FiberCompGroup: function () {
                                    return KnittingDataService.getDataByFullUrl('/api/mrc/YarnSpec/FiberCompGroup');
                                }
                            }
                        });

                        modalInstance.result.then(function (result) {

                            if (result.RF_FIB_COMP_ID && result.RF_FIB_COMP_ID > 0) {
                                vm.fibCompDataSource.read().then(function () {
                                    vm.fabOrder['RF_FIB_COMP_ID'] = result.RF_FIB_COMP_ID;
                                });
                            }

                        }, function () {
                            console.log('Modal dismissed at: ' + new Date());
                        });


                    }

                }                            
            };

            return vm.fibCompDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/Common/FibreCompList').then(function (res) {
                            e.success([{ 'FIB_COMP_NAME': '---New Composition---', RF_FIB_COMP_ID: -1 }].concat(res));
                        });
                    }

                }
            });

        };
        
        function getMOUList() {
            vm.diaMouOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID",
                select: function (e) {                    
                    var item = this.dataItem(e.item);
                    vm.fabOrder.DIA_MOU_CODE = item.MOU_CODE;
                },
                dataBound: function (e) {
                    vm.fabOrder['DIA_MOU_ID'] = 23;
                }
            };

            vm.qtyMouOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.fabOrder.QTY_MOU_CODE = item.MOU_CODE;
                },
                dataBound: function (e) {
                    vm.fabOrder['QTY_MOU_ID'] = 3;
                }
            };

            return KnittingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                
                vm.diaMouDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });

                
                vm.qtyMouDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });
            });
            
        };

        function getDiaTypeList() {
            vm.diaTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {                    
                    var item = this.dataItem(e.item);
                    vm.fabOrder.FIN_DIA_TYPE = item.LK_DATA_NAME_EN;
                },
                dataBound: function (e) {
                    vm.fabOrder['LK_DIA_TYPE_ID'] = 327;
                }
            };

            return vm.diaTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/common/LookupListData/67').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getColorList() {
            vm.colorListOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COLOR_NAME_EN",
                dataValueField: "MC_COLOR_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.fabOrder.COLOR_NAME_EN = item.COLOR_NAME_EN;
                    vm.fabOrder.LK_COL_TYPE_ID = item.LK_COL_TYPE_ID;
                    vm.fabOrder.COL_TYPE_NAME = item.COL_TYPE_NAME;
                    
                    if (item.LK_COL_TYPE_ID != 360) {
                        vm.fabOrder.LK_YD_TYPE_ID = null;
                        vm.ydTypeDataSource.read();
                    }


                    if (item.MC_COLOR_ID < 0) {
                        var modalInstance = $modal.open({
                            animation: true,
                            templateUrl: 'NewColourEntryModal.html',
                            controller: 'ColourMasterModalController',
                            controllerAs: 'vm',
                            size: 'lg',
                            windowClass: 'large-Modal',
                            resolve: {
                                ColourList: function (KnittingDataService) {
                                    return KnittingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll');
                                }
                            }
                        });

                        modalInstance.result.then(function (result) {
                            if (result.MC_COLOR_ID && result.MC_COLOR_ID > 0) {
                                vm.colorListDataSource.read().then(function () {
                                    vm.fabOrder['FAB_COLOR_ID'] = result.MC_COLOR_ID;
                                    vm.fabOrder['COLOR_NAME_EN'] = result.COLOR_NAME_EN;
                                });
                            }
                        }, function () {
                            console.log('Modal dismissed at: ' + new Date());
                        });
                    }
                }
            };

            return vm.colorListDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll').then(function (res) {
                            e.success([{ 'COLOR_NAME_EN': '---New Color---', MC_COLOR_ID: -1 }].concat(res));
                        });
                    }
                }
            });
        };

        function getMcDiaList() {
            vm.mcDiaOption = {
                optionLabel: "-- Dia --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MC_DIA",
                dataValueField: "KNT_MC_DIA_ID"
            };

            return vm.mcDiaDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/Knit/KnitCommon/getMachineDiaList').then(function (res) {                            
                            var dataList = [];
                            angular.forEach(res, function (val, key) {
                                val['MC_DIA'] = val['MC_DIA'].toString();
                                dataList.push(val);
                            });

                            e.success(dataList);
                            //console.log(dataList);
                        });
                    }
                }
            });
        };

        function getMcGgList() {
            vm.mcGgOption = {
                optionLabel: "-- GG --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };

            return vm.mcGgDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByUrlNoToken('/api/common/LookupListData/59').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        vm.onChangeFinDia = function () {
            vm.fabOrder.FIN_DIA_N_DIA_TYPE = vm.fabOrder.FIN_DIA + ' ' + vm.fabOrder.DIA_MOU_CODE + ' ' + vm.fabOrder.FIN_DIA_TYPE;
        }

        vm.onChangeNetFfQty = function () {
            vm.fabOrder.NET_FFAB_QTY_NAME = vm.fabOrder.NET_FFAB_QTY + ' ' + vm.fabOrder.QTY_MOU_CODE;
        }

        vm.isScProgSlAuto = function () {
            vm.fabOrder.SC_PRG_SN = "";
        }

        function getFeederType() {
            vm.feederTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                },
            };

            return vm.feederTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByUrlNoToken('/api/common/LookupListData/60').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getYDType() {
            vm.ydTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                },
            };

            return vm.ydTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByUrlNoToken('/api/common/LookupListData/71').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        vm.addRow = function (data) {
            var fabOrderCopy = angular.copy(data);
            data['fabProdYrn'] = [];

            var yrnDataList = vm.yrnFabGridDataSource.data();
            var yrnTotal = _.sumBy(yrnDataList, function (o) {
                return o.RQD_YRN_QTY || 0;
            });
            var yrnTotalRatio = _.sumBy(yrnDataList, function (o) {
                return o.RATIO_QTY || 0;
            });
            
            if (yrnTotal != fabOrderCopy.NET_FFAB_QTY) {
                alert('Sorry! Yarn qty. and fabric qty. should be equal');
            }
            else if (yrnTotalRatio != 100) {
                alert('Sorry! Total Ratio qty. should be 100%');
            }
            else {
                angular.forEach(yrnDataList, function (val, key) {
                    val['editMode'] = false;
                    data['fabProdYrn'].push(val);
                });


                $scope.$parent.fabOrderGridDataSource.insert(0, data);

                vm.cancelToGrid();
                vm.yrnFabGridDataSource.read();

                vm.isAddToGrid = 'Y';
            }
        }
        
        function findGridIndex(index, KNT_SC_YRN_RCV_H_ID) {
            var dataList = $scope.$parent.mainYrnRcvGridDataSource.data();

            if (index > -1) {
                return index;
            } else {

                return _.findIndex(dataList, function (obj) {
                    return parseInt(obj.KNT_SC_YRN_RCV_H_ID) == parseInt(KNT_SC_YRN_RCV_H_ID);
                });
            }

        };

        vm.updateIndex = 0;
        vm.editRow = function (dataItem) {
            var index = $scope.$parent.fabOrderGridDataSource.indexOf(dataItem);
            //alert('index:' + index);

            vm.updateIndex = findGridIndex(index, dataItem.KNT_SC_YRN_RCV_H_ID);
            //alert(vm.updateHrdGridIndex);

            console.log(dataItem);
            var data = angular.copy(dataItem);
            vm.fabOrder = data;

            vm.yrnFabGridDataSource.read();
            angular.forEach(data['fabProdYrn'], function (val, key) {
                vm.yrnFabGridDataSource.insert(val);
            });

            vm.isAddToGrid = 'N';
        };

        vm.removeRow = function (dataItem) {         
            $scope.$parent.fabOrderGridDataSource.remove(dataItem);
        };

        vm.updateFabOrder = function (data) {
            var fabOrderCopy = angular.copy(data);

            var yrnDataList = vm.yrnFabGridDataSource.data();
            var yrnTotal = _.sumBy( yrnDataList, function (o) {
                return  o.RQD_YRN_QTY||0;
            });
            
            if (yrnTotal != fabOrderCopy.NET_FFAB_QTY) {
                alert('Sorry! Yarn qty. and fabric qty. should be equal');
            }
            else {
                var dataItem = $scope.$parent.fabOrderGridDataSource.getByUid(data.uid);

                console.log(dataItem);

                dataItem.set('SC_BUYER_NAME', fabOrderCopy.SC_BUYER_NAME);
                dataItem.set('SC_WO_REF_NO', fabOrderCopy.SC_WO_REF_NO);
                dataItem.set('RF_FAB_TYPE_ID', fabOrderCopy.RF_FAB_TYPE_ID);
                dataItem.set('FAB_TYPE_SNAME', fabOrderCopy.FAB_TYPE_SNAME);
                dataItem.set('RF_FIB_COMP_ID', fabOrderCopy.RF_FIB_COMP_ID);
                dataItem.set('FIB_COMP_NAME', fabOrderCopy.FIB_COMP_NAME);
                dataItem.set('FIN_DIA', fabOrderCopy.FIN_DIA);
                dataItem.set('DIA_MOU_ID', fabOrderCopy.DIA_MOU_ID);
                dataItem.set('LK_DIA_TYPE_ID', fabOrderCopy.LK_DIA_TYPE_ID);
                dataItem.set('FIN_GSM', fabOrderCopy.FIN_GSM);
                dataItem.set('FAB_COLOR_ID', fabOrderCopy.FAB_COLOR_ID);
                dataItem.set('LK_COL_TYPE_ID', fabOrderCopy.LK_COL_TYPE_ID);
                dataItem.set('COLOR_NAME_EN', fabOrderCopy.COLOR_NAME_EN);
                dataItem.set('NET_FFAB_QTY', fabOrderCopy.NET_FFAB_QTY);
                dataItem.set('QTY_MOU_ID', fabOrderCopy.QTY_MOU_ID);
                dataItem.set('NET_FFAB_QTY_NAME', fabOrderCopy.NET_FFAB_QTY_NAME);
                dataItem.set('KNT_MC_DIA_ID', fabOrderCopy.KNT_MC_DIA_ID);
                dataItem.set('LK_MC_GG_ID', fabOrderCopy.LK_MC_GG_ID);
                dataItem.set('PROD_RATE', fabOrderCopy.PROD_RATE);
                dataItem.set('SC_START_DT', fabOrderCopy.SC_START_DT);
                dataItem.set('SC_END_DT', fabOrderCopy.SC_END_DT);
                dataItem.set('SC_PRG_SN', fabOrderCopy.SC_PRG_SN);
                dataItem.set('LK_YD_TYPE_ID', fabOrderCopy.LK_YD_TYPE_ID);
                dataItem.set('LK_FEDER_TYPE_ID', fabOrderCopy.LK_FEDER_TYPE_ID);

                vm.fabOrder.MC_FAB_PROD_ORD_D_ID = -1;
                vm.fabOrder.NET_FFAB_QTY = '';

                dataItem.set('fabProdYrn', yrnDataList);
                dataItem.set('editMode', false);

                vm.yrnFabGridDataSource.read();

                vm.isAddToGrid = 'Y';
            }
        }
        
        vm.cancelToGrid = function () {
            vm.fabOrder.NET_FFAB_QTY = '';

            vm.fabOrder.PROD_RATE = '';
            vm.fabOrder.MC_BLK_FAB_REQ_H_ID = 0;
            vm.fabOrder.MC_BLK_FAB_REQ_D_ID = 0;
            vm.fabOrder.SC_PRG_SN = '';
            vm.fabOrder.IS_SC_PRG_SN_AUTO = 'Y';

            vm.yrnFabGridDataSource.read();

            vm.isAddToGrid = 'Y';
        };
               
        function getYarnItemList() {            
            return KnittingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=2').then(function (res) {

                angular.forEach(res, function (val, key) {
                    val['YARN_ITEM_ID'] = val['INV_ITEM_ID'];
                    val['YR_SPEC_DESC'] = val['ITEM_NAME_EN'];
                });

                console.log(res);
                yrnListData = res;
            });
        };
        
        function getCatgBrandList() {
            return vm.catBrandDataSource = new kendo.data.DataSource({
                //serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/common/GetItemBrandList';
                        //var url = '/api/purchase/SupplierProfile/GetSupplierBrandList?pSCM_SUPPLIER_ID=' + $scope.$parent.form.SCM_SUPPLIER_ID || 0;

                        //url += KnittingDataService.kFilterStr2QueryParam(params.filter);
                        //console.log(url);

                        //var paramsData = params.filter.replace(/'/g, '').split('~');
                        //console.log(paramsData[2]);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            console.log(res);
                            //e.success([]);
                            var list = res; //_.filter(res, { 'INV_ITEM_CAT_ID': 2 });
                            e.success([{ BRAND_NAME_EN: '--New Brand--', RF_BRAND_ID: -1 }].concat(list));
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

            //return KnittingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
            //    var list = _.filter(res, { 'INV_ITEM_CAT_ID': 2 });
            //    brandListData = list;
            //}, function (err) {
            //    console.log(err);
            //});
        };

        function getFabPartList() {
            return KnittingDataService.LookupListData(79).then(function (res) {
                angular.forEach(res, function (val, key) {
                    val['LK_YFAB_PART_ID'] = val['LOOKUP_DATA_ID'];
                    val['FAB_PART_GRP_NAME_EN'] = val['LK_DATA_NAME_EN'];
                });

                fabPartListData = res;
            }, function (err) {
                console.log(err);
            });
        };

        function getYarnColorGroupList() {
           
            return KnittingDataService.LookupListData(93).then(function (res) {
                angular.forEach(res, function (val, key) {
                    val['LK_YRN_COLR_GRP_ID'] = val['LOOKUP_DATA_ID'];
                    val['COLOR_GRP_NAME_EN'] = val['LK_DATA_NAME_EN'];
                });

                colGrpListData = res;
            }, function (err) {
                console.log(err);
            });
        };

        function YarnItemDropDownEditor(container, options) {
            if (options.model.editMode == false) {
                $("<span>" + options.model.get(options.field).YR_SPEC_DESC + "</span>").appendTo(container);
                return;
            }

            $('<input data-text-field="YR_SPEC_DESC" data-value-field="YARN_ITEM_ID" data-bind="value:' + options.field + '" required />')
                .appendTo(container)
                .kendoDropDownList({
                    filter: "contains",
                    //optionLabel: "--Select--",
                    autoBind: false,
                    dataSource: {
                        data: yrnListData
                        //transport: {
                        //    read: function (e) {
                        //        var url = "/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=2";
                        //        var webapi = new kendo.data.transports.webapi({});
                        //        var params = webapi.parameterMap(e.data);
                        //        if (params.filter) {
                        //            url += '&pYR_SPEC_DESC=' + params.filter.replace(/'/g, '').split('~')[2];
                        //        }
                        //        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        //            angular.forEach(res, function (val, key) {
                        //                val['YARN_ITEM_ID'] = val['INV_ITEM_ID'];
                        //                val['YR_SPEC_DESC'] = val['ITEM_NAME_EN'];
                        //            });

                        //            e.success(res);
                        //        });                                
                        //    }
                        //},
                        //serverFiltering: true
                    },
                    change: function (e) {
                        var item = this.dataItem(e.item);
                        console.log(item);
                        options.model.YARN_ITEM_ID = options.model.YR_SPEC.YARN_ITEM_ID;
                        //vm.yrnFabGridDataSource.sync();
                        var grid = $("#yrnFabGrid").data("kendoGrid");
                        grid.refresh();
                    }
                });
        }

        function BrandDropDownEditor(container, options) {
            if (options.model.editMode == false) {
                $("<span>" + options.model.get(options.field).BRAND_NAME_EN + "</span>").appendTo(container);
                return;
            }

            $('<input data-text-field="BRAND_NAME_EN" data-value-field="RF_BRAND_ID" data-bind="value:' + options.field + '" required />')
                .appendTo(container)
                .kendoDropDownList({
                    filter: "contains",
                    //optionLabel: "--Select--",
                    autoBind: false,
                    dataSource: vm.catBrandDataSource,
                    change: function (e) {
                        var item = this.dataItem(e.item);
                        console.log(item);
                        
                        options.model.RF_BRAND_ID = item.RF_BRAND_ID;

                        var grid = $("#yrnFabGrid").data("kendoGrid");
                        grid.refresh();
                    },
                    select: function (e) {
                        var item = this.dataItem(e.item);
                        console.log(item);
                        //options.model.CAT_BRAND = { RF_BRAND_ID: item.RF_BRAND_ID, BRAND_NAME_EN: item.BRAND_NAME_EN };
                        //options.model.RF_BRAND_ID = item.RF_BRAND_ID;

                        var aryB = {};
                        if (item.RF_BRAND_ID == -1) {
                            var modalInstance = $modal.open({
                                animation: true,
                                templateUrl: '../../GlobalUI/BrandEntry',
                                controller: function ($modalInstance, $scope, $timeout, KnittingDataService) {
                                    var vm = this;
                                    vm.form = {};
                                    vm.errors = [];
                                    BrandGetAll()
                                    function BrandGetAll() {

                                        KnittingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                                            var list = res; //_.filter(res, { 'INV_ITEM_CAT_ID': 2 });
                                            vm.brandAllList = new kendo.data.DataSource({
                                                data: list
                                            });
                                        });
                                    };

                                    vm.gridBrand = {
                                        pageable: false,
                                        filterable: {
                                            extra: false,
                                            operators: {
                                                string: {
                                                    contains: "Contains",
                                                    startswith: "Starts With",
                                                    eq: "Is Equal To"
                                                }
                                            }
                                        },
                                        height: '400px',
                                        scrollable: true,
                                        groupable: false,
                                        columns: [

                                            { field: "RF_BRAND_ID", hidden: true },
                                            { field: "BRAND_NAME_EN", title: "Brand Name (EN)", width: "30%" },
                                            { field: "BRAND_NAME_BN", title: "Brand Name (BN)", width: "30%" },
                                            { field: "IS_ACTIVE", title: "Is Active", width: "20%" },
                                            {
                                                title: "",
                                                template: '<a title="Edit" ng-click="vm.editBrand(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                                                width: "20%"
                                            }
                                        ],
                                        //editable: true

                                    };

                                    vm.form['BRAND_NAME_EN'] = '';
                                    vm.formDisabled = false;


                                    vm.cancel = function (data) {                                        
                                        aryB = data;
                                        $modalInstance.close(vm.brandAllList);
                                    };

                                    vm.editBrand = function (data) {
                                        vm.form = angular.copy(data);
                                    }

                                    vm.removeBrand = function (data) {

                                        if (!data.RF_BRAND_ID || data.RF_BRAND_ID <= 0) {
                                            vm.brandAllList.remove(data);
                                            return;
                                        };

                                        Dialog.confirm('Removing "' + data.BRAND_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                                             .then(function () {
                                                 vm.brandAllList.remove(data);
                                             });

                                    }

                                    vm.resetBrandInfo = function () {
                                        vm.form = {};
                                    };

                                    vm.submitDataNewBrand = function (dataOri, token) {
                                        var data = angular.copy(dataOri);
                                        data['INV_ITEM_CAT_ID'] = 2;
                                        return KnittingDataService.saveDataByFullUrl(data, '/api/common/BrandSave', token).then(function (res) {
                                            console.log(res);
                                            if (res.success === false) {
                                                vm.errors = res.errors;
                                            }
                                            else {
                                                res['data'] = angular.fromJson(res.jsonStr);
                                                config.appToastMsg(res.data.PMSG);

                                                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                                    vm.form['RF_BRAND_ID'] = parseInt(res.data.OPRF_BRAND_ID);
                                                }
                                                BrandGetAll();
                                                //vm.form = {};
                                            }
                                        }).catch(function (message) {
                                            exception.catcher('XHR loading Failded')(message);
                                        });

                                    };


                                },
                                controllerAs: 'vm',
                                size: 'lg',
                                windowClass: 'large-Modal',

                            });

                            modalInstance.result.then(function (data) {
                                console.log(data.data());
                                                                
                                if (aryB.RF_BRAND_ID && aryB.RF_BRAND_ID > 0) {
                                    
                                    vm.catBrandDataSource.read().then(function () {
                                        options.model.RF_BRAND_ID = aryB.RF_BRAND_ID;
                                        options.model.set('CAT_BRAND', { RF_BRAND_ID: aryB.RF_BRAND_ID, BRAND_NAME_EN: aryB.BRAND_NAME_EN });                                    
                                    });
                                }                     

                            }, function () {
                                console.log('Modal dismissed at: ' + new Date());
                            });
                        }
                        else {
                            options.model.RF_BRAND_ID = item.RF_BRAND_ID;                            
                        }
                    }
                });
        }

        function FabPartDropDownEditor(container, options) {
            console.log(options.model.IS_FBP_VISIBLE);

            if (options.model.IS_FBP_VISIBLE != 'Y') {
                $("<span>" + options.model.get(options.field).FAB_PART_GRP_NAME_EN + "</span>").appendTo(container);
                return;
            }
            else if (options.model.editMode == false) {
                $("<span>" + options.model.get(options.field).FAB_PART_GRP_NAME_EN + "</span>").appendTo(container);
                return;
            }

            $('<input data-text-field="FAB_PART_GRP_NAME_EN" data-value-field="LK_YFAB_PART_ID" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({
                    filter: "contains",
                    //optionLabel: "--Select--",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: function (e) {
                                e.success(fabPartListData);
                            }
                        }
                    },
                    change: function (e) {
                        var item = this.dataItem(e.item);
                        console.log(item);
                        //options.model.FAB_PART = { LK_YFAB_PART_ID: item.LOOKUP_DATA_ID, FAB_PART_GRP_NAME_EN: item.LK_DATA_NAME_EN };
                        options.model.LK_YFAB_PART_ID = item.LK_YFAB_PART_ID;

                        var grid = $("#yrnFabGrid").data("kendoGrid");
                        grid.refresh();
                    }
                });
        }

        function ColGrpDropDownEditor(container, options) {
            if (options.model.editMode == false) {
                $("<span>" + options.model.get(options.field).COLOR_GRP_NAME_EN + "</span>").appendTo(container);
                return;
            }

            $('<input data-text-field="COLOR_GRP_NAME_EN" data-value-field="LK_YRN_COLR_GRP_ID" data-bind="value:' + options.field + '" required />')
                .appendTo(container)
                .kendoDropDownList({
                    filter: "contains",
                    //optionLabel: "--Select--",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: function (e) {
                                e.success(colGrpListData);
                            }
                        }
                    },
                    change: function (e) {
                        var item = this.dataItem(e.item);
                        console.log(item);
                        //options.model.COLOR_GRP = { LK_YRN_COLR_GRP_ID: item.LOOKUP_DATA_ID, COLOR_GRP_NAME_EN: item.LK_DATA_NAME_EN };
                        options.model.LK_YRN_COLR_GRP_ID = item.LK_YRN_COLR_GRP_ID;

                        var grid = $("#yrnFabGrid").data("kendoGrid");
                        grid.refresh();
                    }
                });
        }

        vm.addNewYarn = function () {
            var yrnDataList = vm.yrnFabGridDataSource.data();
            var ratioQty = _.sumBy(yrnDataList, function (o) { return o.RATIO_QTY || 0; });
            var rqdQty = _.sumBy(yrnDataList, function (o) { return o.RQD_YRN_QTY || 0; });
            //console.log(ratioQty);
            //alert(vm.fabOrder.IS_FBP_VISIBLE);

            var data = {
                MC_FAB_PROD_D_YRN_ID: -1, MC_FAB_PROD_ORD_D_ID: -1, YARN_ITEM_ID: -1, LK_YRN_COLR_GRP_ID: 481, RF_BRAND_ID: -1,
                KNT_YRN_LOT_ID: -1, LK_YFAB_PART_ID: (vm.fabOrder.IS_FBP_VISIBLE=='Y'?426:''), STITCH_LEN: null, 
                PCT_ELA_QTY: null, RATIO_QTY: (100 - ratioQty), RQD_YRN_QTY: (vm.fabOrder.NET_FFAB_QTY - rqdQty),
                editMode: true, IS_FBP_VISIBLE: vm.fabOrder.IS_FBP_VISIBLE,
                YR_SPEC: { YARN_ITEM_ID: -1, YR_SPEC_DESC: '--Select--' },
                CAT_BRAND: { RF_BRAND_ID: -1, BRAND_NAME_EN: '--Select--' },
                FAB_PART: { LK_YFAB_PART_ID: (vm.fabOrder.IS_FBP_VISIBLE == 'Y' ? 426 : ''), FAB_PART_GRP_NAME_EN: (vm.fabOrder.IS_FBP_VISIBLE == 'Y' ? 'Top' : '--Select--') },
                COLOR_GRP: { LK_YRN_COLR_GRP_ID: 481, COLOR_GRP_NAME_EN: 'White' }
            };

            //console.log($scope.$parent.form);
            //vm.catBrandDataSource.read();
            vm.yrnFabGridDataSource.insert(0, data);
        }

        vm.removeYarn = function (dataItem) {
            vm.yrnFabGridDataSource.remove(dataItem);
        }

        vm.makeEditMode = function (dataItem) {
            dataItem['editMode'] = !dataItem['editMode'];
        }

        vm.yrnFabGridOption = {
            height: 130,
            sortable: false,
            scrollable: true,
            editable: true,
            selectable: "cell",
            navigatable: true,
            //dataBound: function (e) {
            //    $('#yrnFabGrid td').on('dblclick', function (e) {

            //        if ($(".k-grid-edit-row").length <= 1) {
            //            $("#yrnFabGrid").data("kendoGrid").editCell($(this));
            //            $(this).focusout(function () {
            //                if (!$("#yrnFabGrid").data("kendoGrid").select().hasClass("k-state-selected"))
            //                    if (!$(".k-grid-edit-row input").hasClass("k-invalid"))
            //                        $("#yrnFabGrid").data("kendoGrid").closeCell();
            //            });
            //        }
            //    });
            //},
            columns: [
                //{ field: "YARN_ITEM_ID", title: "YrID", type: "string", width: "5%" },
                {
                    field: "YR_SPEC", title: "Yarn", type: "string", width: "30%", editor: YarnItemDropDownEditor, template: "#=YR_SPEC.YR_SPEC_DESC#",
                    footerTemplate: "Total Record: #=count#",
                    attributes: {
                        "ng-style": "{{this.dataItem.YARN_ITEM_ID<1 ? vm.errstyle:''}}",
                        "ng-required": "true"
                    }
                },
                {
                    field: "FAB_PART", title: "Fab.Part", width: "10%", editor: FabPartDropDownEditor, template: "#=FAB_PART.FAB_PART_GRP_NAME_EN#",
                    attributes: {                        
                        "ng-style": "{{(this.dataItem.LK_YFAB_PART_ID<1 && this.dataItem.IS_FBP_VISIBLE==\'Y\') ? vm.errstyle: ''}}"
                    }
                },
                {
                    field: "COLOR_GRP", title: "Col.Grp", width: "7%", editor: ColGrpDropDownEditor, template: "#=COLOR_GRP.COLOR_GRP_NAME_EN#",
                    attributes: {
                        "ng-style": "{{(this.dataItem.LK_YRN_COLR_GRP_ID<1) ? vm.errstyle: ''}}"
                    }
                },
                {
                    field: "CAT_BRAND", title: "Brand", type: "string", width: "12%", editor: BrandDropDownEditor, template: "#=CAT_BRAND.BRAND_NAME_EN#",
                    attributes: {
                        "ng-style": "{{(this.dataItem.RF_BRAND_ID<1) ? vm.errstyle: ''}}"
                    }
                },
                //{ field: "YRN_LOT_NO", title: "Lot", width: "10%" },
                {
                    title: "Lot#",
                    field: "YRN_LOT_NO",
                    template: function () {
                        return "<ng-form name='frmYrnLotNo'><input type='text' class='form-control' name='YRN_LOT_NO' ng-model='dataItem.YRN_LOT_NO' ng-style='frmYrnLotNo.YRN_LOT_NO.$error.required? vm.errstyle:\"\"' ng-disabled='!dataItem.editMode' required /> </ng-form>";
                    },
                    width: "10%",
                    filterable: false
                },
                //{ field: "STITCH_LEN", title: "SL", width: "10%" },
                {
                    title: "S.Len",
                    field: "STITCH_LEN",                    
                    template: function () {
                        return "<ng-form name='frmSlYrnQty'><input type='number' class='form-control' name='STITCH_LEN' ng-model='dataItem.STITCH_LEN' min='0' ng-style='frmSlYrnQty.STITCH_LEN.$error.min||frmSlYrnQty.STITCH_LEN.$error.required? vm.errstyle:\"\"' ng-disabled='!dataItem.editMode' required /></ng-form>";
                    },
                    width: "5%",
                    filterable: false
                },
                //{ field: "PCT_ELA_QTY", title: "Ela.Pct", width: "10%" },
                {
                    title: "Ela.Pct",
                    field: "PCT_ELA_QTY",
                    template: function () {
                        return "<input type='number' class='form-control' name='PCT_ELA_QTY' ng-model='dataItem.PCT_ELA_QTY' min='0' ng-disabled='!dataItem.editMode' />";
                    },
                    width: "7%",
                    filterable: false
                },
                //{ field: "RATIO_QTY", title: "Rat.Qty", width: "10%" },
                {
                    title: "Ratio Qty",
                    field: "RATIO_QTY",
                    template: function () {
                        return "<ng-form name='frmRatioYrnQty'><input type='number' class='form-control' name='RATIO_QTY' ng-model='dataItem.RATIO_QTY' min='0' ng-style='frmRatioYrnQty.RATIO_QTY.$error.min||frmRatioYrnQty.RATIO_QTY.$error.required? vm.errstyle:\"\"' ng-disabled='!dataItem.editMode' required /></ng-form>";
                    },
                    width: "7%",
                    filterable: false
                },
                //{ field: "RQD_YRN_QTY", title: "Rqd.Qty", width: "10%" },
                {
                    title: "Rqd.Qty",
                    field: "RQD_YRN_QTY",
                    footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmRqdYrnQty'><input type='number' class='form-control' name='RQD_YRN_QTY' ng-model='dataItem.RQD_YRN_QTY' min='0' ng-style='frmRqdYrnQty.RQD_YRN_QTY.$error.min||frmRqdYrnQty.RQD_YRN_QTY.$error.required? vm.errstyle:\"\"' ng-disabled='!dataItem.editMode' required /></ng-form>";
                    },
                    width: "7%",
                    filterable: false
                },
                {
                    title: "Action",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.makeEditMode(dataItem)'><i class='fa fa-pencil'></i></button>&nbsp;<button type='button' class='btn btn-xs red' ng-click='vm.removeYarn(dataItem)' ><i class='fa fa-remove'></i></button>";
                    },
                    width: "5%"
                }
            ]
        };

        vm.yrnFabGridDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    e.success([]);
                    //return KnittingDataService.getDataByFullUrl('/api/knit/KntScRcv/GetYrnListByFabOrd?pMC_FAB_PROD_ORD_D_ID=' + (vm.fabOrder.MC_FAB_PROD_ORD_D_ID || 0)).then(function (res) {

                    //    if (res.length > 0) {
                    //        e.success(res);
                    //    }
                    //    else {
                    //        e.success([]);
                    //    }

                    //}, function (err) {
                    //    console.log(err);
                    //});
                }
            },
            aggregate: [
                { field: "YR_SPEC", aggregate: "count" },
                { field: "RQD_YRN_QTY", aggregate: "sum" }
            ],
            schema: {                
                model: {
                    id: "MC_FAB_PROD_D_YRN_ID",
                    fields: {                        
                        //STITCH_LEN: { type: "number", editable: false },
                        //PCT_ELA_QTY: { type: "number", editable: false },
                        //RATIO_QTY: { type: "number", editable: false },
                        RQD_YRN_QTY: { type: "number", editable: false },
                                                
                        YR_SPEC: { defaultValue: { YARN_ITEM_ID: "", YR_SPEC_DESC: "--Select--" }, editable: true },
                        CAT_BRAND: { defaultValue: { RF_BRAND_ID: "", BRAND_NAME_EN: "--Select--" }, editable: true },
                        FAB_PART: { defaultValue: { LK_YFAB_PART_ID: "", FAB_PART_GRP_NAME_EN: '--Select--' }, editable: true },
                        COLOR_GRP: { defaultValue: { LK_YRN_COLR_GRP_ID: "", COLOR_GRP_NAME_EN: '--Select--' }, editable: true }
                    }
                }
            }
        });

    }
})();
//=============== End for Fabric Color & KP =================






//=============== Start for Knitting Subcontract Program List =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntSubContProgListController', ['$q', 'config', 'Dialog', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', KntSubContProgListController]);
    function KntSubContProgListController($q, config, Dialog, KnittingDataService, $stateParams, $state, $scope, commonDataService) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.form = {};

        //vm.fabOrder = { FIN_DIA_N_DIA_TYPE: '', DIA_MOU_ID: 23, LK_DIA_TYPE_ID: 327, DIA_MOU_CODE: 'Open', QTY_MOU_ID: 3, QTY_MOU_CODE: 'Kg' };
        //vm.fabOrder = { MC_FAB_PROD_ORD_D_ID: -1, MC_FAB_PROD_ORD_H_ID: -1, FIN_DIA_N_DIA_TYPE: '', DIA_MOU_CODE: 'Open', QTY_MOU_CODE: 'Kg' };

       
        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [GetSupplierList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        
        function GetSupplierList() {
            vm.supplierOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SUP_TRD_NAME_EN",
                dataValueField: "SCM_SUPPLIER_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    $stateParams.pSCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                },
                dataBound: function (e) {
                    if ($stateParams.pSCM_SUPPLIER_ID != null && $stateParams.pSCM_SUPPLIER_ID > 0) {
                        vm.form.SCM_SUPPLIER_ID = $stateParams.pSCM_SUPPLIER_ID;
                    }
                }
            };

            return vm.supplierDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });           
        };

        vm.kntScProgGridOption = {
            height: 400,
            sortable: true,
            pageable: true,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains",
                        startswith: "Starts With",
                        eq: "Is Equal To"
                    }
                }
            },            
            columns: [
                { field: "PRG_RCV_NO", title: "Program#", type: "string", width: "10%" },
                { field: "SC_PRG_DT", title: "Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                { field: "SUP_TRD_NAME_EN", title: "Supplier", width: "10%" },
                {
                    title: "Action",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editProgram(dataItem)'><i class='fa fa-edit'></i></button>";
                    },
                    width: "4%"
                }
            ]
        };

        vm.kntScProgGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "KNT_SC_PRG_RCV_ID",
                    fields: {
                        SC_PRG_DT: { type: "date", editable: false }
                    }
                }
            },
            pageSize: 10,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/knit/KntScRcv/GetScRcvProgramByPartyID?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || $stateParams.pSCM_SUPPLIER_ID || 0);

                    url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        console.log(res);
                        e.success(res);
                    });
                }
            },
            sort: [{ field: 'SC_PRG_DT', dir: 'desc' }]
        });
        
      
        vm.getProgramList = function () {            
            vm.kntScProgGridDataSource.read();

            $state.go('KntSubContProgList', { pSCM_SUPPLIER_ID: (vm.form.SCM_SUPPLIER_ID || 0) }, { notify: false });
        };

        vm.addProgram = function () {
            $state.go('KntSubContProgH.FabColorKP', { pKNT_SC_PRG_RCV_ID: 0, pSCM_SUPPLIER_ID: $stateParams.pSCM_SUPPLIER_ID });
        };

        vm.editProgram = function (dataItem) {
            $state.go('KntSubContProgH.FabColorKP', { pKNT_SC_PRG_RCV_ID: dataItem.KNT_SC_PRG_RCV_ID, pSCM_SUPPLIER_ID: $stateParams.pSCM_SUPPLIER_ID });
        };


    }
})();
//=============== End for Knitting Subcontract Program List =================