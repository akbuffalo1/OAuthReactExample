using Android.OS;
using App3.Shared;
using App3.Shared.Base;
using ReactiveUI.AndroidSupport;

namespace App3.Droid
{
    public class BaseReactiveActivity<TViewModel> : ReactiveActionBarActivity<TViewModel> where TViewModel : class, IViewModelBase
    {
       	protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
			Resolver.Resolve<ICurrentActivity>().Activity = this;

            ViewModel = Resolver.Resolve<TViewModel>();
        }
    }
}