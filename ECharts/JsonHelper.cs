﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;

namespace ECharts.Net
{
    /// <summary>
    /// json辅助
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// object转json（包含日期格式处理）
        /// </summary>  
        /// <param name="obj">object</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
#if NETSTANDARD2_0
            var op = new System.Text.Json.Serialization.JsonSerializerOptions { IgnoreNullValues = true, WriteIndented = true };
            return System.Text.Json.Serialization.JsonSerializer.ToString(obj, op);
#else
            var jSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var json = JsonConvert.SerializeObject(obj, (Newtonsoft.Json.Formatting)Formatting.Indented, jSetting);
            var data = JsonConvert.DeserializeObject(json, typeof(object), jSetting);
            var timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd hh:mm" };
            json = JsonConvert.SerializeObject(data, (Newtonsoft.Json.Formatting)Formatting.Indented, timeConverter); 
            return json;
#endif

        }

        /// <summary>
        /// json字符串数组转换为List
        /// </summary>
        /// <param name="str">json字符串数组</param>
        /// <returns></returns>
        public static List<string> JsonToList(this string str)
        {
            var jSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include };
            var data = JsonConvert.DeserializeObject(str, typeof(List<string>), jSetting) as List<string>;
            return data;
        }
    }
}
