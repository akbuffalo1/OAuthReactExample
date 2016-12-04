using System.Linq;
using System.Reactive.Linq;
using Newtonsoft.Json.Linq;
using ReactiveUI.Fody.Helpers;
using ReactiveUI;
using System.Reactive.Subjects;
using App3.Shared.Base;
using App3.Shared.Reactive;

namespace App3.Shared
{
	public interface ITwitterTestViewModel : IViewModelBase
    {
        IReactiveCommand GetProfileCommand { get; }
		IReactiveCommand LogInCommand { get; }
		IReactiveCommand LogOutCommand { get; }
        bool IsLoaderShowing { get; }
        JObject UserData { get; }
        string Error { get; }
        void CheckInitialized();
        bool IsAuthorized { get; }
    }

    public class TwitterTestViewModel : ReactiveViewModel, ITwitterTestViewModel
    {
        public IReactiveCommand GetProfileCommand { get; protected set; }

		public IReactiveCommand LogInCommand { get; protected set; }

		public IReactiveCommand LogOutCommand { get; protected set; }

        public ISubject<bool> ApiAuthorizedSubject { get; protected set; }

		[Reactive]
		public bool IsLoaderShowing { get; protected set; } = false;

        [Reactive] 
        public JObject UserData { get; set; }

        [Reactive] 
        public string Error { get; set; }

        [Reactive] 
        public bool IsAuthorized { get; set; }

		private readonly ITwitterService _service;
        public TwitterTestViewModel(ITwitterService service = null)
        {
            _service = service ?? Resolver.Resolve<ITwitterService>();

			this.WhenActivated(registerDisposable =>
			{
				var canLogoutOrGetProfile = _service.CurrentAccount.AsObservable().Select(account => null != account);
				var canAuthorize = _service
					.ServiceReadySub
					.AsObservable()
					.Select(authenticator => null != authenticator)
					.CombineLatest(canLogoutOrGetProfile, (arg1, arg2) => arg1 && !arg2);

				LogInCommand = ReactiveCommand.CreateAsyncObservable(canAuthorize, args => Observable.Start(() => _service.Authorize()));
				LogOutCommand = ReactiveCommand.CreateAsyncObservable(canLogoutOrGetProfile, args => Observable.Start(async () => await _service.Logout()));
				GetProfileCommand = ReactiveCommand.CreateAsyncObservable(canLogoutOrGetProfile, args => Observable.Start(() =>
				{
					IsLoaderShowing = true;
					_service
						.GetUserData()
						.ObserveOnUI()
						.Catch(ex =>
						{
							IsLoaderShowing = false;
							Error = ex.Message;
						}).SubscribeOnce(data =>
						{
							UserData = data;
							IsLoaderShowing = false;
						});
				}));
			});
        }

        public void CheckInitialized() => _service.Init();
    }
}
