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
    public class GMT_CAPACITY_BYR_ALLOCModel
    {
        public Int64? GMT_CAPACITY_BYR_ALLOC_ID { get; set; }
        public Int64? GMT_CAPACITY_WK_TOT_ID { get; set; }
        public Int64? MC_BYR_ACC_GRP_ID { get; set; }
        public Int64? ALLOC_PCS { get; set; }
        public Int64? BOOKED_PCS { get; set; }
        public Int64? FREE_PCS { get; set; }
        
        public Int64? GMT_PROD_PLN_CLNDR_ID { get; set; }
        public Int64? GMT_PRODUCT_TYP_ID { get; set; }
        public string WEEK_NAME { get; set; }
        public string MN_BYR_WISE_ALLOC_XML { get; set; }
        public string WK_BYR_WISE_ALLOC_XML { get; set; }

        public string BatchSave()
        {
            const string sp = "pkg_planning_common.capcty_byr_alloc_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = ob.GMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pMN_BYR_WISE_ALLOC_XML", Value = ob.MN_BYR_WISE_ALLOC_XML},
                     new CommandParameter() {ParameterName = "pWK_BYR_WISE_ALLOC_XML", Value = ob.WK_BYR_WISE_ALLOC_XML},
                                        
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
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<dynamic> GetByrWiseWkCapcty(long pGMT_PROD_PLN_CLNDR_ID, Int64 pMC_BYR_ACC_GRP_ID)
        {
            string sp = "pkg_planning_common.gmt_capacity_byr_alloc_select";
            try
            {
                int vWeekSl = 0;
                var obList = new List<dynamic>();
                var dataList = new List<GMT_CAPACITY_BYR_ALLOCModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value =pGMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_CAPACITY_BYR_ALLOCModel ob = new GMT_CAPACITY_BYR_ALLOCModel();

                    ob.WEEK_NAME = (dr["WEEK_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WEEK_NAME"]);
                    ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                    ob.GMT_PRODUCT_TYP_ID = (dr["GMT_PRODUCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PRODUCT_TYP_ID"]);

                    ob.GMT_CAPACITY_BYR_ALLOC_ID = (dr["GMT_CAPACITY_BYR_ALLOC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CAPACITY_BYR_ALLOC_ID"]);
                    ob.GMT_CAPACITY_WK_TOT_ID = (dr["GMT_CAPACITY_WK_TOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CAPACITY_WK_TOT_ID"]);
                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    ob.ALLOC_PCS = (dr["ALLOC_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALLOC_PCS"]);
                    ob.BOOKED_PCS = (dr["BOOKED_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BOOKED_PCS"]);
                    ob.FREE_PCS = ob.ALLOC_PCS - ob.BOOKED_PCS; //(dr["FREE_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FREE_PCS"]);

                    dataList.Add(ob);
                }

                var weekList = dataList.Distinct(new GmtWkComparer(a => a.WEEK_NAME)).ToList().OrderBy(o => o.GMT_PROD_PLN_CLNDR_ID).ToList();
                foreach (GMT_CAPACITY_BYR_ALLOCModel obWK in weekList)
                {
                    vWeekSl = vWeekSl + 1;

                    obList.Add(new
                    {
                        WEEK_SL=vWeekSl,
                        WEEK_NAME = obWK.WEEK_NAME,
                        WK_BYR_WISE_ALLOC_LIST = dataList.Where(a => a.GMT_PROD_PLN_CLNDR_ID == obWK.GMT_PROD_PLN_CLNDR_ID).ToList().OrderBy(o => o.GMT_PRODUCT_TYP_ID).ToList()
                    });
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<dynamic> GetMnCapctyFree4ByrAlloc(long pGMT_PROD_PLN_CLNDR_ID)
        {
            string sp = "pkg_planning_common.gmt_capacity_byr_alloc_select";
            try
            {
                var obList = new List<dynamic>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value =pGMT_PROD_PLN_CLNDR_ID}, 
                    new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obList.Add(new
                    {
                        GMT_PRODUCT_TYP_NAME = (dr["GMT_PRODUCT_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_PRODUCT_TYP_NAME"]),
                        MN_FREE_CAP_PCS = (dr["MN_FREE_CAP_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MN_FREE_CAP_PCS"]),
                        MN_FREE_CAP_PCS4BYR_ALLOC = (dr["MN_FREE_CAP_PCS4BYR_ALLOC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MN_FREE_CAP_PCS4BYR_ALLOC"])
                    });
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<dynamic> GetByrWiseMnCapcty(long pGMT_PROD_PLN_CLNDR_ID)
        {
            string sp = "pkg_planning_common.gmt_capacity_byr_alloc_select";
            try
            {
                var obList = new List<dynamic>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value =pGMT_PROD_PLN_CLNDR_ID}, 
                    new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obList.Add(new
                    {
                        MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]),
                        BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]),
                        
                        IS_BASIC_EXISTS = (dr["IS_BASIC_EXISTS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IS_BASIC_EXISTS"]),
                        BASIC_ALLOC_PCS = (dr["BASIC_ALLOC_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BASIC_ALLOC_PCS"]),
                        BASIC_BOOKED_PCS = (dr["BASIC_BOOKED_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BASIC_BOOKED_PCS"]),
                        BASIC_FREE_PCS = (dr["BASIC_FREE_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BASIC_FREE_PCS"]),
                        
                        IS_FANCY_EXISTS = (dr["IS_FANCY_EXISTS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IS_FANCY_EXISTS"]),
                        FANCY_ALLOC_PCS = (dr["FANCY_ALLOC_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FANCY_ALLOC_PCS"]),
                        FANCY_BOOKED_PCS = (dr["FANCY_BOOKED_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FANCY_BOOKED_PCS"]),
                        FANCY_FREE_PCS = (dr["FANCY_FREE_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FANCY_FREE_PCS"]),
                        
                        IS_POLO_EXISTS = (dr["IS_POLO_EXISTS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IS_POLO_EXISTS"]),
                        POLO_ALLOC_PCS = (dr["POLO_ALLOC_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["POLO_ALLOC_PCS"]),
                        POLO_BOOKED_PCS = (dr["POLO_BOOKED_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["POLO_BOOKED_PCS"]),
                        POLO_FREE_PCS = (dr["POLO_FREE_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["POLO_FREE_PCS"]),
                        WEEK_LIST = new List<dynamic>()
                    });
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }





    class GmtWkComparer : IEqualityComparer<GMT_CAPACITY_BYR_ALLOCModel>
    {
        private Func<GMT_CAPACITY_BYR_ALLOCModel, object> _funcDistinct;
        public GmtWkComparer(Func<GMT_CAPACITY_BYR_ALLOCModel, object> funcDistinct)
        {
            this._funcDistinct = funcDistinct;
        }

        public bool Equals(GMT_CAPACITY_BYR_ALLOCModel x, GMT_CAPACITY_BYR_ALLOCModel y)
        {
            return _funcDistinct(x).Equals(_funcDistinct(y));
        }

        public int GetHashCode(GMT_CAPACITY_BYR_ALLOCModel obj)
        {
            return this._funcDistinct(obj).GetHashCode();
        }
    }
}