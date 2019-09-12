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
    public class MC_BU_SMPL_CFGModel 
    {
        public Int64 MC_BU_SMPL_CFG_ID { get; set; }
        public Int64 MC_BUYER_ID { get; set; }
        public Int64 RF_SMPL_TYPE_ID { get; set; }
        public Int64 LK_SMPL_SRC_TYPE_ID { get; set; }
        public Int64 SMPL_ORDER { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }


        public List<MC_BU_SMPL_CFGModel> SelectAll()
        {
            string sp = "pkg_merchandising.mc_bu_smpl_cfg_select";
            try
            {
                var obList = new List<MC_BU_SMPL_CFGModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BU_SMPL_CFG_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_BU_SMPL_CFGModel ob = new MC_BU_SMPL_CFGModel();
                            ob.MC_BU_SMPL_CFG_ID = (dr["MC_BU_SMPL_CFG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BU_SMPL_CFG_ID"]);
                            ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                            ob.RF_SMPL_TYPE_ID = (dr["RF_SMPL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SMPL_TYPE_ID"]);
                            ob.LK_SMPL_SRC_TYPE_ID = (dr["LK_SMPL_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SMPL_SRC_TYPE_ID"]);
                            ob.SMPL_ORDER = (dr["SMPL_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SMPL_ORDER"]);
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

        public MC_BU_SMPL_CFGModel Select(long ID)
        {
            string sp = "Select_MC_BU_SMPL_CFG";
            try
            {
                var ob = new MC_BU_SMPL_CFGModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BU_SMPL_CFG_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_BU_SMPL_CFG_ID = (dr["MC_BU_SMPL_CFG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BU_SMPL_CFG_ID"]);
                        ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                        ob.RF_SMPL_TYPE_ID = (dr["RF_SMPL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SMPL_TYPE_ID"]);
                        ob.LK_SMPL_SRC_TYPE_ID = (dr["LK_SMPL_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SMPL_SRC_TYPE_ID"]);
                        ob.SMPL_ORDER = (dr["SMPL_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SMPL_ORDER"]);
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