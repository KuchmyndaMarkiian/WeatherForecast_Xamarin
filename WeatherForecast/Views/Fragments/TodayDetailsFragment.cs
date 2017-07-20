using Android.App;
using Android.OS;
using Android.Views;

namespace WeatherForecast.Views.Fragments
{
    public class TodayDetailsFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.todayDetailsFragment, container, false);
        }
    }
}