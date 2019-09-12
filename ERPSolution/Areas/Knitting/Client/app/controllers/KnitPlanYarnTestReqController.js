(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitPlanYarnTestReqController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$modal', '$filter', '$window', '$sessionStorage', KnitPlanYarnTestReqController]);
    function KnitPlanYarnTestReqController($q, config, KnittingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $modal, $filter, $window, $sessionStorage) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};
        console.log(formData);
        vm.form = formData.KNT_YRN_LOT_TEST_H_ID ? formData : { SCM_STORE_ID: '2', ATTN_TO: '152', TEST_WO_BY: cur_user_id, TEST_WO_DT: vm.today, FROM_DATE: vm.today, TO_DATE: vm.today };
        vm.formItem = {};
        vm.form["FROM_DATE"] = vm.today;
        vm.form["TO_DATE"] = vm.today;

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [RequisitionList(), getUserData(), ItemList(), GetReqStoreList(), GetDiaTypeList(), GetStatusList(),
                yarnCountList(), fabricTypeList(), GetYarnColorGroupList(), fiberCompositionList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.formItem["colorlist"] = [{ LK_COLR_GRP_ID: "0", MC_COLOR_ID: "0" }];

        if ($stateParams.pKNT_YRN_LOT_TEST_H_ID >= 0) {
            vm.YarnLotList = $sessionStorage.SessionMessage ? $sessionStorage.SessionMessage : [];
        }
        else
            vm.YarnLotList = [];

        vm.clearSession = function () {
            sessionStorage.clear();
            vm.YarnLotList = [];
        }

        vm.AddColor = function (indx) {
            var idx = indx + 1;
            var item = {
                LK_COLR_GRP_ID: "0",
                MC_COLOR_ID: "0"
            };
            vm.formItem.colorlist.splice(idx, "0", item);
        }

        vm.removeColor = function (data) {
            var index = vm.formItem.colorlist.indexOf(data);
            vm.formItem.colorlist.splice(index, 1);
        }

        function yarnCountList() {
            return vm.YarnCountList = {
                optionLabel: "-- Select Yarn Count --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return KnittingDataService.getDataByFullUrl('/api/Common/YarnCountList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "YR_COUNT_DESC",
                dataValueField: "RF_YRN_CNT_ID"
            };
        };

        function fiberCompositionList() {
            return vm.fibCompListData = {
                optionLabel: "-- Select Fiber Composition --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return KnittingDataService.getDataByFullUrl('/api/Common/FibreCompList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "FIB_COMP_NAME",
                dataValueField: "RF_FIB_COMP_ID"
            };
        };

        function fabricTypeList() {
            return vm.fabricTypeListData = {
                optionLabel: "-- Select Fabric Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return KnittingDataService.getDataByFullUrl('/api/Common/FabricTypeList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "FAB_TYPE_NAME",
                dataValueField: "RF_FAB_TYPE_ID"
            };
        };



        function GetDiaTypeList() {
            return vm.diaTypeList = {
                optionLabel: "-- Select Dia Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(67).then(function (res) {
                                e.success(res);
                            });
                        }
                    } //327
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        $scope.colorNameAuto = function (val) {

            return KnittingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll?pCOLOR_NAME_EN=' + val).then(function (res) {
                return res;
            });

        };

        $scope.onSelectColor = function (data, item) {
            //console.log(item);
            data.MC_COLOR_ID = item.MC_COLOR_ID;
        }

        vm.loadDefaultColor = function (itemC) {
            if (itemC) {
                return KnittingDataService.getDataByFullUrl('/api/knit/KntPlanYarnTest/SelectColorByCGID?pLK_COLR_GRP_ID=' + itemC.LK_COLR_GRP_ID).then(function (res) {
                    if (res) {
                        itemC.MC_COLOR_ID = res.MC_COLOR_ID;
                        itemC.COLOR_NAME_EN = res.COLOR_NAME_EN;
                    }
                });
            }

        };




        vm.openYarnLotItemDtl = function () {

            var FROM_DATE = "";
            if (vm.form.FROM_DATE) {
                var _fdate = $filter('date')(vm.form.FROM_DATE, 'yyyy-MM-ddTHH:mm:ss');
                FROM_DATE = _fdate.split('T')[0];
            }
            var TO_DATE = "";
            if (vm.form.TO_DATE) {
                var _tdate = $filter('date')(vm.form.TO_DATE, 'yyyy-MM-ddTHH:mm:ss');
                TO_DATE = _tdate.split('T')[0];
            }

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'yarnLotItemDtl.html',
                controller: function ($scope, $modalInstance, YarnItemList, YarnLotList) {

                    $scope.today = new Date();
                    $scope.dtFormat = config.appDateFormat;
                    yarnCountListModal();
                    yarnBrandListModal();

                    $scope.FROM_DATE_LNopen = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.FROM_DATE_LNopened = true;
                    }
                    $scope.TO_DATE_LNopen = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.TO_DATE_LNopened = true;
                    }

                    console.log(YarnItemList[0]);
                    $scope.YarnItemList = YarnItemList;



                    function yarnCountListModal() {
                        return $scope["YarnCountList"] = {
                            optionLabel: "-- Select Yarn Count --",
                            filter: "contains",
                            autoBind: true,
                            dataSource: {
                                transport: {
                                    read: function (e) {
                                        return KnittingDataService.getDataByFullUrl('/api/Common/YarnCountList').then(function (res) {
                                            e.success(res);
                                        }, function (err) {
                                            console.log(err);
                                        });
                                    }
                                }
                            },
                            dataTextField: "YR_COUNT_DESC",
                            dataValueField: "RF_YRN_CNT_ID"
                        };
                    };


                    function yarnBrandListModal() {
                        return $scope["categoryBrandList"] = {
                            optionLabel: "-- Select Brand --",
                            filter: "contains",
                            autoBind: true,
                            dataSource: {
                                transport: {
                                    read: function (e) {
                                        KnittingDataService.getDataByFullUrl('/api/purchase/SupplierProfile/BrandListByItemCategory?pINV_ITEM_CAT_ID=2').then(function (res) {
                                            //KnittingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                                            //var list = _.filter(res, { 'INV_ITEM_CAT_ID': 2 });
                                            e.success(res);
                                        }, function (err) {
                                            console.log(err);
                                        });
                                    }
                                }
                            },
                            dataTextField: "BRAND_NAME_EN",
                            dataValueField: "RF_BRAND_ID"
                        };
                    };

                    $scope.loadUnloadList = function (item) {

                        var idx = (YarnLotList.length || 0);
                        if (!item.CHECK_VAL) {
                            var index = YarnLotList.indexOf(item);
                            YarnLotList.splice(index, 1);
                        }
                        else {
                            YarnLotList.splice(idx, "0", item);
                        }

                        console.log(YarnLotList);
                        console.log(item.CHECK_VAL);

                    }

                    $scope.searchItem = function () {
                        var FROM_DT = "";
                        if ($scope.FROM_DATE) {
                            var _fdate = $filter('date')($scope.FROM_DATE, 'yyyy-MM-ddTHH:mm:ss');
                            FROM_DT = _fdate.split('T')[0];
                        }
                        var TO_DT = "";
                        if ($scope.TO_DATE) {
                            var _tdate = $filter('date')($scope.TO_DATE, 'yyyy-MM-ddTHH:mm:ss');
                            TO_DT = _tdate.split('T')[0];
                        }

                        KnittingDataService.getDataByFullUrl('/api/knit/YarnReceive/GetYarnForTestReq?pFROM_DATE=' + (FROM_DT || null) + '&pTO_DATE=' + (TO_DT || null) + '&pIMP_LC_NO=' + ($scope.IMP_LC_NO || "") + '&pYRN_LOT_NO=' + ($scope.YRN_LOT_NO || "") + '&pRF_BRAND_ID=' + $scope.RF_BRAND_ID + '&pRF_YRN_CNT_ID=' + $scope.RF_YRN_CNT_ID).then(function (res) {
                            $scope.YarnItemList = res;
                        });
                    }

                    $scope.cancel = function (data) {
                        $modalInstance.close(YarnLotList);
                    }

                },
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    YarnItemList: function (KnittingDataService) {

                        return KnittingDataService.getDataByFullUrl('/api/knit/YarnReceive/GetYarnForTestReq?pFROM_DATE=' + (FROM_DATE || null) + '&pTO_DATE=' + (TO_DATE || null));
                    },
                    YarnLotList: function (KnittingDataService) {

                        return vm.YarnLotList;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
                vm.YarnLotList = data;

                $sessionStorage.SessionMessage = angular.copy(data);
                //$window.sessionStorage.setItem("yarnLotList", data);

                ////RETRIEVE VALUE
                //var itm = $window.sessionStorage.getItem("yarnLotList");



            }, function (YarnLotList) {
                vm.YarnLotList = [];
                console.log('Modal dismissed at: ' + new Date());
            });

        };

        vm.loadData = function (item) {
            vm.formItem.RF_YRN_CNT_ID = item.RF_YRN_CNT_ID;
            vm.formItem.RF_BRAND_ID = item.RF_BRAND_ID;
            vm.formItem.YARN_ITEM_ID = item.YARN_ITEM_ID;
            vm.formItem.REQ_DOC_NO = item.REQ_DOC_NO;
            vm.formItem.YRN_LOT_NO = item.YRN_LOT_NO;
            vm.formItem.KNT_YRN_LOT_ID = item.KNT_YRN_LOT_ID;
            vm.formItem.RF_FIB_COMP_ID = item.RF_FIB_COMP_ID;


            vm.formItem["colorlist"] = [{ LK_COLR_GRP_ID: "1", MC_COLOR_ID: "1" }];
        }

        //vm.getItemStock = function (e) {
        //    var item = e.sender.dataItem(e.item);
        //    console.log(item);
        //}

        function RequisitionList() {
            return vm.RequisitionItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/KntPlanYarnTest/GetYarnTestDtl?pKNT_YRN_LOT_TEST_H_ID=' + ($stateParams.pKNT_YRN_LOT_TEST_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function GetMOUList() {
            return vm.mOUList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getUserData() {
            return vm.userList = {
                optionLabel: "-- Select User --",
                filter: "startswith",
                autoBind: true,
                valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                          '<span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>',
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getUserDatalist().then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    vm.IsChange = false;
                },

                height: 400,
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID"
            };

            //$("#customers").kendoDropDownList(vm.userList);
        }

        vm.TotalRequiredQty = function () {
            if (vm.formItem.LOOSE_QTY < vm.formItem.QTY_PER_PACK)
                if ((vm.formItem.PACK_RQD_QTY > 0 || vm.formItem.LOOSE_QTY > 0) && vm.formItem.QTY_PER_PACK > 0) {

                    vm.formItem.RQD_YRN_QTY = parseFloat((vm.formItem.PACK_RQD_QTY || 0)) * parseFloat(vm.formItem.QTY_PER_PACK) + parseFloat((vm.formItem.LOOSE_QTY || 0));
                }
                else
                    vm.formItem.RQD_YRN_QTY = 0;
            else
                vm.formItem.LOOSE_QTY = 0;
        };


        vm.form.INV_ITEM_CAT_ID = 2;

        $scope.TEST_WO_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.TEST_WO_DT_LNopened = true;
        }


        $scope.FROM_DATE_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.FROM_DATE_LNopened = true;
        }
        $scope.TO_DATE_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.TO_DATE_LNopened = true;
        }

        vm.categoryBrandList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    KnittingDataService.getDataByFullUrl('/api/purchase/SupplierProfile/BrandListByItemCategory?pINV_ITEM_CAT_ID=2').then(function (res) {
                        //KnittingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                        //var list = _.filter(res, function (o) { return (o.INV_ITEM_CAT_ID === 2 || o.PARENT_ID === 2) });
                        ////var list = _.filter(res, { 'INV_ITEM_CAT_ID': 2 });
                        e.success(res);

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });

        function ItemList() {
            return vm.DyItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        //KnittingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=2').then(function (res) {
                        KnittingDataService.getDataByFullUrl('/api/knit/YarnReceive/GetYarnForTest/null/null').then(function (res) {
                            console.log("AFZAL");
                            console.log(res);
                            console.log("AFZAL");
                            e.success(res);
                        });
                    }
                }
            });
        };

        function GetItemCategoryList() {

            KnittingDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {
                var filter = _.filter(res, { 'ITEM_CAT_LEVEL': 1 });
                vm.itemCategoryList = new kendo.data.DataSource({
                    data: filter
                });
            });
        }

        vm.getItemDataByCategory = function (e) {
            var itemss = e.sender.dataItem(e.item);
            KnittingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (itemss.INV_ITEM_CAT_ID || 2)).then(function (res) {
                vm.DyItemList = new kendo.data.DataSource({
                    data: res
                });
            });

        };

        vm.addToGrid = function (isValid) {

            if (fnValidateSub() == true) {

                var ids = "";
                for (var i = 0; i < vm.formItem.colorlist.length; i++) {
                    if (ids.length > 0)
                        ids = ids + "," + vm.formItem.colorlist[i].LK_COLR_GRP_ID + ":" + vm.formItem.colorlist[i].MC_COLOR_ID;
                    else
                        ids = vm.formItem.colorlist[i].LK_COLR_GRP_ID + ":" + vm.formItem.colorlist[i].MC_COLOR_ID;
                }

                vm.formItem.LK_COLR_GRP_LST = ids;

                var item = _.filter(vm.DyItemList.data(), function (o) {
                    return o.YARN_ITEM_ID === parseInt(vm.formItem.YARN_ITEM_ID)
                })[0];

                vm.formItem.ITEM_NAME_EN = item.ITEM_NAME_EN;

                var itemB = _.filter(vm.categoryBrandList.data(), function (o) {
                    return o.RF_BRAND_ID === parseInt(vm.formItem.RF_BRAND_ID)
                })[0];

                vm.formItem.BRAND_NAME_EN = itemB.BRAND_NAME_EN;

                var data = angular.copy(vm.formItem);

                //vm.formItem.LK_COLR_GRP_LST = !data.LK_COLR_GRP_LST ? '0' : data.LK_COLR_GRP_LST.join(',');

                if (vm.formItem.uid) {
                    // Update

                    var row = vm.RequisitionItemList.getByUid(vm.formItem.uid);
                    var count = _.filter(vm.RequisitionItemList.data(), function (o) {
                        return (o.YARN_ITEM_ID === vm.formItem.YARN_ITEM_ID && o.uid != vm.formItem.uid)
                    }).length;

                    if (count <= 1) {
                        row["YARN_ITEM_ID"] = vm.formItem.YARN_ITEM_ID;
                        row["ITEM_NAME_EN"] = vm.formItem.ITEM_NAME_EN;
                        row["RF_BRAND_ID"] = vm.formItem.RF_BRAND_ID;
                        row["BRAND_NAME_EN"] = vm.formItem.BRAND_NAME_EN;
                        row["KNT_YRN_STR_REQ_H_ID"] = vm.formItem.KNT_YRN_STR_REQ_H_ID;
                        row["KNT_YRN_LOT_ID"] = vm.formItem.KNT_YRN_LOT_ID;
                        row["LK_YRN_COLR_GRP_ID"] = vm.formItem.LK_YRN_COLR_GRP_ID;
                        row["YRN_COLR_GRP"] = vm.formItem.YRN_COLR_GRP;
                        row["YRN_LOT_NO"] = vm.formItem.YRN_LOT_NO;
                        row["YRN_LOT_NO"] = vm.formItem.YRN_LOT_NO;

                        row["RQD_QTY"] = vm.formItem.RQD_QTY;
                        row["CONE_QTY"] = vm.formItem.CONE_QTY;
                        row["LK_COLR_GRP_LST"] = vm.formItem.LK_COLR_GRP_LST;
                        row["HAS_YRN_FAB"] = vm.formItem.HAS_YRN_FAB;

                        row["SP_NOTE"] = vm.formItem.SP_NOTE;

                        vm.formItem = {
                            'YARN_ITEM_ID': '', 'RF_BRAND_ID': '', 'RF_YRN_CNT_ID': '',
                            'RF_FAB_TYPE_ID': vm.formItem.RF_FAB_TYPE_ID,
                            'FIN_GSM': vm.formItem.FIN_GSM,
                            'RF_FIB_COMP_ID': vm.formItem.RF_FIB_COMP_ID,
                            'FIN_DIA': vm.formItem.FIN_DIA,
                            'LK_DIA_TYPE_ID': vm.formItem.LK_DIA_TYPE_ID,
                            'MC_DIA': vm.formItem.MC_DIA,
                            'STITCH_LEN': vm.formItem.STITCH_LEN,
                            'SCM_STORE_ID': vm.formItem.SCM_STORE_ID
                        };
                        vm.formItem["colorlist"] = [{ LK_COLR_GRP_ID: "1", MC_COLOR_ID: "1" }];

                        //config.appToastMsg("Item information has been updated successfully!");
                    }
                    else {
                        //config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }

                } else {
                    //insert

                    var count = _.filter(vm.RequisitionItemList.data(), function (o) {
                        return (o.YARN_ITEM_ID === vm.formItem.YARN_ITEM_ID && o.RF_BRAND_ID === vm.formItem.RF_BRAND_ID && o.LK_YRN_COLR_GRP_ID === vm.formItem.LK_YRN_COLR_GRP_ID)
                    }).length;

                    if (count == 0) {

                        var idx = vm.RequisitionItemList.data().length + 1;
                        vm.RequisitionItemList.insert(idx, vm.formItem);
                        vm.formItem = {
                            'YARN_ITEM_ID': '', 'RF_BRAND_ID': '', 'RF_YRN_CNT_ID': '',
                            'RF_FAB_TYPE_ID': vm.formItem.RF_FAB_TYPE_ID,
                            'FIN_GSM': vm.formItem.FIN_GSM,
                            'RF_FIB_COMP_ID': vm.formItem.RF_FIB_COMP_ID,
                            'FIN_DIA': vm.formItem.FIN_DIA,
                            'LK_DIA_TYPE_ID': vm.formItem.LK_DIA_TYPE_ID,
                            'MC_DIA': vm.formItem.MC_DIA,
                            'STITCH_LEN': vm.formItem.STITCH_LEN,
                            'SCM_STORE_ID': vm.formItem.SCM_STORE_ID,

                        };
                        vm.formItem["colorlist"] = [{ LK_COLR_GRP_ID: "1", MC_COLOR_ID: "1" }];

                        var gview = vm.RequisitionItemList.data();
                    }
                    else {
                        //config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }
                }
            }
            console.log(vm.RequisitionItemList.data());
        };

        vm.editItemData = function (data) {
            var edata = angular.copy(data);
            KnittingDataService.getDataByFullUrl('/api/knit/KntPlanYarnTest/GetYarnTestDtl2?pKNT_YRN_LOT_TEST_D1_ID=' + edata.KNT_YRN_LOT_TEST_D1_ID).then(function (res) {

                vm.formItem = edata;
                if (res.length > 0)
                    vm.formItem["colorlist"] = res;
                else
                    vm.formItem["colorlist"] = [{ LK_COLR_GRP_ID: "1", MC_COLOR_ID: "1" }];
            });

        }

        vm.removeItemData = function (data) {

            if (!data.KNT_YRN_STR_REQ_D_ID || data.KNT_YRN_STR_REQ_D_ID <= 0) {
                vm.RequisitionItemList.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.RequisitionItemList.remove(data);
                 });

        }

        vm.resetItemData = function () {
            vm.formItem = {};
        };

        vm.reset = function () {

            $state.go('YarnTestReq', { pKNT_YRN_STR_REQ_H_ID: 0 });

        };


        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 4;
            var PARENT_ID = 0;
            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;
            var sList = null;
            KnittingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                sList = res;
                console.log(sList);
                if (res.length == 1) {
                    vm.form.RF_ACTN_VIEW = 0;
                    vm.form.RF_ACTN_STATUS_ID = res[0].RF_ACTN_STATUS_ID;
                    vm.form.ACTN_STATUS_NAME = res[0].ACTN_STATUS_NAME;
                    //alert(vm.form.ACTN_STATUS_NAME);
                }
                else {
                    vm.form.RF_ACTN_VIEW = 1;
                }
            });

            return vm.statusList = {
                optionLabel: "-- Select Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                                if (res.length == 1) {
                                    vm.form.RF_ACTN_STATUS_ID = res[0].RF_ACTN_STATUS_ID;
                                    vm.form.ACTN_STATUS_NAME = res[0].ACTN_STATUS_NAME;
                                    //alert(vm.form.RF_ACTN_STATUS_ID);
                                }
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "ACTN_STATUS_NAME",
                dataValueField: "RF_ACTN_STATUS_ID"
            };

        };

        function GetReqTypeList() {

            return vm.reqTypeList = {
                optionLabel: "-- Select Req Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetReqType').then(function (res) {
                                var list = _.filter(res, function (o) { return o.RF_REQ_TYPE_ID == 4 })
                                //KnittingDataService.LookupListData(88).then(function (res) {
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "REQ_TYPE_NAME",
                dataValueField: "RF_REQ_TYPE_ID"
            };
        };

        function GetReqStoreList() {

            return vm.reqStoreList = {
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
                dataValueField: "SCM_STORE_ID"
            };
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
                dataValueField: "HR_COMPANY_ID"
            };
        };

        function GetYarnColorGroupList() {
            return vm.yarnColorGroupList = new kendo.data.DataSource({
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


        vm.printKnitCard = function (item) {
            var url = '/Knitting/Knit/KnitCardTestRpt/#/KnitCardTestRpt?pKNT_JOB_CRD_H_ID=' + item.KNT_JOB_CRD_H_ID;
            var opt = 'location=yes,height=570,width=' + ($window.outerWidth - 300) + ',scrollbars=yes,status=yes';
            $window.open(url, "_blank", opt);
        }


        vm.gridOptionsItem = {
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
            height: '300px',
            scrollable: true,
            columns: [
                { field: "YARN_ITEM_ID", hidden: true },
                { field: "RF_BRAND_ID", hidden: true },
                { field: "LK_DIA_TYPE_ID", hidden: true },
                { field: "KNT_YRN_LOT_ID", hidden: true },
                { field: "RF_YRN_CNT_ID", hidden: true },
                { field: "KNT_JOB_CRD_H_ID", hidden: true },

                { field: "REQ_DOC_NO", hidden: true },
                { field: "MC_COLOR_ID", hidden: true },
                { field: "RF_FAB_TYPE_ID", hidden: true },
                { field: "RF_FIB_COMP_ID", hidden: true },
                { field: "FIN_GSM", hidden: true },
                { field: "FIN_DIA", hidden: true },
                { field: "SCM_STORE_ID", hidden: true },
                { field: "HAS_YRN_FAB", hidden: true },
                { field: "STITCH_LEN", hidden: true },
                { field: "LK_COLR_GRP_LST", hidden: true },
                { field: "COLOR_NAME_EN", hidden: true },

                { field: "ITEM_NAME_EN", title: "Yarn Item", type: "string", width: "15%" },
                { field: "YRN_COLR_GRP", title: "Color Group", type: "string", width: "10%" },
                { field: "BRAND_NAME_EN", title: "Brand", type: "string", width: "10%" },
                { field: "YRN_LOT_NO", title: "Lot#", type: "string", width: "10%" },
                { field: "RQD_QTY", title: "Reqd Qty", type: "string", width: "8%" },
                { field: "KNT_JOB_CRD_H_ID", title: "ID", type: "string", width: "6%" },

                //{ field: "SP_NOTE", title: "Note", width: "10%" },
                //{ field: "SP_NOTE", title: "Note", width: "10%" },
                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" ng-if="!dataItem.KNT_YRN_ISS_D_ID>0" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a  title="Edit" ng-click="vm.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>' +
                        ' <a class="btn btn-xs yellow-gold" ng-click="vm.printKnitCard(dataItem)"><i class="fa fa-file-text"> Print Knit Card</i></a></a>',
                    width: "15%"
                }
            ],
            //editable: true
        };

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                data["IS_FINALIZED"] = 'N';
                data["ATTN_TO"] = !data.ATTN_TO ? '0' : data.ATTN_TO.join(',');

                data["XML_TEST_D"] = KnittingDataService.xmlStringShort(vm.RequisitionItemList.data().map(function (o) {
                    return {
                        KNT_YRN_LOT_TEST_D1_ID: o.KNT_YRN_LOT_TEST_D1_ID == null ? 0 : o.KNT_YRN_LOT_TEST_D1_ID,
                        YARN_ITEM_ID: o.YARN_ITEM_ID,
                        SCM_STORE_ID: o.SCM_STORE_ID,
                        KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID == null ? 0 : o.KNT_YRN_LOT_ID,
                        RF_BRAND_ID: o.RF_BRAND_ID == null ? 0 : o.RF_BRAND_ID,
                        RF_FAB_TYPE_ID: o.RF_FAB_TYPE_ID == null ? 1 : o.RF_FAB_TYPE_ID,
                        RF_FIB_COMP_ID: o.RF_FIB_COMP_ID == null ? 1 : o.RF_FIB_COMP_ID,
                        LK_DIA_TYPE_ID: o.LK_DIA_TYPE_ID == null ? 1 : o.LK_DIA_TYPE_ID,
                        FAB_COLOR_ID: o.MC_COLOR_ID == null ? 1 : o.MC_COLOR_ID,
                        FIN_DIA: o.FIN_DIA == null ? "" : o.FIN_DIA,
                        FIN_GSM: o.FIN_GSM == null ? "" : o.FIN_GSM,
                        STITCH_LEN: o.STITCH_LEN == null ? "" : o.STITCH_LEN,
                        RQD_QTY: o.RQD_QTY == null ? 0 : o.RQD_QTY,
                        CONE_QTY: o.CONE_QTY == null ? 0 : o.CONE_QTY,
                        HAS_YRN_FAB: o.HAS_YRN_FAB == null ? "" : o.HAS_YRN_FAB,
                        LK_COLR_GRP_LST: o.LK_COLR_GRP_LST
                    }
                }));

                var id = vm.form.KNT_YRN_LOT_TEST_H_ID

                return KnittingDataService.saveDataByUrl(data, '/KntPlanYarnTest/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go('KnitPlanYarnTestReq', { pKNT_YRN_LOT_TEST_H_ID: id }, { reload: true });
                        }
                        else {

                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPKNT_YRN_LOT_TEST_H_ID) > 0) {
                                vm.form.KNT_YRN_LOT_TEST_H_ID = res.data.OPKNT_YRN_LOT_TEST_H_ID;
                                $state.go($state.current, { pKNT_YRN_LOT_TEST_H_ID: res.data.OPKNT_YRN_LOT_TEST_H_ID });
                            }
                        }

                    }
                });
            }
        };

        vm.updateAll = function (dataOri) {


            var data = angular.copy(dataOri);

            data["IS_FINALIZED"] = 'N';
            data["ATTN_TO"] = !data.ATTN_TO ? '0' : data.ATTN_TO.join(',');

            data["XML_TEST_D"] = KnittingDataService.xmlStringShort(vm.RequisitionItemList.data().map(function (o) {
                return {
                    KNT_YRN_LOT_TEST_D1_ID: o.KNT_YRN_LOT_TEST_D1_ID == null ? 0 : o.KNT_YRN_LOT_TEST_D1_ID,
                    YARN_ITEM_ID: o.YARN_ITEM_ID,
                    SCM_STORE_ID: o.SCM_STORE_ID,
                    KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID == null ? 0 : o.KNT_YRN_LOT_ID,
                    RF_BRAND_ID: o.RF_BRAND_ID == null ? 0 : o.RF_BRAND_ID,
                    RF_FAB_TYPE_ID: o.RF_FAB_TYPE_ID == null ? 1 : o.RF_FAB_TYPE_ID,
                    RF_FIB_COMP_ID: o.RF_FIB_COMP_ID == null ? 1 : o.RF_FIB_COMP_ID,
                    LK_DIA_TYPE_ID: o.LK_DIA_TYPE_ID == null ? 1 : o.LK_DIA_TYPE_ID,
                    FAB_COLOR_ID: o.MC_COLOR_ID == null ? 1 : o.MC_COLOR_ID,
                    FIN_DIA: o.FIN_DIA == null ? "" : o.FIN_DIA,
                    FIN_GSM: o.FIN_GSM == null ? "" : o.FIN_GSM,
                    STITCH_LEN: o.STITCH_LEN == null ? "" : o.STITCH_LEN,
                    RQD_QTY: o.RQD_QTY == null ? 0 : o.RQD_QTY,
                    CONE_QTY: o.CONE_QTY == null ? 0 : o.CONE_QTY,
                    HAS_YRN_FAB: o.HAS_YRN_FAB == null ? "" : o.HAS_YRN_FAB,
                    LK_COLR_GRP_LST: o.LK_COLR_GRP_LST
                }
            }));

            var id = vm.form.KNT_YRN_LOT_TEST_H_ID

            return KnittingDataService.saveDataByUrl(data, '/KntPlanYarnTest/Update').then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);
                    console.log(res);
                    if (id > 0) {
                        config.appToastMsg(res.data.PMSG);
                        $state.go('KnitPlanYarnTestReq', { pKNT_YRN_LOT_TEST_H_ID: id }, { reload: true });
                    }
                    else {

                        config.appToastMsg(res.data.PMSG);
                        if (parseInt(res.data.OPKNT_YRN_LOT_TEST_H_ID) > 0) {
                            vm.form.KNT_YRN_LOT_TEST_H_ID = res.data.OPKNT_YRN_LOT_TEST_H_ID;
                            $state.go($state.current, { pKNT_YRN_LOT_TEST_H_ID: res.data.OPKNT_YRN_LOT_TEST_H_ID });
                        }
                    }

                }
            });
        };


        vm.completeAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                data["IS_FINALIZED"] = 'Y';
                data["ATTN_TO"] = !data.ATTN_TO ? '0' : data.ATTN_TO.join(',');

                data["XML_TEST_D"] = KnittingDataService.xmlStringShort(vm.RequisitionItemList.data().map(function (o) {
                    return {
                        KNT_YRN_LOT_TEST_D1_ID: o.KNT_YRN_LOT_TEST_D1_ID == null ? 0 : o.KNT_YRN_LOT_TEST_D1_ID,
                        YARN_ITEM_ID: o.YARN_ITEM_ID,
                        SCM_STORE_ID: o.SCM_STORE_ID,
                        KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID == null ? 0 : o.KNT_YRN_LOT_ID,
                        RF_BRAND_ID: o.RF_BRAND_ID == null ? 0 : o.RF_BRAND_ID,
                        RF_FAB_TYPE_ID: o.RF_FAB_TYPE_ID == null ? 1 : o.RF_FAB_TYPE_ID,
                        RF_FIB_COMP_ID: o.RF_FIB_COMP_ID == null ? 1 : o.RF_FIB_COMP_ID,
                        LK_DIA_TYPE_ID: o.LK_DIA_TYPE_ID == null ? 1 : o.LK_DIA_TYPE_ID,
                        FAB_COLOR_ID: o.FAB_COLOR_ID == null ? 1 : o.FAB_COLOR_ID,
                        FIN_DIA: o.FIN_DIA == null ? "" : o.FIN_DIA,
                        FIN_GSM: o.FIN_GSM == null ? "" : o.FIN_GSM,
                        STITCH_LEN: o.STITCH_LEN == null ? "" : o.STITCH_LEN,
                        RQD_QTY: o.RQD_QTY == null ? 0 : o.RQD_QTY,
                        CONE_QTY: o.CONE_QTY == null ? 0 : o.CONE_QTY,
                        HAS_YRN_FAB: o.HAS_YRN_FAB == null ? "" : o.HAS_YRN_FAB,
                        LK_COLR_GRP_LST: o.LK_COLR_GRP_LST
                    }
                }));

                var id = vm.form.KNT_YRN_LOT_TEST_H_ID

                return KnittingDataService.saveDataByUrl(data, '/KntPlanYarnTest/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go('KnitPlanYarnTestReq', { pKNT_YRN_LOT_TEST_H_ID: id }, { reload: true });
                        }
                        else {

                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPKNT_YRN_LOT_TEST_H_ID) > 0) {
                                vm.form.KNT_YRN_LOT_TEST_H_ID = res.data.OPKNT_YRN_LOT_TEST_H_ID;
                                $state.go($state.current, { pKNT_YRN_LOT_TEST_H_ID: res.data.OPKNT_YRN_LOT_TEST_H_ID });
                            }
                        }

                    }
                });
            }
        };

        vm.printRequisition = function () {

            vm.form.REPORT_CODE = 'RPT-3511';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReport');
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
        }

    }


})();

