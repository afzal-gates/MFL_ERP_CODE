(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('GreyFabReqListInHController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', '$modal', 'Dialog', 'cur_user_id', GreyFabReqListInHController]);
    function GreyFabReqListInHController($q, config, DyeingDataService, $stateParams, $state, $scope, $modal, Dialog, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.form = {};

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetReqSourceList(), RequisitionList(), getColorList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function getColorList() {

            vm.colorDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/ColourMaster/SelectAll?pOption=3000";
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '&pCOLOR_NAME_EN=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pCOLOR_NAME_EN';
                        }

                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            }

        }

        function GetReqSourceList() {
            return vm.reqSourceList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=5&pSC_USER_ID=' + cur_user_id).then(function (res) {
                                var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 512 || x.LK_WH_TYPE_ID == 511 });
                                e.success(list);
                            });
                        }
                    }
                },
                change: function (e) {
                    var item = this.dataItem(e.item);
                    if (item.SCM_STORE_ID) {
                        var multiselect = $("#ORDER_LIST").data("kendoDropDownList");
                        multiselect.dataSource.read();
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
            };
        };

        vm.supplierListDs = {
            transport: {
                read: function (e) {
                    return DyeingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=579').then(function (res) {
                        e.success(res);
                    });
                }
            }
        }

        vm.actionStatusDs = {
            transport: {
                read: function (e) {
                    return DyeingDataService.getDataByFullUrl('/api/security/RequestApprovalWorkFlow/GetActionStatusByID?pRF_ACTN_TYPE_ID=' + $state.current.data.RF_ACTN_TYPE_ID).then(function (res) {
                        e.success(res);
                    });
                }

            }
        };

        vm.onSearch = function (data) {
            var filter = [];
            angular.forEach(data, function (val, key) {
                if (val && val.length > 0) {
                    filter.push({
                        field: key, operator: "contains", value: val
                    })
                }
            });
            //vm.gridOptionsDS.filter(filter);
            vm.searchData();
        };




        $scope.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST #</h5><p> #: data.STYLE_NO# || #: data.FAB_PROD_CAT_SNAME #</p></span>';
        $scope.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST # (#: data.STYLE_NO||"" #)</h5></span>';


        $scope.getIssueTotal = function () {
            var total = 0;
            if (vm.RollProductionList)
                for (var i = 0; i < vm.RollProductionList.length; i++) {
                    var obj = vm.RollProductionList[i];
                    total += obj.FAB_ROLL_WT;
                }
            return total;
        }




        $scope.FabOederByOhDs = {
            transport: {
                read: function (e) {
                    vm.showSplash = true;
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);

                    var url = "/api/mrc/StyleHExt/getFabProdOrderDataOh";
                    url += "?pMC_BYR_ACC_ID=" + (vm.form.MC_BYR_ACC_ID || null);

                    if (params.filter) {
                        url += '&pORDER_NO_LST=' + params.filter.replace(/'/g, '').split('~')[2];
                    } else {
                        url += '&pORDER_NO_LST';
                    }
                    url += '&pRF_FAB_PROD_CAT_LST=' + $state.current.data.RF_FAB_PROD_CAT_LST;

                    return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                        vm.showSplash = false;
                    });
                }
            },
            serverFiltering: true
        };

        $scope.buyerAcList = {
            optionLabel: "--- Buyer A/C ---",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return DyeingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            },
            change: function (e) {
                var item = this.dataItem(e.item);
                if (item.MC_BYR_ACC_ID) {
                    var multiselect = $("#ORDER_LIST").data("kendoDropDownList");
                    multiselect.dataSource.read();
                }
            },
            dataTextField: "BYR_ACC_NAME_EN",
            dataValueField: "MC_BYR_ACC_ID"
        };



        //vm.taBookingPoHrdDs = new kendo.data.DataSource({
        //    serverPaging: true,
        //    serverFiltering: true,
        //    schema: {
        //        data: "data",
        //        total: "total",
        //        model: {
        //            fields: {
        //                STR_REQ_DT: { type: "date" }
        //            }
        //        }
        //    },
        //    transport: {
        //        read: function (e) {
        //            var webapi = new kendo.data.transports.webapi({});
        //            var params = webapi.parameterMap(e.data);
        //            var url = '/GreyFabReq/List';
        //            url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;

        //            url += DyeingDataService.kFilterStr2QueryParam(params.filter);

        //            url += '&pRF_ACTN_TYPE_ID=' + $state.current.data.RF_ACTN_TYPE_ID;
        //            url += '&pSCM_STORE_ID=' + $state.current.data.SCM_STORE_ID;
        //            //$state.go($state.current, { page: params.page });

        //            DyeingDataService.getDataByUrl(url).then(function (res) {
        //                e.success(res);
        //            });
        //        }
        //    },
        //    page: ($stateParams.page || 1),
        //    pageSize: 10
        //});

        vm.markAsCompleted = function (data) {
            Dialog.confirm('Marking as Issued for : ' + data.STR_REQ_NO + '...?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                  .then(function () {
                      data['pOption'] = 1002;
                      data['RF_ACTN_STATUS_ID'] = $state.current.data.RF_ACTN_TYPE_ID == 13 ? 43 : 49;
                      return DyeingDataService.saveDataByUrl(data, '/GreyFabReq/SaveDyeGstrIssH').then(function (res) {
                          if (res.success === false) {
                          }
                          else {
                              res['data'] = angular.fromJson(res.json);

                              if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                  vm.gridOptionsDS.read();
                              }
                              config.appToastMsg(res.data.PMSG);
                          }
                      });

                  });
        };

        vm.printDeliveryChallan = function (pData) {
            if (pData.DYE_GSTR_ISS_H_ID > 0) {
                var data = {
                    DYE_GSTR_ISS_H_ID: pData.DYE_GSTR_ISS_H_ID
                };
                var form = document.createElement("form");
                form.setAttribute("method", "post");
                form.setAttribute("action", '/api/dye/dyereport/previewreportrdlc');
                form.setAttribute("target", '_blank');

                var params = angular.extend({ REPORT_CODE: 'RPT-4028' }, data);

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
            else {
                vm.form["REPORT_CODE"] = 'RPT-6005';

                vm.form["DF_SC_PT_ISS_H_ID"] = 0;
                vm.form["DYE_GSTR_REQ_H_ID"] = pData.DYE_GSTR_REQ_H_ID;

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
            }
        };

        vm.newIssueModal = function (data) {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'DyeScProgramIssueModal.html',
                controller: function ($scope, config, DyeingDataService, $modalInstance, ChallanList) {
                    $scope.form = {};
                    $scope.today = new Date();
                    $scope.dtFormat = config.appDateFormat;

                    $scope.dateOptions = {
                        formatYear: 'yy',
                        startingDay: 6
                    };

                    $scope.data = data;

                    $scope.ChallanList = ChallanList;

                    $scope.form['ISS_DT'] = new Date();

                    $scope.onSelect = function (data) {
                        $modalInstance.close({
                            DYE_GSTR_ISS_H_ID: data.DYE_GSTR_ISS_H_ID
                        });
                    };

                    $scope.form['RF_ACTN_STATUS_ID'] = 57;
                    $scope.form['SCM_STORE_ID'] = 8;
                    $scope.form['DYE_GSTR_REQ_H_ID'] = data.DYE_GSTR_REQ_H_ID;
                    $scope.form['DYE_GSTR_ISS_H_ID'] = -1;

                    $scope.ISS_DTopen = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.ISS_DTopened = true;
                    }


                    $scope.issue = function (data) {

                        Dialog.confirm('Issuing challan: ' + data.ISS_CHALAN_NO + '...?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                       .then(function () {
                           data['pOption'] = 1001;
                           data['RF_ACTN_STATUS_ID'] = 56;
                           return DyeingDataService.saveDataByUrl(data, '/GreyFabReq/SaveDyeGstrIssH').then(function (res) {
                               if (res.success === false) {
                               }
                               else {
                                   res['data'] = angular.fromJson(res.json);

                                   if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                       $modalInstance.dismiss();
                                   }
                                   config.appToastMsg(res.data.PMSG);
                               }
                           });

                       });
                    };


                    $scope.save = function (data) {
                        data['pOption'] = 1000;
                        return DyeingDataService.saveDataByUrl(data, '/GreyFabReq/SaveDyeGstrIssH').then(function (res) {
                            if (res.success === false) {
                            }
                            else {
                                res['data'] = angular.fromJson(res.json);

                                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                    $modalInstance.close({
                                        DYE_GSTR_ISS_H_ID: parseInt(res.data.OP_DYE_GSTR_ISS_H_ID || 0)
                                    });
                                }
                                config.appToastMsg(res.data.PMSG);
                            }
                        });
                    };

                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }
                },
                resolve: {
                    ChallanList: function () {
                        return DyeingDataService.getDataByUrl('/GreyFabReq/QueryDyeGstrIssH?pOption=3002&pDYE_GSTR_REQ_H_ID=' + data.DYE_GSTR_REQ_H_ID);
                    }
                },
                size: 'lg',
                windowClass: 'app-modal-window'
            });

            modalInstance.result.then(function (dta) {
                $state.go('GreyFabReqIssue', { pDYE_BT_CARD_H_ID: data.DYE_BT_CARD_H_ID, pDYE_GSTR_ISS_H_ID: dta.DYE_GSTR_ISS_H_ID, state: $state.current.name, page: ($stateParams.page || 1) }, { reload: true });
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };

        vm.goToIssue = function (data) {

            console.log(data.parent());
            $state.go('GreyFabReqIssue', { pDYE_BT_CARD_H_ID: data.DYE_BT_CARD_H_ID, pDYE_GSTR_ISS_H_ID: data.DYE_GSTR_ISS_H_ID, state: $state.current.name, page: ($stateParams.page || 1) }, { reload: true });
        };


        vm.goToIssuePreTreatment = function (data) {

            console.log(data.DYE_GSTR_REQ_H_ID);
            $state.go('GreyFabReqPTIssue', { pDYE_GSTR_REQ_H_ID: data.DYE_GSTR_REQ_H_ID, state: $state.current.name, page: ($stateParams.page || 1) }, { reload: true });
        };


        vm.printBatchCard = function (dataItem) {

            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-4032';

            vm.form.DYE_BATCH_NO = dataItem.DYE_BATCH_NO;
            vm.form.DYE_BT_CARD_H_ID = dataItem.DYE_BT_CARD_H_ID;
            //vm.form.KNT_SC_PRG_ISS_ID = dataItem.KNT_SC_PRG_ISS_ID;

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
        }

        vm.searchData = function () {

            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        vm.showSplash = true;
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/GreyFabReq/List';
                        url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;

                        url += DyeingDataService.kFilterStr2QueryParam(params.filter);

                        url += '&pMC_BYR_ACC_ID=' + vm.form.MC_BYR_ACC_ID;
                        url += '&pMC_FAB_PROD_ORD_H_ID=' + vm.form.MC_FAB_PROD_ORD_H_ID;
                        url += '&pRF_ACTN_TYPE_ID=' + vm.form.RF_ACTN_TYPE_ID;
                        url += '&pMC_COLOR_ID=' + vm.form.MC_COLOR_ID;
                        url += '&pSCM_STORE_ID=' + vm.form.SCM_STORE_ID;
                        url += '&pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO || '');

                        //$state.go($state.current, { page: params.page });

                        DyeingDataService.getDataByUrl(url).then(function (res) {
                            vm.showSplash = false;
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


        function RequisitionList() {
            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {

                        vm.showSplash = true;
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/GreyFabReq/List';
                        url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;

                        url += DyeingDataService.kFilterStr2QueryParam(params.filter);

                        url += '&pRF_ACTN_TYPE_ID=' + $state.current.data.RF_ACTN_TYPE_ID;
                        url += '&pSCM_STORE_ID=' + $state.current.data.SCM_STORE_ID;
                        //$state.go($state.current, { page: params.page });

                        DyeingDataService.getDataByUrl(url).then(function (res) {
                            vm.showSplash = false;
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


        vm.gridOptions = {
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains"
                    }
                }
            },
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            sortable: true,
            resizable: true,
            detailTemplate: kendo.template($("#template").html()),
            detailInit: detailInit,
            columns: [
                {
                    field: "STR_REQ_NO", title: "Requisition No", width: "12%",
                    template: "<span style=\"display: block;\">#=STR_REQ_NO #</span> <span style=\"display: block;font-style:italic; color:red;\"><small>#:PENDING#</small><span>"
                },

                { field: "STR_REQ_DT", title: "Req Date", width: "10%", template: "#= kendo.toString(kendo.parseDate(STR_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #" },
                { field: "PLAN_BATCH_QTY", title: "Req(Kg)", width: "8%" },
                { field: "ACT_BATCH_QTY", title: "Issue(Kg)", width: "8%" },
                { field: "DYE_BATCH_NO", title: "Batch No", width: "10%" },

                { field: "EVENT_NAME_S", title: "Status", type: "string", width: "10%", template: "# if( ACTN_ROLE_FLAG!=='DN') {#<h4 style=\"padding-bottom: 0px;padding-top: 5px;margin-top: 0px;margin-bottom: 0px;\" class=\"badge badge-danger\">#=EVENT_NAME_S #<h4># }# # if( ACTN_ROLE_FLAG==='DN') {#<h4 style=\"padding-bottom: 0px;padding-top: 5px;margin-top: 0px;margin-bottom: 0px;\" class=\"badge badge-success\">#=EVENT_NAME_S #<h4># }#" },
                { field: "REQ_TYPE_NAME", title: "Requisition Type", type: "string", width: "15%" },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "15%" },

                {
                    title: "Action",
                    template: "# if( ACTN_ROLE_FLAG!=='DN') {# <a ng-click='vm.markAsCompleted(dataItem)'  class='btn btn-xs'> Mark as Issued</a> #}#",
                    width: "10%"
                },
                {
                    title: "Delete",
                    template: "# if( ACTN_ROLE_FLAG!=='DN') {# <a ng-click='vm.reqDelete(dataItem)'  class='btn btn-xs red'> Delete</a> #}#",
                    width: "10%"
                }
            ]
        };


        vm.reqDelete = function (data) {
            Dialog.confirm('Are you want to delete grey requisition: "' + data.STR_REQ_NO + '.', 'Attention', ['Yes', 'No'])
                .then(function () {
                    return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/GreyFabReq/DeleteReq').then(function (res) {

                        if (res.success === false) {
                            vm.errors = res.errors;
                        }
                        else {
                            res['data'] = angular.fromJson(res.jsonStr);
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, {}, { reload: true });
                        }
                    });
                });
        }

        function detailInit(ev) {
            var DYE_GSTR_REQ_H_ID = ev.data.DYE_GSTR_REQ_H_ID;

            var deltailRow = ev.detailRow;
            var i = 1;

            var showTreatmentDtl = function (eee) {
                var dataItem = this.dataItem($(eee.currentTarget).closest("tr"));
                vm.goToIssuePreTreatment(dataItem);
            };

            var showIssueDtl = function (eee) {
                var dataItem = this.dataItem($(eee.currentTarget).closest("tr"));
                vm.goToIssue(dataItem);
            };

            var printBatch = function (eee) {
                var dataItem = this.dataItem($(eee.currentTarget).closest("tr"));
                vm.printBatchCard(dataItem);
            };

            deltailRow.find(".tabstrip").kendoTabStrip({
                animation: {
                    open: { effects: "fadeIn" }
                }
            });

            deltailRow.find(".DyeBatch").kendoGrid({
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success(ev.data.items);
                        }
                    },
                    schema: {
                        model: {
                            fields: {
                                DYE_BATCH_NO: { type: "string" },
                                ACT_BATCH_QTY: { type: "string" },
                                DYE_MACHINE_NO: { type: "string" },
                                ACT_LOAD_TIME: { type: "date" },
                                ACT_UN_LOAD_TIME: { type: "date" },
                                BYR_ACC_GRP_NAME_EN: { type: "string" },
                                STYLE_NO: { type: "string" },
                                ORDER_NO: { type: "string" },
                                COLOR_NAME_EN: { type: "string" },
                            }
                        }
                    },
                },
                sortable: false,
                columns: [
                     { field: "DYE_BATCH_NO", title: "Batch#", type: "string", width: "10%" },
                     { field: "ACT_BATCH_QTY", title: "Req (Kg)", type: "string", width: "5%" },
                     { field: "DYE_MACHINE_NO", title: "M/C", type: "string", width: "10%" },

                     { field: "ACT_LOAD_TIME", title: "Load", width: "10%", format: "{0:dd-MMM-yyyy h:mm tt}" },
                     { field: "ACT_UN_LOAD_TIME", title: "Unload", width: "10%", format: "{0:dd-MMM-yyyy h:mm tt}" },

                     { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer", type: "string", width: "10%" },
                     { field: "STYLE_NO", title: "Style", type: "string", width: "10%" },
                     { field: "ORDER_NO", title: "Order #", type: "string", width: "10%" },
                     { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "10%" },

                     { command: { text: "Action", click: showIssueDtl }, title: "Action", width: "8%" },
                     { command: { text: "Print", click: printBatch }, title: "Print", width: "7%" },

                ]
            });


            deltailRow.find(".PreTreatment").kendoGrid({
                sortable: true,

                dataSource: {
                    serverPaging: true,
                    serverFiltering: true,
                    schema: {
                        data: "data",
                        total: "total",
                        model: {
                            fields: {
                                SC_PRG_NO: { type: "string" },
                                PRG_ISS_DT: { type: "date" },
                                EXP_DELV_DT: { type: "date" },
                            }
                        }
                    },
                    transport: {
                        read: function (e) {
                            var webapi = new kendo.data.transports.webapi({});
                            var params = webapi.parameterMap(e.data);
                            var pm = DyeingDataService.kFilterStr2QueryParam(params.filter);

                            DyeingDataService.getDataByFullUrl('/api/Dye/ScPreTreatment/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pDYE_GSTR_REQ_H_ID=' + DYE_GSTR_REQ_H_ID + '&pUSER_ID=' + cur_user_id).then(function (res) {
                                e.success(res);
                            });
                        }
                    },
                    pageSize: 10
                },

                filterable: {
                    extra: false,
                    operators: {
                        string: {
                            contains: "Contains",
                            startswith: "Starts With",
                            eq: "Is Equal To"
                        }
                    }
                },
                scrollable: true,
                pageable: true,
                columns: [
                    { field: "SC_PRG_NO", title: "Program No 2", type: "string", width: "10%" },
                    { field: "PRG_ISS_DT", title: "Issue Date", type: "date", template: "#= kendo.toString(kendo.parseDate(PRG_ISS_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%", filter: true },
                    { field: "EXP_DELV_DT", title: "Exp. Delivery Date", type: "date", template: "#= kendo.toString(kendo.parseDate(EXP_DELV_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%", filter: true },
                    { field: "CHALAN_NO", title: "Challan No", type: "string", width: "10%" },
                    { field: "CHALAN_DT", title: "Challan Date", type: "date", template: "#= kendo.toString(kendo.parseDate(CHALAN_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%", filter: true },
                    { field: "COMP_NAME_EN", title: "Company", type: "string", width: "10%" },
                    { field: "SUP_TRD_NAME_EN", title: "Supplier", type: "string", width: "10%" },
                    { field: "STORE_NAME_EN", title: "Store", type: "string", width: "10%" },
                    { field: "GATE_PASS_NO", title: "Gate Pass No", type: "string", width: "10%" },
                    { command: { text: "Edit", click: showTreatmentDtl }, title: "Action", width: "8%" },

                ]
            });
        }



        vm.taBookingPoHrdDtl = function (data) {
            return {
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success(data.items);
                        }
                    },
                    schema: {
                        model: {
                            fields: {
                                ACT_LOAD_TIME: { type: "date" },
                                ACT_UN_LOAD_TIME: { type: "date" }
                            }
                        }
                    },
                },
                scrollable: true,
                columns: [
                     { field: "DYE_BATCH_NO", title: "Batch#", type: "string", width: "15%" },
                     { field: "ACT_BATCH_QTY", title: "Req (Kg)", type: "string", width: "10%" },
                     { field: "DYE_MACHINE_NO", title: "M/C", type: "string", width: "10%" },

                     { field: "ACT_LOAD_TIME", title: "Load", width: "15%", format: "{0:dd-MMM-yyyy h:mm tt}" },
                     { field: "ACT_UN_LOAD_TIME", title: "Unload", width: "15%", format: "{0:dd-MMM-yyyy h:mm tt}" },

                     { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer", type: "string", width: "10%" },
                     { field: "STYLE_NO", title: "Style", type: "string", width: "10%" },
                     { field: "ORDER_NO", title: "Order #", type: "string", width: "20%" },
                     { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "10%" },
                     {
                         title: "Action",
                         template: function () {
                             return "<a ng-click='vm.goToIssue(dataItem)'  ng-if='(dataItem.IS_PERFORMER === \"Y\")' class='btn btn-xs blue'><i class='fa fa-edit'> {{dataItem.NXT_ACTION_NAME}}</i></a>" +
                             " <a ng-click='vm.printBatchCard(dataItem)' class='btn btn-xs green'><i class='fa fa-print'> Print</i></a>";
                         },
                         width: "10%"
                     }
                ]
            };
        };

    }

})();