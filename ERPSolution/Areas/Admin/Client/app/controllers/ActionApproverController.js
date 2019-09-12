(function () {
    'use strict';

    angular.module('multitex.admin').controller('ActionApproverController', ['AdminService', 'logger', 'config', '$q', '$stateParams', '$state', '$http', '$scope', ActionApproverController]);

    function ActionApproverController(AdminService, logger, config, $q, $stateParams, $state, $http, $scope) {

        var vm = this;
        vm.form = {};
        activate();

        function activate() {
            var promise = [getOfficeList(), getCompanyList(), ActionStatusData()];
            return $q.all(promise).then(function () {
            });
        }

        vm.form['RF_ACTION_CFG_H_ID'] = $stateParams.atData.RF_ACTION_CFG_H_ID;

        if ($stateParams.configData != null) {
            var data = $stateParams.configData;
            data['EMPLOYEE_CODE2'] = data.EXEC_BY_CODE;
            data['EMPLOYEE_CODE'] = data.APPROVER_CODE;
            data['EXE_BY'] = data.EXEC_BY_ID ? 'Other' : 'Self';

            vm.form = data;
        } else {
            vm.form['EXE_BY'] = 'Self';
        }

    
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
                    console.log(this.dataItem(e.item));
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


             vm.subdepartmentListData = {
                 optionLabel: "-- Select Section --",
                 filter: "startswith",
                 autoBind: true,
                 dataSource: {
                     transport: {
                         read: function (e) {
                             AdminService.getSubDepartmentData().then(function (res) {
                                 e.success(res);
                             });
                         }
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

        function ActionStatusData() {
            return vm.actionStatusData = {
                optionLabel: "-- Select Action Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrEmployee/LookupListData",  //+ "&pType=" + showType
                                params: {
                                    pLookupTableId: $stateParams.atData.LK_STATUS_TBL_ID
                                }
                            }).
                            success(function (data, status, headers, config) {
                                e.success(data)
                            }).
                            error(function (data, status, headers, config) {
                                alert('something went wrong')
                                console.log(status);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };



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

        $scope.onSelectItem = function (item) {
            vm.form['APPROVER_ID'] = item.HR_EMPLOYEE_ID;
            vm.form['HR_COMPANY_ID'] = item.HR_COMPANY_ID;
            vm.form['HR_OFFICE_ID'] = item.HR_OFFICE_ID;
            vm.form['HR_DEPARTMENT_ID'] = item.CORE_DEPT_ID!=0?item.CORE_DEPT_ID:item.HR_DEPARTMENT_ID;


            vm.showEmployeeInfo = true;
            vm.employee = item;
        }


        $scope.onSelectItem2 = function (item) {
            console.log(item);
            vm.onDutyEmployee = item;
            vm.form['EXEC_BY_ID'] = item.HR_EMPLOYEE_ID;
        }

        vm.SubmitLvl = function (data, url, token, isUpdate) {
            if (data.EXE_BY == 'Self') {
                data['EXEC_BY_ID'] = null;
            }

            data['IS_LEAF'] = !data.IS_LEAF ? 'N' : data.IS_LEAF;
            data['IS_AUTO_APRVD'] = !data.IS_AUTO_APRVD ? 'N' : data.IS_AUTO_APRVD;
            data['IS_NOTIFY_EMAIL'] = !data.IS_NOTIFY_EMAIL ? 'N' : data.IS_NOTIFY_EMAIL;


            AdminService.SubmitLvlData(data, url, token).then(function (res) {
                vm.errors = [];
                if (res.success === false) {
                    return vm.errors = res.errors;
                } else {
                    config.appToastMsg(res.vMsg);
                    $state.go('ActionType', { RF_ACTION_CFG_H_ID: data.RF_ACTION_CFG_H_ID }, { reload: true });
                }
            });
        }


    }



})();