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
    public class KNT_ROLL_QC_GRDModel
    {
        public Int64 KNT_ROLL_QC_GRD_ID { get; set; }
        public Decimal GRADE_PT_FROM { get; set; }
        public Decimal GRADE_PT_TO { get; set; }
        public Int64 LK_QC_GRD_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_KNT_ROLL_QC_GRD";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_ROLL_QC_GRD_ID", Value = ob.KNT_ROLL_QC_GRD_ID},
                     new CommandParameter() {ParameterName = "pGRADE_PT_FROM", Value = ob.GRADE_PT_FROM},
                     new CommandParameter() {ParameterName = "pGRADE_PT_TO", Value = ob.GRADE_PT_TO},
                     new CommandParameter() {ParameterName = "pLK_QC_GRD_ID", Value = ob.LK_QC_GRD_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
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

        public string Update()
        {
            const string sp = "SP_KNT_ROLL_QC_GRD";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_ROLL_QC_GRD_ID", Value = ob.KNT_ROLL_QC_GRD_ID},
                     new CommandParameter() {ParameterName = "pGRADE_PT_FROM", Value = ob.GRADE_PT_FROM},
                     new CommandParameter() {ParameterName = "pGRADE_PT_TO", Value = ob.GRADE_PT_TO},
                     new CommandParameter() {ParameterName = "pLK_QC_GRD_ID", Value = ob.LK_QC_GRD_ID},
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
            const string sp = "SP_KNT_ROLL_QC_GRD";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_ROLL_QC_GRD_ID", Value = ob.KNT_ROLL_QC_GRD_ID},
                     new CommandParameter() {ParameterName = "pGRADE_PT_FROM", Value = ob.GRADE_PT_FROM},
                     new CommandParameter() {ParameterName = "pGRADE_PT_TO", Value = ob.GRADE_PT_TO},
                     new CommandParameter() {ParameterName = "pLK_QC_GRD_ID", Value = ob.LK_QC_GRD_ID},
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

        public List<KNT_ROLL_QC_GRDModel> SelectAll()
        {
            string sp = "Select_KNT_ROLL_QC_GRD";
            try
            {
                var obList = new List<KNT_ROLL_QC_GRDModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_ROLL_QC_GRD_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_ROLL_QC_GRDModel ob = new KNT_ROLL_QC_GRDModel();
                    ob.KNT_ROLL_QC_GRD_ID = (dr["KNT_ROLL_QC_GRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_ROLL_QC_GRD_ID"]);
                    ob.GRADE_PT_FROM = (dr["GRADE_PT_FROM"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GRADE_PT_FROM"]);
                    ob.GRADE_PT_TO = (dr["GRADE_PT_TO"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GRADE_PT_TO"]);
                    ob.LK_QC_GRD_ID = (dr["LK_QC_GRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_QC_GRD_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_ROLL_QC_GRDModel Select(long ID)
        {
            string sp = "Select_KNT_ROLL_QC_GRD";
            try
            {
                var ob = new KNT_ROLL_QC_GRDModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_ROLL_QC_GRD_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_ROLL_QC_GRD_ID = (dr["KNT_ROLL_QC_GRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_ROLL_QC_GRD_ID"]);
                    ob.GRADE_PT_FROM = (dr["GRADE_PT_FROM"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GRADE_PT_FROM"]);
                    ob.GRADE_PT_TO = (dr["GRADE_PT_TO"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GRADE_PT_TO"]);
                    ob.LK_QC_GRD_ID = (dr["LK_QC_GRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_QC_GRD_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_ROLL_QC_GRDModel SelectByValue(Decimal? pGRADE_PT = null)
        {
            string sp = "pkg_knit_roll.knt_roll_qc_grd_select";
            try
            {
                var ob = new KNT_ROLL_QC_GRDModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGRADE_PT", Value =pGRADE_PT},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_ROLL_QC_GRD_ID = (dr["KNT_ROLL_QC_GRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_ROLL_QC_GRD_ID"]);
                    ob.GRADE_PT_FROM = (dr["GRADE_PT_FROM"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GRADE_PT_FROM"]);
                    ob.GRADE_PT_TO = (dr["GRADE_PT_TO"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GRADE_PT_TO"]);
                    ob.LK_QC_GRD_ID = (dr["LK_QC_GRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_QC_GRD_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.QC_GRD_NAME = (dr["QC_GRD_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_GRD_NAME"]);
                    
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string QC_GRD_NAME { get; set; }
    }
}