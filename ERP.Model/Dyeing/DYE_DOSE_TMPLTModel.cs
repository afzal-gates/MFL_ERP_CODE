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
    public class DYE_DOSE_TMPLTModel
    {
        public Int64 DYE_DOSE_TMPLT_ID { get; set; }
        public Int64 MC_COLOR_GRP_ID { get; set; }
        public Int64 DYE_PRD_PHASE_TYPE_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Decimal DOSE_QTY { get; set; }
        public Int64 DOSE_MOU_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DYE_DOSE_TMPLT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_ID", Value = ob.DYE_DOSE_TMPLT_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                     new CommandParameter() {ParameterName = "pDYE_PRD_PHASE_TYPE_ID", Value = ob.DYE_PRD_PHASE_TYPE_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pDOSE_QTY", Value = ob.DOSE_QTY},
                     new CommandParameter() {ParameterName = "pDOSE_MOU_ID", Value = ob.DOSE_MOU_ID},
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
            const string sp = "SP_DYE_DOSE_TMPLT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_ID", Value = ob.DYE_DOSE_TMPLT_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                     new CommandParameter() {ParameterName = "pDYE_PRD_PHASE_TYPE_ID", Value = ob.DYE_PRD_PHASE_TYPE_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pDOSE_QTY", Value = ob.DOSE_QTY},
                     new CommandParameter() {ParameterName = "pDOSE_MOU_ID", Value = ob.DOSE_MOU_ID},
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
            const string sp = "SP_DYE_DOSE_TMPLT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_ID", Value = ob.DYE_DOSE_TMPLT_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                     new CommandParameter() {ParameterName = "pDYE_PRD_PHASE_TYPE_ID", Value = ob.DYE_PRD_PHASE_TYPE_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pDOSE_QTY", Value = ob.DOSE_QTY},
                     new CommandParameter() {ParameterName = "pDOSE_MOU_ID", Value = ob.DOSE_MOU_ID},
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

        public List<DYE_DOSE_TMPLTModel> SelectAll()
        {
            string sp = "Select_DYE_DOSE_TMPLT";
            try
            {
                var obList = new List<DYE_DOSE_TMPLTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_DOSE_TMPLTModel ob = new DYE_DOSE_TMPLTModel();
                    ob.DYE_DOSE_TMPLT_ID = (dr["DYE_DOSE_TMPLT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DOSE_TMPLT_ID"]);
                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    ob.DYE_PRD_PHASE_TYPE_ID = (dr["DYE_PRD_PHASE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_PRD_PHASE_TYPE_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.DOSE_QTY = (dr["DOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DOSE_QTY"]);
                    ob.DOSE_MOU_ID = (dr["DOSE_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DOSE_MOU_ID"]);
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

        public List<DYE_DOSE_TMPLTModel> SelectByCG(Int64 pMC_COLOR_GRP_ID)
        {
            string sp = "pkg_dye_batch_program.dye_dose_tmplt_select";
            try
            {
                var obList = new List<DYE_DOSE_TMPLTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value =pMC_COLOR_GRP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_DOSE_TMPLTModel ob = new DYE_DOSE_TMPLTModel();
                    ob.DYE_DOSE_TMPLT_ID = (dr["DYE_DOSE_TMPLT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DOSE_TMPLT_ID"]);
                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    ob.DYE_PRD_PHASE_TYPE_ID = (dr["DYE_PRD_PHASE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_PRD_PHASE_TYPE_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.DOSE_QTY = (dr["DOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DOSE_QTY"]);
                    ob.DOSE_MOU_ID = (dr["DOSE_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DOSE_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.QTY_MOU_ST = (dr["QTY_MOU_ST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QTY_MOU_ST"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    //ob.SL_NO = (dr["SL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SL_NO"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DYE_DOSE_TMPLTModel Select(long ID)
        {
            string sp = "Select_DYE_DOSE_TMPLT";
            try
            {
                var ob = new DYE_DOSE_TMPLTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DYE_DOSE_TMPLT_ID = (dr["DYE_DOSE_TMPLT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DOSE_TMPLT_ID"]);
                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    ob.DYE_PRD_PHASE_TYPE_ID = (dr["DYE_PRD_PHASE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_PRD_PHASE_TYPE_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.DOSE_QTY = (dr["DOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DOSE_QTY"]);
                    ob.DOSE_MOU_ID = (dr["DOSE_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DOSE_MOU_ID"]);
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

        public string QTY_MOU_ST { get; set; }

        public string ITEM_CAT_NAME_EN { get; set; }

        public string SL_NO { get; set; }

        public string ITEM_CODE { get; set; }
    }
}