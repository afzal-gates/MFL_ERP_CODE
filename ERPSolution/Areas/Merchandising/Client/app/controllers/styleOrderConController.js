(function () {
    'use strict';
    angular.module('multitex.mrc').controller('StyleOrderConController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'formData', '$filter', '$modal', '$timeout', '$sessionStorage', StyleOrderConController]);
    function StyleOrderConController($q, config, MrcDataService, $stateParams, $state, $scope, formData, $filter, $modal, $timeout, $sessionStorage) {


        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.currentState = $state.current.name;
        vm.dtFormat = config.appDateFormat;
        vm.errors = null;
        vm.tnaTaskList = [];
        vm.OrderTypeList = [];
        var extStatus = [];

        $scope.setCurrentState = function (name) {
            vm.currentState = name;
        }

        var key = 'MC_ORDER_H_ID';

        vm.today = new Date();

        vm.StyleData = $scope.$parent.StyleData;
        vm.StyleForm = $scope.$parent.Style;

        activate();
        function activate() {
            var promise = [getExtentionStatus()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        function getExtentionStatus() {
            return MrcDataService.getDataByUrl('/StyleHExt/MainStyleWrtStyleHExtList?pMC_STYLE_H_ID=' + $scope.$parent.StyleData.MC_STYLE_H_ID).then(function (res) {
                vm.dsStyleExtentionList = new kendo.data.DataSource({
                    data: res
                });
            });
        };

        $scope.refreshStyleExtionList = function () {
            return getExtentionStatus();
        };

        vm.params = $stateParams;

        function String2OrdTypeID(IS_N_R) {
            var _IS_N_R;
            switch (IS_N_R) {
                case "N":
                    _IS_N_R = 175;
                    break;
                case "R":
                    _IS_N_R = 174;
                    break;
                case "D":
                    _IS_N_R = 311;
                    break;
            }
            return _IS_N_R;
        }


        vm.openModalForExtionEnrty = function (data) {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ModalForStyleExtEntry.html',
                controller: function ($scope, $modalInstance, MrcDataService, OrderType, Style, formData) {
                    console.log(formData);
                    $scope.OrderTypeList = OrderType;
                    $scope.form = {};

                    if (!formData.WORK_STYLE_NO) {
                        $scope.form['WORK_STYLE_NO'] = Style.STYLE_NO + (formData.IS_N_R=='D'?'-Dev':'');
                        $scope.form['LK_ORD_TYPE_ID'] = String2OrdTypeID(formData.IS_N_R);
                    } else {
                        $scope.form = formData;
                        $scope.form['LK_ORD_TYPE_ID'] = String2OrdTypeID(formData.IS_N_R);
                    }




                    $scope.generateExtionNumber = function (IS_AUTO, LK_ORD_TYPE_ID) {
                        if (IS_AUTO && LK_ORD_TYPE_ID) {
                            var ordTyp;

                            switch (LK_ORD_TYPE_ID) {
                                case 175:
                                    ordTyp = 'N';
                                    break;
                                case 174:
                                    ordTyp = 'R';
                                    break;
                                case 311:
                                    ordTyp = 'D';
                                    break;
                            }


                            return MrcDataService.getDataByUrl('/StyleH/getStyleExtAuto/' + Style.MC_STYLE_H_ID).then(function (ext) {
                                if (ext) {
                                    $scope.form['WORK_STYLE_NO'] = Style.STYLE_NO + '-' + ordTyp + ext;
                                }

                            }, function (err) {
                                $scope.form['WORK_STYLE_NO'] = Style.STYLE_NO;
                            });
                        } else {
                            $scope.form['WORK_STYLE_NO'] = Style.STYLE_NO;
                        }
                    };

                    function OrdTypeID2String(ID) {
                        var _IS_N_R;
                        switch (ID) {
                            case 175:
                                _IS_N_R = "N";
                                break;
                            case 174:
                                _IS_N_R = "R";
                                break;
                            case 311:
                                _IS_N_R = "D";
                                break;
                        }
                        return _IS_N_R;
                    }



                    $scope.save = function (data) {
                        return MrcDataService.saveDataByUrl(
                                {
                                    MC_STYLE_H_EXT_ID: data.MC_STYLE_H_EXT_ID || -1,
                                    STYLE_NO: data.WORK_STYLE_NO,
                                    MC_STYLE_H_ID: Style.MC_STYLE_H_ID,
                                    LK_STYL_DEV_ID: Style.LK_STYL_DEV_ID,
                                    IS_EXT_AUTO: $scope.IS_EXT_AUTO || 'N',
                                    BASE_STYLE: Style.STYLE_NO,
                                    IS_N_R: OrdTypeID2String(data.LK_ORD_TYPE_ID)
                                },
                                '/StyleHExt/Save'
                                ).then(function (res) {

                                    if (res.success === false) {
                                        vm.errors = res.errors;
                                    }
                                    else {
                                        res['data'] = angular.fromJson(res.jsonStr);

                                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                            $modalInstance.close({ MC_STYLE_H_EXT_ID: parseInt(res.data.OPMC_STYLE_H_EXT_ID), IS_N_R: OrdTypeID2String(data.LK_ORD_TYPE_ID), WORK_STYLE_NO: data.WORK_STYLE_NO.trim() });
                                        }
                                        config.appToastMsg(res.data.PMSG);

                                    }

                                });
                    };

                    $scope.cancel = function (data) {
                        $modalInstance.dismiss();
                    }
                },
                resolve: {
                    OrderType: function (MrcDataService) {
                        return MrcDataService.LookupListData(40);
                    },
                    Style: function () {
                        return { MC_STYLE_H_ID: vm.StyleData.MC_STYLE_H_ID, STYLE_NO: vm.StyleData.STYLE_NO, LK_STYL_DEV_ID: vm.StyleData.LK_STYL_DEV_ID };
                    },
                    formData: function () {


                        if (!_.isEmpty(data)) {
                            if ($state.current.LK_STYL_DEV_ID == 265) {
                                return {
                                    WORK_STYLE_NO: data.STYLE_NO, MC_STYLE_H_EXT_ID: data.MC_STYLE_H_EXT_ID, IS_EXT_AUTO: data.IS_EXT_AUTO || 'N',
                                    IS_N_R: "D"
                                }
                            } else {
                                return {
                                    WORK_STYLE_NO: data.STYLE_NO, MC_STYLE_H_EXT_ID: data.MC_STYLE_H_EXT_ID, IS_EXT_AUTO: data.IS_EXT_AUTO || 'N',
                                    IS_N_R: data.IS_N_R||'N'
                                }
                            }
                        } else {

                            if ($state.current.LK_STYL_DEV_ID == 265) {
                                return {
                                    IS_N_R: "D"
                                }
                            } else {
                                return {                              
                                }
                            }

                        }


                    }
                },
                size: 'md',
                windowClass: 'large-Modal'
            });

            modalInstance.result.then(function (data) {

                if ($state.current.LK_STYL_DEV_ID == 265) {
                    $state.go('StyleHDev.OrderConDev.DetailDev', { pMC_STYLE_H_EXT_ID: data.MC_STYLE_H_EXT_ID, pMC_ORDER_H_ID: 0, pIS_N_R: data.IS_N_R, pWORK_STYLE_NO: data.WORK_STYLE_NO }, { reload: 'StyleHDev.OrderConDev' });
                } else {
                    $state.go('StyleH.OrderCon.Detail', { pMC_STYLE_H_EXT_ID: data.MC_STYLE_H_EXT_ID, pMC_ORDER_H_ID: 0, pIS_N_R: data.IS_N_R, pWORK_STYLE_NO: data.WORK_STYLE_NO }, { reload: 'StyleH.OrderCon' });
                }

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });

        };


        function getOrderTypeList() {
            return MrcDataService.LookupListData(40).then(function (res) {
                var LK_STYL_DEV_ID = parseInt(vm.StyleData.LK_STYL_DEV_ID);
                var data = res.map(function (obj) {
                    obj['Disabled'] = (LK_STYL_DEV_ID == 265 || LK_STYL_DEV_ID == 266) && (obj.LOOKUP_DATA_ID == 174 || obj.LOOKUP_DATA_ID == 175) ? true : false;
                    return obj;

                })
                vm.OrderTypeList = data;
            }, function (err) {
                console.log(err);
            });
        }

        vm.gridStyleExtOptions = {
            pageable: false,
            filterable: false,
            sortable: true,
            selectable: true,
            height: '400px',
            scrollable: true,
            dataBound: function (e) {
                //e.sender.thead.hide();
                var grid = this;
                $.each(grid.tbody.find('tr'), function () {
                    if ($stateParams.pMC_STYLE_H_EXT_ID == 0) return;
                    var model = grid.dataItem(this);
                    if (model.MC_STYLE_H_EXT_ID == $stateParams.pMC_STYLE_H_EXT_ID) {
                        $('[data-uid=' + model.uid + ']').addClass('k-state-selected');
                    }
                });
            },
            change: function (e) {
                var item = e.sender.dataItem(this.select());
                // e.sender.expandRow("[data-uid='" + item.uid + "']");

                if ($state.current.LK_STYL_DEV_ID == 265) {
                    $state.go('StyleHDev.OrderConDev.DetailDev', { pMC_STYLE_H_EXT_ID: item.MC_STYLE_H_EXT_ID, pMC_ORDER_H_ID: 0, pIS_N_R: item.IS_N_R, pWORK_STYLE_NO: item.STYLE_NO.trim() }, { reload: 'StyleHDev.OrderConDev.DetailDev' });
                } else {
                    $state.go('StyleH.OrderCon.Detail', { pMC_STYLE_H_EXT_ID: item.MC_STYLE_H_EXT_ID, pMC_ORDER_H_ID: 0, pIS_N_R: item.IS_N_R, pWORK_STYLE_NO: item.STYLE_NO.trim() }, { reload: 'StyleH.OrderCon.Detail' });
                }

            },
            columns: [
                { field: "IS_N_R_ELA", title: "Order Type", width: "100px" },
                { field: "STYLE_NO", title: "Style", width: "190px" },
                { field: "ORDER_NO_LST", title: "Order List", width: "140px" }
            ]
        };

        vm.generateTna = function (data) {

            var resol = {
                StyleData: function () {
                    return $scope.$parent.StyleData;
                },
                Order: function () {
                    return data;
                },
                TnATaskList: function (MrcDataService) {
                    return MrcDataService.getDataByUrl('/StyleH/TnAList/TnATemp/' + data.MC_TNA_TMPLT_H_ID + '/Order/' + data.MC_ORDER_H_ID + '/Buyer/' + data.MC_BUYER_ID);
                },
                DeselectTnaTaskList: function (MrcDataService) {
                    return MrcDataService.getDataByUrl('/TnaTaskGroup/getDeSelectTnaTaskList?pMC_STYLE_H_ID=' + data.MC_STYLE_H_ID);
                }
            };

            var resolDev = {
                StyleData: function () {
                    return $scope.$parent.StyleData;
                },
                Order: function () {
                    return data;
                },
                TnATaskList: function (MrcDataService) {
                    return MrcDataService.getDataByUrl('/StyleH/TnAList/TnATemp/' + data.MC_TNA_TMPLT_H_ID + '/Order/' + data.MC_ORDER_H_ID + '/Buyer/' + data.MC_BUYER_ID);
                },
                DeselectTnaTaskList: function (MrcDataService) {
                    return {}
                }
            };


            var modalInstance = $modal.open({
                animation: true,
                templateUrl: (data.LK_ORD_TYPE_ID == 311) ? '/Merchandising/Mrc/_GenerateDevTnaTemplate' : '/Merchandising/Mrc/_GenerateTnaTemplate',
                controller: (data.LK_ORD_TYPE_ID == 311) ? 'GenerateDevTnaModalController' : 'GenerateTnaModalController',
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: data.LK_ORD_TYPE_ID == 311 ? resolDev : resol
            });

            modalInstance.result.then(function (data) {
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }

        vm.gridStyleExtOptionsDtl = function (Item) {

            return {
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByUrl('/Order/OrderHdrDataList?pMC_STYLE_H_EXT_ID=' + Item.MC_STYLE_H_EXT_ID).then(function (res) {
                                e.success(res.map(function (val) {
                                    val['IS_N_R'] = Item.IS_N_R;
                                    val['STYLE_NO'] = Item.STYLE_NO.trim();
                                    return val;

                                }));
                            })
                        }
                    }
                },
                scrollable: true,
                //dataBound: function (e) {
                //    e.sender.thead.hide();
                //},
                columns: [
                //{ field: "JOB_TRAC_NO", title: "Job #", type: "string", width: "80px" },
                //{ field: "WORK_STYLE_NO", title: "Style No # ", type: "string", width: "80px" },
                { field: "ORDER_NO", title: "PO #", type: "string", width: "150px" },
                {
                    title: "",
                    template: function () {
                        var templ = "<a ui-sref=\"StyleH.OrderCon.Detail({pMC_STYLE_H_EXT_ID: dataItem.MC_STYLE_H_EXT_ID, pMC_ORDER_H_ID: dataItem.MC_ORDER_H_ID, pIS_N_R : dataItem.IS_N_R, pWORK_STYLE_NO: dataItem.STYLE_NO })\" ui-sref-opts=\"{reload: 'StyleH.OrderCon.Detail'}\" class='btn btn-xs blue'><i class='fa fa-edit'> </i> Edit</a>&nbsp;&nbsp;&nbsp;<a ui-sref=\"OrderH.Dtl({ pMC_ORDER_H_ID: dataItem.MC_ORDER_H_ID })\" ng-disabled='dataItem.LK_ORD_STATUS_ID==786' class='btn btn-xs blue'> <i class='fa fa-external-link'> </i> Order Detail</a>&nbsp;&nbsp;<a ng-click='vm.generateTna(dataItem)' class='btn btn-xs purple' ng-disabled='!dataItem.MC_TNA_TMPLT_H_ID'><i class='fa  fa-square-o'></i> T&A</a>"
                        var templDev = "<a ui-sref=\"StyleHDev.OrderConDev.DetailDev({pMC_STYLE_H_EXT_ID: dataItem.MC_STYLE_H_EXT_ID, pMC_ORDER_H_ID: dataItem.MC_ORDER_H_ID, pIS_N_R : dataItem.IS_N_R, pWORK_STYLE_NO: dataItem.STYLE_NO })\" ui-sref-opts=\"{reload: 'StyleHDev.OrderConDev.DetailDev'}\" class='btn btn-xs blue'><i class='fa fa-edit'> </i> Edit</a>&nbsp;&nbsp;<a ng-click='vm.generateTna(dataItem)' class='btn btn-xs purple' ng-disabled='!dataItem.MC_TNA_TMPLT_H_ID'><i class='fa  fa-square-o'></i> generate T&A</a>"

                        return $state.current.LK_STYL_DEV_ID == 265 ? templDev : templ;
                    },
                    width: "120px"
                }

                ]
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



    }

})();