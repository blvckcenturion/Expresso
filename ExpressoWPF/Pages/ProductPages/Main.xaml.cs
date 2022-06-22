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
using Expresso.Implementation;
using Expresso.Model;
using ExpressoWPF.Controls;

namespace ExpressoWPF.Pages.ProductPages
{
    /// <summary>
    /// Lógica de interacción para Main.xaml
    /// </summary>
    public struct ValidatedProduct
    {
        public bool isValidated { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
    }

    public partial class Main : Page
    {
        List<Button> buttons = new List<Button>();
        static ProductImpl productType;

        public Main()
        {
            InitializeComponent();
            buttons.Add(btnSection1);
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

        public static ValidatedProduct ValidateProduct(string productName, string productDescription, string category, bool validateExistance, int productSizes)
        {
            productName = productName.Trim();
            productDescription = productDescription.Trim();

            string error = "";
            ValidatedProduct vp = new ValidatedProduct();

            productType = new ProductImpl();
            if (productName != string.Empty && productDescription != string.Empty && category != string.Empty)
            {
                if (!productType.Exists(productName) || validateExistance == false) 
                {
                    if(productName.Length <= 150 && productDescription.Length <= 300)
                    {
                        
                        if (productSizes > 0)
                        {
                            vp.ProductDescription = productDescription;
                            vp.ProductName = productName;
                            vp.ProductCategory = category;
                            vp.isValidated = true;
                            return vp;
                        } else
                        {
                            error = "El producto debe tener 1 variacion como minimo.";
                        }
                    } else
                    {
                        error = "Los valores indicados superan el rango de longitud establecido.";
                    }
                }
                else
                {
                    error = "Existe un producto con el mismo nombre.";
                }
            }
            else
            {
                error = "Existen campos en blanco que son requeridos.";
            }
            new PopUpWindow(0, error).Show();
            vp.isValidated = false;
            return vp;
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
                    Content.Navigate(new List());
                    break;
                case 1:
                    Content.Navigate(new New(this));
                    break;
            }
        }
    }
}
