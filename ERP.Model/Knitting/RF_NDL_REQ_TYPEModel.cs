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
    public class RF_NDL_REQ_TYPEModel
    {
        public Int64 RF_NDL_REQ_TYPE_ID { get; set; }
        public string NDL_REQ_TYPE_CODE { get; set; }
        public string REQ_TYPE_NAME_EN { get; set; }
        public string REQ_TYPE_NAME_BN { get; set; }
        public string REQ_TYPE_SNAME { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public string IS_NDL_BRK { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_RF_NDL_REQ_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_NDL_REQ_TYPE_ID", Value = ob.RF_NDL_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pNDL_REQ_TYPE_CODE", Value = ob.NDL_REQ_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pREQ_TYPE_NAME_EN", Value = ob.REQ_TYPE_NAME_EN},
                     new CommandParameter() {ParameterName = "pREQ_TYPE_NAME_BN", Value = ob.REQ_TYPE_NAME_BN},
                     new CommandParameter() {ParameterName = "pREQ_TYPE_SNAME", Value = ob.REQ_TYPE_SNAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_NDL_BRK", Value = ob.IS_NDL_BRK},
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
            const string sp = "SP_RF_NDL_REQ_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_NDL_REQ_TYPE_ID", Value = ob.RF_NDL_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pNDL_REQ_TYPE_CODE", Value = ob.NDL_REQ_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pREQ_TYPE_NAME_EN", Value = ob.REQ_TYPE_NAME_EN},
                     new CommandParameter() {ParameterName = "pREQ_TYPE_NAME_BN", Value = ob.REQ_TYPE_NAME_BN},
                     new CommandParameter() {ParameterName = "pREQ_TYPE_SNAME", Value = ob.REQ_TYPE_SNAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_NDL_BRK", Value = ob.IS_NDL_BRK},
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
            const string sp = "SP_RF_NDL_REQ_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_NDL_REQ_TYPE_ID", Value = ob.RF_NDL_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pNDL_REQ_TYPE_CODE", Value = ob.NDL_REQ_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pREQ_TYPE_NAME_EN", Value = ob.REQ_TYPE_NAME_EN},
                     new CommandParameter() {ParameterName = "pREQ_TYPE_NAME_BN", Value = ob.REQ_TYPE_NAME_BN},
                     new CommandParameter() {ParameterName = "pREQ_TYPE_SNAME", Value = ob.REQ_TYPE_SNAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_NDL_BRK", Value = ob.IS_NDL_BRK},
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

        public List<RF_NDL_REQ_TYPEModel> SelectAll()
        {
            string sp = "PKG_KNT_MC_NDL.RF_NDL_REQ_TYPE_Select";
            try
            {
                var obList = new List<RF_NDL_REQ_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_NDL_REQ_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_NDL_REQ_TYPEModel ob = new RF_NDL_REQ_TYPEModel();
                    ob.RF_NDL_REQ_TYPE_ID = (dr["RF_NDL_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_NDL_REQ_TYPE_ID"]);
                    ob.NDL_REQ_TYPE_CODE = (dr["NDL_REQ_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NDL_REQ_TYPE_CODE"]);
                    ob.REQ_TYPE_NAME_EN = (dr["REQ_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME_EN"]);
                    ob.REQ_TYPE_NAME_BN = (dr["REQ_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME_BN"]);
                    ob.REQ_TYPE_SNAME = (dr["REQ_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_SNAME"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.IS_NDL_BRK = (dr["IS_NDL_BRK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_NDL_BRK"]);
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

        public RF_NDL_REQ_TYPEModel Select(long ID)
        {
            string sp = "Select_RF_NDL_REQ_TYPE";
            try
            {
                var ob = new RF_NDL_REQ_TYPEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_NDL_REQ_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.RF_NDL_REQ_TYPE_ID = (dr["RF_NDL_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_NDL_REQ_TYPE_ID"]);
                    ob.NDL_REQ_TYPE_CODE = (dr["NDL_REQ_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NDL_REQ_TYPE_CODE"]);
                    ob.REQ_TYPE_NAME_EN = (dr["REQ_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME_EN"]);
                    ob.REQ_TYPE_NAME_BN = (dr["REQ_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME_BN"]);
                    ob.REQ_TYPE_SNAME = (dr["REQ_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_SNAME"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.IS_NDL_BRK = (dr["IS_NDL_BRK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_NDL_BRK"]);
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
    }
}