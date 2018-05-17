using Android.OS;
using Android.Views;
using Android.Widget;
using Color = Android.Graphics.Color;
using Fragment = Android.Support.V4.App.Fragment;

namespace StatusBarUtil.Sample
{
    public class SimpleFragment : Fragment
    {
        TextView _tvTitle;
        View _fakeStatusBar;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragement_simple, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            _tvTitle = view.FindViewById<TextView>(Resource.Id.tv_title);
            _fakeStatusBar = view.FindViewById(Resource.Id.fake_status_bar);
        }

        public void SetTvTitleBackgroundColor(int color)
        {
            _tvTitle.SetBackgroundColor(new Color(color));
            _fakeStatusBar.SetBackgroundColor(new Color(color));
        }
    }
}
