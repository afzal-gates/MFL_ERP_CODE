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
    public class SCM_STR_OIL_REQ_DModel
    {
        public Int64 SCM_STR_TR_REQ_D_ID { get; set; }
        public Int64 SCM_STR_OIL_REQ_H_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Int64 PACK_RQD_QTY { get; set; }
        public Int64 PACK_MOU_ID { get; set; }
        public Int64 QTY_PER_PACK { get; set; }
        public Int64 RQD_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Decimal PACK_ISS_QTY { get; set; }
        public Int64 ISS_QTY { get; set; }

        public string CENTRAL_STR_STOCK { get; set; }
        public string SUB_STR_STOCK { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string PACK_MOU_CODE { get; set; }
        public string QTY_MOU_CODE { get; set; }



        public string Save()
        {
            const string sp = "SP_SCM_STR_OIL_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_TR_REQ_D_ID", Value = ob.SCM_STR_TR_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_OIL_REQ_H_ID", Value = ob.SCM_STR_OIL_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pPACK_RQD_QTY", Value = ob.PACK_RQD_QTY},
                     new CommandParameter() {ParameterName = "pPACK_MOU_ID", Value = ob.PACK_MOU_ID},
                     new CommandParameter() {ParameterName = "pQTY_PER_PACK", Value = ob.QTY_PER_PACK},
                     new CommandParameter() {ParameterName = "pRQD_QTY", Value = ob.RQD_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pPACK_ISS_QTY", Value = ob.PACK_ISS_QTY},
                     new CommandParameter() {ParameterName = "pISS_QTY", Value = ob.ISS_QTY},
                     
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



        public List<SCM_STR_OIL_REQ_DModel> GetReqDtlByID(Int64 pSCM_STR_OIL_REQ_H_ID)
        {
            string sp = "pkg_scm_str.scm_str_oil_req_h_select";
            try
            {
                var obList = new List<SCM_STR_OIL_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_OIL_REQ_H_ID", Value = pSCM_STR_OIL_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_STR_OIL_REQ_DModel ob = new SCM_STR_OIL_REQ_DModel();
                    ob.SCM_STR_TR_REQ_D_ID = (dr["SCM_STR_TR_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_TR_REQ_D_ID"]);
                    ob.SCM_STR_OIL_REQ_H_ID = (dr["SCM_STR_OIL_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_OIL_REQ_H_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.PACK_RQD_QTY = (dr["PACK_RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_RQD_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_PER_PACK"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.PACK_ISS_QTY = (dr["PACK_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_ISS_QTY"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY"]);

                    ob.CENTRAL_STR_STOCK = (dr["CENTRAL_STR_STOCK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CENTRAL_STR_STOCK"]);
                    ob.SUB_STR_STOCK = (dr["SUB_STR_STOCK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_STR_STOCK"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.PACK_MOU_CODE = (dr["PACK_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PACK_MOU_CODE"]);
                    ob.QTY_MOU_CODE = (dr["QTY_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QTY_MOU_CODE"]);
                   
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCM_STR_OIL_REQ_DModel Select(long ID)
        {
            string sp = "Select_SCM_STR_OIL_REQ_D";
            try
            {
                var ob = new SCM_STR_OIL_REQ_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_TR_REQ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SCM_STR_TR_REQ_D_ID = (dr["SCM_STR_TR_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_TR_REQ_D_ID"]);
                    ob.SCM_STR_OIL_REQ_H_ID = (dr["SCM_STR_OIL_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_OIL_REQ_H_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.PACK_RQD_QTY = (dr["PACK_RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_RQD_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_PER_PACK"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.PACK_ISS_QTY = (dr["PACK_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_ISS_QTY"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY"]);
                    
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}