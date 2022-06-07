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
using System.Data;
using ExpressoWPF.Controls;
using Expresso;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Win32;
using System.IO;

namespace ExpressoWPF.Pages.LocationPages
{
    /// <summary>
    /// Lógica de interacción para List.xaml
    /// </summary>
    public partial class List : Page
    {
        LocationImpl locationType = new LocationImpl();
        TownImpl townType = new TownImpl();
        Expresso.Model.Location location;
        Location point = new Location(); 
        string fileName;
        public List()
        {
            InitializeComponent();
        }

        private void dgvData_Loaded(object sender, RoutedEventArgs e)
        {
            SelectLocations();
            SelectTowns();
        }

        void SelectLocations()
        {
            dgvData.ItemsSource = null;
            try
            {
                DataTable dt = locationType.Select();
                foreach (DataRow dr in dt.Rows)
                {
                    dr["photo"] = ConfigClass.pathPhotoLocation + dr["photo"] + ".jpg";
                }
                dgvData.ItemsSource = dt.DefaultView;
                txtInfo.Text = dgvData.Items.Count + " Registos encontrados.";
                dgvData.Columns[1].Visibility = Visibility.Collapsed;
                dgvData.Columns[2].Visibility = Visibility.Collapsed;
                dgvData.Columns[5].Visibility = Visibility.Collapsed;
                dgvData.Columns[6].Visibility = Visibility.Collapsed;
            } catch(Exception ex)
            {
                showException(ex);
            }
        }

        private void scaleDown(bool select)
        {
            MainContent.SetValue(Grid.ColumnSpanProperty, 1);
            dgvData.FontSize = 24;
            if (select)
            {
                dgvData.ItemsSource = null;
                SelectLocations();
            }
            OptionsContent.Visibility = Visibility.Visible;
        }

        private void scaleUp(bool select)
        {
            MainContent.SetValue(Grid.ColumnSpanProperty, 2);
            dgvData.FontSize = 24;
            dgvData.ItemsSource = null;
            OptionsContent.Visibility = Visibility.Collapsed;
            if (select) SelectLocations();
        }

        void showException(Exception ex)
        {
            new PopUpWindow(0, "No se pudo completar la acción\nComuniquese con el Adm de Sistemas.\n" + ex.Message).Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (location != null)
            {
                try
                {
                    int n = locationType.Delete(location);
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

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            string filter = txtFilter.Text.Trim();
            if (filter != string.Empty)
            {
                try
                {
                    scaleUp(false);
                    DataTable dt = locationType.Select(txtFilter.Text);
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["photo"] = ConfigClass.pathPhotoLocation + dr["photo"] + ".jpg";
                    }
                    dgvData.ItemsSource = dt.DefaultView;
                    txtInfo.Text = dgvData.Items.Count + " Registros Encontrados.";
                    dgvData.Columns[1].Visibility = Visibility.Collapsed;
                    dgvData.Columns[2].Visibility = Visibility.Collapsed;
                    dgvData.Columns[5].Visibility = Visibility.Collapsed;
                    dgvData.Columns[6].Visibility = Visibility.Collapsed;
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
            string name = txtName.Text.Trim();
            string details = txtDetails.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string town = cbTown.Text.Trim();
            string error = "";
            if (name != string.Empty && details != string.Empty && phone != string.Empty && town != string.Empty)
            {
                if(!locationType.Exists(name) || name.ToLower() == location.LocationName.ToLower())
                {
                    try
                    {
                        if (fileName != null)
                        {
                            var fileNameToSave = DateTime.Now.ToFileTime();
                            var imagePath = System.IO.Path.Combine(ConfigClass.pathPhotoLocation + fileNameToSave + ".jpg");
                            File.Copy(fileName, imagePath);
                            location.Photo = fileNameToSave.ToString();
                        }
                        if(point != null)
                        {
                            location.Longitude = point.Longitude;
                            location.Latitude = point.Latitude;
                        }
                        location.PhoneNumber = phone;
                        location.LocationAddress = details;
                        location.TownName = town;
                        location.LocationName = name;

                        int n = locationType.Update(location);
                        if (n > 0)
                        {
                            new PopUpWindow(1, "Registro actualizado de forma exitosa.\n" + DateTime.Now).Show();
                            scaleUp(true);
                            return;
                        }
                        else
                        {
                            error = "No se pudo realizar la actualizacion.\n" + DateTime.Now;
                        }
                    } catch(Exception ex)
                    {
                        showException(ex);
                    }
                } else
                {
                    error = "La denominacion ingresada ya esta en uso.";
                }
            } else
            {
                error = "Existen campos en blanco que son requeridos.";
            } new PopUpWindow(0, error).Show();
        }

        private void dgvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgvData.SelectedItem != null && dgvData.Items.Count > 0)
            {
                SelectTowns();
                
                try
                {
                    DataRowView d = (DataRowView)dgvData.SelectedItem;
                    byte id = byte.Parse(d.Row.ItemArray[1].ToString());
                    location = locationType.Get(id);
                    if(location != null)
                    {
                        fileName = null;
                        point = null;
                        txtName.Text = location.LocationName;
                        txtPhone.Text = location.PhoneNumber;
                        txtDetails.Text = location.LocationAddress;
                        cbTown.SelectedIndex = cbTown.Items.IndexOf(location.TownName);
                        locationImg.Source = new BitmapImage(new Uri(ConfigClass.pathPhotoLocation + location.Photo + ".jpg"));
                        Location loc = new Location(location.Latitude, location.Longitude);
                        Pushpin p = new Pushpin();
                        p.Location = loc;
                        map.Center = loc;
                        map.ZoomLevel = 12;
                        map.Children.Clear();
                        map.Children.Add(p);
                        scaleDown(false);
                    }
                }
                catch (Exception ex)
                {
                    showException(ex);
                }
            }
        }

        private void SelectTowns()
        {
            DataTable categories = new DataTable();
            try
            {
                categories = townType.Select();
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

        private void map_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            var mousePosition = e.GetPosition((UIElement)sender);
            point = map.ViewportPointToLocation(mousePosition);
            Pushpin p = new Pushpin();
            p.Location = point;
            map.Children.Clear();
            map.Children.Add(p);
        }

        private void btnSelectImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Archivos de Imagen|*.jpg";
            if (fd.ShowDialog() == true)
            {
                locationImg.Source = new BitmapImage(new Uri(fd.FileName));
                fileName = fd.FileName;
            }
        }
    }
}
