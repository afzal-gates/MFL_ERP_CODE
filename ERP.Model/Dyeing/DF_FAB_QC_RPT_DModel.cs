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
    public class DF_FAB_QC_RPT_DModel
    {
        public Int64 DF_FAB_QC_RPT_D_ID { get; set; }
        public Int64 DF_FAB_QC_RPT_ID { get; set; }
        public Int64 DF_BT_SUB_LOT_ID { get; set; }

        public string Save()
        {
            const string sp = "SP_DF_FAB_QC_RPT_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_D_ID", Value = ob.DF_FAB_QC_RPT_D_ID},
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_ID", Value = ob.DF_FAB_QC_RPT_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
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
            const string sp = "SP_DF_FAB_QC_RPT_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_D_ID", Value = ob.DF_FAB_QC_RPT_D_ID},
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_ID", Value = ob.DF_FAB_QC_RPT_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
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
            const string sp = "SP_DF_FAB_QC_RPT_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_D_ID", Value = ob.DF_FAB_QC_RPT_D_ID},
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_ID", Value = ob.DF_FAB_QC_RPT_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
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

        public List<DF_FAB_QC_RPT_DModel> SelectAll()
        {
            string sp = "PKG_DF_INSPECTION.DF_FAB_QC_RPT_D_Select";
            try
            {
                var obList = new List<DF_FAB_QC_RPT_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_FAB_QC_RPT_DModel ob = new DF_FAB_QC_RPT_DModel();
                    ob.DF_FAB_QC_RPT_D_ID = (dr["DF_FAB_QC_RPT_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FAB_QC_RPT_D_ID"]);
                    ob.DF_FAB_QC_RPT_ID = (dr["DF_FAB_QC_RPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FAB_QC_RPT_ID"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_FAB_QC_RPT_DModel Select(long ID)
        {
            string sp = "PKG_DF_INSPECTION.DF_FAB_QC_RPT_D_Select";
            try
            {
                var ob = new DF_FAB_QC_RPT_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_FAB_QC_RPT_D_ID = (dr["DF_FAB_QC_RPT_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FAB_QC_RPT_D_ID"]);
                    ob.DF_FAB_QC_RPT_ID = (dr["DF_FAB_QC_RPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FAB_QC_RPT_ID"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);

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