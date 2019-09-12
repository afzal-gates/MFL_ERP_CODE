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
    public class GMT_LN_LOAD_PLANModel
    {
        public Int64 GMT_LN_LOAD_PLAN_ID { get; set; }
        public Int64 MC_ORDER_STYL_ID { get; set; }
        public Int64 HR_PROD_LINE_ID { get; set; }
        public DateTime SEW_START_DT { get; set; }
        public Int64? HRLY_TGT_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Int64? REQ_OPERATOR { get; set; }
        public Int64? REQ_HELPER { get; set; }
        public Int64? NO_OF_MC { get; set; }
        public string REMARKS { get; set; }
        public string XML { get; set; }

        public Int64? pOption { get; set; }

        public string Save()
        {
            const string sp = "pkg_hourly_prod.gmt_ln_load_plan_insert";
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
                     new CommandParameter() {ParameterName = "pOption", Value =ob.pOption},
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

        public object SelectAll(String pHR_PROD_FLR_LST, String pHR_PROD_LINE_LST)
        {
            string sp = "pkg_hourly_prod.gmt_ln_load_plan_select";
            try
            {
                var obList = new List<GMT_LN_LOAD_PLANModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_LST", Value =pHR_PROD_FLR_LST},
                        new CommandParameter() {ParameterName = "pHR_PROD_LINE_LST", Value =pHR_PROD_LINE_LST},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_LN_LOAD_PLANModel ob = new GMT_LN_LOAD_PLANModel();
                            ob.GMT_LN_LOAD_PLAN_ID = (dr["GMT_LN_LOAD_PLAN_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["GMT_LN_LOAD_PLAN_ID"]);
                            ob.MC_ORDER_STYL_ID = (dr["MC_ORDER_STYL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_STYL_ID"]);


                            ob.IS_EDITABLE = (dr["GMT_LN_LOAD_PLAN_ID"] == DBNull.Value) ? true : false;

                            ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                            ob.SEW_START_DT = (dr["SEW_START_DT"] == DBNull.Value) ? DateTime.Today : Convert.ToDateTime(dr["SEW_START_DT"]);

                            if (dr["HRLY_TGT_QTY"] != DBNull.Value)
                            {
                                ob.HRLY_TGT_QTY =  Convert.ToInt64(dr["HRLY_TGT_QTY"]);
                            }
                           
                            ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);


                            if (dr["REQ_OPERATOR"] != DBNull.Value)
                            {
                                ob.REQ_OPERATOR = Convert.ToInt64(dr["REQ_OPERATOR"]);
                            }


                            if (dr["REQ_HELPER"] != DBNull.Value)
                            {
                                ob.REQ_HELPER = Convert.ToInt64(dr["REQ_HELPER"]);
                            }

                            if (dr["SMV"] != DBNull.Value)
                            {
                                ob.SMV = Convert.ToDecimal(dr["SMV"]);
                            }


                            ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                            ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                            ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_NO"]);
                            ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);
                            ob.LINE_DESC_EN = (dr["LINE_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_DESC_EN"]);
                            //ob.HR_PROD_FLR_ID_SPAN = (dr["HR_PROD_FLR_ID_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID_SPAN"]);
                            //ob.HR_PROD_FLR_ID_SL = (dr["HR_PROD_FLR_ID_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID_SL"]);

                            ob.HR_PROD_LINE_ID_SPAN = (dr["HR_PROD_LINE_ID_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID_SPAN"]);
                            ob.HR_PROD_LINE_ID_SL = (dr["HR_PROD_LINE_ID_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID_SL"]);

                            ob.FLOOR_NO = (dr["FLOOR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_NO"]);
                            ob.FLOOR_CODE = (dr["FLOOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_CODE"]);
                            ob.FLOOR_DESC_EN = (dr["FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_EN"]);

                            ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                            ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                            ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                            ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                            ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                            ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                            ob.PROD_LINE_LST = (dr["PROD_LINE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROD_LINE_LST"]);
                            ob.IS_MRGED = (dr["IS_MRGED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MRGED"]);

                            ob.IS_PAST_SSD = (dr["IS_PAST_SSD"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_PAST_SSD"]);
                  

                            obList.Add(ob);
               }

              List<string> MergeLine = obList.Where(b => b.PROD_LINE_LST != String.Empty).Select(o => o.PROD_LINE_LST).ToList();

                return new {
                    data = obList,
                    MergeLine = MergeLine
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_LN_LOAD_PLANModel Select(long ID)
        {
            string sp = "Select_GMT_LN_LOAD_PLAN";
            try
            {
                var ob = new GMT_LN_LOAD_PLANModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_LN_LOAD_PLAN_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.GMT_LN_LOAD_PLAN_ID = (dr["GMT_LN_LOAD_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_LN_LOAD_PLAN_ID"]);
                        ob.MC_ORDER_STYL_ID = (dr["MC_ORDER_STYL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_STYL_ID"]);
                        ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                        ob.SEW_START_DT = (dr["SEW_START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SEW_START_DT"]);
                        ob.HRLY_TGT_QTY = (dr["HRLY_TGT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HRLY_TGT_QTY"]);
                        ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                        ob.REQ_OPERATOR = (dr["REQ_OPERATOR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_OPERATOR"]);
                        ob.REQ_HELPER = (dr["REQ_HELPER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_HELPER"]);
                        ob.NO_OF_MC = (dr["NO_OF_MC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_MC"]);
                        ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long HR_PROD_FLR_ID { get; set; }

        public string LINE_NO { get; set; }

        public string LINE_CODE { get; set; }

        public string LINE_DESC_EN { get; set; }

        public long HR_PROD_FLR_ID_SPAN { get; set; }

        public long HR_PROD_FLR_ID_SL { get; set; }

        public string FLOOR_NO { get; set; }

        public string FLOOR_CODE { get; set; }

        public string FLOOR_DESC_EN { get; set; }

        public bool IS_EDITABLE { get; set; }

        public string ORDER_NO { get; set; }

        public string BYR_ACC_NAME_EN { get; set; }

        public string ITEM_NAME_EN { get; set; }

        public long MC_ORDER_H_ID { get; set; }

        public long MC_BYR_ACC_ID { get; set; }

        public long MC_STYLE_D_ITEM_ID { get; set; }

        public Object getLineLoadingPlanDataEntry(string pHR_PROD_FLR_LST, string pHR_PROD_LINE_LST, DateTime? pPROD_DT)
        {
            string sp = "pkg_hourly_prod.gmt_ln_load_plan_select";
            try
            {
                var obList = new List<GMT_LN_LOAD_PLANModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_LST", Value =pHR_PROD_FLR_LST},
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_LST", Value =pHR_PROD_LINE_LST},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value =pPROD_DT??DateTime.Now.Date},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_LN_LOAD_PLANModel ob = new GMT_LN_LOAD_PLANModel();
                    ob.GMT_LN_LOAD_PLAN_ID = (dr["GMT_LN_LOAD_PLAN_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["GMT_LN_LOAD_PLAN_ID"]);
                    ob.MC_ORDER_STYL_ID = (dr["MC_ORDER_STYL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_STYL_ID"]);


                    ob.IS_EDITABLE = (dr["GMT_LN_LOAD_PLAN_ID"] == DBNull.Value) ? true : false;

                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                    ob.SEW_START_DT = (dr["SEW_START_DT"] == DBNull.Value) ? DateTime.Today : Convert.ToDateTime(dr["SEW_START_DT"]);

                    ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? DateTime.Today : Convert.ToDateTime(dr["PROD_DT"]);


                    ob.DAYS_RUN = (Convert.ToDateTime(ob.PROD_DT) - Convert.ToDateTime(ob.SEW_START_DT)).Days + 1;

                    if (dr["HRLY_TGT_QTY"] != DBNull.Value)
                    {
                        ob.HRLY_TGT_QTY = Convert.ToInt64(dr["HRLY_TGT_QTY"]);
                    }

                    if (dr["HRLY_TGT_EFF"] != DBNull.Value)
                    {
                        ob.HRLY_TGT_EFF = Convert.ToDecimal(dr["HRLY_TGT_EFF"]);
                    }

                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);


                    if (dr["REQ_OPERATOR"] != DBNull.Value)
                    {
                        ob.REQ_OPERATOR = Convert.ToInt64(dr["REQ_OPERATOR"]);
                    }


                    if (dr["REQ_HELPER"] != DBNull.Value)
                    {
                        ob.REQ_HELPER = Convert.ToInt64(dr["REQ_HELPER"]);
                    }


                    if (dr["CUR_OPERATOR"] != DBNull.Value)
                    {
                        ob.CUR_OPERATOR = Convert.ToInt32(dr["CUR_OPERATOR"]);
                    }


                    if (dr["CUR_HELPER"] != DBNull.Value)
                    {
                        ob.CUR_HELPER = Convert.ToInt32(dr["CUR_HELPER"]);
                    }

                    ob.USE_OPERATOR = (dr["USE_OPERATOR"] == DBNull.Value) ? ob.CUR_OPERATOR ?? 0 : Convert.ToInt32(dr["USE_OPERATOR"]);
                    ob.USE_HELPER = (dr["USE_HELPER"] == DBNull.Value) ? ob.CUR_HELPER ?? 0 : Convert.ToInt32(dr["USE_HELPER"]);

                    if (dr["RF_PFLT_RSN_TYPE_ID"] != DBNull.Value)
                    {
                        ob.RF_PFLT_RSN_TYPE_ID = Convert.ToInt64(dr["RF_PFLT_RSN_TYPE_ID"]);
                    }

                    ob.RSN_TYPE_NAME_EN = (dr["RSN_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RSN_TYPE_NAME_EN"]);

                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_NO"]);
                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);
                    ob.LINE_DESC_EN = (dr["LINE_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_DESC_EN"]);
                    ob.HR_PROD_FLR_ID_SPAN = (dr["HR_PROD_FLR_ID_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID_SPAN"]);
                    ob.HR_PROD_FLR_ID_SL = (dr["HR_PROD_FLR_ID_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID_SL"]);

                    ob.HR_PROD_LINE_ID_SPAN = (dr["HR_PROD_LINE_ID_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID_SPAN"]);
                    ob.HR_PROD_LINE_ID_SL = (dr["HR_PROD_LINE_ID_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID_SL"]);

                    ob.FLOOR_NO = (dr["FLOOR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_NO"]);
                    ob.FLOOR_CODE = (dr["FLOOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_CODE"]);
                    ob.FLOOR_DESC_EN = (dr["FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_EN"]);

                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);

                    ob.CT_PRD_QTY = (dr["CT_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CT_PRD_QTY"]);
                    ob.CT_TGT_QTY = (dr["CT_TGT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CT_TGT_QTY"]);


                    if (dr["H1_PRD_QTY"] != DBNull.Value)
                    {
                        ob.H1_PRD_QTY = Convert.ToInt64(dr["H1_PRD_QTY"]);
                    }
                    if (dr["H2_PRD_QTY"] != DBNull.Value)
                    {
                        ob.H2_PRD_QTY = Convert.ToInt64(dr["H2_PRD_QTY"]);
                    }
                    if (dr["H3_PRD_QTY"] != DBNull.Value)
                    {
                        ob.H3_PRD_QTY = Convert.ToInt64(dr["H3_PRD_QTY"]);
                    }
                    if (dr["H4_PRD_QTY"] != DBNull.Value)
                    {
                        ob.H4_PRD_QTY = Convert.ToInt64(dr["H4_PRD_QTY"]);
                    }

                    if (dr["H5_PRD_QTY"] != DBNull.Value)
                    {
                        ob.H5_PRD_QTY = Convert.ToInt64(dr["H5_PRD_QTY"]);
                    }
                    if (dr["H6_PRD_QTY"] != DBNull.Value)
                    {
                        ob.H6_PRD_QTY = Convert.ToInt64(dr["H6_PRD_QTY"]);
                    }
                    if (dr["H7_PRD_QTY"] != DBNull.Value)
                    {
                        ob.H7_PRD_QTY = Convert.ToInt64(dr["H7_PRD_QTY"]);
                    }
                    if (dr["H8_PRD_QTY"] != DBNull.Value)
                    {
                        ob.H8_PRD_QTY = Convert.ToInt64(dr["H8_PRD_QTY"]);
                    }



                    if (dr["OT_TGT_HR"] != DBNull.Value)
                    {
                        ob.OT_TGT_HR = Convert.ToInt64(dr["OT_TGT_HR"]);
                    }

                    

                    if (dr["OT_ACT_PRD_QTY"] != DBNull.Value)
                    {
                        ob.OT_ACT_PRD_QTY = Convert.ToInt64(dr["OT_ACT_PRD_QTY"]);
                    }

                    if (dr["GEN_TGT_HR"] != DBNull.Value)
                    {
                        ob.GEN_TGT_HR = Convert.ToDecimal(dr["GEN_TGT_HR"]);
                    }
                    

                    ob.GMT_HRLY_PROD_ID = (dr["GMT_HRLY_PROD_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["GMT_HRLY_PROD_ID"]);

                    ob.SMV = (dr["SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SMV"]);


                    ob.CHK_GH_PRD = (dr["CHK_GH_PRD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHK_GH_PRD"]);
                    ob.IS_GH_TGT_LOCK = (dr["IS_GH_TGT_LOCK"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_GH_TGT_LOCK"]);
                    ob.IS_OTH_TGT_LOCK = (dr["IS_OTH_TGT_LOCK"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_OTH_TGT_LOCK"]);
                    ob.IS_OT_ACT_LOCK = (dr["IS_OT_ACT_LOCK"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_OT_ACT_LOCK"]);
                    ob.IS_OT_ACT_LOCK_D = ob.IS_OTH_TGT_LOCK=="Y"?"N":"Y";

                    ob.OT_TGT_QTY = (dr["OT_TGT_QTY"] == DBNull.Value) ? (ob.HRLY_TGT_QTY ?? 0) : (ob.IS_OTH_TGT_LOCK=="N"? (ob.HRLY_TGT_QTY ?? 0) : Convert.ToInt64(dr["OT_TGT_QTY"]));


                    ob.IS_GH_TGT_LOCK_S = ob.IS_GH_TGT_LOCK;
                    ob.IS_OTH_TGT_LOCK_S = ob.IS_OTH_TGT_LOCK;
                    ob.IS_OT_ACT_LOCK_S = ob.IS_OT_ACT_LOCK;


                    ob.CHK_GH_PRD_1 = ob.CHK_GH_PRD.IndexOf("1")>=0?"Y":"N";
                    ob.CHK_GH_PRD_1_D = ob.IS_GH_TGT_LOCK == "Y" ? "N" : "Y";
                    ob.CHK_GH_PRD_1_D_S = ob.CHK_GH_PRD_1;
                    ob.CHK_GH_PRD_2 = ob.CHK_GH_PRD.IndexOf("2") >= 0 ? "Y" : "N";
                    ob.CHK_GH_PRD_2_D = (ob.IS_GH_TGT_LOCK=="Y" && ob.CHK_GH_PRD_1 == "Y") ? "N" : "Y";
                    ob.CHK_GH_PRD_2_D_S = ob.CHK_GH_PRD_2;

                    ob.CHK_GH_PRD_3 = ob.CHK_GH_PRD.IndexOf("3") >= 0 ? "Y" : "N";
                    ob.CHK_GH_PRD_3_D = (ob.IS_GH_TGT_LOCK == "Y" && ob.CHK_GH_PRD_1 == "Y" && ob.CHK_GH_PRD_2 == "Y") ? "N" : "Y";

                    ob.CHK_GH_PRD_3_D_S = ob.CHK_GH_PRD_3;


                    ob.CHK_GH_PRD_4 = ob.CHK_GH_PRD.IndexOf("4") >= 0 ? "Y" : "N";
                    ob.CHK_GH_PRD_4_D = (ob.IS_GH_TGT_LOCK == "Y" && ob.CHK_GH_PRD_1 == "Y" && ob.CHK_GH_PRD_2 == "Y" && ob.CHK_GH_PRD_3 == "Y") ? "N" : "Y";
                    ob.CHK_GH_PRD_4_D_S = ob.CHK_GH_PRD_4;


                    ob.CHK_GH_PRD_5 = ob.CHK_GH_PRD.IndexOf("5") >= 0 ? "Y" : "N";
                    ob.CHK_GH_PRD_5_D = (ob.IS_GH_TGT_LOCK == "Y" && ob.CHK_GH_PRD_1 == "Y" && ob.CHK_GH_PRD_2 == "Y" && ob.CHK_GH_PRD_3 == "Y" && ob.CHK_GH_PRD_4 == "Y") ? "N" : "Y";
                    ob.CHK_GH_PRD_5_D_S = ob.CHK_GH_PRD_5;

                    ob.CHK_GH_PRD_6 = ob.CHK_GH_PRD.IndexOf("6") >= 0 ? "Y" : "N";
                    ob.CHK_GH_PRD_6_D = (ob.IS_GH_TGT_LOCK == "Y" && ob.CHK_GH_PRD_1 == "Y" && ob.CHK_GH_PRD_2 == "Y" && ob.CHK_GH_PRD_3 == "Y" && ob.CHK_GH_PRD_4 == "Y" && ob.CHK_GH_PRD_5 == "Y") ? "N" : "Y";
                    ob.CHK_GH_PRD_6_D_S = ob.CHK_GH_PRD_6;
                    ob.CHK_GH_PRD_7 = ob.CHK_GH_PRD.IndexOf("7") >= 0 ? "Y" : "N";
                    ob.CHK_GH_PRD_7_D = (ob.IS_GH_TGT_LOCK == "Y" && ob.CHK_GH_PRD_1 == "Y" && ob.CHK_GH_PRD_2 == "Y" && ob.CHK_GH_PRD_3 == "Y" && ob.CHK_GH_PRD_4 == "Y" && ob.CHK_GH_PRD_5 == "Y" && ob.CHK_GH_PRD_6 == "Y") ? "N" : "Y";
                    ob.CHK_GH_PRD_7_D_S = ob.CHK_GH_PRD_7;
                    ob.CHK_GH_PRD_8 = ob.CHK_GH_PRD.IndexOf("8") >= 0 ? "Y" : "N";
                    ob.CHK_GH_PRD_8_D = (ob.IS_GH_TGT_LOCK == "Y" && ob.CHK_GH_PRD_1 == "Y" && ob.CHK_GH_PRD_2 == "Y" && ob.CHK_GH_PRD_3 == "Y" && ob.CHK_GH_PRD_4 == "Y" && ob.CHK_GH_PRD_5 == "Y" && ob.CHK_GH_PRD_6 == "Y" && ob.CHK_GH_PRD_7== "Y") ? "N" : "Y";
                    ob.CHK_GH_PRD_8_D_S = ob.CHK_GH_PRD_8;


                    ob.PROD_LINE_LST = (dr["PROD_LINE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROD_LINE_LST"]);
                    ob.IS_MRGED = (dr["IS_MRGED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MRGED"]);


                    obList.Add(ob);
                }

                List<string> MergeLine = obList.Where(b => b.PROD_LINE_LST != String.Empty).Select(o => o.PROD_LINE_LST).ToList();

                return new
                {
                    data = obList,
                    MergeLine = MergeLine
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string STYLE_NO { get; set; }

        public long? H1_PRD_QTY { get; set; }
        public long? H2_PRD_QTY { get; set; }
        public long? H3_PRD_QTY { get; set; }
        public long? H4_PRD_QTY { get; set; }
        public long? H5_PRD_QTY { get; set; }
        public long? H6_PRD_QTY { get; set; }
        public long? H7_PRD_QTY { get; set; }
        public long? H8_PRD_QTY { get; set; }

        public long GMT_HRLY_PROD_ID { get; set; }

        public long? OT_TGT_HR { get; set; }

        public long? OT_TGT_QTY { get; set; }

        public long? OT_ACT_PRD_QTY { get; set; }

        public long LK_FLOOR_ID { get; set; }

        public long HR_PROD_LINE_ID_SL { get; set; }

        public long HR_PROD_LINE_ID_SPAN { get; set; }

        public Int32? CUR_OPERATOR { get; set; }
        public Int32? CUR_HELPER { get; set; }
        public Decimal LN_TTL_TARGET { get; set; }
        public Decimal LN_TTL_PROD { get; set; }

        public Decimal LN_ACHV { get; set; }

        public Int32 USE_OPERATOR { get; set; }

        public Int32 USE_HELPER { get; set; }

        public long? RF_PFLT_RSN_TYPE_ID { get; set; }
        public string RSN_TYPE_NAME_EN { get; set; }
        public DateTime? PROD_DT { get; set; }

        public string CHK_GH_PRD { get; set; }

        public string IS_GH_TGT_LOCK { get; set; }

        public string CHK_GH_PRD_1 { get; set; }
        public string CHK_GH_PRD_1_D { get; set; }

        public string CHK_GH_PRD_2 { get; set; }

        public string CHK_GH_PRD_2_D { get; set; }

        public string CHK_GH_PRD_3_D { get; set; }

        public string CHK_GH_PRD_3 { get; set; }

        public string CHK_GH_PRD_4_D { get; set; }

        public string CHK_GH_PRD_4 { get; set; }

        public string CHK_GH_PRD_5 { get; set; }

        public string CHK_GH_PRD_5_D { get; set; }

        public string CHK_GH_PRD_6 { get; set; }

        public string CHK_GH_PRD_6_D { get; set; }

        public string CHK_GH_PRD_7 { get; set; }

        public string CHK_GH_PRD_7_D { get; set; }

        public string CHK_GH_PRD_8 { get; set; }

        public string CHK_GH_PRD_8_D { get; set; }

        public string IS_OTH_TGT_LOCK { get; set; }

        public string IS_OT_ACT_LOCK { get; set; }

        public string CHK_GH_PRD_7_D_S { get; set; }

        public string CHK_GH_PRD_8_D_S { get; set; }

        public string CHK_GH_PRD_6_D_S { get; set; }

        public string CHK_GH_PRD_5_D_S { get; set; }

        public string CHK_GH_PRD_4_D_S { get; set; }

        public string CHK_GH_PRD_3_D_S { get; set; }

        public string CHK_GH_PRD_2_D_S { get; set; }

        public string CHK_GH_PRD_1_D_S { get; set; }

        public string IS_OT_ACT_LOCK_S { get; set; }

        public string IS_OTH_TGT_LOCK_S { get; set; }

        public string IS_GH_TGT_LOCK_S { get; set; }

        public string IS_OT_ACT_LOCK_D { get; set; }

        public decimal UNIT_PRICE { get; set; }

        public decimal TTL_VALUE { get; set; }

        public long LN_TTL_CUR_TARGET { get; set; }

        public decimal SMV { get; set; }

        public int DAYS_RUN { get; set; }

        public decimal? HRLY_TGT_EFF { get; set; }

        public string STYLE_NO_S { get; set; }

        public string ORDER_NO_S { get; set; }

        public decimal LN_EFF { get; set; }

        public Decimal INPUT_MAN_MIN { get; set; }

        public Decimal OUTPUT_MAN_MIN { get; set; }

        public string IS_MRGED { get; set; }

        public string PROD_LINE_LST { get; set; }

        public long CT_PRD_QTY { get; set; }
        public long CT_TGT_QTY { get; set; }
        public decimal CT_ACHV { get; set; }
        public string IS_PAST_SSD { get; set; }
        public decimal GEN_TGT_HR { get; set; }
    }
}