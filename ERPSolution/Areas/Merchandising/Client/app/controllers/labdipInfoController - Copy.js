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
            height: '350px',
            scrollable: true,
            navigatable: true,
            dataSource: LabdipDataSource,
            filterable: true,
            selectable: "row",
            sortable: true,
            //pageSize: 10,
            //pageable: true,
            refresh: true,
            columns: [
                { field: "ORDER_TYPE_NAME", title: "Order Type", type: "string", width: "100px" },
                { field: "LD_REQ_NO", title: "Program No.", type: "string", width: "80px" },
                { field: "LD_REQ_DT", title: "Req. Date", type: "date", width: "150px", format: "{0:dd-MMM-yyyy}" },
                { field: "REMARKS", title: "Remarks", type: "string", width: "100px" },
                //{ field: "TASK_STATUS_NAME", title: "Status", type: "string", width: "200px" },
                {
                    title: "Action",
                    template: function () {
                        return "<a ui-sref='LabdipEntry({ID:dataItem.MC_LD_REQ_H_ID})' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a>";
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
    angular.module('multitex.mrc').controller('labdipInfoController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'MrcDataService', '$window', 'Data', labdipInfoController]);
    function labdipInfoController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, MrcDataService, $window, Data) {

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
        vm.isSendToLab = false;

        $scope.msgPosition = { top: '50%', left: ($window.innerWidth / 2) - 200, pinned: true }


        activate();
        function activate() {
            var promise = [getBuyerListData(), getOrderTypeList(), getUserData(), ColorRefList(), getUserDataForAttnUser(), LightingList(),
                           getStyleList(), getColorList(), getFabricList(), getOrderList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;

            });
        }

        if ($stateParams.bAcId && $stateParams.bAcId > 0) {
            vm.form['MC_BYR_ACC_ID'] = $stateParams.bAcId;
        }

        function getDataByLdReqId(MC_LD_REQ_H_ID) {
            return MrcDataService.getDataByUrl('/Labdip/LabdipDataListByHID/' + MC_LD_REQ_H_ID).then(function (res) {
                res['items'] = res.items.map(function (obj) {

                    obj.TARGET_DT = $filter('date')(obj.TARGET_DT, vm.dtFormat);
                    obj.SHIP_DT = $filter('date')(obj.SHIP_DT, vm.dtFormat);
                    return obj;
                });
                vm.isSendToLab = _.get(res, 'items[0].MC_TNA_TASK_STATUS_ID') > 1 ? true : false;
                res['LD_ATTN_MAIL'] = res.LD_ATTN_MAIL ? res.LD_ATTN_MAIL.split(",") : null;
                vm.form = res;
                vm.obGrid = res.items;
            }, function (err) {
                console.log(err);
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
        $scope.TARGET_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.TARGET_DT_LNopened = true;
        };

        if ($stateParams.ID && !_.isEmpty(Data)) {

            Data['items'] = Data.items.map(function (obj) {
                obj.TARGET_DT = $filter('date')(obj.TARGET_DT, vm.dtFormat);
                obj.SHIP_DT = $filter('date')(obj.SHIP_DT, vm.dtFormat);
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
                dataBound: function (e) {
                    var vParentID = this.dataItem(e.item).MC_BYR_ACC_ID;
                    var data = _.filter(vm.buyerListData, { 'MC_BYR_ACC_ID': vParentID });
                    $("#ddlbuyerList").kendoDropDownList({
                        autoBind: true,
                        optionLabel: "-- Select Buyer --",
                        //index:1,
                        dataTextField: "BUYER_NAME_EN",
                        dataValueField: "MC_BUYER_ID",
                        dataSource: data,
                        filter: "contains"
                    });

                    if (vParentID != '') {
                        MrcDataService.GetAllOthers('/api/mrc/StyleHExt/BuyerWiseStyleHExtList/' + vParentID + '/null').then(function (res) {
                            $("#ddlStyleNoList").kendoDropDownList({
                                autoBind: true,
                                optionLabel: "-- Select Style No --",
                                dataTextField: "STYLE_NO",
                                dataValueField: "MC_STYLE_H_ID",
                                dataSource: res,
                                filter: "contains",
                                select: function (e) {
                                    var vParentStyleID = this.dataItem(e.item).MC_STYLE_H_ID;
                                    vm.formStyle.STYLE_NO = this.dataItem(e.item).STYLE_NO;

                                    if (vParentStyleID != null) {

                                        MrcDataService.GetAllOthers('/api/mrc/Order/OrderHdrDataListWithTnaData/null/null/' + vParentStyleID + '/null/null/7').then(function (res) {
                                            $("#ddlOrderNo").kendoDropDownList({
                                                autoBind: true,
                                                optionLabel: "-- Select Order No. --",
                                                dataTextField: "ORDER_NO",
                                                dataValueField: "MC_ORDER_H_ID",
                                                dataSource: res,
                                                filter: "contains",
                                                select: function (e) {
                                                    var dataItem = this.dataItem(e.item);
                                                    vm.formStyle.ORDER_NO = dataItem.ORDER_NO;
                                                    vm.formStyle.SHIP_DT = $filter('date')(dataItem.SHIP_DT, vm.dtFormat);
                                                    vm.formStyle.TARGET_DT = $filter('date')(dataItem.PLAN_START_DT, vm.dtFormat);
                                                }
                                            });
                                        });



                                        MrcDataService.GetAllOthers('/api/mrc/StyleDItemFab/SelectFabByStyleID/' + vParentStyleID).then(function (res) {
                                            $("#ddlFabricDesc").kendoDropDownList({
                                                autoBind: true,
                                                optionLabel: "-- Select Fabric --",
                                                dataTextField: "FABRIC_DESC",
                                                dataValueField: "MC_STYLE_D_FAB_ID",
                                                dataSource: res,
                                                filter: "contains",
                                                select: function (e) {
                                                    var dataItem = this.dataItem(e.item);
                                                    if (dataItem.RF_FIB_COMP_ID) {
                                                        vm.formStyle.FABRIC_DESC = dataItem.FABRIC_DESC;
                                                    } else {
                                                        vm.formStyle.FABRIC_DESC = '';
                                                    }

                                                }
                                            });
                                        });
                                        MrcDataService.GetAllOthers('/api/mrc/ColourMaster/ColourMappDataByBuyerId/' + vParentStyleID).then(function (res) {
                                            $("#ddlColorbyBuyer").kendoDropDownList({
                                                optionLabel: "-- Select Color --",
                                                template: '#: data.COLOR_NAME_EN # #: data.OTHER_REF #',
                                                valueTemplate: '#: data.COLOR_NAME_EN # #: data.OTHER_REF #',
                                                filter: "contains",
                                                autoBind: true,
                                                dataSource: {
                                                    transport: {
                                                        read: function (e) {
                                                            e.success(
                                                                _.filter(res, function (o) {
                                                                    return o.IS_LD_WIP == 'N';
                                                                })
                                                                );

                                                        }
                                                    }
                                                },
                                                select: function (e) {
                                                    var dataItem = this.dataItem(e.item);
                                                    var vStyleID = dataItem.COLOR_REF_NO;
                                                    vm.formStyle['MC_STYLE_D_ITEM_LST'] = dataItem.MC_STYLE_D_ITEM_LST;
                                                    if (!dataItem.PANTON_NO) {
                                                        vm.formStyle['PANTON_NO'] = "As Swatch Provided";
                                                    }
                                                    else {
                                                        vm.formStyle['PANTON_NO'] = dataItem.PANTON_NO;
                                                    }

                                                    vm.formStyle.COLOR_NAME_EN = dataItem.COLOR_NAME_EN + ' ' + dataItem.OTHER_REF;
                                                },
                                                dataTextField: "COLOR_NAME_EN",
                                                dataValueField: "MC_COLOR_ID"
                                            });

                                        });
                                    }

                                }
                            });
                        });
                    };

                },
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    var vParentID = dataItem.MC_BYR_ACC_ID;
                    var data = _.filter(vm.buyerListData, { 'MC_BYR_ACC_ID': vParentID });

                    if (vParentID != null) {

                        MrcDataService.GetAllOthers('/api/mrc/StyleHExt/BuyerWiseStyleHExtList/' + vParentID + '/null').then(function (res) {
                            $("#ddlStyleNoList").kendoDropDownList({
                                autoBind: true,
                                optionLabel: "-- Select Style No --",
                                dataTextField: "STYLE_NO",
                                dataValueField: "MC_STYLE_H_ID",
                                dataSource: res,
                                filter: "contains",
                                select: function (e) {
                                    var vParentStyleID = this.dataItem(e.item).MC_STYLE_H_ID;
                                    vm.formStyle.STYLE_NO = this.dataItem(e.item).STYLE_NO;

                                    if (vParentStyleID != null) {

                                        MrcDataService.GetAllOthers('/api/mrc/Order/OrderHdrDataListWithTnaData/null/null/' + vParentStyleID + '/null/null/7').then(function (res) {
                                            $("#ddlOrderNo").kendoDropDownList({
                                                autoBind: true,
                                                optionLabel: "-- Select Order No. --",
                                                dataTextField: "ORDER_NO",
                                                dataValueField: "MC_ORDER_H_ID",
                                                dataSource: res,
                                                filter: "contains",
                                                select: function (e) {
                                                    var dataItem = this.dataItem(e.item);
                                                    vm.formStyle.ORDER_NO = dataItem.ORDER_NO;
                                                    vm.formStyle.SHIP_DT = $filter('date')(dataItem.SHIP_DT, vm.dtFormat);
                                                    vm.formStyle.TARGET_DT = $filter('date')(dataItem.PLAN_START_DT, vm.dtFormat);
                                                }
                                            });
                                        });



                                        MrcDataService.GetAllOthers('/api/mrc/StyleDItemFab/SelectFabByStyleID/' + vParentStyleID).then(function (res) {
                                            $("#ddlFabricDesc").kendoDropDownList({
                                                autoBind: true,
                                                optionLabel: "-- Select Fabric --",
                                                dataTextField: "FABRIC_DESC",
                                                dataValueField: "MC_STYLE_D_FAB_ID",
                                                dataSource: res,
                                                filter: "contains",
                                                select: function (e) {
                                                    var dataItem = this.dataItem(e.item);
                                                    if (dataItem.RF_FIB_COMP_ID) {
                                                        vm.formStyle.FABRIC_DESC = dataItem.FABRIC_DESC;
                                                    } else {
                                                        vm.formStyle.FABRIC_DESC = '';
                                                    }

                                                }
                                            });
                                        });
                                        MrcDataService.GetAllOthers('/api/mrc/ColourMaster/ColourMappDataByBuyerId/' + vParentStyleID).then(function (res) {
                                            $("#ddlColorbyBuyer").kendoDropDownList({
                                                optionLabel: "-- Select Color --",
                                                template: '#: data.COLOR_NAME_EN # #: data.OTHER_REF #',
                                                valueTemplate: '#: data.COLOR_NAME_EN # #: data.OTHER_REF #',
                                                filter: "contains",
                                                autoBind: true,
                                                dataSource: {
                                                    transport: {
                                                        read: function (e) {
                                                            e.success(
                                                                _.filter(res, function (o) {
                                                                    return o.IS_LD_WIP == 'N';
                                                                })
                                                                );

                                                        }
                                                    }
                                                },
                                                select: function (e) {
                                                    var dataItem = this.dataItem(e.item);
                                                    var vStyleID = dataItem.COLOR_REF_NO;
                                                    vm.formStyle['MC_STYLE_D_ITEM_LST'] = dataItem.MC_STYLE_D_ITEM_LST;
                                                    if (!dataItem.PANTON_NO) {
                                                        vm.formStyle['PANTON_NO'] = "As Swatch Provided";
                                                    }
                                                    else {
                                                        vm.formStyle['PANTON_NO'] = dataItem.PANTON_NO;
                                                    }

                                                    vm.formStyle.COLOR_NAME_EN = dataItem.COLOR_NAME_EN + ' ' + dataItem.OTHER_REF;;
                                                },
                                                dataTextField: "COLOR_NAME_EN",
                                                dataValueField: "MC_COLOR_ID"
                                            });

                                        });
                                    }

                                }
                            });
                        });


                        $("#ddlbuyerList").kendoDropDownList({
                            autoBind: true,
                            optionLabel: "-- Select Buyer --",
                            dataTextField: "BUYER_NAME_EN",
                            dataValueField: "MC_BUYER_ID",
                            dataSource: data,
                            filter: "contains",
                            select: function (e) {
                                if (this.dataItem(e.item).MC_BUYER_ID) {
                                    MrcDataService.GetAllOthers('/api/mrc/StyleHExt/BuyerWiseStyleHExtList/null/' + parseInt(this.dataItem(e.item).MC_BUYER_ID)).then(function (res) {
                                        $("#ddlStyleNoList").kendoDropDownList({
                                            autoBind: true,
                                            optionLabel: "-- Select Style No --",
                                            dataTextField: "STYLE_NO",
                                            dataValueField: "MC_STYLE_H_ID",
                                            dataSource: res,
                                            filter: "contains",
                                            select: function (e) {
                                                var vParentStyleID = this.dataItem(e.item).MC_STYLE_H_ID;
                                                vm.formStyle.STYLE_NO = this.dataItem(e.item).STYLE_NO;

                                                if (vParentStyleID != null) {

                                                    MrcDataService.GetAllOthers('/api/mrc/Order/OrderHdrDataListWithTnaData/null/null/' + vParentStyleID + '/null/null/7').then(function (res) {
                                                        $("#ddlOrderNo").kendoDropDownList({
                                                            autoBind: true,
                                                            optionLabel: "-- Select Order No. --",
                                                            dataTextField: "ORDER_NO",
                                                            dataValueField: "MC_ORDER_H_ID",
                                                            dataSource: res,
                                                            filter: "contains",
                                                            select: function (e) {
                                                                var dataItem = this.dataItem(e.item);
                                                                vm.formStyle.ORDER_NO = dataItem.ORDER_NO;
                                                                vm.formStyle.SHIP_DT = $filter('date')(dataItem.SHIP_DT, vm.dtFormat);
                                                                vm.formStyle.TARGET_DT = $filter('date')(dataItem.PLAN_START_DT, vm.dtFormat);
                                                            }
                                                        });
                                                    });



                                                    MrcDataService.GetAllOthers('/api/mrc/StyleDItemFab/SelectFabByStyleID/' + vParentStyleID).then(function (res) {
                                                        $("#ddlFabricDesc").kendoDropDownList({
                                                            autoBind: true,
                                                            optionLabel: "-- Select Fabric --",
                                                            dataTextField: "FABRIC_DESC",
                                                            dataValueField: "MC_STYLE_D_FAB_ID",
                                                            dataSource: res,
                                                            filter: "contains",
                                                            select: function (e) {
                                                                var dataItem = this.dataItem(e.item);
                                                                if (dataItem.RF_FIB_COMP_ID) {
                                                                    vm.formStyle.FABRIC_DESC = dataItem.FABRIC_DESC;
                                                                } else {
                                                                    vm.formStyle.FABRIC_DESC = '';
                                                                }

                                                            }
                                                        });
                                                    });
                                                    MrcDataService.GetAllOthers('/api/mrc/ColourMaster/ColourMappDataByBuyerId/' + vParentStyleID).then(function (res) {
                                                        $("#ddlColorbyBuyer").kendoDropDownList({
                                                            optionLabel: "-- Select Color --",
                                                            template: '#: data.COLOR_NAME_EN # #: data.OTHER_REF #',
                                                            valueTemplate: '#: data.COLOR_NAME_EN # #: data.OTHER_REF #',
                                                            filter: "contains",
                                                            autoBind: true,
                                                            dataSource: {
                                                                transport: {
                                                                    read: function (e) {
                                                                        e.success(
                                                                            _.filter(res, function (o) {
                                                                                return o.IS_LD_WIP == 'N';
                                                                            })
                                                                            );

                                                                    }
                                                                }
                                                            },
                                                            select: function (e) {
                                                                var dataItem = this.dataItem(e.item);
                                                                var vStyleID = dataItem.COLOR_REF_NO;


                                                                vm.formStyle['MC_STYLE_D_ITEM_LST'] = dataItem.MC_STYLE_D_ITEM_LST;
                                                                if (!dataItem.PANTON_NO) {
                                                                    vm.formStyle['PANTON_NO'] = "As Swatch Provided";
                                                                }
                                                                else {
                                                                    vm.formStyle['PANTON_NO'] = dataItem.PANTON_NO;
                                                                }

                                                                vm.formStyle.COLOR_NAME_EN = dataItem.COLOR_NAME_EN + ' ' + dataItem.OTHER_REF;
                                                            },
                                                            dataTextField: "COLOR_NAME_EN",
                                                            dataValueField: "MC_COLOR_ID"
                                                        });

                                                    });
                                                }

                                            }
                                        });
                                    });
                                }
                            }
                        });


                    }
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
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

        function getStyleList() {
            return vm.StyleData = {
                optionLabel: "-- Select Style --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success([]);
                            //MrcDataService.GetAllOthers('/api/mrc/StyleH/SelectAll').then(function (res) {
                            //    e.success(res);
                            //}, function (err) {
                            //    console.log(err);
                            //});
                        }
                    }
                },
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_ID"
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
        function getOrderList() {
            return vm.orderNolist = {
                optionLabel: "-- Select Order # --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success([]);
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

        vm.styleItemAdd = function () {

            console.log(vm.obGrid);
            var isDuplicate = _.findIndex(vm.obGrid, { 'MC_STYLE_H_ID': vm.formStyle.MC_STYLE_H_ID, 'MC_COLOR_ID': vm.formStyle.MC_COLOR_ID, 'MC_STYLE_D_ITEM_LST': vm.formStyle.MC_STYLE_D_ITEM_LST });
            if (isDuplicate < 0) {
                vm.obGrid.push({
                    //MC_LD_REQ_D_ID: vm.formStyle.MC_LD_REQ_D_ID,
                    MC_STYLE_H_ID: vm.formStyle.MC_STYLE_H_ID,
                    STYLE_NO: vm.formStyle.STYLE_NO,
                    MC_ORDER_H_ID: vm.formStyle.MC_ORDER_H_ID,
                    ORDER_NO: vm.formStyle.ORDER_NO,
                    MC_COLOR_ID: vm.formStyle.MC_COLOR_ID,
                    COLOR_NAME_EN: vm.formStyle.COLOR_NAME_EN,
                    PANTON_NO: vm.formStyle.PANTON_NO,
                    MC_STYLE_D_FAB_ID: vm.formStyle.MC_STYLE_D_FAB_ID,
                    FABRIC_DESC: vm.formStyle.FABRIC_DESC,
                    COLOR_REF: vm.formStyle.COLOR_REF,
                    LK_LTSRC_ID: vm.formStyle.LK_LTSRC_ID,
                    LK_LTSRC_NAME: vm.formStyle.LK_LTSRC_NAME,
                    REQD_QTY: null,
                    TARGET_DT: $filter('date')(vm.formStyle.TARGET_DT, vm.dtFormat),
                    LK_PRIORITY_ID: null,
                    MC_TNA_TASK_STATUS_ID: 1,
                    COMMENTS: null,
                    APRV_LD_REF: null,
                    SHIP_DT: $filter('date')(vm.formStyle.SHIP_DT, vm.dtFormat),
                    MC_STYLE_D_ITEM_LST: vm.formStyle.MC_STYLE_D_ITEM_LST
                });
            }
            else {
                $scope.kNotification.show("Colour Duplication is not allowed", "error");
                return false;
            }

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

        vm.removeStyleRow = function (index, removeFrom, master) {
            removeFrom.splice(index, 1);
        };

        vm.submitData = function (dataOri, token) {
            var data = angular.copy(dataOri);
            data.LD_REQ_DT = $filter('date')(data.LD_REQ_DT, vm.dtFormat);

            data.items = [];
            data.items = vm.obGrid;

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
            data.items = vm.obGrid;
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