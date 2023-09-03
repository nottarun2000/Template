using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.WPF.ApplicationFunction
{
    public class ApplicationFunction
    {

        public void SetUpResourceDictonaries(Theme theme = Theme.Light)
        {
            App.Current.Resources.MergedDictionaries.Clear();
            App.Current.Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("Theme/Generic.xaml", UriKind.Relative) });
            App.Current.Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("Theme/GenericStyles.xaml", UriKind.Relative) });

            switch (theme)
            {
                case Theme.Light:
                    App.Current.Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("Theme/LightThemeRD.xaml", UriKind.Relative) });
                    break;
                case Theme.Dark:
                    App.Current.Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("Theme/DarkThemeRD.xaml", UriKind.Relative) });
                    break;
                default:
                    App.Current.Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("Theme/LightThemeRD.xaml", UriKind.Relative) });
                    break;

            }

        }

    }
}
