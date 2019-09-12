(function () {
    'use strict';
    angular.module('multitex.purchase').controller('CertificateListController', ['$q', 'config', 'PurchaseDataService', '$stateParams', '$state', '$scope', CertificateListController]);
    function CertificateListController($q, config, PurchaseDataService, $stateParams, $state, $scope) {

        var vm = this;
        vm.Title = $state.current.Title || '';

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        vm.printRequisition = function (dataItem) {

            vm.form.REPORT_CODE = 'RPT-8001';

            vm.form.SCM_PURC_REQ_H_ID = dataItem.SCM_PURC_REQ_H_ID;

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


        vm.gridOptions = {
            dataSource: new kendo.data.DataSource({
                serverPaging: false,
                serverSorting: false,
                serverFiltering: false,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var pm = PurchaseDataService.kFilterStr2QueryParam(params.filter)
                        PurchaseDataService.getDataByFullUrl('/api/Purchase/AuditCertificate/SelectAll').then(function (res) {
                            //var list = _.filter(res.data, function (x) { return x.LK_LOC_SRC_TYPE_ID === 452 });
                            e.success(res);
                        });
                    }
                },
            }),
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
            columns: [
                //{ field: "REG_SL_NO", title: "SL No", type: "string", width: "10%" },
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
                        return ' <a href="/UPLOAD_DOCS/AUD_CRT_DOCS/{{dataItem.ISS_DOC_PATH}}" target="_blank" class="btn btn-xs yellow-gold"><i class="fa fa-eye"></i>View</a>';
                    },
                    width: "8%"
                },
                {
                    title: "Action",
                    template: function () {
                        //return '<a ui-sref="Certificate({pHR_AUD_CERT_REG_ID:dataItem.HR_AUD_CERT_REG_ID})" ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ') && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> Edit</i></a>' +
                        return '<a ui-sref="Certificate({pHR_AUD_CERT_REG_ID:dataItem.HR_AUD_CERT_REG_ID})" class="btn btn-xs blue"><i class="fa fa-edit"> Edit</i></a>' +
                            ' <a ui-sref="Certificate({pHR_AUD_CERT_REG_ID:dataItem.HR_AUD_CERT_REG_ID, pIS_NEW_OR_R:' + "'R'" + '})" class="btn btn-xs green"><i class="fa fa-edit"> Renewal</i></a>';

                    },
                    width: "10%"
                },
            ]
        };
    }

})();