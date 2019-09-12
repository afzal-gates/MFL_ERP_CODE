(function () {
    'use strict';
    angular.module('multitex.mrc').controller('TaBookingController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'PoHeaderData', 'cur_date_server', 'cur_user_id', '$filter', TaBookingController]);
    function TaBookingController($q, config, MrcDataService, $stateParams, $state, $scope, commonDataService, PoHeaderData, cur_date_server, cur_user_id, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.params = $stateParams;
        activate()
        vm.today = new Date();
        vm.showSplash = true;
        function activate() {
            var promise = [getItemTabList(),
                getUserData(), getCurrencyList(), GetReqSourceList(), GetSourceTypeList(), GetStoreList(),
                GetReqTypeList(), GetPriorityList(), GetCompanyList(), GetPaymentMethodList()
            ];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 27;
            var PARENT_ID = 0;

            if ($scope.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = $scope.form.RF_ACTN_STATUS_ID;

            var sList = null;
            MrcDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                var sList = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "AFZAL" })
                console.log(sList);
                if (sList.length == 1) {
                    $scope.form.RF_ACTN_VIEW = 0;
                    $scope.form.RF_ACTN_STATUS_ID = sList[0].RF_ACTN_STATUS_ID;
                    $scope.form.ACTN_STATUS_NAME = sList[0].ACTN_STATUS_NAME;
                    //alert(vm.form.ACTN_STATUS_NAME);
                }
                else {
                    $scope.form.RF_ACTN_VIEW = 1;
                }
            });

            return vm.statusList = {
                optionLabel: "-- Select Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                                var lst = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "AFZAL" })
                                if (lst.length == 1) {
                                    $scope.form.RF_ACTN_VIEW = 0;
                                    $scope.form.RF_ACTN_STATUS_ID = lst[0].RF_ACTN_STATUS_ID;
                                    $scope.form.ACTN_STATUS_NAME = lst[0].ACTN_STATUS_NAME;
                                }
                                else {
                                    $scope.form.RF_ACTN_VIEW = 1;
                                }
                                e.success(lst);
                            });
                        }
                    }
                },
                dataTextField: "ACTN_STATUS_NAME",
                dataValueField: "RF_ACTN_STATUS_ID"
            };
        };

        vm.departmentList = {
            optionLabel: "--Select Dept--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        MrcDataService.getDataByFullUrl('/Hr/Admin/HrDesignation/DepartmentListData').then(function (res) {
                            //PurchaseDataService.getDataByFullUrl('/Hr/Admin/HrDepartment/SelectAll').then(function (res) {


                            e.success(res);
                        });
                    }
                }
            },
            dataTextField: "DEPARTMENT_NAME_EN",
            dataValueField: "HR_DEPARTMENT_ID"
        };

        function getUserData() {
            return vm.userList = {
                optionLabel: "-- Select User --",
                filter: "startswith",
                autoBind: true,
                valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                          '<span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>',
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getUserDatalist().then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    vm.IsChange = false;
                },

                height: 400,
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID"
            };

            //$("#customers").kendoDropDownList(vm.userList);
        }


        function GetPriorityList() {

            return vm.PriorityList = {
                optionLabel: "-- Select Priority --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(63).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function GetPaymentMethodList() {

            return vm.paymentMethodList = {
                optionLabel: "-- Select Payment Method --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/common/GetPayMethod').then(function (res) {
                                //PurchaseDataService.getDataByFullUrl('api/common/GetPayMethod').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "PAY_MTHD_NAME",
                dataValueField: "RF_PAY_MTHD_ID"
            };
        };

        function GetSourceTypeList() {


            return vm.sourceTypeList = {
                optionLabel: "--Source--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(84).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };

        };

        function GetReqTypeList() {
            return vm.reqTypeList = {
                optionLabel: "-- Select Req Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/common/GetReqType').then(function (res) {
                                //PurchaseDataService.LookupListData(88).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "REQ_TYPE_NAME",
                dataValueField: "RF_REQ_TYPE_ID"
            };
        };

        function GetReqSourceList() {
            return vm.reqSourceList = {
                optionLabel: "-- Select Req Source --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/common/GetReqSRC').then(function (res) {
                                //PurchaseDataService.LookupListData(92).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "REQ_SRC_NAME",
                dataValueField: "RF_REQ_SRC_ID"
            };
        };

        function getCurrencyList() {
            return vm.currencyList = {
                optionLabel: "-- Select Currency --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            commonDataService.getCurrencyList().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "CURR_NAME_EN",
                dataValueField: "RF_CURRENCY_ID"
            };
        };

        function GetCompanyList() {

            return vm.companyList = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
        };


        function GetStoreList() {

            return vm.storeList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
            };
        };

        vm.dtFormat = config.appDateFormat;
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        $scope.setSubmitted = function () {
            $scope.PoHeader.$submitted = true;
        }

        $scope.setTnATaskId = function (ID, Text) {
            $scope.form['MC_TNA_TASK_STATUS_ID'] = ID;
            $scope.form['EVENT_NAME'] = Text;
        }

        if (_.isEmpty(PoHeaderData)) {
            $scope.form = {
                ACCS_DELV_DT: new Date(cur_date_server),
                ACCS_PO_DT: cur_date_server,
                IS_SP_INSTRUCTION: 'N',
                ALLOW_ACCESS: 'Y',
                IS_ACC_PO_NOTE: 'N',
                IS_SUPP_ONLINE: 'N',
                ACCS_PO_REQ_BY: cur_user_id,
                LK_PURCH_TYPE_ID: 452,
                IS_CHANGED: false,
                MC_TNA_TASK_STATUS_ID: 24,
                RF_ACTN_STATUS_ID: 0,
                SCM_STORE_ID: 5
            };
            GetStatusList();
            $scope.MC_TNA_TASK_STATUS_ID_OLD = null;

        } else {

            PoHeaderData['ACCS_PO_DT'] = $filter('date')(PoHeaderData.ACCS_PO_DT, config.appDateFormat);
            $scope.form = PoHeaderData;
            $scope.MC_TNA_TASK_STATUS_ID_OLD = angular.copy(PoHeaderData['MC_TNA_TASK_STATUS_ID']);
            GetStatusList();
        }
        $scope.form.SCM_STORE_ID = 5;
        console.log(PoHeaderData);

        $scope.updateHeaderData = function (pSCM_PURC_REQ_H_ID) {
            return MrcDataService.getDataByFullUrl('/api/mrc/TaBooking/getPoHeaderData/' + pSCM_PURC_REQ_H_ID).then(function (res) {
                res['ACCS_PO_DT'] = $filter('date')(res.ACCS_PO_DT, config.appDateFormat);
                res['ACCS_DELV_DT'] = $filter('date')(res.ACCS_DELV_DT, config.appDateFormat);
                $scope.form = res;
                $scope.form.RF_REQ_TYPE_ID = 30;
            });
        };



        vm.ACCS_DELV_DT_OPEN = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.ACCS_DELV_DT_OPENED = true;
        };



        $scope.PURC_REQ_DT_OPEN = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.PURC_REQ_DT_OPENED = true;
        };

        vm.PurchaseTypeList = {
            optionLabel: "--Purchase Type--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return MrcDataService.LookupListData(84).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            },
            dataTextField: "LK_DATA_NAME_EN",
            dataValueField: "LOOKUP_DATA_ID"
        };

        vm.SupplierListList = {
            optionLabel: "--Supplier List--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return MrcDataService.getDataByFullUrl('/api/purchase/SupplierProfile/SupplierListByItemCategory?pINV_ITEM_CAT_ID=7').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            },
            dataTextField: "SUP_TRD_NAME_EN",
            dataValueField: "SCM_SUPPLIER_ID"
        };





        vm.warehouseData = {
            optionLabel: "--Warehouse--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return MrcDataService.getDataByUrl('/TaBooking/getSoreData').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            },
            dataTextField: "STORE_NAME_EN",
            dataValueField: "SCM_STORE_ID"
        };



        function getItemTabList() {

            if (!$stateParams.pIS_ACC_BK_BOM) {

                return MrcDataService.getDataByUrl('/TaBooking/getItemTabList?pBLK_BOM_LIST=' + $stateParams.pBLK_BOM_LIST +
                              '&pBLK_BOM_ACT=' + $stateParams.pBLK_BOM_ACT +
                              '&pSCM_PURC_REQ_H_ID=' + $stateParams.pSCM_PURC_REQ_H_ID +
                              '&pMC_STYL_BGT_H_ID=' + ($stateParams.pMC_STYL_BGT_H_ID || '') +
                              '&pOption=3002'

                              ).then(function (res) {
                                  $scope.tabs = res.map(function (o) {
                                      return {
                                          INV_ITEM_ID: o.INV_ITEM_ID,
                                          ITEM_NAME_EN: o.ITEM_NAME_EN,
                                          IS_TAB_ACT: o.IS_TAB_ACT,
                                          QTY_EST: Math.ceil(o.QTY_EST + (o.QTY_EST * (o.PCT_ADD_QTY / 100))),
                                          PCT_ADD_QTY: o.PCT_ADD_QTY,
                                          RATE_EST: o.RATE_EST,
                                          ACCT_MOU_ID: o.ACCT_MOU_ID,
                                          ACCT_MOU_NAME: o.ACCT_MOU_NAME,
                                          PURCH_MOU_ID: o.PURCH_MOU_ID,
                                          PURCH_MOU_NAME: o.PURCH_MOU_NAME,
                                          INV_ITEM_CAT_ID: o.INV_ITEM_CAT_ID,
                                          ITEM_CAT_NAME_EN: o.ITEM_CAT_NAME_EN,
                                          REMARKS: o.REMARKS,
                                          MC_BUYER_ID: o.MC_BUYER_ID,
                                          BUYER_NAME_EN: o.BUYER_NAME_EN,
                                          MC_STYLE_BLK_BOM_ID: o.MC_STYLE_BLK_BOM_ID,
                                          MC_ACCS_PO_D_ITEM_ID: o.MC_ACCS_PO_D_ITEM_ID,
                                          SCM_PURC_REQ_D_ID: o.SCM_PURC_REQ_D_ID,
                                          IS_SIZE_COLOR: o.IS_SIZE_COLOR,
                                          ITEM_SPEC_LIST: [],
                                          BYR_ACC_NAME_EN: o.BYR_ACC_NAME_EN,
                                          STYLE_NO: o.STYLE_NO,
                                          STYLE_DESC: o.STYLE_DESC,
                                          ORD_MOU_CODE: o.ORD_MOU_CODE,
                                          TOT_ORD_QTY: o.TOT_ORD_QTY
                                      }
                                  });

                                  if (_.isEmpty(PoHeaderData)) {
                                      $scope.form['ACCS_PO_SUB'] = 'Purchase Order for ' + _.map(res, 'ITEM_NAME_EN').join(',');
                                  }

                              });
            }
            else {

                return MrcDataService.getDataByUrl('/AccBk/getItemTabList?pBLK_BOM_LIST=' + $stateParams.pBLK_BOM_LIST +
                              '&pBLK_BOM_ACT=' + $stateParams.pBLK_BOM_ACT +
                              '&pMC_FAB_PROD_ORD_H_ID=' + $stateParams.pMC_FAB_PROD_ORD_H_ID +
                              '&pSCM_PURC_REQ_H_ID=' + $stateParams.pSCM_PURC_REQ_H_ID

                              ).then(function (res) {
                                  $scope.tabs = res.map(function (o) {
                                      return {
                                          INV_ITEM_ID: o.INV_ITEM_ID,
                                          ITEM_NAME_EN: o.ITEM_NAME_EN,
                                          IS_TAB_ACT: o.IS_TAB_ACT,
                                          QTY_EST: Math.ceil(o.QTY_EST + (o.QTY_EST * (o.PCT_ADD_QTY / 100))),
                                          PCT_ADD_QTY: o.PCT_ADD_QTY,
                                          RATE_EST: o.RATE_EST,
                                          ACCT_MOU_ID: o.ACCT_MOU_ID,
                                          ACCT_MOU_NAME: o.ACCT_MOU_NAME,
                                          PURCH_MOU_ID: o.PURCH_MOU_ID,
                                          PURCH_MOU_NAME: o.PURCH_MOU_NAME,
                                          INV_ITEM_CAT_ID: o.INV_ITEM_CAT_ID,
                                          ITEM_CAT_NAME_EN: o.ITEM_CAT_NAME_EN,
                                          REMARKS: o.REMARKS,
                                          MC_BUYER_ID: o.MC_BUYER_ID,
                                          BUYER_NAME_EN: o.BUYER_NAME_EN,
                                          MC_STYLE_BLK_BOM_ID: o.MC_STYLE_BLK_BOM_ID,
                                          MC_ACCS_PO_D_ITEM_ID: o.MC_ACCS_PO_D_ITEM_ID,
                                          MC_ACCS_PO_TMPLT_D_ID:o.MC_ACCS_PO_TMPLT_D_ID,
                                          MC_ACCS_PO_TMPLT_H_ID: o.MC_ACCS_PO_TMPLT_H_ID,
                                          SCM_PURC_REQ_D_ID: o.SCM_PURC_REQ_D_ID,
                                          IS_SIZE_COLOR: o.IS_SIZE_COLOR,
                                          ITEM_SPEC_LIST: [],
                                          BYR_ACC_NAME_EN: o.BYR_ACC_NAME_EN,
                                          STYLE_NO: o.STYLE_NO,
                                          STYLE_DESC: o.STYLE_DESC,
                                          ORD_MOU_CODE: o.ORD_MOU_CODE,
                                          TOT_ORD_QTY: o.TOT_ORD_QTY,
                                          RQD_QTY: o.RQD_QTY,
                                          REV_QTY: o.REV_QTY,
                                          ADDL_QTY: o.ADDL_QTY,
                                          NET_QTY: o.NET_QTY,
                                          BGT_RATE: o.BGT_RATE,
                                      }
                                  });

                                  if (_.isEmpty(PoHeaderData)) {
                                      $scope.form['ACCS_PO_SUB'] = 'Purchase Order for ' + _.map(res, 'ITEM_NAME_EN').join(',');
                                  }

                              });
            }
        };


        $scope.addToGrid = function (obj, INV_ITEM_ID) {
            angular.forEach($scope.tabs, function (val, key) {
                if (INV_ITEM_ID === val.INV_ITEM_ID) {
                    var trueList = []
                    angular.forEach(obj, function (v, k) {

                        if (!(k.trim() == 'RQD_QTY' || k.trim() == 'RQD_QTY_')) {
                            trueList.push(_.some(val.ITEM_SPEC_LIST, function (o) {
                                return o[k] == v;
                            }));
                        }
                    });

                    if (_.some(trueList, function (o) { return o == false })) {
                        $scope.form.IS_CHANGED = true;
                        //$scope.form.IS_SUP_RT_UPD = 'N';
                        //$scope.form.ALLOW_ACCESS == 'Y';
                        val.ITEM_SPEC_LIST.push(obj);
                    } else {
                        config.ToastInfoMsg('Already Added. Plase try another');
                    }

                };
            });
        };

        $scope.updateGrid = function (obj, INV_ITEM_ID, idx) {
            if (idx < 0) {
                return;
            };

            angular.forEach($scope.tabs, function (val, key) {
                if (INV_ITEM_ID === val.INV_ITEM_ID) {
                    angular.forEach(val.ITEM_SPEC_LIST[idx], function (v, k) {
                        if (obj.hasOwnProperty(k)) {
                            val.ITEM_SPEC_LIST[idx][k] = obj[k];
                        }
                    });
                    $scope.form.IS_CHANGED = true;
                };
            });

        }

        $scope.delFromGrid = function (idx, INV_ITEM_ID) {
            angular.forEach($scope.tabs, function (val, key) {
                if (INV_ITEM_ID === val.INV_ITEM_ID) {
                    if (idx >= 0) {
                        val.ITEM_SPEC_LIST.splice(idx, 1);
                    } $scope.form.IS_CHANGED = true;
                };
            });
        };





        $scope.$watch('tabs', function (newVal, oldVal) {
            console.log(newVal);
            console.log(oldVal);
            var actTab = _.find(newVal, function (o) {
                return o.IS_TAB_ACT == true;
            });


            if (angular.isUndefined(oldVal)) {
                if (actTab) {
                    $state.go('TaBooking.item', { itemObj: actTab }, { notify: false });
                }
                return;
            }

            var oldItem = _.find(oldVal, function (o) {
                return o.IS_TAB_ACT == true;
            });



            if (actTab.INV_ITEM_ID != oldItem.INV_ITEM_ID) {
                $state.go('TaBooking.item', { pINV_ITEM_ID: actTab.INV_ITEM_ID, pBLK_BOM_ACT: actTab.MC_STYLE_BLK_BOM_ID, itemObj: actTab }, { reload: 'TaBooking.item' });
            }

        }, true);


    }

})();