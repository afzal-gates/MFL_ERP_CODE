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
    public class GMT_SW_MCN_SPECModel
    {
        public Int64 GMT_SW_MCN_SPEC_ID { get; set; }
        public Int64 GMT_SW_MCN_TYP_ID { get; set; }
        public Int64? GMT_SM_BED_TYP_ID { get; set; }
        public Int64 GMT_STCH_TYP_ID { get; set; }
        public Int64 GMT_SW_APP_TYP_ID { get; set; }
        public string SW_MCN_SPEC { get; set; }
        public string SW_MCN_SPEC_SHORT { get; set; }
        public Int64 NDL_COUNT { get; set; }
        public Int64 STD_RPM { get; set; }
        public Int64 STD_SPI { get; set; }

        public string SW_MCN_TYP_NAME_EN { get; set; }
        public string SM_BED_TYP_NAME_EN { get; set; }
        public string STCH_TYP_NAME_EN { get; set; }
        public string SW_APP_TYP_NAME_EN { get; set; }
        public string THRD_CONS_DTL_XML { get; set; }
        public string MCN_GUIDE_DTL_XML { get; set; }
        public string MCN_PROFILE_DTL_XML { get; set; }




        public string Save()
        {
            const string sp = "pkg_gmt_swmcn.gmt_sw_mcn_spec_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_SW_MCN_SPEC_ID", Value = ob.GMT_SW_MCN_SPEC_ID},
                     new CommandParameter() {ParameterName = "pGMT_SW_MCN_TYP_ID", Value = ob.GMT_SW_MCN_TYP_ID},
                     new CommandParameter() {ParameterName = "pGMT_SM_BED_TYP_ID", Value = ob.GMT_SM_BED_TYP_ID},
                     new CommandParameter() {ParameterName = "pGMT_STCH_TYP_ID", Value = ob.GMT_STCH_TYP_ID},
                     new CommandParameter() {ParameterName = "pGMT_SW_APP_TYP_ID", Value = ob.GMT_SW_APP_TYP_ID},
                     new CommandParameter() {ParameterName = "pSW_MCN_SPEC", Value = ob.SW_MCN_SPEC},
                     new CommandParameter() {ParameterName = "pSW_MCN_SPEC_SHORT", Value = ob.SW_MCN_SPEC_SHORT},

                     new CommandParameter() {ParameterName = "pNDL_COUNT", Value = ob.NDL_COUNT},
                     new CommandParameter() {ParameterName = "pSTD_RPM", Value = ob.STD_RPM},
                     new CommandParameter() {ParameterName = "pSTD_SPI", Value = ob.STD_SPI},

                     new CommandParameter() {ParameterName = "pTHRD_CONS_DTL_XML", Value = ob.THRD_CONS_DTL_XML},
                     new CommandParameter() {ParameterName = "pMCN_GUIDE_DTL_XML", Value = ob.MCN_GUIDE_DTL_XML},
                     new CommandParameter() {ParameterName = "pMCN_PROFILE_DTL_XML", Value = ob.MCN_PROFILE_DTL_XML},
                     
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


        public object GetSwMcnSpecList(Int64 pageNumber, Int64 pageSize, string pSW_MCN_SPEC = null)
        {
            string sp = "pkg_gmt_swmcn.gmt_sw_mcn_spec_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<GMT_SW_MCN_SPECModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSW_MCN_SPEC", Value = pSW_MCN_SPEC},
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_SW_MCN_SPECModel ob = new GMT_SW_MCN_SPECModel();
                    ob.GMT_SW_MCN_SPEC_ID = (dr["GMT_SW_MCN_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SW_MCN_SPEC_ID"]);
                    ob.GMT_SW_MCN_TYP_ID = (dr["GMT_SW_MCN_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SW_MCN_TYP_ID"]);
                    if (dr["GMT_SM_BED_TYP_ID"] != DBNull.Value)
                        ob.GMT_SM_BED_TYP_ID = (dr["GMT_SM_BED_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SM_BED_TYP_ID"]);
                    ob.GMT_STCH_TYP_ID = (dr["GMT_STCH_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_STCH_TYP_ID"]);
                    ob.GMT_SW_APP_TYP_ID = (dr["GMT_SW_APP_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SW_APP_TYP_ID"]);
                    ob.SW_MCN_SPEC = (dr["SW_MCN_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SW_MCN_SPEC"]);
                    ob.SW_MCN_SPEC_SHORT = (dr["SW_MCN_SPEC_SHORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SW_MCN_SPEC_SHORT"]);
                    ob.NDL_COUNT = (dr["NDL_COUNT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NDL_COUNT"]);
                    ob.STD_RPM = (dr["STD_RPM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STD_RPM"]);
                    ob.STD_SPI = (dr["STD_SPI"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STD_SPI"]);

                    ob.SW_MCN_TYP_NAME_EN = (dr["SW_MCN_TYP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SW_MCN_TYP_NAME_EN"]);
                    ob.SM_BED_TYP_NAME_EN = (dr["SM_BED_TYP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SM_BED_TYP_NAME_EN"]);
                    ob.STCH_TYP_NAME_EN = (dr["STCH_TYP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STCH_TYP_NAME_EN"]);
                    ob.SW_APP_TYP_NAME_EN = (dr["SW_APP_TYP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SW_APP_TYP_NAME_EN"]);

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    obList.Add(ob);
                }

                obj.total = vTotalRec;
                obj.data = obList;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_SW_MCN_SPECModel GetSwMcnSpecById(Int64 pGMT_SW_MCN_SPEC_ID)
        {
            string sp = "pkg_gmt_swmcn.gmt_sw_mcn_spec_select";
            try
            {
                var ob = new GMT_SW_MCN_SPECModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_SW_MCN_SPEC_ID", Value = pGMT_SW_MCN_SPEC_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_SW_MCN_SPEC_ID = (dr["GMT_SW_MCN_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SW_MCN_SPEC_ID"]);
                    ob.GMT_SW_MCN_TYP_ID = (dr["GMT_SW_MCN_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SW_MCN_TYP_ID"]);
                    if (dr["GMT_SM_BED_TYP_ID"] != DBNull.Value)
                        ob.GMT_SM_BED_TYP_ID = (dr["GMT_SM_BED_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SM_BED_TYP_ID"]);
                    ob.GMT_STCH_TYP_ID = (dr["GMT_STCH_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_STCH_TYP_ID"]);
                    ob.GMT_SW_APP_TYP_ID = (dr["GMT_SW_APP_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SW_APP_TYP_ID"]);
                    ob.SW_MCN_SPEC = (dr["SW_MCN_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SW_MCN_SPEC"]);
                    ob.SW_MCN_SPEC_SHORT = (dr["SW_MCN_SPEC_SHORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SW_MCN_SPEC_SHORT"]);
                    ob.NDL_COUNT = (dr["NDL_COUNT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NDL_COUNT"]);
                    ob.STD_RPM = (dr["STD_RPM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STD_RPM"]);
                    ob.STD_SPI = (dr["STD_SPI"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STD_SPI"]);
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