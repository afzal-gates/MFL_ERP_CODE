(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrScheduleAssignController', ['logger', 'config', 'HrService', '$q', '$scope', '$http', '$filter', HrScheduleAssignController]);

    function HrScheduleAssignController(logger, config, HrService, $q, $scope, $http, $filter) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.showSplash = true;
        vm.dtFormat = config.appDateFormat;
        vm.scheduleIsChange = false;
        
        vm.insert = true;        
        

        vm.TranCancel = function () {            
            vm.scheduleIsChange = false;
        };

        vm.reset = function () {
            //$scope.frmCompany.submitted = false;
            
            vm.insert = true;
            vm.form = {IS_ACTIVE:'Y'};                        
        };

        vm.editRecord = function (dataItem) {
            vm.insert = false;

            vm.form = dataItem;
            
        };

        vm.newAssign = function () {
            $http({
                method: 'post',
                url: "/Hr/HrScheduleAssign/ShiftTeamListData",  //+ "&pType=" + showType
                params: { pShiftPlanId: vm.shiftPlanID },
                headers: { "RequestVerificationToken": vm.antiForgeryToken }
            }).
                then(function (res) {

                    obData = [];
                    vm.teamListdata = [];
                    var vEffDate;
                    angular.forEach(res.data, function (val, key) {

                        if (val.EFFECTIVE_FROM) {
                            var dtEndGrace = val.EFFECTIVE_FROM.replace(/\/Date\((-?\d+)\)\//, '$1');
                            dtEndGrace = parseInt(dtEndGrace);
                            vEffDate = new Date(dtEndGrace);

                            val['EFFECTIVE_FROM'] = $scope.formatDate1(vEffDate, vm.dtFormat);
                        }

                        if (val.IS_ROLLING == 'Y') {
                            val['IS_ACTIVE'] = "Y";
                        }


                        obData.push(val);
                    });

                    vm.teamListdata = obData;
                    vm.scheduleIsChange = true;
                });
        };


        var obData = [];

        vm.submit = function (isValid, form, insert) {

            var submitData = angular.copy(form);
            
            obData = [];
            angular.forEach(vm.teamListdata, function (val, key) {

                if (val.IS_ACTIVE == 'Y') {
                    //val['HR_SCHEDULE_H_ID'] = vm.form.HR_SCHEDULE_H_ID;
                    val['HR_OFFICE_ID'] = vm.form.HR_OFFICE_ID;

                    obData.push(val);
                }
            });

            submitData.SCHEDULE_ASSIGN_XML = HrService.xmlStringShort(obData.map(function (ob) {
                ob['EFFECTIVE_FROM'] = $filter('date')(ob['EFFECTIVE_FROM'], vm.dtFormat);

                return ob;
            }));
            
            console.log(submitData);
            //return;

            //submit the data to the server
            //if (insert) {
                
            //    $http({
            //        headers: { "RequestVerificationToken": vm.antiForgeryToken },
            //        url: '/Hr/HrScheduleAssign/BatchSave4ScheduleAssignByTeam',
            //        method: 'post',
            //        data: { obAssignList: obData }
            //    }).success(function (data, status, headers, config1) {
            //        vm.errors = [];
            //        if (data.success === false) {
            //            vm.errors = data.errors;
            //        }
            //        else {
                        
            //            config.appToastMsg(data.vMsg);
            //            //console.log(vm.clockData);
            //        }
            //    }).
            //error(function (data, status, headers, config) {
            //    alert('something went wrong')
            //    console.log(status);
            //});
            
            //}
            

            return $http({
                headers: { "RequestVerificationToken": vm.antiForgeryToken },
                url: '/Hr/HrScheduleAssign/BatchSave4ScheduleAssignByTeam',
                method: 'post',
                data: submitData
            })
                .then(function (res) {
                    vm.errors = undefined;
                    if (res.data.success === false) {
                        vm.errors = [];
                        vm.errors = res.data.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.data.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {                            
                            vm.gridDataSource.read();
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

        };

        ////////////////////////////// Start team load ////////////////////////////////

        

        ////////////////////////////// End team load ///////////////////////////////

        vm.ShiftPlanListData = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Hr/HrShiftPlan/SelectAll",  //+ "&pType=" + showType
                            headers: { "RequestVerificationToken": vm.antiForgeryToken }
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
            dataTextField: "SHIFT_PLAN_NAME_EN",
            dataValueField: "HR_SHIFT_PLAN_ID",
            select: function onSelect(e) {
                var dataItem = this.dataItem(e.item);
                vm.shiftPlanID = dataItem.HR_SHIFT_PLAN_ID;

                //console.log(dataItem);
                //vm.myForm.HR_SCHEDULE_H_ID = dataItem.HR_SCHEDULE_H_ID;

                $("#HR_SHIFT_TEAM_ID_BY_PERSON").kendoDropDownList({
                    optionLabel: "Select",
                    dataTextField: "SHIFT_TEAM_NAME_EN",
                    dataValueField: "HR_SHIFT_TEAM_ID",
                    dataSource: {
                        transport: {
                            read: function (e) {
                                $http({
                                    method: 'post',
                                    url: "/Hr/HrEmployee/ShiftTeamListData",  //+ "&pType=" + showType
                                    headers: { "RequestVerificationToken": vm.antiForgeryToken },
                                    params: { pHR_SHIFT_PLAN_ID: dataItem.HR_SHIFT_PLAN_ID }
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
                    filter: "startswith"                    
                });


                $http({
                    method: 'post',
                    url: "/Hr/HrScheduleAssign/ScheduleListData",  //+ "&pType=" + showType
                    params: { schedulePlanId: dataItem.HR_SHIFT_PLAN_ID },
                    headers: { "RequestVerificationToken": vm.antiForgeryToken }
                }).
                then(function (res) {

                    $("#HR_SCHEDULE_H_ID_BY_PERSON").kendoDropDownList({
                        optionLabel: "Select",
                        dataTextField: "SCHEDULE_NAME_EN",
                        dataValueField: "HR_SCHEDULE_H_ID",
                        dataSource: res.data,
                        filter: "startswith"
                    });

                    vm.ScheduleListData = {
                        optionLabel: "Select",
                        filter: "startswith",
                        autoBind: true,
                        dataSource: res.data,
                        dataTextField: "SCHEDULE_NAME_EN",
                        dataValueField: "HR_SCHEDULE_H_ID",
                        //select: function onSelect(e) {
                        //    var dataItem1 = this.dataItem(e.item.index());

                        //    obData = [];
                        //    obData = vm.teamListdata;
                            
                        //    var i = 0;
                        //    angular.forEach(vm.teamListdata, function (val, key) {

                        //        if (val.IS_ROLLING == 'Y') {
                        //            vm.teamListdata[i].HR_SCHEDULE_H_ID = dataItem1.HR_SCHEDULE_H_ID;                                    
                        //        }
                        //        i = i + 1;
                        //    });
                                                        
                        //}
                    };                    

                });


                $http({
                    method: 'post',
                    url: "/Hr/HrScheduleAssign/ShiftTeamListData",  //+ "&pType=" + showType
                    params: { pShiftPlanId: dataItem.HR_SHIFT_PLAN_ID },
                    headers: { "RequestVerificationToken": vm.antiForgeryToken }
                }).
                then(function (res) {

                    obData = [];
                    vm.teamListdata = [];
                    var vEffDate;
                    angular.forEach(res.data, function (val, key) {
                        
                        if (val.EFFECTIVE_FROM) {
                            var dtEndGrace = val.EFFECTIVE_FROM.replace(/\/Date\((-?\d+)\)\//, '$1');
                            dtEndGrace = parseInt(dtEndGrace);
                            vEffDate = new Date(dtEndGrace);

                            val['EFFECTIVE_FROM'] = $scope.formatDate1(vEffDate, vm.dtFormat);
                        }

                        if (val.IS_ROLLING == 'Y') {
                            val['IS_ACTIVE'] = "Y";
                        }


                        obData.push(val);
                    });
                    
                    vm.teamListdata = obData;
                    vm.scheduleIsChange = false;
                });

            }
        };
        

        vm.effectiveDateChange = function (first) {
            if (first) {
                
                for (var i = 1; i < vm.teamListdata.length; i++) {
                    if (vm.teamListdata[i].IS_ACTIVE == 'Y' && vm.teamListdata[i].IS_ROLLING == 'Y') {
                        vm.teamListdata[i].EFFECTIVE_FROM = $filter('date')(vm.teamListdata[0].EFFECTIVE_FROM, vm.dtFormat);
                    }
                }

            }
        };
        vm.expireDateChange = function (first) {
            if (first) {
                
                for (var i = 1; i < vm.teamListdata.length; i++) {
                    if (vm.teamListdata[i].IS_ACTIVE == 'Y' && vm.teamListdata[i].IS_ROLLING == 'Y') {
                        vm.teamListdata[i].EXPIRED_ON = $filter('date')(vm.teamListdata[0].EXPIRED_ON, vm.dtFormat);
                    }
                }

            }
        };


        vm.ScheduleWithPlanListData = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({                            
                            method: 'post',
                            url: "/Hr/HrScheduleAssign/ScheduleWithPlanListData",  //+ "&pType=" + showType
                            headers: { "RequestVerificationToken": vm.antiForgeryToken }
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
            dataTextField: "SHIFT_PLAN_NAME_EN",
            dataValueField: "HR_SCHEDULE_H_ID",
            select: function onSelect(e) {
                var dataItem = this.dataItem(e.item);
                
                $http({
                    method: 'post',
                    url: "/Hr/HrScheduleAssign/ShiftTeamListData",  //+ "&pType=" + showType
                    params: { pShiftPlanId: dataItem.HR_SHIFT_PLAN_ID },
                    headers: { "RequestVerificationToken": vm.antiForgeryToken }
                }).
                then(function (res) {

                    obData = [];
                    vm.teamListdata = [];
                    var vEffDate;
                    angular.forEach(res.data, function (val, key) {

                        //val['HR_SCHEDULE_H_ID'] = vm.form.HR_SCHEDULE_H_ID;
                        //val['HR_OFFICE_ID'] = vm.form.HR_OFFICE_ID;
                        if (val.EFFECTIVE_FROM) {
                            var dtEndGrace = val.EFFECTIVE_FROM.replace(/\/Date\((-?\d+)\)\//, '$1');
                            dtEndGrace = parseInt(dtEndGrace);
                            vEffDate = new Date(dtEndGrace);
                            
                            val['EFFECTIVE_FROM'] = $scope.formatDate1(vEffDate, vm.dtFormat);

                            //console.log($scope.formatDate1(vEffDate, vm.dtFormat));

                        }


                        obData.push(val);
                    });
                    //console.log(res.data);
                    vm.teamListdata = obData;
                    vm.scheduleIsChange = false;
                });
                
            }
        };

        $scope.formatDate1 = function (date, format) {
            return $filter("date")(date, format);
        };


        vm.show = function () {
            if (vm.form.HR_OFFICE_ID == null || vm.form.HR_OFFICE_ID == "") {
                alert('Please select office');
                return;
            }
            if (vm.form.HR_SHIFT_PLAN_ID == null || vm.form.HR_SHIFT_PLAN_ID == "") {
                alert('Please select shift plan');
                return;
            }

            vm.errors = [];
            vm.scheduleIsChange = true;
        };


        vm.CompanyListData = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Hr/HrCompany/CompanyListData",  //+ "&pType=" + showType
                            headers: { "RequestVerificationToken": vm.antiForgeryToken }
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
            dataTextField: "COMP_NAME_EN",
            dataValueField: "HR_COMPANY_ID"
        };

        vm.OfficeListData = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Hr/HrOffice/OfficeListData",  //+ "&pType=" + showType
                            headers: { "RequestVerificationToken": vm.antiForgeryToken }
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
            dataTextField: "OFFICE_NAME_EN",
            dataValueField: "HR_OFFICE_ID"
        };



        vm.today = new Date();        

        vm.EFFECTIVE_FROM_OPEN = function ($event, index) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.teamListdata[index].EFFECTIVE_FROM_OPEND = true;
        };

        

        vm.EXPIRED_ON_OPEN = function ($event, index) {           
            $event.preventDefault();
            $event.stopPropagation();

            vm.teamListdata[index].EXPIRED_ON_OPEND = true;
        };

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };
       
        
        vm.gridOptions = {
            autoBind: true,
            //dataSource: {
            //    transport: {
            //        read: function (e) {
            //            $http({                            
            //                method: 'post',
            //                url: "/Hr/HrScheduleAssign/ScheduleWiseAssign",  //+ "&pType=" + showType
            //                data: { shiftPlaneId: vm.form.HR_SHIFT_PLAN_ID },
            //                headers: { "RequestVerificationToken": vm.antiForgeryToken }
            //            }).
            //            success(function (data, status, headers, config) {
            //                e.success(data)
            //            }).
            //            error(function (data, status, headers, config) {
            //                alert('something went wrong')
            //                console.log(status);
            //            });
            //        }                    
            //    },
            //    pageSize: 10
            //},
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
                { field: "SCHEDULE_NAME_EN", title: "Work Schedule", type: "string", width: "200px" },
                { field: "SHIFT_PLAN_NAME_EN", title: "Shift Plan", type: "string", width: "200px" },
                { field: "SHIFT_TEAM_NAME_EN", title: "Team", type: "string", width: "200px" },
                { field: "EFFECTIVE_FROM", title: "Effective", type: "date", format: "{0:dd-MMM-yyyy}", width: "100px" },
                { field: "EXPIRED_ON", title: "Expire", type: "date", format: "{0:dd-MMM-yyyy}", width: "100px" }//,
                //{
                //   title: "Action",
                //   template: function () {
                //       return "<a ng-click='vm.editRecord(dataItem)' title='Edit'><i class='fa fa-edit'></i></a>";
                //   },
                //   width: "50px"
                //}
            ]
        };

        vm.gridDataSource = new kendo.data.DataSource({
            //serverPaging: true,
            //serverFiltering: true,
            pageSize: 10,
            transport: {
                read: function (e) {
                    //var webapi = new kendo.data.transports.webapi({});
                    //var params = webapi.parameterMap(e.data);
                    //var url = '/Hr/HrScheduleAssign/ScheduleWiseAssign?shiftPlaneId=' + ;
                    //url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize + '&pKNT_SC_GFAB_DLV_H_ID=' + $stateParams.pKNT_SC_GFAB_DLV_H_ID || 0;

                    //console.log(url);

                    //url += config.kFilterStr2QueryParam(params.filter);
                    //if (params.filter.length == 0) {
                    //    remQueryParam();
                    //}
                    //KnittingDataService.getDataByFullUrl(url).then(function (res) {
                    //    e.success(res);
                    //});
                    return $http({
                        method: 'post',
                        url: "/Hr/HrScheduleAssign/ScheduleWiseAssign",  //+ "&pType=" + showType
                        data: { shiftPlaneId: vm.form.HR_SHIFT_PLAN_ID },
                        headers: { "RequestVerificationToken": vm.antiForgeryToken }
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
            schema: {
                //data: "data",
                //total: "total",
                model: {                    
                    fields: {
                        EFFECTIVE_FROM: { type: "date", editable: false },
                        EXPIRED_ON: { type: "date", editable: false }
                    }
                }
            }
        });


        ////////////////////////////// Start For Assign Schedule By person ///////////////////
        vm.insertByPerson = true;
        vm.myForm = {IS_ACTIVE:'Y'};

        vm.effectPersonDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.effectPersonDateOpened = true;
        };

        vm.expirePersonDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.expirePersonDateOpened = true;
        };

        vm.ScheduleListDataByPerson = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Hr/HrScheduleAssign/ScheduleWithPlanListData",  //+ "&pType=" + showType
                            headers: { "RequestVerificationToken": vm.antiForgeryToken },
                            params: { schedulePlanId: null }
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
            dataTextField: "SCHEDULE_NAME_EN",
            dataValueField: "HR_SCHEDULE_H_ID"            
        };

        vm.shiftTeamListData = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Hr/HrEmployee/ShiftTeamListData",  //+ "&pType=" + showType
                            headers: { "RequestVerificationToken": vm.antiForgeryToken },
                            params: { pHR_SHIFT_PLAN_ID: null }
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
            dataTextField: "SHIFT_TEAM_NAME_EN",
            dataValueField: "HR_SHIFT_TEAM_ID"
        };

        
        $scope.emoloyeeAuto = function (val) {
            //alert('a');
            return $http.get('/Hr/HrEmployee/EmployeeAutoData', {
                params: {
                    pEMPLOYEE_CODE: val
                }
            }).then(function (response) {
                return response.data;
            });
        };

        $scope.onSelectItem = function (item) {
            //console.log(item);            
                        
            vm.myForm = item;
            //vm.myForm.HR_OFFICE_ID = item.HR_OFFICE_ID;
            vm.myForm.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN + ', ' + item.DESIGNATION_NAME_EN + ', ' + item.DEPARTMENT_NAME_EN;
            
            $http.get('/Hr/HrScheduleAssign/PersonScheduleAssignListData', {
                params: {
                    pHR_EMPLOYEE_ID: item.HR_EMPLOYEE_ID
                }
            }).then(function (response) {
                if (response.data[0] != undefined) {
                    vm.myForm.HR_SHIFT_PLAN_ID = response.data[0].HR_SHIFT_PLAN_ID;
                    vm.myForm.HR_SHIFT_TEAM_ID = response.data[0].HR_SHIFT_TEAM_ID;
                    vm.myForm.HR_SCHEDULE_ASSIGN_ID = response.data[0].HR_SCHEDULE_ASSIGN_ID;
                    vm.myForm.HR_SCHEDULE_H_ID = response.data[0].HR_SCHEDULE_H_ID;
                    vm.myForm.HR_OFFICE_ID = response.data[0].HR_OFFICE_ID;
                    vm.myForm.IS_ACTIVE = response.data[0].IS_ACTIVE;

                    var dt = moment(response.data[0].EFFECTIVE_FROM)._d;
                    vm.myForm.EFFECTIVE_FROM = $filter('date')(dt, vm.dtFormat);

                    vm.insertByPerson = false;
                }
                else {
                    vm.insertByPerson = true;
                }               
                       
                
                vm.scheduleAssignPersonGrid();
                return response.data;
            });
            
        }

        $scope.$watch('vm.myForm.EMPLOYEE_CODE', function (newVal, oldVal) {

            if (!newVal || newVal == '') {
                //vm.myForm.HR_EMPLOYEE_ID = null;
                //vm.myForm.EMP_FULL_NAME_EN = null;
                //vm.myForm.DEPARTMENT_NAME_EN = null;
                //vm.myForm.DESIGNATION_NAME_EN = null;
                //vm.myForm.COMP_NAME_EN = null;
                //vm.myForm.HR_COMPANY_ID = null;
                //vm.myForm.HR_OFFICE_ID = null;
                
                vm.myForm = {};
            }
        });


        vm.scheduleAssignPersonGrid = function () {

            var dataSource = new kendo.data.DataSource({
                batch: true,
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Hr/HrScheduleAssign/ScheduleWiseAssignByPerson",  //+ "&pType=" + showType
                            data: { pHR_EMPLOYEE_ID: vm.myForm.HR_EMPLOYEE_ID },
                            headers: { "RequestVerificationToken": vm.antiForgeryToken }
                        }).
                        success(function (data, status, headers, config) {
                            e.success(data)
                        }).
                        error(function (data, status, headers, config) {
                            alert('something went wrong')
                            console.log(status);
                        });
                    },

                    destroy: function (e) {
                        //console.log(e.data.models[0]);                        
                        $http({
                            method: 'post',
                            url: "/Hr/HrScheduleAssign/Delete",  //+ "&pType=" + showType
                            data: { ob: e.data.models[0] },
                            headers: { "RequestVerificationToken": vm.antiForgeryToken }
                        }).
                        success(function (data, status, headers, config) {
                            e.success(data)
                        }).
                        error(function (data, status, headers, config) {
                            alert('something went wrong')
                            console.log(status);
                        });
                        
                    },
                    parameterMap: function (options, operation) {
                        if (operation !== "read" && options.models) {
                            return { models: kendo.stringify(options.models) };
                        }
                    }
                },
                pageSize: 10,
                schema: {
                    model: {
                        id: "HR_SCHEDULE_ASSIGN_ID",
                        fields: {
                            SCHEDULE_NAME_EN: { type: "string", editable: false },
                            SHIFT_PLAN_NAME_EN: { type: "string", editable: false },
                            SHIFT_TEAM_NAME_EN: { type: "string", editable: false },
                            EFFECTIVE_FROM: { type: "date", editable: false },
                            EXPIRED_ON: { type: "date", editable: false }
                        }
                    }
                }
            });



            $("#scheduleAssignHistByPerson").kendoGrid({
                dataSource: dataSource,
                navigatable: true,
                pageable: {
                    refresh: true,
                    pageSizes: true
                },
                //height: 550,
                //toolbar: ["save", "cancel"],
                columns: [
                    { field: "SCHEDULE_NAME_EN", title: "Work Schedule", type: "string", width: "250px" },
                    { field: "SHIFT_PLAN_NAME_EN", title: "Shift Plan", type: "string", width: "200px" },
                    { field: "SHIFT_TEAM_NAME_EN", title: "Team", type: "string", width: "100px" },
                    { field: "EFFECTIVE_FROM", title: "Effective", type: "date", format: "{0:dd-MMM-yyyy}", width: "100px" },
                    { field: "EXPIRED_ON", title: "Expire", type: "date", format: "{0:dd-MMM-yyyy}", width: "100px" },
                    { command: [{ name: "destroy", text: "Remove" }], width: "100px" }
                ],
                editable: {
                    confirmation: false, //"Do you want to remove this record?",
                    mode: "inline"
                }
            });

        };


        vm.submitByPerson = function (isValid, myForm, insertByPerson) {
                        
            //console.log(vm.teamListdata);
            //return;
            
            if (insertByPerson) {
                myForm.IS_ACTIVE='Y';

                $http({
                    headers: { "RequestVerificationToken": vm.antiForgeryToken },
                    url: '/Hr/HrScheduleAssign/Save',
                    method: 'post',
                    data: myForm
                }).success(function (data, status, headers, config1) {
                    vm.errors = [];
                    if (data.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        config.appToastMsg(data.vMsg);                        
                    }
                }).
                error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });

            }
            else {

                $http({
                    headers: { "RequestVerificationToken": vm.antiForgeryToken },
                    url: '/Hr/HrScheduleAssign/Update',
                    method: 'post',
                    data: { ob: myForm }
                }).success(function (data, status, headers, config1) {
                    vm.errors = [];
                    if (data.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        config.appToastMsg(data.vMsg);                        
                    }
                }).
                error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });

            }

            return;
        };

        vm.newAssignByPerson = function () {
            vm.myFormCopy = vm.myForm;
            vm.myForm = { IS_ACTIVE: 'Y' };
            //vm.myForm.EFFECTIVE_FROM = null;
            //vm.myForm.EXPIRED_ON = null;

            vm.insertByPerson = true;
        };

        
        
        ////////////////////////////// End For Assign Schedule By person ///////////////////




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



