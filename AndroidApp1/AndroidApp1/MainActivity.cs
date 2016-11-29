using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;

namespace AndroidApp1
{
    [Activity(Label = "AndroidApp1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
            var button = FindViewById<Button>(Resource.Id.button1);
            button.Click += OnButtonClick;
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            // var view = FindViewById<View>(Resource.Id.view1);
            
        }
    }

    public class RenderView : View
    {
        private Paint paint;

        public RenderView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public RenderView(Context context) : base(context)
        {
            paint = new Paint();
        }

        public RenderView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public RenderView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public RenderView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected void onDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            int x = Width;
            int y = Height;
            int radius;
            radius = 100;
            paint.SetStyle(Paint.Style.Fill);
            paint.Color = Color.Wheat;
            canvas.DrawPaint(paint);
            // Use Color.parseColor to define HTML colors
            paint.Color = Color.ParseColor("#CD5C5C");
            canvas.DrawCircle(x / 2, y / 2, radius, paint);
        }
    }
}

