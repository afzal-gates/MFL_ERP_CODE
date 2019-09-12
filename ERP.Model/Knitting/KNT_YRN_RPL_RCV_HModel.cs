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
    public class KNT_YRN_RPL_RCV_HModel
    {
        public Int64 KNT_YRN_RPL_RCV_H_ID { get; set; }
        public Int64 KNT_YRN_RPL_ISS_H_ID { get; set; }
        public Int64 ITEM_RCV_BY { get; set; }
        public string YRN_MRR_NO { get; set; }
        public DateTime YRN_MRR_DT { get; set; }
        public Int64 RF_PAY_MTHD_ID { get; set; }
        public string CHALAN_NO { get; set; }
        public DateTime CHALAN_DT { get; set; }
        public Int64 RF_CURRENCY_ID { get; set; }
        public Decimal LOC_CONV_RATE { get; set; }
        public string TRK_RCT_NO { get; set; }
        public string DRIVER_NAME { get; set; }
        public Int64 LK_RCV_STATUS_ID { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_KNT_YRN_RPL_RCV_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_RPL_RCV_H_ID", Value = ob.KNT_YRN_RPL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_RPL_ISS_H_ID", Value = ob.KNT_YRN_RPL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pITEM_RCV_BY", Value = ob.ITEM_RCV_BY},
                     new CommandParameter() {ParameterName = "pYRN_MRR_NO", Value = ob.YRN_MRR_NO},
                     new CommandParameter() {ParameterName = "pYRN_MRR_DT", Value = ob.YRN_MRR_DT},
                     new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLOC_CONV_RATE", Value = ob.LOC_CONV_RATE},
                     new CommandParameter() {ParameterName = "pTRK_RCT_NO", Value = ob.TRK_RCT_NO},
                     new CommandParameter() {ParameterName = "pDRIVER_NAME", Value = ob.DRIVER_NAME},
                     new CommandParameter() {ParameterName = "pLK_RCV_STATUS_ID", Value = ob.LK_RCV_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public string Update()
        {
            const string sp = "SP_KNT_YRN_RPL_RCV_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_RPL_RCV_H_ID", Value = ob.KNT_YRN_RPL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_RPL_ISS_H_ID", Value = ob.KNT_YRN_RPL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pITEM_RCV_BY", Value = ob.ITEM_RCV_BY},
                     new CommandParameter() {ParameterName = "pYRN_MRR_NO", Value = ob.YRN_MRR_NO},
                     new CommandParameter() {ParameterName = "pYRN_MRR_DT", Value = ob.YRN_MRR_DT},
                     new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLOC_CONV_RATE", Value = ob.LOC_CONV_RATE},
                     new CommandParameter() {ParameterName = "pTRK_RCT_NO", Value = ob.TRK_RCT_NO},
                     new CommandParameter() {ParameterName = "pDRIVER_NAME", Value = ob.DRIVER_NAME},
                     new CommandParameter() {ParameterName = "pLK_RCV_STATUS_ID", Value = ob.LK_RCV_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
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

        public string Delete()
        {
            const string sp = "SP_KNT_YRN_RPL_RCV_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_RPL_RCV_H_ID", Value = ob.KNT_YRN_RPL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_RPL_ISS_H_ID", Value = ob.KNT_YRN_RPL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pITEM_RCV_BY", Value = ob.ITEM_RCV_BY},
                     new CommandParameter() {ParameterName = "pYRN_MRR_NO", Value = ob.YRN_MRR_NO},
                     new CommandParameter() {ParameterName = "pYRN_MRR_DT", Value = ob.YRN_MRR_DT},
                     new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLOC_CONV_RATE", Value = ob.LOC_CONV_RATE},
                     new CommandParameter() {ParameterName = "pTRK_RCT_NO", Value = ob.TRK_RCT_NO},
                     new CommandParameter() {ParameterName = "pDRIVER_NAME", Value = ob.DRIVER_NAME},
                     new CommandParameter() {ParameterName = "pLK_RCV_STATUS_ID", Value = ob.LK_RCV_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
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

        public List<KNT_YRN_RPL_RCV_HModel> SelectAll()
        {
            string sp = "Select_KNT_YRN_RPL_RCV_H";
            try
            {
                var obList = new List<KNT_YRN_RPL_RCV_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_RPL_RCV_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_RPL_RCV_HModel ob = new KNT_YRN_RPL_RCV_HModel();
                    ob.KNT_YRN_RPL_RCV_H_ID = (dr["KNT_YRN_RPL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_RPL_RCV_H_ID"]);
                    ob.KNT_YRN_RPL_ISS_H_ID = (dr["KNT_YRN_RPL_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_RPL_ISS_H_ID"]);
                    ob.ITEM_RCV_BY = (dr["ITEM_RCV_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_RCV_BY"]);
                    ob.YRN_MRR_NO = (dr["YRN_MRR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_MRR_NO"]);
                    ob.YRN_MRR_DT = (dr["YRN_MRR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["YRN_MRR_DT"]);
                    ob.RF_PAY_MTHD_ID = (dr["RF_PAY_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAY_MTHD_ID"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.LOC_CONV_RATE = (dr["LOC_CONV_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOC_CONV_RATE"]);
                    ob.TRK_RCT_NO = (dr["TRK_RCT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRK_RCT_NO"]);
                    ob.DRIVER_NAME = (dr["DRIVER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DRIVER_NAME"]);
                    ob.LK_RCV_STATUS_ID = (dr["LK_RCV_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RCV_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_YRN_RPL_RCV_HModel Select(long ID)
        {
            string sp = "Select_KNT_YRN_RPL_RCV_H";
            try
            {
                var ob = new KNT_YRN_RPL_RCV_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_RPL_RCV_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_YRN_RPL_RCV_H_ID = (dr["KNT_YRN_RPL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_RPL_RCV_H_ID"]);
                    ob.KNT_YRN_RPL_ISS_H_ID = (dr["KNT_YRN_RPL_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_RPL_ISS_H_ID"]);
                    ob.ITEM_RCV_BY = (dr["ITEM_RCV_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_RCV_BY"]);
                    ob.YRN_MRR_NO = (dr["YRN_MRR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_MRR_NO"]);
                    ob.YRN_MRR_DT = (dr["YRN_MRR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["YRN_MRR_DT"]);
                    ob.RF_PAY_MTHD_ID = (dr["RF_PAY_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAY_MTHD_ID"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.LOC_CONV_RATE = (dr["LOC_CONV_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOC_CONV_RATE"]);
                    ob.TRK_RCT_NO = (dr["TRK_RCT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRK_RCT_NO"]);
                    ob.DRIVER_NAME = (dr["DRIVER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DRIVER_NAME"]);
                    ob.LK_RCV_STATUS_ID = (dr["LK_RCV_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RCV_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}