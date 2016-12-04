using System;
using System.IO;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Android.Content.Res;
using App3.Shared.Platform.Droid.CurrentActivity;
using App3.Shared.Reactive;
using Newtonsoft.Json.Linq;

namespace App3.Shared.Plugins.Json
{
    public interface IJsonFileReader {
        IObservable<JObject> LoadJson(string fileName);
    }

    public class JsonFileReader : IJsonFileReader
    {
       public IObservable<JObject> LoadJson(string fileName)
        {
            return Observable.Create<JObject>(obs =>
            {
                try
                {
                    string configFileContent;    
#if __ANDROID__
                    AssetManager assets = Resolver.Resolve<ICurrentActivity>().Activity.Assets;
                    using (StreamReader sr = new StreamReader(assets.Open(fileName)))
                    {
                        configFileContent = sr.ReadToEnd();
                    }
#else
                    using (StreamReader r = new StreamReader(_fileFullName))
                    {
                        configFileContent = r.ReadToEnd();
                    }
#endif
                    obs.OnNext(JObject.Parse(configFileContent));
                }
                catch (Exception ex)
                { 
                    obs.OnError(ex);
                    throw ex;
                }
                finally
                {
                    obs.OnCompleted();
                }
                return Disposable.Empty;
            }).ObserveOnUI();
        }
    }
}
