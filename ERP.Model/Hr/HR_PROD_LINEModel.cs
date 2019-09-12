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
    public class HR_PROD_LINEModel
    {
        public Int64 HR_PROD_LINE_ID { get; set; }
        public Int64 HR_PROD_FLR_ID { get; set; }
        public string LINE_NO { get; set; }
        public string LINE_CODE { get; set; }
        public string LINE_DESC_EN { get; set; }
        public string LINE_DESC_BN { get; set; }
        public Int64 LK_GARM_TYPE_ID { get; set; }
        public Int64 LK_FLOOR_ID { get; set; }
        public string IS_ACTIVE { get; set; }

        public Int64? GMT_PLN_RSRC_ACCESS_ID { get; set; }


        public Int64 TTL_REQ_OP { get; set; }
        public Int64 TTL_REQ_HP { get; set; }
        public Int64 TTL_PRE_OP { get; set; }
        public Int64 TTL_PRE_HP { get; set; }
        public Int64 TTL_H1 { get; set; }
        public Int64 TTL_H2 { get; set; }
        public Int64 TTL_H3 { get; set; }
        public Int64 TTL_H4 { get; set; }
        public Int64 TTL_H5 { get; set; }
        public Int64 TTL_H6 { get; set; }
        public Int64 TTL_H7 { get; set; }
        public Int64 TTL_H8 { get; set; }
        public Int64 TTL_OT_TARGET { get; set; }
        public Int64 TTL_OT_PROD { get; set; }
        public Int64 TTL_OT_HR { get; set; }
        public long LN_TTL_TARGET { get; set; }

        public long LN_TTL_PROD { get; set; }

        private List<string> _MergeLine = null;

        public List<string> MergeLine
        {
            get
            {
                if (_MergeLine == null)
                {
                    _MergeLine = new List<string>();
                }
                return _MergeLine;
            }
            set
            {
                _MergeLine = value;
            }
        }


        private List<GMT_LN_LOAD_PLANModel> _items = null;

        public List<GMT_LN_LOAD_PLANModel> items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<GMT_LN_LOAD_PLANModel>();
                }
                return _items;
            }
            set
            {
                _items = value;
            }
        }


        private List<GMT_FIN_PRODModel> _datas = null;

        public List<GMT_FIN_PRODModel> datas
        {
            get
            {
                if (_datas == null)
                {
                    _datas = new List<GMT_FIN_PRODModel>();
                }
                return _datas;
            }
            set
            {
                _datas = value;
            }
        }



        public string Save()
        {
            const string sp = "pkg_hr.hr_prod_line_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value = ob.HR_PROD_LINE_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = ob.HR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                     new CommandParameter() {ParameterName = "pLINE_CODE", Value = ob.LINE_CODE},
                     new CommandParameter() {ParameterName = "pLINE_DESC_EN", Value = ob.LINE_DESC_EN},
                     new CommandParameter() {ParameterName = "pLINE_DESC_BN", Value = ob.LINE_DESC_BN},
                     new CommandParameter() {ParameterName = "pLK_GARM_TYPE_ID", Value = ob.LK_GARM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "opHR_PROD_LINE_ID", Value =0, Direction = ParameterDirection.Output},
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

        public List<HR_PROD_LINEModel> SelectAll()
        {
            string sp = "pkg_common.hr_prod_line_select";
            try
            {
                var obList = new List<HR_PROD_LINEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_PROD_LINEModel ob = new HR_PROD_LINEModel();
                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_NO"]);
                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);
                    ob.LINE_DESC_EN = (dr["LINE_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_DESC_EN"]);
                    ob.LINE_DESC_BN = (dr["LINE_DESC_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_DESC_BN"]);
                    ob.LK_GARM_TYPE_ID = (dr["LK_GARM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GARM_TYPE_ID"]);
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);

                    ob.LK_FLOOR_ID_SPAN = (dr["HR_PROD_FLR_ID_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID_SPAN"]);
                    ob.LK_FLOOR_ID_SL = (dr["HR_PROD_FLR_ID_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID_SL"]);

                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.FLOOR_NO = (dr["FLOOR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_NO"]);
                    ob.FLOOR_CODE = (dr["FLOOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_CODE"]);
                    ob.FLOOR_DESC_EN = (dr["FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_EN"]);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<HR_PROD_LINEModel> SelectByID(Int64? pHR_PROD_LINE_ID = null, Int64? pHR_PROD_FLR_ID = null, Int64? pHR_PROD_BLDNG_ID = null, Int64? pHR_OFFICE_ID = null)
        {
            string sp = "pkg_hr.hr_prod_line_select";
            try
            {
                var obList = new List<HR_PROD_LINEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value =pHR_PROD_LINE_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value =pHR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_BLDNG_ID", Value =pHR_PROD_BLDNG_ID},
                     new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value =pHR_OFFICE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_PROD_LINEModel ob = new HR_PROD_LINEModel();
                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_NO"]);
                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);
                    ob.LINE_DESC_EN = (dr["LINE_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_DESC_EN"]);
                    ob.LINE_DESC_BN = (dr["LINE_DESC_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_DESC_BN"]);
                    ob.LK_GARM_TYPE_ID = (dr["LK_GARM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GARM_TYPE_ID"]);
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.HR_PROD_BLDNG_ID = (dr["HR_PROD_BLDNG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_BLDNG_ID"]);
                    
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.FLOOR_NO = (dr["FLOOR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_NO"]);
                    ob.FLOOR_CODE = (dr["FLOOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_CODE"]);
                    ob.FLOOR_DESC_EN = (dr["FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_EN"]);

                    ob.BLDNG_CODE = (dr["BLDNG_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLDNG_CODE"]);
                    ob.BLDNG_DESC_EN = (dr["BLDNG_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLDNG_DESC_EN"]);
                    ob.BLDNG_NO = (dr["BLDNG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLDNG_NO"]);

                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.COMP_DESC = (dr["COMP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_DESC"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.COMP_SNAME = (dr["COMP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_SNAME"]);

                    ob.OFFICE_CODE = (dr["OFFICE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_CODE"]);
                    ob.OFFICE_DESC = (dr["OFFICE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_DESC"]);
                    ob.OFFICE_NAME_EN = (dr["OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_EN"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public List<HR_PROD_LINEModel> getSewingProdDashBoard(string pHR_PROD_FLR_LST, DateTime? pPROD_DT)
        {
            string sp = "pkg_common.hr_prod_line_select";
            try
            {
                var obList = new List<HR_PROD_LINEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_LST", Value =pHR_PROD_FLR_LST},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_PROD_LINEModel ob = new HR_PROD_LINEModel();
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.FLOOR_NO = (dr["FLOOR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_NO"]);
                    ob.FLOOR_CODE = (dr["FLOOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_CODE"]);
                    ob.FLOOR_DESC_EN = (dr["FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_EN"]);

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value =ob.HR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value =pPROD_DT??DateTime.Now.Date},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, "pkg_hourly_prod.gmt_ln_load_plan_select");
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        GMT_LN_LOAD_PLANModel ob1 = new GMT_LN_LOAD_PLANModel();
                        ob1.GMT_LN_LOAD_PLAN_ID = (dr1["GMT_LN_LOAD_PLAN_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr1["GMT_LN_LOAD_PLAN_ID"]);
                        ob1.MC_ORDER_STYL_ID = (dr1["MC_ORDER_STYL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_ORDER_STYL_ID"]);
                        ob1.IS_EDITABLE = (dr1["GMT_LN_LOAD_PLAN_ID"] == DBNull.Value) ? true : false;
                        ob1.HR_PROD_LINE_ID = (dr1["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["HR_PROD_LINE_ID"]);
                        ob1.SEW_START_DT = (dr1["SEW_START_DT"] == DBNull.Value) ? DateTime.Today : Convert.ToDateTime(dr1["SEW_START_DT"]);
                        ob1.PROD_DT = (dr1["PROD_DT"] == DBNull.Value) ? DateTime.Today : Convert.ToDateTime(dr1["PROD_DT"]);
                        ob1.DAYS_RUN = (Convert.ToDateTime(ob1.PROD_DT) - Convert.ToDateTime(ob1.SEW_START_DT)).Days + 1;

                        ob1.UNIT_PRICE = (dr1["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["UNIT_PRICE"]);


                        if (dr1["HRLY_TGT_QTY"] != DBNull.Value)
                        {
                            ob1.HRLY_TGT_QTY = Convert.ToInt64(dr1["HRLY_TGT_QTY"]);
                        }


                        if (dr1["HRLY_TGT_EFF"] != DBNull.Value)
                        {
                            ob1.HRLY_TGT_EFF = Convert.ToDecimal(dr1["HRLY_TGT_EFF"]);
                        }

                        ob1.QTY_MOU_ID = (dr1["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["QTY_MOU_ID"]);


                        if (dr1["REQ_OPERATOR"] != DBNull.Value)
                        {
                            ob1.REQ_OPERATOR = Convert.ToInt64(dr1["REQ_OPERATOR"]);
                        }



                        if (dr1["REQ_HELPER"] != DBNull.Value)
                        {
                            ob1.REQ_HELPER = Convert.ToInt64(dr1["REQ_HELPER"]);
                        }


                        if (dr1["NO_OF_MC"] != DBNull.Value)
                        {
                            ob1.NO_OF_MC = Convert.ToInt64(dr1["NO_OF_MC"]);
                        }

                        if (dr1["USE_OPERATOR"] != DBNull.Value)
                        {
                            ob1.USE_OPERATOR = Convert.ToInt32(dr1["USE_OPERATOR"]);
                        }

                        if (dr1["USE_HELPER"] != DBNull.Value)
                        {
                            ob1.USE_HELPER = Convert.ToInt32(dr1["USE_HELPER"]);
                        }

                        if (dr1["CUR_OPERATOR"] != DBNull.Value)
                        {
                            ob1.CUR_OPERATOR = Convert.ToInt32(dr1["CUR_OPERATOR"]);
                        }

                        if (dr1["CUR_HELPER"] != DBNull.Value)
                        {
                            ob1.CUR_HELPER = Convert.ToInt32(dr1["CUR_HELPER"]);
                        }


                        if (dr1["RF_PFLT_RSN_TYPE_ID"] != DBNull.Value)
                        {
                            ob1.RF_PFLT_RSN_TYPE_ID = Convert.ToInt64(dr1["RF_PFLT_RSN_TYPE_ID"]);
                        }

                        ob1.RSN_TYPE_NAME_EN = (dr1["RSN_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["RSN_TYPE_NAME_EN"]);


                        ob1.REMARKS = (dr1["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["REMARKS"]);

                        ob1.HR_PROD_FLR_ID = (dr1["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["HR_PROD_FLR_ID"]);
                        ob1.LK_FLOOR_ID = (dr1["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["LK_FLOOR_ID"]);
                        ob1.LINE_NO = (dr1["LINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["LINE_NO"]);
                        ob1.LINE_CODE = (dr1["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["LINE_CODE"]);
                        ob1.LINE_DESC_EN = (dr1["LINE_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["LINE_DESC_EN"]);
                        ob1.HR_PROD_FLR_ID_SPAN = (dr1["HR_PROD_FLR_ID_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["HR_PROD_FLR_ID_SPAN"]);
                        ob1.HR_PROD_FLR_ID_SL = (dr1["HR_PROD_FLR_ID_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["HR_PROD_FLR_ID_SL"]);

                        ob1.HR_PROD_LINE_ID_SPAN = (dr1["HR_PROD_LINE_ID_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["HR_PROD_LINE_ID_SPAN"]);
                        ob1.HR_PROD_LINE_ID_SL = (dr1["HR_PROD_LINE_ID_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["HR_PROD_LINE_ID_SL"]);

                        ob1.FLOOR_NO = (dr1["FLOOR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["FLOOR_NO"]);
                        ob1.FLOOR_CODE = (dr1["FLOOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["FLOOR_CODE"]);
                        ob1.FLOOR_DESC_EN = (dr1["FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["FLOOR_DESC_EN"]);
                        ob1.ORDER_NO = (dr1["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ORDER_NO"]);
                        ob1.STYLE_NO = (dr1["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["STYLE_NO"]);

                        ob1.STYLE_NO_S = ob1.STYLE_NO.Count() > 8 ? "[.] " + ob1.STYLE_NO.Substring(ob1.STYLE_NO.Count() - 8) : ob1.STYLE_NO;
                        ob1.ORDER_NO_S = ob1.ORDER_NO.Count() > 8 ? "[.] " + ob1.ORDER_NO.Substring(ob1.ORDER_NO.Count() - 8) : ob1.ORDER_NO;

                        ob1.BYR_ACC_NAME_EN = (dr1["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["BYR_ACC_NAME_EN"]);
                        ob1.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        ob1.MC_BYR_ACC_ID = (dr1["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_BYR_ACC_ID"]);
                        ob1.MC_STYLE_D_ITEM_ID = (dr1["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_STYLE_D_ITEM_ID"]);
                        ob1.MC_ORDER_H_ID = (dr1["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_ORDER_H_ID"]);
                        ob1.CHK_GH_PRD = (dr1["CHK_GH_PRD"] == DBNull.Value) ? "" : Convert.ToString(dr1["CHK_GH_PRD"]);
                        ob1.SMV = (dr1["SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["SMV"]);

                        if (dr1["H1_PRD_QTY"] != DBNull.Value && ob1.CHK_GH_PRD.IndexOf("1") > -1)
                        {
                            ob1.H1_PRD_QTY = Convert.ToInt64(dr1["H1_PRD_QTY"]);
                        }
                        if (dr1["H2_PRD_QTY"] != DBNull.Value && ob1.CHK_GH_PRD.IndexOf("2") > -1)
                        {
                            ob1.H2_PRD_QTY = Convert.ToInt64(dr1["H2_PRD_QTY"]);
                        }
                        if (dr1["H3_PRD_QTY"] != DBNull.Value && ob1.CHK_GH_PRD.IndexOf("3") > -1)
                        {
                            ob1.H3_PRD_QTY = Convert.ToInt64(dr1["H3_PRD_QTY"]);
                        }
                        if (dr1["H4_PRD_QTY"] != DBNull.Value && ob1.CHK_GH_PRD.IndexOf("4") > -1)
                        {
                            ob1.H4_PRD_QTY = Convert.ToInt64(dr1["H4_PRD_QTY"]);
                        }

                        if (dr1["H5_PRD_QTY"] != DBNull.Value && ob1.CHK_GH_PRD.IndexOf("5") > -1)
                        {
                            ob1.H5_PRD_QTY = Convert.ToInt64(dr1["H5_PRD_QTY"]);
                        }
                        if (dr1["H6_PRD_QTY"] != DBNull.Value && ob1.CHK_GH_PRD.IndexOf("6") > -1)
                        {
                            ob1.H6_PRD_QTY = Convert.ToInt64(dr1["H6_PRD_QTY"]);
                        }
                        if (dr1["H7_PRD_QTY"] != DBNull.Value && ob1.CHK_GH_PRD.IndexOf("7") > -1)
                        {
                            ob1.H7_PRD_QTY = Convert.ToInt64(dr1["H7_PRD_QTY"]);
                        }
                        if (dr1["H8_PRD_QTY"] != DBNull.Value && ob1.CHK_GH_PRD.IndexOf("8") > -1)
                        {
                            ob1.H8_PRD_QTY = Convert.ToInt64(dr1["H8_PRD_QTY"]);
                        }



                        if (dr1["OT_TGT_HR"] != DBNull.Value)
                        {
                            ob1.OT_TGT_HR = Convert.ToInt64(dr1["OT_TGT_HR"]);
                        }
                        if (dr1["OT_TGT_QTY"] != DBNull.Value)
                        {
                            ob1.OT_TGT_QTY = Convert.ToInt64(dr1["OT_TGT_QTY"]);
                        }

                        if (dr1["OT_ACT_PRD_QTY"] != DBNull.Value)
                        {
                            ob1.OT_ACT_PRD_QTY = Convert.ToInt64(dr1["OT_ACT_PRD_QTY"]);
                        }

                        ob1.GMT_HRLY_PROD_ID = (dr1["GMT_HRLY_PROD_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr1["GMT_HRLY_PROD_ID"]);

                        ob1.CT_TGT_QTY = (dr1["CT_TGT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["CT_TGT_QTY"]);
                        ob1.CT_PRD_QTY = (dr1["CT_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["CT_PRD_QTY"]);



                        if (ob1.CT_TGT_QTY != 0)
                        {
                            ob1.CT_ACHV = Math.Round(Convert.ToDecimal(((ob1.CT_PRD_QTY / ob1.CT_TGT_QTY) * 100)), 1);
                        }
                        else
                        {
                            ob1.CT_ACHV = 0;
                        }

                        ob.TTL_CT_TGT_QTY += ob1.CT_TGT_QTY;
                        ob.TTL_CT_PRD_QTY += ob1.CT_PRD_QTY;


                        if (ob.TTL_CT_TGT_QTY != 0)
                        {
                            ob.TTL_CT_ACHV = Math.Round(Convert.ToDecimal(((ob.TTL_CT_PRD_QTY / ob.TTL_CT_TGT_QTY) * 100)), 1);
                        }
                        else
                        {
                            ob.TTL_CT_ACHV = 0;
                        }

                        ob1.PROD_LINE_LST = (dr1["PROD_LINE_LST"] == DBNull.Value) ? "" : Convert.ToString(dr1["PROD_LINE_LST"]);


                        ob.TTL_H1 += ob1.H1_PRD_QTY ?? 0;
                        ob.TTL_H2 += ob1.H2_PRD_QTY ?? 0;
                        ob.TTL_H3 += ob1.H3_PRD_QTY ?? 0;
                        ob.TTL_H4 += ob1.H4_PRD_QTY ?? 0;
                        ob.TTL_H5 += ob1.H5_PRD_QTY ?? 0;
                        ob.TTL_H6 += ob1.H6_PRD_QTY ?? 0;
                        ob.TTL_H7 += ob1.H7_PRD_QTY ?? 0;
                        ob.TTL_H8 += ob1.H8_PRD_QTY ?? 0;


                        ob.TTL_TARGET += ob1.HRLY_TGT_QTY ?? 0;

                        ob.TTL_OT_TARGET += ob1.OT_TGT_QTY ?? 0;

                        ob.TTL_OT_PROD += ob1.OT_ACT_PRD_QTY ?? 0;

                        ob.TTL_OT_HR += ob1.OT_TGT_HR ?? 0;

                        ob1.LN_TTL_TARGET = (dr1["LN_TTL_TARGET"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["LN_TTL_TARGET"]);

                        ob1.LN_TTL_CUR_TARGET = (dr1["LN_TTL_TARGET"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["LN_TTL_TARGET"]);

                        ob1.LN_TTL_PROD = (dr1["LN_TTL_PROD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["LN_TTL_PROD"]);

                        if (ob1.LN_TTL_CUR_TARGET != 0)
                        {
                            ob1.LN_ACHV = Math.Round(Convert.ToDecimal(((ob1.LN_TTL_PROD / ob1.LN_TTL_CUR_TARGET) * 100)), 1);
                        }
                        else
                        {
                            ob1.LN_ACHV = 0;
                        }

                        ob1.INPUT_MAN_MIN = (dr1["INPUT_MAN_MIN"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["INPUT_MAN_MIN"]);
                        ob1.OUTPUT_MAN_MIN = (dr1["OUTPUT_MAN_MIN"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["OUTPUT_MAN_MIN"]);

                        if (ob1.INPUT_MAN_MIN > 0)
                        {
                            ob1.LN_EFF = Math.Round(Convert.ToDecimal((ob1.OUTPUT_MAN_MIN / ob1.INPUT_MAN_MIN) * 100), 1);
                        }
                        else
                        {
                            ob1.LN_EFF = 0;
                        }


                        ob1.TTL_VALUE = Math.Round(ob1.LN_TTL_PROD * ob1.UNIT_PRICE, 2);



                        if (ob1.HR_PROD_LINE_ID_SL == 1)
                        {
                            ob.TTL_REQ_OP += ob1.REQ_OPERATOR ?? 0;
                            ob.TTL_REQ_HP += ob1.REQ_HELPER ?? 0;
                            ob.TTL_PRE_OP += ob1.CUR_OPERATOR ?? 0;
                            ob.TTL_PRE_HP += ob1.CUR_HELPER ?? 0;
                            ob.TTL_USE_OP += ob1.USE_OPERATOR;
                            ob.TTL_USE_HP += ob1.USE_HELPER;
                        }

                        ob.G_TTL_VALUE += ob1.TTL_VALUE;
                        ob.G_PROD += (long)ob1.LN_TTL_PROD;
                        ob.G_TARGET += (long)ob1.LN_TTL_TARGET;
                        ob.G_TARGET_CUR += (long)ob1.LN_TTL_CUR_TARGET;
                        ob.TTL_SMV += (decimal)ob1.SMV;

                        ob.INPUT_MAN_MIN += ob1.INPUT_MAN_MIN;
                        ob.OUTPUT_MAN_MIN += ob1.OUTPUT_MAN_MIN;


                        if (ob.G_TARGET_CUR != 0)
                        {
                            ob.G_LN_ACHV = (decimal)Math.Round(Convert.ToDecimal(((ob.G_PROD / ob.G_TARGET_CUR) * 100)), 1);
                        }
                        else
                        {
                            ob.G_LN_ACHV = 0;
                        }

                        if (ob.INPUT_MAN_MIN > 0)
                        {
                            ob.LN_EFF = Math.Round(Convert.ToDecimal((ob.OUTPUT_MAN_MIN / ob.INPUT_MAN_MIN) * 100), 1);
                        }
                        else
                        {
                            ob.LN_EFF = 0;
                        }




                        ob.items.Add(ob1);

                    }


                    var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID_LST", Value =ob.HR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value =pPROD_DT??DateTime.Now.Date},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, "pkg_gmt_fin.gmt_fin_prod_select");
                    foreach (DataRow dr2 in ds2.Tables[0].Rows)
                    {
                        GMT_FIN_PRODModel ob2 = new GMT_FIN_PRODModel();
                        ob2.GMT_FIN_PROD_ID = (dr2["GMT_FIN_PROD_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr2["GMT_FIN_PROD_ID"]);
                        ob2.PROD_DT = (dr2["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr2["PROD_DT"]);
                        ob2.HR_PROD_FLR_ID = (dr2["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["HR_PROD_FLR_ID"]);
                        ob2.RF_GMT_FP_TYPE_ID = (dr2["RF_GMT_FP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["RF_GMT_FP_TYPE_ID"]);

                        ob2.HR_PROD_FLR_ID_SPAN = (dr2["HR_PROD_FLR_ID_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["HR_PROD_FLR_ID_SPAN"]);
                        ob2.HR_PROD_FLR_ID_SL = (dr2["HR_PROD_FLR_ID_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["HR_PROD_FLR_ID_SL"]);

                        if (dr2["HRLY_TGT_QTY"] != DBNull.Value)
                        {
                            ob2.HRLY_TGT_QTY = Convert.ToInt64(dr2["HRLY_TGT_QTY"]);
                        }

                        if (dr2["OT_TGT_HR"] != DBNull.Value)
                        {
                            ob2.OT_TGT_HR = Convert.ToInt64(dr2["OT_TGT_HR"]);
                        }

                        if (dr2["OT_TGT_QTY"] != DBNull.Value)
                        {
                            ob2.OT_TGT_QTY = Convert.ToInt64(dr2["OT_TGT_QTY"]);
                        }
                        if (dr2["QTY_MOU_ID"] != DBNull.Value)
                        {
                            ob2.QTY_MOU_ID = Convert.ToInt64(dr2["QTY_MOU_ID"]);
                        }

                        if (dr2["H1_PRD_QTY"] != DBNull.Value)
                        {
                            ob2.H1_PRD_QTY = Convert.ToInt64(dr2["H1_PRD_QTY"]);
                        }

                        if (dr2["H2_PRD_QTY"] != DBNull.Value)
                        {
                            ob2.H2_PRD_QTY = Convert.ToInt64(dr2["H2_PRD_QTY"]);
                        }
                        if (dr2["H3_PRD_QTY"] != DBNull.Value)
                        {
                            ob2.H3_PRD_QTY = Convert.ToInt64(dr2["H3_PRD_QTY"]);
                        }
                        if (dr2["H4_PRD_QTY"] != DBNull.Value)
                        {
                            ob2.H4_PRD_QTY = Convert.ToInt64(dr2["H4_PRD_QTY"]);
                        }

                        if (dr2["H5_PRD_QTY"] != DBNull.Value)
                        {
                            ob2.H5_PRD_QTY = Convert.ToInt64(dr2["H5_PRD_QTY"]);
                        }

                        if (dr2["H6_PRD_QTY"] != DBNull.Value)
                        {
                            ob2.H6_PRD_QTY = Convert.ToInt64(dr2["H6_PRD_QTY"]);
                        }
                        if (dr2["H7_PRD_QTY"] != DBNull.Value)
                        {
                            ob2.H7_PRD_QTY = Convert.ToInt64(dr2["H7_PRD_QTY"]);
                        }

                        if (dr2["H8_PRD_QTY"] != DBNull.Value)
                        {
                            ob2.H8_PRD_QTY = Convert.ToInt64(dr2["H8_PRD_QTY"]);
                        }
                        if (dr2["OT_ACT_PRD_QTY"] != DBNull.Value)
                        {
                            ob2.OT_ACT_PRD_QTY = Convert.ToInt64(dr2["OT_ACT_PRD_QTY"]);
                        }

                        ob2.FLOOR_CODE = (dr2["FLOOR_CODE"] == DBNull.Value) ? String.Empty : Convert.ToString(dr2["FLOOR_CODE"]);
                        ob2.FLOOR_DESC_EN = (dr2["FLOOR_DESC_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr2["FLOOR_DESC_EN"]);
                        ob2.FLOOR_DESC_BN = (dr2["FLOOR_DESC_BN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr2["FLOOR_DESC_BN"]);
                        ob2.FP_TYPE_CODE = (dr2["FP_TYPE_CODE"] == DBNull.Value) ? String.Empty : Convert.ToString(dr2["FP_TYPE_CODE"]);
                        ob2.FP_TYPE_NAME_EN = (dr2["FP_TYPE_NAME_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr2["FP_TYPE_NAME_EN"]);
                        ob2.FP_TYPE_DESC = (dr2["FP_TYPE_DESC"] == DBNull.Value) ? String.Empty : Convert.ToString(dr2["FP_TYPE_DESC"]);
                        ob2.CUR_HOUR = ((dr2["H1_PRD_QTY"] == DBNull.Value) ? 0 : 1) + ((dr2["H2_PRD_QTY"] == DBNull.Value) ? 0 : 1) + ((dr2["H3_PRD_QTY"] == DBNull.Value) ? 0 : 1) + ((dr2["H4_PRD_QTY"] == DBNull.Value) ? 0 : 1) + ((dr2["H5_PRD_QTY"] == DBNull.Value) ? 0 : 1) + ((dr2["H6_PRD_QTY"] == DBNull.Value) ? 0 : 1) + ((dr2["H7_PRD_QTY"] == DBNull.Value) ? 0 : 1) + ((dr2["H8_PRD_QTY"] == DBNull.Value) ? 0 : 1);


                        ob2.CT_TGT_QTY = (dr2["CT_TGT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["CT_TGT_QTY"]);
                        ob2.CT_PRD_QTY = (dr2["CT_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["CT_PRD_QTY"]);



                        if (ob2.CT_TGT_QTY != 0)
                        {
                            ob2.CT_ACHV = Math.Round(Convert.ToDecimal(((ob2.CT_PRD_QTY / ob2.CT_TGT_QTY) * 100)), 1);
                        }
                        else
                        {
                            ob2.CT_ACHV = 0;
                        }


                        ob.TTL_H1_F += ob2.H1_PRD_QTY ?? 0;
                        ob.TTL_H2_F += ob2.H2_PRD_QTY ?? 0;
                        ob.TTL_H3_F += ob2.H3_PRD_QTY ?? 0;
                        ob.TTL_H4_F += ob2.H4_PRD_QTY ?? 0;
                        ob.TTL_H5_F += ob2.H5_PRD_QTY ?? 0;
                        ob.TTL_H6_F += ob2.H6_PRD_QTY ?? 0;
                        ob.TTL_H7_F += ob2.H7_PRD_QTY ?? 0;
                        ob.TTL_H8_F += ob2.H8_PRD_QTY ?? 0;

                        ob.TTL_TARGET_F += ob2.HRLY_TGT_QTY ?? 0;

                        ob.TTL_OT_TARGET_F += ob2.OT_TGT_QTY ?? 0;

                        ob.TTL_OT_PROD_F += ob2.OT_ACT_PRD_QTY ?? 0;

                        ob.TTL_OT_HR_F += ob2.OT_TGT_HR ?? 0;

                        ob2.LN_TTL_TARGET_F = (ob2.HRLY_TGT_QTY ?? 0) * ob2.CUR_HOUR + (ob2.OT_TGT_QTY ?? 0) * (ob2.OT_TGT_HR ?? 0);
                        ob2.LN_TTL_TARGET_F_CUR = (ob2.HRLY_TGT_QTY ?? 0) * ob2.CUR_HOUR + (ob2.OT_TGT_QTY ?? 0) * (ob2.OT_TGT_HR ?? 0);
                        ob2.LN_TTL_PROD_F = (ob2.H1_PRD_QTY ?? 0) + (ob2.H2_PRD_QTY ?? 0) + (ob2.H3_PRD_QTY ?? 0) + (ob2.H4_PRD_QTY ?? 0) + (ob2.H5_PRD_QTY ?? 0) + (ob2.H6_PRD_QTY ?? 0) + (ob2.H7_PRD_QTY ?? 0) + (ob2.H8_PRD_QTY ?? 0) + (ob2.OT_ACT_PRD_QTY ?? 0);

                        if (ob2.LN_TTL_TARGET_F_CUR != 0)
                        {
                            ob2.LN_ACHV_F = Math.Round(Convert.ToDecimal(((ob2.LN_TTL_PROD_F / ob2.LN_TTL_TARGET_F_CUR) * 100)), 1);
                        }
                        else
                        {
                            ob2.LN_ACHV_F = 0;
                        }


                        ob.G_PROD_F += (long)ob2.LN_TTL_PROD_F;
                        ob.G_TARGET_F += (long)ob2.LN_TTL_TARGET_F;

                        ob.MergeLine = ob.items.Where(b => b.PROD_LINE_LST != String.Empty).Select(o => o.PROD_LINE_LST).ToList();
                        ob.datas.Add(ob2);
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


        public List<HR_PROD_LINEModel> getFloorData()
        {
            string sp = "pkg_common.hr_prod_line_select";
            try
            {
                var obList = new List<HR_PROD_LINEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_PROD_LINEModel ob = new HR_PROD_LINEModel();
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.FLOOR_NO = (dr["FLOOR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_NO"]);

                    ob.TERGET_SETTING_FLAG = (dr["TERGET_SETTING_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TERGET_SETTING_FLAG"]);
                    ob.PROD_ENTRY_FLAG = (dr["PROD_ENTRY_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROD_ENTRY_FLAG"]);
                    ob.IS_ADMIN = (dr["IS_ADMIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ADMIN"]);

                    ob.FLOOR_CODE = (dr["FLOOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_CODE"]);
                    ob.FLOOR_DESC_EN = (dr["FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_EN"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public HR_PROD_LINEModel Select(long ID)
        {
            string sp = "Select_HR_PROD_LINE";
            try
            {
                var ob = new HR_PROD_LINEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_NO"]);
                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);
                    ob.LINE_DESC_EN = (dr["LINE_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_DESC_EN"]);
                    ob.LINE_DESC_BN = (dr["LINE_DESC_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_DESC_BN"]);
                    ob.LK_GARM_TYPE_ID = (dr["LK_GARM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GARM_TYPE_ID"]);
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long LK_FLOOR_ID_SL { get; set; }

        public long LK_FLOOR_ID_SPAN { get; set; }

        public string FLOOR_NO { get; set; }

        public string FLOOR_CODE { get; set; }

        public string FLOOR_DESC_EN { get; set; }

        public long TTL_MC { get; set; }

        public long TTL_TARGET { get; set; }

        public Int64 G_TARGET { get; set; }
        public Int64 G_PROD { get; set; }


        public long TTL_H1_F { get; set; }

        public long TTL_H2_F { get; set; }

        public long TTL_H3_F { get; set; }

        public long TTL_H4_F { get; set; }

        public long TTL_H5_F { get; set; }

        public long TTL_H6_F { get; set; }

        public long TTL_H7_F { get; set; }

        public long TTL_H8_F { get; set; }

        public long TTL_TARGET_F { get; set; }

        public long TTL_OT_TARGET_F { get; set; }

        public long TTL_OT_PROD_F { get; set; }

        public long TTL_OT_HR_F { get; set; }

        public long G_PROD_F { get; set; }

        public long G_TARGET_F { get; set; }

        public int TTL_USE_OP { get; set; }

        public int TTL_USE_HP { get; set; }

        public decimal G_TTL_VALUE { get; set; }

        public long G_TARGET_CUR { get; set; }

        public decimal G_LN_ACHV { get; set; }


        public decimal LN_EFF { get; set; }

        public decimal TTL_SMV { get; set; }

        public decimal INPUT_MAN_MIN { get; set; }

        public decimal OUTPUT_MAN_MIN { get; set; }

        public Int64 TTL_CT_PRD_QTY { get; set; }

        public Int64 TTL_CT_TGT_QTY { get; set; }

        public decimal TTL_CT_ACHV { get; set; }

        public string BLDNG_CODE { get; set; }

        public string BLDNG_DESC_EN { get; set; }

        public string BLDNG_NO { get; set; }

        public string COMP_CODE { get; set; }

        public string COMP_DESC { get; set; }

        public string COMP_NAME_EN { get; set; }

        public string COMP_SNAME { get; set; }

        public string OFFICE_CODE { get; set; }

        public string OFFICE_DESC { get; set; }

        public string OFFICE_NAME_EN { get; set; }

        public long HR_OFFICE_ID { get; set; }

        public long HR_PROD_BLDNG_ID { get; set; }
        public long? COUNT_GMT_CAT { get; set; }
        public long HR_COMPANY_ID { get; set; }

        public List<HR_PROD_LINEModel> getLineByProdType(Int64 pGMT_PRODUCT_TYP_ID, Int64 pHR_PROD_FLR_ID)
        {
            string sp = "pkg_common.hr_prod_line_select";
            try
            {
                var obList = new List<HR_PROD_LINEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = pHR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pGMT_PRODUCT_TYP_ID", Value = pGMT_PRODUCT_TYP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_PROD_LINEModel ob = new HR_PROD_LINEModel();

                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_NO"]);
                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);
                    ob.LINE_DESC_EN = (dr["LINE_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_DESC_EN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.COUNT_GMT_CAT = (dr["COUNT_GMT_CAT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["COUNT_GMT_CAT"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<HR_PROD_LINEModel> GetPlnRsurcAccessLnByFlrUsr(Int64 pSC_USER_ID, Int64 pHR_PROD_FLR_ID)
        {
            string sp = "pkg_common.hr_prod_line_select";
            try
            {
                var obList = new List<HR_PROD_LINEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = pHR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_PROD_LINEModel ob = new HR_PROD_LINEModel();

                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_NO"]);
                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);
                    ob.LINE_DESC_EN = (dr["LINE_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_DESC_EN"]);

                    ob.GMT_PLN_RSRC_ACCESS_ID = (dr["GMT_PLN_RSRC_ACCESS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_RSRC_ACCESS_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ACTIVE"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string IS_ADMIN { get; set; }

        public string PROD_ENTRY_FLAG { get; set; }

        public string TERGET_SETTING_FLAG { get; set; }
    }
}