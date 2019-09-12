using ERP.Model;
using ERPSolution.Common;
using ERPSolution.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Hosting;
using Postal;
using System.Net.Mail;
using System.Net.Mime;

namespace ERPSolution.Areas.Knitting.Controllers
{
    [SignInCheck]
    public class KnitController : BaseController
    {
        public string SendOTP4GreyFabTransMail(Int64 pKNT_ORD_TRN_REQ_H_ID, Int32 pLK_TRN_SRC_ID)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            KNT_ORD_TRN_REQ_HModel ob = new KNT_ORD_TRN_REQ_HModel();
            var rptData = ob.SendOTP4GreyFabTrns(pKNT_ORD_TRN_REQ_H_ID);            

            var email = new SendOTP4GreyFabTransMailService
            {
                To = rptData.MAIL_ADD_LST,
                LK_TRN_SRC_ID = pLK_TRN_SRC_ID,
                SUBJECT_TEXT = (pLK_TRN_SRC_ID == 725) ? "Grey fabric has been transfered (" + rptData.TRN_TYPE_NAME + ")" : "A Grey Fabric Transfer (" + rptData.TRN_TYPE_NAME + ") Requisition is Waiting for Authorization Approval",
                data = rptData //ob.GetSrtFabReqRpt(pKNT_SRT_FAB_REQ_H_ID)

            };            

            emailService.Send(email);
            return rptData.OTP_CODE;
        }

        public JsonResult FireMailSendOTP4GreyFabTran(Int64 pKNT_ORD_TRN_REQ_H_ID, Int32 pLK_TRN_SRC_ID)
        {
            var ob = SendOTP4GreyFabTransMail(pKNT_ORD_TRN_REQ_H_ID, pLK_TRN_SRC_ID);
            Session["multiOTP4GreyFabTrans"] = ob;

            return Json(new { success = true, OTP_CODE = ob }, JsonRequestBehavior.AllowGet);
        }
        
        public static void SendSrtFabAprovMail(Int64 pKNT_SRT_FAB_REQ_H_ID)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            KNT_SRT_FAB_REQ_RPTModel ob = new KNT_SRT_FAB_REQ_RPTModel();
            var rptData = ob.GetSrtFabReqRpt(pKNT_SRT_FAB_REQ_H_ID, "Y");
            
            var email = new SendSrtFabAprovMailService
            {
                To = rptData.fabList[0].MAIL_ADD_LST,

                data = rptData //ob.GetSrtFabReqRpt(pKNT_SRT_FAB_REQ_H_ID)

            };

            //string file = "D:\\a\\multipleKnitCardFormat.pdf";
            //// Create  the file attachment for this e-mail message.
            //Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
            //// Add time stamp information for the file.
            //ContentDisposition disposition = data.ContentDisposition;
            //disposition.CreationDate = System.IO.File.GetCreationTime(file);
            //disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            //disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
            //// Add the file attachment to this e-mail message.
            //email.Attachments.Add(data);

