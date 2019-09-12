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
    public class MC_PRNSO_REQ_HModel
    {
        public Int64 MC_PRNSO_REQ_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 MC_BYR_ACC_ID { get; set; }
        public Int64 MC_BUYER_ID { get; set; }
        public string PRNSO_REQ_NO { get; set; }
        public DateTime PRNSO_REQ_DT { get; set; }
        public Int64 PRNSO_REQ_BY { get; set; }
        public string PRNSO_REQ_TO_LST { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 VERSION_NO { get; set; }

        public List<MC_PRNSO_REQ_DModel> items { get; set; }

        public string SaveBatchData()
        {
            const string sp = "PKG_Print_Strike.mc_prnso_req_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;

            var xml = new System.Text.StringBuilder();
            xml.Append("<trans>");
            foreach (var line in ob.items)
            {                
                var vPrintTypeXML = new System.Text.StringBuilder();
                vPrintTypeXML.Append("-trans+");
                foreach (var itm in line.LK_PRN_TYPE_LST)
                {
                    vPrintTypeXML.AppendLine();
                    vPrintTypeXML.Append(" -row ");
                    vPrintTypeXML.Append(" LOOKUP_DATA_ID=:" + itm.LOOKUP_DATA_ID + ":");
                    vPrintTypeXML.Append(" LK_DATA_NAME_EN=:" + itm.LK_DATA_NAME_EN + ":");
                    vPrintTypeXML.Append(" /+");
                }
                vPrintTypeXML.AppendLine();
                vPrintTypeXML.AppendLine("-/trans+");

                var vPrintColorXML = new System.Text.StringBuilder();
                vPrintColorXML.Append("-trans+");
                foreach (var itm in line.PRN_COLOR_LST)
                {
                    vPrintColorXML.AppendLine();
                    vPrintColorXML.Append(" -row ");
                    vPrintColorXML.Append(" MC_COLOR_ID=:" + itm.MC_COLOR_ID + ":");
                    vPrintColorXML.Append(" COLOR_NAME_EN=:" + itm.COLOR_NAME_EN + ":");
                    vPrintColorXML.Append(" /+");
                }
                vPrintColorXML.AppendLine();
                vPrintColorXML.AppendLine("-/trans+");

                xml.AppendLine();
                xml.Append(" <row ");
                xml.Append(" MC_PRNSO_REQ_D_ID=\"" + line.MC_PRNSO_REQ_D_ID + "\"");
                xml.Append(" MC_PRNSO_REQ_H_ID=\"" + line.MC_PRNSO_REQ_H_ID + "\"");
                xml.Append(" MC_ORDER_H_ID=\"" + line.MC_ORDER_H_ID + "\"");
                xml.Append(" MC_STYLE_D_ITEM_ID=\"" + line.MC_STYLE_D_ITEM_ID + "\"");
                xml.Append(" RF_SMPL_TYPE_ID=\"" + line.RF_SMPL_TYPE_ID + "\"");
                xml.Append(" LK_PRN_TYPE_LST=\"" + vPrintTypeXML + "\"");
                xml.Append(" PRN_COLOR_LST=\"" + vPrintColorXML + "\"");
                xml.Append(" RQD_QTY=\"" + line.RQD_QTY + "\"");
                xml.Append(" QTY_MOU_ID=\"" + line.QTY_MOU_ID + "\"");
                xml.Append(" LK_PRIORITY_ID=\"" + line.LK_PRIORITY_ID + "\"");
                xml.Append(" SP_INSTRUCTION=\"" + line.SP_INSTRUCTION + "\"");
                xml.Append(" CREATED_BY=\"" + HttpContext.Current.Session["multiScUserId"] + "\"");
                xml.Append(" />");
            }
            xml.AppendLine();
            xml.AppendLine("</trans>");

            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {  
                     new CommandParameter() {ParameterName = "pMC_PRNSO_REQ_H_ID", Value = ob.MC_PRNSO_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pPRNSO_REQ_NO", Value = ob.PRNSO_REQ_NO},
                     new CommandParameter() {ParameterName = "pPRNSO_REQ_DT", Value = ob.PRNSO_REQ_DT},
                     new CommandParameter() {ParameterName = "pPRNSO_REQ_BY", Value = ob.PRNSO_REQ_BY},
                     new CommandParameter() {ParameterName = "pPRNSO_REQ_TO_LST", Value = ob.PRNSO_REQ_TO_LST},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     //new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pMC_PRNSO_REQ_D", Value = xml},
                     new CommandParameter() {ParameterName = "V_MC_PRNSO_REQ_H_ID", Value =500, Direction = ParameterDirection.Output}
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

        public string Save()
        {
            const string sp = "SP_MC_PRNSO_REQ_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PRNSO_REQ_H_ID", Value = ob.MC_PRNSO_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pPRNSO_REQ_NO", Value = ob.PRNSO_REQ_NO},
                     new CommandParameter() {ParameterName = "pPRNSO_REQ_DT", Value = ob.PRNSO_REQ_DT},
                     new CommandParameter() {ParameterName = "pPRNSO_REQ_BY", Value = ob.PRNSO_REQ_BY},
                     new CommandParameter() {ParameterName = "pPRNSO_REQ_TO_LST", Value = ob.PRNSO_REQ_TO_LST},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
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
            const string sp = "SP_MC_PRNSO_REQ_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PRNSO_REQ_H_ID", Value = ob.MC_PRNSO_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pPRNSO_REQ_NO", Value = ob.PRNSO_REQ_NO},
                     new CommandParameter() {ParameterName = "pPRNSO_REQ_DT", Value = ob.PRNSO_REQ_DT},
                     new CommandParameter() {ParameterName = "pPRNSO_REQ_BY", Value = ob.PRNSO_REQ_BY},
                     new CommandParameter() {ParameterName = "pPRNSO_REQ_TO_LST", Value = ob.PRNSO_REQ_TO_LST},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
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
            const string sp = "SP_MC_PRNSO_REQ_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PRNSO_REQ_H_ID", Value = ob.MC_PRNSO_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pPRNSO_REQ_NO", Value = ob.PRNSO_REQ_NO},
                     new CommandParameter() {ParameterName = "pPRNSO_REQ_DT", Value = ob.PRNSO_REQ_DT},
                     new CommandParameter() {ParameterName = "pPRNSO_REQ_BY", Value = ob.PRNSO_REQ_BY},
                     new CommandParameter() {ParameterName = "pPRNSO_REQ_TO_LST", Value = ob.PRNSO_REQ_TO_LST},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
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

        public List<MC_PRNSO_REQ_HModel> SelectAll()
        {
            string sp = "Select_MC_PRNSO_REQ_H";
            try
            {
                var obList = new List<MC_PRNSO_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PRNSO_REQ_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_PRNSO_REQ_HModel ob = new MC_PRNSO_REQ_HModel();
                    ob.MC_PRNSO_REQ_H_ID = (dr["MC_PRNSO_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_PRNSO_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.PRNSO_REQ_NO = (dr["PRNSO_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRNSO_REQ_NO"]);
                    ob.PRNSO_REQ_DT = (dr["PRNSO_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PRNSO_REQ_DT"]);
                    ob.PRNSO_REQ_BY = (dr["PRNSO_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRNSO_REQ_BY"]);
                    ob.PRNSO_REQ_TO_LST = (dr["PRNSO_REQ_TO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRNSO_REQ_TO_LST"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_PRNSO_REQ_HModel Select(long ID)
        {
            string sp = "Select_MC_PRNSO_REQ_H";
            try
            {
                var ob = new MC_PRNSO_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PRNSO_REQ_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_PRNSO_REQ_H_ID = (dr["MC_PRNSO_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_PRNSO_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.PRNSO_REQ_NO = (dr["PRNSO_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRNSO_REQ_NO"]);
                    ob.PRNSO_REQ_DT = (dr["PRNSO_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PRNSO_REQ_DT"]);
                    ob.PRNSO_REQ_BY = (dr["PRNSO_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRNSO_REQ_BY"]);
                    ob.PRNSO_REQ_TO_LST = (dr["PRNSO_REQ_TO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRNSO_REQ_TO_LST"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_PRNSO_REQ_HModel> PrintStrikeOffListByBuyerAcc(Int64 pMC_BYR_ACC_ID)
        {
            string sp = "PKG_Print_Strike.mc_prnso_req_h_select";
            try
            {
                var obList = new List<MC_PRNSO_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_PRNSO_REQ_HModel ob = new MC_PRNSO_REQ_HModel();
                    ob.MC_PRNSO_REQ_H_ID = (dr["MC_PRNSO_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_PRNSO_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.PRNSO_REQ_NO = (dr["PRNSO_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRNSO_REQ_NO"]);
                    ob.PRNSO_REQ_DT = (dr["PRNSO_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PRNSO_REQ_DT"]);
                    ob.PRNSO_REQ_BY = (dr["PRNSO_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRNSO_REQ_BY"]);
                    ob.PRNSO_REQ_TO_LST = (dr["PRNSO_REQ_TO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRNSO_REQ_TO_LST"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);

                    sp = "PKG_Print_Strike.mc_prnso_req_h_select";
                    List<MC_PRNSO_REQ_DModel> obDList = new List<MC_PRNSO_REQ_DModel>();
                    var DetailsDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                         new CommandParameter() {ParameterName = "pMC_PRNSO_REQ_H_ID", Value = ob.MC_PRNSO_REQ_H_ID},
                         new CommandParameter() {ParameterName = "pOption", Value = 3003},
                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                    }, sp);
                    foreach (DataRow drDet in DetailsDs.Tables[0].Rows)
                    {
                        MC_PRNSO_REQ_DModel obD = new MC_PRNSO_REQ_DModel();
                        obD.MC_PRNSO_REQ_D_ID = (drDet["MC_PRNSO_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_PRNSO_REQ_D_ID"]);
                        obD.MC_PRNSO_REQ_H_ID = (drDet["MC_PRNSO_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_PRNSO_REQ_H_ID"]);
                        obD.MC_ORDER_H_ID = (drDet["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_ORDER_H_ID"]);
                        obD.MC_STYLE_D_ITEM_ID = (drDet["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_STYLE_D_ITEM_ID"]);
                        obD.RF_SMPL_TYPE_ID = (drDet["RF_SMPL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["RF_SMPL_TYPE_ID"]);
                        
                        obD.pLK_PRN_TYPE_LST = (drDet["LK_PRN_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["LK_PRN_TYPE_LST"]);
                        List<LookupDataModel> obLkList = new List<LookupDataModel>();
                        var LkDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                             new CommandParameter() {ParameterName = "pLK_PRN_TYPE_LST", Value =  obD.pLK_PRN_TYPE_LST},
                             new CommandParameter() {ParameterName = "pOption", Value = 3004},
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                        }, sp);
                        foreach (DataRow drlk in LkDs.Tables[0].Rows)
                        {
                            LookupDataModel obLk = new LookupDataModel();
                            obLk.LOOKUP_DATA_ID = (drlk["LOOKUP_DATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drlk["LOOKUP_DATA_ID"]);
                            obLk.LK_DATA_NAME_EN = (drlk["LK_DATA_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drlk["LK_DATA_NAME_EN"]);
                            obLkList.Add(obLk);
                        }
                        obD.LK_PRN_TYPE_LST = obLkList;


                        obD.pPRN_COLOR_LST = (drDet["PRN_COLOR_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["PRN_COLOR_LST"]);
                        List<MC_COLORModel> obColorList = new List<MC_COLORModel>();
                        var PcolorDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                             new CommandParameter() {ParameterName = "pPRN_COLOR_LST", Value =  obD.pPRN_COLOR_LST},
                             new CommandParameter() {ParameterName = "pOption", Value = 3005},
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                        }, sp);
                        foreach (DataRow drpColor in PcolorDs.Tables[0].Rows)
                        {
                            MC_COLORModel obpColor = new MC_COLORModel();
                            obpColor.MC_COLOR_ID = (drpColor["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drpColor["MC_COLOR_ID"]);
                            obpColor.COLOR_NAME_EN = (drpColor["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drpColor["COLOR_NAME_EN"]);
                            obColorList.Add(obpColor);
                        }
                        obD.PRN_COLOR_LST = obColorList;

                        
                        obD.RQD_QTY = (drDet["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["RQD_QTY"]);
                        obD.QTY_MOU_ID = (drDet["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["QTY_MOU_ID"]);
                        obD.LK_PRIORITY_ID = (drDet["LK_PRIORITY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["LK_PRIORITY_ID"]);
                        obD.SP_INSTRUCTION = (drDet["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["SP_INSTRUCTION"]);
                        obD.CREATION_DATE = (drDet["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drDet["CREATION_DATE"]);
                        obD.CREATED_BY = (drDet["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["CREATED_BY"]);
                        obD.LAST_UPDATE_DATE = (drDet["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drDet["LAST_UPDATE_DATE"]);
                        obD.LAST_UPDATED_BY = (drDet["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["LAST_UPDATED_BY"]);

                        obD.STYLE_NO = (drDet["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["STYLE_NO"]);
                        obD.ORDER_NO = (drDet["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["ORDER_NO"]);
                        obD.ITEM_NAME_EN = (drDet["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["ITEM_NAME_EN"]);
                        obD.SMPL_TYPE_NAME = (drDet["SMPL_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["SMPL_TYPE_NAME"]);
                        obD.MOU_CODE = (drDet["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["MOU_CODE"]);
                        obD.LK_PRIORITY_NAME = (drDet["LK_PRIORITY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["LK_PRIORITY_NAME"]);
                        obD.SHIP_DT = (drDet["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drDet["SHIP_DT"]);
                        obD.MATERIAL_DESC = (drDet["MATERIAL_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["MATERIAL_DESC"]);
                        obD.NatureOfWork = (drDet["NatureOfWork"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["NatureOfWork"]);
                        obD.MC_STYLE_H_ID = (drDet["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_STYLE_H_ID"]);

                        obDList.Add(obD);
                    }
                    ob.items = obDList;
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public MC_PRNSO_REQ_HModel PrintStrikeOffListByHID(Int64 pMC_PRNSO_REQ_H_ID)
        {
            string sp = "PKG_Print_Strike.mc_prnso_req_h_select";
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PRNSO_REQ_H_ID", Value = pMC_PRNSO_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_PRNSO_REQ_HModel ob = new MC_PRNSO_REQ_HModel();
                    ob.MC_PRNSO_REQ_H_ID = (dr["MC_PRNSO_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_PRNSO_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.PRNSO_REQ_NO = (dr["PRNSO_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRNSO_REQ_NO"]);
                    ob.PRNSO_REQ_DT = (dr["PRNSO_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PRNSO_REQ_DT"]);
                    ob.PRNSO_REQ_BY = (dr["PRNSO_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRNSO_REQ_BY"]);
                    ob.PRNSO_REQ_TO_LST = (dr["PRNSO_REQ_TO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRNSO_REQ_TO_LST"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);

                    sp = "PKG_Print_Strike.mc_prnso_req_h_select";
                    List<MC_PRNSO_REQ_DModel> obDList = new List<MC_PRNSO_REQ_DModel>();
                    var DetailsDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                         new CommandParameter() {ParameterName = "pMC_PRNSO_REQ_H_ID", Value = ob.MC_PRNSO_REQ_H_ID},
                         new CommandParameter() {ParameterName = "pOption", Value = 3003},
                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                    }, sp);
                    foreach (DataRow drDet in DetailsDs.Tables[0].Rows)
                    {
                        MC_PRNSO_REQ_DModel obD = new MC_PRNSO_REQ_DModel();
                        obD.MC_PRNSO_REQ_D_ID = (drDet["MC_PRNSO_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_PRNSO_REQ_D_ID"]);
                        obD.MC_PRNSO_REQ_H_ID = (drDet["MC_PRNSO_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_PRNSO_REQ_H_ID"]);
                        obD.MC_ORDER_H_ID = (drDet["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_ORDER_H_ID"]);
                        obD.MC_STYLE_D_ITEM_ID = (drDet["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_STYLE_D_ITEM_ID"]);
                        obD.RF_SMPL_TYPE_ID = (drDet["RF_SMPL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["RF_SMPL_TYPE_ID"]);
                        
                        obD.pLK_PRN_TYPE_LST = (drDet["LK_PRN_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["LK_PRN_TYPE_LST"]);
                        List<LookupDataModel> obLkList = new List<LookupDataModel>();
                        var LkDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                             new CommandParameter() {ParameterName = "pLK_PRN_TYPE_LST", Value =  obD.pLK_PRN_TYPE_LST},
                             new CommandParameter() {ParameterName = "pOption", Value = 3004},
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                        }, sp);
                        foreach (DataRow drlk in LkDs.Tables[0].Rows)
                        {
                            LookupDataModel obLk = new LookupDataModel();
                            obLk.LOOKUP_DATA_ID = (drlk["LOOKUP_DATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drlk["LOOKUP_DATA_ID"]);
                            obLk.LK_DATA_NAME_EN = (drlk["LK_DATA_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drlk["LK_DATA_NAME_EN"]);
                            obLkList.Add(obLk);
                        }
                        obD.LK_PRN_TYPE_LST = obLkList;

                        obD.pPRN_COLOR_LST = (drDet["PRN_COLOR_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["PRN_COLOR_LST"]);
                        List<MC_COLORModel> obColorList = new List<MC_COLORModel>();
                        var PcolorDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                             new CommandParameter() {ParameterName = "pPRN_COLOR_LST", Value =  obD.pPRN_COLOR_LST},
                             new CommandParameter() {ParameterName = "pOption", Value = 3005},
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                        }, sp);
                        foreach (DataRow drpColor in PcolorDs.Tables[0].Rows)
                        {
                            MC_COLORModel obpColor = new MC_COLORModel();
                            obpColor.MC_COLOR_ID = (drpColor["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drpColor["MC_COLOR_ID"]);
                            obpColor.COLOR_NAME_EN = (drpColor["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drpColor["COLOR_NAME_EN"]);
                            obColorList.Add(obpColor);
                        }
                        obD.PRN_COLOR_LST = obColorList;

                        obD.RQD_QTY = (drDet["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["RQD_QTY"]);
                        obD.QTY_MOU_ID = (drDet["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["QTY_MOU_ID"]);
                        obD.LK_PRIORITY_ID = (drDet["LK_PRIORITY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["LK_PRIORITY_ID"]);
                        obD.SP_INSTRUCTION = (drDet["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["SP_INSTRUCTION"]);
                        obD.CREATION_DATE = (drDet["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drDet["CREATION_DATE"]);
                        obD.CREATED_BY = (drDet["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["CREATED_BY"]);
                        obD.LAST_UPDATE_DATE = (drDet["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drDet["LAST_UPDATE_DATE"]);
                        obD.LAST_UPDATED_BY = (drDet["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["LAST_UPDATED_BY"]);

                        obD.STYLE_NO = (drDet["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["STYLE_NO"]);
                        obD.ORDER_NO = (drDet["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["ORDER_NO"]);
                        obD.ITEM_NAME_EN = (drDet["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["ITEM_NAME_EN"]);
                        obD.SMPL_TYPE_NAME = (drDet["SMPL_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["SMPL_TYPE_NAME"]);
                        obD.MOU_CODE = (drDet["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["MOU_CODE"]);
                        obD.LK_PRIORITY_NAME = (drDet["LK_PRIORITY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["LK_PRIORITY_NAME"]);
                        obD.SHIP_DT = (drDet["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drDet["SHIP_DT"]);
                        obD.MATERIAL_DESC = (drDet["MATERIAL_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["MATERIAL_DESC"]);
                        obD.NatureOfWork = (drDet["NatureOfWork"] == DBNull.Value) ? string.Empty : Convert.ToString(drDet["NatureOfWork"]);
                        obD.MC_STYLE_H_ID = (drDet["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDet["MC_STYLE_H_ID"]);

                        obDList.Add(obD);
                    }
                    ob.items = obDList;
                    return ob;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
