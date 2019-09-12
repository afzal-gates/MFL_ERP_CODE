using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class RF_ACTION_CFG_HModel
    {
        public Int64 RF_ACTION_CFG_H_ID { get; set; }
        public Int64 SC_MENU_ID { get; set; }
        public Int64 LK_ACTION_TYPE_ID { get; set; }
        public string ACTION_CFG_DESC { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 LK_STATUS_TBL_ID { get; set; }

        public string MENU_NAME_EN { get; set; }

        public string LK_DATA_NAME_EN { get; set; }

        public List<RF_ACTION_CFG_HModel> ActionTypeList()
        {
            string sp = "pkg_admin.rf_action_cfg_d1_select";
            try
            {
                var obList = new List<RF_ACTION_CFG_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_ACTION_CFG_HModel ob = new RF_ACTION_CFG_HModel();
                    ob.RF_ACTION_CFG_H_ID = (dr["RF_ACTION_CFG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTION_CFG_H_ID"]);
                    ob.SC_MENU_ID = (dr["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_MENU_ID"]);
                    ob.LK_ACTION_TYPE_ID = (dr["LK_ACTION_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ACTION_TYPE_ID"]);
                    ob.ACTION_CFG_DESC = (dr["ACTION_CFG_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTION_CFG_DESC"]);
                    ob.LK_DATA_NAME_EN = (dr["LK_DATA_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DATA_NAME_EN"]);
                    ob.MENU_NAME_EN = (dr["MENU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MENU_NAME_EN"]);
                    ob.LK_STATUS_TBL_ID = (dr["LK_STATUS_TBL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STATUS_TBL_ID"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}