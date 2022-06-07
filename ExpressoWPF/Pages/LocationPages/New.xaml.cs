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
using Microsoft.Maps.MapControl.WPF;
using Expresso.Implementation;
using ExpressoWPF.Controls;
using Microsoft.Win32;
using System.IO;
using Expresso;

namespace ExpressoWPF.Pages.LocationPages
{
    /// <summary>
    /// Lógica de interacción para New.xaml
    /// </summary>
    public partial class New : Page
    {
        TownImpl townType;
        Location point;
        string fileName;
        LocationImpl locationType = new LocationImpl();
        Expresso.Model.Location location;
        Main main;

        public New(Main main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SelectTowns();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            string details = txtDetails.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string town = cbTown.Text.Trim();
            string error = "";

            if(name != string.Empty && details != string.Empty && phone != string.Empty && town != string.Empty)
            {
                if(fileName != null)
                {
                    if (point != null)
                    {
                        if (!locationType.Exists(name))
                        {
                            try 
                            {
                                var fileNameToSave = DateTime.Now.ToFileTime();
                                var imagePath = System.IO.Path.Combine(ConfigClass.pathPhotoEmployee + fileNameToSave + ".jpg");
                                File.Copy(fileName, imagePath);
                                location = new Expresso.Model.Location(name, details, phone, fileNameToSave.ToString(), point.Longitude, point.Latitude, town);
                                int n = locationType.Insert(location);
                                if (n > 0)
                                {
                                    main.SwitchTabs(0);
                                    new PopUpWindow(1, "Insercion de ubicacion realizada de forma exitosa.\n" + DateTime.Now).Show();
                                    return;
                                }

                            } catch(Exception ex)
                            {
                                new PopUpWindow(0, "No se pudo completar la acción\nComuniquese con el Adm de Sistemas.\n" + ex.Message).Show();
                            }
                            
                        }
                        else
                        {
                            error = "La denominacion ingresada ya esta en uso.";
                        }
                    }
                    else
                    {
                        error = "Seleccione una ubicacion para continuar.";
                    }
                } else
                {
                    error = "Seleccione una imagen de la ubicacion para continuar.";
                }
                
            } else
            {
                error = "Existen campos en blanco que son requeridos.";
            }
            new PopUpWindow(0, error).Show();
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

        private void btnAerial_Click(object sender, RoutedEventArgs e)
        {
            map.Mode = new AerialMode(true);
        }

        private void btnStreets_Click(object sender, RoutedEventArgs e)
        {
            map.Mode = new RoadMode();
        }

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            map.ZoomLevel += 1;
        }

        private void SelectTowns()
        {
            DataTable categories = new DataTable();
            townType = new TownImpl();
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

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            map.ZoomLevel -= 1;
        }

        private void btnImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Archivos de Imagen|*.jpg";
            if (fd.ShowDialog() == true)
            {
                locationImg.Source = new BitmapImage(new Uri(fd.FileName));
                txtImg.Text = fd.FileName;
                fileName = fd.FileName;
            }
        }
    }
}
