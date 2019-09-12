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
    public class GMT_SW_MCN_GUIDEModel
    {
        public Int64 GMT_SW_MCN_GUIDE_ID { get; set; }
        public Int64 GMT_SW_MCN_SPEC_ID { get; set; }
        public string MCN_GUIDE_DESC { get; set; }
        public Int64 GMT_SM_GUIDE_TYP_ID { get; set; }
        public string SM_GUIDE_TYP_NAME_EN { get; set; }



        public List<GMT_SW_MCN_GUIDEModel> GetMcnGuideList(Int64 pGMT_SW_MCN_SPEC_ID)
        {
            string sp = "pkg_gmt_swmcn.gmt_sw_mcn_spec_select";
            try
            {
                var obList = new List<GMT_SW_MCN_GUIDEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_SW_MCN_SPEC_ID", Value = pGMT_SW_MCN_SPEC_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_SW_MCN_GUIDEModel ob = new GMT_SW_MCN_GUIDEModel();
                    ob.GMT_SW_MCN_GUIDE_ID = (dr["GMT_SW_MCN_GUIDE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SW_MCN_GUIDE_ID"]);
                    ob.GMT_SW_MCN_SPEC_ID = (dr["GMT_SW_MCN_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SW_MCN_SPEC_ID"]);
                    ob.MCN_GUIDE_DESC = (dr["MCN_GUIDE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MCN_GUIDE_DESC"]);
                    ob.GMT_SM_GUIDE_TYP_ID = (dr["GMT_SM_GUIDE_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SM_GUIDE_TYP_ID"]);

                    ob.SM_GUIDE_TYP_NAME_EN = (dr["SM_GUIDE_TYP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SM_GUIDE_TYP_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_SW_MCN_GUIDEModel Select(long ID)
        {
            string sp = "pkg_gmt_swmcn.gmt_sw_mcn_spec_select";
            try
            {
                var ob = new GMT_SW_MCN_GUIDEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_SW_MCN_GUIDE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_SW_MCN_GUIDE_ID = (dr["GMT_SW_MCN_GUIDE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SW_MCN_GUIDE_ID"]);
                    ob.GMT_SW_MCN_SPEC_ID = (dr["GMT_SW_MCN_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SW_MCN_SPEC_ID"]);
                    ob.MCN_GUIDE_DESC = (dr["MCN_GUIDE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MCN_GUIDE_DESC"]);
                    ob.GMT_SM_GUIDE_TYP_ID = (dr["GMT_SM_GUIDE_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SM_GUIDE_TYP_ID"]);
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