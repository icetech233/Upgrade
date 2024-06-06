using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Upgrade.Core
{
    /// <summary>
    /// JxUpdate更新类
    /// </summary>
    public class JxUpdate
    {
        public const string UPDATE_API_PREFIX = "http://app.jiuxiniot.com/jx-app-manage-service/share/key/";

        public static string AppKey { get; set; } = "a1d3c7507a924f8d86a7a60dd16b6772";

        static JxUpdate() { }

        private static Data cacheData = null;
        private static Version serverVersion = null;
        /// <summary>
        /// UpdateInfo
        /// </summary>
        public static async Task UpdateInfoAsync()
        {
            HttpClient httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync(UPDATE_API_PREFIX + AppKey);
                // 确保请求成功（HTTP状态码 200 表示成功
                response.EnsureSuccessStatusCode();
                // 读取响应的内容
                string responseBody = await response.Content.ReadAsStringAsync();
                // 反序列化
                var _root = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(responseBody);
                // _root.data.currentPackage.version;
                cacheData = _root.data;
                serverVersion = Version.Parse(cacheData.currentPackage.version);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                httpClient.Dispose();
                httpClient = null;
            }
        }

        /// <summary>
        /// KillProc 更新前先杀进程
        /// </summary>
        public static void KillProc()
        {

        }

        internal class Root
        {
            public int code { get; set; }
            public string msg { get; set; }
            public Data data { get; set; }
        }

        internal class Data
        {
            public string name { get; set; } // "网关助手",
            public string previewUrl { get; set; }
            public string iconUrl { get; set; }
            public CurrentPackage currentPackage { get; set; }
        }

        internal class CurrentPackage
        {
            // "size": 4545930,
            public long size { get; set; } // "size": 4545930,
            public string version { get; set; }  // "1.0.0"
            public string buildVersion { get; set; }  // "20240520"
            public string remark { get; set; }  // "测试第一个版本"
            public string sizeStr { get; set; }//  "4.34 MB",
            public string safeDownloadUrl { get; set; }  // 下载链接
            public string updateTime { get; set; }  // "2024-06-05 15:30:44"
        }
        // 
    }
}

