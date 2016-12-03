using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace UIToolkit
{
    /// <summary>
    /// BindableStaticResource is a replacement of StaticResource extension methods in WPF. It can be used where
    /// the resource-key is not statically available in the xaml code 
    ///     (like: {StaticResource ImAKey}. Instead it
    /// can be used where one would like to have a binding to the key 
    ///     (like: {StaticResource ResourceKey={Binding Path=SomeObject.KeyValue}}
    /// The above example can now be achieved using BindableStaticResource instead of StaticResource. The taskpane
    /// uses this class. Here a working example:
    ///     Content="{markupExt:BindableStaticResource {Binding Path=Attribute[Text].Value}}"
    /// </summary>
    public class BindableStaticResource : StaticResourceExtension
    {
        private static readonly DependencyProperty SourceProperty = DependencyProperty.RegisterAttached("SourceDp",
                                                                typeof(string),
                                                                typeof(DependencyObject),
                                                                new UIPropertyMetadata(null));

        public Binding ResourceBinding { get; set; }

        public BindableStaticResource()
        {
        }

        public BindableStaticResource(Binding binding)
        {
            this.ResourceBinding = binding;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var target = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
            var targetObject = (FrameworkElement)target.TargetObject;

            // Check if ResourceBinding is set - this might not be the case at design time:
            if (this.ResourceBinding == null)
                return null;

            // 
            // Connects two dependency properties using the wpf Binding system:
            // 

            this.ResourceBinding.Source = targetObject.DataContext;
            var newTargetObject = new DependencyObject();
            BindingOperations.SetBinding(newTargetObject, SourceProperty, this.ResourceBinding);
            ResourceKey = newTargetObject.GetValue(SourceProperty);

            if (string.IsNullOrEmpty((string)ResourceKey))
            {
                return null;
            }
            else
            {
                var resourceValue = base.ProvideValue(serviceProvider);
                // Use the "ProvideValue" from the base that implements the resource lookup:
                return resourceValue;
            }
        }

        /// <summary>
        /// Hides the ResourceKey from the base class in order to prevent crashes at 
        /// design time when a resource key is null:
        /// </summary>
        public new object ResourceKey
        {
            get
            {
                return base.ResourceKey;
            }
            set
            {
                if (value != null)
                    base.ResourceKey = value;
            }
        }
    }
}
