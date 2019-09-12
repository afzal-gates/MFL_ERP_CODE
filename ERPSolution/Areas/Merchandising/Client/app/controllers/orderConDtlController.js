(function () {
    'use strict';
    angular.module('multitex.mrc').controller('OrderConDtlController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'formData', '$filter', '$modal', '$timeout', 'DateShareService', 'Dialog', '$http', OrderConDtlController]);
    function OrderConDtlController($q, config, MrcDataService, $stateParams, $state, $scope, formData, $filter, $modal, $timeout, DateShareService, Dialog, $http) {


        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.errors = null;

        var key = 'MC_ORDER_H_ID';

        vm.today = new Date();

        vm.StyleData = $scope.$parent.StyleData;

        vm.StyleForm = $scope.$parent.Style;
        vm.isWorkStyleDropdown = false;
        vm.isWorkStyleText = true;
        vm.param = $stateParams;



        activate();
        function activate() {
            var promise = [OrderStatusList(), getRF_CurrencyList(), getMOUList(), getWorkStyleDD()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        $scope.$watchGroup(['vm.form.ORDER_NO', 'vm.form.MC_ORDER_H_ID'], function (newVal, oldVal) {
            if (newVal[0] && newVal[1]) {
                if (newVal[0] && (newVal[1] || 0) < 1) {
                    return MrcDataService.getDataByUrl('/Order/OrderListByOrderNo/' + newVal[0] + '?pLK_ORD_STATUS_ID=363&pIS_PROV=N')
                           .then(function (res) {
                               vm.ordListByOrder = res;
                           }
                   );
                } else {
                    vm.ordListByOrder = [];
                }
            }

        });


        $scope.templateOrderStatus = '<span class="#: LOOKUP_DATA_ID==365 ? \"k-state-disabled\": \"\"#">#=data.LK_DATA_NAME_EN#</span>';
        $scope.onSelectIOrderStatus = function (e) {
            $(".k-state-disabled").parent().click(false);
        };

        vm.sendEmailNotification = function (pMC_ORDER_H_ID) {
               Dialog.confirm('Sending Mail...', 'Are You Sure ?', ['Yes', 'No'])
               .then(function () {
                   return $http({
                       method: 'post',
                       url: '/Merchandising/Mrc/FireOrderConfirmMail',
                       data: { pMC_ORDER_H_ID: pMC_ORDER_H_ID }
                   }).then(function (res) {
                       console.log(res);
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
               });

        };



        vm.companyOpt = {
            optionLabel: "-- All --",
            filter: "contains",
            autoBind: true,
            dataTextField: "COMP_NAME_EN",
            dataValueField: "HR_COMPANY_ID",
            dataSource: {
                transport: {
                    read: function (e) {
                        MrcDataService.getDataByFullUrl('/api/pln/GmtLineLoad/getCompanyData').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            },
            change: function (e) {
                var dataItem = this.dataItem(e.item);
                return vm.officeDs = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/pln/GmtLineLoad/getOfficeList?pHR_COMPANY_ID=' + (dataItem.HR_COMPANY_ID || formData.HR_COMPANY_ID || '')).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                });
            }
        };

        vm.countryList = {
            placeholder: "--Select Country--",
            filter: "startswith",
            valuePrimitive: true,
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        MrcDataService.GetAllOthers('/api/common/GetCountryList').then(function (res) {
                            console.log(res);
                            e.success(res);
                        });
                    }
                },
            },
            dataTextField: "COUNTRY_NAME_EN",
            dataValueField: "HR_COUNTRY_ID"
        };

        vm.officeOpt = {
            optionLabel: "-- All --",
            filter: "contains",
            autoBind: true,
            dataTextField: "OFFICE_NAME_EN",
            dataValueField: "HR_OFFICE_ID"
        };

        vm.officeDs = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    MrcDataService.getDataByFullUrl('/api/pln/GmtLineLoad/getOfficeList?pHR_COMPANY_ID=' + (formData.HR_COMPANY_ID || '')).then(function (res) {
                        e.success(res);
                    });
                }
            }
        });





        $scope.$on('$viewContentLoaded', function () {
            vm.form = formData[key] ? formData : {
                LK_ORD_STATUS_ID: 363, QTY_MOU_ID: 1, RF_CURRENCY_ID: 2, IS_MULTI_SHIP_DT: 'N', ORD_CONF_DT: vm.today,
                itmsOrdShipDT: [
                       { MC_ORDER_SHIP_ID: 0, MC_ORDER_H_ID: 0, SHIP_DT: $filter('date')(vm.today, vm.dtFormat), SHIP_DESC: 'Shipment-1' }
                ], HR_COMPANY_ID: 1, PROD_UNIT_ID: 2
            };
            if ($stateParams.MC_STYLE_H_ID > 0 && $stateParams.MC_ORDER_H_ID < 1) {
                $scope.$parent.setLK_ORD_TYPE_ID(174);
            } else {
                $scope.$parent.setLK_ORD_TYPE_ID(formData['LK_ORD_TYPE_ID'] ? formData.LK_ORD_TYPE_ID : 175);
            }
        });


        $scope.$watch('vm.form', function (newVal, oldVal) {
            newVal['OrderFormValid'] = $scope.OrderConfirmationForm.$valid;
            DateShareService.shared.data = angular.copy(newVal);
        }, true);



        vm.sizeShipmentDateOpened = [];
        vm.sizeShipmentDateOpen = function ($event, index) {
            if (vm.form.IS_ORD_FINALIZED != 'Y') {
                $event.preventDefault();
                $event.stopPropagation();

                vm.sizeShipmentDateOpened[index] = true;
            }
        };



        $scope.$watchGroup(['vm.StyleData.STYLE_NO', 'vm.StyleData.LK_ORD_TYPE_ID'], function (newVal, oldVal) {

            if (!formData[key] || formData[key] < 1) {
                if ((newVal[1] == 175 || newVal[1] == 311) && !vm.StyleData.STYL_EXT_NO) { //New & No Extention
                    vm.form['MC_STYLE_H_EXT_ID'] = vm.StyleData.MC_STYLE_H_EXT_ID;
                    vm.form['IS_N_R'] = 'N';
                    vm.form['HAS_EXT'] = 'N';
                    vm.isWorkStyleDropdown = false;
                    vm.isWorkStyleText = true;
                }
                else if ((newVal[1] == 175 || newVal[1] == 311) && vm.StyleData.STYL_EXT_NO) { //New & With Extention
                    vm.form['IS_N_R'] = 'N';
                    vm.form['HAS_EXT'] = 'N';
                    vm.isWorkStyleDropdown = true;
                    vm.isWorkStyleText = false;
                }
                else if (newVal[1] == 174) { // Repeat
                    vm.form['WORK_STYLE_NO'] = newVal[0];
                    vm.form['MC_STYLE_H_EXT_ID'] = -1;
                    vm.form['IS_N_R'] = 'R';
                    vm.form['HAS_EXT'] = 'N';
                    vm.isWorkStyleDropdown = false;
                    vm.isWorkStyleText = true;

                }

            }

            $timeout(function () {
                if (newVal[1] == 311) {
                    vm.form['LK_ORD_STATUS_ID'] = 208;
                } else {
                    vm.form['LK_ORD_STATUS_ID'] = 363;
                }
            });

        });

        function getWorkStyleDD() {
            if (MrcDataService.parseXmlString(vm.StyleData.EXT_XML).length > 0) {
                vm.isWorkStyleDropdown = true;
                vm.isWorkStyleText = false;
            }

            return vm.WorkStyleDD = {
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success(MrcDataService.parseXmlString(vm.StyleData.EXT_XML));
                        }
                    }
                },
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_EXT_ID"
            };
        };


        vm.generateExtionNumber = function (IS_AUTO) {
            if (IS_AUTO) {

                return MrcDataService.getDataByUrl('/StyleH/getStyleExtAuto/' + vm.StyleData.MC_STYLE_H_ID).then(function (ext) {
                    if (ext) {
                        vm.form['WORK_STYLE_NO'] = vm.StyleData.STYLE_NO + '-' + ext;
                    }

                }, function (err) {
                    console.log(err);
                    vm.form['WORK_STYLE_NO'] = vm.StyleData.STYLE_NO;
                });


            } else {
                vm.form['WORK_STYLE_NO'] = vm.StyleData.STYLE_NO;
            }
        };


        //$scope.$watch('vm.form.MC_TNA_TMPLT_H_ID', function (newVal, oldVal) {

        //    if (newVal == oldVal) {
        //        vm.form['TNA_GENERATION_DISABLED'] = false;
        //    } else {
        //        vm.form['TNA_GENERATION_DISABLED'] = true;
        //    }


        //}, true);


        $scope.$watch('vm.StyleData.STYLE_NO', function (newVal, oldVal) {
            if (formData[key]< 1) {
                vm.form['WORK_STYLE_NO'] = newVal;
            }
        });

        $scope.$watch('vm.StyleData.QTY_MOU_ID', function (newVal, oldVal) {
            if (formData[key]< 1) {
                vm.form['QTY_MOU_ID'] = newVal;
            }
        });

        $scope.$watch('vm.StyleData.HR_COUNTRY_ID_LST', function (newVal, oldVal) {
           
            if (formData[key] < 1) {
                vm.form['HR_COUNTRY_ID_LST'] = newVal;
            }
        }, true);


        $scope.$watchGroup(['vm.form.TOT_ORD_QTY', 'vm.form.UNIT_PRICE'], function (newVal, oldVal) {
            if (newVal[0] && newVal[1]) {
                if (_.isNumber(newVal[0]) && _.isNumber(newVal[1])) {
                    vm.form['TOT_ORD_VALUE'] = newVal[0] * newVal[1];
                }
            }

        });




        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        $scope.TGT_ORD_DOC_RCV_DTopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.TGT_ORD_DOC_RCV_DTopened = true;
        }

        $scope.ConfirmDate_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.ConfirmDate_LNopened = true;
        }



        vm.shipmentDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.shipmentDateOpened = true;
        };
        vm.openDropDown = function (dropDownId) {
            var dropdownlist = $(dropDownId).data("kendoDropDownList");
            dropdownlist.open();
        };


        function getBuyerAcc() {
            return vm.buyerAccList = {
                optionLabel: "-- Select Buyer Acc --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/BuyerAcc/SelectAll').then(function (res) {
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

        function getBuyerListData() {
            return vm.buyerList = {
                optionLabel: "-- Select Buyer --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/buyer/BuyerByUserList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataBound: function (e) {
                    var dataItem = this.dataItem(e.item);
                    var vParentID = dataItem.MC_BUYER_ID;

                    if (vParentID != null && vParentID != "") {
                        MrcDataService.getBuyerWiseAccByUserList(vParentID).then(function (res) {
                            $("#MC_BYR_ACC_ID").kendoDropDownList({
                                autoBind: false,
                                optionLabel: "-- Select Buyer Acc --",
                                dataTextField: "BYR_ACC_NAME_EN",
                                dataValueField: "MC_BYR_ACC_ID",
                                dataSource: res,
                                filter: "startswith"
                            });

                        });


                        MrcDataService.GetAllOthers('/api/mrc/StyleH/BuyerWiseStyleListData/' + vParentID).then(function (res) {
                            $("#MC_STYLE_H_ID").kendoDropDownList({
                                autoBind: false,
                                optionLabel: "-- Select Style --",
                                dataTextField: "STYLE_NO",
                                dataValueField: "MC_STYLE_H_ID",
                                dataSource: res,
                                filter: "startswith"
                            });

                            MrcDataService.GetAllOthers('/api/mrc/StyleDItem/StyleDtlItemList/' + $stateParams.pOrderObj.MC_STYLE_H_ID + '/' + 'A').then(function (res) { //Last level item (All)               
                                vm.itemListData = res;
                            });

                        });

                    };

                },
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    var vParentID = dataItem.MC_BUYER_ID;

                    if (vParentID != null && vParentID != "") {

                        MrcDataService.getBuyerWiseAccByUserList(vParentID).then(function (res) {
                            $("#MC_BYR_ACC_ID").kendoDropDownList({
                                autoBind: false,
                                optionLabel: "-- Select Buyer Acc --",
                                dataTextField: "BYR_ACC_NAME_EN",
                                dataValueField: "MC_BYR_ACC_ID",
                                dataSource: res,
                                filter: "startswith"
                            });
                            MrcDataService.GetAllOthers('/api/mrc/StyleH/BuyerWiseStyleListData/' + vParentID).then(function (res) {
                                $("#MC_STYLE_H_ID").kendoDropDownList({
                                    autoBind: false,
                                    optionLabel: "-- Select Style --",
                                    dataTextField: "STYLE_NO",
                                    dataValueField: "MC_STYLE_H_ID",
                                    dataSource: res,
                                    filter: "startswith",
                                    select: function (e) {
                                        var dataItem = this.dataItem(e.item);
                                        var vStyleID = dataItem.MC_STYLE_H_ID;
                                        vm.form.STYLE_NO = dataItem.STYLE_NO;
                                        vm.form.STYLE_EXT_NO = dataItem.STYL_EXT_NO;

                                        MrcDataService.GetAllOthers('/api/mrc/StyleDItem/StyleDtlItemList/' + vStyleID + '/' + 'A').then(function (res) { //Last level item (All)
                                            vm.itemListData = res;
                                        });
                                    }
                                });

                            });

                        });

                    }
                },
                dataTextField: "BUYER_NAME_EN",
                dataValueField: "MC_BUYER_ID"
            };
        }


        //function getTnaTempList() {
        //    return vm.TnaTempList = {
        //        optionLabel: "-- TNA Template --",
        //        filter: "startswith",
        //        autoBind: true,
        //        template: '<span><h4 style="padding:0px;margin-top: 0px;margin-bottom: 0px;">#: data.TNA_TMPLT_CODE #</h4><p style="padding:0px;margin-top: 0px;margin-bottom: 0px;"> <small>#: data.REMARKS #</small></p></span>',
        //        dataSource: {
        //            transport: {
        //                read: function (e) {
        //                    MrcDataService.getDataByUrl('/StyleH/TnAList/OrderType/null/Buyer/null/LeadTime/null').then(function (res) {
        //                        e.success(res);
        //                    });
        //                }
        //            }
        //        },
        //        dataTextField: "TNA_TMPLT_CODE",
        //        dataValueField: "MC_TNA_TMPLT_H_ID"
        //    };
        //}

        vm.addRow = function (data, copyTo, index) {

            var copiedData = angular.copy(data);
            copiedData['MC_ORDER_SHIP_ID'] = -1;
            angular.forEach(copiedData.cap_itms, function (val) {
                val['MC_GMT_CAP_ITEM_ID'] = -1;
            });

            copyTo.push(copiedData);
        };
        vm.removeRow = function (index, removeFrom, master) {
            removeFrom.splice(index, 1);
        };
        vm.openModalCapacityBkStatus = function (pSHIP_DT) {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'CapacityBookingStatus.html',
                controller: function ($scope, $modalInstance, MrcDataService) {

                    MrcDataService.getDataByFullUrl('/api/pln/PlanCommon/getGmtPlanCapcityDshBrdDataWkShip?pSHIP_DT=' + new Date(pSHIP_DT).toISOString()).then(function (res) {
                        $scope.chartDatas = res;
                    });


                    $scope.cancel = function (data) {
                        $modalInstance.dismiss();
                    }
                },
                size: 'lg',
                windowClass: 'large-Modal'
            });

            modalInstance.result.then(function (data) {

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });

        };

        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        MrcDataService.getDataByUrl('/Order/OrderDataList/null/null/' + (vm.StyleData.MC_STYLE_H_ID || 0) + '/null/null').then(function (res) {
                            e.success(res);
                        })
                    }
                },
                pageSize: 10
            },
            filterable: true,
            selectable: "row",
            sortable: true,
            pageSize: 10,
            pageable: false,
            columns: [
                { field: "JOB_TRAC_NO", title: "Job #", type: "string", width: "80px" },
                { field: "WORK_STYLE_NO", title: "Style No # ", type: "string", width: "80px" },
                { field: "ORDER_NO", title: "PO #", type: "string", width: "80px" },
                {
                    title: "Action",
                    template: function () {
                        return "<a ui-sref='OrderCon.Dtl({MC_STYLE_H_ID:vm.StyleData.MC_STYLE_H_ID,MC_ORDER_H_ID:dataItem.MC_ORDER_H_ID})' class='btn btn-xs blue'><i class='fa fa-edit'> </i></a>";
                    },
                    width: "30px"
                }
            ]
        };



        function getDestPointList() {
            return vm.destPointList = {
                optionLabel: "-- Select Destination --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/DestnationPoint/SelectAll').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "DEST_POINT_NAME_EN",
                dataValueField: "MC_DEST_POINT_ID"
            };
        }

        function getOrderTypeList() {
            return MrcDataService.LookupListData(40).then(function (res) {
                vm.OrderTypeList = res;
                vm.form.LK_ORD_TYPE_ID = res[0].LOOKUP_DATA_ID;
            }, function (err) {
                console.log(err);
            });
        }

        function GetCountryList() {
            return vm.CountryList = {
                optionLabel: "-- Select Country --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetCountryList().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COUNTRY_NAME_EN",
                dataValueField: "HR_COUNTRY_ID"
            };
        };

        function OrderStatusList() {
            return vm.orderStatusList = {
                optionLabel: "-- Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(56).then(function (res) {
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

        function getRF_CurrencyList() {

            return vm.StyleRF_CurrencyList = {
                optionLabel: "-- Select --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/common/CurrencyList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "CURR_CODE",
                dataValueField: "RF_CURRENCY_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                }
            };
        };


        function getMOUList() {
            return vm.MOUList = {
                optionLabel: "--Qty Unit--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getMOUList().then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID"
            };
        }


        $scope.$watchGroup(['vm.form.ORD_CONF_DT', 'vm.form.SHIP_DT'], function (newVal, oldVal) {

            if (newVal[1] && newVal[0]) {
                var a = moment(newVal[1]);
                var b = moment(newVal[0]);
                vm.form.LEAD_TIME = a.diff(b, 'days');
            }

        });



        vm.calculateOq = function (data) {
            if (data.IS_MULTI_SHIP_DT == "N") {
                data['TOT_ORD_QTY'] = _.sumBy(data.cap_itms, function (o) { return (o.ORDER_QTY || 0); });
            } else if (data.IS_MULTI_SHIP_DT == "Y") {
                data['TOT_ORD_QTY'] = _.sumBy(

                      data.itmsOrdShipDT.map(function(o){
                          return {
                              ORDER_QTY :_.sumBy(o.cap_itms, function (o) { return (o.ORDER_QTY || 0); })
                          }
                      })
                    ,

                    function (o) { return (o.ORDER_QTY || 0); });
            }
          
        };

        vm.submitData = function (data, token) {

            var RawData = angular.copy(data);

            RawData['ORD_CONF_DT'] = $filter('date')(RawData.ORD_CONF_DT, 'dd-MMM-yyyy');
            RawData['SHIP_DT'] = $filter('date')(RawData.SHIP_DT, 'dd-MMM-yyyy');
            RawData['MC_STYLE_H_EXT_ID'] = RawData['MC_STYLE_H_EXT_ID_ORD'];


            RawData['MC_BUYER_ID'] = vm.StyleData.MC_BUYER_ID;
            RawData['LK_ORD_TYPE_ID'] = vm.StyleData.LK_ORD_TYPE_ID;

            RawData['STYLE_NO'] = vm.StyleData.STYLE_NO;
            RawData['MC_INQR_H_ID'] = vm.StyleData.MC_INQR_H_ID;
            RawData['STYL_EXT_NO'] = vm.StyleData.STYL_EXT_NO;
            RawData['PARENT_ID'] = vm.StyleData.PARENT_ID;
            RawData['STYLE_DESC'] = vm.StyleData.STYLE_DESC;
            RawData['LK_STYL_DEV_ID'] = vm.StyleData.LK_STYL_DEV_ID;
            RawData['MATERIAL_DESC'] = vm.StyleData.MATERIAL_DESC;
            RawData['MC_STYLE_H_ID'] = vm.StyleData.MC_STYLE_H_ID;
            RawData['MC_BYR_ACC_ID'] = vm.StyleData.MC_BYR_ACC_ID;
            RawData['ORIGIN_ID'] = vm.StyleData.ORIGIN_ID;
            RawData['MANUF_ID'] = vm.StyleData.MANUF_ID;
            RawData['HAS_SET'] = vm.StyleData.HAS_SET;
            RawData['HAS_COMBO'] = vm.StyleData.HAS_COMBO;
            RawData['NO_OF_SET'] = vm.StyleData.NO_OF_SET;
            RawData['QTY_MOU_ID_ST'] = vm.StyleData.QTY_MOU_ID;
            RawData['PCS_PER_PACK'] = vm.StyleData.PCS_PER_PACK;
            RawData['HAS_MODEL'] = vm.StyleData.HAS_MODEL;
            RawData['HAS_MULTI_COL_PACK'] = vm.StyleData.HAS_MULTI_COL_PACK;
            RawData['IS_ACTIVE'] = 'Y';


            RawData['HR_COUNTRY_ID_LST'] = RawData['HR_COUNTRY_ID_LST'].join(',');
            if (RawData.IS_MULTI_SHIP_DT == 'N') {
                RawData['itmsOrdShipDT'][0]['cap_itms'] = RawData['cap_itms'];
             }

            angular.forEach(RawData['itmsOrdShipDT'], function (val, key) {
                if (RawData.IS_MULTI_SHIP_DT == 'N') {
                    val['SHIP_DT'] = $filter('date')(RawData.SHIP_DT, 'dd-MMM-yyyy');
                } else {
                    val['SHIP_DT'] = $filter('date')(val['SHIP_DT'], 'dd-MMM-yyyy');
                }

                val['CapItmXML'] = config.xmlStringShortNoTag(val.cap_itms.map(function (o) {
                    return {
                        MC_GMT_CAP_ITEM_ID: o.MC_GMT_CAP_ITEM_ID,
                        LK_GARM_TYPE_ID: o.LK_GARM_TYPE_ID,
                        ORDER_QTY: o.ORDER_QTY,
                        SMV: o.SMV
                    }
                }));
            });


            return MrcDataService.saveDataByUrl(RawData, '/StyleH/UpdateOrderStyle').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }

                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $state.go($state.current, { MC_STYLE_H_ID: res.data.V_MC_STYLE_H_ID, MC_ORDER_H_ID: res.data.V_MC_ORDER_H_ID }, { reload: true });
                    }
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });




            //Dialog.confirm('You can\'t update Confirmed order Later. If you want to update later,Please click \'No\'', 'Confirmed ?', ['Yes', 'No'])
            //   .then(function () {
            //       return MrcDataService.saveDataByUrl(RawData, '/StyleH/UpdateOrderStyle').then(function (res) {
            //           if (res.success === false) {
            //               vm.errors = res.errors;
            //           }

            //           else {
            //               res['data'] = angular.fromJson(res.jsonStr);
            //               config.appToastMsg(res.data.PMSG);

            //               if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
            //                   $state.go($state.current, { MC_STYLE_H_ID: res.data.V_MC_STYLE_H_ID, MC_ORDER_H_ID: res.data.V_MC_ORDER_H_ID }, { reload: true });
            //               }
            //           }
            //       }).catch(function (message) {
            //           exception.catcher('XHR loading Failded')(message);
            //       });
            //   }, function () {
            //       RawData['LK_ORD_STATUS_ID'] = '';
            //       return MrcDataService.saveDataByUrl(RawData, '/StyleH/UpdateOrderStyle', token).then(function (res) {
            //           if (res.success === false) {
            //               vm.errors = res.errors;
            //           }

            //           else {
            //               res['data'] = angular.fromJson(res.jsonStr);
            //               config.appToastMsg(res.data.PMSG);

            //               if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
            //                   $state.go($state.current, { MC_STYLE_H_ID: res.data.V_MC_STYLE_H_ID, MC_ORDER_H_ID: res.data.V_MC_ORDER_H_ID }, { reload: true });
            //               }
            //           }
            //       }).catch(function (message) {
            //           exception.catcher('XHR loading Failded')(message);
            //       });
            //   })



        }




        //vm.generateTna = function (data) {
        //    var modalInstance = $modal.open({
        //        animation: true,
        //        templateUrl: 'GenerateTnaTemplate.html',
        //        controller: 'GenerateTnaModalController',
        //        size: 'lg',
        //        windowClass: 'large-Modal',
        //        resolve: {
        //            Order: function () {
        //                return data;
        //            },
        //            TnATaskList: function (MrcDataService) {
        //                return MrcDataService.getDataByUrl('/StyleH/TnAList/TnATemp/' + data.MC_TNA_TMPLT_H_ID + '/Order/' + data.MC_ORDER_H_ID + '/Buyer/' + vm.StyleData.MC_BUYER_ID);
        //            }
        //        }
        //    });

        //    modalInstance.result.then(function (data) {
        //        console.log(data);
        //    }, function () {
        //        console.log('Modal dismissed at: ' + new Date());
        //    });
        //}

    }

})();


