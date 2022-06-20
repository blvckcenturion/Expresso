using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Net.Mail;
using System.Configuration;
using System.Net.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Data;
using Expresso.Implementation;
using ExpressoWPF.Controls;
using Expresso.Model;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.IO;

namespace ExpressoWPF.Pages.UserPages
{
    /// <summary>
    /// Lógica de interacción para New.xaml
    /// </summary>
    public partial class New : Page
    {
        Main Main;
        TownImpl townType;
        EmployeeImpl employeeType;
        Employee employee;
        string fileName;
        public New(Main main)
        {
            InitializeComponent();
            Main = main;
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            employeeType = new EmployeeImpl();
            ValidatedUser vu = Main.ValidateUser(txtFirstName.Text, txtLastName.Text, txtSecondLastName.Text, txtPhone.Text, txtAddress.Text, txtCI.Text, txtEmail.Text, cbGender.Text, cbRoles.Text, cbTown.Text, dpBirthDate.SelectedDate.ToString());
            if (vu.IsValidated)
            {
                if(fileName != null)
                {
                    vu.Employee.UserName = generateUserName(vu.Employee.FirstName, vu.Employee.LastName, vu.Employee.CI, vu.Employee.Gender, vu.Employee.Role);
                    vu.Employee.Password = generateUserPassword(vu.Employee.FirstName, vu.Employee.LastName, vu.Employee.BirthDate, vu.Employee.Role);
                    
                    try
                    {
                        var fileNameToSave = DateTime.Now.ToFileTime();
                        var imagePath = System.IO.Path.Combine(ConfigClass.pathPhotoEmployee + fileNameToSave + ".jpg");
                        File.Copy(fileName, imagePath);
                        vu.Employee.Photo = fileNameToSave.ToString();
                        employee = vu.Employee;
                        int n = employeeType.Insert(employee);
                        if (n > 0)
                        {
                            sendEmail(employee.Email, employee.UserName, employee.Password);
                            new PopUpWindow(1, "Insercion de empleado realizada de forma exitosa.\n" + DateTime.Now).Show();
                        }
                        else
                        {
                            new PopUpWindow(0, "No se realizarion inserciones\n" + DateTime.Now).Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        new PopUpWindow(0, "No se pudo completar la acción\nComuniquese con el Adm de Sistemas.\n" + ex.Message).Show();
                    }
                } else
                {
                    new PopUpWindow(0, "Seleccione una imagen de perfil para continuar.").Show();
                }
            }
        }

        


        private void sendEmail(string to, string userName, string password)
        {
            MailMessage correo = new MailMessage();
            // Correo: expresso.app.bolivia@gmail.com
            // Contra: $Univalle2022
            correo.From = new MailAddress("expresso.app.bolivia@gmail.com", "Santiago Sarabia", System.Text.Encoding.UTF8);//Correo de salida
            correo.To.Add(to);
            correo.Subject = "Login de Usuario para Expresso.";
            correo.Body = "Nombre de Usuario: " + userName + "\n Contraseña: " + password;
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.gmail.com"; //Host del servidor de correo
            smtp.Port = 587; //Puerto de salida
            smtp.Credentials = new System.Net.NetworkCredential("expresso.app.bolivia@gmail.com", "pjsjkxdensnklwjm");//Cuenta de correo
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            smtp.EnableSsl = true;
            smtp.Send(correo);
            Main.SwitchTabs(0);
        }


        private string generateUserName(string firstName, string lastName, string ci, char gender, string role)
        {
            firstName = firstName.Substring(0,1);
            lastName = lastName.Substring(0, 1);
            role = role.Substring(0,1);
            string unixTime = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds().ToString();
            unixTime = unixTime.Substring(unixTime.Length-8);
            return firstName + lastName + unixTime;
        }

        private string generateUserPassword(string firstName, string lastName, DateTime date, string role)
        {
            firstName = firstName.ToUpper() + date.Minute; 
            return firstName +  role.Substring(0,3) + DateTime.Now.Second;
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SelectTowns();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Archivos de Imagen|*.jpg";
            if(fd.ShowDialog() == true)
            {
                img.Source = new BitmapImage(new Uri(fd.FileName));
                txtImg.Text = fd.FileName;
                fileName = fd.FileName;
     
            }
        }
    }
}
