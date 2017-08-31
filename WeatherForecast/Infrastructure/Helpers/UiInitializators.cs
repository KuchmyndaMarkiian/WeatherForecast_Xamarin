using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Com.Wang.Avi;

namespace WeatherForecast.Infrastructure.Helpers
{
    static class UiInitializators
    {
        public static void InitializeLoadingDialog(this Dialog dialog)
        {
            dialog.SetContentView(Resource.Layout.loadingDialog);
            dialog.SetCancelable(false);
            dialog.Window.SetBackgroundDrawable(new ColorDrawable(Color.Transparent));
            WindowManagerLayoutParams layoutParams = new WindowManagerLayoutParams();
            layoutParams.CopyFrom(dialog.Window.Attributes);
            layoutParams.Height = layoutParams.Width = ViewGroup.LayoutParams.WrapContent;
            dialog.Window.Attributes = layoutParams;
            var indicatorView = dialog.FindViewById<AVLoadingIndicatorView>(Resource.Id.indicator);
            indicatorView.SmoothToShow();
        }
    }
}