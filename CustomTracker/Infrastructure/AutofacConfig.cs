using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CustomTracker.Models.Abstract;
using CustomTracker.Models.Concrete;

namespace CustomTracker.Infrastructure
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            // получаем экземпляр контейнера
            var builder = new ContainerBuilder();

            // регистрируем контроллер в текущей сборке
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            
            // регистрируем споставление типов
            builder.RegisterType<EFRepository>().As<IRepository>()
                .WithParameter("context", new EFDbContext());

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
          .AppSettings["Email.WriteAsFile"] ?? "false")
            };
           builder.RegisterType<EmailQueryProcessor>().As<IQueryProcessor>().WithParameter("settings", emailSettings);
            /*
            ninjectKernel.Bind<IOrderProcessor>()
              .To<EmailOrderProcessor>()
              .WithConstructorArgument("settings", emailSettings);
            */
            // создаем новый контейнер с теми зависимостями, которые определены выше
            var container = builder.Build();

            // установка сопоставителя зависимостей
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}