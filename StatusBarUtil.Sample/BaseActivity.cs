using Android.Support.V7.App;

namespace StatusBarUtil.Sample
{
    public class BaseActivity : AppCompatActivity
    {
        public override void SetContentView(int layoutResID)
        {
            base.SetContentView(layoutResID);
            SetStatusBar();
        }

        protected virtual void SetStatusBar()
        {
            Jaeger.StatusBarUtil.SetColor(this, Resources.GetColor(Resource.Color.colorPrimary));
        }
    }
}
