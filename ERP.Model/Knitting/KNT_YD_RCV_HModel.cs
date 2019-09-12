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
    public class KNT_YD_RCV_HModel
    {
        public Int64 KNT_YD_RCV_H_ID { get; set; }
        public Int64 KNT_YD_PRG_H_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public string CL_WO_REF_NO { get; set; }
        public string CHALAN_NO { get; set; }
        public DateTime CHALAN_DT { get; set; }
        public string INVOICE_NO { get; set; }
        public string GATE_PASS_NO { get; set; }
        public string VEHICLE_NO { get; set; }
        public string CARRIER_NAME { get; set; }
        public string IS_FINALIZED { get; set; }
        public string REMARKS { get; set; }

        public string IS_REMARKS { get; set; }
        public string STORE_NAME_EN { get; set; }
        public string PRG_REF_NO { get; set; }
        public long MC_FAB_PROD_ORD_H_ID { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string ORDER_NO { get; set; }
        public string MC_FAB_PROD_ORD_H_LST { get; set; }
        public long? TR_PARTY_ID { get; set; }
        public string IS_TRANSFER { get; set; }
        public long RF_ACTN_STATUS_ID { get; set; }


        public string XML { get; set; }
        public string SUP_TRD_NAME_EN { get; set; }
        public long? KNT_SC_PRG_ISS_ID { get; set; }

        public string KNT_SC_FACTORY { get; set; }
        public string ACTN_STATUS_NAME { get; set; }
        public string NXT_ACTION_NAME { get; set; }
        public string EVENT_NAME { get; set; }

        public string IS_TEMP_CHALLAN { get; set; }
        public string IS_PASS { get; set; }

        private List<dynamic> _orders = null;
        public List<dynamic> orders
        {
            get
            {
                if (_orders == null)
                {
                    _orders = new List<dynamic>();
                }
                return _orders;
            }
            set
            {
                _orders = value;
            }
        }



        private List<KNT_YD_RCV_DModel> _details = null;
        public List<KNT_YD_RCV_DModel> details
        {
            get
            {
                if (_details == null)
                {
                    _details = new List<KNT_YD_RCV_DModel>();
                }
                return _details;
            }
            set
            {
                _details = value;
            }
        }

        public string Save()
        {
            const string sp = "PKG_KNT_YD_PRG.knt_yd_rcv_h_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_RCV_H_ID", Value = ob.KNT_YD_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value = ob.KNT_YD_PRG_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},

                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pTR_PARTY_ID", Value = ob.TR_PARTY_ID},
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_ISS_ID", Value = ob.KNT_SC_PRG_ISS_ID},
                     new CommandParameter() {ParameterName = "pIS_TEMP_CHALLAN", Value = ob.IS_TEMP_CHALLAN},
                     new CommandParameter() {ParameterName = "pIS_TRANSFER", Value = ob.IS_TRANSFER},

                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pIS_PASS", Value = ob.IS_PASS},
                                          
                     new CommandParameter() {ParameterName = "pCL_WO_REF_NO", Value = ob.CL_WO_REF_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pINVOICE_NO", Value = ob.INVOICE_NO},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_RECV_QTY_CHANGE", Value = ob.IS_RECV_QTY_CHANGE },

                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_KNT_YD_RCV_H_ID", Value =0, Direction = ParameterDirection.Output}
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

        public List<KNT_YD_RCV_HModel> SelectAll()
        {
            string sp = "Select_KNT_YD_RCV_H";
            try
            {
                var obList = new List<KNT_YD_RCV_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_RCV_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            KNT_YD_RCV_HModel ob = new KNT_YD_RCV_HModel();
                            ob.KNT_YD_RCV_H_ID = (dr["KNT_YD_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_RCV_H_ID"]);
                            ob.KNT_YD_PRG_H_ID = (dr["KNT_YD_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PRG_H_ID"]);
                            ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                            ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                            ob.CL_WO_REF_NO = (dr["CL_WO_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CL_WO_REF_NO"]);
                            ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                            ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                            ob.INVOICE_NO = (dr["INVOICE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INVOICE_NO"]);
                            ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                            ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                            ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                            ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
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



        public KNT_YD_RCV_HModel Select(Int64 pKNT_YD_RCV_H_ID)
        {
            string sp = "PKG_KNT_YD_PRG.knt_yd_rcv_h_select";
            try
            {
                var ob = new KNT_YD_RCV_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_RCV_H_ID", Value = pKNT_YD_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.KNT_YD_RCV_H_ID = (dr["KNT_YD_RCV_H_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["KNT_YD_RCV_H_ID"]);
                        ob.KNT_YD_PRG_H_ID = (dr["KNT_YD_PRG_H_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["KNT_YD_PRG_H_ID"]);

                        ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);

                        ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                        ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                        ob.CL_WO_REF_NO = (dr["CL_WO_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CL_WO_REF_NO"]);
                        ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                        ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                        ob.INVOICE_NO = (dr["INVOICE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INVOICE_NO"]);
                        ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                        ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                        ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                        ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                        ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                        ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                       
                        if (dr["TR_PARTY_ID"] != DBNull.Value)
                        {
                            ob.TR_PARTY_ID = Convert.ToInt64(dr["TR_PARTY_ID"]);
                        }

                        ob.IS_TRANSFER = (dr["IS_TRANSFER"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TRANSFER"]);
                        ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 63 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);

                        ob.RF_ACTN_STATUS_ID_INIT = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 63 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                        
                        if (dr["KNT_SC_PRG_ISS_ID"] != DBNull.Value)
                        {
                            ob.KNT_SC_PRG_ISS_ID = Convert.ToInt64(dr["KNT_SC_PRG_ISS_ID"]);
                        }


                        ob.IS_REMARKS = (dr["REMARKS"] == DBNull.Value) ? "N" : "Y";
                        ob.IS_RECV_QTY_CHANGE = "N";

                        ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                        ob.MC_FAB_PROD_ORD_H_LST = (dr["MC_FAB_PROD_ORD_H_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_FAB_PROD_ORD_H_LST"]);
                        ob.IS_TEMP_CHALLAN = (dr["IS_TEMP_CHALLAN"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_TEMP_CHALLAN"]);

                        ob.details = new KNT_YD_RCV_DModel().SelectAll(ob.KNT_YD_PRG_H_ID, ob.KNT_YD_RCV_H_ID, ob.MC_FAB_PROD_ORD_H_ID);
                          var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {
                                new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_LST", Value = ob.MC_FAB_PROD_ORD_H_LST},
                                new CommandParameter() {ParameterName = "pOption", Value =3004},
                                new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                            }, sp);
                            foreach (DataRow dr1 in ds1.Tables[0].Rows)
                            {
                                ob.orders.Add(new
                                {
                                    MC_FAB_PROD_ORD_H_ID =  (dr1["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr1["MC_FAB_PROD_ORD_H_ID"]),
                                    MC_ORDER_NO_LST = (dr1["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MC_ORDER_NO_LST"])
                                });
                            }
               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<dynamic> getOrderListByProgram(String pMC_FAB_PROD_ORD_H_LST)
        {
                      string sp = "PKG_KNT_YD_PRG.knt_yd_rcv_h_select";
                      try
                      {
                          var obList = new List<dynamic>();
                          OraDatabase db = new OraDatabase();

                          var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {
                                new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_LST", Value = pMC_FAB_PROD_ORD_H_LST},
                                new CommandParameter() {ParameterName = "pOption", Value =3004},
                                new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                            }, sp);
                          foreach (DataRow dr1 in ds1.Tables[0].Rows)
                          {
                              obList.Add(new
                              {
                                  MC_FAB_PROD_ORD_H_ID = (dr1["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr1["MC_FAB_PROD_ORD_H_ID"]),
                                  MC_ORDER_NO_LST = (dr1["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MC_ORDER_NO_LST"])
                              });
                          }
                          return obList;
                      }
                      catch (Exception ex)
                      {
                          throw ex;
                      }
        }

        public string STYLE_NO { get; set; }

        public long RF_ACTN_STATUS_ID_INIT { get; set; }

        public long RF_FAB_PROD_CAT_ID { get; set; }

        public string IS_RECV_QTY_CHANGE { get; set; }
    }
}