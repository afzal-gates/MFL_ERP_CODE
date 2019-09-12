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
    public class MC_SMP_REQ_D1Model
    {
        public Int64? MC_SMP_REQ_D1_ID { get; set; }
        public Int64? MC_SMP_REQ_D_ID { get; set; }
        public Int64? MC_STYLE_D_ITEM_ID { get; set; }
        public Int64? PARENT_ITEM_ID { get; set; }
        public Int64? MC_COLOR_ID { get; set; }
        public Int64? MC_SIZE_ID { get; set; }
        public Int64? RQD_QTY { get; set; }
        public Int64? QTY_MOU_ID { get; set; }

        public string ITEM_SNAME { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string SIZE_CODE { get; set; }
        public DateTime? TARGET_DT { get; set; }
        public Int64? MC_SMP_FEEDBK_ID { get; set; }
        


        public List<MC_SMP_REQ_D1Model> SelectAll()
        {
            string sp = "Select_MC_SMP_REQ_D1";
            try
            {
                var obList = new List<MC_SMP_REQ_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_D1_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SMP_REQ_D1Model ob = new MC_SMP_REQ_D1Model();
                    ob.MC_SMP_REQ_D1_ID = (dr["MC_SMP_REQ_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D1_ID"]);
                    ob.MC_SMP_REQ_D_ID = (dr["MC_SMP_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_ID"]);
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.PARENT_ITEM_ID = (dr["PARENT_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ITEM_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_SMP_REQ_D1Model Select(long ID)
        {
            string sp = "Select_MC_SMP_REQ_D1";
            try
            {
                var ob = new MC_SMP_REQ_D1Model();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_D1_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_SMP_REQ_D1_ID = (dr["MC_SMP_REQ_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D1_ID"]);
                    ob.MC_SMP_REQ_D_ID = (dr["MC_SMP_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_ID"]);
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.PARENT_ITEM_ID = (dr["PARENT_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ITEM_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);                    
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_SMP_REQ_D1Model> SampReqDtl1ListByHID(Int64 pMC_SMP_REQ_H_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_smp_req_d1_select";
            try
            {
                var obList = new List<MC_SMP_REQ_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = pMC_SMP_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SMP_REQ_D1Model ob = new MC_SMP_REQ_D1Model();
                    ob.MC_SMP_REQ_D1_ID = (dr["MC_SMP_REQ_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D1_ID"]);
                    ob.MC_SMP_REQ_D_ID = (dr["MC_SMP_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_ID"]);
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.PARENT_ITEM_ID = (dr["PARENT_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ITEM_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_SMP_REQ_D1Model> SampReqDtl1ListByDID(Int64 pMC_SMP_REQ_D_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_smp_req_d1_select";
            try
            {
                var obList = new List<MC_SMP_REQ_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_ID", Value = pMC_SMP_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SMP_REQ_D1Model ob = new MC_SMP_REQ_D1Model();

                    ob.MC_SMP_FEEDBK_ID = (dr["MC_SMP_FEEDBK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_FEEDBK_ID"]);
                    ob.MC_SMP_REQ_D1_ID = (dr["MC_SMP_REQ_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D1_ID"]);
                    ob.MC_SMP_REQ_D_ID = (dr["MC_SMP_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_ID"]);
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);

                    if (dr["MC_SIZE_ID"] != DBNull.Value)
                        ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);

                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);

                    ob.ITEM_SNAME = (dr["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SNAME"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SampChildItemVmModel> ItemsStyleListByDID(Int64 pMC_SMP_REQ_D_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_smp_req_d1_select";
            try
            {
                var obList = new List<SampChildItemVmModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_ID", Value = pMC_SMP_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SampChildItemVmModel ob = new SampChildItemVmModel();

                    ob.IS_SHOW_HDR = (dr["IS_SHOW_HDR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SHOW_HDR"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    if (dr["PARENT_ITEM_ID"] != DBNull.Value)
                    { ob.PARENT_ITEM_ID = (dr["PARENT_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ITEM_ID"]); }
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_SNAME = (dr["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SNAME"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.ORDER_QTY = (dr["ORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDER_QTY"]);

                    var obColorList = new List<SampColorVmModel>();                
                    var dsColor = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                         new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_ID", Value = pMC_SMP_REQ_D_ID},
                         new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value = ob.MC_STYLE_D_ITEM_ID},
                         new CommandParameter() {ParameterName = "pOption", Value = 3004},
                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                    }, sp);
                    foreach (DataRow drColor in dsColor.Tables[0].Rows)
                    {
                        SampColorVmModel obColor = new SampColorVmModel();
                        obColor.MC_COLOR_ID = (drColor["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drColor["MC_COLOR_ID"]);
                        obColor.COLOR_NAME_EN = (drColor["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drColor["COLOR_NAME_EN"]);
                        obColor.TOTAL_QTY = (drColor["TOTAL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drColor["TOTAL_QTY"]);

                        var obSizeList = new List<MC_SMP_REQ_D1Model>();                
                        var dsSize = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                             new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_ID", Value = pMC_SMP_REQ_D_ID},
                             new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value = ob.MC_STYLE_D_ITEM_ID},
                             new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = obColor.MC_COLOR_ID},
                             new CommandParameter() {ParameterName = "pOption", Value = 3005},
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                        }, sp);
                        foreach (DataRow drSize in dsSize.Tables[0].Rows)
                        {
                            MC_SMP_REQ_D1Model obSize = new MC_SMP_REQ_D1Model();
                            obSize.MC_SMP_REQ_D1_ID = (drSize["MC_SMP_REQ_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drSize["MC_SMP_REQ_D1_ID"]);
                            obSize.MC_SMP_REQ_D_ID = (drSize["MC_SMP_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drSize["MC_SMP_REQ_D_ID"]);
                            obSize.MC_STYLE_D_ITEM_ID = (drSize["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drSize["MC_STYLE_D_ITEM_ID"]);
                            obSize.PARENT_ITEM_ID = (drSize["PARENT_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drSize["PARENT_ITEM_ID"]);
                            if (drSize["TARGET_DT"] != DBNull.Value)
                            { obSize.TARGET_DT = (drSize["TARGET_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drSize["TARGET_DT"]); }

                            obSize.MC_COLOR_ID = (drSize["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drSize["MC_COLOR_ID"]);
                            obSize.MC_SIZE_ID = (drSize["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drSize["MC_SIZE_ID"]);
                            obSize.RQD_QTY = (drSize["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drSize["RQD_QTY"]);
                            obSize.QTY_MOU_ID = (drSize["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drSize["QTY_MOU_ID"]);
                            obSize.SIZE_CODE = (drSize["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(drSize["SIZE_CODE"]);

                            obSizeList.Add(obSize);
                        }
                        obColor.itemsSize = obSizeList;
                        obColorList.Add(obColor);
                    }

                    ob.itemsColor = obColorList;
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


    //public class SampItemStyleVmModel
    //{
    //    public Int64? MC_SMP_REQ_D1_ID { get; set; }
    //    public Int64? MC_SMP_REQ_D_ID { get; set; }
    //    public Int64? MC_STYLE_D_ITEM_ID { get; set; }
    //    public Int64? PARENT_ITEM_ID { get; set; }
    //    //public Int64? RQD_QTY { get; set; }
    //    public Int64? QTY_MOU_ID { get; set; }

    //    public string ITEM_NAME_EN { get; set; }
    //    public Int64? ORDER_QTY { get; set; }
    //    public List<SampChildItemVmModel> childItems { get; set; }
        
    //}

    public class SampChildItemVmModel
    {
        public Int64? MC_SMP_REQ_D1_ID { get; set; }
        public Int64? MC_SMP_REQ_D_ID { get; set; }
        public string MODEL_NO { get; set; }
        public Int64? MC_STYLE_D_ITEM_ID { get; set; }
        public Int64? PARENT_ITEM_ID { get; set; }
        public Int64? QTY_MOU_ID { get; set; }

        public string IS_SHOW_HDR { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string ITEM_SNAME { get; set; }
        public Int64? ORDER_QTY { get; set; }

        public List<SampColorVmModel> itemsColor { get; set; }
    }

    public class SampColorVmModel
    {
        public Int64? MC_COLOR_ID { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public Int64? TOTAL_QTY { get; set; }

        public List<MC_SMP_REQ_D1Model> itemsSize { get; set; }
    }


}