using System;
using System.Windows;
using System.Windows.Controls;

namespace SEDY.PhoneUIToolkit
{
    public class UniformPanel : Panel
    {
        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register("Columns", typeof(int), typeof(UniformPanel), new PropertyMetadata(-1, OnMetricsChanged));
        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register("Rows", typeof(int), typeof(UniformPanel), new PropertyMetadata(-1, OnMetricsChanged));

        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        private static void OnMetricsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as UniformPanel).UpdateLayout();
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

        protected override Size ArrangeOverride(Size finalSize)
        {
            int maxColumns = Columns;
            int maxRows = Rows;
            if (Columns == -1 && Rows == -1)
            {
                maxColumns = 1;
                maxRows = Children.Count;
            }
            else if (Columns == -1 && Rows > 0)
            {
                maxColumns = GetDesiredCount(maxRows);
            }
            else if (Columns > 0 && Rows == -1)
            {
                maxRows = GetDesiredCount(maxColumns);
            }

            var singleCellSize = new Size(finalSize.Width / maxColumns, finalSize.Height / maxRows);
            int rowIndex = 0, colIndex = 0;
            foreach (UIElement child in Children)
            {
                child.Arrange(new Rect(new Point(singleCellSize.Width * colIndex, singleCellSize.Height * rowIndex), singleCellSize));
                if (maxColumns == ++colIndex)
                {
                    rowIndex++;
                    colIndex = 0;
                }
            }
            return finalSize;
        }

        private int GetDesiredCount(int maxRows)
        {
            return Convert.ToInt32(Math.Ceiling((double)this.Children.Count / maxRows));
        }
    }

}
