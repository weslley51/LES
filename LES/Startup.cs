using LES;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System;
using System.IO;

[assembly: OwinStartup(typeof(Startup))]
namespace LES
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			string Root = AppDomain.CurrentDomain.BaseDirectory;
			var PhysicalFileSystem = new PhysicalFileSystem(Path.Combine(Root, "wwwroot"));

			var Options = new FileServerOptions
			{
				RequestPath = PathString.Empty,
				EnableDefaultFiles = true,
				FileSystem = PhysicalFileSystem
			};

			Options.StaticFileOptions.FileSystem = PhysicalFileSystem;
			Options.StaticFileOptions.ServeUnknownFileTypes = true;
			app.UseFileServer(Options);

			app.UseStaticFiles();
		}
	}
}