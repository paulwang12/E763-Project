using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E763_Project
{
    class PFoundation
    {
        readonly public double Ld, Td, H;
        readonly public double Dia;
        readonly public int PileNum;        
        readonly public double rhoPileCap, rhoPile;
        public double Lz
        {
            set;get;
        }
        public double Voc
        {
            get
            {
                return (Ld + 2 * 0.1) * (Td + 2 * 0.1) * 0.1;
            }
        }
        public double VolPileCap
        {
            get
            {
                return Ld * Td * H;
            }
        }
        public double VolOnePile
        {
            get
            {
                return Dia * Dia * 0.25 * Math.PI * Lz;
            }
        }
        public double RebarOnePile
        {
            get
            {
                return rhoPile * VolOnePile;
            }
        }
        public double RebarPileCap
        {
            get
            {
                return rhoPileCap * VolPileCap;
            }
        }



        public PFoundation(Foundation fname)
        {
            switch (fname)
            {
                case Foundation.PC1:
                    Ld = 1.8;
                    Td = 12.8;
                    H = 1.5;
                    Dia = 1.2;
                    PileNum = 4;
                    break;
                case Foundation.PC2:
                    Ld = 6;
                    Td = 9.6;
                    H = 1.8;
                    Dia = 1.2;
                    PileNum = 6;
                    break;
                case Foundation.PC3:
                    Ld = 9;
                    Td = 9;
                    H = 2.5;
                    Dia = 1.8;
                    PileNum = 4;
                    break;
                case Foundation.PC4:
                    Ld = 10;
                    Td = 10;
                    H = 3;
                    Dia = 2;
                    PileNum = 4;
                    break;
                case Foundation.PC5:
                    Ld = 11;
                    Td = 11;
                    H = 3;
                    Dia = 2.2;
                    PileNum = 4;
                    break;
                default:
                    Ld = 0;
                    Td = 0;
                    H = 0;
                    Dia = 0;
                    PileNum = 0;                   
                    
                    break;
            }
        }






        public void WriteRcd(ref DataTable rcdDT,string bridgename,string LR)
        {
            DataRow newrow;
            for (int i = 0; i < PileNum; i++)
            {
                newrow = rcdDT.NewRow();
                newrow["bridge"]=bridgename;
                newrow["class"]="桩基";
                newrow["loc"] = LR;
                newrow["detial"] = string.Format("{0}#", i + 1);
                newrow["name"]="桩长";
                newrow["spec"]=string.Format("D={0}cm",(int)(Dia*100));
                newrow["quantity"]=Lz;
                newrow["xmh"]="-";
                rcdDT.Rows.Add(newrow);

                newrow = rcdDT.NewRow();
                newrow["bridge"] = bridgename;
                newrow["class"] = "桩基";
                newrow["loc"] = LR;
                newrow["detial"] = string.Format("{0}#", i + 1);
                newrow["name"] = "钢筋";
                newrow["spec"] = "RA400/500-2";
                newrow["quantity"] = RebarOnePile;
                newrow["xmh"] = "-";
                rcdDT.Rows.Add(newrow);
            }
            newrow = rcdDT.NewRow();
            newrow["bridge"] = bridgename;
            newrow["class"] = "承台";
            newrow["loc"] = LR;
            newrow["detial"] = "-";
            newrow["name"] = "混凝土";
            newrow["spec"] = "C30";
            newrow["quantity"] =VolPileCap;
            newrow["xmh"] = "-";
            rcdDT.Rows.Add(newrow);

            newrow = rcdDT.NewRow();
            newrow["bridge"] = bridgename;
            newrow["class"] = "承台";
            newrow["loc"] = LR;
            newrow["detial"] = "-";
            newrow["name"] = "钢筋";
            newrow["spec"] = "RA400/500-2";
            newrow["quantity"] = RebarPileCap;
            newrow["xmh"] = "-";
            rcdDT.Rows.Add(newrow);

            newrow = rcdDT.NewRow();
            newrow["bridge"] = bridgename;
            newrow["class"] = "承台";
            newrow["loc"] = LR;
            newrow["detial"] = "-";
            newrow["name"] = "混凝土";
            newrow["spec"] = "C10";
            newrow["quantity"] = Voc;
            newrow["xmh"] = "-";
            rcdDT.Rows.Add(newrow);
        }


    }
}
