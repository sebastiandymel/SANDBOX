using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UIToolkit
{
    public class CirclePanel : Panel
    {
        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register("StartAngle", typeof (double), typeof (CirclePanel),
                new FrameworkPropertyMetadata(-90.0, FrameworkPropertyMetadataOptions.AffectsArrange));
        
        public double StartAngle
        {
            get { return (double) GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }

        public static readonly DependencyProperty RotateChildrenProperty = DependencyProperty.Register(
            "RotateChildren", typeof (bool), typeof (CirclePanel), new PropertyMetadata(true));

        public bool RotateChildren
        {
            get { return (bool) GetValue(RotateChildrenProperty); }
            set { SetValue(RotateChildrenProperty, value); }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Children.Count == 0)
            {
                return finalSize;
            }

            var startingAngle = StartAngle;
            var angle = ConvertToRad(startingAngle);

            // Convert to radians
            var radialAreaPerChild = 360.0/Children.Count;
            var incrementalAngularSpace = ConvertToRad(radialAreaPerChild);

            var radiusX = finalSize.Width/3;
            var radiusY = finalSize.Height/3;

            foreach (UIElement elem in Children)
            {
                //Calculate the point on the circle for the element
                Point childPoint = new Point(Math.Cos(angle)*radiusX, Math.Sin(angle)*radiusY);

                //Offsetting the point to the Avalable rectangular area which is FinalSize.
                Point actualChildPoint = new Point(finalSize.Width/2 + childPoint.X - elem.DesiredSize.Width/2,
                    finalSize.Height/2 + childPoint.Y - elem.DesiredSize.Height/2);

                //Call Arrange method on the child element by giving the calculated point as the placementPoint.
                elem.Arrange(new Rect(actualChildPoint.X, actualChildPoint.Y, elem.DesiredSize.Width,
                    elem.DesiredSize.Height));

                if (RotateChildren)
                {
                    // Rotate element
                    elem.RenderTransformOrigin = new Point(0.5, 0.5);
                    elem.RenderTransform = new RotateTransform() {Angle = ConvertToDegrees(angle) - startingAngle};
                }

                //Calculate the new angle for the next element
                angle += incrementalAngularSpace;
            }

            return finalSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement child in Children)
            {
                child.Measure(availableSize);
            }

            return new Size(double.IsPositiveInfinity(availableSize.Width) ? 0 : availableSize.Width,
                double.IsPositiveInfinity(availableSize.Height) ? 0 : availableSize.Height);
        }

        private static double ConvertToDegrees(double rad)
        {
            return rad*180/Math.PI;
        }

        private static double ConvertToRad(double degrees)
        {
            return degrees*(Math.PI/180);
        }
    }
}
