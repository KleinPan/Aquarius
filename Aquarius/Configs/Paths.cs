using System.IO;

namespace Aquarius.Configs
{
    public class Paths
    {
        /// <summary> ��ǰ����·�� </summary>
        public static string exePath { get; set; }

        /// <summary> ����·�� </summary>
        public static string configPath { get; set; }

        /// <summary> Ĭ�ϲ��·�� </summary>
        public static string pluginPathDefault { get; set; }

        /// <summary> �û����·�� </summary>
        public static string pluginPathForUser { get; set; }


        /// <summary> �û����·�� </summary>
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