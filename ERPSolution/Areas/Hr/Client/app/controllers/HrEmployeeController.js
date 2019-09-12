(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrEmployeeController', ['logger', 'config', '$q', '$scope', '$http', '$location', '$state', '$stateParams', '$filter', 'exception', 'HrService', 'entityService', HrEmployeeController]);

    function HrEmployeeController(logger, config, $q, $scope, $http, $location, $state, $stateParams, $filter, exception, HrService, entityService) {

        var vm = this;
        activate();
        vm.title = config.appTitle;        
        vm.dtFormat = config.appDateFormat;
        
        vm.isCurrMonthJoinDt = 'N';
        vm.showSplash = true;
        vm.insert = true;
        vm.tabID = 0;
        vm.form = {};
        
        vm.yesNoList = [];

        vm.isEmpPayShow = false;
        vm.obPayElement = {};
        vm.empPayElementData = new kendo.data.DataSource({
            data: []
        });
        
        
        $scope.yesNoList = {
            //optionLabel: "Select",
            //filter: "startswith",
            autoBind: true,
            dataSource: [{ CONF_ID: 'Y', CONF_NAME: 'Yes' }, { CONF_ID: 'N', CONF_NAME: 'No' }],
            dataTextField: "CONF_NAME",
            dataValueField: "CONF_ID"//,
            //select: function (e) {
            //    vm.obPayElement = this.dataItem(e.item);
            //}
        };
        


        vm.yesNoList = [{ IS_ACTIVE: 'Y', IS_ACTIVE_NAME: 'Yes' }, { IS_ACTIVE: 'N', IS_ACTIVE_NAME: 'No' }];
        
        vm.gridPayElementColumn = [
                    { field: "PAY_ELEMENT_NAME_EN", title: "Element", width: "200px" },
                    { field: "PAY_ELM_TYPE_NAME_EN", title: "Type", width: "100px" },
                    { field: "IS_CORE_SAL_PART", title: "Core Salary?", width: "100px" },
                    {
                        title: "Amount",
                        template: function () {
                            return "<input type='text' ng-model='dataItem.PAY_AMT' name='PAY_AMT-{{dataItem.uid}}' class='form-control' />";
                        },
                        width: "100px"
                    },
                    {
                        title: "Active?",
                        template: function () {
                            return "<select kendo-drop-down-list ng-model='dataItem.IS_ACTIVE' name='IS_ACTIVE-{{dataItem.uid}}' options='yesNoList' class='form-control' ></select>";
                        },
                        width: "80px"
                    },
                    {
                        title: "Effect Salary?",
                        template: function () {
                            return "<select kendo-drop-down-list ng-model='dataItem.IS_EFFECT_SALARY' name='IS_EFFECT_SALARY-{{dataItem.uid}}' options='yesNoList' class='form-control' ></select>";
                        },
                        width: "100px"
                    }
                    //{ field: "PAY_AMT", title: "Amount", width: "100px", attributes: { style: "text-align:right;" } },
                    //{ field: "IS_CORE_SAL_PART", title: "Core Salary?", width: "100px" },
                    //{ field: "IS_ACTIVE", title: "Active?", width: "70px", editor: yesNoListData },
                    //{ field: "IS_EFFECT_SALARY", title: "Effect Salary?", width: "100px", editor: yesNoListData }
                    //{ command: ["edit","destroy"], title: "&nbsp;", width: "100px" }                    
        ];


        vm.form = { HR_EMPLOYEE_ID:null, IS_OT_HRLY: 'N', IS_TRANSPORT: 'N', IS_HOUSING: 'N', EMP_FIRST_NAME_EN: '', EMP_LAST_NAME_EN: '', EMP_PHOTO_PREVIEW:'', EMP_PAY_LIST:[] };
        vm.today = new Date();
                
        vm.addNew = function () {
            vm.insert = true;

            vm.isAddAnother = false;
            vm.empPayElementData = [];
            vm.isEmpPayShow = false;
            vm.isCurrMonthJoinDt = 'N';
            $("#payGrid").empty();

            vm.errors = undefined;

            vm.form.HR_EMPLOYEE_ID = null;
            vm.form.EMPLOYEE_CODE = "";
            vm.form.TA_PROXI_ID = "";
            vm.form.EMP_FIRST_NAME_EN = "";
            vm.form.EMP_LAST_NAME_EN = "";
            vm.form.EMP_FULL_NAME_EN = "";
            vm.form.EMP_NICKNM_EN = "";
            vm.form.FATHER_NAME_EN = "";
            vm.form.MOTHER_NAME_EN = "";
            vm.form.GUARDIAN_NAME_EN = "";
            vm.form.NATIONAL_ID = "";
            vm.form.TIN_NO = "";
            vm.form.BIRTH_DT = "";
            vm.form.MOB_NO_PRS = "";
            vm.form.HOME_PHONE = "";
            vm.form.HOME_PHONE_EXT = "";
            vm.form.EMAIL_ID_PRS = "";
            vm.form.PASSPORT_NO = "";
            vm.form.PASS_ISS_PLACE = "";
            vm.form.PASS_ISSUE_DT = "";
            vm.form.PASS_EXPIRE_DT = "";
            vm.form.DRIV_LIC_NO = "";
            vm.form.DRIV_LIC_EXPIRE_DT = "";
            vm.form.APNT_ISSUE_DT = "";
            vm.form.JOINING_DT = "";
            vm.form.CONFIRM_DT = "";
            vm.form.PROBATION_PERIOD = "";
            vm.form.PROBATION_DT = "";
            vm.form.MOB_NO_OFF = "";
            vm.form.WORK_PHONE = "";
            vm.form.WORK_PHONE_EXT = "";
            vm.form.EMAIL_ID_OFF = "";
            vm.form.IS_TRANSPORT = 'N';
            vm.form.IS_HOUSING = 'N';
            vm.form.IS_OT_HRLY = 'N';
            vm.form.NO_OF_CHILD = "";
            vm.form.PRE_ADDRESS_EN = "";
            vm.form.PRE_POSTAL_CODE = "";
            vm.form.PERM_ADDRESS_EN = "";
            vm.form.PERM_POSTAL_CODE = "";
            vm.form.GROSS_SALARY = "";
            vm.form.EMP_FIRST_NAME_BN = "";
            vm.form.EMP_LAST_NAME_BN = "";
            vm.form.EMP_FULL_NAME_BN = "";
            vm.form.EMP_NICKNM_BN = "";
            vm.form.PRE_ADDRESS_BN = "";
            vm.form.PERM_ADDRESS_BN = "";
            vm.form.FATHER_NAME_BN = "";
            vm.form.MOTHER_NAME_BN = "";
            vm.form.GUARDIAN_NAME_BN = "";                        
            
            vm.form.EMP_PHOTO_PREVIEW = "";
            vm.form.EMP_PAY_LIST= [];

            vm.form.CORE_DEPT_ID = null;
            vm.form.HR_DEPARTMENT_ID = "";
            vm.form.HR_DESIGNATION_ID = "";

            vm.coreDeptDataSource.read();
            vm.subDeptDataSource.read();
            vm.nomineeGridDataSource.read();

            //$("#HR_DEPARTMENT_ID").empty();
            //vm.form.HR_DEPARTMENT_ID = "";
            //HrService.getSubDepartmentData(vm.form.CORE_DEPT_ID).then(function (res) {
            //    $("#HR_DEPARTMENT_ID").kendoDropDownList({
            //        optionLabel: "Select",
            //        dataTextField: "DEPARTMENT_NAME_EN",
            //        dataValueField: "HR_DEPARTMENT_ID",
            //        dataSource: res,
            //        filter: "startswith"
            //    });
            //});

            //vm.defaultForm = vm.form;
            //vm.form = { HR_EMPLOYEE_ID: null, IS_OT_HRLY: 'N', IS_TRANSPORT: 'N', IS_HOUSING: 'N', EMP_FIRST_NAME_EN: '', EMP_LAST_NAME_EN: '', EMP_PHOTO_PREVIEW: '', EMP_PAY_LIST: [] };

            //vm.form.LK_RELIGION_ID = vm.defaultForm.LK_RELIGION_ID;
            //vm.form.LK_GENDER_ID = vm.defaultForm.LK_GENDER_ID;
            //vm.form.LK_NTLTY_ID = vm.defaultForm.LK_NTLTY_ID;
            //vm.form.LK_JOB_STATUS_ID = vm.defaultForm.LK_JOB_STATUS_ID;
            //vm.form.PRE_DISTRICT_ID = vm.defaultForm.PRE_DISTRICT_ID;
            //vm.form.HR_COUNTRY_ID = vm.defaultForm.HR_COUNTRY_ID;

            

            vm.tabID = 0;
            vm.tabs[0].active = true;
            vm.tabs1[0].active = true;
        };

        //vm.fileUpload= function () {
        //    $('#ATT_FILE').fileupload({
        //        headers: { "RequestVerificationToken": vm.antiForgeryToken },
        //        method: 'post',
        //        dataType: 'json',
        //        url: '/Hr/HrEmployee/UploadFiles',
        //        autoUpload: true,
        //        done: function (e, data) {
        //            $('.file_name').html(data.result.name);
        //            $('.file_type').html(data.result.type);
        //            $('.file_size').html(data.result.size);
        //        }
        //    }).on('fileuploadprogressall', function (e, data) {
        //        var progress = parseInt(data.loaded / data.total * 100, 10);
        //        $('.progress .progress-bar').css('width', progress + '%');
        //    });
        //};

        vm.isSalBreakup = false;
        vm.salBreakup = function () {        
            if (vm.form.GROSS_SALARY_OLD != vm.form.GROSS_SALARY) {
                HrService.getSalaryBreakupListData(vm.form.GROSS_SALARY).then(function (res) {                    
                    vm.form.SALARY_BREAKUP = '';
                    for (var x = 0; x < res.length; x++) {
                        vm.form.SALARY_BREAKUP = vm.form.SALARY_BREAKUP + res[x].PAY_ELEMENT_NAME_EN + ': ' + res[x].PAY_AMT + ', ';

                        var index = HrService.getIndexByKeyValue(vm.empPayElementData._data, 'HR_PAY_ELEMENT_ID', res[x].HR_PAY_ELEMENT_ID);

                        if (index != null) {
                            vm.empPayElementData._data[index].PAY_AMT = res[x].PAY_AMT;
                            vm.empPayElementData._data[index].IS_ACTIVE = res[x].IS_ACTIVE;
                            vm.empPayElementData._data[index].IS_EFFECT_SALARY = res[x].IS_EFFECT_SALARY;
                            vm.empPayElementData._data[index].IS_CORE_SAL_PART = res[x].IS_CORE_SAL_PART;
                        }
                        else {
                            vm.empPayElementData.insert({
                                HR_PAY_ELEMENT_ID: res[x].HR_PAY_ELEMENT_ID, PAY_ELEMENT_NAME_EN: res[x].PAY_ELEMENT_NAME_EN, PAY_AMT: res[x].PAY_AMT,
                                PAY_ELM_TYPE_NAME_EN: res[x].PAY_ELM_TYPE_NAME_EN, IS_ACTIVE: res[x].IS_ACTIVE, IS_EFFECT_SALARY: res[x].IS_EFFECT_SALARY,
                                IS_CORE_SAL_PART: res[x].IS_CORE_SAL_PART
                            });
                        };

                        if (x + 1 >= res.length) {
                            vm.isSalBreakup = true;
                            vm.form.SALARY_BREAKUP = vm.form.SALARY_BREAKUP.substr(0, vm.form.SALARY_BREAKUP.length - 2);
                        };
                    };
                    //getEmpPayGrid();

                });
            }                        
        };

        vm.getGrossSal = function () {
            vm.form.GROSS_SALARY_OLD = vm.form.GROSS_SALARY;
        }


        vm.isAddAnother = false;
        vm.addAnotherEmpPayElement = function () {
            vm.isAddAnother = true;
        };

        //vm.addEmpPayElement = function () {
        //    vm.empPayElementData.push({ HR_PAY_ELEMENT_ID: vm.obPayElement.HR_PAY_ELEMENT_ID, PAY_ELEMENT_NAME_EN: vm.obPayElement.PAY_ELEMENT_NAME_EN, PAY_AMT: vm.obPayElement.PAY_AMT, PAY_ELM_TYPE_NAME_EN: vm.obPayElement.PAY_ELM_TYPE_NAME_EN });
        //};

        //function getEmpPayListData() {
        //    return HrService.getEmpPayListData(null).then(function (res) {
        //        vm.empPayElementData = res;
        //    });
        //};

        function getEmpPayGrid() {
            //var dataSource = new kendo.data.DataSource({
            //    batch: true,
            //    transport: {
            //        read: function (e) {
            //            HrService.getEmpPayListData(vm.form.HR_EMPLOYEE_ID).then(function (res) {
                            
            //                if (vm.isEmpPayShow != true) {
            //                    vm.isEmpPayShow = true;

            //                    for (var x = 0; x < res.length; x++) {
            //                        var index = HrService.getIndexByKeyValue(vm.empPayElementData, 'HR_PAY_ELEMENT_ID', res[x].HR_PAY_ELEMENT_ID);

            //                        if (index != null) {
            //                            vm.empPayElementData[index].PAY_AMT = res[x].PAY_AMT;
            //                            vm.empPayElementData[index].IS_ACTIVE = res[x].IS_ACTIVE;
            //                            vm.empPayElementData[index].IS_EFFECT_SALARY = res[x].IS_EFFECT_SALARY;
            //                            vm.empPayElementData[index].IS_CORE_SAL_PART = res[x].IS_CORE_SAL_PART;
            //                        }
            //                        else {
            //                            vm.empPayElementData.push({
            //                                HR_EMP_PAY_ID: res[x].HR_EMP_PAY_ID, HR_PAY_ELEMENT_ID: res[x].HR_PAY_ELEMENT_ID, PAY_ELEMENT_NAME_EN: res[x].PAY_ELEMENT_NAME_EN,
            //                                PAY_AMT: res[x].PAY_AMT, PAY_ELM_TYPE_NAME_EN: res[x].PAY_ELM_TYPE_NAME_EN, IS_ACTIVE: res[x].IS_ACTIVE,
            //                                IS_EFFECT_SALARY: res[x].IS_EFFECT_SALARY, IS_CORE_SAL_PART: res[x].IS_CORE_SAL_PART
            //                            });
            //                        };
            //                    }
            //                };
            //                //vm.empPayElementData = res;
            //                //console.log(vm.empPayElementData);
            //                if (vm.isAddEmpPayElement == true && vm.obPayElement.HR_PAY_ELEMENT_ID != '' && vm.obPayElement.HR_PAY_ELEMENT_ID!=null) {
            //                    vm.isAddEmpPayElement = false;

            //                    vm.empPayElementData.push({
            //                        HR_PAY_ELEMENT_ID: vm.obPayElement.HR_PAY_ELEMENT_ID, PAY_ELEMENT_NAME_EN: vm.obPayElement.PAY_ELEMENT_NAME_EN,
            //                        PAY_AMT: vm.obPayElement.PAY_AMT, PAY_ELM_TYPE_NAME_EN: vm.obPayElement.PAY_ELM_TYPE_NAME_EN, IS_ACTIVE: vm.obPayElement.IS_ACTIVE,
            //                        IS_EFFECT_SALARY: vm.obPayElement.IS_EFFECT_SALARY, IS_CORE_SAL_PART: vm.obPayElement.IS_CORE_SAL_PART
            //                    });
            //                };
            //                e.success(vm.empPayElementData);
            //            });
            //        },

            //        destroy: function (e) {
            //            //console.log(e);
            //            HrService.deleteRejoinData(e.data.models, vm.antiForgeryToken).then(function (res) {
            //                //config.appToastMsg(res.vMsg);
            //                e.success();

            //            });
            //        },
            //        parameterMap: function (options, operation) {
            //            if (operation !== "read" && options.models) {
            //                return { models: kendo.stringify(options.models) };
            //            }
            //        }
            //    },
            //    pageSize: 10,
            //    schema: {
            //        model: {
            //            id: "HR_EMP_PAY_ID",
            //            fields: {
            //                HR_PAY_ELEMENT_ID: { type: "string", editable: false },
            //                PAY_ELEMENT_NAME_EN: { type: "string", editable: false },
            //                PAY_ELM_TYPE_NAME_EN: { type: "string", editable: false },
            //                PAY_AMT: { type: "number", editable: true },                            
            //                IS_CORE_SAL_PART: { type: "string", editable: false },
            //                IS_ACTIVE: { type: "string", editable: true },
            //                IS_EFFECT_SALARY: { type: "string", editable: true }
            //            }
            //        }
            //    }
            //});
            

            HrService.getEmpPayListData(vm.form.HR_EMPLOYEE_ID).then(function (res) {

                if (vm.isEmpPayShow != true) {
                    vm.isEmpPayShow = true;

                    for (var x = 0; x < res.length; x++) {
                        var index = HrService.getIndexByKeyValue(vm.empPayElementData._data, 'HR_PAY_ELEMENT_ID', res[x].HR_PAY_ELEMENT_ID);

                        if (index != null) {
                            vm.empPayElementData._data[index].PAY_AMT = res[x].PAY_AMT;
                            vm.empPayElementData._data[index].IS_ACTIVE = res[x].IS_ACTIVE;
                            vm.empPayElementData._data[index].IS_EFFECT_SALARY = res[x].IS_EFFECT_SALARY;
                            vm.empPayElementData._data[index].IS_CORE_SAL_PART = res[x].IS_CORE_SAL_PART;
                        }
                        else {
                            vm.empPayElementData.insert({
                                HR_EMP_PAY_ID: res[x].HR_EMP_PAY_ID, HR_PAY_ELEMENT_ID: res[x].HR_PAY_ELEMENT_ID, PAY_ELEMENT_NAME_EN: res[x].PAY_ELEMENT_NAME_EN,
                                PAY_AMT: res[x].PAY_AMT, PAY_ELM_TYPE_NAME_EN: res[x].PAY_ELM_TYPE_NAME_EN, IS_ACTIVE: res[x].IS_ACTIVE,
                                IS_EFFECT_SALARY: res[x].IS_EFFECT_SALARY, IS_CORE_SAL_PART: res[x].IS_CORE_SAL_PART
                            });
                        };
                    }
                };
                //vm.empPayElementData = res;
                //console.log(vm.empPayElementData);
                if (vm.isAddEmpPayElement == true && vm.obPayElement.HR_PAY_ELEMENT_ID != '' && vm.obPayElement.HR_PAY_ELEMENT_ID != null) {
                    vm.isAddEmpPayElement = false;

                    vm.empPayElementData.insert({
                        HR_PAY_ELEMENT_ID: vm.obPayElement.HR_PAY_ELEMENT_ID, PAY_ELEMENT_NAME_EN: vm.obPayElement.PAY_ELEMENT_NAME_EN,
                        PAY_AMT: vm.obPayElement.PAY_AMT, PAY_ELM_TYPE_NAME_EN: vm.obPayElement.PAY_ELM_TYPE_NAME_EN, IS_ACTIVE: vm.obPayElement.IS_ACTIVE,
                        IS_EFFECT_SALARY: vm.obPayElement.IS_EFFECT_SALARY, IS_CORE_SAL_PART: vm.obPayElement.IS_CORE_SAL_PART
                    });
                };
                //e.success(vm.empPayElementData);
            });

            

            

            //$("#payGrid").kendoGrid({
            //    dataSource: dataSource,
            //    navigatable: true,
            //    sortable: true,
            //    pageable: {
            //        refresh: false,
            //        pageSizes: true
            //    },
            //    //height: 550,
            //    //toolbar: ["save", "cancel"],
            //    columns: [
            //        { field: "PAY_ELEMENT_NAME_EN", title: "Element", width: "200px" },
            //        { field: "PAY_ELM_TYPE_NAME_EN", title: "Type", width: "100px" },
            //        { field: "PAY_AMT", title: "Amount", width: "100px", attributes: { style: "text-align:right;" }},
            //        { field: "IS_CORE_SAL_PART", title: "Core Salary?", width: "100px" },
            //        { field: "IS_ACTIVE", title: "Active?", width: "70px", editor: yesNoListData },
            //        { field: "IS_EFFECT_SALARY", title: "Effect Salary?", width: "100px", editor: yesNoListData }
            //        //{ command: ["edit","destroy"], title: "&nbsp;", width: "100px" }                    
            //    ],
            //    editable: true
            //});
               
            function yesNoListData(container, options) {
                //console.log(yesNoList);
                $('<input required data-text-field="CONF_NAME" data-value-field="CONF_ID" data-bind="value:' + options.field + '"/>')
                    .appendTo(container)
                    .kendoDropDownList({
                        autoBind: false,
                        dataSource: yesNoList
                    });
            };
            

        };

        vm.isAddEmpPayElement = false;
        vm.addEmpPayElement = function () {
            vm.isAddEmpPayElement = true;

            var index = HrService.getIndexByKeyValue(vm.empPayElementData, 'HR_PAY_ELEMENT_ID', vm.form.HR_PAY_ELEMENT_ID);

            if (index == null) {
                getEmpPayGrid();
            }
            else {
                alert('Sorry! The selected pay element already exists.');
            }
        };

        function getPayElementListData() {            
            vm.payElementData = {
                optionLabel: "Select",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getPayElementListData("Y", "", null).then(function (res) {
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

        
        $scope.emoloyeeAuto = function (val) {
            //alert('a');
            return HrService.EmployeeAutoDataList(val, 30, null).then(function (res) {            
                return res;
            });
        };


        $scope.onSelectItem = function (item) {
            //console.log(item);            
            vm.form.HR_EMPLOYEE_ID = item.HR_EMPLOYEE_ID;
            vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN;
            vm.form.DEPARTMENT_NAME_EN = item.DEPARTMENT_NAME_EN;
            vm.form.DESIGNATION_NAME_EN = item.DESIGNATION_NAME_EN;
            vm.form.COMP_NAME_EN = item.COMP_NAME_EN;
            vm.form.HR_COMPANY_ID = item.HR_COMPANY_ID;
            vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN + ', ' + item.DESIGNATION_NAME_EN + ', ' + item.DEPARTMENT_NAME_EN;
            vm.form.TA_PROXI_ID_OLD = item.TA_PROXI_ID;
        }

        $scope.$watch('vm.form.EMPLOYEE_CODE', function (newVal, oldVal) {

            if (!newVal || newVal == '') {
                vm.form.HR_EMPLOYEE_ID = null;
                vm.form.EMP_FULL_NAME_EN = null;
                vm.form.DEPARTMENT_NAME_EN = null;
                vm.form.DESIGNATION_NAME_EN = null;
                vm.form.COMP_NAME_EN = null;
                //vm.form.HR_COMPANY_ID = null;
                vm.form.TA_PROXI_ID_OLD = null;
            }

        });

        //vm.saveTutorial = function (form) {
        //    alert('i am from save tutorial');
        //    console.log(form);
        //    entityService.saveTutorial(form, vm.antiForgeryToken)
        //                 .then(function (data) {
        //                     console.log(data);
        //                 });
        //};

        //console.log($stateParams.pEmployeeObj);
        if ($stateParams.pEmployeeObj != null) {
            
            vm.form = $stateParams.pEmployeeObj
            //alert(vm.form.EMP_PHOTO_PATH);
            var dt = moment($stateParams.pEmployeeObj.BIRTH_DT)._d;
            vm.form['BIRTH_DT'] =$stateParams.pEmployeeObj.BIRTH_DT==null?'': $filter('date')(dt, vm.dtFormat);

            dt = moment($stateParams.pEmployeeObj.CONFIRM_DT)._d;
            vm.form['CONFIRM_DT'] = $stateParams.pEmployeeObj.CONFIRM_DT == null ? '' : $filter('date')(dt, vm.dtFormat);

            dt = moment($stateParams.pEmployeeObj.JOINING_DT)._d;
            vm.form['JOINING_DT'] = $stateParams.pEmployeeObj.JOINING_DT == null ? '' : $filter('date')(dt, vm.dtFormat);

            var todayMN = $filter('date')(new Date(), 'M');
            var joinDtMN = $filter('date')(dt, 'M');

            console.log(todayMN + '-' + joinDtMN);
            if (todayMN == joinDtMN) {
                vm.isCurrMonthJoinDt = 'Y';
            }

            dt = moment($stateParams.pEmployeeObj.APNT_ISSUE_DT)._d;
            vm.form['APNT_ISSUE_DT'] = $stateParams.pEmployeeObj.APNT_ISSUE_DT == null ? '' : $filter('date')(dt, vm.dtFormat);

            dt = moment($stateParams.pEmployeeObj.PROBATION_DT)._d;
            vm.form['PROBATION_DT'] = $stateParams.pEmployeeObj.PROBATION_DT == null ? '' : $filter('date')(dt, vm.dtFormat);

            dt = moment($stateParams.pEmployeeObj.OFFER_DT)._d;
            vm.form['OFFER_DT'] = $stateParams.pEmployeeObj.OFFER_DT == null ? '' : $filter('date')(dt, vm.dtFormat);

            dt = moment($stateParams.pEmployeeObj.REPORTING_DT)._d;
            vm.form['REPORTING_DT'] = $stateParams.pEmployeeObj.REPORTING_DT == null ? '' : $filter('date')(dt, vm.dtFormat);

            dt = moment($stateParams.pEmployeeObj.PASS_ISSUE_DT)._d;
            vm.form['PASS_ISSUE_DT'] = $stateParams.pEmployeeObj.PASS_ISSUE_DT == null ? '' : $filter('date')(dt, vm.dtFormat);

            dt = moment($stateParams.pEmployeeObj.PASS_EXPIRE_DT)._d;
            vm.form['PASS_EXPIRE_DT'] = $stateParams.pEmployeeObj.PASS_EXPIRE_DT == null ? '' : $filter('date')(dt, vm.dtFormat);

            dt = moment($stateParams.pEmployeeObj.VISA_EXPIRE_DT)._d;
            vm.form['VISA_EXPIRE_DT'] = $stateParams.pEmployeeObj.VISA_EXPIRE_DT == null ? '' : $filter('date')(dt, vm.dtFormat);

            dt = moment($stateParams.pEmployeeObj.DRIV_LIC_EXPIRE_DT)._d;
            vm.form['DRIV_LIC_EXPIRE_DT'] = $stateParams.pEmployeeObj.DRIV_LIC_EXPIRE_DT == null ? '' : $filter('date')(dt, vm.dtFormat);

            vm.form['PROBATION_PERIOD'] = $stateParams.pEmployeeObj.PROBATION_PERIOD == 0 ? '' : $stateParams.pEmployeeObj.PROBATION_PERIOD;
            vm.form['WORK_PHONE_EXT'] = $stateParams.pEmployeeObj.WORK_PHONE_EXT == 0 ? '' : $stateParams.pEmployeeObj.WORK_PHONE_EXT;
            vm.form['HOME_PHONE_EXT'] = $stateParams.pEmployeeObj.HOME_PHONE_EXT == 0 ? '' : $stateParams.pEmployeeObj.HOME_PHONE_EXT;
            vm.form['LK_JOB_STATUS_ID'] = $stateParams.pEmployeeObj.LK_JOB_STATUS_ID == 0 ? '' : $stateParams.pEmployeeObj.LK_JOB_STATUS_ID;

            

            //vm.empPayElementData = $stateParams.pEmployeeObj.EMP_PAY_LIST;
            //vm.form['HR_EMPLOYEE_ID'] = $stateParams.pEmployeeObj.HR_EMPLOYEE_ID;            
            //console.log(vm.empPayElementData);

            getEmpPayGrid();
            vm.insert = false;
        };
        
        vm.confirmDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.confirmDateOpened = true;
        };

        vm.joiningDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.joiningDateOpened = true;
        };

        vm.apptIssueDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.apptIssueDateOpened = true;
        };

        vm.birthDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.birthDateOpened = true;
        };
        
        vm.passportIssueDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.passportIssueDateOpened = true;
        };

        vm.passportExpireDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.passportExpireDateOpened = true;
        };

        vm.licenseExpireDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.licenseExpireDateOpened = true;
        };               

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };
        
        vm.changeProbtionPeriod = function () {
           
            var probtionDt;
            if (vm.form.PR_PERIOD_TYPE_ID == 1) {
                probtionDt = moment(vm.form.JOINING_DT).add(vm.form.PROBATION_PERIOD, 'days');
            }
            else if (vm.form.PR_PERIOD_TYPE_ID == 2) {
                probtionDt = moment(vm.form.JOINING_DT).add(vm.form.PROBATION_PERIOD, 'weeks');
            }
            else if (vm.form.PR_PERIOD_TYPE_ID == 3) {                
                probtionDt = moment(vm.form.JOINING_DT).add(vm.form.PROBATION_PERIOD, 'months');
            }
            else if (vm.form.PR_PERIOD_TYPE_ID == 4) {
                probtionDt = moment(vm.form.JOINING_DT).add(vm.form.PROBATION_PERIOD, 'years');
            };

            var dt = moment(probtionDt)._d;
            
            vm.form.PROBATION_DT = $filter('date')(dt, vm.dtFormat);
        };
        
        
        vm.onChngJoinDt4CurrMonth = function () {
            var todayMN = $filter('date')(new Date(), 'M');
            var joinDtMN = $filter('date')(vm.form.JOINING_DT, 'M');

            console.log(todayMN + '-' + joinDtMN);
            if (todayMN == joinDtMN) {
                vm.isCurrMonthJoinDt = 'Y';
            }
        }

        vm.backToList = function (pState) {
            //alert(pState);

            if (pState == 'EmployeeEntryPay') {                
                return $state.go('EmployeeEntryPayList', { pState: pState });
            }
            else {
                return $state.go('EmployeeList', { pState: pState });
            }
        };

        ///======== Start for Nominee ==========
        vm.nomForm = {
            HR_EMP_NOMINEE_ID: -1, NOM_NAME_EN: null, NOM_NAME_BN: null, NOM_ADDRESS_EN: null, NOM_ADDRESS_BN: null,
            NOM_MOB_NO: null, PCT_SHARE: 100, NOM_IMG: null, NOM_IMG_FILE: null
        };

        $scope.$watchGroup(['vm.nomForm.NOM_IMG_FILE'], function (newVal, OldVal) {
            if (newVal[0]) {
                vm.nomForm.NOM_IMG = newVal[0].base64;
            }
            
        }, true);

        vm.onChangeNomRelation = function (key) {
            console.log(key);
        }

        function getNomineeList() {

            return vm.nomineeGridDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.getDataByFullUrl('/api/hr/Employee/GetGmtNomineeList?pHR_EMPLOYEE_ID=' + (vm.form.HR_EMPLOYEE_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        vm.nomineeGridOption = {
            height: '150px',
            sortable: false,
            scrollable: true,
            pageable: false,
            editable: false,
            selectable: "row",
            columns: [
                { field: "NOM_NAME_EN", title: "Name", width: "20%" },
                { field: "NOM_ADDRESS_EN", title: "Address", width: "30%" },
                { field: "NOM_MOB_NO", title: "Contract#", width: "20%" },
                { field: "RELATION_WITH_NOM", title: "Relation", width: "10%" },
                { field: "PCT_SHARE", title: "Share(%)", width: "10%" },
                {
                    title: "",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editNomRow(dataItem)' ><i class='fa fa-edit'></i></button>&nbsp;" +
                            "<button type='button' class='btn btn-xs red' ng-click='vm.removeNomRow(dataItem)' ><i class='fa fa-remove'></i></button>";
                    },
                    width: "15%"
                }
            ]
        };

        
        vm.addNomRow = function () {
            
            var listLength = vm.nomineeGridDataSource.data().length;
            var dataList = vm.nomineeGridDataSource.data();
            var sharePct = 0;

            angular.forEach(dataList, function (val, key) {
                sharePct = sharePct + val['PCT_SHARE'];
            });

            if (listLength >= 3) {
                alert('Sorry! You can add maximum 3 nominee');
            }
            else {
                vm.nomForm.HR_EMP_NOMINEE_ID = 0;
                vm.nomineeGridDataSource.insert(vm.nomForm);
                vm.resetNomRow();
            }
            
        }

        vm.editNomRow = function (dataItem) {
            var data = angular.copy(dataItem);
            //data['OLD_PCT_SHARE'] = data.PCT_SHARE;

            vm.nomForm = data; //{ RF_GARM_PART_ID: dataItem.RF_GARM_PART_ID, GARM_PART_CODE: dataItem.GARM_PART_CODE, GARM_PART_NAME: dataItem.GARM_PART_NAME, IS_FLAT_CIR: dataItem.IS_FLAT_CIR, IS_ACTIVE: dataItem.IS_ACTIVE };
        }

        vm.updateNomRow = function (data) {
                        
            var dataItem = vm.nomineeGridDataSource.getByUid(data.uid);
            console.log(dataItem);

            dataItem.set('LK_RELATION_ID', data.LK_RELATION_ID);
            dataItem.set('RELATION_WITH_NOM', data.RELATION_WITH_NOM);
            dataItem.set('NOM_NAME_EN', data.NOM_NAME_EN);
            dataItem.set('NOM_NAME_BN', data.NOM_NAME_BN);
            dataItem.set('NOM_ADDRESS_EN', data.NOM_ADDRESS_EN);
            dataItem.set('NOM_ADDRESS_BN', data.NOM_ADDRESS_BN);
            dataItem.set('NOM_MOB_NO', data.NOM_MOB_NO);
            dataItem.set('PCT_SHARE', data.PCT_SHARE);
            dataItem.set('NOM_IMG', data.NOM_IMG);
            dataItem.set('NOM_IMG_FILE', data.NOM_IMG_FILE);

            dataItem.set('editMode', false);

            console.log(dataItem);

            vm.resetNomRow();
            
        }

        vm.removeNomRow = function (dataItem) {
            vm.nomineeGridDataSource.remove(dataItem);
        };

        vm.resetNomRow = function () {
            var item = angular.copy(vm.nomForm);

            vm.nomForm = { 
                HR_EMP_NOMINEE_ID: -1, LK_RELATION_ID: item.LK_RELATION_ID, NOM_NAME_EN: null, NOM_NAME_BN: null, NOM_ADDRESS_EN: null, NOM_ADDRESS_BN: null,
                NOM_MOB_NO: null, PCT_SHARE: 100, NOM_IMG: null, NOM_IMG_FILE: null
            };
        }        
        //========= End for Nominee

        vm.nextTab = function () {
            //alert('test');
            vm.tabID = vm.tabID + 1;
            vm.tabs[vm.tabID].active = true;

            if (vm.isEmpPayShow != true) {                
                getEmpPayGrid();                
            }
        };

        vm.previousTab = function () {
            //alert('test');
            vm.tabID = vm.tabID - 1;
            vm.tabs[vm.tabID].active = true;
        };

        /////////////////////////////////////////////
        vm.nextTab1 = function () {
            //alert('test');
            vm.tabID = vm.tabID + 1;
            vm.tabs1[vm.tabID].active = true;

            if (vm.isEmpPayShow != true) {
                getEmpPayGrid();
            }
        };

        vm.previousTab1 = function () {
            //alert('test');
            vm.tabID = vm.tabID - 1;
            vm.tabs1[vm.tabID].active = true;
        };
        ///////////////////////////////////////////////

        vm.copyPresentAddress = function () {
            vm.form.PERM_ADDRESS_EN = vm.form.PRE_ADDRESS_EN;
            vm.form.PERM_DIVISION_ID = vm.form.PRE_DIVISION_ID
            vm.form.PERM_DISTRICT_ID = vm.form.PRE_DISTRICT_ID;
            vm.form.PERM_UPOZILA_ID = vm.form.PRE_UPOZILA_ID;
            vm.form.PERM_POSTAL_CODE = vm.form.PRE_POSTAL_CODE;
        };

        vm.fullNameE = function () {
            vm.form.EMP_FULL_NAME_EN = vm.form.EMP_FIRST_NAME_EN + ' ' + vm.form.EMP_LAST_NAME_EN;
        };

        vm.TranCancel = function () {            
            vm.reset();
            vm.errors = undefined;
        };

        vm.reset = function () {
            vm.insert = true;
            vm.formCopy = vm.form;
            vm.form = { IS_ACTIVE: 'Y' };

            //vm.form.HR_COMPANY_GRP_ID = vm.formCopy.HR_COMPANY_GRP_ID;
            //vm.form.LK_OFF_TYPE_ID = vm.formCopy.LK_OFF_TYPE_ID;
            //vm.form.HR_TIMEZONE_ID = vm.formCopy.HR_TIMEZONE_ID;
            //vm.form.HR_GEO_DISTRICT_ID = vm.formCopy.HR_GEO_DISTRICT_ID;
            //vm.form.HR_COUNTRY_ID = vm.formCopy.HR_COUNTRY_ID;            
        };

        vm.exchangeProxy = function (isValid, form) {
            $http({
                headers: { "RequestVerificationToken": vm.antiForgeryToken },
                url: '/Hr/HrEmployee/ExchangeProxyUpdate',
                method: 'post',
                data: { pHR_EMPLOYEE_ID: form.HR_EMPLOYEE_ID, pTA_PROXI_ID: form.TA_PROXI_ID, pTA_PROXI_ID_OLD: form.TA_PROXI_ID_OLD }
            }).success(function (data, status, headers, config1) {
                vm.errors = undefined;
                if (data.success === false) {
                    vm.errors = data.errors;
                }
                else {
                    if (data.vMsg.substr(0, 9) == 'MULTI-001') {
                        vm.reset();
                    };
                    config.appToastMsg(data.vMsg);

                }
            }).error(function (data, status, headers, config) {
                alert('something went wrong')
                console.log(status);
            });  
        };
            
        
        function uploadPhoto() {
            var data = new FormData();
            var files = $("#ATT_FILE").get(0).files;
            // Add the uploaded image content to the form data collection                                
            if (files.length > 0) {
                data.append("UploadedImage", files[0]);
                data.append("EMPLOYEE_CODE", vm.form.EMPLOYEE_CODE);
            }
            $.ajax({
                method: 'post',
                url: '/Hr/HrEmployee/UploadFiles',
                contentType: false,
                processData: false,
                data: data
            });
        };

        vm.submit = function (isValid, form, insert) {
            
            //if (!isValid) return;
            
            //alert(insert);
                           
            uploadPhoto();
            vm.nomSL = 0;

            var nomDataList = vm.nomineeGridDataSource.data();
            console.log(nomDataList);

            var pctVal=0;
            angular.forEach(nomDataList, function (val, key) {
                pctVal = parseInt(pctVal) + parseInt(val['PCT_SHARE']);
            });

            if (pctVal>0 && pctVal != 100) {
                alert('Nominee`s percent should be 100');
                return;
            }

            form['EMP_NOM_DTL_XML'] = HrService.xmlStringShort(nomDataList.map(function (ob) {
                console.log(ob);

                vm.nomSL=vm.nomSL+1;
                if (vm.nomSL == 1) {
                    ob['NOM_SL'] = 1;
                    form.NOM_IMG1 = ob.NOM_IMG;
                }
                else if (vm.nomSL == 2) {
                    ob['NOM_SL'] = 2;
                    form.NOM_IMG2 = ob.NOM_IMG;
                }
                else if (vm.nomSL == 3) {
                    ob['NOM_SL'] = 3;
                    form.NOM_IMG3 = ob.NOM_IMG;
                }

                return ob;
            }));


            console.log(form);

            if (insert) {
                
                form.HR_EMPLOYEE_ID = 0;
                if (form.PAGE_NAME == 'EmployeeEntryPay' && form.GROSS_SALARY > 0) {
                    form.EMP_PAY_LIST = vm.empPayElementData._data; //$("#payGrid").data("kendoGrid").dataSource.data();
                }
                else {
                    form.EMP_PAY_LIST = null;//$("#payGrid").data("kendoGrid").dataSource.data();
                }
                
                
                $http({
                    headers: { "RequestVerificationToken": vm.antiForgeryToken },
                    url: '/Hr/HrEmployee/Save',
                    method: 'post',
                    data: form
                }).success(function (data, status, headers, config1) {
                    vm.errors = undefined;
                    if (data.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        data['data'] = angular.fromJson(data.jsonStr);

                        if (data.data.PMSG.substr(0, 9) == 'MULTI-001') {                            
                            vm.form.HR_EMPLOYEE_ID = data.data.PEMP_ID;
                            vm.form.EMPLOYEE_CODE = data.data.PEMP_CODE;
                            vm.form.TA_PROXI_ID = data.data.PPROXI_ID;
                            vm.insert = false;

                            vm.nomineeGridDataSource.read();
                        }
                        
                        console.log(data['data']);
                        config.appToastMsg(data.data.PMSG);
                    }
                }).error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });

                //entityService.saveTutorial(form, '/Hr/HrEmployee/Save', vm.antiForgeryToken)
                //    .then(function (data, status, headers, config1) {
                    
                //    vm.errors = undefined;
                //    if (data.success === false) {
                //        vm.errors = data.errors;
                //    }
                //    else {                        
                //        config.appToastMsg(data.vMsg);                       
                //    }
                    
                //}).catch(function (message) {
                //    exception.catcher('XHR loading Failded')(message);
                //});
                
            
            }
            else {                
                if (form.PAGE_NAME == 'EmployeeEntryPay' && form.GROSS_SALARY > 0) {
                    form.EMP_PAY_LIST = $("#payGrid").data("kendoGrid").dataSource.data();
                }
                else {
                    form.EMP_PAY_LIST = null;//$("#payGrid").data("kendoGrid").dataSource.data();
                }
                               
                //console.log(formData);

                $http({
                    headers: { "RequestVerificationToken": vm.antiForgeryToken },
                    url: '/Hr/HrEmployee/Update',
                    method: 'post',
                    data: { ob:form }
                }).success(function (data, status, headers, config1) {
                    vm.errors = undefined;
                    if (data.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        //if (data.vMsg.substr(0, 9) == 'MULTI-001') {
                        //    vm.reset();
                        //};                        
                        config.appToastMsg(data.vMsg);

                    }
                }).error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });
                                 
                
                //console.log(form);

                //entityService.saveTutorial(form, '/Hr/HrEmployee/Update', vm.antiForgeryToken)
                //    .then(function (data, status, headers, config1) {
                        
                //        vm.errors = undefined;
                //        if (data.success === false) {
                //            vm.errors = data.errors;
                //        }
                //        else {
                //            config.appToastMsg(data.vMsg);
                //        }

                //    }).catch(function (message) {
                //        exception.catcher('XHR loading Failded')(message);
                //    });            


            }

            return;

        };


        vm.titleListData = [{ TLTLE_ID: 'Mr.', TITLE_NAME: 'Mr.' }, { TLTLE_ID: 'Miss.', TITLE_NAME: 'Miss.' }, { TLTLE_ID: 'Mrs.', TITLE_NAME: 'Mrs.' }];
        
        function geoDevisionList() {
            vm.geoDevisionListData = {
                optionLabel: "Select",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrEmployee/GeoDivisionListData"  //+ "&pType=" + showType
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
                dataTextField: "DIVISION_NAME_EN",
                dataValueField: "HR_GEO_DIVISION_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    var vHR_GEO_DIVISION_ID = dataItem.HR_GEO_DIVISION_ID; //this.value();

                    //alert('select '+vParentID);

                    if (vHR_GEO_DIVISION_ID != null) {
                                                
                        $("#PRE_DISTRICT_ID").kendoDropDownList({
                            optionLabel: "Select",
                            filter: "startswith",
                            dataTextField: "DISTRICT_NAME_EN",
                            dataValueField: "HR_GEO_DISTRICT_ID",
                            dataSource: {
                                transport: {
                                    read: function (e) {
                                        $http({
                                            method: 'post',
                                            url: "/Hr/HrOffice/DistrictListData",  //+ "&pType=" + showType
                                            data: { pHR_GEO_DIVISION_ID: vHR_GEO_DIVISION_ID }
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
                                var vHR_GEO_DISTRICT_ID = dataItem.HR_GEO_DISTRICT_ID;
                                //console.log(dataItem);
                                $("#PRE_UPOZILA_ID").kendoDropDownList({
                                    optionLabel: "Select",
                                    filter: "startswith",
                                    dataTextField: "UPOZILA_NAME_EN",
                                    dataValueField: "HR_GEO_UPOZILA_ID",
                                    dataSource: {
                                        transport: {
                                            read: function (e) {
                                                $http({
                                                    method: 'post',
                                                    url: "/Hr/HrEmployee/UpozilaListData",  //+ "&pType=" + showType
                                                    data: { pHR_GEO_DISTRICT_ID: vHR_GEO_DISTRICT_ID }
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
                                    }
                                });
                            }
                        });

                    }

                }
            };
        };


        function permDevisionList() {
            vm.permDevisionListData = {
                optionLabel: "Select",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrEmployee/GeoDivisionListData"  //+ "&pType=" + showType
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
                dataTextField: "DIVISION_NAME_EN",
                dataValueField: "HR_GEO_DIVISION_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    var vHR_GEO_DIVISION_ID = dataItem.HR_GEO_DIVISION_ID; //this.value();

                    //alert('select '+vParentID);

                    if (vHR_GEO_DIVISION_ID != null) {

                        $("#PERM_DISTRICT_ID").kendoDropDownList({
                            optionLabel: "Select",
                            filter: "startswith",
                            dataTextField: "DISTRICT_NAME_EN",
                            dataValueField: "HR_GEO_DISTRICT_ID",
                            dataSource: {
                                transport: {
                                    read: function (e) {
                                        $http({
                                            method: 'post',
                                            url: "/Hr/HrOffice/DistrictListData",  //+ "&pType=" + showType
                                            data: { pHR_GEO_DIVISION_ID: vHR_GEO_DIVISION_ID }
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
                                var vHR_GEO_DISTRICT_ID = dataItem.HR_GEO_DISTRICT_ID;
                                //console.log(dataItem);
                                $("#PERM_UPOZILA_ID").kendoDropDownList({
                                    optionLabel: "Select",
                                    filter: "startswith",
                                    dataTextField: "UPOZILA_NAME_EN",
                                    dataValueField: "HR_GEO_UPOZILA_ID",
                                    dataSource: {
                                        transport: {
                                            read: function (e) {
                                                $http({
                                                    method: 'post',
                                                    url: "/Hr/HrEmployee/UpozilaListData",  //+ "&pType=" + showType
                                                    data: { pHR_GEO_DISTRICT_ID: vHR_GEO_DISTRICT_ID }
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
                                    }
                                });
                            }
                        });

                    }

                }
            };
        };

        function geoDistrictList() {
            vm.geoDistrictListData = {
                optionLabel: "Select",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrOffice/DistrictListData",  //+ "&pType=" + showType
                                data:{pHR_GEO_DIVISION_ID:null}
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
        };


        function preUpozilaList() {
            vm.preUpozilaListData = {
                optionLabel: "Select",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrEmployee/UpozilaListData",  //+ "&pType=" + showType
                                data: { pHR_GEO_DISTRICT_ID: null }
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
                dataTextField: "UPOZILA_NAME_EN",
                dataValueField: "HR_GEO_UPOZILA_ID"
            };
        };
        

        function relationList() {            
            return $http({
                url: '/Hr/HrEmployee/LookupListData',
                method: 'get',
                params: {
                    pLookupTableId: 15,
                }
            })
                .then(function (result) {                    
                    vm.relationListData = result.data;
                    vm.nomRelationListData = result.data;
                    //vm.form.LK_RELATION_ID = vm.relationListData[0].LOOKUP_DATA_ID;

                    vm.nomRelationOptions = {
                        optionLabel: "Select",
                        filter: "startswith",
                        autoBind: true,
                        dataSource: result.data,                        
                        dataTextField: "LK_DATA_NAME_EN",
                        dataValueField: "LOOKUP_DATA_ID",
                        select: function (e) {
                            var item = this.dataItem(e.item);
                            console.log(item);

                            vm.nomForm.RELATION_WITH_NOM = item.LK_DATA_NAME_EN;
                        }
                    };
                
                    return;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        function religionList() {
            return $http({
                url: '/Hr/HrEmployee/LookupListData',
                method: 'get',
                params: {
                    pLookupTableId: 2,
                }
            })
                .then(function (result) {
                    vm.religionListData = result.data;
                    //vm.form.LK_RELIGION_ID = vm.religionListData[0].LOOKUP_DATA_ID;
                    return;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        function genderList() {
            return $http({
                url: '/Hr/HrEmployee/LookupListData',
                method: 'get',
                params: {
                    pLookupTableId: 16,
                }
            })
                .then(function (result) {
                    vm.genderListData = result.data;
                    //vm.form.LK_GENDER_ID = vm.genderListData[0].LOOKUP_DATA_ID;
                    return;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        function maritalStatusList() {
            return $http({
                url: '/Hr/HrEmployee/LookupListData',
                method: 'get',
                params: {
                    pLookupTableId: 3,
                }
            })
                .then(function (result) {
                    vm.maritalStatusListData = result.data;
                    //vm.form.LK_MRTL_STATUS_ID = vm.maritalStatusListData[0].LOOKUP_DATA_ID;
                    return;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        vm.onChangeMaritalStatus = function () {
            if (vm.form.LK_MRTL_STATUS_ID != 8) {
                vm.form.NO_OF_CHILD = "";
            }
        }

        function nationalityList() {
            return $http({
                url: '/Hr/HrEmployee/LookupListData',
                method: 'get',
                params: {
                    pLookupTableId: 13,
                }
            })
                .then(function (result) {
                    vm.nationalityListData = result.data;
                    //vm.form.LK_NTLTY_ID=vm.nationalityListData[0].LOOKUP_DATA_ID;
                    return;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        function bloodGroupList() {
            return $http({
                url: '/Hr/HrEmployee/LookupListData',
                method: 'get',
                params: {
                    pLookupTableId: 14,
                },
            })
                .then(function (result) {
                    vm.bloodGroupListData = result.data;
                    return;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };
        
        function companyList() {
            return $http({
                url: '/Hr/HrEmployee/CompanyListData',
                method: 'get'
            })
                .then(function (result) {
                    vm.companyListData = result.data;
                    //vm.form.HR_COMPANY_ID = vm.companyListData[0].HR_COMPANY_ID;
                    return;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };
        
        function officeList() {
            return $http({
                url: '/Hr/HrEmployee/OfficeListData',
                method: 'get'
            })
                .then(function (result) {
                    vm.officeListData = result.data;
                    //vm.form.HR_OFFICE_ID = vm.officeListData[0].HR_OFFICE_ID;
                    return;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        function appointmentStatusList() {
            return $http({
                url: '/Hr/HrEmployee/LookupListData',
                method: 'get',
                params: {
                    pLookupTableId: 9,
                }
            })
                .then(function (result) {
                    vm.appointmentStatusListData = result.data;
                    //vm.form.LK_APNT_STATUS_ID = vm.appointmentStatusListData[1].LOOKUP_DATA_ID;
                    return;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };
        
        function jobStatusList() {
            return $http({
                url: '/Hr/HrEmployee/LookupListData',
                method: 'get',
                params: {
                    pLookupTableId: 11,
                },
            })
                .then(function (result) {
                    vm.jobStatusListData = result.data;
                    //vm.form.LK_JOB_STATUS_ID = vm.jobStatusListData[0].LOOKUP_DATA_ID;
                    return;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        function periodTypeList() {
            return $http({
                url: '/Hr/HrEmployee/PeriodTypeListData',
                method: 'get'
            })
                .then(function (result) {
                    vm.periodTypeListData = result.data;
                    return;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        function countryList() {
            return $http({
                url: '/Hr/HrEmployee/CountryListData',
                method: 'get'
            })
                .then(function (result) {
                    vm.countryListData = result.data;
                    return;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        //function floorList() {
        //    return $http({
        //        url: '/Hr/HrEmployee/LookupListData',
        //        method: 'get',
        //        params: {
        //            pLookupTableId: 18,
        //        },
        //    })
        //        .then(function (result) {
        //            vm.floorListData = result.data;
        //            return;
        //        }).catch(function (message) {
        //            exception.catcher('XHR loading Failded')(message);
        //        });
        //};

        //function lineList() {
        //    vm.lineListData = [{ LINE_NO: 0 }];

        //    for (var i = 0; i < 50; i++) {
        //        vm.lineListData[i] = { LINE_NO: i + 1 };
        //    }
        //};

        function floorList() {
            return vm.floorListData = {
                optionLabel: "Select",
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
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    vm.form.FLOOR_NAME = dataItem.LK_DATA_NAME_EN;
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function lineList() {
            return vm.lineListData = {
                optionLabel: "Select",
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

        function shiftTeamList() {
            return $http({
                url: '/Hr/HrEmployee/ShiftTeamListData',
                method: 'get',
                params: { pHR_SHIFT_PLAN_ID: null }
            })
                .then(function (result) {
                    vm.shiftTeamListData = result.data;
                    return;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        function departmentList() {
            vm.departmentListData = {
                optionLabel: "Select",
                filter: "startswith",
                autoBind: true,
                //dataSource: {
                //    transport: {
                //        read: function (e) {
                //            $http({
                //                method: 'post',
                //                url: "/Hr/HrEmployee/DeptListData",  //+ "&pType=" + showType   
                //                params: { pPARENT_ID: null }
                //            }).
                //            success(function (data, status, headers, config) {
                //                e.success(data)
                //            }).
                //            error(function (data, status, headers, config) {
                //                alert('something went wrong')
                //                console.log(status);
                //            });
                //        }
                //    }
                //},
                dataTextField: "DEPARTMENT_NAME_EN",
                dataValueField: "HR_DEPARTMENT_ID"
            };


            return vm.subDeptDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {

                        return HrService.getSubDepartmentData(vm.form.CORE_DEPT_ID || 0).then(function (res) {
                            console.log(res);
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        //function departmentList() {
        //    return $http({
        //        url: '/Hr/HrEmployee/DeptListData',
        //        method: 'get',
        //        params: { pPARENT_ID: null }
        //    })
        //    .then(function (result) {
        //        vm.departmentListData = result.data;
        //        return;
        //    }).catch(function (message) {
        //        exception.catcher('XHR loading Failded')(message);
        //    });                            
        //};
        

        vm.shiftPlanListData = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Hr/HrEmployee/ShiftPlanListData"  //+ "&pType=" + showType
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
            change: function shiftTeamList() {
                var vShiftID = this.value();
                //alert(vShiftID);
                return $http({
                    url: '/Hr/HrEmployee/ShiftTeamListData',
                    method: 'get',
                    params: { pHR_SHIFT_PLAN_ID: vShiftID }
                })
                    .then(function (result) {
                        vm.shiftTeamListData = result.data;
                        //vm.form.HR_SHIFT_TEAM_ID = vm.shiftTeamListData[0].HR_SHIFT_TEAM_ID;
                        return;
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
            },
            dataTextField: "SHIFT_PLAN_NAME_EN",
            dataValueField: "HR_SHIFT_PLAN_ID"
        };

        

        function parentDepartmentList() {
            vm.parentDepartmentListData = {
                optionLabel: "Select",
                filter: "startswith",
                autoBind: true,
                dataTextField: "DEPARTMENT_NAME_EN",
                dataValueField: "HR_DEPARTMENT_ID",
                //dataSource: {
                //    transport: {
                //        read: function (e) {
                //            $http({
                //                method: 'post',
                //                url: "/Hr/HrEmployee/ParentDeptListData"  //+ "&pType=" + showType
                //            }).
                //            success(function (data, status, headers, config) {
                //                e.success(data)
                //            }).
                //            error(function (data, status, headers, config) {
                //                alert('something went wrong')
                //                console.log(status);
                //            });
                //        }
                //    }//,
                //    //group: { field: "PARENT_NAME" }
                //},
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    var vParentID = dataItem.HR_DEPARTMENT_ID; //this.value();                    
                    vm.form.CORE_DEPT_ID = dataItem.HR_DEPARTMENT_ID;

                    if (vParentID != null) {
                       
                        vm.subDeptDataSource.read();

                        //HrService.getSubDepartmentData(vParentID).then(function (res) {
                        //    $("#HR_DEPARTMENT_ID").kendoDropDownList({
                        //        autoBind: true,
                        //        optionLabel: "Select",
                        //        dataTextField: "DEPARTMENT_NAME_EN",
                        //        dataValueField: "HR_DEPARTMENT_ID",
                        //        dataSource: res,
                        //        filter: "startswith"
                        //    });

                        //});


                        HrService.getDesigData(vParentID).then(function (res) {
                            $("#dropdownlist").kendoDropDownList({
                                autoBind: true,
                                optionLabel: "Select",
                                dataTextField: "DESIGNATION_NAME_EN",
                                dataValueField: "HR_DESIGNATION_ID",
                                dataSource: res,
                                filter: "startswith",
                                select: function (e) {
                                    var dataItem = this.dataItem(e.item);
                                    vm.form.HR_GRADE_ID = dataItem.HR_GRADE_ID;
                                }
                            });

                        });

                    }

                }
            };


            return vm.coreDeptDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {

                        return $http({
                            method: 'post',
                            url: "/Hr/HrEmployee/ParentDeptListData"  //+ "&pType=" + showType
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
            });

        };
        
                     
        vm.designationListData = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Hr/HrEmployee/DesigListData",  //+ "&pType=" + showType
                            data: { pOrganoDeptId: vm.form.PARENT_ID }
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
                vm.form.HR_GRADE_ID = dataItem.HR_GRADE_ID;
            },
            dataTextField: "DESIGNATION_NAME_EN",
            dataValueField: "HR_DESIGNATION_ID"
        };
        

        vm.gradeListData = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Hr/HrEmployee/GradeListData"  //+ "&pType=" + showType
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
            dataTextField: "GRADE_NO_EN",
            dataValueField: "HR_GRADE_ID"
        };
          
        

        //vm.gridOptions = {
        //    autoBind: true,
        //    dataSource: {
        //        transport: {
        //            read: function (e) {
        //                $http({
        //                    method: 'post',
        //                    url: "/Hr/HrOffice/OfficeListData"  //+ "&pType=" + showType
        //                }).
        //                success(function (data, status, headers, config) {
        //                    e.success(data)
        //                }).
        //                error(function (data, status, headers, config) {
        //                    alert('something went wrong')
        //                    console.log(status);
        //                });
        //            }                    
        //        },
        //        pageSize: 10
        //    },
        //    filterable: {
        //        mode: "row"
        //    },
        //    selectable: "row",
        //    sortable: true,
        //    pageSize: 10,
        //    pageable: {
        //        refresh: true,
        //        pageSizes: true,
        //        buttonCount: 5
        //    },
        //    columns: [                
        //        //{ field: "HR_YRLY_CALNDR_ID", title: "ID" },            
        //        //{ field: "DESIGNATION_CODE", title: "Code", type: "string", width: "150px" },
        //        { field: "OFFICE_CODE", title: "Code", type: "string", width: "100px" },
        //        { field: "OFFICE_NAME_EN", title: "Name", type: "string", width: "200px" },
        //        { field: "OFFICE_TYPE_NAME_EN", title: "Office Type", type: "string", width: "150px" },
        //        { field: "ADDRESS_EN", title: "Address", type: "string", width: "200px" },
        //        { field: "PHONE_NO", title: "Phone#", type: "string", width: "150px" },
        //        //{ field: "HR_COMPANY_GRP_NAME_EN", title: "Group Name", type: "string", width: "200px" },
                
        //        //{ field: "COMP_BUS_TYPE_NAME", title: "Business Type", type: "string", width: "150px" },
        //        //{ field: "VAT_REG_NO", title: "Vat Registation#", type: "string", width: "150px" },
        //        //{ field: "YEAR_ESTD", title: "Establish Year", type: "string", width: "100px" },
        //        //{ field: "COMP_WEB", title: "Web Site", type: "string", width: "200px" },
        //        //{ field: "COMP_DESC", title: "Description", type: "string", width: "300px" },

        //        //{ field: "IS_ACTIVE", title: "Active?", type: "string", width: "100px" }
        //        {
        //            title: "Action",
        //            template: function () {
        //                return "<a ng-click='vm.tabs[0].active = true;vm.editRecord(dataItem)' title='Edit'><i class='fa fa-edit'></i></a>";
        //                //return "<a ng-click='vm.editRecord(dataItem)' title='Edit'><i class='fa fa-edit'></i></a>";
        //            },
        //            width: "50px"
        //        }
        //    ]
        //};


        vm.tabs = [
        { title: 'Personal Information', content: "<h3>Dynamic content 1</h3>", disabled: true },
        { title: 'Job Detail', content: 'Dynamic content 2', disabled: true },
        { title: 'Contract Detail', content: 'Dynamic content 3', disabled: true },
        { title: 'Nominee Detail', content: 'Dynamic content 4', disabled: true },
        { title: 'Document', content: 'Dynamic content 5', disabled: true },
        //{ title: 'Allowance/Deduction', content: 'Dynamic content 6', disabled: true },
        { title: 'Optional', content: 'Dynamic content 7', disabled: true }
        ];

        vm.tabs1 = [
        { title: 'Personal Information', content: "<h3>Dynamic content 1</h3>", disabled: true, active: false },
        { title: 'Job Detail', content: 'Dynamic content 2', disabled: true },
        { title: 'Contract Detail', content: 'Dynamic content 3', disabled: true, active: false },
        { title: 'Document', content: 'Dynamic content 4', disabled: true, active: false },
        { title: 'Allowance/Deduction', content: 'Dynamic content 5', disabled: true, active: true },
        { title: 'Optional', content: 'Dynamic content 6', disabled: true, active: false }
        ];


        function activate(){
            var promise = [geoDevisionList(), geoDistrictList(), preUpozilaList(), permDevisionList(), relationList(), religionList(), genderList(), maritalStatusList(), nationalityList(),
                bloodGroupList(), companyList(), officeList(), appointmentStatusList(), jobStatusList(), periodTypeList(),
                countryList(), floorList(), lineList(), shiftTeamList(), parentDepartmentList(), departmentList(), //getEmpPayListData(),
                getPayElementListData(), getNomineeList()];

            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;

                vm.isEmpPayShow = true;
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