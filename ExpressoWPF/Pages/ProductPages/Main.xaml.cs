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
using Expresso.Implementation;
using Expresso.Model;
using System.Data;


namespace ExpressoWPF.Pages.ProductPages
{
    /// <summary>
    /// Lógica de interacción para Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        List<Page> pages = new List<Page>();
        List<Button> buttons = new List<Button>();
        public Main()
        {
            InitializeComponent();
            pages.Add(new List());
            buttons.Add(btnSection1);
            pages.Add(new New(this));
            buttons.Add(btnSection2);
            SwitchTabs(0);
        }

        private void btnSection1_Click(object sender, RoutedEventArgs e)
        {
            SwitchTabs(0);
        }

        private void btnSection2_Click(object sender, RoutedEventArgs e)
        {
            SwitchTabs(1);
        }

        public void SwitchTabs(byte value)
        {
            
            Content.Navigate(pages[value]);
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


        }
    }
}
