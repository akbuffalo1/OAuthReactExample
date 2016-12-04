#if _OAUTH_
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using App3.Shared.Platform.Droid.CurrentActivity;
using Xamarin.Auth;

namespace App3.Plugins.OAuth.Droid
{
    public class OAuthAccountHelper : IOAuthAccountHelper
    {
        private ICurrentActivity _context;
        public OAuthAccountHelper(ICurrentActivity context)
        {
            _context = context;
        }

		public void StartAuthorization(OAuth1Authenticator authentificator)
		{
			var startAuthorizationIntent = authentificator.GetUI(_context.Activity);
			_context.Activity.StartActivity(startAuthorizationIntent);
		}

        public void Save(Account account, string providerName)
        {
            AccountStore.Create(_context.Activity).Save(account, providerName);
        }

        public void Delete(Account account, string providerName)
        {
            AccountStore.Create(_context.Activity).Delete(account, providerName);
        }

        public async Task<IEnumerable<Account>> FindAccountsAsync(string providerName)
        {
            return await AccountStore.Create(_context.Activity).FindAccountsForServiceAsync(providerName);
        }

        public IObservable<IEnumerable<Account>> FindAccountsObservable(string providerName)
        {
            return Observable.Create<IEnumerable<Account>>(async obs =>
            {
                var accounts = await AccountStore.Create(_context.Activity).FindAccountsForServiceAsync(providerName);
                obs.OnNext(accounts);
                obs.OnCompleted();
                return Disposable.Empty;
            });
        }
    }
}
#endif