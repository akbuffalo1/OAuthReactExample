using System;
using Android.App;
using Android.Runtime;
using App3.Plugins.OAuth;
using App3.Plugins.OAuth.Droid;
using App3.Shared;
using App3.Shared.DI;
using App3.Shared.Plugins.Json;
using App3.Shared.Plugins.OAuth;
using TinyIoC;

namespace App3.Droid
{
	[Application(Name = "com.example.App3", Theme = "@style/AppTheme")]
	public class App : Application
	{
		public App(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }

		public override void OnCreate()
		{
			base.OnCreate();

			var ioc = new App3.Shared.DI.TinyContainer(TinyIoCContainer.Current);

			Resolver.SetResolver(ioc.GetResolver());
			ioc.Register<IDependencyContainer>(ioc);

			ioc.Register<ICurrentActivity, CurrentActivity>();
			ioc.Register<IJsonFileReader, JsonFileReader>();
			ioc.Register<IOAuthServiceProvider, OAuthServiceProvider>();
			ioc.Register<IOAuthAccountHelper, OAuthAccountHelper>();
			ioc.Register<ITwitterService, TwitterService>();
			ioc.Register<ITwitterTestViewModel, TwitterTestViewModel>();
		}
	}
}