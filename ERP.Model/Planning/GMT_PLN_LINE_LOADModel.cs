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
    public class GMT_PLN_LINE_LOAD_OTHERSModel
    {
        public Int64 GMT_PLN_LINE_LOAD_ID { get; set; }
        public Int32 HR_PROD_LINE_ID { get; set; }
        public string LINE_CODE { get; set; }
        public DateTime SEW_START_DT { get; set; }
        public DateTime SEW_END_DT { get; set; }
        public Int64 ALLOCATED_QTY { get; set; }
        public Int64 INITIAL_PROD_QTY { get; set; }
        public int GMT_PLN_STRIPE_PHASE_ID { get; set; }
        public string PLN_STRIPE_PHASE_NAME { get; set; }

        public List<GMT_PLN_LINE_LOAD_OTHERSModel> Query(Int64 pMC_ORDER_ITEM_PLN_ID, Int64 pHR_PROD_LINE_ID, Int64 pGMT_PLN_LINE_LOAD_ID)
        {
            string sp = "PKG_GMT_PLN_LINE_LOAD.gmt_pln_line_ld_mrg_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_PLN_LINE_LOAD_OTHERSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_ITEM_PLN_ID", Value = pMC_ORDER_ITEM_PLN_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value = pHR_PROD_LINE_ID},
                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = pGMT_PLN_LINE_LOAD_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PLN_LINE_LOAD_OTHERSModel ob = new GMT_PLN_LINE_LOAD_OTHERSModel();

                    ob.GMT_PLN_LINE_LOAD_ID = (dr["GMT_PLN_LINE_LOAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_LINE_LOAD_ID"]);
                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["HR_PROD_LINE_ID"]);
                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);
                    ob.SEW_START_DT = (dr["SEW_START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SEW_START_DT"]);
                    ob.SEW_END_DT = (dr["SEW_END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SEW_END_DT"]);
                    ob.ALLOCATED_QTY = (dr["ALLOCATED_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALLOCATED_QTY"]);
                    ob.INITIAL_PROD_QTY = (dr["INITIAL_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INITIAL_PROD_QTY"]);
                    ob.GMT_PLN_STRIPE_PHASE_ID = (dr["GMT_PLN_STRIPE_PHASE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["GMT_PLN_STRIPE_PHASE_ID"]);
                    ob.PLN_STRIPE_PHASE_NAME = (dr["GMT_PLN_STRIPE_PHASE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLN_STRIPE_PHASE_NAME"]);
                    ob.IS_SAME_LINE = (dr["IS_SAME_LINE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SAME_LINE"]);
                    ob.TOT_PROD = (dr["TOT_PROD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_PROD"]);
                    ob.DAYS_DE = (dr["DAYS_DE"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["DAYS_DE"]);
                    ob.SHIP_DE = (dr["SHIP_DE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIP_DE"]);
                    ob.sts = new List<GMT_PLN_LINE_LOAD_STSModel>();
                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                         new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = ob.GMT_PLN_LINE_LOAD_ID },
                                         new CommandParameter() {ParameterName = "pOption", Value = 3017},
                                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                  }, "PKG_GMT_PLN_LINE_LOAD.gmt_pln_line_load_select");
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        GMT_PLN_LINE_LOAD_STSModel ob1 = new GMT_PLN_LINE_LOAD_STSModel();
                        ob1.GMT_PLN_LINE_LD_STS_ID = (dr1["GMT_PLN_LINE_LD_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["GMT_PLN_LINE_LD_STS_ID"]);
                        ob1.LK_PLN_STS_GRP_ID = (dr1["LK_PLN_STS_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["LK_PLN_STS_GRP_ID"]);
                        ob1.PLN_STS_AREA_NAME = (dr1["PLN_STS_AREA_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PLN_STS_AREA_NAME"]);
                        ob1.PLN_STS_CODE = (dr1["PLN_STS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PLN_STS_CODE"]);
                        ob1.STS_COLOR_CODE = (dr1["STS_COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["STS_COLOR_CODE"]);
                        ob1.PLN_STS_NAME = (dr1["PLN_STS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PLN_STS_NAME"]);
                        ob.sts.Add(ob1);
                    }

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string IS_SAME_LINE { get; set; }

        public long TOT_PROD { get; set; }

        public int DAYS_DE { get; set; }

        public string SHIP_DE { get; set; }

        public List<GMT_PLN_LINE_LOAD_STSModel> sts { get; set; }
    }
    public class GMT_PLN_LINE_LD_MRGModel
    {
        public Int64 GMT_PLN_LINE_LD_MRG_ID { get; set; }
        public Int64 MRG_PROD_LINE_ID { get; set; }

        public List<GMT_PLN_LINE_LD_MRGModel> Query(Int64 pGMT_PLN_LINE_LOAD_ID)
        {
            string sp = "PKG_GMT_PLN_LINE_LOAD.gmt_pln_line_ld_mrg_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_PLN_LINE_LD_MRGModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = pGMT_PLN_LINE_LOAD_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PLN_LINE_LD_MRGModel ob = new GMT_PLN_LINE_LD_MRGModel();
                    ob.GMT_PLN_LINE_LD_MRG_ID = (dr["GMT_PLN_LINE_LD_MRG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_LINE_LD_MRG_ID"]);
                    ob.MRG_PROD_LINE_ID = (dr["MRG_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MRG_PROD_LINE_ID"]);
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

    public class GMT_PLN_LINE_LOAD_STSModel
	{
        public Int64 GMT_PLN_LINE_LD_STS_ID { get; set; }
        public string PLN_STS_AREA_NAME { get; set; }
        public string PLN_STS_CODE {get;set;}
        public string PLN_STS_NAME {get;set;}
        public string STS_COLOR_CODE {get;set;}
        public long LK_PLN_STS_GRP_ID { get; set; }

        public string PLN_STS_P_CODE { get; set; }
    }
    public class LINE_LOAD_RESOURCEModel
    {

        public Int64 id { get; set; }
        public string name { get; set; }

        public int HR_PROD_FLR_ID { get; set; }
        public int? HR_PROD_LINE_ID { get; set; }

        public string LINE_LBL_COLOR { get; set; }

        public string LINE_CRITICALITY { get; set; }

        public string BEST_FIT_ITEM { get; set; }

        public string IS_ACTV_POINT { get; set; }
        public int TTL_ACTV_POINT { get; set; }
        public bool expanded { get; set; }



        private List<LINE_LOAD_RESOURCEModel> _children = null;
        public List<LINE_LOAD_RESOURCEModel> children
        {
            get
            {
                if (_children == null)
                {
                    _children = new List<LINE_LOAD_RESOURCEModel>();
                }
                return _children;
            }
        }


        private List<dynamic> _holidays = null;
        public List<dynamic> holidays
        {
            get
            {
                if (_holidays == null)
                {
                    _holidays = new List<dynamic>();
                }
                return _holidays;
            }
            set
            {
                _holidays = value;
            }
        }

        public List<dynamic> merged_days { get; set; }
        public List<dynamic> mc_utis { get; set; }


        public List<LINE_LOAD_RESOURCEModel> getResourceData(Int64? pHR_COMPANY_ID, Int64? pHR_OFFICE_ID, Int64? pHR_PROD_FLR_ID, Int64? pHR_PROD_LINE_ID, DateTime pSTART_DT, DateTime pEND_DT, String pRESOURCES, Int64? pMC_ORDER_SHIP_ID)
        {
            string sp = "pkg_gmt_pln_line_load.gmt_pln_line_load_select";
            try
            {
                var obList = new List<LINE_LOAD_RESOURCEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = pHR_OFFICE_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = pHR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pRESOURCES", Value = pRESOURCES},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    LINE_LOAD_RESOURCEModel ob = new LINE_LOAD_RESOURCEModel();
                    List<dynamic> holidays = new List<dynamic>();
                    

                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt16(dr["HR_PROD_FLR_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt16(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt16(dr["HR_OFFICE_ID"]);
                    ob.id = ob.HR_PROD_FLR_ID+10000;
                    ob.name = (dr["NAME_"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["NAME_"]);
                    ob.expanded = true;
                    ob.merged_days = new List<dynamic>();

                    ob.mc_utis = new List<dynamic>();



                    var ds6= db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                         new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = ob.HR_PROD_FLR_ID },
                                         new CommandParameter() {ParameterName = "pSTART_DT", Value = pSTART_DT },
                                         new CommandParameter() {ParameterName = "pEND_DT", Value = pEND_DT },
                                         new CommandParameter() {ParameterName = "pOption", Value =3019},
                                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                     }, sp);
                    foreach (DataRow dr6 in ds6.Tables[0].Rows)
                    {
                        ob.mc_utis.Add(new
                        {
                            start = (dr6["START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr6["START_DT"]),
                            end = (dr6["END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr6["END_DT"]),
                            PLAN_OP = (dr6["PLAN_OP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr6["PLAN_OP"]),
                            TTL_ACTV_POINT = (dr6["TTL_ACTV_POINT"] == DBNull.Value) ? 0 : Convert.ToInt32(dr6["TTL_ACTV_POINT"])
                        });

                    }

                    

                    var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                         new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID },
                                         new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID },
                                         new CommandParameter() {ParameterName = "pSTART_DT", Value = pSTART_DT },
                                         new CommandParameter() {ParameterName = "pEND_DT", Value = pEND_DT },
                                         new CommandParameter() {ParameterName = "pOption", Value =3007},
                                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                     }, sp);
                    foreach (DataRow dr2 in ds2.Tables[0].Rows)
                    {
                        holidays.Add(new
                        {
                            start = (dr2["START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr2["START_DT"]),
                            end = (dr2["END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr2["END_DT"]),
                            backColor = (dr2["COLOR_CODE"] == DBNull.Value) ? "N" : Convert.ToString(dr2["COLOR_CODE"]),
                            DAY_TYPE = (dr2["DAY_TYPE"] == DBNull.Value) ? "NA" : Convert.ToString(dr2["DAY_TYPE"])
                        });

                    }




                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                         new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value =ob.HR_PROD_FLR_ID},
                                         new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                                         new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value = pHR_PROD_LINE_ID},
                                         new CommandParameter() {ParameterName = "pRESOURCES", Value = pRESOURCES},
                                         new CommandParameter() {ParameterName = "pMC_ORDER_SHIP_ID", Value = pMC_ORDER_SHIP_ID},
                                         new CommandParameter() {ParameterName = "pOption", Value =3002},
                                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                     }, sp);
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        LINE_LOAD_RESOURCEModel ob1 = new LINE_LOAD_RESOURCEModel();
                        ob1.id = (dr1["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["HR_PROD_LINE_ID"]);
                        ob1.HR_PROD_FLR_ID = ob.HR_PROD_FLR_ID;
                        ob1.HR_PROD_LINE_ID = (dr1["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt16(dr1["HR_PROD_LINE_ID"]);
                        ob1.name = (dr1["NAME_"] == DBNull.Value) ? String.Empty : Convert.ToString(dr1["NAME_"]);
                        ob1.LINE_LBL_COLOR = (dr1["LINE_LBL_COLOR"] == DBNull.Value) ? String.Empty : Convert.ToString(dr1["LINE_LBL_COLOR"]);
                        ob1.LINE_CRITICALITY = (dr1["LINE_CRITICALITY"] == DBNull.Value) ? String.Empty : Convert.ToString(dr1["LINE_CRITICALITY"]);
                        ob1.BEST_FIT_ITEM = (dr1["BEST_FIT_ITEM"] == DBNull.Value) ? String.Empty : Convert.ToString(dr1["BEST_FIT_ITEM"]);
                        ob1.IS_ACTV_POINT = (dr1["IS_ACTV_POINT"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_ACTV_POINT"]);
                        ob1.TTL_ACTV_POINT = (dr1["TTL_ACTV_POINT"] == DBNull.Value) ? 0 : Convert.ToInt16(dr1["TTL_ACTV_POINT"]);

                        ob1.merged_days = new List<dynamic>();

                        var ds3 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                         new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value = ob1.HR_PROD_LINE_ID },
                                         new CommandParameter() {ParameterName = "pSTART_DT", Value = pSTART_DT },
                                         new CommandParameter() {ParameterName = "pEND_DT", Value = pEND_DT },
                                         new CommandParameter() {ParameterName = "pOption", Value =3013},
                                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                     }, sp);
                        foreach (DataRow dr3 in ds3.Tables[0].Rows)
                        {
                            ob1.merged_days.Add(new
                            {
                                start = (dr3["START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr3["START_DT"]),
                                end = (dr3["END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr3["END_DT"]),
                                backColor = (dr3["COLOR_CODE"] == DBNull.Value) ? "N" : Convert.ToString(dr3["COLOR_CODE"]),
                                DAY_TYPE = (dr3["DAY_TYPE"] == DBNull.Value) ? "NA" : Convert.ToString(dr3["DAY_TYPE"])
                            });

                        }
                       

                        ob1.holidays = holidays;
                        ob.children.Add(ob1);
                    }

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LINE_LOAD_RESOURCEModel> getResourceDataDD()
        {
            string sp = "pkg_gmt_pln_line_load.gmt_pln_line_load_select";
            try
            {
                var obList = new List<LINE_LOAD_RESOURCEModel>();
                OraDatabase db = new OraDatabase();
                var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                         new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                                         new CommandParameter() {ParameterName = "pOption", Value =3009},
                                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                     }, sp);
                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {
                    LINE_LOAD_RESOURCEModel ob1 = new LINE_LOAD_RESOURCEModel();
                    ob1.id = (dr1["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["HR_PROD_LINE_ID"]);
                    ob1.HR_PROD_FLR_ID = (dr1["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr1["HR_PROD_FLR_ID"]);
                    ob1.HR_PROD_LINE_ID = (dr1["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt16(dr1["HR_PROD_LINE_ID"]);
                    ob1.name = (dr1["NAME_"] == DBNull.Value) ? String.Empty : Convert.ToString(dr1["NAME_"]);
                    ob1.LINE_LBL_COLOR = (dr1["LINE_LBL_COLOR"] == DBNull.Value) ? String.Empty : Convert.ToString(dr1["LINE_LBL_COLOR"]);
                    ob1.LINE_CRITICALITY = (dr1["LINE_CRITICALITY"] == DBNull.Value) ? String.Empty : Convert.ToString(dr1["LINE_CRITICALITY"]);
                    ob1.BEST_FIT_ITEM = (dr1["BEST_FIT_ITEM"] == DBNull.Value) ? String.Empty : Convert.ToString(dr1["BEST_FIT_ITEM"]);
                    ob1.IS_ACTV_POINT = (dr1["IS_ACTV_POINT"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_ACTV_POINT"]);
                    ob1.TTL_ACTV_POINT = (dr1["TTL_ACTV_POINT"] == DBNull.Value) ? 0 : Convert.ToInt16(dr1["TTL_ACTV_POINT"]);
                    ob1.IS_PERMITTED = (dr1["IS_PERMITTED"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_PERMITTED"]);
                    obList.Add(ob1);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<dynamic> getGmtLnLoadPhaseData()
        {
            string sp = "pkg_gmt_pln_line_load.gmt_pln_line_load_select";
            try
            {
                var obList = new List<dynamic>();
                OraDatabase db = new OraDatabase();
                var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                         new CommandParameter() {ParameterName = "pOption", Value =3010},
                                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                     }, sp);
                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {
                    obList.Add(new {
                        GMT_PLN_STRIPE_PHASE_ID = Convert.ToInt32(dr1["GMT_PLN_STRIPE_PHASE_ID"]),
                        PLN_STRIPE_PHASE_NAME = Convert.ToString(dr1["PLN_STRIPE_PHASE_NAME"])
                    });
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public int HR_COMPANY_ID { get; set; }

        public int HR_OFFICE_ID { get; set; }

        public string IS_PERMITTED { get; set; }
    }


    public class GMT_PLN_LINE_LOADModel
    {
        public Int64 GMT_PLN_LINE_LOAD_ID { get; set; }
        public Int64? PARENT_ID { get; set; }
        public Int64 HR_PROD_LINE_ID { get; set; }
        public DateTime SEW_START_DT { get; set; }
        public DateTime SEW_END_DT { get; set; }
        public Int64 ALLOCATED_QTY { get; set; }
        public Int32 PLAN_MP { get; set; }
        public Int64 PLAN_WH { get; set; }
        public string IS_LRN_CRV_APP { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime LOADED_TIME { get; set; }
        public Decimal SMV { get; set; }
        public Int64 REQ_SAH { get; set; }
        public string BAR_COLOR_CODE { get; set; }
        public string IS_FINALIZED { get; set; }
        public string REMARKS { get; set; }

        public Int64 id { get; set; }
        public string text { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public Int64 resource { get; set; }

        public string barBackColor  { get; set; }
        public string barColor { get; set; }
        public string backColor  { get; set; }
        public string borderColor { get; set; }
        public string fontColor { get; set; }


        public Boolean barHidden { get; set; }
        public Boolean clickDisabled { get; set; }
        public Boolean moveVDisabled { get; set; }
        public Boolean moveHDisabled { get; set; }
        public Boolean deleteDisabled { get; set; }
        public Boolean doubleClickDisabled { get; set; }
        public Boolean moveDisabled { get; set; }
        public Boolean resizeDisabled { get; set; }
        public Boolean rightClickDisabled { get; set; }

        public string ORDER_NO { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public DateTime SHIP_DT { get; set; }
        public long MC_ORDER_H_ID { get; set; }
        public long MC_BYR_ACC_ID { get; set; }
        public long MC_STYLE_D_ITEM_ID { get; set; }
        public long MC_STYLE_H_ID { get; set; }
        public List<GMT_PLN_LINE_LOAD_STSModel> sts { get; set; }
        public List<GMT_PLN_LINE_LD_MRGModel> merges { get; set; }
        public List<GMT_PLN_LINE_LOAD_OTHERSModel> others_allocations { get; set; }

        public string XML { get; set; }
        public Int32 GMT_PLN_STRIPE_PHASE_ID { get; set; }
        public string IS_USED_IN_TRGT { get; set; }
        private List<GMT_PLN_LINE_LOAD_DModel> _details = null;
        public List<GMT_PLN_LINE_LOAD_DModel> details
        {
            get
            {
                if (_details == null)
                {
                    _details = new List<GMT_PLN_LINE_LOAD_DModel>();
                }
                return _details;
            }
            set
            {
                _details = value;
            }
        }


        public string SaveManualLoading()
        {
            const string sp = "pkg_gmt_pln_line_load.SaveManualLoading";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pOption", Value = ob.pOption ?? 1000 },
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public string Delete( Int64 pGMT_PLN_LINE_LOAD_ID, int pOption)
        {
            const string sp ="pkg_name.gmt_pln_line_load_delete";
            var i = 1;
            string jsonStr="{";
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value =pGMT_PLN_LINE_LOAD_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
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

        public List<GMT_PLN_LINE_LOADModel> getEventData(DateTime pSTART_DT, DateTime pEND_DT, String pRESOURCES, Int64? pMC_ORDER_SHIP_ID, string pIS_REGULAR_VIEW, string pIS_GMT_ITEM_VIEW)
        {
            string sp = "pkg_gmt_pln_line_load.gmt_pln_line_load_select";
            try
            {
                var obList = new List<GMT_PLN_LINE_LOADModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSTART_DT", Value =pSTART_DT.Date },
                     new CommandParameter() {ParameterName = "pEND_DT", Value = pEND_DT.Date },
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"] },
                     new CommandParameter() {ParameterName = "pRESOURCES", Value = pRESOURCES },
                     new CommandParameter() {ParameterName = "pMC_ORDER_SHIP_ID", Value = pMC_ORDER_SHIP_ID },
                     new CommandParameter() {ParameterName = "pIS_REGULAR_VIEW", Value = pIS_REGULAR_VIEW },
                     new CommandParameter() {ParameterName = "pIS_GMT_ITEM_VIEW", Value = pIS_GMT_ITEM_VIEW },

                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_PLN_LINE_LOADModel ob = new GMT_PLN_LINE_LOADModel();
                            ob.GMT_PLN_LINE_LOAD_ID = (dr["GMT_PLN_LINE_LOAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_LINE_LOAD_ID"]);
                            ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                            ob.MC_ORDER_ITEM_PLN_ID = (dr["MC_ORDER_ITEM_PLN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_ITEM_PLN_ID"]);
                            ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                            ob.SEW_START_DT = (dr["SEW_START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SEW_START_DT"]);
                            ob.SEW_END_DT = (dr["SEW_END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SEW_END_DT"]);
                            ob.ALLOCATED_QTY = (dr["ALLOCATED_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALLOCATED_QTY"]);
                            ob.PLAN_MP = (dr["PLAN_MP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLAN_MP"]);
                            ob.PLAN_WH = (dr["PLAN_WH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLAN_WH"]);
                            ob.IS_LRN_CRV_APP = (dr["IS_LRN_CRV_APP"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["IS_LRN_CRV_APP"]);
                            ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                            ob.LOADED_TIME = (dr["LOADED_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOADED_TIME"]);
                            ob.SMV = (dr["SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SMV"]);
                            ob.REQ_SAH = (dr["REQ_SAH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_SAH"]);
                            ob.BAR_COLOR_CODE = (dr["BAR_COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BAR_COLOR_CODE"]);
                            ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                            ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                            ob.id = ob.GMT_PLN_LINE_LOAD_ID;
                            ob.resource = ob.HR_PROD_LINE_ID;
                            ob.start = ob.SEW_START_DT;
                            ob.end = ob.SEW_END_DT;
                            ob.text = (dr["TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TEXT"]);
                            ob.barColor = (dr["BAR_COLOR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BAR_COLOR"]);
                            ob.barBackColor = (dr["BAR_BACK_COLOR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BAR_BACK_COLOR"]);
                            ob.backColor = (dr["BACK_COLOR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BACK_COLOR"]);
                            ob.borderColor = (dr["BORDER_COLOR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BORDER_COLOR"]);
                            ob.fontColor = (dr["FONT_COLOR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FONT_COLOR"]);
                            ob.barHidden = (dr["BAR_HIDDEN"] == DBNull.Value) ? false : Convert.ToString(dr["BAR_HIDDEN"])=="Y";


                            ob.clickDisabled = (dr["CLICK_DISABLED"] == DBNull.Value) ? false: Convert.ToString(dr["CLICK_DISABLED"])=="Y";
                            ob.moveVDisabled = (dr["MOVE_V_DISABLED"] == DBNull.Value) ? false: Convert.ToString(dr["MOVE_V_DISABLED"])=="Y";
                            ob.moveHDisabled = (dr["MOVE_H_DISABLED"] == DBNull.Value) ? false : Convert.ToString(dr["MOVE_H_DISABLED"])=="Y";
                            ob.deleteDisabled = (dr["DELETE_DISABLED"] == DBNull.Value) ? false : Convert.ToString(dr["DELETE_DISABLED"])=="Y";
                            ob.doubleClickDisabled = (dr["DOUBLE_CLICK_DISABLED"] == DBNull.Value) ? false : Convert.ToString(dr["DOUBLE_CLICK_DISABLED"])=="Y";
                            ob.moveDisabled = (dr["MOVE_DISABLED"] == DBNull.Value) ? false: Convert.ToString(dr["MOVE_DISABLED"])=="Y";
                            ob.resizeDisabled = (dr["RESIZE_DISABLED"] == DBNull.Value) ? false : Convert.ToString(dr["RESIZE_DISABLED"])=="Y";
                            ob.rightClickDisabled = (dr["RIGHT_CLICK_DISABLED"] == DBNull.Value) ? false : Convert.ToString(dr["RIGHT_CLICK_DISABLED"])=="Y";

                            ob.PLAN_OP = (dr["PLAN_OP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLAN_OP"]);
                            ob.PLAN_HP = (dr["PLAN_HP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLAN_HP"]);


                            ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                            ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                            ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                            ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                            ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);
                            ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);
                  
                            ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                            ob.ORDER_QTY = (dr["ORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDER_QTY"]);
                            ob.INITIAL_PROD_QTY = (dr["INITIAL_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INITIAL_PROD_QTY"]);
                            ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                            ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                            ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                            ob.GMT_PLN_STRIPE_PHASE_ID = (dr["GMT_PLN_STRIPE_PHASE_ID"] == DBNull.Value) ? 1 : Convert.ToInt32(dr["GMT_PLN_STRIPE_PHASE_ID"]);
                            ob.PLN_STRIPE_PHASE_NAME = (dr["PLN_STRIPE_PHASE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLN_STRIPE_PHASE_NAME"]);
                            ob.RGB_COL_CODE = (dr["RGB_COL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE"]);

                  

                            ob.STYL_KEY_IMG = (dr["STYL_KEY_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_KEY_IMG"]);

                            ob.MRG_ACTV_POINT = (dr["MRG_ACTV_POINT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MRG_ACTV_POINT"]);
                            ob.TOT_PROD = (dr["TOT_PROD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_PROD"]);

                            ob.sts = new List<GMT_PLN_LINE_LOAD_STSModel>();
                                 var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                         new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = ob.GMT_PLN_LINE_LOAD_ID },
                                         new CommandParameter() {ParameterName = "pOption", Value = 3014},
                                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                  }, sp);
                                  foreach (DataRow dr1 in ds1.Tables[0].Rows)
                                  {
                                      GMT_PLN_LINE_LOAD_STSModel ob1 = new GMT_PLN_LINE_LOAD_STSModel();
                                      ob1.GMT_PLN_LINE_LD_STS_ID = (dr1["GMT_PLN_LINE_LD_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["GMT_PLN_LINE_LD_STS_ID"]);
                                      ob1.PLN_STS_AREA_NAME = (dr1["PLN_STS_AREA_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PLN_STS_AREA_NAME"]);
                                      ob1.PLN_STS_CODE = (dr1["PLN_STS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PLN_STS_CODE"]);
                                      ob1.PLN_STS_P_CODE = (dr1["PLN_STS_P_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PLN_STS_P_CODE"]);
                                      ob1.STS_COLOR_CODE = (dr1["STS_COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["STS_COLOR_CODE"]);
                                      ob1.PLN_STS_NAME = (dr1["PLN_STS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PLN_STS_NAME"]);
                                      ob.sts.Add(ob1);
                                  }
                  obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<dynamic> getCompanyData()
        {
            string sp = "pkg_gmt_pln_line_load.gmt_pln_line_load_select";
            try
            {
                var obList = new List<dynamic>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"] },
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obList.Add(new {
                        COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]),
                        HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"])
                    });
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<dynamic> getOfficeList(Int32? pHR_COMPANY_ID)
        {
            string sp = "pkg_gmt_pln_line_load.gmt_pln_line_load_select";
            try
            {
                var obList = new List<dynamic>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"] },
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID },
                     new CommandParameter() {ParameterName = "pOption", Value = 3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obList.Add(new
                    {
                        OFFICE_NAME_EN = (dr["OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_EN"]),
                        HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["HR_OFFICE_ID"])
                    });
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<dynamic> getFloorData(Int32? pHR_OFFICE_ID)
        {
            string sp = "pkg_gmt_pln_line_load.gmt_pln_line_load_select";
            try
            {
                var obList = new List<dynamic>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"] },
                     new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = pHR_OFFICE_ID },
                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obList.Add(new
                    {
                        FLOOR_CODE = (dr["FLOOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_CODE"]),
                        HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["HR_PROD_FLR_ID"])
                    });
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<dynamic> getLineData(Int32? pHR_PROD_FLR_ID)
        {
            string sp = "pkg_gmt_pln_line_load.gmt_pln_line_load_select";
            try
            {
                var obList = new List<dynamic>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"] },
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = pHR_PROD_FLR_ID },
                     new CommandParameter() {ParameterName = "pOption", Value = 3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obList.Add(new
                    {
                        LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]),
                        HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["HR_PROD_LINE_ID"])
                    });
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string EventUpdate()
        {
            const string sp = "pkg_gmt_pln_line_load.event_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = ob.GMT_PLN_LINE_LOAD_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value = ob.HR_PROD_LINE_ID},
                     new CommandParameter() {ParameterName = "pSEW_START_DT", Value = ob.SEW_START_DT},
                     new CommandParameter() {ParameterName = "pSEW_END_DT", Value = ob.SEW_END_DT},

                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = ob.pOption ?? 1000},
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
        public List<GMT_PLN_LINE_LOADModel> getLoadedDateByOrderItem(Int64 pMC_ORDER_ITEM_PLN_ID)
        {
            string sp = "pkg_gmt_pln_line_load.gmt_pln_line_load_select";
            try
            {
                var obList = new List<GMT_PLN_LINE_LOADModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_ITEM_PLN_ID", Value = pMC_ORDER_ITEM_PLN_ID },
                     new CommandParameter() {ParameterName = "pOption", Value = 3008},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"] },
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PLN_LINE_LOADModel ob = new GMT_PLN_LINE_LOADModel();
                    ob.GMT_PLN_LINE_LOAD_ID = (dr["GMT_PLN_LINE_LOAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_LINE_LOAD_ID"]);

                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }


                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["HR_PROD_FLR_ID"]);
                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                    ob.SEW_START_DT = (dr["SEW_START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SEW_START_DT"]);
                    ob.SEW_END_DT = (dr["SEW_END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SEW_END_DT"]);
                    ob.ALLOCATED_QTY = (dr["ALLOCATED_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALLOCATED_QTY"]);
                    ob.INITIAL_PROD_QTY = (dr["INITIAL_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INITIAL_PROD_QTY"]);
                    ob.PLAN_MP = (dr["PLAN_MP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLAN_MP"]);
                    ob.PLAN_OP = (dr["PLAN_OP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLAN_OP"]);
                    ob.PLAN_HP = (dr["PLAN_HP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLAN_HP"]);

                    if (dr["PLAN_EFFICIENCY"] != DBNull.Value)
                    {
                        ob.PLAN_EFFICIENCY = Convert.ToDecimal(dr["PLAN_EFFICIENCY"]);
                    }

                    ob.PLAN_WH = (dr["PLAN_WH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLAN_WH"]);
                    ob.IS_LRN_CRV_APP = (dr["IS_LRN_CRV_APP"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_LRN_CRV_APP"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.TTL_ACTV_POINT = (dr["TTL_ACTV_POINT"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TTL_ACTV_POINT"]);
                    ob.IS_ACTV_POINT = (dr["IS_ACTV_POINT"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ACTV_POINT"]);
                    
                    ob.clickDisabled = (dr["CLICK_DISABLED"] == DBNull.Value) ? false : Convert.ToString(dr["CLICK_DISABLED"]) == "Y";
                    ob.moveVDisabled = (dr["MOVE_V_DISABLED"] == DBNull.Value) ? false : Convert.ToString(dr["MOVE_V_DISABLED"]) == "Y";
                    ob.moveHDisabled = (dr["MOVE_H_DISABLED"] == DBNull.Value) ? false : Convert.ToString(dr["MOVE_H_DISABLED"]) == "Y";
                    ob.deleteDisabled = (dr["DELETE_DISABLED"] == DBNull.Value) ? false : Convert.ToString(dr["DELETE_DISABLED"]) == "Y";
                    ob.doubleClickDisabled = (dr["DOUBLE_CLICK_DISABLED"] == DBNull.Value) ? false : Convert.ToString(dr["DOUBLE_CLICK_DISABLED"]) == "Y";
                    ob.moveDisabled = (dr["MOVE_DISABLED"] == DBNull.Value) ? false : Convert.ToString(dr["MOVE_DISABLED"]) == "Y";
                    ob.resizeDisabled = (dr["RESIZE_DISABLED"] == DBNull.Value) ? false : Convert.ToString(dr["RESIZE_DISABLED"]) == "Y";
                    ob.rightClickDisabled = (dr["RIGHT_CLICK_DISABLED"] == DBNull.Value) ? false : Convert.ToString(dr["RIGHT_CLICK_DISABLED"]) == "Y";
                    ob.GMT_PLN_STRIPE_PHASE_ID = (dr["GMT_PLN_STRIPE_PHASE_ID"] == DBNull.Value) ? 1: Convert.ToInt32(dr["GMT_PLN_STRIPE_PHASE_ID"]);
                    ob.ALLOCATION_DESC = (dr["ALLOCATION_DESC"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["ALLOCATION_DESC"]);

                    ob.TOT_PROD = (dr["TOT_PROD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_PROD"]);

                    ob.sts = new List<GMT_PLN_LINE_LOAD_STSModel>();

                    var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                         new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = ob.GMT_PLN_LINE_LOAD_ID },
                                         new CommandParameter() {ParameterName = "pOption", Value = 3017},
                                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                  }, sp);
                    foreach (DataRow dr1 in ds2.Tables[0].Rows)
                    {
                        GMT_PLN_LINE_LOAD_STSModel ob1 = new GMT_PLN_LINE_LOAD_STSModel();
                        ob1.GMT_PLN_LINE_LD_STS_ID = (dr1["GMT_PLN_LINE_LD_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["GMT_PLN_LINE_LD_STS_ID"]);
                        ob1.LK_PLN_STS_GRP_ID = (dr1["LK_PLN_STS_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["LK_PLN_STS_GRP_ID"]);
                        ob1.PLN_STS_AREA_NAME = (dr1["PLN_STS_AREA_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PLN_STS_AREA_NAME"]);
                        ob1.PLN_STS_CODE = (dr1["PLN_STS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PLN_STS_CODE"]);
                        ob1.STS_COLOR_CODE = (dr1["STS_COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["STS_COLOR_CODE"]);
                        ob1.PLN_STS_NAME = (dr1["PLN_STS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PLN_STS_NAME"]);
                        ob.sts.Add(ob1);
                    }

                    ob.HR_PROD_LINE_LST = new List<Int32>() { };
                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                                new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = ob.GMT_PLN_LINE_LOAD_ID },
                                new CommandParameter() {ParameterName = "pOption", Value = 3012},
                                new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                            }, sp);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in ds1.Tables[0].Rows)
                        {
                            ob.HR_PROD_LINE_LST.Add(Convert.ToInt32(dr1["MRG_PROD_LINE_ID"]));
                        }
                    }
                    else
                    {
                        ob.HR_PROD_LINE_LST.Add(-1);
                    }
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public GMT_PLN_LINE_LOADModel getForcastLineLoadData(
            Int64 pHR_PROD_LINE_ID,
            Int64 pALLOCATED_QTY,
            DateTime? pSEW_START_DT,
            DateTime? pSEW_END_DT,
            Int64 pPLAN_MP,
            Int64 pPLAN_WH,
            string pIS_LRN_CRV_APP,
            decimal? pPLAN_EFFICIENCY,
            Int64 pMC_ORDER_ITEM_PLN_ID,
            Int64 pGMT_PRODUCT_TYP_ID,
            Int64 pRF_FAB_CLASS_ID,
            decimal pSMV,
            Int64? pGMT_PLN_LINE_LOAD_ID
        )
        {

            string sp = "";

            if (pSEW_START_DT == null && pSEW_END_DT !=null)
            {
                 sp = "pkg_gmt_pln_line_load.sewing_start_date_fresh_mode";
            } else if (pSEW_START_DT != null && pSEW_END_DT == null)
            {
                 sp = "pkg_gmt_pln_line_load.sewing_end_date_fresh_mode";
            }

            try
            {
                GMT_PLN_LINE_LOADModel ob = new GMT_PLN_LINE_LOADModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value = pHR_PROD_LINE_ID },
                     new CommandParameter() {ParameterName = "pALLOCATED_QTY", Value = pALLOCATED_QTY },
                     new CommandParameter() {ParameterName = "pSEW_START_DT", Value = pSEW_START_DT },
                     new CommandParameter() {ParameterName = "pSEW_END_DT", Value = pSEW_END_DT },

                     new CommandParameter() {ParameterName = "pPLAN_MP", Value = pPLAN_MP },
                     new CommandParameter() {ParameterName = "pPLAN_WH", Value = pPLAN_WH },
                     new CommandParameter() {ParameterName = "pIS_LRN_CRV_APP", Value = pIS_LRN_CRV_APP },
                     new CommandParameter() {ParameterName = "pPLAN_EFFICIENCY", Value = pPLAN_EFFICIENCY },
                     new CommandParameter() {ParameterName = "pMC_ORDER_ITEM_PLN_ID", Value = pMC_ORDER_ITEM_PLN_ID },
                     new CommandParameter() {ParameterName = "pTO_PROD_TYP_ID", Value = pGMT_PRODUCT_TYP_ID },

                     new CommandParameter() {ParameterName = "pRF_FAB_CLASS_ID", Value = pRF_FAB_CLASS_ID },

                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = pGMT_PLN_LINE_LOAD_ID },
                     
                     new CommandParameter() {ParameterName = "pSMV", Value = pSMV },
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SEW_START_DT = (dr["SEW_START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SEW_START_DT"]);
                    ob.SEW_END_DT = (dr["SEW_END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SEW_END_DT"]);
                    ob.ALLOCATED_QTY = (dr["ALLOCATED_QTY"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ALLOCATED_QTY"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_PLN_LINE_LOADModel getEstimatedAllocatedQty(
                Int64 pHR_PROD_LINE_ID,
                DateTime pSEW_START_DT,
                DateTime pSEW_END_DT,
                Int64 pPLAN_MP,
                Int64 pPLAN_WH,
                string pIS_LRN_CRV_APP,
                decimal? pPLAN_EFFICIENCY,
                Int64 pMC_ORDER_ITEM_PLN_ID,
                Int64 pGMT_PRODUCT_TYP_ID,
                Int64 pRF_FAB_CLASS_ID,
                decimal pSMV,
                Int64? pGMT_PLN_LINE_LOAD_ID
            )
        {
            string sp = "pkg_gmt_pln_line_load.find_auto_allocation_qty";
            try
            {
               
                OraDatabase db = new OraDatabase();
                GMT_PLN_LINE_LOADModel ob = new GMT_PLN_LINE_LOADModel();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value = pHR_PROD_LINE_ID },
                     new CommandParameter() {ParameterName = "pSEW_START_DT", Value = pSEW_START_DT },
                     new CommandParameter() {ParameterName = "pSEW_END_DT", Value = pSEW_END_DT },
                     new CommandParameter() {ParameterName = "pPLAN_MP", Value = pPLAN_MP },
                     new CommandParameter() {ParameterName = "pPLAN_WH", Value = pPLAN_WH },
                     new CommandParameter() {ParameterName = "pIS_LRN_CRV_APP", Value = pIS_LRN_CRV_APP },
                     new CommandParameter() {ParameterName = "pPLAN_EFFICIENCY", Value = pPLAN_EFFICIENCY },
                     new CommandParameter() {ParameterName = "pMC_ORDER_ITEM_PLN_ID", Value = pMC_ORDER_ITEM_PLN_ID },
                     new CommandParameter() {ParameterName = "pTO_PROD_TYP_ID", Value = pGMT_PRODUCT_TYP_ID },
                     new CommandParameter() {ParameterName = "pRF_FAB_CLASS_ID", Value = pRF_FAB_CLASS_ID },
                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = pGMT_PLN_LINE_LOAD_ID },
                     new CommandParameter() {ParameterName = "pSMV", Value = pSMV },
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);



                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SEW_START_DT = (dr["SEW_START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SEW_START_DT"]);
                    ob.SEW_END_DT = (dr["SEW_END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SEW_END_DT"]);
                    ob.ALLOCATED_QTY = (dr["ALLOCATED_QTY"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ALLOCATED_QTY"]);
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ALLOCATED_QTY = Convert.ToInt64(dr["ALLOCATED_QTY"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public int TTL_ACTV_POINT { get; set; }

        public string IS_ACTV_POINT { get; set; }

        public DateTime FindSewingStartDateByLine(long pHR_PROD_LINE_ID)
        {
            string sp = "pkg_gmt_pln_line_load.gmt_pln_line_load_select";
            try
            {
                DateTime SEW_START_DT = new DateTime().Date;
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value = pHR_PROD_LINE_ID },
                     new CommandParameter() {ParameterName = "pOption", Value = 3011 },
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SEW_START_DT = Convert.ToDateTime(dr["SEW_START_DT"]);
                }
                return SEW_START_DT;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public GMT_PLN_LINE_LOADModel getEventDataForTuning(Int64 pGMT_PLN_LINE_LOAD_ID)
        {
            string sp = "pkg_gmt_pln_line_load.gmt_pln_line_load_select";
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = pGMT_PLN_LINE_LOAD_ID },
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"] },
                     new CommandParameter() {ParameterName = "pOption", Value = 3015},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                GMT_PLN_LINE_LOADModel ob = new GMT_PLN_LINE_LOADModel();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    
                    ob.GMT_PLN_LINE_LOAD_ID = (dr["GMT_PLN_LINE_LOAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_LINE_LOAD_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);

                    if (dr["PLAN_EFFICIENCY"] != DBNull.Value)
                    {
                        ob.PLAN_EFFICIENCY = Convert.ToDecimal(dr["PLAN_EFFICIENCY"]);
                    }

                    ob.MC_ORDER_ITEM_PLN_ID = (dr["MC_ORDER_ITEM_PLN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_ITEM_PLN_ID"]);
                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                    ob.SEW_START_DT = (dr["SEW_START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SEW_START_DT"]);
                    ob.SEW_END_DT = (dr["SEW_END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SEW_END_DT"]);
                    ob.ALLOCATED_QTY = (dr["ALLOCATED_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALLOCATED_QTY"]);
                    ob.PLAN_MP = (dr["PLAN_MP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLAN_MP"]);
                    ob.PLAN_WH = (dr["PLAN_WH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLAN_WH"]);
                    ob.IS_LRN_CRV_APP = (dr["IS_LRN_CRV_APP"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["IS_LRN_CRV_APP"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.LOADED_TIME = (dr["LOADED_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOADED_TIME"]);
                    ob.SMV = (dr["SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SMV"]);
                    ob.REQ_SAH = (dr["REQ_SAH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_SAH"]);
                    ob.BAR_COLOR_CODE = (dr["BAR_COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BAR_COLOR_CODE"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);
                    ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);

                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.ORDER_QTY = (dr["ORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDER_QTY"]);
                    ob.INITIAL_PROD_QTY = (dr["INITIAL_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INITIAL_PROD_QTY"]);

                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);

                    ob.GMT_PLN_STRIPE_PHASE_ID = (dr["GMT_PLN_STRIPE_PHASE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["GMT_PLN_STRIPE_PHASE_ID"]);

                    ob.RF_FAB_CLASS_ID = (dr["RF_FAB_CLASS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_CLASS_ID"]);
                    ob.GMT_PRODUCT_TYP_ID = (dr["GMT_PRODUCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PRODUCT_TYP_ID"]);

                    ob.sts = new List<GMT_PLN_LINE_LOAD_STSModel>();
                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                         new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = ob.GMT_PLN_LINE_LOAD_ID },
                                         new CommandParameter() {ParameterName = "pOption", Value = 3017},
                                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                  }, sp);
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        GMT_PLN_LINE_LOAD_STSModel ob1 = new GMT_PLN_LINE_LOAD_STSModel();
                        ob1.GMT_PLN_LINE_LD_STS_ID = (dr1["GMT_PLN_LINE_LD_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["GMT_PLN_LINE_LD_STS_ID"]);
                        ob1.LK_PLN_STS_GRP_ID = (dr1["LK_PLN_STS_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["LK_PLN_STS_GRP_ID"]);
                        ob1.PLN_STS_AREA_NAME = (dr1["PLN_STS_AREA_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PLN_STS_AREA_NAME"]);
                        ob1.PLN_STS_CODE = (dr1["PLN_STS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PLN_STS_CODE"]);
                        ob1.STS_COLOR_CODE = (dr1["STS_COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["STS_COLOR_CODE"]);
                        ob1.PLN_STS_NAME = (dr1["PLN_STS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PLN_STS_NAME"]);
                        ob.sts.Add(ob1);
                    }
                    ob.details = new GMT_PLN_LINE_LOAD_DModel().Query(ob.GMT_PLN_LINE_LOAD_ID);

                    ob.TOT_PROD = (dr["TOT_PROD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_PROD"]);

                    ob.IS_PAST = ob.details.Any(x => x.STS_FLAG == "P");
                    ob.DE_ALLOCATED_QTY = 0;
                    ob.TTL_REQ_PROD = ob.details.Sum(x => x.REQ_PROD_QTY);
                    ob.PLAN_OP = (dr["PLAN_OP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLAN_OP"]);
                    ob.PLAN_HP = (dr["PLAN_HP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLAN_HP"]);

                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);

                    ob.IS_COMMENTS_UPT_ALLOC = "Y";
                    ob.IS_COMMENTS_UPT_ITEM = "N";
                    ob.IS_COMMENTS_UPT_ORDER = "N";

                    ob.merges = new List<GMT_PLN_LINE_LD_MRGModel>();
                    ob.merges = new GMT_PLN_LINE_LD_MRGModel().Query(ob.GMT_PLN_LINE_LOAD_ID);
                    ob.others_allocations = new List<GMT_PLN_LINE_LOAD_OTHERSModel>();
                    ob.others_allocations = new GMT_PLN_LINE_LOAD_OTHERSModel().Query(ob.MC_ORDER_ITEM_PLN_ID, ob.HR_PROD_LINE_ID, ob.GMT_PLN_LINE_LOAD_ID);
                    ob.TTL_ALLOCATED_OTHERS = ob.others_allocations.Sum(x => x.ALLOCATED_QTY);
                    ob.TTL_PROD_OTHERS = ob.others_allocations.Sum(x => x.TOT_PROD);
                    ob.TTL_ALLOCATED_QTY = ob.TTL_ALLOCATED_OTHERS + ob.ALLOCATED_QTY;
                    ob.TTL_PROD = ob.TTL_PROD_OTHERS + ob.TOT_PROD;

                    ob.OUT_OF_PLAN_PROD_REC = (dr["OUT_OF_PLAN_PROD_REC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OUT_OF_PLAN_PROD_REC"]);

                    ob.PlanChangeLogs = new List<GMT_PLN_STYL_CNGE_LOGModel>();
                    ob.PlanChangeLogs = new GMT_PLN_STYL_CNGE_LOGModel().getPlanChangeLogData(ob.GMT_PLN_LINE_LOAD_ID);


                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<GMT_PLN_LINE_LOADModel> getOrderItmPlanList(Int64 pMC_STYLE_H_ID, Int64 pMC_STYLE_D_ITEM_ID, Int64 pMC_ORDER_ITEM_PLN_ID)
        {
            string sp = "pkg_gmt_pln_line_load.get_list_for_planning";
            try
            {
                OraDatabase db = new OraDatabase();
                var obList = new List<GMT_PLN_LINE_LOADModel>();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID },
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value = pMC_STYLE_D_ITEM_ID },
                     new CommandParameter() {ParameterName = "pMC_ORDER_ITEM_PLN_ID", Value = pMC_ORDER_ITEM_PLN_ID },
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PLN_LINE_LOADModel ob = new GMT_PLN_LINE_LOADModel();
                    ob.MC_ORDER_ITEM_PLN_ID = (dr["MC_ORDER_ITEM_PLN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_ITEM_PLN_ID"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);
                    ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);
                    ob.ORDER_QTY = (dr["ORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDER_QTY"]);
                    ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LEAD_TIME"]);
                    ob.IS_SELECTED = "N";
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public GMT_PLN_LINE_LOADModel getEventDataById(Int64 pGMT_PLN_LINE_LOAD_ID)
        {
            string sp = "pkg_gmt_pln_line_load.gmt_pln_line_load_select";
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = pGMT_PLN_LINE_LOAD_ID },
                     new CommandParameter() {ParameterName = "pOption", Value = 3021},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                GMT_PLN_LINE_LOADModel ob = new GMT_PLN_LINE_LOADModel();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    ob.GMT_PLN_LINE_LOAD_ID = (dr["GMT_PLN_LINE_LOAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_LINE_LOAD_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);

                    if (dr["PLAN_EFFICIENCY"] != DBNull.Value)
                    {
                        ob.PLAN_EFFICIENCY = Convert.ToDecimal(dr["PLAN_EFFICIENCY"]);
                    }

                    ob.MC_ORDER_ITEM_PLN_ID = (dr["MC_ORDER_ITEM_PLN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_ITEM_PLN_ID"]);
                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                    ob.SEW_START_DT = (dr["SEW_START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SEW_START_DT"]);
                    ob.SEW_END_DT = (dr["SEW_END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SEW_END_DT"]);
                    ob.ALLOCATED_QTY = (dr["ALLOCATED_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALLOCATED_QTY"]);
                    ob.PLAN_MP = (dr["PLAN_MP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLAN_MP"]);
                    ob.PLAN_WH = (dr["PLAN_WH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLAN_WH"]);
                    ob.IS_LRN_CRV_APP = (dr["IS_LRN_CRV_APP"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["IS_LRN_CRV_APP"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.LOADED_TIME = (dr["LOADED_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOADED_TIME"]);
                    ob.SMV = (dr["SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SMV"]);
                    ob.REQ_SAH = (dr["REQ_SAH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_SAH"]);
                    ob.BAR_COLOR_CODE = (dr["BAR_COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BAR_COLOR_CODE"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);
                    ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);

                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.ORDER_QTY = (dr["ORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDER_QTY"]);
                    ob.INITIAL_PROD_QTY = (dr["INITIAL_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INITIAL_PROD_QTY"]);

                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);

                    ob.RF_FAB_CLASS_ID = (dr["RF_FAB_CLASS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_CLASS_ID"]);
                    ob.GMT_PRODUCT_TYP_ID = (dr["GMT_PRODUCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PRODUCT_TYP_ID"]);

                    ob.PLAN_OP = (dr["PLAN_OP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLAN_OP"]);
                    ob.PLAN_HP = (dr["PLAN_HP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLAN_HP"]);
                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);
                    ob.PLN_OFFSET = (dr["PLN_OFFSET"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLN_OFFSET"]);

                    ob.GMT_PLN_STYL_CNGE_LOG_ID = (dr["GMT_PLN_STYL_CNGE_LOG_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["GMT_PLN_STYL_CNGE_LOG_ID"]);

         
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public List<int> HR_PROD_LINE_LST { get; set; }

        public int HR_PROD_FLR_ID { get; set; }

        public string ALLOCATION_DESC { get; set; }

        public decimal? PLAN_EFFICIENCY { get; set; }

        public DateTime ORD_CONF_DT { get; set; }
        public long ORDER_QTY { get; set; }
        public long INITIAL_PROD_QTY { get; set; }
        public long TOT_PROD { get; set; }

        public bool IS_PAST { get; set; }

        public int DE_ALLOCATED_QTY { get; set; }

        public string updateEventForTuning()
        {
            const string sp = "pkg_gmt_pln_line_load.event_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = ob.GMT_PLN_LINE_LOAD_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value = ob.HR_PROD_LINE_ID},
                     new CommandParameter() {ParameterName = "pSEW_START_DT", Value = ob.SEW_START_DT},
                     new CommandParameter() {ParameterName = "pSEW_END_DT", Value = ob.SEW_END_DT},
                     new CommandParameter() {ParameterName = "pPLN_OFFSET", Value = ob.PLN_OFFSET},
                     new CommandParameter() {ParameterName = "pALLOCATED_QTY", Value = ob.ALLOCATED_QTY},
                     new CommandParameter() {ParameterName = "pINITIAL_PROD_QTY", Value = ob.INITIAL_PROD_QTY},
                     new CommandParameter() {ParameterName = "pPLAN_MP", Value = ob.PLAN_MP},
                     new CommandParameter() {ParameterName = "pPLAN_OP", Value = ob.PLAN_OP},
                     new CommandParameter() {ParameterName = "pPLAN_HP", Value = ob.PLAN_HP},
                     new CommandParameter() {ParameterName = "pPLAN_WH", Value = ob.PLAN_WH},

                     new CommandParameter() {ParameterName = "pPLAN_EFFICIENCY", Value = ob.PLAN_EFFICIENCY},
                     new CommandParameter() {ParameterName = "pIS_LRN_CRV_APP", Value = ob.IS_LRN_CRV_APP},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pMERGE_XML", Value = ob.MERGE_XML},

                     new CommandParameter() {ParameterName = "pIS_COMMENTS_UPT_ALLOC", Value = ob.IS_COMMENTS_UPT_ALLOC},
                     new CommandParameter() {ParameterName = "pIS_COMMENTS_UPT_ITEM", Value = ob.IS_COMMENTS_UPT_ITEM},
                     new CommandParameter() {ParameterName = "pIS_COMMENTS_UPT_ORDER", Value = ob.IS_COMMENTS_UPT_ORDER},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},

                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = ob.pOption ?? 1001},
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

        public long TTL_REQ_PROD { get; set; }
        public int? pOption { get; set; }

        public int PLAN_OP { get; set; }

        public int PLAN_HP { get; set; }

        public long RF_FAB_CLASS_ID { get; set; }

        public long GMT_PRODUCT_TYP_ID { get; set; }

        public object getForcastLineLoadDataById(long pGMT_PLN_LINE_LOAD_ID, string pIS_DYNAMIC_START)
        {
            string sp = "pkg_gmt_pln_line_load.find_sewing_end_date";
            object obj = new { }; 
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = pGMT_PLN_LINE_LOAD_ID },
                     new CommandParameter() {ParameterName = "pIS_DYNAMIC_START", Value = pIS_DYNAMIC_START },
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obj = new
                    {
                        SEW_START_DT = Convert.ToDateTime(dr["SEW_START_DT"]),
                        SEW_END_DT = Convert.ToDateTime(dr["SEW_END_DT"])
                    };
                }
                return obj;  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object getForcastLineLoadDataById2(long pGMT_PLN_LINE_LOAD_ID, string pIS_DYNAMIC_END)
        {
            string sp = "pkg_gmt_pln_line_load.find_sewing_start_date";
            object obj = new { };
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = pGMT_PLN_LINE_LOAD_ID },
                     new CommandParameter() {ParameterName = "pIS_DYNAMIC_END", Value = pIS_DYNAMIC_END },
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obj = new
                    {
                        SEW_START_DT = Convert.ToDateTime(dr["SEW_START_DT"]),
                        SEW_END_DT = Convert.ToDateTime(dr["SEW_END_DT"])
                    };
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GMT_PLN_LINE_LOADModel> getProdPlanDataByLine(Int32 pHR_PROD_LINE_ID, DateTime? pSTART_DT = null, DateTime? pEND_DT = null, Int32? pOption=null)
        {
            string sp = "pkg_gmt_pln_line_load.gmt_pln_line_load_select";
            try
            {
                var obList = new List<GMT_PLN_LINE_LOADModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value =pHR_PROD_LINE_ID },
                     new CommandParameter() {ParameterName = "pSTART_DT", Value =pSTART_DT },
                     new CommandParameter() {ParameterName = "pEND_DT", Value =pEND_DT },
                     new CommandParameter() {ParameterName = "pOption", Value = pOption },
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PLN_LINE_LOADModel ob = new GMT_PLN_LINE_LOADModel();

                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["LINE_CODE"]);

                    ob.GMT_PLN_LINE_LOAD_ID = (dr["GMT_PLN_LINE_LOAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_LINE_LOAD_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.MC_ORDER_ITEM_PLN_ID = (dr["MC_ORDER_ITEM_PLN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_ITEM_PLN_ID"]);
                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);

                    ob.IS_ACTIVE = (ob.HR_PROD_LINE_ID == pHR_PROD_LINE_ID) ? "Y" : "N";

                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["HR_PROD_FLR_ID"]);

                    ob.SEW_START_DT = (dr["SEW_START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SEW_START_DT"]);
                    ob.SEW_END_DT = (dr["SEW_END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SEW_END_DT"]);
                    ob.ALLOCATED_QTY = (dr["ALLOCATED_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALLOCATED_QTY"]);
                    ob.PLAN_MP = (dr["PLAN_MP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLAN_MP"]);
                    ob.PLAN_WH = (dr["PLAN_WH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLAN_WH"]);
                    ob.IS_LRN_CRV_APP = (dr["IS_LRN_CRV_APP"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["IS_LRN_CRV_APP"]);
                    ob.LOADED_TIME = (dr["LOADED_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOADED_TIME"]);
                    ob.SMV = (dr["SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SMV"]);
                    ob.REQ_SAH = (dr["REQ_SAH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_SAH"]);
                    ob.BAR_COLOR_CODE = (dr["BAR_COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BAR_COLOR_CODE"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.PLAN_OP = (dr["PLAN_OP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLAN_OP"]);
                    ob.PLAN_HP = (dr["PLAN_HP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLAN_HP"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);

                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);
                    ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);

                    if (dr["SEW_IN_DATE"] != DBNull.Value)
                    {
                        ob.SEW_IN_DATE =  Convert.ToDateTime(dr["SEW_IN_DATE"]);
                    }
                    

                    

                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.ORDER_QTY = (dr["ORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDER_QTY"]);
                    ob.INITIAL_PROD_QTY = (dr["INITIAL_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INITIAL_PROD_QTY"]);

                    ob.TOT_PROD = (dr["TOT_PROD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_PROD"]);

                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.GMT_PLN_STRIPE_PHASE_ID = (dr["GMT_PLN_STRIPE_PHASE_ID"] == DBNull.Value) ? 1 : Convert.ToInt32(dr["GMT_PLN_STRIPE_PHASE_ID"]);
                    ob.PLN_STRIPE_PHASE_NAME = (dr["PLN_STRIPE_PHASE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLN_STRIPE_PHASE_NAME"]);
                    ob.RGB_COL_CODE = (dr["RGB_COL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE"]);
                    ob.STYL_KEY_IMG = (dr["STYL_KEY_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_KEY_IMG"]);
                    ob.IS_USED_IN_TRGT = (dr["IS_USED_IN_TRGT"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_USED_IN_TRGT"]);
                    ob.SHIP_DE = (dr["SHIP_DE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIP_DE"]);
                    ob.DAYS_DE = (dr["DAYS_DE"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["DAYS_DE"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string STYL_KEY_IMG { get; set; }

        public string MERGE_XML { get; set; }

        public long TTL_ALLOCATED_OTHERS { get; set; }

        public long TTL_ALLOCATED_QTY { get; set; }

        public long MRG_ACTV_POINT { get; set; }

        public string LINE_CODE { get; set; }

        public string RGB_COL_CODE { get; set; }

        public string PLN_STRIPE_PHASE_NAME { get; set; }

        public string BYR_ACC_GRP_NAME_EN { get; set; }

        public DateTime? SEW_IN_DATE { get; set; }

        public string DeletePlanData()
        {
            const string sp = "pkg_gmt_pln_line_load.event_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = ob.GMT_PLN_LINE_LOAD_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =1008},
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

        public long PROD_QTY { get; set; }

        public string SHIP_DE { get; set; }

        public int PLN_OFFSET { get; set; }

        public int DAYS_DE { get; set; }

        public long TTL_PROD_OTHERS { get; set; }

        public long TTL_PROD { get; set; }

        public string OUT_OF_PLAN_PROD_REC { get; set; }

        public string IS_COMMENTS_UPT_ALLOC { get; set; }

        public string IS_COMMENTS_UPT_ITEM { get; set; }

        public string IS_COMMENTS_UPT_ORDER { get; set; }
        public long GMT_PLN_STYL_CNGE_LOG_ID { get; set; }
        public List<GMT_PLN_STYL_CNGE_LOGModel> PlanChangeLogs { get; set; }
        public long MC_ORDER_ITEM_PLN_ID { get; set; }
        public int LEAD_TIME { get; set; }
        public string IS_SELECTED { get; set; }
    }
}