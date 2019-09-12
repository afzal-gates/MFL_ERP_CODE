(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('MachineWashProductionController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'cur_user_id', 'Dialog', '$filter', MachineWashProductionController]);
    function MachineWashProductionController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, cur_user_id, Dialog, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.TimeFormat = config.appTimeFormat;
        vm.ACTION_DIS = 0;
        vm.REPROCESS_FLAG = 0;
        vm.today = new Date();
        //vm.form = {};

        $scope.datePickerOptions = {
            parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
        };

        vm.form = { REQ_STORE_ID: '4', ISS_STORE_ID: '6', STR_REQ_BY: cur_user_id, STR_REQ_DT: vm.today, RF_ACTN_VIEW: 0, DYE_RE_PROC_TYPE_ID: 1 };
        vm.formItem = { 'QTY_MOU_ID': '3', 'CENTRAL_STK': 0, 'SUB_STK': 0 };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetMachineList(), GetCompanyList(), getUserData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
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
                            DyeingDataService.getUserDatalist().then(function (res) {
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

        }

        vm.departmentList = {
            optionLabel: "--Select Dept--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/Hr/Admin/HrDesignation/DepartmentListData').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            },
            dataTextField: "DEPARTMENT_NAME_EN",
            dataValueField: "HR_DEPARTMENT_ID"
        };

        function GetCompanyList() {

            return vm.companyList = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
        };

        vm.getRequisitionInfo = function () {

            DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueRequisition/GetDCIssueRequisitionInfo?pSTR_REQ_NO=' + (vm.form.STR_REQ_NO || "")).then(function (res) {
                vm.form = res;
                if ((vm.form.DYE_STR_REQ_H_ID || 0) > 0) {

                    DyeingDataService.getDataByFullUrl('/api/dye/DCIssueRequisition/GetDCIssueRequisitionInfoDtlByID?pDYE_STR_REQ_H_ID=' + (vm.form.DYE_STR_REQ_H_ID || 0)).then(function (res) {
                        vm.recipeItemList = res;
                    });
                }

            });

        };


        function GetMachineList() {

            DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueRequisition/GetMachineInfoByTypeID?pDYE_MC_TYPE_ID=0').then(function (res) {
                vm.machineList = new kendo.data.DataSource({
                    data: res
                });
            });
        };

        $scope.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.STR_REQ_DT_LNopened = true;
        }

        function ReceiveItemList() {
            vm.ReceiveItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/DCIssueRequisition/GetDCIssueRequisitionInfoDtlByID?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };


        vm.resetItemData = function () {
            vm.formItem = { QTY_MOU_ID: '3', CENTRAL_STK: 0, SUB_STK: 0 };
        };

        vm.reset = function () {

            $state.go($state.current, { pDYE_STR_REQ_H_ID: 0 }, { inherit: false, reload: true });
        };


        
        vm.submitAll = function (dataOri, id) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);

                console.log(data);

                vm.showSplash = true;
                
                if (id == 0) {
                    var _ldate = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                    data["LOAD_TIME"] = _ldate;
                    data["UN_LOAD_TIME"] = null;
                    data["DYE_BT_PROD_ID"] = 0;

                }
                else {

                    var _ldate = $filter('date')(data.LOAD_TIME, 'yyyy-MM-ddTHH:mm:ss');
                    data["LOAD_TIME"] = _ldate;

                    var _date = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                    data["UN_LOAD_TIME"] = _date;
                }

                var _fdate = $filter('date')(vm.form.STR_REQ_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["STR_REQ_DT"] = _fdate;

                return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DCIssueRequisition/SaveMW').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        vm.showSplash = false;
                    }
                });
            }
        };


    }


})();

