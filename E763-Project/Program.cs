using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoCAD;

namespace E763_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            //SQX LKSQX = new SQX("LK", Resource1.LKSQX);
            GenAllRcd();
            //ReadLevels();
            Console.WriteLine("Press Any Key To Continue ...");
            Console.ReadKey();
        }

        
        static void GenAllRcd()
        {

            DataTable AllRcd = new DataTable();
            AllRcd.Columns.Add("bridge", typeof(string));
            AllRcd.Columns.Add("class", typeof(string));
            AllRcd.Columns.Add("loc", typeof(string));
            AllRcd.Columns.Add("detial", typeof(string));
            AllRcd.Columns.Add("name",typeof(string));
            AllRcd.Columns.Add("spec",typeof(string));
            AllRcd.Columns.Add("quantity",typeof(double));
            AllRcd.Columns.Add("quantity2", typeof(double));
            AllRcd.Columns.Add("xmh", typeof(string));
            AllRcd.Columns.Add("NewID", typeof(string));            
            //--------------------------------------------------------------------------------------
            //创建完善桥梁一览表数据
            //--------------------------------------------------------------------------------------
            DataTable LKBOB = Ext.GetDT(Resource1.LKBOB);
            DataTable RKBOB = Ext.GetDT(Resource1.RKBOB);
            List<Tuple<double, double>> LKDMX = Ext.GetBG(Resource1.LK_DMX);
            List<Tuple<double, double>> LKSJX = Ext.GetBG(Resource1.LK_SJX);
            List<Tuple<double, double>> RKDMX = Ext.GetBG(Resource1.R1_DMX);
            List<Tuple<double, double>> RKSJX = Ext.GetBG(Resource1.R1_SJX);
            RKDMX.AddRange(Ext.GetBG(Resource1.R2_DMX));
            RKSJX.AddRange(Ext.GetBG(Resource1.R2_SJX));
            RKDMX.AddRange(Ext.GetBG(Resource1.R3_DMX));
            RKSJX.AddRange(Ext.GetBG(Resource1.R3_SJX));
            RKDMX.AddRange(Ext.GetBG(Resource1.R4_DMX));
            RKSJX.AddRange(Ext.GetBG(Resource1.R4_SJX));
            RKDMX.AddRange(Ext.GetBG(Resource1.R5_DMX));
            RKSJX.AddRange(Ext.GetBG(Resource1.R5_SJX));
            RKDMX.AddRange(Ext.GetBG(Resource1.R6_DMX));
            RKSJX.AddRange(Ext.GetBG(Resource1.R6_SJX));
            RKDMX.AddRange(Ext.GetBG(Resource1.R7_DMX));
            RKSJX.AddRange(Ext.GetBG(Resource1.R7_SJX));


            Ext.AddNPTS(ref LKBOB);
            Ext.AddWidth(ref LKBOB);
            Ext.AddBeamType(ref LKBOB);
            Ext.AddBG(ref LKBOB, "DMX", ref LKDMX);
            Ext.AddBG(ref LKBOB, "SJX", ref LKSJX);
            Ext.AddGroupNum(ref LKBOB);
            Ext.AddGroupType(ref LKBOB);

            Ext.AddNPTS(ref RKBOB);
            Ext.AddWidth(ref RKBOB);
            Ext.AddBeamType(ref RKBOB);
            Ext.AddBG(ref RKBOB, "DMX", ref RKDMX);
            Ext.AddBG(ref RKBOB, "SJX", ref RKSJX);
            Ext.AddGroupNum(ref RKBOB);
            Ext.AddGroupType(ref RKBOB);

            //LKBOB.DataTableToCSV("左线桥梁");
            //RKBOB.DataTableToCSV("右线桥梁");
            LKBOB.DataTableToExcel("左线桥梁", "1");
            RKBOB.DataTableToExcel("右线桥梁", "1");


            //--------------------------------------------------------------------------------------
            //输出清单记录
            //--------------------------------------------------------------------------------------
            //OutPut.SubStructure(ref AllRcd, ref LKBOB);
            //OutPut.SupStructure(ref AllRcd, ref LKBOB);
            //OutPut.Auxiliary(ref AllRcd, ref LKBOB);

            //OutPut.SubStructure(ref AllRcd, ref RKBOB);
            //OutPut.SupStructure(ref AllRcd, ref RKBOB);
            //OutPut.Auxiliary(ref AllRcd, ref RKBOB);
            //--------------------------------------------------------------------------------------
            // 匹配桥梁信息
            //--------------------------------------------------------------------------------------

            //AllRcd.AddInfo(ref LKBOB, ref RKBOB);


            //AllRcd.DataTableToCSV("材料清单表");
            //AllRcd.DataTableToExcel("材料清单", "左线");











            //int dPK = 0;
            //List<double> dmbg = new List<double>();
            //List<double> sjbg = new List<double>();
            //DataTable DMXBG = new DataTable();
            //DMXBG.Columns.Add("PK", typeof(string));
            //DMXBG.Columns.Add("NoS", typeof(string));
            //DMXBG.Columns.Add("DMX", typeof(List<double>));
            //DMXBG.Columns.Add("SJX", typeof(List<double>));
            //DMXBG.Columns.Add("NPTS", typeof(int));
            //List<int> SpanList = new List<int>() { 10, 15, 25, 35, 50, 60 };


            //int rowIdx = 0;
            //int i = 0;
            //foreach (DataRow item in LKBG.Rows)
            //{
            //    var ff = from a in item.ItemArray select a.ToString();
            //    List<string> wds = ff.ToList();
            //    if (i == 0)
            //    {
            //        dmbg.Clear();
            //        sjbg.Clear();
            //    }
            //    else
            //    {
            //        var mm = from b in LKBG.Rows[i - 1].ItemArray select b.ToString();
            //        List<string> wdsBefore = mm.ToList();
            //        dPK = (int)Math.Round(double.Parse(wds[0]) - double.Parse(wdsBefore[0]), 0);
            //        if (SpanList.Contains(dPK))
            //        {
            //            ;

            //        }
            //        else
            //        {
            //            if ((string)LKBOB.Rows[rowIdx]["PK"] == "LK185+190.0")
            //            {
            //                ;

            //            }
            //            DMXBG.Rows.Add(LKBOB.Rows[rowIdx]["PK"], LKBOB.Rows[rowIdx]["NumOfSpan"], dmbg, sjbg, sjbg.Count);
            //            dmbg.Clear();
            //            sjbg.Clear();
            //            //dmbg = (List<double>)LKBOB.Rows[rowIdx]["DMBG"];
            //            //sjbg = (List<double>)LKBOB.Rows[rowIdx]["SJBG"];
            //            rowIdx++;
            //        }
            //    }




            //    dmbg.Add(double.Parse(wds[1]));
            //    sjbg.Add(double.Parse(wds[2]));

            //    i++;



            //}


            //ACadConnector.ExtrudeLevel("LK", 147545, 64.499);


            //ACadConnector.ExtrudeData("LKdata",147545,64.499);


        }
        static void ReadLevels()
        {
            //ACadConnector.ExtrudeLevel("LK", 147545, 64.5);
            //ACadConnector.ExtrudeLevel("R1", 153045, -135.5);
            //ACadConnector.ExtrudeLevel("R2", 164145, -135.5);
            ACadConnector.ExtrudeLevel("R3", 171745, -35.5);
            //ACadConnector.ExtrudeLevel("R4", 176445, -35.5);
            //ACadConnector.ExtrudeLevel("R5", 187445, 264.5);
            //ACadConnector.ExtrudeLevel("R6", 210745,564.5);
            //ACadConnector.ExtrudeLevel("R7", 235845, 664.5);
        }
    }
}
