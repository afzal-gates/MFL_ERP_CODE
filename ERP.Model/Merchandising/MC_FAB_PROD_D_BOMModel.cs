using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.Xml.Serialization;
using System.IO;

namespace ERP.Model
{
    public class MC_FAB_PROD_D_BOMModel
    {
        public Int64 MC_FAB_PROD_D_BOM_ID { get; set; }
        public Int64 MC_FAB_PROD_H_BOM_ID { get; set; }
        public Int64 GMT_COLOR_ID { get; set; }
        public Int64 GMT_SIZE_ID { get; set; }
        public Decimal ORDER_QTY { get; set; }
        public Decimal PCT_ALLOW_QTY { get; set; }
        public Decimal CONS_QTY { get; set; }
        public Decimal BOOK_QTY { get; set; }
        public Decimal REV_QTY { get; set; }
        public Decimal NET_BOOK_QTY { get; set; }
        public Int64 CONS_MOU_ID { get; set; }
        public Int64 PURCH_MOU_ID { get; set; }
        public Decimal PO_QTY { get; set; }
        public Decimal RCV_QTY { get; set; }
        public Decimal ISSUE_QTY { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_MC_FAB_PROD_D_BOM";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_D_BOM_ID", Value = ob.MC_FAB_PROD_D_BOM_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_H_BOM_ID", Value = ob.MC_FAB_PROD_H_BOM_ID},
                     new CommandParameter() {ParameterName = "pGMT_COLOR_ID", Value = ob.GMT_COLOR_ID},
                     new CommandParameter() {ParameterName = "pGMT_SIZE_ID", Value = ob.GMT_SIZE_ID},
                     new CommandParameter() {ParameterName = "pORDER_QTY", Value = ob.ORDER_QTY},
                     new CommandParameter() {ParameterName = "pPCT_ALLOW_QTY", Value = ob.PCT_ALLOW_QTY},
                     new CommandParameter() {ParameterName = "pCONS_QTY", Value = ob.CONS_QTY},
                     new CommandParameter() {ParameterName = "pBOOK_QTY", Value = ob.BOOK_QTY},
                     new CommandParameter() {ParameterName = "pREV_QTY", Value = ob.REV_QTY},
                     new CommandParameter() {ParameterName = "pNET_BOOK_QTY", Value = ob.NET_BOOK_QTY},
                     new CommandParameter() {ParameterName = "pCONS_MOU_ID", Value = ob.CONS_MOU_ID},
                     new CommandParameter() {ParameterName = "pPURCH_MOU_ID", Value = ob.PURCH_MOU_ID},
                     new CommandParameter() {ParameterName = "pPO_QTY", Value = ob.PO_QTY},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pISSUE_QTY", Value = ob.ISSUE_QTY},
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
            const string sp = "SP_MC_FAB_PROD_D_BOM";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_D_BOM_ID", Value = ob.MC_FAB_PROD_D_BOM_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_H_BOM_ID", Value = ob.MC_FAB_PROD_H_BOM_ID},
                     new CommandParameter() {ParameterName = "pGMT_COLOR_ID", Value = ob.GMT_COLOR_ID},
                     new CommandParameter() {ParameterName = "pGMT_SIZE_ID", Value = ob.GMT_SIZE_ID},
                     new CommandParameter() {ParameterName = "pORDER_QTY", Value = ob.ORDER_QTY},
                     new CommandParameter() {ParameterName = "pPCT_ALLOW_QTY", Value = ob.PCT_ALLOW_QTY},
                     new CommandParameter() {ParameterName = "pCONS_QTY", Value = ob.CONS_QTY},
                     new CommandParameter() {ParameterName = "pBOOK_QTY", Value = ob.BOOK_QTY},
                     new CommandParameter() {ParameterName = "pREV_QTY", Value = ob.REV_QTY},
                     new CommandParameter() {ParameterName = "pNET_BOOK_QTY", Value = ob.NET_BOOK_QTY},
                     new CommandParameter() {ParameterName = "pCONS_MOU_ID", Value = ob.CONS_MOU_ID},
                     new CommandParameter() {ParameterName = "pPURCH_MOU_ID", Value = ob.PURCH_MOU_ID},
                     new CommandParameter() {ParameterName = "pPO_QTY", Value = ob.PO_QTY},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pISSUE_QTY", Value = ob.ISSUE_QTY},
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
            const string sp = "SP_MC_FAB_PROD_D_BOM";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_D_BOM_ID", Value = ob.MC_FAB_PROD_D_BOM_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_H_BOM_ID", Value = ob.MC_FAB_PROD_H_BOM_ID},
                     new CommandParameter() {ParameterName = "pGMT_COLOR_ID", Value = ob.GMT_COLOR_ID},
                     new CommandParameter() {ParameterName = "pGMT_SIZE_ID", Value = ob.GMT_SIZE_ID},
                     new CommandParameter() {ParameterName = "pORDER_QTY", Value = ob.ORDER_QTY},
                     new CommandParameter() {ParameterName = "pPCT_ALLOW_QTY", Value = ob.PCT_ALLOW_QTY},
                     new CommandParameter() {ParameterName = "pCONS_QTY", Value = ob.CONS_QTY},
                     new CommandParameter() {ParameterName = "pBOOK_QTY", Value = ob.BOOK_QTY},
                     new CommandParameter() {ParameterName = "pREV_QTY", Value = ob.REV_QTY},
                     new CommandParameter() {ParameterName = "pNET_BOOK_QTY", Value = ob.NET_BOOK_QTY},
                     new CommandParameter() {ParameterName = "pCONS_MOU_ID", Value = ob.CONS_MOU_ID},
                     new CommandParameter() {ParameterName = "pPURCH_MOU_ID", Value = ob.PURCH_MOU_ID},
                     new CommandParameter() {ParameterName = "pPO_QTY", Value = ob.PO_QTY},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pISSUE_QTY", Value = ob.ISSUE_QTY},
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

