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

    public class MKT_FAB_RATE_TMPLTModel
    {
        public Int64 MKT_FAB_RATE_TMPLT_ID { get; set; }
        public Int64 MC_BYR_ACC_GRP_ID { get; set; }
        public Int64 LK_RATE_DATA_ID { get; set; }
        public Decimal RATE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string XML { get; set; }


        public dynamic getMktRateChartData()
        {
            dynamic ob = new ExpandoObject();
            ob.ByrAccs = new List<dynamic>();
            ob.datas = new List<dynamic>();

            string sp = "pkg_inquiry.mkt_fab_rate_tmplt_select";
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
                    ob.ByrAccs.Add(new
                    {
                        MC_BYR_ACC_GRP_ID = (dr2["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr2["MC_BYR_ACC_GRP_ID"]),
                        BYR_ACC_GRP_NAME_EN = (dr2["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["BYR_ACC_GRP_NAME_EN"])
                    });
                }

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    IDictionary<string, object> obObj = new ExpandoObject();
                    obObj["LK_RATE_DATA_ID"] = (dr["LK_RATE_DATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LK_RATE_DATA_ID"]);
                    obObj["LK_DATA_NAME_EN"] = (dr["LK_DATA_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DATA_NAME_EN"]);
                    obObj["LK_TABLE_NAME_EN"] = (dr["LK_TABLE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_TABLE_NAME_EN"]);
                    obObj["ROW_SPAN"] = (dr["ROW_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ROW_SPAN"]);
                    obObj["ROW_NUMBER"] = (dr["ROW_NUMBER"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ROW_NUMBER"]);

                    foreach (dynamic ord in ob.ByrAccs)
                    {

                        obObj[ord.MC_BYR_ACC_GRP_ID.ToString()] = new ExpandoObject();
                        var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                            new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ord.MC_BYR_ACC_GRP_ID},
                                            new CommandParameter() {ParameterName = "pLK_RATE_DATA_ID", Value =   obObj["LK_RATE_DATA_ID"] },
                                            new CommandParameter() {ParameterName = "pOption", Value = 3002},
                                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                    }, sp);

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in ds1.Tables[0].Rows)
                            {
                                obObj[ord.MC_BYR_ACC_GRP_ID.ToString()] = new
                                {
                                    MKT_FAB_RATE_TMPLT_ID = (dr1["MKT_FAB_RATE_TMPLT_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr1["MKT_FAB_RATE_TMPLT_ID"]),
                                    MC_BYR_ACC_GRP_ID = ord.MC_BYR_ACC_GRP_ID,
                                    RATE = (dr1["RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["RATE"])
                                };
                            }
                        }
                        else
                        {
                            obObj[ord.MC_BYR_ACC_GRP_ID.ToString()] = new
                            {
                                RATE = "",
                                MKT_FAB_RATE_TMPLT_ID =-1,
                                MC_BYR_ACC_GRP_ID = ord.MC_BYR_ACC_GRP_ID,
                            };
                        }
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


        public string Save()
         // pOption=1000=>General Form Save
        {
            const string sp = "pkg_inquiry.mkt_fab_rate_tmplt_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
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
    }
}