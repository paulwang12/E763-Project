using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFM = System.Windows.Forms;
using MOExcel = Microsoft.Office.Interop.Excel;
using System.Threading.Tasks;
using System.Data;
using System.Runtime.InteropServices;
using System.IO;

namespace E763_Project
{
    public static class Ext
    {

        public static int CountBeams(int W,BeamType bt)
        {
            int bms = 0;
            switch (bt)
            {   
                case BeamType.F10:
                    bms= 1;
                    break;
                case BeamType.F15:
                    bms = 1;
                    break;
                case BeamType.T25:
                    if (W==1365)
                    {
                        bms = 8;
                    }
                    else if (W == 1465)
                    {
                        bms = 9;
                    }
                    else if (W == 1715)
                    {
                        bms = 10;
                    }
                    else if (W == 1815)
                    {
                        bms = 11;
                    }
                    break;
                case BeamType.T35:
                    if (W == 1365)
                    {
                        bms = 7;
                    }
                    else if (W == 1465)
                    {
                        bms = 8;
                    }
                    else if (W == 1715)
                    {
                        bms = 9;
                    }
                    else if (W == 1815)
                    {
                        bms = 10;
                    }
                    break;
                case BeamType.B50:
                    bms = 1;
                    break;
                case BeamType.B60:
                    bms = 1;
                    break;
                default:
                    break;
            }
            return bms;
        }

        public static double PK2NUM(string pk)
        {
            double pks = 0;
            try
            {
                int i = pk.IndexOf('K');
                string a = pk.Remove(i + 1);
                pk = pk.Replace(a, "");
                var ff = pk.Split('+');
                pks = double.Parse(ff[0]) * 1000 + double.Parse(ff[1]);
            }
            catch (Exception)
            {
                throw;
            }
            return pks;
        }

        public static double InterP(this List<Tuple<double,double>> tar,double x0)
        {
            Tuple<double, double> t1, t2;
            tar.Sort((p1, p2) => p1.Item1.CompareTo(p2.Item1));
            var f = from a in tar select a.Item1;
            var xlist =f.ToList();
            if (xlist.Contains(x0))
            {
                return  tar[xlist.IndexOf(x0)].Item2;
            }
            else
            {
                xlist.Add(x0);
                xlist.Sort();
                int i0 = xlist.IndexOf(x0);
                t1 = tar[i0 - 1];
                t2 = tar[i0];

                return t1.Item2 + (t2.Item2 - t1.Item2) / (t2.Item1 - t1.Item1) * (x0 - t1.Item1);
            }            
        }

        public static List<Tuple<double,double>> GetBG(string res)
        {
            List<Tuple<double, double>> ret = new List<Tuple<double, double>>();
            var item = res;
            var ss = item.Split('\n');            
            foreach (var line in ss)
            {
                if (line == "")
                {
                    continue;
                }
                var tmp = line.Trim('\r');
                var wds = tmp.Split(',');
                try
                {
                    Tuple<double, double> loc = new Tuple<double, double>(double.Parse(wds[0]), double.Parse(wds[1]));
                    ret.Add(loc);
                }
                catch (Exception)
                {
                    continue;                    
                }                
            }
            return ret;
        }


        public static DataTable GetDT(string res)
        {
            DataTable ret = new DataTable();
            var item = res;
            var ss = item.Split('\n');
            int i = 0;
            foreach (var line in ss)
            {
                
                if (line=="")
                {
                    continue;
                }
                var tmp = line.Trim('\r');
                var wds=tmp.Split(',');
                if (i==0)
                {
                    foreach (var wd in wds)
                    {
                        ret.Columns.Add(wd,typeof(string));
                    }
                }
                else
                {
                    DataRow newrow = ret.NewRow();

                    for (int j = 0; j < ret.Columns.Count; j++)
                    {
                        newrow[ret.Columns[j].ColumnName] = wds[j];
                    }                    
                    ret.Rows.Add(newrow);
                }
                i ++;
            }


            return ret;

        }

