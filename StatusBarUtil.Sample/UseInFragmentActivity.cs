using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Com.Ashokvarma.Bottomnavigation;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace StatusBarUtil.Sample
{
    [Activity]
    public class UseInFragmentActivity : BaseActivity
    {
        class TabSelectedListener : Java.Lang.Object, BottomNavigationBar.IOnTabSelectedListener
        {
            ViewPager _vpHome;

            public TabSelectedListener(ViewPager vpHome)
            {
                _vpHome = vpHome;
            }

            public void OnTabReselected(int position) { }

            public void OnTabSelected(int position)
            {
                _vpHome.CurrentItem = position;
            }

            public void OnTabUnselected(int position) { }
        }

        class FragmentAdapter : FragmentPagerAdapter
        {
            List<Fragment> _fragmentList;

            public FragmentAdapter(FragmentManager fm, List<Fragment> fragmentList) : base(fm)
            {
                _fragmentList = fragmentList;
            }

            public override int Count => _fragmentList.Count;

            public override Fragment GetItem(int position) => _fragmentList[position];
        }

        ViewPager _vpHome;
        BottomNavigationBar _bottomNavigationBar;
        List<Fragment> _fragmentList = new List<Fragment>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_use_in_fragment);
            _vpHome = FindViewById<ViewPager>(Resource.Id.vp_home);
            _bottomNavigationBar = FindViewById<BottomNavigationBar>(Resource.Id.bottom_navigation_bar);
            _bottomNavigationBar.AddItem(new BottomNavigationItem(Resource.Drawable.ic_favorite, "One"))
                .AddItem(new BottomNavigationItem(Resource.Drawable.ic_gavel, "Two"))
                .AddItem(new BottomNavigationItem(Resource.Drawable.ic_grade, "Three"))
                .AddItem(new BottomNavigationItem(Resource.Drawable.ic_group_work, "Four"))
                .Initialise();

            _bottomNavigationBar.SetTabSelectedListener(new TabSelectedListener(_vpHome));

            _fragmentList.Add(new ImageFragment());
            _fragmentList.Add(new SimpleFragment());
            _fragmentList.Add(new SimpleFragment());
            _fragmentList.Add(new SimpleFragment());

            _vpHome.PageSelected += (s, e) =>
            {
                _bottomNavigationBar.SelectTab(e.Position);
                switch (e.Position)
                {
                    case 0:
                        break;
                    default:
                        var random = new Random();
                        var color = (int)(0xff000000 | random.Next(0xffffff));
                        if (_fragmentList[e.Position] is SimpleFragment)
                        {
                            ((SimpleFragment)_fragmentList[e.Position]).SetTvTitleBackgroundColor(color);
                        }
                        break;
                }
            };
            _vpHome.Adapter = new FragmentAdapter(SupportFragmentManager, _fragmentList);
        }

        protected override void SetStatusBar()
        {
            Jaeger.StatusBarUtil.SetTranslucentForImageViewInFragment(this, null);
        }
    }
}
