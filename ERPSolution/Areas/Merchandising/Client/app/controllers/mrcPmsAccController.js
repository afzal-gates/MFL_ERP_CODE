(function () {
    'use strict';
    angular.module('multitex.mrc').controller('MrcPmsAccController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'BuyerAccData', MrcPmsAccController]);
    function MrcPmsAccController($q, config, MrcDataService, $stateParams, $state, $scope, BuyerAccData) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        var V_MC_BYR_ACC_ID;
        vm.form = {
            FIRSTDATE: null,
            LASTDATE: null,
            FIRSTDATE_MIN: null,
            LASTDATE_MIN : null
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
            return MrcDataService.getDataByFullUrl('/api/mrc/Order/OrderShipmentMonth?pMC_BYR_ACC_ID').then(function (res) {
                vm.shipMonthList = new kendo.data.DataSource({
                    data: res
                })
            });
        }


        vm.dtFormat = config.appDateFormat;
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.FIRSTDATE_OPEN = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.FIRSTDATE_OPENED = true;
        };

        vm.LASTDATE_OPEN = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.LASTDATE_OPENED = true;
        };


        vm.changeDtRange = function (monthOf, FROM_DT, TO_DATE) {
            if (FROM_DT, TO_DATE) {
                $state.go('pmsAcc.gridAcc', {
                    pMC_BYR_ACC_ID: V_MC_BYR_ACC_ID, pFIRSTDATE: ((FROM_DT instanceof Date) ? FROM_DT.toISOString() : FROM_DT), pLASTDATE: ((TO_DATE instanceof Date) ? TO_DATE.toISOString() : TO_DATE), pMONTHOF: (monthOf ||'NA')
                }, { reload: 'pmsAcc.gridAcc' });
            } else {
                $state.go('pmsAcc.gridAcc', { pMC_BYR_ACC_ID: V_MC_BYR_ACC_ID, pFIRSTDATE: null, pLASTDATE: null, pMONTHOF: null }, { reload: 'pms.grid' });
            }
        };

        vm.onChangeShipMonth = function (data, e) {
            var item = e.sender.dataItem(e.sender.item);
            if (item.hasOwnProperty('MONTHOF') && item.MONTHOF) {
                data['FIRSTDATE'] = item.FIRSTDATE;
                data['LASTDATE'] = item.LASTDATE;
                data['MONTHOF'] = item.MONTHOF;
                data['FIRSTDATE_MIN'] = item.FIRSTDATE;
                data['LASTDATE_MIN'] = item.LASTDATE;
                vm.changeDtRange(item.MONTHOF, item.FIRSTDATE, item.LASTDATE);

               // $state.go('pms.grid', { pMC_BYR_ACC_ID: V_MC_BYR_ACC_ID, pFIRSTDATE: item.FIRSTDATE, pLASTDATE: item.LASTDATE, pMONTHOF: item.MONTHOF }, { reload: 'pms.grid' });
            } else {

                data['FIRSTDATE'] = null;
                data['LASTDATE'] = null;
                data['MONTHOF'] = null;
                data['FIRSTDATE_MIN'] = null;
                data['LASTDATE_MIN'] = null;
                vm.changeDtRange(null, null, null);
               // $state.go('pms.grid', { pMC_BYR_ACC_ID: V_MC_BYR_ACC_ID, pFIRSTDATE: null, pLASTDATE: null, pMONTHOF: null }, { reload: 'pms.grid' });
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
            $state.go('pmsAcc.gridAcc', { pMC_BYR_ACC_ID: tab.MC_BYR_ACC_ID, pFIRSTDATE: vm.form.FIRSTDATE, pLASTDATE: vm.form.LASTDATE, pMONTHOF: vm.form.MONTHOF }, { reload: 'pmsAcc.gridAcc' });
        }


    }

})();


