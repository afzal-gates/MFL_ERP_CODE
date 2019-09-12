using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.ComponentModel;
using System.IO;

namespace ERP.Model
{
    public class InvReportModel
    {
        public string REPORT_CODE { get; set; }
        public string IS_EXCEL_FORMAT { get; set; }
        public string PAGE_SIZE_NAME { get; set; }
        public Int64? SCM_STR_RCV_H_ID { get; set; }



        public DataSet ItemListByUser()
        {
            var ob = this;
            string sp = "pkg_reports.inv_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"] },
		            new CommandParameter() {ParameterName = "pOption", Value = 3000}
                }, sp);

            ds.Tables[0].TableName = "ITEM_LIST";
            
            return ds;
        }

        public DataSet InvItemList()
        {
            var ob = this;
            string sp = "pkg_reports.inv_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                                    
		            new CommandParameter() {ParameterName = "pOption", Value = 3000}
                }, sp);

            ds.Tables[0].TableName = "INV_ITEM_LIST";                        

            return ds;
        }



        public DataSet GoodsReceiveNote()
        {
            var ob = this;
            string sp = "pkg_inv_report.inv_report_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pSCM_STR_RCV_H_ID", Value = ob.SCM_STR_RCV_H_ID },
		            new CommandParameter() {ParameterName = "pOption", Value = 3000}
                }, sp);

            ds.Tables[0].TableName = "MRR";

            return ds;
        }


        public DataSet GeneralStockTaking()
        {
            var ob = this;
            string sp = "pkg_inv_report.inv_report_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},  
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},  
                    new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},  
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},  
                    //new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},  
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},  
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},  
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value =  HttpContext.Current.Session["multiScUserId"]},  
		            new CommandParameter() {ParameterName = "pOption", Value = 3001}
                }, sp);

            ds.Tables[0].TableName = "MRR";

            return ds;
        }

        public DataSet GeneralCurrentStock()
        {
            var ob = this;
            string sp = "pkg_inv_report.inv_report_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},  
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},  
		            new CommandParameter() {ParameterName = "pOption", Value = 3002}
                }, sp);

            ds.Tables[0].TableName = "STOCK";

            return ds;
        }


        public DataSet GenItemReqSlip()
        {
            var ob = this;
            string sp = "pkg_inv_report.inv_report_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pSCM_STR_GEN_REQ_H_ID", Value = ob.SCM_STR_GEN_REQ_H_ID},   
		            new CommandParameter() {ParameterName = "pOption", Value = 3003}
                }, sp);

            ds.Tables[0].TableName = "Slip";

            return ds;
        }


        public DataSet GeneralStockLedger()
        {
            var ob = this;
            string sp = "pkg_inv_report.inv_report_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},                    
		            new CommandParameter() {ParameterName = "pOption", Value = 3004}

                }, sp);

            return ds;
        }


        private void LoadImage(DataRow objDataRow, string strImageField)
        {
            try
            {
                //FileStream fs = new FileStream(FilePath,
                //           System.IO.FileMode.Open, System.IO.FileAccess.Read);
                //byte[] Image = new byte[objDataRow[""].Length];
                //fs.Read(Image, 0, Convert.ToInt32(fs.Length));
                //fs.Close();
                //objDataRow[strImageField] = Image;
            }
            catch (Exception ex)
            {
               
            }
        }







        public DateTime? FROM_DATE { get; set; }
        public DateTime? TO_DATE { get; set; }
        public Int64? INV_ITEM_CAT_ID { get; set; }
        public Int64? HR_OFFICE_ID { get; set; }
        public Int64? HR_COMPANY_ID { get; set; }
        public Int64? SCM_STORE_ID { get; set; }

        public Int64? SCM_STR_GEN_REQ_H_ID { get; set; }

        public object INV_ITEM_ID { get; set; }
    }
}