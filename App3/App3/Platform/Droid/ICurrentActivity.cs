#if __ANDROID__

using Android.App;

namespace App3.Shared.Platform.Droid.CurrentActivity
{
    /// <summary>
    /// Current Activity Interface
    /// </summary>
    public interface ICurrentActivity
    {
        /// <summary>
        /// Gets or sets the activity.
        /// </summary>
        /// <value>The activity.</value>
        Activity Activity { get; set; }
    }
}

#endif