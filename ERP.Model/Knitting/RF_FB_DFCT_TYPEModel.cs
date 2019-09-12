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
    public class RF_FB_DFCT_TYPEModel
    {
        public Int64 RF_FB_DFCT_TYPE_ID { get; set; }
        public string FB_DFCT_TYPE_CODE { get; set; }
        public string FB_DFCT_TYPE_NAME { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public string IS_CALC_APLY { get; set; }
        public Int64 STD_PT { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 DFCT_QTY { get; set; }

        public string Save()
        {
            const string sp = "SP_RF_FB_DFCT_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FB_DFCT_TYPE_ID", Value = ob.RF_FB_DFCT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pFB_DFCT_TYPE_CODE", Value = ob.FB_DFCT_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pFB_DFCT_TYPE_NAME", Value = ob.FB_DFCT_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_CALC_APLY", Value = ob.IS_CALC_APLY},
                     new CommandParameter() {ParameterName = "pSTD_PT", Value = ob.STD_PT},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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
            const string sp = "SP_RF_FB_DFCT_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FB_DFCT_TYPE_ID", Value = ob.RF_FB_DFCT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pFB_DFCT_TYPE_CODE", Value = ob.FB_DFCT_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pFB_DFCT_TYPE_NAME", Value = ob.FB_DFCT_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_CALC_APLY", Value = ob.IS_CALC_APLY},
                     new CommandParameter() {ParameterName = "pSTD_PT", Value = ob.STD_PT},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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
            const string sp = "SP_RF_FB_DFCT_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FB_DFCT_TYPE_ID", Value = ob.RF_FB_DFCT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pFB_DFCT_TYPE_CODE", Value = ob.FB_DFCT_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pFB_DFCT_TYPE_NAME", Value = ob.FB_DFCT_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_CALC_APLY", Value = ob.IS_CALC_APLY},
                     new CommandParameter() {ParameterName = "pSTD_PT", Value = ob.STD_PT},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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

        public List<RF_FB_DFCT_TYPEModel> SelectAll()
        {
            string sp = "pkg_knit_common.rf_fb_dfct_type_select";
            try
            {
                var obList = new List<RF_FB_DFCT_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FB_DFCT_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_FB_DFCT_TYPEModel ob = new RF_FB_DFCT_TYPEModel();
                    ob.RF_FB_DFCT_TYPE_ID = (dr["RF_FB_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FB_DFCT_TYPE_ID"]);
                    ob.FB_DFCT_TYPE_CODE = (dr["FB_DFCT_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FB_DFCT_TYPE_CODE"]);
                    ob.FB_DFCT_TYPE_NAME = (dr["FB_DFCT_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FB_DFCT_TYPE_NAME"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.IS_CALC_APLY = (dr["IS_CALC_APLY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CALC_APLY"]);
                    ob.STD_PT = (dr["STD_PT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STD_PT"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.RF_FB_DFCT_GRP_ID = (dr["RF_FB_DFCT_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FB_DFCT_GRP_ID"]);
                    ob.RF_RESP_DEPT_ID = (dr["RF_RESP_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);
                    ob.ALT_DISP_ORDER = (dr["ALT_DISP_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALT_DISP_ORDER"]);
                    
                    ob.IS_MULTI_DEPT_ALLOW = (dr["IS_MULTI_DEPT_ALLOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MULTI_DEPT_ALLOW"]);
                    ob.MULTI_DEPT_LST = (dr["MULTI_DEPT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MULTI_DEPT_LST"]);
                    



                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RF_FB_DFCT_TYPEModel Select(long ID)
        {
            string sp = "Select_RF_FB_DFCT_TYPE";
            try
            {
                var ob = new RF_FB_DFCT_TYPEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FB_DFCT_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.RF_FB_DFCT_TYPE_ID = (dr["RF_FB_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FB_DFCT_TYPE_ID"]);
                    ob.FB_DFCT_TYPE_CODE = (dr["FB_DFCT_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FB_DFCT_TYPE_CODE"]);
                    ob.FB_DFCT_TYPE_NAME = (dr["FB_DFCT_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FB_DFCT_TYPE_NAME"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.IS_CALC_APLY = (dr["IS_CALC_APLY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CALC_APLY"]);
                    ob.STD_PT = (dr["STD_PT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STD_PT"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
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

        public long RF_FB_DFCT_GRP_ID { get; set; }

        public long RF_RESP_DEPT_ID { get; set; }

        public long ALT_DISP_ORDER { get; set; }

        public string IS_MULTI_DEPT_ALLOW { get; set; }

        public string MULTI_DEPT_LST { get; set; }
    }
}