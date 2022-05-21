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
using System.Windows.Shapes;

namespace ExpressoWPF.Controls
{
    /// <summary>
    /// Lógica de interacción para PopUpWindow.xaml
    /// </summary>
    public partial class PopUpWindow : Window
    {
        List<string> bgColor = new List<string>() { "TextTertiaryColor", "TextSecundaryColor" };
        List<String> titles = new List<String>() { "Error", "Enhorabuena" };
        List<String> subTitles = new List<String>() { "No se pudo completar la accion.", "Accion completada con exito." };
        List<String> images = new List<String>() { "/Resources/Images/Error.png", "/Resources/Images/Success.png" };

        public PopUpWindow(int type, string message)
        {
            InitializeComponent();
            txtMsg.Text = message;
            backgroundBorder.Background = (Brush)FindResource(bgColor[type]);
            txtTitle.Text = titles[type];
            txtSubTitle.Text = subTitles[type];
            img.Source = new BitmapImage(new Uri(images[type], UriKind.RelativeOrAbsolute));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
