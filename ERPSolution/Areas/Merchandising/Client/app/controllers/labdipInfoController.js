(function () {
    'use strict';
    angular.module('multitex.mrc').controller('labdipListController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'MrcDataService', '$timeout', labdipListController]);
    function labdipListController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, MrcDataService, $timeout) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        vm.today = new Date();
        vm.form = {}; //{ IS_MCR: 'N' };
        vm.obGrid = [];
        vm.selectedRow = {};

        activate();
        function activate() {
            var promise = [getBuyerAccListData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function getBuyerAccListData() {
            return vm.buyerAccList = {
                optionLabel: "-- Select Buyer Account--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
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



        vm.showLabdip = function () {
            MrcDataService.GetAllOthers('/api/mrc/Labdip/LabdipListByBuyerAcc/' + vm.form.MC_BYR_ACC_ID).then(function (res) {

                angular.forEach(res, function (val, key) {
                    val['LD_REQ_DT'] = $filter('date')(val['LD_REQ_DT'], vm.dtFormat);
                });
                var dataSource = new kendo.data.DataSource({
                    data: res,
                    pageSize: 50,
                });

                $timeout(function () {
                    $('#labdipGrid').data("kendoGrid").setDataSource(dataSource);
                })


            });





        };


        if ($stateParams.BAC && $stateParams.BAC > 0) {
            vm.form['MC_BYR_ACC_ID'] = $stateParams.BAC;
            vm.showLabdip();
        }

        vm.addNewLabdip = function () {
            return $state.go('LabdipEntry', { inherit: false });
        };

        var LabdipDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    e.success([]);
                },
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return { models: kendo.stringify(options.models) };
                    }
                }
            },
            pageSize: 10
        });

        vm.gridOptionsLabdip = {
            autoBind: true,
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
            scrollable: true,
            navigatable: true,
            dataSource: LabdipDataSource,
            sortable: true,
            pageSize: 10,
            pageable: true,
            refresh: true,
            columns: [
       
                { field: "LD_REQ_NO", title: "Program No.", type: "string", width: "80px" },
                { field: "STYLE_NO", title: "Style #", type: "string", width: "80px" },
                { field: "ORDER_LIST", title: "Order #", type: "string", width: "80px" },
                { field: "LD_REQ_DT", title: "Req. Date", type: "date", width: "150px", format: "{0:dd-MMM-yyyy}" },
                { field: "REMARKS", title: "Remarks", type: "string", width: "100px" },
                {
                    title: "Action",
                    template: function () {
                        return "<a ui-sref='LabdipEntry({ID:dataItem.MC_LD_REQ_H_ID,bAcId :dataItem.MC_BYR_ACC_ID,pMC_BUYER_ID:dataItem.MC_BUYER_ID,pMC_STYLE_H_EXT_LST: dataItem.MC_STYLE_H_EXT_LST,pMC_STYLE_H_ID:dataItem.MC_STYLE_H_ID,pHAS_EXT:dataItem.HAS_EXT})' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a>";
                    },
                    width: "70px"
                }
            ]
        };

    }

})();



//////============= Labdip Info Controller ====================

