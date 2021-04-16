using AppCore;
using AppCore.Tools;
using HHDB.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AS_380
{
    /// <summary>
    /// 类型：AS_380
    /// 通讯：TCP/IP协议，单向
    /// </summary>
    public class AS380TCP : IDataCollect
    {
        string char2 = ((char)2).ToString();//STX 本文开始
        string char3 = ((char)3).ToString();//EXT 本文结束
        string char4 = ((char)4).ToString();//EOT 传输结束
        string char5 = ((char)5).ToString();//ENQ 请求
        string char6 = ((char)6).ToString();//ACK 确认回应
        string char10 = ((char)10).ToString();// LF 换行键
        string char13 = ((char)13).ToString();//CR 归位键
        string char11 = ((char)11).ToString();//VT 垂直定位符号
        string char28 = ((char)28).ToString();//FS 文件分割符
        private string DataRaw;
        private string sampleno = null;
        string ref_high = "";
        string ref_low = "";
        string result_mark = "";
        public string GetResultData(string text)
        {
            DataRaw += text;
            if (text.IndexOf(char28) >= 0)
            {
                return char6;
            }
            return "";
        }
        public string GetResultData()
        {
            if (!string.IsNullOrEmpty(DataRaw) && DataRaw.IndexOf(char28) >= 0)
            {
                string[] line = null;
                string data = null;
                string[] refvalue = null;
                int pos;
                while (!string.IsNullOrEmpty(DataRaw) && DataRaw.IndexOf('\r') >= 0)
                {
                    pos = StringOperation.Pos(DataRaw, '\r');
                    data = StringOperation.Left(DataRaw, pos);
                    DataRaw = StringOperation.Mid(DataRaw, pos + 1);
                    if (!string.IsNullOrEmpty(data))
                    {
                        line = data.Split('|');
                        if (line[0].IndexOf("OBR") >= 0)
                        {
                            sampleno = line[2];
                        }
                        else if (line[0].IndexOf("OBX") >= 0)
                        {
                            if (!string.IsNullOrEmpty(sampleno))
                            {
                                refvalue = line[7].Split('-');
                                if (refvalue.Length == 2)
                                {
                                    ref_low = refvalue[0];
                                    ref_high = refvalue[1];
                                }
                                else
                                {
                                    ref_low = "";
                                    ref_high = "";
                                }
                                if (line[8].Trim().ToUpper() == "L")
                                {
                                    result_mark = "↓";
                                }
                                else if (line[8].Trim().ToUpper() == "H")
                                {
                                    result_mark = "↑";
                                }
                                else
                                {
                                    result_mark = "";
                                }
                                SQLHelp.UpdateItem(sampleno, AppConfig.Instance.GroupId, line[3], line[5], result_mark, line[6], ref_low, ref_high, line[7]);
                                BaseDeal.DataLog(DateTime.Now.ToString() + "  " + "样本号:" + sampleno + "，项目代号:" + AppConfig.Instance.GroupId + "，项目仪器编码:" + line[3] + " " + "，项目结果:" + line[5] + "\r\n");
                            }
                        }
                    }
                }
            }
            return "";
        }
    }
}