            emailService.Send(email);
        }

        public JsonResult FireMail(Int64 pKNT_SRT_FAB_REQ_H_ID)
        {
            SendSrtFabAprovMail(pKNT_SRT_FAB_REQ_H_ID);
            return Json(new { success=true}, JsonRequestBehavior.AllowGet);
        }
        
        //
        // GET: /Knitting/Knit/
        public ActionResult FabProdKnitOrder()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View("FabricProductionKnittingOrder");
        }

        public PartialViewResult _FabProdKnitOrder()
        {
            return PartialView("_FabricProductionKnittingOrder");
        }


        public ActionResult YarnPurReq()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult YarnPurchaseRequisition()
        {
            return PartialView();
        }

        public ActionResult GenBuffYarnRequisitionList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _GenBuffYarnRequisitionList()
        {
            return PartialView();
        }

        public ActionResult GenBuffYarnRequisition()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _GenBuffYarnRequisition()
        {
            return PartialView();
        }
        public ActionResult YarnReceive()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _YarnReceive()
        {
            return PartialView();
        }

        public ActionResult YarnReceiveView()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _YarnReceiveView()
        {
            return PartialView();
        }
        
        public ActionResult YarnReceiveList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _YarnReceiveList()
        {
            return PartialView();
        }

        public ActionResult KnitPlan()
        {
           return View();
        }

        public PartialViewResult _KnitPlan()
        {
            return PartialView();
        }


        public ActionResult YarnOpeningBalance()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _YarnOpeningBalance()
        {
            return PartialView();
        }

        public PartialViewResult _KnitPlanJobCard()
        {
            return PartialView();
        }
        
        public ActionResult JobCardDashboard()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _JobCardDashboard()
        {
            return PartialView();
        }

        public ActionResult KnitSubContractProgram()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _KnitSubcontProgH()
        {
            return PartialView();
        }
        public PartialViewResult _KnitSubcontProgYrnRcv()
        {
            return PartialView();
        }
        public PartialViewResult _KnitSubcontProgFabColorKP()
        {
            return PartialView();
        }
        public ActionResult KnitSubContractProgramList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _knittingSubcontProgList()
        {
            return PartialView();
        }

        public ActionResult ScProgramList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ScProgramList()
        {
            return PartialView();
        }

        public ActionResult YarnIssueList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public ActionResult KntMachnOpratorAssign()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _KntMachnOpratorAssign()
        {
            return PartialView();
        }


        public PartialViewResult _YarnIssueList()
        {
            return PartialView();
        }

        
        public ActionResult YarnIssue()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _YarnIssue()
        {
            return PartialView();
        }

        public ActionResult YarnIssueReqList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _YarnIssueReqList()
        {
            return PartialView();
        }

        public ActionResult YarnIssueReq()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _YarnIssueReq()
        {
            return PartialView();
        }

        public ActionResult YarnIssueRtnReqList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public ActionResult KnitPlanJobCardRollProd()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _YarnIssueRtnReqList()
        {
            return PartialView();
        }
        public PartialViewResult _KnitPlanJobCardRollProd()
        {
            return PartialView();
        }

        public ActionResult YarnIssueRtnReq()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _YarnIssueRtnReq()
        {
            return PartialView();
        }

        
        public ActionResult YarnIssueRtnList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _YarnIssueRtnList()
        {
            return PartialView();
        }

        public ActionResult YarnIssueRtn()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _YarnIssueRtn()
        {
            return PartialView();
        }


        public ActionResult KntReport()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _KntReportParams()
        {
            return PartialView();
        }

        public ActionResult KntYrnStkList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _KntYrnStkList()
        {
            return PartialView();
        }
        public ActionResult KntMCLoadingPlan()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _KntMCLoadingPlan()
        {
            return PartialView();
        }

        public PartialViewResult _KntMCLoadingPlanPending()
        {
            return PartialView();
        }


        public PartialViewResult _YarnReqInHouseProd()
        {
            return PartialView();
        }

        public ActionResult KnitPlanJobCardRollInsp()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _KnitPlanJobCardRollInsp()
        {
            return PartialView();
        }

        public ActionResult KnitPlanJobCardRollProdTab()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _KnitPlanJobCardRollProdTab()
        {
            return PartialView();
        }
        
        public ActionResult YarnTest()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _YarnTest()
        {
            return PartialView();
        }
        
        public ActionResult KntGreyFabRcv()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _KntGreyFabRcv()
        {
            return PartialView();
        }

        public ActionResult KntScGreyFabDelv()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _KntScGreyFabDelvH()
        {
            return PartialView();
        }
        public PartialViewResult _KntScGreyFabDelvD()
        {
            return PartialView();
        }
        public PartialViewResult _KntSciYrnRtnD()
        {
            return PartialView();
        } 
        public PartialViewResult _KntScGreyFabDelvList()
        {
            return PartialView();
        }

        public ActionResult CollarCuffOrderReq()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _CollarCuffOrderReqList()
        {
            return PartialView();
        }

        public ActionResult CollarCuffOrderProd()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _CollarCuffOrderProdH()
        {
            return PartialView();
        }
        public PartialViewResult _CollarCuffOrderProdD()
        {
            return PartialView();
        }


        public ActionResult YarnTestReqList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _YarnTestReqList()
        {
            return PartialView();
        }

        public ActionResult YarnTestReq()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _YarnTestReq()
        {
            return PartialView();
        }


        public ActionResult YarnTestIssueList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _YarnTestIssueList()
        {
            return PartialView();
        }

        public ActionResult YarnTestIssue()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _YarnTestIssue()
        {
            return PartialView();
        }

        

        public ActionResult KntMachineProfile()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _KntMachineProfile()
        {
            return PartialView();
        }

        public ActionResult YarnApprovalWithoutTest()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _YarnApprovalWithoutTest()
        {
            return PartialView();
        }

        public ActionResult YarnTestIssueRtnReqList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _YarnTestIssueRtnReqList()
        {
            return PartialView();
        }

        public ActionResult YarnTestIssueRtnReq()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _YarnTestIssueRtnReq()
        {
            return PartialView();
        }
        public ActionResult YarnTestIssueRtn()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _YarnTestIssueRtn()
        {
            return PartialView();
        }
        public PartialViewResult _KnitPlanModal()
        {
            return PartialView();
        }
        public ActionResult KnitCardRpt()
        {
           return View();
        }
        public PartialViewResult _KnitCardRpt()
        {
            return PartialView();
        }
        
        public PartialViewResult _KnitPlanJobCardSR()
        {
            return PartialView();
        }
        public PartialViewResult _KnitPlanJobCardCollarCuff()
        {
            return PartialView();
        }

        public PartialViewResult _KnitPlanJobCardCollarCuffSco()
        {
            return PartialView();
        }

        public ActionResult KnitDailyProduction()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _KnitDailyProductionList()
        {
            return PartialView();
        }

        public ActionResult CollarCuffStrRcv()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _CollarCuffStrRcvH()
        {
            return PartialView();
        }
        public PartialViewResult _CollarCuffStrRcvList()
        {
            return PartialView();
        }
        

        public ActionResult KnitFabricStoreStk()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _KnitFabricStoreStkList()
        {
            return PartialView();
        }

        public ActionResult YarnRtnToSupReqList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _YarnRtnToSupReqList()
        {
            return PartialView();
        }

        public ActionResult YarnRtnToSupReq()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _YarnRtnToSupReq()
        {
            return PartialView();
        }

        public ActionResult SmplFabProdOrderMnul()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _SmplFabProdOrderMnul()
        {
            return PartialView();
        }




        public ActionResult ScGreyFabRcvFormParty()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _ScGreyFabRcvFromPartyH()
        {
            return PartialView();
        }
        public PartialViewResult _ScGreyFabRcvFromPartyFabRcv()
        {
            return PartialView();
        }
        public PartialViewResult _ScGreyFabRcvFromPartyYrnRcv()
        {
            return PartialView();
        }
        public PartialViewResult _ScGreyFabRcvFromPartyClcfRcv()
        {
            return PartialView();
        }
        public PartialViewResult _ScGreyFabRcvFromPartyList()
        {
            return PartialView();
        }


        public ActionResult ShortFabProdOrder()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ShortFabProdOrderH()
        {
            return PartialView();
        }
        public PartialViewResult _ShortFabProdOrderD()
        {
            return PartialView();
        }
        public PartialViewResult _ShortFabProdOrderReason()
        {
            return PartialView();
        }
        public PartialViewResult _ShortFabProdOrderResposibility()
        {
            return PartialView();
        }
        public PartialViewResult _ShortFabProdOrderList()
        {
            return PartialView();
        }

        public ActionResult ShortFabProdOrderRpt()
        {
            return View();
        }
        public PartialViewResult _ShortFabProdOrderRpt()
        {
            return PartialView();
        }
        public PartialViewResult _SrtFabProdOrdAprovList()
        {
            return PartialView();
        }
        


        public ActionResult DailyTrimsReceiveList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DailyTrimsReceiveList()
        {
            return PartialView();
        }

        public ActionResult DailyTrimsReceive()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DailyTrimsReceive()
        {
            return PartialView();
        }


        public ActionResult DailyTrimsReceiveView()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View("DailyTrimsReceive");
        }

        public PartialViewResult _DailyTrimsReceiveView()
        {
            return PartialView();
        }

        public ActionResult ScYrnRcvChallan()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _ScYrnRcvChallanH()
        {
            return PartialView();
        }
        public PartialViewResult _ScYrnRcvChallanD()
        {
            return PartialView();
        }

        public PartialViewResult orderTrimsItemDtl()
        {
            return PartialView();
        }

        public ActionResult ScoGreyFabRtnChlnToParty()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ScoGreyFabRtnChlnToPartyH()
        {
            return PartialView();
        }

        public PartialViewResult _ScoGreyFabRtnChlnToPartyFabRtn()
        {
            return PartialView();
        }
        //public PartialViewResult _ScoGreyFabRtnChlnToPartyYrnRtn()
        //{
        //    return PartialView();
        //}

        public PartialViewResult _ScoGreyFabRtnChlnToPartyList()
        {
            return PartialView();
        }
        public PartialViewResult _StyleDItemFabricationKnt()
        {
            return PartialView();
        }

        public ActionResult KnitYdProgram()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _KnitYdProgram()
        {
            return PartialView();
        }

        public ActionResult ScoProgTrans()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _ScoProgTransH()
        {
            return PartialView();
        }

        public PartialViewResult _ScoProgTransYrnTrans()
        {
            return PartialView();
        }
        public PartialViewResult _ScoProgTransList()
        {
            return PartialView();
        }

        public PartialViewResult _KnitYdProgramList()
        {
            return PartialView();
        }

        public ActionResult SciGreyFabRtnChlnFromParty()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _SciGreyFabRtnChlnFromPartyH()
        {
            return PartialView();
        }

        public PartialViewResult _SciGreyFabRtnChlnFromPartyFabRtn()
        {
            return PartialView();
        }
       
        public PartialViewResult _SciGreyFabRtnChlnFromPartyList()
        {
            return PartialView();
        }

        public ActionResult ScoCollarCuffProg()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _ScoCollarCuffProgList()
        {
            return PartialView();
        }
        public PartialViewResult _ScoCollarCuffProgH()
        {
            return PartialView();
        }
        public PartialViewResult _ScoCollarCuffProgD()
        {
            return PartialView();
        }

        public ActionResult KnitYdYrnRecv()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _KnitYdYrnRecv()
        {
            return PartialView();
        }

        public PartialViewResult _KnitYdYrnRecvList()
        {
            return PartialView();
        }

        public ActionResult KnitPlanYd()
        {
            return View();
        }

        public PartialViewResult _KnitPlanYd()
        {
            return PartialView();
        }
        public PartialViewResult _KnitPlanJobCardYd()
        {
            return PartialView();
        }
        public PartialViewResult _KnitPlanYdModal()
        {
            return PartialView();
        }

        public PartialViewResult _LblPrinterConfig()
        {
            return PartialView();
        }

        

        public ActionResult McOilReq4SubStr()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _McOilReq4SubStrH()
        {
            return PartialView();
        }
        public PartialViewResult _McOilReq4SubStrList()
        {
            return PartialView();
        }
        public PartialViewResult _McOilReqIss4SubStrH()
        {
            return PartialView();
        }


        public ActionResult DailyMachineOilConsumptionList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _DailyMachineOilConsumptionList()
        {
            return PartialView();
        }

        public ActionResult DailyMachineOilConsumption()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _DailyMachineOilConsumption()
        {
            return PartialView();
        }


        public ActionResult McNeedleBroken()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _McNeedleBrokenH()
        {
            return PartialView();
        }
        public PartialViewResult _McNeedleBrokenD()
        {
            return PartialView();
        }
        public PartialViewResult _McNeedleBrokenList()
        {
            return PartialView();
        }


        public ActionResult McnNeedleOpnBal()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _McnNeedleOpnBal()
        {
            return PartialView();
        }


        public ActionResult McnNeedleAssign()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _McnNeedleAssign()
        {
            return PartialView();
        }


        public ActionResult KnitPlanYarnTestReqList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _KnitPlanYarnTestReqList()
        {
            return PartialView();
        }

        public ActionResult KnitPlanYarnTestReq()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _KnitPlanYarnTestReq()
        {
            return PartialView();
        }



        
        

        public ActionResult YarnTestResultList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _YarnTestResultList()
        {
            return PartialView();
        }
        
        
        public ActionResult KntScProgYd()
        {
            return View();
        }

        public PartialViewResult _KnitScProgYdRpt()
        {
            return PartialView();
        }

        public PartialViewResult _KnitYarnTestRpt()
        {
            return PartialView();
        }
        
        public PartialViewResult _KnitYdYrnRecvTrns()
        {
            return PartialView();
        }
        public ActionResult YarnTestResultPrint()
        {
            return View();
        }

        public PartialViewResult _YarnTestResultPrint()
        {
            return PartialView();
        }
                
        public PartialViewResult _KnitPlanYdTrnsModal()
        {
            return PartialView();
        }
        public PartialViewResult _KnitPlanJobCardYdTrns()
        {
            return PartialView();
        }

        public ActionResult KnitScPartyRate()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _KnitScPartyRate()
        {
            return PartialView();
        }


        [System.Web.Mvc.HttpPost]
        //[MyValidateAntiForgeryToken]
        public JsonResult uploadImage(SCM_SC_QUOT_RATE_HModel ob)
        {
            string extension = null;
            string path = "";
            if (ob.ATT_FILE != null && ob.ATT_FILE.ContentLength > 0)
            {
                extension = Path.GetExtension(ob.ATT_FILE.FileName);
                path = Path.Combine(Server.MapPath("~/UPLOAD_DOCS/SC_PARTY_DOC"), ob.DOC_PATH_REF);
                ob.ATT_FILE.SaveAs(path);
            }
            return Json(new { path }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult KnitYarnTestRegister()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _KnitYarnTestRegister()
        {
            return PartialView();
        }

        public PartialViewResult _YdStatement()
        {
            return PartialView();
        }

        public ActionResult KnitYdStatementRpt()
        {
            return View();
        }

        public PartialViewResult _KnitYdStatementRpt()
        {
            return PartialView();
        }

        public ActionResult McnNeedleReqList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _McnNeedleReqList()
        {
            return PartialView();
        }

        public ActionResult McnNeedleReq()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _McnNeedleReq()
        {
            return PartialView();
        }

        public ActionResult McnNeedleIssueList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _McnNeedleIssueList()
        {
            return PartialView();
        }

        public ActionResult McnNeedleIssue()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _McnNeedleIssue()
        {
            return PartialView();
        }

        public ActionResult AdvPurReqList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _AdvPurReqList()
        {
            return PartialView();
        }

        public ActionResult AdvPurReq()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _AdvPurReq()
        {
            return PartialView();
        }

        public ActionResult PurchasePlanList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _PurchasePlanList()
        {
            return PartialView();
        }

        public ActionResult ComparativeStatement()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ComparativeStatement()
        {
            return PartialView();
        }

        public ActionResult KnitCardTestRpt()
        {
            return View();
        }
        public PartialViewResult _KnitCardTestRpt()
        {
            return PartialView();
        }
        public ActionResult KnitMCOilOB()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _KnitMCOilOB()
        {
            return PartialView();
        }

        public ActionResult KnitMcNeedleRcvList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _KnitMcNeedleRcvList()
        {
            return PartialView();
        }

        public ActionResult KnitMcNeedleRcv()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _KnitMcNeedleRcv()
        {
            return PartialView();
        }
        
        public ActionResult KnitMcNeedleRcvView()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _KnitMcNeedleRcvView()
        {
            return PartialView();
        }

        
        public ActionResult KnitMcOilRcvList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _KnitMcOilRcvList()
        {
            return PartialView();
        }

        public ActionResult KnitMcOilRcv()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _KnitMcOilRcv()
        {
            return PartialView();
        }
        
        public ActionResult KnitMcOilRcvView()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _KnitMcOilRcvView()
        {
            return PartialView();
        }

        public ActionResult KnitUserRequisitionMap()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _KnitUserRequisitionMap()
        {
            return PartialView();
        }

        public ActionResult ScoBillProc()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _ScoBillProcH()
        {
            return PartialView();
        }
        public PartialViewResult _ScoBillProcD()
        {
            return PartialView();
        }
        public PartialViewResult _ScoBillProcList()
        {
            return PartialView();
        }

        public ActionResult SciBillProc()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _SciBillProcH()
        {
            return PartialView();
        }
        public PartialViewResult _SciBillProcD()
        {
            return PartialView();
        }
        public PartialViewResult _SciBillProcList()
        {
            return PartialView();
        }

        public ActionResult SciYarnWipTran()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _SciYarnWipTranH()
        {
            return PartialView();
        }
        public PartialViewResult _SciYarnWipTranD()
        {
            return PartialView();
        }
        public PartialViewResult _SciYarnWipTranList()
        {
            return PartialView();
        }

        public ActionResult KnitYdProcLossAdj()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _KnitYdProcLossAdjH()
        {
            return PartialView();
        }
        public PartialViewResult _KnitYdProcLossAdjD()
        {
            return PartialView();
        }
        public PartialViewResult _KnitYdProcLossAdjList()
        {
            return PartialView();
        }


        public ActionResult OrdWiseGreyTrnReq()
        {
            return View();
        }
        public PartialViewResult _OrdWiseGreyTrnReqH()
        {
            return PartialView();
        }
        public PartialViewResult _OrdWiseGreyTrnReqD()
        {
            return PartialView();
        }
        public PartialViewResult _OrdWiseGreyTrnReqList()
        {
            return PartialView();
        }


        public ActionResult YdRecvChaln()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _YdRecvChalnH()
        {
            return PartialView();
        }
        public PartialViewResult _YdRecvChalnD()
        {
            return PartialView();
        }
        public PartialViewResult _YdRecvChalnList()
        {
            return PartialView();
        }


        public ActionResult ScoYdTrChaln()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _ScoYdTrChalnH()
        {
            return PartialView();
        }
        public PartialViewResult _ScoYdTrChalnList()
        {
            return PartialView();
        }
      



        public ActionResult FabBk4Labdip()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _FabBk4LabdipH()
        {
            return PartialView();
        }
        public PartialViewResult _FabBk4LabdipD()
        {
            return PartialView();
        }
        public PartialViewResult _FabBk4LabdipList()
        {
            return PartialView();
        }

        public PartialViewResult _KnitYdProgramYarnReqModal()
        {
            return PartialView();
        }
        public PartialViewResult _KnitCardUnAssignQtyModal()
        {
            return PartialView();
        }


        public ActionResult PartyWiseYarnIssueChallanList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _PartyWiseYarnIssueChallanList()
        {
            return PartialView();
        }


        public ActionResult PartyWiseYarnIssueChallan()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _PartyWiseYarnIssueChallan()
        {
            return PartialView();
        }

        public ActionResult YarnRtnToSupIssueList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _YarnRtnToSupIssueList()
        {
            return PartialView();
        }

        public ActionResult YarnRtnToSupIssue()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _YarnRtnToSupIssue()
        {
            return PartialView();
        }



        public static void SendYrnRtnMail(Int64 pKNT_YRN_STR_RPL_REQ_H_ID)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            KNT_YRN_STR_RPL_REQ_DModel ob = new KNT_YRN_STR_RPL_REQ_DModel();
            var rptData = ob.SelectByID(pKNT_YRN_STR_RPL_REQ_H_ID);
            var model= new KNT_YRN_STR_RPL_REQ_HModel().Select(pKNT_YRN_STR_RPL_REQ_H_ID);
            if (model.EMAIL_TO_LST.Length > 5)
            {
                var email = new SendYarnReplacementMailService
                {
                    obj = model,
                    data = rptData
                };
                emailService.Send(email);
            }
            
        }

        public JsonResult FireYarnRtnMail(Int64 pKNT_YRN_STR_RPL_REQ_H_ID)
        {
            SendYrnRtnMail(pKNT_YRN_STR_RPL_REQ_H_ID);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        
	}
}