//================== Schedule Assign By Random Person =========================
(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrAssignByRandomPersonController', ['logger', 'config', 'HrService', '$q', '$scope', '$window', '$http', '$filter', '$state', HrAssignByRandomPersonController]);
    function HrAssignByRandomPersonController(logger, config, HrService, $q, $scope, $window, $http, $filter, $state) {

        var vm = this;
        
        vm.Title = $state.current.Title;
        vm.showSplash = true;
        vm.dtFormat = config.appDateFormat;
        vm.scheduleIsChange = false;
        vm.today = new Date();

        $("#EMPLOYEE_CODE").focus();

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });
        

        vm.form = { EFFECTIVE_FROM: moment(vm.today).format("DD-MMM-YYYY") };

        activate();
        function activate() {
            var promise = [getShiftTeamList(), getScheduleList(), getRestoreBatchList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.EFFECTIVE_FROM_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.EFFECTIVE_FROM_LNopened = true;
        };

        function getShiftTeamList() {
            vm.shiftTeamOptions = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SHIFT_TEAM_NAME_EN",
                dataValueField: "HR_SHIFT_TEAM_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);                    
                }
            };

            return vm.shiftTeamDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.getDataByFullUrl('/api/hr/Attendance/GetShiftTeamList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getScheduleList() {
            vm.scheduleOptions = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SCHEDULE_NAME_EN",
                dataValueField: "HR_SCHEDULE_H_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.scheduleDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.getDataByFullUrl('/api/hr/Attendance/GetScheduleList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        $scope.emoloyeeAuto = function (val) {           
            return $http.get('/Hr/HrEmployee/EmployeeAutoData', {
                params: {
                    pEMPLOYEE_CODE: val
                }
            }).then(function (response) {
                return response.data;
            });
        };

        $scope.onSelectItem = function (item) {

            console.log(item);            
            vm.myForm.HR_EMPLOYEE_ID = item.HR_EMPLOYEE_ID;
            vm.myForm.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN;
            vm.myForm.DEPARTMENT_NAME_EN = item.DEPARTMENT_NAME_EN;
            vm.myForm.DESIGNATION_NAME_EN = item.DESIGNATION_NAME_EN;
            vm.myForm.COMP_NAME_EN = item.COMP_NAME_EN;
            vm.myForm.HR_COMPANY_ID = item.HR_COMPANY_ID;
            vm.myForm.HR_OFFICE_ID = item.HR_OFFICE_ID;
            
            vm.myForm.EMP_PROFILE = item.EMP_FULL_NAME_EN + ', ' + item.DESIGNATION_NAME_EN + ', ' + item.DEPARTMENT_NAME_EN;                        
        }

        $scope.$watch('vm.myForm.EMPLOYEE_CODE', function (newVal, oldVal) {

            if (!newVal || newVal == '') {                
                vm.myForm = {};
            }
        });

        vm.addNewEmp = function () {
            //vm.assignEmpListDataSource.insert(0, vm.form);
            //vm.form = {};
            if (vm.myForm.HR_EMPLOYEE_ID != null) {
                vm.assignEmpListDataSource.insert(0, vm.myForm);
                vm.myForm = {};
            }
        }

        //$("#EMPLOYEE_CODE").keypress(function (e) {
        //    alert(e.which);
        //    if (e.which == 13) {
        //        alert('x');
        //        vm.assignEmpListDataSource.insert(0, vm.myForm);
        //        vm.form = {};
        //    };
        //});

        vm.removeRow = function (dataItem) {
            vm.assignEmpListDataSource.remove(dataItem);
        }

        vm.assignEmpListOptions = {
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
            //dataBound: function (e) {
            //    collapseAllGroups(this);
            //},
            columns: [
                { field: "EMPLOYEE_CODE", title: "Code", width: "15%" },
                { field: "EMP_FULL_NAME_EN", title: "Name", width: "25%" },
                { field: "DEPARTMENT_NAME_EN", title: "Department", width: "20%" },
                { field: "DESIGNATION_NAME_EN", title: "Designation", width: "20%" },
                { field: "COMP_NAME_EN", title: "Company", width: "15%" },
                {
                    title: "",
                    template: function () {
                        return "<button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ><i class='fa fa-remove'></i></button>";
                    },
                    width: "5%"
                }
            ]
        };

        vm.assignEmpListDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    e.success([]);                   
                }
            }
        });


        vm.errors = undefined;
        vm.assignSchedule = function () {
            var submitData = angular.copy(vm.form);
            submitData.EFFECTIVE_FROM = $filter('date')(submitData.EFFECTIVE_FROM, vm.dtFormat);

            var empListGridData = vm.assignEmpListDataSource.data();
           
            console.log(empListGridData);            
           
            //$scope.$parent.errors = [];
            //angular.forEach(clcfRateGridData, function (val, key) {
            //    if (val['PROD_RATE'] == null || val['PROD_RATE'] == '') {
            //        $scope.$parent.errors.push({ key: "", error: 'Assign rate should not empty' });
            //    }
            //});

            //if ($scope.$parent.errors != undefined && $scope.$parent.errors.length > 0) {
            //    return;
            //}
            //else {
            //    $scope.$parent.errors = undefined;
            //}

            submitData.EMP_XML = HrService.xmlStringShort(empListGridData.map(function (ob) {
                return { HR_EMPLOYEE_ID: ob.HR_EMPLOYEE_ID, EMPLOYEE_CODE: ob.EMPLOYEE_CODE, HR_COMPANY_ID: ob.HR_COMPANY_ID, HR_OFFICE_ID: ob.HR_OFFICE_ID };
            }));

            console.log(submitData);
            //return;

            return HrService.saveDataByUrl(submitData, '/api/hr/Attendance/ScheduleByRandomPersonBatchSave').then(function (res) {
                console.log(res);

                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {                        
                        vm.assignEmpListDataSource.read();
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });


        };


        function ScheduleDropDownEditor(container, options) {

            $('<input data-text-field="SCHEDULE_NAME_EN" data-value-field="HR_SCHEDULE_H_ID" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({
                    filter: "contains",
                    optionLabel: "--N/A--",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: function (e) {
                                return HrService.getDataByFullUrl('/api/hr/Attendance/GetScheduleList').then(function (res) {
                                    e.success(res);
                                });
                            }
                        }
                    },
                    change: function (e) {
                        var item = this.dataItem(e.item);
                        console.log(item);
                        options.model.HR_SCHEDULE_H_ID = item.HR_SCHEDULE_H_ID;
                        options.model.SCHEDULE_NAME_EN = item.SCHEDULE_NAME_EN;
                        
                        //var grid = $("#assignRateGrid").data("kendoGrid");
                        //grid.refresh();
                    }
                });
        }

        vm.restoreBatchListOptions = {
            editable: true,
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
            //dataBound: function (e) {
            //    collapseAllGroups(this);
            //},
            columns: [
                { field: "BATCH_NO", title: "Batch#", width: "10%" },
                { field: "SHIFT_TEAM_NAME_EN", title: "Team", width: "20%" },
                { field: "NO_OF_EMPLOYEE", title: "Tot.Emp", width: "10%" },                
                { field: "HR_SCHEDULE", title: "Schedule", width: "40%", filterable: false, editor: ScheduleDropDownEditor, template: "#=HR_SCHEDULE.SCHEDULE_NAME_EN#" },
                { field: "EFFECTIVE_FROM", title: "Eff.Date", width: "10%", format: "{0:dd-MMM-yyyy}" },
                {
                    title: "",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.restoreTeam(dataItem)' >Restore</button>";
                    },
                    width: "10%"
                }
            ]
        };

        function getRestoreBatchList() {
            vm.restoreBatchListDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        return HrService.getDataByFullUrl('/api/hr/Attendance/GetShiftTeamRestoreBatchList').then(function (res) {
                            console.log(res);

                            e.success(res);
                        });
                    }
                },
                schema: {
                    model: {
                        //id: "HR_YR_INCR_D_ID",
                        fields: {
                            BATCH_NO: { type: "number", editable: false },
                            SHIFT_TEAM_NAME_EN: { type: "string", editable: false },
                            NO_OF_EMPLOYEE: { type: "number", editable: false },
                            EFFECTIVE_FROM: { type: "date", editable: true },
                            HR_SCHEDULE: { defaultValue: { HR_SCHEDULE_H_ID: "", SCHEDULE_NAME_EN: "--N/A--" }, editable: true }
                        }
                    }
                }
            });
        }

        vm.restoreTeam = function (dataItem) {
            var submitData = angular.copy(dataItem);
            submitData.EFFECTIVE_FROM = $filter('date')(submitData.EFFECTIVE_FROM, vm.dtFormat);
            
            console.log(submitData);
            //return;

            return HrService.saveDataByUrl(submitData, '/api/hr/Attendance/SchdulRestoreByRandomBatchSave').then(function (res) {
                console.log(res);

                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        vm.restoreBatchListDataSource.read();
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });


        };


    }



})();