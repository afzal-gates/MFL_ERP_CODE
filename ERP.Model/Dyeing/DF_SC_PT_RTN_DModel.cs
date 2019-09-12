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
    public class DF_SC_PT_RTN_DModel
    {
        public Int64 DF_SC_PT_RTN_D_ID { get; set; }
        public Int64 DF_SC_PT_RCV_H_ID { get; set; }
        public Int64 DF_SC_PT_CHL_ISS_D_ID { get; set; }
        public Int64 KNT_STYL_FAB_ITEM_ID { get; set; }
        public Int64 RTN_ROLL_QTY { get; set; }
        public Decimal RTN_FAB_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public List<DF_SC_PT_RTN_ROLLModel> RTN_ROLL_LST { get; set; }
        public string Save()
        {
            const string sp = "DF_PT_RCV.DF_SC_PT_RTN_D_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_RTN_D_ID", Value = ob.DF_SC_PT_RTN_D_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_RCV_H_ID", Value = ob.DF_SC_PT_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_D_ID", Value = ob.DF_SC_PT_CHL_ISS_D_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_ID", Value = ob.KNT_STYL_FAB_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRTN_ROLL_QTY", Value = ob.RTN_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pRTN_FAB_QTY", Value = ob.RTN_FAB_QTY},
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
            const string sp = "SP_DF_SC_PT_RTN_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_RTN_D_ID", Value = ob.DF_SC_PT_RTN_D_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_RCV_H_ID", Value = ob.DF_SC_PT_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_D_ID", Value = ob.DF_SC_PT_CHL_ISS_D_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_ID", Value = ob.KNT_STYL_FAB_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRTN_ROLL_QTY", Value = ob.RTN_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pRTN_FAB_QTY", Value = ob.RTN_FAB_QTY},
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
            const string sp = "SP_DF_SC_PT_RTN_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_RTN_D_ID", Value = ob.DF_SC_PT_RTN_D_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_RCV_H_ID", Value = ob.DF_SC_PT_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_D_ID", Value = ob.DF_SC_PT_CHL_ISS_D_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_ID", Value = ob.KNT_STYL_FAB_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRTN_ROLL_QTY", Value = ob.RTN_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pRTN_FAB_QTY", Value = ob.RTN_FAB_QTY},
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

        public List<DF_SC_PT_RTN_DModel> SelectAll(Int64? pDF_SC_PT_RCV_H_ID=null)
        {
            string sp = "PKG_DF_PT.DF_SC_PT_RTN_D_SELECT";
            try
            {
                var obList = new List<DF_SC_PT_RTN_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_RCV_H_ID", Value =pDF_SC_PT_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SC_PT_RTN_DModel ob = new DF_SC_PT_RTN_DModel();
                    ob.DF_SC_PT_RTN_D_ID = (dr["DF_SC_PT_RTN_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_RTN_D_ID"]);
                    ob.DF_SC_PT_RCV_H_ID = (dr["DF_SC_PT_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_RCV_H_ID"]);
                    ob.DF_SC_PT_CHL_ISS_D_ID = (dr["DF_SC_PT_CHL_ISS_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_CHL_ISS_D_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.RTN_ROLL_QTY = (dr["RTN_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RTN_ROLL_QTY"]);
                    ob.RTN_FAB_QTY = (dr["RTN_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RTN_FAB_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_QTY"]);
                    ob.ISS_ROLL_QTY = (dr["ISS_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_ROLL_QTY"]);

                    //ob.SC_PRG_NO = (dr["SC_PRG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_PRG_NO"]);
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

                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    ob.FAB_ITEM_DESC = (dr["FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_DESC"]);

                    ob.RTN_ROLL_LST = new DF_SC_PT_RTN_ROLLModel().SelectByID(ob.DF_SC_PT_RTN_D_ID);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_SC_PT_RTN_DModel Select(long ID)
        {
            string sp = "Select_DF_SC_PT_RTN_D";
            try
            {
                var ob = new DF_SC_PT_RTN_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_RTN_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_SC_PT_RTN_D_ID = (dr["DF_SC_PT_RTN_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_RTN_D_ID"]);
                    ob.DF_SC_PT_RCV_H_ID = (dr["DF_SC_PT_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_RCV_H_ID"]);
                    ob.DF_SC_PT_CHL_ISS_D_ID = (dr["DF_SC_PT_CHL_ISS_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_CHL_ISS_D_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.RTN_ROLL_QTY = (dr["RTN_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RTN_ROLL_QTY"]);
                    ob.RTN_FAB_QTY = (dr["RTN_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RTN_FAB_QTY"]);
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

        public decimal RCV_GREY_QTY { get; set; }

        public decimal ISS_QTY { get; set; }

        public decimal ISS_ROLL_QTY { get; set; }

        public string FAB_TYPE_NAME { get; set; }

        public string FIB_COMP_NAME { get; set; }

        public string BUYER_NAME_EN { get; set; }

        public string BYR_ACC_NAME_EN { get; set; }

        public string BYR_ACC_GRP_NAME_EN { get; set; }

        public string FABRIC_DESC { get; set; }

        public string ORDER_NO_LIST { get; set; }

        public string FAB_PROD_CAT_NAME { get; set; }

        public string STYLE_NO { get; set; }

        public string ORDER_NO { get; set; }

        public string COLOR_NAME_EN { get; set; }

        public string FIN_DIA { get; set; }

        public string FIN_GSM { get; set; }

        public string KNT_YRN_LOT_LST { get; set; }

        public string DIA_TYPE_NAME { get; set; }

        public string MC_ORDER_NO_LST { get; set; }

        public string FAB_ITEM_DESC { get; set; }
    }
}