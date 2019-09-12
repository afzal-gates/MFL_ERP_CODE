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
    public class BlkSmpModel
    {
        //public string BLK_SMP_DATA_CODE { get; set; }
        public string buyer { get; set; }
        public string order_no { get; set; }
        public string style_no { get; set; }
        public string work_style_no { get; set; }
        public string revision_no { get; set; }
        public DateTime? revision_date { get; set; }
        public string order_type { get; set; }
        public DateTime issue_date { get; set; }
        public DateTime shipment_date { get; set; }
        public string comment { get; set; }
        public List<BlkSmpDtlModel> details { get; set; }
        public List<BlkSmpCollarCuffModel> collarcuff_detail { get; set; }
        //public List<BlkSmpCollarCuffModel> cuff_detail { get; set; }



        public List<BlkSmpModel> GetOrderByBookingDate(DateTime? pBOOKING_DT)
        {
            string sp = "pkg_mc_fab_booking.mc_blk_fab_req_h_select";
            try
            {
                var obList = new List<BlkSmpModel>();
                
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pBOOKING_DT", Value =pBOOKING_DT},
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    BlkSmpModel ob = new BlkSmpModel();
                    //ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    //ob.BLK_SMP_DATA_CODE = (dr["BLK_SMP_DATA_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLK_SMP_DATA_CODE"]);
                    ob.buyer = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.order_no = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.style_no = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.work_style_no = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.revision_no = (dr["REVISION_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REVISION_NO"]);
                    if (dr["REVISION_DT"] != DBNull.Value)
                    { ob.revision_date = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]); }
                    ob.order_type = (dr["BOOKING_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BOOKING_TYPE"]);
                    ob.issue_date = (dr["BOOKING_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BOOKING_DT"]);
                    ob.shipment_date = (dr["SHIPMENT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIPMENT_DT"]);

                    var obDtlList = new List<BlkSmpDtlModel>();
                    var dsDtl = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                        new CommandParameter() {ParameterName = "pBOOKING_DT", Value =pBOOKING_DT},
                        new CommandParameter() {ParameterName = "pBLK_SMP_DATA_CODE", Value =Convert.ToString(dr["BLK_SMP_DATA_CODE"])},
                        new CommandParameter() {ParameterName = "pOption", Value =3006},
                        new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                     }, sp);
                    foreach (DataRow drDtl in dsDtl.Tables[0].Rows)
                    {
                        BlkSmpDtlModel obDtl = new BlkSmpDtlModel();

                        obDtl.bk_id = (drDtl["BLK_SMP_DATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drDtl["BLK_SMP_DATA_ID"]);
                        obDtl.section = (drDtl["GMT_PART"] == DBNull.Value) ? string.Empty : Convert.ToString(drDtl["GMT_PART"]);
                        obDtl.color = (drDtl["FAB_COLOR"] == DBNull.Value) ? string.Empty : Convert.ToString(drDtl["FAB_COLOR"]);
                        obDtl.fiber_composition = (drDtl["FIBER_COMP"] == DBNull.Value) ? string.Empty : Convert.ToString(drDtl["FIBER_COMP"]);
                        obDtl.machine_gauge = (drDtl["MACN_GG"] == DBNull.Value) ? string.Empty : Convert.ToString(drDtl["MACN_GG"]);
                        obDtl.gsm = (drDtl["GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(drDtl["GSM"]);
                        obDtl.dia = (drDtl["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(drDtl["FIN_DIA"]);
                        obDtl.order_qty = (drDtl["BOOK_QTY"] == DBNull.Value) ? string.Empty : Convert.ToString(drDtl["BOOK_QTY"]);
                        obDtl.process_loss = (drDtl["PLOSS_PCT"] == DBNull.Value) ? string.Empty : Convert.ToString(drDtl["PLOSS_PCT"]);
                        obDtl.fabric_type = (drDtl["FAB_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(drDtl["FAB_TYPE"]);
                        obDtl.note = "";

                        obDtlList.Add(obDtl);
                    }

                    var obCollarList = new List<BlkSmpCollarCuffModel>();
                    var dsCollar = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                        new CommandParameter() {ParameterName = "pBOOKING_DT", Value = pBOOKING_DT},
                        new CommandParameter() {ParameterName = "pBLK_SMP_DATA_CODE", Value = Convert.ToString(dr["BLK_SMP_DATA_CODE"])},
                        new CommandParameter() {ParameterName = "pBOOKING_TYPE", Value = ob.order_type},
                        new CommandParameter() {ParameterName = "pSTYLE_NO", Value = ob.style_no},
                        new CommandParameter() {ParameterName = "pORDER_NO", Value = ob.order_no},
                        new CommandParameter() {ParameterName = "pOption", Value = ob.order_type=="Bulk"?3007:3008},
                        new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                     }, sp);
                    foreach (DataRow drCollar in dsCollar.Tables[0].Rows)
                    {
                        BlkSmpCollarCuffModel obCollar = new BlkSmpCollarCuffModel();
                        obCollar.section = (drCollar["GARM_PART_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(drCollar["GARM_PART_CODE"]);
                        obCollar.size = (drCollar["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(drCollar["SIZE_CODE"]);
                        obCollar.measurement = (drCollar["MEASUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(drCollar["MEASUREMENT"]);
                        obCollar.pcs = (drCollar["PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(drCollar["PCS"]);

                        obCollarList.Add(obCollar);
                    }

                    //var obCuffList = new List<BlkSmpCollarCuffModel>();
                    //var dsCuff = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    //{
                    //    new CommandParameter() {ParameterName = "pBOOKING_DT", Value =pBOOKING_DT},
                    //    new CommandParameter() {ParameterName = "pBLK_SMP_DATA_CODE", Value =Convert.ToString(dr["BLK_SMP_DATA_CODE"])},
                    //    new CommandParameter() {ParameterName = "pBOOKING_TYPE", Value = ob.order_type},
                    //    new CommandParameter() {ParameterName = "pOption", Value =3008},
                    //    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                    // }, sp);
                    //foreach (DataRow drCuff in dsCuff.Tables[0].Rows)
                    //{
                    //    BlkSmpCollarCuffModel obCuff = new BlkSmpCollarCuffModel();
                    //    obCuff.section = (drCuff["GARM_PART_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(drCuff["GARM_PART_CODE"]);
                    //    obCuff.size = (drCuff["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(drCuff["SIZE_CODE"]);
                    //    obCuff.measurement = (drCuff["MEASUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(drCuff["MEASUREMENT"]);
                    //    obCuff.pcs = (drCuff["CUFF_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(drCuff["CUFF_PCS"]);

                    //    obCuffList.Add(obCuff);
                    //}

                    ob.details = obDtlList;
                    ob.collarcuff_detail = obCollarList;
                    //ob.cuff_detail = obCuffList;

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



    public class BlkSmpDtlModel
    {
        public Int64 bk_id { get; set; }
        public string section { get; set; }
        public string color { get; set; }
        public string fiber_composition { get; set; }
        public string machine_gauge { get; set; }
        public string gsm { get; set; }
        public string dia { get; set; }
        public string order_qty { get; set; }
        public string process_loss { get; set; }
        public string fabric_type { get; set; }
        public string note { get; set; }
    }

    public class BlkSmpCollarCuffModel
    {        
        public string section { get; set; }
        public string size { get; set; }
        public string measurement { get; set; }
        public Int64 pcs { get; set; }
    }

    //public class BlkSmpDtlCollarCuffModel
    //{
    //    public string section { get; set; }
    //    public string color { get; set; }
    //    public string fiber_composition { get; set; }
    //    public string machine_gauge { get; set; }
    //    public string gsm { get; set; }
    //    public string dia { get; set; }
    //    public string order_qty { get; set; }
    //    public string process_loss { get; set; }
    //    public string fabric_type { get; set; }
    //    public string note { get; set; }
    //}


}