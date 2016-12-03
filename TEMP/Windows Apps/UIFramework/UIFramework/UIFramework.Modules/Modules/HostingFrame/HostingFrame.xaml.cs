using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace UIFramework.Modules.Modules
{
    /// <summary>
    /// Interaction logic for HostingFrame.xaml
    /// </summary>
    [Export(typeof(HostingFrame))]
    [Export(typeof(IContentHost))]
    public partial class HostingFrame : UserControl, IContentHost
    {
        public HostingFrame()
        {
            InitializeComponent();
        }

        public void SetContent(FrameworkElement content)
        {
            this.FrameContent.Content = content;
        }
    }
}
