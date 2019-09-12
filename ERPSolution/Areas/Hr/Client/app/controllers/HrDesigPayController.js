(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrDesigPayController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'HrService', HrDesigPayController]);

    function HrDesigPayController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, HrService) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.dtFormat = config.appDateFormat;
        vm.timeFormat = config.appTimeFormat;
        vm.showSplash = true;
        
        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
            function (e) {
                e.preventDefault();
            });

        vm.insert = true;
        vm.today = new Date();                
        
        vm.isDesigPayShow = false;

        vm.reset = function () {
            vm.form = {};            
            vm.insert = true;
        };

        vm.next = function () {
            vm.isDesigPayShow = true;
            vm.desigPayGrid();
        };        

        $scope.$watchGroup(['vm.form.HR_PAY_ELEMENT_ID', 'vm.form.HR_DEPARTMENT_ID'], function (newVal, oldVal) {
            //alert(newVal); 
            if (newVal[0] != null && newVal[1] != null) {
                $("#desigPayGrid").empty();
                vm.isDesigPayShow = false;
            }
        });
        
        vm.desigPayGrid = function () {

            var dataSource = new kendo.data.DataSource({
                batch: true,
                transport: {
                    read: function (e) {
                        HrService.getDesigPayListData(vm.form.HR_DEPARTMENT_ID, vm.form.HR_PAY_ELEMENT_ID).then(function (res) {
                            vm.desigPayElementData = res;
                            //console.log(vm.desigPayElementData);
                            
                            e.success(vm.desigPayElementData);
                        });
                    },
                    parameterMap: function (options, operation) {
                        if (operation !== "read" && options.models) {
                            return { models: kendo.stringify(options.models) };
                        }
                    }
                },
                pageSize: 1000,
                schema: {
                    model: {
                        id: "HR_DESIG_PAY_ID",
                        fields: {
                            HR_PAY_ELEMENT_ID: { type: "string", editable: false },
                            DESIGNATION_NAME_EN: { type: "string", editable: false },                            
                            PAY_RATE: { type: "number", editable: true },
                            IS_EFFECT_SALARY: { type: "string", editable: true },
                            IS_ACTIVE: { type: "string", editable: true }
                        }
                    }
                }
            });

            var yesNoList = [{ CONF_ID: 'Yes', CONF_NAME: 'Yes' }, { CONF_ID: 'No', CONF_NAME: 'No' }];

            $("#desigPayGrid").kendoGrid({
                dataSource: dataSource,
                navigatable: true,
                sortable: true,
                pageable: {
                    refresh: false,
                    pageSizes: false
                },
                height: 300,
                //toolbar: ["save", "cancel"],
                columns: [
                    { field: "DESIGNATION_NAME_EN", title: "Designation", width: "200px" },                    
                    { field: "PAY_RATE", title: "Amount", width: "100px", attributes: { style: "text-align:right;" } },
                    { field: "IS_EFFECT_SALARY", title: "Effect Salary?", width: "100px", editor: yesNoListData },
                    { field: "IS_ACTIVE", title: "Active?", width: "100px", editor: yesNoListData }
                    //{ command: ["edit","destroy"], title: "&nbsp;", width: "100px" }                    
                ],
                editable: true
            });

            function yesNoListData(container, options) {
                //console.log(options.field);
                $('<input required data-text-field="CONF_NAME" data-value-field="CONF_ID" data-bind="value:' + options.field + '"/>')
                    .appendTo(container)
                    .kendoDropDownList({
                        autoBind: false,
                        dataSource: yesNoList
                    });
            };
        };
                

        function getCompanyList() {
            return vm.companyList = {
                //optionLabel: "-- Select --",
                filter: "startswith",
                autoBind: true,
                //index:1,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getCompanyData().then(function (res) {
                                e.success(res);

                                $('#ACC_PAY_PERIOD_ID').kendoDropDownList({
                                    //optionLabel: "-- Select --",
                                    filter: "startswith",
                                    autoBind: true,
                                    dataSource: {
                                        transport: {
                                            read: function (e) {
                                                $http({
                                                    method: 'post',
                                                    url: "/Hr/HrDisciplinAction/CompanyOpenPeriodListData",  //+ "&pType=" + showType
                                                    params: {
                                                        pHR_COMPANY_ID: res[0].HR_COMPANY_ID,
                                                        pHR_PERIOD_TYPE_ID:3
                                                    }
                                                }).
                                                success(function (data, status, headers, config) {
                                                    e.success(data);
                                                    vm.form.ACC_PAY_PERIOD_ID = res[0].HR_COMPANY_ID;
                                                }).
                                                error(function (data, status, headers, config) {
                                                    alert('something went wrong')
                                                    console.log(status);
                                                });
                                            }
                                        }
                                    },
                                    dataTextField: "MONTH_YEAR_NAME",
                                    dataValueField: "RF_FISCAL_YEAR_ID"
                                });


                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID",
                select: function (e) {
                    //alert('a');
                    var dataItem = this.dataItem(e.item);
                    var vHR_COMPANY_ID = dataItem.HR_COMPANY_ID; //this.value();

                    $('#ACC_PAY_PERIOD_ID').kendoDropDownList({
                        //optionLabel: "-- Select --",
                        filter: "startswith",
                        autoBind: true,
                        dataSource: {
                            transport: {
                                read: function (e) {
                                    $http({
                                        method: 'post',
                                        url: "/Hr/HrDisciplinAction/CompanyOpenPeriodListData",  //+ "&pType=" + showType
                                        params: {
                                            pHR_COMPANY_ID: vHR_COMPANY_ID,
                                            pHR_PERIOD_TYPE_ID:3
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
                        dataTextField: "MONTH_YEAR_NAME",
                        dataValueField: "RF_FISCAL_YEAR_ID"
                    });
                }
            };
        };

        function getPayElementListData() {
            vm.payElementData = {
                optionLabel: "Select",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getPayElementListData("Y","",159).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "PAY_ELEMENT_NAME_EN",
                dataValueField: "HR_PAY_ELEMENT_ID",
                select: function (e) {
                    vm.obPayElement = this.dataItem(e.item);
                }
            };
        };

        function getDepartmentListData() {
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
        };

        vm.submit = function (form, insert) {
            
            vm.obList = $("#desigPayGrid").data("kendoGrid").dataSource.data();
            //console.log(vm.obList);

            $http({
                headers: { "RequestVerificationToken": vm.antiForgeryToken },
                url: '/Hr/HrDesigPay/BatchSave',
                method: 'post',
                data: vm.obList
            }).success(function (data, status, headers, config1) {
                vm.errors = [];
                if (data.success === false) {
                    vm.errors = data.errors;
                }
                else {
                    if (data.vMsg.substr(0, 9) == 'MULTI-001') {
                        //vm.reset();
                        vm.desigPayGrid();
                    };
                    config.appToastMsg(data.vMsg);

                }
            }).error(function (data, status, headers, config) {
                alert('something went wrong')
                console.log(status);
            });
                        
        };


        

        
        function activate(){
            var promise = [getPayElementListData(), getDepartmentListData()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

       
            
        
    }

    

})();

