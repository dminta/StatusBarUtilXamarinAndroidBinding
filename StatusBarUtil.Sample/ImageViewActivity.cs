using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace StatusBarUtil.Sample
{
    [Activity]
    public class ImageViewActivity : BaseActivity
    {
        Toolbar _toolbar;
        View _viewNeedOffset;
        SeekBar _sbChangeAlpha;
        TextView _tvStatusAlpha;

        int _alpha;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_image_view);

            Com.R0adkll.Slidr.Slidr.Attach(this);

            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _viewNeedOffset = FindViewById(Resource.Id.view_need_offset);
            _tvStatusAlpha = FindViewById<TextView>(Resource.Id.tv_status_alpha);
            _sbChangeAlpha = FindViewById<SeekBar>(Resource.Id.sb_change_alpha);

            SetSupportActionBar(_toolbar);
            SupportActionBar?.SetDisplayHomeAsUpEnabled(true);

            _sbChangeAlpha.Max = 255;
            _sbChangeAlpha.ProgressChanged += (s, e) =>
            {
                _alpha = e.Progress;
                Jaeger.StatusBarUtil.SetTranslucentForImageView(this, _alpha, _viewNeedOffset);
                _tvStatusAlpha.Text = _alpha.ToString();
            };
            _sbChangeAlpha.Progress = Jaeger.StatusBarUtil.DefaultStatusBarAlpha;
        }

        protected override void SetStatusBar()
        {
            _viewNeedOffset = FindViewById(Resource.Id.view_need_offset);
            Jaeger.StatusBarUtil.SetTranslucentForImageView(this, _viewNeedOffset);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}
