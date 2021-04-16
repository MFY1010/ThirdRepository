using Newtonsoft.Json;

namespace AppCore.Tools
{
    public class JsonHelper
    {
        /// <summary>
        /// 对象转JSON
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// JSON转对象 ，获得dynamic类型的对象
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static dynamic JsonToObject(string jsonString)
        {
            return JsonConvert.DeserializeObject<dynamic>(jsonString);
        }

        public static string ToJsonString<T>(T obj)
        {
            string str = JsonConvert.SerializeObject(obj);
            return str;
        }

        public static T ToPares<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
