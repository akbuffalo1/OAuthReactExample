#if __ANDROID__

using Android.App;
using System;


namespace App3.Shared.Platform.Droid.CurrentActivity
{
    /// <summary>
    /// Implementation for Feature
    /// </summary>
    public class CurrentActivity : ICurrentActivity
    {
        /// <summary>
        /// Gets or sets the activity.
        /// </summary>
        /// <value>The activity.</value>
        public Activity Activity
        {
            get;
            set;
        }
    }
}

#endif