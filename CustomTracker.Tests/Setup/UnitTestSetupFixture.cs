using Autofac;
using Autofac.Extras.Moq;
using Autofac.Integration.Mvc;
using CustomTracker.Models.Abstract;
using CustomTracker.Models.Concrete;
using CustomTracker.Tests.Moq.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CustomTracker.Tests.Setup
{
    //[SetUpFixture]
    class UnitTestSetupFixture
    {
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("===============");
            Console.WriteLine("=====START=====");
            Console.WriteLine("===============");
            InitBuilder();

        }

        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("===============");
            Console.WriteLine("=====BYE!======");
            Console.WriteLine("===============");
        }
        
        protected void InitBuilder()
        {

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<MockEFRepository>().As<IRepository>();
            //builder.RegisterType<RoleStore<IdentityRole>>().As<IRoleStore<IdentityRole, string>>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
