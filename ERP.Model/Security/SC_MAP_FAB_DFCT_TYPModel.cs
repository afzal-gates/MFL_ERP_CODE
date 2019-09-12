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
    public class SC_MAP_FAB_DFCT_TYPModel
    {
        public Int64 SC_MAP_FAB_DFCT_TYP_ID { get; set; }
        public Int64 LK_FAB_INSP_TYPE_ID { get; set; }
        public Int64 RF_FB_DFCT_TYPE_ID { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string XML_MAP_D { get; set; }


        public string Save()
        {
            const string sp = "pkg_common.sc_map_fab_dfct_typ_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_MAP_FAB_DFCT_TYP_ID", Value = ob.SC_MAP_FAB_DFCT_TYP_ID},
                     new CommandParameter() {ParameterName = "pLK_FAB_INSP_TYPE_ID", Value = ob.LK_FAB_INSP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FB_DFCT_TYPE_ID", Value = ob.RF_FB_DFCT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pXML_MAP_D", Value = ob.XML_MAP_D},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
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

        public string Update()
        {
            const string sp = "SP_SC_MAP_FAB_DFCT_TYP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_MAP_FAB_DFCT_TYP_ID", Value = ob.SC_MAP_FAB_DFCT_TYP_ID},
                     new CommandParameter() {ParameterName = "pLK_FAB_INSP_TYPE_ID", Value = ob.LK_FAB_INSP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FB_DFCT_TYPE_ID", Value = ob.RF_FB_DFCT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
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
            const string sp = "SP_SC_MAP_FAB_DFCT_TYP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_MAP_FAB_DFCT_TYP_ID", Value = ob.SC_MAP_FAB_DFCT_TYP_ID},
                     new CommandParameter() {ParameterName = "pLK_FAB_INSP_TYPE_ID", Value = ob.LK_FAB_INSP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FB_DFCT_TYPE_ID", Value = ob.RF_FB_DFCT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
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

        public List<SC_MAP_FAB_DFCT_TYPModel> SelectAll()
        {
            string sp = "Select_SC_MAP_FAB_DFCT_TYP";
            try
            {
                var obList = new List<SC_MAP_FAB_DFCT_TYPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_MAP_FAB_DFCT_TYP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SC_MAP_FAB_DFCT_TYPModel ob = new SC_MAP_FAB_DFCT_TYPModel();
                    ob.SC_MAP_FAB_DFCT_TYP_ID = (dr["SC_MAP_FAB_DFCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_MAP_FAB_DFCT_TYP_ID"]);
                    ob.LK_FAB_INSP_TYPE_ID = (dr["LK_FAB_INSP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FAB_INSP_TYPE_ID"]);
                    ob.RF_FB_DFCT_TYPE_ID = (dr["RF_FB_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FB_DFCT_TYPE_ID"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
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

        public List<SC_MAP_FAB_DFCT_TYPModel> SelectByID(Int64? pLK_FAB_INSP_TYPE_ID = null, Int64? pOption = null)
        {
            string sp = "pkg_common.SC_MAP_FAB_DFCT_TYP_Select";
            try
            {
                var obList = new List<SC_MAP_FAB_DFCT_TYPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pLK_FAB_INSP_TYPE_ID", Value =pLK_FAB_INSP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption==null?3001:pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SC_MAP_FAB_DFCT_TYPModel ob = new SC_MAP_FAB_DFCT_TYPModel();
                    ob.SC_MAP_FAB_DFCT_TYP_ID = (dr["SC_MAP_FAB_DFCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_MAP_FAB_DFCT_TYP_ID"]);
                    ob.LK_FAB_INSP_TYPE_ID = (dr["LK_FAB_INSP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FAB_INSP_TYPE_ID"]);
                    ob.RF_FB_DFCT_TYPE_ID = (dr["RF_FB_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FB_DFCT_TYPE_ID"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.RF_RESP_DEPT_ID = (dr["RF_RESP_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);
                    ob.RF_RESP_DEPT_ID_OLD = ob.RF_RESP_DEPT_ID;

                    ob.IS_MULTI_DEPT_ALLOW = (dr["IS_MULTI_DEPT_ALLOW"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MULTI_DEPT_ALLOW"]);
                    ob.MULTI_DEPT_LST = (dr["MULTI_DEPT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MULTI_DEPT_LST"]);

                    ob.IS_CALC_APLY = (dr["IS_CALC_APLY"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_CALC_APLY"]);
                    ob.STD_PT = (dr["STD_PT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STD_PT"]);


                    ob.FB_DFCT_TYPE_CODE = (dr["FB_DFCT_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FB_DFCT_TYPE_CODE"]);
                    ob.FB_DFCT_TYPE_NAME = (dr["FB_DFCT_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FB_DFCT_TYPE_NAME"]);
                    ob.FB_DFCT_GRP_NAME = (dr["FB_DFCT_GRP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FB_DFCT_GRP_NAME"]);
                    ob.RESP_DEPT_NAME = (dr["RESP_DEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RESP_DEPT_NAME"]);

                    ob.resDeptList = new RF_RESP_DEPTModel().getRespDeptListByID(ob.MULTI_DEPT_LST.Length > 0 ? ob.MULTI_DEPT_LST : ob.RF_RESP_DEPT_ID.ToString());

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SC_MAP_FAB_DFCT_TYPModel Select(long ID)
        {
            string sp = "Select_SC_MAP_FAB_DFCT_TYP";
            try
            {
                var ob = new SC_MAP_FAB_DFCT_TYPModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_MAP_FAB_DFCT_TYP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SC_MAP_FAB_DFCT_TYP_ID = (dr["SC_MAP_FAB_DFCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_MAP_FAB_DFCT_TYP_ID"]);
                    ob.LK_FAB_INSP_TYPE_ID = (dr["LK_FAB_INSP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FAB_INSP_TYPE_ID"]);
                    ob.RF_FB_DFCT_TYPE_ID = (dr["RF_FB_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FB_DFCT_TYPE_ID"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
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

        public string FB_DFCT_TYPE_CODE { get; set; }

        public string FB_DFCT_TYPE_NAME { get; set; }

        public string FB_DFCT_GRP_NAME { get; set; }

        public string RESP_DEPT_NAME { get; set; }

        public string IS_MULTI_DEPT_ALLOW { get; set; }

        public string MULTI_DEPT_LST { get; set; }

        public string IS_CALC_APLY { get; set; }

        public decimal STD_PT { get; set; }

        public long RF_RESP_DEPT_ID { get; set; }

        public long RF_RESP_DEPT_ID_OLD { get; set; }

        public List<RF_RESP_DEPTModel> resDeptList { get; set; }
    }
}