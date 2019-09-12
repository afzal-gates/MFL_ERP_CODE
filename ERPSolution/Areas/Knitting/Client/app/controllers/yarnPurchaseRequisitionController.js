(function () {
    'use strict';
    angular.module('multitex.knitting').controller('YarnPurchaseRequisitionController', ['$q', 'config', 'commonDataService', 'KnittingDataService', '$stateParams', '$state', '$scope', 'formData', '$modal', '$filter', 'Dialog', 'cur_user_id', YarnPurchaseRequisitionController]);
    function YarnPurchaseRequisitionController($q, config, commonDataService, KnittingDataService, $stateParams, $state, $scope, formData, $modal, $filter, Dialog, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.form = formData.MC_FAB_PROD_ORD_H_ID ? formData : { QTY_MOU_ID: 3, PURC_REQ_BY: cur_user_id };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [YarnRequisitionDtlList(), getItemDataByCategory(), GetYarnColorGroupList(), GetSourceTypeList(), GetMOUList(), loadCartList(),
                getUserData(), getCurrencyList(), GetReqSourceList(), getBuyerAcGrpList(), GetBuyerList(), styleListByBuyer(), GetPriorityList(), GetBrandList(),
                GetReqTypeList(), getTnaStatusList(), GetCompanyList(), GetPaymentMethodList(), getFiscalYearData(), loadTnA()
            ];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        if (($stateParams.pMC_FAB_PROD_ORD_H_ID || 0) > 0 || ($stateParams.pSCM_PURC_REQ_H_ID || 0) > 0)
            vm.form = formData;
        else
            vm.form = { MC_FAB_PROD_ORD_H_ID: 0, QTY_MOU_ID: 3, PURC_REQ_BY: cur_user_id };

        vm.formItem = {};

        vm.form.QTY_MOU_ID = 3;
        vm.form.IS_ORDER = true;
        vm.form.IS_REQUISITION = false;
        vm.form.RF_REQ_TYPE_ID = 1;
        vm.form.HR_DEPARTMENT_ID = 46;
        vm.form.PURC_REQ_BY = cur_user_id;
        vm.form.PURC_REQ_TO = 75;

        vm.showRequisition = function () {
            vm.form.IS_ORDER = false;
            vm.form.IS_REQUISITION = true;
        }

        if ($stateParams.pSCM_PURC_REQ_H_ID) {
            vm.showRequisition();
        }

        $scope.SHIP_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.SHIP_DT_LNopened = true;
        }

        $scope.PLAN_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.PLAN_DT_LNopened = true;
        }

        $scope.PURC_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.PURC_REQ_DT_LNopened = true;
        }

        $scope.TARGET_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.TARGET_DT_LNopened = true;
        }


        function loadTnA() {
            if (vm.form.MC_FAB_PROD_ORD_H_ID > 0) {
                KnittingDataService.getDataByFullUrl('/api/knit/KnitYarnReq/SelectTnaByOrderList?pMC_ORDER_H_ID_LST=' + (vm.form.MC_ORDER_H_ID_LST || "")).then(function (res) {

                    vm.tnaList = res;
                    vm.tnaOrderList = [];

                    for (var i = 0; i < res.length; i++) {
                        var item = angular.copy(res[i]);
                        var data = _.filter(vm.tnaOrderList, function (x) { return x.MC_ORDER_H_ID == item.MC_ORDER_H_ID });
                        if (data.length == 0) {
                            vm.tnaOrderList.splice(1, "0", item);
                        }
                    }
                });
            }
        }

        function GetBrandList() {

            return vm.categoryBrandList = {
                optionLabel: "-Select Brand-",
                filter: "Contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "BRAND_NAME_EN",
                dataValueField: "RF_BRAND_ID"
            };
        };

        //vm.categoryBrandList = new kendo.data.DataSource({
        //    transport: {
        //        read: function (e) {
        //            KnittingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
        //                e.success(res);

        //            }, function (err) {
        //                console.log(err);
        //            });
        //        }
        //    }
        //});

        //if (vm.form.MC_FAB_PROD_ORD_H_ID == 0) {
        //    vm.OrderColorGroupList = [{ INV_ITEM_ID: 0 }];
        //}

        function GetBuyerList() {
            vm.buyerList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/mrc/buyer/SelectAll').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };


        function GetPriorityList() {

            return vm.PriorityList = {
                optionLabel: "-Priority-",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(63).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        vm.selectStyleList = function (e) {
            var item = e.sender.dataItem(e.item);
            vm.formItem.MC_BUYER_ID = item.MC_BUYER_ID;
            vm.styleList.read();
        }

        function styleListByBuyer() {

            return vm.styleList = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        if (vm.form.MC_FAB_PROD_ORD_H_ID > 0) {
                            e.success([]);
                        }
                        else {
                            var webapi = new kendo.data.transports.webapi({});
                            var params = webapi.parameterMap(e.data);

                            var url = '/api/mrc/StyleHExt/ByrWiseStyleHExtWithoutBookingList?pMC_BYR_ACC_ID=' + ((vm.formItem.MC_BUYER_ID > 0) ? vm.formItem.MC_BUYER_ID : 0) + '&MC_BYR_ACC_GRP_ID=' + ((vm.formItem.MC_BYR_ACC_GRP_ID > 0) ? vm.formItem.MC_BYR_ACC_GRP_ID : 0);

                            //if (vm.form.STYLE_NO)
                            //    url = url + "&pSTYLE_NO=" + (vm.form.STYLE_NO || "");
                            //else
                            url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                            return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });

                        }
                    }
                }
            });
        };

        vm.selectOrderList = function (e) {
            if (vm.form.MC_FAB_PROD_ORD_H_ID == 0) {
                vm.OrderList = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/mrc/Order/OrderHdrDataList?pMC_STYLE_H_EXT_ID=' + (vm.formItem.MC_STYLE_H_EXT_ID || 0)).then(function (res) {
                                e.success(res);

                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                });
            }
        }


        vm.selectOrderInfo = function (e) {

            var item = e.sender.dataItem(e.item);

            //obj.RQD_QTY = item.TOT_ORD_QTY;

            vm.formItem.TOT_ORD_QTY = item.TOT_ORD_QTY;
            vm.formItem.UNIT_PRICE = item.UNIT_PRICE;
            vm.formItem.SHIP_DT = item.SHIP_DT;
        }

        vm.openBufferDtl = function (obj) {
            console.log(obj);
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'bufferStock.html',
                controller: function ($scope, $modalInstance, colorList) {

                    $scope.OrderColorList = colorList;
                    $scope.cancel = function (data) {
                        $modalInstance.close([]);
                    }
                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    colorList: function (KnittingDataService) {
                        return KnittingDataService.getDataByFullUrl('/api/knit/KnitYarnReq/BufferStockDtlByID?pMC_BUYER_ID=' + obj.MC_BUYER_ID + '&pINV_ITEM_ID=' + obj.INV_ITEM_ID + '&pLK_YRN_COLR_GRP_ID=' + obj.LK_YRN_COLR_GRP_ID);

                    }
                }
            });
        };

        vm.openTnaDtl = function (pMC_ORDER_H_ID_LST) {
            console.log(pMC_ORDER_H_ID_LST);
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'orderTna.html',
                controller: function ($scope, $modalInstance, tnaList) {

                    $scope.gridOptionsTna = {
                        autoBind: true,
                        dataSource: {
                            transport: {
                                read: function (e) {
                                    var list = _.filter(tnaList, function (x) {
                                        return x.MC_TNA_TASK_ID == 44
                                        || x.MC_TNA_TASK_ID == 48
                                        || x.MC_TNA_TASK_ID == 43
                                        || x.MC_TNA_TASK_ID == 60
                                        || x.MC_TNA_TASK_ID == 49
                                        || x.MC_TNA_TASK_ID == 53
                                        || x.MC_TNA_TASK_ID == 50
                                    });
                                    e.success(list);
                                }
                            },
                            pageSize: 100,
                            group: {
                                field: "ORDER_NO"
                            }
                        },
                        height: 1000,
                        sortable: true,
                        groupable: true,
                        columns: [
                            { field: "ORDER_NO", title: "Order No", type: "string", width: "20%" },
                            { field: "TA_TASK_NAME_EN", title: "T&A Task Name", type: "string", width: "30%" },
                            { field: "PLAN_START_DT", title: "Plan Start Date", type: "date", format: "{0:yyyy-MM-dd}", width: "20%" },
                            { field: "DAYS_TO_GO", title: "Days To Go", type: "string", width: "20%" },
                        ]
                    };
                    $scope.OrderTnaList = tnaList;
                    $scope.cancel = function (data) {
                        $modalInstance.close([]);
                    }
                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    tnaList: function (KnittingDataService) {
                        return KnittingDataService.getDataByFullUrl('/api/knit/KnitYarnReq/SelectTnaByOrderList?pMC_ORDER_H_ID_LST=' + pMC_ORDER_H_ID_LST);

                    }
                }
            });
        };

        vm.openMapUI = function (MC_FAB_PROD_ORD_H_ID, pMC_BUYER_ID) {
            console.log(pMC_BUYER_ID);
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'colorYarnMap.html',
                controller: function ($scope, $modalInstance, colorList) {

                    getItemData();
                    GetColorGroupList();

                    $scope.OrderColorList = colorList;
                    $scope.OrderColorGroupList = [];
                    $scope.cancel = function (data) {
                        $modalInstance.close($scope.OrderColorGroupList);
                    }

                    function getItemData() {

                        return $scope.ItemList = {
                            optionLabel: "-- Select Yarn Item--",
                            filter: "contains",
                            autoBind: true,
                            dataSource: {
                                transport: {
                                    read: function (e) {
                                        KnittingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=2').then(function (res) {
                                            e.success(res);
                                        });
                                    }
                                }
                            },
                            dataTextField: "ITEM_NAME_EN",
                            dataValueField: "INV_ITEM_ID"
                        };
                    };

                    function GetColorGroupList() {
                        return $scope.yarnColorGroupList = new kendo.data.DataSource({
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

                    $scope.removeYarn = function (index, item) {

                        var idx = $scope.OrderColorList.indexOf(item);
                        $scope.OrderColorList.splice(idx, 1);
                    }


                    $scope.copyYarn = function (index, item) {
                        var data = angular.copy(item);
                        var idx = $scope.OrderColorList.length;
                        $scope.OrderColorList.splice(index, "0", data);
                    }

                    $scope.arrangeYarnList = function () {

                        var dataitem = {};

                        var ColorGroupList = [];
                        for (var i = 0; i < $scope.OrderColorList.length; i++) {
                            var item = angular.copy($scope.OrderColorList[i]);
                            var data = _.filter(ColorGroupList, function (x) { return x.INV_ITEM_ID == item.INV_ITEM_ID && x.LK_YRN_COLR_GRP_ID == item.LK_YRN_COLR_GRP_ID });
                            if (data.length > 0) {
                                var idx = ColorGroupList.indexOf(data[0]);
                                ColorGroupList.splice(idx, 1);

                                data[0].RQD_QTY = data[0].RQD_QTY + item.RQD_QTY;
                                //data[0].ORDER_NO = data[0].ORDER_NO + "," + item.ORDER_NO;
                                ColorGroupList.splice(idx, "0", data[0]);
                            }
                            else {
                                ColorGroupList.splice(1, "0", item);
                            }
                        }

                        var dataList = ColorGroupList.map(function (o) {
                            return {
                                MC_BUYER_ID: o.MC_BUYER_ID == null ? 0 : o.MC_BUYER_ID,
                                INV_ITEM_ID: o.INV_ITEM_ID == null ? 0 : o.INV_ITEM_ID,
                                LK_YRN_COLR_GRP_ID: o.LK_YRN_COLR_GRP_ID == null ? 0 : o.LK_YRN_COLR_GRP_ID,
                                MC_FAB_PROD_ORD_H_ID: o.MC_FAB_PROD_ORD_H_ID,
                                MC_STYLE_H_EXT_ID: o.MC_STYLE_H_EXT_ID,
                                RQD_QTY: o.RQD_QTY == null ? 0 : o.RQD_QTY,
                                MKT_RATE: o.MKT_RATE == null ? 0 : o.MKT_RATE,
                                ORDER_NO: o.ORDER_NO
                            }
                        });
                        dataitem["dataList"] = dataList;

                        KnittingDataService.saveDataByUrl(dataitem, '/KnitYarnReq/BufferStockByID').then(function (res) {
                            $scope.OrderColorGroupList = res;
                        });
                    }
                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    colorList: function (KnittingDataService) {
                        return KnittingDataService.getDataByFullUrl('/api/knit/KnitYarnReq/GetColorWiseFabOrdListByID?pMC_FAB_PROD_ORD_H_ID=' + (MC_FAB_PROD_ORD_H_ID || 0));
                    }
                }
            });
            modalInstance.result.then(function (dataItem) {
                console.log(dataItem);
                vm.OrderColorGroupList = [];
                vm.OrderColorGroupList = dataItem;
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });

        };

        function loadCartList() {
            //if (vm.form.MC_FAB_PROD_ORD_H_ID > 0) {
            //    KnittingDataService.getDataByFullUrl('/api/knit/KnitYarnReq/GetColorWiseFabOrdListByID?pMC_FAB_PROD_ORD_H_ID=' + vm.form.MC_FAB_PROD_ORD_H_ID).then(function (res) {
            //        vm.OrderColorGroupList = res;
            //    });
            //}
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

        function GetReqSourceList() {
            return vm.reqSourceList = {
                optionLabel: "-- Select Req Source --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetReqSRC').then(function (res) {
                                //KnittingDataService.LookupListData(92).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "REQ_SRC_NAME",
                dataValueField: "RF_REQ_SRC_ID"
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
                                //KnittingDataService.LookupListData(88).then(function (res) {
                                var lst = _.filter(res, function (x) { return x.RF_REQ_TYPE_ID == 1 });
                                e.success(lst);
                            });
                        }
                    }
                },
                dataTextField: "REQ_TYPE_NAME",
                dataValueField: "RF_REQ_TYPE_ID"
            };
        };

        function getTnaStatusList() {
            return vm.TnaStatusList = {
                optionLabel: "-- Select TNA Status --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/mrc/TnaTaskStatus/SelectApprovRejectStatus?pMC_TNA_TASK_ID=46').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "EVENT_NAME",
                dataValueField: "MC_TNA_TASK_STATUS_ID"
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

        vm.departmentList = {
            optionLabel: "--Select Department--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/Hr/Admin/HrDesignation/DepartmentListData').then(function (res) {

                            e.success(res);
                        });
                    }
                }
            },
            dataTextField: "DEPARTMENT_NAME_EN",
            dataValueField: "HR_DEPARTMENT_ID"
        };

        function GetPaymentMethodList() {

            return vm.paymentMethodList = {
                optionLabel: "-- Select Payment Method --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetPayMethod').then(function (res) {
                                //KnittingDataService.getDataByFullUrl('api/common/GetPayMethod').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "PAY_MTHD_NAME",
                dataValueField: "RF_PAY_MTHD_ID"
            };
        };

        function getFiscalYearData() {
            return vm.FiscalYearData = {
                optionLabel: "-- Select Fiscal Year --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/hr/hrleavetrans/FiscalYearDataAll').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "FY_NAME_EN",
                dataValueField: "RF_FISCAL_YEAR_ID"
            };
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

        function getItemDataByCategory() {
            KnittingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=2').then(function (res) {
                vm.ItemList = new kendo.data.DataSource({
                    data: res
                });
            });

        };

        function GetYarnColorGroupList() {
            //var item = {
            //    INV_ITEM_ID: "",
            //    LK_YRN_COLR_GRP_ID: ""
            //};

            //vm.OrderColorGroupList = [item];
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


        function GetSourceTypeList() {

            return vm.sourceTypeList = {
                optionLabel: "--Source--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(84).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        
        function getBuyerAcGrpList() {

            return vm.buyerAcGrpList = {
                optionLabel: "--- Select Buyer Group ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (res) {
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

                    vm.form.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;

                    vm.styleList.read();
                },
                dataTextField: "BYR_ACC_GRP_NAME_EN",
                dataValueField: "MC_BYR_ACC_GRP_ID"
            };
        }

        function getBuyerAccListData() {
            return vm.buyerAccList = {
                optionLabel: "-- Select Buyer Account--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        };

        vm.styleListByBuyerACGrp = function (MC_BYR_ACC_GRP_ID) {

            if (MC_BYR_ACC_GRP_ID.length == 0) {
                vm.styleList = new kendo.data.DataSource({
                    data: []
                })
                return;
            }
            else {
                alert("");
                vm.styleList.read();
            }
        };


        vm.styleListByBuyerAC = function (MC_BYR_ACC_ID) {

            if (MC_BYR_ACC_ID.length == 0) {
                vm.styleList = new kendo.data.DataSource({
                    data: []
                })
                return;
            }

            KnittingDataService.getDataByFullUrl('/api/mrc/StyleH/BuyerAccWiseStyleListData/' + MC_BYR_ACC_ID).then(function (res) {
                console.log(res);
                vm.styleList = new kendo.data.DataSource({
                    data: res
                })
            })
        };

        //function styleListByBuyer(MC_BYR_ACC_ID) {

        //    if (!MC_BYR_ACC_ID) {
        //        vm.styleList = new kendo.data.DataSource({
        //            data: []
        //        })
        //        return;
        //    }

        //    KnittingDataService.getDataByFullUrl('/api/mrc/StyleHExt/BuyerWiseStyleHExtList/' + MC_BYR_ACC_ID + '/0').then(function (res) {
        //        vm.styleList = new kendo.data.DataSource({
        //            data: res
        //        })
        //    })
        //};

        function GetProductionTypeList() {
            return vm.productionTypeList = {
                optionLabel: "--Type--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(89).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        vm.showFabProdOrderList = function () {
            vm.gridOptions.dataSource.read();
        }

        vm.removeItem = function (data) {
            if (!data.SCM_YRN_PURC_REQ_D_ID || data.SCM_YRN_PURC_REQ_D_ID <= 0) {
                vm.yarnRequisitionDtlList.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.SCM_YRN_PURC_REQ_D_ID + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.yarnRequisitionDtlList.remove(data);
                 });
        }


        vm.addToGrid = function (dataItem) {
            var item = angular.copy(dataItem);
            if (fnValidateSub() == true) {
                if (!vm.OrderColorGroupList) {
                    vm.OrderColorGroupList = [];
                }

                var order = _.filter(vm.OrderList.data(), function (o) {
                    return o.MC_ORDER_H_ID === parseInt(item.MC_ORDER_H_ID)
                })[0];
                if (order)
                    item.ORDER_NO = order.ORDER_NO;
                item.RF_REQ_SRC_ID = 2;
                console.log(item);
                var data = _.filter(vm.OrderColorGroupList, function (x) { return x.MC_STYLE_H_EXT_ID == item.MC_STYLE_H_EXT_ID && x.INV_ITEM_ID == item.INV_ITEM_ID && x.LK_YRN_COLR_GRP_ID == item.LK_YRN_COLR_GRP_ID });
                console.log(data);
                if (data.length > 0) {
                    config.appToastMsg("MTEX-005 Duplicate");
                    //var idx = vm.OrderColorGroupList.indexOf(data);
                    //vm.OrderColorGroupList.splice(idx, 1);
                    //vm.OrderColorGroupList.splice(idx, "0", data);
                }
                else {
                    vm.OrderColorGroupList.splice(vm.OrderColorGroupList.length + 1, "0", item);
                }

            }
        };

        function YarnRequisitionDtlList() {
            if ($stateParams.pSCM_PURC_REQ_H_ID) {
                vm.yarnRequisitionDtlList = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/knit/KnitYarnReq/YarnReqDtlByID?pSCM_PURC_REQ_H_ID=' + $stateParams.pSCM_PURC_REQ_H_ID).then(function (res) {
                                e.success(res);
                            });
                        }
                    },
                    schema: {
                        model: {
                            fields: {
                                ITEM_NAME_EN: { type: "string" },
                                RQD_QTY: { type: "number" }
                            }

                        }
                    },
                    group: {
                        field: "ITEM_NAME_EN", title: "Yarn", aggregates: [{ field: "RQD_QTY", aggregate: "sum" }]
                    },
                    aggregate: [{ field: "RQD_QTY", aggregate: "sum" }]
                });
            }
            else {
                vm.yarnRequisitionDtlList = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/knit/KnitYarnReq/CartDtlByID').then(function (res) {
                                e.success(res);
                            });
                        }
                    },
                    schema: {
                        model: {
                            fields: {
                                ITEM_NAME_EN: { type: "string" },
                                RQD_QTY: { type: "number" }
                            }

                        }
                    },
                    group: {
                        field: "ITEM_NAME_EN", title: "Yarn", aggregates: [{ field: "RQD_QTY", aggregate: "sum" }]
                    },
                    aggregate: [{ field: "RQD_QTY", aggregate: "sum" }]
                });
            }
        };

        vm.removeReqItem = function (dataItem) {

            Dialog.confirm('Removing Order: "' + dataItem.ORDER_DTL + ' & Item: ' + dataItem.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {

                     return KnittingDataService.saveDataByUrl(dataItem, '/KnitYarnReq/Delete').then(function (res) {

                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             config.appToastMsg(res.data.PMSG);
                             vm.yarnRequisitionDtlList.remove(dataItem);
                         }
                     });
                 });
        }

        vm.gridOptions = {
            height: 400,
            sortable: true,
            groupable: true,
            columns: [
                { field: "SCM_PURC_REQ_D_ID", hidden: true },
                { field: "SCM_PURC_REQ_D_TMP_ID", hidden: true },
                { field: "INV_ITEM_ID", hidden: true },
                { field: "MC_STYLE_H_EXT_ID", hidden: true },
                { field: "LK_YRN_COLR_GRP_ID", hidden: true },
                { field: "SCM_PURC_REQ_H_ID", hidden: true },
                { field: "MC_FAB_PROD_ORD_H_ID", hidden: true },
                { field: "SCM_PURC_REQ_ORD_ID", hidden: true },
                { field: "RF_REQ_SRC_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "MKT_RATE", hidden: true },
                { field: "LK_PRIORITY_ID", hidden: true },
                { field: "LK_LOC_SRC_TYPE_ID", hidden: true },
                { field: "RF_BRAND_ID", hidden: true },
                { field: "LOT_REF_NO", hidden: true },
                { field: "SHADE_REF_NO", hidden: true },
                { field: "TARGET_DT", hidden: true },
                { field: "SP_NOTE", hidden: true },
                { field: "ORDER_DTL", title: "Order Ref.", type: "string", width: "15%" },
                //{ field: "BUYER_NAME_EN", title: "Buyer", type: "string", width: "7%" },
                //{ field: "STYLE_NO", title: "Style #", type: "string", width: "7%" },
                //{ field: "ORDER_NO", title: "Order #", type: "string", width: "7%" },
                { field: "ITEM_NAME_EN", title: "Yarn Info", hidden: true, type: "string", width: "15%" },
                { field: "LK_YRN_COLR_GRP_NAME", title: "Color Group", type: "string", width: "7%" },
                { field: "RQD_QTY", title: "Reqd. Qty", type: "string", width: "7%", aggregates: ["sum"], groupFooterTemplate: "Total: #=sum#", footerTemplate: "Total Weight: #=sum#", },
                { field: "BUFR_ALOC_QTY", title: "Available Buffer Qty", type: "string", width: "7%" },
                { field: "CAL_RQD_QTY", title: "LC Pipeline Qty", type: "string", width: "7%" },
                { field: "CONS_DOZ", title: "Alloc. from Buffer Qty", type: "string", width: "7%" },
                { field: "MKT_RATE", title: "Plan Qty for Purchase", type: "string", width: "7%" },
                { field: "NXT_BUFR_QTY", title: "Alloc. this Order By[lot#]", type: "string", width: "7%" },
                { field: "SOURCE_NAME", title: "Source", type: "string", width: "7%" },
                //{ field: "TOT_ORD_QTY", title: "Brand", type: "string", width: "7%" },
                //{ field: "SHIP_DT_STR", title: "Lot# Ref", type: "date", format: "{0:yyyy-MM-dd}", width: "7%" },
                //{ field: "SHIP_DT", title: "Note", width: "7%" },
                {
                    title: "Action",
                    template: function () {
                        return "</a><a ng-click='vm.removeReqItem(dataItem)' class='btn btn-xs red'><i class='fa fa-remove'> Remove</i></a></a>";
                    },
                    width: "5%"
                }
            ]
        };


        vm.printRequisition = function () {

            vm.form.REPORT_CODE = 'RPT-3562';
            vm.form.SCM_PURC_REQ_H_ID = vm.form.SCM_PURC_REQ_H_ID;

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

        function convertDate(old) {
            var _isudate = $filter('date')(old, 'yyyy-MM-ddTHH:mm:ss');
            return _isudate;
        }

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);

                data['INV_ITEM_CAT_ID'] = 2;
                data['IS_ORDER_WISE'] = 'Y';

                data['PURC_REQ_DT'] = convertDate(data.PURC_REQ_DT);
                data['REQ_ATTN_MAIL'] = '1';

                data['LK_PRIORITY_ID'] = '1';
                data['IS_ALL_BYR'] = 'N';
                data['MC_BUYER_LST'] = vm.form.MC_BUYER_ID;

                data["RequisitionItemList"] = vm.yarnRequisitionDtlList.data().map(function (o) {
                    return {
                        SCM_PURC_REQ_D_TMP_ID: o.SCM_PURC_REQ_D_TMP_ID == null ? 0 : o.SCM_PURC_REQ_D_TMP_ID,
                        SCM_PURC_REQ_D_ID: o.SCM_PURC_REQ_D_ID == null ? 0 : o.SCM_PURC_REQ_D_ID,
                        SCM_PURC_REQ_H_ID: o.SCM_PURC_REQ_H_ID == null ? 0 : o.SCM_PURC_REQ_H_ID,
                        SCM_PURC_REQ_ORD_ID: o.SCM_PURC_REQ_ORD_ID == null ? 0 : o.SCM_PURC_REQ_ORD_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID,
                        LK_YRN_COLR_GRP_ID: o.LK_YRN_COLR_GRP_ID,
                        MC_FAB_PROD_ORD_H_ID: o.MC_FAB_PROD_ORD_H_ID == 0 ? null : o.MC_FAB_PROD_ORD_H_ID,
                        MC_STYLE_H_EXT_ID: o.MC_STYLE_H_EXT_ID == 0 ? null : o.MC_STYLE_H_EXT_ID,
                        RF_REQ_SRC_ID: o.RF_REQ_SRC_ID == 0 ? null : o.RF_REQ_SRC_ID,
                        LK_PRIORITY_ID: o.LK_PRIORITY_ID == 0 ? null : o.LK_PRIORITY_ID,
                        BUFR_ALOC_QTY: o.BUFR_ALOC_QTY == null ? 0 : o.BUFR_ALOC_QTY,
                        CAL_RQD_QTY: o.PLAN_RQD_QTY == null ? 0 : o.PLAN_RQD_QTY,
                        RQD_QTY: o.RQD_QTY == null ? 0 : o.RQD_QTY,
                        CONS_DOZ: o.CONS_DOZ == null ? 0 : o.CONS_DOZ,
                        MKT_RATE: o.MKT_RATE == null ? 0 : o.MKT_RATE,
                        TARGET_DT: o.TARGET_DT,
                        RF_CURRENCY_ID: o.RF_CURRENCY_ID == 0 ? 2 : o.RF_CURRENCY_ID,
                        ORD_ADV_ALOC_QTY: o.ORD_ADV_ALOC_QTY == null ? 0 : o.ORD_ADV_ALOC_QTY,
                        NXT_BUFR_QTY: o.NXT_BUFR_QTY == null ? 0 : o.NXT_BUFR_QTY,
                        QTY_MOU_ID: 3,
                        //QTY_MOU_ID: o.QTY_MOU_ID == 0 ? 3 : o.QTY_MOU_ID,
                        LK_LOC_SRC_TYPE_ID: o.LK_LOC_SRC_TYPE_ID == null ? 1 : o.LK_LOC_SRC_TYPE_ID,
                        RF_BRAND_ID: o.RF_BRAND_ID == 0 ? null : o.RF_BRAND_ID,
                        LOT_REF_NO: o.LOT_REF_NO == null ? "" : o.LOT_REF_NO,
                        SP_NOTE: o.SP_NOTE
                    }
                });

                var id = vm.form.SCM_PURC_REQ_H_ID

                return KnittingDataService.saveDataByUrl(data, '/KnitYarnReq/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        config.appToastMsg(res.data.PMSG);
                        if (res.data.OPSCM_PURC_REQ_H_ID)
                            $state.go($state.current, { pSCM_PURC_REQ_H_ID: res.data.OPSCM_PURC_REQ_H_ID }, { reload: true });
                    }
                });
            }
        };


        vm.addCartAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                data['IS_ORDER_WISE'] = 'Y';
                console.log(vm.OrderColorGroupList);
                data["RequisitionItemList"] = vm.OrderColorGroupList.map(function (o) {
                    return {
                        SCM_PURC_REQ_D_ID: o.SCM_PURC_REQ_D_ID == null ? 0 : o.SCM_PURC_REQ_D_ID,
                        SCM_PURC_REQ_D_TMP_ID: o.SCM_PURC_REQ_D_TMP_ID == null ? 0 : o.SCM_PURC_REQ_D_TMP_ID,
                        SCM_PURC_REQ_H_ID: o.SCM_PURC_REQ_H_ID == null ? 0 : o.SCM_PURC_REQ_H_ID,
                        SCM_PURC_REQ_ORD_ID: o.SCM_PURC_REQ_ORD_ID == null ? 0 : o.SCM_PURC_REQ_ORD_ID,

                        INV_ITEM_ID: o.INV_ITEM_ID,
                        LK_YRN_COLR_GRP_ID: o.LK_YRN_COLR_GRP_ID,
                        MC_FAB_PROD_ORD_H_ID: o.MC_FAB_PROD_ORD_H_ID == 0 ? null : o.MC_FAB_PROD_ORD_H_ID,
                        MC_STYLE_H_EXT_ID: o.MC_STYLE_H_EXT_ID == 0 ? null : o.MC_STYLE_H_EXT_ID,
                        RF_REQ_SRC_ID: o.RF_REQ_SRC_ID == null ? 3 : o.RF_REQ_SRC_ID == 0 ? 3 : o.RF_REQ_SRC_ID,
                        LK_PRIORITY_ID: o.LK_PRIORITY_ID == 0 ? null : o.LK_PRIORITY_ID,
                        BUFR_ALOC_QTY: o.BUFR_ALOC_QTY == null ? 0 : o.BUFR_ALOC_QTY,
                        CAL_RQD_QTY: o.PLAN_RQD_QTY == null ? 0 : o.PLAN_RQD_QTY,
                        RQD_QTY: o.RQD_QTY == null ? 0 : o.RQD_QTY,
                        CONS_DOZ: o.CONS_DOZ == null ? 0 : o.CONS_DOZ,
                        MKT_RATE: o.MKT_RATE == null ? 0 : o.MKT_RATE,
                        TARGET_DT: o.TARGET_DT,
                        RF_CURRENCY_ID: o.RF_CURRENCY_ID == 0 ? 2 : o.RF_CURRENCY_ID,
                        ORD_ADV_ALOC_QTY: o.ORD_ADV_ALOC_QTY == null ? 0 : o.ORD_ADV_ALOC_QTY,
                        NXT_BUFR_QTY: o.NXT_BUFR_QTY == null ? 0 : o.NXT_BUFR_QTY,
                        QTY_MOU_ID: 3,
                        //QTY_MOU_ID: o.QTY_MOU_ID == 0 ? 3 : o.QTY_MOU_ID,
                        LK_LOC_SRC_TYPE_ID: o.LK_LOC_SRC_TYPE_ID == null ? 1 : o.LK_LOC_SRC_TYPE_ID,
                        RF_BRAND_ID: o.RF_BRAND_ID == 0 ? null : o.RF_BRAND_ID,
                        LOT_REF_NO: o.LOT_REF_NO == null ? "" : o.LOT_REF_NO,
                        SP_NOTE: o.SP_NOTE
                    }
                });

                return KnittingDataService.saveDataByUrl(data, '/KnitYarnReq/AddCart').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        config.appToastMsg(res.data.PMSG);
                        $state.go($state.current, { pMC_FAB_PROD_ORD_H_ID: 0 }, { reload: true });
                    }
                });
            }
        };
    }

})();