        public List<MC_FAB_PROD_D_BOMModel> SelectAll()
        {
            string sp = "Select_MC_FAB_PROD_D_BOM";
            try
            {
                var obList = new List<MC_FAB_PROD_D_BOMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_D_BOM_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROD_D_BOMModel ob = new MC_FAB_PROD_D_BOMModel();
                    ob.MC_FAB_PROD_D_BOM_ID = (dr["MC_FAB_PROD_D_BOM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_D_BOM_ID"]);
                    ob.MC_FAB_PROD_H_BOM_ID = (dr["MC_FAB_PROD_H_BOM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_H_BOM_ID"]);
                    ob.GMT_COLOR_ID = (dr["GMT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_COLOR_ID"]);
                    ob.GMT_SIZE_ID = (dr["GMT_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SIZE_ID"]);
                    ob.ORDER_QTY = (dr["ORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ORDER_QTY"]);
                    ob.PCT_ALLOW_QTY = (dr["PCT_ALLOW_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_ALLOW_QTY"]);
                    ob.CONS_QTY = (dr["CONS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONS_QTY"]);
                    ob.BOOK_QTY = (dr["BOOK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BOOK_QTY"]);
                    ob.REV_QTY = (dr["REV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REV_QTY"]);
                    ob.NET_BOOK_QTY = (dr["NET_BOOK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_BOOK_QTY"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    ob.PO_QTY = (dr["PO_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PO_QTY"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.ISSUE_QTY = (dr["ISSUE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_QTY"]);
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

        public MC_FAB_PROD_D_BOMModel Select(long ID)
        {
            string sp = "Select_MC_FAB_PROD_D_BOM";
            try
            {
                var ob = new MC_FAB_PROD_D_BOMModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_D_BOM_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_FAB_PROD_D_BOM_ID = (dr["MC_FAB_PROD_D_BOM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_D_BOM_ID"]);
                    ob.MC_FAB_PROD_H_BOM_ID = (dr["MC_FAB_PROD_H_BOM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_H_BOM_ID"]);
                    ob.GMT_COLOR_ID = (dr["GMT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_COLOR_ID"]);
                    ob.GMT_SIZE_ID = (dr["GMT_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SIZE_ID"]);
                    ob.ORDER_QTY = (dr["ORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ORDER_QTY"]);
                    ob.PCT_ALLOW_QTY = (dr["PCT_ALLOW_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_ALLOW_QTY"]);
                    ob.CONS_QTY = (dr["CONS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONS_QTY"]);
                    ob.BOOK_QTY = (dr["BOOK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BOOK_QTY"]);
                    ob.REV_QTY = (dr["REV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REV_QTY"]);
                    ob.NET_BOOK_QTY = (dr["NET_BOOK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_BOOK_QTY"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    ob.PO_QTY = (dr["PO_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PO_QTY"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.ISSUE_QTY = (dr["ISSUE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_QTY"]);
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

        public List<MC_FAB_PROD_D_BOMModel> SelectByID(long? pINV_ITEM_ID, long? pMC_FAB_PROD_ORD_H_ID, long? pSCM_PURC_REQ_H_ID)
        {
            string sp = "PKG_BUDGET_SHEET.MC_FAB_PROD_D_BOM_SELECT";
            try
            {
                var obList = new List<MC_FAB_PROD_D_BOMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value =pINV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value =pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value =pSCM_PURC_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROD_D_BOMModel ob = new MC_FAB_PROD_D_BOMModel();
                    ob.MC_FAB_PROD_H_BOM_ID = (dr["MC_FAB_PROD_H_BOM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_H_BOM_ID"]);
                    //ob.SCM_PURC_REQ_D_ACS_ID = (dr["SCM_PURC_REQ_D_ACS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_D_ACS_ID"]);                    
                    ob.SCM_PURC_REQ_ORD_ID = (dr["SCM_PURC_REQ_ORD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_ORD_ID"]);
                    ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);
                    ob.GMT_COLOR_ID = (dr["GMT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_COLOR_ID"]);
                    ob.GMT_SIZE_ID = (dr["GMT_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SIZE_ID"]);
                    ob.ORDER_QTY = (dr["ORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ORDER_QTY"]);
                    ob.PCT_ALLOW_QTY = (dr["PCT_ALLOW_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_ALLOW_QTY"]);
                    ob.CONS_QTY = (dr["CONS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONS_QTY"]);
                    ob.BOOK_QTY = (dr["BOOK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BOOK_QTY"]);
                    ob.REV_QTY = (dr["REV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REV_QTY"]);
                    ob.NET_BOOK_QTY = (dr["NET_BOOK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_BOOK_QTY"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    ob.PO_QTY = (dr["PO_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PO_QTY"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.ISSUE_QTY = (dr["ISSUE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_QTY"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    
                    ob.INV_ITEM_ID = pINV_ITEM_ID;
                    ob.MC_FAB_PROD_ORD_H_ID = pMC_FAB_PROD_ORD_H_ID;

                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.PROD_SIZE_CODE = (dr["PROD_SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROD_SIZE_CODE"]);
                    ob.PROD_COLOR_NAME_EN = (dr["PROD_COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROD_COLOR_NAME_EN"]);
                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.PARTICULARS = (dr["PARTICULARS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARTICULARS"]);
                    MC_ACCS_PO_TMPLT_CFGModel obj = new MC_ACCS_PO_TMPLT_CFGModel();
                    obj.CNTRL_NAME = "";
                    obj.COL_VAL = "";
                    ob.PARTICULARS_LST = new List<MC_ACCS_PO_TMPLT_CFGModel>();
                    ob.PARTICULARS_LST.Add(obj);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static List<MC_ACCS_PO_TMPLT_CFGModel> ReadXML(string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<MC_ACCS_PO_TMPLT_CFGModel>));

            // convert string to stream
            byte[] byteArray = Encoding.ASCII.GetBytes(xmlString);
            MemoryStream stream = new MemoryStream(byteArray);

            //// convert stream to string
            //StreamReader reader = new StreamReader(stream);
            //string text = reader.ReadToEnd();
            using (StreamReader reader = new StreamReader(stream))
            {
                List<MC_ACCS_PO_TMPLT_CFGModel> dezerializedList = (List<MC_ACCS_PO_TMPLT_CFGModel>)serializer.Deserialize(reader);
                return dezerializedList;
            }

        }

        public string COLOR_NAME_EN { get; set; }

        public string SIZE_CODE { get; set; }

        public string PROD_SIZE_CODE { get; set; }

        public string PROD_COLOR_NAME_EN { get; set; }

        public string ITEM_SPEC_AUTO { get; set; }

        public string PARTICULARS { get; set; }

        public string MOU_CODE { get; set; }

        public List<MC_ACCS_PO_TMPLT_CFGModel> PARTICULARS_LST { get; set; }

        public long? MC_FAB_PROD_ORD_H_ID { get; set; }

        public long? INV_ITEM_ID { get; set; }

        public long MC_ORD_TRMS_ITEM_ID { get; set; }

        public long SCM_PURC_REQ_D_ACS_ID { get; set; }

        public long SCM_PURC_REQ_ORD_ID { get; set; }

        public string ITEM_CODE { get; set; }
    }

}