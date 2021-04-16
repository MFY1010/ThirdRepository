using AppCore;
using AppCore.Tools;
using Dapper;
using HHDB.Common;
using System;
using System.Data.Common;

namespace HHDB.DAL
{
    public static class SQLHelp
    {
        public static DbConnection db = DBHelp.GetPeisDB(AppConfig.Instance.Conn);

        /// <summary>
        /// 更新体检人员数据（只更新结果值）
        /// </summary>
        /// <param name="paid"></param>
        /// <param name="groupid"></param>
        /// <param name="insetrcode"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool UpdateItem(string paid, string groupid, string instr_code, string result)
        {
            try
            {
                if (!string.IsNullOrEmpty(paid) && !string.IsNullOrEmpty(groupid) && !string.IsNullOrEmpty(instr_code))
                {
                    string sql = "update T_ITEM_DT set result = @result where pa_id = @pa_id and instr_code = @instr_code and group_id = @group_id";
                    var parameters = new DynamicParameters();
                    parameters.Add("@pa_id", paid);
                    parameters.Add("@instr_code", instr_code);
                    parameters.Add("@group_id", groupid);
                    parameters.Add("@result", result);
                    return db.Execute(sql, parameters) > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                BaseDeal.LogError(DateTime.Now.ToString() + "  " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 更新体检人员数据 包括结果，判定，单位，参考范围
        /// </summary>
        /// <param name="paid"></param>
        /// <param name="groupid"></param>
        /// <param name="instr_code"></param>
        /// <param name="result"></param>
        /// <param name="result_mark"></param>
        /// <param name="unit"></param>
        /// <param name="ref_low"></param>
        /// <param name="ref_high"></param>
        /// <param name="ref_value"></param>
        /// <returns></returns>
        public static bool UpdateItem(string paid, string groupid, string instr_code, string result, string result_mark, string unit, string ref_low, string ref_high, string ref_value)
        {
            if (!string.IsNullOrEmpty(paid) && !string.IsNullOrEmpty(groupid) && !string.IsNullOrEmpty(instr_code) && !string.IsNullOrEmpty(result))
            {
                string sql = @"update T_ITEM_DT set result = @result,result_mark = @result_mark ,unit=@unit,ref_low=@ref_low,ref_high=@ref_high,ref_value=@ref_value
                                where pa_id = @pa_id and instr_code = @instr_code and group_id = @group_id";
                var parameters = new DynamicParameters();
                parameters.Add("@pa_id", paid);
                parameters.Add("@instr_code", instr_code);
                parameters.Add("@group_id", groupid);
                parameters.Add("@result", result);
                parameters.Add("@result_mark", result_mark);
                parameters.Add("@unit", unit);
                parameters.Add("@ref_low", ref_low);
                parameters.Add("@ref_high", ref_high);
                parameters.Add("@ref_value", ref_value);
                return db.Execute(sql, parameters) > 0;
            }
            return false;
        }

        /// <summary>
        /// 更新体检人员数据 包括结果，判定，单位
        /// </summary>
        /// <param name="paid"></param>
        /// <param name="groupid"></param>
        /// <param name="instr_code"></param>
        /// <param name="result"></param>
        /// <param name="result_mark"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool UpdateItem(string paid, string groupid, string instr_code, string result, string result_mark, string unit)
        {
            if (!string.IsNullOrEmpty(paid) && !string.IsNullOrEmpty(groupid) && !string.IsNullOrEmpty(instr_code) && !string.IsNullOrEmpty(result))
            {
                string sql = @"update T_ITEM_DT set result = @result,result_mark = @result_mark ,unit=@unit
                                where pa_id = @pa_id and instr_code = @instr_code and group_id = @group_id";
                var parameters = new DynamicParameters();
                parameters.Add("@pa_id", paid);
                parameters.Add("@instr_code", instr_code);
                parameters.Add("@group_id", groupid);
                parameters.Add("@result", result);
                parameters.Add("@result_mark", result_mark);
                parameters.Add("@unit", unit);
                return db.Execute(sql, parameters) > 0;
            }
            return false;
        }
    }
}
