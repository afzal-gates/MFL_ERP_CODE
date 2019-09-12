﻿(function () {
    'use strict';
    angular.module('multitex.knitting').controller('GmtReportController', ['logger', 'config', 'GarmentsDataService', 'gmtRptData', '$q', '$state', '$scope', '$http', '$sessionStorage', '$filter', 'access_token', 'cur_user_id', GmtReportController]);
    function GmtReportController(logger, config, GarmentsDataService, gmtRptData, $q, $state, $scope, $http, $sessionStorage, $filter, access_token, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';

        activate();
        vm.title = config.appTitle;
        vm.dtFormat = config.appDateFormat;
        vm.showSplash = true;                        

        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> (#: data.ORDER_NO||""#)</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO||"" #)</h5></span>';

        vm.insert = true;        
        vm.today = new Date();
        vm.rptList = angular.copy(gmtRptData);

        vm.form = {
            REPORT_CODE: 'RPT-1500', IS_EXCEL_FORMAT: 'N', FROM_DATE: $filter('date')(vm.today, vm.dtFormat), TO_DATE: $filter('date')(vm.today, vm.dtFormat),
            SCM_SUPPLIER_ID: 0
        };

        //vm.form.REPORT_CODE = 'RPT-3501';
        //vm.form.IS_EXCEL_FORMAT = 'N';
        
        //$scope.$watch('vm.form.REPORT_CODE', function (newVal, oldVal) {
        //    vm.form.pREPORT_CODE = vm.form.REPORT_CODE;
        //});
        
        //vm.form.FROM_DATE = $filter('date')(vm.today, vm.dtFormat);
        //vm.form.TO_DATE = $filter('date')(vm.today, vm.dtFormat);

        

        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                
                vm.showSplash = false;
            });
        }


        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        //function getReportDataList() {
        //    return HrService.GetAllOthers('/api/common/getReportDataListByUser/' + vm.pRF_REPORT_GRP_ID).then(function (res) {
        //        vm.reportDataList = res;
        //    }, function (err) {
        //        console.log(err);
        //    });
        //};

        //vm.readTemplate = function () {
        //    $http({
        //        method: 'get',
        //        url: "/Hr/HrReport/ReadTemplate"
        //    });
        //};

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

        function getShipmentMonth() {
            
            return vm.shipMonthList = {
                optionLabel: "--Select Month--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectShipmentMonth/' + (vm.form.MC_BYR_ACC_ID || 0) + '/0/' + (vm.form.RF_FAB_PROD_CAT_ID || 0)).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "MONTHOF",
                dataValueField: "MONTHOF"
            };

        }



        vm.setBuyerName = function (e) {
            var obj = e.sender.dataItem(e.item);
            vm.form.BYR_ACC_GRP_NAME_EN = obj.BYR_ACC_GRP_NAME_EN
            vm.form['MC_BYR_ACC_ID_LST'] = (obj.MC_BYR_ACC_ID_LST || '');
        }

        vm.setStyleOrder = function (e) {
            var obj = e.sender.dataItem(e.item);
            vm.form.STYLE_NO = obj.STYLE_NO
        }

        vm.setColorName = function (e) {
            var obj = e.sender.dataItem(e.item);
            vm.form.COLOR_NAME_EN = obj.COLOR_NAME_EN
        }

        vm.setQCStatus = function (e) {
            var obj = e.sender.dataItem(e.item);
            vm.form.QC_STS_TYP_NAME = obj.QC_STS_TYP_NAME
        }

        vm.setShiftName = function (e) {
            var obj = e.sender.dataItem(e.item);
            vm.form.SCHEDULE_NAME_EN = obj.SCHEDULE_NAME_EN
        }

        vm.setPrdType = function (e) {
            var obj = e.sender.dataItem(e.item);
            vm.form.KNT_SC_PRG_ISS_NAME = obj.KNT_SC_PRG_ISS_NAME
        }

        vm.setSupplier = function (e) {
            var obj = e.sender.dataItem(e.item);
            vm.form.SUP_TRD_NAME_EN = obj.SUP_TRD_NAME_EN
        }

        vm.setFabType = function (e) {
            var obj = e.sender.dataItem(e.item);
            vm.form.FAB_TYPE_NAME = obj.FAB_TYPE_NAME
        }

        vm.onChangeProdCat = function () {

            var prodCatList = []; 
            prodCatList = _.map(_.filter(vm.prodCatDataSource.data(), function (ob) {
                return _.some(vm.form.RF_FAB_PROD_CAT_ID_LST, function (val) {
                    return ob.RF_FAB_PROD_CAT_ID == val;
                });
            }), 'FAB_PROD_CAT_SNAME');

            console.log(prodCatList);
            vm.form.RF_FAB_PROD_CAT_NAME_LST = prodCatList.length > 0 ? prodCatList.join(',') : 'All';
        }

        function getProdTypeList() {

            return vm.prodTypeList = {
                optionLabel: "--Select Production Type--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    data: [{ 'KNT_SC_PRG_ISS_ID': 0, 'KNT_SC_PRG_ISS_NAME': "In-House" }, { 'KNT_SC_PRG_ISS_ID': 9999999999, 'KNT_SC_PRG_ISS_NAME': "Sub-Contact" }]
                },
                dataTextField: "KNT_SC_PRG_ISS_NAME",
                dataValueField: "KNT_SC_PRG_ISS_ID"
            };
        };

        function getFabricTypeList() {
            vm.fabricTypeList = {
                optionLabel: "--Fabric Type--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/Common/FabricTypeList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "FAB_TYPE_NAME",
                dataValueField: "RF_FAB_TYPE_ID"
            };
        }

        function getShiftList() {

            return vm.shiftList = {
                optionLabel: "--Select Shift--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/knit/KntMachinOprAssign/GetKnitScheduleList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "SCHEDULE_NAME_EN",
                dataValueField: "HR_SCHEDULE_H_ID"
            };
        };
        
        function getYarnItemList() {
            return vm.YarnItemList = {
                optionLabel: "--Select Yarn Item--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=2').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "ITEM_NAME_EN",
                dataValueField: "INV_ITEM_ID"
            };
        }

        function getCotnTypeList() {
            return vm.CotnTypeList = {
                optionLabel: "--Cotton Type--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(62).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }

        function getYarnCountList() {

            return vm.YarnCountList = {
                optionLabel: "--Count--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/Common/YarnCountList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });

                        }
                    }
                },
                dataTextField: "YR_COUNT_DESC",
                dataValueField: "RF_YRN_CNT_ID"
            };
        }


        function getCompanyList() {
            return vm.companyList = {
                optionLabel: "-- Select Company --",
                filter: "startswith",
                autoBind: true,
                //index:1,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getCompanyData().then(function (res) {
                                e.success(res);

                                //////////////////////============//////////////////////////////
                                $('#ACC_PAY_PERIOD_ID_MONTH').kendoDropDownList({
                                    optionLabel: "-- Select Period --",
                                    filter: "startswith",
                                    autoBind: true,
                                    dataSource: {
                                        transport: {
                                            read: function (e) {
                                                $http({
                                                    method: 'get',
                                                    url: "/api/common/GetAccPayPeriod/?pHR_COMPANY_ID=" + res[0].HR_COMPANY_ID + "&pHR_PERIOD_TYPE_ID=" + vm.periodTypeId + "&pIS_SHOW4_RPT=Y",
                                                    headers: { 'Authorization': 'Bearer ' + access_token }
                                                }).
                                                success(function (data, status, headers, config) {
                                                    e.success(data);
                                                    if (data.length > 0) {
                                                        vm.form.ACC_PAY_PERIOD_ID_MONTH = data[0].ACC_PAY_PERIOD_ID;
                                                        vm.form.FROM_DATE = data[0].START_DATE;
                                                        vm.form.TO_DATE = data[0].END_DATE;
                                                    };
                                                }).
                                                error(function (data, status, headers, config) {
                                                    alert('something went wrong')
                                                    console.log(status);
                                                });
                                            }
                                        }
                                    },
                                    dataTextField: "MONTH_YEAR_NAME",
                                    dataValueField: "ACC_PAY_PERIOD_ID",
                                    //template: '<span>#: MONTH_YEAR_NAME #</span>',
                                    select: function (e) {
                                        var dataItem = this.dataItem(e.item);
                                        vm.form.ACC_PAY_PERIOD_ID_MONTH = dataItem.ACC_PAY_PERIOD_ID;
                                        var dt = moment(dataItem.START_DATE)._d;
                                        vm.form['FROM_DATE'] = dataItem.START_DATE == null ? '' : $filter('date')(dt, vm.dtFormat);
                                        dt = moment(dataItem.END_DATE)._d;
                                        vm.form['TO_DATE'] = dataItem.END_DATE == null ? '' : $filter('date')(dt, vm.dtFormat);
                                    }
                                });

                                ////////////////////////////////////////////////////
                                $('#ACC_PAY_PERIOD_ID').kendoDropDownList({
                                    optionLabel: "-- Select Period --",
                                    filter: "startswith",
                                    autoBind: true,
                                    dataSource: {
                                        transport: {
                                            read: function (e) {
                                                $http({
                                                    method: 'get',
                                                    url: "/api/common/GetAccPayPeriod/?pHR_COMPANY_ID=" + res[0].HR_COMPANY_ID + "&pHR_PERIOD_TYPE_ID=" + vm.periodTypeId + "&pIS_SHOW4_RPT=Y",
                                                    headers: { 'Authorization': 'Bearer ' + access_token }
                                                }).
                                                success(function (data, status, headers, config) {
                                                    e.success(data);
                                                    if (data.length > 0) {
                                                        vm.form.ACC_PAY_PERIOD_ID = data[0].ACC_PAY_PERIOD_ID;
                                                        vm.form.FROM_DATE = data[0].START_DATE;
                                                        vm.form.TO_DATE = data[0].END_DATE;
                                                    };
                                                }).
                                                error(function (data, status, headers, config) {
                                                    alert('something went wrong')
                                                    console.log(status);
                                                });
                                            }
                                        }
                                    },
                                    dataTextField: "REMARKS",
                                    dataValueField: "ACC_PAY_PERIOD_ID",
                                    //template: '<span>#: MONTH_YEAR_NAME #</span>',
                                    select: function (e) {
                                        var dataItem = this.dataItem(e.item);
                                        vm.form.ACC_PAY_PERIOD_ID = dataItem.ACC_PAY_PERIOD_ID;
                                        var dt = moment(dataItem.START_DATE)._d;
                                        vm.form['FROM_DATE'] = dataItem.START_DATE == null ? '' : $filter('date')(dt, vm.dtFormat);
                                        dt = moment(dataItem.END_DATE)._d;
                                        vm.form['TO_DATE'] = dataItem.END_DATE == null ? '' : $filter('date')(dt, vm.dtFormat);
                                    }
                                });
                                ///////////////////////////////////////////////////
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID",
                select: function (e) {
                    //alert('a');
                    var dataItem = this.dataItem(e.item);
                    var vHR_COMPANY_ID = dataItem.HR_COMPANY_ID; //this.value();
                    vm.form.ACC_PAY_PERIOD_ID = null;

                    ////////////////////////////////////////////////////////
                    $('#ACC_PAY_PERIOD_ID_MONTH').kendoDropDownList({
                        optionLabel: "-- Select Period --",
                        filter: "startswith",
                        autoBind: true,
                        dataSource: {
                            transport: {
                                read: function (e) {
                                    $http({
                                        method: 'get',
                                        url: "/api/common/GetAccPayPeriod/?pHR_COMPANY_ID=" + vHR_COMPANY_ID + "&pHR_PERIOD_TYPE_ID=" + vm.periodTypeId + "&pIS_SHOW4_RPT=Y",
                                        headers: { 'Authorization': 'Bearer ' + access_token }
                                    }).
                                    success(function (data, status, headers, config) {
                                        e.success(data);
                                        if (data.length > 0) {
                                            vm.form.ACC_PAY_PERIOD_ID_MONTH = data[0].ACC_PAY_PERIOD_ID;
                                            vm.form.FROM_DATE = data[0].START_DATE;
                                            vm.form.TO_DATE = data[0].END_DATE;
                                        };

                                    }).
                                    error(function (data, status, headers, config) {
                                        alert('something went wrong')
                                        console.log(status);
                                    });
                                }
                            }
                        },
                        dataTextField: "MONTH_YEAR_NAME",
                        dataValueField: "ACC_PAY_PERIOD_ID",
                        //valueTemplate: '<span>#: MONTH_YEAR_NAME #</span>',
                        select: function (e) {
                            var dataItem = this.dataItem(e.item);
                            vm.form.ACC_PAY_PERIOD_ID_MONTH = dataItem.ACC_PAY_PERIOD_ID;

                            var dt = moment(dataItem.START_DATE)._d;
                            vm.form['FROM_DATE'] = dataItem.START_DATE == null ? '' : $filter('date')(dt, vm.dtFormat);
                            dt = moment(dataItem.END_DATE)._d;
                            vm.form['TO_DATE'] = dataItem.END_DATE == null ? '' : $filter('date')(dt, vm.dtFormat);
                        }
                    });

                    ////////////////////////////////////////////////////////
                    $('#ACC_PAY_PERIOD_ID').kendoDropDownList({
                        optionLabel: "-- Select Period --",
                        filter: "startswith",
                        autoBind: true,
                        dataSource: {
                            transport: {
                                read: function (e) {
                                    $http({
                                        method: 'get',
                                        url: "/api/common/GetAccPayPeriod/?pHR_COMPANY_ID=" + vHR_COMPANY_ID + "&pHR_PERIOD_TYPE_ID=" + vm.periodTypeId + "&pIS_SHOW4_RPT=Y",
                                        headers: { 'Authorization': 'Bearer ' + access_token }
                                    }).
                                    success(function (data, status, headers, config) {
                                        e.success(data);
                                        if (data.length > 0) {
                                            vm.form.ACC_PAY_PERIOD_ID = data[0].ACC_PAY_PERIOD_ID;
                                            vm.form.FROM_DATE = data[0].START_DATE;
                                            vm.form.TO_DATE = data[0].END_DATE;
                                        };

                                    }).
                                    error(function (data, status, headers, config) {
                                        alert('something went wrong')
                                        console.log(status);
                                    });
                                }
                            }
                        },
                        dataTextField: "REMARKS",
                        dataValueField: "ACC_PAY_PERIOD_ID",
                        //valueTemplate: '<span>#: MONTH_YEAR_NAME #</span>',
                        select: function (e) {
                            var dataItem = this.dataItem(e.item);
                            vm.form.ACC_PAY_PERIOD_ID = dataItem.ACC_PAY_PERIOD_ID;
                            
                            var dt = moment(dataItem.START_DATE)._d;
                            vm.form['FROM_DATE'] = dataItem.START_DATE == null ? '' : $filter('date')(dt, vm.dtFormat);
                            dt = moment(dataItem.END_DATE)._d;
                            vm.form['TO_DATE'] = dataItem.END_DATE == null ? '' : $filter('date')(dt, vm.dtFormat);
                        }
                    });
                    /////////////////////////////////////////////////////////////////////////
                }

            };

        };

        function getOfficeList() {
            return vm.officeListData = {
                optionLabel: "-- Select Office --",
                //template: '<span class="text-primary">#= EMPLOYEE_CODE #</span> :  #= EMP_FULL_NAME_EN #',
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrOffice/OfficeListData"
                            }).
                            success(function (data, status, headers, config) {
                                e.success(data)
                            }).
                            error(function (data, status, headers, config) {
                                alert('something went wrong')
                                console.log(status);
                            });
                        }
                    }
                },
                dataTextField: "OFFICE_NAME_EN",
                dataValueField: "HR_OFFICE_ID"
            };
        };

        function getStoreList() {

            return vm.storeList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
                                //KnittingDataService.LookupListData(92).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.STORE_NAME_EN = item.STORE_NAME_EN;
                }
            };
        };

        function getProdCatSourceList() {
            vm.prodCatSourceOption = {
                optionLabel: "-- Select Type --",
                filter: "contains",
                autoBind: true,
                dataSource: vm.prodCatDataSource,
                dataTextField: "FAB_PROD_CAT_NAME",
                dataValueField: "RF_FAB_PROD_CAT_ID"
            };

            return vm.prodCatDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/GetFabProdCat';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };
        
        function GetCategoryBrandList() {

            return vm.categoryBrandList = {
                optionLabel: "-- Select Brand --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                                var list = _.filter(res, { 'INV_ITEM_CAT_ID': 2 });
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "BRAND_NAME_EN",
                dataValueField: "RF_BRAND_ID"
            };
        }

        function GetQcStatusTypeList() {

            return vm.QcStatusTypeList = {
                optionLabel: "-- Select QC Status --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/knit/KntScChlnRcv/GetQcStatusTypeList?pKNT_QC_STS_TYPE_ID_LST=1,3,4').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "QC_STS_TYP_NAME",
                dataValueField: "KNT_QC_STS_TYPE_ID"
            };
        }


        function GetColorList() {

            return vm.colorList = {
                optionLabel: "-- Select Color --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll').then(function (res) {
                               
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COLOR_NAME_EN",
                dataValueField: "MC_COLOR_ID"
            };
        }

        $scope.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST #</h5><p> #: data.STYLE_NO||"" #</p></span>';
        $scope.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST # (#: data.STYLE_NO||"" #)</h5></span>';

        $scope.FabOederByOhDs = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);

                    var url = "/api/mrc/StyleHExt/getFabProdOrderDataOh";
                    url += "?pMC_BYR_ACC_ID=" + (vm.form.MC_BYR_ACC_ID || null);

                    if (params.filter) {
                        url += '&pORDER_NO_LST=' + params.filter.replace(/'/g, '').split('~')[2];
                    } else {
                        url += '&pORDER_NO_LST';
                    }

                    if (vm.form.RF_FAB_PROD_CAT_ID > 1) {
                        url += '&pRF_FAB_PROD_CAT_ID=' + vm.form.RF_FAB_PROD_CAT_ID
                    };

                    if (vm.form.FIRSTDATE) {
                        url += '&pFIRSTDATE=' + vm.form.FIRSTDATE + '&pLASTDATE=' + vm.form.LASTDATE;
                    };

                    url += '&pIS_YD_ONLY=Y';
                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
        }
            },
            serverFiltering: true
        });

        //function getBuyerAcList() {
            
        //    return vm.buyerAcList = {
        //        optionLabel: "--- Select Buyer A/C ---",
        //        filter: "contains",
        //        autoBind: true,                
        //        dataSource: {
        //            transport: {
        //                read: function (e) {                            
        //                    KnittingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/SelectAll').then(function (res) {
        //                        e.success(res);
        //                    }, function (err) {
        //                        console.log(err);
        //                    });
        //                }
        //            }
        //        },
        //        dataTextField: "BYR_ACC_NAME_EN",
        //        dataValueField: "MC_BYR_ACC_ID"
        //    };
        //};

        function getBuyerAcList() {

            return vm.buyerAcList = {
                optionLabel: "--- Select Buyer A/C ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/SelectAll').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        };

        vm.getBuyerAccWiseStyleList = function (e) {

            var MC_BYR_ACC_ID = e.sender.dataItem(e.item).MC_BYR_ACC_ID;
            
            if (vm.isOrder == true) {
                vm.byrAccWiseBookingStyleDataSource = new kendo.data.DataSource({
                    serverFiltering: true,
                    transport: {
                        read: function (e) {
                            var webapi = new kendo.data.transports.webapi({});
                            var params = webapi.parameterMap(e.data);
                            var url = '/api/mrc/StyleHExt/ByrWiseBookingStyleHExtList/' + MC_BYR_ACC_ID + '/' + null + '?';
                            url += KnittingDataService.kFilterStr2QueryParam(params.filter);
                            url += '&pMC_STYLE_H_EXT_ID';

                            return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                                e.success(res);

                                console.log(res);
                            }, function (err) {
                                console.log(err);
                            })
                        }
                    }
                });
            }
            else if (vm.isStyleExt == true) {
                vm.buyerAccWiseStyleDataSource = new kendo.data.DataSource({
                    serverFiltering: true,
                    transport: {
                        read: function (e) {
                            var webapi = new kendo.data.transports.webapi({});
                            var params = webapi.parameterMap(e.data);
                            var url = '/api/mrc/StyleHExt/BuyerWiseStyleHExtList/' + MC_BYR_ACC_ID + '/' + null + '?';
                            url += KnittingDataService.kFilterStr2QueryParam(params.filter);
                            url += '&pMC_STYLE_H_EXT_ID';

                            return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                                e.success(res);

                                console.log(res);
                            }, function (err) {
                                console.log(err);
                            })
                        }
                    }
                });
            }
        };

        function getBuyerAcGrpList() {

            return vm.buyerAcGrpList = {
                optionLabel: "--- Select Buyer Group ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (res) {
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

                    vm.form.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    vm.form.MC_STYLE_H_ID = 0;

                    vm.styleExtDataSource.read();
                    
                },
                //dataBound: function (e) {
                //    if ($stateParams.pMC_BYR_ACC_ID != null && $stateParams.pMC_BYR_ACC_ID > 0) {
                //        vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;

                //        vm.styleExtDataSource.read();
                //        //vm.styleFabricDataSource.read();
                //        //vm.getOrderByBuyerAC($stateParams.pMC_BYR_ACC_ID);
                //    }
                //},
                dataTextField: "BYR_ACC_GRP_NAME_EN",
                dataValueField: "MC_BYR_ACC_GRP_ID"
            };
        }

        function getByrAccWiseStyleExtList() {
            vm.styleExtOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_EXT_ID"//,
                //select: function (e) {
                //    var item = this.dataItem(e.item);
                //    vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
                //    vm.form.MC_ORDER_H_ID_LST = item.MC_ORDER_H_ID;

                //    vm.colorListDataSource.read();
                //    vm.styleFabricDataSource.read();
                //},
                //dataBound: function (e) {
                //    if ($stateParams.pMC_STYLE_H_EXT_ID != null && $stateParams.pMC_STYLE_H_EXT_ID > 0) {
                //        vm.form.MC_STYLE_H_EXT_ID = $stateParams.pMC_STYLE_H_EXT_ID;

                //        vm.styleFabricDataSource.read();
                //    }
                //}
            };

            return vm.styleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Order/GetOrderList?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : ''); //pMC_BYR_ACC_ID=' + ((vm.form.MC_BYR_ACC_ID > 0) ? vm.form.MC_BYR_ACC_ID : 0);
                        //url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getReqTypeList() {

            return vm.reqTypeList = {
                optionLabel: "-- Select Req. Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetReqType').then(function (res) {
                                //KnittingDataService.LookupListData(88).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "REQ_TYPE_NAME",
                dataValueField: "RF_REQ_TYPE_ID"
            };
        };

        function getYrnIssRtnClallanList() {
            vm.yrnIssRtnOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "RET_CHALAN_NO",
                dataValueField: "KNT_YRN_ISS_RET_H_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.form.RET_CHALAN_NO = item.RET_CHALAN_NO;
                }
            };

            return vm.yrnIssRtnDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/YarnIssueReturn/SelectAll/' + 1 + '/' + 10 + '?pRF_REQ_TYPE_ID=' + vm.form.RF_REQ_TYPE_ID;
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            //console.log('===============');
                            console.log(res.data);
                            e.success(res.data);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };


        function GetYarnSupplierList() {
            return vm.yarnSupplierList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/SelectAllByCat/2').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getSupplierList() {
            vm.supplierOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SUP_TRD_NAME_EN",
                dataValueField: "SCM_SUPPLIER_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;

                    if (vm.isScProgNo == true) {
                        vm.scProgDataSource.read();
                        vm.scOrderDataSource.read();
                    }
                    else if (vm.isScIssProgNo == true) {
                        vm.scIssProgDataSource.read();
                    }
                    else if (vm.isScoGreyFabRcvChln == true) {
                        vm.scoGreyFabRcvChalnDataSource.read();
                    }
                }                
            };

            return vm.supplierDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getScoGreyFabRcvChlnList() {
            vm.scoGreyFabRcvChalnOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "CHALAN_NO",
                dataValueField: "KNT_SCO_CHL_RCV_H_ID"
            };

            return vm.scoGreyFabRcvChalnDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KntScChlnRcv/GetRcvChallanList?pageNumber=1&pageSize=10&pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || 0);
                        
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res.data);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function GetScProgList() {
            vm.scProgOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "PRG_RCV_NO",
                dataValueField: "KNT_SC_PRG_RCV_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.form.KNT_SC_PRG_RCV_ID = item.KNT_SC_PRG_RCV_ID;
                    vm.scOrderDataSource.read();
                }
                //dataBound: function (e) {
                //    if ($stateParams.pSCM_SUPPLIER_ID != null && $stateParams.pSCM_SUPPLIER_ID > 0) {
                //        vm.form.SCM_SUPPLIER_ID = $stateParams.pSCM_SUPPLIER_ID;
                //    }
                //}
            };

            return vm.scProgDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KntScRcv/GetScRcvProgramList?pSCM_SUPPLIER_ID=' + vm.form.SCM_SUPPLIER_ID;
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function GetScOrderList() {
            vm.scOrderOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SC_ORDER_NO",
                dataValueField: "SCM_SC_WO_REF_ID"
            };

            return vm.scOrderDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {                        
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KntScRcv/GetScOrdRefByPartyID?&pageNumber=1&pageSize=10&pSCM_SUPPLIER_ID=' + ((vm.form.SCM_SUPPLIER_ID == null) ? 0 : vm.form.SCM_SUPPLIER_ID) +
                                    '&pKNT_SC_PRG_RCV_ID=' + vm.form.KNT_SC_PRG_RCV_ID;
                        
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);
                        console.log(url);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);
                        
                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res.data);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function GetScIssProgList() {
            vm.scIssProgOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "PRG_ISS_NO",
                dataValueField: "KNT_SC_PRG_ISS_ID"              
            };

            return vm.scIssProgDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KnitPlan/ScProgramPagingData?&pageNumber=1&pageSize=10&pSCM_SUPPLIER_ID=' + vm.form.SCM_SUPPLIER_ID;
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res.data);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };
             
        $scope.scFabDelvChallanAuto = function (val) {
            return KnittingDataService.getDataByFullUrl('/api/knit/KntGreyFabDelv/ScGreyFabDelvChallanAuto?pCHALAN_NO=' + val).then(function (res) {
                return res;
            }, function (err) {
                console.log(err);
            });

            //return $http.get('/Hr/HrEmployee/EmployeeAutoData', {
            //    params: {
            //        pEMPLOYEE_CODE: val,
            //        pLK_JOB_STATUS_ID: null
            //    }
            //}).then(function (response) {
            //    return response.data;
            //});
        };

        $scope.onScFabDelvChallanSelectItem = function (item) {
            console.log(item);
            vm.form.SC_FAB_DELV_CHALAN_NO = item.CHALAN_NO;
        }

        $scope.scYrnIssChallanAuto = function (val) {
            return KnittingDataService.getDataByFullUrl('/api/knit/YarnIssue/YarnIssueChallanAuto?pCHALAN_NO=' + val).then(function (res) {
                return res;
            }, function (err) {
                console.log(err);
            });            
        };

        $scope.onScYrnIssChallanSelectItem = function (item) {
            console.log(item);
            vm.form.ISS_CHALAN_NO = item.ISS_CHALAN_NO;
        }

        vm.rptOnChange = function (itm,idx) {
            //alert(idx);           
            console.log(kntRptData[idx]);
            var rptList = [];
            rptList = angular.copy(kntRptData);
            vm.form.REPORT_CODE = rptList[idx].RPT_CODE;
        }

        $scope.$watch('vm.form.REPORT_CODE', function (newVal, oldVal) {

            vm.isLC = false;
            vm.isLot = false;
            vm.isBrand = false;
            vm.isByrAccGrp = false;
            vm.isRDLC = false;
            vm.isSupplier = false;
            vm.isScProgNo = false;
            vm.isScIssProgNo = false;
            vm.isScOrder = false;
            vm.isScYrnIssChallan = false;
            vm.isScDelvChallan = false;
            vm.isByrAcc = false;            
            vm.isStyleExt = false;
            vm.isProdCatSource = false;
            vm.isProdCatIdLst = false;
            vm.isOrder = false;
            vm.isStore = false;
            vm.isReqType = false;
            vm.isYrnRtnChallanNo = false;
            vm.isFormDate = false;
            vm.isToDate = false;
            vm.isQcStatus = false;
            vm.isColor = false;
            vm.isYarnSupplier = false;
            vm.isYC = false;
            vm.isCT = false;
            vm.isItem = false;
            vm.isShipMonth = false;
            vm.isNoOfCount = false;
            vm.isScoGreyFabRcvChln = false;
            vm.isJobCard = false;
            vm.isShift = false;
            
            vm.isFabRoll = false;
            vm.isFabType = false;
            vm.isSubContactParty = false;
            vm.isProdType = false;
            vm.isFabOrderYd = false;
            
            if (vm.form.REPORT_CODE == 'RPT-3501') { //RPT-3501" Knitting Sub-contract Program Cum Contract Report                
                vm.isSupplier = true;                
                vm.isScIssProgNo = true;                
            }
            else if (vm.form.REPORT_CODE == 'RPT-3502') { //RPT-3502" Knitting Subcontract Yarn Delivery Challan Issue Report                
                vm.isSupplier = true;                
                vm.isScIssProgNo = true;                
                vm.isScYrnIssChallan = true;                
            }
            else if (vm.form.REPORT_CODE == 'RPT-3503') { //RPT-3503" Yarn Receive Statement               
                vm.isFormDate = true;
                vm.isToDate = true;               
            }
            else if (vm.form.REPORT_CODE == 'RPT-3504') { //RPT-3504" Yarn Current Stock Report
                vm.isRDLC = false;                
            }
            else if (vm.form.REPORT_CODE == 'RPT-3505') { //RPT-3505" Order Wise Yarn Issue Register Report                
                vm.isByrAccGrp = true;                
                vm.isProdCatSource = true;
                vm.isOrder = true;                
            }
            else if (vm.form.REPORT_CODE == 'RPT-3506') { //RPT-3506" Yarn Issue Statement                
                vm.isFormDate = true;
                vm.isToDate = true;                
            }
            else if (vm.form.REPORT_CODE == 'RPT-3507') { //RPT-3507" Yarn Requsition for Knitting Bulk                
                vm.isFormDate = true;
                vm.isToDate = true;                
            }
            else if (vm.form.REPORT_CODE == 'RPT-3508') { //RPT-3508" Knitting Subcontract Fabric Delivery Challan Report                
                vm.isSupplier = true;
                vm.isScProgNo = true;                
                vm.isScOrder = true;                
                vm.isScDelvChallan = true;                
            }
            else if (vm.form.REPORT_CODE == 'RPT-3509') { //RPT-3509" Yarn Return Challan (Yarn Test)
                vm.form.RF_REQ_TYPE_ID = 4;
                vm.yrnIssRtnDataSource.read();

                vm.isYrnRtnChallanNo = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3512') { //RPT-3512" Yarn Requsition for Knitting Sample
                vm.isFormDate = true;
                vm.isToDate = true;              
            }
            else if (vm.form.REPORT_CODE == 'RPT-3514') { //RPT-3514" Party wise grey store ledger                
                vm.isSupplier = true;
                vm.isScProgNo = true;                
                vm.isScOrder = true;                
            }
            else if (vm.form.REPORT_CODE == 'RPT-3515') { //RPT-3515" Grey Fabric Delivery Challan to Store
                vm.isRDLC = true;                                
                vm.isStore = true;
                vm.isProdCatIdLst = true;
                vm.isFormDate = true;                
            }
            else if (vm.form.REPORT_CODE == 'RPT-3516') { //RPT-3516" Grey Fabric Roll Inspection
                vm.isRDLC = true;
                vm.isByrAccGrp = true;
                vm.isOrder = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isQcStatus = true;
                vm.isColor = true;
                vm.isShift = true;
                vm.isJobCard = true;

                vm.isFabRoll = true;
                vm.isFabType = true;
                vm.isSubContactParty = true;
                vm.isProdType = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3517') { //RPT-3517" S/C Party wise grey store ledger (Out House)
                vm.isSupplier = true;
                vm.isByrAccGrp = true;
                vm.isOrder = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3518') { //RPT-3518" Yarn Stock With Value
                vm.isRDLC = true;
                vm.isSupplier = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isLC = true;
                vm.isLot = true;
                vm.isBrand = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3519') { //RPT-3519" Daily M/C and Shift wise Knitting Statement               
                vm.isFormDate = true;                
            }
            else if (vm.form.REPORT_CODE == 'RPT-3522') { //RPT-3523" Challan wise Party Ledger (S/C Out-house)
                vm.isSupplier = true;                
                vm.isFormDate = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3523') { //RPT-3523" Order Statement (S/C Out-house)                
                vm.isSupplier = true;               
                vm.isByrAccGrp = true;                
                vm.isOrder = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3525') { //RPT-3525" Order and color wise fabric receive to grey store                
                vm.isProdCatSource = true;
                vm.isToDate = true;
                vm.isByrAccGrp = true;
                vm.isOrder = true;                
            }
            else if (vm.form.REPORT_CODE == 'RPT-3526') { //RPT-3526" M/C wise monthly knitting production - capacity vs achievement                
                vm.isFormDate = true;
                vm.isToDate = true;
            }            
            else if (vm.form.REPORT_CODE == 'RPT-3527') { //RPT-3527" Order and Color wise Inspection Report                
                vm.isRDLC = true;
                vm.isByrAccGrp = true;
                vm.isOrder = true;
                //vm.isFormDate = true;
                //vm.isToDate = true;
                vm.isQcStatus = true;
                vm.isColor = true;
                vm.isShipMonth = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3529') { //RPT-3529" Daily Gray QC Summary Report                
                vm.isRDLC = true;
                vm.isByrAccGrp = true;
                vm.isOrder = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isQcStatus = true;
                vm.isColor = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3530') { //RPT-3530" Monthly Out Side Quality Summary                
                vm.isRDLC = true;
                vm.isByrAccGrp = true;
                vm.isOrder = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isQcStatus = true;
                vm.isSupplier = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3531') { //RPT-3531" Order and color wise knitting production status
                vm.isByrAccGrp = true;
                vm.isProdCatSource = true;
                vm.isOrder = true;
                vm.isShipMonth = true;

                vm.form.RF_FAB_PROD_CAT_ID = 2;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3532') { //RPT-3532" Lot wise loose yarn statement
                vm.isFormDate = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3533') { //RPT-3533" Supplier Wise Yarn Test Result
                vm.isRDLC = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isYarnSupplier = true;
                vm.isColor = true;
                vm.isYC = true;
                vm.isLot = true;
                vm.isItem = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3534') { //RPT-3534" Lot & Count Wise Loose Yarn Stock
                vm.isRDLC = true;
                //vm.isFormDate = true;
                //vm.isToDate = true;
                vm.isYarnSupplier = true;
                vm.isBrand = true;
                vm.isYC = true;
                vm.isCT = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3535') { //RPT-3535" Party wise Knitting Performance by Program (S/C Out-house)
                vm.isSupplier = true;
                vm.isByrAccGrp = true;
                vm.isOrder = true;
                vm.isFormDate = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3536') { //RPT-3536" Yarn Inventory Aging
                vm.isRDLC = true;
                //vm.isFormDate = true;
                //vm.isToDate = true;
                vm.isYarnSupplier = true;
                vm.isBrand = true;
                vm.isYC = true;
                vm.isCT = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3537') { //RPT-3537" Order and Color wise Fabric Production Statement
                vm.isByrAccGrp = true;
                vm.isProdCatSource = true;
                vm.isOrder = true;
                vm.isShipMonth = true;

                vm.form.RF_FAB_PROD_CAT_ID = 2;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3538') { //RPT-3538" Monthly Gray QC Summary Report                
                vm.isRDLC = true;
                vm.isByrAccGrp = true;
                vm.isOrder = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isQcStatus = true;
                vm.isColor = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3539') { //RPT-3539" Monthly Gray QC Summary With Defect Type                
                vm.isRDLC = true;
                vm.isByrAccGrp = true;
                vm.isOrder = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isQcStatus = true;
                vm.isColor = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3540') { //RPT-3540" Party wise Knitting Performance Summary (S/C Out-house)
                vm.isNoOfCount = true;
                vm.isFormDate = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3542') { //RPT-3542" Daily Roll Labelling Statement (S/C Out-house)
                vm.isSupplier = true;
                vm.isScoGreyFabRcvChln = true;
                vm.isJobCard = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3543') { //"RPT-3543" M/C and Shift wise Oil Consumption & Unit Cost
                vm.isStore= true;
                vm.isFormDate = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3544') { //"RPT-3544" M/C and Operator wise Oil Consumption & Unit Cost
                vm.isStore = true;
                vm.isFormDate = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3545') { //"RPT-3545" Daily Collar/Cuff Transfer to QC and Store
                vm.isStore = true;
                vm.isFormDate = true;                
            }
            else if (vm.form.REPORT_CODE == 'RPT-3546') { //"RPT-3546" Roll# wise Grey Fabric Delivery Challan to Store
                vm.isRDLC = true;
                vm.isStore = true;
                vm.isProdCatIdLst = true;
                vm.isFormDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3547') { //"RPT-3547" Order and Color wise Collar/Cuff production
                
                //vm.isProdCatIdLst = true;
                vm.isOrder = true;
                vm.isColor = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3548') { //"RPT-3548" Daily Collar/Cuff Production

                //vm.isProdCatIdLst = true;
                vm.isOrder = true;
                vm.isColor = true;
                vm.isFormDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3550') { //"RPT-3548" Daily Collar/Cuff Production
                vm.isByrAccGrp = true;
                vm.isFabOrderYd = true;
                vm.isProdCatSource = true;
                vm.isFormDate = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3551') { //RPT-3551" Excess Yarn Statement
                vm.isFormDate = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3552') { //RPT-3552" M/C and Date wise Needle Broken
                vm.isFormDate = true;
                vm.isToDate = true;
            }

        });


        vm.preview = function () {
            
            var url;
            if (vm.isRDLC == true) {
                url = '/api/Gmt/GmtReport/PreviewReportRDLC';
            }
            else {
                url = '/api/Gmt/GmtReport/PreviewReport';
            }

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.FROM_DATE = $filter('date')(vm.form.FROM_DATE, vm.dtFormat);
            vm.form.TO_DATE = $filter('date')(vm.form.TO_DATE, vm.dtFormat);
           
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