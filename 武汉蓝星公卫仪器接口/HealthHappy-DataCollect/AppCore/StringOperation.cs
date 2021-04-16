using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCore
{
    public static class StringOperation
    {
        public static string Left(string param, int length)
        {
            if (length <= 0)
                return null;
            string result = param.Substring(0, length);
            return result;
        }
        public static string Right(string param, int length)
        {
            if (length <= 0)
                return null;
            string result = param.Substring(param.Length - length, length);
            return result;
        }
        public static string Mid(string param, int startIndex, int length)
        {
            if (length <= 0)
                return null;
            if (startIndex > param.Length - 1)
                return null;
            string result = param.Substring(startIndex, length);
            return result;
        }
        public static string Mid(string param, int startIndex)
        {
            if (startIndex > param.Length - 1)
                return null;
            string result = param.Substring(startIndex);
            return result;
        }
        public static int Pos(string param, char chr)
        {
            return param.IndexOf(chr);
        }
        public static int Pos(string param, string str)
        {
            return param.IndexOf(str);
        }
        /// <summary>
        /// 字符分割字符串
        /// </summary>
        /// <param name="param"></param>
        /// <param name="chr"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static int StringToArray(string param, char chr, ref string[] line)
        {
            line = param.Split(chr);
            return line.Length;
        }
        /// <summary>
        /// 字符串分割字符串
        /// </summary>
        /// <param name="param"></param>
        /// <param name="str"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static int StringToArray(string param, string str, ref string[] line)
        {
            //line = Regex.Split(param, str, RegexOptions.IgnoreCase);
            line = param.Split(new string[] { str }, StringSplitOptions.RemoveEmptyEntries);
            return line.Length;
        }
        /// <summary>
        /// 字符数组分割字符串
        /// </summary>
        /// <param name="param"></param>
        /// <param name="chr"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static int StringToArray(string param, char[] chr, ref string[] line)
        {
            line = param.Split(chr);
            return line.Length;
        }
        /// <summary>
        /// 获取校验和
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string SumCheck(string data)
        {
            byte[] bs = System.Text.Encoding.Default.GetBytes(data);
            int num = 0;
            //所有字节累加
            for (int i = 0; i < bs.Length; i++)
            {
                num = (num + bs[i]) % 0xFFFF;
            }
            string hexOutput = String.Format("{0:X}", Convert.ToInt32(num & 0xff));
            return hexOutput;
        }
    }
}
