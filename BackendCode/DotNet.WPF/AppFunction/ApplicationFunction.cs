using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.WPF.ApplicationFunction
{
    public class ApplicationFunction
    {

        public void SetUpResourceDictionaries(Theme theme = Theme.Light)
        {
            // Clear any existing merged dictionaries from the application resources.
            App.Current.Resources.MergedDictionaries.Clear();

            // Add the generic resource dictionary and generic styles resource dictionary.
            App.Current.Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("Theme/Generic.xaml", UriKind.Relative) });
            App.Current.Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("Theme/GenericStyles.xaml", UriKind.Relative) });

            // Based on the specified theme (default is Light), add the corresponding theme resource dictionary.
            switch (theme)
            {
                case Theme.Light:
                    App.Current.Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("Theme/LightThemeRD.xaml", UriKind.Relative) });
                    break;
                case Theme.Dark:
                    App.Current.Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("Theme/DarkThemeRD.xaml", UriKind.Relative) });
                    break;
                default:
                    // Default to Light theme if an unsupported theme is specified.
                    App.Current.Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("Theme/LightThemeRD.xaml", UriKind.Relative) });
                    break;
            }
        }

    }
}
