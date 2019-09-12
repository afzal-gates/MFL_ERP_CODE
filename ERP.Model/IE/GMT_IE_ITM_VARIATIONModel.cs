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

    public class GMT_IE_ITM_VR_HEADModel
    {
        public int INV_ITEM_CAT_ID { get; set; }
        public string ITEM_CAT_NAME_EN { get; set; }
        public List<GMT_IE_ITM_VARIATIONModel> items { get; set; }
    }
    public class GMT_IE_ITM_VARIATIONModel
    {
        public Int64 GMT_IE_ITM_VARIATION_ID { get; set; }
        public Int64 LK_ITEM_GRP_ID { get; set; }
        public Int64 INV_ITEM_CAT_ID { get; set; }
        public string TECH_SPEC_DESC { get; set; }
        public Int64 REQ_OP { get; set; }
        public Int64 REQ_HLP { get; set; }
        public Decimal SMV { get; set; }
        public Int64 PICK_PROD_PER_HR { get; set; }
        public Int64 DAYS_TO_PICK_PROD { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string XML { get; set; }

        public string Save()
         // pOption=1000=>General Form Save
        {
            const string sp = "PKG_GMT_IE.gmt_ie_itm_variation_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public List<GMT_IE_ITM_VR_HEADModel> getItmVariationData4Costing(int pLK_ITEM_GRP_ID, Int32 pORD_VOL, string pSEARCH_TXT)
        {
            string sp = "PKG_GMT_IE.gmt_ie_itm_variation_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_IE_ITM_VR_HEADModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pLK_ITEM_GRP_ID", Value = pLK_ITEM_GRP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_IE_ITM_VR_HEADModel ob = new GMT_IE_ITM_VR_HEADModel();
                
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.items = new List<GMT_IE_ITM_VARIATIONModel>();
                    ob.items = this.Query(ob.INV_ITEM_CAT_ID, pORD_VOL, pSEARCH_TXT);
                    if (ob.items.Count > 0)
                    {
                        obList.Add(ob);
                    }
                    
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private List<GMT_IE_ITM_VARIATIONModel> Query(int pINV_ITEM_CAT_ID, Int32 pORD_VOL, string pSEARCH_TXT)
        {
            string sp = "PKG_GMT_IE.gmt_ie_itm_variation_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_IE_ITM_VARIATIONModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = pINV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pORD_VOL", Value = pORD_VOL},
                     new CommandParameter() {ParameterName = "pSEARCH_TXT", Value = pSEARCH_TXT},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_IE_ITM_VARIATIONModel ob = new GMT_IE_ITM_VARIATIONModel();

                    ob.GMT_IE_ITM_CM_ID = (dr["GMT_IE_ITM_CM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_IE_ITM_CM_ID"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.TECH_SPEC_DESC = (dr["TECH_SPEC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TECH_SPEC_DESC"]);
                    ob.REQ_OP = (dr["REQ_OP"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_OP"]);
                    ob.REQ_HLP = (dr["REQ_HLP"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_HLP"]);
                    ob.SMV = (dr["SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SMV"]);
                    ob.CM_PER_DZ = (dr["CM_PER_DZ"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CM_PER_DZ"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public dynamic FindGmtTechSpecVariationData(long pINV_ITEM_CAT_ID)
        {
            dynamic ob = new ExpandoObject();
            ob.OrdVols = new List<dynamic>();
            ob.datas = new List<dynamic>();

            string sp = "PKG_GMT_IE.gmt_ie_itm_variation_select";
            //pOption=3000=>Select All Data
            try
            {
                OraDatabase db = new OraDatabase();
                var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr2 in ds2.Tables[0].Rows)
                {
                    ob.OrdVols.Add(new
                    {
                        GMT_IE_ITM_OQVOL_ID = (dr2["GMT_IE_ITM_OQVOL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["GMT_IE_ITM_OQVOL_ID"]),
                        ORD_VOL_1 = (dr2["ORD_VOL_1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["ORD_VOL_1"]),
                        ORD_VOL_2 = (dr2["ORD_VOL_2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["ORD_VOL_2"])
                    });
                }

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = pINV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);


                if (ds.Tables[0].Rows.Count > 0)
                {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            IDictionary<string, object> obObj = new ExpandoObject();
                            obObj["GMT_IE_ITM_VARIATION_ID"] = (dr["GMT_IE_ITM_VARIATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_IE_ITM_VARIATION_ID"]);
                            obObj["INV_ITEM_CAT_ID"] = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                            obObj["LK_ITEM_GRP_ID"] = (dr["LK_ITEM_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_GRP_ID"]);
                            obObj["TECH_SPEC_DESC"] = (dr["TECH_SPEC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TECH_SPEC_DESC"]);
                            obObj["REQ_OP"] = (dr["REQ_OP"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_OP"]);
                            obObj["REQ_HLP"] = (dr["REQ_HLP"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_HLP"]);
                            obObj["SMV"] = (dr["SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SMV"]);
                            obObj["PICK_PROD_PER_HR"] = (dr["PICK_PROD_PER_HR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PICK_PROD_PER_HR"]);
                            obObj["DAYS_TO_PICK_PROD"] = (dr["DAYS_TO_PICK_PROD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAYS_TO_PICK_PROD"]);
                    
                            foreach (dynamic ord in ob.OrdVols)
                            {

                                obObj[ord.GMT_IE_ITM_OQVOL_ID.ToString()] = new ExpandoObject();
                                var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                            new CommandParameter() {ParameterName = "pGMT_IE_ITM_OQVOL_ID", Value = ord.GMT_IE_ITM_OQVOL_ID},
                                            new CommandParameter() {ParameterName = "pGMT_IE_ITM_VARIATION_ID", Value =   obObj["GMT_IE_ITM_VARIATION_ID"] },
                                            new CommandParameter() {ParameterName = "pOption", Value = 3002},
                                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                    }, sp);

                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                                    {

                                        obObj[ord.GMT_IE_ITM_OQVOL_ID.ToString()] = new
                                        {
                                            GMT_IE_ITM_CM_ID = (dr1["GMT_IE_ITM_CM_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr1["GMT_IE_ITM_CM_ID"]),
                                            CM_PER_DZ = (dr1["CM_PER_DZ"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["CM_PER_DZ"]),
                                            GMT_IE_ITM_OQVOL_ID = ord.GMT_IE_ITM_OQVOL_ID,
                                            CPM = (dr1["CPM"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["CPM"])
                                        };
                                    }
                                }
                                else
                                {

                                    obObj[ord.GMT_IE_ITM_OQVOL_ID.ToString()] = new
                                    {
                                        GMT_IE_ITM_CM_ID = 0,
                                        CM_PER_DZ = 0,
                                        GMT_IE_ITM_OQVOL_ID = ord.GMT_IE_ITM_OQVOL_ID,
                                        CPM = 0
                                    };
                                }

                            }
                            ob.datas.Add(obObj);
                        }
                }
                else
                {

                    IDictionary<string, object> obObj = new ExpandoObject();
                    obObj["GMT_IE_ITM_VARIATION_ID"] = -1;
                    obObj["INV_ITEM_CAT_ID"] = pINV_ITEM_CAT_ID;
                    obObj["TECH_SPEC_DESC"] = "";
                    obObj["REQ_OP"] = 0;
                    obObj["REQ_HLP"] = 0;
                    obObj["SMV"] = 0;
                    obObj["PICK_PROD_PER_HR"] = 0;
                    obObj["DAYS_TO_PICK_PROD"] = 0;

                    foreach (dynamic ord in ob.OrdVols)
                    {
                        obObj[ord.GMT_IE_ITM_OQVOL_ID.ToString()] = new ExpandoObject();
                        obObj[ord.GMT_IE_ITM_OQVOL_ID.ToString()] = new
                        {
                            CM_PER_DZ = 0,
                            GMT_IE_ITM_OQVOL_ID = ord.GMT_IE_ITM_OQVOL_ID,
                            CPM = 0
                        };
                    }
                    ob.datas.Add(obObj);
                }

                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ITEM_CAT_NAME_EN { get; set; }

        public long GMT_IE_ITM_CM_ID { get; set; }

        public decimal CM_PER_DZ { get; set; }
    }
}