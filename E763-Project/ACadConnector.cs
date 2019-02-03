using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoCAD;

namespace E763_Project
{
    public static class ACadConnector
    {
        public static void GetSelection(out AcadSelectionSet curSelection)
        {
            AcadApplication AcadApp = null;
            AcadDocument doc = null;
            AcadSelectionSets SelSets = null;
            curSelection = null;
            
            try
            {
                Console.WriteLine("正在连接CAD2018...成功");
                AcadApp = (AcadApplication)Marshal.GetActiveObject("AutoCAD.Application.22");
                doc = AcadApp.ActiveDocument;
                SelSets = doc.SelectionSets;
                Console.WriteLine("选择内容...");
            }
            catch (Exception)
            {
                Console.WriteLine("无法连接AutoCAD...");
                return;
            }


            try
            {
                curSelection = SelSets.Add("NewSelection");
            }
            catch (Exception)
            {
                var item = from AcadSelectionSet a in SelSets where a.Name == "NewSelection" select a;
                curSelection = item.First();
            }
            finally
            {
                curSelection.Clear();
            }


            curSelection.SelectOnScreen();


            Console.WriteLine("共选择{0}个对象。", curSelection.Count);
            
        }

        public static void ExtrudeLevel(string fname,double dx,double dy)
        {
            AcadSelectionSet ASS;
            GetSelection(out ASS);
            // 提取设计线
            List<Tuple<double, double>> SJX = new List<Tuple<double, double>>();
            List<Tuple<double, double>> DMX = new List<Tuple<double, double>>();
            Tuple<double, double> coord;
            var sjxpl = from AcadEntity a in ASS where a.Layer == "ZD设计线" & a.EntityName == "AcDbPolyline" select a as AcadLWPolyline;
            var dmxpl = from AcadEntity a in ASS where a.Layer == "ZD地面线" & a.EntityName == "AcDbPolyline" select a as AcadLWPolyline;
            var ss1 = from a in sjxpl select a.Coordinates;
            var ss2 = from a in dmxpl select a.Coordinates;
            foreach (var item in ss1)
            {
                for (int i = 0; i < item.Length / 2; i++)
                {
                    coord = new Tuple<double, double>(item[i * 2], item[i * 2 + 1]);
                    SJX.Add(coord);
                }

            }
            SJX.Sort((pt1, pt2) => pt1.Item1.CompareTo(pt2.Item1));
            SJX = SJX.Distinct().ToList();
            foreach (var item in ss2)
            {
                for (int i = 0; i < item.Length / 2; i++)
                {
                    coord = new Tuple<double, double>(item[i * 2], item[i * 2 + 1]);
                    DMX.Add(coord);
                }

            }
            DMX.Sort((pt1, pt2) => pt1.Item1.CompareTo(pt2.Item1));
            DMX = DMX.Distinct().ToList();

            FileStream fs = new FileStream(string.Format("{0}-SJX.csv", fname), FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.WriteLine("x0,level");
            foreach (var item in SJX)
            {                
                sw.WriteLine("{0:F},{1:F}", item.Item1 + dx, item.Item2 + dy);
                sw.Flush();
            }
            sw.Close();
            fs.Close();

            fs= new FileStream(string.Format("{0}-DMX.csv", fname), FileMode.Create);
            sw = new StreamWriter(fs, Encoding.Default);

            sw.WriteLine("x0,level");
            foreach (var item in DMX)
            {
                sw.WriteLine("{0:F},{1:F}", item.Item1 + dx, item.Item2 + dy);
                sw.Flush();
            }
            sw.Close();
            fs.Close();


        }


        public static void ExtrudeData(string fname,double dx=0,double dy=0)
        {
            AcadSelectionSet ASS;
            GetSelection(out ASS);

            // 提取桥梁里程
            var items = from AcadEntity a in ASS where a.EntityName == "AcDbText" select a as AcadText;
            var brlist = from AcadText a in items where a.TextString.Contains("Bridge") select a.TextString as string;
            var BRs = brlist.ToList();
            // 提取设计线
            List<Tuple<double, double>> SJX = new List<Tuple<double, double>>();
            List<Tuple<double, double>> DMX = new List<Tuple<double, double>>();
            Tuple<double, double> coord;
            var sjxpl = from AcadEntity a in ASS where a.Layer == "ZD设计线" & a.EntityName == "AcDbPolyline" select a as AcadLWPolyline;
            var dmxpl = from AcadEntity a in ASS where a.Layer == "ZD地面线" & a.EntityName == "AcDbPolyline" select a as AcadLWPolyline;
            var ss1 = from a in sjxpl select a.Coordinates;
            var ss2 = from a in dmxpl select a.Coordinates;
            foreach (var item in ss1)
            {
                for (int i = 0; i < item.Length / 2; i++)
                {
                    coord = new Tuple<double, double>(item[i * 2], item[i * 2 + 1]);
                    SJX.Add(coord);
                }

            }
            SJX.Sort((pt1, pt2) => pt1.Item1.CompareTo(pt2.Item1));
            SJX = SJX.Distinct().ToList();
            foreach (var item in ss2)
            {
                for (int i = 0; i < item.Length / 2; i++)
                {
                    coord = new Tuple<double, double>(item[i * 2], item[i * 2 + 1]);
                    DMX.Add(coord);
                }

            }
            DMX.Sort((pt1, pt2) => pt1.Item1.CompareTo(pt2.Item1));
            DMX = DMX.Distinct().ToList();

            // 提取桥位标高
            var tmp = from AcadEntity a in ASS where a.Layer == "ZD结构物" & a.EntityName == "AcDbPolyline" select a as AcadLWPolyline;
            List<double> QW = new List<double>();
            foreach (var item in tmp)
            {
                var ff = (double[])item.Coordinates;
                if (ff.Length == 4 & ff[0] == ff[2] & ff[1] > ff[3])
                {
                    QW.Add(ff[0]);
                }

            }
            QW.Sort();
            QW = QW.Distinct().ToList();
            double dmbg, sjbg;

            FileStream fs = new FileStream(string.Format("{0}.csv",fname), FileMode.Create);
            StreamWriter sw = new StreamWriter(fs,Encoding.Default);

            sw.WriteLine("x0,地面线Y,设计线Y");
            foreach (var item in QW)
            {
                dmbg = DMX.InterP(item);
                sjbg = SJX.InterP(item);
                sw.WriteLine("{0:F},{1:F},{2:F}", item+dx, dmbg+dy, sjbg+dy);
                sw.Flush();
            }
            sw.Close();
            fs.Close();







            //FileStream fs = new FileStream("dmx.csv", FileMode.Create);
            //StreamWriter sw = new StreamWriter(fs);
            //foreach (var f in DMX)
            //{
            //    sw.WriteLine("{0:F},{1:F}", f.Item1, f.Item2);
            //    sw.Flush();
            //}
            //sw.Close();
            //fs.Close();

            //fs = new FileStream("sjx.csv", FileMode.Create);
            //sw = new StreamWriter(fs);
            //foreach (var f in SJX)
            //{
            //    sw.WriteLine("{0:F},{1:F}", f.Item1, f.Item2);
            //    sw.Flush();
            //}
            //sw.Close();
            //fs.Close();

            //;

        }

    }
}
