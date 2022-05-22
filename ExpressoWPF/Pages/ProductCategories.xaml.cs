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
using System.Data;

namespace ExpressoWPF.Pages
{
    /// <summary>
    /// Lógica de interacción para ProductCategories.xaml
    /// </summary>
    public partial class ProductCategories : Page
    {
        ProductCategoryImpl productCategoryType;
        ProductCategory productCategory;
        byte op = 0;
        public ProductCategories()
        {
            InitializeComponent();
        }

        void EnabledButtons()
        {
            btnInsert.IsEnabled = false;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;

            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            txtProductCategoryName.IsEnabled = true;
            txtProductCategoryName.Focus();
        }

        void DisabledButtons()
        {
            btnInsert.IsEnabled = true;
            btnUpdate.IsEnabled = true;
            btnDelete.IsEnabled = true;

            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
            txtProductCategoryName.Clear();
            txtProductCategoryName.IsEnabled = false;
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            EnabledButtons();
            this.op = 1;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            EnabledButtons();
            this.op = 2;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (productCategory != null)
            {
                if (MessageBox.Show("Esta realmente segur@ \n de eliminar el registro?", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    productCategoryType = new ProductCategoryImpl();
                    try
                    {
                        int n = productCategoryType.Delete(productCategory);
                        if (n > 0)
                        {
                            lblInfo.Content = "Registro eliminado con exito - " + DateTime.Now;
                            DisabledButtons();
                            Select();
                        }
                        else
                        {
                            lblInfo.Content = "No se eliminaron registros - " + DateTime.Now;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un item a Eliminar");
            }
        }

        //private void btnSave_Click(object sender, RoutedEventArgs e)
        //{

        //    switch (this.op)
        //    {
        //        case 1:
        //            //Insert
        //            if(txtProductCategoryName.Text.Length != 0)
        //            {
        //                productCategory = new ProductCategory(txtProductCategoryName.Text);
        //                productCategoryType = new ProductCategoryImpl();
        //                try
        //                {
        //                    int n = productCategoryType.Insert(productCategory);
        //                    if (n > 0)
        //                    {
        //                        lblInfo.Content = "Registro insertado con exito - " + DateTime.Now;
        //                        DisabledButtons();
        //                        Select();
        //                    }
        //                    else
        //                    {
        //                        lblInfo.Content = "No se realizarion inserciones - " + DateTime.Now;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show(ex.Message);
        //                }
        //            } else
        //            {
        //                MessageBox.Show("Ingrese un nombre de categoria valido.");
        //            }
                    
        //            break;
        //        case 2:
        //            if(productCategory !=null)
        //            {
        //                //Update
        //                productCategory.ProductCategoryName = txtProductCategoryName.Text;
        //                productCategoryType = new ProductCategoryImpl();
        //                try
        //                {
        //                    int n = productCategoryType.Update(productCategory);
        //                    if (n > 0)
        //                    {
        //                        lblInfo.Content = "Registro modificado con exito - " + DateTime.Now;
        //                        DisabledButtons();
        //                        Select();
        //                    }
        //                    else
        //                    {
        //                        lblInfo.Content = "No se realizarion actualizaciones - " + DateTime.Now;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show(ex.Message);
        //                }
        //            } else
        //            {
        //                MessageBox.Show("Seleccione una Categoria de Producto para modificar.");
        //            }
        //            break;
        //    }
        //}
    

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DisabledButtons();
        }

        void Select()
        {
            productCategoryType = new ProductCategoryImpl();
            dgvData.ItemsSource = null;

            try
            {
                dgvData.ItemsSource = productCategoryType.Select().DefaultView;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo completar la acción\nComuniquese con el Adm de Sistemas." + ex.Message);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Select();
            lblInfo.Content = dgvData.Items.Count + " Registros Encontrados";
        }

        private void dgvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvData.SelectedItem != null && dgvData.Items.Count > 0)
            {
                try
                {
                    DataRowView d = (DataRowView)dgvData.SelectedItem;
                    byte id = byte.Parse(d.Row.ItemArray[0].ToString());
                    productCategoryType = new ProductCategoryImpl();
                    productCategory = productCategoryType.Get(id);
                    if (productCategory != null)
                    {
                        txtProductCategoryName.Text = productCategory.ProductCategoryName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
