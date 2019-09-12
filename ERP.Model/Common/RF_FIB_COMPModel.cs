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
    public class RF_FIB_COMPModel
    {
        public Int64? RF_FIB_COMP_ID { get; set; }
        public string FIB_COMP_CODE { get; set; }
        public Int64 RF_FIB_COMP_GRP_ID { get; set; }
        public string FIB_COMP_NAME { get; set; }
        public string IS_BLEND { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public string BLEND_DESC { get; set; }
        public string IS_ELA_MXD { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public String LK_FIB_TYPE_LST { get; set; }

        public string Save()
        {
            const string sp = "pkg_common.rf_fib_comp_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_GRP_ID", Value = ob.RF_FIB_COMP_GRP_ID},
                     new CommandParameter() {ParameterName = "pFIB_COMP_NAME", Value = ob.FIB_COMP_NAME},                     
                     new CommandParameter() {ParameterName = "pLK_FIB_TYPE_LST", Value = ob.LK_FIB_TYPE_LST},
                     new CommandParameter() {ParameterName = "pIS_ELA_MXD", Value = ob.IS_ELA_MXD},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "V_RF_FIB_COMP_ID", Value =null, Direction = ParameterDirection.Output}
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


        public List<RF_FIB_COMPModel> SelectAll(string pFIB_COMP_NAME, int pOption)
        {
            string sp = "pkg_common.rf_fib_comp_select";
            try
            {
                var obList = new List<RF_FIB_COMPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pFIB_COMP_NAME", Value =pFIB_COMP_NAME},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_FIB_COMPModel ob = new RF_FIB_COMPModel();
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.FIB_COMP_CODE = (dr["FIB_COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_CODE"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.IS_BLEND = (dr["IS_BLEND"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_BLEND"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_ELA_MXD = (dr["IS_ELA_MXD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ELA_MXD"]);
                    ob.BLEND_DESC = (dr["BLEND_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLEND_DESC"]);
                    ob.LK_FIB_TYPE_LST = (dr["LK_FIB_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FIB_TYPE_LST"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RF_FIB_COMP_GRP_ID = (dr["RF_FIB_COMP_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_GRP_ID"]);                   
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RF_FIB_COMPModel> getFibComposition4DD()
        {
            string sp = "pkg_common.rf_fib_comp_select";
            try
            {
                var obList = new List<RF_FIB_COMPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_FIB_COMPModel ob = new RF_FIB_COMPModel();
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.FIB_COMP_CODE = (dr["FIB_COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_CODE"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.IS_ELA_MXD = (dr["IS_ELA_MXD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ELA_MXD"]);
                    ob.IS_PC_CVC = (dr["IS_PC_CVC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IS_PC_CVC"]);
                    ob.RATE_YRN = (dr["RATE_YRN"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RATE_YRN"]);

                    ob.IS_COTTON = (dr["IS_COTTON"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IS_COTTON"]);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RF_FIB_COMPModel Select(long ID)
        {
            string sp = "pkg_common.rf_fib_comp_select";
            try
            {
                var ob = new RF_FIB_COMPModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.FIB_COMP_CODE = (dr["FIB_COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_CODE"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.IS_BLEND = (dr["IS_BLEND"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_BLEND"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_ELA_MXD = (dr["IS_ELA_MXD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ELA_MXD"]);
                    ob.BLEND_DESC = (dr["BLEND_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLEND_DESC"]);
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

        public int IS_PC_CVC { get; set; }
        public decimal RATE_YRN { get; set; }

        public int IS_COTTON { get; set; }
    }
}