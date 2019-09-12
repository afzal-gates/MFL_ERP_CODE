(function () {
    'use strict';
    angular.module('multitex.mrc').controller('PrintStrikeOffListController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'MrcDataService', PrintStrikeOffListController]);
    function PrintStrikeOffListController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, MrcDataService) {

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
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/BuyerAcc/getBuyerAccListByUserId/' + vm.form.CREATED_BY).then(function (res) {
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

        vm.showPrintDesign = function () {
            return MrcDataService.GetAllOthers('/api/mrc/PrintStrikeOff/PrintStrikeOffListByBuyerAcc/' + vm.form.MC_BYR_ACC_ID).then(function (res) {
                var dataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            angular.forEach(res, function (val, key) {
                                val['PRNSO_REQ_DT'] = $filter('date')(val['PRNSO_REQ_DT'], vm.dtFormat);
                                //alert(val['items'].length);
                                for (var i = 0; i < val['items'].length; i++) {
                                    val['items'][i].SHIP_DT = $filter('date')(val['items'][i].SHIP_DT, vm.dtFormat);
                                }
                            });
                            e.success(res);
                        }
                    },
                    pageSize: 50,
                });
                $('#PrintGrid').data("kendoGrid").setDataSource(dataSource);
            });
        };

        vm.addNewPrintDesign = function () {
            return $state.go('PrintStrikeOff', { pPrintObj: null });
        };

        vm.editPrintRecord = function (dataItem) {
            vm.form = dataItem;
            return $state.go('PrintStrikeOff', { pPrintObj: dataItem });
        };

        var PrintDataSource = new kendo.data.DataSource({
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

        vm.gridOptionsPrint = {
            autoBind: true,
            height: '350px',
            scrollable: true,
            navigatable: true,
            dataSource: PrintDataSource,
            filterable: true,
            selectable: "row",
            sortable: true,
            //pageSize: 10,
            //pageable: true,
            refresh: true,
            columns: [
                //{ field: "ORDER_TYPE_NAME", title: "Order Type", type: "string", width: "100px" },
                { field: "PRNSO_REQ_NO", title: "Program No.", type: "string", width: "80px" },
                { field: "PRNSO_REQ_DT", title: "Req. Date", type: "date", width: "150px", format: "{0:dd-MMM-yyyy}" },
                { field: "REMARKS", title: "Remarks", type: "string", width: "100px" },
                {
                    title: "Action",
                    template: function () {
                        return "<a ng-click='vm.editPrintRecord(dataItem)' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a>";
                    },
                    width: "70px"
                }
            ]
        };

    }

})();



//////============= Print Design and Strike Off Controller ====================

