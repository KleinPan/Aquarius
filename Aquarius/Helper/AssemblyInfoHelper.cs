using System.Reflection;

namespace Aquarius.Helper
{
    public class AssemblyInfoHelper
    {
        public Assembly assembly;
        public AssemblyInfoHelper()
        {
             assembly = Assembly.GetExecutingAssembly();

            VersionInfo = assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)[0] as AssemblyFileVersionAttribute;
            CompanyInfo = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false)[0] as AssemblyCompanyAttribute;
            ProductInfo = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0] as AssemblyProductAttribute;
            TitleInfo = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0] as AssemblyTitleAttribute;
            CopyrightInfo = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0] as AssemblyCopyrightAttribute;
            DescriptionInfo = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0] as AssemblyDescriptionAttribute;
        }

        /// <summary> 版本信息 </summary>
        public AssemblyFileVersionAttribute VersionInfo
        {
            get;
            private set;
        }

        /// <summary> 公司信息 </summary>
        public AssemblyCompanyAttribute CompanyInfo
        {
            get;
            private set;
        }

        /// <summary> 产品信息 </summary>
        public AssemblyProductAttribute ProductInfo
        {
            get;
            private set;
        }

        /// <summary> 标题信息 </summary>
        public AssemblyTitleAttribute TitleInfo
        {
            get;
            private set;
        }

        /// <summary> 版权信息 </summary>
        public AssemblyCopyrightAttribute CopyrightInfo
        {
            get;
            private set;
        }

        /// <summary> 描述信息 </summary>
        public AssemblyDescriptionAttribute DescriptionInfo
        {
            get;
            private set;
        }
    }
}