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
    /// Lógica de interacción para ErrorPopUp.xaml
    /// </summary>
    public partial class ErrorPopUp : Window
    {
        public ErrorPopUp(string error)
        {
            InitializeComponent();
            txtError.Text = error;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
