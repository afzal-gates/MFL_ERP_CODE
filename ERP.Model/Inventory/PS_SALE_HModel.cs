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
    public class PS_SALE_HModel
    {
        public Int64 PS_SALE_H_ID { get; set; }
        public string MEMO_NO { get; set; }
        [Required(ErrorMessage="Memo date required")]
        public DateTime MEMO_DT { get; set; }
        //[Required(ErrorMessage = "Employee/customer must be required")]
        public Int64? SOLD_TO { get; set; }
        [Required(ErrorMessage = "Sold by must be required")]
        public Int64 SOLD_BY { get; set; }
        [Required(ErrorMessage = "Counter# must be required")]
        public Int64? PS_COUNTR_ID { get; set; }
        public Decimal CRD_LMT_EARNED { get; set; }
        public Decimal ALREADY_SOLD_AMT { get; set; }
        public Decimal SOLD_AMT { get; set; }
        public Decimal DISC_PCT { get; set; }
        public Decimal DISC_AMT { get; set; }
        public string REMARKS { get; set; }
        
        public string CUST_NAME { get; set; }
        public string CUST_MOB { get; set; }
        public Int64 LK_CUST_TYPE_ID { get; set; }

        public Decimal VAT_PCT { get; set; }
        public Decimal VAT_AMT { get; set; }

        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public List<PS_SALE_DModel> SalesDtlItem { get; set; }


        public string Save()
        {
            const string sp = "SP_PS_SALE_H";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPS_SALE_H_ID", Value = ob.PS_SALE_H_ID},
                     new CommandParameter() {ParameterName = "pMEMO_NO", Value = ob.MEMO_NO},
                     new CommandParameter() {ParameterName = "pMEMO_DT", Value = ob.MEMO_DT},
                     new CommandParameter() {ParameterName = "pSOLD_TO", Value = ob.SOLD_TO},
                     new CommandParameter() {ParameterName = "pSOLD_BY", Value = ob.SOLD_BY},
                     new CommandParameter() {ParameterName = "pPS_COUNTR_ID", Value = ob.PS_COUNTR_ID},
                     new CommandParameter() {ParameterName = "pCRD_LMT_EARNED", Value = ob.CRD_LMT_EARNED},
                     new CommandParameter() {ParameterName = "pSOLD_AMT", Value = ob.SOLD_AMT},
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
                     jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                    if (i < ds.Tables["OUTPARAM"].Rows.Count) 
                     { 
                      jsonStr += ",";
                     }  
                     else  
                      {  
                          jsonStr += "}"; 
                      }  
                         i++ ;
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
            const string sp = "SP_PS_SALE_H";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPS_SALE_H_ID", Value = ob.PS_SALE_H_ID},
                     new CommandParameter() {ParameterName = "pMEMO_NO", Value = ob.MEMO_NO},
                     new CommandParameter() {ParameterName = "pMEMO_DT", Value = ob.MEMO_DT},
                     new CommandParameter() {ParameterName = "pSOLD_TO", Value = ob.SOLD_TO},
                     new CommandParameter() {ParameterName = "pSOLD_BY", Value = ob.SOLD_BY},
                     new CommandParameter() {ParameterName = "pPS_COUNTR_ID", Value = ob.PS_COUNTR_ID},
                     new CommandParameter() {ParameterName = "pCRD_LMT_EARNED", Value = ob.CRD_LMT_EARNED},
                     new CommandParameter() {ParameterName = "pSOLD_AMT", Value = ob.SOLD_AMT},
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
                     jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                    if (i < ds.Tables["OUTPARAM"].Rows.Count) 
                     { 
                      jsonStr += ",";
                     }  
                     else  
                      {  
                          jsonStr += "}"; 
                      }  
                         i++ ;
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
            const string sp = "SP_PS_SALE_H";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPS_SALE_H_ID", Value = ob.PS_SALE_H_ID},
                     new CommandParameter() {ParameterName = "pMEMO_NO", Value = ob.MEMO_NO},
                     new CommandParameter() {ParameterName = "pMEMO_DT", Value = ob.MEMO_DT},
                     new CommandParameter() {ParameterName = "pSOLD_TO", Value = ob.SOLD_TO},
                     new CommandParameter() {ParameterName = "pSOLD_BY", Value = ob.SOLD_BY},
                     new CommandParameter() {ParameterName = "pPS_COUNTR_ID", Value = ob.PS_COUNTR_ID},
                     new CommandParameter() {ParameterName = "pCRD_LMT_EARNED", Value = ob.CRD_LMT_EARNED},
                     new CommandParameter() {ParameterName = "pSOLD_AMT", Value = ob.SOLD_AMT},
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
                     jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                    if (i < ds.Tables["OUTPARAM"].Rows.Count) 
                     { 
                      jsonStr += ",";
                     }  
                     else  
                      {  
                          jsonStr += "}"; 
                      }  
                         i++ ;
                 }
                 return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PS_SALE_HModel> SelectAll()
        {
            string sp = "Select_PS_SALE_H";
            try
            {
                var obList = new List<PS_SALE_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPS_SALE_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            PS_SALE_HModel ob = new PS_SALE_HModel();
                            ob.PS_SALE_H_ID = (dr["PS_SALE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PS_SALE_H_ID"]);
                            ob.MEMO_NO = (dr["MEMO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MEMO_NO"]);
                            ob.MEMO_DT = (dr["MEMO_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["MEMO_DT"]);
                            ob.SOLD_TO = (dr["SOLD_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SOLD_TO"]);
                            ob.SOLD_BY = (dr["SOLD_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SOLD_BY"]);
                            ob.PS_COUNTR_ID = (dr["PS_COUNTR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PS_COUNTR_ID"]);
                            ob.CRD_LMT_EARNED = (dr["CRD_LMT_EARNED"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CRD_LMT_EARNED"]);
                            ob.SOLD_AMT = (dr["SOLD_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SOLD_AMT"]);
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

        public PS_SALE_HModel Select(long ID)
        {
            string sp = "Select_PS_SALE_H";
            try
            {
                var ob = new PS_SALE_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPS_SALE_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.PS_SALE_H_ID = (dr["PS_SALE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PS_SALE_H_ID"]);
                        ob.MEMO_NO = (dr["MEMO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MEMO_NO"]);
                        ob.MEMO_DT = (dr["MEMO_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["MEMO_DT"]);
                        ob.SOLD_TO = (dr["SOLD_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SOLD_TO"]);
                        ob.SOLD_BY = (dr["SOLD_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SOLD_BY"]);
                        ob.PS_COUNTR_ID = (dr["PS_COUNTR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PS_COUNTR_ID"]);
                        ob.CRD_LMT_EARNED = (dr["CRD_LMT_EARNED"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CRD_LMT_EARNED"]);
                        ob.SOLD_AMT = (dr["SOLD_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SOLD_AMT"]);
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

        public string BatchSavePOSItem()
        {
            const string sp = "pkg_fair_shop.batch_save_pos_item";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            var xml = new System.Text.StringBuilder();
            xml.Append("<trans>");
            foreach (var line in ob.SalesDtlItem)
            {
                xml.AppendLine();
                xml.Append(" <row ");
                xml.Append(" PS_SALE_D_ID=\"" + line.PS_SALE_D_ID + "\"");
                xml.Append(" PS_SALE_H_ID=\"" + ob.PS_SALE_H_ID + "\"");
                xml.Append(" INV_ITEM_ID=\"" + line.INV_ITEM_ID + "\"");
                xml.Append(" SOLD_QTY=\"" + line.SOLD_QTY + "\"");
                xml.Append(" QTY_MOU_ID=\"" + line.QTY_MOU_ID + "\"");
                xml.Append(" UNIT_PRICE=\"" + line.UNIT_PRICE + "\"");
                xml.Append(" IS_DELETED=\"" + line.IS_DELETED + "\"");
                xml.Append(" CREATED_BY=\"" + line.CREATED_BY + "\"");
                xml.Append(" />");
            }
            xml.AppendLine();
            xml.AppendLine("</trans>");
            
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPS_SALE_H_ID", Value = ob.PS_SALE_H_ID},
                     new CommandParameter() {ParameterName = "pMEMO_NO", Value = ob.MEMO_NO},
                     new CommandParameter() {ParameterName = "pMEMO_DT", Value = ob.MEMO_DT},
                     new CommandParameter() {ParameterName = "pSOLD_TO", Value = ob.SOLD_TO},
                     new CommandParameter() {ParameterName = "pSOLD_BY", Value = ob.SOLD_BY},
                     new CommandParameter() {ParameterName = "pPS_COUNTR_ID", Value = ob.PS_COUNTR_ID},
                     new CommandParameter() {ParameterName = "pCRD_LMT_EARNED", Value = ob.CRD_LMT_EARNED},
                     new CommandParameter() {ParameterName = "pSOLD_AMT", Value = ob.SOLD_AMT},
                     new CommandParameter() {ParameterName = "pDISC_PCT", Value = ob.DISC_PCT},
                     new CommandParameter() {ParameterName = "pDISC_AMT", Value = ob.DISC_AMT},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},              

                     new CommandParameter() {ParameterName = "pCUST_NAME", Value = ob.CUST_NAME},
                     new CommandParameter() {ParameterName = "pCUST_MOB", Value = ob.CUST_MOB},
                     new CommandParameter() {ParameterName = "pLK_CUST_TYPE_ID", Value = ob.LK_CUST_TYPE_ID},

                     new CommandParameter() {ParameterName = "pVAT_PCT", Value = ob.VAT_PCT},
                     new CommandParameter() {ParameterName = "pVAT_AMT", Value = ob.VAT_AMT},

                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pSalesDtlItem", Value = xml.ToString()},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pSAVE_MEMO_NO", Value =500, Direction = ParameterDirection.Output}
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


    }
}