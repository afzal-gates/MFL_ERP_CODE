(function () {
    'use strict';
    angular.module('multitex.ie').controller('GmtStylItmController', ['$q', 'config', 'IeDataService', '$stateParams', '$state', '$scope', 'BuyerAccData', GmtStylItmController]);
    function GmtStylItmController($q, config, IeDataService, $stateParams, $state, $scope, BuyerAccData) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        var V_MC_BYR_ACC_ID;
        vm.form = {
            FIRSTDATE: null,
            LASTDATE: null
        };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getOrderShipmentMonth()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function getOrderShipmentMonth() {
            return IeDataService.getDataByFullUrl('/api/mrc/Order/OrderShipmentMonth?pMC_BYR_ACC_ID').then(function (res) {
                vm.shipMonthList = new kendo.data.DataSource({
                    data: res
                })
            });
        }


        vm.onChangeShipMonth = function (data, e) {
            var item = e.sender.dataItem(e.sender.item);
            if (item.hasOwnProperty('MONTHOF') && item.MONTHOF) {
                data['FIRSTDATE'] = item.FIRSTDATE;
                data['LASTDATE'] = item.LASTDATE;
                data['MONTHOF'] = item.MONTHOF;

                $state.go('GmtStylItmList.grid', { pMC_BYR_ACC_ID: V_MC_BYR_ACC_ID, pFIRSTDATE: item.FIRSTDATE, pLASTDATE: item.LASTDATE, pMONTHOF: item.MONTHOF }, { reload: 'GmtStylItmList.grid' });
            } else {

                data['FIRSTDATE'] = null;
                data['LASTDATE'] = null;
                data['MONTHOF'] = null;

                $state.go('GmtStylItmList.grid', { pMC_BYR_ACC_ID: V_MC_BYR_ACC_ID, pFIRSTDATE: null, pLASTDATE: null, pMONTHOF: null }, { reload: 'GmtStylItmList.grid' });
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
            $state.go('GmtStylItmList.grid', { pMC_BYR_ACC_ID: tab.MC_BYR_ACC_ID, pFIRSTDATE: vm.form.FIRSTDATE, pLASTDATE: vm.form.LASTDATE, pMONTHOF: vm.form.MONTHOF }, { reload: 'GmtStylItmList.grid' });
        }


    }

})();




