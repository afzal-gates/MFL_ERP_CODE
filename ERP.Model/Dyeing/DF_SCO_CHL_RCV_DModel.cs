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
    public class DF_SCO_CHL_RCV_DModel
    {
        public Int64 DF_SCO_CHL_RCV_D_ID { get; set; }
        public Int64 DF_SCO_CHL_RCV_H_ID { get; set; }
        public Int64 DF_SC_PRG_ISS_H_ID { get; set; }
        public Int64 DYE_BT_CARD_H_ID { get; set; }
        public Int64 KNT_STYL_FAB_ITEM_H_ID { get; set; }
        public Int64 RCV_ROLL_QTY { get; set; }
        public Decimal RCV_FIN_FAB_QTY { get; set; }
        public Decimal RCV_GREY_FAB_QTY { get; set; }
        public Decimal RTN_GREY_FAB_QTY { get; set; }
        public Decimal LOSS_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string NOTE { get; set; }
        public string IS_STORED { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Decimal PRE_RCV_QTY { get; set; }
        public Decimal BATCH_QTY { get; set; }

        public string DYE_BATCH_NO { get; set; }
        public string DYE_LOT_NO { get; set; }
        public string FAB_TYPE_NAME { get; set; }
        public string FIB_COMP_NAME { get; set; }
        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string ORDER_NO { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string FIN_DIA { get; set; }
        public string FIN_GSM { get; set; }
        public string LK_DIA_TYPE_NAME { get; set; }
        public bool IS_ACTIVE { get; set; }
        public long ISS_QTY { get; set; }
        public long DF_SC_BT_CHL_ISS_D_ID { get; set; }
        public string DF_PROC_TYPE_LST { get; set; }
        public string NXT_DF_PROC_TYPE_LST { get; set; }
        public long ISS_ROLL_QTY { get; set; }
        public string DF_PROC_TYPE_NAME_LIST { get; set; }
        public string CHALAN_NO { get; set; }
        public DateTime CHALAN_DT { get; set; }
        public string DIA_TYPE_NAME { get; set; }
        public string FAB_ITEM_DESC { get; set; }
        public long LK_DIA_TYPE_ID { get; set; }
        
        public List<DF_SCO_CHL_RCV_PROCModel> processTypeList { get; set; }

        public string Save()
        {
            const string sp = "SP_DF_SCO_CHL_RCV_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_CHL_RCV_D_ID", Value = ob.DF_SCO_CHL_RCV_D_ID},
                     new CommandParameter() {ParameterName = "pDF_SCO_CHL_RCV_H_ID", Value = ob.DF_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_H_ID", Value = ob.KNT_STYL_FAB_ITEM_H_ID},
                     new CommandParameter() {ParameterName = "pRCV_ROLL_QTY", Value = ob.RCV_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pRCV_FIN_FAB_QTY", Value = ob.RCV_FIN_FAB_QTY},
                     new CommandParameter() {ParameterName = "pLOSS_QTY", Value = ob.LOSS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pNOTE", Value = ob.NOTE},
                     new CommandParameter() {ParameterName = "pIS_STORED", Value = ob.IS_STORED},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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
            const string sp = "SP_DF_SCO_CHL_RCV_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_CHL_RCV_D_ID", Value = ob.DF_SCO_CHL_RCV_D_ID},
                     new CommandParameter() {ParameterName = "pDF_SCO_CHL_RCV_H_ID", Value = ob.DF_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_H_ID", Value = ob.KNT_STYL_FAB_ITEM_H_ID},
                     new CommandParameter() {ParameterName = "pRCV_ROLL_QTY", Value = ob.RCV_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pRCV_FIN_FAB_QTY", Value = ob.RCV_FIN_FAB_QTY},
                     new CommandParameter() {ParameterName = "pLOSS_QTY", Value = ob.LOSS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pNOTE", Value = ob.NOTE},
                     new CommandParameter() {ParameterName = "pIS_STORED", Value = ob.IS_STORED},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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
            const string sp = "SP_DF_SCO_CHL_RCV_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_CHL_RCV_D_ID", Value = ob.DF_SCO_CHL_RCV_D_ID},
                     new CommandParameter() {ParameterName = "pDF_SCO_CHL_RCV_H_ID", Value = ob.DF_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_H_ID", Value = ob.KNT_STYL_FAB_ITEM_H_ID},
                     new CommandParameter() {ParameterName = "pRCV_ROLL_QTY", Value = ob.RCV_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pRCV_FIN_FAB_QTY", Value = ob.RCV_FIN_FAB_QTY},
                     new CommandParameter() {ParameterName = "pLOSS_QTY", Value = ob.LOSS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pNOTE", Value = ob.NOTE},
                     new CommandParameter() {ParameterName = "pIS_STORED", Value = ob.IS_STORED},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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

        public List<DF_SCO_CHL_RCV_DModel> SelectByID(Int64? pDF_SCO_CHL_RCV_H_ID=null)
        {
            string sp = "PKG_DF_SC_PROGRAM.DF_SCO_CHL_RCV_D_Select";
            try
            {
                var obList = new List<DF_SCO_CHL_RCV_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_CHL_RCV_H_ID", Value =pDF_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SCO_CHL_RCV_DModel ob = new DF_SCO_CHL_RCV_DModel();

                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.LK_DIA_TYPE_NAME = (dr["LK_DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DIA_TYPE_NAME"]);
                    ob.FAB_ITEM_DESC = (dr["FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_DESC"]);
                    ob.IS_ACTIVE = true;
                    
                    ob.DF_SCO_CHL_RCV_D_ID = (dr["DF_SCO_CHL_RCV_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_CHL_RCV_D_ID"]);
                    ob.DF_SCO_CHL_RCV_H_ID = (dr["DF_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_CHL_RCV_H_ID"]);
                    //ob.DF_SC_PRG_ISS_H_ID = (dr["DF_SC_PRG_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PRG_ISS_H_ID"]);
                    ob.DF_SC_BT_CHL_ISS_D_ID = (dr["DF_SC_BT_CHL_ISS_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_BT_CHL_ISS_D_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY"]);
                    ob.ISS_ROLL_QTY = (dr["ISS_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_ROLL_QTY"]);
                    ob.RCV_ROLL_QTY = (dr["RCV_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_ROLL_QTY"]);
                    ob.RCV_GREY_FAB_QTY = (dr["RCV_GREY_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_GREY_FAB_QTY"]);
                    ob.RCV_FIN_FAB_QTY = (dr["RCV_FIN_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_FIN_FAB_QTY"]);
                    ob.RTN_GREY_FAB_QTY = (dr["RTN_GREY_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RTN_GREY_FAB_QTY"]);
                    ob.LOSS_QTY = (dr["LOSS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOSS_QTY"]);
                    ob.BATCH_QTY = (dr["BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BATCH_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.NOTE = (dr["NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOTE"]);
                    ob.IS_STORED = (dr["IS_STORED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_STORED"]);

                    ob.DF_PROC_TYPE_LST = (dr["DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_LST"]);
                    ob.NXT_DF_PROC_TYPE_LST = (dr["NXT_DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_DF_PROC_TYPE_LST"]);

                    //ob.processTypeList = new DF_SCO_CHL_RCV_PROCModel().SelectByID(ob.DF_SCO_CHL_RCV_H_ID,ob.DYE_BT_CARD_H_ID);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_SCO_CHL_RCV_DModel Select(long ID)
        {
            string sp = "Select_DF_SCO_CHL_RCV_D";
            try
            {
                var ob = new DF_SCO_CHL_RCV_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_CHL_RCV_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_SCO_CHL_RCV_D_ID = (dr["DF_SCO_CHL_RCV_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_CHL_RCV_D_ID"]);
                    ob.DF_SCO_CHL_RCV_H_ID = (dr["DF_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_CHL_RCV_H_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DF_SC_BT_CHL_ISS_D_ID = (dr["DF_SC_BT_CHL_ISS_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_BT_CHL_ISS_D_ID"]);
                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);
                    ob.RCV_ROLL_QTY = (dr["RCV_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_ROLL_QTY"]);
                    ob.RCV_FIN_FAB_QTY = (dr["RCV_FIN_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_FIN_FAB_QTY"]);
                    ob.LOSS_QTY = (dr["LOSS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOSS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.NOTE = (dr["NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOTE"]);
                    ob.IS_STORED = (dr["IS_STORED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_STORED"]);
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


        public List<DF_SCO_CHL_RCV_DModel> SelectAllForBillByID(Int64? pDF_SCO_CHL_RCV_H_ID = null)
        {
            string sp = "PKG_DF_SC_PROGRAM.DF_SCO_CHL_RCV_D_SELECT";
            try
            {
                var obList = new List<DF_SCO_CHL_RCV_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_CHL_RCV_H_ID", Value =pDF_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SCO_CHL_RCV_DModel ob = new DF_SCO_CHL_RCV_DModel();
                    ob.DF_SCO_CHL_RCV_H_ID = (dr["DF_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_CHL_RCV_H_ID"]);
                    ob.RCV_ROLL_QTY = (dr["RCV_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_ROLL_QTY"]);
                    ob.RCV_GREY_FAB_QTY = (dr["RCV_GREY_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_GREY_FAB_QTY"]);
                    ob.RCV_FIN_FAB_QTY = (dr["RCV_FIN_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_FIN_FAB_QTY"]);
                    ob.RTN_GREY_FAB_QTY = (dr["RTN_GREY_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RTN_GREY_FAB_QTY"]);
                    ob.BATCH_QTY = (dr["BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BATCH_QTY"]);

                    ob.DF_PROC_TYPE_LST = (dr["DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_LST"]);
                    ob.DIA_TYPE_NAME = (dr["DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE_NAME"]);

                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);

                    ob.DF_PROC_TYPE_NAME_LIST = (dr["DF_PROC_TYPE_NAME_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_NAME_LIST"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.NXT_DF_PROC_TYPE_LST = (dr["NXT_DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_DF_PROC_TYPE_LST"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DF_SCO_CHL_RCV_DModel> SelectAllForAOPByID(Int64? pDF_SCO_CHL_RCV_H_ID = null)
        {
            string sp = "PKG_DF_SC_PROGRAM.DF_SCO_CHL_RCV_D_SELECT";
            try
            {
                var obList = new List<DF_SCO_CHL_RCV_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_CHL_RCV_H_ID", Value =pDF_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SCO_CHL_RCV_DModel ob = new DF_SCO_CHL_RCV_DModel();
                    ob.DF_SCO_CHL_RCV_H_ID = (dr["DF_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_CHL_RCV_H_ID"]);
                    ob.RCV_ROLL_QTY = (dr["RCV_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_ROLL_QTY"]);
                    ob.RCV_GREY_FAB_QTY = (dr["RCV_GREY_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_GREY_FAB_QTY"]);
                    ob.RCV_FIN_FAB_QTY = (dr["RCV_FIN_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_FIN_FAB_QTY"]);
                    ob.RTN_GREY_FAB_QTY = (dr["RTN_GREY_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RTN_GREY_FAB_QTY"]);
                    ob.BATCH_QTY = (dr["BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BATCH_QTY"]);

                    ob.DF_PROC_TYPE_LST = (dr["DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_LST"]);
                    ob.FAB_ITEM_DESC = (dr["FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_DESC"]);

                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);

                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                    ob.DF_PROC_TYPE_NAME_LIST = (dr["DF_PROC_TYPE_NAME_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_NAME_LIST"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.NXT_DF_PROC_TYPE_LST = (dr["NXT_DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_DF_PROC_TYPE_LST"]);
                    
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