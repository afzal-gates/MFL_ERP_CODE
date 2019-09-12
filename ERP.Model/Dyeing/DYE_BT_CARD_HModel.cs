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
    public class DYE_BT_CARD_HModel
    {
        public Int64 DYE_BT_CARD_H_ID { get; set; }
        public Int64 DYE_BATCH_PLAN_ID { get; set; }
        public Int64 MC_LD_RECIPE_H_ID { get; set; }
        public Int64 LQR_RATIO { get; set; }
        public string DYE_LOT_NO { get; set; }
        public string DYE_BATCH_NO { get; set; }
        public DateTime BATCH_REQ_DT { get; set; }
        public Int64 ACT_BATCH_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Int64 MERGE_BT_CRD_ID { get; set; }
        public DateTime ACT_LOAD_TIME { get; set; }
        public DateTime ACT_UN_LOAD_TIME { get; set; }
        public Int64 OPERATOR_ID { get; set; }
        public Int64 LK_SHIFT_TYPE_ID { get; set; }
        public string BT_FLR_TEMP { get; set; }
        public Int64 DYE_BT_STS_TYPE_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public Int64? MC_BYR_ACC_GRP_ID { get; set; }
        public Int64? MC_STYLE_H_ID { get; set; }
        public long? RF_ACTN_STATUS_ID { get; set; }
        public long? INV_ITEM_CAT_ID { get; set; }
        public Int64 PLAN_BATCH_QTY { get; set; }
        public decimal ISSUE_QTY { get; set; }

        public Boolean IS_SELECT { get; set; }

        public List<DF_BT_SUB_LOTModel> subLot = new List<DF_BT_SUB_LOTModel>();

        private List<DYE_BT_CARD_GRPModel> _groups = null;
        public List<DYE_BT_CARD_GRPModel> groups
        {
            get
            {
                if (_groups == null)
                {
                    _groups = new List<DYE_BT_CARD_GRPModel>();
                }
                return _groups;
            }
            set
            {
                _groups = value;
            }
        }


        public string Save()
        {
            const string sp = "SP_DYE_BT_CARD_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_PLAN_ID", Value = ob.DYE_BATCH_PLAN_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_RECIPE_H_ID", Value = ob.MC_LD_RECIPE_H_ID},
                     new CommandParameter() {ParameterName = "pLQR_RATIO", Value = ob.LQR_RATIO},
                     new CommandParameter() {ParameterName = "pDYE_LOT_NO", Value = ob.DYE_LOT_NO},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pBATCH_REQ_DT", Value = ob.BATCH_REQ_DT},
                     new CommandParameter() {ParameterName = "pACT_BATCH_QTY", Value = ob.ACT_BATCH_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pMERGE_BT_CRD_ID", Value = ob.MERGE_BT_CRD_ID},
                     new CommandParameter() {ParameterName = "pACT_LOAD_TIME", Value = ob.ACT_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pACT_UN_LOAD_TIME", Value = ob.ACT_UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pOPERATOR_ID", Value = ob.OPERATOR_ID},
                     new CommandParameter() {ParameterName = "pLK_SHIFT_TYPE_ID", Value = ob.LK_SHIFT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pBT_FLR_TEMP", Value = ob.BT_FLR_TEMP},
                     new CommandParameter() {ParameterName = "pDYE_BT_STS_TYPE_ID", Value = ob.DYE_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public string Update()
        {
            const string sp = "SP_DYE_BT_CARD_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_PLAN_ID", Value = ob.DYE_BATCH_PLAN_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_RECIPE_H_ID", Value = ob.MC_LD_RECIPE_H_ID},
                     new CommandParameter() {ParameterName = "pLQR_RATIO", Value = ob.LQR_RATIO},
                     new CommandParameter() {ParameterName = "pDYE_LOT_NO", Value = ob.DYE_LOT_NO},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pBATCH_REQ_DT", Value = ob.BATCH_REQ_DT},
                     new CommandParameter() {ParameterName = "pACT_BATCH_QTY", Value = ob.ACT_BATCH_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pMERGE_BT_CRD_ID", Value = ob.MERGE_BT_CRD_ID},
                     new CommandParameter() {ParameterName = "pACT_LOAD_TIME", Value = ob.ACT_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pACT_UN_LOAD_TIME", Value = ob.ACT_UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pOPERATOR_ID", Value = ob.OPERATOR_ID},
                     new CommandParameter() {ParameterName = "pLK_SHIFT_TYPE_ID", Value = ob.LK_SHIFT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pBT_FLR_TEMP", Value = ob.BT_FLR_TEMP},
                     new CommandParameter() {ParameterName = "pDYE_BT_STS_TYPE_ID", Value = ob.DYE_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
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

        public string Delete()
        {
            const string sp = "SP_DYE_BT_CARD_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_PLAN_ID", Value = ob.DYE_BATCH_PLAN_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_RECIPE_H_ID", Value = ob.MC_LD_RECIPE_H_ID},
                     new CommandParameter() {ParameterName = "pLQR_RATIO", Value = ob.LQR_RATIO},
                     new CommandParameter() {ParameterName = "pDYE_LOT_NO", Value = ob.DYE_LOT_NO},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pBATCH_REQ_DT", Value = ob.BATCH_REQ_DT},
                     new CommandParameter() {ParameterName = "pACT_BATCH_QTY", Value = ob.ACT_BATCH_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pMERGE_BT_CRD_ID", Value = ob.MERGE_BT_CRD_ID},
                     new CommandParameter() {ParameterName = "pACT_LOAD_TIME", Value = ob.ACT_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pACT_UN_LOAD_TIME", Value = ob.ACT_UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pOPERATOR_ID", Value = ob.OPERATOR_ID},
                     new CommandParameter() {ParameterName = "pLK_SHIFT_TYPE_ID", Value = ob.LK_SHIFT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pBT_FLR_TEMP", Value = ob.BT_FLR_TEMP},
                     new CommandParameter() {ParameterName = "pDYE_BT_STS_TYPE_ID", Value = ob.DYE_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
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

        public List<DYE_BT_CARD_HModel> SelectAll()
        {
            string sp = "pkg_dye_batch_program.dye_bt_card_h_select";
            try
            {
                var obList = new List<DYE_BT_CARD_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_CARD_HModel ob = new DYE_BT_CARD_HModel();
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_BATCH_PLAN_ID = (dr["DYE_BATCH_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_PLAN_ID"]);
                    ob.MC_LD_RECIPE_H_ID = (dr["MC_LD_RECIPE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_RECIPE_H_ID"]);
                    ob.LQR_RATIO = (dr["LQR_RATIO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LQR_RATIO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.BATCH_REQ_DT = (dr["BATCH_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BATCH_REQ_DT"]);
                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_BATCH_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.MERGE_BT_CRD_ID = (dr["MERGE_BT_CRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MERGE_BT_CRD_ID"]);
                    ob.ACT_LOAD_TIME = (dr["ACT_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_LOAD_TIME"]);
                    ob.ACT_UN_LOAD_TIME = (dr["ACT_UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_UN_LOAD_TIME"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.LK_SHIFT_TYPE_ID = (dr["LK_SHIFT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SHIFT_TYPE_ID"]);
                    ob.BT_FLR_TEMP = (dr["BT_FLR_TEMP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_FLR_TEMP"]);
                    ob.DYE_BT_STS_TYPE_ID = (dr["DYE_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_STS_TYPE_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DYE_BT_CARD_HModel> SelectByID(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_STYLE_H_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pMC_STYLE_H_EXT_ID = null, Int64? pMC_COLOR_ID = null)
        {
            string sp = "pkg_dye_batch_program.dye_bt_card_h_select";
            try
            {
                var obList = new List<DYE_BT_CARD_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     //new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value =pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value =pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_CARD_HModel ob = new DYE_BT_CARD_HModel();
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_BATCH_PLAN_ID = (dr["DYE_BATCH_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_PLAN_ID"]);
                    ob.MC_LD_RECIPE_H_ID = (dr["MC_LD_RECIPE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_RECIPE_H_ID"]);
                    ob.LQR_RATIO = (dr["LQR_RATIO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LQR_RATIO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.BATCH_REQ_DT = (dr["BATCH_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BATCH_REQ_DT"]);
                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_BATCH_QTY"]);

                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DYE_BT_CARD_HModel> SelectForDCProgram(Int64? pDYE_BT_CARD_H_ID = null)
        {
            string sp = "pkg_dye_batch_program.dye_bt_card_h_select";
            try
            {
                var obList = new List<DYE_BT_CARD_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value =pDYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_CARD_HModel ob = new DYE_BT_CARD_HModel();
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_BATCH_PLAN_ID = (dr["DYE_BATCH_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_PLAN_ID"]);
                    ob.MC_LD_RECIPE_H_ID = (dr["MC_LD_RECIPE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_RECIPE_H_ID"]);
                    ob.LQR_RATIO = (dr["LQR_RATIO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LQR_RATIO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.BATCH_REQ_DT = (dr["BATCH_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BATCH_REQ_DT"]);
                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_BATCH_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.MERGE_BT_CRD_ID = (dr["MERGE_BT_CRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MERGE_BT_CRD_ID"]);
                    ob.ACT_LOAD_TIME = (dr["ACT_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_LOAD_TIME"]);
                    ob.ACT_UN_LOAD_TIME = (dr["ACT_UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_UN_LOAD_TIME"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.LK_SHIFT_TYPE_ID = (dr["LK_SHIFT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SHIFT_TYPE_ID"]);
                    ob.BT_FLR_TEMP = (dr["BT_FLR_TEMP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_FLR_TEMP"]);
                    ob.DYE_BT_STS_TYPE_ID = (dr["DYE_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_STS_TYPE_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);

                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);

                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);
                    ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);
                    ob.DYE_MTHD_NAME = (dr["DYE_MTHD_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MTHD_NAME"]);

                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DYE_BT_CARD_HModel Select(long ID)
        {
            string sp = "pkg_dye_batch_program.dye_bt_card_h_select";
            try
            {
                var ob = new DYE_BT_CARD_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_BATCH_PLAN_ID = (dr["DYE_BATCH_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_PLAN_ID"]);
                    ob.MC_LD_RECIPE_H_ID = (dr["MC_LD_RECIPE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_RECIPE_H_ID"]);
                    ob.LQR_RATIO = (dr["LQR_RATIO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LQR_RATIO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.BATCH_REQ_DT = (dr["BATCH_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BATCH_REQ_DT"]);
                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_BATCH_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.MERGE_BT_CRD_ID = (dr["MERGE_BT_CRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MERGE_BT_CRD_ID"]);
                    ob.ACT_LOAD_TIME = (dr["ACT_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_LOAD_TIME"]);
                    ob.ACT_UN_LOAD_TIME = (dr["ACT_UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_UN_LOAD_TIME"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.LK_SHIFT_TYPE_ID = (dr["LK_SHIFT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SHIFT_TYPE_ID"]);
                    ob.BT_FLR_TEMP = (dr["BT_FLR_TEMP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_FLR_TEMP"]);
                    ob.DYE_BT_STS_TYPE_ID = (dr["DYE_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_STS_TYPE_ID"]);
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

        public List<DYE_BT_CARD_HModel> SearchBatch(string pDYE_BATCH_NO = null, Int64? pMC_STYLE_H_ID = null)
        {
            string sp = "pkg_dye_batch_program.dye_bt_card_h_select";
            try
            {
                var obList = new List<DYE_BT_CARD_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value =pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_CARD_HModel ob = new DYE_BT_CARD_HModel();
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_BATCH_PLAN_ID = (dr["DYE_BATCH_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_PLAN_ID"]);
                    ob.MC_LD_RECIPE_H_ID = (dr["MC_LD_RECIPE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_RECIPE_H_ID"]);
                    ob.LQR_RATIO = (dr["LQR_RATIO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LQR_RATIO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.BATCH_REQ_DT = (dr["BATCH_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BATCH_REQ_DT"]);
                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_BATCH_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.MERGE_BT_CRD_ID = (dr["MERGE_BT_CRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MERGE_BT_CRD_ID"]);
                    ob.ACT_LOAD_TIME = (dr["ACT_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_LOAD_TIME"]);
                    ob.ACT_UN_LOAD_TIME = (dr["ACT_UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_UN_LOAD_TIME"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.LK_SHIFT_TYPE_ID = (dr["LK_SHIFT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SHIFT_TYPE_ID"]);
                    ob.BT_FLR_TEMP = (dr["BT_FLR_TEMP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_FLR_TEMP"]);
                    ob.DYE_BT_STS_TYPE_ID = (dr["DYE_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_STS_TYPE_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DYE_BT_CARD_HModel> UpdateBatchCardLabRecipeNo(Int64? pDYE_BT_CARD_H_ID = null, Int64? pMC_COLOR_ID = null, Int64? pMC_STYLE_H_EXT_ID = null)
        {
            string sp = "pkg_dye_batch_program.dye_bt_card_h_lab_update";
            try
            {
                var obList = new List<DYE_BT_CARD_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value =pDYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_CARD_HModel ob = new DYE_BT_CARD_HModel();

                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.MC_LD_RECIPE_H_ID = (dr["MC_LD_RECIPE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_RECIPE_H_ID"]);
                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                    ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);
                    ob.DYE_MTHD_NAME = (dr["DYE_MTHD_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MTHD_NAME"]);
                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<DYE_BT_CARD_HModel> FabricHoldList(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_STYLE_H_EXT_ID = null, Int64? pMC_COLOR_ID = null)
        {
            string sp = "PKG_DF_REPORT.DF_RPT_SELECT";
            try
            {
                var obList = new List<DYE_BT_CARD_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =6010}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_CARD_HModel ob = new DYE_BT_CARD_HModel();

                    ob.ACT_PROD_DT = (dr["ACT_PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_PROD_DT"]);

                    ob.FB_DFCT_TYPE_NAME = (dr["FB_DFCT_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FB_DFCT_TYPE_NAME"]);
                    ob.RESP_DEPT_NAME = (dr["RESP_DEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RESP_DEPT_NAME"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.BATCH_QTY = (dr["BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BATCH_QTY"]);
                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_BATCH_QTY"]);

                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.SUB_LOT_NO = (dr["SUB_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_LOT_NO"]);
                    ob.LK_FBR_GRP_NAME = (dr["LK_FBR_GRP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FBR_GRP_NAME"]);
                    ob.LK_DIA_TYPE_NAME = (dr["LK_DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DIA_TYPE_NAME"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.RQD_GSM = (dr["RQD_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_GSM"]);

                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    ob.OFFICE_SNAME = (dr["OFFICE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_SNAME"]);
                    ob.COMP_SNAME = (dr["COMP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_SNAME"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DYE_BT_CARD_HModel> SelectAopBatchList(int pageNo, int pageSize, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pMC_STYLE_H_EXT_ID = null, Int64? pMC_COLOR_ID = null, string pDYE_BATCH_NO = null)
        {
            string sp = "PKG_DF_SC_PROGRAM.DF_AOP_BATCH_FAB_SELECT";
            try
            {
                var obList = new List<DYE_BT_CARD_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value =pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value =pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_CARD_HModel ob = new DYE_BT_CARD_HModel();
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    //ob.DYE_BATCH_PLAN_ID = (dr["DYE_BATCH_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_PLAN_ID"]);
                    //ob.MC_LD_RECIPE_H_ID = (dr["MC_LD_RECIPE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_RECIPE_H_ID"]);
                    //ob.LQR_RATIO = (dr["LQR_RATIO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LQR_RATIO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.BATCH_REQ_DT = (dr["BATCH_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BATCH_REQ_DT"]);
                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_BATCH_QTY"]);

                    //ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    //ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    //ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    //ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    //ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DYE_MACHINE_NO { get; set; }

        public string BUYER_NAME_EN { get; set; }

        public string STYLE_NO { get; set; }

        public string ORDER_NO { get; set; }

        public string COLOR_NAME_EN { get; set; }

        public string LD_RECIPE_NO { get; set; }

        public long MC_COLOR_GRP_ID { get; set; }

        public string COLOR_GRP_NAME_EN { get; set; }

        public long LK_DYE_MTHD_ID { get; set; }

        public string DYE_MTHD_NAME { get; set; }
        public long DYE_MACHINE_ID { get; set; }
        public long MC_STYLE_H_EXT_ID { get; set; }
        public long MC_COLOR_ID { get; set; }
        public string IS_PERFORMER { get; set; }
        public string ACTN_ROLE_FLAG { get; set; }
        public string NXT_ACTION_NAME { get; set; }


        public List<DYE_BT_CARD_HModel> getBatchListByFabReqH(
             Int64? pDYE_GSTR_REQ_H_ID = null,
             string pIS_PERFORMER = null,
             string pACTN_ROLE_FLAG = null,
             string pNXT_ACTION_NAME = null,
             Int64? pDYE_GSTR_ISS_H_ID = null
            )
        {
            string sp = "PKG_DYE_GFAB_REQ.grey_fab_req_list_select";
            try
            {
                var obList = new List<DYE_BT_CARD_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_GSTR_REQ_H_ID", Value =pDYE_GSTR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_CARD_HModel ob = new DYE_BT_CARD_HModel();
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_GSTR_REQ_H_ID = pDYE_GSTR_REQ_H_ID;
                    ob.DYE_GSTR_ISS_H_ID = pDYE_GSTR_ISS_H_ID;
                    ob.DYE_BATCH_PLAN_ID = (dr["DYE_BATCH_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_PLAN_ID"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_BATCH_QTY"]);
                    ob.ACT_LOAD_TIME = (dr["ACT_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_LOAD_TIME"]);
                    ob.ACT_UN_LOAD_TIME = (dr["ACT_UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_UN_LOAD_TIME"]);
                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);

                    ob.PLAN_BATCH_QTY = (dr["PLAN_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLAN_BATCH_QTY"]);

                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);

                    ob.IS_PERFORMER = pIS_PERFORMER;
                    ob.ACTN_ROLE_FLAG = pACTN_ROLE_FLAG;
                    ob.NXT_ACTION_NAME = pNXT_ACTION_NAME;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DYE_BT_CARD_HModel getBatchDataWithGroup(
                    Int64 pDYE_BT_CARD_H_ID,
                    Int64 pDYE_GSTR_ISS_H_ID
        )
        {
            string sp = "PKG_DYE_GFAB_REQ.grey_fab_req_list_select";
            try
            {
                DYE_BT_CARD_HModel ob = new DYE_BT_CARD_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = pDYE_BT_CARD_H_ID },
                     new CommandParameter() {ParameterName = "pDYE_GSTR_ISS_H_ID", Value = pDYE_GSTR_ISS_H_ID },
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_BATCH_PLAN_ID = (dr["DYE_BATCH_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_PLAN_ID"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_BATCH_QTY"]);
                    ob.ACT_LOAD_TIME = (dr["ACT_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_LOAD_TIME"]);
                    ob.ACT_UN_LOAD_TIME = (dr["ACT_UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_UN_LOAD_TIME"]);
                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.RF_ACTN_STATUS_ID_ISS = (dr["RF_ACTN_STATUS_ID_ISS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID_ISS"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);

                    ob.PLAN_BATCH_QTY = (dr["PLAN_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLAN_BATCH_QTY"]);

                    ob.DYE_GSTR_REQ_H_ID = (dr["DYE_GSTR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_REQ_H_ID"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);

                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.IS_BATCH_STORE = (dr["IS_BATCH_STORE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_BATCH_STORE"]);

                    ob.groups = new DYE_BT_CARD_GRPModel().getGropData(ob.DYE_BT_CARD_H_ID);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DYE_BT_CARD_HModel GetPreTreatmentInfo(Int64 pDYE_GSTR_REQ_H_ID)
        {
            string sp = "PKG_DYE_GFAB_REQ.grey_fab_req_list_select";
            try
            {
                DYE_BT_CARD_HModel ob = new DYE_BT_CARD_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_GSTR_REQ_H_ID", Value = pDYE_GSTR_REQ_H_ID },
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    //ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    //ob.DYE_BATCH_PLAN_ID = (dr["DYE_BATCH_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_PLAN_ID"]);
                    //ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    //ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_BATCH_QTY"]);
                    //ob.ACT_LOAD_TIME = (dr["ACT_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_LOAD_TIME"]);
                    //ob.ACT_UN_LOAD_TIME = (dr["ACT_UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_UN_LOAD_TIME"]);
                    //ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    //ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    //ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    //ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    //ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    //ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    //ob.RF_ACTN_STATUS_ID_ISS = (dr["RF_ACTN_STATUS_ID_ISS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID_ISS"]);
                    //ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);

                    ob.DYE_GSTR_REQ_H_ID = (dr["DYE_GSTR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);

                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);

                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.SC_PRG_NO = (dr["SC_PRG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_PRG_NO"]);

                    //ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    //ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    //ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    //ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    //ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.PRG_ISS_DT = (dr["PRG_ISS_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PRG_ISS_DT"]);
                    ob.EXP_DELV_DT = (dr["EXP_DELV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EXP_DELV_DT"]);

                    ob.groups = new DYE_BT_CARD_GRPModel().getGropDataForPreTreatment(ob.DYE_GSTR_REQ_H_ID);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DYE_BT_CARD_HModel> GetBatchFabricByBatchNo(string pDYE_BATCH_NO = null, Int64? pDYE_STR_REQ_H_ID = null, Int64? pOption = 3006)
        {
            string sp = "pkg_dye_batch_program.scan_dye_bt_card_select";
            try
            {
                var obList = new List<DYE_BT_CARD_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value =pDYE_BATCH_NO},  
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value =pDYE_STR_REQ_H_ID},  
                     new CommandParameter() {ParameterName = "pOption", Value =pOption==null?3006:pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_CARD_HModel ob = new DYE_BT_CARD_HModel();

                    ob.FABRIC_GROUP_NAME = (dr["FABRIC_GROUP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_GROUP_NAME"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                    ob.FIN_QTY = (dr["FIN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FIN_QTY"]);

                    ob.FULL_RQD_QTY = (dr["FULL_RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FULL_RQD_QTY"]);
                    ob.FULL_FINIS_QTY = (dr["FULL_FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FULL_FINIS_QTY"]);

                    ob.KNT_YRN_LOT = (dr["KNT_YRN_LOT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT"]);

                    ob.LK_DIA_TYPE_NAME = (dr["LK_DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DIA_TYPE_NAME"]);
                    ob.RQD_GSM = (dr["RQD_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_GSM"]);

                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);

                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? "" : Convert.ToString(dr["FIN_DIA"]);
                    ob.ISSUE_QTY = (dr["ISSUE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_QTY"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DYE_BT_CARD_HModel> GetBatchIssueFabricByBatchNo(string pDYE_BATCH_NO = null, Int64? pDYE_STR_REQ_H_ID = null, Int64? pOption = 3009)
        {
            string sp = "pkg_dye_batch_program.scan_dye_bt_card_select";
            try
            {
                var obList = new List<DYE_BT_CARD_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = pDYE_BATCH_NO},  
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value =pDYE_STR_REQ_H_ID},  
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_CARD_HModel ob = new DYE_BT_CARD_HModel();

                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.FAB_TYPE_DESC = (dr["FAB_TYPE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_DESC"]);
                    ob.FAB_ITEM_DESC = (dr["FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_DESC"]);
                    
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    //ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);

                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                    ob.FIN_QTY = (dr["FIN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FIN_QTY"]);

                    ob.FULL_RQD_QTY = (dr["FULL_RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FULL_RQD_QTY"]);
                    ob.FULL_FINIS_QTY = (dr["FULL_FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FULL_FINIS_QTY"]);

                    ob.ISSUE_QTY = (dr["ISSUE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_QTY"]);

                    if (ob.LK_DIA_TYPE_ID == 734)
                    {
                        ob.FLAT_KNIT_ITEM_LIST = new DF_BT_SUB_LOT_CLCFModel().SelectByID(ob.DYE_BT_CARD_H_ID, ob.DF_BT_SUB_LOT_ID, ob.KNT_STYL_FAB_ITEM_H_ID);
                    }

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DYE_BT_CARD_HModel> GetFabricByBatchNo(Int64? pDYE_BT_CARD_H_ID = null, string pLK_FBR_GRP_LST = null)
        {
            string sp = "pkg_dye_batch_program.scan_dye_bt_card_select";
            try
            {
                var obList = new List<DYE_BT_CARD_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = pDYE_BT_CARD_H_ID},  
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_LST", Value =pLK_FBR_GRP_LST},  
                     new CommandParameter() {ParameterName = "pOption", Value =3010},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_CARD_HModel ob = new DYE_BT_CARD_HModel();

                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.FAB_TYPE_DESC = (dr["FAB_TYPE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_DESC"]);
                    ob.FAB_ITEM_DESC = (dr["FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_DESC"]);

                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    //ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);

                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                    ob.FIN_QTY = (dr["FIN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FIN_QTY"]);
                    ob.BATCH_QTY = Convert.ToInt64(ob.RQD_QTY);

                    ob.FULL_RQD_QTY = (dr["FULL_RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FULL_RQD_QTY"]);
                    ob.FULL_FINIS_QTY = (dr["FULL_FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FULL_FINIS_QTY"]);

                    ob.ISSUE_QTY = (dr["ISSUE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_QTY"]);

                    if (ob.LK_DIA_TYPE_ID == 734)
                    {
                        ob.FLAT_KNIT_ITEM_LIST = new DF_BT_SUB_LOT_CLCFModel().SelectByID(ob.DYE_BT_CARD_H_ID, ob.DF_BT_SUB_LOT_ID, ob.KNT_STYL_FAB_ITEM_H_ID);
                    }

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string FAB_TYPE_SNAME { get; set; }
        public string FABRIC_GROUP_NAME { get; set; }
        public string FIB_COMP_NAME { get; set; }
        public decimal RQD_QTY { get; set; }
        public decimal FIN_QTY { get; set; }
        public string KNT_YRN_LOT { get; set; }


        public long? DYE_GSTR_REQ_H_ID { get; set; }

        public string STR_REQ_NO { get; set; }

        public string SUP_TRD_NAME_EN { get; set; }
        public long RF_ACTN_STATUS_ID_ISS { get; set; }
        public long? DYE_GSTR_ISS_H_ID { get; set; }

        public long HR_COMPANY_ID { get; set; }

        public long HR_OFFICE_ID { get; set; }

        public string REMARKS { get; set; }

        public string SC_PRG_NO { get; set; }

        public string CHALAN_NO { get; set; }

        public string GATE_PASS_NO { get; set; }

        public string VEHICLE_NO { get; set; }

        public string CARRIER_NAME { get; set; }

        public string COMP_NAME_EN { get; set; }

        public DateTime STR_REQ_DT { get; set; }

        public DateTime PRG_ISS_DT { get; set; }

        public DateTime EXP_DELV_DT { get; set; }

        public DateTime CHALAN_DT { get; set; }

        public string LK_DIA_TYPE_NAME { get; set; }

        public string RQD_GSM { get; set; }

        public long LK_DIA_TYPE_ID { get; set; }

        public long LK_FBR_GRP_ID { get; set; }

        public long RF_FAB_TYPE_ID { get; set; }

        public long RF_FIB_COMP_ID { get; set; }

        public decimal FULL_RQD_QTY { get; set; }

        public decimal FULL_FINIS_QTY { get; set; }

        public Int64? DF_BT_SUB_LOT_ID { get; set; }

        public string IS_BATCH_STORE { get; set; }

        public DateTime ACT_PROD_DT { get; set; }

        public string FB_DFCT_TYPE_NAME { get; set; }

        public string RESP_DEPT_NAME { get; set; }

        public string SUB_LOT_NO { get; set; }

        public string LK_FBR_GRP_NAME { get; set; }

        public string OFFICE_SNAME { get; set; }

        public string COMP_SNAME { get; set; }

        public long BATCH_QTY { get; set; }

        public string FIN_DIA { get; set; }

        public long KNT_STYL_FAB_ITEM_H_ID { get; set; }

        public string FAB_TYPE_DESC { get; set; }

        public string FAB_ITEM_DESC { get; set; }

        public int TOTAL_REC { get; set; }

        public List<DF_BT_SUB_LOT_CLCFModel> FLAT_KNIT_ITEM_LIST { get; set; }

        public decimal FINIS_QTY { get; set; }
        public Int32 ROLL_QTY { get; set; }
        
        public long DF_RIB_SHADE_RPT_D_ID { get; set; }

        public long DF_RIB_SHADE_RPT_H_ID { get; set; }
    }
}