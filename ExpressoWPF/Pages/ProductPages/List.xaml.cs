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
using ExpressoWPF.Controls;

namespace ExpressoWPF.Pages.ProductPages
{
    /// <summary>
    /// Lógica de interacción para List.xaml
    /// </summary>
    public partial class List : Page
    {
        ProductImpl productType;
        Product p;
        public List()
        {
            InitializeComponent();
        }

        private void dgvData_Loaded(object sender, RoutedEventArgs e)
        {
            Select();
        }

        void Select()
        {
            productType = new ProductImpl();
            dgvData.ItemsSource = null;

            try
            {
                dgvData.ItemsSource = productType.Select().DefaultView;
                txtInfo.Text = dgvData.Items.Count + " Registros Encontrados.";
            }
            catch (Exception ex)
            {
                new PopUpWindow(0,"No se pudo completar la acción\nComuniquese con el Adm de Sistemas.\n" + ex.Message).Show();
            }
        }

    }
}
