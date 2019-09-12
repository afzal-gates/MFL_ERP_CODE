using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net.NetworkInformation;

namespace ERP.Model
{
    public class HR_TA_DEVICE_CFGModel //: IHR_TA_DEVICE_CFGModel
    {
        public Int64 HR_TA_DEVICE_CFG_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 HR_OFFICE_ID { get; set; }
        public string TA_DEVICE_CODE { get; set; }
        public Int64 MAT_PRODUCT_ID { get; set; }
        public Int64 LK_TA_DEV_TYPE_ID { get; set; }
        public string POSITION_DESC { get; set; }
        public string MODEL_NBR { get; set; }
        public string IP_NBR { get; set; }
        public string PORT_NBR { get; set; }
        public string IS_ON_OR_OFF { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        //public Int64 LAST_UPDATED_BY { get; set; }

        public List<HR_TA_DEVICE_CFGModel> DeviceListData(Int64 pLK_TA_DEV_TYPE_ID)
        {
            string sp = "pkg_attendance.hr_ta_device_cfg_select";
            Ping ping = new Ping();

            try
            {
                var obList = new List<HR_TA_DEVICE_CFGModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pLK_TA_DEV_TYPE_ID", Value = pLK_TA_DEV_TYPE_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3002},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_TA_DEVICE_CFGModel ob = new HR_TA_DEVICE_CFGModel();
                    ob.HR_TA_DEVICE_CFG_ID = (dr["HR_TA_DEVICE_CFG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_TA_DEVICE_CFG_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.TA_DEVICE_CODE = (dr["TA_DEVICE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_DEVICE_CODE"]);
                    ob.MAT_PRODUCT_ID = (dr["MAT_PRODUCT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAT_PRODUCT_ID"]);
                    ob.LK_TA_DEV_TYPE_ID = (dr["LK_TA_DEV_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_TA_DEV_TYPE_ID"]);
                    ob.POSITION_DESC = (dr["POSITION_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["POSITION_DESC"]);
                    ob.MODEL_NBR = (dr["MODEL_NBR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NBR"]);
                    ob.IP_NBR = (dr["IP_NBR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IP_NBR"]);
                    ob.PORT_NBR = (dr["PORT_NBR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PORT_NBR"]);
                    //ob.IS_ON_OR_OFF = (dr["IS_ON_OR_OFF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ON_OR_OFF"]);

                    if (ob.IP_NBR != "N/A")
                    {
                        PingReply pingresult = ping.Send(ob.IP_NBR);
                        if (pingresult.Status.ToString() == "Success")
                        { ob.IS_ON_OR_OFF = "Y"; }
                        else
                        { ob.IS_ON_OR_OFF = "N"; }
                    }

                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
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