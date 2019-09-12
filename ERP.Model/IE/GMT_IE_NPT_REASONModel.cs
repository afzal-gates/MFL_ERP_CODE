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
    public class GMT_IE_NPT_REASONModel
    {
        public Int64 GMT_IE_NPT_REASON_ID { get; set; }
        public string REASON_CODE { get; set; }
        public string REASON_DESC_EN { get; set; }
        public string REASON_DESC_BN { get; set; }
        public string REASON_SNAME { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public string COLR_CODE { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        private string _label;
        private Int64 _value;
      
        public string label
        {
            get
            {
                _label = this.REASON_CODE + " : " + this.REASON_DESC_EN;
                return _label;
            }
        }
        public Int64 value
        {
            get
            {
                _value = this.GMT_IE_NPT_REASON_ID;
                return _value;
            }
        }

        public string Save()
         // pOption=1000=>General Form Save
        {
            const string sp ="PKG_GMT_IE.gmt_ie_npt_reason_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_IE_NPT_REASON_ID", Value = ob.GMT_IE_NPT_REASON_ID},
                     new CommandParameter() {ParameterName = "pREASON_CODE", Value = ob.REASON_CODE},
                     new CommandParameter() {ParameterName = "pREASON_DESC_EN", Value = ob.REASON_DESC_EN},
                     new CommandParameter() {ParameterName = "pREASON_DESC_BN", Value = ob.REASON_DESC_BN},
                     new CommandParameter() {ParameterName = "pREASON_SNAME", Value = ob.REASON_SNAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pCOLR_CODE", Value = ob.COLR_CODE},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_GMT_IE_NPT_REASON_ID", Value =0, Direction = ParameterDirection.Output}
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
        public List<GMT_IE_NPT_REASONModel> Query(int pOption)
        {
            string sp = "PKG_GMT_IE.gmt_ie_npt_reason_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_IE_NPT_REASONModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_IE_NPT_REASON_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_IE_NPT_REASONModel ob = new GMT_IE_NPT_REASONModel();
                            ob.GMT_IE_NPT_REASON_ID = (dr["GMT_IE_NPT_REASON_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_IE_NPT_REASON_ID"]);
                            ob.REASON_CODE = (dr["REASON_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_CODE"]);
                            ob.REASON_DESC_EN = (dr["REASON_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC_EN"]);
                            ob.REASON_DESC_BN = (dr["REASON_DESC_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC_BN"]);
                            ob.REASON_SNAME = (dr["REASON_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_SNAME"]);
                            ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                            ob.COLR_CODE = (dr["COLR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLR_CODE"]);
                            ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
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