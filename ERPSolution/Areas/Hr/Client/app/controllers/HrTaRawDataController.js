(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrTaRawDataController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', 'HrService', 'entityService', HrTaRawDataController]);

    function HrTaRawDataController(logger, config, $q, $scope, $http, exception, $filter, $state, HrService, entityService) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.dtFormat = config.appDateFormat;
        vm.timeFormat = config.appTimeFormat;
        vm.showSplash = true;
        
        vm.insert = true;
        vm.today = new Date();


        //var currMonth=moment()
        var lastDayOfMonth = moment().daysInMonth();

        var firstDate = moment().date(1);
        var lastDate = moment().date(lastDayOfMonth);


        vm.form = [];
        var dt = firstDate._d;
        //vm.form.FROM_DT = $filter('date')(dt, vm.dtFormat);
        dt = lastDate._d;
        //vm.form.TO_DT = $filter('date')(dt, vm.dtFormat);

        $scope.isShowDeviceList = 'N';
        vm.IMPORT_TYPE = 99;
        vm.form.IMPORT_TYPE = vm.IMPORT_TYPE;
        vm.isTextFile = false;
        
        
        vm.isShowDeviceListChange = function () {
            vm.IMPORT_TYPE = vm.form.IMPORT_TYPE;
            vm.form.IMPORT_TYPE = null;
        };
             
        if ($state.current.name != "IDCardPrint") {
              
            //console.log($state.current.name);
            $scope.$watch('vm.form.IMPORT_TYPE', function (newVal, oldVal) {
                //alert(newVal + ' ' + oldVal);
                if (newVal == null) {
                    vm.form.IMPORT_TYPE = vm.IMPORT_TYPE;
                    newVal = vm.IMPORT_TYPE;
                };

                //if (newVal != oldVal) {
                if (vm.form.IMPORT_TYPE == 3 || vm.form.IMPORT_TYPE == 4 || vm.form.IMPORT_TYPE == 98) {
                    vm.isTextFile = true;
                }
                else {
                    vm.isTextFile = false;
                }

                HrService.getDeviceData(newVal).then(function (res) {

                    $("#grid").kendoGrid({
                        autoBind: true,
                        dataSource: {
                            data: res,
                            pageSize: 5
                        },
                        selectable: "row",
                        sortable: true,
                        pageable: {
                            refresh: true,
                            pageSizes: true,
                            buttonCount: 5
                        },
                        columns: [
                            { field: "TA_DEVICE_CODE", title: "Device Code", type: "string", width: "100px" },
                            { field: "MODEL_NBR", title: "Model", type: "string", width: "100px" },
                            { field: "POSITION_DESC", title: "Location", type: "string", width: "200px" },
                            { field: "IP_NBR", title: "IP Address", type: "string", width: "100px" },
                            { field: "PORT_NBR", title: "Port", type: "string", width: "100px" },
                            { field: "IS_ON_OR_OFF", title: "On/Off", type: "string", width: "30px", hidden: true },
                            {
                                title: "On/Off",
                                template: function (e) {

                                    if (e.IS_ON_OR_OFF == "Y") {
                                        return "&nbsp;&nbsp;&nbsp;<span class='badge badge-success'>&nbsp;</span>";
                                    }
                                    else if (e.IS_ON_OR_OFF == "N") {
                                        return "&nbsp;&nbsp;&nbsp;<span class='badge badge-danger'>&nbsp;</span>";
                                    }
                                    else {
                                        return "<span>&nbsp;&nbsp;&nbsp;N/A</span>";
                                    }

                                },
                                width: "50px"
                            }
                            //{ field: "IS_ON_OR_OFF", title: "On/Off", type: "string", width: "50px" }

                        ]
                    });

                });

                //};
            });
        }

                
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.fromDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.fromDateOpened = true;
        };

        vm.toDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.toDateOpened = true;
        };


        //vm.submit = function (isValid, form, insert) {

        //    //if (!isValid) return;

        //    //alert('ok');            

        //    if (insert) {

        //        entityService.saveTutorial(form, '/Hr/HrTaRawData/ImportRawData', vm.antiForgeryToken)
        //            .then(function (data, status, headers, config1) {

        //                vm.errors = undefined;
        //                if (data.success === false) {
        //                    vm.errors = data.errors;
        //                }
        //                else {
        //                    config.appToastMsg(data.vMsg);
        //                }

        //            }).catch(function (message) {
        //                exception.catcher('XHR loading Failded')(message);
        //            });


        //    }
        //}

        function uploadHOTextFile() {
            
            var data = new FormData();
            var files = $("#ATT_FILE").get(0).files;
            // Add the uploaded image content to the form data collection                                
            if (files.length > 0) {
                data.append("UploadedImage", files[0]);
            }
            $.ajax({
                method: 'post',
                url: '/Hr/HrTaRawData/uploadHOTextFile',
                contentType: false,
                processData: false,
                data: data
            });
        };


        vm.importData = function () {
            
            //console.log(form.ATT_FILE);
            
            var x=confirm("Are you want to collect attendance data?");
            if (x == false) {
                return;
            }

            if ($state.is("ImportRawdata.List")) {
                $state.go('ImportRawdata');
            }

            
            if (vm.form.IMPORT_TYPE == 3 || vm.form.IMPORT_TYPE == 4 || vm.form.IMPORT_TYPE == 98) {
                uploadHOTextFile();
            }

            //entityService.saveTutorial(form, '/Hr/HrTaRawData/Import', vm.antiForgeryToken)
            //        .then(function (data, status, headers, config1) {

            //            vm.errors = undefined;
            //            if (data.success === false) {
            //                vm.errors = data.errors;
            //            }
            //            else {
            //                config.appToastMsg(data.vMsg);
            //            }

            //        }).catch(function (message) {
            //            exception.catcher('XHR loading Failded')(message);
            //        });

            $http({
                headers: { "RequestVerificationToken": vm.antiForgeryToken },
                url: '/Hr/HrTaRawData/Import',
                method: 'post',              
                data: { pIMPORT_TYPE: vm.form.IMPORT_TYPE }
            }).then(function (data, status, headers, config1) {

                vm.errors = undefined;
                if (data.success === false) {
                    vm.errors = data.errors;
                }
                else {
                    config.appToastMsg(data.data.vMsg);
                    alert(data.data.vMsg.substr(9));
                    $state.go('ImportRawdata.List');
                }

            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });

        };
        
        /////////////////// ================= For ID CardPrint ================ ///////////////////////////
        vm.cardPrintOptionList = [{ CARD_PRINT_OPTION_ID: 1, CARD_PRINT_OPTION_NAME: "Bangla Font" }, { CARD_PRINT_OPTION_ID: 2, CARD_PRINT_OPTION_NAME: "English Font" }];
        vm.CARD_PRINT_OPTION = 1;
        vm.CARD_PRINT_OPTION_ID = 1;
        
        vm.onChangeCardPrintOption = function (itm) {
            console.log(itm);

            vm.CARD_PRINT_OPTION_ID = itm.CARD_PRINT_OPTION_ID;
        }

        vm.empListData = [];
        $("#salaryTranGrid").empty();
        vm.empIndex = -1;

        vm.add = function () {
            if (vm.form.HR_EMPLOYEE_ID != null) {
                
                vm.empIndex = vm.empIndex + 1;

                var index = HrService.getIndexByKeyValue(vm.empListData, 'HR_EMPLOYEE_ID', vm.form.HR_EMPLOYEE_ID);

                if (index == null) {                    
                    vm.idCardPrintGrid();                    
                }
                else {
                    alert('Sorry! The selected employee already exists.');
                }

                vm.form = {};                
            }
        };

        vm.TA_FLAG = "";
        vm.idCardPrintGrid = function () {
            //vm.form.RECORD_SL = vm.empListData.length + 1;            

            //$('#IDCardPrintGrid').data("kendoGrid").dataSource.insert(0, {
            //    RECORD_SL: vm.form.RECORD_SL, HR_EMPLOYEE_ID: vm.form.HR_EMPLOYEE_ID, EMPLOYEE_CODE: vm.form.EMPLOYEE_CODE, EMP_FULL_NAME_EN: vm.form.EMP_FULL_NAME_EN,
            //    DESIGNATION_NAME_EN: vm.form.DESIGNATION_NAME_EN, SECTION: vm.form.CORE_DEPARTMENT_NAME + '-' + vm.form.DEPARTMENT_NAME_EN});

            var dataSource = new kendo.data.DataSource({
                batch: true,
                transport: {
                    read: function (e) {
                        vm.form.RECORD_SL = vm.empListData.length + 1;

                        vm.empListData.push({
                            RECORD_SL:vm.form.RECORD_SL, HR_EMPLOYEE_ID: vm.form.HR_EMPLOYEE_ID, EMPLOYEE_CODE: vm.form.EMPLOYEE_CODE, EMP_FULL_NAME_EN: vm.form.EMP_FULL_NAME_EN,
                            DESIGNATION_NAME_EN: vm.form.DESIGNATION_NAME_EN, SECTION: vm.form.CORE_DEPARTMENT_NAME + '-' + vm.form.DEPARTMENT_NAME_EN                            
                        });

                        if (vm.empListData.length > 0) {
                            vm.TA_FLAG = "";
                            for (var i = 0; i < vm.empListData.length; i++) {
                                if (vm.TA_FLAG == "") {
                                    vm.TA_FLAG = vm.empListData[i].HR_EMPLOYEE_ID;
                                }
                                else {
                                    vm.TA_FLAG = vm.TA_FLAG + ',' + vm.empListData[i].HR_EMPLOYEE_ID;
                                }
                            }
                        }                                               

                        e.success(vm.empListData);
                    },
                    destroy: function (e) {                        
                        //console.log(e.data.models[0].HR_EMPLOYEE_ID);
                        var index = HrService.getIndexByKeyValue(vm.empListData, 'HR_EMPLOYEE_ID', e.data.models[0].HR_EMPLOYEE_ID);                        
                        vm.empListData.splice(index, 1);
                        
                        e.success(vm.empListData);
                                                
                        if (vm.empListData.length > 0) {
                            vm.TA_FLAG = "";
                            for (var i = 0; i < vm.empListData.length; i++) {
                                if (vm.TA_FLAG == "") {
                                    vm.TA_FLAG = vm.empListData[i].HR_EMPLOYEE_ID;
                                }
                                else {
                                    vm.TA_FLAG = vm.TA_FLAG + ',' + vm.empListData[i].HR_EMPLOYEE_ID;
                                }
                            }
                        }                        
                    },
                    parameterMap: function (options, operation) {
                        if (operation !== "read" && options.models) {
                            return { models: kendo.stringify(options.models) };
                        }
                    }
                },
                sort: [{ field: 'RECORD_SL', dir: 'desc' }],
                pageSize: 1000,
                schema: {
                    model: {
                        id: "HR_EMPLOYEE_ID",
                        fields: {
                            RECORD_SL: { type: "number", editable: false },
                            HR_EMPLOYEE_ID: { type: "number", editable: false },
                            EMPLOYEE_CODE: { type: "string", editable: false },
                            EMP_FULL_NAME_EN: { type: "string", editable: false },
                            SECTION: { type: "string", editable: false },
                            DESIGNATION_NAME_EN: { type: "string", editable: false }
                        }
                    }
                }
                //aggregate: [
                //    { field: "PAY_AMT", aggregate: "sum" }
                //]
            });


            //var treeView = $("#IDCardPrintGrid").data("kendoGrid"), message;
            //console.log(treeView.dataSource._data);

            //console.log(dataSource._data);

            $("#IDCardPrintGrid").kendoGrid({
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
                    { template: '<img src="/UPLOAD_DOCS/EMP_PHOTOS/#: data.EMPLOYEE_CODE #.jpg" alt="No Photo" style="border:1px solid black; height:55px;width:50px" />', width: "40px" },
                    { field: "RECORD_SL", title: "SL", width: "50px", hidden: true },
                    { field: "EMPLOYEE_CODE", title: "Emp. Code", width: "60px" },
                    { field: "EMP_FULL_NAME_EN", title: "Name", width: "200px" },
                    { field: "SECTION", title: "Section", width: "100px" },
                    { field: "DESIGNATION_NAME_EN", title: "Designation", width: "100px" },
                    { command: "destroy", text: "Remove", title: "&nbsp;", width: "50px" }
                    //{ template: '<img id="' + vm.form.RECORD_SL + '" src="/UPLOAD_DOCS/EMP_PHOTOS/' + vm.form.EMPLOYEE_CODE + '.jpg" alt="No Photo" style="border:1px solid black; height:55px;width:50px" />', width: "60px" }
                ],                
                editable: {
                    confirmation: false, //"Do you want to remove this record?",
                    mode: "inline"
                }
            });

            //function unitTypeListData(container, options) {
            //    //console.log(options.field);
            //    $('<input required data-text-field="UNIT_TYPE_NAME" data-value-field="UNIT_TYPE_ID" data-bind="value:' + options.field + '"/>')
            //        .appendTo(container)
            //        .kendoDropDownList({
            //            autoBind: false,
            //            dataSource: unitTypeList
            //        });
            //};
        };

        

        $scope.emoloyeeAuto = function (val) {
            return HrService.EmployeeAutoDataList(val, 30, null).then(function (res) {
                return res;
            });
        };

        $scope.onSelectItem = function (item) {
            //console.log(item);            
            vm.form.HR_EMPLOYEE_ID = item.HR_EMPLOYEE_ID;
            vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN;
            vm.form.CORE_DEPARTMENT_NAME = item.CORE_DEPARTMENT_NAME;
            vm.form.DEPARTMENT_NAME_EN = item.DEPARTMENT_NAME_EN;
            vm.form.DESIGNATION_NAME_EN = item.DESIGNATION_NAME_EN;
            //vm.form.COMP_NAME_EN = item.COMP_NAME_EN;
            //vm.form.HR_COMPANY_ID = item.HR_COMPANY_ID;
            vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN + ', ' + item.DESIGNATION_NAME_EN + ', ' + item.CORE_DEPARTMENT_NAME + '-'+ item.DEPARTMENT_NAME_EN;
        }

        $scope.$watch('vm.form.EMPLOYEE_CODE', function (newVal, oldVal) {

            if (!newVal || newVal == '') {
                vm.form.HR_EMPLOYEE_ID = null;
                vm.form.EMP_FULL_NAME_EN = null;
                vm.form.DEPARTMENT_NAME_EN = null;
                vm.form.DESIGNATION_NAME_EN = null;
                //vm.form.COMP_NAME_EN = null;
                //vm.form.HR_COMPANY_ID = null;
                //vm.form.EMP_FULL_NAME_EN = null;
            }

        });


        
        /////////////////// ================= For ID CardPrint ================ ///////////////////////////





        function activate(){
            var promise = [];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

       
            
        
    }

    

})();