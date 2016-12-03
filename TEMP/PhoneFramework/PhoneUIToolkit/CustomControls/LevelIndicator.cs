using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SEDY.PhoneUIToolkit
{
    public class LevelIndicator : Control
    {
        private Rectangle indicator;
        private Grid track;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.indicator = GetTemplateChild("ProgressBarIndicator") as Rectangle;
            this.track = GetTemplateChild("DeterminateRoot") as Grid;
        }

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(LevelIndicator), new PropertyMetadata((double)0.0, OnValueChanged));

        public double Max
        {
            get { return (double)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        public static DependencyProperty MaxProperty = DependencyProperty.Register("Max", typeof(double), typeof(LevelIndicator), new PropertyMetadata((double)100.0, OnValueChanged));

        public bool UseAnimation
        {
            get { return (bool)GetValue(UseAnimationProperty); }
            set { SetValue(UseAnimationProperty, value); }
        }

        public static readonly DependencyProperty UseAnimationProperty = DependencyProperty.Register("UseAnimation", typeof(bool), typeof(LevelIndicator), new PropertyMetadata(true));


        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LevelIndicator).InvalidateMeasure();
        }


        private void UpdateProgress(double width)
        {
            if (this.indicator == null && this.track == null)
            {
                ApplyTemplate();
            }

            if (this.indicator == null || this.track == null)
            {
                return;
            }

            var value = GetCoercedValue();
            var factor = GetFactor(value);
            var progressWidth = width;
            var finalWidth = factor * progressWidth;

            if (UseAnimation)
            {
                var widthAnimation = new DoubleAnimation
                                     {
                                         From = Double.IsNaN(indicator.Width) ? 0 : indicator.Width,
                                         To = finalWidth,
                                         Duration = new Duration(TimeSpan.FromMilliseconds(50))
                                     };

                var storyboard = new Storyboard();
                storyboard.Children.Add(widthAnimation);
                Storyboard.SetTarget(widthAnimation, indicator);
                Storyboard.SetTargetProperty(widthAnimation, new PropertyPath("Rectangle.Width"));
                storyboard.Begin();
            }
            else
            {
                indicator.Width = finalWidth;
            }
        }

        private double GetCoercedValue()
        {
            if (double.IsNaN(Value))
            {
                return 0;
            }

            if (Value > Max)
            {
                return Max;
            }

            return Value;
        }

        private double GetFactor(double value)
        {
            var factor = double.IsInfinity(value) ? 1 : value/Max;
            if (double.IsNaN(factor))
            {
                factor = 0;
            }
            return factor;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            UpdateProgress(availableSize.Width);
            return base.MeasureOverride(availableSize);
        }
    }
}
