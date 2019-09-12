(function () {
    'use strict';

    angular.module('multitex.admin').controller('ActionApproverAddController', ['AdminService', 'logger', 'config', '$q', '$stateParams', '$state', '$http', '$scope', ActionApproverAddController]);

    function ActionApproverAddController(AdminService, logger, config, $q, $stateParams, $state, $http, $scope) {

        var vm = this;
        vm.form = {};
        activate();

        function activate() {
            var promise = [getOfficeList(), getCompanyList()];
            return $q.all(promise).then(function () {
            });
        }

        vm.form['RF_ACTION_CFG_H_ID'] = $stateParams.atData.RF_ACTION_CFG_H_ID;

        if ($stateParams.configData != null) {

            
            vm.form = $stateParams.configData;

            AdminService.getApproverLvlData(vm.form.RF_ACTION_CFG_H_ID, vm.form.RF_ACTION_CFG_D1_ID).then(function (res) {
                vm.lvlDatas = res;
            });
        }

        console.log($stateParams.configData);

        vm.subdepartmentListData = {
            optionLabel: "-- Select Section --",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        AdminService.getSubDepartmentData($stateParams.configData.HR_DEPARTMENT_ID).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            },
            dataTextField: "DEPARTMENT_NAME_EN",
            dataValueField: "HR_DEPARTMENT_ID"
        };

        vm.departmentList = {
                optionLabel: "-- Select --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: {
                            url: "/Hr/Admin/HrDesignation/DepartmentListData",
                            dataType: "json"
                        }
                    }
                },
                select: function (e) {
                    var HR_DEPARTMENT_ID = this.dataItem(e.item).HR_DEPARTMENT_ID;
          
                    if (HR_DEPARTMENT_ID) {
                        AdminService.getSubDepartmentData(HR_DEPARTMENT_ID).then(function (res) {
                            $("#SECTION_ID").kendoDropDownList({
                                optionLabel: "-- Select Section --",
                                dataTextField: "DEPARTMENT_NAME_EN",
                                dataValueField: "HR_DEPARTMENT_ID",
                                dataSource: res,
                                filter: "startswith"
                            });

                        })
                    }
                },
                dataTextField: "DEPARTMENT_NAME_EN",
                dataValueField: "HR_DEPARTMENT_ID"
            };



        function getOfficeList() {
            return vm.getOfficeList = {
                optionLabel: "-- Select Office --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: {
                            url: "/Hr/HrEmployee/OfficeListData",
                            dataType: "json"
                        }
                    }
                },
                dataTextField: "OFFICE_NAME_EN",
                dataValueField: "HR_OFFICE_ID"
            };
        }

        vm.submitLvlData = function (items) {
            var data = [];
            angular.forEach(items, function (val, key) {
                if (val.ADD) {
                    if (val.EXEC_BY_CODE == '' || !val.EXEC_BY_CODE) {
                        val['EXEC_BY_ID'] = val.APPROVER_ID;
                    }

                    if (val.ALT_APPROVER_CODE == '' || !val.ALT_APPROVER_CODE) {
                        val['ALT_APPROVER_ID'] = val.APPROVER_ID;
                    }

                    data.push(val);
                }


            });
            return AdminService.submitLvlData(data).then(function (res) {
                if (res.success === false) {
                    return vm.errors = res.errors;
                } else {
                    config.appToastMsg(res.vMsg);
                    $state.go('ActionType', { RF_ACTION_CFG_H_ID: data[0].RF_ACTION_CFG_H_ID }, { reload: true });
                }
            })
        }

        $scope.emoloyeeAuto = function (val) {
            return $http.get('/Hr/hrleavetrans/EmployeeAutoData', {
                params: {
                    pEMPLOYEE_CODE: val,
                    LK_GENDER_ID: null
                }
            }).then(function (response) {
                return response.data;
            });

        };


        $scope.onSelectItem1 = function (newitem, olditem,index) {
            olditem['APPROVER_CODE'] = newitem.EMPLOYEE_CODE;
            olditem['APPROVER_ID'] = newitem.HR_EMPLOYEE_ID;
            olditem['COMP_NAME_EN'] = newitem.COMP_NAME_EN;
            olditem['HR_COMPANY_ID'] = newitem.HR_COMPANY_ID;
            olditem['OFFICE_NAME_EN'] = newitem.OFFICE_NAME_EN;
            olditem['HR_OFFICE_ID'] = newitem.HR_OFFICE_ID;
            olditem['HR_DEPARTMENT_ID'] = newitem.CORE_DEPT_ID;
            olditem['DEPARTMENT_NAME_EN'] = newitem.DEPARTMENT_NAME_EN;
            vm.lvlDatas[index] = olditem;
        }

        $scope.onSelectItem2 = function (newitem, olditem, index) {
            olditem['EXEC_BY_CODE'] = newitem.EMPLOYEE_CODE;
            olditem['EXEC_BY_ID'] = newitem.HR_EMPLOYEE_ID;
            vm.lvlDatas[index] = olditem;
        }

        $scope.onSelectItem3 = function (newitem, olditem, index) {
            olditem['ALT_APPROVER_CODE'] = newitem.EMPLOYEE_CODE;
            olditem['ALT_APPROVER_ID'] = newitem.HR_EMPLOYEE_ID;
            console.log(olditem);
            vm.lvlDatas[index] = olditem;
        }




        function getCompanyList() {
            return vm.companyList = {
                optionLabel: "-- Select Company --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            AdminService.getCompanyData().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
        };


    }



})();