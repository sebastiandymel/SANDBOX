using System.ComponentModel.Composition;
using System.Windows;

namespace UIFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export("Shell", typeof(Window))]
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();
        }
    }
}
