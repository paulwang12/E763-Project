using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E763_Project
{
    public static class OutPut
    {
        // 公共字典

        public readonly static Dictionary<BeamType, double> BeamHeight = new Dictionary<BeamType, double>()
        {
            {BeamType.B50,4.2 },
            {BeamType.B60,4.2 },
            {BeamType.T25,2.07 },
            {BeamType.T35,2.6 },
            {BeamType.F10,1.1 },
            {BeamType.F15,1.1 },
        };


        public static void SupStructure(ref DataTable alrcd, ref DataTable BoB)
        {

            for (int i = 0; i < BoB.Rows.Count; i++)
            {
                DataRow data = BoB.Rows[i];
                string BriName = (string)data["PK"];
                Console.WriteLine(BriName);
                foreach (var item in (string)data["W"])
                {
                    string cla = "";
                    string loc = item.ToString();
                    string det = "";
                    double Q1 = 0;
                    double Q2 = 0;
                    List<BeamType> BTlist = (List<BeamType>)data["BeamTypes"];
                    List<int> BridgeW = (List<int>)data["Width"];
                    int SideSpanToCount = ((int)data["GroupNum"]-(BTlist.Count((x)=>x==BeamType.B60))/3)*2;
                    Q2 = SideSpanToCount;
                    int BridgeWI = item=='L' ? BridgeW[0] : BridgeW[1];
                    for (int j = 0; j < BTlist.Count; j++)
                    {
                        BeamType btitem = BTlist[j];
                        cla = btitem.ToString();
                        double Ac=0;
                        double Vc = 0;
                        double Rebar=0;
                        double PreRebar = 0;
                        double Vpre = 11.04;
                        double Vinplace = 0;
                        double BearingSteel;


                        if (btitem==BeamType.B60)
                        {
                            double Lc = 0;
                            if (BridgeWI==1365)
                            {
                                Ac = 9.6604;
                                Lc = 18.3;
                            }
                            else if (BridgeWI == 1465)
                            {
                                Ac = 10.0549;
                                Lc = 19.3;
                            }
                            else if (BridgeWI == 1715)
                            {
                                Ac = 11.4707;
                                Lc = 22.2;
                            }
                            else if (BridgeWI == 1815)
                            {
                                Ac = 11.8;
                                Lc = 23.1;
                            }
                            else
                            {
                                Ac = 0;
                            }
                            Vc = Ac * (35 + 60 + 35);
                            Rebar = Vc * 190;
                            PreRebar = Vc * 50;
                            
                            WriteRcd(ref alrcd, BriName, cla, loc, det, "混凝土", "C50",Vc,130,35,21);
                            WriteRcd(ref alrcd, BriName, cla, loc, det, "钢筋", "HRB400",Rebar,1,61,22);
                            WriteRcd(ref alrcd, BriName, cla, loc, det, "预应力筋", "SPB-F15.2", PreRebar, 1,65,24);
                            WriteRcd(ref alrcd, BriName, "混凝土涂装", loc, det, "", "", Lc * 130, 1,83,36);
                            j += 2;
                        }
                        else if (btitem == BeamType.B50)
                        {
                            double Lc = 0;
                            if (BridgeWI == 1365)
                            {
                                Ac = 9.6604;
                                Lc = 18.3;

                            }
                            else if (BridgeWI == 1465)
                            {
                                Ac = 10.0549;
                                Lc =19.3;

                            }
                            else if (BridgeWI == 1715)
                            {
                                Ac = 11.4707;
                                Lc = 22.2;
                            }
                            else if (BridgeWI == 1815)
                            {
                                Ac = 11.8;
                                Lc = 23.1;
                            }
                            else
                            {
                                Ac = 0;
                                Lc = 0;
                            }
                            Vc = Ac *50;
                            Rebar = Vc * 190;
                            PreRebar = Vc * 40;

                            WriteRcd(ref alrcd, BriName, cla, loc, det, "混凝土", "C50", Vc, 50,34,21);
                            WriteRcd(ref alrcd, BriName, cla, loc, det, "钢筋", "HRB400", Rebar, 1,60,22);
                            WriteRcd(ref alrcd, BriName, cla, loc, det, "预应力筋", "SPB-F15.2", PreRebar, 1,64,24);
                            WriteRcd(ref alrcd, BriName, "混凝土涂装", loc, det, "", "", Lc * 50, 1,83,36);

                        }
                        else if (btitem==BeamType.T35)
                        {
                            
                            Vpre = 26.048;
                           
                            SideSpanToCount -= 1;
                            if (SideSpanToCount>=0)
                            {
                                det = "边跨";
                            }
                            else
                            {
                                det = "中跨";
                            }
                            int beamNum = 0;            
                            if (BridgeWI == 1365)
                            {
                                beamNum = 7;
                                
                            }
                            else if (BridgeWI == 1465)
                            {
                                beamNum = 8;

                            }
                            else if (BridgeWI == 1715)
                            {
                                beamNum = 9;

                            }
                            else if (BridgeWI == 1815)
                            {
                                beamNum = 10;
                            }
                            else
                            {
                                beamNum = 0;
                            }
                            for (int k = 0; k < beamNum; k++)
                            {
                                if(k==0| k == beamNum - 1)
                                {
                                    // 边跨
                                    det= det.Substring(0,2) + "边梁";
                                    Vinplace = 2.55;
                                    BearingSteel = 74.9177;
                                    
                                }
                                else
                                {
                                    // 中跨
                                    det= det.Substring(0, 2) + "中梁";
                                    Vinplace = 2.55;
                                    BearingSteel = 95.1236;
                                }
                                WriteRcd(ref alrcd, BriName, cla, loc, det, "混凝土", "C50预制", Vpre, Q2,32,19);
                                WriteRcd(ref alrcd, BriName, cla, loc, det, "混凝土", "C50现浇", Vinplace, Q2,33,21);
                                WriteRcd(ref alrcd, BriName, cla, loc, det, "钢筋", "HRB400", Vpre * 170 + Vinplace * 180, Q2,59,22);
                                WriteRcd(ref alrcd, BriName, cla, loc, det, "预应力筋", "SPB-F15.2", Vpre * 65, Q2,63,24);
                                WriteRcd(ref alrcd, BriName, "调平钢板", loc, det, "钢材", "",BearingSteel, Q2,85,25);
                                WriteRcd(ref alrcd, BriName, "混凝土涂装", loc, det, "", "",5.6948 * 35, Q2,83,36);
                            }



                        }
                        else if (btitem == BeamType.T25)
                        {
                            Vpre = 11.04;                            
                            
                            SideSpanToCount -= 1;
                            if (SideSpanToCount >= 0)
                            {
                                det = "边跨";
                            }
                            else
                            {
                                det = "中跨";
                            }
                            int beamNum = 0;
                            if (BridgeWI == 1365)
                            {
                                beamNum = 8;

                            }
                            else if (BridgeWI == 1465)
                            {
                                beamNum = 9;

                            }
                            else if (BridgeWI == 1715)
                            {
                                beamNum = 10;

                            }
                            else if (BridgeWI == 1815)
                            {
                                beamNum = 11;
                            }
                            else
                            {
                                beamNum = 0;
                            }
                            for (int k = 0; k < beamNum; k++)
                            {
                                if (k == 0 | k == beamNum - 1)
                                {
                                    // 边跨
                                    det=det.Substring(0, 2) + "边梁";
                                    Vinplace = 1;
                                    BearingSteel = 68.107;
                                }
                                else
                                {
                                    // 中跨
                                    det = det.Substring(0, 2) + "中梁";
                                    Vinplace = 2.0;
                                    BearingSteel = 86.476;
                                }
                                WriteRcd(ref alrcd, BriName, cla, loc, det, "混凝土", "C50预制", Vpre, Q2,30,19);
                                WriteRcd(ref alrcd, BriName, cla, loc, det, "混凝土", "C50现浇", Vinplace, Q2,31,21);
                                WriteRcd(ref alrcd, BriName, cla, loc, det, "钢筋", "HRB400", Vpre * 170 + Vinplace * 180, Q2,58,22);
                                WriteRcd(ref alrcd, BriName, cla, loc, det, "预应力筋", "SPB-F15.2", Vpre * 65, Q2,62,24);
                                WriteRcd(ref alrcd, BriName, "调平钢板", loc, det, "钢材", "", BearingSteel, Q2,85,25);
                                WriteRcd(ref alrcd, BriName, "混凝土涂装", loc, det,"", "", 4.292*25, Q2,83,36);
                            }

                        }
                        else
                        {

                            // 框架上部
                            if ((string)data["W"]!="LR")
                            {
                                double A = double.Parse((string)data["A"]);
                                double L0 = BridgeWI * 0.01 / Math.Cos((90 - A) / 180.0 * Math.PI);
                                double span = double.Parse(btitem.ToString().Substring(1, 2));
                                double dist= span* Math.Cos((90 - A) / 180.0 * Math.PI);
                                double L1 = span * Math.Cos(A / 180.0 * Math.PI);
                                WriteRcd(ref alrcd, BriName, cla, loc, "-", "混凝土", "C50", 1.0*(L0+L1)* dist, 1,26,21);
                                WriteRcd(ref alrcd, BriName, cla, loc, "-", "钢筋", "HRB400", 170.0 * (L0 + L1) * dist, 1,54,22);
                                WriteRcd(ref alrcd, BriName, "混凝土涂装", loc, "-", "", "", (L0 + L1) * dist, 1, 83, 36);
                            }
                            else
                            {
                                if (loc != "R")
                                {
                                    double A = double.Parse((string)data["A"]);
                                    double L0 = (BridgeW[0]+ BridgeW[1]) * 0.01 / Math.Cos((90 - A) / 180.0 * Math.PI);
                                    double span = double.Parse(btitem.ToString().Substring(1, 2));
                                    double dist = span * Math.Cos((90 - A) / 180.0 * Math.PI);
                                    double L1 = span * Math.Cos(A / 180.0 * Math.PI);
                                    WriteRcd(ref alrcd, BriName, cla, "LR", "-", "混凝土", "C50", 1.0 * (L0 + L1) * dist, 1,26,21);
                                    WriteRcd(ref alrcd, BriName, cla, "LR", "-", "钢筋", "HRB400", 170.0 * (L0 + L1) * dist, 1,54,22);
                                    WriteRcd(ref alrcd, BriName, "混凝土涂装", "LR", "-", "", "", (L0 + L1) * dist, 1, 83, 36);
                                }
                            }

                            
                            
                        }




                    }
                }
            }
        }


        public static void SubStructure(ref DataTable alrcd,ref DataTable BoB)
        {
            for (int i = 0; i < BoB.Rows.Count; i++)
            {                
                DataRow data = BoB.Rows[i];
                string BriName = (string)data["PK"];
                Console.WriteLine(BriName);
                foreach (var item in (string)data["W"])
                {
                    string cla = "";
                    string loc = item.ToString();
                    string det = "";                    
                    double Q1 = 0;
                    double Q2 = 0;
                    double CapBeamH = 0;
                    double CapBeamT = 0;
                    double CapBeamL = 0;
                    List<double> PierS = new List<double>();
                    List<double> dmx = (List<double>)data["DMX"];
                    List<double> sjx = (List<double>)data["SJX"];
                    List<BeamType> BTlist = (List<BeamType>)data["BeamTypes"];
                    List<int> BridgeW = (List<int>)data["Width"];
                    int BridgeWI = item=='L' ? BridgeW[0]:BridgeW[1];
                    
                    double PierHClear;
                    for (int j = 0; j < dmx.Count; j++)
                    {                        
                        
                        if (j == 0 | j == dmx.Count - 1)
                        {
                            //框架桥台
                            if (BTlist[0]<=BeamType.F15) 
                            {
                                PierHClear = sjx[j] - dmx[j] - 1+ 0.5;

                                if ((string)data["W"] != "LR")
                                {
                                    double A = double.Parse((string)data["A"]);
                                    double L0 = BridgeWI * 0.01 / Math.Cos((90 - A) / 180.0 * Math.PI);
                                    double span = double.Parse(BTlist[0].ToString().Substring(1, 2));
                                    double L1 = span * Math.Cos(A / 180.0 * Math.PI);
                                    WriteRcd(ref alrcd, BriName, "翼墙", loc, "-", "混凝土", "C50", 2.0 *  PierHClear*PierHClear, 1,27,15);
                                    WriteRcd(ref alrcd, BriName, "翼墙", loc, "-", "钢筋", "HRB400", 2*170.0 * PierHClear * PierHClear, 1,55,22);
                                    WriteRcd(ref alrcd, BriName, "侧墙", loc, "-", "混凝土", "C50", 1.0 * (L0 + L1) * PierHClear, 1,27,15);
                                    WriteRcd(ref alrcd, BriName, "侧墙", loc, "-", "钢筋", "HRB400", 170.0 * (L0 + L1) * PierHClear, 1,55,22);
                                    WriteRcd(ref alrcd, BriName, "承台", loc, "-", "混凝土", "C30", 3.0 * (L0 + L1+1), 1,13,13);                                   
                                    WriteRcd(ref alrcd, BriName, "承台", loc, "-", "混凝土", "C15", 0.1*2.2 * (L0 + L1 + 1+0.2), 1,7,7);
                                    WriteRcd(ref alrcd, BriName, "承台", loc, "-", "钢筋", "HRB400", 120*3.0 * (L0 + L1 + 1), 1,41,22);
                                    WriteRcd(ref alrcd, BriName, "搭板", loc, "", "混凝土", "C35", 0.3 * 4.5 * (L0 + L1) * 1, 1, 16, 20);
                                    WriteRcd(ref alrcd, BriName, "搭板", loc, "", "混凝土", "C15", 0.1 * (4.5 + 0.2) * ((L0 + L1) + 0.2) * 1, 1, 7, 7);
                                    WriteRcd(ref alrcd, BriName, "搭板", loc, "", "钢筋", "HRB400", 0.3 * 4.5 * (L0 + L1) * 150, 1, 44, 22);
                                    WXS.GetV(out double Vw, out double Vb, 2, (L0 + L1 + 1), 1.5,2, 3);
                                    WriteRcd(ref alrcd, BriName, "基础开挖", loc, "-", "-", "-",Vw, 1,1,1);
                                    WriteRcd(ref alrcd, BriName, "基础回填", loc, "-", "-", "-", Vb, 1,2,2);
                                    WriteRcd(ref alrcd, BriName, "台背回填", loc, "-", "-", "-", (1.75+1.75+0.5*PierHClear)*PierHClear*0.5*(L0+L1), 1,3,3);
                                    WriteRcd(ref alrcd, BriName, "沥青涂装", loc, "侧墙", "沥青", "", (L0 + L1 + 1) * 2 * 1,1,84,37);
                                    WriteRcd(ref alrcd, BriName, "沥青涂装", loc, "承台", "沥青", "", (L0 + L1 + 1 + 2.0) * 2 * 1.5 + (L0 + L1 + 1) * 2.0, 1,84,37);
                                    double CV, CC, CA;
                                    WXS.Cone(out CV, out CC, out CA, PierHClear * 1.5, PierHClear * 1.5, PierHClear);
                                    WriteRcd(ref alrcd, BriName, "锥坡", loc, "锥坡土方", "土", "", 0.5 * CV, 1, 4, 4);
                                    WriteRcd(ref alrcd, BriName, "锥坡", loc, "锥坡护道", "", "", 0.5 * CC * 0.458, 1, 6, 6);
                                    WriteRcd(ref alrcd, BriName, "锥坡", loc, "锥坡护坡", "", "", 0.5 * CA, 1, 5, 5);


                                    double LZ = WXS.GetLZ(Ext.PK2NUM(BriName));
                                    int Znum = (int)Math.Ceiling(((L0 + L1+PierHClear *2*2 + 1) - 2 - 1.2) / 3.6);
                                    double VV = LZ * 1.2 * 1.2 * 0.25 * Math.PI;
                                    for (int k = 0; k < Znum; k++)
                                    {
                                        string detial = string.Format("{0}#", k + 1);                                        
                                        WriteRcd(ref alrcd, BriName, "桩基D120", loc, detial, "混凝土", "C30", LZ, VV,8,8);
                                        WriteRcd(ref alrcd, BriName, "桩基D120", loc, detial, "钢筋", "HRB400", VV * 120, 1,36,23);
                                    }
                                }
                                else 
                                {
                                    if (loc != "R")
                                    {

                                        double A = double.Parse((string)data["A"]);
                                        double L0 = (BridgeW[0] + BridgeW[1]) * 0.01 / Math.Cos((90 - A) / 180.0 * Math.PI);
                                        double span = double.Parse(BTlist[0].ToString().Substring(1, 2));
                                        double L1 = span * Math.Cos(A / 180.0 * Math.PI);
                                        WriteRcd(ref alrcd, BriName, "翼墙", "LR", "-", "混凝土", "C50", 2.0 * PierHClear * PierHClear, 1,27,15);
                                        WriteRcd(ref alrcd, BriName, "翼墙", "LR", "-", "钢筋", "HRB400", 2 * 170.0 * PierHClear * PierHClear, 1,55,22);
                                        WriteRcd(ref alrcd, BriName, "侧墙", "LR", "-", "混凝土", "C50", 1.0 * (L0 + L1) * PierHClear, 1,27,15);
                                        WriteRcd(ref alrcd, BriName, "侧墙", "LR", "-", "钢筋", "HRB400", 170.0 * (L0 + L1) * PierHClear, 1,55,22);
                                        WriteRcd(ref alrcd, BriName, "承台", "LR", "-", "混凝土", "C30", 3.0 * (L0 + L1 + 1), 1,13,13);
                                        WriteRcd(ref alrcd, BriName, "承台", "LR", "-", "混凝土", "C15", 0.1 * 2.2 * (L0 + L1 + 1 + 0.2), 1,7,7);
                                        WriteRcd(ref alrcd, BriName, "承台", "LR", "-", "钢筋", "HRB400", 120 * 3.0 * (L0 + L1 + 1), 1,41,22);
                                        WriteRcd(ref alrcd, BriName, "搭板", "LR", "", "混凝土", "C35", 0.3 * 4.5 * (L0 + L1) * 1, 1, 16, 20);
                                        WriteRcd(ref alrcd, BriName, "搭板", "LR", "", "混凝土", "C15", 0.1 * (4.5 + 0.2) * ((L0 + L1) + 0.2) * 1, 1, 7, 7);
                                        WriteRcd(ref alrcd, BriName, "搭板", "LR", "", "钢筋", "HRB400", 0.3 * 4.5 * (L0 + L1) * 150, 1, 44, 22);
                                        WXS.GetV(out double Vw, out double Vb, 2, (L0 + L1 + 1), 1.5, 2, 3);
                                        WriteRcd(ref alrcd, BriName, "基础开挖", "LR", "-", "-", "-", Vw, 1,1,1);
                                        WriteRcd(ref alrcd, BriName, "基础回填", "LR", "-", "-", "-", Vb, 1,2,2);
                                        WriteRcd(ref alrcd, BriName, "台背回填", "LR", "-", "-", "-", (1.75 + 1.75 + 0.5 * PierHClear) * PierHClear * 0.5 * (L0 + L1), 1,3,3);
                                        WriteRcd(ref alrcd, BriName, "沥青涂装", "LR", "侧墙", "沥青", "", (L0 + L1 + 1) * 2 * 1, 1,84,37);
                                        WriteRcd(ref alrcd, BriName, "沥青涂装", "LR", "承台", "沥青", "", (L0 + L1 + 1 + 2.0) * 2 * 1.5 + (L0 + L1 + 1) * 2.0, 1,84,37);
                                        double CV, CC, CA;
                                        WXS.Cone(out CV, out CC, out CA, PierHClear * 1.5, PierHClear * 1.5, PierHClear);
                                        WriteRcd(ref alrcd, BriName, "锥坡", "LR", "锥坡土方", "土", "", 0.5 * CV, 1, 4, 4);
                                        WriteRcd(ref alrcd, BriName, "锥坡", "LR", "锥坡护道", "", "", 0.5 * CC * 0.458, 1, 6, 6);
                                        WriteRcd(ref alrcd, BriName, "锥坡", "LR", "锥坡护坡", "", "", 0.5 * CA, 1, 5, 5);

                                        double LZ = WXS.GetLZ(Ext.PK2NUM(BriName));
                                        int Znum = (int)Math.Ceiling(((L0 + L1 + 1) - 2 - 1.2) / 3.6);
                                        double VV = LZ * 1.2 * 1.2 * 0.25 * Math.PI;
                                        for (int k = 0; k < Znum; k++)
                                        {
                                            string detial = string.Format("{0}#", k + 1);
                                            WriteRcd(ref alrcd, BriName, "桩基D120", "LR", detial, "混凝土", "C30", LZ, VV,8,8);
                                            WriteRcd(ref alrcd, BriName, "桩基D120", "LR", detial, "钢筋", "HRB400", VV * 120, 1,36,23);
                                        }
                                    }
                                }


                            }
                            // 常规桥台
                            else
                            {
                                WriteAbutment(ref alrcd, ref data, loc, j);
                                //PierHClear = sjx[j] - dmx[j];
                                //WriteRcd(ref alrcd, BriName, "桥台", loc, det, "混凝土", "C40", PierHClear, 1);
                            }
                            
                        }
                        else
                        {
                            // 桥墩
                            PierHClear = sjx[j] - dmx[j] - BeamHeight[BTlist[j]]+0.5;
                            if (PierHClear >= 40)
                            {
                                cla = "空心墩3.0x7.0";                                
                                det = "-";
                                if (BTlist[j] >= BeamType.B50 | BTlist[j - 1] >= BeamType.B50)
                                {
                                    double ht =  4.5;
                                    CapBeamH = 0;
                                    Q1 = ht * 21 + (PierHClear - ht - CapBeamH) * 10.8;
                                    Q2 = PierHClear;
                                    WritePC5(ref alrcd, BriName, loc);
                                }
                                else
                                {
                                    double ht = 3.5;
                                    CapBeamH = BridgeWI>=1700?2.5:2.3;
                                    Q1 = ht * 21 + (PierHClear - ht - CapBeamH) * 10.8;
                                    Q2 = PierHClear;
                                    CapBeamT = 3.4;
                                    CapBeamL = BridgeWI == 1815 ? 17.5 : BridgeWI == 1715 ? 16.5 : BridgeWI == 1465 ? 14 : 13;
                                    double VolofCB = CapBeamT * CapBeamL * CapBeamH;
                                    WriteRcd(ref alrcd, BriName, "盖梁", loc, det, "混凝土", "C40", VolofCB, 1,25,16);
                                    WriteRcd(ref alrcd, BriName, "盖梁", loc, det, "钢筋", "HRB400", VolofCB * 180, 1,53,22);
                                    WritePC4(ref alrcd, BriName, loc);
                                }
                                WriteRcd(ref alrcd, BriName, cla, loc, det, "混凝土", "C40", Q1, Q2,24,15);
                                WriteRcd(ref alrcd, BriName, cla, loc, det, "钢筋", "HRB400", Q1 * 180, Q2,52,22);
                                WriteRcd(ref alrcd, BriName, "沥青涂装", loc, "桥墩", "沥青", "", (3.0+7.0) * 2 * 1, 1,84,37);
                            }
                            else if (PierHClear>=25)
                            {
                                if (BTlist[j]>=BeamType.B50 |BTlist[j-1]>=BeamType.B50)
                                {
                                    cla = "空心墩2.8x7.0";                                    
                                    det = "-";
                                    double ht =4.5;
                                    Q1 = ht * 19.6 + (PierHClear - ht) * 10.24;
                                    Q2 = PierHClear;
                                    WriteRcd(ref alrcd, BriName, cla, loc, det, "混凝土", "C40", Q1, Q2,23,15);
                                    WriteRcd(ref alrcd, BriName, cla, loc, det, "钢筋", "HRB400", Q1 * 180, Q2,51,22);
                                    WritePC4(ref alrcd, BriName, loc);
                                    WriteRcd(ref alrcd, BriName, "沥青涂装", loc, "桥墩", "沥青", "", (2.8 + 7.0) * 2 * 1, 1,84,37);
                                }
                                else if (BridgeWI>=1700)
                                {
                                    cla = "空心墩2.5x7.0";                                    
                                    det = "-";
                                    double ht = 3.5;
                                    CapBeamH =2.5;
                                    Q1 = ht * 17.5 + (PierHClear - ht- CapBeamH) * 10.04;
                                    Q2 = PierHClear;
                                    WriteRcd(ref alrcd, BriName, cla, loc, det, "混凝土", "C40", Q1, Q2,23,15);
                                    WriteRcd(ref alrcd, BriName, cla, loc, det, "钢筋", "HRB400", Q1 * 180, Q2,51,22);
                                    CapBeamT = 2.9;
                                    CapBeamL = BridgeWI == 1815 ? 17.5 : BridgeWI == 1715 ? 16.5 : BridgeWI == 1465 ? 14 : 13;
                                    double VolofCB = CapBeamT * CapBeamL * CapBeamH;
                                    WriteRcd(ref alrcd, BriName, "盖梁", loc, det, "混凝土", "C40", VolofCB, 1,25,16);
                                    WriteRcd(ref alrcd, BriName, "盖梁", loc, det, "钢筋", "HRB400", VolofCB * 180, 1,53,22);
                                    WritePC3(ref alrcd, BriName, loc);
                                    WriteRcd(ref alrcd, BriName, "沥青涂装", loc, "桥墩", "沥青", "", (2.5+ 7.0) * 2 * 1, 1,84,37);
                                }
                                else
                                {
                                    cla = "空心墩2.0x7.0";                                    
                                    det = "-";
                                    double ht = 3.5;
                                    CapBeamH = 2.3;
                                    Q1 = ht * 14 + (PierHClear - ht- CapBeamH) * 9.28;
                                    Q2 = PierHClear;
                                    WriteRcd(ref alrcd, BriName, cla, loc, det, "混凝土", "C40", Q1, Q2,23,15);
                                    WriteRcd(ref alrcd, BriName, cla, loc, det, "钢筋", "HRB400", Q1 * 180, Q2,51,22);
                                    CapBeamT = 2.4;
                                    CapBeamL = BridgeWI == 1815 ? 17.5 : BridgeWI == 1715 ? 16.5 : BridgeWI == 1465 ? 14 : 13;
                                    double VolofCB = CapBeamT * CapBeamL * CapBeamH;
                                    WriteRcd(ref alrcd, BriName, "盖梁", loc, det, "混凝土", "C40", VolofCB, 1,25,16);
                                    WriteRcd(ref alrcd, BriName, "盖梁", loc, det, "钢筋", "HRB400", VolofCB * 180, 1,53,22);
                                    WritePC3(ref alrcd, BriName, loc);
                                    WriteRcd(ref alrcd, BriName, "沥青涂装", loc, "桥墩", "沥青", "", (2.0 + 7.0) * 2 * 1, 1,84,37);
                                }

                            }
                            else if (PierHClear>=10)
                            {
                                if (BTlist[j] >= BeamType.B50 | BTlist[j - 1] >= BeamType.B50)
                                {
                                    cla = "实心墩2.2x7.0";                                    
                                    det = "-";                                    
                                    Q1 = PierHClear * 15.4;
                                    Q2 = PierHClear;
                                    WriteRcd(ref alrcd, BriName, cla, loc, det, "混凝土", "C40", Q1, Q2,19,15);
                                    WriteRcd(ref alrcd, BriName, cla, loc, det, "钢筋", "HRB400", Q1 * 160, Q2,47,22);
                                    WritePC3(ref alrcd, BriName, loc);
                                    WriteRcd(ref alrcd, BriName, "沥青涂装", loc, "桥墩", "沥青", "", (2.2 + 7.0) * 2 * 1, 1,84,37);
                                }
                                else if (BTlist[j] >= BeamType.T35 | BTlist[j - 1] >= BeamType.T35)
                                {
                                    if (BridgeWI >= 1700)
                                    {
                                        CapBeamH = 2.5;
                                    }
                                    else
                                    {
                                        CapBeamH = 2.3;
                                    }
                                    cla = "实心墩1.8x7.0";                                    
                                    det = "-";
                                    Q1 = (PierHClear-CapBeamH) * 12.6;
                                    Q2 = PierHClear;
                                    WriteRcd(ref alrcd, BriName, cla, loc, det, "混凝土", "C40", Q1, Q2,19,15);
                                    WriteRcd(ref alrcd, BriName, cla, loc, det, "钢筋", "HRB400", Q1 * 160, Q2,47,22);
                                    CapBeamT = 2.2;
                                    CapBeamL = BridgeWI == 1815 ? 17.5 : BridgeWI == 1715 ? 16.5 : BridgeWI == 1465 ? 14 : 13;
                                    double VolofCB = CapBeamT * CapBeamL * CapBeamH;
                                    WriteRcd(ref alrcd, BriName, "盖梁", loc, det, "混凝土", "C40", VolofCB, 1,25,16);
                                    WriteRcd(ref alrcd, BriName, "盖梁", loc, det, "钢筋", "HRB400", VolofCB * 180, 1,53,22);
                                    WritePC3(ref alrcd, BriName, loc);
                                    WriteRcd(ref alrcd, BriName, "沥青涂装", loc, "桥墩", "沥青", "", (1.8 + 7.0) * 2 * 1, 1,84,37);
                                }
                                else
                                {
                                    cla = "实心墩1.8x7.0";                                    
                                    det = "-";
                                    CapBeamH = 2.0;
                                    Q1 = (PierHClear- CapBeamH) * 12.6;
                                    Q2 = PierHClear;
                                    WriteRcd(ref alrcd, BriName, cla, loc, det, "混凝土", "C40", Q1, Q2,19,15);
                                    WriteRcd(ref alrcd, BriName, cla, loc, det, "钢筋", "HRB400", Q1 * 160, Q2,47,22);
                                    CapBeamT = 2.4;
                                    CapBeamL = BridgeWI == 1815 ? 17.5 : BridgeWI == 1715 ? 16.5 : BridgeWI == 1465 ? 14 : 13;
                                    double VolofCB = CapBeamT * CapBeamL * CapBeamH;
                                    WriteRcd(ref alrcd, BriName, "盖梁", loc, det, "混凝土", "C40", VolofCB, 1,25,16);
                                    WriteRcd(ref alrcd, BriName, "盖梁", loc, det, "钢筋", "HRB400", VolofCB * 180, 1,53,22);
                                    WritePC2(ref alrcd, BriName, loc);
                                    WriteRcd(ref alrcd, BriName, "沥青涂装", loc, "桥墩", "沥青", "", (1.8+ 7.0) * 2 * 1, 1,84,37);

                                }

                            }
                            else if(PierHClear>0)
                            {
                                cla = "柱式墩";
                                CapBeamH = 1.7;
                                Q1 = (PierHClear - CapBeamH) * 0.78;
                                if (Q1<0)
                                {
                                    Q1 = 0;
                                }
                                Q2 = PierHClear;
                                WriteRcd(ref alrcd, BriName, cla, loc, "1#", "混凝土", "C40", Q1, Q2,17,15);
                                WriteRcd(ref alrcd, BriName, cla, loc, "1#", "钢筋", "HRB400", Q1 * 160, Q2,45,22);
                                WriteRcd(ref alrcd, BriName, cla, loc, "2#", "混凝土", "C40", Q1, Q2,17,15);
                                WriteRcd(ref alrcd, BriName, cla, loc, "2#", "钢筋", "HRB400", Q1 * 160, Q2,45,22);
                                WriteRcd(ref alrcd, BriName, cla, loc, "3#", "混凝土", "C40", Q1, Q2,17,15);
                                WriteRcd(ref alrcd, BriName, cla, loc, "3#", "钢筋", "HRB400", Q1 * 160, Q2,45,22);
                                CapBeamT = 1.6;
                                CapBeamL = BridgeWI == 1815 ? 17.5 : BridgeWI == 1715 ? 16.5 : BridgeWI == 1465 ? 14 : 13;
                                double VolofCB = CapBeamT * CapBeamL * CapBeamH;
                                WriteRcd(ref alrcd, BriName, "盖梁", loc, det, "混凝土", "C40", VolofCB, 1,25,16);
                                WriteRcd(ref alrcd, BriName, "盖梁", loc, det, "钢筋", "HRB400", VolofCB * 180, 1,53,22);
                                WritePC1(ref alrcd, BriName, loc);
                                WriteRcd(ref alrcd, BriName, "沥青涂装", loc, "桥墩", "沥青", "", Math.PI*1.0 * 1, 1,84,37);


                            }
                            else
                            {
                                //WriteRcd(ref alrcd, BriName, "挖方墩台", loc, det, "混凝土", "C40", 0,0);
                                CapBeamH = 1.7;
                                CapBeamT = 1.6;
                                CapBeamL = BridgeWI == 1815 ? 17.5 : BridgeWI == 1715 ? 16.5 : BridgeWI == 1465 ? 14 : 13;
                                double VolofCB = CapBeamT * CapBeamL * CapBeamH;
                                WriteRcd(ref alrcd, BriName, "盖梁", loc, det, "混凝土", "C40", VolofCB, 1,25,16);
                                WriteRcd(ref alrcd, BriName, "盖梁", loc, det, "钢筋", "HRB400", VolofCB * 180, 1,53,22);
                            }



                        }





                    }
                }
                

            }



        }

        
        public static void Auxiliary(ref DataTable alrcd, ref DataTable BoB)
        {
            for (int i = 0; i < BoB.Rows.Count; i++)
            {
                DataRow data = BoB.Rows[i];
                string BriName = (string)data["PK"];
                string LR = (string)data["W"];
                int FuShu = LR == "LR" ? 2 : 1;
                int KuaShu = ((List<BeamType>)data["BeamTypes"]).Count();
                List<int> BridgeW = (List<int>)data["Width"];
                double Width = LR == "LR" ? (BridgeW[0] + BridgeW[1]) * 0.01 : LR == "L" ? BridgeW[0]*0.01 : BridgeW[1]*0.01;
                int GroupNum = (int)data["GroupNum"];
                double E26 = KuaShu * 25 * FuShu *0.1;
                double mfcl=LR=="LR"? (4 * KuaShu * 25 + 9 * 2 * 2) * 2: 4 * KuaShu * 25 + 9 * 2 * 4;
                mfcl += Width * (GroupNum + 1) * 2 + 1.6 * E26;
                int ExpandJointToCount = (int)data["GroupNum"] + 1;
                double Q2 = 0;
                List<BeamType> BTlist = (List<BeamType>)data["BeamTypes"];
                Q2 = ExpandJointToCount;

                foreach (var item in (string)data["W"])
                {                    
                    string loc = item.ToString();
                    int BridgeWI = item=='L' ? BridgeW[0] : BridgeW[1];
                    double RoadW = BridgeWI * 0.01 - 3.15;
                    double PlateW = BridgeWI * 0.01 - 0.70;

                    for (int j = 0; j < BTlist.Count; j++)
                    {
                        BeamType btitem = BTlist[j];
                        int BeamNum = Ext.CountBeams(BridgeWI, btitem);
                        if (btitem == BeamType.B60)
                        {
                            WriteRcd(ref alrcd, BriName, "支座", loc, "", "箱梁支座", "JBZD600×700×172", 4, 1,66,32);
                            WriteRcd(ref alrcd, BriName, "支座", loc, "", "箱梁支座", "JBZC900×900×202", 4, 1,67,33);
                            WriteRcd(ref alrcd, BriName, "桥面铺装", loc, "", "沥青混凝土", "", RoadW * 130, 1,79,38);
                            WriteRcd(ref alrcd, BriName, "桥面铺装", loc, "", "防水层", "", RoadW * 130, 1,80,42);
                            WriteRcd(ref alrcd, BriName, "泄水管", loc, "", "铸铁", "", 13, 1,74,29);
                            WriteRcd(ref alrcd, BriName, "排水管", loc, "", "PVC?", "", 130, 1,75,41);
                            WriteRcd(ref alrcd, BriName, "行车道护栏", loc, "", "钢材", "", 130 * 2, 1,77,31);
                            WriteRcd(ref alrcd, BriName, "路缘石", loc, "", "路缘石", "", 130 * 2, 1,88,35);
                            WriteRcd(ref alrcd, BriName, "人行道护栏", loc, "", "钢材", "", 130 * 30, 1,76,30);
                            WriteRcd(ref alrcd, BriName, "人行道板", loc, "", "混凝土", "MB40", 130 * 2 * 0.485, 1,82,18);
                            WriteRcd(ref alrcd, BriName, "人行道PVC", loc, "", "PVC", "", 130 * 8, 1,89,40);
                            j += 2;
                        }
                        else if (btitem == BeamType.B50)
                        {
                            if (j == 0)
                            {
                                WriteRcd(ref alrcd, BriName, "支座", loc, "", "箱梁支座", "JBZD600×700×172", 2, 1,66,32);
                                WriteRcd(ref alrcd, BriName, "支座", loc, "", "箱梁支座", "JBZC900×900×202", 2, 1,67,33);
                            }
                            else if(j==BTlist.Count-1)
                            {
                                WriteRcd(ref alrcd, BriName, "支座", loc, "", "箱梁支座", "JBZD600×700×172", 2, 1,66,32);
                            }
                            else
                            {
                                WriteRcd(ref alrcd, BriName, "支座", loc, "", "箱梁支座", "JBZC900×900×202", 2, 1,67,33);
                            }
                            WriteRcd(ref alrcd, BriName, "桥面铺装", loc, "", "沥青混凝土", "", RoadW * 50 , 1,79,38);
                            WriteRcd(ref alrcd, BriName, "桥面铺装", loc, "", "防水层", "", RoadW * 50, 1,80,42);
                            WriteRcd(ref alrcd, BriName, "泄水管", loc, "", "铸铁", "", 5, 1,74,29);
                            WriteRcd(ref alrcd, BriName, "排水管", loc, "", "PVC?", "", 50, 1,75,41);
                            WriteRcd(ref alrcd, BriName, "行车道护栏", loc, "", "钢材", "",50 * 2, 1,77,31);
                            WriteRcd(ref alrcd, BriName, "路缘石", loc, "", "路缘石", "", 50* 2, 1,88,35);
                            WriteRcd(ref alrcd, BriName, "人行道护栏", loc, "", "钢材", "", 50* 30, 1,76,30);
                            WriteRcd(ref alrcd, BriName, "人行道板", loc, "", "混凝土", "MB40", 50 * 2 * 0.485, 1,82,18);
                            WriteRcd(ref alrcd, BriName, "人行道PVC", loc, "", "PVC", "",50* 8, 1,89,40);
                        }
                        else if (btitem == BeamType.T35)
                        {
                            WriteRcd(ref alrcd, BriName, "支座", loc, "", "T梁支座", "RN400", BeamNum*2, 1,86,34);
                            WriteRcd(ref alrcd, BriName, "桥面板", loc, "", "混凝土", "MB45", PlateW * 35.0*0.25 , 1,81,17);
                            WriteRcd(ref alrcd, BriName, "桥面板", loc, "", "钢筋", "B500B", PlateW * 35.0 * 0.25 * 200.0, 1,90,22);
                            WriteRcd(ref alrcd, BriName, "桥面铺装", loc, "", "沥青混凝土", "", RoadW * 35 , 1,79,38);
                            WriteRcd(ref alrcd, BriName, "桥面铺装", loc, "", "防水层", "", RoadW * 35, 1,80,42);
                            WriteRcd(ref alrcd, BriName, "泄水管", loc, "", "铸铁", "", 3.5, 1,74,29);
                            WriteRcd(ref alrcd, BriName, "排水管", loc, "", "PVC?", "", 35, 1,75,41);
                            WriteRcd(ref alrcd, BriName, "行车道护栏", loc, "", "钢材", "",35 * 2, 1,77,31);
                            WriteRcd(ref alrcd, BriName, "路缘石", loc, "", "路缘石", "", 35 * 2, 1,88,35);
                            WriteRcd(ref alrcd, BriName, "人行道护栏", loc, "", "钢材", "", 35 * 30, 1,76,30);
                            WriteRcd(ref alrcd, BriName, "人行道板", loc, "", "混凝土", "MB40", 35 * 2 * 0.485, 1,82,18);
                            WriteRcd(ref alrcd, BriName, "人行道PVC", loc, "", "PVC", "", 35 * 8, 1,89,40);
                        }
                        else if (btitem == BeamType.T25)
                        {
                            WriteRcd(ref alrcd, BriName, "支座", loc, "", "T梁支座", "RN200", BeamNum * 2, 1,86,34);
                            WriteRcd(ref alrcd, BriName, "桥面板", loc, "", "混凝土", "MB45", PlateW * 25.0 * 0.25, 1,81,17);
                            WriteRcd(ref alrcd, BriName, "桥面板", loc, "", "钢筋", "B500B", PlateW * 25.0 * 0.25*200.0, 1,90,22);
                            WriteRcd(ref alrcd, BriName, "桥面铺装", loc, "", "沥青混凝土", "", RoadW * 25 , 1,79,38);
                            WriteRcd(ref alrcd, BriName, "桥面铺装", loc, "", "防水层", "", RoadW * 25, 1,80,42);
                            WriteRcd(ref alrcd, BriName, "泄水管", loc, "", "铸铁","", 2.5, 1,74,29);
                            WriteRcd(ref alrcd, BriName, "排水管", loc, "", "PVC?", "", 25, 1,75,41);
                            WriteRcd(ref alrcd, BriName, "行车道护栏", loc, "", "钢材", "", 25*2, 1,77,31);
                            WriteRcd(ref alrcd, BriName, "路缘石", loc, "", "路缘石", "", 25 * 2, 1,88,35);
                            WriteRcd(ref alrcd, BriName, "人行道护栏", loc, "", "钢材", "", 25 *30, 1,76,30);
                            WriteRcd(ref alrcd, BriName, "人行道板", loc, "", "混凝土", "MB40", 25 *2*0.485, 1,82,18);
                            WriteRcd(ref alrcd, BriName, "人行道PVC", loc, "", "PVC", "", 25 * 8, 1,89,40);

                        }
                        else
                        {
                            double LL = 0;
                            if (btitem == BeamType.F10)
                            {
                                LL = 10.0;
                            }
                            else
                            {
                                LL = 15.0;
                            }

                            // 框架上部
                            WriteRcd(ref alrcd, BriName, "桥面铺装", loc, "", "沥青混凝土", "", RoadW * LL , 1,79,38);
                            WriteRcd(ref alrcd, BriName, "桥面铺装", loc, "", "防水层", "", RoadW * LL, 1,80,42);
                            WriteRcd(ref alrcd, BriName, "泄水管", loc, "", "铸铁", "", LL * 0.1, 1,74,29);
                            WriteRcd(ref alrcd, BriName, "排水管", loc, "", "PVC?", "", LL, 1,75,41);
                            WriteRcd(ref alrcd, BriName, "行车道护栏", loc, "", "钢材", "", LL , 1,77,31);
                            WriteRcd(ref alrcd, BriName, "路缘石", loc, "", "路缘石", "", LL * 2, 1,88,35);
                            WriteRcd(ref alrcd, BriName, "人行道护栏", loc, "", "钢材", "", LL * 30, 1,76,30);
                            WriteRcd(ref alrcd, BriName, "人行道板", loc, "", "混凝土", "MB40", LL * 2 * 0.485, 1,82,18);
                            WriteRcd(ref alrcd, BriName, "人行道PVC", loc, "", "PVC", "", LL * 4, 1,89,40);

                        }
                    }
                }

                WriteRcd(ref alrcd, BriName, "接头密封处理",LR, "-", "-", "-", mfcl, 1,91,39);

                string ExtJonitString = "";
                List<BeamType> GroupTys = (List<BeamType>)data["GroupTypes"];

                if (GroupTys[0]<=BeamType.F15)
                {
                    continue;
                }
                else if (double.Parse((string)data["L"]) <= 105)
                {
                    WriteRcd(ref alrcd, BriName, "伸缩缝", LR, "-", "MT-100", "-", Width, 1,68,26);
                    WriteRcd(ref alrcd, BriName, "伸缩缝", LR, "-", "MT-100", "-", Width, 1,68,26);
                }
                    
                else
                {
                    int xmh,newid;
                    for (int jj = 0; jj < GroupTys.Count + 1; jj++)
                    {
                        if (jj == 0)
                        {
                            ExtJonitString = GroupTys[0] == BeamType.T25 ? "MT-100" : "MT-240";
                            xmh = GroupTys[0] == BeamType.T25 ? 68 : 71;
                            newid = GroupTys[0] == BeamType.T25 ? 26 : 27;
                        }
                        else if (jj==GroupTys.Count)
                        {
                            ExtJonitString = GroupTys.Last() == BeamType.T25 ? "MT-100" : "MT-240";
                            xmh = GroupTys[0] == BeamType.T25 ? 68 : 71;
                            newid = GroupTys[0] == BeamType.T25 ? 26 : 27;
                        }
                        else
                        {
                            if (GroupTys[jj]==BeamType.T25& GroupTys[jj-1] == BeamType.T25)
                            {
                                ExtJonitString = "MT-240";
                                xmh = 71;
                                newid = 27;
                            }
                            else
                            {
                                ExtJonitString = "MT-400";
                                xmh = 73;
                                newid = 28;
                            }                            
                        }
                        WriteRcd(ref alrcd, BriName, "伸缩缝", LR, "-",ExtJonitString, "-",Width, 1,xmh,newid);
                    }
                }

                

            }
        }


        /// <summary>
        /// 写入记录
        /// </summary>
        /// <param name="alrcd"></param>
        /// <param name="BriName"></param>
        /// <param name="cla"></param>
        /// <param name="loc"></param>
        /// <param name="det"></param>
        /// <param name="mname"></param>
        /// <param name="spec"></param>
        /// <param name="Q1"></param>
        /// <param name="Q2"></param>
        public static void WriteRcd(ref DataTable alrcd,
            string BriName,string cla,string loc, string det, string mname,string spec,
            double Q1,double Q2,int xmh,int NewID)
        {
            DataRow newRow = alrcd.NewRow();
            newRow["bridge"] = BriName;
            newRow["class"] = cla;
            newRow["loc"] = loc;
            newRow["detial"] = det;
            newRow["name"] = mname;
            newRow["spec"] = spec;
            newRow["quantity"] = Q1;
            newRow["quantity2"] = Q2;
            newRow["xmh"] = xmh;
            newRow["NewID"] = NewID;
            alrcd.Rows.Add(newRow);
        }

        static void WritePC1(ref DataTable alrcd, string BriName, string loc)
        {
            // PC1 ==================================================================================
            WriteRcd(ref alrcd, BriName, "承台", loc, "", "混凝土", "C30", 34.56, 1,13,13);
            WriteRcd(ref alrcd, BriName, "承台", loc, "", "钢筋", "HRB400", 34.56 * 140, 1,41,22);
            WriteRcd(ref alrcd, BriName, "承台", loc, "", "混凝土", "C15", 2.6, 1,7,7);
            double LZ = WXS.GetLZ(Ext.PK2NUM(BriName));
            double Ld = 1.2;
            double VV = Ld * Ld * 0.25 * Math.PI * LZ;
            int NumPile = 4;
            for (int k = 0; k < NumPile; k++)
            {
                string detial = string.Format("{0}#", k + 1);
                WriteRcd(ref alrcd, BriName, "桩基D120", loc, detial, "混凝土", "C30", LZ, VV,8,8);
                WriteRcd(ref alrcd, BriName, "桩基D120", loc, detial, "钢筋", "HRB400", VV * 120, 1,36,23);
            }
            double Vw, Vb;
            WXS.GetV(out Vw, out Vb,1.8, 12.8, 1.5 + 0.1, 1.5 + 0.6, 1.5 + 1);
            WriteRcd(ref alrcd, BriName, "基础开挖", loc, "", "土石方", "", Vw, 1,1,1);
            WriteRcd(ref alrcd, BriName, "基础回填", loc, "", "土石方", "", Vb, 1,2,2);
            WriteRcd(ref alrcd, BriName, "沥青涂装", loc, "承台", "沥青", "", (1.8 + 12.8) * 2 * 1.5 + 1.8 * 12.8, 1,84,37);
            // End of PC1 ============================================================================
        }
        static void WritePC2(ref DataTable alrcd, string BriName, string loc)
        {
            // PC2 ==================================================================================
            WriteRcd(ref alrcd, BriName, "承台", loc, "", "混凝土", "C30", 46.656, 1,13,13);
            WriteRcd(ref alrcd, BriName, "承台", loc, "", "钢筋", "HRB400", 46.656 * 140, 1,41,22);
            WriteRcd(ref alrcd, BriName, "承台", loc, "", "混凝土", "C15", 2.812, 1,7,7);
            double LZ = WXS.GetLZ(Ext.PK2NUM(BriName));
            double Ld = 1.2;
            double VV = Ld * Ld * 0.25 * Math.PI * LZ;
            int NumPile = 6;
            for (int k = 0; k < NumPile; k++)
            {
                string detial = string.Format("{0}#", k + 1);
                WriteRcd(ref alrcd, BriName, "桩基D120", loc, detial, "混凝土", "C30", LZ, VV,8,8);
                WriteRcd(ref alrcd, BriName, "桩基D120", loc, detial, "钢筋", "HRB400", VV * 120, 1,36,23);
            }
            double Vw, Vb;
            WXS.GetV(out Vw, out Vb, 6, 9.6, 1.8 + 0.1, 1.8 + 0.6, 1.8 + 1);
            WriteRcd(ref alrcd, BriName, "基础开挖", loc, "", "土石方", "", Vw, 1,1,1);
            WriteRcd(ref alrcd, BriName, "基础回填", loc, "", "土石方", "", Vb, 1,2,2);
            WriteRcd(ref alrcd, BriName, "沥青涂装", loc, "承台", "沥青", "", (6+9.6) * 2 * 1.8 +9.6 * 6.0, 1,84,37);
            // End of PC2 ============================================================================
        }
        /// <summary>
        /// 输出PC3基础
        /// </summary>
        /// <param name="alrcd"></param>
        /// <param name="BriName"></param>
        /// <param name="loc"></param>
        static void WritePC3(ref DataTable alrcd,string BriName,string loc)
        {            
            WriteRcd(ref alrcd, BriName, "承台", loc, "", "混凝土", "C30", 72.9, 1,13,13);
            WriteRcd(ref alrcd, BriName, "承台", loc, "", "钢筋", "HRB400", 72.9 * 140, 1,41,22);
            WriteRcd(ref alrcd, BriName, "承台", loc, "", "混凝土", "C15", 3.136, 1,7,7);
            double LZ = WXS.GetLZ(Ext.PK2NUM(BriName));
            double Ld = 1.8;
            double VV = Ld * Ld * 0.25 * Math.PI * LZ;
            int NumPile = 4;
            for (int k = 0; k < NumPile; k++)
            {
                string detial = string.Format("{0}#", k + 1);
                WriteRcd(ref alrcd, BriName, "桩基D180", loc, detial, "混凝土", "C30", LZ, VV,10,10);
                WriteRcd(ref alrcd, BriName, "桩基D180", loc, detial, "钢筋", "HRB400", VV * 120, 1,38,23);
            }
            double Vw, Vb;
            WXS.GetV(out Vw, out Vb, 9, 9, 2.5 + 0.1, 2.5 + 0.6, 2.5 + 1);
            WriteRcd(ref alrcd, BriName, "基础开挖", loc, "", "土石方", "", Vw, 1,1,1);
            WriteRcd(ref alrcd, BriName, "基础回填", loc, "", "土石方", "", Vb, 1,2,2);
            WriteRcd(ref alrcd, BriName, "沥青涂装", loc, "承台", "沥青", "", 9 * 4 * 2.5 + 9.0 * 9.0, 1,84,37);            
        }
        /// <summary>
        /// 输出PC4基础
        /// </summary>
        /// <param name="alrcd"></param>
        /// <param name="BriName"></param>
        /// <param name="loc"></param>
        static void WritePC4(ref DataTable alrcd, string BriName, string loc)
        {
            WriteRcd(ref alrcd, BriName, "承台", loc, "", "混凝土", "C30", 108, 1,13,13);
            WriteRcd(ref alrcd, BriName, "承台", loc, "", "钢筋", "HRB400", 108 * 140, 1,41,22);
            WriteRcd(ref alrcd, BriName, "承台", loc, "", "混凝土", "C15", 3.844, 1,7,7);
            double LZ = WXS.GetLZ(Ext.PK2NUM(BriName));
            double Ld = 2;
            double VV = Ld * Ld * 0.25 * Math.PI * LZ;
            int NumPile = 4;
            for (int k = 0; k < NumPile; k++)
            {
                string detial = string.Format("{0}#", k + 1);
                WriteRcd(ref alrcd, BriName, "桩基D200", loc, detial, "混凝土", "C30", LZ, VV,11,11);
                WriteRcd(ref alrcd, BriName, "桩基D200", loc, detial, "钢筋", "HRB400", VV * 120, 1,39,23);
            }
            double Vw, Vb;
            WXS.GetV(out Vw, out Vb, 10, 10, 3 + 0.1, 3 + 0.6, 3 + 1);
            WriteRcd(ref alrcd, BriName, "基础开挖", loc, "", "土石方", "", Vw, 1,1,1);
            WriteRcd(ref alrcd, BriName, "基础回填", loc, "", "土石方", "", Vb, 1,2,2);
            WriteRcd(ref alrcd, BriName, "沥青涂装", loc, "承台", "沥青", "", 10 * 4 * 3.0 + 10.0 * 10.0, 1,84,37);
        }
        /// <summary>
        /// 输出PC5基础
        /// </summary>
        /// <param name="alrcd"></param>
        /// <param name="BriName"></param>
        /// <param name="loc"></param>
        static void WritePC5(ref DataTable alrcd, string BriName, string loc)
        {
            // PC5 ==================================================================================
            WriteRcd(ref alrcd, BriName, "承台", loc, "", "混凝土", "C30", 130.68, 1,13,13);
            WriteRcd(ref alrcd, BriName, "承台", loc, "", "钢筋", "HRB400", 130.68 * 140, 1,41,22);
            WriteRcd(ref alrcd, BriName, "承台", loc, "", "混凝土", "C15", 4.624, 1,7,7);
            double LZ = WXS.GetLZ(Ext.PK2NUM(BriName));
            double Ld = 2.2;
            double VV = Ld * Ld * 0.25 * Math.PI * LZ;
            int NumPile = 4;
            for (int k = 0; k < NumPile; k++)
            {
                string detial = string.Format("{0}#", k + 1);
                WriteRcd(ref alrcd, BriName, "桩基D220", loc, detial, "混凝土", "C30", LZ, VV,12,12);
                WriteRcd(ref alrcd, BriName, "桩基D220", loc, detial, "钢筋", "HRB400", VV * 120, 1,40,23);
            }
            double Vw, Vb;
            WXS.GetV(out Vw, out Vb, 11, 11, 3 + 0.1, 3 + 0.6, 3 + 1);
            WriteRcd(ref alrcd, BriName, "基础开挖", loc, "", "土石方", "", Vw, 1,1,1);
            WriteRcd(ref alrcd, BriName, "基础回填", loc, "", "土石方", "", Vb, 1,2,2);
            WriteRcd(ref alrcd, BriName, "沥青涂装", loc, "承台", "沥青", "", 11 * 4 * 3.0 + 11.0 * 11.0, 1,84,37);
            // End of PC5 ============================================================================  
        }

        /// <summary>
        /// 输出桥台
        /// </summary>
        /// <param name="alrcd">总记录表</param>
        /// <param name="data">当前数据行</param>
        /// <param name="Side">当前侧</param>                
        /// <param name="jj">桩号索引</param>                
        static void WriteAbutment(ref DataTable alrcd,ref DataRow data,string Side,int jj)
        {
            string BriName = (string)data["PK"];
            List<double> dmx = (List<double>)data["DMX"];
            List<double> sjx = (List<double>)data["SJX"];
            List<BeamType> BTlist = (List<BeamType>)data["BeamTypes"];
            List<int> BridgeW = (List<int>)data["Width"];
            int BridgeWI = Side=="L" ? BridgeW[0] : BridgeW[1];
            double AbutW = BridgeWI * 0.01 - 0.7;
            double Embarkment = sjx[jj] - dmx[jj];
            BeamType curBT = jj == 0 ? BTlist[0] : BTlist.Last();
            double BackWallH = BeamHeight[curBT];

            double AbutWallH = Embarkment - BackWallH;
            if (AbutWallH<1.0)
            {
                AbutWallH = 1.0;
            }
            double AbutWallT = curBT <= BeamType.T35 ? 1.8 : 2.5;
            double EmbarInFact = AbutWallH + BackWallH;
            double WingWallA = 0;


            if (AbutWallH>=8)
            {
                WingWallA = EmbarInFact * 1.55;
            }
            else if (AbutWallH >= 5)
            {
                WingWallA = ((BackWallH + 1.8) * 1.5 + 0.7 - AbutWallT) + EmbarInFact * 1.55 + Math.Pow((BackWallH + 1.8) * 1.5 - AbutWallT, 2) / 1.5 * 0.5;
            }
            else
            {
                WingWallA = (EmbarInFact * 1.5 + 0.7 - AbutWallT) + EmbarInFact * 1.55 + Math.Pow(EmbarInFact * 1.5 - AbutWallT, 2) / 1.5 * 0.5;
            }
            WriteRcd(ref alrcd, BriName, "背墙", Side, "", "混凝土", "C35", BackWallH * 0.65 * AbutW, 1,15,16);
            WriteRcd(ref alrcd, BriName, "背墙", Side, "", "钢筋", "HRB400", BackWallH * 0.65 * AbutW * 120, 1,43,22);
            WriteRcd(ref alrcd, BriName, "台身", Side, "", "混凝土", "C35", AbutWallH * AbutWallT * AbutW, 1,14,14);
            WriteRcd(ref alrcd, BriName, "台身", Side, "", "钢筋", "HRB400", AbutWallH * AbutWallT * AbutW * 120, 1,42,22);
            WriteRcd(ref alrcd, BriName, "翼墙", Side, "", "混凝土", "C35", WingWallA * 0.5 , 1,15,16);
            WriteRcd(ref alrcd, BriName, "翼墙", Side, "", "钢筋", "HRB400", WingWallA * 0.5 * 120, 1,43,22);
            WriteRcd(ref alrcd, BriName, "翼墙", Side, "", "混凝土", "C35", WingWallA * 0.5, 1,15,16);
            WriteRcd(ref alrcd, BriName, "翼墙", Side, "", "钢筋", "HRB400", WingWallA * 0.5 * 120, 1,43,22);
            WriteRcd(ref alrcd, BriName, "承台", Side, "", "混凝土", "C35", 1.6 *5.6 * AbutW, 1,13,13);
            WriteRcd(ref alrcd, BriName, "承台", Side, "", "混凝土", "C15", 0.1 * 5.8 * (AbutW+0.2), 1,7,7);
            WriteRcd(ref alrcd, BriName, "承台", Side, "", "钢筋", "HRB400", 1.6 * 5.6 * AbutW * 140, 1,41,22);
            WriteRcd(ref alrcd, BriName, "搭板", Side, "", "混凝土", "C35", 0.3 * 4.5 * AbutW * 1, 1,16,20);
            WriteRcd(ref alrcd, BriName, "搭板", Side, "", "混凝土", "C15", 0.1 * (4.5 + 0.2) * (AbutW + 0.2) * 1, 1,7,7);
            WriteRcd(ref alrcd, BriName, "搭板", Side, "", "钢筋", "HRB400", 0.3 * 4.5 * AbutW * 150, 1,44,22);
            WriteRcd(ref alrcd, BriName, "台背回填", Side, "", "", "", (1.75+EmbarInFact*0.5+1.75)*EmbarInFact*0.5* AbutW , 1,3,3);
            double Vw, Vb;
            WXS.GetV(out Vw, out Vb, 5.6, AbutW, 1.6 + 0.1, 1.6 + 0.6, 1.6 + 1);
            WriteRcd(ref alrcd, BriName, "基础开挖", Side, "", "土石方", "", Vw, 1, 1, 1);
            WriteRcd(ref alrcd, BriName, "基础回填", Side, "", "土石方", "", Vb, 1, 2, 2);
            WriteRcd(ref alrcd, BriName, "沥青涂装", Side, "桥台", "沥青", "", (5.6+AbutW) * 2 * 1.6 + 5.6 * AbutW, 1, 84, 37);
            double CV, CC, CA;
            if (EmbarInFact<=5)
            {
                WXS.Cone(out CV, out CC, out CA, EmbarInFact * 1.5, EmbarInFact * 1.5, EmbarInFact);
            }
            else
            {
                WXS.Cone(out CV, out CC, out CA, EmbarInFact * 1.5, (EmbarInFact-5)*1.75+5 * 1.5, EmbarInFact);
            }
            WriteRcd(ref alrcd, BriName, "锥坡", Side, "锥坡土方", "土", "", 0.5 * CV, 1,4,4);
            WriteRcd(ref alrcd, BriName, "锥坡", Side, "锥坡护道", "", "", 0.5 * CC * 0.458, 1,6,6);
            WriteRcd(ref alrcd, BriName, "锥坡", Side, "锥坡护坡", "", "", 0.5 * CA, 1,5,5);


            double LZ = WXS.GetLZ(Ext.PK2NUM(BriName));
            double Ld = 1.2;
            double VV = Ld * Ld * 0.25 * Math.PI * LZ;
            int NumPile = 8;
            for (int k = 0; k < NumPile; k++)
            {
                string detial = string.Format("{0}#", k + 1);
                WriteRcd(ref alrcd, BriName, "桩基D120", Side, detial, "混凝土", "C30", LZ, VV,8,8);
                WriteRcd(ref alrcd, BriName, "桩基D120", Side, detial, "钢筋", "HRB400", VV * 120, 1,36,23);
            }

        }


        public static void AddInfo(this DataTable alrcd,ref DataTable LKBOB,ref DataTable RKBOB)
        {
            alrcd.Columns.Add("beamtypes", typeof(string));
            alrcd.Columns.Add("L", typeof(double));
            alrcd.Columns.Add("W", typeof(double));
            string pk;
            for (int i = 0; i < alrcd.Rows.Count; i++)
            {
                pk = alrcd.Rows[i]["bridge"].ToString();

                alrcd.Rows[i]["beamtypes"] = GetInfo(pk, 5, ref LKBOB, ref RKBOB);
                alrcd.Rows[i]["L"] = GetInfo(pk, 3, ref LKBOB, ref RKBOB);
                alrcd.Rows[i]["W"] = GetWidth(pk,  ref LKBOB, ref RKBOB);

            }





        }


        public static string GetWidth(string pkstring,  ref DataTable LKBOB, ref DataTable RKBOB)
        {
            var f1 = LKBOB.Select("PK='" + pkstring + "'");
            var f2 = RKBOB.Select("PK='" + pkstring + "'");
            if (f1.Count() != 0)
            {
                var ret =(List<int>) f1.First()[8] ;
                return ret[0].ToString()+ret[1].ToString();
            }
            else
            {
                var ret = (List<int>)f2.First()[8];
                return ret[0].ToString() + ret[1].ToString();
            }


        }


        public static string GetInfo(string pkstring,int n, ref DataTable LKBOB, ref DataTable RKBOB)
        {
            var f1 = LKBOB.Select("PK='" + pkstring + "'");
            var f2 = RKBOB.Select("PK='" + pkstring + "'");
            if (f1.Count() != 0)
            {
                return f1.First()[n].ToString();
            }
            else
            {
                return f2.First()[n].ToString();

            }


        }

    }
}
