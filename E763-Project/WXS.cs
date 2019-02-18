using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E763_Project
{
    public static class WXS
    {


        public static double GetLZ(double pk)
        {
            if (pk <= 169938)
            {
                return 20;
            }
            else if (pk <= 173053)
            {
                return 15;

            }
            else if (pk <= 181837)
            {
                return 20;

            }
            else if (pk <= 216956)
            {
                return 15;

            }
            else if (pk <= 218742)
            {
                return 25;

            }
            else if (pk <= 223255)
            {
                return 15;

            }
            else
            {
                return 25;
            }
        }


        public static int GetRightW2(string pk)
        {
            int i = pk.IndexOf('K');
            string a = pk.Remove(i + 1);
            pk = pk.Replace(a, "");
            var ff = pk.Split('+');
            double pks = double.Parse(ff[0]) * 1000 + double.Parse(ff[1]);
            if (pks <= 164200)
            {
                if (((pks >= 151889) && (pks < 155701)) || ((pks >= 161245) && (pks < 163569)))
                {
                    return 1815;
                }
                else
                {
                    return 1465;
                }
            }
            else if (pks <= 222300)
            {
                if (((pks >= 164722) && (pks < 166558)) || ((pks >= 171812) && (pks < 174400)) || ((pks >= 176545) && (pks < 179767)) || ((pks >= 187691) && (pks < 190246.869)) || ((pks >= 191520) && (pks < 193075)) || ((pks >= 195015) && (pks < 196035)) || ((pks >= 196720) && (pks < 199628)) || ((pks >= 200710) && (pks < 206017)) || ((pks >= 207838) && (pks < 217472)))
                {
                    return 1715;
                }
                else
                {
                    return 1365;
                }
            }
            else
            {
                if (((pks >= 223516) && (pks < 225144)) || ((pks >= 226580) && (pks < 228052)) || ((pks >= 229615) && (pks < 231910)) || ((pks >= 234025) && (pks < 239482)) || ((pks >= 247966) && (pks < 252459)))
                {
                    return 1815;
                }
                else
                {
                    return 1465;
                }
            }
        }


        /// <summary>
        /// 方案2
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static int GetLeftW2(string pk)
        {
            int i = pk.IndexOf('K');
            string a = pk.Remove(i + 1);
            pk = pk.Replace(a, "");
            var ff = pk.Split('+');
            double pks = double.Parse(ff[0]) * 1000 + double.Parse(ff[1]);
            if (pks <= 164200)
            {
                if (((pks >= 154621) && (pks < 156136)) || ((pks >= 162423) && (pks < 163671)))
                {
                    return 1815;
                }
                else
                {
                    return 1465;
                }
            }
            else if (pks <= 222300)
            {
                if (((pks >= 179000) && (pks < 182320))|| ((pks >= 218396) && (pks < 221001)))
                {
                    return 1715;
                }
                else
                {
                    return 1365;
                }
            }
            else
            {
                if (((pks >= 224071) && (pks < 225571)) || ((pks >= 226983) && (pks < 228408)) || ((pks >= 238381) && (pks < 240602)))
                {
                    return 1815;
                }
                else
                {
                    return 1465;
                }
            }
        }

        public static int GetLeftW(string pk)
        {
            int i = pk.IndexOf('K');
            string a = pk.Remove(i + 1);
            pk = pk.Replace(a, "");
            var ff = pk.Split('+');
            double pks = double.Parse(ff[0]) * 1000 + double.Parse(ff[1]);
            if (pks <= 164200)
            {
                if ((pks >= 154152) && (pks < 158103) | (pks >= 162437) && (pks < 163654))
                {
                    return 1815;
                }
                else
                {
                    return 1465;
                }
            }
            else if (pks <= 222300)
            {
                if ((pks >= 179000) && (pks < 182320) | (pks >= 196065) && (pks < 198030) | (pks >= 218395) && (pks < 221000))
                {
                    return 1715;
                }
                else
                {
                    return 1365;
                }
            }
            else
            {
                if ((pks >= 224070) && (pks < 225572) | (pks >= 226983) && (pks < 228408) | (pks >= 230948) && (pks < 232148) | (pks >= 233534) && (pks < 234920) | (pks >= 241880) && (pks < 243230) | (pks >= 244609) && (pks < 247266))
                {
                    return 1815;
                }
                else
                {
                    return 1465;
                }
            }
        }
        //public static Tuple<int,int> GetW2(string pk)
        public static int GetRightW(string pk)
        {
            int i = pk.IndexOf('K');
            string a = pk.Remove(i + 1);
            pk = pk.Replace(a, "");
            var ff = pk.Split('+');
            double pks = double.Parse(ff[0]) * 1000 + double.Parse(ff[1]);
            if (pks <= 164200)
            {
                if ((pks >= 151378) && (pks < 155505) | (pks >= 161245) && (pks < 163569))
                {
                    return 1815;
                }
                else
                {
                    return 1465;
                }
            }
            else if (pks <= 222300)
            {
                if ((pks >= 164723) && (pks < 166559) | (pks >= 171803.652) && (pks < 174411) | (pks >= 176630) && (pks < 179767) | (pks >= 186192) && (pks < 196775) | (pks >= 205410) && (pks < 217472.5))
                {
                    return 1715;
                }
                else
                {
                    return 1365;
                }
            }
            else
            {
                if ((pks >= 223516) && (pks < 225144) | (pks >= 226580) && (pks < 228052) | (pks >= 229615) && (pks < 231963) | (pks >= 235987.019) && (pks < 242332) | (pks >= 243969) && (pks < 245792) | (pks >= 249711) && (pks < 252000))
                {
                    return 1815;
                }
                else
                {
                    return 1465;
                }
            }
        }



        public static void GetV(out double Vw, out double Vb, double Lon, double Tran, double Height, double wagaoA, double wagaoB)
        {
            double Plat = 1;
            double Slo = 1;
            double S1, S2, S3, S4;
            S1 = (Lon + 2 * Plat) * (Tran + 2 * Plat);//S1=S3
            S2 = (Lon + 2 * Plat + 2 * Slo * (wagaoA + wagaoB) / 2) * (Tran + 2 * Plat + 2 * Slo * (wagaoA + wagaoB) / 2);//S2
            S4 = (Lon + 2 * Plat + 2 * Slo * Math.Min(wagaoA, wagaoB)) * (Tran + 2 * Plat + 2 * Slo * Math.Min(wagaoA, wagaoB));//S4

            Vw = (wagaoA + wagaoB) * 0.5 * (S1 + S2 + Math.Sqrt(S1 * S2)) / 3.0;     //挖方体积Vw
            double Vf = Lon * Tran * Height;                                       //基础体积Vf
            Vb = 1.0 / 3.0 * Math.Min(wagaoA, wagaoB) * (S1 + S4 + Math.Sqrt(S1 * S4)) - Vf;  //回填体积Vh
        }

        /// <summary>
        /// 求圆锥体积、周长、表面积
        /// </summary>
        /// <param name="V">体积</param>
        /// <param name="C">周长</param>
        /// <param name="A">表面积</param>
        /// <param name="s">短轴长</param>
        /// <param name="l">长轴长</param>
        /// <param name="h">高</param>
        public static void Cone(out double V, out double C, out double A, double s, double l, double h)
        {
            //
            if (h < 4)
            {
                V = Math.PI * (1.5 * h) * (1.5 * h) * h / 3;
                C = Math.PI * 1.5 * h * 2 * 0.4578;
                A = (Math.PI * (1.5 * h) * (1.5 * h) * h / 3 - Math.PI * (1.5 * h - 0.12) * (1.5 * h - 0.12) * (h - 0.12) / 3) / 0.12;
            }
            else
            {
                V = Math.PI * (1.5 * h) * (1.5 * 4 + 1.75 * (h - 4)) * h / 3;
                C = (2 * Math.PI * 1.5 * h + 4 * (4 * 1.5 + 1.75 * (h - 4) - 1.5 * h)) * 0.4578;
                A = (Math.PI * (1.5 * h) * (4 * 1.5 + 1.75 * (h - 4)) * h / 3 - Math.PI * (1.5 * h - 0.12) * (4 * 1.5 + 1.75 * (h - 4) - 0.12) * (h - 0.12) / 3) / 0.12;
            }


        }
    }
}
