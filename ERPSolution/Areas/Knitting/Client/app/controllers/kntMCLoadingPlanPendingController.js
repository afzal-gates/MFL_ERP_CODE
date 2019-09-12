(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntMCLoadingPlanPendingController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', '$modal', '$filter', KntMCLoadingPlanPendingController]);
    function KntMCLoadingPlanPendingController($q, config, KnittingDataService, $stateParams, $state, $scope, $modal, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.params = $stateParams;
        vm.errors = null;

        vm.multiCard = { KNT_JOB_CRD_LIST: [] };
        vm.data = {};
        vm.form = { pOption: 3000 };
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [queryDataSource(3000), getFloorList(), getMcDiaList(), getKnitCardList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        $scope.datePickerOptions = {
            parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
        };


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
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);
                        
                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });

                    }
                }
            });
        }

        vm.printMultiKnitCard = function () {
            var url = '/api/Knit/KntReport/PreviewReport';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.multiCard.REPORT_CODE = 'RPT-3564';

            if (vm.multiCard.KNT_JOB_CRD_LIST) {
                vm.multiCard.KNT_JOB_CRD_H_ID_LST = vm.multiCard.KNT_JOB_CRD_LIST.join(',');
            }

            var params = angular.copy(vm.multiCard);

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


        function getFloorList() {
            return KnittingDataService.getDataByFullUrl('/api/hr/GetProdFloorList?pLK_PFLR_TYP_ID=527&pOption=3003').then(function (res) {
                vm.floorList = [{
                    FLOOR_DESC_EN : 'All',
                    HR_PROD_FLR_ID : null
                }].concat(res.map(function (o) {
                    return {
                        FLOOR_DESC_EN: o.FLOOR_DESC_EN,
                        HR_PROD_FLR_ID: o.HR_PROD_FLR_ID
                    }
                }));
            });
        };

        vm.onChangeOption = function (data) {
            queryDataSource(data.pOption, (data.HR_PROD_FLR_ID || null), (data.ACT_MC_DIA_ID || null),'', (data.LK_COL_TYPE_ID || -1), (data.IS_RIB || 'M'));
        };

        function queryDataSource(pOption, pHR_PROD_FLR_ID, pACT_MC_DIA_ID, pWORK_STYLE_NO, pLK_COL_TYPE_ID, pIS_RIB) {
            vm.pendingKnitCardDs = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/knit/KnitPlan/getKnitCardPendingList?pOption=' + pOption + '&pHR_PROD_FLR_ID=' + (pHR_PROD_FLR_ID || null) + '&pACT_MC_DIA_ID=' + (pACT_MC_DIA_ID || null) + ' &pWORK_STYLE_NO=' + (pWORK_STYLE_NO || '') + '&pLK_COL_TYPE_ID=' + (pLK_COL_TYPE_ID || -1) + '&pIS_RIB=' + (pIS_RIB||'M');
                            return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                                e.success(res);
                            });

                    }
                }
            });
        }

        vm.onChangeFloor = function (pHR_PROD_FLR_ID) {

            vm.form['WORK_STYLE_NO'] = '';
            vm.form['ACT_MC_DIA_ID'] = '';
            vm.form['LK_COL_TYPE_ID'] = -1;
            vm.form['IS_RIB'] = 'M';
            vm.form['pOption'] = 3000;

            queryDataSource(3000, pHR_PROD_FLR_ID);
        };

        vm.onChangeStyleOrderChange = function (data) {
            queryDataSource(3000, (data.HR_PROD_FLR_ID || null), (data.ACT_MC_DIA_ID || null), data.WORK_STYLE_NO, (data.LK_COL_TYPE_ID||-1),(data.IS_RIB||'M'));
        };

        vm.onChangeMcDia = function (e) {
            var item = e.sender.dataItem(e.sender.item);
            vm.form['WORK_STYLE_NO'] = '';
            queryDataSource(3000, (vm.form.HR_PROD_FLR_ID || null), (item.KNT_MC_DIA_ID || null), '', (vm.form.LK_COL_TYPE_ID || -1), (vm.form.IS_RIB || 'M'));
        };

        function getKnitMachineList(ACT_MC_DIA_ID, HR_PROD_FLR_ID) {
            return KnittingDataService.getDataByUrl('/KnitPlan/getKnitMachine?pACT_MC_DIA_ID=' + (ACT_MC_DIA_ID || null) + '&pHR_PROD_FLR_ID=' + (HR_PROD_FLR_ID||null)).then(function (res) {
                vm.KnitMachineListDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }




        function setSchedule(KNT_MACHINE_ID, DURATION, KNT_JOB_CRD_H_ID) {
            return KnittingDataService.getDataByUrl('/KnitPlan/getScheduleByMachine?pKNT_MACHINE_ID=' + KNT_MACHINE_ID + '&pDURATION=' + DURATION + '&pKNT_JOB_CRD_H_ID=' + KNT_JOB_CRD_H_ID).then(function (res) {

                vm.data['START_DT'] = res.START_DT;
                vm.data['END_DT'] = res.END_DT;

            });
        }

        $scope.$watchGroup(['vm.data.ASIGN_QTY', 'vm.data.TG_D_PROD_QTY'], function (newVal, oldVal) {
            if (vm.data.KNT_MACHINE_ID && vm.data.KNT_JOB_CRD_H_ID) {
                if (!isNaN(Math.ceil(newVal[0] * (24 / (newVal[1] || 1))))) {
                    vm.data['DURATION'] = Math.ceil(newVal[0] * (24 / (newVal[1] || 1)));

                    setSchedule(vm.data.KNT_MACHINE_ID, Math.ceil(newVal[0] * (24 / (newVal[1] || 1))), vm.data.KNT_JOB_CRD_H_ID);
                }
            }
        });

        $scope.$watch('vm.data.START_DT', function (newVal, oldVal) {
            if (newVal && oldVal) {
                if (vm.data.ASIGN_QTY > 0 && vm.data.TG_D_PROD_QTY > 0 && vm.data.DURATION > 0) {
                    vm.data['END_DT'] = new DayPilot.Date(newVal).addHours(vm.data.DURATION).toStringSortable();
                }
            }
        });

        //$scope.$watch('vm.data.END_DT', function (newVal, oldVal) {
        //    if (newVal && oldVal && vm.data.START_DT) {

        //        var a = moment(newVal);
        //        var b = moment('2016-05-06T20:03:55');

        //        console.log(a.diff(b, 'minutes')) // 44700
        //        console.log(a.diff(b, 'hours')) // 745



        //        if (vm.data.ASIGN_QTY > 0 && vm.data.TG_D_PROD_QTY > 0 && vm.data.DURATION > 0) {
        //            vm.data['END_DT'] = new DayPilot.Date(newVal).addHours(vm.data.DURATION).toStringSortable();
        //        }
        //    }
        //});


        vm.onMachineChange = function (e) {
            if (e.sender.dataItem(e.sender.item).KNT_MACHINE_ID && (vm.data.TG_D_PROD_QTY || 0) > 0 && (vm.data.ASIGN_QTY || 0) > 0) {
                vm.data['DURATION'] = Math.ceil(vm.data.ASIGN_QTY * (24 / (vm.data.TG_D_PROD_QTY || 1)));
                setSchedule(e.sender.dataItem(e.sender.item).KNT_MACHINE_ID,vm.data.DURATION);
            }
        };


        function getMcDiaList() {
            return KnittingDataService.getDataByUrl('/KnitCommon/getMachineDiaList').then(function (res) {
                vm.machineDiaDs = new kendo.data.DataSource({
                    data: res
                });
            });
        };

        vm.submitData = function (dataOri, isValid) {

            if (!isValid)
                return;

            if (!dataOri.KNT_MACHINE_ID) {
                $scope.KnittingPendingForm.KNT_MACHINE_ID.$setValidity('required', false);
            }

            var data = {
              
                KNT_JOB_CRD_H_ID: dataOri.KNT_JOB_CRD_H_ID,
                ASIGN_QTY: dataOri.ASIGN_QTY,
                TG_D_PROD_QTY: dataOri.TG_D_PROD_QTY,
                START_DT: dataOri.START_DT,
                END_DT: dataOri.END_DT,
                KNT_MACHINE_ID: dataOri.KNT_MACHINE_ID
            }

            return KnittingDataService.saveDataByUrl(data, '/KnitPlan/UpdateJobCardData').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $state.go('KntPendingLoadingPlan',vm.params, { reload: true });
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        function pendingKnitCardList(st,end) {
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/knit/KntReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = angular.extend({ REPORT_CODE: 'RPT-3521' }, {
                FROM_DATE: st,
                TO_DATE : end
            });

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



        vm.pendingKnitCardList = function (data) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'StartEndDateModal.html',
                controller: function ($scope, $modalInstance) {
                    $scope.showSplash = true;
                    activate();

                    $scope.today = new Date();
                    $scope.dtFormat = config.appDateFormat;
                    $scope.formInvalid = false;

                    $scope.dateOptions = {
                        formatYear: 'yy',
                        startingDay: 6
                    };

                    $scope.form = {
                        FROM_DATE: $scope.today,
                        TO_DATE: $scope.today,
                    };

                    $scope.RevisionDate_LNopen = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.RevisionDate_LNopened = true;
                    }
                    $scope.RevisionDate_LNopen2 = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.RevisionDate_LNopened2 = true;
                    }


                    $scope.save = function (token, valid) {
                        if (!valid) return;
                        $modalInstance.close($scope.form);
                    };


                    $scope.cancel = function () {
                        $modalInstance.dismiss('cancel');
                    };
                },
                size: 'small',
                windowClass: 'large-Modal'
            });

            modalInstance.result.then(function (data) {


                pendingKnitCardList(
                         $filter('date')(data.FROM_DATE),
                         $filter('date')(data.TO_DATE)
                     );
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };

        vm.pendingKnitCardOpt = {
            autoBind: true,
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
            change: function (e) {

                var item = e.sender.dataItem(this.select());

                console.log(item);

                $scope.$apply(function () {
                    vm.date = {
                        START_DT: item.START_DT,
                        END_DT: item.END_DT
                    };
                    vm.data = item;
                    getKnitMachineList(item.ACT_MC_DIA_ID, item.HR_PROD_FLR_ID);
                });
             
            },
            selectable: true,
            sortable: true,
            pageable: false,
            height: 600,
            scrollable: {
                virtual: true
            },
            columns: [
                { field: "JOB_CRD_NO", title: "KntCrd#", type: "string", width: "60px", filterable: false, template: '#:JOB_CRD_NO# <small style="display:block;">(#:ASIGN_QTY-UN_ASIGN_QTY# Kg)</small>' },
                { field: "BYR_ACC_NAME_EN", title: "Byr Acc/Style", type: "string", width: "80px", filterable: false, template: '#:BYR_ACC_NAME_EN# <br><small>#:WORK_STYLE_NO#</small>' },
                { field: "WORK_STYLE_NO", title: "Order/ShipDt", width: "80px", filterable: false, template: '#:ORDER_NO_LIST# <small style="display:block;color:red;">{{dataItem.SHIP_DT|date:"dd-MMM-yyyy"}}</small>' },
                { field: "FAB_TYPE_NAME", title: "FabType", type: "string", width: "60px", filterable: true },
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "70px", filterable: false },
                { field: "MC_DIA_GG", title: "McxGge", type: "string", width: "70px", filterable: false },
                { field: "FIN_DIA_N_DIA_TYPE", title: "Fin.Dia", type: "string", width: "70px", filterable: false },
                //{ field: "YRN_DETAILS", title: "Yarn Details", type: "string", width: "100px", filterable: true }
            ]
        };

    }

})();