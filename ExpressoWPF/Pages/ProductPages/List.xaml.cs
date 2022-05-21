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
using System.Data;

namespace ExpressoWPF.Pages.ProductPages
{
    /// <summary>
    /// Lógica de interacción para List.xaml
    /// </summary>
    public partial class List : Page
    {
        ProductImpl productType;
        ProductCategoryImpl productCategoryType;
        Product product;
        public List()
        {
            InitializeComponent();
        }

        private void dgvData_Loaded(object sender, RoutedEventArgs e)
        {
            SelectProducts();
        }

        void SelectCategories()
        {
            DataTable categories = new DataTable();
            productCategoryType = new ProductCategoryImpl();
            cbCategories.Items.Clear();
            try
            {
                categories = productCategoryType.Select();
                
                foreach (DataRow row in categories.Rows)
                {
                    cbCategories.Items.Add(row["Nombre de la Categoria"].ToString());
                }
            } catch(Exception ex)
            {
                showException(ex);
            }
        }

        void SelectProducts()
        {
            productType = new ProductImpl();
            dgvData.ItemsSource = null;

            try
            {
                dgvData.ItemsSource = productType.Select().DefaultView;
                txtInfo.Text = dgvData.Items.Count + " Registros Encontrados.";
                dgvData.Columns[0].Visibility = Visibility.Collapsed;
                dgvData.Columns[4].Visibility = Visibility.Collapsed;
                
            }
            catch (Exception ex)
            {
                showException(ex);
            }
        }

        private void scaleDown()
        {
            MainContent.SetValue(Grid.ColumnSpanProperty, 1);
            dgvData.FontSize = 14;
            dgvData.ItemsSource = null;
            SelectProducts();
            OptionsContent.Visibility = Visibility.Visible;
        }

        private void scaleUp()
        {
            MainContent.SetValue(Grid.ColumnSpanProperty, 2);
            dgvData.FontSize = 24;
            dgvData.ItemsSource = null;
            SelectProducts();
            
            OptionsContent.Visibility = Visibility.Collapsed;
        }

        private void dgvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgvData.SelectedItem != null && dgvData.Items.Count > 0)
            {
                SelectCategories();
                try
                {
                    DataRowView d = (DataRowView)dgvData.SelectedItem;
                    byte id = byte.Parse(d.Row.ItemArray[0].ToString());
                    productType = new ProductImpl();
                    product = productType.Get(id);
                    if (product != null)
                    {
                        txtProductBasePrice.Text = product.BasePrice.ToString();
                        txtProductName.Text = product.ProductName;
                        txtProductDescription.Text = product.ProductDescription;
                        cbCategories.SelectedIndex = cbCategories.Items.IndexOf(product.ProductCategoryName);
                        scaleDown();
                    }
                } catch(Exception ex)
                {
                    showException(ex);
                }
            } 
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (product != null)
            {
                productType = new ProductImpl();
                try
                {
                    int n = productType.Delete(product);
                    if (n > 0)
                    {
                        new PopUpWindow(1, "Registro eliminado de forma exitosa.\n" + DateTime.Now).Show();
                        scaleUp();
                    } else
                    {
                        new PopUpWindow(0, "No se realizo la eliminacion.\n" + DateTime.Now).Show();
                    }
                } catch(Exception ex)
                {
                    showException(ex);
                }
            }
        }

        void showException(Exception ex)
        {
            new PopUpWindow(0, "No se pudo completar la acción\nComuniquese con el Adm de Sistemas.\n" + ex.Message).Show();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(product != null)
            {
                productType = new ProductImpl();
                bool validateName = product.ProductName.ToLower() != txtProductName.Text.ToLower() ? true : false;
                ValidatedProduct vp = Main.ValidateProduct(txtProductName.Text, txtProductDescription.Text, txtProductBasePrice.Text, cbCategories.Text, validateName);
                if(vp.isValidated)
                {
                    try
                    {
                        product.ProductName = vp.ProductName;
                        product.ProductDescription = vp.ProductDescription;
                        product.BasePrice = vp.ProductBasePrice;
                        product.ProductCategoryName = vp.ProductCategory;
                        int n = productType.Update(product);
                        if (n > 0)
                        {
                            new PopUpWindow(1, "Registro actualizado de forma exitosa.\n" + DateTime.Now).Show();
                            scaleUp();
                        }
                        else
                        {
                            new PopUpWindow(0, "No se pudo realizar la actualizacion.\n" + DateTime.Now).Show();
                        }
                    } catch(Exception ex)
                    {
                        showException(ex);
                    }
                    
                }
                
            }
        }
    }
}
