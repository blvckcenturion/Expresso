using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using Microsoft.Win32;

namespace ExpressoWPF.Pages.UserPages
{
    /// <summary>
    /// Lógica de interacción para List.xaml
    /// </summary>
    public partial class List : Page
    {
        EmployeeImpl employeeType;
        Employee employee;
        TownImpl townType;
        string fileName;
        List<string> roles = new List<string>() { "Administrador", "Gerente", "Cajero" };

        public List()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            string filter = txtFilter.Text.Trim();
            if (filter != string.Empty)
            {
                employeeType = new EmployeeImpl();
                try
                {
                    scaleUp(false);
                    DataTable dt = employeeType.Select(txtFilter.Text);
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["photo"] = ConfigClass.pathPhotoEmployee + dr["photo"];
                    }

                    dgvData.ItemsSource = dt.DefaultView;
                    txtInfo.Text = dgvData.Items.Count + " Registros Encontrados.";
                    dgvData.Columns[1].Visibility = Visibility.Collapsed;
                    dgvData.Columns[2].Visibility = Visibility.Collapsed;
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

        private void scaleDown(bool select)
        {
            MainContent.SetValue(Grid.ColumnSpanProperty, 1);
            dgvData.FontSize = 12;
            if (select) {
                dgvData.ItemsSource = null;
                SelectEmployees();
            }
            OptionsContent.Visibility = Visibility.Visible;
        }

        private void scaleUp(bool select)
        {
            MainContent.SetValue(Grid.ColumnSpanProperty, 2);
            dgvData.FontSize = 18;
            dgvData.ItemsSource = null;
            OptionsContent.Visibility = Visibility.Collapsed;
            if (select) SelectEmployees();
        }
        private void dgvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvData.SelectedItem != null && dgvData.Items.Count > 0)
            {
                SelectTowns();
                try
                {
                    DataRowView d = (DataRowView)dgvData.SelectedItem;
                    byte id = byte.Parse(d.Row.ItemArray[1].ToString());
                    employeeType = new EmployeeImpl();
                    employee = employeeType.Get(id);
                    if (employee != null)
                    {
                        txtEmail.Text = employee.Email;
                        txtAddress.Text = employee.Address;
                        txtPhone.Text = employee.Phones;
                        cbRole.SelectedIndex =roles.IndexOf(employee.Role);
                        cbTown.SelectedIndex = cbTown.Items.IndexOf(employee.TownName);
                        userImg.Source = new BitmapImage(new Uri(ConfigClass.pathPhotoEmployee + employee.Photo));
                        scaleDown(false);
                    }
                }
                catch (Exception ex)
                {
                    showException(ex);
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string address = txtAddress.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string role = cbRole.Text.Trim();
            string town = cbTown.Text.Trim();
            string error = "";
                 
            if (employee != null)
            {
                employeeType = new EmployeeImpl();
                if(email != string.Empty && address != string.Empty && phone != string.Empty && role != string.Empty && town != string.Empty)
                {
                    if (Main.IsValidEmail(email)) 
                    {
                        employee.Address = address;
                        employee.Phones = phone;
                        employee.Role = role;
                        employee.TownName = town;
                        if (!employeeType.Exists(txtEmail.Text)|| employee.Email.ToLower() == email.ToLower())
                        {
                            
                            employee.Email = email;
                            try
                            {
                                if (fileName != null)
                                {
                                    var fileNameToSave = DateTime.Now.ToFileTime() + System.IO.Path.GetExtension(fileName);
                                    var imagePath = System.IO.Path.Combine(ConfigClass.pathPhotoEmployee + fileNameToSave);
                                    File.Copy(fileName, imagePath);
                                    employee.Photo = fileNameToSave;
                                }
                                int n = employeeType.Update(employee);
                                if(n > 0)
                                {
                                    new PopUpWindow(1, "Registro actualizado de forma exitosa.\n" + DateTime.Now).Show();
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
                        }
                        else
                        {
                            error = "El email ingresado, ya esta registrado.\n Intenta con otro.";
                        }
                    } else
                    {
                        error = "Email invalido.";
                    }
                    
                } else
                {
                    error = "Existen campos en blanco que son requeridos.";
                   
                }
                new PopUpWindow(0, error).Show();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(employee != null)
            {
                employeeType = new EmployeeImpl();
                try
                {
                    int n = employeeType.Delete(employee);
                    if (n > 0)
                    {
                        new PopUpWindow(1, "Registro eliminado de forma exitosa.\n" + DateTime.Now).Show();
                        scaleUp(true);
                    }
                    else
                    {
                        new PopUpWindow(0, "No se realizo la eliminacion.\n" + DateTime.Now).Show();
                    }
                } catch(Exception ex)
                {
                    showException(ex);
                }
            }
        }

        void SelectEmployees()
        {
            employeeType = new EmployeeImpl();
            dgvData.ItemsSource = null;
            try
            {
                DataTable dt = employeeType.Select();
                foreach(DataRow dr in dt.Rows)
                {
                    dr["photo"] = ConfigClass.pathPhotoEmployee + dr["photo"];
                }

                dgvData.ItemsSource = dt.DefaultView;
                txtInfo.Text = dgvData.Items.Count + " Registos encontrados.";
                dgvData.Columns[1].Visibility = Visibility.Collapsed;
                dgvData.Columns[2].Visibility = Visibility.Collapsed;

            } catch(Exception ex)
            {
                showException(ex); 
            }
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

        void showException(Exception ex)
        {
            new PopUpWindow(0, "No se pudo completar la acción\nComuniquese con el Adm de Sistemas.\n" + ex.Message).Show();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SelectEmployees();
            SelectTowns();
        }

        private void btnSelectImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == true)
            {
                var extension = System.IO.Path.GetExtension(fd.FileName);
                if (extension == ".png" || extension == ".jpg")
                {
                    userImg.Source = new BitmapImage(new Uri(fd.FileName));
                    fileName = fd.FileName;
                }
                else
                {
                    new PopUpWindow(0, "Seleccione una imagen valida.").Show();
                }

            }
        }
    }
}
