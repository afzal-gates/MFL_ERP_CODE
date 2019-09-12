(function () {
    'use strict';
    angular.module('multitex.mrc').controller('TaBookingItemController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'multi_login_dept_id', 'Dialog', '$window', '$filter', '$modal', 'cur_user_id', TaBookingItemController]);
    function TaBookingItemController($q, config, MrcDataService, $stateParams, $state, $scope, multi_login_dept_id, Dialog, $window, $filter, $modal, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.gridList = [];
        vm.labelRepo = {};
        vm.pcTRepo = {};
        vm.EDIT_MODE = false;
        vm.EDIT_INDEX = -1;
        var controlListBkUp = [];

        vm.ColorList = [];
        vm.SizeList = [];

        vm.params = $stateParams;

        console.log($stateParams);
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getTempleteData(), getControlList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;

            });
        }



        function getTempleteData() {
            //console.log("AFZAL");
            //console.log(vm.params.pBLK_BOM_ACT);
            //console.log(vm.params.pMC_FAB_PROD_ORD_H_ID);

            MrcDataService.getDataByFullUrl("/api/mrc/AccBk/getItemTemplet?pINV_ITEM_ID=" + $stateParams.pINV_ITEM_ID + "&pMC_FAB_PROD_ORD_H_ID=" + $stateParams.pMC_FAB_PROD_ORD_H_ID + "&pSCM_PURC_REQ_H_ID=" + $stateParams.pSCM_PURC_REQ_H_ID).then(function (res) {
                vm.params.itemObj['bomItemList'] = res.map(function (o) {
                    return {
                        MC_FAB_PROD_D_BOM_ID: o.MC_FAB_PROD_D_BOM_ID,
                        MC_FAB_PROD_H_BOM_ID: o.MC_FAB_PROD_H_BOM_ID,
                        GMT_COLOR_ID: o.GMT_COLOR_ID,
                        MC_SIZE_ID: o.GMT_SIZE_ID,
                        ORDER_QTY: o.ORDER_QTY,
                        PCT_ALLOW_QTY: o.PCT_ALLOW_QTY,
                        MC_ORD_TRMS_ITEM_ID: o.MC_ORD_TRMS_ITEM_ID,
                        ITEM_SPEC_AUTO: o.ITEM_SPEC_AUTO,
                        CONS_QTY: o.CONS_QTY,
                        BOOK_QTY: o.BOOK_QTY,
                        REV_QTY: o.REV_QTY,
                        NET_BOOK_QTY: o.NET_BOOK_QTY,
                        CONS_MOU_ID: o.CONS_MOU_ID,
                        PURCH_MOU_ID: o.PURCH_MOU_ID,
                        PO_QTY: o.PO_QTY,
                        RCV_QTY: o.RCV_QTY,
                        ISSUE_QTY: o.ISSUE_QTY,
                        COLOR_NAME_EN: o.COLOR_NAME_EN,
                        SIZE_CODE: o.SIZE_CODE,
                        MOU_CODE: o.MOU_CODE,
                        PROD_COLOR_NAME_EN: o.PROD_COLOR_NAME_EN,
                        PROD_SIZE_CODE: o.PROD_SIZE_CODE,
                        ITEM_CODE: o.ITEM_CODE,
                        PARTICULARS_LST: o.PARTICULARS.length > 5 ? MrcDataService.parseXmlString(o.PARTICULARS) : o.PARTICULARS_LST //[{ 'CNTRL_NAME': '', 'COL_VAL': '' }]
                    }
                });
            });

        }

        function getControlList() {
            vm.controlList = {
                optionLabel: "--Select--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl("/api/mrc/TempTaBk/getTemplateControl?pMC_ACCS_PO_TMPLT_H_ID=0").then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "CNTRL_LABEL",
                dataValueField: "CNTRL_NAME"

                //CNTRL_LABEL
            };
        }

        vm.copyAll = function (id) {
            var obj = angular.copy(vm.params.itemObj.bomItemList[0]);
            for (var i = 1; i < vm.params.itemObj.bomItemList.length; i++) {
                if (id == 1) {
                    vm.params.itemObj.bomItemList[i].PROD_COLOR_NAME_EN = angular.copy(obj.PROD_COLOR_NAME_EN);
                }
                else if (id == 2) {
                    vm.params.itemObj.bomItemList[i].PROD_SIZE_CODE = angular.copy(obj.PROD_SIZE_CODE);
                }
                else if (id == 3) {
                    vm.params.itemObj.bomItemList[i].ITEM_SPEC = angular.copy(obj.ITEM_SPEC);
                }
                else if (id == 4) {
                    vm.params.itemObj.bomItemList[i].ITEM_CODE = angular.copy(obj.ITEM_CODE);
                }
                else if (id == 5) {
                    vm.params.itemObj.bomItemList[i].PARTICULARS_LST = angular.copy(obj.PARTICULARS_LST);
                }
            }
        }

        vm.addCtrl = function (index, item) {
            var idx = index + 1;
            var nObj = { CNTRL_NAME: '', COL_VAL: '' };
            item.splice(idx, 0, nObj);
        }

        vm.delCtrl = function (idx, item) {
            item.splice(idx, 1);
        }

        vm.checkKey = function (key) {
            return _.endsWith(key, '_');
        }

        console.log(multi_login_dept_id);

        vm.getPCTValue = function (key) {
            var obj = _.find(vm.controlList, function (o) {
                return o.CNTRL_NAME === key;
            });

            if (obj) {
                return obj.PCT_WIDTH;
            }
            return 20;
        }


        function getDropDownButtonList() {
            var url = '/TnaTaskStatus/SelectApprovRejectStatus?pMC_TNA_TASK_ID=46&pHR_DEPARTMENT_ID=' + multi_login_dept_id;


            if ($scope.$parent.form.MC_TNA_TASK_STATUS_ID == null || $scope.$parent.form.MC_TNA_TASK_STATUS_ID == 24 || $scope.$parent.form.MC_TNA_TASK_STATUS_ID == 30) {
                url += '&pPARENT_ID=null'
            } else {
                url += '&pPARENT_ID=' + $scope.$parent.form.MC_TNA_TASK_STATUS_ID;
            }

            return MrcDataService.getDataByUrl(url).then(function (res) {
                if (_.some(res, function (o) {
                    return o.STATUS_ORDER == 1
                })) {

                    vm.buttonDropDwnList = _.filter(res, function (o) {
                        return (o.STATUS_ORDER == 1 || o.STATUS_ORDER == 2);
                    });

                } else {
                    vm.buttonDropDwnList = res;
                }

                setTimeout(function () {
                    $(".dropdown-menu li a").click(function () {
                        $(this).parents(".dropdown").find('.btn').html($(this).text() + ' <span class="caret"></span>');
                        $(this).parents(".dropdown").find('.btn').val($(this).data('value'));
                        $scope.$parent.setTnATaskId($(this).data('value'), $(this).text());

                    });

                    if ($scope.$parent.form.ALLOW_ACCESS === 'Y') {
                        $(".dropdown-menu li a").parents(".dropdown").find('.btn').html(($scope.$parent.form.EVENT_NAME || 'Create Draft') + ' <span class="caret"></span>');
                        $(".dropdown-menu li a").parents(".dropdown").find('.btn').val($scope.$parent.form.MC_TNA_TASK_STATUS_ID);
                    }
                }, 500)
            });
        }

        vm.SizeColorList = [];
        vm.newSizeList = [];


        vm.removeSizeColor = function (colsiz, size) {
            var idx = vm.SizeColorList.indexOf(colsiz);
            var index = vm.SizeColorList[idx].SizeList.indexOf(size);

            vm.SizeColorList[idx].SizeList.splice(index, 1);
        }


        vm.addSizeColor = function (colsiz, size) {

            var indx = vm.SizeColorList.indexOf(colsiz);
            var idx = vm.SizeColorList[indx].SizeList.length;
            var item = angular.copy(size);
            vm.SizeColorList[indx].SizeList.splice(idx, "0", item);
        }

        vm.removeSize = function (size) {
            var idx = vm.SizeList.indexOf(size);
            vm.SizeList.splice(idx, 1);
        }


        vm.addSize = function (size) {

            var idx = vm.SizeList.length;
            var item = angular.copy(size);
            vm.SizeList.splice(idx, "0", item);
        }


        vm.removeColor = function (size) {
            var idx = vm.ColorList.indexOf(size);
            vm.ColorList.splice(idx, 1);
        }


        vm.addColor = function (size) {

            var idx = vm.ColorList.length;
            var item = angular.copy(size);
            vm.ColorList.splice(idx, "0", item);
        }


        vm.edit = function (data, idx) {
            controlListBkUp = angular.copy(vm.controlList);
            angular.forEach(vm.controlList, function (val, key) {
                if (val.CNTRL_TYPE === 'select') {
                    val['COL_VAL'] = data[val.CNTRL_NAME].toString();
                } else {
                    val['COL_VAL'] = data[val.CNTRL_NAME];
                }
            });

            vm.EDIT_MODE = true
            vm.EDIT_INDEX = idx;
        }

        vm.cancel = function () {
            if (controlListBkUp.length > 0) {
                vm.controlList = controlListBkUp;
            }
            vm.EDIT_MODE = false;
            vm.EDIT_INDEX = -1;
        }

        vm.update = function (data, isValid) {
            if (!isValid) {
                return;
            }
            var f = {};
            angular.forEach(data, function (val, key) {
                if (val.CNTRL_TYPE === 'select') {
                    f[val.CNTRL_NAME] = val.COL_VAL;
                    f[val.CNTRL_NAME + '_'] = _.find(val.OPTION_VAL, function (o) { return o.KEY_VAL === val.COL_VAL }).TEXT_VAL;
                } else {
                    f[val.CNTRL_NAME] = val.COL_VAL;
                    f[val.CNTRL_NAME + '_'] = val.COL_VAL;
                }
                f['IS_CHANGED'] = true;
                vm.labelRepo[val.CNTRL_NAME + '_'] = val.CNTRL_LABEL;
                vm.pcTRepo[val.CNTRL_NAME + '_'] = val.PCT_WIDTH;
            });

            console.log(f);

            $scope.$parent.updateGrid(f, $stateParams.itemObj.INV_ITEM_ID, vm.EDIT_INDEX);
            vm.EDIT_MODE = false;
            vm.EDIT_INDEX = -1;
        };

        vm.removeItem = function (idx) {
            $scope.$parent.delFromGrid(idx, $stateParams.itemObj.INV_ITEM_ID);
        };


        vm.printRequisition = function (item) {
            vm.form = item;
            vm.form["SCM_PURC_REQ_H_ID"] = item.pSCM_PURC_REQ_H_ID;
            vm.form.REPORT_CODE = 'RPT-2012';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReportRDLC');
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

        vm.openReportPo = function (pSCM_PURC_REQ_H_ID) {
            Dialog.confirm('Generate PO "' + $scope.$parent.form.PURC_REQ_NO + '.', 'Attention', ['Yes', 'No'])
                .then(function () {
                    if ((pSCM_PURC_REQ_H_ID || -1) < 1) {
                        return;
                    }
                    $window.location.href = '/Purchase/Purchase/GeneralPOList?a=340/#/GeneralPO?pCM_IMP_PO_H_ID=0&pSCM_PURC_REQ_H_ID=' + pSCM_PURC_REQ_H_ID;
                    //$state.go('GeneralPO', { pSCM_PURC_REQ_H_ID: pSCM_PURC_REQ_H_ID }, { reload: true });
                });


            //$window.open('/Merchandising/Mrc/TaBookingPoRpt/' + pSCM_PURC_REQ_H_ID + '/#/RptPoView?pSCM_PURC_REQ_H_ID=' + pSCM_PURC_REQ_H_ID, "_blank", "location=yes,height=570,width=800,scrollbars=yes,status=yes");
        };

        vm.submit = function (valid,id) {
            //alert("");
            //return;
            if ($scope.$parent.PoHeader.$invalid) {
                $scope.$parent.setSubmitted();

                if ($scope.$parent.form.IS_SUP_RT_UPD == 'Y') {
                    $scope.myForm.$submitted = true;
                }

                config.ToastInfoMsg('Please fill up Required Field.');
                return;
            }

            if (!valid) {
                if ($scope.$parent.form.IS_SUP_RT_UPD == 'Y') {
                    $scope.myForm.$submitted = true;
                    config.ToastInfoMsg('Please fill up Required Field.');
                    return;
                }
            }

            var tabs = angular.copy($scope.$parent.tabs);
            var form = angular.copy($scope.$parent.form);
            var itemData = [];

            angular.forEach(tabs, function (val, key) {
                console.log(val);
                itemData.push({
                    SCM_PURC_REQ_D_ID: val.SCM_PURC_REQ_D_ID,
                    MC_FAB_PROD_H_BOM_ID: val.MC_FAB_PROD_H_BOM_ID,
                    INV_ITEM_ID: val.INV_ITEM_ID,
                    QTY_MOU_ID: val.QTY_MOU_ID,
                    INV_ITEM_CAT_ID: val.INV_ITEM_CAT_ID,
                    MC_ACCS_PO_H_ID: val.MC_ACCS_PO_H_ID || -1,
                    ITEM_SPEC_LIST_XML: MrcDataService.xmlStringShortNoTag(val.bomItemList.map(function (o) {
                        return {
                            MC_FAB_PROD_D_BOM_ID: o.MC_FAB_PROD_D_BOM_ID,
                            MC_FAB_PROD_H_BOM_ID: o.MC_FAB_PROD_H_BOM_ID,
                            GMT_COLOR_ID: o.GMT_COLOR_ID,
                            MC_SIZE_ID: o.GMT_SIZE_ID,
                            ORDER_QTY: o.ORDER_QTY,
                            PCT_ALLOW_QTY: o.PCT_ALLOW_QTY,
                            MC_ORD_TRMS_ITEM_ID: o.MC_ORD_TRMS_ITEM_ID,
                            ITEM_SPEC_AUTO: o.ITEM_SPEC_AUTO,
                            CONS_QTY: o.CONS_QTY,
                            BOOK_QTY: o.BOOK_QTY,
                            REV_QTY: o.REV_QTY,
                            NET_BOOK_QTY: o.NET_BOOK_QTY,
                            CONS_MOU_ID: o.CONS_MOU_ID,
                            PURCH_MOU_ID: o.PURCH_MOU_ID,
                            QTY_MOU_ID: o.CONS_MOU_ID == null ? o.QTY_MOU_ID : o.CONS_MOU_ID,
                            PO_QTY: o.PO_QTY,
                            RCV_QTY: o.RCV_QTY,
                            ISSUE_QTY: o.ISSUE_QTY,
                            PROD_COLOR_NAME_EN: o.PROD_COLOR_NAME_EN,
                            PROD_SIZE_CODE: o.PROD_SIZE_CODE,
                            ITEM_CODE: o.ITEM_CODE,
                            PARTICULARS_LST: MrcDataService.xmlStringLongChild(o.PARTICULARS_LST.map(function (x) {
                                return {
                                    CNTRL_NAME: x.CNTRL_NAME,
                                    COL_VAL: x.COL_VAL
                                }
                            }))
                        }
                    }))
                });
            });
            form['XML'] = MrcDataService.xmlStringShort(itemData);
            form['ACCS_PO_DT'] = $filter('date')(form.ACCS_PO_DT, config.appDateFormat);
            form['ACCS_DELV_DT'] = $filter('date')(form.ACCS_DELV_DT, config.appDateFormat);
            form['PURC_REQ_DT'] = $filter('date')(form.PURC_REQ_DT, config.appDateFormat);
            
            form['LK_PURC_PROD_GRP_ID'] = 791;
            form['MC_STYL_BGT_H_ID'] = $stateParams.pMC_STYL_BGT_H_ID;
            form['MC_FAB_PROD_ORD_H_ID'] = $stateParams.pMC_FAB_PROD_ORD_H_ID;
            form['SCM_PURC_REQ_H_ID'] = $stateParams.pSCM_PURC_REQ_H_ID;
            form['RF_ACTN_STATUS_ID'] = $scope.$parent.form.RF_ACTN_STATUS_ID;
            form['IS_UPDATE'] = id;
            
            if ($scope.$parent.form.MC_TNA_TASK_STATUS_ID != $scope.$parent.MC_TNA_TASK_STATUS_ID_OLD) {
                Dialog.confirm('Do you really want option:  `' + $scope.$parent.form.EVENT_NAME + '` selected? <br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.showSplash = true;
                     return MrcDataService.saveDataByUrl(form, '/AccBk/AccSave').then(function (res) {
                         //return MrcDataService.saveDataByUrl(form, '/TaBooking/TotalSave').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                             vm.showSplash = false;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             vm.showSplash = false;
                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 $state.go('TaBooking.item', { pSCM_PURC_REQ_H_ID: parseInt(res.data.OPSCM_PURC_REQ_H_ID) });
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     });
                 }, function () {

                     form['MC_TNA_TASK_STATUS_ID'] = $scope.$parent.MC_TNA_TASK_STATUS_ID_OLD;
                     vm.showSplash = true;
                     return MrcDataService.saveDataByUrl(form, '/AccBk/AccSave').then(function (res) {
                         //return MrcDataService.saveDataByUrl(form, '/TaBooking/TotalSave').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                             vm.showSplash = false;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             vm.showSplash = false;
                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 $state.go('TaBooking.item', { pSCM_PURC_REQ_H_ID: parseInt(res.data.OP_MC_STYL_BGT_H_ID) });
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     });

                 });
            } else {
                form['MC_TNA_TASK_STATUS_ID'] = $scope.$parent.MC_TNA_TASK_STATUS_ID_OLD;
                vm.showSplash = true;
                return MrcDataService.saveDataByUrl(form, '/AccBk/AccSave').then(function (res) {
                    //return MrcDataService.saveDataByUrl(form, '/TaBooking/TotalSave').then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        vm.showSplash = false;
                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            if ($stateParams.pSCM_PURC_REQ_H_ID > 0) {
                                $window.location.reload();
                            } else {
                                $state.go('TaBooking.item', { pSCM_PURC_REQ_H_ID: parseInt(res.data.OP_MC_STYL_BGT_H_ID) });
                            }
                        }

                        config.appToastMsg(res.data.PMSG);
                    }
                });
            }

        }

    }

})();