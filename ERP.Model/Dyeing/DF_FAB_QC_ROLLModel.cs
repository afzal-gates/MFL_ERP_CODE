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
    public class DF_FAB_QC_ROLLModel
    {
        public Int64 DF_FAB_QC_ROLL_ID { get; set; }
        public Int64 DF_FAB_QC_RPT_ID { get; set; }
        public Int64 KNT_FAB_ROLL_ID { get; set; }
        public Decimal RQD_FIN_DIA { get; set; }
        public Decimal ACT_FIN_DIA { get; set; }
        public Decimal ACT_ROLL_WT { get; set; }
        public Decimal TOTAL_PT { get; set; }
        public Decimal GRADE_PT { get; set; }
        public Int64 LK_QC_GRD_ID { get; set; }
        public string ACT_FIN_GSM { get; set; }
        public string SHADE_GRP { get; set; }
        public Int64 KNT_QC_STS_TYPE_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 DF_BT_SUB_LOT_ID { get; set; }
        
        public string Save()
        {
            const string sp = "SP_DF_FAB_QC_ROLL";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_ROLL_ID", Value = ob.DF_FAB_QC_ROLL_ID},
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_ID", Value = ob.DF_FAB_QC_RPT_ID},
                     new CommandParameter() {ParameterName = "pKNT_FAB_ROLL_ID", Value = ob.KNT_FAB_ROLL_ID},
                     new CommandParameter() {ParameterName = "pRQD_FIN_DIA", Value = ob.RQD_FIN_DIA},
                     new CommandParameter() {ParameterName = "pACT_FIN_DIA", Value = ob.ACT_FIN_DIA},
                     new CommandParameter() {ParameterName = "pACT_ROLL_WT", Value = ob.ACT_ROLL_WT},
                     new CommandParameter() {ParameterName = "pTOTAL_PT", Value = ob.TOTAL_PT},
                     new CommandParameter() {ParameterName = "pGRADE_PT", Value = ob.GRADE_PT},
                     new CommandParameter() {ParameterName = "pLK_QC_GRD_ID", Value = ob.LK_QC_GRD_ID},
                     new CommandParameter() {ParameterName = "pACT_FIN_GSM", Value = ob.ACT_FIN_GSM},
                     new CommandParameter() {ParameterName = "pSHADE_GRP", Value = ob.SHADE_GRP},
                     new CommandParameter() {ParameterName = "pKNT_QC_STS_TYPE_ID", Value = ob.KNT_QC_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public string Update()
        {
            const string sp = "SP_DF_FAB_QC_ROLL";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_ROLL_ID", Value = ob.DF_FAB_QC_ROLL_ID},
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_ID", Value = ob.DF_FAB_QC_RPT_ID},
                     new CommandParameter() {ParameterName = "pKNT_FAB_ROLL_ID", Value = ob.KNT_FAB_ROLL_ID},
                     new CommandParameter() {ParameterName = "pRQD_FIN_DIA", Value = ob.RQD_FIN_DIA},
                     new CommandParameter() {ParameterName = "pACT_FIN_DIA", Value = ob.ACT_FIN_DIA},
                     new CommandParameter() {ParameterName = "pACT_ROLL_WT", Value = ob.ACT_ROLL_WT},
                     new CommandParameter() {ParameterName = "pTOTAL_PT", Value = ob.TOTAL_PT},
                     new CommandParameter() {ParameterName = "pGRADE_PT", Value = ob.GRADE_PT},
                     new CommandParameter() {ParameterName = "pLK_QC_GRD_ID", Value = ob.LK_QC_GRD_ID},
                     new CommandParameter() {ParameterName = "pACT_FIN_GSM", Value = ob.ACT_FIN_GSM},
                     new CommandParameter() {ParameterName = "pSHADE_GRP", Value = ob.SHADE_GRP},
                     new CommandParameter() {ParameterName = "pKNT_QC_STS_TYPE_ID", Value = ob.KNT_QC_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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
            const string sp = "SP_DF_FAB_QC_ROLL";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_ROLL_ID", Value = ob.DF_FAB_QC_ROLL_ID},
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_ID", Value = ob.DF_FAB_QC_RPT_ID},
                     new CommandParameter() {ParameterName = "pKNT_FAB_ROLL_ID", Value = ob.KNT_FAB_ROLL_ID},
                     new CommandParameter() {ParameterName = "pRQD_FIN_DIA", Value = ob.RQD_FIN_DIA},
                     new CommandParameter() {ParameterName = "pACT_FIN_DIA", Value = ob.ACT_FIN_DIA},
                     new CommandParameter() {ParameterName = "pACT_ROLL_WT", Value = ob.ACT_ROLL_WT},
                     new CommandParameter() {ParameterName = "pTOTAL_PT", Value = ob.TOTAL_PT},
                     new CommandParameter() {ParameterName = "pGRADE_PT", Value = ob.GRADE_PT},
                     new CommandParameter() {ParameterName = "pLK_QC_GRD_ID", Value = ob.LK_QC_GRD_ID},
                     new CommandParameter() {ParameterName = "pACT_FIN_GSM", Value = ob.ACT_FIN_GSM},
                     new CommandParameter() {ParameterName = "pSHADE_GRP", Value = ob.SHADE_GRP},
                     new CommandParameter() {ParameterName = "pKNT_QC_STS_TYPE_ID", Value = ob.KNT_QC_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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

        public List<DF_FAB_QC_ROLLModel> SelectAll(Int64? pDF_FAB_QC_RPT_ID = null)
        {
            string sp = "PKG_DF_INSPECTION.DF_FAB_QC_ROLL_Select";
            try
            {
                var obList = new List<DF_FAB_QC_ROLLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_ID", Value =pDF_FAB_QC_RPT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_FAB_QC_ROLLModel ob = new DF_FAB_QC_ROLLModel();
                    ob.DF_FAB_QC_ROLL_ID = (dr["DF_FAB_QC_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FAB_QC_ROLL_ID"]);
                    ob.DF_FAB_QC_RPT_ID = (dr["DF_FAB_QC_RPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FAB_QC_RPT_ID"]);
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);
                    ob.DF_ROLL_SL = (dr["DF_ROLL_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_ROLL_SL"]);
                    ob.DF_FAB_GRP_ID = (dr["DF_FAB_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FAB_GRP_ID"]);

                    ob.RQD_FIN_DIA = (dr["RQD_FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_FIN_DIA"]);

                    ob.ACT_FIN_DIA = (dr["ACT_FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_FIN_DIA"]);
                    ob.ACT_ROLL_WT = (dr["ACT_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_ROLL_WT"]);
                    ob.TOTAL_PT = (dr["TOTAL_PT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOTAL_PT"]);
                    ob.GRADE_PT = (dr["GRADE_PT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GRADE_PT"]);
                    ob.LK_QC_GRD_ID = (dr["LK_QC_GRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_QC_GRD_ID"]);
                    ob.ACT_FIN_GSM = (dr["ACT_FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_GSM"]);
                    ob.SHADE_GRP = (dr["SHADE_GRP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHADE_GRP"]);
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.FAB_ROLL_NO = (dr["FAB_ROLL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ROLL_NO"]);
                    ob.FAB_ROLL_WT = (dr["FAB_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_ROLL_WT"]);

                    ob.QC_GRD_NAME = (dr["QC_GRD_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_GRD_NAME"]);


                    if ((dr["LK_RTN_TYP_ID"] != DBNull.Value))
                        ob.LK_RTN_TYP_ID = Convert.ToInt64(dr["LK_RTN_TYP_ID"]);
                    if ((dr["SUB_LOT_NO"] != DBNull.Value))
                        ob.SUB_LOT_NO = Convert.ToString(dr["SUB_LOT_NO"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DF_FAB_QC_ROLLModel> SelectAllLot(Int64? pDF_FAB_QC_RPT_ID = null)
        {
            string sp = "PKG_DF_INSPECTION.DF_FAB_QC_ROLL_Select";
            try
            {
                var obList = new List<DF_FAB_QC_ROLLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_ID", Value =pDF_FAB_QC_RPT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_FAB_QC_ROLLModel ob = new DF_FAB_QC_ROLLModel();
                    ob.DF_FAB_QC_RPT_ID = (dr["DF_FAB_QC_RPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FAB_QC_RPT_ID"]);

                    if ((dr["DF_BT_SUB_LOT_ID"] != DBNull.Value))
                        ob.DF_BT_SUB_LOT_ID = Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    if ((dr["SUB_LOT_NO"] != DBNull.Value))
                        ob.SUB_LOT_NO = Convert.ToString(dr["SUB_LOT_NO"]);

                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.QC_STS_TYP_NAME = (dr["QC_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_STS_TYP_NAME"]);
                    ob.RTN_TYP_NAME = (dr["RTN_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RTN_TYP_NAME"]);
                    ob.SUB_BATCH_NO = (dr["SUB_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_BATCH_NO"]);
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_FAB_QC_ROLLModel Select(long ID)
        {
            string sp = "PKG_DF_INSPECTION.DF_FAB_QC_ROLL_Select";
            try
            {
                var ob = new DF_FAB_QC_ROLLModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_ROLL_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_FAB_QC_ROLL_ID = (dr["DF_FAB_QC_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FAB_QC_ROLL_ID"]);
                    ob.DF_FAB_QC_RPT_ID = (dr["DF_FAB_QC_RPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FAB_QC_RPT_ID"]);
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);
                    ob.RQD_FIN_DIA = (dr["RQD_FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_FIN_DIA"]);
                    ob.ACT_FIN_DIA = (dr["ACT_FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_FIN_DIA"]);
                    ob.ACT_ROLL_WT = (dr["ACT_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_ROLL_WT"]);
                    ob.TOTAL_PT = (dr["TOTAL_PT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOTAL_PT"]);
                    ob.GRADE_PT = (dr["GRADE_PT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GRADE_PT"]);
                    ob.LK_QC_GRD_ID = (dr["LK_QC_GRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_QC_GRD_ID"]);
                    ob.ACT_FIN_GSM = (dr["ACT_FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_GSM"]);
                    ob.SHADE_GRP = (dr["SHADE_GRP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHADE_GRP"]);
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.FAB_ROLL_NO = (dr["FAB_ROLL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ROLL_NO"]);
                    ob.FAB_ROLL_WT = (dr["FAB_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_ROLL_WT"]);

                    if ((dr["LK_RTN_TYP_ID"] != DBNull.Value))
                        ob.LK_RTN_TYP_ID = Convert.ToInt64(dr["LK_RTN_TYP_ID"]);
                    if ((dr["SUB_LOT_NO"] != DBNull.Value))
                        ob.SUB_LOT_NO = Convert.ToString(dr["SUB_LOT_NO"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long DF_ROLL_SL { get; set; }

        public string FAB_ROLL_NO { get; set; }

        public decimal FAB_ROLL_WT { get; set; }

        public Int64? DF_FAB_GRP_ID { get; set; }
        public Int64? LK_RTN_TYP_ID { get; set; }
        public string SUB_LOT_NO { get; set; }
        public string QC_GRD_NAME { get; set; }

        public string DYE_LOT_NO { get; set; }

        public string DYE_BATCH_NO { get; set; }

        public string QC_STS_TYP_NAME { get; set; }

        public string RTN_TYP_NAME { get; set; }

        public string SUB_BATCH_NO { get; set; }
    }
}