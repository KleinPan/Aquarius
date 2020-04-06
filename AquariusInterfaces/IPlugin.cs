using System.Windows.Controls;

namespace AquariusInterfaces
{
    public interface IPlugin
    {
        /// <summary> 插件名称 </summary>
        string Name { get; }

        /// <summary> 描述 </summary>
        string Description { get; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get;  }

        public Control Show();
    }
}