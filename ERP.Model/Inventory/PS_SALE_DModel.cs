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
    public class PS_SALE_DModel
    {
        public Int64 PS_SALE_D_ID { get; set; }
        public Int64 PS_SALE_H_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Decimal SOLD_QTY { get; set; }
        public Decimal UNIT_PRICE { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string PARTICULARS { get; set; }
        public string IS_DELETED { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_PS_SALE_D";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPS_SALE_D_ID", Value = ob.PS_SALE_D_ID},
                     new CommandParameter() {ParameterName = "pPS_SALE_H_ID", Value = ob.PS_SALE_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pSOLD_QTY", Value = ob.SOLD_QTY},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pPARTICULARS", Value = ob.PARTICULARS},
                     new CommandParameter() {ParameterName = "pIS_DELETED", Value = ob.IS_DELETED},
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
            const string sp = "SP_PS_SALE_D";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPS_SALE_D_ID", Value = ob.PS_SALE_D_ID},
                     new CommandParameter() {ParameterName = "pPS_SALE_H_ID", Value = ob.PS_SALE_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pSOLD_QTY", Value = ob.SOLD_QTY},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pPARTICULARS", Value = ob.PARTICULARS},
                     new CommandParameter() {ParameterName = "pIS_DELETED", Value = ob.IS_DELETED},
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
            const string sp = "SP_PS_SALE_D";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPS_SALE_D_ID", Value = ob.PS_SALE_D_ID},
                     new CommandParameter() {ParameterName = "pPS_SALE_H_ID", Value = ob.PS_SALE_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pSOLD_QTY", Value = ob.SOLD_QTY},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pPARTICULARS", Value = ob.PARTICULARS},
                     new CommandParameter() {ParameterName = "pIS_DELETED", Value = ob.IS_DELETED},
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

        public List<PS_SALE_DModel> SelectAll()
        {
            string sp = "Select_PS_SALE_D";
            try
            {
                var obList = new List<PS_SALE_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPS_SALE_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            PS_SALE_DModel ob = new PS_SALE_DModel();
                            ob.PS_SALE_D_ID = (dr["PS_SALE_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PS_SALE_D_ID"]);
                            ob.PS_SALE_H_ID = (dr["PS_SALE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PS_SALE_H_ID"]);
                            ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                            ob.SOLD_QTY = (dr["SOLD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SOLD_QTY"]);
                            ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                            ob.PARTICULARS = (dr["PARTICULARS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARTICULARS"]);
                            ob.IS_DELETED = (dr["IS_DELETED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DELETED"]);
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

        public PS_SALE_DModel Select(long ID)
        {
            string sp = "Select_PS_SALE_D";
            try
            {
                var ob = new PS_SALE_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPS_SALE_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.PS_SALE_D_ID = (dr["PS_SALE_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PS_SALE_D_ID"]);
                        ob.PS_SALE_H_ID = (dr["PS_SALE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PS_SALE_H_ID"]);
                        ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                        ob.SOLD_QTY = (dr["SOLD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SOLD_QTY"]);
                        ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                        ob.PARTICULARS = (dr["PARTICULARS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARTICULARS"]);
                        ob.IS_DELETED = (dr["IS_DELETED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DELETED"]);
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