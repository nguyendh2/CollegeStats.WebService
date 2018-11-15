using Autofac;
using Autofac.Integration.Mvc;
using CollegeStats.BusinessLogic.Interface;
using CollegeStats.BusinessLogic.Service;
using CollegeStats.DataAccess.Dao;
using CollegeStats.DataAccess.Interface;
using CollegeStats.WebService.Controllers;

namespace CollegeStats.WebService
{
    public static class AutoFacMapper
    {
        private static IContainer Container { get; set; }
        public static ILifetimeScope LifeTimeScope { get; set; }
        private static bool IsInitialized { get; set; }
        public static void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AnnualCostDao>().As<IAnnualCostDao>();
            builder.RegisterType<AnnualCostService>().As<IAnnualCostService>();
            builder.RegisterType<AnnualCostController>().InstancePerRequest();
            Container = builder.Build();
            IsInitialized = true;
        }

        public static void BeginLifeTimeScope()
        {
            if(IsInitialized==false)
                Initialize();
            LifeTimeScope = Container.BeginLifetimeScope();
        }

        public static void EndLifeTimeScope()
        {
            LifeTimeScope.Dispose();
        }
    }
}
