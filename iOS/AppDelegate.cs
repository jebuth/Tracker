using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace Tracker2.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            // oxyplot told me to add this 
            global::OxyPlot.Xamarin.Forms.Platform.iOS.PlotViewRenderer.Init();
            //
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
