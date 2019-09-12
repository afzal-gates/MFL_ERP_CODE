(function () {
    'use strict';

    var items;
    var modalInstance;
    
    angular.module('multitex.hr').controller('HrShiftPlanController', ['logger', 'config', '$q', '$scope', '$http', '$modal', HrShiftPlanController]);

    function HrShiftPlanController(logger, config, $q, $scope, $http, $modal) {

        var vm = this;
        //console.log(vm);
        activate();
        vm.title = config.appTitle;
        vm.showSplash = true;

        vm.insert = true;
        

        vm.TranCancel = function () {
            vm.reset();
        };

        vm.reset = function () {
            vm.insert = true;
            vm.formCopy = vm.form;
            vm.form = { IS_ACTIVE: 'Y', IS_ROLLING:'N', IS_MULTI_SHIFT:'N', TEAMS_REQ:1 };

            vm.form.ROLLING_CYCLE_ID = vm.formCopy.ROLLING_CYCLE_ID;
            //vm.form.LK_OFF_TYPE_ID = vm.formCopy.LK_OFF_TYPE_ID;
            //vm.form.HR_TIMEZONE_ID = vm.formCopy.HR_TIMEZONE_ID;
            //vm.form.HR_GEO_DISTRICT_ID = vm.formCopy.HR_GEO_DISTRICT_ID;
            //vm.form.HR_COUNTRY_ID = vm.formCopy.HR_COUNTRY_ID;
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

                $http({
                    headers: { "RequestVerificationToken": vm.antiForgeryToken },
                    url: '/Hr/HrShiftPlan/Save',
                    method: 'post',
                    data: { ob: form }
                }).success(function (data, status, headers, config1) {
                    vm.errors = [];
                    if (data.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        if (data.vMsg.substr(0, 9) == 'MULTI-001') {
                            vm.reset();
                        };

                        config.appToastMsg(data.vMsg);
                        //console.log(vm.clockData);
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
                    url: '/Hr/HrShiftPlan/Update',
                    method: 'post',
                    data: { ob: form }
                }).success(function (data, status, headers, config1) {
                    vm.errors = [];
                    if (data.success === false) {
                        vm.errors = data.errors;
                    }
                    else {                        
                        if (data.vMsg.substr(0, 9) == 'MULTI-001') {
                            vm.reset();
                        };
                        config.appToastMsg(data.vMsg);
                        //console.log(vm.clockData);
                    }
                }).
                   error(function (data, status, headers, config) {
                       alert('something went wrong')
                       console.log(status);
                   });

                

            }

            return;

        };



        vm.shiftTypeListData = {
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


        

        $http({
            method: 'post',
            url: "/Hr/HrShiftPlan/PeriodTypeListData",  //+ "&pType=" + showType                
            headers: { "RequestVerificationToken": vm.antiForgeryToken }
        }).then(function (res) {
            vm.periodTypeListData = res.data;
        });


        $http({
            method: 'post',
            url: "/Hr/HrShiftPlan/DayListData",  //+ "&pType=" + showType                
            headers: { "RequestVerificationToken": vm.antiForgeryToken }
        }).then(function (res) {
            vm.dayListData = res.data;
        });

        //vm.periodTypeListData = {
        //    optionLabel: "Select",
        //    filter: "startswith",
        //    autoBind: true,
        //    dataSource: {
        //        transport: {
        //            read: function (e) {
        //                $http({
        //                    method: 'post',
        //                    url: "/Hr/HrShiftPlan/PeriodTypeListData"  //+ "&pType=" + showType
        //                }).
        //                success(function (data, status, headers, config) {
        //                    e.success(data)
        //                }).
        //                error(function (data, status, headers, config) {
        //                    alert('something went wrong')
        //                    console.log(status);
        //                });
        //            }
        //        }
        //    },
        //    dataTextField: "PERIOD_TYPE_NAME_EN",
        //    dataValueField: "HR_PERIOD_TYPE_ID"
        //};

        //vm.gridData = [];

        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Hr/HrShiftPlan/SelectAll"  //+ "&pType=" + showType
                        }).
                        success(function (data, status, headers, config) {
                            //vm.gridData = data;
                            e.success(data);
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
                { field: "SHIFT_PLAN_CODE", title: "Code", type: "string", width: "100px" },
                { field: "SHIFT_PLAN_NAME_EN", title: "Name", type: "string", width: "200px" },
                { field: "DAY_WORK_HOURS", title: "Hours", type: "number", width: "150px" },
                { field: "WORK_DAYS_WK", title: "Days in week", type: "number", width: "150px" },
                { field: "AVG_WK_HOURS", title: "Avg. Hrs. per week", type: "number", width: "150px" },
                { field: "IS_ROLLING", title: "Roll?", type: "string", width: "100px" },
                { field: "ROLLING_DAYS", title: "Roll Days", type: "number", width: "100px" },
                {
                    title: "Action",
                    template: function () {
                        return "<a ng-click='vm.tabs[0].active = true;vm.editRecord(dataItem)' title='Edit'><i class='fa fa-edit'></i></a>&nbsp; <a ng-click='shiftTeamModal(dataItem)' title='Shift Team'><i class='fa fa-external-link-square'></i></a>";
                    },
                    width: "50px"
                }
            ]
        };


        vm.tabs = [
        { title: 'Entry', content: 'Dynamic content 1' },
        { title: 'List', content: 'Dynamic content 2' }
        //{ title: 'Dynamic Title 3', content: 'Dynamic content 3' }
        ];


        //var grid = $("#planGrid").kendoGrid.dataItem;
        //vm.gridTest = function () {
        //    alert('g');
        //    console.log(vm.gridData);
        //}

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

        

        //$scope.open = function (dataItem) {
            
        //    items = dataItem;

        //    var modalInstance = $modal.open({
        //        templateUrl: '/Hr/HrShiftPlan/ShiftTeam', //'myModalContent.html',
        //        controller: 'HrShiftTeamController',
        //        controllerAs: 'vm',
        //        windowClass: 'large-Modal',
        //        //size: size//,
        //        resolve: {
        //            items: function () {                        
        //                return items;
        //            }
        //        }
        //    });
        //};

        $scope.shiftTeamModal = function (dataItem) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ShiftTeamModal.html',
                controller: 'HrShiftTeamController',                
                size: 'md',
                scope: $scope,
                windowClass: 'large-Modal',
                resolve: {
                    shiftPlanData: function () {
                        return dataItem;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                //console.log('received');
                console.log(data);               

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };

               

    };

    
})();





