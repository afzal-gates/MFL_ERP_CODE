(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrCompanyController', ['logger', 'config', '$q', '$scope', '$http', HrCompanyController]);

    function HrCompanyController(logger, config, $q, $scope, $http) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.showSplash = true;
        
        vm.insert = true;        
        

        vm.TranCancel = function () {            
            vm.insert = true;
            vm.reset();
        };

        vm.reset = function () {
            //$scope.frmCompany.submitted = false;
            
            vm.insert = true;
            vm.form = {IS_ACTIVE:'Y', HR_COMPANY_GRP_ID:1};                        
        };

        vm.editRecord = function (dataItem) {
            vm.insert = false;

            vm.form = dataItem;            
        };

        vm.submit = function (isValid, form, insert) {

            //console.log(insert);

            if (!isValid) return;

            //alert('ok');            

            //submit the data to the server
            if (insert) {

                $http.post('/Hr/HrCompany/Save', vm.form).success(function (data, status, headers, config) {



                    if (data != '') {
                        if (data.substr(0, 9) == 'MULTI-001') {
                            toastr.success(data.substr(9), "MultiTEX");
                            vm.reset();
                            //vm.TRAN_MODE = 1;
                        }
                        if (data.substr(0, 9) == 'MULTI-002') {
                            toastr.info(data.substr(9), "MultiTEX");
                        }
                        if (data.substr(0, 9) == 'MULTI-003') {
                            toastr.warning(data.substr(9), "MultiTEX");
                        }
                        if (data.substr(0, 9) == 'MULTI-005') {
                            toastr.error(data.substr(9), "MultiTEX");
                        }

                    }
                }).
                error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });
            
            }
            else {

                $http.post('/Hr/HrCompany/Update', vm.form).success(function (data, status, headers, config) {

                    if (data != '') {
                        if (data.substr(0, 9) == 'MULTI-001') {
                            toastr.success(data.substr(9), "MultiTEX");
                            //$window.location.reload();
                            vm.reset();
                            //vm.TRAN_MODE = 1;
                        }
                        if (data.substr(0, 9) == 'MULTI-002') {
                            toastr.info(data.substr(9), "MultiTEX");
                        }
                        if (data.substr(0, 9) == 'MULTI-003') {
                            toastr.warning(data.substr(9), "MultiTEX");
                        }
                        if (data.substr(0, 9) == 'MULTI-005') {
                            toastr.error(data.substr(9), "MultiTEX");
                        }

                    }
                }).
                error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });

            }

            return;

        };


        vm.save = function () {
            //$http({                               
            //    data: {
            //        TRAN_MODE : vm.TRAN_MODE,
            //        HR_COMPANY_ID : vm.HR_COMPANY_ID,                    
            //        COMP_CODE : vm.COMP_CODE,                        
            //        COMP_NAME_EN : vm.COMP_NAME_EN,
            //        COMP_NAME_BN : vm.COMP_NAME_BN,
            //        COMP_SNAME : vm.COMP_SNAME,
            //        COMP_DESC : vm.COMP_DESC,
            //        LK_COMP_TYPE_ID : vm.LK_COMP_TYPE_ID,
            //        LK_BUS_TYPE_ID : vm.LK_BUS_TYPE_ID,
            //        HR_COMPANY_GRP_ID : vm.HR_COMPANY_GRP_ID,
            //        VAT_REG_NO : vm.VAT_REG_NO,
            //        YEAR_ESTD : vm.YEAR_ESTD,
            //        COMP_WEB : vm.COMP_WEB,
            //        IS_ACTIVE: vm.IS_ACTIVE == true ? 'Y' : 'N'                    
            //    },                
            //    method: 'post',
            //    url: "/Hr/HrCompany/Save"  //+ "&pType=" + showType
            //}).
            //success(function (data, status, headers, config) {
            //    //e.success(data)
            //    //console.log(data);
            //    //logger.success(data, null);
            //    if (data != '')
            //    {
            //        if (data.substr(0, 9) == 'MULTI-001') {
            //            toastr.success(data.substr(9), "MultiTEX");
            //            vm.reset();
            //            vm.TRAN_MODE = 1;
            //        }
            //        if (data.substr(0, 9) == 'MULTI-002') {
            //            toastr.info(data.substr(9), "MultiTEX");
            //        }
            //        if (data.substr(0, 9) == 'MULTI-003') {
            //            toastr.warning(data.substr(9), "MultiTEX");
            //        }
            //        if (data.substr(0, 9) == 'MULTI-005') {
            //            toastr.error(data.substr(9), "MultiTEX");
            //        }

            //    }
            //}).
            //error(function (data, status, headers, config) {
            //    alert('something went wrong')
            //    console.log(status);
            //});
        };


        vm.compGroupList = {
            //optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({                            
                            method: 'post',
                            url: "/Hr/HrCompany/CompanyGroupListData"  //+ "&pType=" + showType
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
            dataTextField: "COMP_GRP_NAME_EN",
            dataValueField: "HR_COMPANY_GRP_ID"
        };


        vm.compTypeList = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            params: {
                                pLookupTableId: 4,                                
                            },
                            method: 'post',
                            url: "/Hr/HrCompany/LookupListData"  //+ "&pType=" + showType
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
                            
        vm.businessTypeList = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            params: {
                                pLookupTableId: 5,
                            },
                            method: 'post',
                            url: "/Hr/HrCompany/LookupListData"  //+ "&pType=" + showType
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


        vm.tabs = [
        { title: 'Entry', content: 'Dynamic content 1' },
        { title: 'List', content: 'Dynamic content 2' }                        
        ];
                
        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            //params: {
                            //    pShowDepartment: showDesigGrpId,
                            //    pType: showType
                            //},
                            method: 'post',
                            url: "/Hr/HrCompany/CompanyListData"  //+ "&pType=" + showType
                        }).
                        success(function (data, status, headers, config) {
                            e.success(data)
                        }).
                        error(function (data, status, headers, config) {
                            alert('something went wrong')
                            console.log(status);
                        });
                    }                    
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
                pageSizes: true,
                buttonCount: 5
            },
            columns: [                
                { field: "COMP_CODE", title: "Code", type: "string", width: "100px" },
                { field: "COMP_NAME_EN", title: "Name [E]", type: "string", width: "200px" },
                { field: "COMP_TYPE_NAME", title: "Company Type", type: "string", width: "150px" },
                { field: "COMP_BUS_TYPE_NAME", title: "Business Type", type: "string", width: "150px" },
                { field: "COMP_WEB", title: "Web Site", type: "string", width: "200px" },
                {
                    title: "Action",
                    template: function () {
                        return "<a ng-click='vm.tabs[0].active = true;vm.editRecord(dataItem)' title='Edit'><i class='fa fa-edit'></i></a>";
                    },
                    width: "50px"
                }
            ]
        };

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