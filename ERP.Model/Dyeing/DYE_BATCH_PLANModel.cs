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
    public class DYE_BATCH_PLANModel
    {
        public Int64 DYE_BATCH_PLAN_ID { get; set; }
        public Int64 DYE_BATCH_SCDL_ID { get; set; }
        public Int64 DYE_MACHINE_ID { get; set; }
        public Int64 MC_FAB_PROD_ORD_H_ID { get; set; }
        public string LNK_ORD_H_ID_LST { get; set; }
        public Int64 FAB_COLOR_ID { get; set; }
        public Int64 RF_FAB_TYPE_ID { get; set; }
        public Int64 LK_DYE_MTHD_ID { get; set; }
        public Int64 PRIORITY_NO { get; set; }
        public Int64 RQD_PRD_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public DateTime LOAD_TIME { get; set; }
        public DateTime UN_LOAD_TIME { get; set; }
        public Int64 REVISION_NO { get; set; }
        public string REV_REASON { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64? SCM_STORE_ID { get; set; }
        public string IS_BATCH_STORE { get; set; }


        public string XML { get; set; }

        private List<SIZE_QTY_VIEWMODEL> _col_cu_reqs = null;
        public List<SIZE_QTY_VIEWMODEL> col_cu_reqs
        {
            get
            {
                if (_col_cu_reqs == null)
                {
                    _col_cu_reqs = new List<SIZE_QTY_VIEWMODEL>();
                }
                return _col_cu_reqs;
            }
            set
            {
                _col_cu_reqs = value;
            }
        }

        private List<MC_TRMS_DY_PRODModel> _woven_trims_avail = null;
        public List<MC_TRMS_DY_PRODModel> woven_trims_avai
        {
            get
            {
                if (_woven_trims_avail == null)
                {
                    _woven_trims_avail = new List<MC_TRMS_DY_PRODModel>();
                }
                return _woven_trims_avail;
            }
            set
            {
                _woven_trims_avail = value;
            }
        }



        private List<NON_COL_CUF_A_VIEWMODEL> _non_col_cu_avail = null;
        public List<NON_COL_CUF_A_VIEWMODEL> non_col_cu_avail
        {
            get
            {
                if (_non_col_cu_avail == null)
                {
                    _non_col_cu_avail = new List<NON_COL_CUF_A_VIEWMODEL>();
                }
                return _non_col_cu_avail;
            }
            set
            {
                _non_col_cu_avail = value;
            }
        }

        private List<DYE_BT_PLAN_FIN_DIAModel> _fin_dias = null;
        public List<DYE_BT_PLAN_FIN_DIAModel> fin_dias
        {
            get
            {
                if (_fin_dias == null)
                {
                    _fin_dias = new List<DYE_BT_PLAN_FIN_DIAModel>();
                }
                return _fin_dias;
            }
            set
            {
                _fin_dias = value;
            }
        }



        private List<COL_CUF_A_VIEWMODEL> _col_cu_avail = null;
        public List<COL_CUF_A_VIEWMODEL> col_cu_avail
        {
            get
            {
                if (_col_cu_avail == null)
                {
                    _col_cu_avail = new List<COL_CUF_A_VIEWMODEL>();
                }
                return _col_cu_avail;
            }
            set
            {
                _col_cu_avail = value;
            }
        }


        private List<MC_TRMS_DY_PRODModel> _trims_avail = null;
        public List<MC_TRMS_DY_PRODModel> trims_avail
        {
            get
            {
                if (_trims_avail == null)
                {
                    _trims_avail = new List<MC_TRMS_DY_PRODModel>();
                }
                return _trims_avail;
            }
            set
            {
                _trims_avail = value;
            }
        }


        public string Save()
        {
            const string sp = "pkg_DYE_BT_FAB_REQ.create_batch_fab_req";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pXML_DF", Value = ob.XML_DF},

                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public string CreateRequisition()
        {
            const string sp = "pkg_DYE_BT_FAB_REQ.create_fab_req_fab";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {

                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPREFIX", Value = ob.PREFIX},
                     new CommandParameter() {ParameterName = "pRF_ACTN_TYPE_ID", Value = ob.RF_ACTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pIS_BATCH_STORE", Value = ob.IS_BATCH_STORE==null?"N":ob.IS_BATCH_STORE},
                     
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public List<DYE_BATCH_PLANModel> getProgramData(Int64? pDYE_BATCH_SCDL_ID, Int64? pDYE_MACHINE_ID, Int64 pOption, string pDYE_BATCH_NO, Int64? pDYE_SC_PRG_ISS_ID = null)
        {
            string sp = "pkg_DYE_BT_FAB_REQ.dye_batch_plan_select";
            try
            {
                var obList = new List<DYE_BATCH_PLANModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_SCDL_ID", Value = pDYE_BATCH_SCDL_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = pDYE_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pDYE_SC_PRG_ISS_ID", Value = pDYE_SC_PRG_ISS_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = pOption},


                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BATCH_PLANModel ob = new DYE_BATCH_PLANModel();
                    ob.DYE_BATCH_PLAN_ID = (dr["DYE_BATCH_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_PLAN_ID"]);
                    ob.DYE_BATCH_SCDL_ID = (dr["DYE_BATCH_SCDL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_SCDL_ID"]);
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.LNK_ORD_H_ID_LST = (dr["LNK_ORD_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LNK_ORD_H_ID_LST"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);

                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);

                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);

                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);

                    ob.PRIORITY_NO = (dr["PRIORITY_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRIORITY_NO"]);
                    ob.RQD_PRD_QTY = (dr["RQD_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_PRD_QTY"]);

                    ob.LEFT_TO_REQ = (dr["LEFT_TO_REQ"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEFT_TO_REQ"]);

                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.LOAD_TIME = (dr["LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOAD_TIME"]);
                    ob.UN_LOAD_TIME = (dr["UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["UN_LOAD_TIME"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);

                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FABRIC_SNAME = (dr["FABRIC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_SNAME"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.BASE_STYLE_NO = (dr["BASE_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BASE_STYLE_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.IS_BATCH_CREATED = (dr["IS_BATCH_CREATED"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["IS_BATCH_CREATED"]);
                    ob.DYE_BATCH_NO_LST = (dr["DYE_BATCH_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO_LST"]);
                    ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);
                    ob.IS_FAB_REQ_DONE = (dr["IS_FAB_REQ_DONE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FAB_REQ_DONE"]);

                    
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                    ob.MC_BLK_REVISION_NO = (dr["MC_BLK_REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_REVISION_NO"]);

                    ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);

                    ob.IS_LOT_MIX = (dr["IS_LOT_MIX"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_LOT_MIX"]);
                    ob.OTP_CODE = (dr["OTP_CODE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OTP_CODE"]);

                    ob.IS_SELECTED_BT = false;//pOption == 3003;

                    ob.fin_dias = new DYE_BT_PLAN_FIN_DIAModel().getBtDiaFinByPlanId(3003, ob.DYE_BATCH_PLAN_ID);

                    


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DYE_BATCH_PLANModel Select(long ID)
        {
            string sp = "Select_DYE_BATCH_PLAN";
            try
            {
                var ob = new DYE_BATCH_PLANModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_PLAN_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DYE_BATCH_PLAN_ID = (dr["DYE_BATCH_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_PLAN_ID"]);
                    ob.DYE_BATCH_SCDL_ID = (dr["DYE_BATCH_SCDL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_SCDL_ID"]);
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.LNK_ORD_H_ID_LST = (dr["LNK_ORD_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LNK_ORD_H_ID_LST"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                    ob.PRIORITY_NO = (dr["PRIORITY_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRIORITY_NO"]);
                    ob.RQD_PRD_QTY = (dr["RQD_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_PRD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.LOAD_TIME = (dr["LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOAD_TIME"]);
                    ob.UN_LOAD_TIME = (dr["UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["UN_LOAD_TIME"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string FAB_TYPE_NAME { get; set; }

        public string COLOR_NAME_EN { get; set; }

        public string FABRIC_SNAME { get; set; }

        public string WORK_STYLE_NO { get; set; }

        public string BASE_STYLE_NO { get; set; }

        public string STYLE_DESC { get; set; }

        public string BYR_ACC_NAME_EN { get; set; }

        public string ORDER_NO_LIST { get; set; }

        public List<DYE_BATCH_PLANModel> GetDataFabReqCal(
            string pMC_FAB_PROD_ORD_H_LST,
            long? pDYE_BATCH_SCDL_ID,
            long pFAB_COLOR_ID,
            long pRF_FAB_TYPE_ID,
            decimal pRQD_PRD_QTY,
            Int64? pINV_ITEM_CAT_ID,
            Int64? pDYE_BATCH_PLAN_ID,
            string pIS_BATCH_STORE
        )
        {
            string sp = "pkg_DYE_BT_FAB_REQ.dye_batch_select_req_cal";
            try
            {
                var obList = new List<DYE_BATCH_PLANModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_LST", Value = pMC_FAB_PROD_ORD_H_LST},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_PLAN_ID", Value = pDYE_BATCH_PLAN_ID},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_SCDL_ID", Value = pDYE_BATCH_SCDL_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = pFAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = pRF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = pINV_ITEM_CAT_ID},

                     new CommandParameter() {ParameterName = "pRQD_PRD_QTY", Value = pRQD_PRD_QTY},
                     new CommandParameter() {ParameterName = "pOption", Value = (pINV_ITEM_CAT_ID==35||pINV_ITEM_CAT_ID==7)?3002 : 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BATCH_PLANModel ob = new DYE_BATCH_PLANModel();
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);

                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);

                    ob.MC_FAB_PROD_ORD_H_SPAN = (dr["MC_FAB_PROD_ORD_H_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_SPAN"]);
                    ob.MC_FAB_PROD_ORD_H_SL = (dr["MC_FAB_PROD_ORD_H_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_SL"]);

                    //ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);

                    ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);

                    ob.DYE_BT_CARD_GRP_ID = (dr["DYE_BT_CARD_GRP_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["DYE_BT_CARD_GRP_ID"]);
                    ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);

                    ob.TOTAL = (dr["TOTAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOTAL"]);

                    ob.NET_GFAB_QTY = (dr["NET_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_GFAB_QTY"]);

                    ob.BODY_TOTAL = (dr["BODY_TOTAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BODY_TOTAL"]);

                    ob.REQ_BODY = (dr["REQ_BODY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REQ_BODY"]);


                    ob.RQD_PRD_QTY = (dr["RQD_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_PRD_QTY"]);
                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? ob.RQD_PRD_QTY : Convert.ToDecimal(dr["ACT_BATCH_QTY"]);

                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);


                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);

                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);

                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.FAB_GROUP = (dr["FAB_GROUP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_GROUP"]);
                    // ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);

                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);

                    //Trims & Woven
                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.RQD_QTY_YDS = (dr["RQD_QTY_YDS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY_YDS"]);
                    ob.RQD_QTY_KG = (dr["RQD_QTY_KG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY_KG"]);
                    ob.DYE_BT_CARD_D_TRMS_ID = (dr["DYE_BT_CARD_D_TRMS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_D_TRMS_ID"]);
                    //Trims & Woven

                    ob.IS_FLAT_CIR = (dr["IS_FLAT_CIR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FLAT_CIR"]);

                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    if (dr["LK_DIA_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_DIA_TYPE_ID = Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    }

                    ob.RQD_GSM = (dr["RQD_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_GSM"]);

                    ob.WASH_TYPE_TXT = (dr["WASH_TYPE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WASH_TYPE_TXT"]);
                    ob.FINISH_TYPE_TXT = (dr["FINISH_TYPE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FINISH_TYPE_TXT"]);


                    if (ob.INV_ITEM_CAT_ID == 7)
                    {
                        ob.RQD_QTY_KG = ob.NET_GFAB_QTY;
                    }
                    //ob.INV_ITEM_CAT_ID = pINV_ITEM_CAT_ID ?? 0;

                    if (ob.IS_FLAT_CIR == "F")
                    {
                        ob.col_cu_reqs = new SIZE_QTY_VIEWMODEL().getColCufReqCalData(
                            ob.MC_FAB_PROD_ORD_H_ID,
                            ob.RF_GARM_PART_ID,
                            ob.BODY_TOTAL,
                            ob.REQ_BODY,
                            pFAB_COLOR_ID

                        );
                    }

                    if (ob.IS_FLAT_CIR == "C")
                    {
                        ob.non_col_cu_avail = new NON_COL_CUF_A_VIEWMODEL().getNoColCufAvailabilityData(ob.MC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID, ob.RF_FAB_TYPE_ID,
                                                                                                        ob.DYE_BT_CARD_H_ID, ob.RF_FIB_COMP_ID, ob.DYE_BT_CARD_GRP_ID,
                                                                                                        (ob.LK_FBR_GRP_ID != 192 ? "N" : pIS_BATCH_STORE),
                                                                                                        ob.LK_DIA_TYPE_ID);
                    }

                    if (ob.IS_FLAT_CIR == "F")
                    {
                        ob.col_cu_avail = new COL_CUF_A_VIEWMODEL().getColCufAvailabilityData(ob.MC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID, ob.RF_FAB_TYPE_ID, ob.RF_GARM_PART_ID, ob.MC_STYLE_H_ID, ob.col_cu_reqs, ob.DYE_BT_CARD_H_ID, ob.DYE_BT_CARD_GRP_ID);

                        foreach (var xx in ob._col_cu_avail)
                        {
                            xx.ASSIGN_QTY = xx.ASSIGN_QTY < 0 ? ob.col_cu_reqs.Where(m => m.MESUREMENT == xx.MESUREMENT).FirstOrDefault().RQD_PC_QTY : xx.ASSIGN_QTY;
                        }
                    }


                    if (ob.IS_FLAT_CIR == "T")
                    {
                        ob.trims_avail = new MC_TRMS_DY_PRODModel().QueryStockBalance(ob.MC_FAB_PROD_ORD_H_ID.ToString(), pFAB_COLOR_ID, 7);
                    }

                    if (pINV_ITEM_CAT_ID == 7 || pINV_ITEM_CAT_ID == 35)
                    {
                        ob.woven_trims_avai = new MC_TRMS_DY_PRODModel().FabReqStockBalance(ob.MC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID, pINV_ITEM_CAT_ID);
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

        public List<DYE_BATCH_PLANModel> GetDataFabReqCalScP(
            string pMC_FAB_PROD_ORD_H_LST,
            long pFAB_COLOR_ID,
            long pRF_FAB_TYPE_ID,
            decimal pRQD_PRD_QTY,
            Int64? pINV_ITEM_CAT_ID,
            Int64? pDYE_BATCH_PLAN_ID,
            long pDYE_SC_PRG_ISS_ID
         )
        {
            string sp = "pkg_DYE_BT_FAB_REQ.dye_batch_select_req_cal_scp";
            try
            {
                var obList = new List<DYE_BATCH_PLANModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_LST", Value = pMC_FAB_PROD_ORD_H_LST},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_PLAN_ID", Value = pDYE_BATCH_PLAN_ID},
                     new CommandParameter() {ParameterName = "pDYE_SC_PRG_ISS_ID", Value = pDYE_SC_PRG_ISS_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = pFAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = pRF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = pINV_ITEM_CAT_ID},

                     new CommandParameter() {ParameterName = "pRQD_PRD_QTY", Value = pRQD_PRD_QTY},
                     new CommandParameter() {ParameterName = "pOption", Value = (pINV_ITEM_CAT_ID==35||pINV_ITEM_CAT_ID==7)?3002 : 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BATCH_PLANModel ob = new DYE_BATCH_PLANModel();
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);

                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);

                    ob.MC_FAB_PROD_ORD_H_SPAN = (dr["MC_FAB_PROD_ORD_H_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_SPAN"]);
                    ob.MC_FAB_PROD_ORD_H_SL = (dr["MC_FAB_PROD_ORD_H_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_SL"]);

                    //ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);

                    ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);

                    ob.DYE_BT_CARD_GRP_ID = (dr["DYE_BT_CARD_GRP_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["DYE_BT_CARD_GRP_ID"]);
                    ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);

                    ob.TOTAL = (dr["TOTAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOTAL"]);

                    ob.NET_GFAB_QTY = (dr["NET_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_GFAB_QTY"]);

                    ob.BODY_TOTAL = (dr["BODY_TOTAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BODY_TOTAL"]);

                    ob.REQ_BODY = (dr["REQ_BODY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REQ_BODY"]);


                    ob.RQD_PRD_QTY = (dr["RQD_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_PRD_QTY"]);
                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? ob.RQD_PRD_QTY : Convert.ToDecimal(dr["ACT_BATCH_QTY"]);

                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);


                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);

                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);

                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.FAB_GROUP = (dr["FAB_GROUP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_GROUP"]);
                    // ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);

                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);

                    //Trims & Woven
                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.RQD_QTY_YDS = (dr["RQD_QTY_YDS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY_YDS"]);
                    ob.RQD_QTY_KG = (dr["RQD_QTY_KG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY_KG"]);
                    ob.DYE_BT_CARD_D_TRMS_ID = (dr["DYE_BT_CARD_D_TRMS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_D_TRMS_ID"]);
                    //Trims & Woven

                    ob.IS_FLAT_CIR = (dr["IS_FLAT_CIR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FLAT_CIR"]);

                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);



                    if (ob.IS_FLAT_CIR == "F")
                    {
                        ob.col_cu_reqs = new SIZE_QTY_VIEWMODEL().getColCufReqCalData(
                            ob.MC_FAB_PROD_ORD_H_ID,
                            ob.RF_GARM_PART_ID,
                            ob.BODY_TOTAL,
                            ob.REQ_BODY,
                            pFAB_COLOR_ID

                        );
                    }

                    if (ob.IS_FLAT_CIR == "C")
                    {

                        ob.fin_dias = new DYE_BT_PLAN_FIN_DIAModel().Query(3002, pINV_ITEM_CAT_ID, ob.RF_FAB_TYPE_ID, ob.RF_FIB_COMP_ID, pFAB_COLOR_ID, ob.MC_FAB_PROD_ORD_H_ID, -1);

                        ob.non_col_cu_avail = new NON_COL_CUF_A_VIEWMODEL().getNoColCufAvailabilityData(ob.MC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID, ob.RF_FAB_TYPE_ID, ob.DYE_BT_CARD_H_ID, ob.RF_FIB_COMP_ID, ob.DYE_BT_CARD_GRP_ID, "N");
                    }

                    if (ob.IS_FLAT_CIR == "F")
                    {
                        ob.col_cu_avail = new COL_CUF_A_VIEWMODEL().getColCufAvailabilityData(ob.MC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID, ob.RF_FAB_TYPE_ID, ob.RF_GARM_PART_ID, ob.MC_STYLE_H_ID, ob.col_cu_reqs, ob.DYE_BT_CARD_H_ID, ob.DYE_BT_CARD_GRP_ID);
                    }


                    if (pINV_ITEM_CAT_ID == 7 || pINV_ITEM_CAT_ID == 35)
                    {
                        ob.woven_trims_avai = new MC_TRMS_DY_PRODModel().FabReqStockBalance(ob.MC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID, pINV_ITEM_CAT_ID);
                    }
                    ob.INV_ITEM_CAT_ID = pINV_ITEM_CAT_ID ?? 0;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DYE_BATCH_PLANModel> GetDataFabReqCalForProgram(string pMC_FAB_PROD_ORD_H_LST, long pFAB_COLOR_ID)
        {
            string sp = "pkg_DYE_BT_FAB_REQ.dye_batch_select_req_cal";
            try
            {
                var obList = new List<DYE_BATCH_PLANModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_LST", Value = pMC_FAB_PROD_ORD_H_LST},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = pFAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BATCH_PLANModel ob = new DYE_BATCH_PLANModel();



                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);

                    ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);

                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);

                    ob.MC_FAB_PROD_ORD_H_SPAN = (dr["MC_FAB_PROD_ORD_H_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_SPAN"]);
                    ob.MC_FAB_PROD_ORD_H_SL = (dr["MC_FAB_PROD_ORD_H_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_SL"]);

                    ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);

                    ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);


                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);

                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);

                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.FAB_GROUP = (dr["FAB_GROUP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_GROUP"]);

                    ob.IS_FLAT_CIR = (dr["IS_FLAT_CIR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FLAT_CIR"]);

                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);

                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.FINISH_TYPE_TXT = (dr["FINISH_TYPE_TXT"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FINISH_TYPE_TXT"]);
                    ob.WASH_TYPE_TXT = (dr["WASH_TYPE_TXT"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["WASH_TYPE_TXT"]);

                    if (ob.IS_FLAT_CIR == "C")
                    {
                        ob.non_col_cu_avail = new NON_COL_CUF_A_VIEWMODEL().getNoColCufAvailabilityData(ob.MC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID, ob.RF_FAB_TYPE_ID, -1, ob.RF_FIB_COMP_ID, -1, "N");
                    }

                    if (ob.IS_FLAT_CIR == "F")
                    {
                        ob.col_cu_avail = new COL_CUF_A_VIEWMODEL().getColCufAvailabilityData(ob.MC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID, ob.RF_FAB_TYPE_ID, ob.RF_GARM_PART_ID, ob.MC_STYLE_H_ID, ob.col_cu_reqs, -1, -1);
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


        public List<DYE_BATCH_PLANModel> GetDataFabReqCalForProgramWithTrims(string pMC_FAB_PROD_ORD_H_LST, long pFAB_COLOR_ID)
        {
            string sp = "pkg_DYE_BT_FAB_REQ.dye_batch_select_req_cal";
            try
            {
                var obList = new List<DYE_BATCH_PLANModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_LST", Value = pMC_FAB_PROD_ORD_H_LST},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = pFAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BATCH_PLANModel ob = new DYE_BATCH_PLANModel();

                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);

                    ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);

                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);

                    ob.MC_FAB_PROD_ORD_H_SPAN = (dr["MC_FAB_PROD_ORD_H_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_SPAN"]);
                    ob.MC_FAB_PROD_ORD_H_SL = (dr["MC_FAB_PROD_ORD_H_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_SL"]);

                    ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);

                    ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);


                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);

                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);

                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.FAB_GROUP = (dr["FAB_GROUP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_GROUP"]);

                    ob.IS_FLAT_CIR = (dr["IS_FLAT_CIR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FLAT_CIR"]);

                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);

                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);


                    if (ob.IS_FLAT_CIR == "C")
                    {
                        ob.non_col_cu_avail = new NON_COL_CUF_A_VIEWMODEL().getNoColCufAvailabilityData(ob.MC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID, ob.RF_FAB_TYPE_ID, -1, ob.RF_FIB_COMP_ID, -1, "N");
                    }

                    if (ob.IS_FLAT_CIR == "F")
                    {
                        ob.col_cu_avail = new COL_CUF_A_VIEWMODEL().getColCufAvailabilityData(ob.MC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID, ob.RF_FAB_TYPE_ID, ob.RF_GARM_PART_ID, ob.MC_STYLE_H_ID, ob.col_cu_reqs, -1, -1);
                    }

                    obList.Add(ob);
                }

                DYE_BATCH_PLANModel ob2 = new DYE_BATCH_PLANModel();
                ob2.IS_FLAT_CIR = "T";
                ob2.FAB_GROUP = "Trims & Accessories";
                ob2.trims_avail = new MC_TRMS_DY_PRODModel().QueryStockBalance(pMC_FAB_PROD_ORD_H_LST, pFAB_COLOR_ID, 7);
                if (ob2.trims_avail.Count > 0)
                    obList.Add(ob2);
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DYE_BATCH_PLANModel> GetDataFabReqCalForProgramScP(string pMC_FAB_PROD_ORD_H_LST, long pFAB_COLOR_ID)
        {
            string sp = "pkg_DYE_BT_FAB_REQ.dye_batch_select_req_cal_scp";
            try
            {
                var obList = new List<DYE_BATCH_PLANModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_LST", Value = pMC_FAB_PROD_ORD_H_LST},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = pFAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BATCH_PLANModel ob = new DYE_BATCH_PLANModel();



                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);

                    ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);

                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);

                    ob.MC_FAB_PROD_ORD_H_SPAN = (dr["MC_FAB_PROD_ORD_H_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_SPAN"]);
                    ob.MC_FAB_PROD_ORD_H_SL = (dr["MC_FAB_PROD_ORD_H_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_SL"]);

                    ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);

                    ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);


                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);

                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);

                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.FAB_GROUP = (dr["FAB_GROUP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_GROUP"]);

                    ob.IS_FLAT_CIR = (dr["IS_FLAT_CIR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FLAT_CIR"]);

                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);

                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);


                    if (ob.IS_FLAT_CIR == "C")
                    {
                        ob.fin_dias = new DYE_BT_PLAN_FIN_DIAModel().Query(3002, 34, ob.RF_FAB_TYPE_ID, ob.RF_FIB_COMP_ID, pFAB_COLOR_ID, ob.MC_FAB_PROD_ORD_H_ID, -1);
                        ob.non_col_cu_avail = new NON_COL_CUF_A_VIEWMODEL().getNoColCufAvailabilityData(ob.MC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID, ob.RF_FAB_TYPE_ID, -1, ob.RF_FIB_COMP_ID, -1, "N");
                    }

                    if (ob.IS_FLAT_CIR == "F")
                    {
                        ob.col_cu_avail = new COL_CUF_A_VIEWMODEL().getColCufAvailabilityData(ob.MC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID, ob.RF_FAB_TYPE_ID, ob.RF_GARM_PART_ID, ob.MC_STYLE_H_ID, ob.col_cu_reqs, -1, -1);
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

        public decimal TOTAL { get; set; }

        public decimal NET_GFAB_QTY { get; set; }

        public string FAB_GROUP { get; set; }

        public long MC_FAB_PROD_ORD_D_ID { get; set; }

        public string FIN_DIA { get; set; }

        public string IS_FLAT_CIR { get; set; }

        public long RF_GARM_PART_ID { get; set; }

        public decimal BODY_TOTAL { get; set; }

        public decimal REQ_BODY { get; set; }

        public long MC_STYLE_D_FAB_ID { get; set; }

        public long IS_BATCH_CREATED { get; set; }
        public long MC_STYLE_H_ID { get; set; }
        public long MC_FAB_PROD_ORD_H_SPAN { get; set; }
        public long MC_FAB_PROD_ORD_H_SL { get; set; }

        public string LD_RECIPE_NO { get; set; }

        public string DYE_BATCH_NO_LST { get; set; }

        public decimal ACT_BATCH_QTY { get; set; }

        public string DYE_BATCH_NO { get; set; }

        public long DYE_BT_CARD_H_ID { get; set; }

        public bool IS_SELECTED_BT { get; set; }

        public Int64 RF_ACTN_TYPE_ID { get; set; }
        public string PREFIX { get; set; }
        public Int64 RF_REQ_TYPE_ID { get; set; }
        public long DYE_BT_CARD_GRP_ID { get; set; }
        public long LK_FBR_GRP_ID { get; set; }
        public string DYE_LOT_NO { get; set; }
        public string FIB_COMP_NAME { get; set; }
        public long RF_FIB_COMP_ID { get; set; }
        public long INV_ITEM_CAT_ID { get; set; }

        public string ITEM_SPEC_AUTO { get; set; }

        public decimal RQD_QTY_YDS { get; set; }

        public decimal RQD_QTY_KG { get; set; }

        public long DYE_BT_CARD_D_TRMS_ID { get; set; }

        public string Delete()
        {
            const string sp = "pkg_DYE_BT_FAB_REQ.delete_program";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_PLAN_ID", Value = ob.DYE_BATCH_PLAN_ID},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public string XML_DF { get; set; }
        public long LEFT_TO_REQ { get; set; }
        public string FINISH_TYPE_TXT { get; set; }
        public string WASH_TYPE_TXT { get; set; }
        public long? LK_DIA_TYPE_ID { get; set; }
        public string RQD_GSM { get; set; }
        public long MC_BLK_FAB_REQ_H_ID { get; set; }
        public long MC_BLK_REVISION_NO { get; set; }
        public long MC_STYLE_H_EXT_ID { get; set; }
        public long MC_BYR_ACC_GRP_ID { get; set; }
        public long RF_FAB_PROD_CAT_ID { get; set; }

        public string IS_LOT_MIX { get; set; }

        public Int64? OTP_CODE { get; set; }

        public string IS_FAB_REQ_DONE { get; set; }
    }

    public class SIZE_QTY_VIEWMODEL
    {
        public string SIZE_CODE { get; set; }
        public string MESUREMENT { get; set; }
        public Int64 RQD_PC_QTY { get; set; }
        public Int64 MC_CLCF_ORD_REQ_ID { get; set; }

        public List<SIZE_QTY_VIEWMODEL> getColCufReqCalData(Int64 pMC_FAB_PROD_ORD_H_ID, Int64 pRF_GARM_PART_ID, Decimal pBODY_TOTAL, Decimal pREQ_BODY, Int64 pFAB_COLOR_ID)
        {
            string sp = "pkg_DYE_BT_FAB_REQ.dye_batch_col_cuff";
            try
            {
                var obList = new List<SIZE_QTY_VIEWMODEL>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_ID", Value = pRF_GARM_PART_ID},
                     new CommandParameter() {ParameterName = "pBODY_TOTAL", Value = pBODY_TOTAL},
                     new CommandParameter() {ParameterName = "pREQ_BODY", Value = pREQ_BODY},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = pFAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SIZE_QTY_VIEWMODEL ob = new SIZE_QTY_VIEWMODEL();
                    ob.RQD_PC_QTY = (dr["RQD_PC_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_PC_QTY"]);
                    ob.MC_CLCF_ORD_REQ_ID = (dr["MC_CLCF_ORD_REQ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_CLCF_ORD_REQ_ID"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.MESUREMENT = (dr["MESUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MESUREMENT"]);
                    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public long MC_SIZE_ID { get; set; }
    }

    public class NON_COL_CUF_A_VIEWMODEL
    {
        public string FIN_DIA { get; set; }
        public string FIN_GSM { get; set; }
        public Int64 KNT_STYL_FAB_ITEM_ID { get; set; }
        public String KNT_YRN_LOT_LST { get; set; }
        public Decimal RCV_ROLL_WT { get; set; }
        public decimal ASSIGN_QTY { get; set; }
        public decimal RQD_QTY { get; set; }
        public long NO_OF_ROLL { get; set; }

        public long DYE_BT_CARD_D_FAB_ID { get; set; }
        public long INV_ORD_GFAB_STK_ID { get; set; }
        public long DYE_BT_CARD_GRP_ID { get; set; }
        public long KNT_YRN_LOT_ID { get; set; }
        public decimal TTL_ISS_QTY { get; set; }

        public List<NON_COL_CUF_A_VIEWMODEL> getNoColCufAvailabilityData(
            Int64 pMC_FAB_PROD_ORD_H_ID,
            Int64 pFAB_COLOR_ID,
            Int64 pRF_FAB_TYPE_ID,
            Int64 pDYE_BT_CARD_H_ID,
            Int64 pRF_FIB_COMP_ID,
            Int64 pDYE_BT_CARD_GRP_ID,
            string pIS_BATCH_STORE,
            Int64? pLK_DIA_TYPE_ID = null
        )
        {
            string sp = "pkg_DYE_BT_FAB_REQ.dye_bt_non_col_cuff_avail";
            try
            {
                var obList = new List<NON_COL_CUF_A_VIEWMODEL>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = pFAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = pRF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = pDYE_BT_CARD_H_ID},

                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = pRF_FIB_COMP_ID},

                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_GRP_ID", Value = pDYE_BT_CARD_GRP_ID},
                     new CommandParameter() {ParameterName = "pIS_BATCH_STORE", Value = pIS_BATCH_STORE},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = pLK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    NON_COL_CUF_A_VIEWMODEL ob = new NON_COL_CUF_A_VIEWMODEL();
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);

                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);

                    ob.INV_ORD_GFAB_STK_ID = (dr["INV_ORD_GFAB_STK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ORD_GFAB_STK_ID"]);
                    ob.RCV_ROLL_WT = (dr["RCV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_ROLL_WT"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);
                    ob.JOB_CRD_NO = (dr["JOB_CRD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_CRD_NO"]);
                    
                    ob.DYE_BT_CARD_D_FAB_ID = (dr["DYE_BT_CARD_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_D_FAB_ID"]);
                    ob.ASSIGN_QTY = (dr["ASSIGN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ASSIGN_QTY"]);
                    ob.RQD_ROLL_QTY = (dr["RQD_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["RQD_ROLL_QTY"]);

                    ob.DYE_BT_CARD_GRP_ID = pDYE_BT_CARD_GRP_ID;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<NON_COL_CUF_A_VIEWMODEL> getItemDataByBtGrp(
                Int64 pDYE_BT_CARD_GRP_ID,
                Int64 pDYE_GSTR_ISS_H_ID
        )
        {
            string sp = "pkg_DYE_BT_FAB_REQ.dye_bt_non_col_cuff_avail";
            try
            {
                var obList = new List<NON_COL_CUF_A_VIEWMODEL>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_GRP_ID", Value = pDYE_BT_CARD_GRP_ID},
                     new CommandParameter() {ParameterName = "pDYE_GSTR_ISS_H_ID", Value = pDYE_GSTR_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    NON_COL_CUF_A_VIEWMODEL ob = new NON_COL_CUF_A_VIEWMODEL();
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.RCV_ROLL_WT = (dr["RCV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_ROLL_WT"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                    ob.NO_OF_ROLL = (dr["NO_OF_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_ROLL"]);

                    ob.TTL_ISS_QTY = (dr["TTL_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_ISS_QTY"]);



                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<NON_COL_CUF_A_VIEWMODEL> getItemDataByPreTreatment(Int64 pKNT_STYL_FAB_ITEM_ID, Int64 pDYE_GSTR_REQ_H_ID)
        {
            string sp = "pkg_DYE_BT_FAB_REQ.dye_bt_non_col_cuff_avail";
            try
            {
                var obList = new List<NON_COL_CUF_A_VIEWMODEL>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_ID", Value = pKNT_STYL_FAB_ITEM_ID},
                     new CommandParameter() {ParameterName = "pDYE_GSTR_REQ_H_ID", Value = pDYE_GSTR_REQ_H_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    NON_COL_CUF_A_VIEWMODEL ob = new NON_COL_CUF_A_VIEWMODEL();
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.RCV_ROLL_WT = (dr["RCV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_ROLL_WT"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                    ob.NO_OF_ROLL = (dr["NO_OF_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_ROLL"]);

                    ob.TTL_ISS_QTY = (dr["TTL_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_ISS_QTY"]);

                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<NON_COL_CUF_A_VIEWMODEL> CheckIsRollIssuable(Int64? pDYE_BT_CARD_GRP_ID, string pFAB_ROLL_NO, Int64? pKNT_STYL_FAB_ITEM_ID, Int64? pDYE_GSTR_REQ_H_ID, Int64? pOption = null)
        {
            string sp = "pkg_DYE_BT_FAB_REQ.dye_bt_non_col_cuff_avail";
            try
            {
                string jsonStr = "";
                int i = 0;
                var obList = new List<NON_COL_CUF_A_VIEWMODEL>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_GRP_ID", Value = pDYE_BT_CARD_GRP_ID},
                     new CommandParameter() {ParameterName = "pDYE_GSTR_REQ_H_ID", Value = pDYE_GSTR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_ID", Value = pKNT_STYL_FAB_ITEM_ID},
                     new CommandParameter() {ParameterName = "pFAB_ROLL_NO", Value = pFAB_ROLL_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption==null?3003:3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    NON_COL_CUF_A_VIEWMODEL ob = new NON_COL_CUF_A_VIEWMODEL();
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    obList.Add(ob);
                }
                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    NON_COL_CUF_A_VIEWMODEL ob = new NON_COL_CUF_A_VIEWMODEL();
                    jsonStr += (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                    if (i < ds.Tables["OUTPARAM"].Rows.Count)
                    {
                        jsonStr += ",";
                    }
                    else
                    {
                        jsonStr += "}";
                    }
                    i++;

                    ob.MC_ORDER_NO_LST = jsonStr;
                    ob.KNT_STYL_FAB_ITEM_ID = 0;
                    if (obList.Count > 0)
                    {
                        obList[0].MC_ORDER_NO_LST = jsonStr;
                    }
                    else
                        obList.Add(ob);

                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int RQD_ROLL_QTY { get; set; }

        public string MC_ORDER_NO_LST { get; set; }

        public string COLOR_NAME_EN { get; set; }

        public long RF_FAB_TYPE_ID { get; set; }

        public long RF_FIB_COMP_ID { get; set; }

        public long LK_DIA_TYPE_ID { get; set; }

        public string JOB_CRD_NO { get; set; }
    }

    public class COL_CUF_A_VIEWMODEL
    {
        public string SIZE_CODE { get; set; }
        public string MESUREMENT { get; set; }
        public Int64 KNT_CLCF_STYL_ITEM_ID { get; set; }
        public Int64 PRD_QTY { get; set; }
        public Decimal RQD_QTY { get; set; }
        public decimal? ISS_QTY { get; set; }
        public decimal ISS_QTY_KG { get; set; }

        public long MC_SIZE_ID { get; set; }
        public long? ASSIGN_QTY { get; set; }
        public long DYE_BT_CARD_D_CLCF_ID { get; set; }
        public long DYE_BT_CARD_GRP_ID { get; set; }
        public string XML { get; set; }
        public long DYE_GSTR_BT_ISS_D2_ID { get; set; }
        public string KNT_YRN_LOT_LST { get; set; }


        public List<COL_CUF_A_VIEWMODEL> getColCufAvailabilityData(Int64 pMC_FAB_PROD_ORD_H_ID, Int64 pFAB_COLOR_ID, Int64 pRF_FAB_TYPE_ID, Int64 pRF_GARM_PART_ID, Int64 pMC_STYLE_H_ID, List<SIZE_QTY_VIEWMODEL> reqs, Int64 pDYE_BT_CARD_H_ID, Int64 pDYE_BT_CARD_GRP_ID)
        {
            string sp = "pkg_DYE_BT_FAB_REQ.dye_bt_non_col_cuff_avail";
            try
            {
                var obList = new List<COL_CUF_A_VIEWMODEL>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = pFAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = pRF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_ID", Value = pRF_GARM_PART_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = pDYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_GRP_ID", Value = pDYE_BT_CARD_GRP_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    COL_CUF_A_VIEWMODEL ob = new COL_CUF_A_VIEWMODEL();
                    ob.KNT_CLCF_STYL_ITEM_ID = (dr["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STYL_ITEM_ID"]);
                    ob.DYE_BT_CARD_GRP_ID = pDYE_BT_CARD_GRP_ID;
                    ob.MESUREMENT = (dr["MESUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MESUREMENT"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);

                    ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);

                    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                    ob.PRD_QTY = (dr["PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRD_QTY"]);
                    ob.ASSIGN_QTY = (dr["ASSIGN_QTY"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["ASSIGN_QTY"]);
                    ob.DYE_BT_CARD_D_CLCF_ID = (dr["DYE_BT_CARD_D_CLCF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_D_CLCF_ID"]);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<COL_CUF_A_VIEWMODEL> getColCufAvailabilityDataByGrop(Int64 pDYE_BT_CARD_GRP_ID, Int64 pDYE_GSTR_ISS_H_ID)
        {
            string sp = "pkg_DYE_BT_FAB_REQ.dye_bt_non_col_cuff_avail";
            try
            {
                var obList = new List<COL_CUF_A_VIEWMODEL>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_GRP_ID", Value = pDYE_BT_CARD_GRP_ID},
                     new CommandParameter() {ParameterName = "pDYE_GSTR_ISS_H_ID", Value = pDYE_GSTR_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    COL_CUF_A_VIEWMODEL ob = new COL_CUF_A_VIEWMODEL();
                    ob.KNT_CLCF_STYL_ITEM_ID = (dr["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STYL_ITEM_ID"]);
                    ob.MESUREMENT = (dr["MESUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MESUREMENT"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                    ob.RQD_QTY_KG = (dr["RQD_QTY_KG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY_KG"]);
                    
                    ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);

                    if (dr["ISS_QTY"] != DBNull.Value)
                    {
                        ob.ISS_QTY = Convert.ToDecimal(dr["ISS_QTY"]);
                    }

                    ob.ISS_QTY_KG = (dr["ISS_QTY_KG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_QTY_KG"]);

                    ob.TTL_ISS_QTY = (dr["TTL_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_ISS_QTY"]);
                    ob.TTL_ISS_QTY_KG = (dr["ISS_QTY_KG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_QTY_KG"]);

                    ob.STK_BALANCE = (dr["STK_BALANCE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_BALANCE"]);
                    ob.STK_QTY = (dr["STK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_QTY"]);
                    
                    ob.DYE_BT_CARD_D_CLCF_ID = (dr["DYE_BT_CARD_D_CLCF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_D_CLCF_ID"]);
                    ob.DYE_GSTR_BT_ISS_D2_ID = (dr["DYE_GSTR_BT_ISS_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_BT_ISS_D2_ID"]);

                    ob.NO_OF_ROLL = (dr["NO_OF_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_ROLL"]);
                    ob.HOLE_NO = (dr["HOLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HOLE_NO"]);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SaveColCuffIssueData()
        {
            const string sp = "PKG_DYE_GFAB_REQ.knt_btc_roll_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_GSTR_ISS_H_ID", Value = ob.DYE_GSTR_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = ob.LK_FBR_GRP_ID},
                     
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1001},
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





        public Int64 DYE_BT_CARD_H_ID { get; set; }

        public Int64? DYE_GSTR_ISS_H_ID { get; set; }

        public decimal TTL_ISS_QTY_KG { get; set; }

        public decimal TTL_ISS_QTY { get; set; }
        public long NO_OF_ROLL { get; set; }

        public decimal STK_BALANCE { get; set; }

        public Int64? LK_FBR_GRP_ID { get; set; }


        public string HOLE_NO { get; set; }

        public decimal STK_QTY { get; set; }

        public decimal RQD_QTY_KG { get; set; }
    }

}