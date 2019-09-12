using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;

namespace ERP.Model
{
    public class MC_ACCS_PO_HModel
    {
        public Int64 MC_ACCS_PO_H_ID { get; set; }
        public Int64 MC_STYL_BGT_H_ID { get; set; }
        public string ACCS_PO_NO { get; set; }
        public string ACCS_PO_SUB { get; set; }
        public DateTime ACCS_PO_DT { get; set; }
        public Int64 ACCS_PO_REQ_BY { get; set; }
        public DateTime ACCS_DELV_DT { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public Int64 LK_PURCH_TYPE_ID { get; set; }
        public string SP_INSTRUCTION { get; set; }
        public string IS_SP_INSTRUCTION { get; set; }

        public string ACC_PO_NOTE { get; set; }
        public string IS_ACC_PO_NOTE { get; set; }
        public string IS_SUPP_ONLINE { get; set; }
        public Int64? MC_TNA_TASK_STATUS_ID { get; set; }

        public Int64 VERSION_NO { get; set; }
        public string STORE_NAME_EN { get; set; }
        public string LK_PURCH_TYPE_NAME { get; set; }
        public string XML { get; set; }
        public string BLK_BOM_LIST { get; set; }
        public bool IS_CHANGED { get; set; }
        public string EVENT_NAME { get; set; }
        public Int64? SCM_SUPPLIER_ID { get; set; }

        public string Submit()
        {
            const string sp = "pkg_acc_booking.mc_accs_po_h_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_H_ID", Value = ob.MC_ACCS_PO_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYL_BGT_H_ID", Value = ob.MC_STYL_BGT_H_ID},
                     new CommandParameter() {ParameterName = "pACCS_PO_NO", Value = ob.ACCS_PO_NO},
                     new CommandParameter() {ParameterName = "pACCS_PO_SUB", Value = ob.ACCS_PO_SUB},
                     new CommandParameter() {ParameterName = "pACCS_PO_DT", Value = ob.ACCS_PO_DT.Date},
                     new CommandParameter() {ParameterName = "pACCS_PO_REQ_BY", Value = ob.ACCS_PO_REQ_BY},
                     new CommandParameter() {ParameterName = "pACCS_DELV_DT", Value = ob.ACCS_DELV_DT.Date},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pLK_PURCH_TYPE_ID", Value = ob.LK_PURCH_TYPE_ID},
                     new CommandParameter() {ParameterName = "pSP_INSTRUCTION", Value = ob.SP_INSTRUCTION},
                     new CommandParameter() {ParameterName = "pACC_PO_NOTE", Value = ob.ACC_PO_NOTE},
                     new CommandParameter() {ParameterName = "pIS_SUPP_ONLINE", Value = ob.IS_SUPP_ONLINE},
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_STATUS_ID", Value = ob.MC_TNA_TASK_STATUS_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "OP_ACCS_PO_NO", Value =0, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_MC_STYL_BGT_H_ID", Value =0, Direction = ParameterDirection.Output},                   
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]}
                 }, sp);

                 foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                 {
                     jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                     if (i < ds.Tables["OUTPARAM"].Rows.Count) 
                     { 
                      jsonStr += ",";
                     }  
                     else  
                      {  
                          jsonStr += "}"; 
                      }  
                       i++ ;
                 }
                 return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_ACCS_PO_HModel> SelectAll()
        {
            string sp = "Select_MC_ACCS_PO_H";
            try
            {
                var obList = new List<MC_ACCS_PO_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_ACCS_PO_HModel ob = new MC_ACCS_PO_HModel();
                            ob.MC_ACCS_PO_H_ID = (dr["MC_ACCS_PO_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ACCS_PO_H_ID"]);
                            ob.MC_STYL_BGT_H_ID = (dr["MC_STYL_BGT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_BGT_H_ID"]);
                            ob.ACCS_PO_NO = (dr["ACCS_PO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACCS_PO_NO"]);
                            ob.ACCS_PO_SUB = (dr["ACCS_PO_SUB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACCS_PO_SUB"]);
                          //  ob.ACCS_PO_DT = (dr["ACCS_PO_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACCS_PO_DT"]);
                            ob.ACCS_PO_REQ_BY = (dr["ACCS_PO_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACCS_PO_REQ_BY"]);
                            ob.ACCS_DELV_DT = (dr["ACCS_DELV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACCS_DELV_DT"]);
                            ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                            ob.LK_PURCH_TYPE_ID = (dr["LK_PURCH_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PURCH_TYPE_ID"]);
                            ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);
                            ob.ACC_PO_NOTE = (dr["ACC_PO_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACC_PO_NOTE"]);
                            ob.IS_SUPP_ONLINE = (dr["IS_SUPP_ONLINE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SUPP_ONLINE"]);
                          
                            if (dr["MC_TNA_TASK_STATUS_ID"] != DBNull.Value)
                            {
                                ob.MC_TNA_TASK_STATUS_ID = Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                            }

                            ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? "Options" : Convert.ToString(dr["EVENT_NAME"]);
                            
                            ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public RF_PAGERModel AccPoHeaderList(Int64 pageNumber, Int64 pageSize, Int64? pMC_BYR_ACC_ID, Int64? pMC_BUYER_ID, Int64? pMC_STYLE_H_EXT_ID,
            string pACCS_PO_SUB, string pACCS_PO_DT,string pACCS_DELV_DT, string pLK_PURCH_TYPE_NAME, string pIS_SUPP_ONLINE, string pSTORE_NAME_EN,
            string pSUP_TRD_NAME_EN, string pMC_TNA_TASK_STATUS_ID, int? pSC_USER_ID, Int64? pMC_ORDER_H_ID, DateTime? pFIRSTDATE, DateTime? pLASTDATE)
        {
            string sp = "pkg_acc_booking.mc_accs_po_h_select_paginate";
            try
            {
                
                var obList = new List<MC_ACCS_PO_HModel>();
                Int64 vTotalRec = 0;
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     new CommandParameter() {ParameterName = "pACCS_PO_SUB", Value = pACCS_PO_SUB},
                     new CommandParameter() {ParameterName = "pACCS_DELV_DT", Value = pACCS_DELV_DT},
                     new CommandParameter() {ParameterName = "pACCS_PO_DT", Value = pACCS_PO_DT},
                     new CommandParameter() {ParameterName = "pLK_PURCH_TYPE_NAME", Value = pLK_PURCH_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pIS_SUPP_ONLINE", Value = pIS_SUPP_ONLINE},
                     new CommandParameter() {ParameterName = "pSTORE_NAME_EN", Value = pSTORE_NAME_EN},
                     new CommandParameter() {ParameterName = "pSUP_TRD_NAME_EN", Value = pSUP_TRD_NAME_EN},
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_STATUS_ID", Value = pMC_TNA_TASK_STATUS_ID},
                     
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pFIRSTDATE", Value = pFIRSTDATE},
                     new CommandParameter() {ParameterName = "pLASTDATE", Value = pLASTDATE},


                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ACCS_PO_HModel ob = new MC_ACCS_PO_HModel();
                    ob.MC_ACCS_PO_H_ID = (dr["MC_ACCS_PO_H_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["MC_ACCS_PO_H_ID"]);
                    ob.MC_STYL_BGT_H_ID = (dr["MC_STYL_BGT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_BGT_H_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    
                    ob.ACCS_PO_NO = (dr["ACCS_PO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACCS_PO_NO"]);
                    ob.ACCS_PO_SUB = (dr["ACCS_PO_SUB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACCS_PO_SUB"]);
                    ob.ACCS_PO_DT = (dr["ACCS_PO_DT"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["ACCS_PO_DT"]);
                    ob.ACCS_PO_REQ_BY = (dr["ACCS_PO_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACCS_PO_REQ_BY"]);
                    ob.ACCS_DELV_DT = (dr["ACCS_DELV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACCS_DELV_DT"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.LK_PURCH_TYPE_ID = (dr["LK_PURCH_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PURCH_TYPE_ID"]);
                    ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);
                    ob.IS_SP_INSTRUCTION = string.IsNullOrEmpty(ob.SP_INSTRUCTION) ? "N" : "Y";
                    ob.ACC_PO_NOTE = (dr["ACC_PO_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACC_PO_NOTE"]);
                    ob.IS_ACC_PO_NOTE = string.IsNullOrEmpty(ob.ACC_PO_NOTE) ? "N" : "Y";
                    ob.IS_SUPP_ONLINE = (dr["IS_SUPP_ONLINE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SUPP_ONLINE"]);
                   
                    

                    if (dr["MC_TNA_TASK_STATUS_ID"] != DBNull.Value)
                    {
                        ob.MC_TNA_TASK_STATUS_ID = Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                    }


                    if (dr["SCM_SUPPLIER_ID"] != DBNull.Value)
                    {
                        ob.SCM_SUPPLIER_ID = Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    }

                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);

                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    ob.LK_PURCH_TYPE_NAME = (dr["LK_PURCH_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_PURCH_TYPE_NAME"]);
                    ob.BLK_BOM_LIST = (dr["BLK_BOM_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLK_BOM_LIST"]);

                    ob.BLK_BOM_ACT = (dr["BLK_BOM_ACT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BLK_BOM_ACT"]);
                    ob.ITEM_ACT = (dr["ITEM_ACT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_ACT"]);
                    
                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);

                    ob.WORK_STYLE = (dr["WORK_STYLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE"]);
                    ob.BASE_STYLE = (dr["BASE_STYLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BASE_STYLE"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);

                    ob.TASK_STATUS_NAME = (dr["TASK_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TASK_STATUS_NAME"]);
                    ob.APPROVED = false;
                    ob.REJECT = false;
                    ob.RESEND = false;

                    obList.Add(ob);
                }

                obj.total = vTotalRec;
                obj.data = obList;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_ACCS_PO_HModel Select(long ID, int? pHR_DEPARTMENT_ID = null)
        {
            string sp = "pkg_acc_booking.mc_accs_po_h_select";
            try
            {
                var ob = new MC_ACCS_PO_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_H_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value =pHR_DEPARTMENT_ID}, 
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_ACCS_PO_H_ID = (dr["MC_ACCS_PO_H_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["MC_ACCS_PO_H_ID"]);
                        ob.MC_STYL_BGT_H_ID = (dr["MC_STYL_BGT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_BGT_H_ID"]);
                        ob.ACCS_PO_NO = (dr["ACCS_PO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACCS_PO_NO"]);
                        ob.ACCS_PO_SUB = (dr["ACCS_PO_SUB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACCS_PO_SUB"]);
                        ob.ACCS_PO_DT = (dr["ACCS_PO_DT"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["ACCS_PO_DT"]);
                        ob.ACCS_PO_REQ_BY = (dr["ACCS_PO_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACCS_PO_REQ_BY"]);
                        ob.ACCS_DELV_DT = (dr["ACCS_DELV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACCS_DELV_DT"]);
                        ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                        ob.LK_PURCH_TYPE_ID = (dr["LK_PURCH_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PURCH_TYPE_ID"]);
                        ob.SP_INSTRUCTION =  (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);
                        ob.IS_SP_INSTRUCTION = string.IsNullOrEmpty(ob.SP_INSTRUCTION) ? "N" : "Y";
                        ob.ACC_PO_NOTE = (dr["ACC_PO_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACC_PO_NOTE"]);
                        ob.IS_ACC_PO_NOTE = string.IsNullOrEmpty(ob.ACC_PO_NOTE) ? "N" : "Y";
                        ob.IS_SUPP_ONLINE = (dr["IS_SUPP_ONLINE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SUPP_ONLINE"]);
                        if (dr["MC_TNA_TASK_STATUS_ID"] != DBNull.Value)
                        {
                            ob.MC_TNA_TASK_STATUS_ID = Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                        }



                        ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? "Options" : Convert.ToString(dr["EVENT_NAME"]);

                        ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);
                        ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);

                        ob.STYLE_NO_BSE = (dr["STYLE_NO_BSE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO_BSE"]);
                        ob.STYLE_NO_EXT = (dr["STYLE_NO_EXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO_EXT"]);
                        ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);

                        if (dr["SCM_SUPPLIER_ID"] != DBNull.Value)
                        {
                            ob.SCM_SUPPLIER_ID = Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                        }

                        ob.ALLOW_ACCESS = (dr["ALLOW_ACCESS"] == DBNull.Value) ? "N" : Convert.ToString(dr["ALLOW_ACCESS"]);
                        ob.IS_SUP_RT_UPD = (dr["IS_SUP_RT_UPD"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SUP_RT_UPD"]);

                        ob.IS_FRM_EDITABLE = (dr["IS_FRM_EDITABLE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FRM_EDITABLE"]);

                        ob.STR_NAME = (dr["STR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_NAME"]);
                        ob.STR_OFFICE_NAME = (dr["STR_OFFICE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_OFFICE_NAME"]);
                        ob.STR_OFFICE_ADDRESS = (dr["STR_OFFICE_ADDRESS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_OFFICE_ADDRESS"]);
                        ob.SUP_NAME = (dr["SUP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_NAME"]);
                        ob.SUP_ADDRESS = (dr["SUP_ADDRESS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_ADDRESS"]);
                        ob.SUP_CP_NAME_EN = (dr["SUP_CP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_CP_NAME_EN"]);
                        ob.SUP_CP_DESIG = (dr["SUP_CP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_CP_DESIG"]);
                        ob.ITEM_LIST = (dr["ITEM_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_LIST"]);

                        ob.IS_CHANGED = false;
                        
               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public Int64 ITEM_ACT { get; set; }
        public Int64 BLK_BOM_ACT { get; set; }
        public Int64 MC_BYR_ACC_ID { get; set; }
        public string STYLE_NO_BSE { get; set; }
        public string BUYER_NAME_EN { get; set; }
        public string STYLE_NO_EXT { get; set; }
        public string SUP_TRD_NAME_EN { get; set; }
        public string ALLOW_ACCESS { get; set; }

        public string IS_SUP_RT_UPD { get; set; }

        public string IS_FRM_EDITABLE { get; set; }

        public string BASE_STYLE { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string WORK_STYLE { get; set; }
        public string ORDER_NO_LIST { get; set; }

        public bool APPROVED { get; set; }

        public bool REJECT { get; set; }

        public bool RESEND { get; set; }

        public string saveDashboardApprovalData(string XML)
        {
            const string sp = "pkg_acc_booking.saveDashboardApprovalData";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pXML", Value = XML},                
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                    if (i < ds.Tables["OUTPARAM"].Rows.Count)
                    {
                        jsonStr += ",";
                    }
                    else
                    {
                        jsonStr += "}";
                    }
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string TASK_STATUS_NAME { get; set; }

        public string setTnAStatus(long pMC_ACCS_PO_H_ID, long pMC_TNA_TASK_STATUS_ID)
        {
            const string sp = "pkg_acc_booking.set_tna_status";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_H_ID", Value = pMC_ACCS_PO_H_ID},  
                      new CommandParameter() {ParameterName = "pMC_TNA_TASK_STATUS_ID", Value = pMC_TNA_TASK_STATUS_ID},  
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                    if (i < ds.Tables["OUTPARAM"].Rows.Count)
                    {
                        jsonStr += ",";
                    }
                    else
                    {
                        jsonStr += "}";
                    }
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long? MC_STYLE_H_ID { get; set; }
        public string STR_NAME { get; set; }
        public string STR_OFFICE_NAME { get; set; }
        public string STR_OFFICE_ADDRESS { get; set; }
        public string SUP_NAME { get; set; }
        public string SUP_ADDRESS { get; set; }
        public string SUP_CP_NAME_EN { get; set; }
        public string SUP_CP_DESIG { get; set; }
        public string ITEM_LIST { get; set; }
    }
}