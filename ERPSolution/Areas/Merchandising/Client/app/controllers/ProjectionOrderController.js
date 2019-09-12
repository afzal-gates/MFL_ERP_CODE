(function () {
    'use strict';
    angular.module('multitex.mrc').controller('ProjectionOrderController', ['$q', 'config', '$http', 'MrcDataService', '$stateParams', '$state', '$scope', '$filter', 'Dialog', 'formData', 'cur_user_id', '$modal', ProjectionOrderController]);
    function ProjectionOrderController($q, config, $http, MrcDataService, $stateParams, $state, $scope, $filter, Dialog, formData, cur_user_id, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        vm.form = formData;


        vm.IS_HIDE = false;

        if ($stateParams.pMC_PROV_FAB_BK_H_ID > 0) {

            var lt = moment(vm.form.SHIP_DT).diff(moment(vm.form.ORD_CONF_DT), 'days');
            vm.form.LEAD_TIME = lt;

            MrcDataService.getDataByFullUrl('/api/mrc/ProjectionFabBk/getProjectionFabBookingByOrderID?pMC_ORDER_H_ID=' + (vm.form.MC_ORDER_H_ID || 0) + '&pMC_PROV_FAB_BK_H_ID=' + (vm.form.MC_PROV_FAB_BK_H_ID || 0)).then(function (res) {

                vm.fabBookingData = angular.copy(res);
            });
        }

        if ($stateParams.pIS_REVISE == 'Y') {
            vm.form.REVISION_NO = vm.form.REVISION_NO + 1;
            vm.form.REVISION_DT = vm.today;
            vm.form.RF_ACTN_STATUS_ID = 0;
            vm.form.REV_REASON = '';
        }

        vm.formItem = {};
        vm.myform = {};
        vm.fabBookingData = [];

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getUserData(), GetReqTypeList(), GetCompanyList(), dailyReceiveList(), getOrderTypeList(), GetStatusList(),
                getBuyerAcc(), getBuyerListData(), getStyleListData(0), getMOUList(), getColorListData(), sendEmail(),
                OrderStatusList(), getYDType(), getGmtPartList(), getDiaTypeList(), getRfMouList(), getStyleDevType()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        $scope.templateOrderStatus = '<span class="#: LOOKUP_DATA_ID==365 ? \"k-state-disabled\": \"\"#">#=data.LK_DATA_NAME_EN#</span>';
        $scope.onSelectIOrderStatus = function (e) {
            $(".k-state-disabled").parent().click(false);
        };


        vm.callOffOrderList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    MrcDataService.GetAllOthers('/api/mrc/ProjectionFabBk/getCallOffOrderByOrderID?pMC_ORDER_H_ID=' + (vm.form.MC_ORDER_H_ID || 0)).then(function (res) {
                        e.success(res);

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });

        vm.CALL_OFF_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            //$scope.CALL_OFF_DT_LNopened = true;
            vm.CALL_OFF_DT_LNopened = true;
        }

        vm.SHIP_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.SHIP_DT_LNopened = true;
        }

        $scope.ORD_CONF_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.ORD_CONF_DT_LNopened = true;
        }

        $scope.ORD_SHIP_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.ORD_SHIP_DT_LNopened = true;
        }

        $scope.PROV_FAB_BK_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.PROV_FAB_BK_DT_LNopened = true;
        }

        $scope.REVISION_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.REVISION_DT_LNopened = true;
        }


        vm.searchData = function () {
            dailyReceiveList();
        }

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
                            MrcDataService.getUserDatalist().then(function (res) {
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

        function getColorTypeList() {
            return vm.colorTypeListData = {
                optionLabel: "-- Select --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(31).then(function (res) {
                                e.success(res);
                            });
                        },
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function getGmtPartList() {
            return MrcDataService.GetAllOthers('/api/common/GmtPartList').then(function (res) {
                //vm.gmtPartList = res;
                $scope.gmtPartList = res;
                //vm.gmtPartCircList = _.filter(res, { 'IS_FLAT_CIR': 'C' });
                //vm.gmtPartFlatList = _.filter(res, { 'IS_FLAT_CIR': 'F' });
            }, function (err) {
                console.log(err);
            });
        }

        function getDiaTypeList() {
            return MrcDataService.GetAllOthers('/api/common/LookupListData/67').then(function (res) {
                $scope.diaTypeList = res;
            }, function (err) {
                console.log(err);
            });
        };

        function getRfMouList() {
            return MrcDataService.GetAllOthers('/api/common/MOUList/Y').then(function (res) {
                $scope.rfMouList = res;
                //vm.rfMouList = res;
            }, function (err) {
                console.log(err);
            });
        };

        function getYDType() {
            return MrcDataService.GetAllOthers('/api/common/LookupListData/71').then(function (res) {
                $scope.ydTypeList = res;
            }, function (err) {
                console.log(err);
            });
        };

        function getStyleDevType() {
            return vm.styleDevTypeList = {
                optionLabel: "-- Select --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(53).then(function (res) {
                                e.success(res);
                            });
                        },
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };

        };


        function GetStatusList() {

            var RF_ACTN_TYPE_ID = 26;
            var PARENT_ID = 0;

            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;

            var sList = null;
            MrcDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                //var sList = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "DN" })
                var sList = res;
                console.log(sList);
                if (sList.length == 1) {
                    vm.form.RF_ACTN_VIEW = 0;
                    vm.form.RF_ACTN_STATUS_ID = sList[0].RF_ACTN_STATUS_ID;
                    vm.form.ACTN_STATUS_NAME = sList[0].ACTN_STATUS_NAME;
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
                            MrcDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                                //var lst = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "DN" })
                                var lst = res;
                                if (lst.length == 1) {
                                    vm.form.RF_ACTN_VIEW = 0;
                                    vm.form.RF_ACTN_STATUS_ID = lst[0].RF_ACTN_STATUS_ID;
                                    vm.form.ACTN_STATUS_NAME = lst[0].ACTN_STATUS_NAME;
                                    //alert(vm.form.ACTN_STATUS_NAME);
                                }
                                else {
                                    vm.form.RF_ACTN_VIEW = 1;
                                }
                                e.success(lst);
                            });
                        }
                    }
                },
                dataTextField: "ACTN_STATUS_NAME",
                dataValueField: "RF_ACTN_STATUS_ID"
            };

        }

        vm.addColorRow = function (data, copyTo) {
            var copiedData = angular.copy(data);
            copiedData.MC_PROV_FAB_BK_D_ID = -1;

            angular.forEach(copiedData.GMT_PARTS, function (val, key) {
                angular.forEach(val['GMT_PARTS_QTY'], function (val1, key1) {
                    val1['MC_PROV_FAB_BK_D_ID'] = -1;
                });
            });

            console.log(copiedData);
            copyTo.push(copiedData);
        };

        vm.removeColorRow = function (index, removeFrom, master, data, data1, token) {
            var submitData = angular.copy(data);
            submitData.MC_PROV_FAB_BK_D_ID = data['MC_PROV_FAB_BK_D_ID'];
            submitData.MC_COLOR_ID = data1['MC_COLOR_ID'];

            console.log(data.MC_COLOR_ID);
            if (submitData.MC_PROV_FAB_BK_D_ID > 0) {
                Dialog.confirm('Do you want to remove the item?', 'Are you sure?', ['Yes', 'No'])
                     .then(function () {

                         return $http({
                             headers: { 'Authorization': 'Bearer ' + token },
                             url: '/api/mrc/SampleReq/DeleteSampFabColor',
                             method: 'post',
                             data: submitData
                         })
                         .then(function (res) {
                             $scope.$parent.errors = undefined;
                             if (res.data.success === false) {
                                 $scope.$parent.errors = [];
                                 $scope.$parent.errors = res.data.errors;
                             }
                             else {

                                 res['data'] = angular.fromJson(res.data.jsonStr);

                                 if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                     removeFrom.splice(index, 1);
                                 };

                                 config.appToastMsg(res.data.PMSG);
                             }
                         }).catch(function (message) {
                             exception.catcher('XHR loading Failded')(message);
                         });

                     });

            }
            else
                removeFrom.splice(index, 1);
        };


        vm.getStyleFab = function () {
            MrcDataService.getDataByFullUrl('/api/mrc/ProjectionFabBk/getProjectionFabBookingByOrderID?pMC_ORDER_H_ID=' + (vm.form.MC_ORDER_H_ID || 0) + '&pMC_PROV_FAB_BK_H_ID=' + (vm.form.MC_PROV_FAB_BK_H_ID || 0)).then(function (res) {
                vm.fabBookingData = res;
            });
        }

        function OrderStatusList() {
            return vm.orderStatusList = {
                optionLabel: "-- Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(53).then(function (res) {
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


        function getOrderTypeList() {
            return MrcDataService.LookupListData(40).then(function (res) {
                vm.OrderTypeList = res;
                vm.form.LK_ORD_TYPE_ID = 174;
            }, function (err) {
                console.log(err);
            });
        }

        function getMOUList() {
            return vm.MOUList = {
                optionLabel: "--Qty Unit--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getMOUList().then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID"
            };
        }


        function getBuyerAcc() {
            return vm.buyerAccList = {
                optionLabel: "-- Select Buyer Acc --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/BuyerAcc/SelectAll').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    vm.form.MC_BYR_ACC_ID = dataItem.MC_BYR_ACC_ID;
                    vm.buyerList.read();
                    vm.styleList.read();
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        };

        function getBuyerListData() {
            return vm.buyerList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        MrcDataService.GetAllOthers('/api/mrc/buyer/getBuyerDropdownListByID/' + (vm.form.MC_BYR_ACC_ID || 0)).then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };


        function getColorListData() {
            var pCOLOR_NAME_EN = "";
            if (vm.form) {
                pCOLOR_NAME_EN = vm.form.COLOR_NAME_EN;
            }

            return vm.colorList = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);

                        //var url = '/api/mrc/ColourMaster/SelectAll?pOption=3000';
                        var url = '/api/mrc/ColourMaster/ColourMappDataByBuyerId/' + (vm.form.MC_STYLE_H_ID || 0);

                        url += MrcDataService.kFilterStr2QueryParam(params.filter);
                        //if (pCOLOR_NAME_EN)
                        //    url = url + '&pCOLOR_NAME_EN=' + (pCOLOR_NAME_EN || "");

                        return MrcDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        }

        function getStyleListData(MC_BYR_ACC_ID) {
            var pSTYLE_NO = "";
            if (vm.form) {
                MC_BYR_ACC_ID = vm.form.MC_BYR_ACC_ID;
                pSTYLE_NO = vm.form.STYLE_NO;
            }
            return vm.styleList = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);

                        var url = '/api/mrc/StyleH/BuyerAcWiseStyleList?pageNumber=1&pageSize=10&pMC_BYR_ACC_ID=' + (vm.form.MC_BYR_ACC_ID || 0);

                        url += MrcDataService.kFilterStr2QueryParam(params.filter);
                        if (pSTYLE_NO)
                            url = url + '&pSTYLE_NO=' + (pSTYLE_NO || "");

                        return MrcDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res.data);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };


        vm.getStyleList = function (e) {
            var item = e.sender.dataItem(e.item);
            vm.form.MC_BUYER_ID = item.MC_BUYER_ID;
            vm.styleList.read();
        }

        vm.getStyleColorList = function (e) {

            var item = e.sender.dataItem(e.item);
            if (item.MC_STYLE_H_ID) {
                vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
                vm.form.STYLE_DESC = item.STYLE_DESC;

                vm.form.PCS_PER_PACK = item.PCS_PER_PACK;
                vm.form.HAS_SET = item.HAS_SET;
                vm.form.HAS_COMBO = item.HAS_COMBO;
                vm.form.HAS_EXT = item.HAS_EXT;

                vm.form.HAS_MULTI_COL_PACK = item.HAS_MULTI_COL_PACK;
                vm.form.LK_STYL_DEV_ID = item.LK_STYL_DEV_ID;
                //vm.form.WORK_STYLE_NO = item.STYLE_NO;
                vm.form.WORK_STYLE_NO = vm.form.MC_PROV_FAB_BK_H_ID > 0 ? vm.form.WORK_STYLE_NO : item.STYLE_NO + "-PROJ";
                vm.form.STYLE_NO = item.STYLE_NO;

                geProvisionalOrderList(item.MC_STYLE_H_ID);
            }
            vm.colorList.read();
        }

        vm.onDataBoundStyleDropDown = function (e) {
            var itm = e.sender.dataItem(e.item);
            if (itm.MC_STYLE_H_ID) {
                geProvisionalOrderList(itm.MC_STYLE_H_ID);
                vm.form.WORK_STYLE_NO = vm.form.MC_PROV_FAB_BK_H_ID > 0 ? vm.form.WORK_STYLE_NO : itm.STYLE_NO + "-PROJ";
            }
        }

        vm.onRefreshProjectionOrderList = function (pMC_STYLE_H_ID) {
            geProvisionalOrderList(pMC_STYLE_H_ID);
        };

        vm.generateExtionNumber = function (IS_AUTO) {
            if (IS_AUTO) {

                return MrcDataService.getDataByUrl('/StyleH/getStyleExtAuto/' + vm.form.MC_STYLE_H_ID).then(function (ext) {
                    if (ext) {
                        vm.form['WORK_STYLE_NO'] = vm.form.WORK_STYLE_NO + '-' + ext;
                    }

                }, function (err) {
                    console.log(err);
                    vm.form['WORK_STYLE_NO'] = vm.form.WORK_STYLE_NO;
                });


            } else {
                vm.form['WORK_STYLE_NO'] = vm.form.WORK_STYLE_NO;
            }
        };

        vm.addDiaRow = function (indx, itmPart, list) {
            var idx = indx + 1;
            console.log(itmPart);
            var item = angular.copy(itmPart);
            item.MC_PROV_FAB_BK_D_ID = -1;
            list.splice(idx, "0", item);
        }

        vm.removeDiaRow = function (data, list) {
            var index = list.indexOf(data);
            list.splice(index, 1);
        }

        function GetReqTypeList() {
            return vm.reqTypeList = {
                optionLabel: "-- Select Req Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/common/GetReqType').then(function (res) {
                                var list = _.filter(res, function (o) { return o.IS_R_I == "R" && (o.RF_REQ_TYPE_ID == 1 || o.RF_REQ_TYPE_ID == 2) });
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "REQ_TYPE_NAME",
                dataValueField: "RF_REQ_TYPE_ID"
            };
        };

        function GetStoreList() {

            return vm.reqSourceList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
                                var list = _.filter(res, function (x) { return x.SCM_STORE_ID == 12 || x.SCM_STORE_ID == 17 });
                                e.success(list);
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
                            MrcDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
        };


        function dailyReceiveList() {
            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var pm = MrcDataService.kFilterStr2QueryParam(params.filter);
                        if (pm.length > 0) {
                            MrcDataService.getDataByFullUrl('/api/inv/StoreReceive/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm).then(function (res) {
                                e.success(res);
                            });
                        }
                        else {
                            if (vm.form.MRR_DT) {
                                var _mrrDate = $filter('date')(vm.form.MRR_DT, 'yyyy-MM-ddTHH:mm:ss');
                                vm.form.MRR_DT = _mrrDate;
                            }

                            MrcDataService.getDataByFullUrl('/api/inv/StoreReceive/SelectAll/' + params.page + '/' + params.pageSize + '?pRF_REQ_TYPE_ID=' + vm.form.RF_REQ_TYPE_ID + '&pMRR_NO=' + (vm.form.MRR_NO || "") + '&pMRR_DT=' + (vm.form.MRR_DT || "") + '&pHR_COMPANY_ID=' + (vm.form.HR_COMPANY_ID || "") + '&pSCM_STORE_ID=' + (vm.form.SCM_STORE_ID || "") + '&pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || "") + '&pRF_REQ_TYPE_ID=' + (vm.form.RF_REQ_TYPE_ID || "") + '&pIMP_LC_NO=' + (vm.form.IMP_LC_NO || "")).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                schema: {
                    data: "data",
                    total: 'total',
                    model: {
                        fields: {
                            DYE_DC_RCV_H_ID: { type: "string" },
                            DC_MRR_NO: { type: "string" },
                            DC_MRR_DT: { type: "date" },
                            COMP_NAME_EN: { type: "string" },
                            PAY_MTHD_NAME: { type: "string" },
                            //FROM_ST_NAME: { type: "string" },
                            SOURCE_TYPE: { type: "string" },
                            CHALAN_NO: { type: "string" },
                            IMP_LC_NO: { type: "string" },
                            REMARKS: { type: "string" }
                        }
                    }
                }
            })
        }


        vm.printPO = function (dataItem) {

            vm.form.REPORT_CODE = 'RPT-2009';

            vm.form.MC_ORDER_H_ID = dataItem.MC_ORDER_H_ID;
            vm.form.MC_PROV_FAB_BK_H_ID = dataItem.MC_PROV_FAB_BK_H_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Mrc/MrcReport/PreviewReportRDLC');
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

        vm.addToGrid = function (dataItem) {

            if (parseInt(dataItem.MC_COLOR_ID || 0) < 1 && parseInt(dataItem.ORDER_QTY || 0) < 1) {
                return;
            }

            var data = angular.copy(dataItem);
            var crl = _.filter(vm.colorList.data(), function (x) { return x.MC_COLOR_ID == data.MC_COLOR_ID });
            data["COLOR_NAME_EN"] = crl[0].COLOR_NAME_EN;

            //var mou = _.filter(vm.MOUList.data(), function (x) { return x.RF_MOU_ID == vm.form.QTY_MOU_ID });
            //data["MOU_CODE"] = mou[0].MOU_CODE;

            var count = _.filter(vm.callOffOrderList.data(), function (x) { return x.MC_COLOR_ID == data.MC_COLOR_ID });
            if (count.length == 0) {
                var idx = vm.callOffOrderList.data().length + 1;
                vm.callOffOrderList.insert(idx, data);
                vm.formItem = { MC_COLOR_ID: '', CALL_OFF_DT: '' };
            }
            else {

                config.appToastMsg("MULTI-005 Duplicate Records!");
                return;
            }


        }


        vm.updateToGrid = function (data) {
            var obj = angular.copy(data);

            var crl = _.filter(vm.colorList.data(), function (x) { return x.MC_COLOR_ID == obj.MC_COLOR_ID });
            obj["COLOR_NAME_EN"] = crl[0].COLOR_NAME_EN;

            var row = vm.callOffOrderList.getByUid(vm.formItem.uid);

            row["QTY_MOU_ID"] = obj.QTY_MOU_ID;
            row["MC_PROV_ORD_DET_ID"] = obj.MC_PROV_ORD_DET_ID;
            row["MC_ORDER_H_ID"] = obj.MC_ORDER_H_ID;
            row["MC_COLOR_ID"] = obj.MC_COLOR_ID;
            //row["CALL_OFF_DT"] = obj.CALL_OFF_DT;
            //row["SHIP_DT"] = obj.SHIP_DT;
            row["COLOR_NAME_EN"] = obj.COLOR_NAME_EN;
            //row["DC_WK_NO"] = obj.DC_WK_NO;
            row["ORDER_QTY"] = obj.ORDER_QTY;

            vm.formItem = { MC_COLOR_ID: '', CALL_OFF_DT: '' };

        }

        vm.clearCallOff = function () {
            vm.formItem = { MC_COLOR_ID: '' }
        }

        vm.editData = function (dataItem) {
            var item = angular.copy(dataItem);
            vm.formItem = item;
        }

        vm.removeData = function (data) {

            Dialog.confirm('Finalizing "' + data.COLOR_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var List = _.filter(vm.callOffOrderList.data(), function (x) { return x.COLOR_NAME_EN == data.COLOR_NAME_EN });
                     for (var i = 0; i < List.length; i++) {
                         var item = List[i];
                         vm.callOffOrderList.remove(item);
                     }

                 });
        }

        vm.gridOptions = {
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains"
                    }
                }
            },
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            sortable: true,
            columns: [
                { field: "MC_PROV_ORD_DET_ID", type: "string", hidden: true },
                { field: "MC_ORDER_H_ID", type: "string", hidden: true },
                { field: "MC_COLOR_ID", type: "string", hidden: true },
                { field: "QTY_MOU_ID", type: "string", hidden: true },

                //{ field: "CALL_OFF_DT", title: "Call-Off Date", type: "date", template: "#= kendo.toString(kendo.parseDate(CALL_OFF_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                //{ field: "SHIP_DT", title: "Shipment Date", type: "date", template: "#= kendo.toString(kendo.parseDate(SHIP_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                //{ field: "REQ_TYPE_NAME", title: "REQ_TYPE_NAME", type: "string", width: "50px" },
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "15%" },
                //{ field: "DC_WK_NO", title: "DE Week", type: "string", width: "10%" },
                { field: "ORDER_QTY", title: "Order Qty", type: "string", width: "10%" },
                //{ field: "MOU_CODE", title: "Unit", type: "string", width: "10%" },
                {
                    title: "Action",
                    template: function () {
                        return '<a ng-click="vm.removeData(dataItem);" class="btn btn-xs red"><i class="fa fa-remove"> Remove</i></a> <a ng-click="vm.editData(dataItem);" class="btn btn-xs green"><i class="fa fa-edit"> Edit</i></a>';
                    },
                    width: "10%"
                }
            ]
        };


        vm.CancelAll = function (dataItem, pIS_UPDATED) {

            Dialog.confirm('Do you want to cancel this order?', 'Attention', ['Yes', 'No'])
                 .then(function () {

                     var data = angular.copy(dataItem);

                     data['CallOffOrderList'] = vm.callOffOrderList.data().map(function (o) {
                         return {
                             MC_PROV_ORD_DET_ID: o.MC_PROV_ORD_DET_ID == null ? 0 : o.MC_PROV_ORD_DET_ID,
                             MC_ORDER_H_ID: o.MC_ORDER_H_ID == null ? 0 : o.MC_ORDER_H_ID,
                             //CALL_OFF_DT: o.CALL_OFF_DT == null ? 0 : o.CALL_OFF_DT,
                             //SHIP_DT: o.SHIP_DT,
                             //DC_WK_NO: o.DC_WK_NO,
                             MC_COLOR_ID: o.MC_COLOR_ID,
                             ORDER_QTY: o.ORDER_QTY,
                             QTY_MOU_ID: vm.form.QTY_MOU_ID == null ? 1 : vm.form.QTY_MOU_ID == 0 ? 1 : vm.form.QTY_MOU_ID
                         }
                     });



                     var vXMLFabQty = [];
                     angular.forEach(vm.fabBookingData, function (val, key) {
                         angular.forEach(val['STYLE_COLORS'], function (val1, key1) {
                             angular.forEach(val1['GMT_PARTS'], function (val2, key2) {
                                 angular.forEach(val2['GMT_PARTS_QTY'], function (val3, key3) {
                                     val3['MC_COLOR_ID'] = val1['MC_COLOR_ID'];
                                     val3['LK_YD_TYPE_ID'] = val1['LK_YD_TYPE_ID'];
                                     val3['COLOR_SPEC'] = val1['COLOR_SPEC'];
                                     val3['IS_CONTRAST'] = val1['IS_CONTRAST'];

                                     val3['RF_FIB_COMP_ID'] = val2['RF_FIB_COMP_ID'];
                                     val3['RF_FAB_TYPE_ID'] = val2['RF_FAB_TYPE_ID'];
                                     val3['SP_INSTRUCTION'] = val1['SP_INSTRUCTION'];
                                     val3['MC_STYLE_D_FAB_ID'] = val2['MC_STYLE_D_FAB_ID'];
                                     if (val2['MC_PROV_FAB_BK_D_ID'] < 0)
                                         val3['MC_PROV_FAB_BK_D_ID'] = val2['MC_PROV_FAB_BK_D_ID'];

                                     if (val.STYLE_COLORS[0].GMT_PARTS[key2].RF_GARM_PART_ID_LST != null) {
                                         if (val.STYLE_COLORS[0].GMT_PARTS[key2].RF_GARM_PART_ID_LST.length > 0) {
                                             val3['RF_GARM_PART_LST'] = val.STYLE_COLORS[0].GMT_PARTS[key2].RF_GARM_PART_ID_LST.join(',');
                                         }
                                     }
                                     val3['FIN_GSM'] = val.STYLE_COLORS[0].GMT_PARTS[key2].FIN_GSM;
                                     val3['FIN_DIA'] = val.STYLE_COLORS[0].GMT_PARTS[key2].FIN_DIA; //val2['FIN_DIA'];
                                     val3['DIA_MOU_ID'] = val.STYLE_COLORS[0].GMT_PARTS[key2].DIA_MOU_ID; //val2['DIA_MOU_ID'];
                                     val3['LK_DIA_TYPE_ID'] = val.STYLE_COLORS[0].GMT_PARTS[key2].LK_DIA_TYPE_ID; //val2['LK_DIA_TYPE_ID'];

                                     vXMLFabQty.push(val3);
                                 });
                             });
                         });
                     });

                     console.log(vXMLFabQty);
                     console.log(vm.formFabQty);
                     if (vXMLFabQty.length > 0) {

                         var fabList = _.filter(vXMLFabQty, function (x) { return x.RQD_QTY > 0 });
                         data['XML_FAB_BK'] = MrcDataService.xmlStringShort(fabList.map(function (ob) {
                             return {
                                 MC_PROV_FAB_BK_D_ID: ob.MC_PROV_FAB_BK_D_ID,
                                 MC_PROV_FAB_BK_H_ID: ob.MC_PROV_FAB_BK_H_ID,
                                 MC_STYLE_D_FAB_ID: ob.MC_STYLE_D_FAB_ID,
                                 RF_GARM_PART_LST: ob.RF_GARM_PART_LST,
                                 FIN_DIA: ob.FIN_DIA,
                                 DIA_MOU_ID: ob.DIA_MOU_ID,
                                 LK_DIA_TYPE_ID: ob.LK_DIA_TYPE_ID,
                                 RQD_FAB_QTY: ob.RQD_QTY,
                                 QTY_MOU_ID: ob.QTY_MOU_ID,
                                 FAB_COLOR_ID: ob.MC_COLOR_ID,
                                 LK_YD_TYPE_ID: ob.LK_YD_TYPE_ID,
                                 COLOR_SPEC: ob.COLOR_SPEC,
                                 IS_CONTRAST: ob.IS_CONTRAST,

                                 LK_FBR_GRP_ID: ob.LK_FBR_GRP_ID,
                                 RF_FAB_TYPE_ID: ob.RF_FAB_TYPE_ID,
                                 RF_FIB_COMP_ID: ob.RF_FIB_COMP_ID,
                                 FIN_GSM: ob.FIN_GSM,

                                 LK_COL_TYPE_ID: 358, //ob.LK_COL_TYPE_ID,
                                 SP_INSTRUCTION: ob.SP_INSTRUCTION,

                                 CONS_QTY: ob.RQD_QTY,
                                 CONS_MOU_ID: ob.QTY_MOU_ID,
                                 UNIT_MOU_ID: ob.QTY_MOU_ID,

                             };
                         }));
                     }
                     else {
                         data['XML_FAB_BK'] = null;
                     }
                     data['LK_STYL_DEV_TYP_ID'] = 267;
                     data['LK_ORD_STATUS_ID'] = 267;
                     data['IS_FINALIZED'] = "Y";
                     data['IS_ACTIVE'] = "N";
                     data['IS_UPDATED'] = pIS_UPDATED;


                     var url = '/ProjectionFabBk/Cancel';

                     return MrcDataService.saveDataByUrl(data, url).then(function (res) {

                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {

                             res['data'] = angular.fromJson(res.jsonStr);
                             console.log(res.data.PMSG);
                             config.appToastMsg(res.data.PMSG);
                             if (res.data.PMSG.indexOf("MULTI-005") < 0)

                                 if (data.EMAIL_TO_LST)
                                     if (data.EMAIL_TO_LST.length > 5) {
                                         var submitData = angular.copy(data);

                                         submitData['REPORT_CODE'] = 'RPT-2009';
                                         submitData['PAGE_SIZE_NAME'] = 'Legal';
                                         submitData['IS_EXPORT_TO_DISK'] = 'Y';


                                         return $http({
                                             method: 'post',
                                             url: '/api/mrc/MrcReport/PreviewReportRDLC',
                                             data: submitData
                                         }).then(function (res) {
                                             $state.go($state.current, { pMC_PROV_FAB_BK_H_ID: res.data.OPMC_PROV_FAB_BK_H_ID, pIS_VIEW: $state.pIS_VIEW }, { inherit: false, reload: true });
                                         }).catch(function (message) {
                                             exception.catcher('XHR loading Failded')(message);
                                         });
                                     }
                             $state.go($state.current, { pMC_PROV_FAB_BK_H_ID: res.data.OPMC_PROV_FAB_BK_H_ID, pIS_VIEW: $state.pIS_VIEW }, { inherit: false, reload: true });

                         }
                     });
                 });
        }

        vm.submitAll = function (dataItem, pIS_UPDATED) {

            if (fnValidate() == true) {
                var data = angular.copy(dataItem);

                data['CallOffOrderList'] = vm.callOffOrderList.data().map(function (o) {
                    return {
                        MC_PROV_ORD_DET_ID: o.MC_PROV_ORD_DET_ID == null ? 0 : o.MC_PROV_ORD_DET_ID,
                        MC_ORDER_H_ID: o.MC_ORDER_H_ID == null ? 0 : o.MC_ORDER_H_ID,
                        //CALL_OFF_DT: o.CALL_OFF_DT == null ? 0 : o.CALL_OFF_DT,
                        //SHIP_DT: o.SHIP_DT,
                        //DC_WK_NO: o.DC_WK_NO,
                        MC_COLOR_ID: o.MC_COLOR_ID,
                        ORDER_QTY: o.ORDER_QTY,
                        QTY_MOU_ID: vm.form.QTY_MOU_ID == null ? 1 : vm.form.QTY_MOU_ID == 0 ? 1 : vm.form.QTY_MOU_ID
                    }
                });



                var vXMLFabQty = [];
                angular.forEach(vm.fabBookingData, function (val, key) {
                    angular.forEach(val['STYLE_COLORS'], function (val1, key1) {
                        //console.log(angular.copy(val1));
                        angular.forEach(val1['GMT_PARTS'], function (val2, key2) {
                            //console.log(angular.copy(val2));
                            angular.forEach(val2['GMT_PARTS_QTY'], function (val3, key3) {
                                //console.log(angular.copy(val3));
                                //val3['MC_PROV_FAB_BK_D_ID'] = val['MC_PROV_FAB_BK_D_ID'];
                                val3['MC_COLOR_ID'] = val1['MC_COLOR_ID'];
                                val3['LK_YD_TYPE_ID'] = val1['LK_YD_TYPE_ID'];
                                val3['COLOR_SPEC'] = val1['COLOR_SPEC'];
                                val3['IS_CONTRAST'] = val1['IS_CONTRAST'];

                                val3['RF_FIB_COMP_ID'] = val2['RF_FIB_COMP_ID'];
                                val3['RF_FAB_TYPE_ID'] = val2['RF_FAB_TYPE_ID'];
                                val3['SP_INSTRUCTION'] = val1['SP_INSTRUCTION'];
                                if (val2['MC_PROV_FAB_BK_D_ID'] < 0)
                                    val3['MC_PROV_FAB_BK_D_ID'] = val2['MC_PROV_FAB_BK_D_ID'];

                                //val3['QTY_MOU_ID'] = val3['QTY_MOU_ID'];
                                //val3['RQD_QTY'] = val3['RQD_QTY'];

                                //alert(val.STYLE_COLORS[0].GMT_PARTS[0].FIN_DIA);

                                val3['MC_STYLE_D_FAB_ID'] = val2['MC_STYLE_D_FAB_ID'];

                                //console.log('---------');
                                //console.log(val.STYLE_COLORS[0].GMT_PARTS[key2].RF_GARM_PART_ID_LST);
                                if (val.STYLE_COLORS[0].GMT_PARTS[key2].RF_GARM_PART_ID_LST != null) {
                                    if (val.STYLE_COLORS[0].GMT_PARTS[key2].RF_GARM_PART_ID_LST.length > 0) {
                                        val3['RF_GARM_PART_LST'] = val.STYLE_COLORS[0].GMT_PARTS[key2].RF_GARM_PART_ID_LST.join(','); //val2['RF_GARM_PART_LST'];
                                    }
                                }
                                val3['FIN_GSM'] = val.STYLE_COLORS[0].GMT_PARTS[key2].FIN_GSM;
                                val3['FIN_DIA'] = val.STYLE_COLORS[0].GMT_PARTS[key2].FIN_DIA; //val2['FIN_DIA'];
                                val3['DIA_MOU_ID'] = val.STYLE_COLORS[0].GMT_PARTS[key2].DIA_MOU_ID; //val2['DIA_MOU_ID'];
                                val3['LK_DIA_TYPE_ID'] = val.STYLE_COLORS[0].GMT_PARTS[key2].LK_DIA_TYPE_ID; //val2['LK_DIA_TYPE_ID'];

                                //val2['OTHER_SPEC']

                                vXMLFabQty.push(val3);
                            });
                        });
                    });
                });

                console.log(vXMLFabQty);
                console.log(vm.formFabQty);
                if (vXMLFabQty.length > 0) {

                    var fabList = _.filter(vXMLFabQty, function (x) { return x.RQD_QTY > 0 });
                    data['XML_FAB_BK'] = MrcDataService.xmlStringShort(fabList.map(function (ob) {
                        return {
                            MC_PROV_FAB_BK_D_ID: ob.MC_PROV_FAB_BK_D_ID,
                            MC_PROV_FAB_BK_H_ID: ob.MC_PROV_FAB_BK_H_ID,
                            MC_STYLE_D_FAB_ID: ob.MC_STYLE_D_FAB_ID,
                            RF_GARM_PART_LST: ob.RF_GARM_PART_LST,
                            FIN_DIA: ob.FIN_DIA,
                            DIA_MOU_ID: ob.DIA_MOU_ID,
                            LK_DIA_TYPE_ID: ob.LK_DIA_TYPE_ID,
                            RQD_FAB_QTY: ob.RQD_QTY,
                            QTY_MOU_ID: ob.QTY_MOU_ID,
                            FAB_COLOR_ID: ob.MC_COLOR_ID,
                            LK_YD_TYPE_ID: ob.LK_YD_TYPE_ID,
                            COLOR_SPEC: ob.COLOR_SPEC,
                            IS_CONTRAST: ob.IS_CONTRAST,

                            LK_FBR_GRP_ID: ob.LK_FBR_GRP_ID,
                            RF_FAB_TYPE_ID: ob.RF_FAB_TYPE_ID,
                            RF_FIB_COMP_ID: ob.RF_FIB_COMP_ID,
                            FIN_GSM: ob.FIN_GSM,

                            LK_COL_TYPE_ID: 358, //ob.LK_COL_TYPE_ID,
                            SP_INSTRUCTION: ob.SP_INSTRUCTION,

                            CONS_QTY: ob.RQD_QTY,
                            CONS_MOU_ID: ob.QTY_MOU_ID,
                            UNIT_MOU_ID: ob.QTY_MOU_ID,

                        };
                    }));
                }
                else {
                    data['XML_FAB_BK'] = null;
                }
                data['LK_STYL_DEV_TYP_ID'] = 267;
                data['LK_ORD_STATUS_ID'] = 267;
                data['IS_FINALIZED'] = "N";
                data['IS_UPDATED'] = pIS_UPDATED;

                //data['STYLE_NO'] = data.STYLE_NO;

                var url = '/ProjectionFabBk/Save';
                if ($stateParams.pIS_REVISE == 'Y')
                    url = '/ProjectionFabBk/Revise';

                return MrcDataService.saveDataByUrl(data, url).then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res.data.PMSG);
                        config.appToastMsg(res.data.PMSG);
                        if (res.data.PMSG.indexOf("MULTI-005") < 0)
                            $state.go($state.current, { pMC_PROV_FAB_BK_H_ID: res.data.OPMC_PROV_FAB_BK_H_ID, pIS_VIEW: $stateParams.pIS_VIEW }, { inherit: false, reload: true });

                    }
                });
            }
        }


        vm.FinalizeAll = function (dataItem) {

            var data = angular.copy(dataItem);

            var _date = $filter('date')(data.PROV_FAB_BK_DT, 'yyyy-MM-ddTHH:mm:ss');
            data["PROV_FAB_BK_DT"] = _date;

            var _odate = $filter('date')(data.ORD_CONF_DT, 'yyyy-MM-ddTHH:mm:ss');
            data["ORD_CONF_DT"] = _odate;

            var _sdate = $filter('date')(data.SHIP_DT, 'yyyy-MM-ddTHH:mm:ss');
            data["SHIP_DT"] = _sdate;

            data['CallOffOrderList'] = vm.callOffOrderList.data().map(function (o) {
                return {
                    MC_PROV_ORD_DET_ID: o.MC_PROV_ORD_DET_ID == null ? 0 : o.MC_PROV_ORD_DET_ID,
                    MC_ORDER_H_ID: o.MC_ORDER_H_ID == null ? 0 : o.MC_ORDER_H_ID,
                    CALL_OFF_DT: o.CALL_OFF_DT == null ? 0 : o.CALL_OFF_DT,
                    SHIP_DT: o.SHIP_DT,
                    DC_WK_NO: o.DC_WK_NO,
                    MC_COLOR_ID: o.MC_COLOR_ID,
                    ORDER_QTY: o.ORDER_QTY,
                    QTY_MOU_ID: vm.form.QTY_MOU_ID == null ? 1 : vm.form.QTY_MOU_ID == 0 ? 1 : vm.form.QTY_MOU_ID
                }
            });



            var vXMLFabQty = [];
            angular.forEach(vm.fabBookingData, function (val, key) {
                angular.forEach(val['STYLE_COLORS'], function (val1, key1) {
                    angular.forEach(val1['GMT_PARTS'], function (val2, key2) {
                        angular.forEach(val2['GMT_PARTS_QTY'], function (val3, key3) {
                            val3['MC_COLOR_ID'] = val1['MC_COLOR_ID'];
                            val3['LK_YD_TYPE_ID'] = val1['LK_YD_TYPE_ID'];
                            val3['COLOR_SPEC'] = val1['COLOR_SPEC'];
                            val3['IS_CONTRAST'] = val1['IS_CONTRAST'];
                            val3['SP_INSTRUCTION'] = val1['SP_INSTRUCTION'];

                            //alert(val.STYLE_COLORS[0].GMT_PARTS[0].FIN_DIA);

                            val3['MC_STYLE_D_FAB_ID'] = val2['MC_STYLE_D_FAB_ID'];

                            if (val2['MC_PROV_FAB_BK_D_ID'] < 0)
                                val3['MC_PROV_FAB_BK_D_ID'] = val2['MC_PROV_FAB_BK_D_ID'];

                            //console.log('---------');
                            //console.log(val.STYLE_COLORS[0].GMT_PARTS[key2].RF_GARM_PART_ID_LST);
                            if (val.STYLE_COLORS[0].GMT_PARTS[key2].RF_GARM_PART_ID_LST != null) {
                                if (val.STYLE_COLORS[0].GMT_PARTS[key2].RF_GARM_PART_ID_LST.length > 0) {
                                    val3['RF_GARM_PART_LST'] = val.STYLE_COLORS[0].GMT_PARTS[key2].RF_GARM_PART_ID_LST.join(','); //val2['RF_GARM_PART_LST'];
                                }
                            }
                            val3['FIN_GSM'] = val.STYLE_COLORS[0].GMT_PARTS[key2].FIN_GSM;
                            val3['FIN_DIA'] = val.STYLE_COLORS[0].GMT_PARTS[key2].FIN_DIA; //val2['FIN_DIA'];
                            val3['DIA_MOU_ID'] = val.STYLE_COLORS[0].GMT_PARTS[key2].DIA_MOU_ID; //val2['DIA_MOU_ID'];
                            val3['LK_DIA_TYPE_ID'] = val.STYLE_COLORS[0].GMT_PARTS[key2].LK_DIA_TYPE_ID; //val2['LK_DIA_TYPE_ID'];
                            val3['MC_PROV_FAB_BK_D_ID'] = val.STYLE_COLORS[0].GMT_PARTS[key2].MC_PROV_FAB_BK_D_ID; //val2['LK_DIA_TYPE_ID'];
                            //val3['MC_PROV_FAB_BK_D_ID'] = val['MC_PROV_FAB_BK_D_ID'];
                            //val2['OTHER_SPEC']

                            vXMLFabQty.push(val3);
                        });
                    });
                });
            });

            console.log(vXMLFabQty);
            //console.log(vm.formFabQty);

            if (vXMLFabQty.length > 0) {

                var fabList = _.filter(vXMLFabQty, function (x) { return x.RQD_QTY > 0 });

                data['XML_FAB_BK'] = MrcDataService.xmlStringShort(fabList.map(function (ob) {
                    return {
                        MC_PROV_FAB_BK_D_ID: ob.MC_PROV_FAB_BK_D_ID,
                        MC_PROV_FAB_BK_H_ID: ob.MC_PROV_FAB_BK_H_ID,
                        MC_STYLE_D_FAB_ID: ob.MC_STYLE_D_FAB_ID,
                        RF_GARM_PART_LST: ob.RF_GARM_PART_LST,
                        FIN_DIA: ob.FIN_DIA,
                        DIA_MOU_ID: ob.DIA_MOU_ID,
                        LK_DIA_TYPE_ID: ob.LK_DIA_TYPE_ID,
                        RQD_FAB_QTY: ob.RQD_QTY,
                        QTY_MOU_ID: ob.QTY_MOU_ID,
                        FAB_COLOR_ID: ob.MC_COLOR_ID,
                        LK_YD_TYPE_ID: ob.LK_YD_TYPE_ID,
                        COLOR_SPEC: ob.COLOR_SPEC,
                        IS_CONTRAST: ob.IS_CONTRAST,

                        LK_FBR_GRP_ID: ob.LK_FBR_GRP_ID,
                        RF_FAB_TYPE_ID: ob.RF_FAB_TYPE_ID,
                        RF_FIB_COMP_ID: ob.RF_FIB_COMP_ID,
                        FIN_GSM: ob.FIN_GSM,

                        LK_COL_TYPE_ID: ob.LK_COL_TYPE_ID,
                        SP_INSTRUCTION: ob.SP_INSTRUCTION,

                        CONS_QTY: ob.RQD_QTY,
                        CONS_MOU_ID: ob.QTY_MOU_ID,
                        UNIT_MOU_ID: ob.QTY_MOU_ID,

                    };
                }));
            }
            else {
                data['XML_FAB_BK'] = null;
            }
            data['LK_STYL_DEV_TYP_ID'] = 267;
            data['LK_ORD_STATUS_ID'] = 267;

            data['IS_FINALIZED'] = "Y";
            data['IS_UPDATED'] = "N";

            return MrcDataService.saveDataByUrl(data, '/ProjectionFabBk/Save').then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);
                    console.log(res.data.PMSG);
                    if (res.data.PMSG.indexOf("MULTI-005") < 0)
                        $state.go($state.current, { pMC_PROV_FAB_BK_H_ID: res.data.OPMC_PROV_FAB_BK_H_ID, pIS_VIEW: $stateParams.pIS_VIEW }, { inherit: false, reload: true });

                }
            });
        }


        ////////// Start Projection Booking Approval Notification       
        function sendEmail() {
            if ($stateParams.pIS_VIEW == 'N' && ($stateParams.pIS_REVISE || 'N') != 'Y')
                if (formData.EMAIL_TO_LST)
                    if (formData.EMAIL_TO_LST.length > 5) {
                        var submitData = angular.copy(formData);

                        submitData['REPORT_CODE'] = 'RPT-2009';
                        submitData['PAGE_SIZE_NAME'] = 'Legal';
                        submitData['IS_EXPORT_TO_DISK'] = 'Y';


                        return $http({
                            method: 'post',
                            url: '/api/mrc/MrcReport/PreviewReportRDLC',
                            data: submitData
                        }).then(function (res) {
                            return res.data;
                        }).catch(function (message) {
                            exception.catcher('XHR loading Failded')(message);
                        });
                    }
        };

        function geProvisionalOrderList(pMC_STYLE_H_ID) {
            MrcDataService.getDataByUrl('/Order/OrderDataList/null/null/' + (pMC_STYLE_H_ID || 0) + '/null/null?pIS_PROV=Y').then(function (res) {
                vm.ProvisionalOrderList = res.filter(function (ob) { return ob.LK_STYL_DEV_TYP_ID != 267; }).sort(function compare(a, b) {
                    var dateA = new Date(a.SHIP_DT);
                    var dateB = new Date(b.SHIP_DT);
                    return dateA - dateB;
                });
                vm.TTL_TOT_ORD_QTY = _.sumBy(res.filter(function (ob) { return ob.LK_STYL_DEV_TYP_ID != 267; }), function (o) { return (o.TOT_ORD_QTY || 0) });

                vm.ProvisionalOrderListBk = res.filter(function (ob) { return ob.LK_STYL_DEV_TYP_ID == 267; });

                vm.TTL_TOT_ORD_QTY_BK = _.sumBy(res.filter(function (ob) { return ob.LK_STYL_DEV_TYP_ID == 267; }), function (o) { return (o.TOT_ORD_QTY || 0) });

                if ($stateParams.pMC_PROV_FAB_BK_H_ID < 1 && vm.ProvisionalOrderList.length > 0) {
                    vm.form.TOT_ORD_QTY = (vm.TTL_TOT_ORD_QTY - vm.TTL_TOT_ORD_QTY_BK);
                    vm.form.QTY_MOU_ID = vm.ProvisionalOrderList[0].QTY_MOU_ID;
                    vm.form.ORD_CONF_DT = vm.ProvisionalOrderList[0].ORD_CONF_DT;
                    vm.form.SHIP_DT = vm.ProvisionalOrderList[0].SHIP_DT;
                    console.log(vm.ProvisionalOrderList[0]);
                    //vm.form.WORK_STYLE_NO = 'CALLOFF-' + new Date().getYear() + new Date().getMonth() + new Date().getDay() + new Date().getHours() + new Date().getMinutes();
                }

            })
        };


    }

})();