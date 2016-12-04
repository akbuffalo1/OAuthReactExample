using App3.Shared.Base;
using ReactiveUI;

namespace App3.Shared.Reactive
{
	public class ReactiveViewModel : ReactiveObject, ISupportsActivation, IViewModelBase
    {
        private readonly ViewModelActivator _viewModelActivator = new ViewModelActivator();

        public ViewModelActivator Activator
        {
            get { return _viewModelActivator; }
        }
    }
}