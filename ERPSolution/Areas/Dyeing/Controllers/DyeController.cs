using ERP.Model;
using ERPSolution.Common;
using ERPSolution.Controllers;
using ERPSolution.Hubs;
using Postal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Hosting;
//using System.Web.Http;
using System.Web.Mvc;

namespace ERPSolution.Areas.Dyeing.Controllers
{
    [SignInCheck]
    [NoCache]
    public class DyeController : BaseController
    {
        //
        // GET: /Dyeing/Dye/
               

        public ActionResult LabdipRecipe()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }
        public PartialViewResult _LabdipRecipe()
        {
            return PartialView();
        }


        public PartialViewResult _LabdipRecipeModal()
        {
            return PartialView();
        }

        public ActionResult LabdipRecipeList()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }
        public PartialViewResult _LabdipRecipeList()
        {
            return PartialView();
        }

        public ActionResult Requisition()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }
        public PartialViewResult _Requisition()
        {
            return PartialView();
        }

        public ActionResult RequisitionList()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }
        public PartialViewResult _RequisitionList()
        {
            return PartialView();
        }

        public ActionResult DyeChemicalReceiveList()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }
        public PartialViewResult _DyeChemicalReceiveList()
        {
            return PartialView();
        }

        public ActionResult DyeChemicalReceive()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }
        public PartialViewResult _DyeChemicalReceive()
        {
            return PartialView();
        }

        public ActionResult DyeChemicalStoreTransferList()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }
        public PartialViewResult _DyeChemicalStoreTransferList()
        {
            return PartialView();
        }

        public ActionResult DyeChemicalStoreTransfer()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }
        public PartialViewResult _DyeChemicalStoreTransfer()
        {
            return PartialView();
        }
        public ActionResult DyeChemicalStoreIssue()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }
        public PartialViewResult _DyeChemicalStoreIssue()
        {
            return PartialView();
        }
        public ActionResult DyeChemicalOpeningBalance()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }
        public PartialViewResult _DyeChemicalOpeningBalance()
        {
            return PartialView();
        }

        public ActionResult DCIssueRequisitionList()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _DCIssueRequisitionList()
        {
            return PartialView();
        }

        public ActionResult DCIssueRequisition()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _DCIssueRequisition()
        {
            return PartialView();
        }

        public ActionResult MachineWashRequisitionList()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _MachineWashRequisitionList()
        {
            return PartialView();
        }

        public ActionResult MachineWashRequisition()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _MachineWashRequisition()
        {
            return PartialView();
        }



        public ActionResult DCIssueList()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _DCIssueList()
        {
            return PartialView();
        }

        public ActionResult DCIssue()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _DCIssue()
        {
            return PartialView();
        }

        public ActionResult DCLoanReturnRequisitionList()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _DCLoanReturnRequisitionList()
        {
            return PartialView();
        }

        public ActionResult DCLoanReturnRequisition()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _DCLoanReturnRequisition()
        {
            return PartialView();
        }

        public ActionResult DCLoanReturn()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _DCLoanReturn()
        {
            return PartialView();
        }
        public ActionResult DyeBatchPlan()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _DyeBatchPlan()
        {
            return PartialView();
        }

        public ActionResult DCBatchProgramRequisitionList()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _DCBatchProgramRequisitionList()
        {
            return PartialView();
        }
        public ActionResult DCBatchProgramRequisition()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _DCBatchProgramRequisition()
        {
            return PartialView();
        }

        public ActionResult DCIssueBatchProgram()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _DCIssueBatchProgram()
        {
            return PartialView();
        }

        public ActionResult DcReport()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _DcReportParams()
        {
            return PartialView();
        }

        public ActionResult DosingTemplate()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _DosingTemplate()
        {
            return PartialView();
        }


        public ActionResult DCBatchProgramAddition()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _DCBatchProgramAddition()
        {
            return PartialView();
        }

        public ActionResult DCRequestLoanRequisitionList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _DCRequestLoanRequisitionList()
        {
            return PartialView();
        }

        public ActionResult DCRequestLoanRequisition()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _DCRequestLoanRequisition()
        {
            return PartialView();
        }


        public ActionResult DCIssueAgainstLoanRequisitionList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _DCIssueAgainstLoanRequisitionList()
        {
            return PartialView();
        }

        public ActionResult DCIssueAgainstLoanRequisition()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _DCIssueAgainstLoanRequisition()
        {
            return PartialView();
        }

        public ActionResult DCIssuedLoanAdjust()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _DCIssuedLoanAdjust()
        {
            return PartialView();
        }

        public ActionResult DCIssuedLoanAdjustList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _DCIssuedLoanAdjustList()
        {
            return PartialView();
        }

        public ActionResult DyeMachine()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _DyeMachine()
        {
            return PartialView();
        }

        public PartialViewResult _DyeBatchFabReq()
        {
            return PartialView();
        }


        public ActionResult DyeDailyBatchProdReprocess()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DyeDailyBatchProdReprocess()
        {
            return PartialView();
        }

        public ActionResult DCOpeningSupLNSTK()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DCOpeningSupLNSTK()
        {
            return PartialView();
        }


        public ActionResult DCIssueBatchProgramList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DCIssueBatchProgramList()
        {
            return PartialView();
        }

        public ActionResult DyeingHtmlRpt()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DyeingProgramRpt()
        {
            return PartialView();
        }

        public PartialViewResult _DyeProgramScdlModal()
        {
            return PartialView();
        }

        public PartialViewResult _DyeBatchProgramModal(String ViewName = "_DyeBatchProgramModal")
        {
            return PartialView(ViewName);
        }

        public PartialViewResult _DyeBatchFabReqSample()
        {
            return PartialView();
        }

        public ActionResult DyeDailyOtherBatchProdReprocess()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DyeDailyOtherBatchProdReprocess()
        {
            return PartialView();
        }


        public ActionResult DyeChemicalReceiveView()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View("DyeChemicalReceive");
        }

        public PartialViewResult _DyeChemicalReceiveView()
        {
            return PartialView();
        }

        public ActionResult DCBatchProgramCompleteList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DCBatchProgramCompleteList()
        {
            return PartialView();
        }

        public ActionResult DyeChemicalLoanReceiveList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DyeChemicalLoanReceiveList()
        {
            return PartialView();
        }

        public ActionResult DyeChemicalLoanReceive()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DyeChemicalLoanReceive()
        {
            return PartialView();
        }

        public ActionResult DyeChemicalLoanReceiveView()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DyeChemicalLoanReceiveView()
        {
            return PartialView();
        }

        public ActionResult CompleteBatchList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _CompleteBatchList()
        {
            return PartialView();
        }

        public ActionResult CompleteBatchReprocess()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _CompleteBatchReprocess()
        {
            return PartialView();
        }

        public ActionResult DCBatchProgramAddRequisition()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DCBatchProgramAddRequisition()
        {
            return PartialView();
        }

        public ActionResult BatchProduction()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _BatchProduction()
        {
            return PartialView();
        }

        public ActionResult CheckRollInspection()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _CheckRollInspection()
        {
            return PartialView();
        }

        public ActionResult DyeingFinishingMachine()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DyeingFinishingMachine()
        {
            return PartialView();
        }

        public ActionResult DyeingFinishingProductionList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DyeingFinishingProductionList()
        {
            return PartialView();
        }

        public ActionResult DyeingFinishingProduction()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DyeingFinishingProduction()
        {
            return PartialView();
        }

        public ActionResult DFMachineProcessMapping()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DFMachineProcessMapping()
        {
            return PartialView();
        }

        public ActionResult MachineWashProduction()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _MachineWashProduction()
        {
            return PartialView();
        }

        public ActionResult DyeingProductionBoard()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DyeingProductionBoard()
        {
            return PartialView();
        }

        public PartialViewResult _DyeMachineStatusModal()
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


        public ActionResult ScProgram()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ScProgram()
        {
            return PartialView();
        }


        public ActionResult DfReport()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _DfReportParams()
        {
            return PartialView();
        }

        public ActionResult ScProgramReceiveList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _ScProgramReceiveList()
        {
            return PartialView();
        }


        public ActionResult ScProgramReceive()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ScProgramReceive()
        {
            return PartialView();
        }

        public ActionResult ScProgramReceiveView()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ScProgramReceiveView()
        {
            return PartialView();
        }

        public ActionResult DFProcessType()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DFProcessType()
        {
            return PartialView();
        }

        public ActionResult ReceiveFinishFabricList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _ReceiveFinishFabricList()
        {
            return PartialView();
        }


        public ActionResult ReceiveFinishFabric()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ReceiveFinishFabric()
        {
            return PartialView();
        }


        public JsonResult DFMachineSave(DF_MACHINEModel ob)
        {
            string jsonStr = "";

            //if (ob.MC_IMAGE_FILE != null && ob.MC_IMAGE_FILE.ContentLength > 0)
            //{
            //    var image = Image.FromStream(ob.MC_IMAGE_FILE.InputStream);

            //    MemoryStream ms = new MemoryStream();
            //    // Convert Image to byte[]
            //    image.Save(ms, image.RawFormat);
            //    byte[] imageBytes = ms.ToArray();

            //    // Convert byte[] to Base64 String
            //    ob.MC_IMAGE = Convert.ToBase64String(imageBytes);
            //    image.Dispose();
            //}

            try
            {
                jsonStr = ob.Save();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return Json(new { success = true, jsonStr }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GreyFabReq()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _GreyFabReqList()
        {
            return PartialView();
        }

        public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        public PartialViewResult _GreyFabReqIssue()
        {
            return PartialView();
        }
        public PartialViewResult _GreyFabReqIssueD()
        {
            return PartialView();
        }
        public PartialViewResult _GreyFabReqIssueTrim()
        {
            return PartialView();
        }

        public ActionResult DyeScProgram()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _DyeScProgram()
        {
            return PartialView();
        }
        public PartialViewResult _GreyFabReqListInH()
        {
            return PartialView();
        }


        public ActionResult RunningBatchList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _RunningBatchList()
        {
            return PartialView();
        }

        public ActionResult RunningBatchEdit()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _RunningBatchEdit()
        {
            return PartialView();
        }


        public ActionResult ScPreTreatmentList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ScPreTreatmentList()
        {
            return PartialView();
        }

        public ActionResult ScPreTreatment()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ScPreTreatment()
        {
            return PartialView();
        }


        public PartialViewResult _GreyFabReqPTIssue()
        {
            return PartialView();
        }

        public PartialViewResult _GreyFabReqPTIssueD()
        {
            return PartialView();
        }


        public ActionResult ScPreTreatmentReceiveList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ScPreTreatmentReceiveList()
        {
            return PartialView();
        }

        public ActionResult ScPreTreatmentReceive()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ScPreTreatmentReceive()
        {
            return PartialView();
        }

        public ActionResult FinishFabricInspection()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _FinishFabricInspection()
        {
            return PartialView();
        }


        public ActionResult FabricInspectionReportList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _FabricInspectionReportList()
        {
            return PartialView();
        }

        public ActionResult FabricInspectionReport()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _FabricInspectionReport()
        {
            return PartialView();
        }



        public ActionResult ScPreTreatmentChallanList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ScPreTreatmentChallanList()
        {
            return PartialView();
        }

        public ActionResult ScPreTreatmentChallan()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ScPreTreatmentChallan()
        {
            return PartialView();
        }

        public ActionResult ScPreTreatmentBillList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ScPreTreatmentBillList()
        {
            return PartialView();
        }

        public ActionResult ScPreTreatmentBill()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ScPreTreatmentBill()
        {
            return PartialView();
        }

        public ActionResult DyeMcMaintenanceList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DyeMcMaintenanceList()
        {
            return PartialView();
        }

        public ActionResult DyeMcMaintenance()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DyeMcMaintenance()
        {
            return PartialView();
        }

        public ActionResult BatchProductionList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _BatchProductionList()
        {
            return PartialView();
        }

        public ActionResult LabdipRecipeViewList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _LabdipRecipeViewList()
        {
            return PartialView();
        }
        public ActionResult LabdipRecipeView()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _LabdipRecipeView()
        {
            return PartialView();
        }

        public ActionResult ScProgramChallanList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ScProgramChallanList()
        {
            return PartialView();
        }
        public ActionResult ScProgramChallan()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ScProgramChallan()
        {
            return PartialView();
        }


        public ActionResult BatchAtDfStore()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _BatchAtDfStore()
        {
            return PartialView();
        }

        public ActionResult BatchAtFabricStore()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _BatchAtFabricStore()
        {
            return PartialView();
        }

        public ActionResult FabricHoldList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _FabricHoldList()
        {
            return PartialView();
        }


        public ActionResult DfOnlineQcList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DfOnlineQcList()
        {
            return PartialView();
        }

        public ActionResult DfOnlineQc()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DfOnlineQc()
        {
            return PartialView();
        }

        public ActionResult ScPreTreatmentReceiveView()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ScPreTreatmentReceiveView()
        {
            return PartialView();
        }


        public ActionResult DfMcMaintenanceList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }


        public ActionResult DfGmtWashProdList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _DfGmtWashProdList()
        {
            return PartialView();
        }

        public ActionResult DfGmtWashProd()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _DfGmtWashProd()
        {
            return PartialView();
        }


        public ActionResult DfScProgramBillList()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }
        public PartialViewResult _DfScProgramBillList()
        {
            return PartialView();
        }
        
        public ActionResult DfScProgramBill()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _DfScProgramBill()
        {
            return PartialView();
        }

        public ActionResult AopBatchList()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }
        public PartialViewResult _AopBatchList()
        {
            return PartialView();
        }

        public ActionResult AopBatch()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _AopBatch()
        {
            return PartialView();
        }

        public ActionResult FinFabDelv2Store()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _FinFabDelv2Store()
        {
            return PartialView();
        }

        public ActionResult DfBtCrdPrint()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _DfBtCrdPrint()
        {
            return PartialView();
        }

        public ActionResult DfBtFinalQC()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _DfBtFinalQC()
        {
            return PartialView();
        }

        public ActionResult ShadeCheckRollInspectionList()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _ShadeCheckRollInspectionList()
        {
            return PartialView();
        }

        public ActionResult ShadeCheckRollInspection()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _ShadeCheckRollInspection()
        {
            return PartialView();
        }

        public ActionResult DfMaterialReqList()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _DfMaterialReqList()
        {
            return PartialView();
        }

        public ActionResult DfMaterialReq()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _DfMaterialReq()
        {
            return PartialView();
        }

        public ActionResult DyeScProgramList()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _DyeScProgramList()
        {
            return PartialView();
        }

        public ActionResult DyeingScProgram()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _DyeingScProgram()
        {
            return PartialView();
        }

        public ActionResult ShadeCheckRollInspectionView()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }
        
        public PartialViewResult _ShadeCheckRollInspectionView()
        {
            return PartialView();
        }
        
        
        
        
        

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult uploadScPTDocs(DF_SC_PT_RCV_HModel ob)
        {
            string extension = null;
            if (ob.ATT_FILE != null && ob.ATT_FILE.ContentLength > 0)
            {
                extension = Path.GetExtension(ob.ATT_FILE.FileName);
                string path = Path.Combine(Server.MapPath("~/UPLOAD_DOCS/SC_CHALLAN"), ob.RPT_PATH_URL);
                ob.ATT_FILE.SaveAs(path);
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);

        }


        public static void SendMixLotMail(string pDYE_BATCH_NO = null)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            DYE_BATCH_PLANModel ob = new DYE_BATCH_PLANModel();
            var rptData = ob.getProgramData(null, null, 3005, pDYE_BATCH_NO);

            var email = new SendMixLotBatchApprovalMailService
            {
                //To = "dev.afzal@multi-fabs.com, cto@multi-fabs.com",
                To = @"dyeing.mgr@multi-fabs.com, dyeing.kader@multi-fabs.com, dev.afzal@multi-fabs.com, cto@multi-fabs.com, mis.jahid@multi-fabs.com, batch.dyeing@multi-fabs.com, dyeing.batch@multi-fabs.com, masum@multi-fabs.com",
                data = rptData[0]
            };

            emailService.Send(email);
        }
    }
}