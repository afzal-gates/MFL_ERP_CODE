(function () {
    'use strict';
    angular.module('multitex.planning').controller('OrderItemListLnPlnController', ['$q', 'config', 'PlanningDataService', '$stateParams', '$state', '$scope', 'BuyerAccData', MrcPmsController]);
    function MrcPmsController($q, config, PlanningDataService, $stateParams, $state, $scope, BuyerAccData) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.state = $state.current.pOption == 3009 ? 'OrderItemListLnPln.list' : 'OrderItemListLnPlnItm.Itemlist';
        var V_MC_BYR_ACC_ID;
        vm.form = {
            FIRSTDATE: null,
            LASTDATE: null
        };

        vm.weeks = [];

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getOrderShipmentMonth()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        

        function getOrderShipmentMonth() {
            return PlanningDataService.getDataByFullUrl('/api/mrc/Order/OrderShipmentMonth?pMC_BYR_ACC_ID').then(function (res) {
                vm.shipMonthList = new kendo.data.DataSource({
                    data: res
                })
            });
        }
        function addDays(date, days) {
            var result = new Date(date);
            result.setDate(result.getDate() + days);
            return result;
        }

        vm.onChangeWeek = function (pMONTHOF, item) {
            $state.go(vm.state, { pMC_BYR_ACC_ID: V_MC_BYR_ACC_ID, pFIRSTDATE: item.FIRSTDATE, pLASTDATE: item.LASTDATE, pMONTHOF: pMONTHOF }, { reload: vm.state });
        };

        vm.onChangeShipMonth = function (data, e) {
            var item = e.sender.dataItem(e.sender.item);
            vm.weeks = [];

            if (item.hasOwnProperty('MONTHOF') && item.MONTHOF) {
                data['FIRSTDATE'] = item.FIRSTDATE;
                data['LASTDATE'] = item.LASTDATE;
                data['MONTHOF'] = item.MONTHOF;

                vm.weeks.push({
                    FIRSTDATE: item.FIRSTDATE,
                    LASTDATE: item.LASTDATE,
                    MONTHOF : 'All Week'
                });
                var strt = moment(item.FIRSTDATE);

                var j = 0;
                var k = 1;

                for (var i = 0; i < 30; i += 6) {
                    vm.weeks.push({
                        FIRSTDATE: addDays(item.FIRSTDATE,i+j+1).toISOString(),
                        LASTDATE: addDays(item.FIRSTDATE, i + 6+1).toISOString(),
                        MONTHOF: 'Week '+k
                    });
                    j = 1;
                    k += 1;
                }
                vm.form['week'] = vm.weeks[0];
                $state.go(vm.state, { pMC_BYR_ACC_ID: V_MC_BYR_ACC_ID, pFIRSTDATE: item.FIRSTDATE, pLASTDATE: item.LASTDATE, pMONTHOF: item.MONTHOF }, { reload: vm.state });
            } else {

                data['FIRSTDATE'] = null;
                data['LASTDATE'] = null;
                data['MONTHOF'] = null;

                $state.go(vm.state, { pMC_BYR_ACC_ID: V_MC_BYR_ACC_ID, pFIRSTDATE: null, pLASTDATE: null, pMONTHOF: null }, { reload: vm.state });
            }
        };



        if ($stateParams.pMC_BYR_ACC_ID) {

            V_MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
            vm.tabList = BuyerAccData.map(function (o) {
                if (o.MC_BYR_ACC_ID == parseInt($stateParams.pMC_BYR_ACC_ID)) {
                    o['IS_TAB_ACT'] = true;
                } else {
                    o['IS_TAB_ACT'] = false;
                }
                return o;
            });

        }

        vm.onSelect = function (tab) {
            V_MC_BYR_ACC_ID = tab.MC_BYR_ACC_ID;
            $state.go(vm.state, { pMC_BYR_ACC_ID: tab.MC_BYR_ACC_ID, pFIRSTDATE: vm.form.FIRSTDATE, pLASTDATE: vm.form.LASTDATE, pMONTHOF: vm.form.MONTHOF }, { reload: vm.state });
        }


    }

})();


