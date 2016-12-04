#if __ANDROID__
using System;
using Android.App;

namespace App3
{
	public interface ICurrentActivity
	{
		Activity Activity { get; set; }
	}

	public class CurrentActivity : ICurrentActivity
	{
		public Activity Activity
		{
			get;
			set;
		}
	}
}
#endif
