////////// Start Header Controller
(function () {
    'use strict';
    angular.module('multitex.inventory').controller('StrIssRtnController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'formData', 'InventoryDataService', 'Dialog', 'cur_user_id', StrIssRtnController]);
    function StrIssRtnController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, formData, InventoryDataService, Dialog, cur_user_id) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        vm.isAddToGrid = 'Y';
        vm.updateGridIndex = null;

        var key = 'SCM_STR_OIL_REQ_H_ID';
        vm.today = new Date();

        vm.form = formData.SCM_STR_GEN_ISS_RTN_H_ID ? formData : { RTN_DT: vm.today, RF_REQ_TYPE_ID: 44, SCM_STORE_ID: 27, STR_REQ_BY: cur_user_id, INV_ITEM_CAT_LST: '' };
        vm.formItem = { SCM_STR_GEN_REQ_D_ID: 0, QTY_MOU_ID: 0, QTY_MOU_CODE: '' };

        vm.errstyle = { 'border': '1px solid #f13d3d' };
        vm.IssueItemList = [];

        activate();
        function activate() {
            var promise = [getReqTypeList(), getCompanyList(), getDepartmentList(),
                            ReturnItemList(), getStoreList(), getUserList(), GetStatusList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        $scope.RTN_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.RTN_DT_LNopened = true;
        };

        vm.searchIssue = function (obj) {
            vm.showSplash = true;
            InventoryDataService.getDataByFullUrl('/api/Inv/GenStrReq/GetGenStrIssueDtlForRtn?pSTR_REQ_NO=' + (obj.STR_REQ_NO || '') + '&pISS_REF_NO=' + (obj.ISS_REF_NO || '')).then(function (res) {
                vm.showSplash = false;
                vm.IssueItemList = res;
            });
        }

        vm.checkReturnQty = function (obj) {
            if (parseFloat(obj.ISS_QTY) < (parseFloat(obj.PRE_QTY) + parseFloat(obj.RTN_QTY))) {
                obj.RTN_QTY = '';
                config.appToastMsg("MULTI-005 Exists Limit!");
            }
        }

        vm.copyAllRtnQty = function () {
            for (var i = 0; i < vm.ReturnItemList.length; i++)
                vm.ReturnItemList[i].RCV_QTY = angular.copy(vm.ReturnItemList[i].RTN_QTY);
        }


        vm.copyRtnQty = function (item) {
            item.RCV_QTY = angular.copy(item.RTN_QTY);
        }


        vm.checkRcvQty = function (item) {
            if (parseFloat(item.RCV_QTY) > parseFloat(item.RTN_QTY)) {
                item.RCV_QTY = '';
                config.appToastMsg("MULTI-005 Exists Receive Qty Limit!");
            }
        }

        function getReqTypeList() {
            vm.reqTypeOptions = {
                optionLabel: "-- Select Req Type --",
                filter: "contains",
                autoBind: true,
                dataTextField: "REQ_TYPE_NAME",
                dataValueField: "RF_REQ_TYPE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.reqTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/GetReqTypeByUser';

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getCompanyList() {
            vm.companyOptions = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.companyDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/CompanyList';

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getDepartmentList() {
            vm.departmentOptions = {
                optionLabel: "-- Select Department --",
                filter: "contains",
                autoBind: true,
                dataTextField: "DEPARTMENT_NAME_EN",
                dataValueField: "HR_DEPARTMENT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.departmentDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/Hr/Admin/HrDesignation/DepartmentListData';

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getStoreList() {
            vm.storeOptions = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.storeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/GetStoreInfo';

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
                            //var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 513 });
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };


        function getUserList() {
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
                            InventoryDataService.getUserDatalist().then(function (res) {
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


        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 10;
            var PARENT_ID = 0;
            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;
            var sList = null;
            InventoryDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
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
                            e.success(sList);
                            //});
                        }
                    }
                },
                dataTextField: "ACTN_STATUS_NAME",
                dataValueField: "RF_ACTN_STATUS_ID"
            };

        };


        function ReturnItemList() {
            InventoryDataService.getDataByFullUrl('/api/Inv/IssueReturn/GetIssueReturnDtlByID?pSCM_STR_GEN_ISS_RTN_H_ID=' + ($stateParams.pSCM_STR_GEN_ISS_RTN_H_ID || 0)).then(function (res) {
                vm.ReturnItemList = res;
            });
        };



        vm.addToGrid = function (data) {

            var item = angular.copy(data);
            console.log(item);
            var count = _.filter(vm.ReturnItemList, function (o) {
                return (o.INV_ITEM_ID === item.INV_ITEM_ID && o.SCM_STR_GEN_ISS_D_ID === item.SCM_STR_GEN_ISS_D_ID)
            }).length;

            if (count == 0) {
                vm.ReturnItemList.push(item);
            }
            else {

                config.appToastMsg("MULTI-005 Duplicate Record Found!");
            }

        };


        vm.editItemData = function (data, step) {

            var mydata = angular.copy(data);
            vm.formItem = mydata;


        }

        vm.removeItemData = function (data) {

            if (!data.KNT_YRN_RCV_D_ID || data.KNT_YRN_RCV_D_ID <= 0) {
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
            vm.formItem['INV_ITEM_ID'] = '';
            vm.formItem['HR_COUNTRY_ID'] = '';
            vm.formItem['PACK_MOU_ID'] = '';
            vm.formItem['QTY_MOU_ID'] = '3';

        };


        vm.printSlip = function (item) {
            //console.log(dataItem);
            //vm.form.SCM_STR_GEN_ISS_RTN_H_ID = item.SCM_STR_GEN_ISS_RTN_H_ID;

            vm.isRDLC = true;
            vm.form.REPORT_CODE = 'RPT-3003';

            var url = '/api/Inv/InvReport/PreviewReportRDLC';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

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


        vm.submitAll = function (dataOri, id) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                data["IS_FINALIZED"] = id;

                data["XML_RTN_D"] = InventoryDataService.xmlStringShort(vm.ReturnItemList.map(function (o) {
                    return {
                        SCM_STR_GEN_ISS_RTN_D_ID: o.SCM_STR_GEN_ISS_RTN_D_ID == null ? 0 : o.SCM_STR_GEN_ISS_RTN_D_ID,
                        SCM_STR_GEN_ISS_RTN_H_ID: o.SCM_STR_GEN_ISS_RTN_H_ID == null ? 0 : o.SCM_STR_GEN_ISS_RTN_H_ID,
                        SCM_STR_GEN_ISS_D_ID: o.SCM_STR_GEN_ISS_D_ID == null ? 0 : o.SCM_STR_GEN_ISS_D_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID,
                        INV_ITEM_CAT_ID: o.INV_ITEM_CAT_ID,
                        RTN_QTY: o.RTN_QTY,
                        RCV_QTY: o.RCV_QTY == null ? 0 : o.RCV_QTY,
                        COST_PRICE: o.COST_PRICE,
                        SEQ_NO: o.SEQ_NO == null ? 0 : o.SEQ_NO,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 1 : o.QTY_MOU_ID,
                    }
                }));

                var id = vm.form.SCM_STR_GEN_ISS_RTN_H_ID

                var _invdate = $filter('date')(data.RTN_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["RTN_DT"] = _invdate;

                return InventoryDataService.saveDataByUrl(data, '/IssueReturn/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pSCM_STR_GEN_ISS_RTN_H_ID: res.data.OPSCM_STR_GEN_ISS_RTN_H_ID }, { reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPSCM_STR_GEN_ISS_RTN_H_ID) > 0) {
                                vm.form.SCM_STR_GEN_ISS_RTN_H_ID = res.data.OPSCM_STR_GEN_ISS_RTN_H_ID;

                                $state.go($state.current, { pSCM_STR_GEN_ISS_RTN_H_ID: res.data.OPSCM_STR_GEN_ISS_RTN_H_ID }, { reload: true });
                            }
                        }
                    }
                });
            }
        };

    }

})();
////////// End Header Controller