(function () {
    'use strict';
    angular.module('multitex.mrc').controller('MrcPmsAccDController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', '$modal', '$window', MrcPmsAccDController]);
    function MrcPmsAccDController($q, config, MrcDataService, $stateParams, $state, $scope, $modal, $window) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.form = {
            RF_FAB_PROD_CAT_ID: ''
        };

        var page = 1;
        var pageSize = 15;


        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetStyleDropDownData(2)];
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
                        MrcDataService.getDataByFullUrl(url).then(function (res) {
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

        $scope.template = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> Order: #: data.ORDER_NO #</p></span>';
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

                    var url = '/api/mrc/PmsAcc/BuyerAccessoriesDashboard';

                    url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += '&pMC_BYR_ACC_ID=' + ($stateParams.pMC_BYR_ACC_ID < 1 ? null : $stateParams.pMC_BYR_ACC_ID);
                    //url += '&pRF_FAB_PROD_CAT_ID=2';
                    url += '&pRF_FAB_PROD_CAT_ID=' + ($stateParams.RF_FAB_PROD_CAT_ID ? $stateParams.RF_FAB_PROD_CAT_ID : 2);

                    page = params.page;
                    pageSize = params.pageSize + params.pageSize;
                    if ($stateParams.pFIRSTDATE && $stateParams.pLASTDATE) {
                        url += '&pFIRSTDATE=' + $stateParams.pFIRSTDATE;
                        url += '&pLASTDATE=' + $stateParams.pLASTDATE;
                    }

                    return MrcDataService.getDataByFullUrl(url).then(function (res) {
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

        $scope.styleOrderAuto = function (val) {
            return $http.get('/Hr/HrEmployee/EmployeeAutoData', {
                params: {
                    pEMPLOYEE_CODE: val,
                    pLK_JOB_STATUS_ID: null
                }
            }).then(function (response) {
                return response.data;
            });
        };

        $scope.onSelectStyleOrder = function (item) {
            //console.log(item);
            vm.form.STYLE_ORDER_NO = item.STYLE_ORDER_NO;
        }


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

                        var url = '/api/mrc/PmsAcc/BuyerAccessoriesDashboard';

                        url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                        url += '&pMC_BYR_ACC_ID=' + ($stateParams.pMC_BYR_ACC_ID < 1 ? null : $stateParams.pMC_BYR_ACC_ID);

                        url += '&pRF_FAB_PROD_CAT_ID=' + (form.RF_FAB_PROD_CAT_ID ? form.RF_FAB_PROD_CAT_ID : '');
                        url += '&pSTYLE_ORDER_NO=' + (form.STYLE_ORDER_NO ? form.STYLE_ORDER_NO : '');

                        url += '&pMC_ORDER_H_ID_LST=' + (form && form.MC_ORDER_H_ID_LST ? form.MC_ORDER_H_ID_LST.join(',') : '');

                        page = params.page;
                        pageSize = params.pageSize + params.pageSize;

                        //$state.go($state.current, { page: params.page, pageSize: params.pageSize }, { notify: false });

                        if ($stateParams.pFIRSTDATE && $stateParams.pLASTDATE) {
                            url += '&pFIRSTDATE=' + $stateParams.pFIRSTDATE;
                            url += '&pLASTDATE=' + $stateParams.pLASTDATE;
                        }

                        return MrcDataService.getDataByFullUrl(url).then(function (res) {
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
                { field: "STYLE_NO", title: "Style", width: "100px", locked: true, template: '# if (STYLE_ORD_SL == 1 ) { #   #=STYLE_NO#  # } #' },
                { field: "STYLE_NO", title: "Work Style", width: "100px", locked: true, template: '# if (STYLE_ORD_SL == 1 ) { #   #=WORK_STYLE_NO# # } #' },
                { field: "STYLE_NO", title: "Order", width: "100px", locked: true, template: '# if (STYLE_ORD_SL == 1 ) { #   #=ORDER_NO# # } #' },
                { title: 'Style Desc.', width: "150px", template: '# if (STYLE_ORD_SL == 1 ) { #   # for (var i = 0; i < data.images.length; i++) { # <img data-ng-src="data:image/png;base64,#:data.images[i].STYL_KEY_IMG#" title="Click for Image Enlarge" ng-click="vm.zoomImage(dataItem.images[#=i#].STYL_KEY_IMG)" alt="No Photo" style="border:1px solid black;width:45px; height:45px" /> # } # <span style=\"display:block;font-style:italic;\"><small><b> #=STYLE_DESC# </small></b></span>  # } #', locked: true },
                { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer", width: "90px", locked: true, filterable: false, template: '# if (STYLE_ORD_SL == 1 ) { #   #=BYR_ACC_GRP_NAME_EN#  # } #' },
                { field: "FIB_COMP_CODE_FULL", title: "Item Details", width: "180px", locked: true, filterable: false, template: '# if (STYLE_ORD_SL == 1 ) { # <small title=\"#= FIB_COMP_CODE_FULL#\"><b>  #= FIB_COMP_CODE_FULL# </small></b> <span ng-click="vm.moreFabricListModal(dataItem.MC_FAB_PROD_ORD_H_ID)" style=\"display:block;cursor:pointer;\"><i class=\"fa fa-search-plus\">more</i></span>    # } #' },
                //{ title: "Navigation", width: "120px", template: '# if (STYLE_ORD_SL == 1 && RF_FAB_PROD_CAT_ID==2) { # <h5> <span style=\"cursor:pointer;\" title=\"Fabric Booking Sheet\" class=\"label label-success\" ng-click=\"vm.printBookingReport(dataItem)\">Booking</span> &nbsp;<span style=\"cursor:pointer;\" title=\"Budget Sheet\" class=\"label label-primary\" ng-click=\"vm.printBudgetSheetReport(dataItem)\">Budget</span>  </h5><h5><span style=\"cursor:pointer;\" title=\"Sample Status\" class=\"label label-warning\" ng-click=\"vm.openSampleStatus(dataItem.MC_ORDER_H_ID)\">Sample & StrikeOff</span></h5> # } #' },
                { field: "COLOR_NAME_EN", title: "Color", width: "100px", filterable: false },

                { field: "UNIT_PRICE", title: "Price", width: "60px", filterable: false, template: '# if (STYLE_ORD_SL == 1 ) { #  #= UNIT_PRICE #   # } #' },

                { field: "ORD_CONF_DT", title: "Confirm Date", type: "date", template: " # if (STYLE_ORD_SL == 1 ) { #  #= kendo.toString(kendo.parseDate(ORD_CONF_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #  # } #", width: "100px", filterable: false },
                { field: "SHIP_DT", title: "Ship Date", type: "date", template: " # if (STYLE_ORD_SL == 1 ) { #  #= kendo.toString(kendo.parseDate(SHIP_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #  # } #", width: "100px", filterable: false },

                { field: "LEAD_TIME", title: "Lead Time", width: "60px", filterable: false, template: '# if (STYLE_ORD_SL == 1 ) { #  #= LEAD_TIME #   # } #' },
                { field: "ACT_SHIP_DATE", title: "Act Ship Date", type: "date", template: "# if (STYLE_ORD_SL == 1 && ACT_SHIP_DATE ) { #  #= kendo.toString(kendo.parseDate(ACT_SHIP_DATE,'yyyy-MM-dd'),'dd-MMM-yyyy') #  # } #", width: "100px", filterable: false },



              { field: "ART_WRK_RCV_DT", title: "Art/W Rcv", type: "date", template: "<b>P:</b># if (STYLE_ORD_SL == 1 && ART_WRK_RCV_DT_P ) { #  #= kendo.toString(kendo.parseDate(ART_WRK_RCV_DT_P,'yyyy-MM-dd'),'dd-MMM-yyyy') #  # } # <br><b>A:</b># if (STYLE_ORD_SL == 1 && ART_WRK_RCV_DT ) { #  #= kendo.toString(kendo.parseDate(ART_WRK_RCV_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #  # } #", width: "120px", filterable: false },

                {
                    title: 'Labdip',
                    columns: [
                            { field: "LD_SEND_DATE", title: "Sent", type: "date", template: "<b>P:</b># if (STYLE_ORD_SL == 1 && LD_SEND_DATE_P ) { # #= kendo.toString(kendo.parseDate(LD_SEND_DATE_P,'yyyy-MM-dd'),'dd-MMM-yyyy') #   # } # <br> <b>A:</b># if (STYLE_ORD_SL == 1 && LD_SEND_DATE ) { # #= kendo.toString(kendo.parseDate(LD_SEND_DATE,'yyyy-MM-dd'),'dd-MMM-yyyy') #   # } #", width: "120px", filterable: false },
                            { field: "LD_APPROVE_DATE", title: "Approved", type: "date", template: "<b>P:</b> # if (STYLE_ORD_SL == 1 && LD_APPROVE_DATE_P ) { # #= kendo.toString(kendo.parseDate(LD_APPROVE_DATE_P,'yyyy-MM-dd'),'dd-MMM-yyyy') #   # } # <br>  <b>P:</b> # if (STYLE_ORD_SL == 1 && LD_APPROVE_DATE ) { # #= kendo.toString(kendo.parseDate(LD_APPROVE_DATE,'yyyy-MM-dd'),'dd-MMM-yyyy') #   # } #", width: "120px", filterable: false }
                    ]
                },
                { field: "ORD_DOC_RCV_DT", title: "O/D Rcv Dt", type: "date", template: "<b>P:</b># if (STYLE_ORD_SL == 1 && ORD_DOC_RCV_DT_P ) { # #= kendo.toString(kendo.parseDate(ORD_DOC_RCV_DT_P,'yyyy-MM-dd'),'dd-MMM-yyyy') #   # } # <br> <b>A:</b># if (STYLE_ORD_SL == 1 && ORD_DOC_RCV_DT ) { # #= kendo.toString(kendo.parseDate(ORD_DOC_RCV_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #   # } #", width: "120px", filterable: false },

                { field: "COL_STD_RECV_DATE", title: "Color Std Recv", type: "date", template: "<b>P:</b># if (STYLE_ORD_SL == 1 && COL_STD_RECV_DATE_P ) { # #= kendo.toString(kendo.parseDate(COL_STD_RECV_DATE_P,'yyyy-MM-dd'),'dd-MMM-yyyy') #   # } # <br>  <b>A:</b># if (STYLE_ORD_SL == 1 && COL_STD_RECV_DATE ) { # #= kendo.toString(kendo.parseDate(COL_STD_RECV_DATE,'yyyy-MM-dd'),'dd-MMM-yyyy') #   # } #", width: "120px", filterable: false },

                {
                    title: 'Yarn',
                    columns: [
                            { field: "YRN_BK_DATE", title: "Booking", type: "date", template: "<b>P:</b># if (STYLE_ORD_SL == 1 && YRN_BK_DATE_P ) { # #= kendo.toString(kendo.parseDate(YRN_BK_DATE_P,'yyyy-MM-dd'),'dd-MMM-yyyy') #   # } #<br>   <b>A:</b># if (STYLE_ORD_SL == 1 && YRN_BK_DATE ) { # #= kendo.toString(kendo.parseDate(YRN_BK_DATE,'yyyy-MM-dd'),'dd-MMM-yyyy') #   # } #", width: "120px", filterable: false },
                            { field: "YRN_INH_DATE", title: "Inhouse", type: "date", template: "<b>P:</b># if (STYLE_ORD_SL == 1 && YRN_INH_DATE_P ) { # #= kendo.toString(kendo.parseDate(YRN_INH_DATE_P,'yyyy-MM-dd'),'dd-MMM-yyyy') #   # } #  <br> <b>A:</b># if (STYLE_ORD_SL == 1 && YRN_INH_DATE ) { # #= kendo.toString(kendo.parseDate(YRN_INH_DATE,'yyyy-MM-dd'),'dd-MMM-yyyy') #   # } #", width: "120px", filterable: false }
                    ]
                },

                {
                    title: 'First Sample',
                    columns: [
                            { title: "Sent", type: "date", template: "<b>P:</b># if (STYLE_ORD_SL == 1 && data.FIRST_SAMPLE.ACT_START_DT_P ) { # #= data.FIRST_SAMPLE.ACT_START_DT_P #   # } # <br> <b>A:</b># if (STYLE_ORD_SL == 1 && data.FIRST_SAMPLE.ACT_START_DT ) { # #= data.FIRST_SAMPLE.ACT_START_DT #   # } #", width: "120px", filterable: false },
                            { title: "Approved", type: "date", template: "<b>P:</b># if (STYLE_ORD_SL == 1 && data.FIRST_SAMPLE.ACT_END_DT_P ) { # #= data.FIRST_SAMPLE.ACT_END_DT_P #   # } # <br> <b>A:</b># if (STYLE_ORD_SL == 1 && data.FIRST_SAMPLE.ACT_END_DT ) { # #= data.FIRST_SAMPLE.ACT_END_DT #   # } #", width: "120px", filterable: false }
                    ]
                },

                {
                    title: 'Counter Sample',
                    columns: [
                            { title: "Sent", type: "date", template: "<b>P:</b># if (STYLE_ORD_SL == 1 && data.COUNTER_SAMPLE.ACT_START_DT_P ) { # #= data.COUNTER_SAMPLE.ACT_START_DT_P #   # } #   <br><b>A:</b># if (STYLE_ORD_SL == 1 && data.COUNTER_SAMPLE.ACT_START_DT ) { # #= data.COUNTER_SAMPLE.ACT_START_DT #   # } #", width: "120px", filterable: false },
                            { title: "Approved", type: "date", template: "<b>P:</b># if (STYLE_ORD_SL == 1 && data.COUNTER_SAMPLE.ACT_END_DT_P ) { # #= data.COUNTER_SAMPLE.ACT_END_DT_P #   # } # <br> <b>A:</b># if (STYLE_ORD_SL == 1 && data.COUNTER_SAMPLE.ACT_END_DT ) { # #= data.COUNTER_SAMPLE.ACT_END_DT #   # } #", width: "120px", filterable: false }
                    ]
                },

                {
                    title: 'Embr. Strike-off',
                    columns: [
                            { title: "Sent", type: "date", template: "<b>P:</b># if (STYLE_ORD_SL == 1 && data.EMBR_STRIKE.ACT_START_DT_P ) { # #= data.EMBR_STRIKE.ACT_START_DT_P #   # } # <br> <b>A:</b># if (STYLE_ORD_SL == 1 && data.EMBR_STRIKE.ACT_START_DT ) { # #= data.EMBR_STRIKE.ACT_START_DT #   # } #", width: "120px", filterable: false },
                            { title: "Approved", type: "date", template: "<b>P:</b># if (STYLE_ORD_SL == 1 && data.EMBR_STRIKE.ACT_END_DT_P ) { # #= data.EMBR_STRIKE.ACT_END_DT_P #   # } # <br> <b>A:</b># if (STYLE_ORD_SL == 1 && data.EMBR_STRIKE.ACT_END_DT ) { # #= data.EMBR_STRIKE.ACT_END_DT #   # } #", width: "120px", filterable: false }
                    ]
                },

                {
                    title: 'Print Strike-off',
                    columns: [
                            { title: "Sent", type: "date", template: "<b>P:</b># if (STYLE_ORD_SL == 1 && data.PRINT_STRIKE.ACT_START_DT_P ) { # #= data.PRINT_STRIKE.ACT_START_DT_P #   # } #  <br>  <b>A:</b># if (STYLE_ORD_SL == 1 && data.PRINT_STRIKE.ACT_START_DT ) { # #= data.PRINT_STRIKE.ACT_START_DT #   # } #", width: "120px", filterable: false },
                            { title: "Approved", type: "date", template: "<b>P:</b># if (STYLE_ORD_SL == 1 && data.PRINT_STRIKE.ACT_END_DT_P ) { # #= data.PRINT_STRIKE.ACT_END_DT_P #   # } # <br> <b>A:</b># if (STYLE_ORD_SL == 1 && data.PRINT_STRIKE.ACT_END_DT ) { # #= data.PRINT_STRIKE.ACT_END_DT #   # } #", width: "120px", filterable: false }
                    ]
                },
                { title : 'Accessories',
                columns :[
                       { field: "TRIM_BOOKING_DATE", title: "Booking", type: "date", template: "<b>P:</b># if (STYLE_ORD_SL == 1 && TRIM_BOOKING_DATE_P ) { # #= kendo.toString(kendo.parseDate(TRIM_BOOKING_DATE_P,'yyyy-MM-dd'),'dd-MMM-yyyy') #   # } # <br> <b>A:</b># if (STYLE_ORD_SL == 1 && TRIM_BOOKING_DATE ) { # #= kendo.toString(kendo.parseDate(TRIM_BOOKING_DATE,'yyyy-MM-dd'),'dd-MMM-yyyy') #   # } #", width: "120px", filterable: false },
                      { field: "TRIM_INHOUSE_DATE", title: "In-house", type: "date", template: "<b>P:</b># if (STYLE_ORD_SL == 1 && TRIM_INHOUSE_DATE_P ) { # #= kendo.toString(kendo.parseDate(TRIM_INHOUSE_DATE_P,'yyyy-MM-dd'),'dd-MMM-yyyy') #   # } # <br> <b>A:</b># if (STYLE_ORD_SL == 1 && TRIM_INHOUSE_DATE ) { # #= kendo.toString(kendo.parseDate(TRIM_INHOUSE_DATE,'yyyy-MM-dd'),'dd-MMM-yyyy') #   # } #", width: "120px", filterable: false },
                      ]
                },

                { field: "PRINT_EMB_AOP_YD", title: "YD/Prt/Emb/AOP/Wash", width: "180px", filterable: false, template: '# if (STYLE_ORD_SL == 1 ) { # <h6><b> # if (IS_YD == \"Y\" ) { # <span class=\"label label-primary\">Y/D</span> # } #  # if (IS_AOP == \"Y\" ) { # <span class=\"label label-info\">AOP</span> # } #   # if (IS_EMB == \"Y\" ) { # <span class=\"label label-success\">Embr</span> # } #  # if (IS_PRINT == \"Y\" ) { # <span class=\"label label-danger\">Print</span> # } #  # if (IS_GMT_WASH == \"Y\" ) { # <span class=\"label label-warning\">Gmt. Wash</span> # } # </b></h6> # } #' },
                { field: "TOT_ORD_QTY_COL", title: "Order Qty", width: "100px", filterable: false },
                { field: "NET_GFAB_QTY", title: "Rq.Fab(Kg)", width: "100px", filterable: false },
                { field: "KNIT_QC_PASS_QTY", title: "Knitting (kg)", width: "100px", filterable: false },
                //{ field: "NET_FFAB_QTY", title: "Fin. Qty.", width: "100px", filterable: false },
                //{ title: "% KntDone", width: "80px", filterable: false, template: ' # if (NET_GFAB_QTY > 0 ) { #   #= kendo.toString((KNIT_QC_PASS_QTY/NET_GFAB_QTY), "p") #   # } #' },
                { field: "DYE_QC_PASS_QTY", title: "Dyeing (kg)", width: "100px", filterable: false },
                //{ field: "DYE_QC_PASS_QTY", title: "Dye Done (%)", width: "100px", filterable: false },
                { title: "Cutting (pcs)", width: "100px", filterable: false, template: '' },
                { title: "Sewing (pcs)", width: "100px", filterable: false, template: '' }
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
                    ItemList: function (MrcDataService) {
                        return MrcDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectForKnitProdOrder/' + (MC_FAB_PROD_ORD_H_ID || 0));
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
                    ItemList: function (MrcDataService) {
                        return MrcDataService.getDataByFullUrl('/api/mrc/StyleH/getSampleTasksByOrder?pMC_ORDER_H_ID=' + pMC_ORDER_H_ID);
                    },
                    StrikeOffs: function (MrcDataService) {
                        return MrcDataService.getDataByFullUrl('/api/mrc/StyleH/getStrikeOffTasksByOrder?pMC_ORDER_H_ID=' + pMC_ORDER_H_ID);
                    }
                }
            });
        }



        vm.onPrint = function () {
            var url = '/Merchandising/Mrc/StyleOrderFollowupRpt?a=263/#/rpt';
            //url += '?page=' + page + '&pageSize=200';
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



    }

})();