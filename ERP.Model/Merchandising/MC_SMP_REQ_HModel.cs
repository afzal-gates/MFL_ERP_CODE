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
    public class MC_SMP_REQ_HModel
    {
        public Int64? MC_SMP_REQ_H_ID { get; set; }
        [Required(ErrorMessage = "Please select company")]
        public Int64? HR_COMPANY_ID { get; set; }
        [Required(ErrorMessage = "Please select buyer a/c#")]
        public string MC_BYR_ACC_LST { get; set; }
        public string[] MC_BYR_ACC_ID_LIST { get; set; }

        public string SMP_REQ_NO { get; set; }
        [Required(ErrorMessage = "Please select requisition date")]
        public DateTime? SMP_REQ_DT { get; set; }
        public Int64? SMP_REQ_BY { get; set; }

        public Int64? MC_TNA_TASK_STATUS_ID { get; set; }
        public string TASK_STATUS_NAME { get; set; }
        public Int64? STATUS_ORDER { get; set; }
        public string ACTION_USER_TYPE { get; set; }


        //[Required(ErrorMessage = "Please select requisition to user")]
        public string SMP_REQ_TO { get; set; }
        public string[] SMP_REQ_TO_LIST { get; set; }
        //[Required(ErrorMessage = "Please select Attention to users")]
        public string SMP_REQ_ATTN { get; set; }
        public string[] SMP_REQ_ATTN_LIST { get; set; }

        public string REMARKS { get; set; }


        public Int64? PARENT_ID { get; set; }
        public Int64? REVISION_NO { get; set; }
        public string IS_ITEM_REV { get; set; }
        public string IS_FAB_REV { get; set; }
        public DateTime? REVISION_DT { get; set; }
        public string REV_REASON { get; set; }


        //public string REQ_TO_NAME_LST { get; set; }
        public string EMPLOYEE_PROFILE { get; set; }
        public string COMP_NAME_EN { get; set; }
        //public string SMP_REQ_TO_LST { get; set; }
        public string DTL_XML { get; set; }
        public string CFG_XML { get; set; }
        public string ASSORT_XML { get; set; }
        public string FAB_QTY_XML { get; set; }

        //[Required(ErrorMessage = "Please select requisition submit users")]
        //public List<Int64> SMP_REQ_TO_LIST { get; set; }

        public List<MC_BYR_ACCModel> MC_BYR_ACC_LIST { get; set; }
        public List<MC_BUYERModel> MC_BUYER_LIST { get; set; }
        public List<HrCompanyModel> HR_COMPANY_LIST { get; set; }
        //public string MC_SMP_REQ_D2_XML { get; set; }
        public string STYLE_NO_LST { get; set; }
        public string ORDER_NO_LST { get; set; }
        public string BYR_ACC_NAME_EN_LST { get; set; }
        public string REQ_TO_NAME_LST { get; set; }
        public string REQ_ATTN_NAME_LST { get; set; }
        public string EMAIL_TO_LST { get; set; }
        public Int64? MC_ORDER_H_ID { get; set; }
        public DateTime? SHIP_DT { get; set; }
        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public Int64? LK_STYL_DEV_TYP_ID { get; set; }
        public DateTime? ACTION_DATE { get; set; }
        public string SMP_REQ_TYPE { get; set; }
        public Int64? RF_SMPL_TYPE_ID { get; set; }
        private List<FAB_BKING_RPTModel> _fabBkingRpt = null;
        public List<FAB_BKING_RPTModel> fabBkingRpt
        {
            get
            {
                if (_fabBkingRpt == null)
                {
                    _fabBkingRpt = new List<FAB_BKING_RPTModel>();
                }
                return _fabBkingRpt;
            }
            set
            {
                _fabBkingRpt = value;
            }
        }


        public string BatchSaveRevise()
        {
            const string sp = "pkg_mc_fab_booking.mc_smp_req_h_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;

            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = ob.MC_SMP_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_LST", Value = ob.MC_BYR_ACC_LST},                     
                     new CommandParameter() {ParameterName = "pSMP_REQ_NO", Value = ob.SMP_REQ_NO},
                     new CommandParameter() {ParameterName = "pSMP_REQ_DT", Value = ob.SMP_REQ_DT},
                     new CommandParameter() {ParameterName = "pSMP_REQ_BY", Value = ob.SMP_REQ_BY},
                     new CommandParameter() {ParameterName = "pSMP_REQ_TO", Value = ob.SMP_REQ_TO},
                     new CommandParameter() {ParameterName = "pSMP_REQ_ATTN", Value = ob.SMP_REQ_ATTN},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},    
                     new CommandParameter() {ParameterName = "pLK_STYL_DEV_TYP_ID", Value = ob.LK_STYL_DEV_TYP_ID},
                     
                     new CommandParameter() {ParameterName = "pIS_ITEM_REV", Value = ob.IS_ITEM_REV},
                     new CommandParameter() {ParameterName = "pIS_FAB_REV", Value = ob.IS_FAB_REV},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                     
                     //new CommandParameter() {ParameterName = "pDTL_XML", Value = ob.DTL_XML.Replace("null", "")},
                     //new CommandParameter() {ParameterName = "pCFG_XML", Value = ob.CFG_XML.Replace("null", "")},
                     //new CommandParameter() {ParameterName = "pASSORT_XML", Value = ob.ASSORT_XML.Replace("null", "")},
                     //new CommandParameter() {ParameterName = "pFAB_QTY_XML", Value = ob.FAB_QTY_XML.Replace("null", "")},
                     new CommandParameter() {ParameterName = "pOption", Value = 1004},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID_RTN", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pSMP_REQ_NO_RTN", Value =500, Direction = ParameterDirection.Output}
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

        public string BatchSave()
        {
            const string sp = "pkg_mc_fab_booking.mc_smp_req_h_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;

            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = ob.MC_SMP_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_LST", Value = ob.MC_BYR_ACC_LST},                     
                     new CommandParameter() {ParameterName = "pSMP_REQ_NO", Value = ob.SMP_REQ_NO},
                     new CommandParameter() {ParameterName = "pSMP_REQ_DT", Value = ob.SMP_REQ_DT},
                     new CommandParameter() {ParameterName = "pSMP_REQ_BY", Value = ob.SMP_REQ_BY},
                     new CommandParameter() {ParameterName = "pSMP_REQ_TO", Value = ob.SMP_REQ_TO},
                     new CommandParameter() {ParameterName = "pSMP_REQ_ATTN", Value = ob.SMP_REQ_ATTN},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},  
                     new CommandParameter() {ParameterName = "pLK_STYL_DEV_TYP_ID", Value = ob.LK_STYL_DEV_TYP_ID},
                     
                     new CommandParameter() {ParameterName = "pIS_ITEM_REV", Value = ob.IS_ITEM_REV},
                     new CommandParameter() {ParameterName = "pIS_FAB_REV", Value = ob.IS_FAB_REV},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},

                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                     
                     new CommandParameter() {ParameterName = "pDTL_XML", Value = ob.DTL_XML.Replace("null", "")},
                     new CommandParameter() {ParameterName = "pCFG_XML", Value = ob.CFG_XML.Replace("null", "")},
                     new CommandParameter() {ParameterName = "pASSORT_XML", Value = ob.ASSORT_XML.Replace("null", "")},
                     new CommandParameter() {ParameterName = "pFAB_QTY_XML", Value = ob.FAB_QTY_XML.Replace("null", "")},
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID_RTN", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pSMP_REQ_NO_RTN", Value =500, Direction = ParameterDirection.Output}
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



        public List<MC_SMP_REQ_HModel> SelectAll()
        {
            string sp = "Select_MC_SMP_REQ_H";
            try
            {
                var obList = new List<MC_SMP_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SMP_REQ_HModel ob = new MC_SMP_REQ_HModel();
                    ob.MC_SMP_REQ_H_ID = (dr["MC_SMP_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);

                    ob.SMP_REQ_NO = (dr["SMP_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMP_REQ_NO"]);
                    ob.SMP_REQ_DT = (dr["SMP_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SMP_REQ_DT"]);
                    ob.SMP_REQ_BY = (dr["SMP_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SMP_REQ_BY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_SMP_REQ_HModel Select(Int64? pMC_SMP_REQ_H_ID, string pIS_EMAIL_NOTIF = null)
        {
            string sp = "pkg_mc_fab_booking.mc_smp_req_h_select";
            try
            {
                var ob = new MC_SMP_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = pMC_SMP_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pIS_EMAIL_NOTIF", Value =pIS_EMAIL_NOTIF},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_SMP_REQ_H_ID = (dr["MC_SMP_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.REQ_TO_NAME_LST = (dr["REQ_TO_NAME_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TO_NAME_LST"]);
                    ob.MC_BYR_ACC_LST = (dr["MC_BYR_ACC_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_BYR_ACC_LST"]);
                    if (ob.MC_BYR_ACC_LST != null)
                    { ob.MC_BYR_ACC_ID_LIST = ob.MC_BYR_ACC_LST.Split(','); }

                    ob.SMP_REQ_NO = (dr["SMP_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMP_REQ_NO"]);
                    ob.SMP_REQ_DT = (dr["SMP_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SMP_REQ_DT"]);
                    ob.SMP_REQ_BY = (dr["SMP_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SMP_REQ_BY"]);
                    ob.EMPLOYEE_PROFILE = (dr["EMPLOYEE_PROFILE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_PROFILE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.SMP_REQ_TO = (dr["SMP_REQ_TO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMP_REQ_TO"]);
                    if (ob.SMP_REQ_TO != null)
                    { ob.SMP_REQ_TO_LIST = ob.SMP_REQ_TO.Split(','); }
                    ob.SMP_REQ_ATTN = (dr["SMP_REQ_ATTN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMP_REQ_ATTN"]);
                    if (ob.SMP_REQ_ATTN != null)
                    { ob.SMP_REQ_ATTN_LIST = ob.SMP_REQ_ATTN.Split(','); }

                    ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                    ob.STATUS_ORDER = (dr["STATUS_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STATUS_ORDER"]);

                    ob.IS_ITEM_REV = (dr["IS_ITEM_REV"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ITEM_REV"]);
                    ob.IS_FAB_REV = (dr["IS_FAB_REV"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FAB_REV"]);

                    if (dr["REVISION_NO"] != DBNull.Value)
                    {
                        ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                        ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                        ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    }

                    var obCompanyList = new HrCompanyModel().CompanyListData();
                    ob.HR_COMPANY_LIST = (List<HrCompanyModel>)obCompanyList;

                    //var obBaList = new MC_BYR_ACCModel().getBuyerAccListByUserId();
                    //ob.MC_BYR_ACC_LIST = (List<MC_BYR_ACCModel>)obBaList;

                    //var obBuyerList = new MC_BUYERModel().BuyerByUserList();
                    //ob.MC_BUYER_LIST = (List<MC_BUYERModel>)obBuyerList;

                    //var obBuyerList = new MC_BUYERModel().BuyerByUserList().Where(m => m.MC_BYR_ACC_ID == ob.MC_BYR_ACC_ID);
                    //ob.MC_BUYER_LIST = obBuyerList.ToList();

                    ob.EMAIL_TO_LST = (dr["EMAIL_TO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_TO_LST"]);
                    ob.BYR_ACC_NAME_EN_LST = (dr["BYR_ACC_NAME_EN_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN_LST"]);
                    ob.STYLE_NO_LST = (dr["STYLE_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO_LST"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string MailSendSuccessfully4SmplProgAprvl(long? pMC_SMP_REQ_H_ID, string pIS_MAIL_SEND)
        {
            const string sp = "pkg_mc_fab_booking.mc_smp_req_h_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = pMC_SMP_REQ_H_ID},                    
                     new CommandParameter() {ParameterName = "pIS_MAIL_SEND", Value = pIS_MAIL_SEND},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                     
                     new CommandParameter() {ParameterName = "pOption", Value = 1005},
                     new CommandParameter() {ParameterName = "pMsg", Value =  500, Direction = ParameterDirection.Output}
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

        public List<MC_SMP_REQ_HModel> GetMailSendPendingListSmplProg()
        {
            string sp = "pkg_mc_fab_booking.mc_smp_req_h_select";
            try
            {
                var obList = new List<MC_SMP_REQ_HModel>();


                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3011},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SMP_REQ_HModel ob = new MC_SMP_REQ_HModel();
                    ob.MC_SMP_REQ_H_ID = (dr["MC_SMP_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_H_ID"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);

                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object BuyerAccWiseBookingList(int pageNumber, int pageSize, Int64? pMC_BYR_ACC_ID, Int64? pMC_SMP_REQ_H_ID, string pLK_ORDER_TYPE_LST, Int64? pMC_STYLE_H_EXT_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_smp_req_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<MC_SMP_REQ_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                    new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = pMC_SMP_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pLK_ORDER_TYPE_LST", Value = pLK_ORDER_TYPE_LST},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3004},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SMP_REQ_HModel ob = new MC_SMP_REQ_HModel();
                    ob.MC_SMP_REQ_H_ID = (dr["MC_SMP_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_H_ID"]);
                    //ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    //ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    //ob.MC_BYR_ACC_LST = (dr["MC_BYR_ACC_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_BYR_ACC_LST"]);

                    ob.STYLE_NO_LST = (dr["STYLE_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO_LST"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);

                    ob.SMP_REQ_NO = (dr["SMP_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMP_REQ_NO"]);
                    ob.SMP_REQ_DT = (dr["SMP_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SMP_REQ_DT"]);
                    if (dr["LK_STYL_DEV_TYP_ID"] != DBNull.Value)
                        ob.LK_STYL_DEV_TYP_ID = (dr["LK_STYL_DEV_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_TYP_ID"]);

                    //ob.SMP_REQ_BY = (dr["SMP_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SMP_REQ_BY"]);
                    //ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    //ob.SMP_REQ_TO = (dr["SMP_REQ_TO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMP_REQ_TO"]);

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    obList.Add(ob);
                }

                obj.total = vTotalRec;
                obj.data = obList;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object getSmpReqNotif(int pageNumber, int pageSize, string pSTYLE_NO, string pSMP_REQ_TYPE, string pBYR_ACC_NAME_EN, DateTime? pSMP_REQ_DT)
        {
            string sp = "pkg_mc_fab_booking.mc_smp_req_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<MC_SMP_REQ_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                    new CommandParameter() {ParameterName = "pageSize", Value = pageSize},                    
                    new CommandParameter() {ParameterName = "pSTYLE_NO", Value = pSTYLE_NO},
                    new CommandParameter() {ParameterName = "pSMP_REQ_TYPE", Value = pSMP_REQ_TYPE},
                    new CommandParameter() {ParameterName = "pBYR_ACC_NAME_EN", Value = pBYR_ACC_NAME_EN},
                    new CommandParameter() {ParameterName = "pSMP_REQ_DT", Value = pSMP_REQ_DT},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pOption", Value = 3005},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SMP_REQ_HModel ob = new MC_SMP_REQ_HModel();
                    ob.MC_SMP_REQ_H_ID = (dr["MC_SMP_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_H_ID"]);
                    ob.SMP_REQ_NO = (dr["SMP_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMP_REQ_NO"]);
                    ob.SMP_REQ_DT = (dr["SMP_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SMP_REQ_DT"]);
                    ob.ACTION_DATE = (dr["ACTION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACTION_DATE"]);
                    ob.STYLE_NO_LST = (dr["STYLE_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO_LST"]);
                    ob.BYR_ACC_NAME_EN_LST = (dr["BYR_ACC_NAME_EN_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN_LST"]);
                    ob.SMP_REQ_TYPE = (dr["SMP_REQ_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMP_REQ_TYPE"]);

                    if (dr["REVISION_NO"] != DBNull.Value)
                    {
                        ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                        ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                        ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    }

                    var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {                             
                             new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value =ob.MC_SMP_REQ_H_ID},
                             new CommandParameter() {ParameterName = "pOption", Value =3010},
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, sp);
                    foreach (DataRow dr2 in ds2.Tables[0].Rows)
                    {
                        FAB_BKING_RPTModel ob2 = new FAB_BKING_RPTModel();
                        ob2.FAB_BKING_ID = (dr2["FAB_BKING_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["FAB_BKING_ID"]);
                        ob2.REVISION_NO_NAME = (dr2["REVISION_NO_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["REVISION_NO_NAME"]);
                        if (dr2["REVISION_NO"] != DBNull.Value)
                            ob2.REVISION_NO = (dr2["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr2["REVISION_NO"]);

                        ob.fabBkingRpt.Add(ob2);
                    }


                    ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                    ob.TASK_STATUS_NAME = (dr["TASK_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TASK_STATUS_NAME"]);
                    ob.ACTION_USER_TYPE = (dr["ACTION_USER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTION_USER_TYPE"]);

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    obList.Add(ob);
                }

                obj.total = vTotalRec;
                obj.data = obList;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string SendToSample()
        {
            const string sp = "pkg_mc_fab_booking.mc_smp_req_h_batch_save";
            string jsonStr = "{";

            var ob = this;
            var i = 1;

            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = ob.MC_SMP_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pSMP_REQ_DT", Value = ob.SMP_REQ_DT},
                     
                     new CommandParameter() {ParameterName = "pIS_ITEM_REV", Value = ob.IS_ITEM_REV},
                     new CommandParameter() {ParameterName = "pIS_FAB_REV", Value = ob.IS_FAB_REV},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                    
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = 1001},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID_RTN", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pSMP_REQ_NO_RTN", Value =500, Direction = ParameterDirection.Output}
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

        public string AcceptSample()
        {
            const string sp = "pkg_mc_fab_booking.mc_smp_req_h_batch_save";
            string jsonStr = "{";

            var ob = this;
            var i = 1;

            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = ob.MC_SMP_REQ_H_ID},                                         
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = 1002},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID_RTN", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pSMP_REQ_NO_RTN", Value =500, Direction = ParameterDirection.Output}
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

        public MC_SMP_REQ_HModel getSmpHdr(Int64 pMC_SMP_REQ_H_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_smp_req_h_select";
            try
            {
                var ob = new MC_SMP_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value =pMC_SMP_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_SMP_REQ_H_ID = (dr["MC_SMP_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_H_ID"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.BYR_ACC_NAME_EN_LST = (dr["BYR_ACC_NAME_EN_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN_LST"]);

                    ob.SMP_REQ_NO = (dr["SMP_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMP_REQ_NO"]);
                    ob.SMP_REQ_DT = (dr["SMP_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SMP_REQ_DT"]);

                    ob.EMPLOYEE_PROFILE = (dr["EMPLOYEE_PROFILE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_PROFILE"]);
                    ob.REQ_TO_NAME_LST = (dr["REQ_TO_NAME_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TO_NAME_LST"]);
                    ob.REQ_ATTN_NAME_LST = (dr["REQ_ATTN_NAME_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_NAME_LST"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_WORK_STYLEModel> getOrdListByReqId(Int64 pMC_SMP_REQ_H_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_smp_req_h_select";
            try
            {
                var obList = new List<MC_WORK_STYLEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = pMC_SMP_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_WORK_STYLEModel ob = new MC_WORK_STYLEModel();
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);

                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_BYR_ACCModel> getByrAcListByReqId(Int64 pMC_SMP_REQ_H_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_smp_req_h_select";
            try
            {
                var obList = new List<MC_BYR_ACCModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = pMC_SMP_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3008},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BYR_ACCModel ob = new MC_BYR_ACCModel();
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SaveDevSmpAutoCreate()
        {
            const string sp = "pkg_mc_fab_booking.dev_sample_auto_create";
            string jsonStr = "{";
            var ob = this;
            var i = 1;

            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = ob.MC_SMP_REQ_H_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_LST", Value = ob.MC_BYR_ACC_LST}, 
                    new CommandParameter() {ParameterName = "pSMP_REQ_TO", Value = ob.SMP_REQ_TO},
                    new CommandParameter() {ParameterName = "pSMP_REQ_ATTN", Value = ob.SMP_REQ_ATTN},
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pLK_STYL_DEV_TYP_ID", Value = ob.LK_STYL_DEV_TYP_ID},
                    new CommandParameter() {ParameterName = "pRF_SMPL_TYPE_ID", Value = ob.RF_SMPL_TYPE_ID},
                    new CommandParameter() {ParameterName = "pSMP_REQ_DT", Value = ob.SMP_REQ_DT},
                    new CommandParameter() {ParameterName = "pSHIP_DT", Value = ob.SHIP_DT},
                    new CommandParameter() {ParameterName = "pSMP_REQ_BY", Value = HttpContext.Current.Session["multiLoginEmpId"]},

                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                                          
                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output},
                    new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID_RTN", Value =500, Direction = ParameterDirection.Output}
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




        public string GetSmplStyleCheck()
        {
            const string sp = "pkg_mc_fab_booking.get_smpl_style_check";
            string jsonStr = "{";
            var ob = this;
            var i = 1;

            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                                         
                     new CommandParameter() {ParameterName = "pDTL_XML", Value = ob.DTL_XML.Replace("null", "")},                     
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}                     
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


    public class MC_SMP_REQ_NotifDtlModel
    {
        public Int64? MC_SMP_REQ_H_ID { get; set; }
        public Int64? MC_BYR_ACC_ID { get; set; }
        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public string STYLE_NO { get; set; }
        public Int64? RF_SMPL_TYPE_ID { get; set; }
        public string SMPL_TYPE_NAME { get; set; }
        public Int64 RQD_QTY { get; set; }
        public Int64 SEW_QTY { get; set; }
        public Int64 DELV_QTY { get; set; }
        public Int64 REJECT_QTY { get; set; }
        public string QTY_MOU_CODE { get; set; }

        public DateTime? ACTION_DATE { get; set; }
        public int? MC_TNA_TASK_STATUS_ID { get; set; }
        public string TASK_STATUS_NAME { get; set; }
        public Int64? MC_SMP_REQ_D_ID { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string IS_BYR_FEEDBK { get; set; }



        public List<MC_SMP_REQ_NotifDtlModel> getSmpReqNotifDtl(Int64 pMC_SMP_REQ_H_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_smp_req_h_select";
            try
            {
                //var ob1 = this;
                var obList = new List<MC_SMP_REQ_NotifDtlModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = pMC_SMP_REQ_H_ID},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pOption", Value = 3007},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SMP_REQ_NotifDtlModel ob = new MC_SMP_REQ_NotifDtlModel();
                    ob.MC_SMP_REQ_H_ID = (dr["MC_SMP_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_H_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.RF_SMPL_TYPE_ID = (dr["RF_SMPL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SMPL_TYPE_ID"]);
                    ob.SMPL_TYPE_NAME = (dr["SMPL_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMPL_TYPE_NAME"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                    ob.DELV_QTY = (dr["DELV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DELV_QTY"]);
                    ob.QTY_MOU_CODE = (dr["QTY_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QTY_MOU_CODE"]);

                    ob.ACTION_DATE = (dr["ACTION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACTION_DATE"]);
                    ob.IS_BYR_FEEDBK = (dr["IS_BYR_FEEDBK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_BYR_FEEDBK"]);
                    ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_TNA_TASK_STATUS_ID"]);
                    ob.TASK_STATUS_NAME = (dr["TASK_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TASK_STATUS_NAME"]);
                    ob.MC_SMP_REQ_D_ID = (dr["MC_SMP_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_ID"]);

                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_SMP_REQ_NotifDtlModel> GetSmpNotifDtl4ByrFeedback(Int64 pMC_SMP_REQ_D_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_smp_req_h_select";
            try
            {
                //var ob1 = this;
                var obList = new List<MC_SMP_REQ_NotifDtlModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_ID", Value = pMC_SMP_REQ_D_ID},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pOption", Value = 3009},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SMP_REQ_NotifDtlModel ob = new MC_SMP_REQ_NotifDtlModel();
                    ob.MC_SMP_REQ_H_ID = (dr["MC_SMP_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_H_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.RF_SMPL_TYPE_ID = (dr["RF_SMPL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SMPL_TYPE_ID"]);
                    ob.SMPL_TYPE_NAME = (dr["SMPL_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SMPL_TYPE_NAME"]);

                    ob.ACTION_DATE = (dr["ACTION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACTION_DATE"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_TNA_TASK_STATUS_ID"]);
                    ob.TASK_STATUS_NAME = (dr["TASK_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TASK_STATUS_NAME"]);
                    ob.MC_SMP_REQ_D_ID = (dr["MC_SMP_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_ID"]);

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