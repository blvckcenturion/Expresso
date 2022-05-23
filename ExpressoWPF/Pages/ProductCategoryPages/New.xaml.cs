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
using ExpressoWPF.Controls;
using Expresso.Model;

namespace ExpressoWPF.Pages.ProductCategoryPages
{
    /// <summary>
    /// Lógica de interacción para New.xaml
    /// </summary>
    public partial class New : Page
    {
        Main Main;
        ProductCategoryImpl productCategoryImpl = new ProductCategoryImpl();
        public New(Main main)
        {
            InitializeComponent();
            Main = main;
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            string error = "";
            if(txtProductCategoryDescription.Text != string.Empty && txtProductCategoryName.Text != string.Empty)
            {
                if(!productCategoryImpl.Exists(txtProductCategoryName.Text))
                {
                    if (txtProductCategoryDescription.Text.Length <= 120 && txtProductCategoryName.Text.Length <= 300)
                    {
                        try
                        {
                            ProductCategory p = new ProductCategory(txtProductCategoryName.Text, txtProductCategoryDescription.Text);
                            int n = productCategoryImpl.Insert(p);
                            if (n > 0)
                            {
                                new PopUpWindow(1, "Insercion de categoria realizada de forma exitosa.\n" + DateTime.Now).Show();
                                Main.SwitchTabs(0);
                                return;
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
                    else
                    {
                        error = "Los valores indicados superan el rango de longitud establecido.";
                    }
                } else
                {
                    error = "Existe una categoria con el mismo nombre.";
                }
            } else
            {
                error = "Existen campos en blanco que son requeridos";
                
            }
            new PopUpWindow(0, error).Show();
        }
    }
}
