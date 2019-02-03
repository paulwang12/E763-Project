using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E763_Project
{

    class Tbeam
    {

    }


    class SupStructure
    {
        public string BridgeName;
        public BeamType ClassName;
        public string Loc;
        public string Detial;
        public double BridgeWidth;

        public readonly double Q1, Q2;
        public readonly string XMH;
        public readonly double RebarRho;

        SupStructure(BeamType bt, double bw,string bn,string loc,string det)
        {
            BridgeName = bn;
            Loc = loc;
            Detial = det;
            ClassName = bt;
            BridgeWidth = bw;

            XMH = "-";
            switch (bt)
            {
                case BeamType.F10:
                    break;
                case BeamType.F15:
                    break;
                case BeamType.T25:
                    break;
                case BeamType.T35:
                    break;
                case BeamType.B50:
                    break;
                case BeamType.B60:
                    if (bw==1365)
                    {
                        Q1 = 9.6604 * 60;
                        Q2 = 1;
                        RebarRho = 190;
                    }
                    else if (bw == 1465)
                    {
                        Q1 = 10.0549 * 60;
                        Q2 = 1;
                        RebarRho = 190;
                    }




                    break;
                default:
                    break;
            }

        }



    }







}
