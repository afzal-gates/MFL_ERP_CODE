(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('PartyWiseYarnIssueChallanController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'formData', 'commonDataService', 'cur_user_id', 'Dialog', '$filter', PartyWiseYarnIssueChallanController]);
    function PartyWiseYarnIssueChallanController($q, config, KnittingDataService, $stateParams, $state, $scope, formData, commonDataService, cur_user_id, Dialog, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.DateTimeFormat = config.appDateTimeFormat;
        vm.TimeFormat = config.appTimeFormat;
        vm.ACTION_DIS = 0;
        vm.REPROCESS_FLAG = 0;
        vm.today = new Date();
        //vm.form = {};

        $scope.datePickerOptions = {
            parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
        };

        vm.form = formData.KNT_YRN_CHL_ISS_H_ID ? formData : { CHALAN_DT: vm.today, RF_ACTN_VIEW: 0, SCM_STORE_ID: 1 };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetReqSourceList(), GetCompanyList(), GetMOUList(), GetSupplierList(), loadOrderDetailData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        vm.IssueItemList = [];

        vm.loadPendingProgram = function () {

            //if (vm.form.IS_ALT_PARTY != 'Y')
            KnittingDataService.getDataByFullUrl('/api/knit/YarnIssue/SelectForChallan?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || 0)).then(function (res) {
                vm.IssueItemList = res;
            });
        }


        function loadOrderDetailData() {
            return vm.ScItemListDS = new kendo.data.DataSource({
                serverPaging: false,
                serverSorting: false,
                serverFiltering: false,
                pageSize: 20,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        KnittingDataService.getDataByUrl('/YarnIssueChallan/GetYarnIssueChallanInfoDtlByID?pKNT_YRN_CHL_ISS_H_ID=' + (formData.KNT_YRN_CHL_ISS_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    model: {
                        id: "pKNT_YRN_CHL_ISS_D_ID",
                        fields: {
                            BYR_ACC_GRP_NAME_EN: { editable: false },
                            STYLE_NO: { editable: false },
                            ORDER_NO: { editable: false },
                            ISS_CHALAN_NO: { editable: false },
                            ITEM_NAME_EN: { editable: false },
                            BRAND_NAME_EN: { editable: false },
                            YRN_COLR_GRP: { editable: false },
                            YRN_LOT_NO: { editable: false },
                            ISSUE_QTY: { editable: false },
                        }
                    }
                }

            })
        }


        vm.gridOptions = {
            pageable: false,
            filterable: false,
            height: '100%',
            scrollable: false,
            columns: [
                { field: "pKNT_YRN_CHL_ISS_D_ID", hidden: true },
                { field: "pKNT_YRN_CHL_ISS_H_ID", hidden: true },
                { field: "KNT_YRN_ISS_D_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },

                 { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer Name", width: "15%" },
                { field: "STYLE_NO", title: "Style No", width: "10%" },
                { field: "ORDER_NO", title: "Order No", width: "15%" },
                { field: "ISS_CHALAN_NO", title: "Issue No", width: "12%" },
                { field: "ITEM_NAME_EN", title: "Yarn Item", width: "15%" },
                { field: "BRAND_NAME_EN", title: "Brand", width: "8%" },
                { field: "YRN_COLR_GRP", title: "Color Group", width: "8%" },
                { field: "YRN_LOT_NO", title: "Lot #", width: "8%" },
                { field: "ISSUE_QTY", title: "Issue Qty", width: "5%" },

                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeRecipeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle">Remove</i></a> ',
                    width: "5%"
                }
            ],
        };

        vm.addToGrid = function (data) {

            var items = angular.copy(_.filter(data, function (o) { return o.SELECT_BIT == 'Y' }));
            for (var i = 0; i < items.length; i++) {
                var obj = items[i];
                console.log(obj);
                var idx = vm.ScItemListDS.indexOf(obj);;
                if (idx < 0) {
                    obj["QTY_MOU_ID"] = 3;
                    var id = vm.ScItemListDS.data().length + 1;
                    vm.ScItemListDS.insert(id, obj);
                }
                else {

                    config.appToastMsg("MULTI-005 Duplicate item name exists!");
                }

            }
        }

        vm.removeRecipeItemData = function (obj) {
            //var idx = vm.ScItemListDS.indexOf(obj);
            vm.ScItemListDS.remove(obj);
        }


        function GetReqSourceList() {
            return vm.storeList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=2&pSC_USER_ID=' + cur_user_id).then(function (res) {
                                var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 511 || x.LK_WH_TYPE_ID == 512 });
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
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
                            KnittingDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
        };


        function GetMOUList() {
            return vm.mOUList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function GetSupplierList() {
            return vm.supplierList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        $scope.CHALAN_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.CHALAN_DT_LNopened = true;
        }


        vm.submitAll = function (dataOri, mode, status) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);

                console.log(data);

                vm.showSplash = true;

                data["IS_UPDATE"] = mode;

                var _idate = $filter('date')(data.CHALAN_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["CHALAN_DT"] = _idate;

                data["IS_FINALIZED"] = status;

                data["XML_CHL_D"] = KnittingDataService.xmlStringShort(vm.ScItemListDS.data().map(function (o) {
                    return {
                        KNT_YRN_CHL_ISS_D_ID: o.KNT_YRN_CHL_ISS_D_ID == 0 ? null : o.KNT_YRN_CHL_ISS_D_ID,
                        KNT_YRN_CHL_ISS_H_ID: o.KNT_YRN_CHL_ISS_H_ID == 0 ? null : o.KNT_YRN_CHL_ISS_H_ID,
                        KNT_YRN_ISS_D_ID: o.KNT_YRN_ISS_D_ID == 0 ? null : o.KNT_YRN_ISS_D_ID,
                        ISSUE_QTY: o.ISSUE_QTY == null ? 0 : o.ISSUE_QTY,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 3 : o.QTY_MOU_ID
                    }
                }));

                return KnittingDataService.saveDataByFullUrl(data, '/api/knit/YarnIssueChallan/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        $state.go($state.current, { pKNT_YRN_CHL_ISS_H_ID: res.data.OPKNT_YRN_CHL_ISS_H_ID }, { reload: true });
                    }
                });
            }
        };


        vm.printRDLC = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-3592';
            vm.form.KNT_YRN_CHL_ISS_H_ID = dataItem.KNT_YRN_CHL_ISS_H_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReportRDLC');
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



    }


})();

