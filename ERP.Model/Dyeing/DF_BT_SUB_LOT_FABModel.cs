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
    public class DF_BT_SUB_LOT_FABModel
    {
        public Int64 DF_BT_SUB_LOT_FAB_ID { get; set; }
        public Int64 DF_BT_SUB_LOT_ID { get; set; }
        public Int64 KNT_STYL_FAB_ITEM_H_ID { get; set; }
        public Decimal LOT_QTY { get; set; }
        public Decimal FINIS_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Int64 NO_OF_ROLL { get; set; }
        public Int64 ACT_NO_ROLL { get; set; }
        public Decimal ACT_FINIS_QTY { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DF_BT_SUB_LOT_FAB";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_FAB_ID", Value = ob.DF_BT_SUB_LOT_FAB_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_H_ID", Value = ob.KNT_STYL_FAB_ITEM_H_ID},
                     new CommandParameter() {ParameterName = "pLOT_QTY", Value = ob.LOT_QTY},
                     new CommandParameter() {ParameterName = "pFINIS_QTY", Value = ob.FINIS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pNO_OF_ROLL", Value = ob.NO_OF_ROLL},
                     new CommandParameter() {ParameterName = "pACT_NO_ROLL", Value = ob.ACT_NO_ROLL},
                     new CommandParameter() {ParameterName = "pACT_FINIS_QTY", Value = ob.ACT_FINIS_QTY},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
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
            const string sp = "SP_DF_BT_SUB_LOT_FAB";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_FAB_ID", Value = ob.DF_BT_SUB_LOT_FAB_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_H_ID", Value = ob.KNT_STYL_FAB_ITEM_H_ID},
                     new CommandParameter() {ParameterName = "pLOT_QTY", Value = ob.LOT_QTY},
                     new CommandParameter() {ParameterName = "pFINIS_QTY", Value = ob.FINIS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pNO_OF_ROLL", Value = ob.NO_OF_ROLL},
                     new CommandParameter() {ParameterName = "pACT_NO_ROLL", Value = ob.ACT_NO_ROLL},
                     new CommandParameter() {ParameterName = "pACT_FINIS_QTY", Value = ob.ACT_FINIS_QTY},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
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
            const string sp = "SP_DF_BT_SUB_LOT_FAB";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_FAB_ID", Value = ob.DF_BT_SUB_LOT_FAB_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_H_ID", Value = ob.KNT_STYL_FAB_ITEM_H_ID},
                     new CommandParameter() {ParameterName = "pLOT_QTY", Value = ob.LOT_QTY},
                     new CommandParameter() {ParameterName = "pFINIS_QTY", Value = ob.FINIS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pNO_OF_ROLL", Value = ob.NO_OF_ROLL},
                     new CommandParameter() {ParameterName = "pACT_NO_ROLL", Value = ob.ACT_NO_ROLL},
                     new CommandParameter() {ParameterName = "pACT_FINIS_QTY", Value = ob.ACT_FINIS_QTY},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
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

        public List<DF_BT_SUB_LOT_FABModel> SelectAll()
        {
            string sp = "Select_DF_BT_SUB_LOT_FAB";
            try
            {
                var obList = new List<DF_BT_SUB_LOT_FABModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_FAB_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_BT_SUB_LOT_FABModel ob = new DF_BT_SUB_LOT_FABModel();
                    ob.DF_BT_SUB_LOT_FAB_ID = (dr["DF_BT_SUB_LOT_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_FAB_ID"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);
                    ob.LOT_QTY = (dr["LOT_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOT_QTY"]);
                    ob.FINIS_QTY = (dr["FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FINIS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.NO_OF_ROLL = (dr["NO_OF_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_ROLL"]);
                    ob.ACT_NO_ROLL = (dr["ACT_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_NO_ROLL"]);
                    ob.ACT_FINIS_QTY = (dr["ACT_FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_FINIS_QTY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
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


        public List<DF_BT_SUB_LOT_FABModel> SelectByID(Int64? pDF_BT_SUB_LOT_ID=null)
        {
            string sp = "PKG_DF_INSPECTION.DF_BT_SUB_LOT_FAB_SELECT";
            try
            {
                var obList = new List<DF_BT_SUB_LOT_FABModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value =pDF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_BT_SUB_LOT_FABModel ob = new DF_BT_SUB_LOT_FABModel();
                    ob.DF_BT_SUB_LOT_FAB_ID = (dr["DF_BT_SUB_LOT_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_FAB_ID"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);
                    ob.LOT_QTY = (dr["LOT_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOT_QTY"]);
                    ob.FINIS_QTY = (dr["FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FINIS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.NO_OF_ROLL = (dr["NO_OF_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_ROLL"]);
                    ob.ACT_NO_ROLL = (dr["ACT_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_NO_ROLL"]);
                    ob.ACT_FINIS_QTY = (dr["ACT_FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_FINIS_QTY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    
                    ob.FAB_ITEM_DESC = (dr["FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_DESC"]);
                    ob.FAB_TYPE_DESC = (dr["FAB_TYPE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_DESC"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FIN_DIA"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);

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

        public DF_BT_SUB_LOT_FABModel Select(long ID)
        {
            string sp = "Select_DF_BT_SUB_LOT_FAB";
            try
            {
                var ob = new DF_BT_SUB_LOT_FABModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_FAB_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_BT_SUB_LOT_FAB_ID = (dr["DF_BT_SUB_LOT_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_FAB_ID"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);
                    ob.LOT_QTY = (dr["LOT_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOT_QTY"]);
                    ob.FINIS_QTY = (dr["FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FINIS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.NO_OF_ROLL = (dr["NO_OF_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_ROLL"]);
                    ob.ACT_NO_ROLL = (dr["ACT_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_NO_ROLL"]);
                    ob.ACT_FINIS_QTY = (dr["ACT_FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_FINIS_QTY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
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

        public string FAB_TYPE_DESC { get; set; }

        public long FIN_DIA { get; set; }

        public string FIN_GSM { get; set; }

        public string FAB_ITEM_DESC { get; set; }

        public long DYE_BT_CARD_H_ID { get; set; }

        public Int64? LK_DIA_TYPE_ID { get; set; }

        public List<DF_BT_SUB_LOT_CLCFModel> FLAT_KNIT_ITEM_LIST { get; set; }
    }
}