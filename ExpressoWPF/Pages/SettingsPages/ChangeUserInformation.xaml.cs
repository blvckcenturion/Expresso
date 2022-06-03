using Expresso.Implementation;
using Expresso.Model;
using ExpressoWPF.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
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

namespace ExpressoWPF.Pages.SettingsPages
{
    /// <summary>
    /// Lógica de interacción para ChangeUserInformation.xaml
    /// </summary>
    public partial class ChangeUserInformation : Page
    {
        TownImpl townType;
        string fileName;
        MainWindow mw;
        public ChangeUserInformation(MainWindow mw)
        {
            this.mw = mw;
            InitializeComponent();
            SelectTowns();
            txtAddress.Text = SessionClass.sessionAddress;
            txtEmail.Text = SessionClass.sessionEmail;
            txtPhone.Text = SessionClass.sessionPhone;
            cbTown.SelectedIndex = cbTown.Items.IndexOf(SessionClass.sessionTown);
            userImg.Source = new BitmapImage(new Uri(ConfigClass.pathPhotoEmployee + SessionClass.sessionPhoto + ".jpg"));
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string address = txtAddress.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string town = cbTown.Text.Trim();
            string error = "";
            Employee employee = new Employee();
            EmployeeImpl employeeType = new EmployeeImpl();
            if (email != string.Empty && address != string.Empty && phone != string.Empty && town != string.Empty)
            {
                if (IsValidEmail(email))
                {
                    employee.Address = address;
                    employee.Phones = phone;
                    employee.TownName = town;
                    employee.Role = SessionClass.sessionRole;
                    employee.Id = SessionClass.sessionUserID;
                    employee.Photo = SessionClass.sessionPhoto;
                    if (!employeeType.Exists(txtEmail.Text) || SessionClass.sessionEmail.ToLower() == email.ToLower())
                    {
                        employee.Email = email;
                        try
                        {
                            if (fileName != null)
                            {
                                var fileNameToSave = DateTime.Now.ToFileTime();
                                var imagePath = System.IO.Path.Combine(ConfigClass.pathPhotoEmployee + fileNameToSave + ".jpg");
                                File.Copy(fileName, imagePath);
                                employee.Photo = fileNameToSave.ToString();
                            }
                            int n = employeeType.Update(employee);
                            if (n > 0)
                            {
                                new PopUpWindow(1, "Registro actualizado de forma exitosa.\n" + DateTime.Now).Show();
                                employee = employeeType.Get((byte)SessionClass.sessionUserID);
                                if (employee != null)
                                {
                                    SessionClass.sessionPhoto = employee.Photo;
                                    SessionClass.sessionEmail = employee.Email;
                                    SessionClass.sessionAddress = employee.Address;
                                    SessionClass.sessionTown = employee.TownName;
                                    SessionClass.sessionPhone = employee.Phones;
                                    mw.profileImg.ImageSource = new BitmapImage(new Uri(ConfigClass.pathPhotoEmployee + SessionClass.sessionPhoto + ".jpg"));
                                }
                                return;
                            }
                            else
                            {
                                error = "No se pudo realizar la actualizacion.\n" + DateTime.Now;
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    else
                    {
                        error = "El email ingresado, ya esta registrado.\n Intenta con otro.";
                    }
                } else
                {
                    error = "Email Invalido.";
                }
            } else
            {
                error = "Existen campos en blanco que son requeridos.";
            }
            new PopUpWindow(0, error).Show();
        }
        public static bool IsValidEmail(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }
            catch (Exception e)
            {
                return false;
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

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Archivos de Imagen|*.jpg";
            if (fd.ShowDialog() == true)
            {
                userImg.Source = new BitmapImage(new Uri(fd.FileName));
                txtImg.Text = fd.FileName;
                fileName = fd.FileName;
            }
        }
    }
}
