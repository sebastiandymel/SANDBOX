using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SpectrumVisualizer.Infrastructure
{
    class DesignTimeDataInjector: DependencyObject
    {
        
        public static string GetDesignTimeViewModel(DependencyObject obj)
        {
            return (string)obj.GetValue(DesignTimeViewModelProperty);
        }

        public static void SetDesignTimeViewModel(DependencyObject obj, string value)
        {
            obj.SetValue(DesignTimeViewModelProperty, value);
        }

        // Using a DependencyProperty as the backing store for DesignTimeViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DesignTimeViewModelProperty =
            DependencyProperty.RegisterAttached("DesignTimeViewModel", typeof(string), typeof(DesignTimeDataInjector), new PropertyMetadata(null, OnDesignTimeDataChanged));

        private static void OnDesignTimeDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var typeName = e.NewValue as string;
            if (string.IsNullOrEmpty(typeName))
            {
                return;
            }

            if (DesignerProperties.IsInDesignTool)
            {
                var element = d as FrameworkElement;
                try
                {
                    var requestedType = Type.GetType(typeName);
                    element.DataContext = Activator.CreateInstance(requestedType);
                }
                catch { }
            }
        }

        
    }
}
