using System;
using System.Collections.Generic;
using System.Data;
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

namespace ExpressoWPF.Pages.ClientPages
{
    /// <summary>
    /// Lógica de interacción para List.xaml
    /// </summary>
    public partial class List : Page
    {
        TownImpl townImpl = new TownImpl();
        ClientImpl clientImpl = new ClientImpl();
        Client client;
        public List()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            string filter = txtFilter.Text.Trim();
            if (filter != string.Empty)
            {
                try
                {
                    scaleUp(false);
                    dgvData.ItemsSource = clientImpl.Select(filter).DefaultView;
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string error = "";
            string name = txtClientName.Text.Trim();
            string nit = txtClientID.Text.Trim();
            string town = cbTown.Text.Trim();

            if(name != string.Empty && nit != string.Empty && town != string.Empty)
            {
                if(!clientImpl.Exists(nit) || nit.ToLower() == client.NIT.ToLower())
                {
                    try
                    {
                        client.NIT = nit;
                        client.Name = name;
                        client.TownName = town;
                        int n = clientImpl.Update(client);
                        if(n > 0)
                        {
                            new PopUpWindow(1, "Registro actualizado de forma exitosa.\n").Show();
                            scaleUp(true);
                            return;
                        } else
                        {
                            error = "No se pudo realizar la actualizacion.\n" + DateTime.Now;
                        }
                    } catch(Exception ex)
                    {
                        showException(ex);
                    }
                } else
                {
                    error = "Existe un cliente con el mismo NIT.";
                }
            } else
            {
                error = "Existen campos en blanco que son requeridos.";
            }
            new PopUpWindow(0, error).Show();
        }

        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            txtFilter.Text = string.Empty;
            scaleUp(true);
            btnFilter.SetValue(Grid.ColumnSpanProperty, 2);
            btnShowAll.Visibility = Visibility.Collapsed;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (client != null)
            {
                try
                {
                    int n = clientImpl.Delete(client);
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

        private void dgvData_Loaded(object sender, RoutedEventArgs e)
        {
            SelectClients();
            SelectTowns();
        }

        private void scaleDown(bool select)
        {
            MainContent.SetValue(Grid.ColumnSpanProperty, 1);
            dgvData.FontSize = 30;
            if (select)
            {
                dgvData.ItemsSource = null;
                SelectClients();
            }
            OptionsContent.Visibility = Visibility.Visible;
        }

        private void scaleUp(bool select)
        {
            MainContent.SetValue(Grid.ColumnSpanProperty, 2);
            dgvData.FontSize = 30;
            OptionsContent.Visibility = Visibility.Collapsed;
            dgvData.ItemsSource = null;
            if (select) SelectClients();
        }

        void SelectClients()
        {
            dgvData.ItemsSource = null;
            try
            {
                DataTable dt = clientImpl.Select();
                dgvData.ItemsSource = dt.DefaultView;
                txtInfo.Text = dgvData.Items.Count + " Registos encontrados.";
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

        private void SelectTowns()
        {
            DataTable categories = new DataTable();
            try
            {
                categories = townImpl.Select();
                cbTown.Items.Clear();
                foreach (DataRow row in categories.Rows)
                {
                    cbTown.Items.Add(row[0].ToString());
                }
            }
            catch (Exception ex)
            {
                new PopUpWindow(0, "No se pudo completar la acción\nComuniquese con el Adm de Sistemas.\n" + ex.Message).Show();
            }
        }

        private void dgvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvData.SelectedItem != null && dgvData.Items.Count > 0)
            {
                SelectTowns();
                try
                {
                    DataRowView d = (DataRowView)dgvData.SelectedItem;
                    byte id = byte.Parse(d.Row.ItemArray[0].ToString());
                    client = clientImpl.Get(id);
                    if (client != null)
                    {
                        txtClientID.Text = client.NIT;
                        txtClientName.Text = client.Name;
                        cbTown.SelectedIndex = cbTown.Items.IndexOf(client.TownName);
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
