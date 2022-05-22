using ExpressoWPF.Controls;
using System;
using System.Collections.Generic;
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
using Expresso.Model;

namespace ExpressoWPF.Pages.UserPages
{
    /// <summary>
    /// Lógica de interacción para Main.xaml
    /// </summary>
    /// 

    public struct ValidatedUser
    {
        public Employee Employee { get; set; }
        public bool IsValidated { get; set; }
        
    }
    public partial class Main : Page
    {
        List<Button> buttons = new List<Button>();
        public Main()
        {
            InitializeComponent();
            InitializeComponent();
            buttons.Add(btnSection1);
            buttons.Add(btnSection2);
            SwitchTabs(0);
        }

        private void btnSection1_Click(object sender, RoutedEventArgs e)
        {
            SwitchTabs(0);
        }

        private void btnSection2_Click(object sender, RoutedEventArgs e)
        {
            SwitchTabs(1);
        }

        public static ValidatedUser ValidateUser(string firstName, string lastName, string secondLastName, string phone, string address, string ci, string email, string gender, string role, string town, string date)
        {
            firstName = firstName.Trim();
            lastName = lastName.Trim();
            secondLastName = secondLastName.Trim();
            ci = ci.Trim();
            email = email.Trim();
            phone = phone.Trim();
            address = address.Trim();
            gender = gender.Trim();
            role = role.Trim();
            town = town.Trim();
            date = date.Trim();
            ValidatedUser vu = new ValidatedUser();
            string error = "";

            if(firstName != string.Empty
                && firstName != string.Empty 
                && lastName != string.Empty 
                && ci != string.Empty 
                && email != string.Empty 
                && phone != string.Empty 
                && gender != string.Empty 
                && role != string.Empty 
                && town != string.Empty
                && date != string.Empty)
            {
                if(firstName.Length <= 120 && lastName.Length <= 120)
                {
                    if(IsValidEmail(email))
                    { 
                        vu.Employee = new Employee("", "", firstName, lastName, secondLastName, ci, phone, address, gender == "Masculino" ? 'M' : 'F', DateTime.Parse(date), role,email) ;
                        vu.IsValidated = true;
                        return vu;
                    } else
                    {
                        error = "El Email ingresado no es valido.";
                    }
                } else
                {
                    error = "Los valores indicados superan el rango de longitud establecido.";
                }
            } else
            {
                error = "Existen campos en blanco que son requeridos.";
            }

            new PopUpWindow(0, error).Show();
            vu.IsValidated = false;
            return vu;
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

        public void SwitchTabs(byte value)
        {
            buttons.ForEach(b =>
            {
                if (value == buttons.IndexOf(b))
                {
                    b.IsEnabled = false;
                    b.Background = (Brush)FindResource("SecondaryAccentColor");
                }
                else
                {
                    b.IsEnabled = true;
                    b.Background = (Brush)FindResource("AccentColor");
                }
            });

            switch (value)
            {
                case 0:
                    Content.Navigate(new List());
                    break;
                case 1:
                    Content.Navigate(new New(this));
                    break;
            }
        }
    }
}
