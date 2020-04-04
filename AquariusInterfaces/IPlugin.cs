using System.Windows.Controls;

namespace AquariusInterfaces
{
    public interface IPlugin
    {
        /// <summary> ������� </summary>
        string Name { get; }

        /// <summary> ���� </summary>
        string Description { get; }

        public Control Show();
    }
}
