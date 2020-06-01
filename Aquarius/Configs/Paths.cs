using System.IO;

namespace Aquarius.Configs
{
    public class Paths
    {
        /// <summary> 当前程序路径 </summary>
        public static string exePath { get; set; }

        /// <summary> 配置路径 </summary>
        public static string configPath { get; set; }

        /// <summary> 默认插件路径 </summary>
        public static string pluginPathDefault { get; set; }

        /// <summary> 用户插件路径 </summary>
        public static string pluginPathForUser { get; set; }


        /// <summary> 用户插件路径 </summary>
        public static string downloadPath { get; set; }
        static Paths()
        {
            exePath = System.IO.Directory.GetCurrentDirectory();

            configPath = exePath + @"\Configs";
            Directory.CreateDirectory(configPath);

            pluginPathDefault = exePath + @"\PluginsDefault";
            Directory.CreateDirectory(pluginPathDefault);

            pluginPathForUser = exePath + @"\PluginsUser";
            Directory.CreateDirectory(pluginPathForUser);

            downloadPath = exePath + @"\Download";
            Directory.CreateDirectory(downloadPath);
        }
    }
}