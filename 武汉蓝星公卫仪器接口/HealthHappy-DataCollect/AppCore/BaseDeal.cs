using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AppCore
{
    public class BaseDeal
    {
        #region 日志
        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="intxt"></param>
        public static void LogError(string intxt)
        {
            string DealTime = BaseDeal.NowData();
            string FilePath = BaseDeal.AppPath() + "\\Error\\";
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            string FileName = FilePath + DealTime.ToString() + ".txt";
            if (!File.Exists(FileName))
            {
                FileStream fs;
                fs = File.Create(FileName);
                fs.Close();
            }
            string filestring = intxt;
            using (FileStream myStream1 = new FileStream(FileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                StreamWriter sw1 = new StreamWriter(myStream1, Encoding.Default);
                sw1.WriteLine(filestring);
                sw1.Close();
                myStream1.Close();
            }
        }

        /// <summary>
        /// 记录接收和发送日志
        /// </summary>
        /// <param name="intxt"></param>
        public static void FileWrite(string intxt)
        {
            string DealTime = BaseDeal.NowData();
            string FilePath = BaseDeal.AppPath() + "\\raw\\";
            if (!Directory.Exists(FilePath))
            {

                Directory.CreateDirectory(FilePath);

            }
            string FileName = FilePath + DealTime.ToString() + ".txt";
            if (!File.Exists(FileName))
            {
                FileStream fs;
                fs = File.Create(FileName);
                fs.Close();

            }
            string filestring = intxt;
            using (FileStream myStream1 = new FileStream(FileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                StreamWriter sw1 = new StreamWriter(myStream1, Encoding.Default);
                sw1.Write(filestring);
                sw1.Close();
                myStream1.Close();
            }

        }

        /// <summary>
        /// 记录接收和发送日志
        /// </summary>
        /// <param name="intxt"></param>
        public static void DataLog(string intxt)
        {
            string DealTime = BaseDeal.NowData();
            string FilePath = BaseDeal.AppPath() + "\\Data\\";
            if (!Directory.Exists(FilePath))
            {

                Directory.CreateDirectory(FilePath);

            }
            string FileName = FilePath + DealTime.ToString() + ".txt";
            if (!File.Exists(FileName))
            {
                FileStream fs;
                fs = File.Create(FileName);
                fs.Close();

            }
            string filestring = intxt;
            using (FileStream myStream1 = new FileStream(FileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                StreamWriter sw1 = new StreamWriter(myStream1, Encoding.Default);
                sw1.Write(filestring);
                sw1.Close();
                myStream1.Close();
            }

        }
        #endregion

        /// <summary>
        /// 获取当天日期
        /// </summary>
        /// <returns>yymmdd格式的日期</returns>
        public static string NowData()
        {
            // DateTime.Now.ToShortTimeString();
            DateTime dt = DateTime.Now;
            //dt = dt.AddMonths(-1);
            string DealTime2 = string.Format("{0:yyMMdd}", dt);
            return DealTime2;
        }

        /// <summary>
        /// 获取程序根目录
        /// </summary>
        /// <returns></returns>
        public static string AppPath()
        {
            string AppPathStr = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            return AppPathStr;

        }
    }
}
