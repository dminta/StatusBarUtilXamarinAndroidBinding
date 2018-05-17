using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Color = Android.Graphics.Color;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace StatusBarUtil.Sample
{
    [Activity(Label = "@string/app_name")]
    public class ColorStatusBarActivity : BaseActivity
    {
        Toolbar _toolbar;
        Button _btnChangeColor;
        SeekBar _sbChangeAlpha;
        TextView _tvStatusAlpha;

        int _color;
        int _alpha;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_color_status_bar);

            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _btnChangeColor = FindViewById<Button>(Resource.Id.btn_change_color);
            _tvStatusAlpha = FindViewById<TextView>(Resource.Id.tv_status_alpha);
            _sbChangeAlpha = FindViewById<SeekBar>(Resource.Id.sb_change_alpha);

            SetSupportActionBar(_toolbar);
            SupportActionBar?.SetDisplayHomeAsUpEnabled(true);

            _btnChangeColor.Click += (s, e) =>
            {
                var random = new Random();
                _color = (int)(0xff000000 | random.Next(0xffffff));
                _toolbar.SetBackgroundColor(new Color(_color));
                Jaeger.StatusBarUtil.SetColor(this, _color, _alpha);
            };

            _sbChangeAlpha.Max = 255;
            _sbChangeAlpha.ProgressChanged += (s, e) =>
            {
                _alpha = e.Progress;
                Jaeger.StatusBarUtil.SetColor(this, _color, _alpha);
                _tvStatusAlpha.Text = _alpha.ToString();
            };
            _sbChangeAlpha.Progress = Jaeger.StatusBarUtil.DefaultStatusBarAlpha;
        }

        protected override void SetStatusBar()
        {
            _color = Resources.GetColor(Resource.Color.colorPrimary);
            Jaeger.StatusBarUtil.SetColor(this, _color);
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
