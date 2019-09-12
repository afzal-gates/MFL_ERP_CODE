
(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrOfficeController', ['logger', 'config', '$q', '$scope', '$http', '$location', '$state', 'HrService', HrOfficeController]);

    function HrOfficeController(logger, config, $q, $scope, $http, $location, $state, HrService) {




        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.showSplash = true;

        vm.insert = true;
        vm.form = {};

        vm.files = [];
        loadCompany();

        function loadCompany() {
            HrService.GetAllOthers('/api/hr/GetCompanyListByID?pHR_OFFICE_ID=' + (vm.form.HR_OFFICE_ID || 0)).then(function (res) {
                vm.companyList = res;
            });
        }

        vm.removeCompany = function (item) {
            if (!item.IS_SELECT) {
                item.IS_PRIMARY = "N";
            }
        }


        vm.deselectOther = function (idx) {
            for (var i = 0; i < vm.companyList.length; i++) {
                if (i != idx) {
                    vm.companyList[i].IS_PRIMARY = "N";
                }
            }
        }

        vm.TranCancel = function () {
            vm.reset();

        };

        vm.reset = function () {
            vm.insert = true;
            vm.formCopy = vm.form;
            vm.form = { IS_ACTIVE: 'Y' };

            vm.form.HR_COMPANY_GRP_ID = vm.formCopy.HR_COMPANY_GRP_ID;
            vm.form.LK_OFF_TYPE_ID = vm.formCopy.LK_OFF_TYPE_ID;
            vm.form.HR_TIMEZONE_ID = vm.formCopy.HR_TIMEZONE_ID;
            vm.form.HR_GEO_DISTRICT_ID = vm.formCopy.HR_GEO_DISTRICT_ID;
            vm.form.HR_COUNTRY_ID = vm.formCopy.HR_COUNTRY_ID;
            loadCompany();

        };


        vm.editRecord = function (dataItem) {
            vm.insert = false;

            //$routeParams = dataItem;
            //$location.path('/OfficeEntry');

            //console.log($routeParams);
            vm.form = dataItem;
            //console.log(dataItem);
            loadCompany();
            return $state.go('OfficeEntry');
        };



        vm.submit = function (isValid, form, insert) {

            //console.log(insert);

            if (!isValid) return;
            var is_def = false;
            for (var i = 0; i < vm.companyList.length; i++) {
                if (vm.companyList[i].IS_PRIMARY == "Y") {
                    is_def = true;
                }
            }

            if (!is_def) {
                toastr.error('Please select primary company!', "MultiTEX");
                return;
            }

            var List = _.filter(vm.companyList, function (x) { return x.IS_SELECT == true });

            form["XML_COMP"] = HrService.xmlStringShort(List.map(function (o) {
                return {
                    HR_COMP_OFF_ID: o.HR_COMP_OFF_ID == null ? 0 : o.HR_COMP_OFF_ID,
                    HR_COMPANY_ID: o.HR_COMPANY_ID == null ? 0 : o.HR_COMPANY_ID,
                    HR_OFFICE_ID: vm.form.HR_OFFICE_ID,
                    IS_PRIMARY: o.IS_PRIMARY == null ? "N" : o.IS_PRIMARY,
                    IS_ACTIVE: "Y"
                }
            }));

            //submit the data to the server
            if (insert) {

                $http({
                    method: 'post',
                    url: '/Hr/HrOffice/Save',
                    data: form,
                    headers: { "RequestVerificationToken": vm.antiForgeryToken }
                }).success(function (data, status, headers, config) {

                    //$location.path('/');
                    //$location.path('/OfficeEntry');                    

                    if (data != '') {
                        if (data.substr(0, 9) == 'MULTI-001') {
                            toastr.success(data.substr(9), "MultiTEX");
                            vm.reset();
                            //$location.path('/OfficeEntry');
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

                        loadCompany();

                    }
                }).
                error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });

            }
            else {
                $http({
                    method: 'post',
                    url: '/Hr/HrOffice/Update',
                    data: form,
                    headers: { "RequestVerificationToken": vm.antiForgeryToken }
                }).success(function (data, status, headers, config) {

                    //console.log($scope);
                    //$scope.$digest();                    

                    if (data != '') {
                        if (data.substr(0, 9) == 'MULTI-001') {
                            toastr.success(data.substr(9), "MultiTEX");

                            vm.reset();
                            return $state.go('OfficeEntry');

                            //$location.path('/OfficeEntry');
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
                        loadCompany();

                    }
                }).
                error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });

            }

            return;

        };


        vm.countryList = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Hr/HrOffice/CountryListData"  //+ "&pType=" + showType
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
            dataTextField: "COUNTRY_NAME_EN",
            dataValueField: "HR_COUNTRY_ID"
        };


        vm.compGroupList = {
            //optionLabel: "Select",
            //filter: "startswith",
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


        vm.timezoneList = {
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
                            url: "/Hr/HrOffice/TimezoneListData"  //+ "&pType=" + showType
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
            dataTextField: "TIMEZONE_TEXT",
            dataValueField: "HR_TIMEZONE_ID"
        };


        vm.districtList = {
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
                            url: "/Hr/HrOffice/DistrictListData"  //+ "&pType=" + showType
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
            dataTextField: "DISTRICT_NAME_EN",
            dataValueField: "HR_GEO_DISTRICT_ID"
        };


        vm.officeTypeList = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            params: {
                                pLookupTableId: 6,
                            },
                            method: 'post',
                            url: "/Hr/HrOffice/LookupListData"  //+ "&pType=" + showType
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


        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Hr/HrOffice/OfficeListData"  //+ "&pType=" + showType
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
            pageSize: 10,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [
                //{ field: "HR_YRLY_CALNDR_ID", title: "ID" },            
                //{ field: "DESIGNATION_CODE", title: "Code", type: "string", width: "150px" },
                { field: "OFFICE_CODE", title: "Code", type: "string", width: "100px" },
                { field: "OFFICE_NAME_EN", title: "Name", type: "string", width: "200px" },
                { field: "OFFICE_TYPE_NAME_EN", title: "Office Type", type: "string", width: "150px" },
                { field: "ADDRESS_EN", title: "Address", type: "string", width: "200px" },
                { field: "PHONE_NO", title: "Phone#", type: "string", width: "150px" },
                //{ field: "HR_COMPANY_GRP_NAME_EN", title: "Group Name", type: "string", width: "200px" },

                //{ field: "COMP_BUS_TYPE_NAME", title: "Business Type", type: "string", width: "150px" },
                //{ field: "VAT_REG_NO", title: "Vat Registation#", type: "string", width: "150px" },
                //{ field: "YEAR_ESTD", title: "Establish Year", type: "string", width: "100px" },
                //{ field: "COMP_WEB", title: "Web Site", type: "string", width: "200px" },
                //{ field: "COMP_DESC", title: "Description", type: "string", width: "300px" },

                //{ field: "IS_ACTIVE", title: "Active?", type: "string", width: "100px" }
                {
                    title: "Action",
                    template: function () {
                        return "<a ng-click='vm.tabs[0].active = true;vm.editRecord(dataItem)' title='Edit'><i class='fa fa-edit'></i></a>";
                        //return "<a ng-click='vm.editRecord(dataItem)' title='Edit'><i class='fa fa-edit'></i></a>";
                    },
                    width: "50px"
                }
            ]
        };


        vm.tabs = [
        { title: 'Entry', content: "<h3>Dynamic content 1</h3>", url: 'OfficeEntry' },
        { title: 'List', content: 'Dynamic content 2', url: 'OfficeList' }
        //{ title: 'Dynamic Title 3', content: 'Dynamic content 3' }
        ];


        function activate() {
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