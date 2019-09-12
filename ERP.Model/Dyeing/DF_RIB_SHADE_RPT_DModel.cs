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
    public class DF_RIB_SHADE_RPT_DModel
    {
        public Int64 DF_RIB_SHADE_RPT_D_ID { get; set; }
        public Int64 DF_RIB_SHADE_RPT_H_ID { get; set; }
        public Int64 KNT_STYL_FAB_ITEM_H_ID { get; set; }
        public Decimal BATCH_QTY { get; set; }
        public Decimal FINIS_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DF_RIB_SHADE_RPT_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_RIB_SHADE_RPT_D_ID", Value = ob.DF_RIB_SHADE_RPT_D_ID},
                     new CommandParameter() {ParameterName = "pDF_RIB_SHADE_RPT_H_ID", Value = ob.DF_RIB_SHADE_RPT_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_H_ID", Value = ob.KNT_STYL_FAB_ITEM_H_ID},
                     new CommandParameter() {ParameterName = "pBATCH_QTY", Value = ob.BATCH_QTY},
                     new CommandParameter() {ParameterName = "pFINIS_QTY", Value = ob.FINIS_QTY},
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
            const string sp = "SP_DF_RIB_SHADE_RPT_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_RIB_SHADE_RPT_D_ID", Value = ob.DF_RIB_SHADE_RPT_D_ID},
                     new CommandParameter() {ParameterName = "pDF_RIB_SHADE_RPT_H_ID", Value = ob.DF_RIB_SHADE_RPT_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_H_ID", Value = ob.KNT_STYL_FAB_ITEM_H_ID},
                     new CommandParameter() {ParameterName = "pBATCH_QTY", Value = ob.BATCH_QTY},
                     new CommandParameter() {ParameterName = "pFINIS_QTY", Value = ob.FINIS_QTY},
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
            const string sp = "SP_DF_RIB_SHADE_RPT_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_RIB_SHADE_RPT_D_ID", Value = ob.DF_RIB_SHADE_RPT_D_ID},
                     new CommandParameter() {ParameterName = "pDF_RIB_SHADE_RPT_H_ID", Value = ob.DF_RIB_SHADE_RPT_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_H_ID", Value = ob.KNT_STYL_FAB_ITEM_H_ID},
                     new CommandParameter() {ParameterName = "pBATCH_QTY", Value = ob.BATCH_QTY},
                     new CommandParameter() {ParameterName = "pFINIS_QTY", Value = ob.FINIS_QTY},
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

        public List<DF_RIB_SHADE_RPT_DModel> SelectAll()
        {
            string sp = "PKG_DF_INSPECTION.DF_RIB_SHADE_RPT_D_SELECT";
            try
            {
                var obList = new List<DF_RIB_SHADE_RPT_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_RIB_SHADE_RPT_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_RIB_SHADE_RPT_DModel ob = new DF_RIB_SHADE_RPT_DModel();
                    ob.DF_RIB_SHADE_RPT_D_ID = (dr["DF_RIB_SHADE_RPT_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_RIB_SHADE_RPT_D_ID"]);
                    ob.DF_RIB_SHADE_RPT_H_ID = (dr["DF_RIB_SHADE_RPT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_RIB_SHADE_RPT_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);
                    ob.BATCH_QTY = (dr["BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BATCH_QTY"]);
                    ob.FINIS_QTY = (dr["FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FINIS_QTY"]);
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


        public List<DF_RIB_SHADE_RPT_DModel> SelectByID(Int64? pDF_RIB_SHADE_RPT_H_ID = null, string pLK_FBR_GRP_LST = null)
        {
            string sp = "PKG_DF_INSPECTION.DF_RIB_SHADE_RPT_D_SELECT";
            try
            {
                var obList = new List<DF_RIB_SHADE_RPT_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_RIB_SHADE_RPT_H_ID", Value=pDF_RIB_SHADE_RPT_H_ID},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_LST", Value=pLK_FBR_GRP_LST},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_RIB_SHADE_RPT_DModel ob = new DF_RIB_SHADE_RPT_DModel();
                    ob.DF_RIB_SHADE_RPT_D_ID = (dr["DF_RIB_SHADE_RPT_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_RIB_SHADE_RPT_D_ID"]);
                    ob.DF_RIB_SHADE_RPT_H_ID = (dr["DF_RIB_SHADE_RPT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_RIB_SHADE_RPT_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);
                    ob.BATCH_QTY = (dr["BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BATCH_QTY"]);
                    ob.ROLL_QTY = (dr["ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ROLL_QTY"]);
                    ob.FINIS_QTY = (dr["FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FINIS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.FAB_ITEM_DESC = (dr["FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_DESC"]);

                    if (ob.LK_DIA_TYPE_ID == 734)
                    {
                        ob.FLAT_KNIT_ITEM_LIST = new DF_BT_SUB_LOT_CLCFModel().SelectByID(ob.DYE_BT_CARD_H_ID, 0, ob.KNT_STYL_FAB_ITEM_H_ID);
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

        public DF_RIB_SHADE_RPT_DModel Select(long ID)
        {
            string sp = "PKG_DF_INSPECTION.DF_RIB_SHADE_RPT_D_SELECT";
            try
            {
                var ob = new DF_RIB_SHADE_RPT_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_RIB_SHADE_RPT_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_RIB_SHADE_RPT_D_ID = (dr["DF_RIB_SHADE_RPT_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_RIB_SHADE_RPT_D_ID"]);
                    ob.DF_RIB_SHADE_RPT_H_ID = (dr["DF_RIB_SHADE_RPT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_RIB_SHADE_RPT_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);
                    ob.BATCH_QTY = (dr["BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BATCH_QTY"]);
                    ob.FINIS_QTY = (dr["FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FINIS_QTY"]);
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

        public string FAB_ITEM_DESC { get; set; }

        public long LK_DIA_TYPE_ID { get; set; }

        public long DYE_BT_CARD_H_ID { get; set; }

        public List<DF_BT_SUB_LOT_CLCFModel> FLAT_KNIT_ITEM_LIST { get; set; }

        public Int32 ROLL_QTY { get; set; }
    }
}