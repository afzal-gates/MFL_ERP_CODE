
(function () {
    'use strict';
    angular.module('multitex.inventory').controller('InvReportController', ['logger', 'config', 'InventoryDataService', 'InvRptData', '$q', '$scope', '$http', '$sessionStorage', '$filter', 'access_token', 'cur_user_id', InvReportController]);
    function InvReportController(logger, config, InventoryDataService, InvRptData, $q, $scope, $http, $sessionStorage, $filter, access_token, cur_user_id) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.dtFormat = config.appDateFormat;
        vm.showSplash = true;

        vm.insert = true;
        vm.today = new Date();
        vm.rptList = angular.copy(InvRptData);


        vm.form = {
            REPORT_CODE: 'RPT-3000', IS_EXCEL_FORMAT: 'N', FROM_DATE: $filter('date')(vm.today, vm.dtFormat), TO_DATE: $filter('date')(vm.today, vm.dtFormat),
            SCM_SUPPLIER_ID: 0
        };

        function activate() {
            var promise = [getOfficeList(), getMcTypeList(), getStoreList(), GetScProgList(), getBuyerAcGrpList(),
                getByrAccWiseStyleExtList(), getOrderColorList(), getGreyQcUserData(),
                getProdCatSourceList(), getReqTypeList(), getYrnIssRtnClallanList(), getSupplierList(), getShipmentMonth(), getAllItemList(),
                GetScOrderList(), GetScIssProgList(), GetQcStatusTypeList(), GetColorList(), getYarnItemList(), GetAllCompanyList(),
                GetCategoryBrandList(), GetYarnSupplierList(), getYarnCountList(), getCotnTypeList(), getScoGreyFabRcvChlnList(),
                getShiftList(), getProdTypeList(), getFabricTypeList(), GetItemCategoryList(), getMachineList(), getKnitCardList(),
                getKnitingType()
            ];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        function getKnitCardList() {
            vm.multiCardDataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                schema: {
                    data: "data",
                    total: "total"
                },
                pageSize: 50,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KnitPlan/getKnitCardList?';//pJOB_CRD_NO=' + (vm.multiCard.KNT_JOB_CRD_H_ID_LST || $stateParams.pSCM_SUPPLIER_ID || 0);

                        url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                        url += InventoryDataService.kFilterStr2QueryParam(params.filter);

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });

                    }
                }
            });
        }

        function getMcTypeList() {
            vm.mcTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MC_TYPE_NAME_EN",
                dataValueField: "KNT_MC_TYPE_ID"
            };

            return vm.mcTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        InventoryDataService.getDataByFullUrl('/api/knit/KnitCommon/KntMcTypeListByUser').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        }

        function getMachineList() {
            return vm.machineList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        InventoryDataService.getDataByFullUrl('/api/Knit/KnitCommon/GetMachineList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
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


        function getGreyQcUserData() {

            return vm.greyQcUserList = {
                optionLabel: "--Select Grey QC User --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getDataByFullUrl('/api/knit/KnitPlanRollInsp/getQCUserList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "EMP_FULL_NAME_EN",
                dataValueField: "HR_EMPLOYEE_ID"
            };

        }

        function getShipmentMonth() {

            return vm.shipMonthList = {
                optionLabel: "--Select Month--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectShipmentMonth/' + (vm.form.MC_BYR_ACC_ID || 0) + '/0/' + (vm.form.RF_FAB_PROD_CAT_ID || 0)).then(function (res) {
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
                            InventoryDataService.getDataByFullUrl('/api/Common/FabricTypeList').then(function (res) {
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
                            InventoryDataService.getDataByFullUrl('/api/knit/KntMachinOprAssign/GetKnitScheduleList').then(function (res) {
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
                            InventoryDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=2').then(function (res) {
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

        function getAllItemList() {
            return vm.ItemList = {
                optionLabel: "--Select Item--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getDataByFullUrl('/api/inv/Item/ItemListWithUserID').then(function (res) {
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
                            InventoryDataService.LookupListData(62).then(function (res) {
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
                            InventoryDataService.getDataByFullUrl('/api/Common/YarnCountList').then(function (res) {
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

        function GetAllCompanyList() {

            return vm.companyList = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
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
                            InventoryDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
                                //InventoryDataService.LookupListData(92).then(function (res) {
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

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
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
                            InventoryDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
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

        function GetItemCategoryList() {

            return vm.categoryList = {
                optionLabel: "-- Select Category --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {
                                //var list = _.filter(res, { 'INV_ITEM_CAT_ID': 2 });
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID"
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
                            InventoryDataService.getDataByFullUrl('/api/knit/KntScChlnRcv/GetQcStatusTypeList?pKNT_QC_STS_TYPE_ID_LST=1,3,4').then(function (res) {
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
                            InventoryDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COLOR_NAME_EN",
                dataValueField: "MC_COLOR_ID"
            };
        }

        function getKnitingType() {

            return vm.knitingTypeList = {
                optionLabel: "--Select Type--",
                //filter: "contains",
                autoBind: true,
                dataSource: {
                    data: [{ IS_FLAT_CIR: "C", KNIT_TYPE_NAME: "Circular" }, { IS_FLAT_CIR: "F", KNIT_TYPE_NAME: "Flat" }]
                },
                dataTextField: "KNIT_TYPE_NAME",
                dataValueField: "IS_FLAT_CIR"
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
                            InventoryDataService.getDataByFullUrl('/api/mrc/BuyerAcc/SelectAll').then(function (res) {
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
                            url += InventoryDataService.kFilterStr2QueryParam(params.filter);
                            url += '&pMC_STYLE_H_EXT_ID';

                            return InventoryDataService.getDataByFullUrl(url).then(function (res) {
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
                            url += InventoryDataService.kFilterStr2QueryParam(params.filter);
                            url += '&pMC_STYLE_H_EXT_ID';

                            return InventoryDataService.getDataByFullUrl(url).then(function (res) {
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
                            InventoryDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (res) {
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
                dataValueField: "MC_STYLE_H_EXT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
                    vm.form.MC_ORDER_H_ID_LST = item.MC_ORDER_H_ID;

                    vm.orderColorListDataSource.read();

                }
            };

            return vm.styleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Order/GetOrderList?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : ''); //pMC_BYR_ACC_ID=' + ((vm.form.MC_BYR_ACC_ID > 0) ? vm.form.MC_BYR_ACC_ID : 0);
                        //url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                        url += InventoryDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getOrderColorList() {
            vm.orderColorListOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COLOR_NAME_EN",
                dataValueField: "MC_COLOR_ID"
            };

            return vm.orderColorListDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        InventoryDataService.getDataByFullUrl('/api/mrc/ColourMaster/ColourMappDataByBuyerId/' + (vm.form.MC_STYLE_H_ID || 0)).then(function (res) {
                            e.success(res);
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
                            InventoryDataService.getDataByFullUrl('/api/common/GetReqType').then(function (res) {
                                //InventoryDataService.LookupListData(88).then(function (res) {
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
                        url += InventoryDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
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
                        InventoryDataService.getDataByFullUrl('/api/purchase/supplierprofile/SelectAllByCat/2').then(function (res) {
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
                        InventoryDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
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

                        url += InventoryDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
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
            };

            return vm.scProgDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KntScRcv/GetScRcvProgramList?pSCM_SUPPLIER_ID=' + vm.form.SCM_SUPPLIER_ID;
                        url += InventoryDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
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

                        url += InventoryDataService.kFilterStr2QueryParam(params.filter);
                        console.log(url);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
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
                        url += InventoryDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res.data);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        $scope.scFabDelvChallanAuto = function (val) {
            return InventoryDataService.getDataByFullUrl('/api/knit/KntGreyFabDelv/ScGreyFabDelvChallanAuto?pCHALAN_NO=' + val).then(function (res) {
                return res;
            }, function (err) {
                console.log(err);
            });

        };

        $scope.onScFabDelvChallanSelectItem = function (item) {
            console.log(item);
            vm.form.SC_FAB_DELV_CHALAN_NO = item.CHALAN_NO;
        }

        $scope.scYrnIssChallanAuto = function (val) {
            return InventoryDataService.getDataByFullUrl('/api/knit/YarnIssue/YarnIssueChallanAuto?pCHALAN_NO=' + val).then(function (res) {
                return res;
            }, function (err) {
                console.log(err);
            });
        };

        $scope.onScYrnIssChallanSelectItem = function (item) {
            console.log(item);
            vm.form.ISS_CHALAN_NO = item.ISS_CHALAN_NO;
        }

        vm.rptOnChange = function (itm, idx) {
            //alert(idx);           
            console.log(InvRptData[idx]);
            var rptList = [];
            rptList = angular.copy(InvRptData);
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
            vm.isFromDate = false;
            vm.isToDate = false;
            vm.isFromDateTime = false;
            vm.isToDateTime = false;
            vm.isQcStatus = false;
            vm.isColor = false;
            vm.isOrderColor = false;
            vm.isYarnSupplier = false;
            vm.isYC = false;
            vm.isCT = false;
            vm.isYarnItem = false;
            vm.isItem = false;
            vm.isShipMonth = false;
            vm.isNoOfCount = false;
            vm.isScoGreyFabRcvChln = false;
            vm.isJobCard = false;
            vm.isMultiKnitCard = false;
            vm.isShift = false;

            vm.isFabRoll = false;
            vm.isFabType = false;
            vm.isSubContactParty = false;
            vm.isProdType = false;
            vm.isFabOrderYd = false;
            vm.isFabOrderSolid = false;
            vm.isCat = false;
            vm.isCompany = false;
            vm.isOffice = false;
            vm.isMcType = false;
            vm.isMachine = false;
            vm.isKnitingType = false;
            vm.isGreyQcUser = false;

            if (vm.form.REPORT_CODE == 'RPT-3000') { //RPT-3000" Item List Report                
                vm.isSupplier = true;
                vm.isScIssProgNo = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3001') { //RPT-3001" General Stock Taking Report                
                vm.isFromDate = true;
                vm.isToDate = true;
                vm.isCompany = true;
                vm.isStore = true;
                vm.isCat = true;
                vm.isItem = true;
                vm.isRDLC = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3002') { //RPT-3002" General Item Current Stock Report                
                vm.isCompany = true;
                vm.isStore = true;
                vm.isCat = true;
                vm.isRDLC = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-3004') { //RPT-3004" General Item Stock Ledger Report        
                vm.isStore = true;
                vm.isFromDate = true;
                vm.isToDate = true;
                vm.isRDLC = true;
                vm.isCompany = true;
                vm.isItem = true;
            }
        });


        vm.preview = function () {

            var url;
            if (vm.isRDLC == true) {
                url = '/api/Inv/InvReport/PreviewReportRDLC';
            }
            else {
                url = '/api/Inv/InvReport/PreviewReport';
            }

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.FROM_DATE = $filter('date')(vm.form.FROM_DATE, vm.dtFormat);
            vm.form.TO_DATE = $filter('date')(vm.form.TO_DATE, vm.dtFormat);

            vm.form.KNT_JOB_CRD_H_ID_LST = null;
            if (vm.form.KNT_JOB_CRD_LIST) {
                vm.form.KNT_JOB_CRD_H_ID_LST = vm.form.KNT_JOB_CRD_LIST.join(',');
            }

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