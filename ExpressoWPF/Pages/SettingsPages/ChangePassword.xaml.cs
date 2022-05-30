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
using ExpressoWPF.Controls;

namespace ExpressoWPF.Pages.SettingsPages
{
    /// <summary>
    /// Lógica de interacción para ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Page
    {
        EmployeeImpl employeeImpl = new EmployeeImpl();
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string error = "";
            if(employeeImpl.MatchesPassword(SessionClass.sessionUserID, txtCurrentPassword.Password))
            {
                if(txtNewPassword.Password != txtCurrentPassword.Password)
                {
                    if(txtNewPasswordRepeat.Password == txtNewPassword.Password)
                    {
                        try
                        {
                            int n = employeeImpl.Update(SessionClass.sessionUserID, txtNewPassword.Password);
                            if (n > 0)
                            {
                                new PopUpWindow(1, "Registro actualizado de forma exitosa.\n" + DateTime.Now).Show();
                                txtCurrentPassword.Password = "";
                                txtNewPassword.Password = "";
                                txtNewPasswordRepeat.Password = "";
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            new PopUpWindow(0, "No se pudo realizar la actualizacion.").Show();
                        }
                    } else
                    {
                        error = "Error, las contraseñas nuevas no son iguales.";
                    }
                } else
                {
                    error = "Error, su nueva contraseña no puede ser identica a la anterior.";
                }
            } else
            {
                error = "Error, la contraseña ingresada no es correcta.";
            }
            new PopUpWindow(0, error).Show();
        }
    }
}
