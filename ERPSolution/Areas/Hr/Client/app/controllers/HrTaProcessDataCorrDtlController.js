(function () {
    'use strict';
    angular.module('multitex.hr').controller('HrTaProcessDataCorrDtlController', ['$q', 'config', 'HrService', '$http', '$stateParams', '$state','logger', HrTaProcessDataCorrDtlController]);
    function HrTaProcessDataCorrDtlController($q, config, HrService, $http, $stateParams, $state, logger) {

        var vm = this;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getDayTypeList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        vm.form = {
            HR_DAY_TYPE_ID: '',
            TIME: {
                IN_TIME_WT: null,
                OUT_TIME_WT :null
            }
        };
        
        function getDayTypeList() {
            return $http({
                method: 'get',
                url: '/hr/HrYearlyCalander/DayTypeListData'
            }).then(function (res) {
                vm.dayTypeList = res.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

 
        vm.formData = $stateParams.formData;
        vm.saveBatchData = function () {
            var time = {};
            var data = [];
            if (vm.form['IS_BATCH_UPDATE']) {
                
                time = angular.copy(vm.form['TIME']);
                delete vm.form['TIME'];
                delete vm.form['IS_BATCH_UPDATE'];

                if (moment(time.IN_TIME_WT).get('hour') == 0 && moment(time.IN_TIME_WT).get('minute') == 0) {
                    time['IN_TIME_WT']=null;
                }

                if (moment(time.OUT_TIME_WT).get('hour') == 0 && moment(time.OUT_TIME_WT).get('minute') == 0) {
                    time['OUT_TIME_WT']=null;
                }

                angular.forEach(vm.form, function (val, key) {
                    if (val.Ckd) {
                        if (vm.formData.PUNCH_TYPE == 105) {
                            val['IN_TIME_WT']=time.IN_TIME_WT;
                        } else if (vm.formData.PUNCH_TYPE == 106) {
                            val['OUT_TIME_WT']=time.OUT_TIME_WT;
                        } else if (vm.formData.PUNCH_TYPE == 107 || vm.formData.PUNCH_TYPE == 629) {
                            val['IN_TIME_WT'] = time.IN_TIME_WT;
                            val['OUT_TIME_WT'] = time.OUT_TIME_WT;
                        } else if(vm.formData.PUNCH_TYPE == 437) {
                            val['IN_TIME_WT'] = null;
                            val['OUT_TIME_WT'] = null;
                        }
                    }

                    data.push({
                        HR_TA_PROCESS_DATA_ID: val.HR_TA_PROCESS_DATA_ID,
                        IN_TIME_WT: val.IN_TIME_WT,
                        OUT_TIME_WT: val.OUT_TIME_WT,
                        PUNCH_TYPE: vm.formData.PUNCH_TYPE,
                        LK_PN_COR_REASON_ID: vm.formData.LK_PN_COR_REASON_ID||'',
                        CORRECTION_REASON_OTHER: vm.formData.CORRECTION_REASON_OTHER ||'',
                        HR_DAY_TYPE_ID: vm.form.HR_DAY_TYPE_ID
                    });
                });                 
     

               if (data.length == 0) {
                   logger.info('Nothing to update');
               } else {
                   return HrService.saveBatchData(data).then(function (res) {
                       config.appToastMsg(res.vMsg);
                       $state.go('HrTaProcessDataCorr.Dtl', $stateParams.dataSource);
                   });
               }

            }
        }


        vm.dtFormat = 'HH:mm';
        vm.dtFormat1 = config.appDateFormat;

        vm.gridOptions = {
            dataSource: $stateParams.data,
            pageable: true,
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
            height: '400px',
            scrollable: true,
            selectable: "cell",
            navigatable: true,
            toolbar: ["save", "cancel"],
            sortable: {
                mode: "single",
                allowUnsort: false
            },

            edit: function (e) {
                var input = e.container.find("input");
                setTimeout(function () {
                    input.select();
                }, 25);

                if (e.model.IS_SELF == 'Y') {
                    e.container.find("input[name='IN_TIME_WT']").each(function () { $(this).attr("disabled", "disabled") });
                    e.container.find("input[name='OUT_TIME_WT']").each(function () { $(this).attr("disabled", "disabled") });
                };
            },

            columns: [

                //{
                //    field: "EMPLOYEE_CODE", title: "", type: "string", width: "60px",
                //    template: function () {
                //        return "{{dataItem.EMPLOYEE_CODE}}<input type='hidden' ng-value='vm.form[dataItem.HR_TA_PROCESS_DATA_ID]=dataItem.HR_TA_PROCESS_DATA_ID' ng-model='vm.form[dataItem.HR_TA_PROCESS_DATA_ID]'>";
                //    }
                //},

                {
                    width: "30px",
                    template: function () {
                        return "<input type='checkbox' checked='checked' ng-init='vm.form[dataItem.HR_TA_PROCESS_DATA_ID].Ckd=true' ng-model='vm.form[dataItem.HR_TA_PROCESS_DATA_ID].Ckd'>";
                    }
                },
                {
                    field: "EMPLOYEE_CODE", title: "Emp. Code", type: "string", width: "70px",
                    template: function () {
                        return "{{dataItem.EMPLOYEE_CODE}}<input type='hidden' ng-value='vm.form[dataItem.HR_TA_PROCESS_DATA_ID].HR_TA_PROCESS_DATA_ID=dataItem.HR_TA_PROCESS_DATA_ID' ng-model='vm.form[dataItem.HR_TA_PROCESS_DATA_ID].HR_TA_PROCESS_DATA_ID'><input type='hidden' ng-value='vm.form[dataItem.HR_TA_PROCESS_DATA_ID].IN_TIME_WT=dataItem.IN_TIME_WT' ng-model='vm.form[dataItem.HR_TA_PROCESS_DATA_ID].IN_TIME_WT'><input type='hidden' ng-value='vm.form[dataItem.HR_TA_PROCESS_DATA_ID].OUT_TIME_WT=dataItem.OUT_TIME_WT' ng-model='vm.form[dataItem.HR_TA_PROCESS_DATA_ID].OUT_TIME_WT'>";
                    }
                },
                { field: "EMP_FULL_NAME_EN", title: "Name", type: "string", width: "150px" },
                //{ field: "DEPARTMENT_NAME_EN", title: "Section", type: "string", width: "110px" },
                //{ field: "DESIGNATION_NAME_EN", title: "Designation", type: "string", width: "100px" },
                { field: "ATTEN_DATE", title: "Date", type: "date", format: "{0:" + vm.dtFormat1 + "}", width: "80px" },
                { field: "IN_TIME_WT", title: "In Time", type: "datetime", format: "{0:" + vm.dtFormat + "}", width: "80px" },
                {
                    field: "OUT_TIME_WT", title: "Out Time", type: "datetime", format: "{0:" + vm.dtFormat + "}", width: "80px",
                    filterable: {
                        cell: {
                            enabled: false
                        }
                    }
                },
                {
                    field: "OT_HR_STRING", title: "OT Hour", type: "string", width: "60px",
                },
                { field: "TA_FLAG", title: "Status", type: "string", width: "50px" },
                { field: "LK_PN_CORR_STATUS", title: "Corr.Type", type: "string", width: "80px" },
                { field: "HR_DAY_TYPE", title: "DayTyp", width: "70px", editor: dayTypDropDownEditor, template: "#=HR_DAY_TYPE.DAY_TYPE_NAME_EN#" },
                //{ command: ["edit"], title: "&nbsp;", width: "80px" }
            ],
            //editable: "inline"
            editable: true
        };

        function dayTypDropDownEditor(container, options) {
            $('<input data-text-field="DAY_TYPE_NAME_EN" data-value-field="HR_DAY_TYPE_ID" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({
                    autoBind: false,
                    optionLabel: "-- N/A --",
                    dataSource: {
                        transport: {
                            read: "/hr/HrYearlyCalander/DayTypeListData"
                        }
                    }
                });
        }

    }

})();