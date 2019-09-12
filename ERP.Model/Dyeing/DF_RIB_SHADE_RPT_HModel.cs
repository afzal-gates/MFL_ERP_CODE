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
    public class DF_RIB_SHADE_RPT_HModel
    {
        public Int64 DF_RIB_SHADE_RPT_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 DYE_BT_CARD_H_ID { get; set; }
        public Int64 RP_SL_NO { get; set; }
        public DateTime RPT_DT { get; set; }
        public Int64 RPT_BY { get; set; }
        public Int64? LK_RP_RTN_TYPE_ID { get; set; }
        public Int64? RE_PROC_TYPE_ID { get; set; }
        public Decimal BATCH_RP_QTY { get; set; }
        public Int64? MAJ_DFCT_TYPE_ID { get; set; }
        public Int64 KNT_QC_STS_TYPE_ID { get; set; }
        public string REMARKS { get; set; }
        public string IS_FINALIZED { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string XML_RPT_D { get; set; }
        public string XML_PARAM { get; set; }


        public string Save()
        {
            const string sp = "PKG_DF_INSPECTION.DF_RIB_SHADE_RPT_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_RIB_SHADE_RPT_H_ID", Value = ob.DF_RIB_SHADE_RPT_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID==0?1:ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pRP_SL_NO", Value = ob.RP_SL_NO},                     
                     new CommandParameter() {ParameterName = "pRCV_DT_FRM_QC", Value = ob.RCV_DT_FRM_QC},
                     new CommandParameter() {ParameterName = "pSHD_APRV_DT", Value = ob.SHD_APRV_DT},
                     new CommandParameter() {ParameterName = "pRPT_BY", Value = ob.RPT_BY},
                     new CommandParameter() {ParameterName = "pLK_RP_RTN_TYPE_ID", Value = ob.LK_RP_RTN_TYPE_ID==0?null:ob.LK_RP_RTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRE_PROC_TYPE_ID", Value = ob.RE_PROC_TYPE_ID==0?null:ob.RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pBATCH_RP_QTY", Value = ob.BATCH_RP_QTY},
                     new CommandParameter() {ParameterName = "pMAJ_DFCT_TYPE_ID", Value = ob.MAJ_DFCT_TYPE_ID==0?null:ob.MAJ_DFCT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pKNT_QC_STS_TYPE_ID", Value = ob.KNT_QC_STS_TYPE_ID==0?1:ob.KNT_QC_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pXML_RPT_D", Value = ob.XML_RPT_D},
                     new CommandParameter() {ParameterName = "pXML_PARAM", Value = ob.XML_PARAM},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "opDF_RIB_SHADE_RPT_H_ID", Value =0, Direction = ParameterDirection.Output},
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

        public string Update()
        {
            const string sp = "PKG_DF_INSPECTION.DF_RIB_SHADE_RPT_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_RIB_SHADE_RPT_H_ID", Value = ob.DF_RIB_SHADE_RPT_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pRP_SL_NO", Value = ob.RP_SL_NO},
                     new CommandParameter() {ParameterName = "pRCV_DT_FRM_QC", Value = ob.RCV_DT_FRM_QC},
                     new CommandParameter() {ParameterName = "pSHD_APRV_DT", Value = ob.SHD_APRV_DT},
                     new CommandParameter() {ParameterName = "pRPT_BY", Value = ob.RPT_BY},
                     new CommandParameter() {ParameterName = "pLK_RP_RTN_TYPE_ID", Value = ob.LK_RP_RTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRE_PROC_TYPE_ID", Value = ob.RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pBATCH_RP_QTY", Value = ob.BATCH_RP_QTY},
                     new CommandParameter() {ParameterName = "pMAJ_DFCT_TYPE_ID", Value = ob.MAJ_DFCT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pKNT_QC_STS_TYPE_ID", Value = ob.KNT_QC_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
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

        public string Delete()
        {
            const string sp = "SP_DF_RIB_SHADE_RPT_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_RIB_SHADE_RPT_H_ID", Value = ob.DF_RIB_SHADE_RPT_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pRP_SL_NO", Value = ob.RP_SL_NO},
                     new CommandParameter() {ParameterName = "pRPT_DT", Value = ob.RPT_DT},
                     new CommandParameter() {ParameterName = "pRPT_BY", Value = ob.RPT_BY},
                     new CommandParameter() {ParameterName = "pLK_RP_RTN_TYPE_ID", Value = ob.LK_RP_RTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRE_PROC_TYPE_ID", Value = ob.RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pBATCH_RP_QTY", Value = ob.BATCH_RP_QTY},
                     new CommandParameter() {ParameterName = "pMAJ_DFCT_TYPE_ID", Value = ob.MAJ_DFCT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pKNT_QC_STS_TYPE_ID", Value = ob.KNT_QC_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
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

        public List<DF_RIB_SHADE_RPT_HModel> SelectAll(int pageNo, int pageSize, string pDYE_BATCH_NO = null, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pMC_COLOR_ID = null)
        {
            string sp = "PKG_DF_INSPECTION.DF_RIB_SHADE_RPT_H_SELECT";
            try
            {
                var obList = new List<DF_RIB_SHADE_RPT_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value =pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value =pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_RIB_SHADE_RPT_HModel ob = new DF_RIB_SHADE_RPT_HModel();
                    ob.DF_RIB_SHADE_RPT_H_ID = (dr["DF_RIB_SHADE_RPT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_RIB_SHADE_RPT_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.RP_SL_NO = (dr["RP_SL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RP_SL_NO"]);
                    ob.RCV_DT_FRM_QC = (dr["RCV_DT_FRM_QC"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT_FRM_QC"]);
                    ob.SHD_APRV_DT = (dr["SHD_APRV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHD_APRV_DT"]);
                    ob.RPT_BY = (dr["RPT_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RPT_BY"]);
                    ob.LK_RP_RTN_TYPE_ID = (dr["LK_RP_RTN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RP_RTN_TYPE_ID"]);
                    ob.RE_PROC_TYPE_ID = (dr["RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RE_PROC_TYPE_ID"]);
                    ob.BATCH_RP_QTY = (dr["BATCH_RP_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BATCH_RP_QTY"]);
                    ob.MAJ_DFCT_TYPE_ID = (dr["MAJ_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAJ_DFCT_TYPE_ID"]);
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_BATCH_QTY"]);

                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                    ob.GREY_QTY = (dr["GREY_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GREY_QTY"]);
                    ob.GREY_ROLL_QTY = (dr["GREY_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GREY_ROLL_QTY"]);
                    ob.FIN_QTY = (dr["FIN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FIN_QTY"]);
                    ob.FIN_ROLL_QTY = (dr["FIN_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FIN_ROLL_QTY"]);
                    ob.QC_STATUS = (dr["QC_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_STATUS"]);

                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DF_RIB_SHADE_RPT_HModel> SelectForRibRequisition(string pDYE_BATCH_NO = null)
        {
            string sp = "PKG_DF_INSPECTION.DF_RIB_SHADE_RPT_H_SELECT";
            try
            {
                var obList = new List<DF_RIB_SHADE_RPT_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value =pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_RIB_SHADE_RPT_HModel ob = new DF_RIB_SHADE_RPT_HModel();
                    ob.DF_RIB_SHADE_RPT_H_ID = (dr["DF_RIB_SHADE_RPT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_RIB_SHADE_RPT_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.RP_SL_NO = (dr["RP_SL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RP_SL_NO"]);
                    ob.RCV_DT_FRM_QC = (dr["RCV_DT_FRM_QC"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT_FRM_QC"]);
                    ob.SHD_APRV_DT = (dr["SHD_APRV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHD_APRV_DT"]);
                    ob.RPT_BY = (dr["RPT_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RPT_BY"]);
                    ob.LK_RP_RTN_TYPE_ID = (dr["LK_RP_RTN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RP_RTN_TYPE_ID"]);
                    ob.RE_PROC_TYPE_ID = (dr["RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RE_PROC_TYPE_ID"]);
                    ob.BATCH_RP_QTY = (dr["BATCH_RP_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BATCH_RP_QTY"]);
                    ob.MAJ_DFCT_TYPE_ID = (dr["MAJ_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAJ_DFCT_TYPE_ID"]);
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_BATCH_QTY"]);

                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DF_RIB_SHADE_RPT_HModel Select(Int64? pDF_RIB_SHADE_RPT_H_ID = null)
        {
            string sp = "PKG_DF_INSPECTION.DF_RIB_SHADE_RPT_H_SELECT";
            try
            {
                var ob = new DF_RIB_SHADE_RPT_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_RIB_SHADE_RPT_H_ID", Value =pDF_RIB_SHADE_RPT_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_RIB_SHADE_RPT_H_ID = (dr["DF_RIB_SHADE_RPT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_RIB_SHADE_RPT_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.RP_SL_NO = (dr["RP_SL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RP_SL_NO"]);
                    ob.RCV_DT_FRM_QC = (dr["RCV_DT_FRM_QC"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT_FRM_QC"]);
                    ob.SHD_APRV_DT = (dr["SHD_APRV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHD_APRV_DT"]);
                    ob.RPT_BY = (dr["RPT_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RPT_BY"]);
                    ob.LK_RP_RTN_TYPE_ID = (dr["LK_RP_RTN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RP_RTN_TYPE_ID"]);
                    ob.RE_PROC_TYPE_ID = (dr["RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RE_PROC_TYPE_ID"]);
                    ob.BATCH_RP_QTY = (dr["BATCH_RP_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BATCH_RP_QTY"]);
                    ob.MAJ_DFCT_TYPE_ID = (dr["MAJ_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAJ_DFCT_TYPE_ID"]);
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.RF_RESP_DEPT_ID = 3;
                    
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    //, , BATCH_REQ_DT, ACT_BATCH_QTY
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DYE_BATCH_NO { get; set; }

        public string DYE_LOT_NO { get; set; }

        public DateTime SHD_APRV_DT { get; set; }

        public DateTime RCV_DT_FRM_QC { get; set; }

        public int TOTAL_REC { get; set; }

        public string BYR_ACC_GRP_NAME_EN { get; set; }

        public string STYLE_NO { get; set; }

        public string MC_ORDER_NO_LST { get; set; }

        public decimal ACT_BATCH_QTY { get; set; }

        public int RF_RESP_DEPT_ID { get; set; }


        public string COLOR_NAME_EN { get; set; }

        public decimal GREY_QTY { get; set; }

        public long GREY_ROLL_QTY { get; set; }

        public decimal FIN_QTY { get; set; }

        public long FIN_ROLL_QTY { get; set; }

        public string QC_STATUS { get; set; }
    }
}