        public static int CountSpans(string spwd)
        {
            int res = 0;
            
            int span, num;
            if (spwd.Contains('+'))
            {
                var ff = spwd.Split('+');
                foreach (var item in ff)
                {
                    GetNumSpan(item,out num,out span);
                    if (num != -1)
                    {
                        res += num;
                    }
                    else
                    {
                        return -1;
                    }
                }
                return res;
            }
            else
            {
                GetNumSpan(spwd,out num,out span);
                return num;
            }



        }


        public static void GetNumSpan(string wd,out int num,out int span)
        {
            
            List<string> SpanList = new List<string>() { "10","15", "25","35","50","60" };
            List<string> ff=new List<string>();
            int ct = 0;

            if (wd.Contains("*"))
            {
                ff=wd.Split('*').ToList();
                ct = ff.Count;
            }
            else if (wd.Contains("x"))
            {
                ff = wd.Split('x').ToList();
                ct = ff.Count;
            }
            else
            {
                try
                {
                    int.Parse(wd);
                    num = 1;
                    span = int.Parse(wd);
                    return;
                }
                catch (Exception)
                {
                    num = -1;
                    span = 0;
                }
                
            }
            if (ct != 2)
            {
                num = -1;
                span = 0;

            }
            else
            {
                if (SpanList.Contains(ff[0]))
                {
                    if (SpanList.Contains(ff[1]))
                    {
                        int a = int.Parse(ff[0]);
                        int b = int.Parse(ff[1]);
                        num = Math.Min(a, b);
                        span = Math.Max(a, b);
                    }
                    else
                    {
                        num = int.Parse(ff[1]);
                        span = int.Parse(ff[0]);
                    }

                }
                else if (SpanList.Contains(ff[1]))
                {
                    num = int.Parse(ff[0]);
                    span = int.Parse(ff[1]);

                }
                else
                {
                    num = -1;
                    span = 0;
                }
            }

            
        }



