using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UIToolkit
{
    public class TwinButton : Control
    {
        public static readonly DependencyProperty Command1Property = DependencyProperty.Register(
            "Command1", typeof (ICommand), typeof (TwinButton), new PropertyMetadata(default(ICommand)));
        
        public static readonly DependencyProperty Command2Property = DependencyProperty.Register(
            "Command2", typeof(ICommand), typeof(TwinButton), new PropertyMetadata(default(ICommand)));
        
        public static readonly DependencyProperty Content1Property = DependencyProperty.Register(
            "Content1", typeof(object), typeof(TwinButton), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty Content2Property = DependencyProperty.Register(
            "Content2", typeof(object), typeof(TwinButton), new PropertyMetadata(default(object)));

        public ICommand Command1
        {
            get { return (ICommand) GetValue(Command1Property); }
            set { SetValue(Command1Property, value); }
        }
        
        public ICommand Command2
        {
            get { return (ICommand) GetValue(Command2Property); }
            set { SetValue(Command2Property, value); }
        }
        
        public object Content1
        {
            get { return (object) GetValue(Content1Property); }
            set { SetValue(Content1Property, value); }
        }

        public object Content2
        {
            get { return (object) GetValue(Content2Property); }
            set { SetValue(Content2Property, value); }
        }
    }
}