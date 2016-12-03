using System.ComponentModel.Composition;
using UIToolkit;

namespace UIFramework.Modules.Views
{
    /// <summary>
    /// Interaction logic for CirclePanelExample.xaml
    /// </summary>
    [Export("CirclePanel", typeof(UIViewBase))]
    public partial class CirclePanelExample : UIViewBase
    {
        public CirclePanelExample()
        {
            InitializeComponent();
        }
    }
}
