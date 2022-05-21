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
using ExpressoWPF.Controls;

namespace ExpressoWPF.Pages.ProductPages
{
    /// <summary> ya vengo joven garcia
    /// Lógica de interacción para New.xaml
    /// </summary>
    public partial class New : Page
    {
        ProductCategoryImpl productCategoryType;
        ProductImpl productType;
        Main Main;

        public New(Main main)
        {
            InitializeComponent();
            Main = main;
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            productType = new ProductImpl();
            productCategoryType = new ProductCategoryImpl();

            ValidatedProduct vp = Main.ValidateProduct(txtProductName.Text, txtProductDescription.Text, txtProductBasePrice.Text, cbCategories.Text, true);
            if (vp.isValidated)
            {
                try
                {
                    Product p = new Product(vp.ProductBasePrice, vp.ProductName, vp.ProductDescription, vp.ProductCategory);
                    int n = productType.Insert(p);
                    if (n > 0)
                    {
                        new PopUpWindow(1, "Insercion de producto realizada de forma exitosa.\n" + DateTime.Now).Show();
                        txtProductBasePrice.Text = String.Empty;
                        txtProductName.Text = String.Empty;
                        txtProductDescription.Text = String.Empty;
                        cbCategories.Text = String.Empty;
                        Main.SwitchTabs(0);
                    }
                    else
                    {
                        new PopUpWindow(0, "No se realizarion inserciones\n" + DateTime.Now).Show();
                    }
                }
                catch (Exception ex)
                {
                    new PopUpWindow(0, "No se pudo completar la acción\nComuniquese con el Adm de Sistemas.\n" + ex.Message).Show();
                }
            }
        }

        void SelectCategories()
        {
            DataTable categories = new DataTable();
            productCategoryType = new ProductCategoryImpl();
            try
            {
                categories = productCategoryType.Select();
                cbCategories.ItemsSource = null;
                foreach (DataRow row in categories.Rows)
                {
                    cbCategories.Items.Add(row["Nombre de la Categoria"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo completar la acción\nComuniquese con el Adm de Sistemas." + ex.Message);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SelectCategories();
        }
    }
}