/////=====================
(function () {
    'use strict';
    angular.module('multitex.ie').controller('GmtStylItmDController', ['$q', 'config', 'IeDataService', '$stateParams', '$state', '$scope', '$modal', '$window', GmtStylItmDController]);
    function GmtStylItmDController($q, config, IeDataService, $stateParams, $state, $scope, $modal, $window) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.form = {
            RF_FAB_PROD_CAT_ID: 2
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



        function GetStyleDropDownData(RF_FAB_PROD_CAT_ID) {
            vm.oderListDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/common/getOrderStyleDropDownData?pMC_BYR_ACC_ID=" + ($stateParams.pMC_BYR_ACC_ID < 1 ? '' : $stateParams.pMC_BYR_ACC_ID);
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);

                        if (params.filter) {
                            url += '&pORDER_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pORDER_NO';
                        }

                        url += '&pOption=3003'
                        url += '&pRF_FAB_PROD_CAT_ID=' + RF_FAB_PROD_CAT_ID;

                        if ($stateParams.pFIRSTDATE && $stateParams.pLASTDATE) {
                            url += '&pFIRSTDATE=' + $stateParams.pFIRSTDATE;
                            url += '&pLASTDATE=' + $stateParams.pLASTDATE;
                        }
                        IeDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            };


        };

        vm.onChangeCat = function (data) {
            GetStyleDropDownData(data);
        };

        $scope.template = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO #</h5><p> Style: #: data.STYLE_NO #</p></span>';

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

                    var url = '/api/ie/ItmOprSpec/BuyerStyleOrderList';

                    url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += '&pMC_BYR_ACC_ID=' + ($stateParams.pMC_BYR_ACC_ID < 1 ? null : $stateParams.pMC_BYR_ACC_ID);
                    url += '&pRF_FAB_PROD_CAT_ID=2';

                    page = params.page;
                    pageSize = params.pageSize;
                    if ($stateParams.pFIRSTDATE && $stateParams.pLASTDATE) {
                        url += '&pFIRSTDATE=' + $stateParams.pFIRSTDATE;
                        url += '&pLASTDATE=' + $stateParams.pLASTDATE;
                    }

                    return IeDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            pageSize: 30
        });


        vm.printBookingReport = function (dataOri) {

            var data = {
                PAGE_SIZE_NAME: 'A4',
                MC_BLK_REVISION_NO: dataOri.LAST_REV_NO,
                MC_BLK_FAB_REQ_H_ID: dataOri.MC_BLK_FAB_REQ_H_ID,
                RES_TITLE:  'Order:'+dataOri.ORDER_NO+'-Booking Report'
            };

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = angular.extend({ REPORT_CODE: 'RPT-2001' }, data);

            for (var i in params) {
                if (params.hasOwnProperty(i)) {

                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    input.value = params[i];
                    form.appendChild(input);
                }
            }
            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
        };

        vm.printBudgetSheetReport = function (dataOri) {
            var data = {
                MC_STYL_BGT_H_ID: dataOri.MC_STYL_BGT_H_ID,
                REVISION_NO: (dataOri.REVISION_NO || 0),
                RES_TITLE: 'Order:' + dataOri.ORDER_NO + '-Budget Sheet'
            };

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = angular.extend({ REPORT_CODE: 'RPT-2003' }, data);

            for (var i in params) {
                if (params.hasOwnProperty(i)) {

                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    input.value = params[i];
                    form.appendChild(input);
                }
            }
            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
        };




        vm.onSearch = function (form) {
            console.log(form);
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

                        var url = '/api/mrc/Pms/BuyerStyleOrderFollowupList';

                        url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                        url += '&pMC_BYR_ACC_ID=' + ($stateParams.pMC_BYR_ACC_ID < 1 ? null : $stateParams.pMC_BYR_ACC_ID);

                        url += '&pRF_FAB_PROD_CAT_ID=' + (form.RF_FAB_PROD_CAT_ID ? form.RF_FAB_PROD_CAT_ID:'');

                        url += '&pMC_ORDER_H_ID_LST=' + (form && form.MC_ORDER_H_ID_LST ? form.MC_ORDER_H_ID_LST.join(',') : '');

                        page = params.page;
                        pageSize = params.pageSize;

                        //$state.go($state.current, { page: params.page, pageSize: params.pageSize }, { notify: false });

                        if ($stateParams.pFIRSTDATE && $stateParams.pLASTDATE) {
                            url += '&pFIRSTDATE=' + $stateParams.pFIRSTDATE;
                            url += '&pLASTDATE=' + $stateParams.pLASTDATE;
                        }

                        return IeDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 30
            });
        };

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
            columns: [
                { field: "STYLE_NO", title: "Style", width: "150px", locked: true, template: '# if(IS_STYL_ITM_OPR_MAP=="Y") { # <span class="label label-success"> #=STYLE_NO# </span> #}else{# <span class="label label-danger"> #=STYLE_NO# </span> #}#' },
                { field: "STYLE_NO", title: "Order", width: "150px", locked: true, template: '#=ORDER_NO#' },
                //{ title: 'Style Desc.', width: "200px", template: '# if (STYLE_ORD_SL == 1 ) { #   # for (var i = 0; i < data.images.length; i++) { # <img data-ng-src="data:image/png;base64,#:data.images[i].STYL_KEY_IMG#" title="Click for Image Enlarge" ng-click="vm.zoomImage(dataItem.images[#=i#].STYL_KEY_IMG)" alt="No Photo" style="border:1px solid black;width:45px; height:45px" /> # } # <span style=\"display:block;font-style:italic;\"><small><b> #=STYLE_DESC# </small></b></span>  # } #', locked: true },
                { title: 'Style Desc.', width: "200px", template: '# for (var i = 0; i < data.images.length; i++) { # <img data-ng-src="data:image/png;base64,#:data.images[i].STYL_KEY_IMG#" title="Click for Image Enlarge" ng-click="vm.zoomImage(dataItem.images[#=i#].STYL_KEY_IMG)" alt="No Photo" style="border:1px solid black;width:45px; height:45px" /> # } # <span style=\"display:block;font-style:italic;\"><small><b> #=STYLE_DESC# </small></b></span>', locked: true },
                { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer", width: "150px", locked: true, filterable: false, template: '#=BYR_ACC_GRP_NAME_EN#' },
                { field: "FIB_COMP_CODE", title: "Fabrication", width: "250px", filterable: false, template: '<small title=\"#= FIB_COMP_CODE_FULL#\"><b>  #= FIB_COMP_CODE# </small></b> <span ng-click="vm.moreFabricListModal(dataItem.MC_FAB_PROD_ORD_H_ID)" style=\"display:block;cursor:pointer;\"><i class=\"fa fa-search-plus\">more</i></span>' },
                //{ title: "Navigation", width: "100px", template: '# <h5> <span style=\"cursor:pointer;\" title=\"Goto Item Spec\" class=\"label label-success\" ng-click=\"vm.gotoGmtItemSpec(dataItem)\" >Item Spec</span></h5>#' },
                //{ title: "Navigation", width: "120px", template: '#<span style=\"cursor:pointer;\" title=\"Fabric Booking Sheet\" class=\"label label-success\" ng-click=\"vm.printBookingReport(dataItem)\">Booking</span> #' },                
                { field: "UNIT_PRICE", title: "Price", width: "60px", filterable: false, template: '#= UNIT_PRICE #' },

                { field: "ORD_CONF_DT", title: "Confirm Date", type: "date", width: "100px", template: "#= kendo.toString(kendo.parseDate(ORD_CONF_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", filterable: false },
                { field: "SHIP_DT", title: "Ship Date", type: "date", width: "100px", template: "#= kendo.toString(kendo.parseDate(SHIP_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", filterable: false },
                {
                    title: "Action",
                    template: function () {
                        return "<a class='btn btn-xs blue' title='Goto Item Spec' ui-sref='GmtItemOprSpec({pMC_BYR_ACC_ID: dataItem.MC_BYR_ACC_ID, pMC_STYLE_H_ID:dataItem.MC_STYLE_H_ID})' ><i class='fa fa-edit'> Item Spec</i></a>";
                    },
                    width: "100px"
                },
            ]
        };


        vm.moreFabricListModal = function (MC_FAB_PROD_ORD_H_ID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'KnitProdOrderFabDtl.html',
                controller: function ($scope, $modalInstance, ItemList) {

                    $scope.ItemList = ItemList;
                    $scope.cancel = function () {
                        $modalInstance.close();
                    }

                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    ItemList: function (IeDataService) {
                        return IeDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectForKnitProdOrder/' + (MC_FAB_PROD_ORD_H_ID || 0));
                    }
                }
            });
        }

        vm.openSampleStatus = function (pMC_ORDER_H_ID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'SampleStatusByOrder.html',
                controller: function ($scope, $modalInstance, ItemList, StrikeOffs) {

                    $scope.ItemList = ItemList;
                    $scope.StrikeOffs = StrikeOffs;

                    $scope.cancel = function () {
                        $modalInstance.close();
                    }

                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    ItemList: function (IeDataService) {
                        return IeDataService.getDataByFullUrl('/api/mrc/StyleH/getSampleTasksByOrder?pMC_ORDER_H_ID=' + pMC_ORDER_H_ID);
                    },
                    StrikeOffs: function (IeDataService) {
                        return IeDataService.getDataByFullUrl('/api/mrc/StyleH/getStrikeOffTasksByOrder?pMC_ORDER_H_ID=' + pMC_ORDER_H_ID);
                    }
                }
            });
        }
        
        vm.onPrint = function () {
            var url = '/Merchandising/Mrc/StyleOrderFollowupRpt?a=263/#/rpt';

            url += '?page=' + page + '&pageSize=' + pageSize;
            url += '&pMC_BYR_ACC_ID=' + ($stateParams.pMC_BYR_ACC_ID < 1 ? null : $stateParams.pMC_BYR_ACC_ID);

            url += '&pRF_FAB_PROD_CAT_ID=' + (vm.form.RF_FAB_PROD_CAT_ID ? vm.form.RF_FAB_PROD_CAT_ID:'');

            url += '&pMC_ORDER_H_ID_LST=' + (vm.form && vm.form.MC_ORDER_H_ID_LST ? vm.form.MC_ORDER_H_ID_LST.join(',') : '');

            if ($stateParams.pFIRSTDATE && $stateParams.pLASTDATE) {
                url += '&pFIRSTDATE=' + $stateParams.pFIRSTDATE;
                url += '&pLASTDATE=' + $stateParams.pLASTDATE;
                url += '&pMONTHOF=' + $stateParams.pMONTHOF;
            }

            var opt = 'location=yes,height=570,width=' + ($window.outerWidth - 300) + ',scrollbars=yes,status=yes';
            var win = $window.open(url, "newwindow", opt);
             win.focus();

        };

        vm.gotoGmtItemSpec = function (dataItem) {
            console.log(dataItem);
            alert('x');
            $state.go('GmtItemOprSpec', { pMC_STYLE_H_ID: dataItem.MC_STYLE_H_ID });
        }


    }

})();