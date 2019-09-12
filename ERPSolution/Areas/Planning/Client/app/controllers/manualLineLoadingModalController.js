(function () {
    'use strict';
    angular.module('multitex.planning').controller('ManualLineLoadingModalController', ['$modalInstance', '$scope', '$q', 'config', '$filter', 'PlanningDataService', 'Dialog', 'data', '$modal', ManualLineLoadingModalController]);

    function ManualLineLoadingModalController($modalInstance, $scope, $q, config, $filter, PlanningDataService, Dialog, data, $modal) {


        activate();
        $scope.showSplash = true;
        $scope.line = [];
        function activate() {
            var promise = [loadResources(), getGmtLnLoadPhaseData(), getItemsData()];
            return $q.all(promise).then(function () {
                $scope.showSplash = false;
            });
        }
        $scope.dtFormat = 'dd-MMM-yy h:00a';

        $scope.datePickerOptions = {
            parseFormats: ["yyyy-MM-ddTHH:00:00"]
        };

        $scope.resourceDs = [];


        $scope.GridDateOpened1 = [];
        $scope.GridDateOpened2 = [];

        $scope.GridDateOpen1 = function ($event, index) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.GridDateOpened1[index] = true;
        };
        $scope.GridDateOpen2 = function ($event, index) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.GridDateOpened2[index] = true;
        };

        $scope.onSelectIPhase = function (e) {
            $(".k-state-disabled").parent().click(false);
        };


        $scope.onAddOtherOrder = function (data) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Planning/Pln/_GmtOrderMergingModal',
                controller: 'GmtOrderMergingModalController',
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    data: function () {
                        return data;
                    }
                }
            });
            modalInstance.result.then(function (dta) {
                getItemsData();
            });

        }
 




        $scope.zoomImage = function (image) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'StyleItemImageStyleG.html',
                controller: function ($scope, $modalInstance) {
                    $scope.image = image;
                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }

                },
                size: 'md',
                windowClass: 'large-Modal'
            });
        }

        $scope.templatePhase = '<span class="#: (GMT_PLN_STRIPE_PHASE_ID==3||GMT_PLN_STRIPE_PHASE_ID==5) ? \"k-state-disabled\": \"\"#">#=data.PLN_STRIPE_PHASE_NAME#</span>';


        $scope.template = '<span class="#: (data.IS_PERMITTED===\"N\") ? \"k-state-disabled\": \"badge badge-default\"#"><h5 style="padding:0px;margin:0px;color:#=data.LINE_LBL_COLOR#;"><b>#: data.name # [#:data.TTL_ACTV_POINT#] </b></h5</span>';
        //$scope.template = '<span><h6 style="padding:0px;margin:0px;color:#=data.LINE_LBL_COLOR#;">#: data.name #</h6><h6><p> #: data.LINE_CRITICALITY#</p></h6></span>';
        $scope.valueTemplate = '<span><h5 style="padding:0px;margin:0px;color:#=data.LINE_LBL_COLOR#;">#: data.name # [#:data.TTL_ACTV_POINT#]</h5></span>';
        function loadResources() {
            return PlanningDataService.getDataByUrl('/GmtLineLoad/getResourceDataDD').then(function (res) {
               
                for (var j = 0; j <= 10; j++) {
                    var list = [];
                    for (var i = 0; i <= 10; i++) {
                        list.push(new kendo.data.DataSource({ data: res }));
                    }
                    $scope.resourceDs.push(list);
                }
                $scope.line = res;
            });
        }

        $scope.onChangeLine = function (itm, e, data) {
            var itmm = e.sender.dataItem(e.sender.item);
            if (itmm.HR_PROD_LINE_ID) {
                itm['STATUS_CHANGE'] = 'Y';
                itm['TTL_ACTV_POINT'] = itmm.TTL_ACTV_POINT;
                itm['IS_ACTV_POINT'] = itmm.IS_ACTV_POINT;
                itm['HR_PROD_FLR_ID'] = itmm.HR_PROD_FLR_ID;


                if (itmm.IS_ACTV_POINT == 'N') {
                    $scope.showSplash = true;
                    PlanningDataService.getDataByUrl('/GmtLineLoad/FindSewingStartDateByLine?pHR_PROD_LINE_ID=' + itmm.HR_PROD_LINE_ID)
                    .then(function (SEW_START_DT) {
                        itm['SEW_START_DT'] = SEW_START_DT;
                        findSewingEnd(data, itm);
                    });
                }
            }
        };

        function getGmtLnLoadPhaseData() {
            return PlanningDataService.getDataByUrl('/GmtLineLoad/getGmtLnLoadPhaseData').then(function (res) {
                console.log(res);
                $scope.pahseDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }



        $scope.addNew = function (item, idx) {
            var itm = angular.copy(item.ln_loads_items[idx]);
            itm.GMT_PLN_LINE_LOAD_ID = -1;
            itm.HR_PROD_LINE_ID = '';
            itm.GMT_PLN_STRIPE_PHASE_ID = 1;
            itm.HR_PROD_LINE_LST = '';
            itm.TOT_PROD = 0
            itm.STATUS_CHANGE = 'Y',
            itm.sts = [];
            itm.ALLOCATED_QTY = (item.ORDER_QTY - item.ln_loads_items.reduce(function (acc, val) {
                return acc + (val.ALLOCATED_QTY || 0);
            }, 0));

            item.ln_loads_items.push(itm);
        };


       

        function getItemsData() {
            return PlanningDataService.getDataByFullUrl('/api/mrc/StyleDItem/GmtItemListForPln?pMC_STYLE_H_ID=' + data.MC_STYLE_H_ID + '&pMC_ORDER_ITEM_PLN_LST=' + (data.MC_ORDER_ITEM_PLN_LST || ''))
               .then(function (res) {
                   $scope.items = res.filter(function (ob) { return ob.SMV > 0; });
               });
        };
        $scope.data = data;
        $scope.save = function (items, isValid) {

            if (!isValid) { return false;}

            var adata = [];

            angular.forEach(items, function (val, key) {
                if (val.IS_INCLUDE_4PLN === 'Y') {
                    adata.push({
                        MC_ORDER_ITEM_PLN_ID: val.MC_ORDER_ITEM_PLN_ID,
                        SMV: val.SMV,
                        RF_FAB_CLASS_ID: val.RF_FAB_CLASS_ID,
                        GMT_PRODUCT_TYP_ID : val.GMT_PRODUCT_TYP_ID,
                        ITEMS: config.xmlStringShortNoTag(val.ln_loads_items.filter(function (r) { return (r.HR_PROD_LINE_ID || 0) > 0;}).map(function (o) {
                            return {
                                GMT_PLN_LINE_LOAD_ID: o.GMT_PLN_LINE_LOAD_ID,
                                HR_PROD_LINE_ID: o.HR_PROD_LINE_ID,
                                SEW_START_DT: ((o.SEW_START_DT instanceof Date) ? o.SEW_START_DT.toISOString().substring(0,o.SEW_START_DT.toISOString().length-5) : o.SEW_START_DT),
                                SEW_END_DT: ((o.SEW_END_DT instanceof Date) ? o.SEW_END_DT.toISOString().substring(0, o.SEW_END_DT.toISOString().length - 5) : o.SEW_END_DT),
                                ALLOCATED_QTY: o.ALLOCATED_QTY,
                                PLAN_HP: o.PLAN_HP,
                                PLAN_OP: o.PLAN_OP,
                                PLAN_MP: (o.PLAN_OP + o.PLAN_HP),
                                PLAN_WH: o.PLAN_WH,
                                PARENT_ID:  (o.IS_ACTV_POINT == 'Y' ? o.PARENT_ID: ''),
                                HR_PROD_LINE_LST: (o.PLAN_MP > o.TTL_ACTV_POINT && o.IS_ACTV_POINT == 'N') ? _.filter(o.HR_PROD_LINE_LST, function (o) { return o > 1; }).join(',') : '',
                                IS_LRN_CRV_APP: o.IS_LRN_CRV_APP,
                                PLAN_EFFICIENCY: (o.PLAN_EFFICIENCY || val.PLAN_EFICNCY),
                                GMT_PLN_STRIPE_PHASE_ID: o.GMT_PLN_STRIPE_PHASE_ID,
                                STATUS_CHANGE: ( o.STATUS_CHANGE || 'N')
                            }

                          })
                        )

                    });
                }

            });

          
            if (adata.length > 0 && adata.some(function (xx) { return xx.ITEMS.length !== 0; })) {
                var XML = config.xmlStringShort(adata);
                return PlanningDataService.saveDataByUrl({ XML: XML, pOption: (data.MC_ORDER_ITEM_PLN_ID ? 1001 : 1000) }, '/GmtLineLoad/SaveManualLoading').then(function (res) {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $scope.showSplash = false;
                        getItemsData();
                    }
                    config.appToastMsg(res.data.PMSG);

                }, function (err) {
                    console.log(err);
                })
            }

        };

        $scope.Reset = function () {
            Dialog.confirm('Reseting........', 'Are you sure?', ['Yes', 'No'])
             .then(function () {
                 getItemsData();
             });
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        $scope.onPlnDateChanhe = function (e, item) {
            console.log(e);
            item['STATUS_CHANGE'] = 'Y';

        }

        $scope.allocatedQtyChange = function (item) {
            item['STATUS_CHANGE'] = 'Y';
            item['ALLOCATED_QTY'] = _.sumBy(item.ln_loads_items, function (o) { return (o.ALLOCATED_QTY || 0); });
            item['PCT_COMPLETION'] = (item.ALLOCATED_QTY / item.ORDER_QTY) * 100;
        };

        function findSewingEnd(data, item) {
            if (item.IS_ACTV_POINT == 'N' && item.SEW_START_DT) {
                var url = '/GmtLineLoad/getForcastLineLoadData?pHR_PROD_LINE_ID=' + item.HR_PROD_LINE_ID +
                    '&pALLOCATED_QTY=' + item.ALLOCATED_QTY + '&pSEW_START_DT='
                    + ((item.SEW_START_DT instanceof Date) ? item.SEW_START_DT.toISOString() : item.SEW_START_DT)
                    + '&pPLAN_MP=' + (item.PLAN_MP < 15 ? 15 : item.PLAN_MP) + '&pPLAN_WH='
                    + (item.PLAN_WH < 8 ? 8 : item.PLAN_WH) + '&pIS_LRN_CRV_APP=' + item.IS_LRN_CRV_APP
                    + '&pPLAN_EFFICIENCY=' + (item.PLAN_EFFICIENCY || 15) + '&pMC_ORDER_ITEM_PLN_ID='
                    + data.MC_ORDER_ITEM_PLN_ID + '&pGMT_PRODUCT_TYP_ID=' + data.GMT_PRODUCT_TYP_ID
                    + '&pRF_FAB_CLASS_ID=' + data.RF_FAB_CLASS_ID
                    + '&pSMV=' + (data.SMV < 3 ? 3 : data.SMV)
                    + '&pGMT_PLN_LINE_LOAD_ID=' + (item.GMT_PLN_LINE_LOAD_ID || '');
                $scope.showSplash = true;
                return PlanningDataService.getDataByUrl(url).then(function (res) {
                    item['SEW_END_DT'] = res.SEW_END_DT;
                    $scope.showSplash = false;
                });
            };
        };

        function findSewingStart(data, item) {
            if (item.IS_ACTV_POINT == 'N' && item.SEW_END_DT) {
                var url = '/GmtLineLoad/getForcastLineLoadData?pHR_PROD_LINE_ID=' + item.HR_PROD_LINE_ID +
                    '&pALLOCATED_QTY=' + item.ALLOCATED_QTY + '&pSEW_END_DT='
                    + ((item.SEW_END_DT instanceof Date) ? item.SEW_END_DT.toISOString() : item.SEW_END_DT)
                    + '&pPLAN_MP=' + (item.PLAN_MP < 15 ? 15 : item.PLAN_MP) + '&pPLAN_WH='
                    + (item.PLAN_WH < 8 ? 8 : item.PLAN_WH) + '&pIS_LRN_CRV_APP=' + item.IS_LRN_CRV_APP
                    + '&pPLAN_EFFICIENCY=' + (item.PLAN_EFFICIENCY || 15) + '&pMC_ORDER_ITEM_PLN_ID='
                    + data.MC_ORDER_ITEM_PLN_ID + '&pGMT_PRODUCT_TYP_ID=' + data.GMT_PRODUCT_TYP_ID
                    + '&pRF_FAB_CLASS_ID=' + data.RF_FAB_CLASS_ID
                    + '&pSMV=' + (data.SMV < 3 ? 3 : data.SMV) 
                    + '&pGMT_PLN_LINE_LOAD_ID=' + (item.GMT_PLN_LINE_LOAD_ID || '');
                $scope.showSplash = true;
                return PlanningDataService.getDataByUrl(url).then(function (res) {
                    item['SEW_START_DT'] = res.SEW_START_DT;
                    $scope.showSplash = false;
                });
            };
        };



        $scope.findSewingEnd = function (data, item) {
            if (item.IS_ACTV_POINT == 'N') {
                findSewingEnd(data, item);
            };
        };

        $scope.findSewingStart = function (data, item) {
            if (item.IS_ACTV_POINT == 'N') {
                findSewingStart(data, item);
            };
        };


        $scope.findOrderQuantity = function (data, item) {
            if (item.IS_ACTV_POINT == 'N' && item.SEW_START_DT && item.SEW_END_DT) {
                var url = '/GmtLineLoad/getEstimatedAllocatedQty?pHR_PROD_LINE_ID=' + item.HR_PROD_LINE_ID +
                           '&pALLOCATED_QTY=' + item.ALLOCATED_QTY + '&pSEW_START_DT='
                           + ((item.SEW_START_DT instanceof Date) ? item.SEW_START_DT.toISOString() : item.SEW_START_DT)
                           + '&pSEW_END_DT=' + ((item.SEW_END_DT instanceof Date) ? item.SEW_END_DT.toISOString() : item.SEW_END_DT)
                           + '&pPLAN_MP=' + (item.PLAN_MP < 15 ? 15 : item.PLAN_MP) + '&pPLAN_WH='
                           + (item.PLAN_WH < 8 ? 8 : item.PLAN_WH) + '&pIS_LRN_CRV_APP=' + item.IS_LRN_CRV_APP
                           + '&pPLAN_EFFICIENCY=' + (item.PLAN_EFFICIENCY || 15) + '&pMC_ORDER_ITEM_PLN_ID='
                           + data.MC_ORDER_ITEM_PLN_ID + '&pGMT_PRODUCT_TYP_ID=' + data.GMT_PRODUCT_TYP_ID
                           + '&pRF_FAB_CLASS_ID=' + data.RF_FAB_CLASS_ID
                           + '&pSMV=' + (data.SMV < 3 ? 3 : data.SMV)
                           + '&pGMT_PLN_LINE_LOAD_ID=' + (item.GMT_PLN_LINE_LOAD_ID || '');


                    $scope.showSplash = true;
                    return PlanningDataService.getDataByUrl(url).then(function (res) {
                        item['ALLOCATED_QTY'] = res.ALLOCATED_QTY;
                        $scope.showSplash = false;
                    });
            }
        };


        $scope.onFindDataSource = function (itm) {
            return $scope.line.filter(function (o) {
                return o.HR_PROD_LINE_ID != parseInt(itm.HR_PROD_LINE_ID) && o.HR_PROD_FLR_ID == parseInt(itm.HR_PROD_FLR_ID);
            });
        };

        $scope.onFindParentData = function (items,itm) {
            return items.filter(function (o) {
                return o.IS_ACTV_POINT === 'N' && o.HR_PROD_FLR_ID == parseInt(itm.HR_PROD_FLR_ID);
            });
        };

        $scope.onChangeParentLine = function (datas,itm) {
            var dddd = datas.find(function (o) { return o.GMT_PLN_LINE_LOAD_ID == itm.PARENT_ID });
            if (dddd) {
                itm['SEW_START_DT'] = dddd.SEW_START_DT;
                itm['SEW_END_DT'] = dddd.SEW_END_DT;
                itm['STATUS_CHANGE'] = 'Y';
            }
        };

        $scope.findSuggestedLine = function (data, item, order) {
                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: '/Planning/Pln/_ManualLineLoadingLineSugModal',
                    controller: 'ManualLineLoadingLineSugModalController',
                    size: 'lg',
                    windowClass: 'app-modal-window',
                    resolve: {
                        data: function () {
                            return order;
                        },
                        lines: function (PlanningDataService) {
                            $scope.showSplash = true;
                            return PlanningDataService.getDataByUrl('/GmtLineLoad/findPlnStatusRulesWithLine?pRF_FAB_CLASS_ID=' + data.RF_FAB_CLASS_ID + '&pMC_STYLE_D_ITEM_ID=' + data.MC_STYLE_D_ITEM_ID + '&pGMT_PRODUCT_TYP_ID=' + data.GMT_PRODUCT_TYP_ID + '&pINV_ITEM_CAT_ID=' + data.INV_ITEM_CAT_ID + '&pPLAN_START_DT=' + (order.PLAN_START_DT || '') + '&pPLAN_END_DT=' + (order.PLAN_END_DT || '') + '&pSHIP_DT=' + (order.SHIP_DT || '') + '&pALLOCATED_QTY=' + item.ALLOCATED_QTY + '&pPLAN_MP=' + item.PLAN_MP + '&pPLAN_WH=' + item.PLAN_WH + '&pIS_LRN_CRV_APP=' + item.IS_LRN_CRV_APP + '&pPLAN_EFFICIENCY=' + (item.PLAN_EFFICIENCY || '') + '&pMC_ORDER_ITEM_PLN_ID=' + data.MC_ORDER_ITEM_PLN_ID + '&pSMV=' + data.SMV + '&pPLAN_OP=' + item.PLAN_OP)
                            .then(function (res) {
                                $scope.showSplash = false;
                                return res;
                            });
                        }
                    }
                });

                modalInstance.result.then(function (d) {
                    item['HR_PROD_LINE_ID'] = d.HR_PROD_LINE_ID;
                    item['SEW_START_DT'] = d.LOAD_START_DT;
                    item['SEW_END_DT'] = d.OQ_ONTYM_DT;
                    item['ALLOCATED_QTY'] = d.OQ_PCT_ONTYM;
                    
                }, function () {
                    console.log('Modal dismissed at: ' + new Date());
                });
        };

    }

})();