//////////////////////////////    Start HrShiftTeamController     ///////////////////////////////////////////////////////////////////////
(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrShiftTeamController', ['$modalInstance', '$q', '$scope', '$http', '$state', '$filter', 'config', 'HrService', 'shiftPlanData', 'Dialog', HrShiftTeamController]);
    function HrShiftTeamController($modalInstance, $q, $scope, $http, $state, $filter, config, HrService, shiftPlanData, Dialog) {

        
        var vm = this;
        $scope.showSplash = true;

        $scope.HR_SHIFT_TEAM_ID = 0;
        $scope.ROLL_SEQ_NO = 1;
        $scope.IS_ROLLING = 'N';
        $scope.IS_ACTIVE = 'Y';
        if (shiftPlanData.IS_ROLLING == 'Y') {
            $scope.IS_ROLLING = 'Y';
            $scope.ROLL_SEQ_NO = 0;
        }

        console.log(shiftPlanData);

        activate();
        function activate() {
            var promise = [getCompanyList(), getShiftPlanList(), getShiftTeamList()];

            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                $scope.showSplash = false;
            });
        }


       

        function getCompanyList() {
            
            $scope.companyOptions = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return $scope.companyDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/CompanyList';
                        
                        return HrService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };
        
        function getShiftPlanList() {
            $scope.shiftPlanOptions = {
                optionLabel: "-- Select Plan --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SHIFT_PLAN_NAME_EN",
                dataValueField: "HR_SHIFT_PLAN_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                },
                dataBound: function (e) {
                    $scope.HR_SHIFT_PLAN_ID = shiftPlanData.HR_SHIFT_PLAN_ID;
                }
            };

            return $scope.shiftPlanDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        
                        return $http({
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
            });
        };

        function getShiftTeamList() {
            $scope.shiftTeamGridOptions = {
                autoBind: true,                
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
                height: '200px',
                scrollable: true,
                sortable: true,
                columns: [
                    { field: "SHIFT_TEAM_CODE", title: "Code", type: "string", width: "100px" },
                    { field: "SHIFT_TEAM_NAME_EN", title: "Name", type: "string", width: "250px" },
                    { field: "IS_ACTIVE", title: "Active?", type: "string", width: "100px" },
                    {
                        title: "Action",
                        template: function () {
                            return "<a ng-click='editRecord(dataItem)' title='Edit'><i class='fa fa-edit'></i></a>";
                        },
                        width: "50px"
                    }
                ]
            };

            return $scope.shiftTeamGridDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {

                        $http({
                            params: { pHR_SHIFT_PLAN_ID: shiftPlanData.HR_SHIFT_PLAN_ID },
                            method: 'post',
                            url: "/Hr/HrShiftTeam/ShiftTeamListData"  //+ "&pType=" + showType
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

        $scope.editRecord = function (dataItem) {
            $scope.HR_SHIFT_TEAM_ID = dataItem.HR_SHIFT_TEAM_ID;

            $scope.HR_COMPANY_ID = dataItem.HR_COMPANY_ID;
            $scope.HR_SHIFT_PLAN_ID = dataItem.HR_SHIFT_PLAN_ID;
            $scope.SHIFT_TEAM_CODE = dataItem.SHIFT_TEAM_CODE;
            $scope.SHIFT_TEAM_NAME_EN = dataItem.SHIFT_TEAM_NAME_EN;
            $scope.SHIFT_TEAM_NAME_BN = dataItem.SHIFT_TEAM_NAME_BN;

            $scope.ROLL_SEQ_NO = dataItem.ROLL_SEQ_NO;
            $scope.IS_ROLLING = dataItem.IS_ROLLING;
            $scope.IS_ACTIVE = dataItem.IS_ACTIVE;
        }

        $scope.save = function (token, valid) {
            if (!valid) return;
            
            var submitData = {
                HR_SHIFT_TEAM_ID: $scope.HR_SHIFT_TEAM_ID, HR_COMPANY_ID: $scope.HR_COMPANY_ID, HR_SHIFT_PLAN_ID: $scope.HR_SHIFT_PLAN_ID,
                SHIFT_TEAM_CODE: $scope.SHIFT_TEAM_CODE, SHIFT_TEAM_NAME_EN: $scope.SHIFT_TEAM_NAME_EN, SHIFT_TEAM_NAME_BN: $scope.SHIFT_TEAM_NAME_BN,
                ROLL_SEQ_NO: $scope.ROLL_SEQ_NO, IS_ROLLING: $scope.IS_ROLLING, IS_ACTIVE: $scope.IS_ACTIVE
            };

            console.log(submitData);

            Dialog.confirm('Do you want to save?', 'Confirmation', ['Yes', 'No'])
                 .then(function () {


                     // $http.post('/Hr/HrShiftTeam/Save', myForm).success(function (data, status, headers, config) {

                     $http({
                         headers: { 'Authorization': 'Bearer ' + token },
                         url: '/api/hr/ShiftTeam/Save',
                         method: 'post',
                         data: submitData
                     })
                     .then(function (res) {
                         //$scope.$parent.errors = undefined;
                         if (res.data.success === false) {
                             //$scope.$parent.errors = [];
                             //$scope.$parent.errors = res.data.errors;
                         }
                         else {

                             res['data'] = angular.fromJson(res.data.jsonStr);

                             //if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                             //    vm.removeItemTempRow(index, removeFrom, master);
                             //};

                             if (res.data.PHR_SHIFT_TEAM_ID_RTN > 0 && res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 $scope.HR_SHIFT_TEAM_ID = res.data.PHR_SHIFT_TEAM_ID_RTN;
                                 $scope.SHIFT_TEAM_CODE = res.data.PSHIFT_TEAM_CODE_RTN;

                                 $scope.shiftTeamGridDataSource.read();
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });


                 });

            //var obj = {};
            //obj = $scope.asortDataList._data.map(function (ob) {
            //    return {
            //        DETAIL_INDEX: ob.DETAIL_INDEX, MC_SMP_REQ_D_ASORT_ID: ob.MC_SMP_REQ_D_ASORT_ID > 0 ? ob.MC_SMP_REQ_D_ASORT_ID : 0,
            //        MC_SMP_REQ_D_ID: ob.MC_SMP_REQ_D_ID, MC_ORDER_H_ID: ob.MC_ORDER_H_ID, STYLE_NO: ob.STYLE_NO, ASSORT_QTY: ob.RQD_QTY,
            //        QTY_MOU_ID: ob.MC_SMP_REQ_D_ASORT_ID > 0 ? ob.QTY_MOU_ID : $scope.QTY_MOU_ID
            //    };
            //});


        };


        $scope.cancelTran = function () {
            $scope.HR_SHIFT_TEAM_ID = 0;
            
            $scope.SHIFT_TEAM_CODE = null;
            $scope.SHIFT_TEAM_NAME_EN = null;
            $scope.SHIFT_TEAM_NAME_BN = null;

            $scope.ROLL_SEQ_NO = 0;            
            $scope.IS_ACTIVE = 'Y';
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

    }

})();





//=====================================================


    
/////////////////////////////    End HrShiftTeamController    ///////////////////////////////////////////////////////////////////////
