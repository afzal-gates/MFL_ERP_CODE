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
    public class MC_ACCS_PO_TMPLT_CFGModel
    {
        public Int64 MC_ACCS_PO_TMPLT_CFG_ID { get; set; }
        public string CNTRL_NAME { get; set; }
        public string CNTRL_TYPE { get; set; }
        public string CNTRL_LABEL { get; set; }
        public string IS_DATA_FETCH { get; set; }
        public string FETCH_URL { get; set; }
        public string OPTION_VAL { get; set; }
        public string IS_REQUIRED { get; set; }
        public Int64 MIN_VAL { get; set; }
        public Int64 MAX_VAL { get; set; }
        public Int64 MINLENGTH { get; set; }
        public Int64 MAXLENGTH { get; set; }
        public string COL_VAL { get; set; }
        public string FIELD_TYPE { get; set; }
        public Decimal FIELD_SIZE { get; set; }

        public Int64 MC_ACCS_PO_TMPLT_H_ID { get; set; }
        public Int64 MC_MAP_ACCS_PO_TMPLT_ID { get; set; }
        public string IS_CHECKED { get; set; }
        public string Save()
        {
            const string sp = "SP_MC_ACCS_PO_TMPLT_CFG";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_TMPLT_CFG_ID", Value = ob.MC_ACCS_PO_TMPLT_CFG_ID},
                     new CommandParameter() {ParameterName = "pCNTRL_NAME", Value = ob.CNTRL_NAME},
                     new CommandParameter() {ParameterName = "pCNTRL_TYPE", Value = ob.CNTRL_TYPE},
                     new CommandParameter() {ParameterName = "pCNTRL_LABEL", Value = ob.CNTRL_LABEL},
                     new CommandParameter() {ParameterName = "pIS_DATA_FETCH", Value = ob.IS_DATA_FETCH},
                     new CommandParameter() {ParameterName = "pFETCH_URL", Value = ob.FETCH_URL},
                     new CommandParameter() {ParameterName = "pOPTION_VAL", Value = ob.OPTION_VAL},
                     new CommandParameter() {ParameterName = "pIS_REQUIRED", Value = ob.IS_REQUIRED},
                     new CommandParameter() {ParameterName = "pMIN_VAL", Value = ob.MIN_VAL},
                     new CommandParameter() {ParameterName = "pMAX_VAL", Value = ob.MAX_VAL},
                     new CommandParameter() {ParameterName = "pMINLENGTH", Value = ob.MINLENGTH},
                     new CommandParameter() {ParameterName = "pMAXLENGTH", Value = ob.MAXLENGTH},
                     new CommandParameter() {ParameterName = "pCOL_VAL", Value = ob.COL_VAL},
                     new CommandParameter() {ParameterName = "pFIELD_TYPE", Value = ob.FIELD_TYPE},
                     new CommandParameter() {ParameterName = "pFIELD_SIZE", Value = ob.FIELD_SIZE},
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
        public List<MC_ACCS_PO_TMPLT_CFGModel> getTempControllsData(Int64? pMC_ACCS_PO_TMPLT_H_ID = null)
        {
            string sp = "pkg_acc_booking.mc_accs_po_tmplt_cfg_select";
            try
            {
                var obList = new List<MC_ACCS_PO_TMPLT_CFGModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_TMPLT_H_ID", Value = pMC_ACCS_PO_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ACCS_PO_TMPLT_CFGModel ob = new MC_ACCS_PO_TMPLT_CFGModel();
                    ob.MC_ACCS_PO_TMPLT_CFG_ID = (dr["MC_ACCS_PO_TMPLT_CFG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ACCS_PO_TMPLT_CFG_ID"]);
                    ob.CNTRL_NAME = (dr["CNTRL_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CNTRL_NAME"]);
                    ob.CNTRL_TYPE = (dr["CNTRL_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CNTRL_TYPE"]);
                    ob.CNTRL_LABEL = (dr["CNTRL_LABEL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CNTRL_LABEL"]);
                    ob.IS_DATA_FETCH = (dr["IS_DATA_FETCH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DATA_FETCH"]);
                    ob.FETCH_URL = (dr["FETCH_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FETCH_URL"]);
                    ob.OPTION_VAL = (dr["OPTION_VAL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPTION_VAL"]);
                    ob.IS_REQUIRED = (dr["IS_REQUIRED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REQUIRED"]);
                    ob.MIN_VAL = (dr["MIN_VAL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MIN_VAL"]);
                    ob.MAX_VAL = (dr["MAX_VAL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_VAL"]);
                    ob.MINLENGTH = (dr["MINLENGTH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MINLENGTH"]);
                    ob.MAXLENGTH = (dr["MAXLENGTH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAXLENGTH"]);
                    ob.COL_VAL = (dr["COL_VAL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COL_VAL"]);
                    ob.FIELD_TYPE = (dr["FIELD_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIELD_TYPE"]);
                    ob.FIELD_SIZE = (dr["FIELD_SIZE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FIELD_SIZE"]);
                    ob.MC_MAP_ACCS_PO_TMPLT_ID = (dr["MC_MAP_ACCS_PO_TMPLT_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["MC_MAP_ACCS_PO_TMPLT_ID"]); //New Treated As -1
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);

                    if (dr["PCT_WIDTH"] != DBNull.Value)
                    {
                        ob.PCT_WIDTH = Convert.ToInt64(dr["PCT_WIDTH"]);
                    }
                    ob.IS_CHECKED = (dr["IS_CHECKED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_CHECKED"]);

                   
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_ACCS_PO_TMPLT_CFGModel> getContrlsByItemNBuyer(Int64 pINV_ITEM_ID,Int64 pMC_STYLE_H_ID, Int64? pMC_BUYER_ID = null, Int64? pMC_STYL_BGT_H_ID = null, Int64? pMC_ACCS_PO_H_ID = null,
            Int64? pINV_ITEM_CAT_ID = null, string pNEED_OPT="Y"
            )
        {
            string sp = "pkg_acc_booking.mc_accs_po_tmplt_d_select";
            try
            {
                var obList = new List<MC_ACCS_PO_TMPLT_CFGModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = pINV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pMC_STYL_BGT_H_ID", Value = pMC_STYL_BGT_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_H_ID", Value = pMC_ACCS_PO_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = pINV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pNEED_OPT", Value = pNEED_OPT},

                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ACCS_PO_TMPLT_CFGModel ob = new MC_ACCS_PO_TMPLT_CFGModel();
                    ob.MC_ACCS_PO_TMPLT_CFG_ID = (dr["MC_ACCS_PO_TMPLT_CFG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ACCS_PO_TMPLT_CFG_ID"]);
                    ob.CNTRL_NAME = (dr["CNTRL_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CNTRL_NAME"]);
                    ob.CNTRL_TYPE = (dr["CNTRL_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CNTRL_TYPE"]);
                    ob.CNTRL_LABEL = (dr["CNTRL_LABEL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CNTRL_LABEL"]);
                    ob.IS_DATA_FETCH = (dr["IS_DATA_FETCH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DATA_FETCH"]);
                    ob.FETCH_URL = (dr["FETCH_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FETCH_URL"]);
                    ob.OPTION_VAL = (dr["OPTION_VAL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPTION_VAL"]);
                    ob.IS_REQUIRED = (dr["IS_REQUIRED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REQUIRED"]);
                    ob.MIN_VAL = (dr["MIN_VAL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MIN_VAL"]);
                    ob.MAX_VAL = (dr["MAX_VAL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_VAL"]);
                    ob.MINLENGTH = (dr["MINLENGTH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MINLENGTH"]);
                    ob.MAXLENGTH = (dr["MAXLENGTH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAXLENGTH"]);
                    ob.COL_VAL = (dr["COL_VAL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COL_VAL"]);
                    ob.FIELD_TYPE = (dr["FIELD_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIELD_TYPE"]);
                    ob.FIELD_SIZE = (dr["FIELD_SIZE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FIELD_SIZE"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);

                    //ob.MC_ACCS_PO_TMPLT_H_ID = (dr["MC_ACCS_PO_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ACCS_PO_TMPLT_H_ID"]);
                    //ob.MC_ACCS_PO_TMPLT_D_ID = (dr["MC_ACCS_PO_TMPLT_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ACCS_PO_TMPLT_D_ID"]);
                    
                    ob.PCT_WIDTH = (dr["PCT_WIDTH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCT_WIDTH"]);

                    ob.OPTION_VAL_ALT = (dr["OPTION_VAL_ALT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPTION_VAL_ALT"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public MC_ACCS_PO_TMPLT_CFGModel Select(long ID)
        {
            string sp = "Select_MC_ACCS_PO_TMPLT_CFG";
            try
            {
                var ob = new MC_ACCS_PO_TMPLT_CFGModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_TMPLT_CFG_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_ACCS_PO_TMPLT_CFG_ID = (dr["MC_ACCS_PO_TMPLT_CFG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ACCS_PO_TMPLT_CFG_ID"]);
                    ob.CNTRL_NAME = (dr["CNTRL_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CNTRL_NAME"]);
                    ob.CNTRL_TYPE = (dr["CNTRL_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CNTRL_TYPE"]);
                    ob.CNTRL_LABEL = (dr["CNTRL_LABEL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CNTRL_LABEL"]);
                    ob.IS_DATA_FETCH = (dr["IS_DATA_FETCH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DATA_FETCH"]);
                    ob.FETCH_URL = (dr["FETCH_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FETCH_URL"]);
                    ob.OPTION_VAL = (dr["OPTION_VAL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPTION_VAL"]);
                    ob.IS_REQUIRED = (dr["IS_REQUIRED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REQUIRED"]);
                    ob.MIN_VAL = (dr["MIN_VAL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MIN_VAL"]);
                    ob.MAX_VAL = (dr["MAX_VAL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_VAL"]);
                    ob.MINLENGTH = (dr["MINLENGTH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MINLENGTH"]);
                    ob.MAXLENGTH = (dr["MAXLENGTH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAXLENGTH"]);
                    ob.COL_VAL = (dr["COL_VAL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COL_VAL"]);
                    ob.FIELD_TYPE = (dr["FIELD_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIELD_TYPE"]);
                    ob.FIELD_SIZE = (dr["FIELD_SIZE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FIELD_SIZE"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Int64 DISPLAY_ORDER { get; set; }
        public Int64? PCT_WIDTH { get; set; }

        public string OPTION_VAL_ALT { get; set; }

        public long MC_ACCS_PO_TMPLT_D_ID { get; set; }
    }

    public class INV_ITEM_DP_M
    {
        public Int64? INV_ITEM_CAT_ID { get; set; }
        public Int64? INV_ITEM_ID { get; set; }
        public String ITEM_NAME_EN { get; set; }
        public string IS_LEAF { get; set; }

        public string ITEM_CAT_NAME_EN { get; set; }


    }

    public class BUYER_DP_M
    {
        public Int64? MC_BUYER_ID { get; set; }
        public String BUYER_NAME_EN { get; set; }

    }

    public class INV_ITEM_CAT_DP_M
    {
        public Int64? INV_ITEM_CAT_ID { get; set; }
        public String ITEM_CAT_NAME_EN { get; set; }

    }



    public class MC_ACCS_PO_TMPLT_DModel
    {
        public Int64 MC_ACCS_PO_TMPLT_D_ID { get; set; }
        public Int64 MC_ACCS_PO_TMPLT_H_ID { get; set; }
        public String BUYER_NAME_EN { get; set; }
        public String ITEM_NAME_EN { get; set; }
        public Int64? INV_ITEM_ID { get; set; }
        public Int64? MC_BUYER_ID { get; set; }

        public Int64? INV_ITEM_CAT_ID { get; set; }
        public String ITEM_CAT_NAME_EN { get; set; }

        public Int64? LOOKUP_DATA_ID { get; set; }
        public String LK_DATA_NAME_EN { get; set; }


        private INV_ITEM_CAT_DP_M _INV_ITEM_CAT_TYPE;
        public INV_ITEM_CAT_DP_M INV_ITEM_CAT_TYPE
        {
            get
            {
                _INV_ITEM_CAT_TYPE = new INV_ITEM_CAT_DP_M()
                {
                    INV_ITEM_CAT_ID = this.INV_ITEM_CAT_ID,
                    ITEM_CAT_NAME_EN = this.ITEM_CAT_NAME_EN ?? ""
                };
                return _INV_ITEM_CAT_TYPE;
            }
        }



        private INV_ITEM_DP_M _INV_ITEM_TYPE;
        public INV_ITEM_DP_M INV_ITEM_TYPE
        {
            get
            {
                _INV_ITEM_TYPE = new INV_ITEM_DP_M()
                {
                    INV_ITEM_ID = this.INV_ITEM_ID,
                    ITEM_NAME_EN = this.ITEM_NAME_EN ?? ""
                };
                return _INV_ITEM_TYPE;
            }
        }


        private BUYER_DP_M _BUYER_TYPE;
        public BUYER_DP_M BUYER_TYPE
        {
            get
            {
                _BUYER_TYPE = new BUYER_DP_M()
                {
                    MC_BUYER_ID = this.MC_BUYER_ID,
                    BUYER_NAME_EN = this.BUYER_NAME_EN ?? ""
                };
                return _BUYER_TYPE;
            }
        }


        private LOOKUP_DATA_VM _CONS_TYPE;
        public LOOKUP_DATA_VM CONS_TYPE
        {
            get
            {
                _CONS_TYPE = new LOOKUP_DATA_VM()
                {
                    LOOKUP_DATA_ID = (int) (this.LOOKUP_DATA_ID==null?0:this.LOOKUP_DATA_ID),
                    LK_DATA_NAME_EN = this.LK_DATA_NAME_EN ?? ""
                };
                return _CONS_TYPE;
            }
        }

        public List<INV_ITEM_DP_M> getTrim_n_AccItemList()
        {
            string sp = "pkg_acc_booking.mc_accs_po_tmplt_d_select";
            try
            {
                var obList = new List<INV_ITEM_DP_M>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEM_DP_M ob = new INV_ITEM_DP_M();
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_LEAF"]);

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                             {
                                new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value =ob.INV_ITEM_CAT_ID},
                                new CommandParameter() {ParameterName = "pIS_LEAF", Value =ob.IS_LEAF}, 
                                new CommandParameter() {ParameterName = "pOption", Value =3004},
                                new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                            }, sp);

                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        INV_ITEM_DP_M ob1 = new INV_ITEM_DP_M();
                        ob1.INV_ITEM_ID = (dr1["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["INV_ITEM_ID"]);
                        ob1.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        ob1.INV_ITEM_CAT_ID = (dr1["INV_ITEM_CAT_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr1["INV_ITEM_CAT_ID"]);
                        ob1.ITEM_CAT_NAME_EN = (dr1["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_CAT_NAME_EN"]);

                        obList.Add(ob1);
                    }
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string Save()
        {
            const string sp = "SP_MC_ACCS_PO_TMPLT_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_TMPLT_D_ID", Value = ob.MC_ACCS_PO_TMPLT_D_ID},
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_TMPLT_H_ID", Value = ob.MC_ACCS_PO_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
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

        public List<MC_ACCS_PO_TMPLT_DModel> getTempHrdDList(Int64 pMC_ACCS_PO_TMPLT_H_ID)
        {
            string sp = "pkg_acc_booking.mc_accs_po_tmplt_d_select";
            try
            {
                var obList = new List<MC_ACCS_PO_TMPLT_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_TMPLT_H_ID", Value = pMC_ACCS_PO_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ACCS_PO_TMPLT_DModel ob = new MC_ACCS_PO_TMPLT_DModel();
                    ob.MC_ACCS_PO_TMPLT_D_ID = (dr["MC_ACCS_PO_TMPLT_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ACCS_PO_TMPLT_D_ID"]);
                    ob.SL = (dr["SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SL"]);

                    if (dr["INV_ITEM_ID"] != DBNull.Value)
                    {
                        ob.INV_ITEM_ID = Convert.ToInt64(dr["INV_ITEM_ID"]);
                    }

                    if (dr["MC_BUYER_ID"] != DBNull.Value)
                    {
                        ob.MC_BUYER_ID = Convert.ToInt64(dr["MC_BUYER_ID"]);
                    }

                    if (dr["INV_ITEM_CAT_ID"] != DBNull.Value)
                    {
                        ob.INV_ITEM_CAT_ID = Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    }

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    

                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_ACCS_PO_TMPLT_DModel Select(long ID)
        {
            string sp = "Select_MC_ACCS_PO_TMPLT_D";
            try
            {
                var ob = new MC_ACCS_PO_TMPLT_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_TMPLT_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_ACCS_PO_TMPLT_D_ID = (dr["MC_ACCS_PO_TMPLT_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ACCS_PO_TMPLT_D_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long SL { get; set; }
    }
    public class MC_ACCS_PO_TMPLT_HModel
    {
        public Int64 MC_ACCS_PO_TMPLT_H_ID { get; set; }
        public string ACCS_PO_TMPLT_NAME { get; set; }

        private List<MC_ACCS_PO_TMPLT_DModel> _PO_TMPLT_D = null;
        public List<MC_ACCS_PO_TMPLT_DModel> PO_TMPLT_D
        {
            get
            {
                if (_PO_TMPLT_D == null)
                {
                    _PO_TMPLT_D = new List<MC_ACCS_PO_TMPLT_DModel>();
                }
                return _PO_TMPLT_D;
            }
            set
            {
                _PO_TMPLT_D = value;
            }
        }

        private List<MC_ACCS_PO_TMPLT_CFGModel> _PO_TMPLT_CFG = null;
        public List<MC_ACCS_PO_TMPLT_CFGModel> PO_TMPLT_CFG
        {
            get
            {
                if (_PO_TMPLT_CFG == null)
                {
                    _PO_TMPLT_CFG = new List<MC_ACCS_PO_TMPLT_CFGModel>();
                }
                return _PO_TMPLT_CFG;
            }
            set
            {
                _PO_TMPLT_CFG = value;
            }
        }

        public string PO_TMPLT_D_XML { get; set; }
        public string PO_TMPLT_CFG_XML { get; set; }

        public string Save()
        {
            const string sp = "pkg_acc_booking.mc_accs_po_tmplt_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_TMPLT_H_ID", Value = ob.MC_ACCS_PO_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pACCS_PO_TMPLT_NAME", Value = ob.ACCS_PO_TMPLT_NAME},
                     new CommandParameter() {ParameterName = "oPO_TMPLT_D_XML", Value = ob.PO_TMPLT_D_XML},
                     new CommandParameter() {ParameterName = "pPO_TMPLT_CFG_XML", Value = ob.PO_TMPLT_CFG_XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "opMC_ACCS_PO_TMPLT_H_ID", Value =0, Direction = ParameterDirection.Output},
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
        public List<MC_ACCS_PO_TMPLT_HModel> SelectAll()
        {
            string sp = "pkg_acc_booking.mc_accs_po_tmplt_h_select";
            try
            {
                var obList = new List<MC_ACCS_PO_TMPLT_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ACCS_PO_TMPLT_HModel ob = new MC_ACCS_PO_TMPLT_HModel();
                    ob.MC_ACCS_PO_TMPLT_H_ID = (dr["MC_ACCS_PO_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ACCS_PO_TMPLT_H_ID"]);
                    ob.ACCS_PO_TMPLT_NAME = (dr["ACCS_PO_TMPLT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACCS_PO_TMPLT_NAME"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_ACCS_PO_TMPLT_HModel Select( Int64 pMC_ACCS_PO_TMPLT_H)
        {
            string sp = "pkg_acc_booking.mc_accs_po_tmplt_h_select";
            try
            {
                var ob = new MC_ACCS_PO_TMPLT_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_TMPLT_H_ID", Value = pMC_ACCS_PO_TMPLT_H },
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_ACCS_PO_TMPLT_H_ID = (dr["MC_ACCS_PO_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ACCS_PO_TMPLT_H_ID"]);
                    ob.ACCS_PO_TMPLT_NAME = (dr["ACCS_PO_TMPLT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACCS_PO_TMPLT_NAME"]);
                    ob.PO_TMPLT_CFG = new MC_ACCS_PO_TMPLT_CFGModel().getTempControllsData(ob.MC_ACCS_PO_TMPLT_H_ID);
                    ob.PO_TMPLT_D = new MC_ACCS_PO_TMPLT_DModel().getTempHrdDList(ob.MC_ACCS_PO_TMPLT_H_ID);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SaveHrdData()
        {
            const string sp = "pkg_acc_booking.mc_accs_po_tmplt_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_TMPLT_H_ID", Value = ob.MC_ACCS_PO_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pACCS_PO_TMPLT_NAME", Value = ob.ACCS_PO_TMPLT_NAME},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

                     new CommandParameter() {ParameterName = "pOption", Value =1001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opMC_ACCS_PO_TMPLT_H_ID", Value =0, Direction = ParameterDirection.Output}
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