        public static void DataTableToCSV(this DataTable dt, string file_name)
        {
            StreamWriter fw=null;
            try
            {
                fw = new StreamWriter(string.Format("C:\\Users\\Bill\\Desktop\\{0}.csv",file_name), false, Encoding.GetEncoding("GBK"));
                //写入表头
                
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    fw.Write(dt.Columns[i].ColumnName);
                    if (i!=dt.Columns.Count-1)
                    {
                        fw.Write(",");

                    }
                    else
                    {
                        fw.Write("\n");
                    }
                    fw.Flush();
                }


                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string[] arrBody = new string[dt.Columns.Count];
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        try
                        {
                            var f =dt.Rows[i][j];
                            arrBody[j] = f.ToString();
                        }
                        catch (Exception)
                        {
                            
                        }
                    }
                    //arrBody[dt.Columns.Count] = GetInfo(arrBody[0], ref lkbob, ref rkbob);
                    fw.WriteLine(arrBody.Together());
                    fw.Flush();
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                fw.Close();
            }
        }



        public static string Together(this string[] arr,string sep=",")
        {
            string res = "";
            for (int i = 0; i < arr.Length; i++)
            {
                res += arr[i];
                if (i != arr.Length - 1)
                {
                    res += sep;
                }

            }
            return res;

        }


        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int pid);
        //函数原型；DWORD GetWindowThreadProcessld(HWND hwnd，LPDWORD lpdwProcessld);
        //参数：hWnd:窗口句柄
        //参数：lpdwProcessld:接收进程标识的32位值的地址。如果这个参数不为NULL，GetWindwThreadProcessld将进程标识拷贝到这个32位值中，否则不拷贝
        //返回值：返回值为创建窗口的线程标识。

        //dt：从数据库读取的数据；file_name：保存路径；sheet_name：表单名称
        public static void DataTableToExcel(this DataTable dt, string file_name, string sheet_name)
        {
            Microsoft.Office.Interop.Excel.Application Myxls = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook Mywkb = Myxls.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet MySht = Mywkb.ActiveSheet;
            MySht.Name = sheet_name;
            Myxls.Visible = false;
            Myxls.DisplayAlerts = false;
            try
            {
                //写入表头
                object[] arrHeader = new object[dt.Columns.Count];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    arrHeader[i] = dt.Columns[i].ColumnName;
                }
                MySht.Range[MySht.Cells[1, 1], MySht.Cells[1, dt.Columns.Count]].Value2 = arrHeader;
                //写入表体数据
                object[,] arrBody = new object[dt.Rows.Count, dt.Columns.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        try
                        {
                            var f=(List<int>)dt.Rows[i][j];
                            arrBody[i, j] = f.ToString2();

                        }
                        
                        catch (Exception)
                        {
                            try
                            {
                                var f = (List<BeamType>)dt.Rows[i][j];
                                arrBody[i, j] = f.ToString2();
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    var f = (List<double>)dt.Rows[i][j];
                                    arrBody[i, j] = f.ToString2();
                                }
                                catch (Exception)
                                {

                                    arrBody[i, j] = dt.Rows[i][j].ToString();
                                }
                                                
                            }                            
                        }                        
                    }
                }
                MySht.Range[MySht.Cells[2, 1], MySht.Cells[dt.Rows.Count + 1, dt.Columns.Count]].Value2 = arrBody;
                if (Mywkb != null)
                {
                    Mywkb.SaveAs(file_name);
                    Mywkb.Close(Type.Missing, Type.Missing, Type.Missing);
                    Mywkb = null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //彻底关闭Excel进程
                if (Myxls != null)
                {
                    Myxls.Quit();
                    try
                    {
                        if (Myxls != null)
                        {
                            int pid;
                            GetWindowThreadProcessId(new IntPtr(Myxls.Hwnd), out pid);
                            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(pid);
                            p.Kill();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Myxls = null;
                }
                GC.Collect();
            }
        }


        public static string ToString2(this List<BeamType> lst)
        {
            string res = "";

            for (int i = 0; i < lst.Count; i++)
            {

                res += string.Format("{0}", lst[i]);
                if (i != lst.Count - 1)
                {
                    res += ",";

                }
            }
            return res;


        }

        public static string ToString2(this List<double> lst)
        {
            string res = "";

            for (int i = 0; i < lst.Count; i++)
            {

                res += string.Format("{0}", lst[i]);
                if (i != lst.Count - 1)
                {
                    res += ",";

                }
            }
            return res;


        }
        public static string ToString2(this List<int> lst)
        {
            string res = "";

            for (int i = 0; i < lst.Count; i++)
            {

                res += string.Format("{0:F0}", lst[i]);
                if (i !=lst.Count-1)
                {
                    res += ",";

                }
            }
            return res;

            
        }

        
        public static void AddWidth(ref DataTable BOB)
        {
            BOB.Columns.Add("Width", typeof(List<int>));
            for (int i = 0; i < BOB.Rows.Count; i++)
            {
                string wd = BOB.Rows[i]["PK"].ToString();
                double pks = PK2NUM(wd);

                List<int> sps = new List<int>();
                sps.Add(WXS.GetLeftW2(wd));
                sps.Add(WXS.GetRightW2(wd));
                BOB.Rows[i]["Width"] = sps;
            }

        }

        public static void AddGroupNum(ref DataTable BOB)
        {
            BOB.Columns.Add("GroupNum", typeof(int));
            for (int i = 0; i < BOB.Rows.Count; i++)
            {
                List<BeamType> wd = (List<BeamType>)BOB.Rows[i]["BeamTypes"];               
                

                BOB.Rows[i]["GroupNum"] = CountGroup(wd);
            }
        }

        public static void AddNPTS(ref DataTable BOB)
        {
            BOB.Columns.Add("NPTS", typeof(List<int>));
            for (int i = 0; i < BOB.Rows.Count; i++)
            {
                string wd = BOB.Rows[i]["NumOfSpan"].ToString();
                List<string> wds = wd.Split('+').ToList();
                List<int> sps = new List<int>();
                foreach (var item in wds)
                {
                    int span, num;
                    Ext.GetNumSpan(item, out num, out span);
                    for (int j = 0; j < num; j++)
                    {
                        sps.Add(span);
                    }
                }
                BOB.Rows[i]["NPTS"] = sps;
            }
        }

        public static void AddBeamType(ref DataTable BOB)
        {
            BOB.Columns.Add("BeamTypes", typeof(List<BeamType>));
            for (int i = 0; i < BOB.Rows.Count; i++)
            {
                List<int> wd = (List<int>)BOB.Rows[i]["NPTS"];
                List<BeamType> ret = GetBeamType(wd);

                BOB.Rows[i]["BeamTypes"] = ret;
            }

        }
        public static void AddGroupType(ref DataTable BOB)
        {
            BOB.Columns.Add("GroupTypes", typeof(List<BeamType>));
            for (int i = 0; i < BOB.Rows.Count; i++)
            {
                List<BeamType> wd = (List<BeamType>)BOB.Rows[i]["BeamTypes"];
                List<BeamType> ret = GetGroup(wd);

                BOB.Rows[i]["GroupTypes"] = ret;
            }

        }







        public static void AddSJXBG(ref DataTable BOB, string ColName, ref SQX sqx)
        {
            BOB.Columns.Add(ColName, typeof(List<double>));
            for (int i = 0; i < BOB.Rows.Count; i++)
            {
                List<int> splist = (List<int>)BOB.Rows[i]["NPTS"];
                List<BeamType> btlist = (List<BeamType>)BOB.Rows[i]["BeamTypes"];
                double Length = double.Parse((string)BOB.Rows[i]["L"]);
                double pks = PK2NUM((string)BOB.Rows[i]["PK"]);
                double pk0 = pks - 0.5 * Length;
                List<double> dmbg = new List<double>();
                for (int j = 0; j <= splist.Count; j++)
                {
                    dmbg.Add(sqx.GetBG(pk0));
                    if (j != splist.Count)
                    {
                        pk0 += splist[j];
                    }
                }
                BOB.Rows[i][ColName] = dmbg;
            }
        }











        public static void AddBG(ref DataTable BOB,string ColName,ref List<Tuple<double,double>> BGLine)
        {
            BOB.Columns.Add(ColName, typeof(List<double>));
            for (int i = 0; i < BOB.Rows.Count; i++)
            {
                List<int> splist = (List<int>)BOB.Rows[i]["NPTS"];
                List<BeamType> btlist = (List<BeamType>)BOB.Rows[i]["BeamTypes"];
                double Length = double.Parse((string)BOB.Rows[i]["L"]);
                double pks = PK2NUM((string)BOB.Rows[i]["PK"]);
                double pk0 = pks - 0.5 * Length;
                List<double> dmbg = new List<double>();
                for (int j = 0; j <= splist.Count; j++)
                {
                    dmbg.Add(BGLine.InterP(pk0));
                    if (j!=splist.Count)
                    {
                        pk0 += splist[j];
                    }     
                }                
                BOB.Rows[i][ColName] = dmbg;
            }
        }



        public static List<BeamType> GetBeamType(List<int> spanList)
        {
            List<BeamType> res = new List<BeamType>();
            for (int i = 0; i < spanList.Count; i++)
            {
                int item = spanList[i];
                if (item == 10)
                {
                    res.Add(BeamType.F10);
                }
                else if (item==15)
                {
                    res.Add(BeamType.F15);
                }
                else if(item==25)
                {
                    res.Add(BeamType.T25);
                }
                else if (item == 50)
                {
                    res.Add(BeamType.B50);
                }
                else if (item == 60)
                {
                    res.Add(BeamType.B60);

                }
                else
                {
                    if (i==0)
                    {
                        if (spanList.Count == 1)
                        {
                            res.Add(BeamType.T35);
                        }
                        else if (spanList[i+1]==60)
                        {
                            res.Add(BeamType.B60);
                        }
                        else
                        {
                            res.Add(BeamType.T35);
                        }                        
                    }
                    else if (i == spanList.Count - 1)
                    {
                        if (spanList[i - 1] == 60)
                        {
                            res.Add(BeamType.B60);
                        }
                        else
                        {
                            res.Add(BeamType.T35);
                        }

                    }
                    else
                    {
                        if (spanList[i + 1] == 60 | spanList[i-1]==60)
                        {
                            res.Add(BeamType.B60);
                        }
                        else
                        {
                            res.Add(BeamType.T35);
                        }

                    }
                }

            }
            return res;


        }


        public static List<BeamType> GetGroup(List<BeamType> Btlist)
        {
            List<BeamType> GroupType = new List<BeamType>();
            int res = 1;
            int AllSpans = Btlist.Count;
            if (Btlist.Contains(BeamType.B60))
            {
                if (Btlist.Contains(BeamType.T25) | Btlist.Contains(BeamType.T35))
                {
                    int i0 = Btlist.IndexOf(BeamType.B60);
                    int i1 = Btlist.LastIndexOf(BeamType.B60);
                    if (i0 == 0)
                    {
                        double a = (AllSpans - 3) / 6.0;
                        res = (int)a + 2;

                        GroupType.Clear();

                        GroupType.Add(BeamType.B60);
                        for (int k = 0; k < res-1; k++)
                        {
                            GroupType.Add(Btlist.Last());
                        }
                        
                    }
                    else if (i1 == AllSpans - 1)
                    {
                        double a = (AllSpans - 3) / 6.0;
                        res = (int)a + 2;
                        GroupType.Clear();
                        for (int k = 0; k < res - 1; k++)
                        {
                            GroupType.Add(Btlist.Last());
                        }
                        GroupType.Add(BeamType.B60);
                    }
                    else if (i0 >= 5)
                    {
                        if (AllSpans - i1 - 1 >= 6)
                        {
                            res = 5;
                            GroupType.Clear();
                            GroupType.Add(Btlist[0]);
                            GroupType.Add(Btlist[0]);                            
                            GroupType.Add(BeamType.B60);
                            GroupType.Add(Btlist.Last());
                            GroupType.Add(Btlist.Last());


                        }
                        else
                        {
                            res = 4;
                            GroupType.Clear();
                            GroupType.Add(Btlist[0]);
                            GroupType.Add(Btlist[0]);
                            GroupType.Add(BeamType.B60);
                            GroupType.Add(Btlist.Last());
                        }
                    }
                    else
                    {
                        if (AllSpans - i1 - 1 >= 6)
                        {
                            res = 4;
                            GroupType.Clear();
                            GroupType.Add(Btlist[0]);
                            GroupType.Add(BeamType.B60);
                            GroupType.Add(Btlist.Last());
                            GroupType.Add(Btlist.Last());
                        }
                        else
                        {
                            res = 3;
                            GroupType.Clear();
                            GroupType.Add(Btlist[0]);
                            GroupType.Add(BeamType.B60);
                            GroupType.Add(Btlist.Last());
                        }
                    }
                }
                else
                {
                    res = 1;
                    GroupType.Clear();
                    
                    GroupType.Add(BeamType.B60);
                }

            }
            else if (AllSpans == 1)
            {
                res = 1;
                GroupType.Add(Btlist[0]);
            }
            else
            {
                double a = AllSpans / 6.0;
                res = (int)a + 1;
                for (int k = 0; k < res; k++)
                {
                    GroupType.Add(Btlist[0]);
                }
            }
            return GroupType;
        }




        public static int CountGroup(List<BeamType> Btlist)
        {
            int res = 1;
            int AllSpans = Btlist.Count;
            if (Btlist.Contains(BeamType.B60))
            {
                if (Btlist.Contains(BeamType.T25)|Btlist.Contains (BeamType.T35))
                {
                    int i0 = Btlist.IndexOf(BeamType.B60);
                    int i1 = Btlist.LastIndexOf(BeamType.B60);
                    if (i0==0)
                    {
                        double a = (AllSpans-3) / 6.0;
                        res = (int)a + 2;
                    }
                    else if (i1==AllSpans-1)
                    {
                        double a = (AllSpans - 3) / 6.0;
                        res = (int)a + 2;
                    }
                    else if (i0 >= 5)
                    {
                        if (AllSpans - i1 - 1 >= 6)
                        {
                            res = 5;
                        }
                        else
                        {
                            res = 4;
                        }
                    }
                    else
                    {
                        if (AllSpans - i1 - 1 >= 6)
                        {
                            res = 4;
                        }
                        else
                        {
                            res = 3;
                        }
                    }
                }
                else
                {
                    res = 1;
                }                

            }
            else if(AllSpans==1)
            {
                res = 1;
            }
            else
            {
                double a = AllSpans / 6.0;
                res = (int)a + 1;
            }
            return res;
        }





    }



}
