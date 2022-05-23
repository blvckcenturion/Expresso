using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Mail;
using System.Configuration;
using System.Net.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Expresso.Implementation;
using ExpressoWPF.Controls;
using Expresso.Model;

namespace ExpressoWPF
{
    /// <summary>
    /// Lógica de interacción para Reset.xaml
    /// </summary>
    public partial class Reset : Window
    {
        EmployeeImpl employeeImpl = new EmployeeImpl();
        public Reset()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (employeeImpl.Exists(txtEmail.Text))
            {
                try
                {
                    Employee employee = employeeImpl.Get(txtEmail.Text);
                    string password = employee.FirstName.ToUpper() + employee.BirthDate.Year + employee.LastName.ToLower() + employee.BirthDate.Minute + employee.Role.Substring(0, 3) + DateTime.Now.Second;
                    int n = employeeImpl.Update(txtEmail.Text, password);
                    if(n > 0)
                    {
                        sendEmail(txtEmail.Text, employee.UserName, password);
                    } else
                    {
                        new PopUpWindow(0, "No se pudo realizar la actualizacion de la contraseña");
                    }
                } catch(Exception ex)
                {
                    new PopUpWindow(0, "No se pudo completar la acción\nComuniquese con el Adm de Sistemas.\n" + ex.Message).Show();
                }
            } else
            {
                new PopUpWindow(0, "El correo ingresado no esta registrado.").Show();
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
            smtp.Credentials = new System.Net.NetworkCredential("expresso.app.bolivia@gmail.com", "$Univalle2022");//Cuenta de correo
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            smtp.EnableSsl = true;
            smtp.Send(correo);
            this.Close();
            new PopUpWindow(1, "La Contraseña fue actualizada de forma exitosa. \n Revise su correo electronico con los detalles de su cuenta.\n").Show();
            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
