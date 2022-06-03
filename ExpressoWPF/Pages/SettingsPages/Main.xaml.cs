using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExpressoWPF.Pages.SettingsPages
{
    /// <summary>
    /// Lógica de interacción para Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        List<Button> buttons = new List<Button>();
        MainWindow mw;
        public Main(MainWindow main)
        {
            mw = main;
            InitializeComponent();
            buttons.Add(btnSection1);
            buttons.Add(btnSection2);
            SwitchTabs(0);
        }

        public void SwitchTabs(byte value)
        {
            buttons.ForEach(b =>
            {
                if (value == buttons.IndexOf(b))
                {
                    b.IsEnabled = false;
                    b.Background = (Brush)FindResource("SecondaryAccentColor");
                }
                else
                {
                    b.IsEnabled = true;
                    b.Background = (Brush)FindResource("AccentColor");
                }
            });

            switch (value)
            {
                case 0:
                    Content.Navigate(new ChangeUserInformation(mw));
                    break;
                case 1:
                    Content.Navigate(new ChangePassword());
                    break;
            }
        }

        private void btnSection1_Click(object sender, RoutedEventArgs e)
        {
            SwitchTabs(0);
        }

        private void btnSection2_Click(object sender, RoutedEventArgs e)
        {
            SwitchTabs(1);
        }
    }
}
