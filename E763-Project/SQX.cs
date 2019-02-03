using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E763_Project
{
    public class SQX
    {
        public string Name;
        List<double[]> Data;
        public double StartStation
        {
            get
            {
                return Data[0][0];
            }
        }
        public double EndStation
        {
            get
            {
                return Data.Last()[0];
            }
        }
        

        public SQX()
        {
            Name = null;
            Data = new List<double[]>();            
        }
        public SQX(string name,byte[] inputs)
        {
            Name = name;
            Decoder d = Encoding.UTF8.GetDecoder();
            int charSize = d.GetCharCount(inputs, 0, inputs.Length);
            char[] chs = new char[charSize];
            d.GetChars(inputs, 0, inputs.Length, chs, 0);
            string s = new string(chs);
            var ss = s.Split('\n');
            foreach (string  item in ss)
            {
                string line=item.TrimEnd('\r');



            }
            //double[,] ABCD = new double[ss.Length, 7];
            //double[] rcd;
            //ArrayList al = new ArrayList();

        }
    }
}
