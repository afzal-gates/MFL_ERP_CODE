(function () {
    'use strict';

    angular.module('multitex.admin').controller('DesigController', ['logger', 'config', '$q', '$scope', '$http', DesigController]);

    function DesigController(logger, config, $q, $scope, $http) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.showSplash = true;
        vm.insert = true;
        vm.editData = [];
        
        vm.errors = [];
        vm.form = { IS_GAJETED: 'N', IS_ACTIVE: 'Y' };
        //vm.HR_DESIGNATION_GRP_ID == null;
        //vm.HR_MANAGEMENT_TYPE_ID == null;

        vm.reset = function () {
            vm.insert = true;
            vm.formCopy = vm.form;
            vm.form = {};
            vm.form = { IS_GAJETED: 'N', IS_ACTIVE: 'Y', HR_DEPARTMENT_ID: vm.formCopy.HR_DEPARTMENT_ID, HR_DESIGNATION_GRP_ID: vm.formCopy.HR_DESIGNATION_GRP_ID, HR_MANAGEMENT_TYPE_ID: vm.formCopy.HR_MANAGEMENT_TYPE_ID, HR_GRADE_ID: vm.formCopy.HR_GRADE_ID };
        };

        vm.TranCancel = function () {
            vm.reset();
        };

        vm.editRecord = function (dataItem) {
            vm.insert = false;
            
            vm.form = dataItem;
            
        };
       

        vm.departmentList = {
            optionLabel: "Select",
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
            dataTextField: "DEPARTMENT_NAME_EN",
            dataValueField: "HR_DEPARTMENT_ID"
        };

        vm.departmentListTreeData = [];
        function departmentListTree() {
            return $http({
                url: '/Hr/Admin/HrDesignation/DepartmentListData',
                method: 'get'
            })
                .then(function (result) {
                    vm.departmentListTreeData = result.data;
                    return;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
                        
            //console.log(vm.departmentListTreeData);
        };

        

        vm.designationGrpList = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: {
                        url: "/Hr/Admin/HrDesignation/DesignationGrpListData",
                        dataType: "json"
                    }
                }
            },
            dataTextField: "DESIG_GRP_NAME_EN",
            dataValueField: "HR_DESIGNATION_GRP_ID"
        };

        vm.managementTypeList = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: {
                        url: "/Hr/Admin/HrDesignation/ManagementTypeListData",
                        dataType: "json"
                    }
                }
            },
            dataTextField: "MNG_TYPE_NAME_EN",
            dataValueField: "HR_MANAGEMENT_TYPE_ID"
        };
             
        vm.gradeList = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: {
                        url: "/Hr/Admin/HrDesignation/GradeListData",
                        dataType: "json"
                    }
                }
            },
            dataTextField: "GRADE_NO_EN",
            dataValueField: "HR_GRADE_ID"
        };

        //////////////////////////////////////////////////////////////////

        vm.submit = function (isValid, form, insert) {
            
            if (insert) {
                $http({
                    headers: { "RequestVerificationToken": vm.antiForgeryToken },
                    url: '/Hr/Admin/HrDesignation/SaveD',
                    method: 'post',
                    data: form
                }).success(function (data, status, headers, config1) {
                   
                    //vm.errors = undefined;
                    if (data.success === false) {                        
                        //console.log(data.errors);
                        vm.errors = data.errors;
                    }
                    else {                        
                        config.appToastMsg(data.vMsg);
                        vm.errors = [];
                        vm.reset();
                        //$state.go('ImportRawdata.List');
                    }

                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
            else {
                $http({
                    headers: { "RequestVerificationToken": vm.antiForgeryToken },
                    url: '/Hr/Admin/HrDesignation/UpdateD',
                    method: 'post',
                    data: form
                }).success(function (data, status, headers, config1) {

                    //vm.errors = undefined;
                    if (data.success === false) {
                        //console.log(data.errors);
                        vm.errors = data.errors;
                    }
                    else {
                        config.appToastMsg(data.vMsg);
                        vm.errors = [];
                        vm.reset();
                        //$state.go('ImportRawdata.List');
                    }

                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            }

        };

        

        ///////////////////////////////////////////////////////////////////////
        //$scope.OnChangeHR_DEPARTMENT_ID = function () {
        //    sessionStorage.setItem("DepartmentID", $scope.HR_DEPARTMENT_ID);
        //}        

        //if (sessionStorage.getItem("DepartmentID") == null) {
        //    vm.DepartmentID = 0;
        //}
        //else {
        //    vm.DepartmentID = sessionStorage.getItem("DepartmentID");
        //    $scope.HR_DEPARTMENT_ID = vm.DepartmentID;
        //}
        /////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////
        //$scope.OnChangeHR_DESIGNATION_GRP_ID = function () {
        //    sessionStorage.setItem("DesigGrpID", $scope.HR_DESIGNATION_GRP_ID);
        //}

        //if (sessionStorage.getItem("DesigGrpID") == null) {
        //    vm.DesigGrpID = 0;
        //}
        //else {
        //    vm.DesigGrpID = sessionStorage.getItem("DesigGrpID");
        //    $scope.HR_DESIGNATION_GRP_ID = vm.DesigGrpID;
        //}
        ///////////////////////////////////////////////////////////
        
        //$scope.ShowDesignationListTable = function () {
        //    $http.get("/Admin/HrDesignation/DesignationListData?pShowDepartment=" + sessionStorage.getItem("DesigGrpID") + '&pType=1')

        //    //console.log(vm.desigList);
        //}

        /////////////////////////////////////////////////////////////////
    //    $scope.gridOptions = {
    //        autoBind: true,
    //        dataSource: {
    //            transport: {
    //                read: function (e) {
    //                    $http({
    //                        params: {
    //                            pShowDepartment: sessionStorage.getItem("sDesigGrpId"),
    //                            pType: sessionStorage.getItem("sReturnDataTypeId")
    //                        },
    //                        method: 'post',
    //                        url: "/Admin/HrDesignation/DesignationListData"  //+ "&pType=" + showType
    //                    }).
    //                    success(function (data, status, headers, config) {
    //                        e.success(data)
    //                    }).
    //                    error(function (data, status, headers, config) {
    //                        alert('something went wrong')
    //                        console.log(status);
    //                    });
    //                }
    //                //read: {
    //                //    url: "/Admin/HrDesignation/DesignationListData?pShowDepartment=" + showDesigGrpId + "&pType=" + showType,
    //                //    dataType: "json",
    //                //}
    //            }
    //        },
    //        filterable: {
    //            mode: "row"
    //        },
    //        selectable: "row",
    //        sortable: true,
    //        pageSize: 10,
    //        pageable: {
    //            refresh: true,
    //            pageSizes: true//,
    //            //buttonCount: 5
    //        },
    //        columns: [
    //            {
    //                title: "Action",
    //                template: function () {
    //                    return "<a ng-click='tabs[0].active = true;clicked(dataItem)'><i class='fa fa-edit'></i></a>";
    //                },
    //                width: "50px"
    //            },
    //            //{ field: "HR_YRLY_CALNDR_ID", title: "ID" },            
    //            //{ field: "DESIGNATION_CODE", title: "Code", type: "string", width: "150px" },
    //            { field: "DESIGNATION_NAME_EN", title: "Designation Name", type: "string", width: "200px" },
    //            { field: "DESIGNATION_SNAME", title: "Short Name", type: "string", width: "150px" },
    //            { field: "DESIG_GRP_NAME_EN", title: "Designation Group", type: "string", width: "200px" },
    //            { field: "MNG_TYPE_NAME_EN", title: "Management Type", type: "string", width: "200px" },
    //            { field: "GRADE_NO_EN", title: "Grade", type: "string", width: "100px" },
    //            { field: "IS_GAJETED", title: "Gajeted?", type: "string", width: "100px" },
    //            { field: "IS_ACTIVE", title: "Active?", type: "string", width: "100px" }
    //        ]
    //    };
            
    //}
        /////////////////////////////////////////////////////////////////
             

        $scope.TRAN_MODE = 1;

        $scope.tabs = [
        { title: 'Entry', content: 'Dynamic content 1' },
        { title: 'List', content: 'Dynamic content 2' }                
        //{ title: 'Dynamic Title 3', content: 'Dynamic content 3' }
        ];
                
        $scope.onClickShowList = function (showDesigGrpId, showType) {
            
            $scope.listShow = 1;

            sessionStorage.setItem("sDesigGrpId", showDesigGrpId);
            sessionStorage.setItem("sReturnDataTypeId", showType);

            //if (showType == 1) {
            //    sessionStorage.setItem("sDeptID", showDesigGrpId);
            //}
            //else if (showType == 2) {
            //    sessionStorage.setItem("sDesigGrpID", showDesigGrpId);
            //}
            //else if (showType == 3) {
            //    sessionStorage.setItem("sManagTypeID", showDesigGrpId);
            //}
            
            
            $scope.gridOptions = {
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                params: {
                                    pShowDepartment: showDesigGrpId,
                                    pType: showType
                                },
                                method: 'post',
                                url: "/Hr/Admin/HrDesignation/DesignationListData"  //+ "&pType=" + showType
                            }).
                            success(function (data, status, headers, config) {
                                e.success(data)
                            }).
                            error(function (data, status, headers, config) {
                                alert('something went wrong')
                                console.log(status);
                            });
                        }
                        //read: {
                        //    url: "/Admin/HrDesignation/DesignationListData?pShowDepartment=" + showDesigGrpId + "&pType=" + showType,
                        //    dataType: "json",
                        //}
                    },
                    pageSize: 10
                },
                filterable: {
                    mode: "row"
                },
                selectable: "row",
                sortable: true,                
                pageable: {
                    refresh: true,
                    pageSizes: true//,
                    //buttonCount: 5
                },
                columns: [                    
                    { field: "DESIGNATION_NAME_EN", title: "Designation Name", type: "string", width: "200px" },
                    //{ field: "DESIGNATION_SNAME", title: "Short Name", type: "string", width: "150px" },
                    { field: "DESIG_GRP_NAME_EN", title: "Designation Group", type: "string", width: "200px" },
                    { field: "MNG_TYPE_NAME_EN", title: "Management Type", type: "string", width: "200px" },
                    { field: "GRADE_NO_EN", title: "Grade", type: "string", width: "100px" },
                    { field: "IS_GAJETED", title: "Gajeted?", type: "string", width: "100px" },
                    { field: "IS_ACTIVE", title: "Active?", type: "string", width: "100px" },
                    {
                        title: "Action",
                        template: function () {
                            return "<a ng-click='tabs[0].active = true;vm.editRecord(dataItem)'><i class='fa fa-edit'></i></a>";
                        },
                        width: "50px"
                    }
                ]
            };
            
        }

        //$scope.HR_DEPARTMENT_ID = "";
        //$scope.HR_DESIGNATION_ID = "";
        //$scope.DESIGNATION_CODE = "";
        //$scope.DESIGNATION_NAME_EN = "";
        //$scope.DESIGNATION_NAME_BN = "";
        //$scope.DESIGNATION_SNAME = "";
        //$scope.DESIGNATION_DESC = "";
        //vm.HR_DEPARTMENT_ID = "";
        //vm.HR_DESIGNATION_GRP_ID = "";
        //vm.HR_MANAGEMENT_TYPE_ID = "";
        //vm.HR_GRADE_ID = "";
        //$scope.DESIG_ORDER = "";
        //$scope.IS_GAJETED = "";
        //$scope.IS_ACTIVE = "";

        $scope.clicked = function (item) {                        

            $scope.TRAN_MODE = 2;

            //$scope.DESIGNATION_NAME_EN = angular.copy($scope.originalDESIGNATION_NAME_EN);

            $scope.HR_DEPARTMENT_ID = item.HR_DEPARTMENT_ID;
            $scope.HR_DESIGNATION_ID = item.HR_DESIGNATION_ID;
            $scope.DESIGNATION_CODE = item.DESIGNATION_CODE;
            $scope.DESIGNATION_NAME_EN = item.DESIGNATION_NAME_EN;
            $scope.DESIGNATION_NAME_BN = item.DESIGNATION_NAME_BN;
            $scope.DESIGNATION_SNAME = item.DESIGNATION_SNAME;
            $scope.DESIGNATION_DESC = item.DESIGNATION_DESC;
            vm.HR_DEPARTMENT_ID = item.HR_DEPARTMENT_ID;
            vm.HR_DESIGNATION_GRP_ID = item.HR_DESIGNATION_GRP_ID;
            vm.HR_MANAGEMENT_TYPE_ID = item.HR_MANAGEMENT_TYPE_ID;
            vm.HR_GRADE_ID = item.HR_GRADE_ID;
            $scope.DESIG_ORDER = item.DESIG_ORDER;
            $scope.IS_GAJETED = item.IS_GAJETED;
            $scope.IS_ACTIVE = item.IS_ACTIVE;
        };
        
        $scope.TranCancel = function () {
            
            //$state.transitionTo($state.current, angular.copy($stateParams), { reload: true, inherit: true, notify: true });
            //alert('x');
            $scope.TRAN_MODE = 1;
            
            $scope.HR_DESIGNATION_ID = '';
            $scope.DESIGNATION_CODE = '';
            $scope.DESIGNATION_NAME_EN = '';
            $scope.DESIGNATION_NAME_BN = '';
            $scope.DESIGNATION_SNAME = '';
            $scope.DESIGNATION_DESC = '';

            //vm.HR_DEPARTMENT_ID = item.HR_DEPARTMENT_ID;
            //vm.HR_DESIGNATION_GRP_ID = item.HR_DESIGNATION_GRP_ID;
            //vm.HR_MANAGEMENT_TYPE_ID = item.HR_MANAGEMENT_TYPE_ID;
            //vm.HR_GRADE_ID = item.HR_GRADE_ID;

            //$scope.DESIG_ORDER = null;
            $scope.IS_GAJETED = '';
            $scope.IS_ACTIVE = '';
        };

        
        function activate(){
            var promise = [departmentListTree()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        //function getData(){
        //     return AdminService.getData().then(function (data) {
        //         //vm.schedulerOptions.dataSource.data = data;
        //         //return vm.schedulerOptions.dataSource.data;
           
        //    });
        //}

 
        

            
        
    }

    

})();