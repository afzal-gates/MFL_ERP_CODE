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
    public class DF_PROC_QC_RPT_DFCTModel
    {
        public Int64 DF_PROC_QC_RPT_DFCT_ID { get; set; }
        public Int64 DF_PROC_QC_RPT_H_ID { get; set; }
        public Int64 RF_FB_DFCT_TYPE_ID { get; set; }
        public Int64 RF_RESP_DEPT_ID { get; set; }
        public Int64 RF_RESP_DEPT_ID_OLD { get; set; }
        public string LAST_UPDATED_BY { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }

        public string FB_DFCT_TYPE_NAME { get; set; }
        public string MULTI_DEPT_LST { get; set; }


        public string Save()
        {
            const string sp = "SP_DF_PROC_QC_RPT_DFCT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_PROC_QC_RPT_DFCT_ID", Value = ob.DF_PROC_QC_RPT_DFCT_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_QC_RPT_H_ID", Value = ob.DF_PROC_QC_RPT_H_ID},
                     new CommandParameter() {ParameterName = "pRF_FB_DFCT_TYPE_ID", Value = ob.RF_FB_DFCT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_RESP_DEPT_ID", Value = ob.RF_RESP_DEPT_ID},
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
            const string sp = "SP_DF_PROC_QC_RPT_DFCT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_PROC_QC_RPT_DFCT_ID", Value = ob.DF_PROC_QC_RPT_DFCT_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_QC_RPT_H_ID", Value = ob.DF_PROC_QC_RPT_H_ID},
                     new CommandParameter() {ParameterName = "pRF_FB_DFCT_TYPE_ID", Value = ob.RF_FB_DFCT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_RESP_DEPT_ID", Value = ob.RF_RESP_DEPT_ID},
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
            const string sp = "SP_DF_PROC_QC_RPT_DFCT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_PROC_QC_RPT_DFCT_ID", Value = ob.DF_PROC_QC_RPT_DFCT_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_QC_RPT_H_ID", Value = ob.DF_PROC_QC_RPT_H_ID},
                     new CommandParameter() {ParameterName = "pRF_FB_DFCT_TYPE_ID", Value = ob.RF_FB_DFCT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_RESP_DEPT_ID", Value = ob.RF_RESP_DEPT_ID},
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

        public List<DF_PROC_QC_RPT_DFCTModel> SelectAll()
        {
            string sp = "Select_DF_PROC_QC_RPT_DFCT";
            try
            {
                var obList = new List<DF_PROC_QC_RPT_DFCTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_PROC_QC_RPT_DFCT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_PROC_QC_RPT_DFCTModel ob = new DF_PROC_QC_RPT_DFCTModel();
                    ob.DF_PROC_QC_RPT_DFCT_ID = (dr["DF_PROC_QC_RPT_DFCT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_QC_RPT_DFCT_ID"]);
                    ob.DF_PROC_QC_RPT_H_ID = (dr["DF_PROC_QC_RPT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_QC_RPT_H_ID"]);
                    ob.RF_FB_DFCT_TYPE_ID = (dr["RF_FB_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FB_DFCT_TYPE_ID"]);
                    ob.RF_RESP_DEPT_ID = (dr["RF_RESP_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DF_PROC_QC_RPT_DFCTModel> SelectByID(Int64? pDF_PROC_QC_RPT_H_ID = null, Int64? pLK_FAB_INSP_TYPE_ID = null)
        {
            string sp = "PKG_DF_PRODUCTION.DF_PROC_QC_RPT_DFCT_Select";
            try
            {
                var obList = new List<DF_PROC_QC_RPT_DFCTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_PROC_QC_RPT_H_ID", Value =pDF_PROC_QC_RPT_H_ID},
                     new CommandParameter() {ParameterName = "pLK_FAB_INSP_TYPE_ID", Value =pLK_FAB_INSP_TYPE_ID},                     
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_PROC_QC_RPT_DFCTModel ob = new DF_PROC_QC_RPT_DFCTModel();
                    ob.DF_PROC_QC_RPT_DFCT_ID = (dr["DF_PROC_QC_RPT_DFCT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_QC_RPT_DFCT_ID"]);
                    ob.DF_PROC_QC_RPT_H_ID = (dr["DF_PROC_QC_RPT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_QC_RPT_H_ID"]);
                    ob.RF_FB_DFCT_TYPE_ID = (dr["RF_FB_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FB_DFCT_TYPE_ID"]);
                    ob.RF_RESP_DEPT_ID = (dr["RF_RESP_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);

                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.RF_RESP_DEPT_ID = (dr["RF_RESP_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);
                    ob.RF_RESP_DEPT_ID_OLD = ob.RF_RESP_DEPT_ID;

                    ob.MULTI_DEPT_LST = (dr["MULTI_DEPT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MULTI_DEPT_LST"]);

                    ob.resDeptList = new RF_RESP_DEPTModel().getRespDeptListByID(ob.MULTI_DEPT_LST.Length > 0 ? ob.MULTI_DEPT_LST : ob.RF_RESP_DEPT_ID.ToString());

                    ob.FB_DFCT_TYPE_NAME = (dr["FB_DFCT_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FB_DFCT_TYPE_NAME"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? "0" : Convert.ToString(dr["LAST_UPDATED_BY"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    //ob.FB_DFCT_GRP_NAME = (dr["FB_DFCT_GRP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FB_DFCT_GRP_NAME"]);
                    ob.RESP_DEPT_NAME = (dr["RESP_DEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RESP_DEPT_NAME"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_PROC_QC_RPT_DFCTModel Select(long ID)
        {
            string sp = "Select_DF_PROC_QC_RPT_DFCT";
            try
            {
                var ob = new DF_PROC_QC_RPT_DFCTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_PROC_QC_RPT_DFCT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_PROC_QC_RPT_DFCT_ID = (dr["DF_PROC_QC_RPT_DFCT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_QC_RPT_DFCT_ID"]);
                    ob.DF_PROC_QC_RPT_H_ID = (dr["DF_PROC_QC_RPT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_QC_RPT_H_ID"]);
                    ob.RF_FB_DFCT_TYPE_ID = (dr["RF_FB_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FB_DFCT_TYPE_ID"]);
                    ob.RF_RESP_DEPT_ID = (dr["RF_RESP_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RF_RESP_DEPTModel> resDeptList { get; set; }

        public string FB_DFCT_GRP_NAME { get; set; }

        public string RESP_DEPT_NAME { get; set; }
    }
}