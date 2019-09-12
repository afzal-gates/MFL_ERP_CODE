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
    public class MC_STYL_BGT_HModel
    {
        public Int64 MC_STYL_BGT_H_ID { get; set; }
        public Int64 MC_BLK_FAB_REQ_H_ID { get; set; }
        public Int64? REVISION_NO { get; set; }
        public DateTime? REVISION_DT { get; set; }
        public String REV_REASON { get; set; }
        public Int64 TOT_ORD_QTY { get; set; }
        public Decimal TOT_ORD_VALUE { get; set; }
        public String BYR_ACC_NAME_EN { get; set; }
        public String STYLE_NO { get; set; }
        public String STYLE_DESC { get; set; }
        public String CURR_NAME_EN { get; set; }
        public Decimal CURR_CONV_LOC { get; set; }
        public String IS_BGT_FINALIZED { get; set; }
        public String IS_BFB_FINALIZED { get; set; }
        public Int64? MC_TNA_TASK_STATUS_ID { get; set; }
        public String ORDER_NO_LIST { get; set; }
        public String ITEM_NAME_LIST { get; set; }
        public String ORD_MOU_CODE { get; set; }
        public Int64? BK_REVISION_NO { get; set; }
        private List<Int64> _REV_DATA = null;
        public List<Int64> REV_DATA
        {
            get
            {
                if (_REV_DATA == null)
                {
                    _REV_DATA = new List<Int64>();
                }
                return _REV_DATA;
            }
            set
            {
                _REV_DATA = value;
            }
        }
        public MC_STYL_BGT_HModel Select(long ID)
        {
            string sp = "pkg_budget_sheet.mc_styl_bgt_h_select";
            try
            {
                var ob = new MC_STYL_BGT_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_STYL_BGT_H_ID = (dr["MC_STYL_BGT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_BGT_H_ID"]);
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);

                    if (dr["REVISION_NO"] != DBNull.Value)
                    {
                        ob.REVISION_NO = Convert.ToInt64(dr["REVISION_NO"]);
                    }

                    if (dr["BK_REVISION_NO"] != DBNull.Value)
                    {
                        ob.BK_REVISION_NO = Convert.ToInt64(dr["BK_REVISION_NO"]);
                    }

                    if (dr["REVISION_DT"] != DBNull.Value)
                    {
                        ob.REVISION_DT = Convert.ToDateTime(dr["REVISION_DT"]);
                    }

                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);

                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.CURR_NAME_EN = (dr["CURR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_NAME_EN"]);

                    ob.IS_BGT_FINALIZED = (dr["IS_BGT_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_BGT_FINALIZED"]);
                    ob.IS_BFB_FINALIZED = (dr["IS_BFB_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_BFB_FINALIZED"]);

                    if (dr["MC_TNA_TASK_STATUS_ID"] != DBNull.Value)
                        ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                    
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.ITEM_NAME_LIST = (dr["ITEM_NAME_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_LIST"]);
                    ob.ORD_MOU_CODE = (dr["ORD_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORD_MOU_CODE"]);
                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    ob.TOT_ORD_VALUE = (dr["TOT_ORD_VALUE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOT_ORD_VALUE"]);
                    ob.CURR_CONV_LOC = (dr["CURR_CONV_LOC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CURR_CONV_LOC"]);

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                             new CommandParameter() {ParameterName = "pMC_STYL_BGT_H_ID", Value =ob.MC_STYL_BGT_H_ID},
                             new CommandParameter() {ParameterName = "pOption", Value =3001},
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, sp);
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        ob.REV_DATA.Add(Convert.ToInt64(dr1["REVISION_NO"]));
                    }

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Save()
        {
            const string sp = "pkg_budget_sheet.mc_styl_bgt_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYL_BGT_H_ID", Value = ob.MC_STYL_BGT_H_ID},
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = ob.MC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},                     
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "PREVISION_NO_RTN", Value =500, Direction = ParameterDirection.Output}
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


    }
}