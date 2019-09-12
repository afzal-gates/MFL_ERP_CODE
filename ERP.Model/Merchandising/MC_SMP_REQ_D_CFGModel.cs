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
    public class MC_SMP_REQ_D_CFGModel
    {
        public Int64 MC_SMP_REQ_D_CFG_ID { get; set; }
        public Int64 MC_SMP_REQ_D_ID { get; set; }
        public Int64? RF_TEST_INST_ID { get; set; }        
        public Int64 MC_STYLE_D_ITEM_ID { get; set; }
        public string IS_ALL_COL { get; set; }
        public string MC_COLOR_LST { get; set; }
        public string MC_SIZE_LST { get; set; }
        public Int64 RQD_QTY { get; set; }
        public string IS_SZ_TBC { get; set; }
        public DateTime? CONFIRM_DT { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string REMARKS { get; set; }
        public string IS_ANY_COL { get; set; }
        public string IS_AVAILABLE_COL { get; set; }

        public string ITEM_SNAME { get; set; }
        public string[] MC_COLOR_LIST { get; set; }
        public string[] MC_SIZE_LIST { get; set; }
        public int SZ_DEFA_QTY { get; set; }
        public Int64? PARENT_ID { get; set; }
        public List<MC_STYLE_D_ITEMModel> childItemList { get; set; }
        public List<MC_COLORModel> styleWiseColorList { get; set; }
        public List<MC_SIZEModel> styleWiseSizeList { get; set; }

        

        public List<MC_SMP_REQ_D_CFGModel> SelectAll()
        {
            string sp = "Select_MC_SMP_REQ_D_CFG";
            try
            {
                var obList = new List<MC_SMP_REQ_D_CFGModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_CFG_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                   MC_SMP_REQ_D_CFGModel ob = new MC_SMP_REQ_D_CFGModel();
                   ob.MC_SMP_REQ_D_CFG_ID = (dr["MC_SMP_REQ_D_CFG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_CFG_ID"]);
                   ob.MC_SMP_REQ_D_ID = (dr["MC_SMP_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_ID"]);
                   ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                   ob.IS_ALL_COL = (dr["IS_ALL_COL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ALL_COL"]);
                   ob.MC_COLOR_LST = (dr["MC_COLOR_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_COLOR_LST"]);
                   ob.IS_ANY_COL = (dr["IS_ANY_COL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ANY_COL"]);
                   ob.IS_AVAILABLE_COL = (dr["IS_AVAILABLE_COL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AVAILABLE_COL"]);

                   ob.MC_SIZE_LST = (dr["MC_SIZE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SIZE_LST"]);
                   ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                   ob.IS_SZ_TBC = (dr["IS_SZ_TBC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SZ_TBC"]);
                   ob.CONFIRM_DT = (dr["CONFIRM_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CONFIRM_DT"]);
                   ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                   ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);        
                            
                   obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_SMP_REQ_D_CFGModel Select(long ID)
        {
            string sp = "Select_MC_SMP_REQ_D_CFG";
            try
            {
                var ob = new MC_SMP_REQ_D_CFGModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_CFG_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {

                   ob.MC_SMP_REQ_D_CFG_ID = (dr["MC_SMP_REQ_D_CFG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_CFG_ID"]);
                   ob.MC_SMP_REQ_D_ID = (dr["MC_SMP_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_ID"]);
                   ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                   ob.IS_ALL_COL = (dr["IS_ALL_COL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ALL_COL"]);
                   ob.MC_COLOR_LST = (dr["MC_COLOR_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_COLOR_LST"]);
                   ob.IS_ANY_COL = (dr["IS_ANY_COL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ANY_COL"]);
                   ob.IS_AVAILABLE_COL = (dr["IS_AVAILABLE_COL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AVAILABLE_COL"]);

                   ob.MC_SIZE_LST = (dr["MC_SIZE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SIZE_LST"]);
                   ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                   ob.IS_SZ_TBC = (dr["IS_SZ_TBC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SZ_TBC"]);
                   ob.CONFIRM_DT = (dr["CONFIRM_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CONFIRM_DT"]);
                   ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                   ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                        
               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string DeleteSampCfg()
        {
            const string sp = "pkg_mc_fab_booking.mc_smp_req_d_cfg_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_ID", Value = ob.MC_SMP_REQ_D_ID},
                    new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_CFG_ID", Value = ob.MC_SMP_REQ_D_CFG_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value = ob.MC_STYLE_D_ITEM_ID},
                    new CommandParameter() {ParameterName = "pRF_TEST_INST_ID", Value = ob.RF_TEST_INST_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 4000},
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

        public object smpCfgItemWiseColorList(string pMC_COLOR_LST)
        {
            string sp = "pkg_merchandising.mc_color_select";
            try
            {
                var obList = new List<MC_COLORModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_COLOR_LST", Value = pMC_COLOR_LST},                           
                     new CommandParameter() {ParameterName = "pOption", Value = 3009},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_COLORModel ob = new MC_COLORModel();
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.COLOR_CODE = (dr["COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_CODE"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.IS_DUMMY_COLOR = (dr["IS_DUMMY_COLOR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DUMMY_COLOR"]);

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