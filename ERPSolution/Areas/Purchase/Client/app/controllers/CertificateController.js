
(function () {
    'use strict';
    angular.module('multitex.purchase').controller('CertificateController', ['$q', 'config', 'PurchaseDataService', '$stateParams', '$state', '$scope', 'formData', 'Dialog', '$http', '$modal', 'commonDataService', 'cur_user_id', '$filter', CertificateController]);
    function CertificateController($q, config, PurchaseDataService, $stateParams, $state, $scope, formData, Dialog, $http, $modal, commonDataService, cur_user_id, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.form = formData.HR_AUD_CERT_REG_ID ? formData : { issue_docs: [], apply_docs: [] };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getUserData(), GetBuyerList(), GetCompanyList(), GetSupplierList(), GetStatusList(), GetApplyStatusList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        if ($stateParams.pIS_NEW_OR_R == 'R') {
            vm.form.IS_NEW_OR_R = 'R';
        }


        if (formData.HR_AUD_CERT_REG_ID > 0) {
            vm.form.issue_docs = [{ 'ISS_DOC_PATH': formData.ISS_DOC_PATH }];
            vm.form.apply_docs = [{ 'APP_DOC_PATH': formData.APP_DOC_PATH }];
        }
        else {
            vm.form.IS_NEW_OR_R = 'N';
        }

        $scope.APPLY_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.APPLY_DT_LNopened = true;
        }

        $scope.ISSUE_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.ISSUE_DT_LNopened = true;
        }

        $scope.VALID_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.VALID_DT_LNopened = true;
        }

        $scope.AUDIT_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.AUDIT_DT_LNopened = true;
        }


        vm.addToIssueDocGrid = function (data) {
            data['ISS_DOC_PATH'] = new Date().getTime() + getExtention(data.ATT_FILE.name);
            vm.form.issue_docs.push(data);
            if (vm.form.issue_docs.length > 0) {
                vm.disableIssue = true;
            } else {
                vm.disableIssue = false;
            }
            vm.fileIssue = {};
        }

        vm.addToApplyDocGrid = function (data) {
            data['APP_DOC_PATH'] = new Date().getTime() + getExtention(data.ATT_FILE.name);
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


        vm.removeIssueDoc = function (index) {

            vm.form.issue_docs.splice(index, 1);

            if (vm.form.issue_docs.length > 0) {
                vm.disableIssue = true;
            } else {
                vm.disableIssue = false;
            }
        }

        vm.removeApplyDoc = function (index) {

            vm.form.apply_docs.splice(index, 1);

            if (vm.form.apply_docs.length > 0) {
                vm.disableApply = true;
            } else {
                vm.disableApply = false;
            }
        }


        vm.loadCT = function (e) {

            var item = e.sender.dataItem(e.item);
            console.log(item);
            vm.form.ISS_BY_ORG_NAME = item.ISS_BY_ORG_NAME;
            vm.form.RESP_EMP_NAME = item.RESP_EMP_NAME;
            vm.form.RESP_EMP_ID = item.RESP_EMP_ID;
            vm.form.MC_BUYER_ID_LST = item.MC_BUYER_ID_LST;
            vm.form.LK_AUD_DOC_TYPE_ID = item.LK_AUD_DOC_TYPE_ID;
            vm.form.CERT_TYPE_TITLE = item.CERT_TYPE_TITLE;
        }

        vm.clearBuyerList = function () {
            vm.form.MC_BUYER_LST = '';
        }


        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 27;
            var PARENT_ID = 0;

            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;

            var sList = null;
            PurchaseDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                var sList = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "DN" })
                console.log(sList);
                if (sList.length == 1) {
                    vm.form.RF_ACTN_VIEW = 0;
                    vm.form.RF_ACTN_STATUS_ID = sList[0].RF_ACTN_STATUS_ID;
                    vm.form.ACTN_STATUS_NAME = sList[0].ACTN_STATUS_NAME;
                    //alert(vm.form.ACTN_STATUS_NAME);
                }
                else {
                    vm.form.RF_ACTN_VIEW = 1;
                }
            });

            return vm.statusList = {
                optionLabel: "-- Select Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                                var lst = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "DN" })
                                if (lst.length == 1) {
                                    vm.form.RF_ACTN_VIEW = 0;
                                    vm.form.RF_ACTN_STATUS_ID = lst[0].RF_ACTN_STATUS_ID;
                                    vm.form.ACTN_STATUS_NAME = lst[0].ACTN_STATUS_NAME;
                                }
                                else {
                                    vm.form.RF_ACTN_VIEW = 1;
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
                dataValueField: "SC_USER_ID"
            };

            //$("#customers").kendoDropDownList(vm.userList);
        }


        vm.certificateTypeList = new kendo.data.DataSource({
            transport: {
                read: function (e) {

                    PurchaseDataService.getDataByFullUrl('/api/Purchase/CertificateType/SelectAll').then(function (res) {
                        e.success(res);

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });


        function GetApplyStatusList() {
            PurchaseDataService.LookupListData(131).then(function (res) {
                vm.AuditType = res;
            });
            return vm.applyStatusList = {
                optionLabel: "-- Select Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.LookupListData(132).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
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
                            PurchaseDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
        };

        vm.departmentList = {
            optionLabel: "--Select Dept--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        PurchaseDataService.getDataByFullUrl('/Hr/Admin/HrDesignation/DepartmentListData').then(function (res) {

                            e.success(res);
                        });
                    }
                }
            },
            dataTextField: "DEPARTMENT_NAME_EN",
            dataValueField: "HR_DEPARTMENT_ID"
        };

        function GetSupplierList() {
            return vm.supplierList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PurchaseDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.printRequisition = function (dataItem) {

            vm.form.REPORT_CODE = 'RPT-8001';

            vm.form.HR_AUD_CERT_REG_ID = dataItem.HR_AUD_CERT_REG_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Pur/PurReport/PreviewReportRDLC');
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

        vm.loadCertificateList = function () {
            if (vm.form.IS_HISTORY == true)
                loadOldList();
        }

        function loadOldList() {
            return vm.gridOptionsDS = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PurchaseDataService.getDataByFullUrl('/api/Purchase/AuditCertificate/GetOldCertificateByID?pHR_COMPANY_ID=' + vm.form.HR_COMPANY_ID + '&pRF_AUD_CERT_TYPE_ID=' + vm.form.RF_AUD_CERT_TYPE_ID).then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
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
            sortable: true,
            columns: [
                //{ field: "REG_SL_NO", title: "SL No", type: "string", width: "10%" },
                //{ field: "IS_NEW_OR_R", title: "Type", type: "string", width: "10%" },
                { field: "DOC_REF_NO", title: "Document Ref#", type: "string", width: "10%" },
                { field: "ORG_DOC_NO", title: "Document #", type: "string", width: "10%" },
                { field: "IS_NEW_OR_R", title: "Type", type: "string", width: "7%" },
                { field: "APPLY_DT", title: "Apply Date", type: "date", template: "#= kendo.toString(kendo.parseDate(APPLY_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "ISSUE_DT", title: "Issue Date", type: "date", template: "#= kendo.toString(kendo.parseDate(ISSUE_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "VALID_DT", title: "Expiry Date", type: "date", template: "#= kendo.toString(kendo.parseDate(VALID_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "CERT_TYPE_TITLE", title: "Title", type: "string", width: "8%" },
                { field: "ISS_BY_ORG_NAME", title: "Org. Name", type: "string", width: "10%" },
                //{ field: "CERT_TYPE_DESC", title: "CERT_TYPE_DESC", type: "string", width: "10%" },
                { field: "RESP_EMP_NAME", title: "Responsible Person", type: "string", width: "12%" },
                {
                    title: "Attached doc",
                    template: function () {
                        return ' <a href="/UPLOAD_DOCS/AUD_CRT_DOCS/{{dataItem.ISS_DOC_PATH}}" target="_blank" class="btn btn-xs green"><i class="fa fa-eye"></i>View</a>';
                    },
                    width: "8%"
                },
                //Document #	Company For	Apply Date	Issue Date	Expiry Date	Responsible Person	Applied By	Attached doc
            ]
        };


        vm.openCapModal = function (data) {
            console.log(data);
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'CorrectiveActionPlan.html',
                controller: function ($scope, $modalInstance, capList, formCap) {

                    $scope.capList = (capList || []);
                    $scope.cap = formCap.HR_AUD_CERT_REG_ID ? formCap : { HR_AUD_CERT_REG_ID: 0 };

                    $scope.cancel = function (data) {
                        $modalInstance.close();
                    }


                    $scope.addToCapDocGrid = function (data) {
                        data['CAP_DOC_PATH'] = new Date().getTime() + getExtentionCap(data.ATT_FILE.name);
                        $scope.capList.push(data);
                        $scope.fileCap = {};
                    }


                    function getExtentionCap(fileName) {
                        var i = fileName.lastIndexOf('.');
                        if (i === -1) return false;
                        return fileName.slice(i)
                    }

                    $scope.saveList = function (data, key) {
                        console.log(key);
                        var dataitem = {};

                        var getModelAsFormDataCap = function (data) {
                            var dataAsFormData = new FormData();
                            angular.forEach(data, function (value, key) {
                                dataAsFormData.append(key, value);
                            });
                            return dataAsFormData;
                        };


                        angular.forEach($scope.capList, function (val, k) {
                            //console.log(val);
                            $http({
                                method: 'post',
                                url: '/Purchase/Purchase/uploadAuditCertificateCap',
                                data: getModelAsFormDataCap(val),
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

                        dataOri["XML_CAP"] = PurchaseDataService.xmlStringShort($scope.capList.map(function (o) {
                            return {
                                HR_AUD_CERT_CAP_ID: o.HR_AUD_CERT_CAP_ID == null ? 0 : o.HR_AUD_CERT_CAP_ID,
                                HR_AUD_CERT_REG_ID: o.HR_AUD_CERT_REG_ID == null ? 0 : o.HR_AUD_CERT_REG_ID,
                                CAP_DOC_PATH: o.CAP_DOC_PATH,
                            }
                        }));

                        return PurchaseDataService.saveDataByFullUrl(dataOri, '/api/Purchase/AuditCertificate/SaveCap').then(function (res) {

                            if (res.success === false) {
                                vm.errors = res.errors;
                            }
                            else {

                                res['data'] = angular.fromJson(res.jsonStr);

                                config.appToastMsg("MULTI-001 Data has been updated successfully");

                            }
                        });

                    }
                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    capList: function (PurchaseDataService) {
                        return PurchaseDataService.getDataByFullUrl('/api/Purchase/AuditCertificate/GetCapByID?pHR_AUD_CERT_REG_ID=' + (data.HR_AUD_CERT_REG_ID || 0));
                    },
                    formCap: function () {
                        return data;
                    }
                }
            });
            modalInstance.result.then(function (dataItem) {
                console.log(dataItem);
                vm.OrderColorGroupList = [];
                vm.OrderColorGroupList = dataItem;
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });

        };

        vm.submitAll = function (data, key) {

            //if (fnValidate() == true) {


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
                    url: '/Purchase/Purchase/uploadAuditCertificate',
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

            angular.forEach(data.issue_docs, function (val, k) {
                //console.log(val);
                $http({
                    method: 'post',
                    url: '/Purchase/Purchase/uploadAuditCertificateOrg',
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

            //var data = angular.copy(item);
            //console.log(data);

            var _apdate = $filter('date')(dataOri.APPLY_DT, 'yyyy-MM-ddTHH:mm:ss');
            dataOri["APPLY_DT"] = _apdate;

            var _isudate = $filter('date')(dataOri.ISSUE_DT, 'yyyy-MM-ddTHH:mm:ss');
            dataOri["ISSUE_DT"] = _isudate;

            var _valdate = $filter('date')(dataOri.VALID_DT, 'yyyy-MM-ddTHH:mm:ss');
            dataOri["VALID_DT"] = _valdate;

            var _auddate = $filter('date')(dataOri.AUDIT_DT, 'yyyy-MM-ddTHH:mm:ss');
            dataOri["AUDIT_DT"] = _auddate;

            if (data.issue_docs.length > 0)
                dataOri["ISS_DOC_PATH"] = (data.issue_docs[0].ISS_DOC_PATH || '');
            if (data.apply_docs.length > 0)
                dataOri["APP_DOC_PATH"] = (data.apply_docs[0].APP_DOC_PATH || '');

            var cl = _.filter(vm.AuditType, function (x) { return x.LOOKUP_DATA_ID == vm.form.LK_AUD_DOC_TYPE_ID });

            dataOri["IS_LICENCE"] = (cl[0].DATA_PREFIX || 'C');
            dataOri["MC_BUYER_ID_LST"] = '0';

            var id = vm.form.HR_AUD_CERT_REG_ID;

            return PurchaseDataService.saveDataByFullUrl(dataOri, '/api/Purchase/AuditCertificate/Save').then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);

                    if (id > 0) {
                        config.appToastMsg("MULTI-002 Data has been updated successfully");
                        $state.go($state.current, { pHR_AUD_CERT_REG_ID: id }, { reload: true });
                    }
                    else {
                        config.appToastMsg(res.data.PMSG);
                        if (res.data.OPHR_AUD_CERT_REG_ID)
                            $state.go($state.current, { pHR_AUD_CERT_REG_ID: res.data.OPHR_AUD_CERT_REG_ID }, { reload: true });
                    }

                }
            });
            //}
        };
    }


})();