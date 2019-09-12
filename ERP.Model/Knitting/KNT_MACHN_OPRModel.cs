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
    public class KNT_MACHN_OPRModel
    {
        public Int64 KNT_MACHN_OPR_ID { get; set; }
        public Int64 KNT_MACHINE_ID { get; set; }
        public Int64 OPERATOR_ID { get; set; }
        public Int64 HR_SCHEDULE_H_ID { get; set; }
        public DateTime EFFECT_FROM { get; set; }
        public DateTime EXPIRED_ON { get; set; }
        public string IS_ACTIVE { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string SCHEDULE_NAME_EN { get; set; }
        public string KNT_MACHINE_NO { get; set; }
        public long KNT_PIN_CODE { get; set; }


        private List<KNT_MACHN_OPRModel> _mc_list = null;
        public List<KNT_MACHN_OPRModel> mc_list
        {
            get
            {
                if (_mc_list == null)
                {
                    _mc_list = new List<KNT_MACHN_OPRModel>();
                }
                return _mc_list;
            }
            set
            {
                _mc_list = value;
            }
        }

        

        public string Save()
        {
            const string sp = "pkg_knit_common.knt_machn_opr_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pKNT_MACHN_OPR_ID", Value = ob.KNT_MACHN_OPR_ID}, 
                    new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = ob.KNT_MACHINE_ID},                     
                    new CommandParameter() {ParameterName = "pOPERATOR_ID", Value = ob.OPERATOR_ID},
                    new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                    new CommandParameter() {ParameterName = "pEFFECT_FROM", Value = ob.EFFECT_FROM},  
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
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

        public List<KNT_MACHN_OPRModel> getMCListBYOperator(long pOPERATOR_ID, Int64? pHR_SCHEDULE_H_ID = null)
        {
            string sp = "pkg_knit_common.knt_machn_opr_select";
            try
            {
                var obList = new List<KNT_MACHN_OPRModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOPERATOR_ID", Value =pOPERATOR_ID},
                     new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value =pHR_SCHEDULE_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_MACHN_OPRModel ob = new KNT_MACHN_OPRModel();
                    ob.KNT_MACHN_OPR_ID = (dr["KNT_MACHN_OPR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MACHN_OPR_ID"]);
                    ob.KNT_MACHINE_ID = (dr["KNT_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                    ob.KNT_MACHINE_NO = (dr["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_NO"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public object GetAssiPersonListByMachinId(long? pKNT_MACHINE_ID, Int64? pHR_SCHEDULE_H_ID = null, Int64? pOption = 3002, String pKNT_MACHINE_ID_LST = null)
        {
            string sp = "pkg_knit_common.knt_machn_opr_select";
            try
            {
                var obList = new List<KNT_MACHN_OPRModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value =pKNT_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value =pHR_SCHEDULE_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID_LST", Value =pKNT_MACHINE_ID_LST},
                     new CommandParameter() {ParameterName = "pOption", Value = pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_MACHN_OPRModel ob = new KNT_MACHN_OPRModel();

                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.KNT_PIN_CODE = (dr["KNT_PIN_CODE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PIN_CODE"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.SCHEDULE_NAME_EN = (dr["SCHEDULE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_EN"]);
                    ob.EFFECT_FROM = (dr["EFFECT_FROM"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EFFECT_FROM"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    
                    ob.KNT_MACHN_OPR_ID = (dr["KNT_MACHN_OPR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MACHN_OPR_ID"]);
                    ob.KNT_MACHINE_ID = (dr["KNT_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                    ob.KNT_MACHINE_NO = (dr["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_NO"]);

                    if (pOption == 3004)
                    {
                        ob.mc_list = getMCListBYOperator(ob.OPERATOR_ID, ob.HR_SCHEDULE_H_ID);
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
    }
}