(function () {
    'use strict';
    angular.module('multitex.knitting').controller('MrcReportController', ['logger', 'config', 'MrcDataService', 'mrcRptData', '$q', '$scope', '$http', '$sessionStorage', '$filter', 'access_token', MrcReportController]);
    function MrcReportController(logger, config, MrcDataService, mrcRptData, $q, $scope, $http, $sessionStorage, $filter, access_token) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.dtFormat = config.appDateFormat;
        vm.showSplash = true;                        

        vm.insert = true;        
        vm.today = new Date();
        vm.rptList = angular.copy(mrcRptData);
        
        vm.bulkBookingTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.WORK_STYLE_NO_LST #</h5><p> (#: data.ORDER_NO_LST||""#)</p></span>';
        vm.bulkBookingValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.WORK_STYLE_NO_LST # (#: data.ORDER_NO_LST||"" #)</h5></span>';

        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> (#: data.ORDER_NO||""#)</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO||"" #)</h5></span>';

        vm.form = {
            REPORT_CODE: '', IS_EXCEL_FORMAT: 'N', FROM_DATE: $filter('date')(vm.today, vm.dtFormat), TO_DATE: $filter('date')(vm.today, vm.dtFormat),
            MC_BYR_ACC_ID: 0
        };
                

        function activate() {
            var promise = [getBuyerAcList(), getByrAccWiseStyleList(), getByrAccWiseStyleExtList(), getBulkBookingList(),
            getDepartmentListForTna()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                
                vm.showSplash = false;
            });
        }

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        $scope.FROM_MONTH_OPEN = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.FROM_MONTH_OPENED = true;
        };

        $scope.TO_MONTH_OPEN = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.TO_MONTH_OPENED = true;
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


        function getDepartmentListForTna() {
            return MrcDataService.GetAllOthers('/api/mrc/TnaTask/getHrDeptsForTna').then(function (res) {
                vm.DeptListDs = new kendo.data.DataSource({
                    data: res
                });
            });
        };

        function getBuyerAcList() {
            
            return vm.buyerAcList = {
                optionLabel: "--- Select Buyer A/C ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.form.MC_BYR_ACC_ID = item.MC_BYR_ACC_ID;
                    vm.form.MC_STYLE_H_ID = null;
                    
                    if (vm.isStyle == true) {
                        vm.styleDataSource.read();
                    }
                    if (vm.isOrder == true) {
                        vm.styleExtDataSource.read();
                    }
                    if (vm.isBulkBooking == true) {
                        vm.bulkBookingDataSource.read();
                    }
                },                
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        }

        function getByrAccWiseStyleList() {
            vm.styleOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_ID"                             
            };

            return vm.styleDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/StyleH/BuyerAcWiseStyleList?&pageNumber=1&pageSize=20' + '&pMC_BYR_ACC_ID=' + ((vm.form.MC_BYR_ACC_ID>0)?vm.form.MC_BYR_ACC_ID:0);
                        url += MrcDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return MrcDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res.data);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getByrAccWiseStyleExtList() {
            vm.styleExtOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_EXT_ID"
            };

            return vm.styleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        //var url = '/api/mrc/StyleHExt/BuyerWiseStyleHExtList/' + ((vm.form.MC_BYR_ACC_ID > 0) ? vm.form.MC_BYR_ACC_ID : 0) + '/null?';
                        var url = '/api/mrc/Order/GetOrderList?pMC_BYR_ACC_ID=' + ((vm.form.MC_BYR_ACC_ID > 0) ? vm.form.MC_BYR_ACC_ID : ''); //pMC_BYR_ACC_ID=' + ((vm.form.MC_BYR_ACC_ID > 0) ? vm.form.MC_BYR_ACC_ID : 0);
                        url += MrcDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return MrcDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getBulkBookingList() {
            vm.bulkBookingOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_EXT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.MC_BLK_FAB_REQ_H_ID = item.MC_BLK_FAB_REQ_H_ID;
                    vm.form.MC_STYLE_H_EXT_ID = item.MC_STYLE_H_EXT_ID;

                    vm.form.REVISION_NO = item.REVISION_NO;
                    vm.form.MC_STYL_BGT_H_ID = item.MC_STYL_BGT_H_ID;
                    vm.form.REVISION_NO = item.BGT_REVISION_NO;
                    vm.form.IS_MULTI_SHIP_DT = item.IS_MULTI_SHIP_DT;
                    //vm.form.PAGE_SIZE_NAME = 'A4';
                }
            };

            return vm.bulkBookingDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                //schema: {
                //    data: "data",
                //    total: "total"
                //},
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/BulkFabBk/BulkFabBookingList/' + (vm.form.MC_BYR_ACC_ID || 0) + '/' + ((vm.form.MC_STYLE_H_ID > 0) ? vm.form.MC_STYLE_H_ID : null) + '/null?';
                        url += '&pageNumber=1&pageSize=10';
                        url += MrcDataService.kFilterStr2QueryParam(params.filter);
                        console.log(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        //if (paramsData[2] != undefined) {
                        //    url += '&pORDER_NO_LST=' + paramsData[2];
                        //}
                        
                        return MrcDataService.getDataByUrl(url).then(function (res) {
                            console.log(res);
                            e.success(res.data);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.rptOnChange = function (itm,idx) {
            //alert(idx);           
            console.log(mrcRptData[idx]);
            var rptList = [];
            rptList = angular.copy(mrcRptData);
            vm.form.REPORT_CODE = rptList[idx].RPT_CODE;
        }

        
        $scope.$watch('vm.form.REPORT_CODE', function (newVal, oldVal) {
            
            vm.isRDLC = false;
            vm.isByrAcc = false;
            vm.isStyle = false;
            vm.isOrder = false;
            vm.isBulkBooking = false;
            vm.isFormDate = false;
            vm.isToDate = false;
            vm.isMonth = false;
            vm.isDeptList = false;

            if (vm.form.REPORT_CODE == 'RPT-2000') { //"RPT-2000" Sample Fabric Booking      
                vm.isRDLC = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-2001') { //RPT-2001" Bulk Fabric Booking                
                vm.isByrAcc = true;                
                vm.isBulkBooking = true;
                //vm.isOrder = true;

                vm.form.MC_BLK_REVISION_NO = -1;                
            }
            else if (vm.form.REPORT_CODE == 'RPT-2002') { //"RPT-2002" Yarn List                
            }
            else if (vm.form.REPORT_CODE == 'RPT-2003') { //"RPT-2003" Budget Sheet               
                vm.isByrAcc = true;                
                vm.isBulkBooking = true;
                vm.isRDLC = true;
                //vm.isOrder = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-2004') { //"RPT-2004" Order Sheet                
            }
            else if (vm.form.REPORT_CODE == 'RPT-2005') { //"RPT-2005" Lapdib Program                
            }
            else if (vm.form.REPORT_CODE == 'RPT-2006') { //"RPT-2006" Sample Production                
                vm.isByrAcc = true;
                vm.isStyle = true;
                vm.isOrder = true;                
                vm.isFormDate = true;                
            }
            else if (vm.form.REPORT_CODE == 'RPT-2011') { //"RPT-2011" Dyeing Finishing T&A Fail    
                vm.form['FROM_DATE'] = '';
                vm.form['TO_DATE'] = '';
                vm.form['FROM_MONTH'] = '';
                vm.isDeptList = true;
                vm.isMonth = true;
                vm.isFormDate = true;
                vm.isRDLC = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-2014') { //"RPT-2014" Order Wise Accessories Status Report  
                vm.form['FROM_DATE'] = '';
                vm.form['TO_DATE'] = '';
                vm.isFormDate = true;
                vm.isRDLC = true;
                vm.isToDate = true;
                vm.isStyle = true;
                vm.isByrAcc = true;
            } else if (vm.form.REPORT_CODE == 'RPT-2016') { //"RPT-2016" Dyeing Finishing T&A Fail    
                vm.form['FROM_DATE'] = '';
                vm.form['TO_DATE'] = '';
                vm.form['FROM_MONTH'] = '';
                vm.isMonth = true;
                vm.isFormDate = true;
                vm.isRDLC = true;
                vm.isToDate = true;
            } else if (vm.form.REPORT_CODE == 'RPT-2017') { //"RPT-2017" Buyer Dyeing Finishing T&A Fail    
                vm.form['FROM_DATE'] = '';
                vm.form['TO_DATE'] = '';
                vm.form['FROM_MONTH'] = '';
                vm.isMonth = true;
                vm.isFormDate = true;
                vm.isRDLC = true;
                vm.isToDate = true;
            } else if (vm.form.REPORT_CODE == 'RPT-2018') { //"RPT-2018" Style-Dept T&A Task Fail   
                vm.form['FROM_DATE'] = '';
                vm.form['TO_DATE'] = '';
                vm.form['FROM_MONTH'] = '';
                vm.isByrAcc = true;
                vm.isMonth = true;
                vm.isFormDate = true;
                vm.isRDLC = true;
                vm.isToDate = true;
            }

                
        });


        vm.preview = function () {
            
            var url;
            if (vm.isRDLC == true) {
                url = '/api/Mrc/MrcReport/PreviewReportRDLC';
            }
            else {
                url = '/api/Mrc/MrcReport/PreviewReport';
            }

            if (vm.form.HR_DEPARTMENT_ID && vm.form.REPORT_CODE == 'RPT-2011') {
                vm.form.DEPARTMENT_NAME_EN = vm.DeptListDs.data().find(function (ob) { return ob.HR_DEPARTMENT_ID == parseInt(vm.form.HR_DEPARTMENT_ID) }).DEPARTMENT_NAME_EN;
            } else {
                vm.form.DEPARTMENT_NAME_EN = '';
            }

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.FROM_DATE = $filter('date')(vm.form.FROM_DATE, vm.dtFormat);
            vm.form.TO_DATE = $filter('date')(vm.form.TO_DATE, vm.dtFormat);
            vm.form.FROM_MONTH = $filter('date')(vm.form.FROM_MONTH, vm.dtFormat);

           
            var params = angular.copy(vm.form);
            
            console.log(params);

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
                                 
        
    }

    

})();