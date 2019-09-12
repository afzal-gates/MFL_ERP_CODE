//============== Start for MbnBillProcessController
(function () {
    'use strict';
    angular.module('multitex.hr').controller('EmpTrnfrController', ['logger', 'config', 'HrService', '$q', '$scope', '$http', '$location', '$state', '$stateParams', '$filter', '$timeout', 'Dialog', 'formData', EmpTrnfrController]);

    function EmpTrnfrController(logger, config, HrService, $q, $scope, $http, $location, $state, $stateParams, $filter, $timeout, Dialog, formData) {
                        
        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        var key = 'HR_EMP_TRNFR_ID';
        
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.today = new Date();
        vm.periodType = 3;
        vm.form = formData[key] ? formData : { HR_EMP_TRNFR_ID: 0 };

        
        
        activate();

        function activate() {
            var promise = [getCompanyList(), getOfficeList(), getSectionList(), getDesignationList(), getLkFloorList(), getLkLineList() /*getProdFloorList(), getProdLineList()*/];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        vm.trnfRefDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.trnfRefDateOpened = true;
        }
        
        vm.effectDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.effectDateOpened = true;
        }

        $scope.emoloyeeAuto = function (val) {
            //return HrService.GetDataByUrl('/api/Hr/EmpAutoDataList/pEMPLOYEE_CODE=' + val).then(function (res) {
            //    return res;
            //}, function (err) {
            //    console.log(err);
            //});

            return $http.get('/Hr/HrEmployee/EmployeeAutoData', {
                params: {
                    pEMPLOYEE_CODE: val,
                    pLK_JOB_STATUS_ID: 30
                }
            }).then(function (response) {
                return response.data;
            });
        };

        $scope.onSelectItem = function (item) {
            console.log(item);            
            vm.coreDeptId = item.CORE_DEPT_ID;
            vm.desigDataSource.read();

            
            vm.form.HR_EMPLOYEE_ID = item.HR_EMPLOYEE_ID;

            vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN;
            vm.form.P_DESIGNATION_NAME_EN = item.DESIGNATION_NAME_EN;

            vm.form.P_COMP_NAME_EN = item.COMP_NAME_EN;
            vm.form.P_OFFICE_NAME_EN = item.OFFICE_NAME_EN;
            vm.form.P_DEPARTMENT_NAME_EN = item.SECTION_NAME;

            //vm.form.SECTION_NAME = item.SECTION_NAME;
            //vm.form.SECTION_NAME = item.SECTION_NAME;
            vm.form.EMP_PROFILE = item.EMP_FULL_NAME_EN + ', ' + item.DESIGNATION_NAME_EN + ', ' + item.SECTION_NAME;

            vm.form.N_HR_COMPANY_ID = item.HR_COMPANY_ID;
            vm.form.N_HR_OFFICE_ID = item.HR_OFFICE_ID;
            vm.form.N_HR_DEPARTMENT_ID = item.HR_DEPARTMENT_ID;
            
            vm.form.N_LK_FLOOR_ID = item.LK_FLOOR_ID;
            vm.form.N_LINE_NO = item.LINE_NO;

            //vm.form.N_HR_PROD_FLR_ID = item.HR_PROD_FLR_ID;
            //vm.form.N_HR_PROD_LINE_ID = item.HR_PROD_LINE_ID;

            vm.form.P_FLOOR_DESC_EN = item.FLOOR_NAME_EN;
            vm.form.P_LINE_NO = item.LINE_NO;

            $timeout(function () {
                vm.form.N_HR_DESIGNATION_ID = item.HR_DESIGNATION_ID;
            }, 1000);
        }

        function getCompanyList() {
           
            vm.compOptions = {
                optionLabel: "-- Select Company --",
                filter: "startswith",
                autoBind: true,
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID",                
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);
                    //return vm.compPayPeriodDataSource = new kendo.data.DataSource({
                    //    transport: {
                    //        read: function (e) {
                    //            return HrService.getDataByFullUrl("/api/common/GetAccPayPeriod/?pHR_COMPANY_ID=" + dataItem.HR_COMPANY_ID + "&pHR_PERIOD_TYPE_ID=" + vm.periodType + "&pIS_CLOSED=N").then(function (res) {
                    //                e.success(res);
                    //            });
                    //        }
                    //    }
                    //});
                },
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.GetAllOthers('/api/common/CompanyList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                }
            }                                  
        };
        
        function getOfficeList() {
            vm.officeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "OFFICE_NAME_EN",
                dataValueField: "HR_OFFICE_ID"
            };

            return vm.officeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.getDataByFullUrl('/api/common/OfficeList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
           
        };
        
        vm.coreDeptId = 0;
        function getSectionList() {
            return vm.sectionOption = {
                optionLabel: "-- Select Section --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getSubDepartmentData().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "DEPARTMENT_NAME_EN",
                dataValueField: "HR_DEPARTMENT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.coreDeptId = item.CORE_DEPT_ID;
                    console.log(item);

                    vm.desigDataSource.read();
                },
            };
        };

        function getDesignationList() {
            vm.desigOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "DESIGNATION_NAME_EN",
                dataValueField: "HR_DESIGNATION_ID"
            };

            return vm.desigDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.getDesigData((vm.coreDeptId || -1)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        };

        function getLkFloorList() {
            return vm.lkFloorOption = {
                optionLabel: "-- Select --",
                filter: "contains",
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
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    vm.form.N_FLOOR_DESC_EN = dataItem.LK_DATA_NAME_EN;
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function getLkLineList() {
            return vm.lkLineOption = {
                optionLabel: "-- Select --",
                filter: "contains",
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

        function getProdFloorList() {
            vm.prodFloorOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FLOOR_DESC_EN",
                dataValueField: "HR_PROD_FLR_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);
                    vm.form.N_HR_PROD_FLR_ID = dataItem.HR_PROD_FLR_ID;

                    vm.prodLineDataSource.read();
                },
            };

            return vm.prodFloorDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.getDataByFullUrl('/api/hr/GetProdFloorList').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                group: { field: "BLDNG_DESC_EN" },
                sort: [{ field: 'HR_PROD_BLDNG_ID', dir: 'asc' }]
            });
        }

        function getProdLineList() {
            vm.prodLineOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LINE_NO",
                dataValueField: "HR_PROD_LINE_ID"
            };

            return vm.prodLineDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.getDataByFullUrl('/api/hr/GetPrdLineList?pHR_PROD_FLR_ID=' + (vm.form.N_HR_PROD_FLR_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        }
        

        //HrService.getDesigData(vParentID).then(function (res) {
        //    $("#dropdownlist").kendoDropDownList({
        //        autoBind: true,
        //        optionLabel: "Select",
        //        dataTextField: "DESIGNATION_NAME_EN",
        //        dataValueField: "HR_DESIGNATION_ID",
        //        dataSource: res,
        //        filter: "startswith",
        //        select: function (e) {
        //            var dataItem = this.dataItem(e.item);
        //            vm.form.HR_GRADE_ID = dataItem.HR_GRADE_ID;
        //        }
        //    });

        //});
        

        vm.editData = function (dataItem) {
            vm.form = angular.copy(dataItem);
            $state.go('EmpTrnfr', { pHR_EMP_TRNFR_ID: dataItem.HR_EMP_TRNFR_ID }, { notify: false });
        }

        vm.tranCancel = function () {
            //vm.form = { HR_INCR_MEMO_ID: 0, IS_BY_ADMIN: 'Y', IS_FINALIZED: 'N', EFFECTIVE_DT: vm.today, MEMO_DT: vm.today };
            vm.form = { HR_EMP_TRNFR_ID: 0 };
            $state.go('EmpTrnfr', { pHR_EMP_TRNFR_ID: 0 }, { notify: false });
        }

        vm.transGridOption = {
            height: 300,
            sortable: true,
            scrollable: {
                virtual: true,
                //scrollable:true
            },
            pageable: false,
            editable: false,
            selectable: "cell",
            navigatable: true,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains",
                        startswith: "Starts With",
                        eq: "Is Equal To"
                    }
                }
            },
            columns: [
                { field: "TRNF_REF_DT", title: "Ref Date", type: "date", format: "{0:dd-MMM-yyyy}", width: "10%", filterable: true },
                { field: "EFFECT_DT", title: "Eff Date", type: "date", format: "{0:dd-MMM-yyyy}", width: "10%", filterable: true },
                { field: "TRNF_REF_NO", title: "Ref#", type: "string", width: "10%", hidden: true },
                { field: "P_EMPLOYEE_CODE", title: "Emp Code", type: "string", width: "10%", filterable: true },
                { field: "P_COMP_NAME_EN", title: "Old Company", type: "string", width: "10%", filterable: false },
                { field: "P_OFFICE_NAME_EN", title: "Old Office", type: "string", width: "10%", filterable: false },
                { field: "P_DEPARTMENT_NAME_EN", title: "Old Section", type: "string", width: "10%", filterable: false },
                { field: "N_EMPLOYEE_CODE", title: "New Code", type: "string", width: "10%", filterable: true, hidden: true },
                { field: "N_COMP_NAME_EN", title: "New Company", type: "string", width: "10%", filterable: false },
                { field: "N_OFFICE_NAME_EN", title: "New Office", type: "string", width: "10%", filterable: false },
                { field: "N_DEPARTMENT_NAME_EN", title: "New Section", type: "string", width: "10%", filterable: false },
                {
                    title: "Action/Status",
                    template: function () {                        
                        return "<button class='btn btn-xs blue' ng-click='vm.editData(dataItem)' ng-show='dataItem.IS_FINALIZED==\"N\"' ><i class='fa fa-edit'> Edit</i></button>" +
                            "&nbsp;<button class='btn btn-xs blue' ng-click='vm.save(dataItem,\"Y\")' ng-show='dataItem.IS_FINALIZED==\"N\"' >Finalize</button>" +
                            "&nbsp;<label class='label label-sm label-success' ng-show='dataItem.IS_FINALIZED==\"Y\"' >Finalized</label>";
                    },
                    width: "10%"
                }
            ]
        };

        vm.transGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            //serverSorting: true,
            serverFiltering: true,
            pageSize: 100,
            transport: {
                read: function (e) {
                    
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/hr/EmpTrans/GetTransData';
                    url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += HrService.kFilterStr2QueryParam(params.filter);

                    return HrService.GetDataByUrl(url).then(function (res) {
                        e.success(res);
                    }, function (err) {
                        console.log(err);
                    });                                       
                }
            },
            schema: {                
                data: "data",
                total: "total",
                model: {
                    id: "HR_EMP_TRNFR_ID",
                    fields: {                        
                        TRNF_REF_DT: { type: "date", editable: false },
                        EFFECT_DT: { type: "date", editable: false }                        
                    }
                }
            }
        });

                   
       

        
        vm.billPrint = function (dataItem) {
            
            dataItem['REPORT_CODE'] = 'RPT-1065';
            
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/Hr/HrReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = angular.copy(dataItem);
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
        }

        vm.billUpdate = function (dataItem) {
            var vMsg = 'Do you want to update the record?';

            var submitData = angular.copy(dataItem);
           
            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {
                submitData.NOTICE_DT = $filter('date')(submitData.NOTICE_DT, vm.dtFormat);
                submitData.PAYMENT_DT = $filter('date')(submitData.PAYMENT_DT, vm.dtFormat);

                return HrService.saveDataByUrl(submitData, '/api/hr/MbnBill/MbnBillPhaseUpdate').then(function (res) {
                    if (res.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        console.log(res);
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            dataItem.IS_EDIT_ENABLED = !dataItem.IS_EDIT_ENABLED;
                        };

                        config.appToastMsg(res.data.PMSG);

                    }
                });
            });
        }

      

        



        vm.save = function (data, pIS_FINALIZED) {
            var submitData = angular.copy(data);

            submitData.TRNF_REF_DT = $filter('date')(submitData.TRNF_REF_DT, vm.dtFormat);
            submitData.EFFECT_DT = $filter('date')(submitData.EFFECT_DT, vm.dtFormat);
            
            
            var vMsg = 'Do you want to save?';
            if (pIS_FINALIZED == 'Y') {
                submitData['IS_FINALIZED'] = pIS_FINALIZED;
                vMsg = "After finalize, the record will lock & update employee information.</br></br> <b>Do you want to save and finalize?</b>";
            }

            console.log(submitData);

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                return HrService.saveDataByUrl(submitData, '/api/hr/EmpTrans/Save').then(function (res) {
                    console.log(res);

                    if (res.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        console.log(res);
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            //vm.reset();

                            //if (pIS_CLOSED == 'Y') {
                            //    vm.form.IS_CLOSED = pIS_CLOSED;
                            //}
                            vm.form.HR_EMP_TRNFR_ID = res.data.PHR_EMP_TRNFR_ID_RTN;
                            vm.form.TRNF_REF_NO = res.data.PTRNF_REF_NO_RTN;

                            vm.transGridDataSource.read();

                            $state.go('EmpTrnfr', { pHR_EMP_TRNFR_ID: res.data.PHR_EMP_TRNFR_ID_RTN }, { notify: false });

                        };

                        config.appToastMsg(res.data.PMSG);

                    }
                });
            });


        };


        vm.memoFinalize = function () {
            return HrService.saveDataByUrl(vm.form, '/api/Hr/HrIncriment/MemoFinalize').then(function (res) {
                if (res.success === false) {
                    vm.errors = data.errors;
                }
                else {
                    console.log(res);
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        //vm.reset();
                        vm.incrMemoGridDataSource.read();                        
                    };

                    config.appToastMsg(res.data.PMSG);

                }
            });
        }
            
        
    }
    
})();
//============== End for MbnBillProcessController


