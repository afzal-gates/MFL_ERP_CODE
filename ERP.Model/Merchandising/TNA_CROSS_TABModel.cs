using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.Globalization;

namespace ERP.Model
{
    public class TNA_CROSS_TABModel
    {
        public Int64 MC_ORDER_H_ID { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string WORK_STYLE_NO { get; set; }
        public string STYLE_DESC { get; set; }
        public string ORDER_NO { get; set; }
        public Int64 TOT_ORD_QTY { get; set; }
        public string MOU_CODE { get; set; }
        public DateTime ORD_CONF_DT { get; set; }
        public DateTime SHIP_DT { get; set; }
        public Int64 LEAD_TIME { get; set; }
        public string ORD_TYPE_NAME { get; set; }
        public Int64 IDX { get; set; }
        public string TEXT { get; set; }
        public string IS_P_A { get; set; }
        public string SE_TEXT { get; set; }

        public string TNA_001 { get; set; }
        public string TNA_002 { get; set; }
        public string TNA_003 { get; set; }
        public string TNA_004 { get; set; }
        public string TNA_005 { get; set; }
        public string TNA_006 { get; set; }
        public string TNA_007 { get; set; }
        public string TNA_008 { get; set; }
        public string TNA_009 { get; set; }
        public string TNA_010 { get; set; }
        public string TNA_011 { get; set; }
        public string TNA_012 { get; set; }
        public string TNA_013 { get; set; }
        public string TNA_014 { get; set; }
        public string TNA_015 { get; set; }
        public string TNA_016 { get; set; }
        public string TNA_017 { get; set; }
        public string TNA_018 { get; set; }
        public string TNA_019 { get; set; }
        public string TNA_020 { get; set; }
        public string TNA_021 { get; set; }
        public string TNA_022 { get; set; }
        public string TNA_023 { get; set; }
        public string TNA_024 { get; set; }
        public string TNA_025 { get; set; }
        public string TNA_026 { get; set; }
        public string TNA_027 { get; set; }
        public string TNA_028 { get; set; }
        public string TNA_029 { get; set; }
        public string TNA_030 { get; set; }
        public string TNA_031 { get; set; }
        public string TNA_032 { get; set; }
        public string TNA_033 { get; set; }
        public string TNA_034 { get; set; }
        public string TNA_035 { get; set; }
        public string TNA_036 { get; set; }
        public string TNA_037 { get; set; }
        public string TNA_038 { get; set; }
        public string TNA_039 { get; set; }
        public string TNA_040 { get; set; }
        public string TNA_041 { get; set; }
        public string TNA_042 { get; set; }
        public string TNA_043 { get; set; }
        public string TNA_044 { get; set; }
        public string TNA_045 { get; set; }
        public string TNA_046 { get; set; }
        public string TNA_047 { get; set; }
        public string TNA_048 { get; set; }
        public string TNA_049 { get; set; }
        public string TNA_050 { get; set; }
        public string TNA_051 { get; set; }
        public string TNA_052 { get; set; }
        public string TNA_053 { get; set; }
        public string TNA_054 { get; set; }
        public string TNA_055 { get; set; }
        public string TNA_056 { get; set; }
        public string TNA_057 { get; set; }
        public string TNA_058 { get; set; }
        public string TNA_059 { get; set; }
        public string TNA_060 { get; set; }
        public string TNA_061 { get; set; }
        public string TNA_062 { get; set; }


        public string TNA_063 { get; set; }
        public string TNA_064 { get; set; }
        public string TNA_065 { get; set; }
        public string TNA_066 { get; set; }
        public string TNA_067 { get; set; }
        public string TNA_068 { get; set; }
        public string TNA_069 { get; set; }
        public string TNA_070 { get; set; }
        public string TNA_071 { get; set; }
        public string TNA_072 { get; set; }
        public string TNA_073 { get; set; }
        public string TNA_074 { get; set; }
        public string TNA_075 { get; set; }
        public string TNA_076 { get; set; }
        public string TNA_077 { get; set; }
        public string TNA_078 { get; set; }
        public string TNA_079 { get; set; }
        public string TNA_080 { get; set; }
        public string TNA_081 { get; set; }
        public string TNA_082 { get; set; }
        public string TNA_083 { get; set; }
        public string TNA_084 { get; set; }
        public string TNA_085 { get; set; }
        public string TNA_086 { get; set; }
        public string TNA_087 { get; set; }
        public string TNA_088 { get; set; }
        public string TNA_089 { get; set; }
        public string TNA_090 { get; set; }
        public string TNA_091 { get; set; }
        public string TNA_092 { get; set; }
        public string TNA_093 { get; set; }
        public string TNA_094 { get; set; }
        public string TNA_095 { get; set; }
        public string TNA_096 { get; set; }
        public string TNA_097 { get; set; }
        public string TNA_098 { get; set; }

        public string TNA_099 { get; set; }
        public string TNA_100 { get; set; }
        public string TNA_101 { get; set; }
        public string TNA_102 { get; set; }
        public string TNA_103 { get; set; }
        public string TNA_104 { get; set; }
        public string TNA_105 { get; set; }
        public string GRP_ORDER { get; set; }

        private TNA_CROSS_TABModel getDateDiff(List<TNA_CROSS_TABModel> oblist,Int64 MC_ORDER_H_ID, TNA_CROSS_TABModel ob)
        {

            TNA_CROSS_TABModel result = new TNA_CROSS_TABModel();

            TNA_CROSS_TABModel ob1 = oblist.Where(x => x.MC_ORDER_H_ID == ob.MC_ORDER_H_ID && x.IS_P_A == "A").FirstOrDefault();
            TNA_CROSS_TABModel ob2 = oblist.Where(x => x.MC_ORDER_H_ID == ob.MC_ORDER_H_ID && x.IS_P_A == "P").FirstOrDefault();

            result.MC_ORDER_H_ID = ob.MC_ORDER_H_ID;
            result.BYR_ACC_NAME_EN = ob.BYR_ACC_NAME_EN;
            result.WORK_STYLE_NO = ob.WORK_STYLE_NO;
            result.STYLE_DESC = ob.STYLE_DESC;
            result.ORDER_NO = ob.ORDER_NO;
            result.TOT_ORD_QTY = ob.TOT_ORD_QTY;
            result.MOU_CODE = ob.MOU_CODE;
            result.ORD_CONF_DT = ob.ORD_CONF_DT;
            result.SHIP_DT = ob.SHIP_DT;
            result.LEAD_TIME = ob.LEAD_TIME;
            result.ORD_TYPE_NAME = ob.ORD_TYPE_NAME;
            result.IDX = ob.IDX;
            result.TEXT = ob.TEXT;
            result.IS_P_A = ob.IS_P_A;
            result.SE_TEXT = ob.SE_TEXT;
            result.GRP_ORDER = "Style# " + ob.WORK_STYLE_NO + ", Order# " + ob.ORDER_NO; 

            result.TNA_001 = String.IsNullOrEmpty(ob1.TNA_001) ?  (!string.IsNullOrEmpty(ob2.TNA_001) && Convert.ToDateTime(ob2.TNA_001) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_001)-DateTime.Now).Days.ToString():""  : (Convert.ToDateTime(ob2.TNA_001) - Convert.ToDateTime(ob1.TNA_001)).Days.ToString();
            result.TNA_002 = String.IsNullOrEmpty(ob1.TNA_002) ?  (!string.IsNullOrEmpty(ob2.TNA_002) && Convert.ToDateTime(ob2.TNA_002) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_002) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_002) - Convert.ToDateTime(ob1.TNA_002)).Days.ToString();
            result.TNA_003 = String.IsNullOrEmpty(ob1.TNA_003) ?  (!string.IsNullOrEmpty(ob2.TNA_003) && Convert.ToDateTime(ob2.TNA_003) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_003) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_003) - Convert.ToDateTime(ob1.TNA_003)).Days.ToString();
            result.TNA_004 = String.IsNullOrEmpty(ob1.TNA_004) ?  (!string.IsNullOrEmpty(ob2.TNA_004) && Convert.ToDateTime(ob2.TNA_004) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_004) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_004) - Convert.ToDateTime(ob1.TNA_004)).Days.ToString();
            result.TNA_005 = String.IsNullOrEmpty(ob1.TNA_005) ?  (!string.IsNullOrEmpty(ob2.TNA_005) && Convert.ToDateTime(ob2.TNA_005) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_005) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_005) - Convert.ToDateTime(ob1.TNA_005)).Days.ToString();
            result.TNA_006 = String.IsNullOrEmpty(ob1.TNA_006) ?  (!string.IsNullOrEmpty(ob2.TNA_006) && Convert.ToDateTime(ob2.TNA_006) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_006) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_006) - Convert.ToDateTime(ob1.TNA_006)).Days.ToString();
            result.TNA_007 = String.IsNullOrEmpty(ob1.TNA_007) ?  (!string.IsNullOrEmpty(ob2.TNA_007) && Convert.ToDateTime(ob2.TNA_007) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_007) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_007) - Convert.ToDateTime(ob1.TNA_007)).Days.ToString();
            result.TNA_008 = String.IsNullOrEmpty(ob1.TNA_008) ?  (!string.IsNullOrEmpty(ob2.TNA_008) && Convert.ToDateTime(ob2.TNA_008) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_008) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_008) - Convert.ToDateTime(ob1.TNA_008)).Days.ToString();
            result.TNA_009 = String.IsNullOrEmpty(ob1.TNA_009) ?  (!string.IsNullOrEmpty(ob2.TNA_009) && Convert.ToDateTime(ob2.TNA_009) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_009) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_009) - Convert.ToDateTime(ob1.TNA_009)).Days.ToString();
            result.TNA_010 = String.IsNullOrEmpty(ob1.TNA_010) ?  (!string.IsNullOrEmpty(ob2.TNA_010) && Convert.ToDateTime(ob2.TNA_010) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_010) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_010) - Convert.ToDateTime(ob1.TNA_010)).Days.ToString();
            result.TNA_011 = String.IsNullOrEmpty(ob1.TNA_011) ?  (!string.IsNullOrEmpty(ob2.TNA_011) && Convert.ToDateTime(ob2.TNA_011) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_011) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_011) - Convert.ToDateTime(ob1.TNA_011)).Days.ToString();
            result.TNA_012 = String.IsNullOrEmpty(ob1.TNA_012) ?  (!string.IsNullOrEmpty(ob2.TNA_012) && Convert.ToDateTime(ob2.TNA_012) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_012) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_012) - Convert.ToDateTime(ob1.TNA_012)).Days.ToString();
            result.TNA_013 = String.IsNullOrEmpty(ob1.TNA_013) ?  (!string.IsNullOrEmpty(ob2.TNA_013) && Convert.ToDateTime(ob2.TNA_013) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_013) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_013) - Convert.ToDateTime(ob1.TNA_013)).Days.ToString();
            result.TNA_014 = String.IsNullOrEmpty(ob1.TNA_014) ?  (!string.IsNullOrEmpty(ob2.TNA_014) && Convert.ToDateTime(ob2.TNA_014) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_014) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_014) - Convert.ToDateTime(ob1.TNA_014)).Days.ToString();
            result.TNA_015 = String.IsNullOrEmpty(ob1.TNA_015) ?  (!string.IsNullOrEmpty(ob2.TNA_015) && Convert.ToDateTime(ob2.TNA_015) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_015) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_015) - Convert.ToDateTime(ob1.TNA_015)).Days.ToString();
            result.TNA_016 = String.IsNullOrEmpty(ob1.TNA_016) ?  (!string.IsNullOrEmpty(ob2.TNA_016) && Convert.ToDateTime(ob2.TNA_016) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_016) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_016) - Convert.ToDateTime(ob1.TNA_016)).Days.ToString();
            result.TNA_017 = String.IsNullOrEmpty(ob1.TNA_017) ?  (!string.IsNullOrEmpty(ob2.TNA_017) && Convert.ToDateTime(ob2.TNA_017) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_017) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_017) - Convert.ToDateTime(ob1.TNA_017)).Days.ToString();
            result.TNA_018 = String.IsNullOrEmpty(ob1.TNA_018) ?  (!string.IsNullOrEmpty(ob2.TNA_018) && Convert.ToDateTime(ob2.TNA_018) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_018) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_018) - Convert.ToDateTime(ob1.TNA_018)).Days.ToString();
            result.TNA_019 = String.IsNullOrEmpty(ob1.TNA_019) ?  (!string.IsNullOrEmpty(ob2.TNA_019) && Convert.ToDateTime(ob2.TNA_019) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_019) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_019) - Convert.ToDateTime(ob1.TNA_019)).Days.ToString();
            result.TNA_020 = String.IsNullOrEmpty(ob1.TNA_020) ?  (!string.IsNullOrEmpty(ob2.TNA_020) && Convert.ToDateTime(ob2.TNA_020) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_020) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_020) - Convert.ToDateTime(ob1.TNA_020)).Days.ToString();
            result.TNA_021 = String.IsNullOrEmpty(ob1.TNA_021) ?  (!string.IsNullOrEmpty(ob2.TNA_021) && Convert.ToDateTime(ob2.TNA_021) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_021) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_021) - Convert.ToDateTime(ob1.TNA_021)).Days.ToString();
            result.TNA_022 = String.IsNullOrEmpty(ob1.TNA_022) ?  (!string.IsNullOrEmpty(ob2.TNA_022) && Convert.ToDateTime(ob2.TNA_022) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_022) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_022) - Convert.ToDateTime(ob1.TNA_022)).Days.ToString();
            result.TNA_023 = String.IsNullOrEmpty(ob1.TNA_023) ?  (!string.IsNullOrEmpty(ob2.TNA_023) && Convert.ToDateTime(ob2.TNA_023) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_023) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_023) - Convert.ToDateTime(ob1.TNA_023)).Days.ToString();
            result.TNA_024 = String.IsNullOrEmpty(ob1.TNA_024) ?  (!string.IsNullOrEmpty(ob2.TNA_024) && Convert.ToDateTime(ob2.TNA_024) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_024) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_024) - Convert.ToDateTime(ob1.TNA_024)).Days.ToString();
            result.TNA_025 = String.IsNullOrEmpty(ob1.TNA_025) ?  (!string.IsNullOrEmpty(ob2.TNA_025) && Convert.ToDateTime(ob2.TNA_025) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_025) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_025) - Convert.ToDateTime(ob1.TNA_025)).Days.ToString();
            result.TNA_026 = String.IsNullOrEmpty(ob1.TNA_026) ?  (!string.IsNullOrEmpty(ob2.TNA_026) && Convert.ToDateTime(ob2.TNA_026) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_026) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_026) - Convert.ToDateTime(ob1.TNA_026)).Days.ToString();
            result.TNA_027 = String.IsNullOrEmpty(ob1.TNA_027) ?  (!string.IsNullOrEmpty(ob2.TNA_027) && Convert.ToDateTime(ob2.TNA_027) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_027) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_027) - Convert.ToDateTime(ob1.TNA_027)).Days.ToString();
            result.TNA_028 = String.IsNullOrEmpty(ob1.TNA_028) ?  (!string.IsNullOrEmpty(ob2.TNA_028) && Convert.ToDateTime(ob2.TNA_028) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_028) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_028) - Convert.ToDateTime(ob1.TNA_028)).Days.ToString();
            result.TNA_029 = String.IsNullOrEmpty(ob1.TNA_029) ?  (!string.IsNullOrEmpty(ob2.TNA_029) && Convert.ToDateTime(ob2.TNA_029) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_029) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_029) - Convert.ToDateTime(ob1.TNA_029)).Days.ToString();
            result.TNA_030 = String.IsNullOrEmpty(ob1.TNA_030) ?  (!string.IsNullOrEmpty(ob2.TNA_030) && Convert.ToDateTime(ob2.TNA_030) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_030) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_030) - Convert.ToDateTime(ob1.TNA_030)).Days.ToString();
            result.TNA_031 = String.IsNullOrEmpty(ob1.TNA_031) ?  (!string.IsNullOrEmpty(ob2.TNA_031) && Convert.ToDateTime(ob2.TNA_031) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_031) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_031) - Convert.ToDateTime(ob1.TNA_031)).Days.ToString();
            result.TNA_032 = String.IsNullOrEmpty(ob1.TNA_032) ?  (!string.IsNullOrEmpty(ob2.TNA_032) && Convert.ToDateTime(ob2.TNA_032) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_032) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_032) - Convert.ToDateTime(ob1.TNA_032)).Days.ToString();
            result.TNA_033 = String.IsNullOrEmpty(ob1.TNA_033) ?  (!string.IsNullOrEmpty(ob2.TNA_033) && Convert.ToDateTime(ob2.TNA_033) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_033) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_033) - Convert.ToDateTime(ob1.TNA_033)).Days.ToString();
            result.TNA_034 = String.IsNullOrEmpty(ob1.TNA_034) ?  (!string.IsNullOrEmpty(ob2.TNA_034) && Convert.ToDateTime(ob2.TNA_034) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_034) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_034) - Convert.ToDateTime(ob1.TNA_034)).Days.ToString();
            result.TNA_035 = String.IsNullOrEmpty(ob1.TNA_035) ?  (!string.IsNullOrEmpty(ob2.TNA_035) && Convert.ToDateTime(ob2.TNA_035) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_035) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_035) - Convert.ToDateTime(ob1.TNA_035)).Days.ToString();
            result.TNA_036 = String.IsNullOrEmpty(ob1.TNA_036) ?  (!string.IsNullOrEmpty(ob2.TNA_036) && Convert.ToDateTime(ob2.TNA_036) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_036) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_036) - Convert.ToDateTime(ob1.TNA_036)).Days.ToString();
            result.TNA_037 = String.IsNullOrEmpty(ob1.TNA_037) ?  (!string.IsNullOrEmpty(ob2.TNA_037) && Convert.ToDateTime(ob2.TNA_037) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_037) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_037) - Convert.ToDateTime(ob1.TNA_037)).Days.ToString();
            result.TNA_038 = String.IsNullOrEmpty(ob1.TNA_038) ?  (!string.IsNullOrEmpty(ob2.TNA_038) && Convert.ToDateTime(ob2.TNA_038) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_038) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_038) - Convert.ToDateTime(ob1.TNA_038)).Days.ToString();
            result.TNA_039 = String.IsNullOrEmpty(ob1.TNA_039) ?  (!string.IsNullOrEmpty(ob2.TNA_039) && Convert.ToDateTime(ob2.TNA_039) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_039) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_039) - Convert.ToDateTime(ob1.TNA_039)).Days.ToString();
            result.TNA_040 = String.IsNullOrEmpty(ob1.TNA_040) ?  (!string.IsNullOrEmpty(ob2.TNA_040) && Convert.ToDateTime(ob2.TNA_040) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_040) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_040) - Convert.ToDateTime(ob1.TNA_040)).Days.ToString();
            result.TNA_041 = String.IsNullOrEmpty(ob1.TNA_041) ?  (!string.IsNullOrEmpty(ob2.TNA_041) && Convert.ToDateTime(ob2.TNA_041) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_041) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_041) - Convert.ToDateTime(ob1.TNA_041)).Days.ToString();
            result.TNA_042 = String.IsNullOrEmpty(ob1.TNA_042) ?  (!string.IsNullOrEmpty(ob2.TNA_042) && Convert.ToDateTime(ob2.TNA_042) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_042) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_042) - Convert.ToDateTime(ob1.TNA_042)).Days.ToString();
            result.TNA_043 = String.IsNullOrEmpty(ob1.TNA_043) ?  (!string.IsNullOrEmpty(ob2.TNA_043) && Convert.ToDateTime(ob2.TNA_043) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_043) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_043) - Convert.ToDateTime(ob1.TNA_043)).Days.ToString();
            result.TNA_044 = String.IsNullOrEmpty(ob1.TNA_044) ?  (!string.IsNullOrEmpty(ob2.TNA_044) && Convert.ToDateTime(ob2.TNA_044) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_044) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_044) - Convert.ToDateTime(ob1.TNA_044)).Days.ToString();
            result.TNA_045 = String.IsNullOrEmpty(ob1.TNA_045) ?  (!string.IsNullOrEmpty(ob2.TNA_045) && Convert.ToDateTime(ob2.TNA_045) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_045) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_045) - Convert.ToDateTime(ob1.TNA_045)).Days.ToString();
            result.TNA_046 = String.IsNullOrEmpty(ob1.TNA_046) ?  (!string.IsNullOrEmpty(ob2.TNA_046) && Convert.ToDateTime(ob2.TNA_046) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_046) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_046) - Convert.ToDateTime(ob1.TNA_046)).Days.ToString();
            result.TNA_047 = String.IsNullOrEmpty(ob1.TNA_047) ?  (!string.IsNullOrEmpty(ob2.TNA_047) && Convert.ToDateTime(ob2.TNA_047) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_047) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_047) - Convert.ToDateTime(ob1.TNA_047)).Days.ToString();
            result.TNA_048 = String.IsNullOrEmpty(ob1.TNA_048) ?  (!string.IsNullOrEmpty(ob2.TNA_048) && Convert.ToDateTime(ob2.TNA_048) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_048) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_048) - Convert.ToDateTime(ob1.TNA_048)).Days.ToString();
            result.TNA_049 = String.IsNullOrEmpty(ob1.TNA_049) ?  (!string.IsNullOrEmpty(ob2.TNA_049) && Convert.ToDateTime(ob2.TNA_049) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_049) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_049) - Convert.ToDateTime(ob1.TNA_049)).Days.ToString();
            result.TNA_050 = String.IsNullOrEmpty(ob1.TNA_050) ?  (!string.IsNullOrEmpty(ob2.TNA_050) && Convert.ToDateTime(ob2.TNA_050) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_050) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_050) - Convert.ToDateTime(ob1.TNA_050)).Days.ToString();
            result.TNA_051 = String.IsNullOrEmpty(ob1.TNA_051) ?  (!string.IsNullOrEmpty(ob2.TNA_051) && Convert.ToDateTime(ob2.TNA_051) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_051) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_051) - Convert.ToDateTime(ob1.TNA_051)).Days.ToString();
            result.TNA_052 = String.IsNullOrEmpty(ob1.TNA_052) ?  (!string.IsNullOrEmpty(ob2.TNA_052) && Convert.ToDateTime(ob2.TNA_052) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_052) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_052) - Convert.ToDateTime(ob1.TNA_052)).Days.ToString();
            result.TNA_053 = String.IsNullOrEmpty(ob1.TNA_053) ?  (!string.IsNullOrEmpty(ob2.TNA_053) && Convert.ToDateTime(ob2.TNA_053) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_053) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_053) - Convert.ToDateTime(ob1.TNA_053)).Days.ToString();
            result.TNA_054 = String.IsNullOrEmpty(ob1.TNA_054) ?  (!string.IsNullOrEmpty(ob2.TNA_054) && Convert.ToDateTime(ob2.TNA_054) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_054) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_054) - Convert.ToDateTime(ob1.TNA_054)).Days.ToString();
            result.TNA_055 = String.IsNullOrEmpty(ob1.TNA_055) ?  (!string.IsNullOrEmpty(ob2.TNA_055) && Convert.ToDateTime(ob2.TNA_055) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_055) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_055) - Convert.ToDateTime(ob1.TNA_055)).Days.ToString();
            result.TNA_056 = String.IsNullOrEmpty(ob1.TNA_056) ?  (!string.IsNullOrEmpty(ob2.TNA_056) && Convert.ToDateTime(ob2.TNA_056) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_056) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_056) - Convert.ToDateTime(ob1.TNA_056)).Days.ToString();
            result.TNA_057 = String.IsNullOrEmpty(ob1.TNA_057) ?  (!string.IsNullOrEmpty(ob2.TNA_057) && Convert.ToDateTime(ob2.TNA_057) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_057) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_057) - Convert.ToDateTime(ob1.TNA_057)).Days.ToString();
            result.TNA_058 = String.IsNullOrEmpty(ob1.TNA_058) ?  (!string.IsNullOrEmpty(ob2.TNA_058) && Convert.ToDateTime(ob2.TNA_058) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_058) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_058) - Convert.ToDateTime(ob1.TNA_058)).Days.ToString();
            result.TNA_059 = String.IsNullOrEmpty(ob1.TNA_059) ?  (!string.IsNullOrEmpty(ob2.TNA_059) && Convert.ToDateTime(ob2.TNA_059) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_059) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_059) - Convert.ToDateTime(ob1.TNA_059)).Days.ToString();
            result.TNA_060 = String.IsNullOrEmpty(ob1.TNA_060) ?  (!string.IsNullOrEmpty(ob2.TNA_060) && Convert.ToDateTime(ob2.TNA_060) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_060) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_060) - Convert.ToDateTime(ob1.TNA_060)).Days.ToString();
            result.TNA_061 = String.IsNullOrEmpty(ob1.TNA_061) ?  (!string.IsNullOrEmpty(ob2.TNA_061) && Convert.ToDateTime(ob2.TNA_061) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_061) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_061) - Convert.ToDateTime(ob1.TNA_061)).Days.ToString();
            result.TNA_062 = String.IsNullOrEmpty(ob1.TNA_062) ?  (!string.IsNullOrEmpty(ob2.TNA_062) && Convert.ToDateTime(ob2.TNA_062) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_062) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_062) - Convert.ToDateTime(ob1.TNA_062)).Days.ToString(); 
            result.TNA_063 = String.IsNullOrEmpty(ob1.TNA_063) ?  (!string.IsNullOrEmpty(ob2.TNA_063) && Convert.ToDateTime(ob2.TNA_063) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_063) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_063) - Convert.ToDateTime(ob1.TNA_063)).Days.ToString();
            result.TNA_064 = String.IsNullOrEmpty(ob1.TNA_064) ?  (!string.IsNullOrEmpty(ob2.TNA_064) && Convert.ToDateTime(ob2.TNA_064) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_064) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_064) - Convert.ToDateTime(ob1.TNA_064)).Days.ToString();
            result.TNA_065 = String.IsNullOrEmpty(ob1.TNA_065) ? (!string.IsNullOrEmpty(ob2.TNA_065) && Convert.ToDateTime(ob2.TNA_065) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_065) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_065) - Convert.ToDateTime(ob1.TNA_065)).Days.ToString();
            result.TNA_066 = String.IsNullOrEmpty(ob1.TNA_066) ? (!string.IsNullOrEmpty(ob2.TNA_066) && Convert.ToDateTime(ob2.TNA_066) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_066) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_066) - Convert.ToDateTime(ob1.TNA_066)).Days.ToString();
            result.TNA_067 = String.IsNullOrEmpty(ob1.TNA_067) ? (!string.IsNullOrEmpty(ob2.TNA_067) && Convert.ToDateTime(ob2.TNA_067) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_067) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_067) - Convert.ToDateTime(ob1.TNA_067)).Days.ToString();
            result.TNA_068 = String.IsNullOrEmpty(ob1.TNA_068) ? (!string.IsNullOrEmpty(ob2.TNA_068) && Convert.ToDateTime(ob2.TNA_068) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_068) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_068) - Convert.ToDateTime(ob1.TNA_068)).Days.ToString();
            result.TNA_069 = String.IsNullOrEmpty(ob1.TNA_069) ? (!string.IsNullOrEmpty(ob2.TNA_069) && Convert.ToDateTime(ob2.TNA_069) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_069) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_069) - Convert.ToDateTime(ob1.TNA_069)).Days.ToString();
            result.TNA_070 = String.IsNullOrEmpty(ob1.TNA_070) ? (!string.IsNullOrEmpty(ob2.TNA_070) && Convert.ToDateTime(ob2.TNA_070) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_070) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_070) - Convert.ToDateTime(ob1.TNA_070)).Days.ToString();
            result.TNA_071 = String.IsNullOrEmpty(ob1.TNA_071) ? (!string.IsNullOrEmpty(ob2.TNA_071) && Convert.ToDateTime(ob2.TNA_071) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_071) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_071) - Convert.ToDateTime(ob1.TNA_071)).Days.ToString();
            result.TNA_072 = String.IsNullOrEmpty(ob1.TNA_072) ? (!string.IsNullOrEmpty(ob2.TNA_072) && Convert.ToDateTime(ob2.TNA_072) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_072) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_072) - Convert.ToDateTime(ob1.TNA_072)).Days.ToString();
            result.TNA_073 = String.IsNullOrEmpty(ob1.TNA_073) ? (!string.IsNullOrEmpty(ob2.TNA_073) && Convert.ToDateTime(ob2.TNA_073) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_073) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_073) - Convert.ToDateTime(ob1.TNA_073)).Days.ToString();
            result.TNA_074 = String.IsNullOrEmpty(ob1.TNA_074) ? (!string.IsNullOrEmpty(ob2.TNA_074) && Convert.ToDateTime(ob2.TNA_074) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_074) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_074) - Convert.ToDateTime(ob1.TNA_074)).Days.ToString();
            result.TNA_075 = String.IsNullOrEmpty(ob1.TNA_075) ? (!string.IsNullOrEmpty(ob2.TNA_075) && Convert.ToDateTime(ob2.TNA_075) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_075) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_075) - Convert.ToDateTime(ob1.TNA_075)).Days.ToString();
            result.TNA_076 = String.IsNullOrEmpty(ob1.TNA_076) ? (!string.IsNullOrEmpty(ob2.TNA_076) && Convert.ToDateTime(ob2.TNA_076) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_076) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_076) - Convert.ToDateTime(ob1.TNA_076)).Days.ToString();
            result.TNA_077 = String.IsNullOrEmpty(ob1.TNA_077) ? (!string.IsNullOrEmpty(ob2.TNA_077) && Convert.ToDateTime(ob2.TNA_077) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_077) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_077) - Convert.ToDateTime(ob1.TNA_077)).Days.ToString();
            result.TNA_078 = String.IsNullOrEmpty(ob1.TNA_078) ? (!string.IsNullOrEmpty(ob2.TNA_078) && Convert.ToDateTime(ob2.TNA_078) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_078) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_078) - Convert.ToDateTime(ob1.TNA_078)).Days.ToString();
            result.TNA_079 = String.IsNullOrEmpty(ob1.TNA_079) ? (!string.IsNullOrEmpty(ob2.TNA_079) && Convert.ToDateTime(ob2.TNA_079) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_079) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_079) - Convert.ToDateTime(ob1.TNA_079)).Days.ToString();
            result.TNA_080 = String.IsNullOrEmpty(ob1.TNA_080) ? (!string.IsNullOrEmpty(ob2.TNA_080) && Convert.ToDateTime(ob2.TNA_080) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_080) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_080) - Convert.ToDateTime(ob1.TNA_080)).Days.ToString();
            result.TNA_081 = String.IsNullOrEmpty(ob1.TNA_081) ? (!string.IsNullOrEmpty(ob2.TNA_081) && Convert.ToDateTime(ob2.TNA_081) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_081) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_081) - Convert.ToDateTime(ob1.TNA_081)).Days.ToString();
            result.TNA_082 = String.IsNullOrEmpty(ob1.TNA_082) ? (!string.IsNullOrEmpty(ob2.TNA_082) && Convert.ToDateTime(ob2.TNA_082) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_082) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_082) - Convert.ToDateTime(ob1.TNA_082)).Days.ToString();
            result.TNA_083 = String.IsNullOrEmpty(ob1.TNA_083) ? (!string.IsNullOrEmpty(ob2.TNA_083) && Convert.ToDateTime(ob2.TNA_083) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_083) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_083) - Convert.ToDateTime(ob1.TNA_083)).Days.ToString();
            result.TNA_084 = String.IsNullOrEmpty(ob1.TNA_084) ? (!string.IsNullOrEmpty(ob2.TNA_084) && Convert.ToDateTime(ob2.TNA_084) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_084) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_084) - Convert.ToDateTime(ob1.TNA_084)).Days.ToString();
            result.TNA_085 = String.IsNullOrEmpty(ob1.TNA_085) ? (!string.IsNullOrEmpty(ob2.TNA_085) && Convert.ToDateTime(ob2.TNA_085) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_085) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_085) - Convert.ToDateTime(ob1.TNA_085)).Days.ToString();
            result.TNA_086 = String.IsNullOrEmpty(ob1.TNA_086) ? (!string.IsNullOrEmpty(ob2.TNA_086) && Convert.ToDateTime(ob2.TNA_086) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_086) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_086) - Convert.ToDateTime(ob1.TNA_086)).Days.ToString();
            result.TNA_087 = String.IsNullOrEmpty(ob1.TNA_087) ? (!string.IsNullOrEmpty(ob2.TNA_087) && Convert.ToDateTime(ob2.TNA_087) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_087) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_087) - Convert.ToDateTime(ob1.TNA_087)).Days.ToString();
            result.TNA_088 = String.IsNullOrEmpty(ob1.TNA_088) ? (!string.IsNullOrEmpty(ob2.TNA_088) && Convert.ToDateTime(ob2.TNA_088) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_088) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_088) - Convert.ToDateTime(ob1.TNA_088)).Days.ToString();
            result.TNA_089 = String.IsNullOrEmpty(ob1.TNA_089) ? (!string.IsNullOrEmpty(ob2.TNA_089) && Convert.ToDateTime(ob2.TNA_089) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_089) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_089) - Convert.ToDateTime(ob1.TNA_089)).Days.ToString();
            result.TNA_090 = String.IsNullOrEmpty(ob1.TNA_090) ? (!string.IsNullOrEmpty(ob2.TNA_090) && Convert.ToDateTime(ob2.TNA_090) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_090) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_090) - Convert.ToDateTime(ob1.TNA_090)).Days.ToString();
            result.TNA_091 = String.IsNullOrEmpty(ob1.TNA_091) ? (!string.IsNullOrEmpty(ob2.TNA_091) && Convert.ToDateTime(ob2.TNA_091) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_091) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_091) - Convert.ToDateTime(ob1.TNA_091)).Days.ToString();
            result.TNA_092 = String.IsNullOrEmpty(ob1.TNA_092) ? (!string.IsNullOrEmpty(ob2.TNA_092) && Convert.ToDateTime(ob2.TNA_092) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_092) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_092) - Convert.ToDateTime(ob1.TNA_092)).Days.ToString();
            result.TNA_093 = String.IsNullOrEmpty(ob1.TNA_093) ? (!string.IsNullOrEmpty(ob2.TNA_093) && Convert.ToDateTime(ob2.TNA_093) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_093) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_093) - Convert.ToDateTime(ob1.TNA_093)).Days.ToString();
            result.TNA_094 = String.IsNullOrEmpty(ob1.TNA_094) ? (!string.IsNullOrEmpty(ob2.TNA_094) && Convert.ToDateTime(ob2.TNA_094) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_094) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_094) - Convert.ToDateTime(ob1.TNA_094)).Days.ToString();
            result.TNA_095 = String.IsNullOrEmpty(ob1.TNA_095) ? (!string.IsNullOrEmpty(ob2.TNA_095) && Convert.ToDateTime(ob2.TNA_095) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_095) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_095) - Convert.ToDateTime(ob1.TNA_095)).Days.ToString();
            result.TNA_096 = String.IsNullOrEmpty(ob1.TNA_096) ? (!string.IsNullOrEmpty(ob2.TNA_096) && Convert.ToDateTime(ob2.TNA_096) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_096) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_096) - Convert.ToDateTime(ob1.TNA_096)).Days.ToString();
            result.TNA_097 = String.IsNullOrEmpty(ob1.TNA_097) ? (!string.IsNullOrEmpty(ob2.TNA_097) && Convert.ToDateTime(ob2.TNA_097) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_097) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_097) - Convert.ToDateTime(ob1.TNA_097)).Days.ToString();
            result.TNA_098 = String.IsNullOrEmpty(ob1.TNA_098) ? (!string.IsNullOrEmpty(ob2.TNA_098) && Convert.ToDateTime(ob2.TNA_098) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_098) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_098) - Convert.ToDateTime(ob1.TNA_098)).Days.ToString();
            result.TNA_099 = String.IsNullOrEmpty(ob1.TNA_099) ? (!string.IsNullOrEmpty(ob2.TNA_099) && Convert.ToDateTime(ob2.TNA_099) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_099) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_099) - Convert.ToDateTime(ob1.TNA_099)).Days.ToString();
            result.TNA_100 = String.IsNullOrEmpty(ob1.TNA_100) ? (!string.IsNullOrEmpty(ob2.TNA_100) && Convert.ToDateTime(ob2.TNA_100) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_100) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_100) - Convert.ToDateTime(ob1.TNA_100)).Days.ToString();
            result.TNA_101 = String.IsNullOrEmpty(ob1.TNA_101) ? (!string.IsNullOrEmpty(ob2.TNA_101) && Convert.ToDateTime(ob2.TNA_101) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_101) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_101) - Convert.ToDateTime(ob1.TNA_101)).Days.ToString();
            result.TNA_102 = String.IsNullOrEmpty(ob1.TNA_102) ? (!string.IsNullOrEmpty(ob2.TNA_102) && Convert.ToDateTime(ob2.TNA_102) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_102) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_102) - Convert.ToDateTime(ob1.TNA_102)).Days.ToString();
            result.TNA_103 = String.IsNullOrEmpty(ob1.TNA_103) ? (!string.IsNullOrEmpty(ob2.TNA_103) && Convert.ToDateTime(ob2.TNA_103) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_103) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_103) - Convert.ToDateTime(ob1.TNA_103)).Days.ToString();
            result.TNA_104 = String.IsNullOrEmpty(ob1.TNA_104) ? (!string.IsNullOrEmpty(ob2.TNA_104) && Convert.ToDateTime(ob2.TNA_104) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_104) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_104) - Convert.ToDateTime(ob1.TNA_104)).Days.ToString();
            result.TNA_105 = String.IsNullOrEmpty(ob1.TNA_105) ? (!string.IsNullOrEmpty(ob2.TNA_105) && Convert.ToDateTime(ob2.TNA_105) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_105) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_105) - Convert.ToDateTime(ob1.TNA_105)).Days.ToString();
       
            return result;           
        }

        public RF_PAGERModel queryData(
             long pageNumber, 
             long pageSize,
             Int64? pMC_BYR_ACC_ID,
             string pMC_ORDER_H_ID_LST,
             DateTime? pFIRSTDATE,
             DateTime? pLASTDATE,
             Int64? pLK_ORD_TYPE_ID

        )
        {
            string sp = "PKG_TNA.tna_matrix_grid_select";
            try
            {
                var obList = new List<TNA_CROSS_TABModel>();
                Int64 total_rec = 0; 
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                    new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =pMC_BYR_ACC_ID},
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID_LST", Value = pMC_ORDER_H_ID_LST},

                    new CommandParameter() {ParameterName = "pFIRSTDATE", Value = pFIRSTDATE},
                    new CommandParameter() {ParameterName = "pLASTDATE", Value = pLASTDATE},

                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value =HttpContext.Current.Session["multiScUserId"]},
                    new CommandParameter() {ParameterName = "pOption", Value =3002},
                    new CommandParameter() {ParameterName = "pLK_ORD_TYPE_ID", Value =pLK_ORD_TYPE_ID},

                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}

                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            TNA_CROSS_TABModel ob = new TNA_CROSS_TABModel();
                            ob.SE_TEXT = (dr["SE_TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SE_TEXT"]);
                            ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                            ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                            ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                            ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                            ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                            ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                            ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                            ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ?   Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);
                            ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ?  Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);
                            ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME"]);
                            ob.ORD_TYPE_NAME = (dr["ORD_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORD_TYPE_NAME"]);
                            ob.IDX = (dr["IDX"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IDX"]);
                            ob.TEXT = (dr["TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TEXT"]);
                            ob.IS_P_A = (dr["IS_P_A"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_P_A"]);

                            ob.GRP_ORDER = "Style# " + ob.WORK_STYLE_NO + ", Order# " + ob.ORDER_NO; 

                            //ob.GRP_ORDER = " Style# " + ob.WORK_STYLE_NO + ", Order# " + ob.ORDER_NO + ", Shipment:" + ob.SHIP_DT.ToString("dd-MMM-yyyy")+", Lead Time : "+ob.LEAD_TIME;
                            if (total_rec < 1)
                            {
                                total_rec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                            }


                            if (ob.SE_TEXT.Length == 2)
                            {
                                obList.Add(this.getDateDiff(obList, ob.MC_ORDER_H_ID, ob));
                            }
                            else
                            {
                               


                                if (dr["TNA_001"] != DBNull.Value)
                                {
                                    ob.TNA_001 = Convert.ToDateTime(dr["TNA_001"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_001 = "";
                                }

                                if (dr["TNA_002"] != DBNull.Value)
                                {
                                    ob.TNA_002 = Convert.ToDateTime(dr["TNA_002"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_002 = "";
                                }

                                if (dr["TNA_003"] != DBNull.Value)
                                {
                                    ob.TNA_003 = Convert.ToDateTime(dr["TNA_003"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_003 = "";
                                }

                                if (dr["TNA_004"] != DBNull.Value)
                                {
                                    ob.TNA_004 = Convert.ToDateTime(dr["TNA_004"]).ToString("dd-MMM-yyyy");
                                }else
                                {
                                    ob.TNA_004 = "";
                                }

                                if (dr["TNA_005"] != DBNull.Value)
                                {
                                    ob.TNA_005 = Convert.ToDateTime(dr["TNA_005"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_005 = "";
                                }

                                if (dr["TNA_006"] != DBNull.Value)
                                {
                                    ob.TNA_006 = Convert.ToDateTime(dr["TNA_006"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_006 = "";
                                }

                                if (dr["TNA_007"] != DBNull.Value)
                                {
                                    ob.TNA_007 = Convert.ToDateTime(dr["TNA_007"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_007 = "";
                                }

                                if (dr["TNA_008"] != DBNull.Value)
                                {
                                    ob.TNA_008 = Convert.ToDateTime(dr["TNA_008"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_008 = "";
                                }

                                if (dr["TNA_009"] != DBNull.Value)
                                {
                                    ob.TNA_009 = Convert.ToDateTime(dr["TNA_009"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_009 = "";
                                }
                                if (dr["TNA_010"] != DBNull.Value)
                                {
                                    ob.TNA_010 = Convert.ToDateTime(dr["TNA_010"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_010 = "";
                                }

                                if (dr["TNA_011"] != DBNull.Value)
                                {
                                    ob.TNA_011 = Convert.ToDateTime(dr["TNA_011"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_011 = "";
                                }

                                if (dr["TNA_012"] != DBNull.Value)
                                {
                                    ob.TNA_012 = Convert.ToDateTime(dr["TNA_012"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_012 = "";
                                }

                                if (dr["TNA_013"] != DBNull.Value)
                                {
                                    ob.TNA_013 = Convert.ToDateTime(dr["TNA_013"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_013 = "";
                                }

                                if (dr["TNA_014"] != DBNull.Value)
                                {
                                    ob.TNA_014 = Convert.ToDateTime(dr["TNA_014"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_014 = "";
                                }

                                if (dr["TNA_015"] != DBNull.Value)
                                {
                                    ob.TNA_015 = Convert.ToDateTime(dr["TNA_015"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_015 = "";
                                }

                                if (dr["TNA_016"] != DBNull.Value)
                                {
                                    ob.TNA_016 = Convert.ToDateTime(dr["TNA_016"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_016 = "";
                                }

                                if (dr["TNA_017"] != DBNull.Value)
                                {
                                    ob.TNA_017 = Convert.ToDateTime(dr["TNA_017"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_017 = "";
                                }

                                if (dr["TNA_018"] != DBNull.Value)
                                {
                                    ob.TNA_018 = Convert.ToDateTime(dr["TNA_018"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_018 = "";
                                }

                                if (dr["TNA_019"] != DBNull.Value)
                                {
                                    ob.TNA_019 = Convert.ToDateTime(dr["TNA_019"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_019 = "";
                                }
                                if (dr["TNA_020"] != DBNull.Value)
                                {
                                    ob.TNA_020 = Convert.ToDateTime(dr["TNA_020"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_020 = "";
                                }
                                if (dr["TNA_021"] != DBNull.Value)
                                {
                                    ob.TNA_021 = Convert.ToDateTime(dr["TNA_021"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_021 = "";
                                }
                                if (dr["TNA_022"] != DBNull.Value)
                                {
                                    ob.TNA_022 = Convert.ToDateTime(dr["TNA_022"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_022 = "";
                                }
                                if (dr["TNA_023"] != DBNull.Value)
                                {
                                    ob.TNA_023 = Convert.ToDateTime(dr["TNA_023"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_023 = "";
                                }
                                if (dr["TNA_024"] != DBNull.Value)
                                {
                                    ob.TNA_024 = Convert.ToDateTime(dr["TNA_024"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_024 = "";
                                }
                                if (dr["TNA_025"] != DBNull.Value)
                                {
                                    ob.TNA_025 = Convert.ToDateTime(dr["TNA_025"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_025 = "";
                                }
                                if (dr["TNA_026"] != DBNull.Value)
                                {
                                    ob.TNA_026 = Convert.ToDateTime(dr["TNA_026"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_026 = "";
                                }
                                if (dr["TNA_027"] != DBNull.Value)
                                {
                                    ob.TNA_027 = Convert.ToDateTime(dr["TNA_027"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_027 = "";
                                }
                                if (dr["TNA_028"] != DBNull.Value)
                                {
                                    ob.TNA_028 = Convert.ToDateTime(dr["TNA_028"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_028 = "";
                                }
                                if (dr["TNA_029"] != DBNull.Value)
                                {
                                    ob.TNA_029 = Convert.ToDateTime(dr["TNA_029"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_029 = "";
                                }
                                if (dr["TNA_030"] != DBNull.Value)
                                {
                                    ob.TNA_030 = Convert.ToDateTime(dr["TNA_030"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_030 = "";
                                }
                                if (dr["TNA_031"] != DBNull.Value)
                                {
                                    ob.TNA_031 = Convert.ToDateTime(dr["TNA_031"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_031 = "";
                                }
                                if (dr["TNA_032"] != DBNull.Value)
                                {
                                    ob.TNA_032 = Convert.ToDateTime(dr["TNA_032"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_032 = "";
                                }
                                if (dr["TNA_033"] != DBNull.Value)
                                {
                                    ob.TNA_033 = Convert.ToDateTime(dr["TNA_033"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_033 = "";
                                }
                                if (dr["TNA_034"] != DBNull.Value)
                                {
                                    ob.TNA_034 = Convert.ToDateTime(dr["TNA_034"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_034 = "";
                                }
                                if (dr["TNA_035"] != DBNull.Value)
                                {
                                    ob.TNA_035 = Convert.ToDateTime(dr["TNA_035"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_035 = "";
                                }
                                if (dr["TNA_036"] != DBNull.Value)
                                {
                                    ob.TNA_036 = Convert.ToDateTime(dr["TNA_036"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_036 = "";
                                }
                                if (dr["TNA_037"] != DBNull.Value)
                                {
                                    ob.TNA_037 = Convert.ToDateTime(dr["TNA_037"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_037 = "";
                                }
                                if (dr["TNA_038"] != DBNull.Value)
                                {
                                    ob.TNA_038 = Convert.ToDateTime(dr["TNA_038"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_038 = "";
                                }
                                if (dr["TNA_039"] != DBNull.Value)
                                {
                                    ob.TNA_039 = Convert.ToDateTime(dr["TNA_039"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_039 = "";
                                }
                                if (dr["TNA_040"] != DBNull.Value)
                                {
                                    ob.TNA_040 = Convert.ToDateTime(dr["TNA_040"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_040 = "";
                                }
                                if (dr["TNA_041"] != DBNull.Value)
                                {
                                    ob.TNA_041 = Convert.ToDateTime(dr["TNA_041"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_041 = "";
                                }
                                if (dr["TNA_042"] != DBNull.Value)
                                {
                                    ob.TNA_042 = Convert.ToDateTime(dr["TNA_042"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_042 = "";
                                }
                                if (dr["TNA_043"] != DBNull.Value)
                                {
                                    ob.TNA_043 = Convert.ToDateTime(dr["TNA_043"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_043 = "";
                                }
                                if (dr["TNA_044"] != DBNull.Value)
                                {
                                    ob.TNA_044 = Convert.ToDateTime(dr["TNA_044"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_044 = "";
                                }
                                if (dr["TNA_045"] != DBNull.Value)
                                {
                                    ob.TNA_045 = Convert.ToDateTime(dr["TNA_045"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_045 = "";
                                }
                                if (dr["TNA_046"] != DBNull.Value)
                                {
                                    ob.TNA_046 = Convert.ToDateTime(dr["TNA_046"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_046 = "";
                                }
                                if (dr["TNA_047"] != DBNull.Value)
                                {
                                    ob.TNA_047 = Convert.ToDateTime(dr["TNA_047"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_047 = "";
                                }
                                if (dr["TNA_048"] != DBNull.Value)
                                {
                                    ob.TNA_048 = Convert.ToDateTime(dr["TNA_048"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_048 = "";
                                }
                                if (dr["TNA_049"] != DBNull.Value)
                                {
                                    ob.TNA_049 = Convert.ToDateTime(dr["TNA_049"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_049 = "";
                                }
                                if (dr["TNA_050"] != DBNull.Value)
                                {
                                    ob.TNA_050 = Convert.ToDateTime(dr["TNA_050"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_050 = "";
                                }
                                if (dr["TNA_051"] != DBNull.Value)
                                {
                                    ob.TNA_051 = Convert.ToDateTime(dr["TNA_051"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_051 = "";
                                }
                                if (dr["TNA_052"] != DBNull.Value)
                                {
                                    ob.TNA_052 = Convert.ToDateTime(dr["TNA_052"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_052 = "";
                                }
                                if (dr["TNA_053"] != DBNull.Value)
                                {
                                    ob.TNA_053 = Convert.ToDateTime(dr["TNA_053"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_053 = "";
                                }
                                if (dr["TNA_054"] != DBNull.Value)
                                {
                                    ob.TNA_054 = Convert.ToDateTime(dr["TNA_054"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_054 = "";
                                }
                                if (dr["TNA_055"] != DBNull.Value)
                                {
                                    ob.TNA_055 = Convert.ToDateTime(dr["TNA_055"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_055 = "";
                                }
                                if (dr["TNA_056"] != DBNull.Value)
                                {
                                    ob.TNA_056 = Convert.ToDateTime(dr["TNA_056"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_056 = "";
                                }
                                if (dr["TNA_057"] != DBNull.Value)
                                {
                                    ob.TNA_057 = Convert.ToDateTime(dr["TNA_057"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_057 = "";
                                }
                                if (dr["TNA_058"] != DBNull.Value)
                                {
                                    ob.TNA_058 = Convert.ToDateTime(dr["TNA_058"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_058 = "";
                                }
                                if (dr["TNA_059"] != DBNull.Value)
                                {
                                    ob.TNA_059 = Convert.ToDateTime(dr["TNA_059"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_059 = "";
                                }
                                if (dr["TNA_060"] != DBNull.Value)
                                {
                                    ob.TNA_060 = Convert.ToDateTime(dr["TNA_060"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_060 = "";
                                }
                                if (dr["TNA_061"] != DBNull.Value)
                                {
                                    ob.TNA_061 = Convert.ToDateTime(dr["TNA_061"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_061 = "";
                                }
                                if (dr["TNA_062"] != DBNull.Value)
                                {
                                    ob.TNA_062 = Convert.ToDateTime(dr["TNA_062"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_062 = "";
                                }

                                if (dr["TNA_063"] != DBNull.Value)
                                {
                                    ob.TNA_063 = Convert.ToDateTime(dr["TNA_063"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_063 = "";
                                }

                                if (dr["TNA_064"] != DBNull.Value)
                                {
                                    ob.TNA_064 = Convert.ToDateTime(dr["TNA_064"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_064 = "";
                                }

                                if (dr["TNA_065"] != DBNull.Value)
                                {
                                    ob.TNA_065 = Convert.ToDateTime(dr["TNA_065"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_065 = "";
                                }

                                if (dr["TNA_066"] != DBNull.Value)
                                {
                                    ob.TNA_066 = Convert.ToDateTime(dr["TNA_066"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_066 = "";
                                }
                                if (dr["TNA_067"] != DBNull.Value)
                                {
                                    ob.TNA_067 = Convert.ToDateTime(dr["TNA_067"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_067 = "";
                                }

                                if (dr["TNA_068"] != DBNull.Value)
                                {
                                    ob.TNA_068 = Convert.ToDateTime(dr["TNA_068"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_068 = "";
                                }

                                if (dr["TNA_069"] != DBNull.Value)
                                {
                                    ob.TNA_069 = Convert.ToDateTime(dr["TNA_069"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_069 = "";
                                }

                                if (dr["TNA_070"] != DBNull.Value)
                                {
                                    ob.TNA_070 = Convert.ToDateTime(dr["TNA_070"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_070 = "";
                                }

                                if (dr["TNA_071"] != DBNull.Value)
                                {
                                    ob.TNA_071 = Convert.ToDateTime(dr["TNA_071"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_071 = "";
                                }

                                if (dr["TNA_072"] != DBNull.Value)
                                {
                                    ob.TNA_072 = Convert.ToDateTime(dr["TNA_072"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_072 = "";
                                }

                                if (dr["TNA_073"] != DBNull.Value)
                                {
                                    ob.TNA_073 = Convert.ToDateTime(dr["TNA_073"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_073 = "";
                                }

                                if (dr["TNA_074"] != DBNull.Value)
                                {
                                    ob.TNA_074 = Convert.ToDateTime(dr["TNA_074"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_074 = "";
                                }
                                if (dr["TNA_075"] != DBNull.Value)
                                {
                                    ob.TNA_075 = Convert.ToDateTime(dr["TNA_075"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_075 = "";
                                }
                                if (dr["TNA_076"] != DBNull.Value)
                                {
                                    ob.TNA_076 = Convert.ToDateTime(dr["TNA_076"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_076 = "";
                                }
                                if (dr["TNA_077"] != DBNull.Value)
                                {
                                    ob.TNA_077 = Convert.ToDateTime(dr["TNA_077"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_077 = "";
                                }
                                if (dr["TNA_078"] != DBNull.Value)
                                {
                                    ob.TNA_078 = Convert.ToDateTime(dr["TNA_078"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_078 = "";
                                }
                                if (dr["TNA_079"] != DBNull.Value)
                                {
                                    ob.TNA_079 = Convert.ToDateTime(dr["TNA_079"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_079 = "";
                                }
                                if (dr["TNA_080"] != DBNull.Value)
                                {
                                    ob.TNA_080 = Convert.ToDateTime(dr["TNA_080"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_080 = "";
                                }
                                if (dr["TNA_081"] != DBNull.Value)
                                {
                                    ob.TNA_081 = Convert.ToDateTime(dr["TNA_081"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_081 = "";
                                }
                                if (dr["TNA_082"] != DBNull.Value)
                                {
                                    ob.TNA_082 = Convert.ToDateTime(dr["TNA_082"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_082 = "";
                                }
                                if (dr["TNA_083"] != DBNull.Value)
                                {
                                    ob.TNA_083 = Convert.ToDateTime(dr["TNA_083"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_083 = "";
                                }
                                if (dr["TNA_084"] != DBNull.Value)
                                {
                                    ob.TNA_084 = Convert.ToDateTime(dr["TNA_084"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_084 = "";
                                }
                                if (dr["TNA_085"] != DBNull.Value)
                                {
                                    ob.TNA_085 = Convert.ToDateTime(dr["TNA_085"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_085 = "";
                                }
                                if (dr["TNA_086"] != DBNull.Value)
                                {
                                    ob.TNA_086 = Convert.ToDateTime(dr["TNA_086"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_086 = "";
                                }
                                if (dr["TNA_087"] != DBNull.Value)
                                {
                                    ob.TNA_087 = Convert.ToDateTime(dr["TNA_087"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_087 = "";
                                }
                                if (dr["TNA_088"] != DBNull.Value)
                                {
                                    ob.TNA_088 = Convert.ToDateTime(dr["TNA_088"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_088 = "";
                                }
                                if (dr["TNA_089"] != DBNull.Value)
                                {
                                    ob.TNA_089 = Convert.ToDateTime(dr["TNA_089"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_089 = "";
                                }
                                if (dr["TNA_090"] != DBNull.Value)
                                {
                                    ob.TNA_090 = Convert.ToDateTime(dr["TNA_090"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_090 = "";
                                }
                                if (dr["TNA_091"] != DBNull.Value)
                                {
                                    ob.TNA_091 = Convert.ToDateTime(dr["TNA_091"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_091 = "";
                                }
                                if (dr["TNA_092"] != DBNull.Value)
                                {
                                    ob.TNA_092 = Convert.ToDateTime(dr["TNA_092"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_092 = "";
                                }
                                if (dr["TNA_093"] != DBNull.Value)
                                {
                                    ob.TNA_093 = Convert.ToDateTime(dr["TNA_093"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_093 = "";
                                }

                                if (dr["TNA_094"] != DBNull.Value)
                                {
                                    ob.TNA_094 = Convert.ToDateTime(dr["TNA_094"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_094 = "";
                                }
                                if (dr["TNA_095"] != DBNull.Value)
                                {
                                    ob.TNA_095 = Convert.ToDateTime(dr["TNA_095"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_095 = "";
                                }
                                if (dr["TNA_096"] != DBNull.Value)
                                {
                                    ob.TNA_096 = Convert.ToDateTime(dr["TNA_096"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_096 = "";
                                }
                                if (dr["TNA_097"] != DBNull.Value)
                                {
                                    ob.TNA_097 = Convert.ToDateTime(dr["TNA_097"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_097 = "";
                                }

                                if (dr["TNA_098"] != DBNull.Value)
                                {
                                    ob.TNA_098 = Convert.ToDateTime(dr["TNA_098"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_098 = "";
                                }

                                if (dr["TNA_099"] != DBNull.Value)
                                {
                                    ob.TNA_099 = Convert.ToDateTime(dr["TNA_099"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_099 = "";
                                }

                                if (dr["TNA_100"] != DBNull.Value)
                                {
                                    ob.TNA_100 = Convert.ToDateTime(dr["TNA_100"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_100 = "";
                                }

                                if (dr["TNA_101"] != DBNull.Value)
                                {
                                    ob.TNA_101 = Convert.ToDateTime(dr["TNA_101"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_101 = "";
                                }

                                if (dr["TNA_102"] != DBNull.Value)
                                {
                                    ob.TNA_102 = Convert.ToDateTime(dr["TNA_102"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_102 = "";
                                }

                                if (dr["TNA_103"] != DBNull.Value)
                                {
                                    ob.TNA_103 = Convert.ToDateTime(dr["TNA_103"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_103 = "";
                                }

                                if (dr["TNA_104"] != DBNull.Value)
                                {
                                    ob.TNA_104 = Convert.ToDateTime(dr["TNA_104"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_104 = "";
                                }

                                if (dr["TNA_105"] != DBNull.Value)
                                {
                                    ob.TNA_105 = Convert.ToDateTime(dr["TNA_105"]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    ob.TNA_105 = "";
                                }

                                obList.Add(ob);
                            }
               }
              return new RF_PAGERModel() {
                  total = total_rec,
                 data = obList
              };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public TNA_CROSS_TABModel Select()
        {
            string sp = "PKG_TNA.tna_matrix_grid_select";
            try
            {
                var ob = new TNA_CROSS_TABModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.TNA_001 = (dr["TNA_001"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_001"]);
                    ob.TNA_002 = (dr["TNA_002"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_002"]);
                    ob.TNA_003 = (dr["TNA_003"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_003"]);
                    ob.TNA_004 = (dr["TNA_004"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_004"]);
                    ob.TNA_005 = (dr["TNA_005"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_005"]);
                    ob.TNA_006 = (dr["TNA_006"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_006"]);
                    ob.TNA_007 = (dr["TNA_007"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_007"]);
                    ob.TNA_008 = (dr["TNA_008"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_008"]);
                    ob.TNA_009 = (dr["TNA_009"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_009"]);
                    ob.TNA_010 = (dr["TNA_010"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_010"]);
                    ob.TNA_011 = (dr["TNA_011"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_011"]);
                    ob.TNA_012 = (dr["TNA_012"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_012"]);
                    ob.TNA_013 = (dr["TNA_013"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_013"]);
                    ob.TNA_014 = (dr["TNA_014"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_014"]);
                    ob.TNA_015 = (dr["TNA_015"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_015"]);
                    ob.TNA_016 = (dr["TNA_016"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_016"]);
                    ob.TNA_017 = (dr["TNA_017"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_017"]);
                    ob.TNA_018 = (dr["TNA_018"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_018"]);
                    ob.TNA_019 = (dr["TNA_019"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_019"]);
                    ob.TNA_020 = (dr["TNA_020"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_020"]);
                    ob.TNA_021 = (dr["TNA_021"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_021"]);
                    ob.TNA_022 = (dr["TNA_022"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_022"]);
                    ob.TNA_023 = (dr["TNA_023"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_023"]);
                    ob.TNA_024 = (dr["TNA_024"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_024"]);
                    ob.TNA_032 = (dr["TNA_032"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_032"]);
                    ob.TNA_025 = (dr["TNA_025"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_025"]);
                    ob.TNA_026 = (dr["TNA_026"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_026"]);
                    ob.TNA_027 = (dr["TNA_027"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_027"]);
                    ob.TNA_028 = (dr["TNA_028"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_028"]);
                    ob.TNA_029 = (dr["TNA_029"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_029"]);
                    ob.TNA_030 = (dr["TNA_030"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_030"]);
                    ob.TNA_031 = (dr["TNA_031"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_031"]);
                    ob.TNA_033 = (dr["TNA_033"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_033"]);
                    ob.TNA_034 = (dr["TNA_034"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_034"]);
                    ob.TNA_035 = (dr["TNA_035"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_035"]);
                    ob.TNA_036 = (dr["TNA_036"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_036"]);
                    ob.TNA_037 = (dr["TNA_037"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_037"]);
                    ob.TNA_038 = (dr["TNA_038"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_038"]);
                    ob.TNA_039 = (dr["TNA_039"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_039"]);
                    ob.TNA_040 = (dr["TNA_040"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_040"]);
                    ob.TNA_041 = (dr["TNA_041"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_041"]);
                    ob.TNA_042 = (dr["TNA_042"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_042"]);
                    ob.TNA_043 = (dr["TNA_043"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_043"]);
                    ob.TNA_044 = (dr["TNA_044"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_044"]);
                    ob.TNA_045 = (dr["TNA_045"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_045"]);
                    ob.TNA_046 = (dr["TNA_046"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_046"]);
                    ob.TNA_047 = (dr["TNA_047"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_047"]);
                    ob.TNA_048 = (dr["TNA_048"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_048"]);
                    ob.TNA_049 = (dr["TNA_049"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_049"]);
                    ob.TNA_050 = (dr["TNA_050"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_050"]);
                    ob.TNA_051 = (dr["TNA_051"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_051"]);
                    ob.TNA_052 = (dr["TNA_052"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_052"]);
                    ob.TNA_053 = (dr["TNA_053"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_053"]);
                    ob.TNA_054 = (dr["TNA_054"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_054"]);
                    ob.TNA_055 = (dr["TNA_055"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_055"]);
                    ob.TNA_056 = (dr["TNA_056"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_056"]);
                    ob.TNA_057 = (dr["TNA_057"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_057"]);
                    ob.TNA_058 = (dr["TNA_058"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_058"]);
                    ob.TNA_059 = (dr["TNA_059"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_059"]);
                    ob.TNA_060 = (dr["TNA_060"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_060"]);
                    ob.TNA_061 = (dr["TNA_061"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_061"]);
                    ob.TNA_062 = (dr["TNA_062"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_062"]);
                    ob.TNA_063 = (dr["TNA_063"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_063"]);
                    ob.TNA_064 = (dr["TNA_064"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_064"]);
                    ob.TNA_065 = (dr["TNA_065"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_065"]);
                    ob.TNA_066 = (dr["TNA_066"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_066"]);
                    ob.TNA_067 = (dr["TNA_067"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_067"]);
                    ob.TNA_068 = (dr["TNA_068"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_068"]);
                    ob.TNA_069 = (dr["TNA_069"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_069"]);
                    ob.TNA_070 = (dr["TNA_070"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_070"]);
                    ob.TNA_071 = (dr["TNA_071"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_071"]);
                    ob.TNA_072 = (dr["TNA_072"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_072"]);
                    ob.TNA_073 = (dr["TNA_073"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_073"]);
                    ob.TNA_074 = (dr["TNA_074"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_074"]);
                    ob.TNA_075 = (dr["TNA_075"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_075"]);
                    ob.TNA_076 = (dr["TNA_076"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_076"]);
                    ob.TNA_077 = (dr["TNA_077"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_077"]);
                    ob.TNA_078 = (dr["TNA_078"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_078"]);
                    ob.TNA_079 = (dr["TNA_079"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_079"]);
                    ob.TNA_080 = (dr["TNA_080"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_080"]);
                    ob.TNA_081 = (dr["TNA_081"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_081"]);
                    ob.TNA_082 = (dr["TNA_082"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_082"]);
                    ob.TNA_083 = (dr["TNA_083"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_083"]);
                    ob.TNA_084 = (dr["TNA_084"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_084"]);
                    ob.TNA_085 = (dr["TNA_085"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_085"]);
                    ob.TNA_086 = (dr["TNA_086"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_086"]);
                    ob.TNA_087 = (dr["TNA_087"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_087"]);
                    ob.TNA_088 = (dr["TNA_088"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_088"]);
                    ob.TNA_089 = (dr["TNA_089"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_089"]);
                    ob.TNA_090 = (dr["TNA_090"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_090"]);
                    ob.TNA_091 = (dr["TNA_091"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_091"]);
                    ob.TNA_092 = (dr["TNA_092"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_092"]);
                    ob.TNA_093 = (dr["TNA_093"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_093"]);
                    ob.TNA_094 = (dr["TNA_094"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_094"]);
                    ob.TNA_095 = (dr["TNA_095"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_095"]);
                    ob.TNA_096 = (dr["TNA_096"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_096"]);
                    ob.TNA_097 = (dr["TNA_097"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_097"]);
                    ob.TNA_098 = (dr["TNA_098"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_098"]);
                    ob.TNA_099 = (dr["TNA_099"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_099"]);
                    ob.TNA_100 = (dr["TNA_100"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_100"]);
                    ob.TNA_101 = (dr["TNA_101"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_101"]);
                    ob.TNA_102 = (dr["TNA_102"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_102"]);
                    ob.TNA_103 = (dr["TNA_103"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_103"]);
                    ob.TNA_104 = (dr["TNA_104"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_104"]);
                    ob.TNA_105 = (dr["TNA_105"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_105"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TN__GROP_HIDE> TNA_CODE_HIDE_SELECT()
        {
            try
            {
                var obList = new List<TN__GROP_HIDE>();
                OraDatabase db = new OraDatabase();


                TN__GROP_HIDE ob = new TN__GROP_HIDE();


                ob.TEXT = "GENERAL";
                ob.ID = 1;
                
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_GRP_ID", Value =1},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, "pkg_tna.mc_tna_task_code_select");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    ob.VALUES.Add((dr["TA_TASK_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_CODE"]));
                }

                obList.Add(ob);

                TN__GROP_HIDE ob1 = new TN__GROP_HIDE();


                ob1.TEXT = "SAMPLE";
                ob1.ID = 2;

                var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_GRP_ID", Value =2},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, "pkg_tna.mc_tna_task_code_select");
                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {

                    ob1.VALUES.Add((dr1["TA_TASK_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["TA_TASK_CODE"]));
                }

                obList.Add(ob1);

                TN__GROP_HIDE ob2 = new TN__GROP_HIDE();


                ob2.TEXT = "DEVELOPMENT";
                ob2.ID = 3;

                var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, "pkg_tna.tna_matrix_grid_select");
                foreach (DataRow dr2 in ds2.Tables[0].Rows)
                {

                    ob2.VALUES.Add((dr2["TA_TASK_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["TA_TASK_CODE"]));
                }

                obList.Add(ob2);
                


                TN__GROP_HIDE ob3 = new TN__GROP_HIDE();


                ob3.TEXT = "BULK";
                ob3.ID = 4;

                var ds3 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_GRP_ID", Value =3},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, "pkg_tna.mc_tna_task_code_select");
                foreach (DataRow dr3 in ds3.Tables[0].Rows)
                {

                    ob3.VALUES.Add((dr3["TA_TASK_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["TA_TASK_CODE"]));
                }

                obList.Add(ob3);

                TN__GROP_HIDE ob4 = new TN__GROP_HIDE();


                ob4.TEXT = "ONLY MY TASK";
                ob4.ID = 5;

                var ds4 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, "pkg_tna.mc_tna_task_code_select");
                foreach (DataRow dr4 in ds4.Tables[0].Rows)
                {

                    ob4.VALUES.Add((dr4["TA_TASK_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr4["TA_TASK_CODE"]));
                }

                obList.Add(ob4);



                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class TNA_CROSS_TAB_RPTModel
    {
        public Int64 MC_ORDER_H_ID { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string WORK_STYLE_NO { get; set; }
        public string STYLE_DESC { get; set; }
        public string ORDER_NO { get; set; }
        public Int64 TOT_ORD_QTY { get; set; }
        public string MOU_CODE { get; set; }
        public DateTime ORD_CONF_DT { get; set; }
        public DateTime SHIP_DT { get; set; }
        public Int64 LEAD_TIME { get; set; }
        public string ORD_TYPE_NAME { get; set; }
        public Int64 IDX { get; set; }
        public string TEXT { get; set; }
        public string IS_P_A { get; set; }
        public string SE_TEXT { get; set; }

        public string TNA_001 { get; set; }
        public string TNA_021 { get; set; }
        public string TNA_022 { get; set; }
        public string TNA_023 { get; set; }
        public string TNA_024 { get; set; }
        public string TNA_025 { get; set; }
        public string TNA_026 { get; set; }
        public string TNA_027 { get; set; }
        public string TNA_028 { get; set; }
        public string TNA_029 { get; set; }
        public string TNA_030 { get; set; }
        public string TNA_032 { get; set; }
        public string TNA_033 { get; set; }     
        public string TNA_105 { get; set; }
        public string GRP_ORDER { get; set; }

        private TNA_CROSS_TAB_RPTModel getDateDiff(List<TNA_CROSS_TAB_RPTModel> oblist, Int64 MC_ORDER_H_ID, TNA_CROSS_TAB_RPTModel ob)
        {

            TNA_CROSS_TAB_RPTModel result = new TNA_CROSS_TAB_RPTModel();

            TNA_CROSS_TAB_RPTModel ob1 = oblist.Where(x => x.MC_ORDER_H_ID == ob.MC_ORDER_H_ID && x.IS_P_A == "A").FirstOrDefault();
            TNA_CROSS_TAB_RPTModel ob2 = oblist.Where(x => x.MC_ORDER_H_ID == ob.MC_ORDER_H_ID && x.IS_P_A == "P").FirstOrDefault();

            result.MC_ORDER_H_ID = ob.MC_ORDER_H_ID;
            result.BYR_ACC_NAME_EN = ob.BYR_ACC_NAME_EN;
            result.WORK_STYLE_NO = ob.WORK_STYLE_NO;
            result.STYLE_DESC = ob.STYLE_DESC;
            result.ORDER_NO = ob.ORDER_NO;
            result.TOT_ORD_QTY = ob.TOT_ORD_QTY;
            result.MOU_CODE = ob.MOU_CODE;
            result.ORD_CONF_DT = ob.ORD_CONF_DT;
            result.SHIP_DT = ob.SHIP_DT;
            result.LEAD_TIME = ob.LEAD_TIME;
            result.ORD_TYPE_NAME = ob.ORD_TYPE_NAME;
            result.IDX = ob.IDX;
            result.TEXT = ob.TEXT;
            result.IS_P_A = ob.IS_P_A;
            result.SE_TEXT = ob.SE_TEXT;
            result.GRP_ORDER = "Style# " + ob.WORK_STYLE_NO + ", Order# " + ob.ORDER_NO;

            result.TNA_001 = String.IsNullOrEmpty(ob1.TNA_001) ? (!string.IsNullOrEmpty(ob2.TNA_001) && Convert.ToDateTime(ob2.TNA_001) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_001) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_001) - Convert.ToDateTime(ob1.TNA_001)).Days.ToString();
            result.TNA_021 = String.IsNullOrEmpty(ob1.TNA_021) ? (!string.IsNullOrEmpty(ob2.TNA_021) && Convert.ToDateTime(ob2.TNA_021) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_021) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_021) - Convert.ToDateTime(ob1.TNA_021)).Days.ToString();
            result.TNA_022 = String.IsNullOrEmpty(ob1.TNA_022) ? (!string.IsNullOrEmpty(ob2.TNA_022) && Convert.ToDateTime(ob2.TNA_022) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_022) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_022) - Convert.ToDateTime(ob1.TNA_022)).Days.ToString();
            result.TNA_023 = String.IsNullOrEmpty(ob1.TNA_023) ? (!string.IsNullOrEmpty(ob2.TNA_023) && Convert.ToDateTime(ob2.TNA_023) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_023) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_023) - Convert.ToDateTime(ob1.TNA_023)).Days.ToString();
            result.TNA_024 = String.IsNullOrEmpty(ob1.TNA_024) ? (!string.IsNullOrEmpty(ob2.TNA_024) && Convert.ToDateTime(ob2.TNA_024) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_024) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_024) - Convert.ToDateTime(ob1.TNA_024)).Days.ToString();
            result.TNA_025 = String.IsNullOrEmpty(ob1.TNA_025) ? (!string.IsNullOrEmpty(ob2.TNA_025) && Convert.ToDateTime(ob2.TNA_025) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_025) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_025) - Convert.ToDateTime(ob1.TNA_025)).Days.ToString();
            result.TNA_026 = String.IsNullOrEmpty(ob1.TNA_026) ? (!string.IsNullOrEmpty(ob2.TNA_026) && Convert.ToDateTime(ob2.TNA_026) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_026) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_026) - Convert.ToDateTime(ob1.TNA_026)).Days.ToString();
            result.TNA_027 = String.IsNullOrEmpty(ob1.TNA_027) ? (!string.IsNullOrEmpty(ob2.TNA_027) && Convert.ToDateTime(ob2.TNA_027) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_027) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_027) - Convert.ToDateTime(ob1.TNA_027)).Days.ToString();
            result.TNA_028 = String.IsNullOrEmpty(ob1.TNA_028) ? (!string.IsNullOrEmpty(ob2.TNA_028) && Convert.ToDateTime(ob2.TNA_028) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_028) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_028) - Convert.ToDateTime(ob1.TNA_028)).Days.ToString();
            result.TNA_030 = String.IsNullOrEmpty(ob1.TNA_030) ? (!string.IsNullOrEmpty(ob2.TNA_030) && Convert.ToDateTime(ob2.TNA_030) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_030) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_030) - Convert.ToDateTime(ob1.TNA_030)).Days.ToString();
            result.TNA_029 = String.IsNullOrEmpty(ob1.TNA_029) ? (!string.IsNullOrEmpty(ob2.TNA_029) && Convert.ToDateTime(ob2.TNA_029) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_029) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_029) - Convert.ToDateTime(ob1.TNA_029)).Days.ToString();
            result.TNA_032 = String.IsNullOrEmpty(ob1.TNA_032) ? (!string.IsNullOrEmpty(ob2.TNA_032) && Convert.ToDateTime(ob2.TNA_032) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_032) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_032) - Convert.ToDateTime(ob1.TNA_032)).Days.ToString();
            result.TNA_033 = String.IsNullOrEmpty(ob1.TNA_033) ? (!string.IsNullOrEmpty(ob2.TNA_033) && Convert.ToDateTime(ob2.TNA_033) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_033) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_033) - Convert.ToDateTime(ob1.TNA_033)).Days.ToString();
          
            result.TNA_105 = String.IsNullOrEmpty(ob1.TNA_105) ? (!string.IsNullOrEmpty(ob2.TNA_105) && Convert.ToDateTime(ob2.TNA_105) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_105) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_105) - Convert.ToDateTime(ob1.TNA_105)).Days.ToString();

            return result;
        }

        public List<TNA_CROSS_TAB_RPTModel> queryData()
        {
            string sp = "PKG_TNA.tna_matrix_grid_select";
            try
            {
                var obList = new List<TNA_CROSS_TAB_RPTModel>();
                DateTime result;
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value =HttpContext.Current.Session["multiScUserId"]},
                    new CommandParameter() {ParameterName = "pOption", Value =3000},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}

                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TNA_CROSS_TAB_RPTModel ob = new TNA_CROSS_TAB_RPTModel();
                    ob.SE_TEXT = (dr["SE_TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SE_TEXT"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);
                    ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME"]);
                    ob.ORD_TYPE_NAME = (dr["ORD_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORD_TYPE_NAME"]);
                    ob.IDX = (dr["IDX"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IDX"]);
                    ob.TEXT = (dr["TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TEXT"]);
                    ob.IS_P_A = (dr["IS_P_A"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_P_A"]);

                    ob.GRP_ORDER = "Style# " + ob.WORK_STYLE_NO + ", Order# " + ob.ORDER_NO;



                    if (ob.SE_TEXT.Length == 2)
                    {
                        obList.Add(this.getDateDiff(obList, ob.MC_ORDER_H_ID, ob));
                    }
                    else
                    {



                        if (dr["TNA_001"] != DBNull.Value)
                        {
                            ob.TNA_001 = DateTime.TryParseExact(Convert.ToString(dr["TNA_001"]), "dd-MMM-yy",CultureInfo.InvariantCulture,
                              DateTimeStyles.None, out result) ? Convert.ToDateTime(dr["TNA_001"]).ToString("dd-MMM-yyyy") : Convert.ToString(dr["TNA_001"]);
                        }
                        else
                        {
                            ob.TNA_001 = "";
                        }

                        if (dr["TNA_021"] != DBNull.Value)
                        {
                            ob.TNA_021 = DateTime.TryParseExact(Convert.ToString(dr["TNA_021"]), "dd-MMM-yy", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out result) ? Convert.ToDateTime(dr["TNA_021"]).ToString("dd-MMM-yyyy") : Convert.ToString(dr["TNA_021"]);
                        }
                        else
                        {
                            ob.TNA_021 = "";
                        }
                        if (dr["TNA_022"] != DBNull.Value)
                        {
                            ob.TNA_022 = DateTime.TryParseExact(Convert.ToString(dr["TNA_022"]), "dd-MMM-yy", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out result) ? Convert.ToDateTime(dr["TNA_022"]).ToString("dd-MMM-yyyy") : Convert.ToString(dr["TNA_022"]);
                        }
                        else
                        {
                            ob.TNA_022 = "";
                        }
                        if (dr["TNA_023"] != DBNull.Value)
                        {
                            ob.TNA_023 = DateTime.TryParseExact(Convert.ToString(dr["TNA_023"]), "dd-MMM-yy", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out result) ? Convert.ToDateTime(dr["TNA_023"]).ToString("dd-MMM-yyyy") : Convert.ToString(dr["TNA_023"]);
                        }
                        else
                        {
                            ob.TNA_023 = "";
                        }
                        if (dr["TNA_024"] != DBNull.Value)
                        {
                            ob.TNA_024 = DateTime.TryParseExact(Convert.ToString(dr["TNA_024"]), "dd-MMM-yy", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out result) ? Convert.ToDateTime(dr["TNA_024"]).ToString("dd-MMM-yyyy") : Convert.ToString(dr["TNA_024"]);
                        }
                        else
                        {
                            ob.TNA_024 = "";
                        }
                        if (dr["TNA_025"] != DBNull.Value)
                        {
                            ob.TNA_025 = DateTime.TryParseExact(Convert.ToString(dr["TNA_025"]), "dd-MMM-yy", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out result) ? Convert.ToDateTime(dr["TNA_025"]).ToString("dd-MMM-yyyy") : Convert.ToString(dr["TNA_025"]);
                        }
                        else
                        {
                            ob.TNA_025 = "";
                        }
                        if (dr["TNA_026"] != DBNull.Value)
                        {
                            ob.TNA_026 = DateTime.TryParseExact(Convert.ToString(dr["TNA_026"]), "dd-MMM-yy", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out result) ? Convert.ToDateTime(dr["TNA_026"]).ToString("dd-MMM-yyyy") : Convert.ToString(dr["TNA_026"]);
                        }
                        else
                        {
                            ob.TNA_026 = "";
                        }
                        if (dr["TNA_027"] != DBNull.Value)
                        {
                            ob.TNA_027 = DateTime.TryParseExact(Convert.ToString(dr["TNA_027"]), "dd-MMM-yy", CultureInfo.InvariantCulture,
                           DateTimeStyles.None, out result) ? Convert.ToDateTime(dr["TNA_027"]).ToString("dd-MMM-yyyy") : Convert.ToString(dr["TNA_027"]);
                        }
                        else
                        {
                            ob.TNA_027 = "";
                        }
                        if (dr["TNA_028"] != DBNull.Value)
                        {
                            ob.TNA_028 = DateTime.TryParseExact(Convert.ToString(dr["TNA_028"]), "dd-MMM-yy", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out result) ? Convert.ToDateTime(dr["TNA_028"]).ToString("dd-MMM-yyyy") : Convert.ToString(dr["TNA_028"]);
                        }
                        else
                        {
                            ob.TNA_028 = "";
                        }
                        if (dr["TNA_029"] != DBNull.Value)
                        {
                            ob.TNA_029 = DateTime.TryParseExact(Convert.ToString(dr["TNA_029"]), "dd-MMM-yy", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out result) ? Convert.ToDateTime(dr["TNA_029"]).ToString("dd-MMM-yyyy") : Convert.ToString(dr["TNA_029"]);
                        }
                        else
                        {
                            ob.TNA_029 = "";
                        }

                        if (dr["TNA_030"] != DBNull.Value)
                        {
                            ob.TNA_030 = DateTime.TryParseExact(Convert.ToString(dr["TNA_030"]), "dd-MMM-yy", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out result) ? Convert.ToDateTime(dr["TNA_030"]).ToString("dd-MMM-yyyy") : Convert.ToString(dr["TNA_030"]);
                        }
                        else
                        {
                            ob.TNA_030 = "";
                        }
                 
                        if (dr["TNA_032"] != DBNull.Value)
                        {
                            ob.TNA_032 = DateTime.TryParseExact(Convert.ToString(dr["TNA_032"]), "dd-MMM-yy", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out result) ? Convert.ToDateTime(dr["TNA_032"]).ToString("dd-MMM-yyyy") : Convert.ToString(dr["TNA_032"]);
                        }
                        else
                        {
                            ob.TNA_032 = "";
                        }
                        if (dr["TNA_033"] != DBNull.Value)
                        {
                            ob.TNA_033 = DateTime.TryParseExact(Convert.ToString(dr["TNA_033"]), "dd-MMM-yy", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out result) ? Convert.ToDateTime(dr["TNA_033"]).ToString("dd-MMM-yyyy") : Convert.ToString(dr["TNA_033"]);
                        }
                        else
                        {
                            ob.TNA_033 = "";
                        }

                        if (dr["TNA_105"] != DBNull.Value)
                        {
                            ob.TNA_105 = DateTime.TryParseExact(Convert.ToString(dr["TNA_105"]), "dd-MMM-yy", CultureInfo.InvariantCulture,
                             DateTimeStyles.None, out result) ? Convert.ToDateTime(dr["TNA_105"]).ToString("dd-MMM-yyyy") : Convert.ToString(dr["TNA_105"]);
                        }
                        else
                        {
                            ob.TNA_105 = "";
                        }

                        obList.Add(ob);
                    }
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    public class TN__GROP_HIDE
    {
        public string TEXT { get; set; }


        private List<String> _VALUES;
        public Int64 ID { get; set; }

        public List<String> VALUES
        {
            get
            {
                if (_VALUES == null)
                {
                    _VALUES = new List<String>();
                }
                return _VALUES;
            }
            set
            {
                _VALUES = value;
            }
        }
    }

    public class TNA_CROSS_TAB_RPT_BLKModel
    {
        public Int64 MC_ORDER_H_ID { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string WORK_STYLE_NO { get; set; }
        public string STYLE_DESC { get; set; }
        public string ORDER_NO { get; set; }
        public Int64 TOT_ORD_QTY { get; set; }
        public string MOU_CODE { get; set; }
        public DateTime ORD_CONF_DT { get; set; }
        public DateTime SHIP_DT { get; set; }
        public Int64 LEAD_TIME { get; set; }
        public string ORD_TYPE_NAME { get; set; }
        public Int64 IDX { get; set; }
        public string TEXT { get; set; }
        public string IS_P_A { get; set; }
        public string SE_TEXT { get; set; }

        public string TNA_038 { get; set; }
        public string TNA_039 { get; set; }
        public string TNA_007 { get; set; }
        public string TNA_008 { get; set; }
        public string TNA_009 { get; set; }
        public string TNA_010 { get; set; }
        public string TNA_013 { get; set; }
        public string TNA_014 { get; set; }
        public string TNA_015 { get; set; }
        public string TNA_016 { get; set; }
        public string TNA_017 { get; set; }
        public string TNA_018 { get; set; }
        public string TNA_022 { get; set; }
        public string TNA_023 { get; set; }

        public string TNA_030 { get; set; }
        public string TNA_052 { get; set; }
        public string TNA_053 { get; set; }
        public string TNA_066 { get; set; }
        public string TNA_067 { get; set; }
        public string TNA_054 { get; set; }
        public string TNA_055 { get; set; }
        public string TNA_068 { get; set; }
        public string TNA_069 { get; set; }
        public string TNA_062 { get; set; }
        public string TNA_063 { get; set; }
        public string TNA_050 { get; set; }
        public string TNA_051 { get; set; }
        public string TNA_060 { get; set; }
        public string TNA_061 { get; set; }
        public string TNA_070 { get; set; }
        public string TNA_071 { get; set; }
        public string TNA_002 { get; set; }
        public string TNA_078 { get; set; }
        public string TNA_077 { get; set; }
        public string TNA_080 { get; set; }
        public string TNA_081 { get; set; }
        public string TNA_082 { get; set; }
        public string TNA_083 { get; set; }
        public string TNA_084 { get; set; }
        public string TNA_085 { get; set; }
        public string TNA_086 { get; set; }
        public string TNA_091 { get; set; }
        public string TNA_092 { get; set; }
        public string TNA_093 { get; set; }
        public string TNA_094 { get; set; }

        public string TNA_095 { get; set; }
        public string TNA_096 { get; set; }
        public string TNA_097 { get; set; }
        public string TNA_098 { get; set; }
        public string TNA_101 { get; set; }
        public string TNA_102 { get; set; }
        public string TNA_103 { get; set; }
        public string GRP_ORDER { get; set; }

        private TNA_CROSS_TAB_RPT_BLKModel getDateDiff(List<TNA_CROSS_TAB_RPT_BLKModel> oblist, Int64 MC_ORDER_H_ID, TNA_CROSS_TAB_RPT_BLKModel ob)
        {

            TNA_CROSS_TAB_RPT_BLKModel result = new TNA_CROSS_TAB_RPT_BLKModel();

            TNA_CROSS_TAB_RPT_BLKModel ob1 = oblist.Where(x => x.MC_ORDER_H_ID == ob.MC_ORDER_H_ID && x.IS_P_A == "A").FirstOrDefault();
            TNA_CROSS_TAB_RPT_BLKModel ob2 = oblist.Where(x => x.MC_ORDER_H_ID == ob.MC_ORDER_H_ID && x.IS_P_A == "P").FirstOrDefault();

            result.MC_ORDER_H_ID = ob.MC_ORDER_H_ID;
            result.BYR_ACC_NAME_EN = ob.BYR_ACC_NAME_EN;
            result.WORK_STYLE_NO = ob.WORK_STYLE_NO;
            result.STYLE_DESC = ob.STYLE_DESC;
            result.ORDER_NO = ob.ORDER_NO;
            result.TOT_ORD_QTY = ob.TOT_ORD_QTY;
            result.MOU_CODE = ob.MOU_CODE;
            result.ORD_CONF_DT = ob.ORD_CONF_DT;
            result.SHIP_DT = ob.SHIP_DT;
            result.LEAD_TIME = ob.LEAD_TIME;
            result.ORD_TYPE_NAME = ob.ORD_TYPE_NAME;
            result.IDX = ob.IDX;
            result.TEXT = ob.TEXT;
            result.IS_P_A = ob.IS_P_A;
            result.SE_TEXT = ob.SE_TEXT;
            result.GRP_ORDER = "Style# " + ob.WORK_STYLE_NO + ", Order# " + ob.ORDER_NO;

            result.TNA_038 = String.IsNullOrEmpty(ob1.TNA_038) ? (!string.IsNullOrEmpty(ob2.TNA_038) && Convert.ToDateTime(ob2.TNA_038) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_038) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_038) - Convert.ToDateTime(ob1.TNA_038)).Days.ToString();
            result.TNA_039 = String.IsNullOrEmpty(ob1.TNA_039) ? (!string.IsNullOrEmpty(ob2.TNA_039) && Convert.ToDateTime(ob2.TNA_039) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_039) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_039) - Convert.ToDateTime(ob1.TNA_039)).Days.ToString();
            result.TNA_007 = String.IsNullOrEmpty(ob1.TNA_007) ? (!string.IsNullOrEmpty(ob2.TNA_007) && Convert.ToDateTime(ob2.TNA_007) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_007) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_007) - Convert.ToDateTime(ob1.TNA_007)).Days.ToString();
            result.TNA_008 = String.IsNullOrEmpty(ob1.TNA_008) ? (!string.IsNullOrEmpty(ob2.TNA_008) && Convert.ToDateTime(ob2.TNA_008) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_008) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_008) - Convert.ToDateTime(ob1.TNA_008)).Days.ToString();
            result.TNA_009 = String.IsNullOrEmpty(ob1.TNA_009) ? (!string.IsNullOrEmpty(ob2.TNA_009) && Convert.ToDateTime(ob2.TNA_009) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_009) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_009) - Convert.ToDateTime(ob1.TNA_009)).Days.ToString();
            result.TNA_010 = String.IsNullOrEmpty(ob1.TNA_010) ? (!string.IsNullOrEmpty(ob2.TNA_010) && Convert.ToDateTime(ob2.TNA_010) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_010) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_010) - Convert.ToDateTime(ob1.TNA_010)).Days.ToString();
            result.TNA_013 = String.IsNullOrEmpty(ob1.TNA_013) ? (!string.IsNullOrEmpty(ob2.TNA_013) && Convert.ToDateTime(ob2.TNA_013) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_013) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_013) - Convert.ToDateTime(ob1.TNA_013)).Days.ToString();
            result.TNA_014 = String.IsNullOrEmpty(ob1.TNA_014) ? (!string.IsNullOrEmpty(ob2.TNA_014) && Convert.ToDateTime(ob2.TNA_014) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_014) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_014) - Convert.ToDateTime(ob1.TNA_014)).Days.ToString();
            result.TNA_015 = String.IsNullOrEmpty(ob1.TNA_015) ? (!string.IsNullOrEmpty(ob2.TNA_015) && Convert.ToDateTime(ob2.TNA_015) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_015) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_015) - Convert.ToDateTime(ob1.TNA_015)).Days.ToString();
            result.TNA_016 = String.IsNullOrEmpty(ob1.TNA_016) ? (!string.IsNullOrEmpty(ob2.TNA_016) && Convert.ToDateTime(ob2.TNA_016) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_016) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_016) - Convert.ToDateTime(ob1.TNA_016)).Days.ToString();
            result.TNA_017 = String.IsNullOrEmpty(ob1.TNA_017) ? (!string.IsNullOrEmpty(ob2.TNA_017) && Convert.ToDateTime(ob2.TNA_017) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_017) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_017) - Convert.ToDateTime(ob1.TNA_017)).Days.ToString();
            result.TNA_018 = String.IsNullOrEmpty(ob1.TNA_018) ? (!string.IsNullOrEmpty(ob2.TNA_018) && Convert.ToDateTime(ob2.TNA_018) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_018) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_018) - Convert.ToDateTime(ob1.TNA_018)).Days.ToString();
            result.TNA_022 = String.IsNullOrEmpty(ob1.TNA_022) ? (!string.IsNullOrEmpty(ob2.TNA_022) && Convert.ToDateTime(ob2.TNA_022) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_022) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_022) - Convert.ToDateTime(ob1.TNA_022)).Days.ToString();
            result.TNA_023 = String.IsNullOrEmpty(ob1.TNA_023) ? (!string.IsNullOrEmpty(ob2.TNA_023) && Convert.ToDateTime(ob2.TNA_023) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_023) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_023) - Convert.ToDateTime(ob1.TNA_023)).Days.ToString();
            result.TNA_030 = String.IsNullOrEmpty(ob1.TNA_030) ? (!string.IsNullOrEmpty(ob2.TNA_030) && Convert.ToDateTime(ob2.TNA_030) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_030) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_030) - Convert.ToDateTime(ob1.TNA_030)).Days.ToString();
            result.TNA_052 = String.IsNullOrEmpty(ob1.TNA_052) ? (!string.IsNullOrEmpty(ob2.TNA_052) && Convert.ToDateTime(ob2.TNA_052) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_052) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_052) - Convert.ToDateTime(ob1.TNA_052)).Days.ToString();
            result.TNA_053 = String.IsNullOrEmpty(ob1.TNA_053) ? (!string.IsNullOrEmpty(ob2.TNA_053) && Convert.ToDateTime(ob2.TNA_053) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_053) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_053) - Convert.ToDateTime(ob1.TNA_053)).Days.ToString();
            result.TNA_066 = String.IsNullOrEmpty(ob1.TNA_066) ? (!string.IsNullOrEmpty(ob2.TNA_066) && Convert.ToDateTime(ob2.TNA_066) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_066) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_066) - Convert.ToDateTime(ob1.TNA_066)).Days.ToString();
            result.TNA_067 = String.IsNullOrEmpty(ob1.TNA_067) ? (!string.IsNullOrEmpty(ob2.TNA_067) && Convert.ToDateTime(ob2.TNA_067) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_067) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_067) - Convert.ToDateTime(ob1.TNA_067)).Days.ToString();
            result.TNA_054 = String.IsNullOrEmpty(ob1.TNA_054) ? (!string.IsNullOrEmpty(ob2.TNA_054) && Convert.ToDateTime(ob2.TNA_054) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_054) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_054) - Convert.ToDateTime(ob1.TNA_054)).Days.ToString();
            result.TNA_055 = String.IsNullOrEmpty(ob1.TNA_055) ? (!string.IsNullOrEmpty(ob2.TNA_055) && Convert.ToDateTime(ob2.TNA_055) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_055) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_055) - Convert.ToDateTime(ob1.TNA_055)).Days.ToString();
            result.TNA_068 = String.IsNullOrEmpty(ob1.TNA_068) ? (!string.IsNullOrEmpty(ob2.TNA_068) && Convert.ToDateTime(ob2.TNA_068) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_068) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_068) - Convert.ToDateTime(ob1.TNA_068)).Days.ToString();
            result.TNA_069 = String.IsNullOrEmpty(ob1.TNA_069) ? (!string.IsNullOrEmpty(ob2.TNA_069) && Convert.ToDateTime(ob2.TNA_069) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_069) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_069) - Convert.ToDateTime(ob1.TNA_069)).Days.ToString();
            result.TNA_062 = String.IsNullOrEmpty(ob1.TNA_062) ? (!string.IsNullOrEmpty(ob2.TNA_062) && Convert.ToDateTime(ob2.TNA_062) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_062) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_062) - Convert.ToDateTime(ob1.TNA_062)).Days.ToString();
            result.TNA_063 = String.IsNullOrEmpty(ob1.TNA_063) ? (!string.IsNullOrEmpty(ob2.TNA_063) && Convert.ToDateTime(ob2.TNA_063) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_063) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_063) - Convert.ToDateTime(ob1.TNA_063)).Days.ToString();
            result.TNA_050 = String.IsNullOrEmpty(ob1.TNA_050) ? (!string.IsNullOrEmpty(ob2.TNA_050) && Convert.ToDateTime(ob2.TNA_050) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_050) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_050) - Convert.ToDateTime(ob1.TNA_050)).Days.ToString();
            result.TNA_051 = String.IsNullOrEmpty(ob1.TNA_051) ? (!string.IsNullOrEmpty(ob2.TNA_051) && Convert.ToDateTime(ob2.TNA_051) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_051) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_051) - Convert.ToDateTime(ob1.TNA_051)).Days.ToString();
            result.TNA_060 = String.IsNullOrEmpty(ob1.TNA_060) ? (!string.IsNullOrEmpty(ob2.TNA_060) && Convert.ToDateTime(ob2.TNA_060) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_060) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_060) - Convert.ToDateTime(ob1.TNA_060)).Days.ToString();
            result.TNA_061 = String.IsNullOrEmpty(ob1.TNA_061) ? (!string.IsNullOrEmpty(ob2.TNA_061) && Convert.ToDateTime(ob2.TNA_061) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_061) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_061) - Convert.ToDateTime(ob1.TNA_061)).Days.ToString();
            result.TNA_070 = String.IsNullOrEmpty(ob1.TNA_070) ? (!string.IsNullOrEmpty(ob2.TNA_070) && Convert.ToDateTime(ob2.TNA_070) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_070) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_070) - Convert.ToDateTime(ob1.TNA_070)).Days.ToString();
            result.TNA_071 = String.IsNullOrEmpty(ob1.TNA_071) ? (!string.IsNullOrEmpty(ob2.TNA_071) && Convert.ToDateTime(ob2.TNA_071) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_071) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_071) - Convert.ToDateTime(ob1.TNA_071)).Days.ToString();
            result.TNA_002 = String.IsNullOrEmpty(ob1.TNA_002) ? (!string.IsNullOrEmpty(ob2.TNA_002) && Convert.ToDateTime(ob2.TNA_002) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_002) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_002) - Convert.ToDateTime(ob1.TNA_002)).Days.ToString();
            result.TNA_078 = String.IsNullOrEmpty(ob1.TNA_078) ? (!string.IsNullOrEmpty(ob2.TNA_078) && Convert.ToDateTime(ob2.TNA_078) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_078) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_078) - Convert.ToDateTime(ob1.TNA_078)).Days.ToString();
            result.TNA_077 = String.IsNullOrEmpty(ob1.TNA_077) ? (!string.IsNullOrEmpty(ob2.TNA_077) && Convert.ToDateTime(ob2.TNA_077) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_077) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_077) - Convert.ToDateTime(ob1.TNA_077)).Days.ToString();
            result.TNA_080 = String.IsNullOrEmpty(ob1.TNA_080) ? (!string.IsNullOrEmpty(ob2.TNA_080) && Convert.ToDateTime(ob2.TNA_080) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_080) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_080) - Convert.ToDateTime(ob1.TNA_080)).Days.ToString();
            result.TNA_081 = String.IsNullOrEmpty(ob1.TNA_081) ? (!string.IsNullOrEmpty(ob2.TNA_081) && Convert.ToDateTime(ob2.TNA_081) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_081) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_081) - Convert.ToDateTime(ob1.TNA_081)).Days.ToString();
            result.TNA_082 = String.IsNullOrEmpty(ob1.TNA_082) ? (!string.IsNullOrEmpty(ob2.TNA_082) && Convert.ToDateTime(ob2.TNA_082) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_082) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_082) - Convert.ToDateTime(ob1.TNA_082)).Days.ToString();
            result.TNA_083 = String.IsNullOrEmpty(ob1.TNA_083) ? (!string.IsNullOrEmpty(ob2.TNA_083) && Convert.ToDateTime(ob2.TNA_083) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_083) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_083) - Convert.ToDateTime(ob1.TNA_083)).Days.ToString();
            result.TNA_084 = String.IsNullOrEmpty(ob1.TNA_084) ? (!string.IsNullOrEmpty(ob2.TNA_084) && Convert.ToDateTime(ob2.TNA_084) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_084) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_084) - Convert.ToDateTime(ob1.TNA_084)).Days.ToString();
            result.TNA_085 = String.IsNullOrEmpty(ob1.TNA_085) ? (!string.IsNullOrEmpty(ob2.TNA_085) && Convert.ToDateTime(ob2.TNA_085) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_085) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_085) - Convert.ToDateTime(ob1.TNA_085)).Days.ToString();
            result.TNA_086 = String.IsNullOrEmpty(ob1.TNA_086) ? (!string.IsNullOrEmpty(ob2.TNA_086) && Convert.ToDateTime(ob2.TNA_086) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_086) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_086) - Convert.ToDateTime(ob1.TNA_086)).Days.ToString();
            result.TNA_091 = String.IsNullOrEmpty(ob1.TNA_091) ? (!string.IsNullOrEmpty(ob2.TNA_091) && Convert.ToDateTime(ob2.TNA_091) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_091) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_091) - Convert.ToDateTime(ob1.TNA_091)).Days.ToString();
            result.TNA_092 = String.IsNullOrEmpty(ob1.TNA_092) ? (!string.IsNullOrEmpty(ob2.TNA_092) && Convert.ToDateTime(ob2.TNA_092) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_092) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_092) - Convert.ToDateTime(ob1.TNA_092)).Days.ToString();
            result.TNA_093 = String.IsNullOrEmpty(ob1.TNA_093) ? (!string.IsNullOrEmpty(ob2.TNA_093) && Convert.ToDateTime(ob2.TNA_093) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_093) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_093) - Convert.ToDateTime(ob1.TNA_093)).Days.ToString();
            result.TNA_094 = String.IsNullOrEmpty(ob1.TNA_094) ? (!string.IsNullOrEmpty(ob2.TNA_094) && Convert.ToDateTime(ob2.TNA_094) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_094) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_094) - Convert.ToDateTime(ob1.TNA_094)).Days.ToString();
            result.TNA_095 = String.IsNullOrEmpty(ob1.TNA_095) ? (!string.IsNullOrEmpty(ob2.TNA_095) && Convert.ToDateTime(ob2.TNA_095) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_095) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_095) - Convert.ToDateTime(ob1.TNA_095)).Days.ToString();
            result.TNA_096 = String.IsNullOrEmpty(ob1.TNA_096) ? (!string.IsNullOrEmpty(ob2.TNA_096) && Convert.ToDateTime(ob2.TNA_096) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_096) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_096) - Convert.ToDateTime(ob1.TNA_096)).Days.ToString();
            result.TNA_097 = String.IsNullOrEmpty(ob1.TNA_097) ? (!string.IsNullOrEmpty(ob2.TNA_097) && Convert.ToDateTime(ob2.TNA_097) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_097) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_097) - Convert.ToDateTime(ob1.TNA_097)).Days.ToString();
            result.TNA_098 = String.IsNullOrEmpty(ob1.TNA_098) ? (!string.IsNullOrEmpty(ob2.TNA_098) && Convert.ToDateTime(ob2.TNA_098) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_098) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_098) - Convert.ToDateTime(ob1.TNA_098)).Days.ToString();
            result.TNA_101 = String.IsNullOrEmpty(ob1.TNA_101) ? (!string.IsNullOrEmpty(ob2.TNA_101) && Convert.ToDateTime(ob2.TNA_101) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_101) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_101) - Convert.ToDateTime(ob1.TNA_101)).Days.ToString();
            result.TNA_102 = String.IsNullOrEmpty(ob1.TNA_102) ? (!string.IsNullOrEmpty(ob2.TNA_102) && Convert.ToDateTime(ob2.TNA_102) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_102) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_102) - Convert.ToDateTime(ob1.TNA_102)).Days.ToString();
            result.TNA_103 = String.IsNullOrEmpty(ob1.TNA_103) ? (!string.IsNullOrEmpty(ob2.TNA_103) && Convert.ToDateTime(ob2.TNA_103) < DateTime.Now) ? (Convert.ToDateTime(ob2.TNA_103) - DateTime.Now).Days.ToString() : "" : (Convert.ToDateTime(ob2.TNA_103) - Convert.ToDateTime(ob1.TNA_103)).Days.ToString();

            return result;
        }

        public List<TNA_CROSS_TAB_RPT_BLKModel> queryData()
        {
            string sp = "PKG_TNA.tna_matrix_grid_select";
            try
            {
                var obList = new List<TNA_CROSS_TAB_RPT_BLKModel>();
                DateTime result;
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value =HttpContext.Current.Session["multiScUserId"]},
                    new CommandParameter() {ParameterName = "pOption", Value =3000},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}

                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TNA_CROSS_TAB_RPT_BLKModel ob = new TNA_CROSS_TAB_RPT_BLKModel();
                    ob.SE_TEXT = (dr["SE_TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SE_TEXT"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);
                    ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME"]);
                    ob.ORD_TYPE_NAME = (dr["ORD_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORD_TYPE_NAME"]);
                    ob.IDX = (dr["IDX"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IDX"]);
                    ob.TEXT = (dr["TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TEXT"]);
                    ob.IS_P_A = (dr["IS_P_A"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_P_A"]);

                    ob.GRP_ORDER = "Style# " + ob.WORK_STYLE_NO + ", Order# " + ob.ORDER_NO;



                    if (ob.SE_TEXT.Length == 2)
                    {
                        obList.Add(this.getDateDiff(obList, ob.MC_ORDER_H_ID, ob));
                    }
                    else
                    {



                        //if (dr["TNA_001"] != DBNull.Value)
                        //{
                        //    ob.TNA_001 = DateTime.TryParseExact(Convert.ToString(dr["TNA_001"]), "dd-MMM-yy", CultureInfo.InvariantCulture,
                        //      DateTimeStyles.None, out result) ? Convert.ToDateTime(dr["TNA_001"]).ToString("dd-MMM-yyyy") : Convert.ToString(dr["TNA_001"]);
                        //}
                        //else
                        //{
                        //    ob.TNA_001 = "";
                        //}

                  

                        obList.Add(ob);
                    }
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