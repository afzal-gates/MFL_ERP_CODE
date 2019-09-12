
(function () {
    'use strict';
    angular.module('multitex.purchase').controller('CertificateTypeController', ['$q', 'config', 'PurchaseDataService', '$stateParams', '$state', '$scope', '$http', CertificateTypeController]);
    function CertificateTypeController($q, config, PurchaseDataService, $stateParams, $state, $scope, $http) {

        var vm = this;
        vm.Title = $state.current.Title || '';

        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.form = { RF_AUD_CERT_TYPE_ID: 0, IS_NOTIFY_EMAIL: 'Y', IS_ACTIVE: 'Y', apply_docs: [], NOTIFY_BF_DAYS: 45 };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getUserData(), getOrgList(), GetAuditTypeList(), GetBuyerList(), GetCertificateList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        vm.emptyVal = function () {
            if (vm.form.IS_NOTIFY_EMAIL == "N")
                vm.form.NOTIFY_BF_DAYS = 0;
            else
                vm.form.NOTIFY_BF_DAYS = 45;
        }


        vm.addToApplyDocGrid = function (data) {
            data['CERT_LOGO'] = new Date().getTime() + getExtention(data.ATT_FILE.name);
            vm.form.apply_docs.push(data);
            if (vm.form.apply_docs.length > 0) {
                vm.disableApply = true;
            } else {
                vm.disableApply = false;
            }
            vm.fileApply = {};
        }


        function getExtention(fileName) {
            var i = fileName.lastIndexOf('.');
            if (i === -1) return false;
            return fileName.slice(i)
        }


        vm.removeAddedDoc = function (index) {

            vm.form.apply_docs.splice(index, 1);

            if (vm.form.apply_docs.length > 0) {
                vm.disableApply = true;
            } else {
                vm.disableApply = false;
            }
        }

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
                            PurchaseDataService.getUserDatalist().then(function (res) {
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
                dataValueField: "HR_EMPLOYEE_ID"
            };

            //$("#customers").kendoDropDownList(vm.userList);
        }


        $scope.LAST_ISSUE_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.LAST_ISSUE_DT_LNopened = true;
        }



        function GetBuyerList() {

            return vm.buyerList = {
                optionLabel: "-- Select Buyer --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.getDataByFullUrl('/api/mrc/buyer/SelectAll').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "BUYER_NAME_EN",
                dataValueField: "MC_BUYER_ID"
            };
        };


        function GetAuditTypeList() {

            return vm.auditTypeList = {
                optionLabel: "-- Select Audit Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.LookupListData(131).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };

        };

        function getOrgList() {
            return vm.orgList = {
                optionLabel: "-- Select Organization --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.getDataByFullUrl('/api/purchase/CertificateType/GetOrganization').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "ISS_ORG_NAME_EN",
                dataValueField: "RF_DOC_ISS_ORG_ID"
            };
        }


        function GetCertificateList() {
            return vm.gridOptionsDS = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PurchaseDataService.getDataByFullUrl('/api/Purchase/CertificateType/SelectAll').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                },
                pageSize: 10
            });
        };

        vm.gridOptions = {
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
            selectable: "row",
            sortable: true,
            pageSize: 10,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [

                { field: "RESP_EMP_ID", hidden: true },
                { field: "RF_AUD_CERT_TYPE_ID", hidden: true },
                { field: "LK_AUD_DOC_TYPE_ID", hidden: true },
                { field: "RF_DOC_ISS_ORG_ID", hidden: true },
                { field: "LK_AUD_DOC_TYPE_ID", hidden: true },
                { field: "MC_BUYER_NAME_LST", hidden: true },
                { field: "MC_BUYER_ID_LST", hidden: true },

                { field: "CERT_TYPE_CODE", title: "Code", type: "string", width: "7%" },
                { field: "CERT_TYPE_TITLE", title: "TITLE", type: "string", width: "10%" },
                { field: "CERT_TYPE_DESC", title: "CERT TYPE DESC", type: "string", width: "15%" },
                { field: "ISS_BY_ORG_NAME", title: "ISSUE BY", type: "string", width: "10%" },
                { field: "ORG_WEB_LINK", title: "WEB LINK", type: "string", width: "10%" },
                { field: "CERT_LOGO", title: "LOGO", type: "string", width: "10%" },
                { field: "IS_ACTIVE", title: "Status", type: "string", width: "7%" },
                { field: "DISPLAY_ORDER", title: "DISPLAY ORDER", type: "string", width: "5%" },
                { field: "IS_NOTIFY_EMAIL", title: "IS NOTIFY EMAIL", type: "string", width: "5%" },
                { field: "NOTIFY_BF_DAYS", title: "NOTIFY DAYS", type: "string", width: "5%" },
                //{ field: "LAST_ISSUE_DT", title: "Update Date", type: "date", template: "#= kendo.toString(kendo.parseDate(LAST_ISSUE_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },

                {
                    title: "Action",
                    //template: '<a  title="Delete" ng-click="vm.removeData(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a> <a  title="Edit" ng-click="vm.editData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    template: '<a  title="Edit" ng-click="vm.editData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "7%"
                }
            ]
        };


        vm.editData = function (data) {
            vm.form = angular.copy(data);
            vm.form['MC_BUYER_ID_LST'] = data.MC_BUYER_ID_LST ? data.MC_BUYER_ID_LST.split(',') : [];
            vm.form.apply_docs = [{ 'CERT_LOGO': angular.copy(data.CERT_LOGO) }];
        }

        vm.resetAll = function () {
            vm.form = { RF_AUD_CERT_TYPE_ID: 0, IS_NOTIFY_EMAIL: 'N', IS_ACTIVE: 'N', LK_AUD_DOC_TYPE_ID: '', RF_DOC_ISS_ORG_ID: '', MC_BUYER_ID_LST: [], RESP_EMP_ID: '' };
        }

        vm.submitAll = function (data, key) {

            if (fnValidate() == true) {

                //var data = angular.copy(dataOri);

                var getModelAsFormData = function (data) {
                    var dataAsFormData = new FormData();
                    angular.forEach(data, function (value, key) {
                        dataAsFormData.append(key, value);
                    });
                    return dataAsFormData;
                };


                angular.forEach(data.apply_docs, function (val, k) {
                    //console.log(val);
                    $http({
                        method: 'post',
                        url: '/Purchase/Purchase/uploadDocLogo',
                        data: getModelAsFormData(val),
                        transformRequest: angular.identity,
                        headers: { 'Content-Type': undefined, "RequestVerificationToken": key }
                    }).success(function (data, status, headers, config1) {
                        console.log(status);
                    }).
                      error(function (data, status, headers, config) {
                          console.log(status);
                      });
                });

                var dataOri = angular.copy(data);
                dataOri["MC_BUYER_ID_LST"] = !dataOri.MC_BUYER_ID_LST ? '' : dataOri.MC_BUYER_ID_LST.join(',');

                if (data.apply_docs.length > 0)
                    dataOri["CERT_LOGO"] = (data.apply_docs[0].CERT_LOGO || '');


                return PurchaseDataService.saveDataByUrl(dataOri, '/CertificateType/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        config.appToastMsg(res.data.PMSG);
                        vm.form = { RF_AUD_CERT_TYPE_ID: 0, IS_NOTIFY_EMAIL: 'Y', IS_ACTIVE: 'Y', LK_AUD_DOC_TYPE_ID: '', RF_DOC_ISS_ORG_ID: '', MC_BUYER_ID_LST: [], RESP_EMP_ID: '', NOTIFY_BF_DAYS: 45 };
                        GetCertificateList();
                    }
                });
            }
        };


    }

})();

