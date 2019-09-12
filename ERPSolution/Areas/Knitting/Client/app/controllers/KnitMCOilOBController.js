(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitMCOilOBController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'Dialog', '$filter', '$timeout', KnitMCOilOBController]);
    function KnitMCOilOBController($q, config, KnittingDataService, $stateParams, $state, $scope, commonDataService, Dialog, $filter, $timeout) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.IS_NEXT = 'Y';
        vm.form = { QTY_MOU_ID: 10 };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getCategoryList(), ItemList(), ReceiveItemList(), GetReqSourceList(), GetCompanyList(), GetMOUList(), getCurrencyList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function getCurrencyList() {
            return vm.currencyList = {
                optionLabel: "-- Select Currency --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            commonDataService.getCurrencyList().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "CURR_NAME_EN",
                dataValueField: "RF_CURRENCY_ID"
            };
        };

        vm.getStoreByID = function () {

            if (vm.form.HR_COMPANY_ID > 0) {

                KnittingDataService.getDataByFullUrl('/api/common/StoreListByOfcComCatID?pHR_COMPANY_ID=' + (vm.form.HR_COMPANY_ID || 0) + '&pINV_ITEM_CAT_LST=206').then(function (res) {
                    vm.reqSourceList = new kendo.data.DataSource({
                        data: res
                    });
                });
            }
        }

        vm.calcCostPrice = function () {
            if (vm.form.UNIT_PRICE > 0 && vm.form.LOC_CONV_RATE > 0)
                vm.form.COST_PRICE = (vm.form.OB_RCV_QTY || 0) * (vm.form.UNIT_PRICE || 0) * (vm.form.LOC_CONV_RATE || 0);
            else
                vm.form.COST_PRICE = 0;
        }
        vm.TotalReceiveQty = function () {

        };

        vm.TotalPriceAmount = function () {
            if (vm.form.OB_RCV_QTY > 0 && vm.form.UNIT_PRICE > 0)
                vm.form.COST_PRICE = (vm.form.OB_RCV_QTY || 0) * (vm.form.UNIT_PRICE || 0) * (vm.form.LOC_CONV_RATE || 0);
            else
                vm.form.COST_PRICE = 0;
        };

        $scope.OB_RCV_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.OB_RCV_DT_LNopened = true;
        }

        function GetReqSourceList() {
            return vm.reqSourceList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=206').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };

        function ReceiveItemList() {

            vm.rcvItemGridDataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/McOil/GetOpeningBlanceList';
                        url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize + '&pHR_COMPANY_ID=' + (vm.form.HR_COMPANY_ID || 0) + '&pSCM_STORE_ID=' + (vm.form.SCM_STORE_ID || 0);

                        KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            console.log(res);

                            vm.isExistsOpnBal = 'N';
                            if (res.data.length > 0) {
                                vm.isExistsOpnBal = 'Y';
                                vm.form.OB_RCV_DT = res.data[0]['OB_RCV_DT'];
                            }

                            e.success(res);
                        });
                    }
                },
                pageSize: 100,
                schema: {
                    data: "data",
                    total: "total",
                    model: {
                        id: "KNT_MCN_OIL_OB_ID",
                        fields: {
                            OB_RCV_DT: { type: "date", editable: false }
                        }
                    }
                }
            });


        };

        function getCategoryList() {
            vm.categoryOptions = {
                optionLabel: "-- Select Category --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID",
                dataBound: function (e) {
                    //var ds = e.sender.dataSource.data();
                    //if (ds.length == 1) {
                    //    e.sender.value(ds[0].INV_ITEM_CAT_ID);
                    //    vm.form.INV_ITEM_CAT_ID = ds[0].INV_ITEM_CAT_ID;
                    //    vm.itemDataSource.read();
                    //}                    

                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.form.INV_ITEM_CAT_ID = item.INV_ITEM_CAT_ID;

                    vm.itemDataSource.read();

                }
            };

            return vm.categoryDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/inv/ItemCategory/SelectAll';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            console.log(res);

                            var list = _.filter(res, function (o) {
                                return (o.INV_ITEM_CAT_ID === 206 || o.PARENT_ID === 206)
                            });
                            e.success(list);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function ItemList() {
            vm.itemOptions = {
                optionLabel: "-- Select Item --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID",
                dataBound: function (e) {
                    //var ds = e.sender.dataSource.data();
                    //if (ds.length == 1) {
                    //    e.sender.value(ds[0].INV_ITEM_CAT_ID);
                    //    vm.form.INV_ITEM_CAT_ID = ds[0].INV_ITEM_CAT_ID;
                    //    vm.itemDataSource.read();
                    //}                    

                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                }
            };

            return vm.itemDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (vm.form.INV_ITEM_CAT_ID || 0);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            console.log(res);

                            //var list = _.filter(res, function (o) {
                            //    return (o.INV_ITEM_CAT_ID === 206 || o.PARENT_ID === 206)
                            //});
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };


        vm.loadOpnBalList = function () {
            vm.IS_NEXT = 'Y';
            vm.rcvItemGridDataSource.read();
        }

        vm.addToGrid = function (isValid) {

            if (fnValidateSub() == true) {
                var item = _.filter(vm.DyItemList.data(), function (o) {
                    return o.INV_ITEM_ID === parseInt(vm.form.INV_ITEM_ID)
                })[0];

                vm.form.ITEM_NAME_EN = item.ITEM_NAME_EN;

                var itemB = _.filter(vm.categoryBrandList.data(), function (o) {
                    return o.RF_BRAND_ID === parseInt(vm.form.RF_BRAND_ID)
                })[0];

                vm.form.BRAND_NAME_EN = itemB.BRAND_NAME_EN;

                //vm.form.PACK_BOSTA = "Cartoon";

                var yColorG = _.filter(vm.yarnColorGroupList.data(), function (o) {
                    return o.LOOKUP_DATA_ID === parseInt(vm.form.LK_YRN_COLR_GRP_ID)
                })[0];

                vm.form.LK_YRN_COLR_GRP_NAME = yColorG.LK_DATA_NAME_EN;

                var mouUnit = _.filter(vm.mOUList.data(), function (o) {
                    return o.RF_MOU_ID === parseInt(vm.form.QTY_MOU_ID)
                })[0];
                vm.form.MOU_CODE = mouUnit.MOU_CODE;

                var packUnit = _.filter(vm.PackMOUList.data(), function (o) {
                    return o.RF_MOU_ID === parseInt(vm.form.PACK_MOU_ID)
                })[0];
                vm.form.PACK_MOU_CODE = packUnit.MOU_CODE;

                vm.form.INV_ITEM_CAT_ID = vm.form.INV_ITEM_CAT_ID;
                if (vm.form.uid) {
                    // Update

                    var row = vm.ReceiveItemList.getByUid(vm.form.uid);
                    var count = _.filter(vm.ReceiveItemList.data(), function (o) {
                        return (o.INV_ITEM_ID === vm.form.INV_ITEM_ID && o.uid != vm.form.uid)
                    }).length;

                    if (count <= 1) {
                        row["INV_ITEM_ID"] = vm.form.INV_ITEM_ID;
                        row["ITEM_NAME_EN"] = vm.form.ITEM_NAME_EN;
                        row["RF_BRAND_ID"] = vm.form.RF_BRAND_ID;
                        row["BRAND_NAME_EN"] = vm.form.BRAND_NAME_EN;
                        row["KNT_YRN_RCV_H_ID"] = vm.form.KNT_YRN_RCV_H_ID;
                        row["SCM_PURC_REQ_D_ID"] = vm.form.SCM_PURC_REQ_D_ID;
                        row["QTY_MOU_ID"] = vm.form.QTY_MOU_ID;
                        row["MOU_CODE"] = vm.form.MOU_CODE;
                        row["LK_YRN_COLR_GRP_ID"] = vm.form.LK_YRN_COLR_GRP_ID;
                        row["LK_YRN_COLR_GRP_NAME"] = vm.form.LK_YRN_COLR_GRP_NAME;
                        row["PACK_MOU_CODE"] = vm.form.PACK_MOU_CODE;
                        row["YRN_LOT_NO"] = vm.form.YRN_LOT_NO;
                        row["CONE_QTY"] = vm.form.CONE_QTY;

                        row["PACK_RCV_QTY"] = vm.form.PACK_RCV_QTY;
                        row["QTY_PER_PACK"] = vm.form.QTY_PER_PACK;
                        row["RCV_QTY"] = vm.form.RCV_QTY;
                        row["UNIT_PRICE"] = vm.form.UNIT_PRICE;
                        row["TOTAL_AMT"] = vm.form.TOTAL_AMT;

                        row["SP_NOTE"] = vm.form.SP_NOTE;

                        config.appToastMsg("Item information has been updated successfully!");
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }

                } else {
                    //insert

                    var count = _.filter(vm.ReceiveItemList.data(), function (o) {
                        return (o.INV_ITEM_ID === vm.form.INV_ITEM_ID && o.RF_BRAND_ID === vm.form.RF_BRAND_ID && o.LK_YRN_COLR_GRP_ID === vm.form.LK_YRN_COLR_GRP_ID);
                    }).length;

                    if (count == 0) {

                        var idx = vm.ReceiveItemList.data().length + 1;
                        vm.ReceiveItemList.insert(idx, vm.form);

                        var gview = vm.ReceiveItemList.data();
                        //console.log(gview);
                        config.appToastMsg("Item information has been added successfully!");
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }
                }
            }
        };

        vm.editItemData = function (data) {
            var itmData = angular.copy(data);

            vm.form.uid = itmData.uid;
            vm.form.KNT_MCN_OIL_OB_ID = itmData.KNT_MCN_OIL_OB_ID;
            vm.form.OB_RCV_DT = itmData.OB_RCV_DT;
            vm.form.INV_ITEM_CAT_ID = itmData.INV_ITEM_CAT_ID;

            vm.form.REMARKS = itmData.REMARKS;
            vm.form.IS_FINALIZED = itmData.IS_FINALIZED;
            vm.form.OB_RCV_QTY = itmData.OB_RCV_QTY;
            vm.form.QTY_MOU_ID = itmData.QTY_MOU_ID;
            vm.form.UNIT_PRICE = itmData.UNIT_PRICE;
            vm.form.COST_PRICE = itmData.COST_PRICE;
            vm.form.HR_COMPANY_ID = itmData.HR_COMPANY_ID;
            vm.form.SCM_STORE_ID = itmData.SCM_STORE_ID;
            vm.form.RF_CURRENCY_ID = itmData.RF_CURRENCY_ID;
            vm.form.LOC_CONV_RATE = itmData.LOC_CONV_RATE;
            //vm.form.OB_RCV_DT = itmData.OB_RCV_DT;

            vm.itemDataSource.read();

            $timeout(function () {
                vm.form.INV_ITEM_ID = itmData.INV_ITEM_ID;
            }, 500);
        }

        vm.removeItemData = function (dataOri) {

            Dialog.confirm('Removing "' + dataOri.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var data = angular.copy(dataOri);

                     return KnittingDataService.saveDataByUrl(data, '/McNeedleOpnBal/Delete').then(function (res) {

                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             vm.rcvItemGridDataSource.read();
                             config.appToastMsg(res.data.PMSG);
                         }
                     });

                 });
        }

        vm.filanlizedItemData = function (dataOri) {

            Dialog.confirm('Finalizing "' + dataOri.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var data = angular.copy(dataOri);
                     data.IS_FINALIZED = 'Y';
                     return KnittingDataService.saveDataByUrl(data, '/McNeedleOpnBal/Save').then(function (res) {

                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             vm.rcvItemGridDataSource.read();
                             config.appToastMsg(res.data.PMSG);
                         }
                     });

                 });
        }

        vm.resetItemData = function () {
            var data = angular.copy(vm.form);
            vm.form = { HR_COMPANY_ID: data.HR_COMPANY_ID, SCM_STORE_ID: data.SCM_STORE_ID, OB_RCV_DT: data.OB_RCV_DT, INV_ITEM_CAT_ID: data.INV_ITEM_CAT_ID, INV_ITEM_ID: '', QTY_MOU_ID: 10, RF_CURRENCY_ID: data.RF_CURRENCY_ID };

        };

        vm.reset = function () {
            var data = angular.copy(vm.form);
            vm.form = { HR_COMPANY_ID: data.HR_COMPANY_ID, SCM_STORE_ID: data.SCM_STORE_ID, OB_RCV_DT: data.OB_RCV_DT, INV_ITEM_CAT_ID: data.INV_ITEM_CAT_ID, INV_ITEM_ID: '', QTY_MOU_ID: 10, RF_CURRENCY_ID: data.RF_CURRENCY_ID };
        };



        function GetCompanyList() {

            return vm.companyList = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID",
                select: function (e) {
                    vm.isExistsOpnBal = 'N';
                }
            };
        };


        function GetMOUList() {
            KnittingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {

                vm.mOUList = new kendo.data.DataSource({
                    data: res
                });

                var list = _.filter(res, function (x) { return x.RF_MOU_ID == 1 });
                vm.PackMOUList = new kendo.data.DataSource({
                    data: list
                });
            });
        };


        vm.selectAll = function (v, index) {
            var data = vm.rcvItemGridDataSource.data(); //angular.copy($('#kendoGrid').data("kendoGrid").dataSource.data());
            console.log(data);

            angular.forEach(data, function (val, key) {
                val['IS_FINALIZED_F'] = v;
            });
        };


        vm.gridOptionsItem = {
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
            height: '400px',
            scrollable: true,
            enableRowSelection: true,
            enableSelectAll: true,
            columns: [

                { field: "RF_CURRENCY_ID", hidden: true },
                { field: "KNT_MCN_OIL_OB_ID", hidden: true },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "15%" },
                { field: "STORE_NAME_EN", title: "Store", type: "string", width: "15%" },
                //{ field: "ITEM_CAT_NAME_EN", title: "Category", type: "string", width: "20%" },
                { field: "ITEM_NAME_EN", title: "Item", type: "string", width: "25%" },
                { field: "OB_RCV_DT", title: "Date", template: "#= kendo.toString(kendo.parseDate(OB_RCV_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "8%" },
                { field: "OB_RCV_QTY", title: "Opening Qty.", width: "8%" },
                { field: "MOU_CODE", title: "UoM", type: "string", width: "10%" },
                { field: "UNIT_PRICE", title: "Unit Price", type: "decimal", template: "#= kendo.toString(UNIT_PRICE,'n') #", width: "8%" },
                { field: "COST_PRICE", title: "Cost Price", type: "decimal", template: "#= kendo.toString(COST_PRICE,'n') #", width: "8%" },

                //{ field: "OB_RCV_DT", title: "OB Date", type: "date", template: "#= kendo.toString(kendo.parseDate(OB_RCV_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                {
                    title: "Action",
                    //<a  title="Delete" ng-click="vm.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> 
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" class="btn btn-xs red"><i class="fa fa-remove"></i></a>  <a  title="Edit" ng-click="vm.editItemData(dataItem)" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" class="btn btn-xs green"><i class="fa fa-edit">Edit</i></a> ',
                    width: "8%"
                },
                {
                    title: "Finalize",
                    // <a  title="Edit" ng-click="vm.filanlizedItemData(dataItem)" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" class="btn btn-xs yellow-gold"><i class="fa fa-edit">Finalize</i></a>
                    template: '<input type="checkBox" ng-model="dataItem.IS_FINALIZED_F" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" data-ng-init=' + "dataItem.IS_FINALIZED_F=dataItem.IS_FINALIZED" + ' ng-true-value="' + "'Y'" + '"" ng-false-value="' + "'N'" + '"/> <a  title="Finalize" ng-click="vm.finalizeData(dataItem)" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '"  class="btn btn-xs yellow-gold"><i class="fa fa-save">Finalize</i></a>',
                    headerTemplate: "<input type='checkBox' ng-model='vm.allCheckBox' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-click='vm.selectAll(vm.allCheckBox,0)'/> <button type='button' title='Finalize All' ng-click='vm.finalizeAllData()' class='btn btn-xs yellow-gold'>Finalize All</button>",
                    width: "8%"
                }

            ],
            //editable: true
        };

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                data['OB_RCV_DT'] = $filter('date')(vm.form.OB_RCV_DT, vm.dtFormat);

                return KnittingDataService.saveDataByUrl(data, '/McOil/SaveOB').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        vm.rcvItemGridDataSource.read();
                        vm.resetItemData();
                        config.appToastMsg(res.data.PMSG);
                    }
                });
            }
        };

        vm.finalizeData = function (dataOri) {

            var data = angular.copy(dataOri);
            data["IS_FINALIZED"] = data.IS_FINALIZED_F;

            data['OB_RCV_DT'] = $filter('date')(vm.form.OB_RCV_DT, vm.dtFormat);

            return KnittingDataService.saveDataByUrl(data, '/McOil/SaveOB').then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    vm.rcvItemGridDataSource.read();
                    config.appToastMsg(res.data.PMSG);
                }
            });
        };


        vm.finalizeAllData = function () {

            var submitRcvData = angular.copy(vm.form);
            var vMsg = 'Do you want to finalize all?';


            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                //submitRcvData['IS_FINALIZED'] = pIS_FINALIZED;
                //submitRcvData['TRAN_REF_DT'] = $filter('date')(vm.form.TRAN_REF_DT, vm.dtFormat);

                var rcvData = vm.rcvItemGridDataSource.data();
                console.log(rcvData);


                submitRcvData.MCN_NDL_OPN_BAL_XML = KnittingDataService.xmlStringShort(rcvData.map(function (ob) {
                    return ob;
                }));

                console.log(submitRcvData);

                return KnittingDataService.saveDataByUrl(submitRcvData, '/McNeedleOpnBal/FinalizeAll').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                            vm.rcvItemGridDataSource.read();
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                });

            });
        }

        vm.printOB = function () {
            vm.form.REPORT_CODE = 'RPT-3557';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');

            var params = vm.form;

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

    }
})();

