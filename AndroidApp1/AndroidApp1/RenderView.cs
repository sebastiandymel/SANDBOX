using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;

namespace AndroidApp1
{
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