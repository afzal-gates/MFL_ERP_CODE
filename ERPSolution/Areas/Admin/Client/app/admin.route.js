(function () {
    'use strict';

    angular
        .module('multitex.admin')
        .run(appRun);

    /* @ngInject */
    function appRun(routerHelper) {
        
        routerHelper.configureStates(getStates(),'/');

      
    }

    function getStates() {
        return [
            /////////// Start for office
            {
                state: 'LeaveTypeEntry',
                config: {
                    url: '/entry',
                    params: {
                        data: null
                    },
                    templateUrl: '/Hr/Admin/hrleavetype/LeaveTypeEntry',
                    controller: 'LeaveTypeEntryController',
                    controllerAs:'vm',
                    title: '404'
                }
            },

            {
                state: 'LeaveTypeList',
                config: {
                    url: '/list',
                    templateUrl: '/Hr/Admin/hrleavetype/LeaveTypeList',
                    controller: 'LeaveTypeListController',
                    controllerAs: 'vm',
                    title: '404'
                }
            },



            ///////////// End for schedule

            ///////////// Start for Action Configuration

            {
                state: 'ActionType',
                config: {
                    url: '/ActionType',
                    params: {
                        RF_ACTION_CFG_H_ID: null,
                        LK_STATUS_TBL_ID: null,
                        lvl:false
                    },
                    views: {
                        "ActionType": {
                            controller: 'ActionTypeController',
                            controllerAs: 'vm',
                            templateUrl: '/Hr/Admin/ActionConfig/ActionType'
                        }
                    }
                }
            },
            {
                state: 'ActionType.add',
                config: {
                    params: {
                        atData: null,
                        configData: null
                    },
                    url: '/add',
                    controller: 'ActionConfigController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/Admin/ActionConfig/ActionConfig'
                }
            },

            {
                state: 'ActionType.approver',
                config: {
                    params: {
                        atData: null,
                        configData: null
                    },
                    url: '/approver',
                    controller: 'ActionApproverController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/Admin/ActionConfig/ActionApprover'
                }
            },

            {
                state: 'ActionType.approveradd',
                config: {
                    params: {
                        atData: null,
                        configData: null
                    },
                    url: '/approver/edit',
                    controller: 'ActionApproverAddController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/Admin/ActionConfig/ActionApproverAdd'
                }
            },




            ///////////// End for Action Configuration






        ];
    }
})();
