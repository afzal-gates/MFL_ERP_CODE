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
    public class DF_SC_PT_ISS_D1Model
    {
        public Int64 DF_SC_PT_ISS_D1_ID { get; set; }
        public Int64 DF_SC_PT_ISS_H_ID { get; set; }
        public Int64 KNT_STYL_FAB_ITEM_ID { get; set; }
        public Int64 RQD_QTY { get; set; }
        public Int64 ISS_ROLL_QTY { get; set; }
        public Int64 ISS_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string ALT_FAB_ITEM_LST { get; set; }
        public string DF_PROC_TYPE_LST { get; set; }
        public string DF_PROC_TYPE_NAME_LST { get; set; }


        public string FAB_TYPE_NAME { get; set; }
        public string FIB_COMP_NAME { get; set; }
        public string BUYER_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string ORDER_NO { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string FIN_DIA { get; set; }
        public string FIN_GSM { get; set; }
        public string KNT_YRN_LOT_LST { get; set; }
        public string DIA_TYPE_NAME { get; set; }


        public string Save()
        {
            const string sp = "SP_DF_SC_PT_ISS_D1";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D1_ID", Value = ob.DF_SC_PT_ISS_D1_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_H_ID", Value = ob.DF_SC_PT_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_ID", Value = ob.KNT_STYL_FAB_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRQD_QTY", Value = ob.RQD_QTY},
                     new CommandParameter() {ParameterName = "pISS_ROLL_QTY", Value = ob.ISS_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pISS_QTY", Value = ob.ISS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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
            const string sp = "SP_DF_SC_PT_ISS_D1";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D1_ID", Value = ob.DF_SC_PT_ISS_D1_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_H_ID", Value = ob.DF_SC_PT_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_ID", Value = ob.KNT_STYL_FAB_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRQD_QTY", Value = ob.RQD_QTY},
                     new CommandParameter() {ParameterName = "pISS_ROLL_QTY", Value = ob.ISS_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pISS_QTY", Value = ob.ISS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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
            const string sp = "SP_DF_SC_PT_ISS_D1";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D1_ID", Value = ob.DF_SC_PT_ISS_D1_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_H_ID", Value = ob.DF_SC_PT_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_ID", Value = ob.KNT_STYL_FAB_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRQD_QTY", Value = ob.RQD_QTY},
                     new CommandParameter() {ParameterName = "pISS_ROLL_QTY", Value = ob.ISS_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pISS_QTY", Value = ob.ISS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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

        public List<DF_SC_PT_ISS_D1Model> SelectAll(Int64? pDF_SC_PT_ISS_H_ID = null)
        {
            string sp = "PKG_DF_PT.DF_SC_PT_ISS_D1_Select";
            try
            {
                var obList = new List<DF_SC_PT_ISS_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_H_ID", Value =pDF_SC_PT_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SC_PT_ISS_D1Model ob = new DF_SC_PT_ISS_D1Model();
                    ob.DF_SC_PT_ISS_D1_ID = (dr["DF_SC_PT_ISS_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D1_ID"]);
                    ob.DF_SC_PT_ISS_H_ID = (dr["DF_SC_PT_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                    ob.ISS_ROLL_QTY = (dr["ISS_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_ROLL_QTY"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.DF_PROC_TYPE_NAME_LST = (dr["DF_PROC_TYPE_NAME_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_NAME_LST"]);
                    ob.DF_PROC_TYPE_LST = (dr["DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_LST"]);

                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.FAB_PROD_CAT_NAME = (dr["FAB_PROD_CAT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_NAME"]);

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);
                    ob.DIA_TYPE_NAME = (dr["DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE_NAME"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DF_SC_PT_ISS_D1Model> SelectForDF(string pSC_PRG_NO = null, Int64? pLK_DIA_TYPE_ID = null, string pLK_FBR_GRP_LST=null)
        {
            string sp = "PKG_DF_PT.DF_SC_PT_ISS_D1_Select";
            try
            {
                var obList = new List<DF_SC_PT_ISS_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_PRG_NO", Value =pSC_PRG_NO},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value =pLK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_LST", Value =pLK_FBR_GRP_LST},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SC_PT_ISS_D1Model ob = new DF_SC_PT_ISS_D1Model();
                    ob.DF_SC_PT_ISS_D1_ID = (dr["DF_SC_PT_ISS_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D1_ID"]);
                    ob.DF_SC_PT_ISS_H_ID = (dr["DF_SC_PT_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                    ob.ISS_ROLL_QTY = (dr["ISS_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_ROLL_QTY"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);

                    ob.DF_PROC_TYPE_NAME_LST = (dr["DF_PROC_TYPE_NAME_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_NAME_LST"]);
                    ob.DF_PROC_TYPE_LST = (dr["DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_LST"]);

                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.FAB_PROD_CAT_NAME = (dr["FAB_PROD_CAT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_NAME"]);

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);
                    ob.DIA_TYPE_NAME = (dr["DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE_NAME"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_SC_PT_ISS_D1Model Select(long ID)
        {
            string sp = "Select_DF_SC_PT_ISS_D1";
            try
            {
                var ob = new DF_SC_PT_ISS_D1Model();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D1_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_SC_PT_ISS_D1_ID = (dr["DF_SC_PT_ISS_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D1_ID"]);
                    ob.DF_SC_PT_ISS_H_ID = (dr["DF_SC_PT_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                    ob.ISS_ROLL_QTY = (dr["ISS_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_ROLL_QTY"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
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



        public List<DF_SC_PT_ISS_D1Model> SelectBatchFabForPT(string pDYE_BATCH_NO = null)
        {
            string sp = "PKG_DF_PT.DYE_BATCH_ISSUE_FAB_SELECT";
            try
            {
                var obList = new List<DF_SC_PT_ISS_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value =pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SC_PT_ISS_D1Model ob = new DF_SC_PT_ISS_D1Model();
                    //ob.DF_SC_PT_ISS_D1_ID = (dr["DF_SC_PT_ISS_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D1_ID"]);
                    //ob.DF_SC_PT_ISS_H_ID = (dr["DF_SC_PT_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                    ob.ISS_ROLL_QTY = (dr["ISS_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_ROLL_QTY"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);

                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.FAB_PROD_CAT_NAME = (dr["FAB_PROD_CAT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_NAME"]);
                    ob.COL_TYPE_NAME = (dr["COL_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COL_TYPE_NAME"]);
                    
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);
                    ob.DIA_TYPE_NAME = (dr["DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE_NAME"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string BYR_ACC_NAME_EN { get; set; }

        public string BYR_ACC_GRP_NAME_EN { get; set; }

        public string FABRIC_DESC { get; set; }

        public string ORDER_NO_LIST { get; set; }

        public string FAB_PROD_CAT_NAME { get; set; }

        public long RF_FAB_TYPE_ID { get; set; }

        public long FAB_COLOR_ID { get; set; }

        public Int64? DYE_BT_CARD_H_ID { get; set; }

        public string COL_TYPE_NAME { get; set; }
    }
}