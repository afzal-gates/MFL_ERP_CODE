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
    public class MC_ACCS_PO_D_ITEMModel
    {
        public Int64 MC_ACCS_PO_D_ITEM_ID { get; set; }
        public Int64 MC_ACCS_PO_H_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }

        public string ITEM_NAME_EN { get; set; }
        public Int64 BLK_BOM_ID { get; set; }
        public string XML { get; set; }
        public Int64 SL { get; set; }

        private List<MC_ACCS_PO_D_SPECModel> _ITEM_SPEC_LIST = null;

        public List<MC_ACCS_PO_D_SPECModel> ITEM_SPEC_LIST
        {
            get
            {
                if (_ITEM_SPEC_LIST == null)
                {
                    _ITEM_SPEC_LIST = new List<MC_ACCS_PO_D_SPECModel>();
                }
                return _ITEM_SPEC_LIST;
            }
            set
            {
                _ITEM_SPEC_LIST = value;
            }
        }

        private List<MC_ACCS_PO_TMPLT_CFGModel> _CONTROLS = null;
        public List<MC_ACCS_PO_TMPLT_CFGModel> CONTROLS
        {
            get
            {
                if (_CONTROLS == null)
                {
                    _CONTROLS = new List<MC_ACCS_PO_TMPLT_CFGModel>();
                }
                return _CONTROLS;
            }
            set
            {
                _CONTROLS = value;
            }
        }



        


        public string Submit()
        {
            const string sp = "pkg_acc_booking.mc_accs_po_d_item_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_D_ITEM_ID", Value = ob.MC_ACCS_PO_D_ITEM_ID },
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_H_ID", Value = ob.MC_ACCS_PO_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public List<MC_ACCS_PO_D_ITEMModel> getItemForList(Int64 pMC_ACCS_PO_H_ID)
        {
            string sp = "pkg_acc_booking.mc_accs_po_d_item_select";
            try
            {
                var obList = new List<MC_ACCS_PO_D_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_H_ID", Value = pMC_ACCS_PO_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_ACCS_PO_D_ITEMModel ob = new MC_ACCS_PO_D_ITEMModel();
                            ob.SL = (dr["SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SL"]);
                            ob.MC_ACCS_PO_D_ITEM_ID = (dr["MC_ACCS_PO_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ACCS_PO_D_ITEM_ID"]);
                            ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);                  
                            ob.BLK_BOM_ID = (dr["BLK_BOM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BLK_BOM_ID"]);
                            ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);


                            ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                            ob.MC_STYL_BGT_H_ID = (dr["MC_STYL_BGT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_BGT_H_ID"]);
                            ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);

                            ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);   
                  
                            ob.ITEM_SPEC_LIST = new MC_ACCS_PO_D_SPECModel().Query(ob.MC_ACCS_PO_D_ITEM_ID); //Conditional Check
                            ob.CONTROLS = new MC_ACCS_PO_TMPLT_CFGModel().getContrlsByItemNBuyer(ob.INV_ITEM_ID, ob.MC_STYLE_H_ID, ob.MC_BUYER_ID, ob.MC_STYL_BGT_H_ID, ob.MC_ACCS_PO_H_ID, ob.INV_ITEM_CAT_ID,"N"); //Conditional Check

                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_ACCS_PO_D_ITEMModel Select(long ID)
        {
            string sp = "Select_MC_ACCS_PO_D_ITEM";
            try
            {
                var ob = new MC_ACCS_PO_D_ITEMModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_D_ITEM_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_ACCS_PO_D_ITEM_ID = (dr["MC_ACCS_PO_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ACCS_PO_D_ITEM_ID"]);
                        ob.MC_ACCS_PO_H_ID = (dr["MC_ACCS_PO_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ACCS_PO_H_ID"]);
                        ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                        ob.ITEM_SPEC_LIST = new MC_ACCS_PO_D_SPECModel().Query(ob.MC_ACCS_PO_D_ITEM_ID);
               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long MC_BUYER_ID { get; set; }

        public long MC_STYL_BGT_H_ID { get; set; }

        public long INV_ITEM_CAT_ID { get; set; }

        public long MC_STYLE_H_ID { get; set; }
    }
}