(function () {
    'use strict';

    angular
        .module('multitex.core')
        .run(appRun);

    /* @ngInject */
    function appRun(routerHelper, $state, $rootScope) {

        routerHelper.configureStates(getStates(), '/');
        $rootScope.$state = $state;
        $rootScope._ = window._;
    }

    function getStates() {
        return [
            {
                state: 'UserDashBoard',
                config: {
                    url: '/',
                    controller: 'UserDashBoardController',
                    controllerAs: 'vm',
                    templateUrl: '/Home/UserDashBoard'

                }
            },
            {
                state: 'SignInCaptcha',
                config: {
                    cache: false,
                    url: '/LoginCaptcha',
                    templateUrl: '/Security/ScUser/_CaptchaImage'

                }
            },
            {
                state: 'LineLoadPlan',
                config: {
                    url: '/LineLoadPlan?pLINE_LIST&pFLOOR_LIST&pIS_WIN_SCREEN',
                    views: {
                        "LineLoadPlan": {
                            controller: 'LineLoadPlanController',
                            controllerAs: 'vm',
                            templateUrl: '/Home/_LineLoadPlan'
                        },
                    },
                    resolve: {
                        FloorData: function (DashBoardService) {
                            return DashBoardService.getDataByUrl('/api/common/GetSewingFloorData');
                        },
                        BuyerAcc: function (DashBoardService) {
                            return DashBoardService.getDataByUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId');
                        }
                    },

                    Title: 'Line Load Plan'
                },
            },
            {
                state: 'HourlyProductionEntry',
                config: {
                    url: '/HourlyProductionEntry?pLINE_LIST&pFLOOR_LIST',
                    views: {
                        "HourlyProductionEntry": {
                            controller: 'HourlyProductionEntryController',
                            controllerAs: 'vm',
                            templateUrl: '/Home/_HourlyProductionEntry'
                        },
                    },
                    resolve: {
                        FloorData: function (DashBoardService) {
                            return DashBoardService.getDataByUrl('/api/common/GetSewingFloorData');
                        }
                    },

                    Title: 'Hourly Sewing Production Entry'
                },
            },
            {
                state: 'PartialManpowerEntry',
                config: {
                    url: '/PartialManpowerEntry?pLINE_LIST&pFLOOR_LIST',
                    views: {
                        "HourlyProductionEntry": {
                            controller: 'PartialManpowerEntryController',
                            controllerAs: 'vm',
                            templateUrl: '/Home/_PartitalManpowerEntry'
                        },
                    },
                    resolve: {
                        FloorData: function (DashBoardService) {
                            return DashBoardService.getDataByUrl('/api/common/GetSewingFloorData');
                        }
                    },

                    Title: 'Man Minutes Adjustment'
                },
            },

            {
                state: 'HourlyFinProductionEntry',
                config: {
                    url: '/HourlyFinProductionEntry?pLINE_LIST&pFLOOR_LIST',
                    views: {
                        "HourlyProductionEntry": {
                            controller: 'HourlyFinProductionEntryController',
                            controllerAs: 'vm',
                            templateUrl: '/Home/_HourlyFinProductionEntry'
                        },
                    },
                    resolve: {
                        FloorData: function (DashBoardService) {
                            return DashBoardService.getDataByUrl('/api/common/GetSewingFloorData');
                        }
                    },

                    Title: 'Hourly Finishing Production Entry'
                },
            },


        ];
    }
})();
