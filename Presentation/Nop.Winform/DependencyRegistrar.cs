using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Services.Tests;

namespace Nop.Winform
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            //注入ObjectContext
            builder.Register<IDbContext>(c => new NopObjectContext("test")).InstancePerLifetimeScope();

            // 注入ef到仓储
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            // 注入Service及接口
            builder.RegisterAssemblyTypes(typeof(TestService).Assembly)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();

            //注入controllers
            //builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            //注入forms
            var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IRegistrarForm))))
            .ToArray(); 
            foreach (var formtype in types)
            {
                builder.RegisterAssemblyTypes(formtype.Assembly); 
            }

        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order
        {
            get { return 2; }
        }
    }
}
