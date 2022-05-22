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
using System.Windows.Shapes;
using Expresso.Implementation;
using System.Data;
using ExpressoWPF.Controls;

namespace ExpressoWPF
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            EmployeeImpl employeeImpl = new EmployeeImpl();
            DataTable t = new DataTable();
            try
            {
                t = employeeImpl.Login(txtUserName.Text, txtPassword.Password);
                if(t.Rows.Count > 0)
                {
                    SessionClass.sessionUserID = int.Parse(t.Rows[0][0].ToString());
                    SessionClass.sessionUserName = t.Rows[0][1].ToString();
                    SessionClass.sessionFirstName = t.Rows[0][2].ToString();
                    SessionClass.sessionLastName = t.Rows[0][3].ToString();
                    SessionClass.sessionSecondLastName = t.Rows[0][4].ToString();
                    SessionClass.sessionCI = t.Rows[0][5].ToString();
                    SessionClass.sessionPhone = t.Rows[0][6].ToString();
                    SessionClass.sessionAddress = t.Rows[0][7].ToString();
                    SessionClass.sessionGender = char.Parse(t.Rows[0][8].ToString());
                    SessionClass.sessionBirthDate = DateTime.Parse(t.Rows[0][9].ToString());
                    SessionClass.sessionRole = t.Rows[0][10].ToString();
                    SessionClass.sessionTown = t.Rows[0][11].ToString();
                    SessionClass.sessionEmail = t.Rows[0][12].ToString();
                    MainWindow win = new MainWindow();
                    win.Show();
                    this.Visibility = Visibility.Hidden;
                } else
                {
                    new PopUpWindow(0, "Usuario o Contraseña Incorrectos.").Show();
                }
            } catch(Exception ex)
            {
                new PopUpWindow(0, "No se pudo completar la accion.\n Contactese con el Adm de Sistemas.\n" + ex.Message).Show();
            }
        }
    }
}