(function () {
    'use strict';
    angular.module('multitex.mrc').controller('MktCostBreakDownController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'MouData', 'DateShareService', '$filter', 'formData', 'Dialog', MktCostBreakDownController]);
    function MktCostBreakDownController($q, config, MrcDataService, $stateParams, $state, $scope, MouData, DateShareService, $filter, formData, Dialog) {
        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.errors = null;
        vm.mktCostDatas = [];
        vm.mktCostDatasStore = [];
        vm.MAIN_COST = 0;
        vm.COST_PER_PC = 0;
        vm.MKT_CAL_MOU = 5;
        vm.MKT_CAL_MOU_NAME = 5;



        vm.StyleData = $scope.$parent.StyleData;


        activate();
        function activate() {
            var promise = [GetMktCostHeadData(), getMOUList()]
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }




        $scope.$watch('vm.mktCostDatas', function (newVal, oldVal) {
            vm.MAIN_COST = 0;
            vm.TOT_COST = 0;
            angular.forEach(newVal, function (val1, key1) {

                val1.HEAD_COST = parseFloat(parseFloat((val1.QTY_CONS * val1.RATE_CONS), 3).toFixed(3));

                if (val1.PARENT_ID != 2 && val1.HEAD_COST > 0) {
                    vm.MAIN_COST += val1.HEAD_COST;
                }

                if (val1.IS_PCT == 'Y') {
                    val1.QTY_CONS = 0;
                    val1.RATE_CONS = 0;
                    val1.HEAD_COST = parseFloat(parseFloat((vm.MAIN_COST * ((val1.PCT_COST || 0) / 100)), 3).toFixed(3));
                } else {
                    val1.PCT_COST = 0;
                }

                vm.TOT_COST += val1.HEAD_COST;

                angular.forEach(val1.items, function (val2, key2) {

                    val2.HEAD_COST = parseFloat(parseFloat((val2.QTY_CONS * val2.RATE_CONS), 3).toFixed(3));
                    if (val2.PARENT_ID != 2 && val2.HEAD_COST > 0) {
                        vm.MAIN_COST += val2.HEAD_COST;
                    }

                    if (val2.IS_PCT == 'Y') {
                        val2.QTY_CONS = 0;
                        val2.RATE_CONS = 0;
                        val2.HEAD_COST = parseFloat(parseFloat((vm.MAIN_COST * ((val2.PCT_COST || 0) / 100)), 3).toFixed(3));
                    } else {
                        val2.PCT_COST = 0;
                    }

                    vm.TOT_COST += val2.HEAD_COST;
                });
            });

            if (vm.MKT_CAL_MOU == 2 || vm.MKT_CAL_MOU == 12) {
                vm.COST_PER_PC = parseFloat(parseFloat((vm.TOT_COST / ((vm.StyleData.PCS_PER_PACK || 1) * 12)), 3).toFixed(3));
            } else if (vm.MKT_CAL_MOU == 5) {
                vm.COST_PER_PC = parseFloat(parseFloat((vm.TOT_COST / 12), 3).toFixed(3));
            }


        }, true);


        vm.submitData = function (MktCost, token) {
            var toBSvdMktCost = [];
            angular.forEach(angular.copy(MktCost), function (val1, key1) {
                if (val1.HEAD_COST && val1.HEAD_COST > 0) {
                    toBSvdMktCost.push({
                        MC_STYL_MKT_COST_ID: val1.MC_STYL_MKT_COST_ID,
                        MC_STYLE_H_EXT_ID: val1.MC_STYLE_H_EXT_ID,
                        MC_COST_HEAD_ID: val1.MC_COST_HEAD_ID,
                        IS_PCT: val1.IS_PCT,
                        PCT_COST: val1.PCT_COST,
                        QTY_CONS: val1.QTY_CONS,
                        CONS_MOU_ID: val1.CONS_MOU_ID,
                        RATE_CONS: val1.RATE_CONS,
                        PURCH_MOU_ID: val1.PURCH_MOU_ID,
                        HEAD_COST: val1.HEAD_COST,
                        REMARKS: val1.REMARKS
                    });
                }

                angular.forEach(val1.items, function (val2, key2) {
                    if (val2.HEAD_COST && val2.HEAD_COST > 0) {
                        toBSvdMktCost.push({
                            MC_STYL_MKT_COST_ID: val2.MC_STYL_MKT_COST_ID,
                            MC_STYLE_H_EXT_ID: val2.MC_STYLE_H_EXT_ID,
                            MC_COST_HEAD_ID: val2.MC_COST_HEAD_ID,
                            IS_PCT: val2.IS_PCT,
                            PCT_COST: val2.PCT_COST,
                            QTY_CONS: val2.QTY_CONS,
                            CONS_MOU_ID: val2.CONS_MOU_ID,
                            RATE_CONS: val2.RATE_CONS,
                            PURCH_MOU_ID: val2.PURCH_MOU_ID,
                            HEAD_COST: val2.HEAD_COST,
                            REMARKS: val2.REMARKS
                        });
                    }
                });
            });

            console.log('Order Form: ' + DateShareService.shared.data.OrderFormValid);
            console.log('Style Form: ' + $scope.$parent.StyleInOrderCon.$valid);
            console.log('Mkt Len: ' + toBSvdMktCost.length);


            if (!DateShareService.shared.data.OrderFormValid || !$scope.$parent.StyleInOrderCon.$valid || toBSvdMktCost.length == 0) {
                return;
            }



            var RawData = angular.extend({}, DateShareService.shared.data, { MKT_XML: MrcDataService.xmlStringShort(toBSvdMktCost) });
            RawData['ORD_CONF_DT'] = $filter('date')(DateShareService.shared.data.ORD_CONF_DT, 'dd-MMM-yyyy');
            RawData['SHIP_DT'] = $filter('date')(DateShareService.shared.data.SHIP_DT, 'dd-MMM-yyyy');
            RawData['IS_MULTI_SHIP_DT'] = DateShareService.shared.data['IS_MULTI_SHIP_DT'] || "N";

            RawData['MC_STYLE_H_EXT_ID'] = vm.StyleData.STYL_EXT_NO ? vm.StyleData.MC_STYLE_H_EXT_ID : (DateShareService.shared.data.MC_STYLE_H_EXT_ID_ORD || -1);

            RawData['MC_BUYER_ID'] = vm.StyleData.MC_BUYER_ID;
            RawData['LK_ORD_TYPE_ID'] = vm.StyleData.LK_ORD_TYPE_ID;

            RawData['STYLE_NO'] = vm.StyleData.STYLE_NO;
            RawData['MC_INQR_H_ID'] = vm.StyleData.MC_INQR_H_ID;
            RawData['STYL_EXT_NO'] = vm.StyleData.STYL_EXT_NO;
            RawData['PARENT_ID'] = vm.StyleData.PARENT_ID;
            RawData['STYLE_DESC'] = vm.StyleData.STYLE_DESC;
            RawData['LK_STYL_DEV_ID'] = vm.StyleData.LK_STYL_DEV_ID;
            RawData['MATERIAL_DESC'] = vm.StyleData.MATERIAL_DESC;
            RawData['IS_ACTIVE'] = 'Y';
            RawData['MC_STYLE_H_ID'] = vm.StyleData.MC_STYLE_H_ID;
            RawData['MC_BYR_ACC_ID'] = vm.StyleData.MC_BYR_ACC_ID;
            RawData['SZ_RANGE'] = vm.StyleData.SZ_RANGE;
            RawData['IS_ACTIVE'] = 'Y';

            RawData['ORIGIN_ID'] = vm.StyleData.ORIGIN_ID;
            RawData['MANUF_ID'] = vm.StyleData.MANUF_ID;
            RawData['HAS_SET'] = vm.StyleData.HAS_SET;
            RawData['HAS_COMBO'] = vm.StyleData.HAS_COMBO;
            RawData['NO_OF_SET'] = vm.StyleData.NO_OF_SET;
            RawData['QTY_MOU_ID_ST'] = vm.StyleData.QTY_MOU_ID;
            RawData['PCS_PER_PACK'] = vm.StyleData.PCS_PER_PACK || 0;
            RawData['HAS_MODEL'] = vm.StyleData.HAS_MODEL;
            RawData['HAS_MULTI_COL_PACK'] = vm.StyleData.HAS_MULTI_COL_PACK;
            RawData['EXT_XML'] = vm.StyleData.EXT_XML;
            RawData['MC_SIZE_LST'] = vm.StyleData.MC_SIZE_LST;
            RawData['pIS_OC'] = 'Y';



            Dialog.confirm('You can\'t update Confirmed order Later. If you want to update later,Please click \'No\'', 'Confirmed ?', ['Yes', 'No'])
                .then(function () {
                    return MrcDataService.saveDataByUrl(RawData, '/StyleH/UpdateOrderStyle', token).then(function (res) {
                        if (res.success === false) {
                            vm.errors = res.errors;
                        }

                        else {
                            res['data'] = angular.fromJson(res.jsonStr);
                            config.appToastMsg(res.data.PMSG);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                $state.go($state.current, { MC_STYLE_H_ID: res.data.V_MC_STYLE_H_ID, MC_ORDER_H_ID: res.data.V_MC_ORDER_H_ID }, { reload: true });
                            }
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });

                }, function () {
                    RawData['LK_ORD_STATUS_ID'] = '';
                    return MrcDataService.saveDataByUrl(RawData, '/StyleH/UpdateOrderStyle', token).then(function (res) {
                        if (res.success === false) {
                            vm.errors = res.errors;
                        }

                        else {
                            res['data'] = angular.fromJson(res.jsonStr);
                            config.appToastMsg(res.data.PMSG);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                $state.go($state.current, { MC_STYLE_H_ID: res.data.V_MC_STYLE_H_ID, MC_ORDER_H_ID: res.data.V_MC_ORDER_H_ID }, { reload: true });
                            }
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });



        }

        function GetMktCostHeadData() {
            return MrcDataService.getDataByUrl('/StyleH/FindMktCostHead/' + (vm.StyleData.STYL_EXT_NO ? vm.StyleData.MC_STYLE_H_EXT_ID : (formData.MC_STYLE_H_EXT_ID_ORD || -1))).then(function (res) {
                vm.mktCostDatas = res;
                vm.mktCostDatasStore = angular.copy(res);

                angular.forEach(vm.mktCostDatas, function (val, key) {
                    if ((val.MC_COST_HEAD_ID == 6 || val.MC_COST_HEAD_ID == 4) && val.HEAD_COST > 0) {
                        val.Dtl = true;
                        val['items'] = [];
                    } else {
                        val.Dtl = false;
                    }

                });


            }, function (err) {
                console.log(err)
            });
        }

        vm.changeDetails = function (data) {
            console.log(data);

            if (data.Dtl) {
                data['items'] = [];
            } else {
                data['HEAD_COST'] = 0;
                data['QTY_CONS'] = 0;
                data['RATE_CONS'] = 0;
                data['items'] = angular.copy(vm.mktCostDatasStore[data.index].items);
            }
        };

        function getMOUList() {
            vm.MOUList = {
                optionLabel: "Unt",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success(MouData);
                        }
                    }
                },
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID"
            };
        }


        $scope.$watch('$parent.StyleData.QTY_MOU_ID', function (newVal, oldVal) {

            if (newVal == 2 || newVal == 12) {
                vm.MKT_CAL_MOU = newVal;



                angular.forEach(vm.mktCostDatas, function (val1, key1) {

                    if (val1.PARENT_ID != 3 || val1.MC_COST_HEAD_ID == 7) {
                        val1.CONS_MOU_ID = newVal;
                        val1.PURCH_MOU_ID = newVal;
                    }
                    angular.forEach(val1.items, function (val2, key1) {
                        if (val2.PARENT_ID != 6) {
                            val2.CONS_MOU_ID = newVal;
                            val2.PURCH_MOU_ID = newVal;
                        }
                    })

                });
            } else {
                angular.forEach(vm.mktCostDatas, function (val1, key1) {
                    if (val1.PARENT_ID != 3 || val1.MC_COST_HEAD_ID == 7) {
                        val1.CONS_MOU_ID = 5;
                        val1.PURCH_MOU_ID = 5;
                    }
                    angular.forEach(val1.items, function (val2, key1) {
                        if (val2.PARENT_ID != 6) {
                            val2.CONS_MOU_ID = 5;
                            val2.PURCH_MOU_ID = 5;
                        }
                    })
                    vm.MKT_CAL_MOU = 5;

                });
            }

            vm.MKT_CAL_MOU_NAME = _.filter(MouData, function (o) {
                return o.RF_MOU_ID == vm.MKT_CAL_MOU;
            })[0].MOU_CODE;

        });



    }

})();