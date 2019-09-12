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
    public class DF_SC_PT_ISS_D1_CLCFModel
    {
        public Int64 DF_SC_PT_ISS_D1_CLCF_ID { get; set; }
        public Int64 DF_SC_PT_ISS_H_ID { get; set; }
        public Int64 KNT_CLCF_STYL_ITEM_ID { get; set; }
        public Int64 RQD_QTY { get; set; }
        public Int64 ISS_ROLL_QTY { get; set; }
        public Int64 ISS_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DF_SC_PT_ISS_D1_CLCF";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D1_CLCF_ID", Value = ob.DF_SC_PT_ISS_D1_CLCF_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_H_ID", Value = ob.DF_SC_PT_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_CLCF_STYL_ITEM_ID", Value = ob.KNT_CLCF_STYL_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRQD_QTY", Value = ob.RQD_QTY},
                     new CommandParameter() {ParameterName = "pISS_ROLL_QTY", Value = ob.ISS_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pISS_QTY", Value = ob.ISS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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
            const string sp = "SP_DF_SC_PT_ISS_D1_CLCF";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D1_CLCF_ID", Value = ob.DF_SC_PT_ISS_D1_CLCF_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_H_ID", Value = ob.DF_SC_PT_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_CLCF_STYL_ITEM_ID", Value = ob.KNT_CLCF_STYL_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRQD_QTY", Value = ob.RQD_QTY},
                     new CommandParameter() {ParameterName = "pISS_ROLL_QTY", Value = ob.ISS_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pISS_QTY", Value = ob.ISS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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
            const string sp = "SP_DF_SC_PT_ISS_D1_CLCF";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D1_CLCF_ID", Value = ob.DF_SC_PT_ISS_D1_CLCF_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_H_ID", Value = ob.DF_SC_PT_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_CLCF_STYL_ITEM_ID", Value = ob.KNT_CLCF_STYL_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRQD_QTY", Value = ob.RQD_QTY},
                     new CommandParameter() {ParameterName = "pISS_ROLL_QTY", Value = ob.ISS_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pISS_QTY", Value = ob.ISS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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

        public List<DF_SC_PT_ISS_D1_CLCFModel> SelectByID(Int64? pDF_SC_PT_ISS_H_ID = null)
        {
            string sp = "PKG_DF_PT.DF_SC_PT_ISS_D1_CLCF_Select";
            try
            {
                var obList = new List<DF_SC_PT_ISS_D1_CLCFModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_H_ID", Value=pDF_SC_PT_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SC_PT_ISS_D1_CLCFModel ob = new DF_SC_PT_ISS_D1_CLCFModel();
                    ob.DF_SC_PT_ISS_D1_CLCF_ID = (dr["DF_SC_PT_ISS_D1_CLCF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D1_CLCF_ID"]);
                    ob.DF_SC_PT_ISS_H_ID = (dr["DF_SC_PT_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_H_ID"]);
                    ob.KNT_CLCF_STYL_ITEM_ID = (dr["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STYL_ITEM_ID"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                    ob.ISS_ROLL_QTY = (dr["ISS_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_ROLL_QTY"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
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

        public DF_SC_PT_ISS_D1_CLCFModel Select(long ID)
        {
            string sp = "Select_DF_SC_PT_ISS_D1_CLCF";
            try
            {
                var ob = new DF_SC_PT_ISS_D1_CLCFModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D1_CLCF_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_SC_PT_ISS_D1_CLCF_ID = (dr["DF_SC_PT_ISS_D1_CLCF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D1_CLCF_ID"]);
                    ob.DF_SC_PT_ISS_H_ID = (dr["DF_SC_PT_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_H_ID"]);
                    ob.KNT_CLCF_STYL_ITEM_ID = (dr["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STYL_ITEM_ID"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                    ob.ISS_ROLL_QTY = (dr["ISS_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_ROLL_QTY"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
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
    }
}