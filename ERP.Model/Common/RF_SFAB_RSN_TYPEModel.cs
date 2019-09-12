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
    public class RF_SFAB_RSN_TYPEModel
    {
        public Int64 RF_SFAB_RSN_TYPE_ID { get; set; }
        public string SFAB_RSN_TYPE_NAME { get; set; }
        public string IS_ACTIVE { get; set; }



        public string Save()
        {
            const string sp = "pkg_knit_common.rf_sfab_rsn_type_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_SFAB_RSN_TYPE_ID", Value = ob.RF_SFAB_RSN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pSFAB_RSN_TYPE_NAME", Value = ob.SFAB_RSN_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
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



        public List<RF_SFAB_RSN_TYPEModel> getSrtFabBkReasonTyp()
        {
            string sp = "pkg_knit_common.rf_sfab_rsn_type_select";
            try
            {
                var obList = new List<RF_SFAB_RSN_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_SFAB_RSN_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_SFAB_RSN_TYPEModel ob = new RF_SFAB_RSN_TYPEModel();
                    ob.RF_SFAB_RSN_TYPE_ID = (dr["RF_SFAB_RSN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SFAB_RSN_TYPE_ID"]);
                    ob.SFAB_RSN_TYPE_NAME = (dr["SFAB_RSN_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SFAB_RSN_TYPE_NAME"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                   
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RF_SFAB_RSN_TYPEModel Select(long ID)
        {
            string sp = "Select_RF_SFAB_RSN_TYPE";
            try
            {
                var ob = new RF_SFAB_RSN_TYPEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_SFAB_RSN_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.RF_SFAB_RSN_TYPE_ID = (dr["RF_SFAB_RSN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SFAB_RSN_TYPE_ID"]);
                    ob.SFAB_RSN_TYPE_NAME = (dr["SFAB_RSN_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SFAB_RSN_TYPE_NAME"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    

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