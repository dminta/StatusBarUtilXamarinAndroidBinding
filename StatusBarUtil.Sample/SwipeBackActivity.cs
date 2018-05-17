using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using System;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace StatusBarUtil.Sample
{
    [Activity]
    public class SwipeBackActivity : BaseActivity
    {
        Button _btnChangeColor;
        int _color = Color.Gray;
        Toolbar _toolbar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Com.R0adkll.Slidr.Slidr.Attach(this);
            SetContentView(Resource.Layout.swipe_back_activity);
            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _btnChangeColor = FindViewById<Button>(Resource.Id.btn_change_color);

            SetSupportActionBar(_toolbar);
            SupportActionBar?.SetDisplayHomeAsUpEnabled(true);

            _toolbar.SetBackgroundColor(new Color(_color));
            _btnChangeColor.Click += (s, e) =>
            {

                var random = new Random();
                _color = (int)(0xff000000 | random.Next(0xffffff));
                _toolbar.SetBackgroundColor(new Color(_color));
                Jaeger.StatusBarUtil.SetColorForSwipeBack(this, _color, 38);
            };
        }

        protected override void SetStatusBar()
        {
            Jaeger.StatusBarUtil.SetColorForSwipeBack(this, _color, 38);
        }
    }
}
