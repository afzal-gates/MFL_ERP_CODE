(function () {
    'use strict';

    angular
        .module('multitex.mrc')
        .run(appRun);

    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates(), '/');
    }

    function getStates() {
        return [


            /////////// Start for PrintNstrikeOff
            {
                state: 'PrintStrikeOff',
                config: {
                    url: '/PrintStrikeOff',
                    controller: 'PrintStrikeOffController',
                    params: {
                        pPrintObj: null
                    },
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_PrintStrikeOffEntry',
                    Title: 'Print Design and Strike Off'
                }
            },
            {
                state: 'PrintStrikeOffList',
                config: {
                    url: '/PrintStrikeOffList',
                    controller: 'PrintStrikeOffListController',
                    params: {
                        pPrintObj: null
                    },
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_PrintStrikeOffList',
                    Title: 'Print Design and Strike Off List'
                }
            },
            {
                state: 'PrintSubmission',
                config: {
                    url: '/PrintSubmission',
                    controller: 'PrintSubmissionController',
                    params: {
                        pPrintObj: null
                    },
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_printSubmission',
                    Title: 'Print and Strike Off Submission and Feedback'
                }
            },
            /////////// Start for PrintNstrikeOff

            /////////// Start for Labdip Info
            {
                state: 'LabdipEntry',
                config: {
                    url: '/LabdipEntry?ID&bAcId&pMC_BUYER_ID&pMC_STYLE_H_EXT_LST&pMC_STYLE_H_ID&pHAS_EXT',
                    controller: 'labdipInfoController',
                    resolve: {
                        Data: function (MrcDataService, $stateParams) {
                            if (angular.isDefined($stateParams.ID) && $stateParams.ID > 0) {
                                return MrcDataService.getDataByUrl('/Labdip/LabdipDataListByHID/' + $stateParams.ID);
                            } else {
                                return {};
                            }
                        },
                        FabricationList: function (MrcDataService, $stateParams) {
                            if ($stateParams.pMC_STYLE_H_ID) {
                                return MrcDataService.GetAllOthers('/api/mrc/StyleDItemFab/SelectFabByStyleID/' + $stateParams.pMC_STYLE_H_ID + '?pLK_FBR_GRP_ID=192');
                            } else {
                                return [];
                            }
                           
                        },
                        ColorList: function (MrcDataService, $stateParams) {
                            if ($stateParams.pMC_STYLE_H_ID) {
                                return MrcDataService.GetAllOthers('/api/mrc/ColourMaster/ColourMappDataByBuyerId/' + $stateParams.pMC_STYLE_H_ID + '?pLK_COL_TYPE_LIST=358,407,360,361,432');
                            } else {
                                return [];
                            }                           
                        },
                        LightSourceList: function (MrcDataService) {
                            return MrcDataService.LookupListData(68);                      
                        },
                        StyleList: function (MrcDataService, $stateParams) {
                            if ($stateParams.pMC_STYLE_H_ID) {
                                return MrcDataService.GetAllOthers('/api/mrc/StyleH/Select/' + $stateParams.pMC_STYLE_H_ID);
                            } else {
                                return [];
                            }
                        },

                        OrderList: function (MrcDataService, $stateParams) {
                            if ($stateParams.pMC_STYLE_H_ID) {
                                return MrcDataService.GetAllOthers('/api/mrc/StyleH/OrdersByStyleID/' + $stateParams.pMC_STYLE_H_ID);
                            } else {
                                return [];
                            }
                        },



                    },
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_LabdipEntry',
                    Title: 'Lapdip Program'
                }
            },
            {
                state: 'LabdipList',
                config: {
                    url: '/LabdipList?BAC',
                    controller: 'labdipListController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_LabdipList',
                    Title: 'Labdip List'
                }
            },
            {
                state: 'LabdipSubmission',
                config: {
                    url: '/LabdipSubmission',
                    controller: 'labdipSubmissionController',
                    params: {
                        pLabdipObj: null
                    },
                    resolve: {
                        CompositionList: function (MrcDataService) {
                            return MrcDataService.getDataByFullUrl('/api/Common/FibreCompList');
                        },
                        ColGrpList: function (MrcDataService) {
                            return MrcDataService.getDataByUrl('/ColourMaster/ColourGroupList');
                        },
                        DyeingMethod: function (MrcDataService) {
                            return MrcDataService.getDataByFullUrl('/api/common/GetDyeingMethodList');
                        }
                    },
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_LabdipSubmission',
                    Title: 'Labdip Submission and Feedback'
                }
            },
            /////////// Start for Labdip Info

            /////////// Start for Sample Booking

            {
                state: 'SmplFabBookEntry',
                config: {
                    url: '/smplfabbookentry/:pMC_SMP_REQ_H_ID?bAcId&bid&pLK_STYL_DEV_TYP_ID',
                    views: {
                        "SmplFabBookEntry": {
                            controller: 'smplFabBookController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_SmplFabBookEntry'
                        }
                    },
                    Title: 'Sample Fabric Booking',
                    resolve: {
                        byrAcData: function (MrcDataService, $stateParams) {
                            return MrcDataService.GetAllOthers('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                                var data = angular.copy(res);
                                return data;
                            }, function (err) {
                                console.log(err);
                            });                            
                        },
                        userData: function (MrcDataService, $stateParams) {                           
                            return MrcDataService.GetAllOthers('/api/mrc/UserBuyerAcc/GetUsersByByrAcLst?pMC_SMP_REQ_H_ID=' + $stateParams.pMC_SMP_REQ_H_ID).then(function (res) {
                                var data = angular.copy(res);
                                return data;
                            }, function (err) {
                                console.log(err);
                            });                            
                        },
                        formData: function (MrcDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pMC_SMP_REQ_H_ID) && $stateParams.pMC_SMP_REQ_H_ID > 0) {
                                return MrcDataService.getDataByUrl('/SampleReq/Select/' + $stateParams.pMC_SMP_REQ_H_ID).then(function (res) {
                                    //var vReqDate = res.SMP_REQ_DT;// kendo.toString(res.SMP_REQ_DT, "dd/MMM/yyyy hh:mm tt");
                                    //alert(vReqDate);
                                    //res['SMP_REQ_DT'] = vReqDate;
                                    console.log(res);
                                    return res;
                                }, function (err) {
                                    console.log(err);
                                });
                            }
                            else {
                                return {};
                            }
                        }
                    }
                }
            },
            {
                state: 'SmplFabBookEntry.Dtl',
                config: {
                    url: '/dtl',
                    views: {
                        "SmplFabBookEntryDtl": {
                            controller: 'smplFabBookDtlController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_SmplFabBookEntryDtl'
                        }
                    },
                    Title: 'Sample Fabric Booking',
                    resolve: {
                        formDtlList: function (MrcDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pMC_SMP_REQ_H_ID) && $stateParams.pMC_SMP_REQ_H_ID > 0) {
                                return MrcDataService.getDataByUrl('/SampleReq/SampReqDtlListByHID/' + $stateParams.pMC_SMP_REQ_H_ID).then(function (res) {
                                    var data = angular.copy(res);
                                    return data;
                                }, function (err) {
                                    console.log(err);
                                });
                            }
                            else {
                                return {};
                            }
                        },
                        bookingData: function (MrcDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pMC_SMP_REQ_H_ID) && $stateParams.pMC_SMP_REQ_H_ID > 0) {
                                return MrcDataService.getDataByUrl('/SampleReq/SamFabBookingQtyList/' + $stateParams.pMC_SMP_REQ_H_ID).then(function (res) {
                                    
                                    var data = angular.copy(res);
                                    return data;
                                }, function (err) {
                                    console.log(err);
                                });
                            }
                            else {
                                return {};
                            }
                        }
                    }
                }
            },
            {
                state: 'SmplFabBookList',
                config: {
                    url: '/smplfabbooklist/:pMC_BYR_ACC_ID/:pMC_STYLE_H_ID',
                    views: {
                        "SmplFabBookList": {
                    controller: 'smplFabBookListController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_SmplFabBookList'
                        }
                    },
                    params: {
                        pBookingObj: null
                    },
                    Title: 'Sample Program & Fabric Booking List'
                }
            },
            {
                state: 'SmplProdEntry',
                config: {
                    url: '/smplprodentry/:pMC_SMP_REQ_H_ID/:pMC_BYR_ACC_ID/:pMC_STYLE_H_EXT_ID/:pRF_SMPL_TYPE_ID/:pPROD_DT/:pPROD_BATCH_NO',
                    views: {
                        "SmplProdEntry": {
                            controller: 'SmplProdController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_SmplProdEntry'
                        }
                    },
                    Title: 'Sample Production',
                    resolve: {
                        formData: function (MrcDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pMC_SMP_REQ_H_ID) && $stateParams.pMC_SMP_REQ_H_ID > 0) {
                                return MrcDataService.getDataByUrl('/SampleReq/getSmpHdr/' + $stateParams.pMC_SMP_REQ_H_ID).then(function (res) {
                                    var data = angular.copy(res);
                                    return data;
                                }, function (err) {
                                    console.log(err);
                                });
                            }
                            else {
                                return {};
                            }
                        }
                    }
                }
            },                        
            {
                state: 'SmplProdEntry.Dtl',
                config: {
                    url: '/dtl',
                    views: {
                        "SmplProdEntryDtl": {
                            controller: 'SmplProdDtlController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_SmplProdEntryDtl'
                        }
                    },
                    Title: 'Sample Production',
                }
            },
            {
                state: 'SmplBuyerFeedback',
                config: {
                    url: '/smpByrFeedback/:pMC_SMP_REQ_D_ID',
                    views: {
                        "SmplBuyerFeedback": {
                            controller: 'SmplBuyerFeedbackController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_SmplBuyerFeedback'
                        }
                    },
                    Title: 'Buyer Feedback for Sample',
                }
            },
            /////////// Start for Sample Booking


            /////////// Start for Order Info
            {
                state: 'OrderH',
                config: {
                    url: '/orderh/:pMC_ORDER_H_ID?SID&BAID',
                    controller: 'OrderHController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_OrderH',
                    //Title: 'Order Confirmation Detail',
                    resolve: {
                        formData: function (MrcDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pMC_ORDER_H_ID) && $stateParams.pMC_ORDER_H_ID > 0) {
                                return MrcDataService.getDataByUrl('/Order/OrderHdrData/' + $stateParams.pMC_ORDER_H_ID).then(function (res) {
                                    var data = angular.copy(res);
                                    return data;
                                }, function (err) {
                                    console.log(err);
                                });
                            }
                            else {
                                return {};
                            }
                        },
                        ordStatusList: function (MrcDataService, $stateParams) {
                            return MrcDataService.LookupListData(56).then(function (res) {
                                var data = angular.copy(res);
                                return data;
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                }
            },
            {
                state: 'OrderH.Dtl',
                config: {
                    url: '/dtl',
                    views: {
                        "OrderDtl": {
                            controller: 'OrderDtlController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_OrderDtl',
                        }
                    },
                    Title: 'Order Confirmation Detail',
                    resolve: {
                        formItemsStyleData: function (MrcDataService, $stateParams) {
                            //if (angular.isDefined($stateParams.pMC_ORDER_H_ID) && $stateParams.pMC_ORDER_H_ID > 0) {
                            //    return MrcDataService.getDataByUrl('/Order/OrderItemsData/' + $stateParams.pMC_ORDER_H_ID).then(function (res) {
                            //        var data = angular.copy(res);
                            //        return data;
                            //    }, function (err) {
                            //        console.log(err);
                            //    });
                            //}
                            //else {
                            //    return {};
                            //}
                        }
                    }
                }
            },
            {
                state: 'OrderList',
                config: {
                    url: '/orderList/:pMC_BYR_ACC_ID/:pMC_STYLE_H_ID',
                    controller: 'orderListController',
                    params: {
                        pOrderObj: null,
                        pMC_BYR_ACC_ID: null
                    },
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_OrderList',
                    Title: 'Order Confirmation List'
                }
            },
            /////////// Start for Order Info


            /////////// Start for Bulk Fabric Booking
            {
                state: 'BulkFabBkEntry',
                config: {
                    url: '/bulkFabBkEntry/:pMC_BLK_FAB_REQ_H_ID?SID&BAID&SEXID&ORDID&pIS_COPY',
                    controller: 'bulkFabBkController',
                    //params: {
                    //    pMC_BLK_FAB_REQ_H_ID: null,
                    //},
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_BulkFabBkEntry',
                    Title: 'Bulk Fabric Booking',
                    resolve: {
                        formData: function (MrcDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pMC_BLK_FAB_REQ_H_ID) && $stateParams.pMC_BLK_FAB_REQ_H_ID > 0) {
                                return MrcDataService.getDataByUrl('/BulkFabBk/Select/' + $stateParams.pMC_BLK_FAB_REQ_H_ID).then(function (res) {
                                    
                                    if ($stateParams.pIS_COPY == 'Y') {
                                        res['IS_BFB_FINALIZED'] = 'N';                                        
                                    }

                                    var data = angular.copy(res);
                                    var ordList = res.MC_ORDER_H_ID_LST.split(',');
                                    var itmList = res.STYLE_D_ITEM_LST.split(',');

                                    data.MC_ORDER_H_ID_LST = ordList;
                                    data.STYLE_D_ITEM_LST = itmList;

                                    console.log(data);

                                    return data;
                                    //vm.buyerAcList = res;
                                }, function (err) {
                                    console.log(err);
                                });
                            }
                            else {
                                return {};
                            }
                        },
                        revisionList: function (MrcDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pMC_BLK_FAB_REQ_H_ID) && $stateParams.pMC_BLK_FAB_REQ_H_ID > 0) {
                                return MrcDataService.getDataByUrl('/BulkFabBk/RevisionList?pMC_BLK_FAB_REQ_H_ID=' + $stateParams.pMC_BLK_FAB_REQ_H_ID).then(function (res) {
                                    var data = angular.copy(res);
                                    data.splice(0, 1);

                                    return data;

                                }, function (err) {
                                    console.log(err);
                                });
                            }
                            else {
                                return {};
                            }
                        }
                    }
                }
            },
            {
                state: 'BulkFabBkEntry.Dtl',
                config: {
                    url: '/dtl',
                    views: {
                        "BulkFabBkDtl": {
                            controller: 'bulkFabBkDtlController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_BulkFabBkCadCon',
                            //params: {
                            //    MC_BLK_FAB_REQ_H_ID: null,
                            //    MC_BLK_FAB_REQ_D_ID: null
                            //},
                        }
                    },
                    Title: 'Bulk Fabric Booking'
                }
            },
            {
                state: 'BulkFabBkEntry.ProcessConsData',
                config: {
                    url: '/processConsData',
                    views: {
                        "ProcessConsDtl": {
                            controller: 'bulkFabBkDtlController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_BulkFabBkProcessConsDtl',
                            //params: {
                            //    MC_BLK_FAB_REQ_H_ID: null,
                            //    MC_BLK_FAB_REQ_D_ID: null
                            //},
                        }
                    },
                    Title: 'Bulk Fabric Booking'
                }
            },
            {
                state: 'BulkFabBkList',
                config: {
                    url: '/bulkFabBkList/:pMC_BYR_ACC_ID/:pMC_STYLE_H_ID',
                    controller: 'bulkFabBkListController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_BulkFabBkList',
                    Title: 'Bulk Fabric Booking List'//,
                    //resolve: {
                    //    revisionList: function (MrcDataService, $stateParams) {
                    //        if (angular.isDefined($stateParams.pMC_BLK_FAB_REQ_H_ID) && $stateParams.pMC_BLK_FAB_REQ_H_ID > 0) {
                    //            return MrcDataService.getDataByUrl('/BulkFabBk/RevisionList?pMC_BLK_FAB_REQ_H_ID=' + $stateParams.pMC_BLK_FAB_REQ_H_ID).then(function (res) {
                    //                var data = angular.copy(res);

                    //                return data;

                    //            }, function (err) {
                    //                console.log(err);
                    //            });
                    //        }
                    //        else {
                    //            return {};
                    //        }
                    //    }
                    //}
                }
            },            
            {
                state: 'BlkBudgetAprvl',
                config: {
                    url: '/blkbudgetaprvl',
                    controller: 'BlkBudgetAprvlController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_BulkBudgetAprvl',
                    Title: 'Fabric Booking List (Approved)'
                }
            },
            /////////// Start for Bulk Fabric Booking


            /////////// Start for Buyer Info
            {
                state: 'BuyerEntry',
                config: {
                    url: '/BuyerEntry',
                    controller: 'BuyerInfoController',
                    params: {
                        MC_BUYER_ID: null
                    },
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_BuyerEntry',
                    Title: 'Buyer Master',
                    resolve: {
                        formData: function (MrcDataService, $stateParams) {
                            if (angular.isDefined($stateParams.MC_BUYER_ID) && $stateParams.MC_BUYER_ID > 0) {
                                return MrcDataService.selectBuyerData($stateParams.MC_BUYER_ID);
                            } else {
                                return {};
                            }
                        }
                    }
                }
            },
            {
                state: 'BuyerList',
                config: {
                    url: '/BuyerList',
                    controller: 'BuyerListController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_BuyerList',
                    Title: 'Buyer List',
                    resolve: {
                        BuyerList: function (MrcDataService) {
                            return MrcDataService.getAllBuyerDatas();
                        }
                    }
                }
            },
            ///////////// End for Buyer Info

             /////////// Start for Office
            {
                state: 'OfficeEntry',
                config: {
                    url: '/OfficeEntry',
                    controller: 'OfficeInfoController',
                    params: {
                        MC_BUYER_OFF_ID: null
                    },
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_OfficeEntry',
                    Title: 'Buyer Office Master',
                    resolve: {
                        formData: function (MrcDataService, $stateParams) {
                            if (angular.isDefined($stateParams.MC_BUYER_OFF_ID) && $stateParams.MC_BUYER_OFF_ID > 0) {
                                return MrcDataService.selectOfficeData($stateParams.MC_BUYER_OFF_ID);
                            } else {
                                return {};
                            }
                        }
                    }
                }
            },

            {
                state: 'OfficeList',
                config: {
                    url: '/OfficeList',
                    controller: 'OfficeListController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_OfficeList',
                    Title: 'Office List',
                    resolve: {
                        OfficeList: function (MrcDataService) {
                            return MrcDataService.selectAllData("BuyerOffice");
                        }
                    }
                }
            },

        ///////////// End for Office
        {
            state: 'SizeMaster',
            config: {
                url: '/SizeMaster',
                controller: 'SizeMasterController',
                params: {
                    MC_SIZE_ID: null
                },
                controllerAs: 'vm',
                templateUrl: '/Merchandising/Mrc/_SizeMaster',
                Title: 'Size Master',
                resolve: {
                    formData: function (MrcDataService, $stateParams) {
                        if (angular.isDefined($stateParams.MC_SIZE_ID) && $stateParams.MC_SIZE_ID > 0) {
                            return MrcDataService.selectSizeData($stateParams.MC_SIZE_ID);
                        } else {
                            return {};
                        }
                    },
                    AllSizeDatas: function (MrcDataService) {
                        return MrcDataService.getAllSizeDatas();
                    }
                }
            }
        },

        {
            state: 'ColourMaster',
            config: {
                url: '/ColourMaster',
                controller: 'ColourMasterController',
                params: {
                    MC_COLOR_ID: null
                },
                controllerAs: 'vm',
                templateUrl: '/Merchandising/Mrc/_ColourMaster',
                Title: 'Colour Master',
                resolve: {
                    formData: function (MrcDataService, $stateParams) {
                        if (angular.isDefined($stateParams.MC_COLOR_ID) && $stateParams.MC_COLOR_ID > 0) {
                            return MrcDataService.selectData('ColourMaster', $stateParams.MC_COLOR_ID);
                        } else {
                            return {};
                        }
                    },
                    ColourList: function (MrcDataService) {
                        return MrcDataService.selectAllData('ColourMaster');
                    }
                }
            }
        },
        {
            state: 'TnaTemplate',
            config: {
                url: '/TnaTemplate/{MC_TNA_TMPLT_H_ID}?CPY_TNA_TMPLT_ID',
                controller: 'TnaTemplateController',
                controllerAs: 'vm',
                templateUrl: '/Merchandising/Mrc/_TnaTemplate',
                Title: 'Template for TNA',
                resolve: {
                    formData: function (MrcDataService, $stateParams) {

                        if ($stateParams.CPY_TNA_TMPLT_ID && $stateParams.CPY_TNA_TMPLT_ID > 0 && $stateParams.MC_TNA_TMPLT_H_ID == 0) {
                            return MrcDataService.selectData('TnaTemplate', $stateParams.CPY_TNA_TMPLT_ID);
                        } else if (!$stateParams.CPY_TNA_TMPLT_ID && $stateParams.MC_TNA_TMPLT_H_ID > 0) {
                            return MrcDataService.selectData('TnaTemplate', $stateParams.MC_TNA_TMPLT_H_ID);
                        } else {
                            return [];
                        }
                    }
                }
            }
        },
        {
            state: 'TnaTemplateList',
            config: {
                url: '/TnaTemplateList',
                controller: 'TnaTemplateListController',
                controllerAs: 'vm',
                templateUrl: '/Merchandising/Mrc/_TnaTemplateList',
                Title: 'TNA Template List'
            }
        },

        {
            state: 'TnaTemplate.Dtl',
            config: {
                url: '/Dtl',
                controller: 'TnaTemplateDtlController',
                controllerAs: 'vm',
                templateUrl: '/Merchandising/Mrc/_TnaTemplateDtl',
                resolve: {
                    TnATaskList: function (MrcDataService, $stateParams) {
                        return MrcDataService.getTaskList($stateParams.MC_TNA_TMPLT_H_ID);
                    }
                }
            }
        },

        {
            state: 'TaskList',
            config: {
                url: '/TaskList',
                controller: 'TaskListController',
                params: {
                    MC_TNA_TASK_ID: null
                },
                controllerAs: 'vm',
                templateUrl: '/Merchandising/Mrc/_Task',
                Title: 'Task/Action List for Garment Supply Chain',
                resolve: {
                    formData: function (MrcDataService, $stateParams) {
                        if (angular.isDefined($stateParams.MC_TNA_TASK_ID) && $stateParams.MC_TNA_TASK_ID > 0) {
                            return MrcDataService.selectData('TnaTask', $stateParams.MC_TNA_TASK_ID);
                        } else {
                            return {};
                        }
                    }
                }
            }
        },
        {
            state: 'UserBuyerAcc',
            config: {
                url: '/List',
                controller: 'UserBuyerAccController',
                controllerAs: 'vm',
                templateUrl: '/Merchandising/Mrc/_BuyerPermission',
                Title: 'Buyer Account Permission'
            }
        },

        {
            state: 'TnaTaskPermission',
            config: {
                url: '/TnA/TaskPermission?Id',
                controller: 'TnaTaskPermissionController',
                controllerAs: 'vm',
                templateUrl: '/Merchandising/Mrc/_TnaTaskPermission',
                Title: 'T&A Task Permission'
            }
        },

        {
            state: 'Inquiry',
            config: {
                url: '/Inq/:MC_INQR_H_ID',
                controller: 'InquiryController',
                controllerAs: 'vm',
                templateUrl: '/Merchandising/Mrc/_Inquiry',
                Title: 'Inquiry',
                resolve: {
                    formData: function (MrcDataService, $stateParams) {
                        if (angular.isDefined($stateParams.MC_INQR_H_ID) && $stateParams.MC_INQR_H_ID > 0) {
                            return MrcDataService.selectData('InquiryH', $stateParams.MC_INQR_H_ID);
                        } else {
                            return {};
                        }
                    }
                }
            }
        },
        {
            state: 'InquiryList',
            config: {
                url: '/InqList',
                controller: 'InquiryListController',
                controllerAs: 'vm',
                templateUrl: '/Merchandising/Mrc/_InquiryList',
                Title: 'Colour Master'
            }
        },
        {
            state: 'Inquiry.Style',
            config: {
                url: '/Style/:MC_STYLE_ID',
                controller: 'InqStyleController',
                controllerAs: 'vm',
                templateUrl: '/Merchandising/Mrc/_InqStyle',
                Title: 'Style Entry',
                resolve: {
                    formData: function (MrcDataService, $stateParams) {
                        if (angular.isDefined($stateParams.MC_STYLE_ID) && $stateParams.MC_STYLE_ID > 0) {
                            return MrcDataService.selectData('Style', $stateParams.MC_STYLE_ID);
                        } else {
                            return {};
                        }
                    }
                }
            }
        },

        {
            state: 'Inquiry.StyleList',
            config: {
                url: '/Style/:StyleId/List',
                controller: 'InqStyleListController',
                controllerAs: 'vm',
                templateUrl: '/Merchandising/Mrc/_InqStyleList',
                Title: 'Colour Master'
            }
        },


        {
            state: 'Inquiry.Style.Item',
            config: {
                url: '/Item/:MC_STYLE_ITEM_ID',
                controller: 'InqItemController',
                controllerAs: 'vm',
                templateUrl: '/Merchandising/Mrc/_InqItem',
                Title: 'Colour Master',
                resolve: {
                    formData: function (MrcDataService, $stateParams) {
                        if (angular.isDefined($stateParams.MC_STYLE_ITEM_ID) && $stateParams.MC_STYLE_ITEM_ID > 0) {
                            return MrcDataService.selectData('StyleItem', $stateParams.MC_STYLE_ITEM_ID);
                        } else {
                            return {};
                        }
                    },
                    StyleData: function (MrcDataService, $stateParams) {
                        if (angular.isDefined($stateParams.MC_STYLE_ID) && $stateParams.MC_STYLE_ID > 0) {
                            return MrcDataService.selectData('Style', $stateParams.MC_STYLE_ID);
                        } else {
                            return {};
                        }
                    }
                }
            }
        },

        {
            state: 'Inquiry.Style.ItemList',
            config: {
                url: '/Item/:ItemId/List',
                controller: 'InqItemListController',
                controllerAs: 'vm',
                templateUrl: '/Merchandising/Mrc/_InqItemList',
                Title: 'Colour Master'
            }
        },

        {
            state: 'Inquiry.Style.Item.Component',
            config: {
                url: '/Component/:ComponetId',
                controller: 'InqComponentController',
                controllerAs: 'vm',
                templateUrl: '/Merchandising/Mrc/_InqComponent',
                Title: 'Colour Master',
                resolve: {
                    formData: function (MrcDataService, $stateParams) {
                        if (angular.isDefined($stateParams.MC_COLOR_ID) && $stateParams.MC_COLOR_ID > 0) {
                            return MrcDataService.selectData('ColourMaster', $stateParams.MC_COLOR_ID);
                        } else {
                            return {};
                        }
                    }
                }
            }
        },

        {
            state: 'Inquiry.Style.Item.ComponentList',
            config: {
                url: '/Component/:ComponetId/List',
                controller: 'InqComponentListController',
                controllerAs: 'vm',
                templateUrl: '/Merchandising/Mrc/_InqComponentList',
                Title: 'Colour Master'
            }
        },

        {
            state: 'StyleH',
            config: {
                url: '/master/{MC_STYLE_H_ID:int}',
                controller: 'StyleMasterController',
                params: {
                    MC_STYLE_H_ID: 0
                },
                controllerAs: 'vm',
                templateUrl: '/Merchandising/Mrc/_StyleMaster',
                resolve: {
                    formData: function (MrcDataService, $stateParams) {
                        if (angular.isDefined($stateParams.MC_STYLE_H_ID) && $stateParams.MC_STYLE_H_ID > 0) {
                            return MrcDataService.selectData('StyleH', $stateParams.MC_STYLE_H_ID);
                        } else {
                            return {};
                        }
                    }                    
                },
                Title: 'Style Registration',
                LK_STYL_DEV_ID: 366,

            }
        },

        {
            state: 'StyleHDev',
            config: {
                url: '/masterDev/{MC_STYLE_H_ID:int}',
                controller: 'StyleMasterController',
                params: {
                    MC_STYLE_H_ID: 0
                },
                controllerAs: 'vm',
                templateUrl: '/Merchandising/Mrc/_StyleMaster?viewName=_StyleMasterDev',
                resolve: {
                    formData: function (MrcDataService, $stateParams) {
                        if (angular.isDefined($stateParams.MC_STYLE_H_ID) && $stateParams.MC_STYLE_H_ID > 0) {
                            //return MrcDataService.getDataByFullUrl('/api/mrc/StyleH/Select/' + $stateParams.MC_STYLE_H_ID + '?pLK_STYL_DEV_ID=265').then(function (res) {
                            //    return res.data;

                            //    console.log(res);
                            //}, function (err) {
                            //    console.log(err);
                            //});

                            return MrcDataService.selectData('StyleH', $stateParams.MC_STYLE_H_ID);

                        } else {
                            return {};
                        }
                    }
                },
                LK_STYL_DEV_ID: 265,
                Title: 'Development Style Registration'
            }
        },        
        {
            state: 'StyleH.Item',
            config: {
                url: '/Item/{MC_STYLE_D_ITEM_ID:int}?PARENT_ID',
                views: {
                    "Item": {
                        controller: 'StyleDItemController',
                        controllerAs: 'vm',
                        templateUrl: '/Merchandising/Mrc/_StyleDItem'
                    }
                },

                resolve: {
                    formData: function (MrcDataService, $stateParams) {
                        if (angular.isDefined($stateParams.MC_STYLE_D_ITEM_ID) && $stateParams.MC_STYLE_D_ITEM_ID > 0) {
                            return MrcDataService.selectData('StyleDItem', $stateParams.MC_STYLE_D_ITEM_ID);
                        } else {
                            return {};
                        }
                    }
                },
             LK_STYL_DEV_ID: 366
            }
            
        },

        {
            state: 'StyleHDev.ItemDev',
            config: {
                url: '/ItemDev/{MC_STYLE_D_ITEM_ID:int}?PARENT_ID',
                views: {
                    "Item": {
                        controller: 'StyleDItemController',
                        controllerAs: 'vm',
                        templateUrl: '/Merchandising/Mrc/_StyleDItem?viewName=_StyleDItemDev'
                    }
                },
                resolve: {
                    formData: function (MrcDataService, $stateParams) {
                        if (angular.isDefined($stateParams.MC_STYLE_D_ITEM_ID) && $stateParams.MC_STYLE_D_ITEM_ID > 0) {
                            return MrcDataService.selectData('StyleDItem', $stateParams.MC_STYLE_D_ITEM_ID);
                        } else {
                            return {};
                        }
                    }
                },
                LK_STYL_DEV_ID: 265
            }
        },
        {
            state: 'StyleH.Fab',
            config: {
                url: '/Fab/:MC_STYLE_D_FAB_ID',
                views: {
                    "Fabrication": {
                        controller: 'StyleDItemFabController',
                        controllerAs: 'vm',
                        templateUrl: '/Merchandising/Mrc/_StyleDItemFabrication'
                    }
                },
                resolve: {
                    formData: function (MrcDataService, $stateParams) {
                        if (angular.isDefined($stateParams.MC_STYLE_D_FAB_ID) && $stateParams.MC_STYLE_D_FAB_ID > 0) {
                            return MrcDataService.selectData('StyleDItemFab', $stateParams.MC_STYLE_D_FAB_ID);
                        } else {
                            return {};
                        }
                    },
                    FiberTypeList: function (MrcDataService) {
                        return MrcDataService.LookupListData(76);
                    },
                    YrnCount: function (MrcDataService) {
                        return MrcDataService.getDataByFullUrl('/api/Common/YarnCountList');
                    }
                },
                LK_STYL_DEV_ID: 366

            }
        },

                {
                    state: 'StyleHDev.FabDev',
                    config: {
                        url: '/FabDev/:MC_STYLE_D_FAB_ID',
                        views: {
                            "Fabrication": {
                                controller: 'StyleDItemFabController',
                                controllerAs: 'vm',
                                templateUrl: '/Merchandising/Mrc/_StyleDItemFabrication?viewName=_StyleDItemFabricationDev'
                            }
                        },
                        resolve: {
                            formData: function (MrcDataService, $stateParams) {
                                if (angular.isDefined($stateParams.MC_STYLE_D_FAB_ID) && $stateParams.MC_STYLE_D_FAB_ID > 0) {
                                    return MrcDataService.selectData('StyleDItemFab', $stateParams.MC_STYLE_D_FAB_ID);
                                } else {
                                    return {};
                                }
                            },
                            FiberTypeList: function (MrcDataService) {
                                return MrcDataService.LookupListData(76);
                            },
                            YrnCount: function (MrcDataService) {
                                return MrcDataService.getDataByFullUrl('/api/Common/YarnCountList');
                            }
                        },
                        LK_STYL_DEV_ID: 265

                    }
                },
        {
            state: 'StyleH.OrderCon',
            config: {
                url: '/OrderCon/:pMC_STYLE_H_EXT_ID?pIS_N_R&pWORK_STYLE_NO',
                views: {
                    "OrderCon": {
                        controller: 'StyleOrderConController',
                        controllerAs: 'vm',
                        templateUrl: '/Merchandising/Mrc/_StyleOrderCon'
                    }
                },
                LK_STYL_DEV_ID: 366
            }
        },

        {
            state: 'StyleH.OrderCon.Detail',
            config: {
                url: '/Order/:pMC_ORDER_H_ID',
                views: {
                    "Detail": {
                        controller: 'StyleOrderConDtlController',
                        controllerAs: 'vm',
                        templateUrl: '/Merchandising/Mrc/_StyleOrderConDtl'
                    }
                },
                resolve: {
                    formData: function (MrcDataService, $stateParams) {
                        return MrcDataService.getDataByUrl('/StyleH/SelectOrder/' + ($stateParams.pMC_ORDER_H_ID || -1) + '?pMC_STYLE_H_ID=' + $stateParams.MC_STYLE_H_ID);
                    }
                },

                LK_STYL_DEV_ID: 366
            }
        },

        {
                    state: 'StyleHDev.OrderConDev',
                    config: {
                        url: '/OrderConDev/:pMC_STYLE_H_EXT_ID?pIS_N_R&pWORK_STYLE_NO',
                        views: {
                            "OrderCon": {
                                controller: 'StyleOrderConController',
                                controllerAs: 'vm',
                                templateUrl: '/Merchandising/Mrc/_StyleOrderCon?viewName=_StyleOrderConDev'
                            }
                        },
                        LK_STYL_DEV_ID: 265
                    }
                },

        {
            state: 'StyleHDev.OrderConDev.DetailDev',
            config: {
                url: '/OrderDev/:pMC_ORDER_H_ID',
                views: {
                    "Detail": {
                        controller: 'StyleOrderConDtlDevController',
                        controllerAs: 'vm',
                        templateUrl: '/Merchandising/Mrc/_StyleOrderConDtl?viewName=_StyleOrderConDtlDev'
                    }
                },
                resolve: {
                    formData: function (MrcDataService, $stateParams) {
                        if (angular.isDefined($stateParams.pMC_ORDER_H_ID) && $stateParams.pMC_ORDER_H_ID > 0) {
                            return MrcDataService.getDataByUrl('/StyleH/SelectOrder/' + $stateParams.pMC_ORDER_H_ID);
                        } else {
                            return {};
                        }
                    }
                },
                LK_STYL_DEV_ID: 265
            }
        },

        {
            state: 'StyleH.ColRef',
            config: {
                url: '/ColRef/:MC_BUYER_ID',
                views: {
                    "ColRef": {
                        controller: 'StyleColRefToBuyerController',
                        controllerAs: 'vm',
                        templateUrl: '/Merchandising/Mrc/_StyleColRef'
                    }
                },
                resolve: {
                    Style: function ($stateParams) {
                        return { MC_STYLE_H_ID: $stateParams.MC_STYLE_H_ID }
                    },
                    ColGrpList: function (MrcDataService) {
                        return MrcDataService.getDataByUrl('/ColourMaster/ColourGroupList');
                    },
                    DyeingMethod: function (MrcDataService) {
                        return MrcDataService.getDataByFullUrl('/api/common/GetDyeingMethodList');
                    }
                },
                LK_STYL_DEV_ID: 366

            }
        },
        {
            state: 'StyleHDev.ColRefDev',
            config: {
                url: '/ColRefDev/:MC_BUYER_ID',
                views: {
                    "ColRef": {
                        controller: 'StyleColRefToBuyerController',
                        controllerAs: 'vm',
                        templateUrl: '/Merchandising/Mrc/_StyleColRef?viewName=_StyleColRefDev'
                    }
                },
                resolve: {
                    Style: function ($stateParams) {
                        return { MC_STYLE_H_ID: $stateParams.MC_STYLE_H_ID }
                    },
                    ColGrpList: function (MrcDataService) {
                        return MrcDataService.getDataByUrl('/ColourMaster/ColourGroupList');
                    },
                    DyeingMethod: function (MrcDataService) {
                        return MrcDataService.getDataByFullUrl('/api/common/GetDyeingMethodList');
                    }
                },
                LK_STYL_DEV_ID: 265

            }
        },
        {
            state: 'StyleH.StyleBom',
            config: {
                url: '/StyleBom',
                views: {
                    "StyleBom": {
                        controller: 'StyleBomController',
                        controllerAs: 'vm',
                        templateUrl: '/Merchandising/Mrc/_StyleBom'
                    }
                },
                resolve: {
                    MouData: function (MrcDataService) {
                        return MrcDataService.getDataByFullUrl('/api/common/MOUList/N');
                    }
                }

            }
        },

        {
            state: 'OrderCon',
            config: {
                url: '/Style/{MC_STYLE_H_ID:int}',
                controller: 'OrderConController',
                params: {
                    MC_STYLE_H_ID: 0
                },
                controllerAs: 'vm',
                templateUrl: '/Merchandising/Mrc/_OrderConEntry',
                resolve: {
                    formData: function (MrcDataService, $stateParams) {
                        if (angular.isDefined($stateParams.MC_STYLE_H_ID) && $stateParams.MC_STYLE_H_ID > 0) {
                            return MrcDataService.getDataByUrl('/StyleH/SelectStyle/' + $stateParams.MC_STYLE_H_ID);
                        } else {
                            return {};
                        }
                    }
                },
                Title: 'Order Confirmation'
            }
        },

        {
            state: 'OrderCon.Dtl',
            config: {
                url: '/Order/{MC_ORDER_H_ID:int}',
                params: {
                    MC_ORDER_H_ID: 0
                },
                views: {
                    "OrderDetail": {
                        controller: 'OrderConDtlController',
                        controllerAs: 'vm',
                        templateUrl: '/Merchandising/Mrc/_OrderConDtlEntry'
                    },
                    //"MktCostBreakDown": {
                    //    controller: 'MktCostBreakDownController',
                    //    controllerAs: 'vm',
                    //    templateUrl: '/Merchandising/Mrc/_MktCostBreakDown'
                    //}
                },
                resolve: {
                    formData: function (MrcDataService, $stateParams) {
                            return MrcDataService.getDataByUrl('/StyleH/SelectOrder/' + ($stateParams.MC_ORDER_H_ID||-1));
                    },
                    MouData: function (MrcDataService) {
                        return MrcDataService.getDataByFullUrl('/api/common/MOUList/Y');
                    }
                }

            }
        },

        /////////// Start for StyleList            
            {
                state: 'StyleList',
                config: {
                    url: '/StyleList?BAC',
                    controller: 'StyleListController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_StyleList',
                    Title: 'Style Gallery',
                    LK_STYL_DEV_ID: 366
                }
            },

            {
                state: 'StyleListDev',
                config: {
                    url: '/StyleListDev?BAC',
                    controller: 'StyleListController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_StyleList?viewName=_StyleListDev',
                    Title: 'Development Style Gallery',
                    LK_STYL_DEV_ID: 265

                }
            },            
            ///////////// End for StyleList


            /////////// Buyer Wise Sample Mapping            
            {
                state: 'BuyerSampleMapping',
                config: {
                    url: '/BuyerSampleMap',
                    controller: 'BuyerSampleMappingController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_BuyerSample',
                    Title: 'Buyer Wise Sample Mapping ',
                    resolve: {
                        SizeList: function (MrcDataService) {
                            return MrcDataService.selectAllData('SizeMaster');
                        }
                    }
                }
            },
            /////////////  Buyer Wise Sample Mapping  

             /////////// Buyer Wise Sample Mapping            
            {
                state: 'GsmCountConfig',
                config: {
                    url: '/GsmCountConfig',
                    controller: 'GsmYarnConfigController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_GsmCountConfig',
                    Title: 'Yarn Count Config',
                    resolve: {
                        Constructions: function (MrcDataService) {
                            return MrcDataService.getDataByFullUrl('/api/Common/FabricTypeList?pINV_ITEM_CAT_ID=34');
                        },
                        FiberTypeList: function (MrcDataService) {
                            return MrcDataService.LookupListData(76);
                        },
                        Counts: function (MrcDataService) {
                            return MrcDataService.getDataByFullUrl('/api/Common/YarnCountList');
                        },
                        Gauges: function (MrcDataService) {
                            return MrcDataService.LookupListData(59);
                        },
                        CountPostions: function (MrcDataService) {
                            return MrcDataService.LookupListData(79);
                        },
                        Template: function (MrcDataService) {
                            return MrcDataService.getDataByUrl('/YarnSpec/FibCombTemplateData?pMC_FIB_COMB_TMPLT_ID');
                        }


                    }
                }
            },
            /////////////  Buyer Wise Sample Mapping  

           /////////// Yarn Item Reg             
            {
                state: 'YarnItemReg',
                config: {
                    url: '/YarnItemReg/{INV_ITEM_CAT_ID}',
                    params: {
                        INV_ITEM_CAT_ID: 0
                    },
                    controller: 'YarnItemController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_YarnItem',
                    Title: 'Yarn Item Registration',
                    resolve: {
                        formData: function (MrcDataService, $stateParams) {
                            if (angular.isDefined($stateParams.INV_ITEM_CAT_ID) && $stateParams.INV_ITEM_CAT_ID > 0) {
                                return MrcDataService.getDataByUrl('/StyleH/SelectOrder/' + $stateParams.INV_ITEM_CAT_ID);
                            } else {
                                return {};
                            }
                        }
                    }

                }
            },
            /////////////  Yarn Item Reg   
            /////////// Budget Sheet Start        
            {
                state: 'BudgetSheet',
                config: {
                    url: '/BudgetSheet/{pMC_BLK_FAB_REQ_H_ID:int}/{pMC_STYLE_H_ID:int}/{pMC_STYL_BGT_H_ID:int}',
                    controller: 'BudgetSheetController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_BudgetSheet',
                    Title: 'Budget Sheet',
                    resolve: {
                        formData: function (MrcDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pMC_BLK_FAB_REQ_H_ID) && $stateParams.pMC_BLK_FAB_REQ_H_ID > 0) {
                                return MrcDataService.getDataByUrl('/BudgetSheet/getBudgetHeaderData/' + $stateParams.pMC_BLK_FAB_REQ_H_ID);
                            }
                            else {
                                return {};
                            }
                        },
                        MouData: function (MrcDataService) {
                            return MrcDataService.getDataByFullUrl('/api/common/MOUList/N');
                        },
                        ItemData: function (MrcDataService, $stateParams) {
                            return MrcDataService.getDataByUrl('/StyleH/ItemDataForStyleBlkBOM/' + $stateParams.pMC_STYLE_H_ID + '/' + $stateParams.pMC_BLK_FAB_REQ_H_ID + '/' + $stateParams.pMC_STYL_BGT_H_ID)
                        }
                    }

                }
            },
            /////////////  Budget Sheet End 

            /////////// Rate Chart Start   
            {
                state: 'RateChart',
                config: {
                    url: '/RateChart',
                    controller: 'RateChartController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_RateChart',
                    Title: 'Rate Chart',
                    resolve: {
                        CurrencyList: function (MrcDataService) {
                            return MrcDataService.GetAllOthers('/api/common/CurrencyList');
                        },
                        MOUList: function (MrcDataService) {
                            return MrcDataService.getMOUList();
                        },
                        DyeingMethod: function (MrcDataService) {
                            return MrcDataService.getDataByFullUrl('/api/common/GetDyeingMethodList');
                        },
                        ColourGrp: function (MrcDataService) {
                            return MrcDataService.getDataByUrl('/ColourMaster/ColourGroupList');
                        },
                        Template: function (MrcDataService) {
                            return MrcDataService.getDataByUrl('/YarnSpec/FibCombTemplateData?pMC_FIB_COMB_TMPLT_ID');
                        }
                    }
                }
            },
            /////////////  Rate Chart Start   

            {
                state: 'RateChart.Knitting',
                config: {
                    url: '/Knitting',
                    Title: 'Rate Chart - Knitting',
                    views: {
                        "sub-view": {
                            controller: 'RateChartKnitingController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_RateChartKnitting'
                        }
                    }
                }
            },

            {
                state: 'RateChart.Dyeing',
                config: {
                    url: '/Dyeing',
                    Title: 'Rate Chart - Dyeing',
                    views: {
                        "sub-view": {
                            controller: 'RateChartDyeingController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_RateChartDyeing'
                        }
                    }
                }
            },

            {
                state: 'RateChart.DFin',
                config: {
                    url: '/DFin',
                    Title: 'Rate Chart - Dyeing Finishing',
                    views: {
                        "sub-view": {
                            controller: 'RateChartDFinController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_RateChartDFin'
                        }
                    }
                }
            },

            {
                state: 'RateChart.AOP',
                config: {
                    url: '/AOP',
                    Title: 'Rate Chart - AOP',
                    views: {
                        "sub-view": {
                            controller: 'RateChartAOPController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_RateChartAOP'
                        }
                    }
                }
            },

            {
                state: 'RateChart.YD',
                config: {
                    url: '/YD',
                    Title: 'Rate Chart - YD',
                    views: {
                        "sub-view": {
                            controller: 'RateChartYDController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_RateChartYD'
                        }
                    }
                }
            },
            {
                state: 'BuyerBom',
                config: {
                    url: '/BuyerTA',
                    Title: 'Buyer Wise Trim & Accessories',
                    controller: 'BuyerBomController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_BuyerBom',
                    resolve: {
                        MouData: function (MrcDataService) {
                            return MrcDataService.getDataByFullUrl('/api/common/MOUList/N');
                        }
                    }

                }
            },
            {
                state: 'TnAFollowup',
                config: {
                    url: '/main/{MC_BYR_ACC_ID:int}/TnaTask/{MC_ORDER_H_ID:int}',
                    Title: 'T&A Followup',
                    controller: 'TnAFollowupController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_TnAFollowup'

                }
            },
            {
                state: 'TnAFollowup.TnaTask',
                config: {
                    url: '/list',
                    Title: 'T&A Followup',
                    controller: 'TnAFollowupTaskController',
                    controllerAs: 'vm',
                    template: '<kendo-grid k-options="vm.PO_LIST_OPTIONS_TASK" k-data-source="vm.dataSource" id="PO_LIST_TASK"></kendo-grid>'
                }
            },
            {
                state: 'TempTaBk',
                config: {
                    url: '/TempTaBk/{pMC_ACCS_PO_TMPLT_H_ID:int}',
                    Title: 'Trim & Accessories Spec. Configuration',
                    controller: 'TempTaBkController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_TempTaBk_List'
                }
            },
            //{
            //    state: 'TaBooking',
            //    config: {
            //        url: '/TaBk/BgtHrd/{pMC_STYL_BGT_H_ID:int}/PoHrd/{pMC_ACCS_PO_H_ID:int}?pBLK_BOM_LIST&pBLK_BOM_ACT&pMC_STYLE_H_ID&pMC_FAB_PROD_ORD_H_ID&pIS_ACC_BK_BOM',
            //        Title: 'Trim & Accessories Booking',
            //        controller: 'TaBookingController',
            //        controllerAs: 'vm',
            //        resolve: {
            //            PoHeaderData: function (MrcDataService, $stateParams, multi_login_dept_id) {
            //                if ($stateParams.pMC_ACCS_PO_H_ID && $stateParams.pMC_ACCS_PO_H_ID > 0) {
            //                    return MrcDataService.getDataByFullUrl('/api/mrc/TaBooking/getPoHeaderData/' + $stateParams.pMC_ACCS_PO_H_ID +
            //                        '?pHR_DEPARTMENT_ID=' + multi_login_dept_id);
            //                } else {
            //                    return {};
            //                }                            
            //            }
            //        },
            //        templateUrl: '/Merchandising/Mrc/_TaBooking'
            //    }
            //},
            {
                state: 'TaBooking',
                config: {
                    url: '/TaBk/BgtHrd/0/PoHrd/{pSCM_PURC_REQ_H_ID:int}?pBLK_BOM_LIST&pBLK_BOM_ACT&pMC_STYLE_H_ID&pMC_FAB_PROD_ORD_H_ID&pIS_ACC_BK_BOM',
                    Title: 'Trim & Accessories Booking',
                    controller: 'TaBookingController',
                    controllerAs: 'vm',
                    resolve: {
                        PoHeaderData: function (MrcDataService, $stateParams, multi_login_dept_id) {
                            if ($stateParams.pSCM_PURC_REQ_H_ID && $stateParams.pSCM_PURC_REQ_H_ID > 0) {
                                return MrcDataService.getDataByFullUrl('/api/knit/KnitYarnReq/GetYarnBuffReqInfo?pSCM_PURC_REQ_H_ID=' + $stateParams.pSCM_PURC_REQ_H_ID);
                            } else {
                                return {};
                            }
                        }
                    },
                    templateUrl: '/Merchandising/Mrc/_TaBooking'
                }
            },
            {
                state: 'TaBooking.item',
                config: {
                    url: '/Item/{pINV_ITEM_ID:int}',
                    Title: 'Trim & Accessories Booking',
                    params: {
                        itemObj: {}
                    },
                    views: {
                        "TabInside": {
                            controller: 'TaBookingItemController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_TaBookingItem',
                        }
                    }
                }
            },
            {
                state: 'TaBookingList',
                config: {
                    url: '/TaBookingList?pMC_BYR_ACC_ID',
                    Title: 'Trim & Accessories Booking List',
                    controller: 'TaBookingListController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_TaBookingList'
                }
            },
            {
                state: 'TaBookingRpt',
                config: {
                    url: '/RptView?pMC_ACCS_PO_H_ID',
                    controller: 'TaBookingRptController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_TaBookingRpt',

                }
            },
            {
                state: 'TNATaskStatus',
                config: {
                    url: '/TNATaskStatus',
                    Title: 'TNA Task Status',
                    controller: 'TNATaskStatusController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_TNATaskStatus'
                }
            },            
            //{
            //    state: 'TaBookingRpt',
            //    config: {
            //        url: '/RptView',
            //        controller: 'TaBookingRptController',
            //        controllerAs: 'vm',
            //        templateUrl: '/Merchandising/Mrc/_TaBookingRpt',

            //    }
            //},
            {
                state: 'OrderSCM',
                config: {
                    url: '/OrderSCM',
                    Title: 'List of Confirmed Order & Development',//'Sourcing of Trims & Accessories',
                    controller: 'OrderSCMController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_OrderSCM'
                }
            },
            {
                state: 'CollarCuffManagement',
                config: {
                    url: '/CollarCuffManagement',
                    Title: 'Collar & Cuff Measurement Chart',//'Sourcing of Trims & Accessories',
                    controller: 'CollarCuffController',
                    templateUrl: '/Merchandising/Mrc/_CollarCuffManagement',
                    controllerAs: 'vm',
                    resolve: {
                        styleID: function () {
                            return null;
                        }
                    }
                }
            },
            {
                state: 'StyleDashboard',
                config: {
                    url: '/styleDashboard',
                    views: {
                        "StyleDashboard": {
                            controller: 'StyleDashboardController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_StyleDashboard'
                        }
                    },
                    Title: 'Style Dashboard'
                }
            },
            {
                state: 'MrcReportParams',
                config: {
                    url: '/mrcReportParams',
                    views: {
                        "MrcReportParams": {
                            controller: 'MrcReportController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_MrcReportParams'
                        }
                    },
                    Title: 'Merchandising Reports',
                    resolve: {
                        mrcRptData: function (MrcDataService) {
                            return MrcDataService.getDataByUrlNoToken('/api/common/getReportDataListByUser/' + 3); //// Here 3 is report group ID
                        }
                    }
                }
            },
            {
                state: 'TnaFollowupMatrixGrid',
                config: {
                    url: '/TnaFollowupMatrix',
                    views: {
                        "TnaFollowupMatrix": {
                            controller: 'TnaFollowupMatrixGridController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_TnaFollowupMatrix'
                        }
                    },
                    Title: 'TnA Followup Matrix View',
                    reloadOnSearch: false,
                    resolve: {
                        CODE_TEXT: function (MrcDataService) {
                            return MrcDataService.getDataByFullUrl('/api/common/getTnAMatrixTextCode');
                        }
                    }
                }
            },
            {
                state: 'DyeingProg4BulkBookingMnul',
                config: {
                    url: '/dyeProg4BulkMnul/:pMC_FAB_PROD_ORD_H_ID?&pRF_FAB_PROD_CAT_ID&pMC_BYR_ACC_GRP_ID&pMC_STYLE_H_EXT_ID',
                    views: {
                        "DyeingProg4BulkBookingMnul": {
                            controller: 'DyeingProgramController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_DyeingProg4BulkBookingMnul'
                        }
                    },
                    Title: 'Dyeing Program for Trims',
                    reloadOnSearch: false//,
                    //resolve: {
                    //    CODE_TEXT: function (MrcDataService) {
                    //        return MrcDataService.getDataByFullUrl('/api/common/getTnAMatrixTextCode');
                    //    }
                    //}
                }
            },
            {
                state: 'StyleListFabDev',
                config: {
                    url: '/styleListFabDev?BAC',                    
                    views: {
                        "StyleListFabDev": {
                            controller: 'StyleListFabDevController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_StyleListFabDev'
                        }
                    },                                        
                    Title: 'Fabric Development Style Gallery',
                    LK_STYL_DEV_ID: 555,
                    reloadOnSearch: false
                }
            },
            {
                state: 'StyleHFabDev',
                config: {
                    url: '/masterFabDev/{MC_STYLE_H_ID:int}',
                    views: {
                        "StyleHFabDev": {
                            controller: 'StyleMasterFabDevController',                            
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_StyleMasterFabDev'
                        }
                    },                                                       
                    resolve: {
                        formData: function (MrcDataService, $stateParams) {
                            if (angular.isDefined($stateParams.MC_STYLE_H_ID) && $stateParams.MC_STYLE_H_ID > 0) {
                                
                                return MrcDataService.selectData('StyleH', $stateParams.MC_STYLE_H_ID);

                            } else {
                                return {};
                            }
                        }
                    },
                    LK_STYL_DEV_ID: 555,
                    Title: 'Fabric Development Style Registration'
                }
            },
            {
                state: 'StyleHFabDev.Fab',
                config: {
                    url: '/Fab/:MC_STYLE_D_FAB_ID',
                    views: {
                        "FabricationFabDev": {
                            controller: 'StyleDItemFabricationFabDevController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_StyleDItemFabricationFabDev'
                        }
                    },
                    resolve: {
                        formData: function (MrcDataService, $stateParams) {
                            if (angular.isDefined($stateParams.MC_STYLE_D_FAB_ID) && $stateParams.MC_STYLE_D_FAB_ID > 0) {
                                return MrcDataService.selectData('StyleDItemFab', $stateParams.MC_STYLE_D_FAB_ID);
                            } else {
                                return {};
                            }
                        },
                        FiberTypeList: function (MrcDataService) {
                            return MrcDataService.LookupListData(76);
                        },
                        YrnCount: function (MrcDataService) {
                            return MrcDataService.getDataByFullUrl('/api/Common/YarnCountList');
                        }
                    },
                    LK_STYL_DEV_ID: 555,
                    Title: 'Fabric Development Style Registration'
                }
            },
            {
                state: 'StyleHFabDev.FabrBook',
                config: {
                    url: '/FabrBook/:MC_FAB_PROD_ORD_H_ID',
                    views: {
                        "FabricBookingFabDev": {
                            controller: 'StyleDFabricBookingFabDevController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_StyleDFabricBookingFabDev'
                        }
                    },
                    resolve: {
                        formData: function (MrcDataService, $stateParams) {
                            //alert($stateParams.MC_STYLE_H_ID);
                            if (angular.isDefined($stateParams.MC_STYLE_H_ID) && $stateParams.MC_STYLE_H_ID > 0) {
                                return MrcDataService.getDataByFullUrl('/api/Knit/FabProdKnitOrder/GetFabProdOrdHdrByStyleId?&pRF_FAB_PROD_CAT_ID=' + 3 + '&pMC_STYLE_H_ID=' + $stateParams.MC_STYLE_H_ID);
                            } else {
                              return {}
                            }
                        }                        
                    },                    
                    Title: 'Fabric Development Style Registration'
                }
            },
            {
                state: 'pms',
                config: {
                    url: '/pms/:pMC_BYR_ACC_ID',
                    controller: 'MrcPmsController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_pms',
                    Title: 'Buyer wise Style and Order Followup',
                    resolve: {
                        BuyerAccData: function (MrcDataService, $stateParams) {
                            return MrcDataService.getDataByUrl('/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                                return [{ BYR_ACC_NAME_EN: 'All', IS_TAB_ACT: true, MC_BYR_ACC_ID: -1 }].concat(res.map(function (o) {
                                    o['IS_TAB_ACT'] = false;
                                    return o;
                                }));
                            });
                        }
                    }
                }
            },
            {
                state: 'pms.grid',
                config: {
                    url: '/grid?pFIRSTDATE&pLASTDATE&page&pageSize&pMONTHOF',
                    views: {
                        "TabInside": {
                            controller: 'MrcPmsDController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_pmsD'
                        }
                    },
                    Title: 'Buyer wise Style and Order Followup'
                }
            },
            {
                state: 'report_pms',
                config: {
                    url: '/rpt?page&pageSize&pFIRSTDATE&pLASTDATE&pMC_BYR_ACC_ID&pRF_FAB_PROD_CAT_ID&pMC_ORDER_H_ID_LST&pMONTHOF',
                    controller: 'StyleOrderFollowupRptController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_StyleOrderFollowupRpt'
                }
            },

            {
                state: 'TaBookingPoRpt',
                config: {
                    url: '/RptPoView?pSCM_PURC_REQ_H_ID',
                    controller: 'TaBookingPoRptController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_TaBookingPoRpt',

                }           
            },
            {
                state: 'ProjectionOrderList',
                config: {
                    url: '/ProjectionOrderList',
                    controller: 'ProjectionOrderListController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_ProjectionOrderList',
                    Title: 'Projection/Call-Off Order List',

                }
            },
            {
                state: 'ProjectionOrder',
                config: {
                    url: '/ProjectionOrder?pMC_PROV_FAB_BK_H_ID&pIS_VIEW&pIS_REVISE&pMC_BYR_ACC_ID&pMC_BUYER_ID&pMC_STYLE_H_ID&pSTYLE_NO',
                    controller: 'ProjectionOrderController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_ProjectionOrder',
                    Title: 'Projection/Call-Off Order',
                    resolve: {
                        formData: function (MrcDataService, $stateParams, cur_user_id) {
                            if (angular.isDefined($stateParams.pMC_PROV_FAB_BK_H_ID) && $stateParams.pMC_PROV_FAB_BK_H_ID > 0) {

                                return MrcDataService.getDataByFullUrl('/api/mrc/ProjectionFabBk/SelectByID?pMC_PROV_FAB_BK_H_ID=' + ($stateParams.pMC_PROV_FAB_BK_H_ID || 0)).then(function (res) {
                                    return res;
                                });
                            }                            
                            else {
                                return {
                                    MC_PROV_FAB_BK_H_ID: -1,
                                    MC_BYR_ACC_ID: ($stateParams.pMC_BYR_ACC_ID || -1),
                                    MC_BUYER_ID: ($stateParams.pMC_BUYER_ID || -1),
                                    MC_STYLE_H_ID: ($stateParams.pMC_STYLE_H_ID || -1),
                                    STYLE_NO: ($stateParams.pSTYLE_NO || ''),
                                    LK_STYL_DEV_TYP_ID: 267,
                                    IS_PROV: 'Y',
                                    LK_STYL_DEV_ID: 366,
                                    QTY_MOU_ID: 1,
                                    PROV_FAB_BK_BY: parseInt(cur_user_id||0),
                                    PROV_FAB_BK_TO: 109, //49
                                    PROV_FAB_BK_DT: new Date(),


                                };
                            }
                        }
                    }
                }
            },
            {
                state: 'AccessoriesBooking',
                config: {
                    url: '/AccessoriesBooking',
                    controller: 'AccessoriesBookingController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_AccessoriesBooking',
                    Title: 'Accessories Booking',

                }
            },
            
            {
                state: 'AccessoriesBookingList',
                config: {
                    url: '/AccessoriesBookingList',
                    controller: 'AccessoriesBookingListController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_AccessoriesBookingList',
                    Title: 'Accessories Booking List',

                }
            },

            //{
            //    state: 'AccessoriesBooking',
            //    config: {
            //        url: '/AccessoriesBooking?pMC_PROV_FAB_BK_H_ID',
            //        controller: 'AccessoriesBookingController',
            //        controllerAs: 'vm',
            //        templateUrl: '/Merchandising/Mrc/_AccessoriesBooking',
            //        Title: 'Accessories Booking',
            //        resolve: {
            //            formData: function (MrcDataService, $stateParams) {
            //                if (angular.isDefined($stateParams.pMC_PROV_FAB_BK_H_ID) && $stateParams.pMC_PROV_FAB_BK_H_ID > 0) {

            //                    return MrcDataService.getDataByFullUrl('/api/mrc/AccessoriesBooking/SelectByID?pMC_PROV_FAB_BK_H_ID=' + ($stateParams.pMC_PROV_FAB_BK_H_ID || 0)).then(function (res) {
            //                        return res;
            //                    });
            //                }
            //                else {
            //                    return {};
            //                }
            //            }
            //        }
            //    }
            //},
            {
                state: 'AddFabBkingRpt',                
                config: {
                    url: '/addFabBkingRpt?pMC_BLK_ADFB_REQ_H_ID',
                    views: {
                        "AddFabBkingRpt": {
                            controller: 'AddFabBkingRptController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_AddFabBkingRpt'
                        }
                    },                    
                    resolve: {
                        addFabBkRptData: function (MrcDataService, $stateParams) {
                            return MrcDataService.getDataByFullUrl('/api/mrc/AddFabBk/GetAddFabBkRpt?pMC_BLK_ADFB_REQ_H_ID=' + $stateParams.pMC_BLK_ADFB_REQ_H_ID || 0);
                        }
                    },
                    Title: 'Additional Fabric Booking Report',
                    reloadOnSearch: false
                }
            },
            {
                state: 'AddFabBkingH',
                config: {
                    url: '/addFabBkingH/:pMC_BLK_ADFB_REQ_H_ID',
                    views: {
                        "AddFabBkingH": {
                            controller: 'AdditionalFabBkingHController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_AddFabBkingH'
                        }
                    },
                    resolve: {
                        addFabBkingHdrData: function (MrcDataService, $stateParams) {
                            if ($stateParams.pMC_BLK_ADFB_REQ_H_ID && $stateParams.pMC_BLK_ADFB_REQ_H_ID > 0) {
                                return MrcDataService.getDataByFullUrl('/api/mrc/AddFabBk/GetAddFabBkingHdr?pMC_BLK_ADFB_REQ_H_ID=' + ($stateParams.pMC_BLK_ADFB_REQ_H_ID || 0));
                            }
                            else {
                                return {};
                            }
                        },
                        userLavelData: function (MrcDataService, $stateParams) {
                            return MrcDataService.getDataByFullUrl('/api/mrc/AddFabBk/GetAddFabBkUserLavel');
                        }
                    },
                    Title: 'Requisition for Additional Fabric Booking',
                    reloadOnSearch: false
                }
            },
            {
                state: 'AddFabBkingH.Dtl',
                config: {
                    url: '/dtl?pMC_BYR_ACC_ID&pMC_BLK_FAB_REQ_H_ID&pMC_STYLE_H_EXT_ID',
                    views: {
                        "AddFabBkingH.Dtl": {
                            controller: 'AdditionalFabBkingDController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_AddFabBkingD'
                        }
                    },
                    Title: 'Requisition for Additional Fabric Booking',
                    reloadOnSearch: false
                }
            },
            {
                state: 'AddFabBkingList',
                config: {
                    url: '/addFabBkingList?pMC_BYR_ACC_ID&pMC_STYLE_H_EXT_ID',
                    views: {
                        "AddFabBkingList": {
                            controller: 'AdditionalFabBkingListController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_AddFabBkingList'
                        }
                    },
                    Title: 'Requisition for Additional Fabric Booking',
                    resolve: {
                        userLavelData: function (MrcDataService, $stateParams) {
                            return MrcDataService.getDataByFullUrl('/api/mrc/AddFabBk/GetAddFabBkUserLavel');
                        }
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'pmsAcc',
                config: {
                    url: '/pmsAcc/:pMC_BYR_ACC_ID',
                    controller: 'MrcPmsAccController',
                    controllerAs: 'vm',
                    templateUrl: '/Merchandising/Mrc/_pmsAcc',
                    Title: 'Buyer Accessories Dashboard',
                    resolve: {
                        BuyerAccData: function (MrcDataService, $stateParams) {
                            return MrcDataService.getDataByUrl('/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                                return [{ BYR_ACC_NAME_EN: 'All', IS_TAB_ACT: true, MC_BYR_ACC_ID: -1 }].concat(res.map(function (o) {
                                    o['IS_TAB_ACT'] = false;
                                    return o;
                                }));
                            });
                        }
                    }
                }
            },
            {
                state: 'pmsAcc.gridAcc',
                config: {
                    url: '/gridAcc?pFIRSTDATE&pLASTDATE&page&pageSize&pMONTHOF',
                    views: {
                        "TabInsideAcc": {
                            controller: 'MrcPmsAccDController',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_pmsAccD'
                        }
                    },
                    Title: 'Buyer Accessories Dashboard'
                }
            },
            {
                state: 'RateChartForInquiry',
                config: {
                    url: '/edit',
                    views: {
                        "RateChartForInquiry": {
                            controller: 'RateChartForInquiry',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_RateChartForInquiry'
                        }
                    },
                    Title: 'Rate Chart for Inquiry'
                }
            },

            {
                state: 'UploadStyleOrder',
                config: {
                    url: '/upldStylOrd',
                    views: {
                        "UploadStyleOrder": {
                            controller: 'UploadStyleOrder',
                            controllerAs: 'vm',
                            templateUrl: '/Merchandising/Mrc/_UploadStyleOrder'
                        }
                    },
                    Title: 'Upload Style and Order Details',
                    reloadOnSearch: false
                }
            },
        


      ];
    }
})();
