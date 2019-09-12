(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DyeingFinishingReportController', ['logger', 'config', 'DyeingDataService', 'dyeRptData', '$q', '$scope', '$http', '$sessionStorage', '$filter', 'access_token', 'cur_user_id', DyeingFinishingReportController]);
    function DyeingFinishingReportController(logger, config, DyeingDataService, dyeRptData, $q, $scope, $http, $sessionStorage, $filter, access_token, cur_user_id) {

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
                getByrAccWiseStyleExtList(), getShiftList(), GetProcessList(), GetMachineList()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);

                vm.showSplash = false;
            });
        }

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



        vm.partyList = {
            optionLabel: "-- Select Party --",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return DyeingDataService.getDataByFullUrl('/api/purchase/SupplierProfile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
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
                dataTextField: "FAB_PROD_CAT_NAME",
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

        $scope.ScChallanNoAuto = function (val) {

            return DyeingDataService.getDataByFullUrl('/api/Dye/DfScProgram/SelectByChallanNo?pCHALAN_NO=' + val).then(function (res) {
                return res;
            });

        };

        $scope.onSelectScChallan = function (item) {

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

            vm.isStore = false;
            vm.isItemCategory = false;
            vm.isIssRefNo = false;
            vm.isItemSourc = false;
            vm.isFormDate = false;
            vm.isToDate = false;
            vm.isRDLC = false;
            vm.isIssRefNo2 = false;

            vm.isBuyer = false;
            vm.isBuyerAc = false;
            vm.isBuyerAcGrp = false;
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

            vm.isDFMachine = false;
            vm.isShift = false;
            vm.isChallanNo = false;
            vm.isDFProcess = false;


            if (vm.form.REPORT_CODE == 'RPT-6000') { //RPT-6000 Daily Production Report of Dyeing Finishing
                vm.isFormDate = true;
                vm.isToDate = false;
                vm.isRDLC = true;

                vm.isDFMachine = true;
                vm.isShift = true;
                vm.isDFProcess = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-6001') { //RPT-6001" Dyeing Finishing S/C Program Delivery Report
                vm.isRDLC = true;
                vm.isChallanNo = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-6002') { //RPT-6002" Party wise S/C Dyeing & Dyeing Finishing Production Summary
                vm.isRDLC = true;
                vm.isFormDate = true;
                vm.isScParty = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-6003') { //RPT-6003" Party wise S/C Dyeing & Dyeing Finishing Production Register
                vm.isRDLC = true;
                vm.isFormDate = true;
                vm.isToDate = true;
                vm.isScParty = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-6012') { //RPT-6012" S/C Program Delivery & Receive Ledger
                vm.isRDLC = true;
                vm.isFormDate = true;
                //vm.isToDate = true;
                vm.isScParty = true;
                vm.isBuyerAcGrp = true;
                vm.isStyleEx = true;
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