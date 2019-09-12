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
    public class CM_IMP_LC_DModel
    {
        public Int64 CM_IMP_LC_D_ID { get; set; }
        public Int64 CM_IMP_LC_H_ID { get; set; }
        public Int64 CM_EXP_LC_H_ID { get; set; }
        public Int64 CM_IMP_PI_H_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Decimal LC_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Decimal UNIT_PRICE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_CM_IMP_LC_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_D_ID", Value = ob.CM_IMP_LC_D_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_H_ID", Value = ob.CM_EXP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PI_H_ID", Value = ob.CM_IMP_PI_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLC_QTY", Value = ob.LC_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
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
            const string sp = "SP_CM_IMP_LC_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_D_ID", Value = ob.CM_IMP_LC_D_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_H_ID", Value = ob.CM_EXP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PI_H_ID", Value = ob.CM_IMP_PI_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLC_QTY", Value = ob.LC_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
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
            const string sp = "SP_CM_IMP_LC_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_D_ID", Value = ob.CM_IMP_LC_D_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_H_ID", Value = ob.CM_EXP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PI_H_ID", Value = ob.CM_IMP_PI_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLC_QTY", Value = ob.LC_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
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

        public List<CM_IMP_LC_DModel> SelectAll()
        {
            string sp = "PKG_CM_IMPORT_LC.CM_IMP_LC_D_Select";
            try
            {
                var obList = new List<CM_IMP_LC_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CM_IMP_LC_DModel ob = new CM_IMP_LC_DModel();
                    ob.CM_IMP_LC_D_ID = (dr["CM_IMP_LC_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_D_ID"]);
                    ob.CM_IMP_LC_H_ID = (dr["CM_IMP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_H_ID"]);
                    ob.CM_EXP_LC_H_ID = (dr["CM_EXP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_EXP_LC_H_ID"]);
                    ob.CM_IMP_PI_H_ID = (dr["CM_IMP_PI_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PI_H_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.LC_QTY = (dr["LC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LC_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
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


        public List<CM_IMP_LC_DModel> SelectByID(Int64? pCM_IMP_LC_H_ID = null)
        {
            string sp = "PKG_CM_IMPORT_LC.CM_IMP_LC_D_Select";
            try
            {
                var obList = new List<CM_IMP_LC_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value =pCM_IMP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CM_IMP_LC_DModel ob = new CM_IMP_LC_DModel();
                    ob.CM_IMP_LC_D_ID = (dr["CM_IMP_LC_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_D_ID"]);
                    ob.CM_IMP_LC_H_ID = (dr["CM_IMP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_H_ID"]);
                    ob.CM_EXP_LC_H_ID = (dr["CM_EXP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_EXP_LC_H_ID"]);
                    ob.CM_IMP_PI_H_ID = (dr["CM_IMP_PI_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PI_H_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.LC_QTY = (dr["LC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LC_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.PI_NO_IMP = (dr["PI_NO_IMP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PI_NO_IMP"]);
                    ob.EXP_LCSC_NO = (dr["EXP_LCSC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EXP_LCSC_NO"]);
                    ob.TTL_VALUE = ob.LC_QTY * ob.UNIT_PRICE;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CM_IMP_LC_DModel Select(long ID)
        {
            string sp = "PKG_CM_IMPORT_LC.CM_IMP_LC_D_Select";
            try
            {
                var ob = new CM_IMP_LC_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.CM_IMP_LC_D_ID = (dr["CM_IMP_LC_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_D_ID"]);
                    ob.CM_IMP_LC_H_ID = (dr["CM_IMP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_H_ID"]);
                    ob.CM_EXP_LC_H_ID = (dr["CM_EXP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_EXP_LC_H_ID"]);
                    ob.CM_IMP_PI_H_ID = (dr["CM_IMP_PI_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PI_H_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.LC_QTY = (dr["LC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LC_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
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

        public string ITEM_NAME_EN { get; set; }

        public string PI_NO_IMP { get; set; }

        public string EXP_LCSC_NO { get; set; }

        public decimal TTL_VALUE { get; set; }
    }
}