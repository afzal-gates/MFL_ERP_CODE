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
    public class KNT_SCO_CLCF_PRG_DModel
    {
        public Int64 KNT_SCO_CLCF_PRG_D_ID { get; set; }
        public Int64 KNT_SCO_CLCF_PRG_H_ID { get; set; }
        public Int64 MC_CLCF_ORD_REQ_ID { get; set; }
        public Int64 KNT_PLAN_D_ID { get; set; }
        public Int64 KNT_CLCF_STYL_ITEM_ID { get; set; }
        public string GREY_MEAS { get; set; }
        public Int64 RQD_QTY { get; set; }
        public Int64? CANCEL_QTY { get; set; }
        public Decimal PROD_RATE { get; set; }
        public Decimal BED_LN { get; set; }
        public Int64? LN_MOU_ID { get; set; }
        
        public string GARM_PART_NAME { get; set; }
        public Int64? MC_COLOR_ID { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string SIZE_CODE { get; set; }
        public string MESUREMENT { get; set; }
        public string KNT_YRN_LOT_LST { get; set; }
        public Int64? LEFT_REQ_QTY { get; set; }
        public Int64? RF_GARM_PART_ID { get; set; }
        public Int64? LK_FK_DGN_TYP_ID { get; set; }
        public string FK_DESIGN_TYPE_NAME { get; set; }
        public Int64? MC_FAB_PROC_RATE_ID { get; set; }
        public object YRN_DETAILS
        {
            get
            {
                return new { KNT_PLAN_D_ID = this.KNT_PLAN_D_ID, KNT_YRN_LOT_LST = this.KNT_YRN_LOT_LST ?? "" };
            }
        }




        public List<KNT_SCO_CLCF_PRG_DModel> GetScoCollarCuffDtl(Int64 pKNT_SCO_CLCF_PRG_H_ID, Int64? pMC_FAB_PROD_ORD_H_ID = null, string pRF_GARM_PART_LST = null)
        {
            string sp = "pkg_knit_subcontract.knt_sco_clcf_prg_h_select";
            try
            {
                var obList = new List<KNT_SCO_CLCF_PRG_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pKNT_SCO_CLCF_PRG_H_ID", Value = pKNT_SCO_CLCF_PRG_H_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_LST", Value = pRF_GARM_PART_LST},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    KNT_SCO_CLCF_PRG_DModel ob = new KNT_SCO_CLCF_PRG_DModel();
                //    ob.KNT_SCO_CLCF_PRG_D_ID = (dr["KNT_SCO_CLCF_PRG_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CLCF_PRG_D_ID"]);
                //    ob.KNT_SCO_CLCF_PRG_H_ID = (dr["KNT_SCO_CLCF_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CLCF_PRG_H_ID"]);
                //    ob.KNT_CLCF_STYL_ITEM_ID = (dr["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STYL_ITEM_ID"]);
                //    ob.GREY_MEAS = (dr["GREY_MEAS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GREY_MEAS"]);
                //    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                //    ob.PROD_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_RATE"]);
                //    ob.BED_LN = (dr["BED_LN"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BED_LN"]);
                //    ob.LN_MOU_ID = (dr["LN_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LN_MOU_ID"]);

                //    obList.Add(ob);
                //}

                var i = 0;
                //i = ds.Tables[0].Rows.Count;

                do
                {
                    KNT_SCO_CLCF_PRG_DModel ob = new KNT_SCO_CLCF_PRG_DModel();
                    ob.KNT_SCO_CLCF_PRG_D_ID = (ds.Tables[0].Rows[i]["KNT_SCO_CLCF_PRG_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["KNT_SCO_CLCF_PRG_D_ID"]);
                    ob.KNT_SCO_CLCF_PRG_H_ID = (ds.Tables[0].Rows[i]["KNT_SCO_CLCF_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["KNT_SCO_CLCF_PRG_H_ID"]);
                    ob.MC_CLCF_ORD_REQ_ID = (ds.Tables[0].Rows[i]["MC_CLCF_ORD_REQ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["MC_CLCF_ORD_REQ_ID"]);
                    ob.KNT_CLCF_STYL_ITEM_ID = (ds.Tables[0].Rows[i]["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["KNT_CLCF_STYL_ITEM_ID"]);
                    
                    ob.KNT_PLAN_D_ID = (ds.Tables[0].Rows[i]["KNT_PLAN_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["KNT_PLAN_D_ID"]);
                    ob.KNT_YRN_LOT_LST = (ds.Tables[0].Rows[i]["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : ds.Tables[0].Rows[i]["KNT_YRN_LOT_LST"].ToString();

                    ob.MC_FAB_PROD_ORD_H_ID = (ds.Tables[0].Rows[i]["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["MC_FAB_PROD_ORD_H_ID"]);

                    if (ob.KNT_PLAN_D_ID < 1)
                    {
                        var obYrnList = new KNT_CLCF_YARN_VM().GetCollarCuffYarnList(ob.MC_FAB_PROD_ORD_H_ID, Convert.ToInt64(ds.Tables[0].Rows[i]["RF_FAB_PROD_CAT_ID"]), Convert.ToInt64(ds.Tables[0].Rows[i]["MC_STYLE_H_EXT_ID"]), Convert.ToInt64(ds.Tables[0].Rows[i]["MC_COLOR_ID"]));
                        if (obYrnList.Count==1)
                        {                            
                            ob.KNT_PLAN_D_ID = Convert.ToInt64(obYrnList[0].KNT_PLAN_D_ID);
                            ob.KNT_YRN_LOT_LST = obYrnList[0].KNT_YRN_LOT_LST;
                        }
                    }



                    ob.GREY_MEAS = (ds.Tables[0].Rows[i]["GREY_MEAS"] == DBNull.Value) ? string.Empty : ds.Tables[0].Rows[i]["GREY_MEAS"].ToString();

                    ob.LEFT_REQ_QTY = (ds.Tables[0].Rows[i]["LEFT_REQ_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["LEFT_REQ_QTY"]);
                    ob.RQD_QTY = (ds.Tables[0].Rows[i]["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["RQD_QTY"]);
                    ob.CANCEL_QTY = (ds.Tables[0].Rows[i]["CANCEL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["CANCEL_QTY"]);
                    ob.PROD_RATE = (ds.Tables[0].Rows[i]["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["PROD_RATE"]);
                    ob.BED_LN = (ds.Tables[0].Rows[i]["BED_LN"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["BED_LN"]);
                    ob.LN_MOU_ID = (ds.Tables[0].Rows[i]["LN_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["LN_MOU_ID"]);
                    
                    ob.RF_GARM_PART_ID = (ds.Tables[0].Rows[i]["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["RF_GARM_PART_ID"]);
                    ob.GARM_PART_NAME = (ds.Tables[0].Rows[i]["GARM_PART_NAME"] == DBNull.Value) ? string.Empty : ds.Tables[0].Rows[i]["GARM_PART_NAME"].ToString();
                    ob.MC_COLOR_ID = (ds.Tables[0].Rows[i]["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["MC_COLOR_ID"]);
                    ob.COLOR_NAME_EN = (ds.Tables[0].Rows[i]["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : ds.Tables[0].Rows[i]["COLOR_NAME_EN"].ToString();
                    ob.SIZE_CODE = (ds.Tables[0].Rows[i]["SIZE_CODE"] == DBNull.Value) ? string.Empty : ds.Tables[0].Rows[i]["SIZE_CODE"].ToString();
                    ob.MESUREMENT = (ds.Tables[0].Rows[i]["MESUREMENT"] == DBNull.Value) ? string.Empty : ds.Tables[0].Rows[i]["MESUREMENT"].ToString();
                    
                    ob.LK_FK_DGN_TYP_ID = (ds.Tables[0].Rows[i]["LK_FK_DGN_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["LK_FK_DGN_TYP_ID"]);
                    ob.FK_DESIGN_TYPE_NAME = (ds.Tables[0].Rows[i]["FK_DESIGN_TYPE_NAME"] == DBNull.Value) ? string.Empty : ds.Tables[0].Rows[i]["FK_DESIGN_TYPE_NAME"].ToString();

                    if (ds.Tables[0].Rows[i]["MC_FAB_PROC_RATE_ID"] != DBNull.Value)
                        ob.MC_FAB_PROC_RATE_ID = (ds.Tables[0].Rows[i]["MC_FAB_PROC_RATE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["MC_FAB_PROC_RATE_ID"]);
                    
                    obList.Add(ob);

                    i++;
                } while (i < ds.Tables[0].Rows.Count);

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_SCO_CLCF_PRG_DModel Select(long ID)
        {
            string sp = "Select_KNT_SCO_CLCF_PRG_D";
            try
            {
                var ob = new KNT_SCO_CLCF_PRG_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CLCF_PRG_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_SCO_CLCF_PRG_D_ID = (dr["KNT_SCO_CLCF_PRG_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CLCF_PRG_D_ID"]);
                    ob.KNT_SCO_CLCF_PRG_H_ID = (dr["KNT_SCO_CLCF_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CLCF_PRG_H_ID"]);
                    ob.KNT_CLCF_STYL_ITEM_ID = (dr["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STYL_ITEM_ID"]);
                    ob.GREY_MEAS = (dr["GREY_MEAS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GREY_MEAS"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                    ob.PROD_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_RATE"]);
                    ob.BED_LN = (dr["BED_LN"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BED_LN"]);
                    ob.LN_MOU_ID = (dr["LN_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LN_MOU_ID"]);


                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }









        public long MC_FAB_PROD_ORD_H_ID { get; set; }
    }
}