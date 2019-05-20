using LES.Structure;
using LES.Data;
using LES.Data.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Data.Entity;
using System.Web.Http;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(LES.App_Start.SimpleInjector), "Initialize")]
namespace LES.App_Start
{
	public static class SimpleInjector
	{
		public static void Initialize()
		{
			var container = new Container();
			container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

			InitializeContainer(container);

			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
			container.Verify();
			GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
		}

		private static void InitializeContainer(Container container)
		{
			container.Register<DbContext>(() => new Contexto(), Lifestyle.Scoped);

			container.Register(
				typeof(IRepository<>),
				new[] { typeof(Startup).Assembly });

			container.Register(
				typeof(IBusiness<>),
				new[] { typeof(Startup).Assembly });

			container.Collection.Register(
				typeof(IStrategy<>),
				new[] { typeof(Startup).Assembly });
						
			container.RegisterConditional(typeof(IRepository<>), typeof(Repository<>), c => !c.Handled);
			container.RegisterConditional(typeof(IBusiness<>), typeof(Business<>), c => !c.Handled);
		}
	}
}