using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using ActionBarDrawerToggle = Android.Support.V7.App.ActionBarDrawerToggle;
using DrawerLayout = Android.Support.V4.Widget.DrawerLayout;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace StatusBarUtil.Sample
{
    [Activity(MainLauncher = true)]
    public class MainActivity : BaseActivity
    {
        DrawerLayout _drawerLayout;
        Toolbar _toolbar;
        CheckBox _chbTranslucent;
        Button _btnSetColor;
        Button _btnSetTransparent;
        Button _btnSetTranslucent;
        Button _btnSetForImageView;
        Button _btnUseInFragment;
        Button _btnSetColorForSwipeBack;

        ViewGroup _contentLayout;
        SeekBar _sbChangeAlpha;
        TextView _tvStatusAlpha;

        int _statusBarColor;
        int _alpha = Jaeger.StatusBarUtil.DefaultStatusBarAlpha;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            _contentLayout = FindViewById<ViewGroup>(Resource.Id.main);
            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _chbTranslucent = FindViewById<CheckBox>(Resource.Id.chb_translucent);
            _btnSetColor = FindViewById<Button>(Resource.Id.btn_set_color);
            _btnSetTransparent = FindViewById<Button>(Resource.Id.btn_set_transparent);
            _btnSetTranslucent = FindViewById<Button>(Resource.Id.btn_set_translucent);
            _btnSetForImageView = FindViewById<Button>(Resource.Id.btn_set_for_image_view);
            _btnUseInFragment = FindViewById<Button>(Resource.Id.btn_use_in_fragment);
            _btnSetColorForSwipeBack = FindViewById<Button>(Resource.Id.btn_set_color_for_swipe_back);
            _sbChangeAlpha = FindViewById<SeekBar>(Resource.Id.sb_change_alpha);
            _tvStatusAlpha = FindViewById<TextView>(Resource.Id.tv_status_alpha);
            SetSupportActionBar(_toolbar);

            var toggle = new ActionBarDrawerToggle(this, _drawerLayout, _toolbar, Resource.String.navigation_drawer_open,
                Resource.String.navigation_drawer_close);
            _drawerLayout.SetDrawerListener(toggle);
            toggle.SyncState();

            _btnSetColor.Click += (s, e) =>
            {
                var intent = new Intent(this, typeof(ColorStatusBarActivity));
                StartActivity(intent);
            };

            _btnSetTransparent.Click += (s, e) =>
            {
                var intent = new Intent(this, typeof(ImageStatusBarActivity));
                intent.PutExtra(ImageStatusBarActivity.ExtraIsTransparent, true);
                StartActivity(intent);
            };

            _btnSetTranslucent.Click += (s, e) =>
            {
                var intent = new Intent(this, typeof(ImageStatusBarActivity));
                intent.PutExtra(ImageStatusBarActivity.ExtraIsTransparent, false);
                StartActivity(intent);
            };

            _btnSetForImageView.Click += (s, e) =>
            {
                var intent = new Intent(this, typeof(ImageViewActivity));
                StartActivity(intent);
            };

            _btnUseInFragment.Click += (s, e) =>
            {
                var intent = new Intent(this, typeof(UseInFragmentActivity));
                StartActivity(intent);
            };

            _btnSetColorForSwipeBack.Click += (s, e) =>
            {
                var intent = new Intent(this, typeof(SwipeBackActivity));
                StartActivity(intent);
            };

            _chbTranslucent.Click += (s, e) =>
            {
                if (_chbTranslucent.Checked)
                {
                    _contentLayout.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.bg_monkey));
                    Jaeger.StatusBarUtil.SetTranslucentForDrawerLayout(this, _drawerLayout, _alpha);
                    _toolbar.SetBackgroundColor(Resources.GetColor(Android.Resource.Color.Transparent));
                }
                else
                {
                    _contentLayout.SetBackgroundDrawable(null);
                    _toolbar.SetBackgroundColor(Resources.GetColor(Resource.Color.colorPrimary));
                    Jaeger.StatusBarUtil.SetColorForDrawerLayout(this, _drawerLayout, Resources.GetColor(Resource.Color.colorPrimary), _alpha);
                }
            };

            _sbChangeAlpha.Max = 255;
            _sbChangeAlpha.ProgressChanged += (s, e) =>
            {
                _alpha = e.Progress;
                if (_chbTranslucent.Checked)
                {
                    Jaeger.StatusBarUtil.SetTranslucentForDrawerLayout(this, _drawerLayout, _alpha);
                }
                else
                {
                    Jaeger.StatusBarUtil.SetColorForDrawerLayout(this, _drawerLayout, _statusBarColor, _alpha);
                }
                _tvStatusAlpha.Text = _alpha.ToString();
            };
            _sbChangeAlpha.Progress = Jaeger.StatusBarUtil.DefaultStatusBarAlpha;
        }

        protected override void SetStatusBar()
        {
            _statusBarColor = Resources.GetColor(Resource.Color.colorPrimary);
            Jaeger.StatusBarUtil.SetColorForDrawerLayout(this, FindViewById<DrawerLayout>(Resource.Id.drawer_layout), _statusBarColor, _alpha);
        }
    }
}
