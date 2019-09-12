////////// Start Header Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntMcOilReq4SubStrHController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'formData', 'KnittingDataService', 'Dialog', 'cur_user_id', KntMcOilReq4SubStrHController]);
    function KntMcOilReq4SubStrHController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, formData, KnittingDataService, Dialog, cur_user_id) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        //console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        vm.isAddToGrid = 'Y';
        vm.updateGridIndex = null;

        var key = 'SCM_STR_OIL_REQ_H_ID';
        vm.today = new Date();
        vm.form = formData[key] ? formData : {
            STR_REQ_DT: vm.today, SCM_STR_OIL_REQ_H_ID: 0, RF_ACTN_STATUS_ID: 0, HR_COMPANY_ID: 1, RF_REQ_TYPE_ID: 19, HR_DEPARTMENT_ID: 46,
            ISS_STORE_ID: 12, RCV_STORE_ID: 13, STR_REQ_BY_NAME: cur_user_id, OIL_REQ_D_XML: '', INV_ITEM_CAT_ID: 206
        };
        vm.form.INV_ITEM_CAT_ID = 206;
        vm.formItem = { SCM_STR_TR_REQ_D_ID: 0, QTY_MOU_ID: 0, QTY_MOU_CODE: '' };

        //$('#FAB_ROLL_NO').focus();

        //$("input[type=text]").focus(function () { $(this).select(); }).mouseup(
        //   function (e) {
        //       e.preventDefault();
        //   });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getCategoryList(), getReqTypeList(), getCompanyList(), getDepartmentList(), getStoreList(), getUserList(), getItemList(), getMouList(),
                getStatusList()
            ];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;
            });
        }

        vm.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.STR_REQ_DT_LNopened = true;
        };

        function getCategoryList() {
            vm.categoryOptions = {
                optionLabel: "-- Select Category --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID",
                dataBound: function (e) {
                    var ds = e.sender.dataSource.data();
                    if (ds.length == 1) {
                        e.sender.value(ds[0].INV_ITEM_CAT_ID);
                        vm.form.INV_ITEM_CAT_ID = ds[0].INV_ITEM_CAT_ID;
                        vm.itemDataSource.read();
                    }
                    //else if (ds.length > 0 && $stateParams.pLK_FLOOR_ID > 0) {
                    //    e.sender.value($stateParams.pLK_FLOOR_ID);
                    //    vm.form.LK_FLOOR_ID = $stateParams.pLK_FLOOR_ID;
                    //}

                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.form.INV_ITEM_CAT_ID = item.INV_ITEM_CAT_ID;
                    vm.itemDataSource.read();
                    console.log(item);

                }
            };

            return vm.categoryDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/inv/ItemCategory/SelectAll';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            //var list = _.filter(res, function (o) { return (o.INV_ITEM_CAT_ID === 206) });                            
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.form.INV_ITEM_CAT_ID = item.INV_ITEM_CAT_ID;
                    vm.itemDataSource.read();
                    console.log(item);
                }
            });
        };

        function getReqTypeList() {
            vm.reqTypeOptions = {
                optionLabel: "-- Select Req Type --",
                filter: "contains",
                autoBind: true,
                dataTextField: "REQ_TYPE_NAME",
                dataValueField: "RF_REQ_TYPE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.reqTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/GetReqType';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getCompanyList() {
            vm.companyOptions = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.companyDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/CompanyList';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getDepartmentList() {
            vm.departmentOptions = {
                optionLabel: "-- Select Department --",
                filter: "contains",
                autoBind: true,
                dataTextField: "DEPARTMENT_NAME_EN",
                dataValueField: "HR_DEPARTMENT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.departmentDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/Hr/Admin/HrDesignation/DepartmentListData';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getStoreList() {
            vm.storeOptions = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.storeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=206';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            //var list = _.filter(res, function (x) { return x.SCM_STORE_ID == 12 || x.SCM_STORE_ID == 13 });
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        //vm.userTemplate = '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>';
        //vm.userValueTemplate = '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span> <span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>';
        function getUserList() {
            vm.userOptions = {
                optionLabel: "-- Select User --",
                filter: "contains",
                autoBind: true,
                valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                          '<span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>',
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.userDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/SelectAllUserData';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getItemList() {
            vm.itemOptions = {
                optionLabel: "-- Select Item --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ITEM_NAME_EN",
                dataValueField: "INV_ITEM_ID",
                //dataBound: function (e) {
                //    var ds = e.sender.dataSource.data();
                //    if (ds.length == 1) {
                //        e.sender.value(ds[0].INV_ITEM_ID);
                //        vm.formItem.INV_ITEM_ID = ds[0].INV_ITEM_ID;
                //        vm.formItem.ITEM_NAME_EN = ds[0].ITEM_NAME_EN;
                //        vm.formItem.PACK_MOU_ID = ds[0].PACK_MOU_ID;                        
                //    }                    
                //},
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.formItem.ITEM_NAME_EN = item.ITEM_NAME_EN;
                    vm.formItem.PACK_MOU_ID = item.PACK_MOU_ID;
                    vm.formItem.QTY_PER_PACK = item.PACK_RATIO;

                    console.log(item);

                    var data = _.filter(vm.packMouDataSource.data(), function (ob) {
                        return ob.RF_MOU_ID == item.PACK_MOU_ID;
                    });
                    vm.formItem.PACK_MOU_CODE = data[0].MOU_CODE;

                    var url = '/api/knit/Req4SubStr/GetItemStockByID?pISS_STORE_ID=' + (vm.form.ISS_STORE_ID || 0) + '&pRCV_STORE_ID=' + (vm.form.RCV_STORE_ID || 0) + '&pINV_ITEM_ID=' + (item.INV_ITEM_ID || 0);
                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        vm.formItem.CENTRAL_STR_STOCK = res['CENTRAL_STR_STOCK'];
                        vm.formItem.SUB_STR_STOCK = res['SUB_STR_STOCK'];
                    }, function (err) {
                        console.log(err);
                    });
                }
            };

            return vm.itemDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (vm.form.INV_ITEM_CAT_ID || 206);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getMouList() {
            vm.packMouOptions = {
                optionLabel: "--Pack UoM--",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID"//,
                //select: function (e) {
                //    var item = this.dataItem(e.item);                    
                //    console.log(item);
                //    vm.formItem.PACK_MOU_CODE = item.MOU_CODE;
                //}                
            };

            vm.qtyMouOptions = {
                optionLabel: "-- UoM --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.formItem.QTY_MOU_CODE = item.MOU_CODE;
                }
            };


            return KnittingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {

                vm.packMouDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });

                vm.qtyMouDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });

            }, function (err) {
                console.log(err);
            });


        };

        //var RF_ACTN_TYPE_ID = 14;
        //var PARENT_ID = 0;
        //if (vm.form.RF_ACTN_STATUS_ID > 0) {
        //    PARENT_ID = vm.form.RF_ACTN_STATUS_ID;
        //}
        function getStatusList() {

            var RF_ACTN_TYPE_ID = 14;
            var PARENT_ID = 0;
            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;

            vm.actionOptions = {
                optionLabel: "-- Select Status --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ACTN_STATUS_NAME",
                dataValueField: "RF_ACTN_STATUS_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                },
                dataBound: function (e) {
                    var ds = e.sender.dataSource.data();
                    if (ds.length == 1) {
                        e.sender.value(ds[0].RF_ACTN_STATUS_ID);
                        vm.form.RF_ACTN_STATUS_ID_NEXT = ds[0].RF_ACTN_STATUS_ID;
                    }
                }
            };

            return vm.actionDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id;

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };


        vm.TotalReqQty = function () {
            if ((vm.formItem.PACK_RQD_QTY || 0) > 0) {
                var total = parseFloat(vm.formItem.PACK_RQD_QTY) * parseFloat(vm.formItem.QTY_PER_PACK);
                var stock = parseFloat(vm.formItem.CENTRAL_STK);
                if (total > stock) {

                    vm.formItem.PACK_RQD_QTY = '';
                    vm.formItem.RQD_QTY = 0;
                }
                else {
                    vm.formItem.RQD_QTY = total;
                }
            }
            else
                vm.formItem.PACK_RQD_QTY = 0;
        };



        vm.reqDtlOptions = {
            height: 350,
            sortable: true,
            pageable: false,
            editable: false,
            selectable: "row",
            navigatable: true,
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
            columns: [
                { field: "ITEM_NAME_EN", title: "Item", type: "string", width: "20%", editable: false },
                { field: "CENTRAL_STR_STOCK", title: "Maint. Stock (Ltr)", type: "string", width: "10%", editable: false },
                { field: "SUB_STR_STOCK", title: "Sub Stock (Ltr)", type: "string", width: "10%", editable: false },
                { field: "PACK_RQD_QTY", title: "Reqd. Pack", type: "string", width: "10%", editable: false },
                { field: "PACK_MOU_CODE", title: "Pack Unit", type: "string", width: "10%", editable: false },
                { field: "QTY_PER_PACK", title: "Qty/Pack", type: "string", width: "11%", editable: false },
                { field: "RQD_QTY", title: "Reqd. Qty", type: "string", width: "10%", editable: false, filterable: false },
                { field: "QTY_MOU_CODE", title: "UoM", type: "string", width: "5%", editable: false, filterable: false },
                {
                    title: "",
                    template: function () {
                        return "<button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ng-disabled='dataItem.IS_FINALIZE==\"Y\"' ><i class='fa fa-remove'></i></button>&nbsp;" +
                            "<button type='button' class='btn btn-xs blue' ng-click='vm.editRow(dataItem)' ng-disabled='dataItem.IS_FINALIZE==\"Y\"' ><i class='fa fa-edit'></i></button>&nbsp;";
                    },
                    width: "14%"
                }
            ]
        };


        vm.reqDtlDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    var url = '/api/knit/Req4SubStr/GetReqDtlByID?pSCM_STR_OIL_REQ_H_ID=' + ($stateParams.pSCM_STR_OIL_REQ_H_ID || vm.form.SCM_STR_OIL_REQ_H_ID || 0);

                    KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            }
        });

        vm.addToGrid = function () {
            vm.reqDtlDataSource.insert(0, vm.formItem);
            vm.resetRow();
            //vm.formItem.SCM_STR_TR_REQ_D_ID = 0;
            //vm.formItem.CENTRAL_STR_STOCK = 0;
            //vm.formItem.SUB_STR_STOCK = 0;
            //vm.formItem.PACK_RQD_QTY = 0;
            //vm.formItem.QTY_PER_PACK = 0;
            //vm.formItem.RQD_QTY = 0;

            //vm.itemDataSource.read();
        }

        function findGridIndex(index, SCM_STR_TR_REQ_D_ID) {
            if (index > -1) {
                return index;
            } else {

                return _.findIndex(vm.reqDtlDataSource, function (obj) {
                    return parseInt(obj.SCM_STR_TR_REQ_D_ID) == parseInt(SCM_STR_TR_REQ_D_ID);
                });
            }

        };
        vm.editRow = function (dataItem) {
            vm.isAddToGrid = 'N';
            vm.updateGridIndex = findGridIndex(vm.reqDtlDataSource.indexOf(dataItem), dataItem.SCM_STR_TR_REQ_D_ID);

            vm.formItem = angular.copy(dataItem);
        };

        vm.updateRow = function (data) {
            console.log(data.uid);
            //console.log($scope.$parent.fabReqDtl);
            //console.log(dataItem);

            var dataItem = vm.reqDtlDataSource.getByUid(data.uid);

            //console.log(data.uid);
            console.log(dataItem);

            dataItem.set('INV_ITEM_ID', data.INV_ITEM_ID);
            dataItem.set('CENTRAL_STR_STOCK', data.CENTRAL_STR_STOCK);
            dataItem.set('SUB_STR_STOCK', data.SUB_STR_STOCK);
            dataItem.set('PACK_RQD_QTY', data.PACK_RQD_QTY);
            dataItem.set('PACK_MOU_ID', data.PACK_MOU_ID);
            dataItem.set('PACK_MOU_CODE', data.PACK_MOU_CODE);
            dataItem.set('QTY_PER_PACK', data.QTY_PER_PACK);
            dataItem.set('RQD_QTY', data.RQD_QTY);
            dataItem.set('QTY_MOU_ID', data.QTY_MOU_ID);
            dataItem.set('QTY_MOU_CODE', data.QTY_MOU_CODE);

            vm.resetRow();
        };

        vm.resetRow = function () {
            var data = angular.copy(vm.formItem);
            vm.formItem = {
                SCM_STR_TR_REQ_D_ID: 0, QTY_MOU_ID: 10, QTY_MOU_CODE: 'Ltr', INV_ITEM_ID: data.INV_ITEM_ID, ITEM_NAME_EN: data.ITEM_NAME_EN,
                PACK_MOU_ID: data.PACK_MOU_ID, PACK_MOU_CODE: data.PACK_MOU_CODE, QTY_PER_PACK: data.QTY_PER_PACK
            };
        }

        vm.removeRow = function (dataItem) {
            vm.reqDtlDataSource.remove(dataItem);
            //var dataList = vm.rollRcvGridDataSource.data();

        };

        $scope.$watchGroup(['vm.form.SCM_STORE_ID', 'vm.form.RCV_DT'], function (newVal, oldVal) {

            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    vm.selectedItem = undefined;
                    vm.errors = null;

                    vm.IS_NEXT = 'N';

                    //vm.next();
                    vm.rollRcvGridDataSource.read();
                }
            }
        }, true);




        vm.submitData = function (token) {
            var submitRcvData = angular.copy(vm.form);
            var vMsg = 'Do you want to save?';

            //if (pIS_FINALIZE == 'Y') {
            //    vMsg = 'Do you want to save and finalize?';
            //}

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                submitRcvData['STR_REQ_DT'] = $filter('date')(vm.form.STR_REQ_DT, vm.dtFormat);

                var rcvData = vm.reqDtlDataSource.data();
                console.log(rcvData);


                submitRcvData.OIL_REQ_D_XML = KnittingDataService.xmlStringShort(rcvData.map(function (ob) {
                    return ob;
                }));

                console.log(submitRcvData);

                return $http({
                    headers: { 'Authorization': 'Bearer ' + token },
                    url: '/api/Knit/Req4SubStr/BatchSave',
                    method: 'post',
                    data: submitRcvData
                })
                .then(function (res) {
                    $scope.errors = undefined;
                    if (res.data.success === false) {
                        $scope.errors = [];
                        $scope.errors = res.data.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.data.jsonStr);

                        console.log(res);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            vm.form.SCM_STR_OIL_REQ_H_ID = res['data'].PSCM_STR_OIL_REQ_H_ID_RTN;
                            vm.form.RF_ACTN_STATUS_ID = res['data'].PRF_ACTN_STATUS_ID_RTN;
                            PARENT_ID = res['data'].PRF_ACTN_STATUS_ID_RTN;

                            $stateParams.pSCM_STR_OIL_REQ_H_ID = res['data'].PSCM_STR_OIL_REQ_H_ID_RTN;
                            $state.go('KntMcOilReq4SubStr', { pSCM_STR_OIL_REQ_H_ID: res.data.PSCM_STR_OIL_REQ_H_ID_RTN }, { notify: false });

                            vm.reqDtlDataSource.read();
                            vm.actionDataSource.read();
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });
        };

        vm.backToList = function () {
            return $state.go('KntMcOilReq4SubStrList');
        };

        vm.cancel = function () {
            return $state.go('KntMcOilReq4SubStr', { pSCM_STR_OIL_REQ_H_ID: 0 });
        };


        vm.printRequest = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-3598';

            vm.form.SCM_STR_OIL_REQ_H_ID = dataItem.SCM_STR_OIL_REQ_H_ID;

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
        }


        vm.printChallan = function () {
            //console.log(dataItem);

            vm.isRDLC = true;
            vm.form.REPORT_CODE = 'RPT-3515';

            if (vm.form.SCM_STORE_ID == null || vm.form.SCM_STORE_ID == '') {
                vm.form.SCM_STORE_ID = -1;
            }

            var url;
            if (vm.isRDLC == true) {
                url = '/api/Knit/KntReport/PreviewReportRDLC';
            }
            else {
                url = '/api/Knit/KntReport/PreviewReport';
            }

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.FROM_DATE = $filter('date')(vm.form.RCV_DT, vm.dtFormat);

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

        vm.submitRequsition = function () {
            var submitRcvData = angular.copy(vm.form);
            var vMsg = 'Do you want to submit?';

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                console.log(submitRcvData);

                return $http({
                    headers: { 'Authorization': 'Bearer ' + $scope.token },
                    url: '/api/Knit/Req4SubStr/SubmitRequisition',
                    method: 'post',
                    data: submitRcvData
                })
                .then(function (res) {
                    $scope.errors = undefined;
                    if (res.data.success === false) {
                        $scope.errors = [];
                        $scope.errors = res.data.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.data.jsonStr);

                        console.log(res);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            PARENT_ID = res['data'].PRF_ACTN_STATUS_ID_RTN;

                            vm.actionDataSource.read();
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });
        };

        vm.verifyRequsition = function () {
            var submitRcvData = angular.copy(vm.form);
            var vMsg = 'Do you want to verify?';

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                console.log(submitRcvData);

                return $http({
                    headers: { 'Authorization': 'Bearer ' + $scope.token },
                    url: '/api/Knit/Req4SubStr/VerifyRequisition',
                    method: 'post',
                    data: submitRcvData
                })
                .then(function (res) {
                    $scope.errors = undefined;
                    if (res.data.success === false) {
                        $scope.errors = [];
                        $scope.errors = res.data.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.data.jsonStr);

                        console.log(res);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            vm.mcOilReq4SubStrListDataSource.read();
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });
        };

        vm.issueRequsition = function () {
            return $state.go('KntMcOilReqIss4SubStr', { pSCM_STR_OIL_REQ_H_ID: vm.form.SCM_STR_OIL_REQ_H_ID });
        };

    }

})();
////////// End Header Controller






