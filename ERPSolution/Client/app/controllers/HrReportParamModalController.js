(function () {
    'use strict';

    angular.module('multitex').controller('HrReportParamModalController', ['$modalInstance', '$q', '$scope', '$http', '$state', '$filter', 'reportCode', 'config', 'DashBoardService', 'access_token', HrReportParamModalController]);

    function HrReportParamModalController($modalInstance, $q, $scope, $http, $state, $filter, reportCode, config, DashBoardService, access_token) {
        $scope.showSplash = true;
        activate();

        $scope.REPORT_CODE = angular.copy(reportCode);

        //alert($scope.REPORT_CODE);
        if ($scope.REPORT_CODE == 'RPT-1052') {
            //alert('ok');
            $scope.HR_MANAGEMENT_TYPE_ID = 2;
            $scope.REPORT_TYPE_ID = 1;
            $scope.SALARY_PART_PCT = 70;
        }

        $scope.dtFormat = config.appDateFormat;
        $scope.formInvalid = false;
                

        function activate() {
            var promise = [getCompanyOpenPeriod()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                $scope.showSplash = false;
            });
        }

        ////////////////////////////////////////////////////
        function getCompanyOpenPeriod() {
            return $scope.companyOpenPeriodList = {
                optionLabel: "-- Select Period --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'get',
                                url: "/api/common/GetAccPayPeriod/?pHR_COMPANY_ID=" + $scope.HR_COMPANY_ID + "&pHR_PERIOD_TYPE_ID=" + 3 + "&pIS_SHOW4_RPT=Y",
                                headers: { 'Authorization': 'Bearer ' + access_token }
                                //method: 'post',
                                //url: "/Hr/HrDisciplinAction/CompanyOpenPeriodListData",  //+ "&pType=" + showType
                                //params: {
                                //    pHR_COMPANY_ID: $scope.HR_COMPANY_ID,
                                //    pHR_PERIOD_TYPE_ID: 3
                                //}
                            }).
                            success(function (data, status, headers, config) {
                                e.success(data);
                                if (data.length > 0) {
                                    //vm.form.ACC_PAY_PERIOD_ID = data[0].ACC_PAY_PERIOD_ID;
                                    $scope.FROM_DATE = data[0].START_DATE;
                                    $scope.TO_DATE = data[0].END_DATE;
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
                    //vm.form.ACC_PAY_PERIOD_ID = dataItem.ACC_PAY_PERIOD_ID;
                    var dt = moment(dataItem.START_DATE)._d;
                    $scope['FROM_DATE'] = dataItem.START_DATE == null ? '' : $filter('date')(dt, $scope.dtFormat);
                    dt = moment(dataItem.END_DATE)._d;
                    $scope['TO_DATE'] = dataItem.END_DATE == null ? '' : $filter('date')(dt, $scope.dtFormat);
                }
            };
        };

        
        $scope.reportPrint = function () {
            //vm.form.REPORT_CODE = 'RPT-1021';

            //console.log($scope);
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/Hr/HrReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = $scope;
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


        //$scope.$watch('Task.ACT_START_DT', function (newVal, oldVal) {

        //    if(!moment(newVal).isSame(moment(oldVal))){
        //        if (moment(newVal).isSame(moment($scope.CURR_DATE)) || moment(newVal).isAfter(moment($scope.CURR_DATE))) {
        //            $scope.formInvalid = false;
        //        } else {
        //            $scope.formInvalid = true;
        //        }
        //    }
        //});


        //$scope.save = function (data, token,valid) {
        //    if (!valid) return;
        //    return DashBoardService.saveDataByUrl(data,'/api/common/SaveTnAData', token).then(function (res) {
        //        data['data'] = angular.fromJson(res.jsonStr);
        //        config.appToastMsg(data.data.PMSG);
        //        if (data.data.PMSG.substr(0, 9) == 'MULTI-001') {
        //            var a = moment(data.PLAN_START_DT);
        //            var b = moment(data.ACT_START_DT);

        //            $scope.OrderTnaData[$scope.Order.index].items[$scope.Task.index]['ACT_START_DT'] = data.ACT_START_DT;
        //            $scope.OrderTnaData[$scope.Order.index].items[$scope.Task.index]['LAG_DAYS'] = b.diff(a, 'days');
        //        }

        //    }, function (err) {
        //        console.log(err);
        //    });

        //    $modalInstance.close(data);
        //};

        //$scope.ACT_START_DTopen = function ($event) {
        //    $event.preventDefault();
        //    $event.stopPropagation();
        //    $scope.ACT_START_DTopened = true;
        //};


        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})();