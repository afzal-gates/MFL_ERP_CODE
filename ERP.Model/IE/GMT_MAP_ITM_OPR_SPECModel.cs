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
    public class GMT_MAP_ITM_OPR_SPECModel
    {
        public Int64 GMT_MAP_ITM_OPR_SPEC_ID { get; set; }
        public Int64 MC_STYLE_D_ITEM_ID { get; set; }
        public Int64 GMT_PART_OPR_SPEC_ID { get; set; }
        public string GMT_MAP_ITM_OPR_XML { get; set; }
        public int? RF_FAB_CLASS_ID { get; set; }
        public int? LK_GARM_TYPE_ID { get; set; }
        public int? NO_OF_OPR { get; set; }
        public int? NO_OF_HLPR { get; set; }
        public decimal? SMV { get; set; }
        public decimal? PLAN_EFICNCY { get; set; }

        public string BatchSave()
        {
            const string sp = "pkg_planning_common.gmt_map_itm_opr_spec_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value = ob.MC_STYLE_D_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_CLASS_ID", Value = ob.RF_FAB_CLASS_ID},
                      new CommandParameter() {ParameterName = "pLK_GARM_TYPE_ID", Value = ob.LK_GARM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pNO_OF_OPR", Value = ob.NO_OF_OPR},
                     new CommandParameter() {ParameterName = "pNO_OF_HLPR", Value = ob.NO_OF_HLPR},
                     new CommandParameter() {ParameterName = "pSMV", Value = ob.SMV},
                     new CommandParameter() {ParameterName = "pPLAN_EFICNCY", Value = ob.PLAN_EFICNCY},
                     new CommandParameter() {ParameterName = "pGMT_MAP_ITM_OPR_XML", Value = ob.GMT_MAP_ITM_OPR_XML},

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

     

        public List<GMT_MAP_ITM_OPR_SPECModel> SelectAll()
        {
            string sp = "Select_GMT_MAP_ITM_OPR_SPEC";
            try
            {
                var obList = new List<GMT_MAP_ITM_OPR_SPECModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_MAP_ITM_OPR_SPEC_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_MAP_ITM_OPR_SPECModel ob = new GMT_MAP_ITM_OPR_SPECModel();
                    ob.GMT_MAP_ITM_OPR_SPEC_ID = (dr["GMT_MAP_ITM_OPR_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_MAP_ITM_OPR_SPEC_ID"]);
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.GMT_PART_OPR_SPEC_ID = (dr["GMT_PART_OPR_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PART_OPR_SPEC_ID"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_MAP_ITM_OPR_SPECModel Select(long ID)
        {
            string sp = "Select_GMT_MAP_ITM_OPR_SPEC";
            try
            {
                var ob = new GMT_MAP_ITM_OPR_SPECModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_MAP_ITM_OPR_SPEC_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_MAP_ITM_OPR_SPEC_ID = (dr["GMT_MAP_ITM_OPR_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_MAP_ITM_OPR_SPEC_ID"]);
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.GMT_PART_OPR_SPEC_ID = (dr["GMT_PART_OPR_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PART_OPR_SPEC_ID"]);
                    
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