(function () {
    'use strict';
    angular.module('multitex.planning').controller('OrderItemListLnPlnDController', ['$q', 'config', 'PlanningDataService', '$stateParams', '$state', '$scope', '$modal', '$window', OrderItemListLnPlnDController]);
    function OrderItemListLnPlnDController($q, config, PlanningDataService, $stateParams, $state, $scope, $modal, $window) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.state = $state.current.pOption == 3009 ? 'OrderItemListLnPln.list' : 'OrderItemListLnPlnItm.Itemlist';

        vm.form = {
            LK_ORD_TYPE_ID: ''
        };

        var page = 1;
        var pageSize = 15;


        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetStyleDropDownData(null)];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });

        }

        vm.params = $stateParams;

        console.log($state.current.pOption);

        vm.gmtCategoryGroupDs = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    PlanningDataService.getDataByFullUrl('/api/mrc/StyleItem/InvItemByParent/8').then(function (res) {
                        e.success(res);
                    });
                }
            }
        });

        vm.basicFancyPoloDs = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    PlanningDataService.getDataByFullUrl('/api/common/LookupListData/37').then(function (res) {
                        e.success(res);
                    });
                }
            }
        });

        vm.onChangeItemGroup = function (e) {
            var item = e.sender.dataItem(e.sender.item);

            vm.form['INV_ITEM_CAT_ID'] = '';
            vm.form['LK_ORD_TYPE_ID'] = '';

            GetStyleDropDownData((item.INV_ITEM_CAT_ID || ''));


            vm.gmtCategoryDs = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByFullUrl('/api/inv/ItemCategory/ItemCategList4LastLevel?pPARENT_ID=' + (item.INV_ITEM_CAT_ID || 8)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        };

        vm.onChangeItemCategory = function (e) {
            var itm = e.sender.dataItem(e.sender.item);
            vm.form['LK_ORD_TYPE_ID'] = '';

            GetStyleDropDownData((vm.form.INV_ITEM_CAT_ID_P||''), (itm.INV_ITEM_CAT_ID || ''));
        };

        vm.onChangeOrderType = function (pLK_ORD_TYPE_ID) {
            GetStyleDropDownData((vm.form.INV_ITEM_CAT_ID_P || ''), (vm.form.INV_ITEM_CAT_ID || ''),pLK_ORD_TYPE_ID);
        };

        vm.gmtCategoryDs = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    PlanningDataService.getDataByFullUrl('/api/inv/ItemCategory/ItemCategList4LastLevel?pPARENT_ID=8').then(function (res) {
                        e.success(res);
                    });
                }
            }
        });



        vm.onAddOtherOrder = function (data) {
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
                vm.gridOptionsDS.read();
            });

        }

        vm.oderListDs = {
            transport: {
                read: function (e) {

                    var url = "/api/common/getOrderStyleDropDownDataForPln?pMC_BYR_ACC_ID=" + ($stateParams.pMC_BYR_ACC_ID < 1 ? '' : $stateParams.pMC_BYR_ACC_ID);
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);



                    url += '&pINV_ITEM_CAT_ID_P=' + (vm.pINV_ITEM_CAT_ID_P || null);
                    url += '&pINV_ITEM_CAT_ID=' + (vm.pINV_ITEM_CAT_ID || null);
                    url += '&pLK_ORD_TYPE_ID=' + (vm.pLK_ORD_TYPE_ID || null);

                    if ($stateParams.pFIRSTDATE && $stateParams.pLASTDATE) {
                        url += '&pFIRSTDATE=' + $stateParams.pFIRSTDATE;
                        url += '&pLASTDATE=' + $stateParams.pLASTDATE;
                    }


                    if (params.filter) {
                        url += '&pORDER_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                        return PlanningDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    } else {
                        e.success([]);
                    }
                }
            },
            serverFiltering: true
        };

        function GetStyleDropDownData(pINV_ITEM_CAT_ID_P, pINV_ITEM_CAT_ID, pLK_ORD_TYPE_ID ) {
            vm.pINV_ITEM_CAT_ID_P = pINV_ITEM_CAT_ID_P;
            vm.pINV_ITEM_CAT_ID = pINV_ITEM_CAT_ID;
            vm.pLK_ORD_TYPE_ID = pLK_ORD_TYPE_ID;
        };

        vm.onChangeCat = function (data) {
            GetStyleDropDownData(data);
        };

        $scope.template = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO #</h5><p> Style: #: data.STYLE_NO #</p></span>';
        $scope.valueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO||"" #)</h5></span>';

        vm.gridOptionsDS = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            serverSorting: false,
            schema: {
                data: 'data',
                total: 'total',
                model: {
                    fields: {
                        ORD_CONF_DT: { type: 'date' },
                        SHIP_DT: { type: 'date' }
                    }
                }
            },
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);

                    var url = '/api/mrc/Pms/BuyerStyleOrderFollowupListForPln';
                    url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += '&pMC_BYR_ACC_ID=' + ($stateParams.pMC_BYR_ACC_ID < 1 ? null : $stateParams.pMC_BYR_ACC_ID);
                    url += '&pOption=' + $state.current.pOption;
                    page = params.page;
                    pageSize = params.pageSize;
                    if ($stateParams.pFIRSTDATE && $stateParams.pLASTDATE) {
                        url += '&pFIRSTDATE=' + $stateParams.pFIRSTDATE;
                        url += '&pLASTDATE=' + $stateParams.pLASTDATE;
                    }
                    if ($stateParams.pMC_BYR_ACC_ID > 0 || ($stateParams.pFIRSTDATE && $stateParams.pLASTDATE)) {
                        return PlanningDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    } else {
                        e.success([]);
                    }
                }
            },
            pageSize: 30
        });




        vm.onSearch = function (form) {

            vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                serverSorting: false,
                schema: {
                    data: 'data',
                    total: 'total',
                    model: {
                        fields: {
                            ORD_CONF_DT: { type: 'date' },
                            SHIP_DT: { type: 'date' }
                        }
                    }
                },
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Pms/BuyerStyleOrderFollowupListForPln';

                        url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                        url += '&pMC_BYR_ACC_ID=' + ($stateParams.pMC_BYR_ACC_ID < 1 ? null : $stateParams.pMC_BYR_ACC_ID);

                        url += '&pINV_ITEM_CAT_ID_P=' + (form.INV_ITEM_CAT_ID_P||'');
                        url += '&pINV_ITEM_CAT_ID=' + (form.INV_ITEM_CAT_ID||'');
                        url += '&pLK_ORD_TYPE_ID=' + (form.LK_ORD_TYPE_ID||'');

                        url += '&pMC_ORDER_H_ID_LST=' + (form && form.MC_ORDER_SHIP_LST ? form.MC_ORDER_SHIP_LST.join(',') : '');
                        url += '&pOption=' + $state.current.pOption;

                        page = params.page;
                        pageSize = params.pageSize;
                        if ($stateParams.pFIRSTDATE && $stateParams.pLASTDATE) {
                            url += '&pFIRSTDATE=' + $stateParams.pFIRSTDATE;
                            url += '&pLASTDATE=' + $stateParams.pLASTDATE;
                        }
                        url += '&pLK_GARM_TYPE_ID=' + (form.LK_GARM_TYPE_ID||'');
                        return PlanningDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 30
            });
        };

        vm.openManualLineLoadingModal = function (data) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Planning/Pln/_ManualLineLoadingModal',
                controller: 'ManualLineLoadingModalController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    data: function () {
                        return data;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                vm.gridOptionsDS.read();
                //$state.go($state.current, $stateParams, { reload: vm.state, inherit: true });
            }, function () {
                vm.gridOptionsDS.read();
            });
        }

        vm.zoomImage = function (image) {
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

        vm.colsHItem= [
                    { title: "", type: "string", width: "40px", template: '# if(data.IS_IE_UPD<1) {#<h6 style=\"padding-bottom: 0px;padding-top: 0px;\" class=\"badge badge-danger\">...<h6># }# ' },
                   
                    { field: "STYLE_NO", title: "Style", width: "100px" },
                    { field: "ORDER_NO", title: "Order", width: "150px", template: '#= ORDER_NO#  # if(data.IS_PROV ==\"Y\") {#<h6 style=\"padding-bottom: 0px;padding-top: 0px;\" class=\"badge badge-danger\">Projected<h6># }# ' },
                    { title: 'Style Desc.', width: "150px", template: '# for (var i = 0; i < data.images.length; i++) { # <img data-ng-src="data:image/png;base64,#:data.images[i].STYL_KEY_IMG#" title="Click for Image Enlarge" ng-click="vm.zoomImage(dataItem.images[#=i#].STYL_KEY_IMG)" alt="No Photo" style="border:1px solid black;width:45px; height:45px" /> # } # <span style=\"display:block;font-style:italic;\"><small><b> #=STYLE_DESC# </small></b></span>' },
                    { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer", width: "90px", filterable: false },
                    { field: "FIB_COMP_CODE", title: "Fabrication", width: "180px", filterable: false, template: '<small title=\"#= FIB_COMP_CODE_FULL#\"><b>  #= FIB_COMP_CODE# </small></b>' },

                    { field: "TOT_ORD_QTY", title: "Order Qty", width: "60px", filterable: false },

                    { field: "ORD_CONF_DT", title: "Confirm Date", type: "date", template: "#= kendo.toString(kendo.parseDate(ORD_CONF_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "100px" },
                    { field: "SHIP_DT", title: "Ship Date", type: "date", template: " #= kendo.toString(kendo.parseDate(SHIP_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') # ", width: "100px", filterable: false },

                    { field: "LEAD_TIME", title: "Lead Time", width: "60px", filterable: false },
                    { field: "PRINT_EMB_AOP_YD", title: "YD/Prt/Emb/AOP/Wash", width: "100px", filterable: false, template: '<h6><b> # if (IS_YD == \"Y\" ) { # <span class=\"label label-primary\">Y/D</span> # } #  # if (IS_AOP == \"Y\" ) { # <span class=\"label label-info\">AOP</span> # } #   # if (IS_EMB == \"Y\" ) { # <span class=\"label label-success\">Embr</span> # } #  # if (IS_PRINT == \"Y\" ) { # <span class=\"label label-danger\">Print</span> # } #  # if (IS_GMT_WASH == \"Y\" ) { # <span class=\"label label-warning\">Gmt. Wash</span> # } # </b></h6>' }
          ];


        vm.colsHPln = [
                { title: "", type: "string", width: "40px", template: '# if(data.TODO_CNT>0) {#<h6 style=\"padding-bottom: 0px;padding-top: 0px;\" class=\"badge badge-danger\">...<h6># }# ' },
                { field: "STYLE_NO", title: "Style", width: "100px" },
                 { field: "ORDER_NO", title: "Order", width: "150px", template: '#= ORDER_NO#  # if(data.IS_PROV ==\"Y\") {#<h6 style=\"padding-bottom: 0px;padding-top: 0px;\" class=\"badge badge-danger\">Projected<h6># }# ' },
                { title: 'Style Desc.', width: "150px", template: '# for (var i = 0; i < data.images.length; i++) { # <img data-ng-src="data:image/png;base64,#:data.images[i].STYL_KEY_IMG#" title="Click for Image Enlarge" ng-click="vm.zoomImage(dataItem.images[#=i#].STYL_KEY_IMG)" alt="No Photo" style="border:1px solid black;width:45px; height:45px" /> # } # <span style=\"display:block;font-style:italic;\"><small><b> #=STYLE_DESC# </small></b></span>' },
                { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer", width: "90px", filterable: false },
                { field: "FIB_COMP_CODE", title: "Fabrication", width: "180px", filterable: false, template: '<small title=\"#= FIB_COMP_CODE_FULL#\"><b>  #= FIB_COMP_CODE# </small></b>' },

                { field: "TOT_ORD_QTY", title: "Order Qty", width: "60px", filterable: false },

                { field: "ORD_CONF_DT", title: "Confirm Date", type: "date", template: "#= kendo.toString(kendo.parseDate(ORD_CONF_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "100px" },
                { field: "SHIP_DT", title: "Ship Date", type: "date", template: " #= kendo.toString(kendo.parseDate(SHIP_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') # ", width: "100px", filterable: false },

                { field: "LEAD_TIME", title: "Lead Time", width: "60px", filterable: false },
                { field: "PRINT_EMB_AOP_YD", title: "YD/Prt/Emb/AOP/Wash", width: "100px", filterable: false, template: '<h6><b> # if (IS_YD == \"Y\" ) { # <span class=\"label label-primary\">Y/D</span> # } #  # if (IS_AOP == \"Y\" ) { # <span class=\"label label-info\">AOP</span> # } #   # if (IS_EMB == \"Y\" ) { # <span class=\"label label-success\">Embr</span> # } #  # if (IS_PRINT == \"Y\" ) { # <span class=\"label label-danger\">Print</span> # } #  # if (IS_GMT_WASH == \"Y\" ) { # <span class=\"label label-warning\">Gmt. Wash</span> # } # </b></h6>' },

                {
                    title: "Action",
                    template: function () {
                        return "<a ng-click='vm.openManualLineLoadingModal(dataItem)' ng-disabled='dataItem.IS_IE_UPD < 1'  class='btn btn-xs blue'><i class='fa fa-upload'> Line Load</i></a><i ng-if='dataItem.IS_IE_UPD < 1' style='\"cursor\":\"pointer;\"' title='{{dataItem.IS_IE_UPD < 1? \"MP, M/C,Criticality & Fabric Class data Missing\":\"\"}}' class='fa  fa-question-circle font-red'></i>";
                    },
                    width: "100px"
                }
        ];

        vm.colsDPln = [
                    { field: "ITEM_CAT_NAME_EN", title: "Category", type: "string", width: "100px" },
                    { field: "ITEM_NAME_EN", title: "Item Desc", type: "string", width: "200px", template: '<small><b>#:ORDER_NO# </b></small><br><small>Confirm:<b>#:ORD_CONF_DT# </b>  Ship:<b><span style="color: red;"> #:SHIP_DT# </span></b></small><br>  <img data-ng-src="data:image/png;base64,#:STYL_KEY_IMG#" title="Click for Image Enlarge" ng-click="vm.zoomImage(dataItem.STYL_KEY_IMG)" alt="No Photo" style="border:1px solid black;width:45px; height:45px" /> <span style=\"font-style:italic;\"><small><b> #=ITEM_NAME_EN# </small></b></span>' },
                    { field: "PRODUCT_TYP_NAME_EN", title: "Criticality", type: "string", width: "100px" },
                    { field: "FAB_CLASS_NAME_EN", title: "Fab. Class", type: "string", width: "100px" },
                    { field: "NO_OF_OPR", title: "OP", type: "number", width: "50px" },
                    { field: "NO_OF_HLPR", title: "ASO", type: "number", width: "50px" },
                    { field: "SMV", title: "SMV", type: "number", width: "50px" },

                    {
                        field: "PLAN_EFICNCY", title: "Trgt Effi.", type: "number", width: "100px",
                    },
                    {
                        field: "PRINT_EMB_AOP_YD", title: "Additional Process", type: "string", width: "200px",
                        template: '<h6><b> # if (IS_YD == \"Y\" ) { # <span class=\"label label-primary\">Y/D</span> # } #  # if (IS_AOP == \"Y\" ) { # <span class=\"label label-info\">AOP</span> # } #   # if (IS_EMB == \"Y\" ) { # <span class=\"label label-success\">Embr</span> # } #  # if (IS_PRINT == \"Y\" ) { # <span class=\"label label-danger\">Print</span> # } #  # if (IS_GMT_WASH == \"Y\" ) { # <span class=\"label label-warning\">Gmt. Wash</span> # } # </b></h6>'
                    },
                    { field: "ORDER_QTY", title: "Order Qty", type: "number", width: "100px" },
                    { title: "Loaded", type: "number", width: "100px", template: "<div style='width: 100%' kendo-Progress-Bar k-min='0' k-max='(dataItem.ALLOCATED_QTY < dataItem.ORDER_QTY)? dataItem.ORDER_QTY : dataItem.ALLOCATED_QTY' k-value='dataItem.ALLOCATED_QTY' > </div> <br> <a class='btn btn-link' ng-click='vm.onAddOtherOrder(dataItem)'> +Merge Order </a> " },
                    { title: "Left2Load", type: "number", width: "100px", template: '{{(dataItem.ALLOCATED_QTY - dataItem.ORDER_QTY)>0 ? \"+\": \"\"}} {{dataItem.ALLOCATED_QTY - dataItem.ORDER_QTY}} ( {{(dataItem.ALLOCATED_QTY/dataItem.ORDER_QTY)*100|number:1}}%)' }
        ];

        vm.colsDItem = [
            { field: "ITEM_CAT_NAME_EN", title: "Category", type: "string", width: "100px" },
            { field: "ITEM_NAME_EN", title: "Item Desc", type: "string", width: "200px", template: '<small><b>#:ORDER_NO# </b></small><br><small>Confirm:<b>#:ORD_CONF_DT#  </b> Ship: <b><span style="color: red;"> #:SHIP_DT# </span></b></small><br><img data-ng-src="data:image/png;base64,#:STYL_KEY_IMG#" title="Click for Image Enlarge" ng-click="vm.zoomImage(dataItem.STYL_KEY_IMG)" alt="No Photo" style="border:1px solid black;width:45px; height:45px" /> <span style=\"font-style:italic;\"><small><b> #=ITEM_NAME_EN# </small></b></span>' },
            { field: "PRODUCT_TYP_NAME_EN", title: "Criticality", type: "string", width: "100px" },
            { field: "FAB_CLASS_NAME_EN", title: "Fab. Class", type: "string", width: "100px" },
            { field: "NO_OF_OPR", title: "OP", type: "number", width: "50px" },
            { field: "NO_OF_HLPR", title: "ASO", type: "number", width: "50px" },
            { field: "SMV", title: "SMV", type: "number", width: "50px" },

            {
                field: "PLAN_EFICNCY", title: "Trgt Effi.", type: "number", width: "100px",
            },
            {
                field: "PRINT_EMB_AOP_YD", title: "Additional Process", type: "string", width: "200px",
                template: '<h6><b> # if (IS_YD == \"Y\" ) { # <span class=\"label label-primary\">Y/D</span> # } #  # if (IS_AOP == \"Y\" ) { # <span class=\"label label-info\">AOP</span> # } #   # if (IS_EMB == \"Y\" ) { # <span class=\"label label-success\">Embr</span> # } #  # if (IS_PRINT == \"Y\" ) { # <span class=\"label label-danger\">Print</span> # } #  # if (IS_GMT_WASH == \"Y\" ) { # <span class=\"label label-warning\">Gmt. Wash</span> # } # </b></h6>'
            },
            { field: "ORDER_QTY", title: "Order Qty", type: "number", width: "100px" },
            {
                title: "Action",
                template: function () {
                    return "<a ng-click='vm.onClickForItemSpec(dataItem)'  class='btn btn-xs blue'>Item Spec</a>";
                },
                width: "100px"
            }
        ];


        vm.gridOptions = {
            toolbar: kendo.template($("#PmsToolbarTemplate").html()),
            autoBind: true,
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
            resizable: true,
            sortable: false,
            pageable: true,

            height: 600,
            columns: $state.current.pOption == 3009 ? vm.colsHPln : vm.colsHItem
        };

        vm.planerDashboardD = function (item) {
            console.log(item);

            return {
                dataSource: {
                    transport: {
                        read: function (e) {
                            PlanningDataService.getDataByFullUrl('/api/mrc/StyleDItem/GmtItemListForPln?pMC_STYLE_H_ID=' + item.MC_STYLE_H_ID + '&pMC_ORDER_ITEM_PLN_LST=' + item.MC_ORDER_ITEM_PLN_LST).then(function (res) {
                                e.success(res.map(function (ob) {
                                    ob['MC_BYR_ACC_ID'] = item.MC_BYR_ACC_ID;
                                    ob['MC_STYLE_H_ID'] = item.MC_STYLE_H_ID;
                                    return ob;
                                }));
                            })
                        }
                    }
                },
                scrollable: true,
                columns: $state.current.pOption == 3009 ? vm.colsDPln : vm.colsDItem
            };


        };

        function checkPopUpClosed(win) {
            var timer = setInterval(function () {
                if (win.closed) {
                    clearInterval(timer);
                    $state.go($state.current, $stateParams, { reload: vm.state, inherit: true });
                }
            }, 1000);
        };

        vm.onClickForItemSpec = function (data) {
            console.log(data.parent());
            var url = '/IE/IE/GmtStylItmList?a=316/#/itmOprSpec/itmDtl?pMC_STYLE_D_ITEM_ID=' + data.MC_STYLE_D_ITEM_ID + '&pINV_ITEM_CAT_ID=' + data.INV_ITEM_CAT_ID + '&pMC_BYR_ACC_ID=' + data.MC_BYR_ACC_ID + '&pMC_STYLE_H_ID=' + data.MC_STYLE_H_ID;
            var opt = 'location=yes,height=570,width=' + ($window.outerWidth - 300) + ',scrollbars=yes,status=yes,modal=yes';
            var win = $window.open(url, "newwindow", opt);
            win.focus();
            checkPopUpClosed(win);
        };



    }

})();