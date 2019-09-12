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
    public class MC_PRNSO_REQ_DModel
    {
        public Int64 MC_PRNSO_REQ_D_ID { get; set; }
        public Int64 MC_PRNSO_REQ_H_ID { get; set; }
        public Int64 MC_ORDER_H_ID { get; set; }
        public string ORDER_NO { get; set; }
        public Int64 MC_STYLE_D_ITEM_ID { get; set; }
        public string ITEM_NAME_EN { get; set; }

        public Int64 RF_SMPL_TYPE_ID { get; set; }
        public string SMPL_TYPE_NAME { get; set; }

        public string pLK_PRN_TYPE_LST { get; set; }
        public List<LookupDataModel> LK_PRN_TYPE_LST { get; set; }
        public string pPRN_COLOR_LST { get; set; }
        public List<MC_COLORModel> PRN_COLOR_LST { get; set; }

        public Int64 RQD_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string MOU_CODE { get; set; }

        public Int64 LK_PRIORITY_ID { get; set; }
        public string LK_PRIORITY_NAME { get; set; }

        public string SP_INSTRUCTION { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public Int64 MC_STYLE_H_ID { get; set; }
        public string STYLE_NO { get; set; }

        public string MATERIAL_DESC { get; set; }
        public DateTime SHIP_DT { get; set; }
        public string NatureOfWork { get; set; }  //LK_GARM_TYPE_ID  
        public string OLD_STYLE_REF { get; set; }
        public string ImgSrc { get; set; }

        public string Save()
        {
            const string sp = "SP_MC_PRNSO_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PRNSO_REQ_D_ID", Value = ob.MC_PRNSO_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pMC_PRNSO_REQ_H_ID", Value = ob.MC_PRNSO_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value = ob.MC_STYLE_D_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRF_SMPL_TYPE_ID", Value = ob.RF_SMPL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_PRN_TYPE_LST", Value = ob.LK_PRN_TYPE_LST},
                     new CommandParameter() {ParameterName = "pPRN_COLOR_LST", Value = ob.PRN_COLOR_LST},
                     new CommandParameter() {ParameterName = "pRQD_QTY", Value = ob.RQD_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pLK_PRIORITY_ID", Value = ob.LK_PRIORITY_ID},
                     new CommandParameter() {ParameterName = "pSP_INSTRUCTION", Value = ob.SP_INSTRUCTION},
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
            const string sp = "SP_MC_PRNSO_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PRNSO_REQ_D_ID", Value = ob.MC_PRNSO_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pMC_PRNSO_REQ_H_ID", Value = ob.MC_PRNSO_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value = ob.MC_STYLE_D_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRF_SMPL_TYPE_ID", Value = ob.RF_SMPL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_PRN_TYPE_LST", Value = ob.LK_PRN_TYPE_LST},
                     new CommandParameter() {ParameterName = "pPRN_COLOR_LST", Value = ob.PRN_COLOR_LST},
                     new CommandParameter() {ParameterName = "pRQD_QTY", Value = ob.RQD_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pLK_PRIORITY_ID", Value = ob.LK_PRIORITY_ID},
                     new CommandParameter() {ParameterName = "pSP_INSTRUCTION", Value = ob.SP_INSTRUCTION},
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
            const string sp = "SP_MC_PRNSO_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PRNSO_REQ_D_ID", Value = ob.MC_PRNSO_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pMC_PRNSO_REQ_H_ID", Value = ob.MC_PRNSO_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value = ob.MC_STYLE_D_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRF_SMPL_TYPE_ID", Value = ob.RF_SMPL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_PRN_TYPE_LST", Value = ob.LK_PRN_TYPE_LST},
                     new CommandParameter() {ParameterName = "pPRN_COLOR_LST", Value = ob.PRN_COLOR_LST},
                     new CommandParameter() {ParameterName = "pRQD_QTY", Value = ob.RQD_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pLK_PRIORITY_ID", Value = ob.LK_PRIORITY_ID},
                     new CommandParameter() {ParameterName = "pSP_INSTRUCTION", Value = ob.SP_INSTRUCTION},
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

        public List<MC_PRNSO_REQ_DModel> SelectAll()
        {
            string sp = "Select_MC_PRNSO_REQ_D";
            try
            {
                var obList = new List<MC_PRNSO_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PRNSO_REQ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_PRNSO_REQ_DModel ob = new MC_PRNSO_REQ_DModel();
                    ob.MC_PRNSO_REQ_D_ID = (dr["MC_PRNSO_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_PRNSO_REQ_D_ID"]);
                    ob.MC_PRNSO_REQ_H_ID = (dr["MC_PRNSO_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_PRNSO_REQ_H_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.RF_SMPL_TYPE_ID = (dr["RF_SMPL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SMPL_TYPE_ID"]);
                    //ob.LK_PRN_TYPE_LST = (dr["LK_PRN_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_PRN_TYPE_LST"]);
                    //ob.PRN_COLOR_LST = (dr["PRN_COLOR_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRN_COLOR_LST"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.LK_PRIORITY_ID = (dr["LK_PRIORITY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PRIORITY_ID"]);
                    ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);
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

        public MC_PRNSO_REQ_DModel Select(long ID)
        {
            string sp = "Select_MC_PRNSO_REQ_D";
            try
            {
                var ob = new MC_PRNSO_REQ_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PRNSO_REQ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_PRNSO_REQ_D_ID = (dr["MC_PRNSO_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_PRNSO_REQ_D_ID"]);
                    ob.MC_PRNSO_REQ_H_ID = (dr["MC_PRNSO_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_PRNSO_REQ_H_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.RF_SMPL_TYPE_ID = (dr["RF_SMPL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SMPL_TYPE_ID"]);
                    //ob.LK_PRN_TYPE_LST = (dr["LK_PRN_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_PRN_TYPE_LST"]);
                    //ob.PRN_COLOR_LST = (dr["PRN_COLOR_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRN_COLOR_LST"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.LK_PRIORITY_ID = (dr["LK_PRIORITY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PRIORITY_ID"]);
                    ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);
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

    }
}