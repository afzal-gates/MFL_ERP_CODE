//(function() {
//    'use strict';

//    angular
//        .module('multitex.hr')
//        .run(appRun);

//    // appRun.$inject = ['routehelper'];

//    /* @ngInject */
//    function appRun(routehelper) {
//        routehelper.configureRoutes(getRoutes());
//    }

//    function getRoutes() {
//        return [
//            {
//                url: '/',
//                config: {
//                    templateUrl: '/hr/HrOffice/OfficeEntry',
//                    controller: 'HrOfficeController',
//                    controllerAs: 'vm'
//                    //foodata: 'OfficeEntry'
//                }
//            },
//            {
//                url: '/OfficeEntry',
//                config: {
//                    templateUrl: '/hr/HrOffice/OfficeEntry',
//                    controller: 'HrOfficeController',
//                    controllerAs: 'vm'
//                    //foodata: 'OfficeEntry'
//                }
//            },
//             {
//                 url: '/OfficeList',
//                 config: {
//                     templateUrl: '/hr/HrOffice/OfficeList',
//                     controller: 'HrOfficeController',
//                     controllerAs: 'vm',
//                     foodata: 'OfficeList'
//                 }
//             }
//        ];
//    }
//})();



(function () {
    'use strict';

    angular
        .module('multitex.security')
        .run(appRun);

    /* @ngInject */
    function appRun(routerHelper) {

        routerHelper.configureStates(getStates(), '/');


    }

    function getStates() {
        return [

            {
                state: 'ChangePassword',
                config: {
                    url: '/changepassword',
                    controller: "ScUserController",
                    params: {
                        data: null
                    },
                    controllerAs: "vm",
                    templateUrl: '/Security/ScUser/_ChangePassword',
                    title: 'Change Password'
                }
            },

            {
                state: 'UserEntry',
                config: {
                    url: '/create',
                    controller: "ScUserController",
                    params: {
                        data: null
                    },
                    controllerAs: "vm",
                    templateUrl: '/Security/ScUser/_CreateUser',
                    title: 'Create User Profile'
                }
            },
            {
                state: 'UserList',
                config: {
                    url: '/List',
                    controller: "ScUserListController",
                    controllerAs: "vm",
                    templateUrl: '/Security/ScUser/_UserList',
                    title: 'User List'
                }
            },
            {
                state: 'RequestApprovalWorkFlow',
                config: {
                    url: '/RequestApprovalWorkFlow',
                    controller: "RequestApprovalWorkFlowController",
                    controllerAs: "vm",
                    templateUrl: '/Security/Security/_RequestApprovalWorkFlow',
                    title: 'Request & Approval Work Flow List'
                }
            },
            {
                state: 'UserStoreMap',
                config: {
                    url: '/UserStoreMap',
                    controller: "UserStoreMapController",
                    controllerAs: "vm",
                    templateUrl: '/Security/Security/_UserStoreMap',
                    title: 'User & Store Mapping'
                }
            },
            {
                state: 'UserOfficeMap',
                config: {
                    url: '/UserOfficeMap',
                    controller: "UserOfficeMapController",
                    controllerAs: "vm",
                    templateUrl: '/Security/Security/_UserOfficeMap',
                    title: 'User & Office Mapping'
                }
            },
            {
                state: 'ReportTemplate',
                config: {
                    url: '/ReportTemplate',
                    controller: "ReportTemplateController",
                    controllerAs: "vm",
                    templateUrl: '/Security/Security/_ReportTemplate',
                    title: 'Report Template'
                }
            },
            {
               state: 'GlobalNotification',
               config: {
                   url: '/GlobalNotification',
                   controller: 'GlobalNotificationController',
                   controllerAs: 'vm',
                   templateUrl: '/Security/Security/_GlobalNotification',
                   Title: 'Global Notification'
               }
            },
            {
                state: 'UserProdCatMap',
                config: {
                    url: '/UserProdCatMap',
                    controller: "UserProdCatMapController",
                    controllerAs: "vm",
                    templateUrl: '/Security/Security/_UserProdCatMap',
                    title: 'User & Production Category Mapping'
                }
            },
            {
                state: 'FabDfctTypeMap',
                config: {
                    url: '/FabDfctTypeMap',
                    controller: "FabDfctTypeMapController",
                    controllerAs: "vm",
                    templateUrl: '/Security/Security/_FabDfctTypeMap',
                    title: 'Fabric Defect Type Mapping'
                }
            },
            {
                state: 'BuyerStoreMap',
                config: {
                    url: '/BuyerStoreMap',
                    controller: "BuyerStoreMapController",
                    controllerAs: "vm",
                    templateUrl: '/Security/Security/_BuyerStoreMap',
                    title: 'Buyer & Store Mapping'
                }
            },
            

            
            

        ];
    }
})();
