using Android.Content;
using Xamarin.Forms.Platform.Android;
using Android.Gms.Ads;
using Xamarin.Forms;
using MasterDetail.Controls;
using MasterDetail.Droid.Renderer;

[assembly: ExportRenderer(typeof(AdMobView), typeof(AdMobRenderer))]
namespace MasterDetail.Droid.Renderer
{
    public class AdMobRenderer : ViewRenderer
    {
        public AdMobRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if ((Element is AdMobView adMobElement) && (e.OldElement == null))
            {
                var ad = new AdView(Context)
                {
                    AdSize = AdSize.SmartBanner,
                    AdUnitId = "ca-app-pub-5340043973194653/6114982135"

                };
                
                var requestbuilder = new AdRequest.Builder();
                requestbuilder.AddTestDevice("E87386B52557DA63E5D592B582D47DAB");
                ad.LoadAd(requestbuilder.Build());
                
                SetNativeControl(ad);
            }
        }
    }
}