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
    public class KNT_YDP_D_COLModel
    {
        public Int64 KNT_YDP_D_COL_ID { get; set; }
        public Int64 KNT_YD_PRG_H_ID { get; set; }
        public Int64 YRN_COLOR_ID { get; set; }
        public string IS_SWATCH { get; set; }
        public string PANTON_NO { get; set; }
        public string COL_REF_NO { get; set; }
        public string IS_YDIP_RQD { get; set; }
        public string REMARKS { get; set; }
        public DateTime APRVL_DT { get; set; }
        public string COMMENTS { get; set; }
        public Int64? LK_YD_APV_STS_ID { get; set; }
        public string COLOR_NAME_EN { get; set; }

        private List<KNT_YDP_D_YRNModel> _details = null;
        public List<KNT_YDP_D_YRNModel> details
        {
            get
            {
                if (_details == null)
                {
                    _details = new List<KNT_YDP_D_YRNModel>();
                }
                return _details;
            }
            set
            {
                _details = value;
            }
        }

        public long RQD_YD_QTY { get; set; }
        public long MC_FAB_PROD_ORD_H_ID { get; set; }
        public string ORDER_NO_LIST { get; set; }
        public string STYLE_NO { get; set; }

        public string Save()
        {
            const string sp = "SP_KNT_YDP_D_COL";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YDP_D_COL_ID", Value = ob.KNT_YDP_D_COL_ID},
                     new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value = ob.KNT_YD_PRG_H_ID},
                     new CommandParameter() {ParameterName = "pYRN_COLOR_ID", Value = ob.YRN_COLOR_ID},
                     new CommandParameter() {ParameterName = "pIS_SWATCH", Value = ob.IS_SWATCH},
                     new CommandParameter() {ParameterName = "pPANTON_NO", Value = ob.PANTON_NO},
                     new CommandParameter() {ParameterName = "pCOL_REF_NO", Value = ob.COL_REF_NO},
                     new CommandParameter() {ParameterName = "pIS_YDIP_RQD", Value = ob.IS_YDIP_RQD},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pAPRVL_DT", Value = ob.APRVL_DT},
                     new CommandParameter() {ParameterName = "pCOMMENTS", Value = ob.COMMENTS},
                     new CommandParameter() {ParameterName = "pLK_YD_APV_STS_ID", Value = ob.LK_YD_APV_STS_ID},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
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
        public List<KNT_YDP_D_COLModel> getColorSummaryList(string pMC_FAB_PROD_ORD_H_LST, Int64? pKNT_YD_PRG_H_ID, Int64? pPARENT_ID = null)
        {
            string sp = "PKG_KNT_YD_PRG.knt_yd_prg_h_select";
            try
            {
                var obList = new List<KNT_YDP_D_COLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_LST", Value = pMC_FAB_PROD_ORD_H_LST},
                     new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value = pKNT_YD_PRG_H_ID??pPARENT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =pPARENT_ID.Equals(null)?3004:3008},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YDP_D_COLModel ob = new KNT_YDP_D_COLModel();

                    if (pPARENT_ID.Equals(null))
                    {
                        ob.KNT_YDP_D_COL_ID = (dr["KNT_YDP_D_COL_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["KNT_YDP_D_COL_ID"]);
                    }
                    else
                    {
                        ob.KNT_YDP_D_COL_ID = -1;
                    }

                    ob.KNT_YD_PRG_H_ID = (dr["KNT_YD_PRG_H_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["KNT_YD_PRG_H_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.YRN_COLOR_ID = (dr["YRN_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YRN_COLOR_ID"]);

                    ob.COL_REF_NO = (dr["COL_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COL_REF_NO"]);

                    ob.LK_YD_SRC_TYP_ID = (dr["LK_YD_SRC_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YD_SRC_TYP_ID"]);
                    ob.COL_REF_NO = (dr["COL_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COL_REF_NO"]);
                    ob.IS_YDIP_RQD = (dr["IS_YDIP_RQD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_YDIP_RQD"]);

                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.APRVL_DT = (dr["APRVL_DT"] == DBNull.Value) ? DateTime.Now.Date : Convert.ToDateTime(dr["APRVL_DT"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                    ob.RQD_YD_QTY = (dr["RQD_YD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_YD_QTY"]);

                    if (dr["LK_YD_APV_STS_ID"] != DBNull.Value)
                    {
                        ob.LK_YD_APV_STS_ID = Convert.ToInt64(dr["LK_YD_APV_STS_ID"]);
                    }

                    ob.COMMENTS = (dr["COMMENTS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMMENTS"]);

                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);


                    if (dr["KNT_YDP_D_COL_ID"] != DBNull.Value)
                    {
                        ob.details = new KNT_YDP_D_YRNModel().queryData(Convert.ToInt64(dr["KNT_YDP_D_COL_ID"]));
                    }
                    else
                    {
                        ob.details = new KNT_YDP_D_YRNModel().queryData(ob.KNT_YDP_D_COL_ID);
                    }

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_YDP_D_COLModel Select(long ID)
        {
            string sp = "Select_KNT_YDP_D_COL";
            try
            {
                var ob = new KNT_YDP_D_COLModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YDP_D_COL_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_YDP_D_COL_ID = (dr["KNT_YDP_D_COL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YDP_D_COL_ID"]);
                    ob.KNT_YD_PRG_H_ID = (dr["KNT_YD_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PRG_H_ID"]);
                    ob.YRN_COLOR_ID = (dr["YRN_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YRN_COLOR_ID"]);
                    ob.IS_SWATCH = (dr["IS_SWATCH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SWATCH"]);
                    ob.PANTON_NO = (dr["PANTON_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PANTON_NO"]);
                    ob.COL_REF_NO = (dr["COL_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COL_REF_NO"]);
                    ob.IS_YDIP_RQD = (dr["IS_YDIP_RQD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_YDIP_RQD"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.APRVL_DT = (dr["APRVL_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["APRVL_DT"]);
                    ob.COMMENTS = (dr["COMMENTS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMMENTS"]);

                    if (dr["LK_YD_APV_STS_ID"] != DBNull.Value)
                    {
                        ob.LK_YD_APV_STS_ID = Convert.ToInt64(dr["LK_YD_APV_STS_ID"]);
                    }
                    ob.details = new KNT_YDP_D_YRNModel().queryData(ob.KNT_YDP_D_COL_ID);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long LK_YD_SRC_TYP_ID { get; set; }
    }
}