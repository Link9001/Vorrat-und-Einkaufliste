using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HouseholdmanagementTool.UI.UIHelpers
{
    public static class WindowExtentionClass
    {
        public static bool IsOpen<T>(this Window window)
            where T : Window
        {
            return Application.Current.Windows.OfType<T>().Any();
        }

        public static bool IsOpen<T>(this Window window, string windowName)
            where T : Window
        {
            return Application.Current.Windows.OfType<T>().Any(x => x.Name == windowName);
        }
    }
}