////////// Start List Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntMcOilReq4SubStrListController', ['logger', 'config', '$q', '$scope', '$http', 'Hub', '$rootScope', 'exception', '$filter', '$state', '$stateParams', '$location', 'KnittingDataService', 'Dialog', 'cur_user_id', KntMcOilReq4SubStrListController]);
    function KntMcOilReq4SubStrListController(logger, config, $q, $scope, $http, Hub, $rootScope, exception, $filter, $state, $stateParams, $location, KnittingDataService, Dialog, cur_user_id) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        var hubUrl = "/signalr";
        var connection = $.hubConnection(hubUrl, { useDefaultPath: true });
        var hub = connection.createHubProxy("dashboard");
        var hubStart = connection.start();

        //console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        vm.LIST_FROM = $stateParams.pLIST_FROM;

        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        var key = 'SCM_STR_OIL_REQ_H_ID';
        vm.today = new Date();
        vm.form = {
            STR_REQ_DT: vm.today, SCM_STR_OIL_REQ_H_ID: 0, HR_COMPANY_ID: 1, RF_REQ_TYPE_ID: 19, ISS_STORE_ID: 12,
            RCV_STORE_ID: 13, STR_REQ_BY_NAME: cur_user_id, OIL_REQ_D_XML: '', INV_ITEM_CAT_ID: 206
        };
        vm.formItem = { SCM_STR_TR_REQ_D_ID: 0 };

        //$('#FAB_ROLL_NO').focus();

        //$("input[type=text]").focus(function () { $(this).select(); }).mouseup(
        //   function (e) {
        //       e.preventDefault();
        //   });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getMcOilReq4SubStrList()];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;
            });
        }

        var hub = new Hub('dashboard', {
            listeners: {
                'newConnection': function (id) {
                    vm.connected.push(id);
                    $rootScope.$apply();
                },
                'removeConnection': function (id) {
                    $rootScope.$apply();
                },
                'broadcastMcOilReq4SubStrList': function () {
                    getMcOilReq4SubStrList();
                    $rootScope.$apply()
                },

            },
            methods: [],
            errorHandler: function (error) {
                console.error(error);
            }
        });

        vm.connected = [];


        vm.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.STR_REQ_DT_LNopened = true;
        };


        vm.mcOilReq4SubStrListOptions = {
            height: 400,
            sortable: true,
            pageable: true,
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
            columns: [
                { field: "STR_REQ_NO", title: "Requisition#", type: "string", width: "14%" },
                { field: "STR_REQ_DT", title: "Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                { field: "ISS_STORE_NAME_EN", title: "From", width: "15%" },
                { field: "RCV_STORE_NAME_EN", title: "To", width: "15%" },
                { field: "RQD_QTY", title: "Rqd.Qty", width: "8%" },
                { field: "ISS_QTY", title: "Iss.Qty", width: "8%" },
                {
                    title: "Status",
                    field: "ACTN_STATUS_NAME",
                    template: function () {
                        return "<label class='label label-sm label-warning' ng-show='dataItem.RF_ACTN_STATUS_ID<=45'>{{dataItem.ACTN_STATUS_NAME}}</label> <label class='label label-sm label-success' ng-show='dataItem.RF_ACTN_STATUS_ID==46'>{{dataItem.ACTN_STATUS_NAME}}</label>";
                    },
                    width: "15%"
                },
                { field: "USER_VERIFIER_NAME", title: "Verifier", hidden: true },
                { field: "USER_ISSUER_NAME", title: "Issuer", hidden: true },
                {
                    title: "Action",
                    template: function () {
                        return "<a type='button' class='btn btn-xs blue' ng-click='vm.editRequsition(dataItem)' ng-if='dataItem.RF_ACTN_STATUS_ID==44'><i class='fa fa-edit'></i></a>&nbsp;" +
                            "<a type='button' class='btn btn-xs blue' ng-click='vm.editRequsition(dataItem)' ng-if='dataItem.RF_ACTN_STATUS_ID>44'>View</a>&nbsp;" +
                            "<a type='button' class='btn btn-xs blue' ng-click='vm.submitRequsition(dataItem)' ng-if='dataItem.RF_ACTN_STATUS_ID==44'>Submit</a>&nbsp;" +
                            //"<a type='button' class='btn btn-xs blue' ng-click='vm.verifyRequsition(dataItem)' ng-if='(dataItem.RF_ACTN_STATUS_ID==45 && dataItem.USER_VERIFIER_NAME==\"VERIFIER\")'>Verify</a>&nbsp;" +
                            "<a type='button' class='btn btn-xs blue' ng-click='vm.issueRequsition(dataItem)' ng-if='(dataItem.RF_ACTN_STATUS_ID==45 && dataItem.USER_ISSUER_NAME==\"ISSUER\")'>Issue</a>&nbsp;" +
                            "<a class='btn btn-xs green' ng-click='vm.printRequest(dataItem)'><i class='fa fa-print'></i> Print</a>";
                    },
                    width: "15%"
                }
            ]
        };



        vm.printRequest = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-3598';

            vm.form.SCM_STR_OIL_REQ_H_ID = dataItem.SCM_STR_OIL_REQ_H_ID;

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
        }



        function getMcOilReq4SubStrList() {

            vm.mcOilReq4SubStrListDataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                schema: {
                    data: "data",
                    total: "total",
                    model: {
                        id: "SCM_STR_OIL_REQ_H_ID",
                        fields: {
                            STR_REQ_DT: { type: "date", editable: false }
                        }
                    }
                },
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/Req4SubStr/GetReqList';
                        url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;

                        console.log(url);

                        KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        }



        vm.addNew = function () {
            return $state.go('KntMcOilReq4SubStr', { pSCM_STR_OIL_REQ_H_ID: 0 });
        };

        vm.editRequsition = function (dataItem) {
            return $state.go('KntMcOilReq4SubStr', { pSCM_STR_OIL_REQ_H_ID: dataItem.SCM_STR_OIL_REQ_H_ID });
        };

        vm.issueRequsition = function (dataItem) {
            return $state.go('KntMcOilReqIss4SubStr', { pSCM_STR_OIL_REQ_H_ID: dataItem.SCM_STR_OIL_REQ_H_ID });
        };


        vm.submitRequsition = function (dataItem) {
            var submitRcvData = angular.copy(dataItem);
            var vMsg = 'Do you want to submit?';

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                console.log(submitRcvData);

                return $http({
                    headers: { 'Authorization': 'Bearer ' + $scope.token },
                    url: '/api/Knit/Req4SubStr/SubmitRequisition',
                    method: 'post',
                    data: submitRcvData
                })
                .then(function (res) {
                    $scope.errors = undefined;
                    if (res.data.success === false) {
                        $scope.errors = [];
                        $scope.errors = res.data.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.data.jsonStr);

                        console.log(res);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            vm.mcOilReq4SubStrListDataSource.read();
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });
        };

        vm.verifyRequsition = function (dataItem) {
            var submitRcvData = angular.copy(dataItem);
            var vMsg = 'Do you want to verify?';

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                console.log(submitRcvData);

                return $http({
                    headers: { 'Authorization': 'Bearer ' + $scope.token },
                    url: '/api/Knit/Req4SubStr/VerifyRequisition',
                    method: 'post',
                    data: submitRcvData
                })
                .then(function (res) {
                    $scope.errors = undefined;
                    if (res.data.success === false) {
                        $scope.errors = [];
                        $scope.errors = res.data.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.data.jsonStr);

                        console.log(res);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            vm.mcOilReq4SubStrListDataSource.read();
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });
        };


        vm.printChallan = function () {
            //console.log(dataItem);

            vm.isRDLC = true;
            vm.form.REPORT_CODE = 'RPT-3515';

            if (vm.form.SCM_STORE_ID == null || vm.form.SCM_STORE_ID == '') {
                vm.form.SCM_STORE_ID = -1;
            }

            var url;
            if (vm.isRDLC == true) {
                url = '/api/Knit/KntReport/PreviewReportRDLC';
            }
            else {
                url = '/api/Knit/KntReport/PreviewReport';
            }

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.FROM_DATE = $filter('date')(vm.form.RCV_DT, vm.dtFormat);

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
////////// End List Controller







////////// Start for Issue Header Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntMcOilReqIss4SubStrHController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'formData', 'KnittingDataService', 'Dialog', 'cur_user_id', KntMcOilReqIss4SubStrHController]);
    function KntMcOilReqIss4SubStrHController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, formData, KnittingDataService, Dialog, cur_user_id) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        //console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        vm.isAddToGrid = 'Y';
        vm.updateGridIndex = null;

        var key = 'SCM_STR_OIL_REQ_H_ID';
        vm.today = new Date();
        vm.form = formData[key] ? formData : {
            STR_REQ_DT: vm.today, SCM_STR_OIL_REQ_H_ID: 0, RF_ACTN_STATUS_ID: 0, HR_COMPANY_ID: 1, RF_REQ_TYPE_ID: 19, HR_DEPARTMENT_ID: 46,
            ISS_STORE_ID: 12, RCV_STORE_ID: 13, STR_REQ_BY_NAME: cur_user_id, OIL_REQ_D_XML: '', INV_ITEM_CAT_ID: 206
        };
        vm.formItem = { SCM_STR_TR_REQ_D_ID: 0, QTY_MOU_ID: 10, QTY_MOU_CODE: 'Ltr' };
        vm.form.INV_ITEM_CAT_ID = 206;
        //$('#FAB_ROLL_NO').focus();

        //$("input[type=text]").focus(function () { $(this).select(); }).mouseup(
        //   function (e) {
        //       e.preventDefault();
        //   });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getCategoryList(), getReqTypeList(), getCompanyList(), getDepartmentList(), getStoreList(), getUserList(), getItemList(), getMouList(),
                getStatusList()
            ];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;
            });
        }

        vm.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.STR_REQ_DT_LNopened = true;
        };

        function getCategoryList() {
            vm.categoryOptions = {
                optionLabel: "-- Select Category --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID",
                dataBound: function (e) {
                    var ds = e.sender.dataSource.data();
                    if (ds.length == 1) {
                        e.sender.value(ds[0].INV_ITEM_CAT_ID);
                        vm.form.INV_ITEM_CAT_ID = ds[0].INV_ITEM_CAT_ID;
                        vm.itemDataSource.read();
                    }
                    //else if (ds.length > 0 && $stateParams.pLK_FLOOR_ID > 0) {
                    //    e.sender.value($stateParams.pLK_FLOOR_ID);
                    //    vm.form.LK_FLOOR_ID = $stateParams.pLK_FLOOR_ID;
                    //}

                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.form.INV_ITEM_CAT_ID = item.INV_ITEM_CAT_ID;
                    vm.itemDataSource.read();
                    console.log(item);

                }
            };

            return vm.categoryDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/inv/ItemCategory/SelectAll';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            //var list = _.filter(res, function (o) { return (o.INV_ITEM_CAT_ID === 206) });
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.form.INV_ITEM_CAT_ID = item.INV_ITEM_CAT_ID;
                    vm.itemDataSource.read();

                    console.log(item);
                }
            });
        };

        function getReqTypeList() {
            vm.reqTypeOptions = {
                optionLabel: "-- Select Req Type --",
                filter: "contains",
                autoBind: true,
                dataTextField: "REQ_TYPE_NAME",
                dataValueField: "RF_REQ_TYPE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.reqTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/GetReqType';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getCompanyList() {
            vm.companyOptions = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.companyDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/CompanyList';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getDepartmentList() {
            vm.departmentOptions = {
                optionLabel: "-- Select Department --",
                filter: "contains",
                autoBind: true,
                dataTextField: "DEPARTMENT_NAME_EN",
                dataValueField: "HR_DEPARTMENT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.departmentDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/Hr/Admin/HrDesignation/DepartmentListData';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getStoreList() {
            vm.storeOptions = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.storeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        //var url = '/api/common/GetStoreInfo';
                        var url = '/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=206';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        //return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            //var list = _.filter(res, function (x) { return x.SCM_STORE_ID == 12 || x.SCM_STORE_ID == 13 || x.SCM_STORE_ID == 14 });
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        //vm.userTemplate = '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>';
        //vm.userValueTemplate = '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span> <span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>';
        function getUserList() {
            vm.userOptions = {
                optionLabel: "-- Select User --",
                filter: "contains",
                autoBind: true,
                valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                          '<span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>',
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.userDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/SelectAllUserData';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getItemList() {
            vm.itemOptions = {
                optionLabel: "-- Select Item --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ITEM_NAME_EN",
                dataValueField: "INV_ITEM_ID",
                //dataBound: function (e) {
                //    var ds = e.sender.dataSource.data();
                //    if (ds.length == 1) {
                //        e.sender.value(ds[0].INV_ITEM_ID);
                //        vm.formItem.INV_ITEM_ID = ds[0].INV_ITEM_ID;
                //        vm.formItem.ITEM_NAME_EN = ds[0].ITEM_NAME_EN;
                //        vm.formItem.PACK_MOU_ID = ds[0].PACK_MOU_ID;                        
                //    }                    
                //},
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.formItem.ITEM_NAME_EN = item.ITEM_NAME_EN;
                    vm.formItem.PACK_MOU_ID = item.PACK_MOU_ID;
                    console.log(item);

                    var data = _.filter(vm.packMouDataSource.data(), function (ob) {
                        return ob.RF_MOU_ID == item.PACK_MOU_ID;
                    });
                    vm.formItem.PACK_MOU_CODE = data[0].MOU_CODE;
                    console.log(data);
                }
            };

            return vm.itemDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (vm.form.INV_ITEM_CAT_ID || 206);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getMouList() {
            vm.packMouOptions = {
                optionLabel: "--Pack UoM--",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID"//,
                //select: function (e) {
                //    var item = this.dataItem(e.item);                    
                //    console.log(item);
                //    vm.formItem.PACK_MOU_CODE = item.MOU_CODE;
                //}                
            };

            vm.qtyMouOptions = {
                optionLabel: "-- UoM --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.formItem.QTY_MOU_CODE = item.MOU_CODE;
                }
            };


            return KnittingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {

                vm.packMouDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });

                vm.qtyMouDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });

            }, function (err) {
                console.log(err);
            });


        };

        var RF_ACTN_TYPE_ID = 14;
        var PARENT_ID = 0;
        if (vm.form.RF_ACTN_STATUS_ID > 0) {
            PARENT_ID = vm.form.RF_ACTN_STATUS_ID;
        }
        function getStatusList() {
            vm.actionOptions = {
                optionLabel: "-- Select Status --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ACTN_STATUS_NAME",
                dataValueField: "RF_ACTN_STATUS_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                },
                dataBound: function (e) {
                    var ds = e.sender.dataSource.data();
                    if (ds.length == 1) {
                        e.sender.value(ds[0].RF_ACTN_STATUS_ID);
                        vm.form.RF_ACTN_STATUS_ID_NEXT = ds[0].RF_ACTN_STATUS_ID;
                    }
                }
            };

            return vm.actionDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id;

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };


        vm.TotalReqQty = function () {
            if ((vm.formItem.PACK_RQD_QTY || 0) > 0) {
                var total = parseFloat(vm.formItem.PACK_RQD_QTY) * parseFloat(vm.formItem.QTY_PER_PACK);
                var stock = parseFloat(vm.formItem.CENTRAL_STK);
                if (total > stock) {

                    vm.formItem.PACK_RQD_QTY = '';
                    vm.formItem.RQD_QTY = 0;
                }
                else {
                    vm.formItem.RQD_QTY = total;
                }
            }
            else
                vm.formItem.PACK_RQD_QTY = 0;
        };



        vm.reqDtlOptions = {
            height: 350,
            sortable: true,
            pageable: false,
            editable: false,
            //selectable: "row",
            navigatable: true,
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
            columns: [
                { field: "ITEM_NAME_EN", title: "Item", type: "string", width: "20%", editable: false, footerTemplate: "Total Record: #=count#" },
                { field: "CENTRAL_STR_STOCK", title: "Maint. Stock (Ltr)", type: "string", width: "10%", editable: false },
                { field: "SUB_STR_STOCK", title: "Sub Stock (Ltr)", type: "string", width: "10%", editable: false },
                { field: "PACK_RQD_QTY", title: "Reqd. Pack", type: "string", width: "10%", editable: false, footerTemplate: "#=sum#" },
                { field: "PACK_MOU_CODE", title: "Pack Unit", type: "string", width: "10%", editable: false },
                { field: "QTY_PER_PACK", title: "Qty/Pack", type: "string", width: "11%", editable: false },
                { field: "RQD_QTY", title: "Reqd. Qty", type: "string", width: "10%", editable: false, filterable: false, footerTemplate: "#=sum#" },
                { field: "QTY_MOU_CODE", title: "UoM", type: "string", width: "5%", editable: false, filterable: false },
                {
                    title: "Iss. Pack",
                    field: "PACK_ISS_QTY",
                    footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmIssPackQty'><input type='number' class='form-control' name='PACK_ISS_QTY' ng-model='dataItem.PACK_ISS_QTY' ng-blur='vm.onBlurIncrAmt(dataItem)' min='1' max='{{dataItem.PACK_RQD_QTY}}' required ng-style='(frmIssPackQty.PACK_ISS_QTY.$error.min||frmIssPackQty.PACK_ISS_QTY.$error.max||frmIssPackQty.PACK_ISS_QTY.$error.required)? vm.errstyle:\"\"' ng-change='vm.onChangeIssPackQty(dataItem)' /></ng-form>";
                    },
                    width: "7%",
                    filterable: false
                },
                {
                    title: "Iss. Qty",
                    field: "PACK_ISS_QTY",
                    footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmIssQty'><input type='number' class='form-control' name='ISS_QTY' ng-model='dataItem.ISS_QTY' ng-blur='vm.onBlurIncrAmt(dataItem)' min='1' required ng-style='(frmIssQty.ISS_QTY.$error.min||frmIssQty.ISS_QTY.$error.required)? vm.errstyle:\"\"' readonly /></ng-form>";
                    },
                    width: "7%",
                    filterable: false
                }
            ]
        };


        vm.reqDtlDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    var url = '/api/knit/Req4SubStr/GetReqDtlByID?pSCM_STR_OIL_REQ_H_ID=' + ($stateParams.pSCM_STR_OIL_REQ_H_ID || vm.form.SCM_STR_OIL_REQ_H_ID || 0);

                    KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            aggregate: [
                { field: "ITEM_NAME_EN", aggregate: "count" },
                { field: "PACK_RQD_QTY", aggregate: "sum" },
                { field: "RQD_QTY", aggregate: "sum" },
                { field: "PACK_ISS_QTY", aggregate: "sum" },
                { field: "ISS_QTY", aggregate: "sum" }
            ]
        });

        vm.onChangeIssPackQty = function (dataItem) {
            dataItem.ISS_QTY = parseFloat(dataItem.PACK_ISS_QTY) * parseFloat(dataItem.QTY_PER_PACK);
        }

        vm.addToGrid = function () {
            vm.reqDtlDataSource.insert(0, vm.formItem);
            vm.resetRow();
            //vm.formItem.SCM_STR_TR_REQ_D_ID = 0;
            //vm.formItem.CENTRAL_STR_STOCK = 0;
            //vm.formItem.SUB_STR_STOCK = 0;
            //vm.formItem.PACK_RQD_QTY = 0;
            //vm.formItem.QTY_PER_PACK = 0;
            //vm.formItem.RQD_QTY = 0;

            //vm.itemDataSource.read();
        }

        function findGridIndex(index, SCM_STR_TR_REQ_D_ID) {
            if (index > -1) {
                return index;
            } else {

                return _.findIndex(vm.reqDtlDataSource, function (obj) {
                    return parseInt(obj.SCM_STR_TR_REQ_D_ID) == parseInt(SCM_STR_TR_REQ_D_ID);
                });
            }

        };
        vm.editRow = function (dataItem) {
            vm.isAddToGrid = 'N';
            vm.updateGridIndex = findGridIndex(vm.reqDtlDataSource.indexOf(dataItem), dataItem.SCM_STR_TR_REQ_D_ID);

            vm.formItem = angular.copy(dataItem);
        };

        vm.updateRow = function (data) {
            console.log(data.uid);
            //console.log($scope.$parent.fabReqDtl);
            //console.log(dataItem);

            var dataItem = vm.reqDtlDataSource.getByUid(data.uid);

            //console.log(data.uid);
            console.log(dataItem);

            dataItem.set('INV_ITEM_ID', data.INV_ITEM_ID);
            dataItem.set('CENTRAL_STR_STOCK', data.CENTRAL_STR_STOCK);
            dataItem.set('SUB_STR_STOCK', data.SUB_STR_STOCK);
            dataItem.set('PACK_RQD_QTY', data.PACK_RQD_QTY);
            dataItem.set('PACK_MOU_ID', data.PACK_MOU_ID);
            dataItem.set('PACK_MOU_CODE', data.PACK_MOU_CODE);
            dataItem.set('QTY_PER_PACK', data.QTY_PER_PACK);
            dataItem.set('RQD_QTY', data.RQD_QTY);
            dataItem.set('QTY_MOU_ID', data.QTY_MOU_ID);
            dataItem.set('QTY_MOU_CODE', data.QTY_MOU_CODE);

            vm.resetRow();
        };

        vm.resetRow = function () {
            var data = angular.copy(vm.formItem);
            vm.formItem = {
                SCM_STR_TR_REQ_D_ID: 0, QTY_MOU_ID: 10, QTY_MOU_CODE: 'Ltr', INV_ITEM_ID: data.INV_ITEM_ID, ITEM_NAME_EN: data.ITEM_NAME_EN,
                PACK_MOU_ID: data.PACK_MOU_ID, PACK_MOU_CODE: data.PACK_MOU_CODE
            };
        }

        vm.removeRow = function (dataItem) {
            vm.reqDtlDataSource.remove(dataItem);
            //var dataList = vm.rollRcvGridDataSource.data();

        };

        $scope.$watchGroup(['vm.form.SCM_STORE_ID', 'vm.form.RCV_DT'], function (newVal, oldVal) {

            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    vm.selectedItem = undefined;
                    vm.errors = null;

                    vm.IS_NEXT = 'N';

                    //vm.next();
                    vm.rollRcvGridDataSource.read();
                }
            }
        }, true);






        vm.backToList = function () {
            return $state.go('KntMcOilReq4SubStrList');
        };

        vm.cancel = function () {
            return $state.go('KntMcOilReq4SubStr', { pSCM_STR_OIL_REQ_H_ID: 0 });
        };

        vm.printChallan = function () {
            //console.log(dataItem);

            vm.isRDLC = true;
            vm.form.REPORT_CODE = 'RPT-3515';

            if (vm.form.SCM_STORE_ID == null || vm.form.SCM_STORE_ID == '') {
                vm.form.SCM_STORE_ID = -1;
            }

            var url;
            if (vm.isRDLC == true) {
                url = '/api/Knit/KntReport/PreviewReportRDLC';
            }
            else {
                url = '/api/Knit/KntReport/PreviewReport';
            }

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.FROM_DATE = $filter('date')(vm.form.RCV_DT, vm.dtFormat);

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

        vm.submitIssueRequisition = function (pIS_FINALIZE) {
            var submitRcvData = angular.copy(vm.form);
            submitRcvData['IS_FINALIZE'] = pIS_FINALIZE;

            if (pIS_FINALIZE == 'Y') {
                var vMsg = 'Do you want to save and finalize?';
            }
            else {
                var vMsg = 'Do you want to save?';
            }

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                var rcvData = vm.reqDtlDataSource.data();
                submitRcvData.OIL_REQ_D_XML = KnittingDataService.xmlStringShort(rcvData.map(function (ob) {
                    return ob;
                }));

                console.log(submitRcvData);

                return $http({
                    headers: { 'Authorization': 'Bearer ' + $scope.token },
                    url: '/api/Knit/Req4SubStr/IssueRequisition',
                    method: 'post',
                    data: submitRcvData
                })
                .then(function (res) {
                    $scope.errors = undefined;
                    if (res.data.success === false) {
                        $scope.errors = [];
                        $scope.errors = res.data.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.data.jsonStr);

                        console.log(res);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                            if (pIS_FINALIZE == 'Y') {
                                PARENT_ID = res['data'].PRF_ACTN_STATUS_ID_RTN;
                                vm.actionDataSource.read();
                            }
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });
        };



    }

})();
////////// End for Issue Header Controller