(function () {
    'use strict';
    angular.module('multitex.mrc').controller('PrintStrikeOffController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'MrcDataService', PrintStrikeOffController]);
    function PrintStrikeOffController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, MrcDataService) {

        //vm.form = formData.MC_ORDER_H_ID ? formData : { IS_ACTIVE: 'Y' };
        
        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        vm.today = new Date();
        vm.form = {}; //{ IS_MCR: 'N' };
        vm.obGrid = [];
        vm.selectedRow = {};
        vm.formStyle = {};

        activate();
        function activate() {
            var promise = [getBuyerAccListData(), getBuyerListData(), getUserData(), PrintTypeList(), PriorityList(), getUserDataForAttnUser(),
                getCompanyList(), getRF_MouList(), getSampleTypeList(), getColorList(), getItemlist(), getStyleList(), getOrderList()]; 
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };
        vm.form.PRNSO_REQ_DT = new Date();
        //vm.form.PRNSO_REQ_DT = $filter('date')(vm.today, vm.dtFormat);

        $scope.PRNSO_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.PRNSO_REQ_DT_LNopened = true;
        };

        if ($stateParams.pPrintObj != null) {
            $('#btnUpdate').show();
            $('#btnSave').attr('disabled', true);
            $('#btnSubmit').attr('disabled', false);
            //console.log($stateParams.pPrintObj);    

            angular.forEach($stateParams.pPrintObj.items, function (val, key) {
                var vLK_PRN_TYPE_LST = [];
                angular.forEach(val['LK_PRN_TYPE_LST'], function (val1, key1) {
                    vLK_PRN_TYPE_LST.push({ LOOKUP_DATA_ID: val1['LOOKUP_DATA_ID'], LK_DATA_NAME_EN: val1['LK_DATA_NAME_EN'] });
                });
                val['LK_PRN_TYPE_LST'] = vLK_PRN_TYPE_LST;
            });
            angular.forEach($stateParams.pPrintObj.items, function (val, key) {
                var vPRN_COLOR_LST = [];
                angular.forEach(val['PRN_COLOR_LST'], function (val1, key1) {
                    vPRN_COLOR_LST.push({ MC_COLOR_ID: val1['MC_COLOR_ID'], COLOR_NAME_EN: val1['COLOR_NAME_EN'] });
                });
                val['PRN_COLOR_LST'] = vPRN_COLOR_LST;
            });

            vm.form = $stateParams.pPrintObj;
            vm.form.PRNSO_REQ_TO_LST = $stateParams.pPrintObj.PRNSO_REQ_TO_LST.split(',');
            vm.obGrid = $stateParams.pPrintObj.items;

            //for (var i = 0; i < vm.obGrid.length; i++) {
            //    if (vm.obGrid[i].RF_ACTN_STATUS_ID < 2) {
            //        $('#btnSendToLab').show();
            //    }
            //    if (vm.obGrid[i].RF_ACTN_STATUS_ID > 1) {
            //        $('#btnUpdate').hide();
            //        $('#btnSendToLab').hide();
            //    }
            //}
            vm.form.items = [];
        };

        function getBuyerAccListData() {
            return vm.buyerAccList = {
                optionLabel: "-- Select Buyer Account--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/BuyerAcc/getBuyerAccListByUserId/' + vm.form.CREATED_BY).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataBound: function (e) {
                    var dataItem = this.dataItem(e.item);
                    var vParentID = dataItem.MC_BYR_ACC_ID;
                    var data = _.filter(vm.buyerListData, { 'MC_BYR_ACC_ID': vParentID });
                    //getBuyerAccWiseStyleListData(vParentID);
                    //getOrderHdrDataList(vParentID);
                    $("#ddlbuyerList").kendoDropDownList({
                        autoBind: false,
                        optionLabel: "-- Select Buyer --",
                        //index:1,
                        dataTextField: "BUYER_NAME_EN",
                        dataValueField: "MC_BUYER_ID",
                        dataSource: data,
                        filter: "startswith"
                    });
                },
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    var vParentID = dataItem.MC_BYR_ACC_ID;
                    var data = _.filter(vm.buyerListData, { 'MC_BYR_ACC_ID': vParentID });

                    if (vParentID != null) {
                        $("#ddlbuyerList").kendoDropDownList({
                            autoBind: false,
                            optionLabel: "-- Select Buyer --",
                            dataTextField: "BUYER_NAME_EN",
                            dataValueField: "MC_BUYER_ID",
                            dataSource: data,
                            filter: "startswith"
                        });
                        //MrcDataService.GetAllOthers('/api/mrc/buyer/BuyerListByBuyerAcc/' + vParentID).then(function (res) {
                        //    $("#ddlbuyerList").kendoDropDownList({
                        //        autoBind: false,
                        //        optionLabel: "-- Select Buyer --",
                        //        dataTextField: "BUYER_NAME_EN",
                        //        dataValueField: "MC_BUYER_ID",
                        //        dataSource: res,
                        //        filter: "startswith"
                        //    });
                        //});
                        MrcDataService.GetAllOthers('/api/mrc/Order/OrderDataList/null/null/null/null/' + vParentID).then(function (res) {
                            $("#ddlOrderNo").kendoDropDownList({
                                autoBind: false,
                                optionLabel: "-- Select Order No. --",
                                dataTextField: "ORDER_NO",
                                dataValueField: "MC_ORDER_H_ID",
                                dataSource: res,
                                filter: "startswith",
                                select: function (e) {
                                    var dataItem = this.dataItem(e.item);
                                    vm.formStyle.ORDER_NO = dataItem.ORDER_NO;
                                    vm.formStyle.SHIP_DT = $filter('date')(dataItem.SHIP_DT, vm.dtFormat);
                                }
                            });
                        });
                        MrcDataService.GetAllOthers('/api/mrc/StyleH/BuyerAccWiseStyleListData/' + vParentID).then(function (res) {
                            $("#ddlStyleNoList").kendoDropDownList({
                                autoBind: false,
                                optionLabel: "-- Select Style No --",
                                dataTextField: "STYLE_NO",
                                dataValueField: "MC_STYLE_H_ID",
                                dataSource: res,
                                filter: "startswith",
                                select: function (e) {
                                    var dataItem = this.dataItem(e.item);
                                    var vParentStyleID = dataItem.MC_STYLE_H_ID;
                                    vm.formStyle.STYLE_NO = dataItem.STYLE_NO;
                                    vm.formStyle.MATERIAL_DESC = dataItem.MATERIAL_DESC;
                                    vm.formStyle.OLD_STYLE_REF = dataItem.STYLE_NO;
                                    if (vParentStyleID != null) {
                                        MrcDataService.GetAllOthers('/api/mrc/StyleDItem/StyleWiseItemList/' + vParentStyleID).then(function (res) {
                                            $("#ddlItemList").kendoDropDownList({
                                                autoBind: false,
                                                optionLabel: "-- Select Item --",
                                                dataTextField: "ITEM_NAME_EN",
                                                dataValueField: "MC_STYLE_D_ITEM_ID",
                                                dataSource: res,
                                                filter: "startswith",
                                                select: function (e) {                                                  
                                                    var dataItem = this.dataItem(e.item);
                                                    vm.formStyle.ITEM_NAME_EN = dataItem.ITEM_NAME_EN;
                                                    vm.formStyle.NatureOfWork = dataItem.NatureOfWork;
                                                    vm.formStyle.ImgSrc = '/Merchandising/Mrc/GetFileByDItem/' + dataItem.MC_STYLE_D_ITEM_ID;
                                                    $('#imgKeyImage').attr('src', vm.formStyle.ImgSrc);

                                                    MrcDataService.GetAllOthers('/api/mrc/StyleDItem/Select/' + dataItem.MC_STYLE_D_ITEM_ID).then(function (res) {
                                                        var vLK_PRN_TYPE_LST = [];
                                                        angular.forEach(res.LK_PRN_TYPE_LST, function (val, key) {
                                                            vLK_PRN_TYPE_LST.push({ LOOKUP_DATA_ID: val['LOOKUP_DATA_ID'], LK_DATA_NAME_EN: val['LK_DATA_NAME_EN'] });                                              
                                                        });
                                                        res.LK_PRN_TYPE_LST = vLK_PRN_TYPE_LST;
                                                        vm.formStyle.LK_PRN_TYPE_LST = res.LK_PRN_TYPE_LST;;
                                                    });

                                                }
                                            });
                                        });
                                    }

                                }
                            });
                        });
                    }
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        };

        //function getBuyerAccWiseStyleListData(pMC_BYR_ACC_ID) {
        //    return vm.StyleData = {
        //        optionLabel: "-- Select Style --",
        //        filter: "startswith",
        //        autoBind: true,
        //        dataSource: {
        //            transport: {
        //                read: function (e) {
        //                    MrcDataService.GetAllOthers('/api/mrc/StyleH/BuyerAccWiseStyleListData/' + pMC_BYR_ACC_ID).then(function (res) {
        //                        e.success(res);
        //                    }, function (err) {
        //                        console.log(err);
        //                    });
        //                }
        //            }
        //        },
        //        dataTextField: "STYLE_NO",
        //        dataValueField: "MC_STYLE_H_ID"
        //    };
        //};
        //function getOrderHdrDataList(pMC_BYR_ACC_ID) {
        //    return vm.orderNolist = {
        //        optionLabel: "-- Select Order # --",
        //        filter: "startswith",
        //        autoBind: true,
        //        dataSource: {
        //            transport: {
        //                read: function (e) {
        //                    MrcDataService.GetAllOthers('/api/mrc/Order/OrderDataList/null/null/null/null/' + pMC_BYR_ACC_ID).then(function (res) {
        //                        e.success(res);
        //                    }, function (err) {
        //                        console.log(err);
        //                    });
        //                }
        //            }
        //        },
        //        select: function (e) {
        //            var dataItem = this.dataItem(e.item);
        //            vm.formStyle.ORDER_NO = dataItem.ORDER_NO;
        //            vm.formStyle.SHIP_DT = dataItem.SHIP_DT;
        //        },
        //        dataTextField: "ORDER_NO",
        //        dataValueField: "MC_ORDER_H_ID"
        //    };
        //}

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
                filter: "startswith",
                autoBind: true,
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
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID"
            };
        };

        function getUserDataForAttnUser() {
            return vm.userListForAttnUser = {
                optionLabel: "-- Select User --",
                filter: "startswith",
                autoBind: true,
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

        function PrintTypeList() {
            return MrcDataService.LookupListData(66).then(function (res) {
                vm.printTypeList = res;
            }, function (err) {
                console.log(err);
            });
        }

        function PriorityList() {
            return vm.priorityList = {
                optionLabel: "-- Select Priority --",
                filter: "startswith",
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
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    vm.formStyle.LK_PRIORITY_NAME = dataItem.LK_DATA_NAME_EN;
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }

        function getRF_MouList() {
            return vm.RF_MouList = {
                optionLabel: "-- Select Unit--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/common/MOUList/Y').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    vm.formStyle.MOU_CODE = dataItem.MOU_CODE;
                },
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID"
            };
        };

        function getSampleTypeList() {
            return vm.sampleTypeList = {
                optionLabel: "-- Select --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/common/SelectAllSampleTypeData').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    vm.formStyle.SMPL_TYPE_NAME = dataItem.SMPL_TYPE_NAME;
                },
                dataTextField: "SMPL_TYPE_NAME",
                dataValueField: "RF_SMPL_TYPE_ID"
            };
        };

        function getColorList() {
            return MrcDataService.GetAllOthers('/api/mrc/ColourMaster/SelectAll').then(function (res) {
                vm.colorList = res;
            }, function (err) {
                console.log(err);
            });
        }

        function getStyleList() {
            return vm.StyleData = {
                optionLabel: "-- Select Style --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/StyleH/SelectAll').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_ID"
            };
        }

        function getOrderList() {
            return vm.orderNolist = {
                optionLabel: "-- Select Order # --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/Order/OrderDataList/null/null/null/null/null').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    vm.formStyle.ORDER_NO = dataItem.ORDER_NO;
                    vm.formStyle.SHIP_DT = dataItem.SHIP_DT;
                },
                dataTextField: "ORDER_NO",
                dataValueField: "MC_ORDER_H_ID"
            };
        }

        function getItemlist() {
            return vm.itemList = {
                optionLabel: "-- Select Item --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/StyleDItem/SelectAll/').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "ITEM_NAME_EN",
                dataValueField: "MC_STYLE_D_ITEM_ID"
            };
        }

        function getCompanyList() {
            return vm.companyList = {
                optionLabel: "-- Select Company --",
                filter: "startswith",
                autoBind: true,
                index: 1,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/common/CompanyList').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
        };

        vm.lsitView = function () {
            $state.go('PrintStrikeOffList');
        };

        vm.cancel = function () {
            vm.formStyle.MC_STYLE_H_ID = null;
            vm.formStyle.STYLE_NO = null;
            vm.formStyle.MC_ORDER_H_ID = null;
            vm.formStyle.ORDER_NO = null;

            vm.formStyle.RF_SMPL_TYPE_ID = null;
            vm.formStyle.SMPL_TYPE_NAME = null;
            vm.formStyle.MC_STYLE_D_ITEM_ID = null;
            vm.formStyle.ITEM_NAME_EN = null;

            vm.formStyle.MATERIAL_DESC = null;
            vm.formStyle.SHIP_DT = null;
            vm.formStyle.NatureOfWork = null;
            vm.formStyle.OLD_STYLE_REF = null;

            vm.formStyle.LK_PRN_TYPE_LST = null;
            vm.formStyle.PRN_COLOR_LST = null;
            vm.formStyle.RQD_QTY = null;

            vm.formStyle.QTY_MOU_ID = null;
            vm.formStyle.MOU_CODE = null;

            vm.formStyle.LK_PRIORITY_ID = null;
            vm.formStyle.LK_PRIORITY_NAME = null;
            vm.formStyle.SP_INSTRUCTION = null;
            //vm.obGrid = [];
        };

        vm.styleItemAdd = function () {
            //vm.formStyle['items'] = [];
            vm.obGrid.push({
                //MC_PRNSO_REQ_D_ID: vm.formStyle.MC_PRNSO_REQ_D_ID,
                MC_STYLE_H_ID: vm.formStyle.MC_STYLE_H_ID,
                STYLE_NO: vm.formStyle.STYLE_NO,
                MC_ORDER_H_ID: vm.formStyle.MC_ORDER_H_ID,
                ORDER_NO: vm.formStyle.ORDER_NO,

                RF_SMPL_TYPE_ID: vm.formStyle.RF_SMPL_TYPE_ID,
                SMPL_TYPE_NAME: vm.formStyle.SMPL_TYPE_NAME,
                MC_STYLE_D_ITEM_ID: vm.formStyle.MC_STYLE_D_ITEM_ID,
                ITEM_NAME_EN: vm.formStyle.ITEM_NAME_EN,

                MATERIAL_DESC: vm.formStyle.MATERIAL_DESC,
                SHIP_DT: $filter('date')(vm.formStyle.SHIP_DT, vm.dtFormat),
                NatureOfWork: vm.formStyle.NatureOfWork,
                OLD_STYLE_REF: vm.formStyle.OLD_STYLE_REF,

                LK_PRN_TYPE_LST: vm.formStyle.LK_PRN_TYPE_LST,
                PRN_COLOR_LST: vm.formStyle.PRN_COLOR_LST,
                RQD_QTY: vm.formStyle.RQD_QTY,

                QTY_MOU_ID: vm.formStyle.QTY_MOU_ID,
                MOU_CODE: vm.formStyle.MOU_CODE,

                LK_PRIORITY_ID: vm.formStyle.LK_PRIORITY_ID,
                LK_PRIORITY_NAME: vm.formStyle.LK_PRIORITY_NAME,
                SP_INSTRUCTION: vm.formStyle.SP_INSTRUCTION,
                ImgSrc: vm.formStyle.ImgSrc
            });
        };

        vm.styleUpdate = function (data) {
            vm.obGrid[data.index] = data;
            $('#btnAdd').show();
            $('#btnUpdateAdd').hide();
        }

        vm.EditStyleRow = function (index, data, copyTo) {
            vm.formStyle = angular.copy(data);
            vm.formStyle['index'] = index;
            $('#btnAdd').hide();
            $('#btnUpdateAdd').show();
            $('#imgKeyImage').attr('src', '/Merchandising/Mrc/GetFileByDItem/' + vm.formStyle.MC_STYLE_D_ITEM_ID);
        };

        vm.removeStyleRow = function (index, removeFrom, master) {
            removeFrom.splice(index, 1);
        };

        vm.submitData = function (data, token) {

            data.PRNSO_REQ_DT = $filter('date')(data.PRNSO_REQ_DT, vm.dtFormat);
            data.items = [];
            data.items = vm.obGrid;
            if (vm.obGrid.length < 1) {
                alert('Please add print Design and Strike Off');
                $('#ddlStyleNo').focus();
                return false;
            }
            if ($('#mulRequesto').val() != null) {
                data['PRNSO_REQ_TO_LST'] = data.PRNSO_REQ_TO_LST.join();
            }

            return MrcDataService.SaveBatchData(data, token, '/api/mrc/PrintStrikeOff/SaveBatchData').then(function (res) {
                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    vm.form.MC_PRNSO_REQ_H_ID = res.data.V_MC_PRNSO_REQ_H_ID;

                    //if (res.data.V_MC_LD_REQ_H_ID > 0) {
                    //    $('#btnSave').hide();
                    //    //$state.go("LabdipEntry", { mc_ld_req_h_id: res.data.v_mc_ld_req_h_id });
                    //}

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $('#btnSave').attr('disabled', true);
                        $('#btnSubmit').attr('disabled', false);
                        vm.isSaved = true;
                    };
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        vm.UpdateBatchData = function (data, token) {

            data.PRNSO_REQ_DT = $filter('date')(data.PRNSO_REQ_DT, vm.dtFormat);
            data.items = [];
            data.items = vm.obGrid;
            if (vm.obGrid.length < 1) {
                alert('Please add print Design and Strike Off');
                $('#ddlStyleNo').focus();
                return false;
            }
            if ($('#mulRequesto').val() != null) {
                data['PRNSO_REQ_TO_LST'] = data.PRNSO_REQ_TO_LST.join();
            }

            return MrcDataService.SaveBatchData(data, token, '/api/mrc/PrintStrikeOff/UpdateBatchData').then(function (res) {
                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    vm.form.MC_PRNSO_REQ_H_ID = res.data.V_MC_PRNSO_REQ_H_ID;

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        //$('#btnSave').hide();
                        //$('#btnSendToLab').attr('disabled', false);
                        vm.isSaved = true;
                    };
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        vm.updateSubmitData = function (data, token) {

            data.PRNSO_REQ_DT = $filter('date')(data.PRNSO_REQ_DT, vm.dtFormat);
            data.items = [];
            data.items = vm.obGrid;
            //for (var i = 0; i < data.items.length; i++) {
            //    data.items[i].RF_ACTN_STATUS_ID = 2;
            //}
            if (vm.obGrid.length < 1) {
                alert('Please add print Design and Strike Off');
                $('#ddlStyleNo').focus();
                return false;
            }
            if ($('#mulRequesto').val() != null) {
                data['PRNSO_REQ_TO_LST'] = data.PRNSO_REQ_TO_LST.join();
            }

            return MrcDataService.SaveBatchData(data, token, '/api/mrc/PrintStrikeOff/SaveBatchData').then(function (res) {
                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        vm.isSaved = true;
                    };
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

    }

})();