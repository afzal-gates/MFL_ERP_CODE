
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DCBatchProductionListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$rootScope', '$scope', 'Hub', 'cur_user_id', '$filter', 'Dialog', '$modal', DCBatchProductionListController]);
    function DCBatchProductionListController($q, config, DyeingDataService, $stateParams, $state, $rootScope, $scope, Hub, cur_user_id, $filter, Dialog, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        var hubUrl = "/signalr";
        var connection = $.hubConnection(hubUrl, { useDefaultPath: true });
        var hub = connection.createHubProxy("dashboard");
        var hubStart = connection.start();

        vm.form = { REPORT_CODE: '', DT_TYPE_ID: '' };
        vm.filterDateData = [{ DT_TYPE_ID: 'D', TYPE_NAME: 'Rceive Date' }, { DT_TYPE_ID: 'F', TYPE_NAME: 'Return DF Date' }, { DT_TYPE_ID: 'C', TYPE_NAME: 'Check-Roll Date' }]


        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [batchIssueRequisitionList(), GetStatusList(), getRollStatusList(), getBuyerAcGrpList(), GetColorList(), GetFilterDateList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.clearData = function () {
            vm.form = { MC_BYR_ACC_ID: '', MC_COLOR_ID: '', DYE_BT_STS_TYPE_ID: '', LK_CHQ_RL_STS_ID: '' }
        }



        function GetFilterDateList() {
            return vm.filterDateList = {
                optionLabel: "-- Select Type --",
                filter: "contains",
                autoBind: true,
                dataSource: vm.filterDateData,
                dataTextField: "TYPE_NAME",
                dataValueField: "DT_TYPE_ID"
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
                            DyeingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COLOR_NAME_EN",
                dataValueField: "MC_COLOR_ID"
            };
        };

        function GetStatusList() {

            return vm.statusList = {
                optionLabel: "-- Select Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchStatusType').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "BT_STS_TYP_NAME",
                dataValueField: "DYE_BT_STS_TYPE_ID"
            };
        };


        function getRollStatusList() {
            return vm.rollStatusList = {
                optionLabel: "--Select Roll Status-",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(111).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
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
                dataTextField: "BYR_ACC_GRP_NAME_EN",
                dataValueField: "MC_BYR_ACC_GRP_ID"
            };
        }

        function getBuyerAcList() {

            return vm.buyerAcList = {
                optionLabel: "--- Select Buyer A/C ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
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
        }

        $scope.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.STR_REQ_DT_LNopened = true;
        }

        $scope.FROM_DATE_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.FROM_DATE_LNopened = true;
        }

        $scope.TO_DATE_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.TO_DATE_LNopened = true;
        }



        var hub = new Hub('dashboard', {
            listeners: {
                'batchIssueRequisitionList': function () {
                    batchIssueRequisitionList();
                    $rootScope.$apply()
                },
                'newConnection': function (id) {
                    vm.connected.push(id);
                    $rootScope.$apply();
                },
                'removeConnection': function (id) {
                    $rootScope.$apply();
                }

            },
            methods: [],
            errorHandler: function (error) {
                console.error(error);
            }
        });

        vm.connected = [];



        vm.searchData = function () {
            var pm = "?";
            if (vm.form.FROM_DATE) {
                var _fdate = $filter('date')(vm.form.FROM_DATE, "MM/dd/yyyy");
                pm = pm + "pFROM_DATE=" + _fdate + "&";
            }

            if (vm.form.TO_DATE) {
                var _tdate = $filter('date')(vm.form.TO_DATE, "MM/dd/yyyy");
                pm = pm + "pTO_DATE=" + _tdate + "&";
            }
            console.log(pm);
            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: false,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);

                        var url = '/api/dye/DCBatchProgramRequisition/SelectAllQCBatch/' + (params.page || 1) + '/' + (params.pageSize || 10) + pm + 'pMC_BYR_ACC_GRP_ID='
                            + vm.form.MC_BYR_ACC_GRP_ID + '&pSTYLE_ORDER_NO=' + (vm.form.STYLE_ORDER_NO || "") + '&pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO || "")
                            + '&pMC_COLOR_ID=' + vm.form.MC_COLOR_ID + '&pLK_CHQ_RL_STS_ID=' + vm.form.LK_CHQ_RL_STS_ID + '&pDT_TYPE_ID=' + (vm.form.DT_TYPE_ID || "")
                            + '&pUSER_ID=' + cur_user_id + '&pOption=3011';
                        console.log(url);
                        DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total'
                }
            })
        }


        function batchIssueRequisitionList() {
            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var pm = DyeingDataService.kFilterStr2QueryParam(params.filter);

                        DyeingDataService.getDataByFullUrl('/api/dye/DCBatchProgramRequisition/SelectAllQCBatch/' + params.page + '/' + params.pageSize + '?' + pm + '&pUSER_ID=' + cur_user_id + '&pOption=3011').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total'
                }
            })
        }

        //$scope.toolTipOptions = {
        //    filter: "td",
        //    position: "top",
        //    content: function (e) {
        //        var grid = e.target.closest(".k-grid").getKendoGrid();
        //        var dataItem = grid.dataItem(e.target.closest("tr"));
        //        return dataItem.REMARKS;

        //    },
        //    show: function (e) {
        //        this.popup.element[0].style.width = "100%";
        //    }
        //}

        vm.gridOptions = {
            dataSource: new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var pm = DyeingDataService.kFilterStr2QueryParam(params.filter);

                        DyeingDataService.getDataByFullUrl('/api/dye/DCBatchProgramRequisition/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pRF_REQ_TYPE_ID=7&pUSER_ID=' + cur_user_id + '&pOption=3004').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total'
                }
            }),
            dataBound: function () {
                this.expandRow(this.tbody.find("tr.k-master-row").first());
            },
            filterable: false,
            //filterable: {
            //    extra: false,
            //    operators: {
            //        string: {
            //            contains: "Contains"
            //        }
            //    }
            //},
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            //scrollable: true,
            groupbale: true,
            sortable: false,
            height: "650px",
            width: '1500px;',
            columns: [
                { field: "DYE_BATCH_NO", title: "Batch No", type: "string", width: "9%", template: "<a ui-sref=\"CheckRollInspection({pDYE_BATCH_NO:dataItem.DYE_BATCH_NO})\" style=\"color:black\" >#=DYE_BATCH_NO #</a>" },
                { field: "BUYER_NAME_EN", title: "Buyer Name", type: "string", width: "10%" },
                { field: "STYLE_NO", title: "Style|Order", type: "string", width: "12%", template: "<span style=\"overflow: hidden; display: block;\">#=STYLE_NO #</span> <span style=\"overflow: hidden; display: block; color:orange;\">#=ORDER_NO #</span>" },
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "8%" },
                { field: "FAB_TYPE_SNAME", title: "Fab Type", type: "string", width: "5%" },
                { field: "FIN_GSM", title: "GSM", type: "string", width: "3%" },

                { field: "TTL_RQD", title: "Qty", type: "string", width: "3%" },

                { field: "LOAD_TIME", title: "Load Time", type: "date", template: "#= kendo.toString(new Date(LOAD_TIME), 'dd-MM-yy hh:mmtt') #", width: "6%" },
                //{ field: "UN_LOAD_TIME", title: "Unload Time", type: "date", template: "#= kendo.toString(new Date(UN_LOAD_TIME), 'dd-MM-yy hh:mmtt') #", width: "10%" },
                { field: "UN_LOAD_TIME", title: "Unload Time", type: "date", template: "#if(UN_LOAD_TIME === null) {#<div class='customClass'>#:null#</div>#} else{##:kendo.toString(new Date(UN_LOAD_TIME), 'dd-MM-yy hh:mmtt')##}#", width: "6%" },
                { field: "CK_ROLL_RCV_DT", title: "BT. Rcv", type: "date", template: "#if(CK_ROLL_RCV_DT === null) {#<div class='customClass'>#:null#</div>#} else{##:kendo.toString(new Date(CK_ROLL_RCV_DT), 'dd-MM-yy hh:mmtt')##}#", width: "6%" },
                { field: "CK_ROLL_FIN_DT", title: "DF. Rcv", type: "date", template: "#if(CK_ROLL_FIN_DT === null) {#<div class='customClass'>#:null#</div>#} else{##:kendo.toString(new Date(CK_ROLL_FIN_DT), 'dd-MM-yy hh:mmtt')##}#", width: "6%" },
                { field: "CHK_ROLL_DT", title: "Check Date", type: "date", template: "#if(CHK_ROLL_DT === null) {#<div class='customClass'>#:null#</div>#} else{##:kendo.toString(new Date(CHK_ROLL_DT), 'dd-MM-yy hh:mmtt')##}#", width: "6%" },

                { field: "DF_PROC_TYPE_LST", title: "Fin. Proc", type: "string", width: "8%" },
                { field: "COMP_NAME_EN", title: "Unit", type: "string", width: "4%" },
                {
                    field: "REQ_TYPE_NAME", title: "Proc", type: "string", width: "3%"
                    //, template: "<span style=\"overflow: hidden; display: block;\">#=REQ_TYPE_NAME #</span> <span style=\"overflow: hidden; display: block;\">#=FAB_TYPE_SNAME #</span>"
                },

                {
                    field: "CHQ_RL_STS_NAME", title: "Status", type: "string", width: "5%",
                    template: "<span style=\"overflow: hidden; display: block;\">#=CHQ_RL_STS_NAME #</span>"
                },

                {
                    field: "REMARKS", title: "Remarks", type: "string", width: "10%",
                    template: "<span style=\"overflow: hidden; display: block;\">#=REMARKS #</span>"
                },
                {
                    title: "Action",
                    template: function () {
                        return '<ul kendo-menu k-orientation="menuOrientation" k-rebind="menuOrientation" k-on-select="onSelect(kendoEvent)">' +
                               '<li><span style="color:black;">Update</span>' +
                                   '<ul style="width:100px;">' +

                                        '<li ng-if="dataItem.LK_CHQ_RL_STS_ID!=561 && dataItem.IS_FINALIZED==' + "'N'" + '" style="padding:5px;"><a ng-click="vm.submitAll(dataItem,561)" style="color:green"><i class="fa fa-check" > OK</i></a> </li>' +
                                        '<li ng-if="dataItem.LK_CHQ_RL_STS_ID!=562 && dataItem.IS_FINALIZED==' + "'N'" + '" style="padding:5px;"><a ng-click="vm.submitAll(dataItem,562)" style="color:red" ><i class="fa fa-remove">Not OK</i></a></li>' +
                                        '<li ng-if="dataItem.LK_CHQ_RL_STS_ID!=563 && dataItem.IS_FINALIZED==' + "'N'" + '" style="padding:5px;"><a ng-click="vm.submitAll(dataItem,563)" style="color:black" ><i class="fa fa-hand-o-up">Hold</i></a></li>' +
                                        '<li ng-if="dataItem.LK_CHQ_RL_STS_ID!=564 && dataItem.IS_FINALIZED==' + "'N'" + '" style="padding:5px;"><a ng-click="vm.submitAll(dataItem,564)" style="color:yellowgreen" ><i class="fa fa-check">Cond. OK</i></a></li>' +


                                   '</ul>' +
                               '</li></ul> ';
                    },
                    width: "8%"
                },
            ]
        };



        vm.printQCData = function () {


            vm.form.FROM_DATE = $filter('date')(vm.form.FROM_DATE, vm.dtFormat);
            vm.form.TO_DATE = $filter('date')(vm.form.TO_DATE, vm.dtFormat);
            
            vm.form.REPORT_CODE = 'RPT-4035';
            console.log(vm.form);
            //vm.form.DYE_STR_REQ_H_ID = dataItem.DYE_STR_REQ_H_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Dye/DyeReport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');

            var params = vm.form;

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

        vm.submitAll = function (dataOri, id) {

            var data = angular.copy(dataOri);

            data["LK_CHQ_RL_STS_ID"] = id;

            if (id == 561) {
                data["IS_ROLL_OK"] = "Y";
                data.DYE_BT_STS_TYPE_ID = 7;
            }
            else if (id == 562) {
                data["IS_ROLL_OK"] = "N";
                data.DYE_BT_STS_TYPE_ID = 8;
            }
            else if (id == 563) {
                data["IS_ROLL_OK"] = "H";
                data.DYE_BT_STS_TYPE_ID = 6;
            }
            else {
                data["IS_ROLL_OK"] = "Y";
                data.DYE_BT_STS_TYPE_ID = 7;
            }

            Dialog.confirm('Do You Want to Finalized!!!".', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     data["IS_FINALIZED"] = "Y";

                     return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DCBatchProgramRequisition/BatchQCStatusUpdate').then(function (res) {

                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             config.appToastMsg(res.data.PMSG);

                             $state.go('BatchProductionList', {}, { reload: true });
                         }
                     });

                 }, function () {

                     data["IS_FINALIZED"] = "N";

                     return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DCBatchProgramRequisition/BatchQCStatusUpdate').then(function (res) {

                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             config.appToastMsg(res.data.PMSG);

                             $state.go('BatchProductionList', {}, { reload: true });
                         }
                     });
                 });
        }


    }

})();