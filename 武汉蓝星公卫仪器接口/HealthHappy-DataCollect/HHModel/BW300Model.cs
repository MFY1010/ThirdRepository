using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHModel
{
    public class BW300Model
    {
        /// <summary>
        /// 检验日期
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 样本编号
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 条码号
        /// </summary>
        public string barcode { get; set; }
        /// <summary>
        /// 结果集
        /// </summary>
        public List<ResultsItem> results { get; set; }
    }
    public class ResultsItem
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public string item { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string unit { get; set; }
        /// <summary>
        /// 是否异常
        /// </summary>
        public string abnormal { get; set; }
    }
}
