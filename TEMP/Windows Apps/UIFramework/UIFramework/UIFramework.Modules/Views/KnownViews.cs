using System.Collections.Generic;

namespace UIFramework.Modules.Modules.Views
{
    public static class KnownViews
    {
        private static readonly IEnumerable<string> views = new List<string>
                                                            {
                                                                "CirclePanel",
                                                                "TwinButtons"
                                                            };

        public static IEnumerable<string> Views { get { return views; } } 
    }
}