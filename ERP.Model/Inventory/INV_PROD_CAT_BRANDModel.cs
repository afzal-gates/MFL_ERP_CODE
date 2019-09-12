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
    public class INV_PROD_CAT_BRANDModel
    {
        public Int64 INV_PROD_CAT_BRAND_ID { get; set; }
        public Int64 INV_ITEM_CAT_ID { get; set; }
        public Int64 RF_BRAND_ID { get; set; }

        public string BRAND_NAME_EN { get; set; }
        public string name { get; set; }
        
        //public DateTime CREATION_DATE { get; set; }
        //public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        //public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_INV_PROD_CAT_BRAND";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_PROD_CAT_BRAND_ID", Value = ob.INV_PROD_CAT_BRAND_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},                     
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

       
        public List<INV_PROD_CAT_BRANDModel> SelectAll()
        {
            string sp = "Select_INV_PROD_CAT_BRAND";
            try
            {
                var obList = new List<INV_PROD_CAT_BRANDModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_PROD_CAT_BRAND_ID", Value = 0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_PROD_CAT_BRANDModel ob = new INV_PROD_CAT_BRANDModel();
                    ob.INV_PROD_CAT_BRAND_ID = (dr["INV_PROD_CAT_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_PROD_CAT_BRAND_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public INV_PROD_CAT_BRANDModel Select(long ID)
        {
            string sp = "Select_INV_PROD_CAT_BRAND";
            try
            {
                var ob = new INV_PROD_CAT_BRANDModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_PROD_CAT_BRAND_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.INV_PROD_CAT_BRAND_ID = (dr["INV_PROD_CAT_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_PROD_CAT_BRAND_ID"]);
                        ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                        ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);                        
               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object getCategWiseBrandList(int? pINV_ITEM_CAT_ID = null)
        {
            string sp = "pkg_inventory.inv_item_cat_select";
            try
            {
                var obList = new List<INV_PROD_CAT_BRANDModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = pINV_ITEM_CAT_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3008},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_PROD_CAT_BRANDModel ob = new INV_PROD_CAT_BRANDModel();
                    ob.INV_PROD_CAT_BRAND_ID = (dr["INV_PROD_CAT_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_PROD_CAT_BRAND_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.name = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}