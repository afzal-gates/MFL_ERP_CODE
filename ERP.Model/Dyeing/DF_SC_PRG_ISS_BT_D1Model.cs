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
    public class DF_SC_PRG_ISS_BT_D1Model
    {
        public Int64 DF_SC_PRG_ISS_BT_D1_ID { get; set; }
        public Int64 DF_SC_PRG_ISS_H_ID { get; set; }
        public Int64 DYE_BT_CARD_H_ID { get; set; }
        public Int64 KNT_STYL_FAB_ITEM_H_ID { get; set; }
        public Int64 LK_DIA_TYPE_ID { get; set; }
        public Int64 RF_FAB_TYPE_ID { get; set; }
        public Int64 RF_FIB_COMP_ID { get; set; }
        public string RQD_GSM { get; set; }
        public Int64 FIN_DIA { get; set; }
        public Int64 ISS_ROLL_QTY { get; set; }
        public Int64 ISS_QTY { get; set; }

        public Int64 QTY_MOU_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string DF_PROC_TYPE_LST { get; set; }

        public List<DF_SC_PRG_ISS_PROC_D2Model> processTypeList { get; set; }
        public string Save()
        {
            const string sp = "SP_DF_SC_PRG_ISS_BT_D1";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PRG_ISS_BT_D1_ID", Value = ob.DF_SC_PRG_ISS_BT_D1_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PRG_ISS_H_ID", Value = ob.DF_SC_PRG_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_H_ID", Value = ob.KNT_STYL_FAB_ITEM_H_ID},
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
            const string sp = "SP_DF_SC_PRG_ISS_BT_D1";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PRG_ISS_BT_D1_ID", Value = ob.DF_SC_PRG_ISS_BT_D1_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PRG_ISS_H_ID", Value = ob.DF_SC_PRG_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_H_ID", Value = ob.KNT_STYL_FAB_ITEM_H_ID},
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
            const string sp = "SP_DF_SC_PRG_ISS_BT_D1";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PRG_ISS_BT_D1_ID", Value = ob.DF_SC_PRG_ISS_BT_D1_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PRG_ISS_H_ID", Value = ob.DF_SC_PRG_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_H_ID", Value = ob.KNT_STYL_FAB_ITEM_H_ID},
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

        public List<DF_SC_PRG_ISS_BT_D1Model> SelectAll()
        {
            string sp = "Select_DF_SC_PRG_ISS_BT_D1";
            try
            {
                var obList = new List<DF_SC_PRG_ISS_BT_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PRG_ISS_BT_D1_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SC_PRG_ISS_BT_D1Model ob = new DF_SC_PRG_ISS_BT_D1Model();
                    ob.DF_SC_PRG_ISS_BT_D1_ID = (dr["DF_SC_PRG_ISS_BT_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PRG_ISS_BT_D1_ID"]);
                    ob.DF_SC_PRG_ISS_H_ID = (dr["DF_SC_PRG_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PRG_ISS_H_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);
                    ob.ISS_ROLL_QTY = (dr["ISS_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_ROLL_QTY"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
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

        public List<DF_SC_PRG_ISS_BT_D1Model> SelectByID(Int64? pDF_SC_PRG_ISS_H_ID = null, string pCHALAN_NO = null)
        {
            string sp = "PKG_DF_SC_PROGRAM.DF_SC_PRG_ISS_BT_D1_Select";
            try
            {
                var obList = new List<DF_SC_PRG_ISS_BT_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PRG_ISS_H_ID", Value =pDF_SC_PRG_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value =pCHALAN_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SC_PRG_ISS_BT_D1Model ob = new DF_SC_PRG_ISS_BT_D1Model();

                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.RQD_GSM = (dr["RQD_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_GSM"]);

                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FIN_DIA"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);
                    ob.DIA_TYPE_NAME = (dr["DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE_NAME"]);
                    ob.DF_PROC_TYPE_LST = (dr["DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_LST"]);
                    ob.FAB_ITEM_DESC = (dr["FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_DESC"]);

                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_BATCH_QTY"]);
                    ob.ISS_QTY = ob.ACT_BATCH_QTY;
                    ob.IS_ACTIVE = true;

                    ob.DF_SC_PRG_ISS_BT_D1_ID = (dr["DF_SC_PRG_ISS_BT_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PRG_ISS_BT_D1_ID"]);
                    ob.DF_SC_PRG_ISS_H_ID = (dr["DF_SC_PRG_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PRG_ISS_H_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);
                    ob.ISS_ROLL_QTY = (dr["ISS_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_ROLL_QTY"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.processTypeList = new DF_SC_PRG_ISS_PROC_D2Model().SelectByID(ob.DF_SC_PRG_ISS_H_ID, ob.DYE_BT_CARD_H_ID);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_SC_PRG_ISS_BT_D1Model Select(Int64? pDF_SC_PRG_ISS_H_ID = null)
        {
            string sp = "Select_DF_SC_PRG_ISS_BT_D1";
            try
            {
                var ob = new DF_SC_PRG_ISS_BT_D1Model();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PRG_ISS_H_ID", Value =pDF_SC_PRG_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_SC_PRG_ISS_BT_D1_ID = (dr["DF_SC_PRG_ISS_BT_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PRG_ISS_BT_D1_ID"]);
                    ob.DF_SC_PRG_ISS_H_ID = (dr["DF_SC_PRG_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PRG_ISS_H_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);
                    ob.ISS_ROLL_QTY = (dr["ISS_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_ROLL_QTY"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.DF_PROC_TYPE_LST = (dr["DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_LST"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DYE_BATCH_NO { get; set; }

        public string DYE_LOT_NO { get; set; }

        public string FAB_TYPE_NAME { get; set; }

        public string FIB_COMP_NAME { get; set; }

        public string BUYER_NAME_EN { get; set; }

        public string STYLE_NO { get; set; }

        public string ORDER_NO { get; set; }

        public string COLOR_NAME_EN { get; set; }

        public string FIN_GSM { get; set; }

        public string KNT_YRN_LOT_LST { get; set; }

        public string DIA_TYPE_NAME { get; set; }

        public long ACT_BATCH_QTY { get; set; }

        public bool IS_ACTIVE { get; set; }

        public string FAB_ITEM_DESC { get; set; }
    }
}