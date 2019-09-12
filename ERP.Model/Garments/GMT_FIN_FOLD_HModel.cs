using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.Dynamic;

namespace ERP.Model
{
    public class GMT_FIN_FOLD_HModel
    {
        public Int64 GMT_FIN_FOLD_H_ID { get; set; }
        public Int64 HR_PROD_FLR_ID { get; set; }
        public Int64 GMT_PROD_PLN_CLNDR_ID { get; set; }
        public string FLOOR_DESC_EN { get; set; }
        public Int64 HR_NO { get; set; }
        public string MC_ORDER_H_LST { get; set; }
        public string HR_NAME { get; set; }
        public string GMT_FIN_FOLD_H_XML { get; set; }
        public string GMT_FIN_FOLD_D_XML { get; set; }

        public List<ExpandoObject> itemsOrder { get; set; }


        private List<GMT_FIN_FOLD_DModel> _itemsGmtFinFoldDtl = null;
        public List<GMT_FIN_FOLD_DModel> itemsGmtFinFoldDtl
        {
            get
            {
                if (_itemsGmtFinFoldDtl == null)
                {
                    _itemsGmtFinFoldDtl = new List<GMT_FIN_FOLD_DModel>();
                }
                return _itemsGmtFinFoldDtl;
            }
            set
            {
                _itemsGmtFinFoldDtl = value;
            }
        }


        public string BatchSaveGmtFinFold()
        {
            const string sp = "pkg_gmt_fin.gmt_fin_fold_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_FIN_FOLD_H_XML", Value = ob.GMT_FIN_FOLD_H_XML},
                     new CommandParameter() {ParameterName = "pGMT_FIN_FOLD_D_XML", Value = ob.GMT_FIN_FOLD_D_XML},
             
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
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


        public List<ExpandoObject> GetGmtFinFoldOrderList(Int64? pGMT_FIN_FOLD_H_ID, string pMC_ORDER_H_LST)
        {
            string sp = "pkg_gmt_fin.gmt_fin_fold_h_select";
            try
            {
                var obList = new List<ExpandoObject>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_LST", Value =pMC_ORDER_H_LST},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dynamic ob = new ExpandoObject();

                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);


                    var obOrdItmColorList = new List<dynamic>();
                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                         new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value =ob.MC_ORDER_H_ID},
                         new CommandParameter() {ParameterName = "pOption", Value =3002},
                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                     }, sp);
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        dynamic obOrdItmColor = new ExpandoObject();

                        obOrdItmColor.MC_ORDER_SHIP_ID = (dr1["MC_ORDER_SHIP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_ORDER_SHIP_ID"]);
                        obOrdItmColor.MC_STYLE_D_ITEM_ID = (dr1["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_STYLE_D_ITEM_ID"]);
                        obOrdItmColor.MC_COLOR_ID = (dr1["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_COLOR_ID"]);
                        obOrdItmColor.MC_ORDER_COL_ID = (dr1["MC_ORDER_COL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_ORDER_COL_ID"]);
                        obOrdItmColor.HR_COUNTRY_ID = (dr1["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["HR_COUNTRY_ID"]);
                        obOrdItmColor.MC_ORDER_CNTRY_ID = (dr1["MC_ORDER_CNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_ORDER_CNTRY_ID"]);

                        obOrdItmColor.SHIP_DESC = (dr1["SHIP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["SHIP_DESC"]);
                        obOrdItmColor.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        obOrdItmColor.COUNTRY_NAME_EN = (dr1["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["COUNTRY_NAME_EN"]);
                        obOrdItmColor.COLOR_NAME_EN = (dr1["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["COLOR_NAME_EN"]);

                        var obOrdSizeList = new List<dynamic>();
                        var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                             new CommandParameter() {ParameterName = "pMC_ORDER_COL_ID", Value =obOrdItmColor.MC_ORDER_COL_ID},
                             new CommandParameter() {ParameterName = "pGMT_FIN_FOLD_H_ID", Value =pGMT_FIN_FOLD_H_ID},
                             new CommandParameter() {ParameterName = "pOption", Value =3003},
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, sp);
                        foreach (DataRow dr2 in ds2.Tables[0].Rows)
                        {
                            dynamic obOrdSize = new ExpandoObject();

                            obOrdSize.MC_ORDER_SIZE_ID = (dr2["MC_ORDER_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["MC_ORDER_SIZE_ID"]);
                            obOrdSize.MC_SIZE_ID = (dr2["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["MC_SIZE_ID"]);
                            obOrdSize.GMT_FIN_FOLD_D_ID = (dr2["GMT_FIN_FOLD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["GMT_FIN_FOLD_D_ID"]);
                            obOrdSize.MC_COLOR_ID = (dr2["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["MC_COLOR_ID"]);
                            obOrdSize.HR_COUNTRY_ID = (dr2["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["HR_COUNTRY_ID"]);
                            obOrdSize.GMT_FIN_FOLD_H_ID = (dr2["GMT_FIN_FOLD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["GMT_FIN_FOLD_H_ID"]);
                            obOrdSize.MC_STYLE_D_ITEM_ID = (dr2["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["MC_STYLE_D_ITEM_ID"]);
                            obOrdSize.MC_ORDER_SHIP_ID = (dr2["MC_ORDER_SHIP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["MC_ORDER_SHIP_ID"]);
                            obOrdSize.FOLD_QTY = (dr2["FOLD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["FOLD_QTY"]);
                            obOrdSize.SIZE_CODE = (dr2["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["SIZE_CODE"]);

                            obOrdSizeList.Add(obOrdSize);
                        }
                        obOrdItmColor.itemsOrdSize = obOrdSizeList;
                        obOrdItmColorList.Add(obOrdItmColor);
                    }

                    ob.itemsOrdItmColor = obOrdItmColorList;
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GMT_FIN_FOLD_HModel> GetGmtFinFoldData(DateTime pCALENDAR_DT, string pHR_PROD_FLR_LST)
        {
            string sp = "pkg_gmt_fin.gmt_fin_fold_h_select";
            try
            {
                var obList = new List<GMT_FIN_FOLD_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCALENDAR_DT", Value =pCALENDAR_DT},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_LST", Value =pHR_PROD_FLR_LST},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_FIN_FOLD_HModel ob = new GMT_FIN_FOLD_HModel();

                    ob.GMT_FIN_FOLD_H_ID = (dr["GMT_FIN_FOLD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_FIN_FOLD_H_ID"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                    ob.HR_NO = (dr["HR_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_NO"]);
                    ob.MC_ORDER_H_LST = (dr["MC_ORDER_H_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_LST"]);

                    ob.FLOOR_DESC_EN = (dr["FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_EN"]);
                    ob.HR_NAME = (dr["HR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HR_NAME"]);


                    //dynamic obItemsOrder = new { x = 1, y = "abc" };
                    ob.itemsOrder = ob.GetGmtFinFoldOrderList(ob.GMT_FIN_FOLD_H_ID, ob.MC_ORDER_H_LST);

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