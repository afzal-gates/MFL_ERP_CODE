(function () {
    'use strict';

    angular.module('multitex.admin').controller('ActionConfigController', ['AdminService', 'logger', 'config', '$q', '$stateParams', '$state', '$http','$scope', ActionConfigController]);

    function ActionConfigController(AdminService, logger, config, $q, $stateParams, $state, $http, $scope) {

        var vm = this;
        vm.form = {};
      
        activate();

        function activate() {
            var promise = [getCompanyList(),floorList(), lineList(), ActionStatusData(), getOfficeList()];
            return $q.all(promise).then(function () {
            });
        }

        vm.form['RF_ACTION_CFG_H_ID'] = $stateParams.atData.RF_ACTION_CFG_H_ID;

        if ($stateParams.configData != null) {
            vm.form = $stateParams.configData;
        }

         vm.departmentList = {
               optionLabel: "-- Select Department --",
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

        function floorList() {
            return vm.floorListData = {
                optionLabel: "-- Select Floor --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrEmployee/LookupListData",  //+ "&pType=" + showType
                                params: {
                                    pLookupTableId: 18
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
                //select: function (e) {
                //    var dataItem = this.dataItem(e.item.index());
                //    vm.form.FLOOR_NAME = dataItem.LK_DATA_NAME_EN;
                //},
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function lineList() {
            return vm.lineListData = {
                optionLabel: "-- Select Line --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrEmployee/LineListData"  //+ "&pType=" + showType                                
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
                dataTextField: "Text",
                dataValueField: "Value"
            };
        };

        vm.saveData = function (data, url,token,isUpdate) {
            console.log(data);
            AdminService.saveConfigData(data, url, token).then(function (res) {
                vm.errors = [];
                if (res.success === false) {
                    return vm.errors = res.errors;
                } else {
                    config.appToastMsg(res.vMsg);
                    $state.go('ActionType', { RF_ACTION_CFG_H_ID: data.RF_ACTION_CFG_H_ID }, {reload:true});
                }
            });
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

        $scope.onSelectItem = function (item) {
            vm.form['HR_EMPLOYEE_ID'] = item.HR_EMPLOYEE_ID;
            vm.showEmployeeInfo = true;
            vm.employee = item;
        }




    }



})();