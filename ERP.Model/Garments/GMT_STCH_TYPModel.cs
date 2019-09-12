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
    public class GMT_STCH_TYPModel
    {
        public Int64 GMT_STCH_TYP_ID { get; set; }
        public string STCH_TYP_CODE { get; set; }
        public string STCH_TYP_NAME_EN { get; set; }
        public string STCH_TYP_NAME_BN { get; set; }
        public string STCH_TYP_SNAME { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public string IS_ACTIVE { get; set; }


        public string Save()
        {
            const string sp = "SP_GMT_STCH_TYP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_STCH_TYP_ID", Value = ob.GMT_STCH_TYP_ID},
                     new CommandParameter() {ParameterName = "pSTCH_TYP_CODE", Value = ob.STCH_TYP_CODE},
                     new CommandParameter() {ParameterName = "pSTCH_TYP_NAME_EN", Value = ob.STCH_TYP_NAME_EN},
                     new CommandParameter() {ParameterName = "pSTCH_TYP_NAME_BN", Value = ob.STCH_TYP_NAME_BN},
                     new CommandParameter() {ParameterName = "pSTCH_TYP_SNAME", Value = ob.STCH_TYP_SNAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
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


        public List<GMT_STCH_TYPModel> GetGmtStitchType()
        {
            string sp = "pkg_gmt_swmcn.gmt_stch_typ_select";
            try
            {
                var obList = new List<GMT_STCH_TYPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_STCH_TYPModel ob = new GMT_STCH_TYPModel();
                    ob.GMT_STCH_TYP_ID = (dr["GMT_STCH_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_STCH_TYP_ID"]);
                    ob.STCH_TYP_CODE = (dr["STCH_TYP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STCH_TYP_CODE"]);
                    ob.STCH_TYP_NAME_EN = (dr["STCH_TYP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STCH_TYP_NAME_EN"]);
                    ob.STCH_TYP_NAME_BN = (dr["STCH_TYP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STCH_TYP_NAME_BN"]);
                    ob.STCH_TYP_SNAME = (dr["STCH_TYP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STCH_TYP_SNAME"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
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

        public GMT_STCH_TYPModel Select(long ID)
        {
            string sp = "pkg_gmt_swmcn.gmt_stch_typ_select";
            try
            {
                var ob = new GMT_STCH_TYPModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_STCH_TYP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_STCH_TYP_ID = (dr["GMT_STCH_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_STCH_TYP_ID"]);
                    ob.STCH_TYP_CODE = (dr["STCH_TYP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STCH_TYP_CODE"]);
                    ob.STCH_TYP_NAME_EN = (dr["STCH_TYP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STCH_TYP_NAME_EN"]);
                    ob.STCH_TYP_NAME_BN = (dr["STCH_TYP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STCH_TYP_NAME_BN"]);
                    ob.STCH_TYP_SNAME = (dr["STCH_TYP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STCH_TYP_SNAME"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
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