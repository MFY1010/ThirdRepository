using AppCore;
using AppCore.Tools;
using HHDB.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BW300
{
    /// <summary>
    /// 类型：BW-300
    /// 通讯：TCP/IP协议，单向
    /// </summary>
    public class BW300TCP : IDataCollect
    {
        private List<Source> _source = new List<Source>();
        private string DataRaw;
        string result_mark = "";
        public string GetResultData(string text)
        {
            DataRaw += text;
            if (!string.IsNullOrEmpty(DataRaw) && DataRaw.IndexOf("}]}") > 0)
            {
                int pos;
                int pos2;
                pos = StringOperation.Pos(DataRaw, "}]}") + 3;
                pos2 = StringOperation.Pos(DataRaw, @"date") - 2;
                Source c = new Source();
                c.id = System.Guid.NewGuid().ToString();
                c.source = StringOperation.Mid(DataRaw, pos2, pos - pos2);
                _source.Add(c);
                DataRaw = "";
            }
            return "";
        }
        public string GetResultData()
        {
            while (_source.Count > 0)
            {
                var data = _source.FirstOrDefault();
                _source.Remove(data);
                try
                {
                    var source = JsonHelper.ToPares<HHModel.BW300Model>(data.source);
                    foreach (var item in source.results)
                    {
                        result_mark = item.abnormal.Trim().ToUpper() == "FALSE" ? "正常" : "异常";
                        SQLHelp.UpdateItem(source.barcode, AppConfig.Instance.GroupId, item.item, item.value, result_mark, item.unit);
                        BaseDeal.DataLog(DateTime.Now.ToString() + "  " + "样本号:" + source.barcode + "，项目代号:" + AppConfig.Instance.GroupId + "，项目仪器编码:" + item.item + " " + "，项目结果:" + item.value + "\r\n");
                    }
                }
                catch (Exception ex)
                {
                    BaseDeal.LogError(DateTime.Now.ToString() + "  " + ex.Message);
                    continue;
                }
            }
            //if (!string.IsNullOrEmpty(DataRaw) && DataRaw.IndexOf("}]}") > 0)
            //{
            //    int pos;
            //    int pos2;
            //    string data = null;
            //    while (!string.IsNullOrEmpty(DataRaw) && DataRaw.IndexOf("}]}") > 0)
            //    {
            //        pos = StringOperation.Pos(DataRaw, "}]}") + 3;
            //        pos2 = StringOperation.Pos(DataRaw, @"date") - 2;
            //        data = StringOperation.Left(DataRaw, pos);
            //        DataRaw = StringOperation.Mid(DataRaw, pos2, pos - pos2);
            //        try
            //        {
            //            var source = JsonHelper.ToPares<HHModel.BW300Model>(data);
            //            foreach (var item in source.results)
            //            {
            //                result_mark = item.abnormal.Trim().ToUpper() == "FALSE" ? "正常" : "异常";
            //                SQLHelp.UpdateItem(source.barcode, AppConfig.Instance.GroupId, item.item, item.value, result_mark, item.unit);
            //                BaseDeal.DataLog(DateTime.Now.ToString() + "  " + "样本号:" + source.barcode + "，项目代号:" + AppConfig.Instance.GroupId + "，项目仪器编码:" + item.item + " " + "，项目结果:" + item.value + "\r\n");
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            BaseDeal.LogError(DateTime.Now.ToString() + "  " + ex.Message);
            //            continue;
            //        }
            //    }
            //}
            return "";
        }

        class Source
        {
            public string source;
            public string id;
        }
    }
}
