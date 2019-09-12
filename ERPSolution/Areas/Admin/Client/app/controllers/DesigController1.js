(function () {
    'use strict';

    angular.module('multitex.admin').controller('DesigController', ['logger', 'config', '$q', '$scope', '$http', DesigController]);

    function DesigController(logger, config, $q, $scope, $http) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.showSplash = true;
        vm.editData = [];

        vm.fromDate = null;
        vm.toDate = null;

        vm.enable = false;
        vm.DesignationId = 0;
       
        vm.departmentList = {
            //optionLabel: "Select",
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
        
        
        
        vm.designationGrpList = {
            //optionLabel: "Select",
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

        $scope.OnClickUpdate = function () {
            return $http.get('/Hr/Admin/HrDesignation/Save?');
        }

        ///////////////////////////////////////////////////////////////////////
        $scope.OnChangeHR_DEPARTMENT_ID = function () {
            sessionStorage.setItem("DepartmentID", $scope.HR_DEPARTMENT_ID);
        }        

        if (sessionStorage.getItem("DepartmentID") == null) {
            vm.DepartmentID = 0;
        }
        else {
            vm.DepartmentID = sessionStorage.getItem("DepartmentID");
            $scope.HR_DEPARTMENT_ID = vm.DepartmentID;
        }
        /////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////
        $scope.OnChangeHR_DESIGNATION_GRP_ID = function () {
            sessionStorage.setItem("DesigGrpID", $scope.HR_DESIGNATION_GRP_ID);
        }

        if (sessionStorage.getItem("DesigGrpID") == null) {
            vm.DesigGrpID = 0;
        }
        else {
            vm.DesigGrpID = sessionStorage.getItem("DesigGrpID");
            $scope.HR_DESIGNATION_GRP_ID = vm.DesigGrpID;
        }
        ///////////////////////////////////////////////////////////

        $scope.TRAN_MODE = 0;

        //$("#grid").kendoGrid({
        //    dataSource: {
        //        transport: {
        //            read: {
        //                url: "/Admin/HrDesignation/DesignationListData?pShowDepartment=" + vm.DepartmentID + "&pType=1",
        //                dataType: "json",
        //            },
        //            update: {
        //                url: "/Admin/HrDesignation/Edit",
        //                contentType: "application/json",
        //                accepts: "application/json",
        //                type: "put",

        //                dataType: "json"
        //            },
        //            destroy: {
        //                url: "/Products/Destroy",
        //                dataType: "jsonp"
        //            },
        //            create: {
        //                url: "/Admin/HrYearlyCalander/Save",
        //                contentType: "application/json",
        //                accepts: "application/json",
        //                type: "put",

        //                dataType: "json"
        //            },
        //            parameterMap: function (data, type) {
        //                if (type == "update" && data) {
        //                    return JSON.stringify(data);
        //                }
        //                if (type == "create" && data) {
        //                    return JSON.stringify(data);
        //                }
        //            }
        //        },
        //        pageSize: 10,
        //        schema: {
        //            model: {
        //                id: "HR_DESIGNATION_ID",
        //                fields: {
        //                    DESIGNATION_CODE: { editable: false },
        //                    HR_DEPARTMENT_ID: {},
        //                    DESIGNATION_NAME_EN: {},
        //                    DESIGNATION_NAME_BN: {},
        //                    DESIGNATION_DESC: {},
        //                    DESIGNATION_SNAME: {},
        //                    HR_DESIGNATION_GRP_ID: {},
        //                    HR_MANAGEMENT_TYPE_ID: {},
        //                    IS_GAJETED: {},
        //                    HR_GRADE_ID: {},
        //                    DESIG_ORDER: {},
        //                    IS_ACTIVE: {},
        //                    TRAN_MODE:{}
        //                }
        //            }
        //        }
        //    },
        //    //height: 550,
        //    //groupable: true,
        //    filterable: {
        //        mode: "row"
        //    },
        //    sortable: true,
        //    pageable: {
        //        refresh: true,
        //        pageSizes: true,
        //        buttonCount: 5
        //    },
        //    //toolbar: ["create"],
        //    columns: [
        //    //{ field: "HR_YRLY_CALNDR_ID", title: "ID" },            
        //    { field: "DESIGNATION_CODE", title: "Code", type: "string", width: "150px" },
        //    { field: "DESIGNATION_NAME_EN", title: "Designation Name", type: "string", width: "200px" },
        //    { field: "DESIGNATION_SNAME", title: "Short Name", type: "string", width: "150px" },
        //    { field: "DESIG_GRP_NAME_EN", title: "Designation Group", type: "string", width: "200px" },            
        //    { field: "MNG_TYPE_NAME_EN", title: "Management Type", type: "string", width: "200px" },
        //    //{ field: "DESIG_ORDER", title: "Order", type: "string" },
        //    //{ field: "IS_GAJETED", title: "Gajeted?", type: "string" },
        //    //{ field: "IS_ACTIVE", title: "Active?", type: "string" },
        //    {
        //        command: [{
        //            name: "Edit",
        //            click: function (e) {
        //                // e.target is the DOM element representing the button
        //                var tr = $(e.target).closest("tr"); // get the current table row (tr)
        //                // get the data bound to the current table row
        //                var data = this.dataItem(tr);
        //                vm.editData = this.dataItem(tr);

        //                if (data != null) {
        //                    $scope.HR_DEPARTMENT_ID = data.HR_DEPARTMENT_ID;
        //                    $scope.HR_DESIGNATION_ID = data.HR_DESIGNATION_ID;
        //                    $scope.DESIGNATION_CODE = data.DESIGNATION_CODE;
        //                    $scope.DESIGNATION_NAME_EN = data.DESIGNATION_NAME_EN;
        //                    $scope.DESIGNATION_NAME_BN = data.DESIGNATION_NAME_BN;
        //                    $scope.DESIGNATION_SNAME = data.DESIGNATION_SNAME;
        //                    $scope.DESIGNATION_DESC = data.DESIGNATION_DESC;
        //                    $scope.HR_DESIGNATION_GRP_ID = data.HR_DESIGNATION_GRP_ID;
        //                    $scope.HR_MANAGEMENT_TYPE_ID = data.HR_MANAGEMENT_TYPE_ID;
        //                    $scope.HR_GRADE_ID = data.HR_GRADE_ID;
        //                    $scope.DESIG_ORDER = data.DESIG_ORDER;
        //                    $scope.IS_GAJETED = data.IS_GAJETED;
        //                    $scope.IS_ACTIVE = data.IS_ACTIVE;

        //                    $scope.TRAN_MODE = 2;

        //                    sessionStorage.setItem("DepartmentID", data.HR_DEPARTMENT_ID);

        //                    return $http.get('/Admin/HrDesignation/Index');
        //                }

        //            }
        //        }], title: "&nbsp;", width: "80px"
        //    }
        //    ],
        //    //editable: "popup"
        //});

        //alert(sessionStorage.getItem("DepartmentID"));


        $scope.ShowDesignationListTable = function () {
            $http.get("/Hr/Admin/HrDesignation/DesignationListData?pShowDepartment=" + $scope.HR_DEPARTMENT_ID + '&pType=1')

            //console.log(vm.desigList);
        }

        /////////////////////////////////////////////////////////////////

        $scope.TranCancel = function () {
            $scope.TRAN_MODE = 0;
            $scope.DESIGNATION_NAME_EN = "";
            $scope.DESIGNATION_NAME_BN = "";
            $scope.HR_DESIGNATION_ID = "";
            $scope.DESIGNATION_CODE = "";
            $scope.DESIGNATION_SNAME = "";
            $scope.DESIGNATION_DESC = "";
            $scope.HR_DESIGNATION_GRP_ID = "";
            $scope.HR_MANAGEMENT_TYPE_ID = "";
            $scope.HR_GRADE_ID = "";
            $scope.DESIG_ORDER = "";
            $scope.IS_GAJETED = "";
            $scope.IS_ACTIVE = "";
        }

        $scope.TranAddNew = function () {
            $scope.TRAN_MODE = 1;
            $scope.DESIGNATION_NAME_EN = "";
            $scope.DESIGNATION_NAME_BN = "";
            $scope.HR_DESIGNATION_ID = "";
            $scope.DESIGNATION_CODE = "";
            $scope.DESIGNATION_SNAME = "";
            $scope.DESIGNATION_DESC = "";
            $scope.HR_DESIGNATION_GRP_ID = "";
            $scope.HR_MANAGEMENT_TYPE_ID = "";
            $scope.HR_GRADE_ID = "";
            $scope.DESIG_ORDER = "";
            $scope.IS_GAJETED = "";
            $scope.IS_ACTIVE = "Y";
        }

        ///////// Start Click Show Button 
        $scope.ShowDesignationList = function () {

            alert('hh');

            $scope.TRAN_MODE = 0;
            $scope.DESIGNATION_NAME_EN = "";
            $scope.DESIGNATION_NAME_BN = "";
            $scope.HR_DESIGNATION_ID = "";
            $scope.DESIGNATION_CODE = "";
            $scope.DESIGNATION_SNAME = "";
            $scope.DESIGNATION_DESC = "";
            $scope.HR_DESIGNATION_GRP_ID = "";
            $scope.HR_MANAGEMENT_TYPE_ID = "";
            $scope.HR_GRADE_ID = "";
            $scope.DESIG_ORDER = "";
            $scope.IS_GAJETED = "";
            $scope.IS_ACTIVE = "";

            //alert($scope.ShowMonth);
            $("#grid").kendoGrid({
                dataSource: {
                    transport: {
                        read: {
                            url: "/Hr/Admin/HrDesignation/DesignationListData?pShowDepartment=" + $scope.HR_DEPARTMENT_ID + "&pType=1",
                            dataType: "json",
                        },
                        update: {
                            url: "/Hr/Admin/HrDesignation/Edit",
                            contentType: "application/json",
                            accepts: "application/json",
                            type: "put",

                            dataType: "json"
                        },
                        destroy: {
                            url: "/Products/Destroy",
                            dataType: "jsonp"
                        },
                        create: {
                            url: "/Hr/Admin/HrYearlyCalander/Save",
                            contentType: "application/json",
                            accepts: "application/json",
                            type: "put",

                            dataType: "json"
                        },
                        parameterMap: function (data, type) {
                            if (type == "update" && data) {
                                return JSON.stringify(data);
                            }
                            if (type == "create" && data) {
                                return JSON.stringify(data);
                            }
                        }
                    },
                    pageSize: 10,
                    schema: {
                        model: {
                            id: "HR_DESIGNATION_ID",
                            fields: {
                                DESIGNATION_CODE: { editable: false },
                                HR_DEPARTMENT_ID: {},
                                DESIGNATION_NAME_EN: {},
                                DESIGNATION_NAME_BN: {},
                                DESIGNATION_DESC: {},
                                DESIGNATION_SNAME: {},
                                HR_DESIGNATION_GRP_ID: {},
                                HR_MANAGEMENT_TYPE_ID: {},
                                IS_GAJETED: {},
                                HR_GRADE_ID: {},
                                DESIG_ORDER: {},
                                IS_ACTIVE: {},
                                TRAN_MODE:{}
                            }
                        }
                    }
                },
                //height: 550,
                //groupable: true,
                filterable: {
                    mode: "row"
                },
                sortable: true,
                pageable: {
                    refresh: true,
                    pageSizes: true,
                    buttonCount: 5
                },
                //toolbar: ["create"],
                columns: [
                //{ field: "HR_YRLY_CALNDR_ID", title: "ID" },            
                { field: "DESIGNATION_CODE", title: "Code", type: "string", width: "150px" },
                { field: "DESIGNATION_NAME_EN", title: "Designation Name", type: "string", width: "200px" },
                //{ field: "DESIGNATION_SNAME", title: "Short Name", type: "string", width: "150px" },
                { field: "DESIG_GRP_NAME_EN", title: "Designation Group", type: "string", width: "200px" },
                { field: "MNG_TYPE_NAME_EN", title: "Management Type", type: "string", width: "200px" },
                //{ field: "DESIG_ORDER", title: "Order", type: "string" },
                //{ field: "IS_GAJETED", title: "Gajeted?", type: "string" },
                //{ field: "IS_ACTIVE", title: "Active?", type: "string" },
                {
                    command: [{

                        template: '<button type="button" data-dismiss="modal" class="btn default" ng-click="test()">Close</button>'
                        //name : "Edit",
                        //click: function (e) {

                        //    // e.target is the DOM element representing the button
                        //    var tr = $(e.target).closest("tr"); // get the current table row (tr)
                        //    // get the data bound to the current table row
                        //    var data = this.dataItem(tr);
                        //    vm.editData = this.dataItem(tr);

                        //    if (data != null) {
                        //        $scope.HR_DEPARTMENT_ID = data.HR_DEPARTMENT_ID;
                        //        $scope.HR_DESIGNATION_ID = data.HR_DESIGNATION_ID;
                        //        $scope.DESIGNATION_CODE = data.DESIGNATION_CODE;
                        //        $scope.DESIGNATION_NAME_EN = data.DESIGNATION_NAME_EN;
                        //        $scope.DESIGNATION_NAME_BN = data.DESIGNATION_NAME_BN;
                        //        $scope.DESIGNATION_SNAME = data.DESIGNATION_SNAME;
                        //        $scope.DESIGNATION_DESC = data.DESIGNATION_DESC;
                        //        $scope.HR_DESIGNATION_GRP_ID = data.HR_DESIGNATION_GRP_ID;
                        //        $scope.HR_MANAGEMENT_TYPE_ID = data.HR_MANAGEMENT_TYPE_ID;
                        //        $scope.HR_GRADE_ID = data.HR_GRADE_ID;
                        //        $scope.DESIG_ORDER = data.DESIG_ORDER;
                        //        $scope.IS_GAJETED = data.IS_GAJETED;
                        //        $scope.IS_ACTIVE = data.IS_ACTIVE;

                        //        $scope.TRAN_MODE = 2;

                        //        sessionStorage.setItem("DepartmentID", data.HR_DEPARTMENT_ID);                                
                                
                        //        return $http.get('/Admin/HrDesignation/Edit');
                        //    }                            
                        //}
                        
                    }], title: "Action", width: "80px"
                }
                ],
                //editable: "popup"
            });
            
        };


        $scope.test = function () {
            $scope.DESIGNATION_NAME_EN = "Maruf";
        }

        ///////// End Click Show Button 

        

        function desigGrpDropDownEditor(container, options) {
            $('<input required data-text-field="DESIG_GRP_NAME_EN" data-value-field="HR_DESIGNATION_GRP_ID" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({
                    optionLabel: "Select",
                    autoBind: true,
                    dataSource: {
                        transport: {
                            read: {
                                url: "/Hr/Admin/HrDesignation/DesignationGrpListData",
                                dataType: "json"
                            }
                        }                        
                    }
                });
        }

        


        function activate(){
            var promise = [];
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