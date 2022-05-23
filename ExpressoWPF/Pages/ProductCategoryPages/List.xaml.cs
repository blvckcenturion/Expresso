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
using Expresso.Model;
using Expresso.Implementation;
using ExpressoWPF.Controls;
using System.Data;

namespace ExpressoWPF.Pages.ProductCategoryPages
{
    /// <summary>
    /// Lógica de interacción para List.xaml
    /// </summary>
    public partial class List : Page
    {
        ProductCategoryImpl productCategoryImpl = new ProductCategoryImpl();
        ProductCategory productCategory;
        public List()
        {
            InitializeComponent();
        }

        private void scaleDown(bool select)
        {
            MainContent.SetValue(Grid.ColumnSpanProperty, 1);
            dgvData.FontSize = 14;
            if (select)
            {
                dgvData.ItemsSource = null;
                SelectProductCategories();
            }
            OptionsContent.Visibility = Visibility.Visible;
        }

        private void scaleUp(bool select)
        {
            MainContent.SetValue(Grid.ColumnSpanProperty, 2);
            dgvData.FontSize = 24;
            dgvData.ItemsSource = null;
            OptionsContent.Visibility = Visibility.Collapsed;
            if (select) SelectProductCategories();
        }

        private void SelectProductCategories()
        {
            dgvData.ItemsSource = null;
            try
            {
                dgvData.ItemsSource = productCategoryImpl.Select().DefaultView;
                txtInfo.Text = dgvData.Items.Count + " Registros Encontrados.";
                dgvData.Columns[0].Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                showException(ex);
            }
        }

        void showException(Exception ex)
        {
            new PopUpWindow(0, "No se pudo completar la acción\nComuniquese con el Adm de Sistemas.\n" + ex.Message).Show();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SelectProductCategories();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            string filter = txtFilter.Text.Trim();
            if (filter != string.Empty)
            {
                try
                {
                    scaleUp(false);
                    dgvData.ItemsSource = productCategoryImpl.Select(filter).DefaultView;
                    txtInfo.Text = dgvData.Items.Count + " Registros Encontrados.";
                    dgvData.Columns[0].Visibility = Visibility.Collapsed;
                    btnFilter.SetValue(Grid.ColumnSpanProperty, 1);
                    btnShowAll.Visibility = Visibility.Visible;
                    txtFilter.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    showException(ex);
                }
            }
        }

        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            txtFilter.Text = string.Empty;
            scaleUp(true);
            btnFilter.SetValue(Grid.ColumnSpanProperty, 2);
            btnShowAll.Visibility = Visibility.Collapsed;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (productCategory != null)
            {
                string error = "";
                if (txtDescription.Text != string.Empty && txtName.Text != string.Empty)
                {
                    if (!productCategoryImpl.Exists(txtName.Text) || txtName.Text == productCategory.ProductCategoryName)
                    {
                        if (txtDescription.Text.Length <= 120 && txtName.Text.Length <= 300)
                        {
                            try
                            {
                                productCategory.ProductCategoryName = txtName.Text;
                                productCategory.ProductCategoryDescription = txtDescription.Text;
                                int n = productCategoryImpl.Insert(productCategory);
                                if (n > 0)
                                {
                                    new PopUpWindow(1, "Registro actualizado de forma exitosa.\n" + DateTime.Now).Show();
                                    scaleUp(true);
                                    return;
                                }
                                else
                                {
                                    new PopUpWindow(0, "No se pudo realizar la actualizacion.\n" + DateTime.Now).Show();
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
                    }
                    else
                    {
                        error = "Existe una categoria con el mismo nombre.";
                    }
                }
                else
                {
                    error = "Existen campos en blanco que son requeridos";

                }
                new PopUpWindow(0, error).Show();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (productCategory != null)
            {
                try
                {
                    int n = productCategoryImpl.Delete(productCategory);
                    if (n > 0)
                    {
                        new PopUpWindow(1, "Registro eliminado de forma exitosa.\n" + DateTime.Now).Show();
                        scaleUp(true);
                    }
                    else
                    {
                        new PopUpWindow(0, "No se realizo la eliminacion.\n" + DateTime.Now).Show();
                    }
                }
                catch (Exception ex)
                {
                    showException(ex);
                }
            }
        }

        private void dgvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvData.SelectedItem != null && dgvData.Items.Count > 0)
            {
                try
                {
                    DataRowView d = (DataRowView)dgvData.SelectedItem;
                    byte id = byte.Parse(d.Row.ItemArray[0].ToString());
                    productCategory = productCategoryImpl.Get(id);
                    if (productCategory != null)
                    {
                        txtName.Text = productCategory.ProductCategoryName;
                        txtDescription.Text = productCategory.ProductCategoryDescription;
                        scaleDown(false);
                    }
                }
                catch (Exception ex)
                {
                    showException(ex);
                }
            }
        }
    }
}
