using TamweelyMobileApp.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(CustomPageRenderer))]

namespace TamweelyMobileApp.iOS
{
    public class CustomPageRenderer : PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            OverrideUserInterfaceStyle = UIUserInterfaceStyle.Light;
        }
    }
}