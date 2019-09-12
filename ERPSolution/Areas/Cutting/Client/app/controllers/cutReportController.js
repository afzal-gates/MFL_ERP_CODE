(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutReportController', ['logger', 'config', 'CuttingDataService', 'CutRptData', '$q', '$scope', '$http', '$sessionStorage', '$filter', 'access_token', 'cur_user_id', CutReportController]);
    function CutReportController(logger, config, CuttingDataService, CutRptData, $q, $scope, $http, $sessionStorage, $filter, access_token, cur_user_id) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.dtFormat = config.appDateFormat;
        vm.showSplash = true;                        

        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> #: data.ORDER_NO #</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO #)</h5></span>';


        vm.insert = true;        
        vm.today = new Date();
        vm.rptList = angular.copy(CutRptData);

        console.log(vm.rptList);

        vm.form = {
            REPORT_CODE: 'RPT-3501', IS_EXCEL_FORMAT: 'N', FROM_DATE: $filter('date')(vm.today, vm.dtFormat), TO_DATE: $filter('date')(vm.today, vm.dtFormat),
            SCM_SUPPLIER_ID: 0, FROM_MONTH: $filter('date')(vm.today, 'MMM/yyyy'), TO_MONTH: $filter('date')(vm.today, 'MMM/yyyy')
        };


       

        function activate() {
            var promise = [getBuyerAcGrpList(), getByrAccWiseStyleExtList(), getCuttingTableList() ];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


             

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

       
        vm.fromDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.fromDateOpened = true;
        };

        vm.toDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.toDateOpened = true;
        };

        vm.FROM_MONTH_OPEN = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.FROM_MONTH_OPENED = true;
        };

        vm.TO_MONTH_OPEN = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.TO_MONTH_OPENED = true;
        };

        function getBuyerAcGrpList() {

            return vm.buyerAcGrpList = {
                optionLabel: "--- Select Buyer Group ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            CuttingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.form.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    vm.styleExtDataSource.read();
                    
                },
                dataTextField: "BYR_ACC_GRP_NAME_EN",
                dataValueField: "MC_BYR_ACC_GRP_ID"
            };
        }


        function getByrAccWiseStyleExtList() {
            vm.styleExtOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ORDER_NO",
                dataValueField: "MC_ORDER_H_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.form.MC_ORDER_H_ID = item.MC_ORDER_H_ID;
                    vm.orderColorListDataSource.read();
                    
                }
            };

            return vm.styleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/common/getOrderStyleDropDownDataGmt?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : '');
                        url += CuttingDataService.kFilterStr2QueryParam(params.filter);
                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.orderColorListDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                   return CuttingDataService.getDataByFullUrl('/api/common/getOrderColorDataForGmt?pMC_ORDER_H_ID=' + (vm.form.MC_ORDER_H_ID || 0)).then(function (res) {
                        e.success(res);
                    });
                }
            }
        });


        function getCuttingTableList() {
            vm.cuttingTableOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "TABLE_NO",
                dataValueField: "GMT_CUT_TABLE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.GMT_CUT_TABLE_ID = item.GMT_CUT_TABLE_ID;
                }
            };

            return vm.cuttingTableDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/Cutting/MrkrReq/GetCuttingTableList';

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };


        vm.rptOnChange = function (itm,idx) {
            //alert(idx);           
            console.log(CutRptData[idx]);
            var rptList = [];
            rptList = angular.copy(CutRptData);
            vm.form.REPORT_CODE = rptList[idx].RPT_CODE;
        }

        $scope.$watch('vm.form.REPORT_CODE', function (newVal, oldVal) {

            vm.isRDLC = false;
            vm.isByrAccGrp = false;            
            vm.isByrAcc = false;            
            vm.isStyleExt = false;            
            vm.isOrder = false;
            vm.isFormMonth = false;
            vm.isToMonth = false;
            vm.isFormDate = false;
            vm.isToDate = false;            
            vm.isOrderColor = false;
            vm.isProdType = false;            
            vm.isCompany = false;
            vm.isOffice = false;
            vm.isCutTable = false;
            
            
            
            if (vm.form.REPORT_CODE == 'RPT-4507') { //RPT-4507" Date, Order and Color wise Cut Panel Inspection Summery
                vm.isRDLC = true;                                
                vm.isByrAccGrp = true;
                vm.isOrder = true;
                vm.isOrderColor = true;
                vm.isFormDate = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4508') { //RPT-4508" Order and Color wise Cut Panel Inspection Summery
                vm.isRDLC = true;
                vm.isByrAccGrp = true;
                vm.isOrder = true;
                vm.isOrderColor = true;
                vm.isFormDate = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4509') { //RPT-4509" Daily Table Wise Cutting Production
                vm.isRDLC = true;
                vm.isByrAccGrp = true;
                vm.isOrder = true;                
                vm.isCutTable = true;
                vm.isFormDate = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4510') { //RPT-4510" Daily Cutting Target and Achive
                vm.isRDLC = true;                
                vm.isCutTable = true;
                vm.isFormDate = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4511') { //RPT-4511" Order and Color wise Cutting summery
                vm.isRDLC = true;                
                vm.isOrder = true;
                vm.isOrderColor = true;
                vm.isFormDate = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4512') { //RPT-4512" Order, Color and Cutting# wise Cut Panel Inspection Summery
                vm.isRDLC = true;
                vm.isByrAccGrp = true;
                vm.isOrder = true;
                vm.isOrderColor = true;
                vm.isFormDate = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4513') { //RPT-4513" Date wise Plies
                vm.isRDLC = true;
                vm.isByrAccGrp = true;
                vm.isOrder = true;
                vm.isOrderColor = true;
                vm.isFormDate = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4516') { //RPT-4516" Daily Cutting Production
                vm.isRDLC = true;                
                vm.isOrder = true;                
                vm.isFormDate = true;                
            }
            else if (vm.form.REPORT_CODE == 'RPT-4517') { //RPT-4517" Daily Table Wise Cutting Production
                vm.isRDLC = true;
                vm.isByrAccGrp = true;
                vm.isOrder = true;
                vm.isCutTable = true;
                vm.isFormMonth = true;
                vm.isToMonth = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4518') { //RPT-4518" Date wise Cut Panel Inspection Summery
                vm.isRDLC = true;                
                vm.isFormDate = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4519') { //RPT-4519" Buyer wise Daily Input Balance
                vm.isRDLC = true;
                vm.isOrder = true;
                vm.isOrderColor = true;
                vm.isFormDate = true;                
            }
            else if (vm.form.REPORT_CODE == 'RPT-4520') { //RPT-4520" Date and Cutting# wise Plies
                vm.isRDLC = true;
                vm.isByrAccGrp = true;
                vm.isOrder = true;
                vm.isOrderColor = true;
                vm.isFormDate = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4521') { //RPT-4521" Bundle Chart for Embellishment
                vm.isRDLC = true;
                vm.isByrAccGrp = true;
                vm.isOrder = true;
                vm.isOrderColor = true;
                vm.isFormDate = true;
                vm.isToDate = true;
            }
        });


        vm.preview = function () {
            var url;
            if (vm.isRDLC == true) {
                url = '/api/Cutting/CutReport/PreviewReportRDLC';
            }
            else {
                url = '/api/Cutting/CutReport/PreviewReport';
            }
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');
            
            vm.form.FROM_DATE = $filter('date')(vm.form.FROM_DATE, vm.dtFormat);
            vm.form.TO_DATE = $filter('date')(vm.form.TO_DATE, vm.dtFormat);


            var params = angular.copy(vm.form);
            //params['REPORT_CODE'] = 'RPT-4514';
            //params['IS_EXCEL_FORMAT'] = 'Y'
            console.log(params);

            for (var i in params) {
                if (params.hasOwnProperty(i)) {

                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    //input.value = params[i];
                    input.value = (params[i] instanceof Date) ? params[i].toISOString() : params[i];
                    form.appendChild(input);
                }
            }

            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);

        };
                                 
        
    }

    

})();