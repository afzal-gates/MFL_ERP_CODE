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
    public class KNT_YD_PRG_HModel
    {
        public Int64 KNT_YD_PRG_H_ID { get; set; }
        public Int64? PARENT_ID { get; set; }
        public long MC_BYR_ACC_ID { get; set; }
        public Int64 REVISION_NO { get; set; }
        public string PRG_REF_NO { get; set; }
        public DateTime PRG_ISS_DT { get; set; }
        public DateTime DELV_TGT_DT { get; set; }
        public Int64 YD_PRG_BY { get; set; }
        public string PRG_ATTN_MAIL { get; set; }
        public string IS_SUB_CONTR { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public string REMARKS { get; set; }
        public string IS_FINALIZED { get; set; }

        public string IS_FINALIZED_M { get; set; }
        public Int64 RF_FAB_PROD_CAT_ID { get; set;}

        public string FEEDER_PLANS_XML { get; set; }
        public string COL_REQS_XML { get; set; }
        public string YDP_YRN_REQ_XML { get; set; }

        public string IS_REMARKS { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string ORDER_NO { get; set; }
        public long MC_FAB_PROD_ORD_H_ID { get; set; }
        public Int64? RF_ACTN_STATUS_ID { get; set; }
        public string FAB_PROD_CAT_SNAME { get; set; }
        public long? MC_STYLE_H_ID { get; set; }
        public string IS_REQS_DONE { get; set; }
        public string IS_REVISABLE { get; set; }
        public string PARENT_PRG_REF_NO { get; set; }
        public string SUP_TRD_NAME_EN { get; set; }

        private List<KNT_COMBO_ColModel> _feeder_plans = null;
        public List<KNT_COMBO_ColModel> feeder_plans
        {
            get
            {
                if (_feeder_plans == null)
                {
                    _feeder_plans = new List<KNT_COMBO_ColModel>();
                }
                return _feeder_plans;
            }
            set
            {
                _feeder_plans = value;
            }
        }
        private List<KNT_YDP_D_COLModel> _col_reqs = null;
        public List<KNT_YDP_D_COLModel> col_reqs
        {
            get
            {
                if (_col_reqs == null)
                {
                    _col_reqs = new List<KNT_YDP_D_COLModel>();
                }
                return _col_reqs;
            }
            set
            {
                _col_reqs = value;
            }
        }

        private List<KNT_YDP_D_YRNModel> _colYrnDtl = null;
        public List<KNT_YDP_D_YRNModel> colYrnDtl
        {
            get
            {
                if (_colYrnDtl == null)
                {
                    _colYrnDtl = new List<KNT_YDP_D_YRNModel>();
                }
                return _colYrnDtl;
            }
            set
            {
                _colYrnDtl = value;
            }
        }
        

        public string Save()
        {
            const string sp = "PKG_KNT_YD_PRG.knt_yd_prg_h_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value = ob.KNT_YD_PRG_H_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_LST", Value = ob.MC_FAB_PROD_ORD_H_LST},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pPRG_REF_NO", Value = ob.PRG_REF_NO},
                     new CommandParameter() {ParameterName = "pPRG_ISS_DT", Value = ob.PRG_ISS_DT},
                     new CommandParameter() {ParameterName = "pDELV_TGT_DT", Value = ob.DELV_TGT_DT},
                     new CommandParameter() {ParameterName = "pYD_PRG_BY", Value = ob.YD_PRG_BY},
                     new CommandParameter() {ParameterName = "pPRG_ATTN_MAIL", Value = ob.PRG_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pIS_SUB_CONTR", Value = ob.IS_SUB_CONTR},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pFEEDER_PLANS_XML", Value = ob.FEEDER_PLANS_XML},
                     new CommandParameter() {ParameterName = "pCOL_REQS_XML", Value = ob.COL_REQS_XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_KNT_YD_PRG_H_ID", Value =0, Direction = ParameterDirection.Output}
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


        public string CreateRequisition()
        {
            const string sp = "pkg_knt_yd_prg.create_store_requisition";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value = ob.KNT_YD_PRG_H_ID},
                     new CommandParameter() {ParameterName = "pYDP_YRN_REQ_XML", Value = ob.YDP_YRN_REQ_XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
    
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
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
        
        public List<KNT_YD_PRG_HModel> SelectAll()
        {
            string sp = "Select_KNT_YD_PRG_H";
            try
            {
                var obList = new List<KNT_YD_PRG_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            KNT_YD_PRG_HModel ob = new KNT_YD_PRG_HModel();
                            ob.KNT_YD_PRG_H_ID = (dr["KNT_YD_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PRG_H_ID"]);
                            if (dr["PARENT_ID"] != DBNull.Value)
                            {
                                ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                            }
                            ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                            
                            ob.MC_FAB_PROD_ORD_H_LST = (dr["MC_FAB_PROD_ORD_H_LST"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["MC_FAB_PROD_ORD_H_LST"]);
                            ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                            ob.PRG_REF_NO = (dr["PRG_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_REF_NO"]);
                            ob.PRG_ISS_DT = (dr["PRG_ISS_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PRG_ISS_DT"]);
                            ob.DELV_TGT_DT = (dr["DELV_TGT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["DELV_TGT_DT"]);
                            ob.YD_PRG_BY = (dr["YD_PRG_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YD_PRG_BY"]);
                            ob.PRG_ATTN_MAIL = (dr["PRG_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_ATTN_MAIL"]);
                            ob.IS_SUB_CONTR = (dr["IS_SUB_CONTR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SUB_CONTR"]);
                            ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                            ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);
                            ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                            ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                            ob.IS_FINALIZED_M = ob.IS_FINALIZED;

                            ob.IS_REQS_DONE = (dr["IS_REQS_DONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REQS_DONE"]);
                            ob.IS_REVISABLE = (dr["IS_REVISABLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REVISABLE"]);
                  
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public KNT_YD_PRG_HModel Select(Int64? pKNT_YD_PRG_H_ID,Int64? pPARENT_ID)
        {
            string sp = "PKG_KNT_YD_PRG.knt_yd_prg_h_select";
            try
            {
                var ob = new KNT_YD_PRG_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value =pKNT_YD_PRG_H_ID??pPARENT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = pPARENT_ID.Equals(null)?3001:3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                        ob.KNT_YD_PRG_H_ID = (dr["KNT_YD_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PRG_H_ID"]);
                        if (dr["PARENT_ID"] != DBNull.Value)
                        {
                            ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                        }

                        if (dr["MC_STYLE_H_ID"] != DBNull.Value)
                        {
                            ob.MC_STYLE_H_ID = Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                        }

                        ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                        ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                        ob.PRG_REF_NO = (dr["PRG_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_REF_NO"]);
                        ob.PRG_ISS_DT = (dr["PRG_ISS_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PRG_ISS_DT"]);
                        ob.DELV_TGT_DT = (dr["DELV_TGT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["DELV_TGT_DT"]);
                        ob.YD_PRG_BY = (dr["YD_PRG_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YD_PRG_BY"]);
                       
                        ob.PRG_ATTN_MAIL = (dr["PRG_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_ATTN_MAIL"]);
                        ob.IS_SUB_CONTR = (dr["IS_SUB_CONTR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SUB_CONTR"]);
                        ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                        ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);
                        ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                        ob.IS_REMARKS = String.IsNullOrEmpty(ob.REMARKS)?"N":"Y";
                        ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);

                        ob.IS_FINALIZED_M = ob.IS_FINALIZED;

                        ob.IS_REQS_DONE = (dr["IS_REQS_DONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REQS_DONE"]);
                        ob.IS_REVISABLE = (dr["IS_REVISABLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REVISABLE"]);

                        ob.MC_FAB_PROD_ORD_H_LST = (dr["MC_FAB_PROD_ORD_H_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_FAB_PROD_ORD_H_LST"]);
                        ob.feeder_plans = new KNT_COMBO_ColModel().getColorList(ob.MC_FAB_PROD_ORD_H_LST, pKNT_YD_PRG_H_ID, pPARENT_ID);
                        ob.col_reqs = new KNT_YDP_D_COLModel().getColorSummaryList(ob.MC_FAB_PROD_ORD_H_LST, pKNT_YD_PRG_H_ID, pPARENT_ID);
                                
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object getProgramNoAuto()
        {
            string sp = "PKG_KNT_YD_PRG.knt_yd_prg_h_select";
            try
            {
                var ob = new Object();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3018},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    ob = new
                    {
                        PRG_REF_NO = Convert.ToString(dr["PRG_REF_NO"])
                    };
                  

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public string MC_FAB_PROD_ORD_H_LST { get; set; }

        public RF_PAGERModel getProgamList(
               Int64 pageNumber, 
               Int64 pageSize, 
               Int64? pSCM_SUPPLIER_ID, 
               string pPRG_REF_NO, 
               string pPRG_ISS_DT, 
               string pDELV_TGT_DT,
               Int64? pMC_BYR_ACC_ID,
               Int64? pMC_FAB_PROD_ORD_H_ID,
               string pIS_PL_ADJ
        )
        {
            string sp = "PKG_KNT_YD_PRG.knt_yd_prg_h_select";
            try
            {
                var obList = new List<KNT_YD_PRG_HModel>();
                Int64 vTotalRec = 0;
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pPRG_REF_NO", Value =pPRG_REF_NO},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pPRG_ISS_DT", Value = pPRG_ISS_DT},
                     new CommandParameter() {ParameterName = "pDELV_TGT_DT", Value = pDELV_TGT_DT},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pIS_PL_ADJ", Value =pIS_PL_ADJ},
                     new CommandParameter() {ParameterName = "pOption", Value =3009},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YD_PRG_HModel ob = new KNT_YD_PRG_HModel();
                    ob.KNT_YD_PRG_H_ID = (dr["KNT_YD_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PRG_H_ID"]);
                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.PRG_REF_NO = (dr["PRG_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_REF_NO"]);
                    ob.PRG_ISS_DT = (dr["PRG_ISS_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PRG_ISS_DT"]);
                    ob.DELV_TGT_DT = (dr["DELV_TGT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["DELV_TGT_DT"]);
                    ob.YD_PRG_BY = (dr["YD_PRG_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YD_PRG_BY"]);

                    ob.PRG_ATTN_MAIL = (dr["PRG_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_ATTN_MAIL"]);
                    ob.IS_SUB_CONTR = (dr["IS_SUB_CONTR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SUB_CONTR"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_REMARKS = String.IsNullOrEmpty(ob.REMARKS) ? "N" : "Y";
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);

                    ob.IS_FINALIZED_M = ob.IS_FINALIZED;

                    ob.IS_REQS_DONE = (dr["IS_REQS_DONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REQS_DONE"]);
                    ob.IS_REVISABLE = (dr["IS_REVISABLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REVISABLE"]);

                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.PARENT_PRG_REF_NO = (dr["PARENT_PRG_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARENT_PRG_REF_NO"]);

                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.FAB_PROD_CAT_SNAME = (dr["FAB_PROD_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_SNAME"]);

                    ob.colYrnDtl = new KNT_YDP_D_YRNModel().GetColorYrnByPrg(ob.KNT_YD_PRG_H_ID);

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

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

        public List<KNT_YD_PRG_HModel> getYdProgramListDs(string pPRG_REF_NO)
        {
            string sp = "PKG_KNT_YD_PRG.knt_yd_prg_h_select";
            try
            {
                var obList = new List<KNT_YD_PRG_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPRG_REF_NO", Value =pPRG_REF_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =3017},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YD_PRG_HModel ob = new KNT_YD_PRG_HModel();
                    ob.KNT_YD_PRG_H_ID = (dr["KNT_YD_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PRG_H_ID"]);
                    ob.PRG_REF_NO = (dr["PRG_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_REF_NO"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.FAB_PROD_CAT_SNAME = (dr["FAB_PROD_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_SNAME"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_YD_PRG_HModel> getYdProgramDropDownData(string pPRG_REF_NO, long? pKNT_YD_PRG_H_ID)
        {
            string sp = "PKG_KNT_YD_PRG.knt_yd_prg_h_select";
            try
            {
                var obList = new List<KNT_YD_PRG_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPRG_REF_NO", Value =pPRG_REF_NO},
                     new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value = pKNT_YD_PRG_H_ID},

                     new CommandParameter() {ParameterName = "pOption", Value = 3012},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YD_PRG_HModel ob = new KNT_YD_PRG_HModel();
                    ob.KNT_YD_PRG_H_ID = (dr["KNT_YD_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PRG_H_ID"]);
                    ob.PRG_REF_NO = (dr["PRG_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_REF_NO"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.MC_FAB_PROD_ORD_H_LST = (dr["MC_FAB_PROD_ORD_H_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_FAB_PROD_ORD_H_LST"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public RF_PAGERModel getYdYrnRecvList(long pageNumber, long pageSize, long? pSCM_SUPPLIER_ID, string pPRG_REF_NO, string pCHALAN_NO, Int64? pMC_BYR_ACC_ID,Int64? pMC_FAB_PROD_ORD_H_ID,string pIS_TRANSFER,Int64? pRF_ACTN_STATUS_ID)
        {
            string sp = "PKG_KNT_YD_PRG.knt_yd_prg_h_select";
            try
            {
                var obList = new List<KNT_YD_RCV_HModel>();
                Int64 vTotalRec = 0;
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pPRG_REF_NO", Value =pPRG_REF_NO},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = pCHALAN_NO},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     
                     new CommandParameter() {ParameterName = "pIS_TRANSFER", Value = pIS_TRANSFER},

                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = pRF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3013},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YD_RCV_HModel ob = new KNT_YD_RCV_HModel();

                    ob.KNT_YD_RCV_H_ID = (dr["KNT_YD_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_RCV_H_ID"]);
                    ob.KNT_YD_PRG_H_ID = (dr["KNT_YD_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PRG_H_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.CL_WO_REF_NO = (dr["CL_WO_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CL_WO_REF_NO"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.INVOICE_NO = (dr["INVOICE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INVOICE_NO"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    ob.PRG_REF_NO = (dr["PRG_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_REF_NO"]);

                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);

                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.KNT_SC_FACTORY = (dr["KNT_SC_FACTORY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_SC_FACTORY"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);

                    ob.IS_TRANSFER = (dr["IS_TRANSFER"] == DBNull.Value) ? "N": Convert.ToString(dr["IS_TRANSFER"]);

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    if (dr["KNT_SC_PRG_ISS_ID"] != DBNull.Value)
                    {
                        ob.KNT_SC_PRG_ISS_ID = Convert.ToInt64(dr["KNT_SC_PRG_ISS_ID"]);
                    }

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

        
    }
}