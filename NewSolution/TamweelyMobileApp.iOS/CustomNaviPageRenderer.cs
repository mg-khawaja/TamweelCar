using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TamweelyMobileApp.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNaviPageRenderer))]

namespace TamweelyMobileApp.iOS
{
    public class CustomNaviPageRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            OverrideUserInterfaceStyle = UIUserInterfaceStyle.Light;
        }
    }
}