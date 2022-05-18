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

namespace ExpressoWPF.Pages
{
    /// <summary>
    /// Lógica de interacción para Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        ProductImpl productType;
        Product product;
        byte op = 0;
        public Products()
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
            txtProductName.IsEnabled = true;
            txtProductDescription.IsEnabled = true;
            txtBasePrice.IsEnabled = true;
            txtCategory.IsEnabled = true;
            txtProductName.Focus();
        }

        void DisabledButtons()
        {
            btnInsert.IsEnabled = true;
            btnUpdate.IsEnabled = true;
            btnDelete.IsEnabled = true;

            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
            txtProductName.Clear();
            txtProductDescription.Clear();
            txtBasePrice.Clear();
            txtCategory.Clear();
            txtProductName.IsEnabled = false;
            txtProductDescription.IsEnabled = false;
            txtBasePrice.IsEnabled = false;
            txtCategory.IsEnabled = false;
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
            if (product != null)
            {
                if (MessageBox.Show("Esta realmente segur@ \n de eliminar el registro?", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    productType = new ProductImpl();
                    try
                    {
                        int n = productType.Delete(product);
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            switch (this.op)
            {
                case 1:
                    //Insert
                    product = new Product(float.Parse(txtBasePrice.Text), txtProductName.Text, txtProductDescription.Text, byte.Parse(txtCategory.Text));
                    productType = new ProductImpl();
                    try
                    {
                        int n = productType.Insert(product);
                        if (n > 0)
                        {
                            lblInfo.Content = "Registro insertado con exito - " + DateTime.Now;
                            DisabledButtons();
                            Select();
                        }
                        else
                        {
                            lblInfo.Content = "No se realizarion inserciones - " + DateTime.Now;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                case 2:
                    if (product != null)
                    {
                        //Update
                        product.ProductName = txtProductName.Text;
                        product.ProductDescription = txtProductDescription.Text;
                        product.BasePrice = float.Parse(txtBasePrice.Text);
                        product.ProductCategoryId = byte.Parse(txtCategory.Text);
                        productType = new ProductImpl();
                        try
                        {
                            int n = productType.Update(product);
                            if (n > 0)
                            {
                                lblInfo.Content = "Registro modificado con exito - " + DateTime.Now;
                                DisabledButtons();
                                Select();
                            }
                            else
                            {
                                lblInfo.Content = "No se realizarion actualizaciones - " + DateTime.Now;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Seleccione una Categoria de Producto para modificar.");
                    }
                    break;
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DisabledButtons();
        }

        void Select()
        {
            productType = new ProductImpl();
            dgvData.ItemsSource = null;

            try
            {
                dgvData.ItemsSource = productType.Select().DefaultView;
                dgvData.Columns[0].Visibility = Visibility.Collapsed;
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
                    productType = new ProductImpl();
                    product = productType.Get(id);
                    if (product != null)
                    {
                        txtProductName.Text = product.ProductName;
                        txtProductDescription.Text = product.ProductDescription;
                        txtBasePrice.Text = product.BasePrice.ToString();
                        txtCategory.Text = product.ProductCategoryId.ToString();
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
