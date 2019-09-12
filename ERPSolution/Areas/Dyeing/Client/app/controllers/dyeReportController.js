(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DyeReportController', ['logger', 'config', 'DyeingDataService', 'dyeRptData', '$q', '$scope', '$http', '$sessionStorage', '$filter', 'access_token', 'cur_user_id', DyeReportController]);
    function DyeReportController(logger, config, DyeingDataService, dyeRptData, $q, $scope, $http, $sessionStorage, $filter, access_token, cur_user_id) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.dtFormat = config.appDateFormat;
        vm.showSplash = true;

        vm.insert = true;
        vm.today = new Date();
        vm.rptList = angular.copy(dyeRptData);
        console.log(vm.rptList);

        vm.form = {};
        vm.form.REPORT_CODE = (vm.rptList[0].RPT_CODE || 'RPT-4000');
        vm.form.IS_EXCEL_FORMAT = 'N';

        //$scope.$watch('vm.form.REPORT_CODE', function (newVal, oldVal) {
        //    vm.form.pREPORT_CODE = vm.form.REPORT_CODE;
        //});SOURCE FOR RE-PROCESS

        vm.form.FROM_DATE = $filter('date')(vm.today, vm.dtFormat);
        vm.form.TO_DATE = $filter('date')(vm.today, vm.dtFormat);



        function activate() {
            var promise = [getStoreList(), getSourceTypeList(), getItemCategList(), getBuyerAcList(), styleListByBuyer(null), getBuyerAcGrpList(),
                           GetPaymentMethodList(), getByrAccWiseStyleExtList(), getShiftList(), GetProcessList(), GetMachineList(), GetCompanyList(),
                           getShipmentMonth(), getProdCatSourceList()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);

                vm.showSplash = false;
            });
        }


        function GetPaymentMethodList() {

            return vm.payMethodList = {
                optionLabel: "-- Select Payment Method --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/GetPayMethod').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "PAY_MTHD_NAME",
                dataValueField: "RF_PAY_MTHD_ID"
            };
        };

        function getShipmentMonth() {

            return vm.shipMonthList = {
                optionLabel: "--Select Month--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectShipmentMonth/' + (vm.form.MC_BYR_ACC_ID || 0) + '/0/' + (vm.form.RF_FAB_PROD_CAT_ID || 0)).then(function (res) {
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

        function GetCompanyList() {

            return vm.companyList = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
        };

        function getBuyerAcGrpList() {

            return vm.buyerAcGrpList = {
                optionLabel: "--- Select Buyer Group ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (res) {
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
                dataValueField: "MC_STYLE_H_EXT_ID"
            };

            return vm.styleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Order/GetOrderList?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : ''); //pMC_BYR_ACC_ID=' + ((vm.form.MC_BYR_ACC_ID > 0) ? vm.form.MC_BYR_ACC_ID : 0);
                        //url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                        url += DyeingDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            for (var x = 0; x < res.length; x++) {
                                var SO = res[x].STYLE_NO + " (" + res[x].ORDER_NO + ")";
                                res[x].STYLE_NO = SO;
                            }
                            console.log(res);
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };


        //function getBuyerAcGrpList() {

        //    return vm.buyerAcGrpList = {
        //        optionLabel: "--- Select Buyer Group ---",
        //        filter: "contains",
        //        autoBind: true,
        //        dataSource: {
        //            transport: {
        //                read: function (e) {
        //                    DyeingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (res) {
        //                        e.success(res);
        //                    }, function (err) {
        //                        console.log(err);
        //                    });
        //                }
        //            }
        //        },
        //        dataTextField: "BYR_ACC_GRP_NAME_EN",
        //        dataValueField: "MC_BYR_ACC_GRP_ID"
        //    };
        //}

        vm.getBuyerGrpWiseStyleList = function (MC_BYR_ACC_GRP_ID) {

            vm.byrAccWiseBookingStyleDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Order/GetOrderList?pMC_BYR_ACC_GRP_ID=' + ((MC_BYR_ACC_GRP_ID > 0) ? MC_BYR_ACC_GRP_ID : 0);
                        url += DyeingDataService.kFilterStr2QueryParam(params.filter);

                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);

                            console.log(res);
                        }, function (err) {
                            console.log(err);
                        })
                    }
                }
            });

        }

        function getBuyerAcList() {

            return vm.buyerAcList = {
                optionLabel: "-- Select Buyer A/C --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/SelectAll').then(function (res) {
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

        vm.colorGroupList = {
            optionLabel: "-- Colour Group--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return DyeingDataService.getDataByFullUrl('/api/mrc/ColourMaster/ColourGroupList').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        })

                    }
                }
            },
            dataTextField: "COLOR_GRP_NAME_EN",
            dataValueField: "MC_COLOR_GRP_ID"
        };

        vm.colorList = {
            optionLabel: "-- Select Colour --",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return DyeingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        })

                    }
                }
            },
            dataTextField: "COLOR_NAME_EN",
            dataValueField: "MC_COLOR_ID"
        };

        vm.DyItemList = {
            optionLabel: "-- Select Item --",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/ItemDataListByCatList?pINV_ITEM_CAT_LST=3,4').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        })

                    }
                }
            },
            dataTextField: "ITEM_NAME_EN",
            dataValueField: "INV_ITEM_ID"
        };

        vm.supplierList = {
            optionLabel: "-- Select Supplier --",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return DyeingDataService.getDataByFullUrl('/api/purchase/supplierprofile/SelectAllByCat/3,4').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        })

                    }
                }
            },
            dataTextField: "SUP_TRD_NAME_EN",
            dataValueField: "SCM_SUPPLIER_ID"
        };


        vm.machineList = {
            optionLabel: "-- Select Machine --",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueRequisition/GetMachineInfoByTypeID?pDYE_MC_TYPE_ID=0').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        })

                    }
                }
            },
            dataTextField: "DYE_MACHINE_NO",
            dataValueField: "DYE_MACHINE_ID"
        };


        vm.buyerList = {
            optionLabel: "-- Select Buyer --",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return DyeingDataService.getDataByFullUrl('/api/mrc/buyer/SelectAll').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        })

                    }
                }
            },
            dataTextField: "BUYER_NAME_EN",
            dataValueField: "MC_BUYER_ID"
        };


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

        vm.readTemplate = function () {
            $http({
                method: 'get',
                url: "/Hr/HrReport/ReadTemplate"
            });
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

        function getProdCatSourceList() {
            return vm.prodCatSourceList = {
                optionLabel: "-- Select Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/GetFabProdCat').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "FAB_PROD_CAT_SNAME",
                dataValueField: "RF_FAB_PROD_CAT_ID"
            };
        };

        function getBuyerAcList() {

            return vm.buyerAcList = {
                optionLabel: "--- Select Buyer A/C ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/SelectAll').then(function (res) {
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

        function getStoreList() {
            return vm.storeList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=3,4&pSC_USER_ID=' + cur_user_id).then(function (res) {
                                var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 511 || x.LK_WH_TYPE_ID == 512 });
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
            };

            //return vm.storeList = {
            //    optionLabel: "-- Select Store --",
            //    filter: "contains",
            //    autoBind: true,
            //    dataSource: {
            //        transport: {
            //            read: function (e) {
            //                DyeingDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
            //                    var list = _.filter(res, function (o) { return ((o.SCM_STORE_ID === 6 || o.SCM_STORE_ID === 4)) });
            //                    e.success(list);
            //                    //KnittingDataService.LookupListData(92).then(function (res) {
            //                    //e.success(res);
            //                });
            //            }
            //        }
            //    },
            //    dataTextField: "STORE_NAME_EN",
            //    dataValueField: "SCM_STORE_ID",
            //    select: function (e) {
            //        var item = this.dataItem(e.item);
            //        //$scope.dataItem.STORE_NAME_EN = item.STORE_NAME_EN;
            //    }
            //};
        };

        function getItemCategList() {
            return vm.ItemCategList = {
                optionLabel: "-- Select Category --",
                filter: "contains",
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {
                                var list = _.filter(res, function (o) { return ((o.INV_ITEM_CAT_ID === 3 || o.INV_ITEM_CAT_ID === 4)) });
                                e.success(list);
                            });
                            //DyeingDataService.getDataByUrlNoToken('/api/inv/ItemCategory/LoginUserWiseItemCatList').then(function (res) {
                            //    e.success(res);
                            //}, function (err) {
                            //    console.log(err);
                            //});
                        }
                    }
                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.form.ITEM_CAT_NAME_EN = item.ITEM_CAT_NAME_EN;
                },
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID"
            };
        }

        function getSourceTypeList() {

            return vm.sourceTypeList = {
                optionLabel: "-- Select Source --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(84).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };


        function GetMachineList() {

            return vm.dfMachineList = {
                optionLabel: "-- Select Machine --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/dye/DFMachine/SelectAll').then(function (res) {
                                for (var x = 0; x < res.length; x++) {
                                    var SO = res[x].DF_MC_NAME + " (" + res[x].DF_MACHINE_NO + ")";
                                    res[x].DF_MC_NAME = SO;
                                }
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "DF_MC_NAME",
                dataValueField: "DF_MACHINE_ID"
            };
        };

        function GetProcessList() {
            return vm.processList = {
                optionLabel: "-- Select Process --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/GetDFProcessType').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "DF_PROC_TYPE_NAME",
                dataValueField: "DF_PROC_TYPE_ID"
            };
        };


        function getShiftList() {

            return vm.shiftList = {
                optionLabel: "--Select Shift--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/knit/KntMachinOprAssign/GetKnitScheduleList').then(function (res) {
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
                            url += DyeingDataService.kFilterStr2QueryParam(params.filter);
                            url += '&pMC_STYLE_H_EXT_ID';

                            return DyeingDataService.getDataByFullUrl(url).then(function (res) {
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
                            url += DyeingDataService.kFilterStr2QueryParam(params.filter);
                            url += '&pMC_STYLE_H_EXT_ID';

                            return DyeingDataService.getDataByFullUrl(url).then(function (res) {
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

        vm.rptOnChange = function (itm, idx) {
            //alert(idx);           
            console.log(dyeRptData[idx]);
            var rptList = [];
            rptList = angular.copy(dyeRptData);
            vm.form.REPORT_CODE = rptList[idx].RPT_CODE;
        }

        $scope.orderNoAuto = function (val) {

            return DyeingDataService.getDataByFullUrl('/api/mrc/Order/OrderListByOrderNo/' + val).then(function (res) {
                return res;
            });

        };

        $scope.onSelectOrder = function (item) {

            //console.log(item);
            vm.form.MC_ORDER_H_ID = item.MC_ORDER_H_ID;
        }


        $scope.LabNoAuto = function (val) {

            return DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/SelectByLabNo/' + val).then(function (res) {
                return res;
            });

        };

        $scope.onSelectLab = function (item) {

            //console.log(item);
            vm.form.MC_LD_RECIPE_H_ID = item.MC_LD_RECIPE_H_ID;
        }

        $scope.BatchNoAuto = function (val) {

            var lst = _.filter(vm.styleExtDataSource.data(), function (x) { return x.MC_STYLE_H_EXT_ID == (vm.form.MC_STYLE_H_EXT_ID || 0) })
            console.log(lst);
            return DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/SearchBatch?pDYE_BATCH_NO=' + val + '&pMC_STYLE_H_ID=' + ((lst.length > 0 ? lst[0].MC_STYLE_H_ID : null) || null)).then(function (res) {
                return res;
            });

        };

        $scope.onSelectBatch = function (item) {

            //console.log(item);
            vm.form.DYE_BT_CARD_H_ID = item.DYE_BT_CARD_H_ID;
        }

        $scope.issRefAuto = function (val) {
            return DyeingDataService.getDataByFullUrl('/api/Dye/DyeChemicalStoreTransfer/getStoreIssueListByRefNo/' + val).then(function (res) {
                return res;
            });
        };

        $scope.onSelectIssue = function (item) {
            //console.log(item);
            vm.form.DYE_STR_TR_ISS_H_ID = item.DYE_STR_TR_ISS_H_ID;
        }

        $scope.issRefAuto2 = function (val) {
            return DyeingDataService.getDataByFullUrl('/api/Dye/DCIssue/GetIssueByRefNo/' + val).then(function (res) {
                return res;
            });
        };

        $scope.onSelectIssue2 = function (item) {
            //console.log(item);
            vm.form.DYE_DC_ISS_H_ID = item.DYE_DC_ISS_H_ID;
        }

        $scope.$watch('vm.form.REPORT_CODE', function (newVal, oldVal) {

            vm.isCompany = false;
            vm.isStore = false;
            vm.isItemCategory = false;
            vm.isIssRefNo = false;
            vm.isItemSourc = false;
            vm.isFormDate = false;
            vm.isToDate = false;
            vm.isRDLC = false;
            vm.isIssRefNo2 = false;
            vm.isItemSource = false;

            vm.isBuyer = false;
            vm.isBuyerAc = false;
            vm.isBuyerAcGrp = false;
            vm.isBuyerAcGrpList = false;
            vm.isMachine = false;
            vm.isColorGroup = false;
            vm.isColor = false;
            vm.isOrder = false;
            vm.isLab = false;
            vm.isReceipe = false;
            vm.isDayNo = false;
            vm.isSupplier = false;
            vm.isItem = false;
            vm.isBatch = false;
            vm.isStyle = false;
            vm.isStyleEx = false;
            vm.isProdCat = false;
            vm.isShipMonth = false;

            vm.isDFMachine = false;
            vm.isShift = false;
            vm.isDFProcess = false;
            vm.isCheckRoll = false;
            vm.isChallanNo = false;
            vm.isPayMethod = false;


            if (vm.form.REPORT_CODE == 'RPT-4000') { //RPT-4000" Dyes Chemical Batch Program Requisition Report

            }
            else if (vm.form.REPORT_CODE == 'RPT-4001') { //RPT-4001" Dyes Chemical Stock Report
                vm.isStore = true;
                vm.isItemCategory = true;
                vm.isItemSource = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isCompany = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4003') { //RPT-4003" Dyes Chemical Batch Cost Breakdown

                vm.isCompany = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;

                vm.isBuyer = true;
                vm.isBuyerAcGrpList = true;
                vm.isMachine = true;
                vm.isColorGroup = true;
                vm.isColor = true;
                vm.isOrder = true;
                vm.isLab = true;
                vm.isReceipe = true;
                vm.isDayNo = true;
                vm.isBatch = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4004') { //RPT-4004" Dyes Chemical Daily Dyeing Production

                vm.isCompany = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;

                vm.isBuyerAcGrp = true;
                vm.isMachine = true;
                vm.isColorGroup = true;
                vm.isColor = true;
                vm.isOrder = false;
                vm.isLab = true;
                vm.isReceipe = true;
                vm.isDayNo = false;
                vm.isBatch = true;
                vm.isStyleEx = true;
                vm.isCheckRoll = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4005') { //RPT-4005" Dyes Chemical Date Wise Machine Production

                vm.isCompany = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;

                vm.isBuyer = true;
                vm.isMachine = true;
                vm.isColorGroup = true;
                vm.isColor = true;
                vm.isOrder = true;
                vm.isLab = true;
                vm.isReceipe = true;
                vm.isDayNo = true;
                vm.isBatch = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4006') { //RPT-4006" Batch Cost Analysis

                vm.isCompany = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;

                vm.isBuyer = true;
                vm.isMachine = true;
                vm.isColorGroup = true;
                vm.isColor = true;
                vm.isOrder = true;
                vm.isLab = true;
                vm.isReceipe = true;
                vm.isDayNo = true;
                vm.isBatch = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4007') { //RPT-4007" Daily Production & Costing

                vm.isCompany = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;

                vm.isBuyer = true;
                vm.isMachine = true;
                vm.isColorGroup = true;
                vm.isColor = true;
                vm.isOrder = true;
                vm.isLab = true;
                vm.isReceipe = true;
                vm.isDayNo = true;
                vm.isBatch = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4008') { //RPT-4008" Store Transfer Delivery Challan

                vm.isIssRefNo = true;
                vm.isRDLC = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4009') { //RPT-4009" Loan Delivery Challan

                vm.isRDLC = true;
                vm.isIssRefNo2 = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4012') { //RPT-4012" Challan Wise Loan Receive & Delivery

                vm.isItemSource = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;

                vm.isSupplier = true;
                vm.isItem = true;
            }

            else if (vm.form.REPORT_CODE == 'RPT-4013') { //RPT-4012" Daily Receive

                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;
                vm.isSupplier = true;
                vm.isPayMethod = true;
                vm.isItem = true;
            }

            else if (vm.form.REPORT_CODE == 'RPT-4014') { //RPT-4012" Daily Receive
                vm.isStore = true;
                vm.isItemCategory = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isCompany = true;

                vm.isRDLC = true;

                vm.isItem = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4016') { //RPT-4016" Stock Ledger
                vm.isStore = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;
                vm.isCompany = true;
                vm.isItem = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4017') { //RPT-4017" Loan Ledger Summary

                vm.isFormDate = true;
                vm.isToDate = true;

                vm.isSupplier = true;
                vm.isItem = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4018') { //RPT-4018" Loan Ledger Details

                vm.isFormDate = true;
                vm.isToDate = true;

                vm.isSupplier = true;
                vm.isItem = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4019') { //RPT-4019"

                //vm.isRDLC = true;
                vm.isFormDate = true;
                vm.isToDate = true;

                vm.isBuyerAcGrp = true;
                vm.isBatch = true;
                vm.isStyleEx = true;
                vm.form.FROM_DATE = "";
                vm.form.TO_DATE = "";
            }
            else if (vm.form.REPORT_CODE == 'RPT-4020') { //RPT-4020" Buyer Style Without Lab-dip Recipe

                vm.isRDLC = true;

                vm.isBuyerAc = true;
                vm.isColor = true;
                vm.isStyle = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4021') { //RPT-4020" Daily Production Issue

                vm.isCompany = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                //vm.isToDate = true;

                vm.isItemCategory = true;
                //vm.isFormDate = true;
                vm.isRDLC = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4023') { //RPT-4023" Daily Statement of Reprocess

                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4024') { //RPT-4023" Daily Batch Wise Cost Analysis

                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;

                vm.isBuyer = true;
                vm.isMachine = true;
                vm.isColorGroup = true;
                vm.isColor = true;
                vm.isOrder = true;
                vm.isLab = true;
                vm.isReceipe = true;
                vm.isDayNo = true;
                vm.isBatch = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4025') { //RPT-4025" Buyer Wise Production & Costing

                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;
                vm.isCompany = true;
                vm.isBuyer = true;
                vm.isMachine = true;
                vm.isColorGroup = true;
                vm.isColor = true;
                vm.isOrder = true;
                vm.isLab = true;
                vm.isReceipe = true;
                vm.isDayNo = true;
                vm.isBatch = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4026') { //RPT-4026" Machine Loading Performance

                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;

                vm.isMachine = true;
                vm.isDayNo = true;
                vm.isBatch = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4027') { //RPT-4027" Batch Info Without Production

                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;

                vm.isCompany = true;
                vm.isBuyer = true;
                vm.isMachine = true;
                vm.isColorGroup = true;
                vm.isColor = true;
                vm.isOrder = true;
                vm.isReceipe = true;
                vm.isBatch = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-5001') { //RPT-5001" Daily Production Report of Dyeing Finishing

                vm.isFormDate = true;
                vm.isToDate = false;

                vm.isDFMachine = true;
                vm.isShift = true;
                vm.isDFProcess = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4028') { //RPT-4028" Delivery Challan for Dyeing & Finishing

                vm.isRDLC = true;
                vm.isBatch = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4030') { //RPT-4030" Batch Cost Breakdown for PC/CVC

                vm.isCompany = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;

                vm.isBuyer = true;
                vm.isBuyerAcGrpList = true;
                vm.isMachine = true;
                vm.isColorGroup = true;
                vm.isColor = true;
                vm.isOrder = true;
                vm.isLab = true;
                vm.isReceipe = true;
                vm.isDayNo = true;
                vm.isBatch = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4031') { //RPT-4031" Batch Cost Breakdown for Fiber Composition

                vm.isCompany = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;

                vm.isBuyer = true;
                vm.isBuyerAcGrpList = true;
                vm.isMachine = true;
                vm.isColorGroup = true;
                vm.isColor = true;
                vm.isOrder = true;
                vm.isLab = true;
                vm.isReceipe = true;
                vm.isDayNo = true;
                vm.isBatch = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4034') { //RPT-4034" Dyeing Check Roll Status Report

                vm.isCompany = true;
                vm.isRDLC = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isBuyerAcGrp = true;
                vm.isStyleEx = true;
                vm.isBatch = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4035') { //RPT-4035" Dyeing QC Production Report

                vm.isCompany = true;
                vm.isRDLC = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isBuyerAc = true;
                vm.isBuyerAcGrp = true;
                vm.isStyleEx = true;
                vm.isBatch = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4036') { //RPT-4036" Dyes/Chemical Stock Taking Report

                vm.isRDLC = true;
                vm.isStore = true;
                vm.isItemCategory = true;
                vm.isItemSource = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isCompany = true;
                vm.isItem = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4037') { //RPT-4037" Dyes Chemical Daily Dyeing Production With QC

                vm.isCompany = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;

                vm.isBuyerAcGrp = true;
                vm.isMachine = true;
                vm.isColorGroup = true;
                vm.isColor = true;
                vm.isOrder = false;
                vm.isLab = true;
                vm.isReceipe = true;
                vm.isDayNo = false;
                vm.isBatch = true;
                vm.isStyleEx = true;
                vm.isCheckRoll = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4038') { //RPT-4038" Dyes Chemical Loan Summary

                vm.isCompany = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;

                vm.isSupplier = true;
                vm.isItem = true;
                vm.isItemCategory = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4039') { //RPT-4039" Dyeing Reporcess History

                vm.isCompany = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;

                vm.isBuyerAcGrp = true;
                vm.isStyleEx = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4040') { //RPT-4040" Order and Color wise Dyeing Production Statement

                //vm.isFormDate = true;
                //vm.isToDate = true;
                vm.isRDLC = true;
                vm.isProdCat = true;
                vm.isBuyerAcGrp = true;
                vm.isStyleEx = true;
                vm.isOrder = true;

                vm.isShipMonth = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4042') { //RPT-4042" Manual Batch List

                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;

                vm.isCompany = true;
                vm.isProdCat = true;
                vm.isBuyer = true;
                vm.isMachine = true;
                vm.isColorGroup = true;
                vm.isColor = true;
                vm.isBatch = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4044') { //RPT-4044" Challan wise Dyes Chemical Receive

                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;

                vm.isCompany = true;
                vm.isStore = true;
                vm.isItem = true;
                vm.isChallanNo = true;
                vm.isItemSource = true;
                vm.isPayMethod = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4047') { //RPT-4047" Dyes & Chemical Total Receive

                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;

                vm.isCompany = true;
                vm.isStore = true;
                vm.isItem = true;
                vm.isChallanNo = true;
                vm.isItemCategory = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4048') { //RPT-4048" Dyes & Chemical Challan Wise Transfer

                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;

                vm.isCompany = true;
                vm.isStore = true;
                vm.isItem = true;
                vm.isChallanNo = true;
                vm.isItemCategory = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-4049') { //RPT-4049" Dyes/Chemical Source Stock Taking Report

                vm.isRDLC = true;
                vm.isStore = true;
                vm.isItemCategory = true;
                vm.isItemSourc = true;
                vm.isItemSource = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isCompany = true;
                vm.isItem = true;
            }

        });

        function styleListByBuyer(MC_BUYER_ID) {

            if (!MC_BUYER_ID) {
                vm.styleList = new kendo.data.DataSource({
                    data: []
                })
                return;
            }

            DyeingDataService.getDataByFullUrl('/api/mrc/StyleH/BuyerWiseStyleListData/' + MC_BUYER_ID).then(function (sdata) {

                vm.styleList = new kendo.data.DataSource({
                    data: sdata
                })
            })
        };

        vm.getStyleListByBA = function () {
            if (vm.form.MC_BYR_ACC_ID > 0)
                return DyeingDataService.getDataByFullUrl('/api/mrc/StyleH/BuyerAccWiseStyleListData/' + vm.form.MC_BYR_ACC_ID).then(function (res) {
                    vm.styleList = new kendo.data.DataSource({
                        data: res
                    });
                });
            else
                vm.styleList = new kendo.data.DataSource({
                    data: []
                });

        };

        vm.styleListByBuyerID = function (MC_BUYER_ID) {

            if (!MC_BUYER_ID) {
                vm.styleList = new kendo.data.DataSource({
                    data: []
                })
                return;
            }

            DyeingDataService.getDataByFullUrl('/api/mrc/StyleH/BuyerWiseStyleListData/' + MC_BUYER_ID).then(function (sdata) {

                vm.styleList = new kendo.data.DataSource({
                    data: sdata
                })
            })
        };

        vm.preview = function () {
            if (fnValidate() == true) {
                var form = document.createElement("form");
                form.setAttribute("method", "post");
                form.setAttribute("action", '/api/Dye/DyeReport/PreviewReport');
                form.setAttribute("target", '_blank');

                if (vm.isBuyerAcGrpList == true)
                    vm.form['MC_BYR_ACC_GRP_LST'] = !vm.form.MC_BYR_ACC_GRP_LST ? null : vm.form.MC_BYR_ACC_GRP_LST.join(',');

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
            }
        };


        vm.previewRDLC = function () {
            if (fnValidate() == true) {
                var form = document.createElement("form");
                form.setAttribute("method", "post");
                form.setAttribute("action", '/api/Dye/DyeReport/PreviewReportRDLC');
                form.setAttribute("target", '_blank');

                if (vm.isBuyerAcGrpList == true)
                    vm.form['MC_BYR_ACC_GRP_LST'] = !vm.form.MC_BYR_ACC_GRP_LST ? null : vm.form.MC_BYR_ACC_GRP_LST.join(',');

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
            }
        };

        vm.previewCB = function () {

            if (fnValidate() == true) {
                var form = document.createElement("form");
                form.setAttribute("method", "post");
                form.setAttribute("action", '/api/Dye/DyeReport/PreviewReportRDLC');
                form.setAttribute("target", '_blank');

                vm.form.FROM_DATE = $filter('date')(vm.form.FROM_DATE, vm.dtFormat);
                vm.form.TO_DATE = $filter('date')(vm.form.TO_DATE, vm.dtFormat);

                //vm.form.REPORT_CODE = "RPT-4007";

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
            }
        };


    }



})();