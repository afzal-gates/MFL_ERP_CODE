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
    public class MC_BYR_ACCModel
    {
        public Int64 MC_BYR_ACC_ID { get; set; }
        public Int64 MC_BUYER_OFF_ID { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string BYR_ACC_NAME_BN { get; set; }
        public string BYR_ACC_SNAME { get; set; }
        public string BYR_ACC_DESC { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 MC_USR_BYRACC_ID { get; set; }
        public string LK_ORDER_TYPE_LST { get; set; }


        public string Save()
        {
            const string sp = "pkg_merchandising.mc_byr_acc_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_OFF_ID", Value = ob.MC_BUYER_OFF_ID},
                     new CommandParameter() {ParameterName = "pBYR_ACC_NAME_EN", Value = ob.BYR_ACC_NAME_EN},
                     new CommandParameter() {ParameterName = "pBYR_ACC_NAME_BN", Value = ob.BYR_ACC_NAME_BN},
                     new CommandParameter() {ParameterName = "pBYR_ACC_SNAME", Value = ob.BYR_ACC_SNAME},
                     new CommandParameter() {ParameterName = "pBYR_ACC_DESC", Value = ob.BYR_ACC_DESC},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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
                         i++ ;
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
            const string sp = "pkg_merchandising.mc_byr_acc_update";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_OFF_ID", Value = ob.MC_BUYER_OFF_ID},
                     new CommandParameter() {ParameterName = "pBYR_ACC_NAME_EN", Value = ob.BYR_ACC_NAME_EN},
                     new CommandParameter() {ParameterName = "pBYR_ACC_NAME_BN", Value = ob.BYR_ACC_NAME_BN},
                     new CommandParameter() {ParameterName = "pBYR_ACC_SNAME", Value = ob.BYR_ACC_SNAME},
                     new CommandParameter() {ParameterName = "pBYR_ACC_DESC", Value = ob.BYR_ACC_DESC},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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
                         i++ ;
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
            const string sp = "pkg_merchandising.mc_byr_acc_delete";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_OFF_ID", Value = ob.MC_BUYER_OFF_ID},
                     new CommandParameter() {ParameterName = "pBYR_ACC_NAME_EN", Value = ob.BYR_ACC_NAME_EN},
                     new CommandParameter() {ParameterName = "pBYR_ACC_NAME_BN", Value = ob.BYR_ACC_NAME_BN},
                     new CommandParameter() {ParameterName = "pBYR_ACC_SNAME", Value = ob.BYR_ACC_SNAME},
                     new CommandParameter() {ParameterName = "pBYR_ACC_DESC", Value = ob.BYR_ACC_DESC},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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
                         i++ ;
                 }
                 return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_BYR_ACCModel> SelectAll()
        {
            string sp = "pkg_merchandising.mc_byr_acc_select";
            try
            {
                var obList = new List<MC_BYR_ACCModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_BYR_ACCModel ob = new MC_BYR_ACCModel();
                            ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                            ob.MC_BUYER_OFF_ID = (dr["MC_BUYER_OFF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_OFF_ID"]);
                            ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                            ob.BYR_ACC_NAME_BN = (dr["BYR_ACC_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_BN"]);
                            ob.BYR_ACC_SNAME = (dr["BYR_ACC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_SNAME"]);
                            ob.BYR_ACC_DESC = (dr["BYR_ACC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_DESC"]);
                            ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
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

        public MC_BYR_ACCModel Select(Int64 ID)
        {
            string sp = "pkg_merchandising.mc_byr_acc_select";
            try
            {
                var ob = new MC_BYR_ACCModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                        ob.MC_BUYER_OFF_ID = (dr["MC_BUYER_OFF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_OFF_ID"]);
                        ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                        ob.BYR_ACC_NAME_BN = (dr["BYR_ACC_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_BN"]);
                        ob.BYR_ACC_SNAME = (dr["BYR_ACC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_SNAME"]);
                        ob.BYR_ACC_DESC = (dr["BYR_ACC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_DESC"]);
                        ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                        ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string submitBuyerAcc()
        {
          const string sp = "pkg_merchandising.mc_byr_acc_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_OFF_ID", Value = ob.MC_BUYER_OFF_ID},
                     new CommandParameter() {ParameterName = "pBYR_ACC_NAME_EN", Value = ob.BYR_ACC_NAME_EN},
                     new CommandParameter() {ParameterName = "pBYR_ACC_NAME_BN", Value = ob.BYR_ACC_NAME_BN},
                     new CommandParameter() {ParameterName = "pBYR_ACC_SNAME", Value = ob.BYR_ACC_SNAME},
                     new CommandParameter() {ParameterName = "pBYR_ACC_DESC", Value = ob.BYR_ACC_DESC},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1001},
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

        public object getBuyerAccDataByByerId(Int64 MC_BUYER_ID)
        {
            string sp = "pkg_merchandising.mc_byr_acc_select";
            try
            {
                var obList = new List<MC_BYR_ACCModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_OFF_ID", Value =MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        MC_BYR_ACCModel ob = new MC_BYR_ACCModel();
                        ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                        ob.MC_BUYER_OFF_ID = (dr["MC_BUYER_OFF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_OFF_ID"]);
                        ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                        ob.BYR_ACC_NAME_BN = (dr["BYR_ACC_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_BN"]);
                        ob.BYR_ACC_SNAME = (dr["BYR_ACC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_SNAME"]);
                        ob.BYR_ACC_DESC = (dr["BYR_ACC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_DESC"]);
                        ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                        ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                        obList.Add(ob);
                    }
                }
                else
                {
                    MC_BYR_ACCModel ob = new MC_BYR_ACCModel();
                    ob.MC_BYR_ACC_ID = -1;
                    ob.MC_BUYER_OFF_ID = MC_BUYER_ID;
                    ob.BYR_ACC_NAME_EN = "";
                    ob.BYR_ACC_NAME_BN = "";
                    ob.BYR_ACC_SNAME = "";
                    ob.BYR_ACC_DESC = "";
                    ob.DISPLAY_ORDER = 1;
                    ob.IS_ACTIVE = "Y";
                    obList.Add(ob);

                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<MC_BYR_ACCModel> getBuyerAccListByUserId(Int64 SC_USER_ID)
        {
            string sp = "pkg_merchandising.mc_byr_acc_select";
            try
            {
                var obList = new List<MC_BYR_ACCModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value =SC_USER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BYR_ACCModel ob = new MC_BYR_ACCModel();
                    ob.MC_USR_BYRACC_ID = (dr["MC_USR_BYRACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_USR_BYRACC_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_BUYER_OFF_ID = (dr["MC_BUYER_OFF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_OFF_ID"]);
                    
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.BYR_ACC_SNAME = (dr["BYR_ACC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_SNAME"]);
                    ob.BYR_ACC_DESC = (dr["BYR_ACC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_DESC"]);
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

        public List<MC_BYR_ACCModel> getByrAccsByUserId4Costing(Int64 SC_USER_ID)
        {
            string sp = "pkg_merchandising.mc_byr_acc_select";
            try
            {
                var obList = new List<MC_BYR_ACCModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value =SC_USER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3007},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BYR_ACCModel ob = new MC_BYR_ACCModel();
                  
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_BUYER_OFF_ID = (dr["MC_BUYER_OFF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_OFF_ID"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.BYR_ACC_SNAME = (dr["BYR_ACC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_SNAME"]);
                    ob.BYR_ACC_DESC = (dr["BYR_ACC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_DESC"]);
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


        public object getBuyerAccListByUserId()
        {
            string sp = "pkg_merchandising.mc_byr_acc_select";
            try
            {
                Int32 pSC_USER_ID = Convert.ToInt32(HttpContext.Current.Session["multiScUserId"]);
                var obList = new List<MC_BYR_ACCModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BYR_ACCModel ob = new MC_BYR_ACCModel();
                    ob.MC_USR_BYRACC_ID = (dr["MC_USR_BYRACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_USR_BYRACC_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.BYR_ACC_SNAME = (dr["BYR_ACC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_SNAME"]);
                    ob.BYR_ACC_DESC = (dr["BYR_ACC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_DESC"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.LK_ORDER_TYPE_LST = (dr["LK_ORDER_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_ORDER_TYPE_LST"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object getBuyerWiseAccByUserList(Int64 pMC_BUYER_ID)
        {
            string sp = "pkg_merchandising.mc_byr_acc_select";
            try
            {
                var obList = new List<MC_BYR_ACCModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BYR_ACCModel ob = new MC_BYR_ACCModel();
                    //ob.MC_USR_BYRACC_ID = (dr["MC_USR_BYRACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_USR_BYRACC_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.BYR_ACC_SNAME = (dr["BYR_ACC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_SNAME"]);
                    ob.BYR_ACC_DESC = (dr["BYR_ACC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_DESC"]);
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

        public object getBuyerAccListForLineLoadPlan()
        {
            string sp = "pkg_merchandising.mc_byr_acc_select";
            try
            {
                Int32 pSC_USER_ID = Convert.ToInt32(HttpContext.Current.Session["multiScUserId"]);
                var obList = new List<MC_BYR_ACCModel>();


                obList.Add(new MC_BYR_ACCModel()
                {
                    MC_BYR_ACC_ID = -1,
                    BYR_ACC_NAME_EN = "All",
                    BYR_ACC_SNAME = "All",
                    BYR_ACC_DESC = "All"

                });

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BYR_ACCModel ob = new MC_BYR_ACCModel();
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.BYR_ACC_SNAME = (dr["BYR_ACC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_SNAME"]);
                    ob.BYR_ACC_DESC = (dr["BYR_ACC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_DESC"]);
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