(function () {
    'use strict';
    angular.module('multitex.mrc').controller('labdipInfoController', ['logger', 'config', '$q', '$scope', 'exception', '$filter', '$state', '$stateParams', 'MrcDataService', '$window', 'Data', 'FabricationList', 'ColorList', 'LightSourceList','StyleList','OrderList', labdipInfoController]);
    function labdipInfoController(logger, config, $q, $scope, exception, $filter, $state, $stateParams, MrcDataService, $window, Data, FabricationList, ColorList, LightSourceList, StyleList, OrderList) {

        //vm.form = formData.MC_ORDER_H_ID ? formData : { IS_ACTIVE: 'Y' };

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        vm.today = new Date();
        vm.form = {};
        vm.obGrid = [];
        vm.selectedRow = {};
        vm.formStyle = {};
        vm.isSendToLab = false;

        $scope.msgPosition = { top: '50%', left: ($window.innerWidth / 2) - 200, pinned: true }

        activate();
        function activate() {
            var promise = [getBuyerListData(), getOrderTypeList(), getUserData(), ColorRefList(), getUserDataForAttnUser()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        //var ColourListDs = new kendo.data.DataSource({
        //    data: ColorList
        //});

        var FabricationListDs = new kendo.data.DataSource({
            data: FabricationList
        });

        var LightSourceListDs = new kendo.data.DataSource({
            data: LightSourceList
        });

        //var StyleListDs = new kendo.data.DataSource({
        //    data: StyleList
        //});

        var OrderListDs = new kendo.data.DataSource({
            data: OrderList
        });



        function getDataByLdReqId(MC_LD_REQ_H_ID) {
            return MrcDataService.getDataByUrl('/Labdip/LabdipDataListByHID/' + MC_LD_REQ_H_ID).then(function (res) {
                res['items'] = res.items.map(function (obj) {

                    obj.TARGET_DT = $filter('date')(obj.TARGET_DT, vm.dtFormat);
                    obj.SHIP_DT = $filter('date')(obj.SHIP_DT, vm.dtFormat);
                    //obj.StyleListDs = StyleListDs;
                    //obj.ColourListDs = ColourListDs;
                    obj.FabricationListDs = FabricationListDs;
                    obj.LightSourceListDs = LightSourceListDs;
                    obj.OrderListDs = OrderListDs;

                    return obj;
                });
                vm.isSendToLab = _.get(res, 'items[0].MC_TNA_TASK_STATUS_ID') > 1 ? true : false;
                res['LD_ATTN_MAIL'] = res.LD_ATTN_MAIL ? res.LD_ATTN_MAIL.split(",") : null;
                vm.form = res;
                vm.obGrid = res.items;
            });
        }




        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };
        vm.form.LD_REQ_DT = new Date();
        //vm.form.SHIP_DT = $filter('date')(vm.today, vm.dtFormat);

        $scope.LD_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.LD_REQ_DT_LNopened = true;
        };
        //$scope.TARGET_DT_LNopen = function ($event) {
        //    $event.preventDefault();
        //    $event.stopPropagation();
        //    $scope.TARGET_DT_LNopened = true;
        //};



        $scope.dtFormat = config.appDateFormat;
        $scope.GridDateOpened = [];
        $scope.GridDateOpen = function ($event, index) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.GridDateOpened[index] = true;
        };


        if ($stateParams.ID && !_.isEmpty(Data)) {

            Data['items'] = Data.items.map(function (obj) {
                obj.TARGET_DT = $filter('date')(obj.TARGET_DT, vm.dtFormat);
                obj.SHIP_DT = $filter('date')(obj.SHIP_DT, vm.dtFormat);
                obj.FabricationListDs = FabricationListDs;
                obj.LightSourceListDs = LightSourceListDs;
                obj.OrderListDs = OrderListDs;

                return obj;
            });


            vm.obGrid = Data.items;
            vm.isSendToLab = _.get(Data, 'items[0].MC_TNA_TASK_STATUS_ID') > 1 ? true : false;
            vm.form = Data;
            if (Data.LD_ATTN_MAIL.length > 1) {
                vm.form.LD_ATTN_MAIL = Data.LD_ATTN_MAIL.split(',');
            }
            else {
                vm.form.LD_ATTN_MAIL = Data.LD_ATTN_MAIL;
            }
            vm.form.items = [];
        } else {

                    angular.forEach(ColorList, function (ColorItem, key3) {
                        vm.obGrid.push({
                            MC_STYLE_H_ID: StyleList.MC_STYLE_H_ID,
                            MC_ORDER_H_ID: OrderList.length == 1 ? OrderList[0].MC_ORDER_H_ID : '',
                            FabricationListDs:FabricationListDs,
                            LightSourceListDs:LightSourceListDs,
                            OrderListDs: OrderListDs,
                            STYLE_NO :  StyleList.STYLE_NO,
                            COLOR_NAME_EN: ColorItem.COLOR_NAME_EN,
                            MC_COLOR_ID: ColorItem.MC_COLOR_ID,
                            PANTON_NO: ColorItem.PANTON_NO ? ColorItem.PANTON_NO : 'As Swatch Provided',
                            MC_STYLE_D_FAB_ID: (FabricationList.length > 0) ? FabricationList[0].MC_STYLE_D_FAB_ID : null,
                            MC_TNA_TASK_STATUS_ID: 1,
                            //SHIP_DT: $filter('date')(OrderItem.SHIP_DT, vm.dtFormat),
                            MC_STYLE_D_ITEM_LST: ColorItem.MC_STYLE_D_ITEM_LST
                        });
                    });

                    vm.form.LD_REQ_TO = 79;
        };







        vm.printLapDipReport = function (dataOri) {

            var data = {
                MC_LD_REQ_H_ID: dataOri.MC_LD_REQ_H_ID
            };

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = angular.extend({ REPORT_CODE: 'RPT-2005' }, data);

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

        function getBuyerList(pMC_BYR_ACC_ID) {
            MrcDataService.getDataByFullUrl('/api/mrc/buyer/getBuyerDropdownList?MC_BYR_ACC_ID=' + pMC_BYR_ACC_ID).then(function (res) {
                vm.BuyerListDs = new kendo.data.DataSource({
                    data: res
                });
            });
        };

        vm.buyerListOnBound = function (e) {
            var ds = e.sender.dataSource.data();
            console.log($stateParams.pMC_BUYER_ID);
            if (ds.length == 1) {
                e.sender.value(ds[0].MC_BUYER_ID);

                vm.form['MC_BUYER_ID'] = ds[0].MC_BUYER_ID;
            } else {
                e.sender.value($stateParams.pMC_BUYER_ID);
                vm.form['MC_BUYER_ID'] = $stateParams.pMC_BUYER_ID;
            }
        }

        vm.buyerAccList = {
            optionLabel: "-- Select Buyer Account--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        MrcDataService.GetAllOthers('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            },
            dataBound: function (e) {
                var ds = this.dataSource.data();
                if (ds.length == 1) {
                    this.value(ds[0].MC_BYR_ACC_ID);
                    vm.form['MC_BYR_ACC_ID'] = ds[0].MC_BYR_ACC_ID;
                    getBuyerList(ds[0].MC_BYR_ACC_ID);
                } else {
                    this.value($stateParams.bAcId || null);
                    vm.form['MC_BYR_ACC_ID'] = $stateParams.bAcId||null;
                    getBuyerList($stateParams.bAcId||null);
                }
            },
            select: function (e) {
                var dataItem = this.dataItem(e.item);
                getBuyerList(dataItem.MC_BYR_ACC_ID);

            },
            dataTextField: "BYR_ACC_NAME_EN",
            dataValueField: "MC_BYR_ACC_ID"
        };

        function getBuyerListData() {
            return MrcDataService.GetAllOthers('/api/mrc/buyer/BuyerByUserList').then(function (res) {
                vm.buyerListData = res;
            }, function (err) {
                console.log(err);
            });
        };

        function getUserData() {
            return vm.userList = {
                optionLabel: "-- Select User --",
                filter: "contains",
                autoBind: true,
                //valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                //template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                //          '<span class="k-state-default"><h3>#: data.USER_NAME_EN #</h3><p>#: data.DEPARTMENT_NAME_EN #</p><p>#: data.DESIGNATION_NAME_EN #</p></span>',
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/common/getUserData/TnaTask/7').then(function (res) {

                                e.success(_.reject(res, function (obj) {
                                    return obj.SC_USER_ID == vm.form.CREATED_BY;
                                }));

                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID"
            };
        };


        vm.userListReqBy = {
            autoBind: true,
            //valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
            //template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
            //          '<span class="k-state-default"><h3>#: data.USER_NAME_EN #</h3><p>#: data.DEPARTMENT_NAME_EN #</p><p>#: data.DESIGNATION_NAME_EN #</p></span>',
            dataSource: {
                transport: {
                    read: function (e) {
                        MrcDataService.getDataByFullUrl('/api/common/getUserData/TnaTask/7').then(function (res) {

                            e.success(res);
                            //console.log(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            },
            dataTextField: "LOGIN_ID",
            dataValueField: "SC_USER_ID"
        };


        function getUserDataForAttnUser() {
            return vm.userListForAttnUser = {
                optionLabel: "-- Select User --",
                filter: "contains",
                autoBind: true,
                //valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                //template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                //          '<span class="k-state-default"><h3>#: data.USER_NAME_EN #</h3><p>#: data.DEPARTMENT_NAME_EN #</p><p>#: data.DESIGNATION_NAME_EN #</p></span>',
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getUserDatalist().then(function (res) {
                                e.success(res);
                                //console.log(res);

                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    vm.formStyle.LD_ATTN_MAIL = dataItem.LOGIN_ID;
                },
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID"
            };
        };

        function getOrderTypeList() {
            return MrcDataService.LookupListData(40).then(function (res) {
                vm.OrderTypeList = res;
                //console.log(res);
                vm.form.LK_ORD_TYPE_ID = res[0].LOOKUP_DATA_ID;
            }, function (err) {
                console.log(err);
            });
        }

        function ColorRefList() {
            return vm.colorRefList = {
                optionLabel: "-- Select Color Ref. --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(64).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }

        function PriorityList() {
            return vm.priorityList = {
                optionLabel: "-- Select Priority --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(63).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }

        function LightingList() {
            return vm.lightingList = {
                optionLabel: "Select Lighting",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(68).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    vm.formStyle.LK_LTSRC_NAME = dataItem.LK_DATA_NAME_EN;
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }


        function getColorList() {
            return vm.colorList = {
                optionLabel: "-- Select Color --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {

                            e.success([]);
                            //MrcDataService.GetAllOthers('/api/mrc/ColourMaster/SelectAll').then(function (res) {
                            //    e.success(res);
                            //}, function (err) {
                            //    console.log(err);
                            //});
                        }
                    }
                },
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    var vStyleID = dataItem.COLOR_REF_NO;
                    vm.formStyle['MC_STYLE_D_ITEM_LST'] = dataItem.MC_STYLE_D_ITEM_LST;

                    if (dataItem.PANTON_NO == '' || dataItem.PANTON_NO === undefined || dataItem.PANTON_NO == null) {
                        vm.formStyle.COLOR_REF = "As Swatch Provided";
                    }
                    else {
                        vm.formStyle.PANTON_NO = dataItem.PANTON_NO;
                    }

                    vm.formStyle.COLOR_NAME_EN = dataItem.COLOR_NAME_EN;
                },
                dataTextField: "COLOR_NAME_EN",
                dataValueField: "MC_COLOR_ID"
            };
        }
        function getFabricList() {
            return vm.styleDFabList = {
                optionLabel: "-- Select Fabric--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success([]);
                            //MrcDataService.GetAllOthers('/api/mrc/StyleDItemFab/SelectAll').then(function (res) {
                            //    e.success(res);
                            //}, function (err) {
                            //    console.log(err);
                            //});
                        }
                    }
                },
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    vm.formStyle.FABRIC_DESC = dataItem.FABRIC_DESC;
                },
                dataTextField: "FABRIC_DESC",
                dataValueField: "MC_STYLE_D_FAB_ID"
            };
        }


        vm.lsitView = function () {
            $state.go('LabdipList');
        };

        vm.cancel = function () {
            vm.formStyle.MC_STYLE_H_ID = null;
            vm.formStyle.STYLE_NO = null;
            vm.formStyle.MC_ORDER_H_ID = null;
            vm.formStyle.ORDER_NO = null;
            vm.formStyle.MC_COLOR_ID = null;
            vm.formStyle.COLOR_NAME_EN = null;
            vm.formStyle.COLOR_REF = null;
            vm.formStyle.MC_STYLE_D_FAB_ID = null;
            vm.formStyle.FABRIC_DESC = null;
            vm.formStyle.REQD_QTY = null;
            vm.formStyle.TARGET_DT = null;
            vm.formStyle.LK_PRIORITY_ID = null;
            vm.formStyle.COMMENTS = null;
        };

        vm.CopyOrder = function () {
            vm.form.MC_LD_REQ_H_ID = null;
            var i = 0;
            angular.forEach(vm.obGrid, function (val, key) {
                vm.obGrid[i].MC_LD_REQ_D_ID = null;

                var x = 0;
                angular.forEach(vm.obGrid.items, function (val, key) {
                    vm.obGrid[i].items[x].MC_ORDER_SIZE_ID = null;
                    x = x + 1;
                });

                i = i + 1;
            });
        };

        vm.styleItemAdd = function (index) {
            var itm = angular.copy(vm.obGrid[(index==0?1:index) - 1]);
            vm.obGrid.push({
                StyleListDs: StyleListDs,
                ColourListDs: ColourListDs,
                FabricationListDs: FabricationListDs,
                LightSourceListDs: LightSourceListDs,
                OrderListDs: OrderListDs,
                MC_STYLE_H_ID: itm.MC_STYLE_H_ID,
                MC_ORDER_H_ID: itm.MC_ORDER_H_ID,
                PANTON_NO: itm.PANTON_NO,
                MC_STYLE_D_FAB_ID: itm.MC_STYLE_D_FAB_ID,
                COLOR_REF: itm.COLOR_REF,
                LK_LTSRC_ID: itm.LK_LTSRC_ID,
                REQD_QTY: null,
                TARGET_DT: $filter('date')(itm.TARGET_DT, vm.dtFormat),
                MC_TNA_TASK_STATUS_ID: 1,
                COMMENTS: null,
                APRV_LD_REF: null,
                SHIP_DT: $filter('date')(itm.SHIP_DT, vm.dtFormat)
            });

            //var isDuplicate = _.findIndex(vm.obGrid, { 'MC_STYLE_H_ID': vm.formStyle.MC_STYLE_H_ID, 'MC_COLOR_ID': vm.formStyle.MC_COLOR_ID, 'MC_STYLE_D_ITEM_LST': vm.formStyle.MC_STYLE_D_ITEM_LST });
            //if (isDuplicate < 0) {
            //    vm.obGrid.push({
            //        //MC_LD_REQ_D_ID: vm.formStyle.MC_LD_REQ_D_ID,
            //        MC_STYLE_H_ID: vm.formStyle.MC_STYLE_H_ID,
            //        STYLE_NO: vm.formStyle.STYLE_NO,
            //        MC_ORDER_H_ID: vm.formStyle.MC_ORDER_H_ID,
            //        ORDER_NO: vm.formStyle.ORDER_NO,
            //        MC_COLOR_ID: vm.formStyle.MC_COLOR_ID,
            //        COLOR_NAME_EN: vm.formStyle.COLOR_NAME_EN,
            //        PANTON_NO: vm.formStyle.PANTON_NO,
            //        MC_STYLE_D_FAB_ID: vm.formStyle.MC_STYLE_D_FAB_ID,
            //        FABRIC_DESC: vm.formStyle.FABRIC_DESC,
            //        COLOR_REF: vm.formStyle.COLOR_REF,
            //        LK_LTSRC_ID: vm.formStyle.LK_LTSRC_ID,
            //        LK_LTSRC_NAME: vm.formStyle.LK_LTSRC_NAME,
            //        REQD_QTY: null,
            //        TARGET_DT: $filter('date')(vm.formStyle.TARGET_DT, vm.dtFormat),
            //        LK_PRIORITY_ID: null,
            //        MC_TNA_TASK_STATUS_ID: 1,
            //        COMMENTS: null,
            //        APRV_LD_REF: null,
            //        SHIP_DT: $filter('date')(vm.formStyle.SHIP_DT, vm.dtFormat),
            //        MC_STYLE_D_ITEM_LST: vm.formStyle.MC_STYLE_D_ITEM_LST
            //    });
            //}
            //else {
            //    $scope.kNotification.show("Colour Duplication is not allowed", "error");
            //    return false;
            //}

        };

        vm.styleUpdate = function (data) {
            var ddd = angular.copy(data);
            ddd['SHIP_DT'] = $filter('date')(ddd['SHIP_DT'], vm.dtFormat);
            ddd['TARGET_DT'] = $filter('date')(ddd['TARGET_DT'], vm.dtFormat);
            vm.obGrid[data.index] = ddd;
            $('#btnAdd').show();
            $('#btnUpdateAdd').hide();
        }

        vm.EditStyleRow = function (index, data, copyTo) {
            vm.formStyle = angular.copy(data);
            vm.formStyle['index'] = index;
            $('#btnAdd').hide();
            $('#btnUpdateAdd').show();
        };

        vm.removeStyleRow = function (index, removeFrom) {

            if (removeFrom.length > 1) {
                removeFrom.splice(index, 1);
            }

        };

        vm.submitData = function (dataOri, token,isValid) {

            $scope.LapdipForm.$submitted = true;

            if (!isValid) {
                return true;
            }

            var data = angular.copy(dataOri);
            data.LD_REQ_DT = $filter('date')(data.LD_REQ_DT, vm.dtFormat);

            data.items = [];
            data.items = vm.obGrid.map(function (o) {
                o['SHIP_DT'] = $filter('date')(o['SHIP_DT'], vm.dtFormat);
                o['TARGET_DT'] = $filter('date')(o['TARGET_DT'], vm.dtFormat);
                return o;
            });

            if (vm.obGrid.length < 1) {
                $scope.kNotification.show("Please add Labdip Specification", "error");
                return false;
            }

            data['LD_ATTN_MAIL'] = data.LD_ATTN_MAIL ? data.LD_ATTN_MAIL.join(",") : null;

            return MrcDataService.SaveBatchData(data, token, '/api/mrc/Labdip/BatchSaveLabdip').then(function (res) {
                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        getDataByLdReqId(res.data.V_MC_LD_REQ_H_ID);
                        toastr.success('Successfully Saved', "MultiTEX");
                    } else {
                        config.appToastMsg(res.data.PMSG);
                    };

                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        vm.BatchUpdateData = function (dataOri, token) {

            var data = angular.copy(dataOri);
            data.LD_REQ_DT = $filter('date')(data.LD_REQ_DT, vm.dtFormat);
            data.items = [];
            data.items = vm.obGrid.map(function (o) {
                o['SHIP_DT'] = $filter('date')(o['SHIP_DT'], vm.dtFormat);
                o['TARGET_DT'] = $filter('date')(o['TARGET_DT'], vm.dtFormat);
                return o;
            });



            if (vm.obGrid.length < 1) {
                $scope.kNotification.show("Please add Labdip Specification", "error");
                return false;
            }
            data['LD_ATTN_MAIL'] = data.LD_ATTN_MAIL ? data.LD_ATTN_MAIL.join(",") : null;

            return MrcDataService.SaveBatchData(data, token, '/api/mrc/Labdip/BatchUpdateLabdip').then(function (res) {
                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    vm.form.MC_LD_REQ_H_ID = res.data.V_MC_LD_REQ_H_ID;
                    vm.form.LD_REQ_NO = res.data.V_LD_REQ_NO;
                    vm.form.LD_ATTN_MAIL = res.data.V_LD_ATTN_MAIL.split(',');

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        getDataByLdReqId(res.data.V_MC_LD_REQ_H_ID);
                        toastr.success('Successfully Updated', "MultiTEX");
                    } else {
                        config.appToastMsg(res.data.PMSG);
                    };
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };


        vm.masterCancel = function (MC_LD_REQ_H_ID) {
            if (MC_LD_REQ_H_ID) {
                getDataByLdReqId(MC_LD_REQ_H_ID);
            } else {
                $state.go('LabdipEntry', { pLabdipObj: null }, { reload: true });
            }
        }


        vm.SendToLab = function (dataOri, token) {
            var data = angular.copy(dataOri);
            data['LD_ATTN_MAIL'] = data.LD_ATTN_MAIL ? data.LD_ATTN_MAIL.join(",") : null;
            data.LD_REQ_DT = $filter('date')(data.LD_REQ_DT, vm.dtFormat);
            data.items = [];
            data.items = vm.obGrid;
            for (var i = 0; i < data.items.length; i++) {
                data.items[i].RF_ACTN_STATUS_ID = 2;
                data.items[i]['SHIP_DT'] = $filter('date')(data.items[i]['SHIP_DT'], vm.dtFormat);
                data.items[i]['TARGET_DT'] = $filter('date')(data.items[i]['TARGET_DT'], vm.dtFormat);
            }
            if (vm.obGrid.length < 1) {
                $scope.kNotification.show("Please add Labdip Specification", "error");
                return false;
            }
            return MrcDataService.SaveBatchData(data, token, '/api/mrc/Labdip/SendToLab').then(function (res) {
                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        vm.isSendToLab = true;
                        toastr.success('Successfully SendToLab', "MultiTEX");
                    } else {
                        config.appToastMsg(res.data.PMSG);
                    };

                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

    }

})();