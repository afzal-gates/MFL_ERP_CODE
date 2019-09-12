//=============== Start for Fabric Color & KP =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('DyeingProgramController', ['$q', 'config', 'Dialog', 'MrcDataService', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', '$modal', DyeingProgramController]);
    function DyeingProgramController($q, config, Dialog, MrcDataService, $stateParams, $state, $scope, $filter, commonDataService, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        var brandListData = [];
        var fabPartListData = [];
        var colGrpListData = [];
        var colorListData = [];

        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> (#: data.ORDER_NO||""#)</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO||"" #)</h5></span>';

        vm.form = {
            MC_FAB_PROD_ORD_H_ID: -1, RF_FAB_PROD_CAT_ID: -1, MC_BYR_ACC_ID: 0, MC_STYLE_H_ID: 0, MC_STYLE_H_EXT_ID: 0, PROD_ORD_DT: $filter('date')(vm.today, vm.dtFormat),
            LK_FAB_QTY_SRC_ID: 433
        };
        vm.fabOrder = {
            MC_FAB_PROD_ORD_D_ID: -1, MC_FAB_PROD_ORD_H_ID: -1, QTY_MOU_ID: 8, QTY_MOU_CODE: 'Yd',
            COLOR: { MC_COLOR_ID: -1, COLOR_NAME_EN: '' }
        };


        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getProdTypeList(), getBuyerAcList(), getBuyerAcGrpList(), getByrAccWiseStyleExtList(), getItemCategoryList(), getItemListByCategory(),
                getMOUList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        function getBuyerAcList() {

            return vm.buyerAcList = {
                optionLabel: "--- Select Buyer A/C ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/mrc/BuyerAcc/SelectAll').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    $stateParams.MC_BYR_ACC_ID = item.MC_BYR_ACC_ID;
                    vm.form.MC_BYR_ACC_ID = item.MC_BYR_ACC_ID;
                    vm.form.MC_STYLE_H_ID = 0;

                    vm.styleExtDataSource.read();

                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        };

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.PROD_ORD_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.PROD_ORD_DT_LNopened = true;
        }
        vm.LAST_REV_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.LAST_REV_DT_LNopened = true;
        }

        $scope.$watchGroup(['vm.form.RF_FAB_PROD_CAT_ID', 'vm.form.MC_BYR_ACC_GRP_ID', 'vm.form.MC_STYLE_H_EXT_ID'], function (newVal, oldVal) {

            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    vm.IS_NEXT = 'N';
                }
            }
        }, true);

        function getProdTypeList() {
            vm.prodTypeList = {
                optionLabel: "-- Select Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/common/GetFabProdCat').then(function (res) {
                                e.success(_.filter(res, function (ob) { return ob.FAB_PROD_CAT_CODE == 'FP02'; }));
                            });
                        }
                    }
                },
                dataTextField: "FAB_PROD_CAT_SNAME",
                dataValueField: "RF_FAB_PROD_CAT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.LK_FAB_QTY_SRC_ID = 433; //((item.FAB_PROD_CAT_CODE == 'FP01') ? 434 : 435);
                }//,
                //dataBound: function (e) {
                //    var ds = e.sender.dataSource.data();
                //    if (ds.length == 1) {
                //        e.sender.value(ds[0].HR_DEPARTMENT_ID);
                //        vm.form.HR_DEPARTMENT_ID = ds[0].HR_DEPARTMENT_ID;
                //        vm.form.CORE_DEPT_ID = ds[0].CORE_DEPT_ID;
                //    }
                //    else if (ds.length > 0 && $stateParams.pHR_DEPARTMENT_ID > 0) {

                //        e.sender.value($stateParams.pHR_DEPARTMENT_ID);
                //        vm.form.HR_DEPARTMENT_ID = $stateParams.pHR_DEPARTMENT_ID;
                //        var selectedDept = _.filter(ds, function (ob) {
                //            if ($stateParams.pHR_DEPARTMENT_ID == ob.HR_DEPARTMENT_ID) {
                //                //alert('Ok');
                //                vm.form.CORE_DEPT_ID = ob.CORE_DEPT_ID;
                //                return ob;
                //            }
                //        });
                //    }
                //}
            };
        }

        vm.onCategoryDataBound = function (e) {
            var ds = e.sender.dataSource.data();
            console.log(ds);

            if (ds.length == 1) {
                e.sender.value(ds[0].RF_FAB_PROD_CAT_ID);
                vm.form.RF_FAB_PROD_CAT_ID = ds[0].RF_FAB_PROD_CAT_ID;
            }
            //else if (ds.length > 0 && $stateParams.pRF_FAB_PROD_CAT_ID > 0) {

            //    e.sender.value($stateParams.pRF_FAB_PROD_CAT_ID);
            //    vm.form.RF_FAB_PROD_CAT_ID = $stateParams.pRF_FAB_PROD_CAT_ID;
            //    var selectedDept = _.filter(ds, function (ob) {
            //        if ($stateParams.pHR_DEPARTMENT_ID == ob.HR_DEPARTMENT_ID) {
            //            //alert('Ok');
            //            vm.form.CORE_DEPT_ID = ob.CORE_DEPT_ID;
            //            return ob;
            //        }
            //    });
            //}
        };



        function getBuyerAcGrpList() {

            return vm.buyerAcGrpList = {
                optionLabel: "--- Select Buyer Group ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    $stateParams.pMC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    vm.form.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    vm.form.MC_STYLE_H_ID = 0;

                    vm.styleExtDataSource.read();
                    //vm.styleFabricDataSource.read();

                },
                //dataBound: function (e) {
                //    if ($stateParams.pMC_BYR_ACC_ID != null && $stateParams.pMC_BYR_ACC_ID > 0) {
                //        vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;

                //        vm.styleExtDataSource.read();
                //        //vm.styleFabricDataSource.read();
                //        //vm.getOrderByBuyerAC($stateParams.pMC_BYR_ACC_ID);
                //    }
                //},
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        }

        function getByrAccWiseStyleExtList() {
            vm.styleExtOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_EXT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
                    vm.form.MC_ORDER_H_ID_LST = item.MC_ORDER_H_ID;

                    vm.getOrderWiseColorList();
                    //vm.styleFabricDataSource.read();
                }//,
                //dataBound: function (e) {
                //    if ($stateParams.pMC_STYLE_H_EXT_ID != null && $stateParams.pMC_STYLE_H_EXT_ID > 0) {
                //        vm.form.MC_STYLE_H_EXT_ID = $stateParams.pMC_STYLE_H_EXT_ID;

                //        vm.styleFabricDataSource.read();
                //    }
                //}
            };

            return vm.styleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Order/GetOrderList?pMC_BYR_ACC_ID=' + ((vm.form.MC_BYR_ACC_ID > 0) ? vm.form.MC_BYR_ACC_ID : 0);
                        //var url = '/api/mrc/BulkFabBk/BulkFabBookingList/' + null + '/' + ((vm.form.MC_STYLE_H_ID > 0) ? vm.form.MC_STYLE_H_ID : null) + '/null?';
                        //url += '&pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : 0) + '&pageNumber=1&pageSize=10';
                        url += MrcDataService.kFilterStr2QueryParam(params.filter);
                        console.log(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return MrcDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.getOrderWiseColorList = function () {
            if (vm.form.MC_ORDER_H_ID_LST != null) {
                return MrcDataService.GetAllOthers('/api/mrc/Order/MultiOrderWiseColorList/' + vm.form.MC_ORDER_H_ID_LST).then(function (res) {
                    vm.orderWiseColorList = res;
                }, function (err) {
                    console.log(err);
                });
            }
            else {
                vm.orderWiseColorList = [];
            }
        };

        function getItemCategoryList() {

            vm.itemCategOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);

                    vm.fabOrder.INV_ITEM_CAT_ID = item.INV_ITEM_CAT_ID;
                    vm.fabOrder.ITEM_CAT_NAME_EN = item.ITEM_CAT_NAME_EN;

                    vm.itemDataSource.read();
                }
            };

            return vm.itemCategDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/inv/ItemCategory/SelectAll';

                        return MrcDataService.getDataByFullUrl(url).then(function (res) {
                            var dataList = _.filter(res, function (o) { return (o.INV_ITEM_CAT_ID === 35 || o.INV_ITEM_CAT_ID === 38) });

                            e.success(dataList);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        }

        function getItemListByCategory() {

            vm.itemOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ITEM_NAME_EN",
                dataValueField: "MC_ORD_TRMS_ITEM_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.fabOrder.ITEM_NAME_EN = item.ITEM_NAME_EN;

                    if (item.MC_ORD_TRMS_ITEM_ID < 0) {
                        var items = { INV_ITEM_CAT_ID: null, MC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID, MC_BYR_ACC_ID: vm.form.MC_BYR_ACC_ID };
                        console.log(items);

                        var modalInstance = $modal.open({
                            animation: true,
                            templateUrl: '/Knitting/Knit/orderTrimsItemDtl',
                            controller: 'DailyTrimsAccesoriesController',
                            controllerAs: 'vmS',
                            size: 'lg',
                            windowClass: 'app-modal-window',
                            resolve: {
                                formData: function (KnittingDataService) {
                                    //item["INV_ITEM_CAT_ID"] = vm.fabOrder.INV_ITEM_CAT_ID;
                                    //item["MC_STYLE_H_EXT_ID"] = vm.form.MC_STYLE_H_EXT_ID;
                                    return items;
                                }
                            }
                        });

                        modalInstance.result.then(function (result) {
                            console.log(result);
                            vm.itemDataSource.read();

                            //if (result.MC_COLOR_ID && result.MC_COLOR_ID > 0) {
                            //    vm.colorListDataSource.read().then(function () {
                            //        vm.fabOrder['FAB_COLOR_ID'] = result.MC_COLOR_ID;
                            //        vm.fabOrder['COLOR_NAME_EN'] = result.COLOR_NAME_EN;
                            //    });
                            //}
                        }, function () {
                            console.log('Modal dismissed at: ' + new Date());
                        });
                    }
                }
            };

            return vm.itemDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/DailyTrimsReceive/GetTrimsItemListByPordOrdID?pINV_ITEM_CAT_ID=' + (vm.fabOrder.INV_ITEM_CAT_ID || -1) + '&pMC_FAB_PROD_ORD_H_ID=' + (vm.form.MC_FAB_PROD_ORD_H_ID || -1);

                        return MrcDataService.getDataByFullUrl(url).then(function (res) {
                            e.success([{ 'ITEM_NAME_EN': '-- New Item --', MC_ORD_TRMS_ITEM_ID: -1 }].concat(res));
                            //e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        }

        function getMOUList() {

            vm.qtyMouOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.fabOrder.QTY_MOU_CODE = item.MOU_CODE;
                },
                dataBound: function (e) {
                    vm.fabOrder['QTY_MOU_ID'] = 8;
                }
            };

            return MrcDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {

                vm.qtyMouDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });
            });

        };

        function getColorList1() {
            return MrcDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll').then(function (res) {
                colorListData = res;
            });
        }


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
                        MrcDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll').then(function (res) {
                            e.success([{ 'COLOR_NAME_EN': '---New Color---', MC_COLOR_ID: -1 }].concat(res));
                        });
                    }
                }
            });
        };

        vm.genItemColorRow = function (data) {

            console.log(vm.orderWiseColorList);

            angular.forEach(vm.orderWiseColorList, function (val, key) {
                var findData = _.filter(vm.fabOrderGridDataSource.data(), function (ob) {
                    return data['MC_ORD_TRMS_ITEM_ID'] == ob['MC_ORD_TRMS_ITEM_ID'] && val['MC_COLOR_ID'] == ob['MC_COLOR_ID'];
                });

                if (findData.length < 1) {
                    var fabOrderCopy = angular.copy(data);
                    fabOrderCopy['MC_COLOR_ID'] = val['MC_COLOR_ID'];
                    fabOrderCopy['COLOR_NAME_EN'] = val['COLOR_NAME_EN'];
                    fabOrderCopy['MC_COLOR'] = { MC_COLOR_ID: val['MC_COLOR_ID'], COLOR_NAME_EN: val['COLOR_NAME_EN'] };

                    vm.fabOrderGridDataSource.insert(0, fabOrderCopy);
                }
            });

            vm.cancelToGrid();

            vm.isAddToGrid = 'Y';
        }

        vm.addRow = function (dataItem) {
            console.log(dataItem);

            var item = {
                MC_TRMS_DY_PROD_ID: 0,
                MC_ORD_TRMS_ITEM_ID: dataItem.MC_ORD_TRMS_ITEM_ID,
                ITEM_NAME_EN: dataItem.ITEM_NAME_EN,
                QTY_MOU_ID: dataItem.QTY_MOU_ID,
                QTY_MOU_CODE: dataItem.QTY_MOU_CODE,
                NET_DY_QTY: 0,
                MC_FAB_PROD_ORD_H_ID: dataItem.MC_FAB_PROD_ORD_H_ID,
                COLOR_REF: dataItem.COLOR_REF,

                MC_COLOR: dataItem.MC_COLOR,

                IS_DISABLE: false
            };

            //delete item['uid'];

            vm.fabOrderGridDataSource.insert(0, item);
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
            var index = vm.fabOrderGridDataSource.indexOf(dataItem);
            console.log(dataItem);

            dataItem['IS_DISABLE'] = !dataItem['IS_DISABLE'];
        };

        vm.removeRow = function (dataItem) {
            vm.fabOrderGridDataSource.remove(dataItem);
        };

        vm.cancelToGrid = function () {
            vm.fabOrder.MC_ORD_TRMS_ITEM_ID = '';
        }

        function ColorDropDownEditor(container, options) {
            if (options.model.IS_DISABLE) {
                $("<span>" + options.model.get(options.field).COLOR_NAME_EN + "</span>").appendTo(container);
                return;
            };

            $('<input data-text-field="COLOR_NAME_EN" data-value-field="MC_COLOR_ID" data-bind="value:' + options.field + '" required/>')
                .appendTo(container)
                .kendoDropDownList({
                    filter: "contains",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: function (e) {
                                //var url = '/api/mrc/ColourMaster/ColourMappDataByBuyerId/' + parseInt(vm.form.MC_STYLE_H_ID) + '?pLK_COL_TYPE_LIST'; //"/ColourMaster/SelectAll?pOption=3010";
                                var url = '/api/mrc/ColourMaster/SelectAll';
                                //var webapi = new kendo.data.transports.webapi({});
                                //var params = webapi.parameterMap(e.data);
                                //if (params.filter) {
                                //    url += '&pCOLOR_NAME_EN=' + params.filter.replace(/'/g, '').split('~')[2];
                                //}
                                return MrcDataService.getDataByFullUrl(url).then(function (res) {
                                    //e.success(res);
                                    e.success([{ MC_COLOR_ID: "-1", COLOR_NAME_EN: '-- New Color --' }].concat(res));
                                });
                            }
                        }//,
                        //serverFiltering: true
                    },
                    change: function (e) {
                        var item = this.dataItem(e.item);
                        console.log(item);

                        ColourChanged(this.dataItem(e.item), options.model);
                    }
                });
        }

        function ColourChanged(data, dataItem) {
            if (data.MC_COLOR_ID) {
                if (data.MC_COLOR_ID < 0) {
                    var modalInstance = $modal.open({
                        animation: true,
                        templateUrl: 'NewColourEntryModal.html',
                        controller: 'ColourMasterModalController',
                        controllerAs: 'vm',
                        size: 'lg',
                        windowClass: 'large-Modal',
                        resolve: {
                            ColourList: function (MrcDataService) {
                                return MrcDataService.selectAllData('ColourMaster');
                            }
                        }
                    });

                    modalInstance.result.then(function (result) {
                        console.log(result);

                        if (result.MC_COLOR_ID && result.MC_COLOR_ID > 0) {

                            dataItem['MC_COLOR_ID'] = result.MC_COLOR_ID;
                            dataItem['COLOR_NAME_EN'] = result.COLOR_NAME_EN;
                            dataItem['COLOR_REF'] = result.COLOR_REF;

                            dataItem['MC_COLOR'] = { MC_COLOR_ID: result.MC_COLOR_ID, COLOR_NAME_EN: result.COLOR_NAME_EN };
                            dataItem['IS_DISABLE'] = false;

                            vm.fabOrderGridDataSource.sync();
                        }
                    }, function () {
                        console.log('Modal dismissed at: ' + new Date());
                    });

                }
                else {
                    dataItem['MC_COLOR_ID'] = data.MC_COLOR_ID;
                    dataItem['COLOR_REF'] = data.COLOR_REF;
                    vm.fabOrderGridDataSource.sync();
                }
            }
            //else {                
            //    dataItem['MC_COLOR_ID'] = item.MC_COLOR_ID;
            //}
        }

        vm.fabOrderGridOption = {
            height: 220,
            sortable: true,
            editable: true,
            columns: [
                { field: "ITEM_CAT_NAME_EN", title: "Category", width: "20%", hidden: true },
                { field: "ITEM_NAME_EN", title: "Item", width: "40%" },
                { field: "COLOR_NAME_EN", title: "Color", width: "20%", hidden: true },
                { field: "MC_COLOR", title: "Color", width: "20%", filterable: false, editor: ColorDropDownEditor, template: "#=MC_COLOR.COLOR_NAME_EN#" },
                { field: "COLOR_REF", title: "Colour Ref#", type: "string", width: "10%" },
                {
                    filterable: false,
                    //footerTemplate: "#=sum#",
                    field: "NET_DY_QTY",
                    title: "Qty",
                    template: function () {
                        return "<ng-form name='frm'> <input type='number' class='form-control' name='NET_DY_QTY' ng-model='dataItem.NET_DY_QTY' min='0' required ng-style='(frm.NET_DY_QTY.$error.required||frm.NET_DY_QTY.$error.min)? vm.errstyle:\"\"' ng-disabled='dataItem.IS_DISABLE' /> </ng-form>";
                    },
                    width: "10%"
                },
                { field: "QTY_MOU_CODE", title: "UoM", type: "string", width: "10%" },

                //{ field: "SC_START_DT", title: "Start Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                //{ field: "SC_END_DT", title: "End Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                {
                    title: "Action",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.addRow(dataItem)' title='Add Row' ><i class='fa fa-plus'></i></button> <button type='button' class='btn btn-xs blue' ng-click='vm.editRow(dataItem)' title='Edit' ><i class='fa fa-edit'></i></button> <button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ng-disabled='dataItem.MC_TRMS_DY_PROD_ID>0?true:false' ><i class='fa fa-remove'></i></button>";
                    },
                    width: "10%"
                }
            ]
        };

        function getFabOrdDataSource() {
            return MrcDataService.getDataByFullUrl('/api/mrc/BulkFabBk/GetTrmsDyProdList?pMC_FAB_PROD_ORD_H_ID=' + ($stateParams.pMC_FAB_PROD_ORD_H_ID || vm.form.MC_FAB_PROD_ORD_H_ID || 0)).then(function (res) {

                vm.fabOrderGridDataSource = new kendo.data.DataSource({
                    data: res,
                    schema: {
                        model: {
                            id: "MC_TRMS_DY_PROD_ID",
                            fields: {
                                ITEM_NAME_EN: { type: "string", editable: false },
                                COLOR_REF: { type: "string", editable: false },
                                QTY_MOU_CODE: { type: "string", editable: false },
                                MC_COLOR: { defaultValue: { MC_COLOR_ID: "", COLOR_NAME_EN: "-- Select Color --" }, editable: true }
                            }
                        }
                    }
                });

            }, function (err) {
                console.log(err);
            });

            //vm.fabOrderGridDataSource = new kendo.data.DataSource({
            //    batch: true,
            //    transport: {
            //        read: function (e) {

            //        }
            //    },
            //    schema: {
            //        model: {
            //            id: "MC_TRMS_DY_PROD_ID",
            //            fields: {
            //                ITEM_SPEC_AUTO: { type: "string", editable: false },
            //                QTY_MOU_CODE: { type: "string", editable: false },
            //                MC_COLOR: { defaultValue: { MC_COLOR_ID: -1, COLOR_NAME_EN: "-- New Color --" }, editable: true },
            //            }
            //        }
            //    }
            //});
        }

        vm.next = function () {
            vm.showSplash = true;
            vm.errors = undefined;

            return MrcDataService.getDataByFullUrl('/api/Knit/FabProdKnitOrder/GetFabProdOrdHdr?&pRF_FAB_PROD_CAT_ID=' + vm.form.RF_FAB_PROD_CAT_ID +
                '&pMC_STYLE_H_EXT_ID=' + vm.form.MC_STYLE_H_EXT_ID).then(function (res) {
                    console.log(res);

                    var formCopy = angular.copy(vm.form);

                    $stateParams.pMC_FAB_PROD_ORD_H_ID = res['MC_FAB_PROD_ORD_H_ID'];
                    vm.form.MC_FAB_PROD_ORD_H_ID = res['MC_FAB_PROD_ORD_H_ID'];

                    if (res['MC_FAB_PROD_ORD_H_ID'] > 0) {
                        vm.form.PROD_ORD_DT = res['PROD_ORD_DT'];
                        vm.form.LAST_REV_DT = res['LAST_REV_DT'];
                    }
                    else {
                        vm.form.PROD_ORD_DT = $filter('date')(vm.today, vm.dtFormat);
                        vm.form.LAST_REV_DT = "";
                    }

                    vm.form.REASON_SHRT = res['REASON_SHRT'];
                    vm.form.SHARE_SHRT = res['SHARE_SHRT'];
                    vm.form.LAST_REV_NO = res['LAST_REV_NO'];


                    if (res['MC_FAB_PROD_ORD_H_ID']) {
                        $state.go('DyeingProg4BulkBookingMnul', { pMC_FAB_PROD_ORD_H_ID: res['MC_FAB_PROD_ORD_H_ID'], pRF_FAB_PROD_CAT_ID: vm.form.RF_FAB_PROD_CAT_ID, pMC_BYR_ACC_GRP_ID: $stateParams.pMC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID }, { notify: false });

                        vm.itemDataSource.read();
                        //vm.fabOrderGridDataSource.read();
                        getFabOrdDataSource();
                        vm.IS_NEXT = 'Y';
                        vm.showSplash = false;
                    }
                    else {
                        vm.errors = [{ error: 'Sorry! At first complete bulk fabric booking then do it.' }];

                        $state.go('DyeingProg4BulkBookingMnul', { pMC_FAB_PROD_ORD_H_ID: 0, pRF_FAB_PROD_CAT_ID: vm.form.RF_FAB_PROD_CAT_ID, pMC_BYR_ACC_GRP_ID: $stateParams.pMC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID }, { notify: false });
                        vm.IS_NEXT = 'N';
                        vm.showSplash = false;
                    }

                });
        }

        vm.newFabOrder = function () {
            vm.IS_NEXT = 'N';
            vm.cancelToGrid();

            $state.go('DyeingProg4BulkBookingMnul', { pMC_FAB_PROD_ORD_H_ID: 0, pRF_FAB_PROD_CAT_ID: vm.form.RF_FAB_PROD_CAT_ID, pMC_BYR_ACC_GRP_ID: $stateParams.pMC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID }, { notify: false });
        }

        vm.batchSave = function () {

            var submitData = angular.copy(vm.form);
            submitData['DTL_XML'] = "";

            var fabOrdGridData = vm.fabOrderGridDataSource.data();

            var fabDtlData = [];

            angular.forEach(fabOrdGridData, function (val, key) {

                var fabData = {
                    MC_TRMS_DY_PROD_ID: val['MC_TRMS_DY_PROD_ID'], MC_FAB_PROD_ORD_H_ID: submitData['MC_FAB_PROD_ORD_H_ID'], MC_ORD_TRMS_ITEM_ID: val['MC_ORD_TRMS_ITEM_ID'],
                    MC_COLOR_ID: val['MC_COLOR_ID'], RQD_DY_QTY: val['RQD_DY_QTY'], REV_DY_QTY: val['REV_DY_QTY'], NET_DY_QTY: val['NET_DY_QTY'], QTY_MOU_ID: val['QTY_MOU_ID']
                };

                fabDtlData.push(fabData);

            });

            submitData.DTL_XML = MrcDataService.xmlStringShort(fabDtlData.map(function (ob) {
                return ob;
            }));


            console.log(submitData);
            console.log(fabOrdGridData);
            //return;

            Dialog.confirm('Do you want save?', 'Are you sure?', ['Yes', 'No'])
                .then(function () {

                    return MrcDataService.saveDataByFullUrl(submitData, '/api/mrc/BulkFabBk/BatchSaveTrmsDyBooking').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                if (res.data.PMC_FAB_PROD_ORD_H_ID_RTN != 0) {
                                    $stateParams.pMC_FAB_PROD_ORD_H_ID = res.data.PMC_FAB_PROD_ORD_H_ID_RTN;
                                    vm.form.MC_FAB_PROD_ORD_H_ID = res.data.PMC_FAB_PROD_ORD_H_ID_RTN;

                                    getFabOrdDataSource();
                                    //vm.fabOrderGridDataSource.read();

                                }

                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };

        vm.printBooking = function () {
            //alert('p');

            vm.form.REPORT_CODE = 'RPT-2010';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReportRDLC');
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
//=============== End for Fabric Color & KP =================

