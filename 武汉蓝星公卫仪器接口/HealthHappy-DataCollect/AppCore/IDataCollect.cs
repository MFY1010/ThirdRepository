using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCore
{
    public interface IDataCollect
    {
        /// <summary>
        /// 缓存接收到的原始数据
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string GetResultData(string text);

        /// <summary>
        /// 解析缓存数据
        /// </summary>
        /// <returns></returns>
        string GetResultData();
    }
}
