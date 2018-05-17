using Android.OS;
using Android.Views;
using Fragment = Android.Support.V4.App.Fragment;

namespace StatusBarUtil.Sample
{
    public class ImageFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragement_image, container, false);
        }
    }
}
