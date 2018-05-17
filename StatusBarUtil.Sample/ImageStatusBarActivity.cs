using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace StatusBarUtil.Sample
{
    [Activity]
    public class ImageStatusBarActivity : BaseActivity
    {
        public const string ExtraIsTransparent = "is_transparent";

        TextView _tvStatusAlpha;
        RelativeLayout _rootLayout;
        Button _btnChangeBackground;
        bool _isBgChanged;
        SeekBar _sbChangeAlpha;

        bool _isTransparent;
        int _alpha;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            _isTransparent = Intent.GetBooleanExtra(ExtraIsTransparent, false);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_image_status_bar);

            _rootLayout = FindViewById<RelativeLayout>(Resource.Id.root_layout);
            _btnChangeBackground = FindViewById<Button>(Resource.Id.btn_change_background);
            _tvStatusAlpha = FindViewById<TextView>(Resource.Id.tv_status_alpha);
            _sbChangeAlpha = FindViewById<SeekBar>(Resource.Id.sb_change_alpha);

            _btnChangeBackground.Click += (s, e) =>
            {
                _isBgChanged = !_isBgChanged;
                if (_isBgChanged)
                {
                    _rootLayout.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.bg_girl));
                }
                else
                {
                    _rootLayout.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.bg_monkey));
                }
            };

            if (_isTransparent)
            {
                _sbChangeAlpha.Visibility = ViewStates.Visible;
                SetSeekBar();
            }
            else
            {
                _sbChangeAlpha.Visibility = ViewStates.Gone;
            }
        }

        protected override void SetStatusBar()
        {
            if (_isTransparent)
            {
                Jaeger.StatusBarUtil.SetTransparent(this);
            }
            else
            {
                Jaeger.StatusBarUtil.SetTranslucent(this, Jaeger.StatusBarUtil.DefaultStatusBarAlpha);
            }
        }

        void SetSeekBar()
        {
            _sbChangeAlpha.Max = 255;
            _sbChangeAlpha.ProgressChanged += (s, e) =>
            {
                _alpha = e.Progress;
                Jaeger.StatusBarUtil.SetTranslucent(this, _alpha);
                _tvStatusAlpha.Text = _alpha.ToString();
            };
            _sbChangeAlpha.Progress = Jaeger.StatusBarUtil.DefaultStatusBarAlpha;
        }
    }
}
