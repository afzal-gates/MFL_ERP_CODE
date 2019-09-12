(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrScheduleController', ['$state', '$stateParams', 'logger', 'config', '$q', '$rootScope', '$scope', '$http', HrScheduleController]);

    function HrScheduleController($state, $stateParams, logger, config, $q, $rootScope, $scope, $http) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.showSplash = true;
        
        vm.insert = true;
        vm.IsWeekGrpList = false;
        vm.scheduleId;

        
        //vm.TranCancel = function () {            
        //    vm.insert = true;
        //    vm.reset();
        //};

        //vm.reset = function () {
        //    //$scope.frmCompany.submitted = false;
            
        //    vm.insert = true;
        //    vm.IsWeekGrpList = false;
        //    vm.form = {IS_ACTIVE:'Y'};                        
        //};

        vm.editRecord = function (dataItem) {
            vm.insert = false;
            vm.IsWeekGrpList = true;
            vm.form = dataItem;
            vm.errors = {};

            return $state.go('ScheduleEntry.ScheduleClock', { HrScheduleId: dataItem.HR_SCHEDULE_H_ID, HrScheduleD_Id: dataItem.HR_SCHEDULE_D01_ID });

        };

        
        $rootScope.$on('$stateChangeStart',
        function (event, fromState) {
            
            if (fromState.name != 'ScheduleEntry.ScheduleClock') {
                vm.insert = true;
                vm.form = { IS_ACTIVE: 'Y', HR_SHIFT_PLAN_ID: "", HR_SHIFT_TYPE_ID: "", IS_MULTI_GRP:"N" };
                vm.errors = {};
            }
            // Access to all the view config properties.
            // and one special property 'targetView'
            // viewConfig.targetView 
        });

        vm.newSchedule = function () {
            vm.insert = true;
            vm.form = {IS_ACTIVE:'Y'};

            vm.errors = {};
            return $state.go('ScheduleEntry');
        };

        vm.submit = function (isValid, form, insert) {

            //console.log(form);
            //alert('not ok');

            if (!isValid) return;

            //alert('ok');            

            //submit the data to the server
            if (insert) {
                //$.ajax
               
                $http({
                    method: 'POST',
                    url: '/Hr/HrSchedule/DataSave',
                    data: vm.form,
                    headers: { "RequestVerificationToken": vm.antiForgeryToken }
                }).success(function (data, status, headers, config1) {
                    
                    vm.errors = [];
                    if (data.success === false) {                        
                        vm.errors = data.errors;
                    }
                    else {
                        //console.log(data);
                        //config.appToastMsg(data.objReturn.msg);
                        vm.insert = false;
                        return $state.go('ScheduleEntry.ScheduleClock', { HrScheduleId: data.objReturn.HrScheduleId, HrScheduleD_Id: data.objReturn.HrScheduleD_Id });
                    }                   
                }).
                error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });
            
            }
            else {
                $http({
                    method: 'POST',
                    url: '/Hr/HrSchedule/Update',
                    data: vm.form,
                    headers: { "RequestVerificationToken": vm.antiForgeryToken }
                }).success(function (data, status, headers, config1) {

                    vm.errors = [];
                    if (data.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        //console.log(data);
                        config.appToastMsg(data.vMsg);
                        vm.insert = false;
                        //return $state.go('ScheduleEntry.ScheduleClock', { HrScheduleId: data.objReturn.HrScheduleId, HrScheduleD_Id: data.objReturn.HrScheduleD_Id });
                    }
                }).
                error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });

            }
            
            return;

        };


        
        vm.shiftPlanList = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({                            
                            method: 'post',
                            url: "/Hr/HrShiftPlan/SelectAll"  //+ "&pType=" + showType
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
            dataValueField: "HR_SHIFT_PLAN_ID"
        };


        vm.shiftTypeList = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({                            
                            method: 'post',
                            url: "/Hr/HrShiftPlan/ShiftTypeListData"  //+ "&pType=" + showType
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
            dataTextField: "SHIFT_TYPE_NAME_EN",
            dataValueField: "HR_SHIFT_TYPE_ID"
        };
                            
       

        vm.tabs = [
        { title: 'Entry', content: 'Dynamic content 1',url:'ScheduleEntry' },
        { title: 'List', content: 'Dynamic content 2', url: 'ScheduleList' },
        { title: 'Schedule Clock', content: 'Dynamic content 3', url: 'ScheduleClock' }
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
                            url: "/Hr/HrSchedule/ScheduleListData",  //+ "&pType=" + showType
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
                { field: "SCHEDULE_CODE", title: "Code", type: "string", width: "100px" },
                { field: "SCHEDULE_NAME_EN", title: "Name [E]", type: "string", width: "200px" },
                { field: "WK_GRP_NAME_EN", title: "Week Group", type: "string", width: "100px" },
                {
                    title: "",
                    template: function () {
                        return "<a ng-click='vm.tabs[0].active = true;vm.editRecord(dataItem)' title='Edit'><i class='fa fa-edit'></i></a>";
                    },
                    width: "50px"
                }
                //{ field: "COMP_NAME_BN", title: "Name [B]", type: "string", width: "200px" },
                //{ field: "COMP_SNAME", title: "Short Name", type: "string", width: "150px" },
                //{ field: "HR_COMPANY_GRP_ID", title: "Group Name", type: "string", width: "200px" },
                //{ field: "COMP_TYPE_NAME", title: "Company Type", type: "string", width: "150px" },
                //{ field: "COMP_BUS_TYPE_NAME", title: "Business Type", type: "string", width: "150px" },
                //{ field: "VAT_REG_NO", title: "Vat Registation#", type: "string", width: "150px" },
                //{ field: "YEAR_ESTD", title: "Establish Year", type: "string", width: "100px" },
                //{ field: "COMP_WEB", title: "Web Site", type: "string", width: "200px" },
                //{
                //    template: function () {
                //        return '<div class="btn-group pull-right">'+
                //        '<button class="btn btn-default btn-sm dropdown-toggle" type="button" data-toggle="dropdown" aria-expanded="false">'+
                //            'Small button <i class="fa fa-angle-down"></i>'+
                //        '</button>'+
                //        '<ul class="dropdown-menu" role="menu">'+
                //            '<li>'+
                //                '<a href="javascript:;">'+
                //                    'Action'+
                //                '</a>'+
                //            '</li>'+
                //            '<li>'+
                //                '<a href="javascript:;">'+
                //                'Another action'+
                //                '</a>'+
                //            '</li>'+
                //            '<li>'+
                //                '<a href="javascript:;">'+
                //                'Something else here'+
                //                '</a>'+
                //            '</li>'+
                //        '</ul>'+
                //    '</div>'
                //    }
                //}
                //{ field: "COMP_DESC", title: "Description", type: "string", width: "300px" },

                //{ field: "IS_ACTIVE", title: "Active?", type: "string", width: "100px" }
            ]
        };




        /////////////// Start Week Group ////////////
        var dataSource = new kendo.data.DataSource({            
            pageSize: 20,            
            transport: {
                read: function (e) {
                    $http({
                        method: 'post',
                        url: "/Hr/HrScheduleD01/ScheduleWiseWeekGrpListData",  //+ "&pType=" + showType
                        params: { ScheduleId: 30 },
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
                update: function (e) {
                    //console.log(e);
                    $http({
                        method: 'post',
                        url: "/Hr/HrScheduleD01/Update",  //+ "&pType=" + showType
                        //params: { ScheduleId: 30 },
                        data: e.data,
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
                create: function (e) {
                   
                    $http({
                        method: 'post',
                        url: "/Hr/HrScheduleD01/Save",  //+ "&pType=" + showType
                        //params: { ScheduleId: 30 },
                        data: e.data,
                        headers: { "RequestVerificationToken": vm.antiForgeryToken }
                    }).
                    success(function (data, status, headers, config) {
                        e.success(data);
                        logger.success(data.vMsg);
                    }).
                    error(function (data, status, headers, config) {
                        alert('something went wrong')
                        console.log(status);
                    });
                    
                }
                
            },
            //autoSync: true,
            schema: {
                model: {
                    id: "HR_SCHEDULE_D01_ID",
                    fields: {
                        HR_SCHEDULE_D01_ID: { type: "number", editable: true, nullable: true },
                        HR_SCHEDULE_H_ID: { type: "number" },
                        WK_GRP_NAME_EN: { validation: { required: true} },
                        IS_ACTIVE: { defaultValue: "Y" }
                        //CategoryID: { field: "CategoryID", type: "number", defaultValue: 1 },
                        //UnitPrice: { type: "number", validation: { required: true, min: 1 } }
                    }
                }
            }
        });
        
        var isActive = [{
            "value": "Y",
            "text": "Yes"
        },{
            "value": "N",
            "text": "No"
        }]

        vm.gridWeekGrpListOptions={
            dataSource: dataSource,
            filterable: {
                mode: "row"
            },
            //selectable: "row",
            sortable: true,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            //height: 550,
            toolbar: ["create"],
            groupable: false,                       
            columns: [
                { field: "HR_SCHEDULE_D01_ID", title: "Id" },
                { field: "WK_GRP_NAME_EN", title: "Week Group Name", width: "150px" },
                { field: "IS_ACTIVE", title: "Active?", values: isActive, width: "100px" },
                { command: ["edit", "destroy"], title: " ", width: "100px" }],
            editable: "inline"//,
            //edit: onEdit
        };

        function onEdit(editEvent) {
            // Ignore edits of existing rows.
            if (!editEvent.model.isNew()) { return; }
            
            //todo: load default value...
            var defaultValue = 20;
            editEvent.container.find("[name=ProductID]")
                .val(defaultValue)
                .trigger("change");

            editEvent.container;//.trigger("update");
        };

        vm.test=function() {
            alert('i am test');

            var WeekGrpList = $("#WeekGrpList").data("kendoGrid");
            console.log(WeekGrpList);
            //console.log(vm.gridWeekGrpListOptions.dataSource.data);
        };

        ////////////// End Week Group //////////////








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