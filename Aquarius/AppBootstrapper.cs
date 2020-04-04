using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Autofac;

namespace Aquarius
{
    public class AppBootstrapper
    {
        public static IContainer Container { get; set; }

        public AppBootstrapper()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

            //加载实现类的程序集

            List<string> defaultFileList = System.IO.Directory.EnumerateFiles(Configs.Paths.pluginPathDefault).ToList();
            List<string> userFileList = System.IO.Directory.EnumerateFiles(Configs.Paths.pluginPathForUser).ToList();

            foreach (var item in defaultFileList)
            {
                Assembly asm = Assembly.LoadFile(item);

                containerBuilder.RegisterAssemblyTypes(asm).AsImplementedInterfaces().PropertiesAutowired();
            }
            foreach (var item in userFileList)
            {
                Assembly asm = Assembly.LoadFile(item);

                containerBuilder.RegisterAssemblyTypes(asm).AsImplementedInterfaces().PropertiesAutowired();
            }

            //Assembly asm = Assembly.Load("PluginAppFramework");

            //builder.RegisterAssemblyTypes(asm).AsImplementedInterfaces();

            Container = containerBuilder.Build();
        }
    }
}