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
    public class KNT_SRT_FAB_REQ_D1Model
    {
        public Int64 KNT_SRT_FAB_REQ_D1_ID { get; set; }
        public Int64 KNT_SRT_FAB_REQ_H_ID { get; set; }
        public Int64? FAB_COLOR_ID { get; set; }
        public Int64? GMT_COLOR_ID { get; set; }
        public Int64? MC_STYLE_D_FAB_ID { get; set; }
        public Int64? RF_FAB_TYPE_ID { get; set; }
        public Int64? RF_FIB_COMP_ID { get; set; }
        public string FIN_DIA { get; set; }
        public Int64? LK_DIA_TYPE_ID { get; set; }
        public Int64? DIA_MOU_ID { get; set; }
        public string FIN_GSM { get; set; }
        public Decimal? RQD_FAB_QTY { get; set; }
        public Int64? QTY_MOU_ID { get; set; }
        public string SP_INSTRUCTION { get; set; }
        public string DYE_LOT_NO { get; set; }
        public string IS_CONTRAST { get; set; }
        
        public string FAB_REQ_D1_XML { get; set; }
        public string FAB_REQ_D11_XML { get; set; }

        public string COLOR_NAME_EN { get; set; }
        public string GMT_COLOR_NAME_EN { get; set; }
        public string FABRIC_DESC { get; set; }
        public string FIN_DIA_N_DIA_TYPE { get; set; }
        public string RQD_FAB_QTY_NAME { get; set; }


        public string Save()
        {
            const string sp = "pkg_fab_prod_order.knt_srt_fab_prod_ord_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_H_ID", Value = ob.KNT_SRT_FAB_REQ_H_ID},
                     /*
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_D1_ID", Value = ob.KNT_SRT_FAB_REQ_D1_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = ob.FAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value = ob.MC_STYLE_D_FAB_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pFIN_DIA", Value = ob.FIN_DIA},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDIA_MOU_ID", Value = ob.DIA_MOU_ID},
                     new CommandParameter() {ParameterName = "pFIN_GSM", Value = ob.FIN_GSM},
                     new CommandParameter() {ParameterName = "pRQD_FAB_QTY", Value = ob.RQD_FAB_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pSP_INSTRUCTION", Value = ob.SP_INSTRUCTION},
                     */
                     new CommandParameter() {ParameterName = "pFAB_REQ_D1_XML", Value = ob.FAB_REQ_D1_XML},
                     new CommandParameter() {ParameterName = "pFAB_REQ_D11_XML", Value = ob.FAB_REQ_D11_XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
          
                     new CommandParameter() {ParameterName = "pOption", Value = 1001},
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



        public List<KNT_SRT_FAB_REQ_D1Model> GetSrtFabDtlByID(Int64 pKNT_SRT_FAB_REQ_H_ID)
        {
            string sp = "pkg_fab_prod_order.srt_fab_req_h_select";
            try
            {
                var obList = new List<KNT_SRT_FAB_REQ_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_H_ID", Value = pKNT_SRT_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SRT_FAB_REQ_D1Model ob = new KNT_SRT_FAB_REQ_D1Model();
                    ob.KNT_SRT_FAB_REQ_D1_ID = (dr["KNT_SRT_FAB_REQ_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SRT_FAB_REQ_D1_ID"]);
                    ob.KNT_SRT_FAB_REQ_H_ID = (dr["KNT_SRT_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SRT_FAB_REQ_H_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);

                    ob.IS_CONTRAST = (dr["IS_CONTRAST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CONTRAST"]);
                    if (dr["GMT_COLOR_ID"] != DBNull.Value)
                        ob.GMT_COLOR_ID = Convert.ToInt64(dr["GMT_COLOR_ID"]);

                    ob.GMT_COLOR_NAME_EN = (dr["GMT_COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_COLOR_NAME_EN"]);

                    
                    if (dr["FIN_DIA"] != DBNull.Value)
                        ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    if (dr["LK_DIA_TYPE_ID"] != DBNull.Value)
                        ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    if (dr["DIA_MOU_ID"] != DBNull.Value)
                        ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);

                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.RQD_FAB_QTY = (dr["RQD_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_FAB_QTY"]);

                    if (dr["QTY_MOU_ID"] != DBNull.Value)
                        ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    
                    ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.FIN_DIA_N_DIA_TYPE = (dr["FIN_DIA_N_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_N_DIA_TYPE"]);
                    ob.RQD_FAB_QTY_NAME = (dr["RQD_FAB_QTY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_FAB_QTY_NAME"]);
                                        
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_SRT_FAB_REQ_D1Model Select(long ID)
        {
            string sp = "Select_KNT_SRT_FAB_REQ_D1";
            try
            {
                var ob = new KNT_SRT_FAB_REQ_D1Model();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_D1_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_SRT_FAB_REQ_D1_ID = (dr["KNT_SRT_FAB_REQ_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SRT_FAB_REQ_D1_ID"]);
                    ob.KNT_SRT_FAB_REQ_H_ID = (dr["KNT_SRT_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SRT_FAB_REQ_H_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.RQD_FAB_QTY = (dr["RQD_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_FAB_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}