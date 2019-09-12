////////// Start CutMrkr4CadController Controller
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutStrRcvController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'CuttingDataService', 'Dialog', CutStrRcvController]);
    function CutStrRcvController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, CuttingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");
                
        vm.IS_SHOW_MARKER = 'N';
        var key = 'GMT_MRKR_REQ_ID';
        vm.today = new Date();

        vm.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> #: data.FAB_PROD_CAT_SNAME # (#: data.ORDER_NO #)</p></span>';
        vm.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.FAB_PROD_CAT_SNAME||" "||data.ORDER_NO #)</h5></span>';

        vm.form = { GMT_MRKR_ID: 0, MC_BYR_ACC_GRP_ID: 0, MC_STYLE_H_EXT_ID: 0, MC_STYLE_H_ID: 0, MC_ORDER_H_ID: 0, MC_COLOR_ID: 0, itemsOrdSizeRatio: [] };
                                 

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;                
            });
        }

        vm.reqDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.reqDateOpened = true;
        };
        
        
       

        
        
        vm.layGridOption = {
            height: 250,
            scrollable: true,
            pageable: false,
            editable: false,
            selectable: "cell",
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
            columns: [                                
                { field: "SL_NO", title: "Size", width: "15%", filterable: true },
                { field: "BNDL_AVAILABLE", title: "Bundle Available", width: "20%", filterable: false },
                { field: "BNDL_SCANED", title: "Bundle Scanned", width: "15%", filterable: false },
                { field: "BNDL_RECV", title: "Store Rcve", width: "15%", filterable: false },
                { field: "BNDL_REMING", title: "Bundle Remaining", width: "20%", filterable: false },
                {
                    title: "",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='' >Show Bundle</button>";
                    },
                    width: "15%"
                }
            ]
        };


        vm.layGridDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    e.success([{ SL_NO: 1, BNDL_AVAILABLE: 100, BNDL_SCANED: 75, BNDL_RECV: 67, BNDL_REMING: 8 }, { SL_NO: 2, BNDL_AVAILABLE: 120, BNDL_SCANED: 100, BNDL_RECV: 90, BNDL_REMING: 10 }]);

                    //e.success([{ SL_NO: 1, NO_OF_PLY: 15, ROLL_WT: 10 }, { SL_NO: 2, NO_OF_PLY: 19, ROLL_WT: 12 }]);

                    //var url = '/api/Cutting/MrkrReq/GetMarkerList?pMC_STYLE_H_EXT_ID=' + vm.form.MC_STYLE_H_EXT_ID;
                    
                    //console.log(url);

                    //CuttingDataService.getDataByFullUrl(url).then(function (res) {
                    //    if (vm.form.GMT_COLOR_ID > 1) {
                    //        e.success([{
                    //            GMT_MRKR_ID: 1, MRKR_REF_NO: 'MRKR2018050001', MRKR_SH_DESC: 'Cut dia: 60", Len: , Wth: 160"', WAY_TYPE_NAME: 'All GMT one way',
                    //            MARKER_TYPE_NAME: 'Normal', BK_FIN_DIA: '60"', CUT_FIN_DIA: '60"', MRKR_LEN: '', MRKR_WDT: '60"', MRKR_STATUS_NAME: 'Not Approve'
                    //        }]);
                    //    }
                    //    else {
                    //        e.success(res);
                    //    }
                    //});
                }
            }//,           
            //schema: {               
            //    model: {
            //        id: "GMT_MRKR_ID",
            //        fields: {                        
            //            MRKR_REF_NO: { type: "string", filterable: false }
            //        }
            //    }
            //}
        });
            
        



        vm.addNewMarker = function () {
            vm.IS_SHOW_MARKER = 'Y';
            vm.mrkrListGridDataSource.read();
        }
        
        vm.cancel = function () {
            vm.IS_SHOW_MARKER = 'N';
        }

        //$scope.$watchGroup(['vm.form.SCM_STORE_ID', 'vm.form.RCV_DT'], function (newVal, oldVal) {

        //    if (!_.isEqual(newVal, oldVal)) {
        //        if (newVal != '') {
        //            vm.selectedItem = undefined;
        //            vm.errors = null;

        //            vm.IS_NEXT = 'N';
                    
        //            //vm.next();
        //            vm.rollRcvGridDataSource.read();
        //        }
        //    }
        //}, true);
              

    }

})();
////////// End CutMrkr4CadController Controller






