//=============== Start for Sub-contract Program Header =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('ScYarnRcvChallanHController', ['$q', 'config', 'formData', 'Dialog', 'KnittingDataService', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', ScYarnRcvChallanHController]);
    function ScYarnRcvChallanHController($q, config, formData, Dialog, KnittingDataService, $stateParams, $state, $scope, $filter, commonDataService) {

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

        //vm.yarnReceivState = ($state.current.name === 'KntSubContProgH.YarnReceive') ? true : false;
        //vm.fabColorKPState = ($state.current.name === 'KntSubContProgH.FabColorKP') ? true : false;
        

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

        

        

        vm.kntScProgGridOption = {
            height: 400,
            sortable: true,
            pageable: true,
            selectable: "row",
            refresh: true,
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
                { field: "SC_ORDER_NO", title: "Order#", type: "string", width: "150px" },
                { field: "SC_STYLE_NO", title: "Style#", type: "string", width: "150px" },
                { field: "SC_PRG_DT", title: "Date", format: "{0:dd-MMM-yyyy}", width: "100px" },
                { field: "PRG_RCV_NO", title: "Program#", type: "string", width: "150px" },
                { field: "SUP_TRD_NAME_EN", title: "Supplier", width: "300px" }//,
                //{
                //    title: "Action",
                //    template: function () {
                //        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editProgram(dataItem)'><i class='fa fa-edit'></i></button>";
                //    },
                //    width: "4%"
                //}
            ],
            dataBound: function () {
                var grid = this;
                $.each(grid.tbody.find('tr'), function (idx) {

                    var model = grid.dataItem(this);
                    if (model.KNT_SC_PRG_RCV_ID == $stateParams.pKNT_SC_PRG_RCV_ID && $stateParams.pKNT_SC_PRG_RCV_ID > 0) {
                        vm.form.IS_NEXT = 'Y';
                        vm.form.KNT_SC_PRG_RCV_ID = $stateParams.pKNT_SC_PRG_RCV_ID;

                        $('[data-uid=' + model.uid + ']').addClass('k-state-selected');
                    }

                    //if ($stateParams.MC_ORDER_H_ID == 0) return;
                    //var model = grid.dataItem(this);
                    //if (model.MC_ORDER_H_ID == $stateParams.MC_ORDER_H_ID) {
                    //    $scope.Order = model;
                    //    $state.go('TnAFollowup.TnaTask', { MC_ORDER_H_ID: model.MC_ORDER_H_ID }, { reload: 'TnAFollowup.TnaTask' });
                    //    $('[data-uid=' + model.uid + ']').addClass('k-state-selected');
                    //}
                });
            }
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
                    var url = '/api/knit/KntScRcv/GetScRcvProgramByPartyID?pSCM_SUPPLIER_ID=' + ($stateParams.pSCM_SUPPLIER_ID || 0) + '&pLK_SC_PRG_STATUS_ID=538';

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
            $state.go('ScYarnRcvChallanH.YarnReceive', { pKNT_SC_PRG_RCV_ID: 0, pSCM_SUPPLIER_ID: (vm.form.SCM_SUPPLIER_ID || 0) }, { notify: false });

            vm.kntScProgGridDataSource.read();
        };

        
        vm.onChangeSelectProg = function (dataItem) {
            vm.form = {
                KNT_SC_PRG_RCV_ID: dataItem.KNT_SC_PRG_RCV_ID, SCM_SUPPLIER_ID: dataItem.SCM_SUPPLIER_ID, PRG_RCV_NO: dataItem.PRG_RCV_NO,
                SC_STYLE_NO: dataItem.SC_STYLE_NO, SC_ORDER_NO: dataItem.SC_ORDER_NO
            };

            //alert('sasas');
            console.log(dataItem);

            $state.go('ScYarnRcvChallanH.YarnReceive', { pKNT_SC_PRG_RCV_ID: dataItem.KNT_SC_PRG_RCV_ID, pSCM_SUPPLIER_ID: (dataItem.SCM_SUPPLIER_ID || 0) }, { notify: false }); //{ reload: 'ScYarnRcvChallanH.YarnReceive' }

            return KnittingDataService.getDataByFullUrl('/api/knit/KntScRcv/GetScYrnRcvHdrListByPrgID?pKNT_SC_PRG_RCV_ID=' + (dataItem.KNT_SC_PRG_RCV_ID || 0)).then(function (res) {                
                $scope.challanList = [];
                angular.forEach(res, function (val, key) {
                    val['yarnList'] = [];
                    val['CHALAN_DT'] = $filter('date')(val['CHALAN_DT'], config.appDateFormat);
                    $scope.challanList.push(val);
                });
            });
        }

        
        

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
    angular.module('multitex.knitting').controller('ScYarnRcvChallanDController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', '$filter', 'Dialog', 'commonDataService', 'formData', ScYarnRcvChallanDController]);
    function ScYarnRcvChallanDController($q, config, KnittingDataService, $stateParams, $state, $scope, $filter, Dialog, commonDataService, formData) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.yesNoList = [{ LST_ID: 'Y', LST_NAME: 'Yes' }, { LST_ID: 'N', LST_NAME: 'No' }];
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
            var promise = [getYesNoList(), getPrgOrdList(), getStoreList(), getYarnItemList(), /*getYarnColorGroupList(), getCatgBrandList(),*/ GetMOUList()];
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
        
        function getYesNoList() {
            vm.yesNoOption = {
                autoBind: true,
                dataTextField: "LST_NAME",
                dataValueField: "LST_ID",
                dataBound: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.yrnRcvForm.IS_FINALIZE = 'N';
                }
            };

            return vm.yesNoDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        e.success(vm.yesNoList);
                    }
                }
            });
        };

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
                     
                        var url = '/api/knit/KntScRcv/GetScOrdRefByPrgID?&pKNT_SC_PRG_RCV_ID=' + ($scope.$parent.form.KNT_SC_PRG_RCV_ID || 0);
                        
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
       
        vm.batchSave = function () {

            var submitData = angular.copy($scope.form);
            submitData['KNT_SC_YRN_RCV_H_XML'] = "";
            submitData['KNT_SC_YRN_RCV_D_XML'] = "";
                        
            var mainGridData = $scope.challanList;// $scope.mainYrnRcvGridDataSource.data();

            var yrnRcvHdrIndex = 0;
            var yrnRcvDtlIndex = 0;
            var yrnRcvHdrData = [];
            var yrnRcvDtlData = [];
            
            angular.forEach(mainGridData, function (val, key) {
                if (val['IS_FINALIZE'] != 'Y') {
                    yrnRcvHdrIndex = yrnRcvHdrIndex + 1;
                    var hdrData = {
                        YRN_RCV_INDEX: yrnRcvHdrIndex, KNT_SC_YRN_RCV_H_ID: val['KNT_SC_YRN_RCV_H_ID'], KNT_SC_PRG_RCV_ID: val['KNT_SC_PRG_RCV_ID'],
                        CHALAN_DT: val['CHALAN_DT'], CHALAN_NO: val['CHALAN_NO'], SCM_STORE_ID: val['SCM_STORE_ID'], REMARKS: val['REMARKS'], IS_FINALIZE: val['IS_FINALIZE']
                    };

                    yrnRcvHdrData.push(hdrData);
                    var dtlGridData = val['yarnList']; //val['ChildGrid'].dataSource.data();
                    console.log(dtlGridData);
                    yrnRcvDtlIndex = 0;
                    angular.forEach(dtlGridData, function (val1, key1) {
                        yrnRcvDtlIndex = yrnRcvDtlIndex + 1;
                        yrnRcvDtlData.push({
                            YRN_RCV_INDEX: yrnRcvHdrIndex,
                            KNT_SC_YRN_RCV_D_ID: val1.KNT_SC_YRN_RCV_D_ID,
                            KNT_SC_YRN_RCV_H_ID: val1.KNT_SC_YRN_RCV_H_ID,
                            SCM_SC_WO_REF_ID: val1.SCM_SC_WO_REF_ID,
                            YARN_ITEM_ID: val1.YARN_ITEM_ID,
                            KNT_YRN_LOT_ID: val1.KNT_YRN_LOT_ID,
                            LK_YRN_COLR_GRP_ID: val1.LK_YRN_COLR_GRP_ID,
                            RF_BRAND_ID: val1.RF_BRAND_ID,
                            YRN_LOT_NO: val1.YRN_LOT_NO,
                            CTN_RCV_QTY: val1.CTN_RCV_QTY,
                            QTY_PER_CTN: val1.QTY_PER_CTN,
                            RCV_QTY: val1.RCV_QTY,
                            QTY_MOU_ID: val1.QTY_MOU_ID,
                            PACK_MOU_ID: val1.PACK_MOU_ID
                        });
                    });
                }
            });

            //console.log(yrnRcvHdrData);
            //console.log(yrnRcvDtlData);




            submitData.KNT_SC_YRN_RCV_H_XML = KnittingDataService.xmlStringShort(yrnRcvHdrData.map(function (ob) {
                return ob;
            }));

            submitData.KNT_SC_YRN_RCV_D_XML = KnittingDataService.xmlStringShort(yrnRcvDtlData.map(function (ob) {
                return ob;
            }));
           
            console.log(submitData);
            
            Dialog.confirm('Do you want save?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/KntScRcv/BatchSaveSciChallan').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                $state.go('ScYarnRcvChallanH.YarnReceive', { pKNT_SC_PRG_RCV_ID: $scope.$parent.form.KNT_SC_PRG_RCV_ID, pSCM_SUPPLIER_ID: ($scope.$parent.form.SCM_SUPPLIER_ID || 0) }, { reload: 'ScYarnRcvChallanH.YarnReceive' });

                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });

        };

        vm.finalizeChallan = function (itmChln) {

            Dialog.confirm('After finalize, the challan is lock.</br></br> <b>Do you want to save and finalize?</b>', 'Confirmation', ['Yes', 'No'])
               .then(function () {

                   return KnittingDataService.saveDataByFullUrl(itmChln, '/api/knit/KntScRcv/FinalizeSciChallan').then(function (res) {
                       console.log(res);

                       vm.errors = undefined;
                       if (res.success === false) {
                           vm.errors = [];
                           vm.errors = res.errors;
                       }
                       else {

                           res['data'] = angular.fromJson(res.jsonStr);

                           if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                               $state.go('ScYarnRcvChallanH.YarnReceive', { pKNT_SC_PRG_RCV_ID: $scope.$parent.form.KNT_SC_PRG_RCV_ID, pSCM_SUPPLIER_ID: ($scope.$parent.form.SCM_SUPPLIER_ID || 0) }, { reload: 'ScYarnRcvChallanH.YarnReceive' });

                           }
                           config.appToastMsg(res.data.PMSG);
                       }
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
               });
        }


        vm.cancel = function () {
            $state.go('KntSubContProgH.FabColorKP', { pKNT_SC_PRG_RCV_ID: 0, pSCM_SUPPLIER_ID: $stateParams.pSCM_SUPPLIER_ID });
        };


    }


})();
//=============== End for Yarn Receive =================



