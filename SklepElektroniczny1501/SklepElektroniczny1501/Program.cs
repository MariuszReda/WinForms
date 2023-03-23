using Autofac;
using SklepElektroniczny1501.Implementations;
using SklepElektroniczny1501.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SklepElektroniczny1501
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<DatabaseOpeartions>().As<IDatabaseOpeartions>();
            builder.RegisterType<Mapper.Mapper>().As<IMapper>();
           // builder.RegisterType<Produkty>().As<Form>();
           // builder.RegisterType<Produkty>().AsSelf().InstancePerDependency();
          //  builder.RegisterType<Sklep>().AsSelf().InstancePerDependency();

            IContainer container = builder.Build();


            Application.Run(new Sklep(container));//container.Resolve<Sklep>()
        }
    }
}
