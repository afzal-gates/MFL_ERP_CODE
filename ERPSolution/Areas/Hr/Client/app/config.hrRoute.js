(function () {
    'use strict';

    angular
        .module('multitex.hr')
        .run(appRun);

    /* @ngInject */
    function appRun(routerHelper) {

        routerHelper.configureStates(getStates(), '/');


    }

    function getStates() {
        return [

             //////////Start For Assign Employee Bank Account
            {
                state: 'EmpBankAc',
                config: {
                    url: '/empbankac',
                    controller: 'HrAssignEmpAccountController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrAssignEmpAccount/AssignEmpBkAcc',
                    Title: 'Assign Employee Bank A/C'
                }
            },
            //////////End For Assign Employee Bank Account

            //////////Start For Hr Report
            {
                state: 'HrReportParams',
                config: {
                    url: '/hrreportparams',
                    controller: 'HrReportController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrReport/ReportParams',
                    resolve: {
                        hrRptData: function (HrService) {
                            return HrService.GetAllOthers('/api/common/getReportDataListByUser/' + 1); //// Here 1 is report group ID
                        }
                    }
                }
            },
            //////////End For Hr Report

            //////////Start For Bonus Process
            {
                state: 'BonusProcess',
                config: {
                    url: '/bonusprocess',
                    controller: 'HrBonusProcessController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrBonusProcess/BonusProcess',
                    title: 'Bonus Process'
                }
            },
            //////////End For Bonus Process

            //////////Start For Salary Transaction
            {
                state: 'SalaryTran',
                config: {
                    url: '/salarytran',
                    controller: 'HrSalaryTranController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrSalaryTran/SalaryTran'
                }
            },
            //////////End For Salary Transaction

            //////////Start For Salary Process
            {
                state: 'SalaryProcess',
                config: {
                    url: '/salaryprocess',
                    controller: 'HrSalaryProcessController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrSalaryProcess/SalaryProcess',
                    title: 'Salary Process'
                }
            },
            {
                state: 'PartSalaryProcess',
                config: {
                    url: '/partsalaryprocess',
                    controller: 'HrSalaryProcessController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrSalaryProcess/PartSalaryProcess',
                    title: 'Partial Salary Process'
                }
            },
            //////////End For Salary Process

            //////////Start For Designation Pay Setup
            {
                state: 'DesigPaySetup',
                config: {
                    url: '/desigpaysetupentry',
                    controller: 'HrDesigPayController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrDesigPay/DesigPaySetup'
                }
            },
            //////////End For Designation Pay Setup

            //////////Start For Other Bill Process
            {
                state: 'OtherBillProcess',
                config: {
                    url: '/otherbill',
                    controller: 'HrPayBillController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrPayBill/OtherBillProcess',
                    resolve: {
                        billType: function () {
                            return { BILL_TYPE: 'OTHER_BILL' };
                        }
                    }
                }
            },
            //////////End For Other Bill Process

            //////////Start For EL Encashment Process
            {
                state: 'ElProcess',
                config: {
                    url: '/elprocess',
                    controller: 'HrPayBillController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrPayBill/_ElProcess',
                    resolve: {
                        billType: function () {
                            return { BILL_TYPE: 'EL_BILL' };
                        }
                    }
                }
            },
            //////////End For Other Bill Process

            /////////// Start for OT Entitled
            {
                state: 'MappingOtTeam',
                config: {
                    url: '/mappingotteam',
                    controller: 'HrOtEntitledController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrOtEntitled/MappingOtTeam'
                }
            },
            /////////// End for OT Entitled

            /////////// Start for OT Approve
            {
                state: 'OtApprove',
                config: {
                    url: '/otapprove',
                    controller: 'HrOtApproveController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrOtApprove/OtApprove'
                }
            },
            {
                state: 'OtApproveList',
                config: {
                    url: '/otapprovelist',
                    controller: 'HrOtApproveController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrOtApprove/OtApproveList'
                }
            },
            /////////// End for OT Approve


            /////////// Start for Schedule Assign
            {
                state: 'ScheduleAssignByTeam',
                config: {
                    url: '/assignbyteam',
                    controller: 'HrScheduleAssignController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrScheduleAssign/ScheduleAssignByTeam'
                }
            },
            {
                state: 'ScheduleAssignByPerson',
                config: {
                    url: '/assignbyperson',
                    controller: 'HrScheduleAssignController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrScheduleAssign/ScheduleAssignByPerson'
                }
            },            
            /////////// ENd for Schedule Assign

            /////////// Start for Disciplinary Action
            {
                state: 'DisciplinList',
                config: {
                    url: '/disciplinsearch',
                    controller: 'HrDisciplinSearchController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrDisciplinAction/DisciplinSearch'
                }
            },
            {
                state: 'DisciplinList.data',
                config: {
                    url: '/disciplinlistdata',
                    params: {
                        dataSource: null
                    },
                    controller: 'HrDisciplinListController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrDisciplinAction/DisciplinList'
                }
            },
            {
                state: 'DisciplinAction',
                config: {
                    url: '/disciplinaction',
                    params: {
                        pDisciplinActionObj: null
                    },
                    controller: 'HrDisciplinActionController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrDisciplinAction/DisciplinAction'
                }
            },

            /////////// End for Disciplinary Action

            /////////// Start for Yearly Calendar YearlyCalendarParamView
            {
                state: 'YearlyCalendarView',
                config: {
                    url: '/calender',
                    controller: 'HrYearlyCalendarController',
                    controllerAs: 'vm',
                    params: {
                        pHR_COMPANY_ID: null,
                        pRF_FISCAL_YEAR_ID: null
                    },
                    templateUrl: '/Hr/HrYearlyCalander/YearlyCalendarView'
                }
            },
            {
                state: 'EditCalenderDate',
                config: {
                    url: '/EditCalenderDate',
                    controller: 'HrYearlyCalendarEditController',
                    controllerAs: 'vm',
                    params: {
                        pHR_COMPANY_ID: null,
                        pRF_FISCAL_YEAR_ID: null
                    },
                    templateUrl: '/Hr/HrYearlyCalander/EditCalenderDate'
                }
            },
            /////////// End for Yearly Calendar

            /////////// Start for hr_ta_raw_data
            {
                state: 'ImportRawdata',
                config: {
                    url: '/imprawdata',
                    controller: 'HrTaRawDataController',
                    controllerAs: 'vm',
                    //params: {
                    //    pEmployeeObj: null 
                    //},
                    templateUrl: '/Hr/HrTaRawData/ImportRawdata'
                }
            },
            {
                state: 'ImportRawdata.List',
                config: {
                    url: '/list',
                    views: {
                        'List@ImportRawdata': {
                            controller: 'HrTaRawDataListController',
                            controllerAs: 'vm',
                            templateUrl: "/Hr/HrTaRawData/ImportRawdataList"
                        }
                    }
                }
            },
            {
                state: 'IDCardPrint',
                config: {
                    url: '/idcardprint',
                    controller: 'HrTaRawDataController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrTaRawData/IDCardPrint',
                    title: '404'
                }
            },
            /////////// End for hr_ta_raw_data

            /////////// Start for hr_employee
            {
                state: 'EmployeePay',
                config: {
                    url: '/empgrossupd',
                    controller: 'HrEmployeePayController',
                    controllerAs: 'vm',
                    params: {
                        pEmployeeObj: null
                    },
                    templateUrl: '/Hr/HrEmployee/_EmployeePay'
                }
            },
            {
                state: 'EmployeeEntryPay',
                config: {
                    url: '/employeepayIQ',
                    views: {
                        "EmployeeEntryPay": {
                            controller: 'HrEmployeeController',
                            controllerAs: 'vm',
                            templateUrl: '/Hr/HrEmployee/_EmployeeEntryPay'
                        }
                    },                    
                    params: {
                        pEmployeeObj: null
                    }
                }
            },
            {
                state: 'EmployeeEntryPayList',
                config: {
                    url: '/empsearchPay',
                    views: {
                        "EmployeeEntryPayList": {
                            controller: 'HrEmployeeSearchController',
                            controllerAs: 'vm',
                            templateUrl: '/Hr/HrEmployee/EmployeeSearch'
                        }
                    }
                }
            },
            {
                state: 'EmployeeEntryPayList.data',
                config: {
                    url: '/emppaylist',
                    params: {
                        dataSource: null,
                    },
                    controller: 'HrEmployeeListController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrEmployee/EmployeeList'
                }
            },
            {
                state: 'EmployeeEntry',
                config: {
                    url: '/employee',
                    controller: 'HrEmployeeController',
                    controllerAs: 'vm',
                    params: {
                        pEmployeeObj: null
                    },
                    templateUrl: '/Hr/HrEmployee/EmployeeEntry'
                }
            },
            {
                state: 'EmployeeList',
                config: {
                    url: '/empsearch',
                    controller: 'HrEmployeeSearchController',
                    controllerAs: 'vm',
                    params: {
                        pState: null
                    },
                    templateUrl: '/Hr/HrEmployee/EmployeeSearch'
                }
            },
            {
                state: 'EmployeeList.data',
                config: {
                    url: '/listdata',
                    params: {
                        dataSource: null,
                    },
                    controller: 'HrEmployeeListController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrEmployee/EmployeeList'
                }
            },
            {
                state: 'ExchangeProxy',
                config: {
                    url: '/exchangeproxy',
                    controller: 'HrEmployeeController',
                    controllerAs: 'vm',
                    params: {
                        pEmployeeObj: null
                    },
                    templateUrl: '/Hr/HrEmployee/ExchangeProxy'
                }
            },
            {
                state: 'EmployeeRejoin',
                config: {
                    url: '/employeerejoin',
                    controller: 'HrEmployeeRejoinController',
                    controllerAs: 'vm',
                    params: {
                        pEmployeeObj: null
                    },
                    templateUrl: '/Hr/HrEmployee/EmployeeRejoin'
                }
            },
            /////////// End for hr_employee


            /////////// Start for office
            {
                state: 'OfficeEntry',
                config: {
                    url: '/entry',
                    templateUrl: '/hr/HrOffice/OfficeEntry'
                }
            },
            {
                state: 'OfficeList',
                config: {
                    url: '/list',
                    templateUrl: '/hr/HrOffice/OfficeList'
                }
            },
            /////////// End for office

            /////////// Start for schedule
            {
                state: 'ScheduleEntry',
                config: {
                    params: {
                        pNewSchedule: true
                    },
                    url: '/schedule/entry',
                    templateUrl: '/hr/HrSchedule/ScheduleEntry'
                }
            },
            {
                state: 'ScheduleEntry.ScheduleClock',
                config: {
                    //url: '/{HrScheduleId}',
                    params: {
                        HrScheduleId: null,
                        HrScheduleD_Id: null
                    },
                    views: {
                        'ScheduleClock@ScheduleEntry': {
                            controller: 'HrScheduleClock',
                            controllerAs: 'vm',
                            templateUrl: "/hr/HrSchedule/ScheduleClock"
                        }
                    }

                }
            },
            ///////////// End for schedule



            /////////////Start for Leave Transaction
            {
                state: 'LeaveTransEntry',
                config: {

                    params: {
                        LeaveTrans: {}
                    },
                    url: '/LeaveTransEntry',
                    controller: 'LeaveTransController',
                    controllerAs: 'vm',
                    templateUrl: '/hr/hrleavetrans/LeaveTransEntry'
                }
            },
            {
                state: 'eleave',
                config: {
                    url: '/eLeave?i&l&a&m',
                    params: {
                        LeaveTrans: {}
                    },
                    controller: 'eLeaveController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrLeaveTrans/eLeave'
                }
            },
            {
                state: 'LeaveTransList',
                config: {
                    url: '/LeaveTransList',
                    controller: 'LeaveTransListController',
                    controllerAs: 'vm',
                    templateUrl: '/hr/hrleavetrans/LeaveTransList'
                }
            },

            {
                state: 'LeaveTransEdit',
                config: {
                    url: '/LeaveTransEdit',
                    controller: 'LeaveTransEditController',
                    controllerAs: 'vm',
                    templateUrl: '/hr/hrleavetrans/LeaveTransEdit'
                }
            },
            {
                state: 'LeaveTransEdit.data',
                config: {
                    url: '/data',
                    params: {
                        dataSource: {}
                    },
                    controller: 'LeaveTransEditDataController',
                    controllerAs: 'vm',
                    templateUrl: '/hr/hrleavetrans/LeaveTransEditData'
                }
            },
            {
                state: 'LeaveTransList.data',
                config: {
                    params: {
                        datas: {}
                    },
                    url: '/data',
                    views: {
                        'data@LeaveTransList': {
                            controller: 'LeaveTransDataController',
                            controllerAs: 'vm',
                            templateUrl: '/hr/hrleavetrans/LeaveTransListData'
                        }
                    }


                }
            },

            {
                state: 'LeaveReset',
                config: {
                    url: '/LeaveReset',
                    controller: 'LeaveResetController',
                    controllerAs: 'vm',
                    templateUrl: '/hr/hrleavetrans/LeaveReset'
                }
            },

            {
                state: 'LeaveReset.data',
                config: {
                    params: {
                        dataSource: {}
                    },
                    url: '/data',
                    controller: 'LeaveResetDataController',
                    controllerAs: 'vm',
                    templateUrl: '/hr/hrleavetrans/LeaveResetData'
                }
            },


            /////////////End For Leave Transaction


            ///////////Start For Holiday Roaster


        {
            state: 'OffDayRoaster',
            config: {
                url: '/OffDayRoaster',
                controller: 'OffDayRoasterController',
                controllerAs: 'vm',
                templateUrl: '/hr/OffDayRoaster/OffDayRoaster'
            }
        },

        {
            state: 'OffDayRoaster.data',
            config: {
                url: '/data',
                params: {
                    dataSource: null,
                    HR_COMPANY_ID: null,
                    formData: {}
                },
                controller: 'OffDayRoasterDataController',
                controllerAs: 'vm',
                templateUrl: '/hr/OffDayRoaster/OffDayRoasterData'
            }
        },

            //////////End For Holiday Roaster




          //////////Start For HrTaProcessDataCorr
                  {
                      state: 'HrTaProcessDataCorr',
                      config: {
                          url: '/params',
                          controller: 'HrTaProcessDataCorrController',
                          controllerAs: 'vm',
                          templateUrl: '/Hr/HrTaProcessDataCorr/HrTaProcessDataCorr'
                      }
                  },

                {
                    state: 'HrTaProcessDataCorr.Dtl',
                    config: {
                        url: '/dtl',
                        params: {
                            data: null,
                            formData: null
                        },
                        controller: 'HrTaProcessDataCorrDtlController',
                        controllerAs: 'vm',
                        templateUrl: '/Hr/HrTaProcessDataCorr/HrTaProcessDataCorrDtl'
                    }
                },
          //////////End For HrTaProcessDataCorr



          ///////End For Salary Advance

            {
                state: 'SalaryAdvReq',
                config: {
                    url: '/Req?i&l&a&m',
                    controller: 'AdvSalaryController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrSalAdvance/SalaryAdvReq'
                }
            },

            {
                state: 'ManualSalaryAdvReq',
                config: {
                    url: '/SalaryAdvReq?i',
                    controller: 'ManulSalaryAdvReqController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrSalAdvance/ManualSalaryAdvReq'
                }
            },
            {
                state: 'SalaryAdvList',
                config: {
                    url: '/SalaryAdvList',
                    controller: 'SalaryAdvListController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/HrSalAdvance/SalaruAdvList'
                }
            },
            {
                state: 'IncrimentMemo',
                config: {
                    url: '/incrmemo',
                    views: {
                        'IncrimentMemo': {
                            controller: 'HrIncrMemoController',
                            controllerAs: 'vm',
                            templateUrl: "/Hr/Hr/_IncrMemoCreation"                            
                        }
                    },
                    Title: 'Incriment Memo'
                }
            },
            {
                state: 'IncrimentProposalH',
                config: {
                    url: '/incrProposalH/:pHR_YR_INCR_H_ID/:pHR_INCR_MEMO_ID/:pEMPLOYEE_TYPE_ID/:pHR_DEPARTMENT_ID/:pLK_FLOOR_ID/:pPROPOSE_BY/:pIS_FROM_BATCH_LIST',
                    views: {
                        'IncrimentProposalH': {
                            controller: 'HrIncrProposalHController',
                            controllerAs: 'vm',
                            templateUrl: "/Hr/Hr/_IncrProposalH"                          
                        }
                    },                    
                    Title: 'Yearly Incriment Proposal',
                    resolve: {
                        incrHdrData: function (HrService, $stateParams) {
                            if (angular.isDefined($stateParams.pHR_YR_INCR_H_ID) && $stateParams.pHR_YR_INCR_H_ID > 0) {

                                return HrService.getDataByFullUrl('/api/hr/HrIncriment/GetIncrHdr?pHR_YR_INCR_H_ID=' + $stateParams.pHR_YR_INCR_H_ID + '&pHR_INCR_MEMO_ID=' + $stateParams.pHR_INCR_MEMO_ID
                                            + '&pHR_DEPARTMENT_ID=' + $stateParams.pHR_DEPARTMENT_ID + '&pLK_FLOOR_ID=' + $stateParams.pLK_FLOOR_ID + '&pPROPOSE_BY=' + $stateParams.pPROPOSE_BY + '&pIS_FROM_BATCH_LIST=' + $stateParams.pIS_FROM_BATCH_LIST).then(function (res) {
                                                return res;
                                            });

                            }
                            else {
                                return {};
                            }
                        }
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'IncrimentProposalH.Dtl',
                config: {
                    url: '/dtl',
                    views: {
                        "IncrimentProposalH@Dtl": {
                            controller: 'HrIncrProposalDController',
                            controllerAs: 'vm',
                            templateUrl: '/Hr/Hr/_IncrProposalD'                           
                        }
                    },                    
                    Title: 'Yearly Incriment Proposal',                    
                    reloadOnSearch: false
                }
            },
            {
                state: 'IncrProposalBatchList',
                config: {
                    url: '/incrPropBatchList/:pHR_INCR_MEMO_ID',
                    views: {
                        'IncrProposalBatchList': {
                            controller: 'HrIncrProposalBatchListController',
                            controllerAs: 'vm',
                            templateUrl: "/Hr/Hr/_IncrProposalBatchList"
                        }
                    },
                    Title: 'Yearly Incriment Proposal'
                }
            },
            {
                state: 'SpecialIncrH',
                config: {
                    url: '/specialIncrH/:pHR_YR_INCR_H_ID/:pHR_INCR_MEMO_ID/:pEMPLOYEE_TYPE_ID/:pHR_COMPANY_ID/:pPROPOSE_BY/:pIS_FROM_BATCH_LIST',
                    views: {
                        'SpecialIncrH': {
                            controller: 'HrSpecialIncrHController',
                            controllerAs: 'vm',
                            templateUrl: "/Hr/Hr/_SpecialIncrH"
                        }
                    },
                    Title: 'Special Incriment',
                    resolve: {
                        incrHdrData: function (HrService, $stateParams) {
                            
                            if (angular.isDefined($stateParams.pHR_YR_INCR_H_ID) ) {
                                //alert('x');
                                return HrService.getDataByFullUrl('/api/hr/HrSpecialIncr/GetSpecialIncrHdr?pHR_YR_INCR_H_ID=' + $stateParams.pHR_YR_INCR_H_ID + '&pHR_INCR_MEMO_ID=' + $stateParams.pHR_INCR_MEMO_ID).then(function (res) {
                                    return res;
                                });

                                //return HrService.getDataByFullUrl('/api/hr/GetSpecialIncrHdr/GetSpecialIncrHdr?pHR_YR_INCR_H_ID=' + $stateParams.pHR_YR_INCR_H_ID + '&pHR_INCR_MEMO_ID=' + $stateParams.pHR_INCR_MEMO_ID
                                //            + '&pPROPOSE_BY=' + $stateParams.pPROPOSE_BY + '&pIS_FROM_BATCH_LIST=' + $stateParams.pIS_FROM_BATCH_LIST).then(function (res) {
                                //                return res;
                                //            });

                            }
                            else {
                                return {};
                            }
                        }
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'SpecialIncrH.Dtl',
                config: {
                    url: '/dtl',
                    views: {
                        "SpecialIncrH@Dtl": {
                            controller: 'HrSpecialIncrDController',
                            controllerAs: 'vm',
                            templateUrl: '/Hr/Hr/_SpecialIncrD'
                        }
                    },
                    Title: 'Special Incriment',
                    reloadOnSearch: false
                }
            },
            {
                state: 'IncrementProcess',
                config: {
                    url: '/incrProcess',
                    views: {
                        'IncrementProcess': {
                            controller: 'HrIncrProcessController',
                            controllerAs: 'vm',
                            templateUrl: "/Hr/Hr/_IncrProcess"
                        }
                    },
                    Title: 'Yearly Incriment Process'
                }
            },
            {
                state: 'Country',
                config: {
                    url: '/Country',
                    controller: 'CountryController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/Hr/_Country',
                    Title: 'Country'
                }
            },
            {
                state: 'ProductionFloor',
                config: {
                    url: '/ProductionFloor',
                    controller: 'HrProductionFloorController',
                    controllerAs: 'vm',
                    templateUrl: '/Hr/Hr/_ProductionFloor',
                    Title: 'Production Floor'
                }
            },
          ///////End For Salary Advance


          {
              state: 'AssignByRandomPerson',
              config: {
                  url: '/assignByRandom',
                  views: {
                      "AssignByRandomPerson": {
                          controller: 'HrAssignByRandomPersonController',
                          controllerAs: 'vm',
                          templateUrl: '/Hr/Hr/_AssignScheduleByRandomPerson'                          
                      }
                  },
                  Title: 'Assign Schedule By Random Person',
                  reloadOnSearch: false
              }
          },
          {
              state: 'AnnualLeaveByRandomPerson',
              config: {
                  url: '/annualLeaveByRandom',
                  views: {
                      "AnnualLeaveByRandomPerson": {
                          controller: 'AnnualLeaveByRandomPersonController',
                          controllerAs: 'vm',
                          templateUrl: '/Hr/Hr/_AnnualLeaveByRandomPerson'
                      }
                  },
                  Title: 'Annual Leave Reset By Random Person',
                  reloadOnSearch: false
              }
          },
          {
              state: 'CompPayPeriod',
              config: {
                  url: '/compPayPeriod/:pACC_PAY_PERIOD_ID',
                  views: {
                      "CompPayPeriod": {
                          controller: 'CompPayPeriodController',
                          controllerAs: 'vm',
                          templateUrl: '/Hr/Hr/_CompPayPeriod'
                      }
                  },
                  Title: 'Company Pay Period',
                  resolve: {
                      formData: function (HrService, $stateParams) {
                          if (angular.isDefined($stateParams.pACC_PAY_PERIOD_ID) && $stateParams.pACC_PAY_PERIOD_ID > 0) {

                              return HrService.getDataByFullUrl('/api/hr/CompPayPeriod/GetCompPayPeriodByID?pACC_PAY_PERIOD_ID=' + ($stateParams.pACC_PAY_PERIOD_ID || 0)).then(function (res) {
                                  return res;
                              });                              
                          }
                          else {
                              return {};
                          }
                      }
                  },
                  reloadOnSearch: false
              }
          },
          {
              state: 'OtSummeryApprove',
              config: {
                  url: '/otSummeryApprove/:pHR_COMPANY_ID/:pACC_PAY_PERIOD_ID',
                  views: {
                      "OtSummeryApprove": {
                          controller: 'OtSummeryApproveController',
                          controllerAs: 'vm',
                          templateUrl: '/Hr/Hr/_OtSummeryApprove'
                      }
                  },
                  Title: 'OT Summery',                  
                  reloadOnSearch: false
              }
          },
          {
              state: 'MbnBillProcess',
              config: {
                  url: '/mbnProcess',
                  views: {
                      "MbnBillProcess": {
                          controller: 'MbnBillProcessController',
                          controllerAs: 'vm',
                          templateUrl: '/Hr/Hr/_MbnBillProcess'
                      }
                  },
                  Title: 'Maternity Benefit Bill Process',
                  reloadOnSearch: false
              }
          },
          {
              state: 'EmpTrnfr',
              config: {
                  url: '/empTranfr/:pHR_EMP_TRNFR_ID',
                  views: {
                      "EmpTrnfr": {
                          controller: 'EmpTrnfrController',
                          controllerAs: 'vm',
                          templateUrl: '/Hr/Hr/_EmpTrnfr'
                      }
                  },
                  Title: 'Employee Transfer',
                  resolve: {
                      formData: function (HrService, $stateParams) {
                          if (angular.isDefined($stateParams.pHR_EMP_TRNFR_ID) && $stateParams.pHR_EMP_TRNFR_ID > 0) {

                              return HrService.getDataByFullUrl('/api/hr/EmpTrans/GetTransDataByID?pHR_EMP_TRNFR_ID=' + ($stateParams.pHR_EMP_TRNFR_ID || 0)).then(function (res) {
                                  return res;
                              });
                          }
                          else {
                              return {};
                          }
                      }
                  },
                  reloadOnSearch: false
              }
          },
          {
              state: 'SmsBroadcast2Emp',
              config: {
                  url: '/smsCast2Emp',
                  views: {
                      "SmsBroadcast2Emp": {
                          controller: 'SmsBroadcast2EmpController',
                          controllerAs: 'vm',
                          templateUrl: '/Hr/Hr/_SmsBrodcast2Emp'
                      }
                  },
                  Title: 'SMS Broadcast to Employee',
                  reloadOnSearch: false
              }
          },
          {
              state: 'IncrHistory4Emp',
              config: {
                  url: '/incrHistory/:pHR_EMPLOYEE_ID',
                  views: {
                      "IncrHistory4Emp": {
                          controller: 'IncrHistory4EmpController',
                          controllerAs: 'vm',
                          templateUrl: '/Hr/Hr/_IncrHistory4Emp'
                      }
                  },
                  Title: 'Employee Incriment History',
                  resolve: {
                      formData: function (HrService, $stateParams) {
                          if (angular.isDefined($stateParams.pHR_EMPLOYEE_ID) && $stateParams.pHR_EMPLOYEE_ID > 0) {

                              return HrService.getDataByFullUrl('/api/hr/GetEmpDataByID?pHR_EMPLOYEE_ID=' + ($stateParams.pHR_EMPLOYEE_ID || 0)).then(function (res) {
                                  return res;
                              });
                          }
                          else {
                              return {};
                          }
                      }
                  },
                  reloadOnSearch: false
              }
          },



        ];
    }
})();
