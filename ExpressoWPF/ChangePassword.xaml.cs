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
using ExpressoWPF.Controls;

namespace ExpressoWPF
{
    /// <summary>
    /// Lógica de interacción para ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        int UserId;
        EmployeeImpl employeeImpl = new EmployeeImpl();

        public ChangePassword(int id)
        {
            InitializeComponent();
            UserId = id;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (employeeImpl.MatchesPassword(UserId, txtOldPassword.Password))
            {
                if(txtNewPassword.Password != txtOldPassword.Password)
                {
                    if(txtNewPassword.Password == txtRepeatPassword.Password )
                    {
                        if (txtNewPassword.Password.Length > 10) 
                        {
                            try
                            {
                                int n = employeeImpl.Update(UserId, txtNewPassword.Password);
                                if (n > 0)
                                {
                                    new PopUpWindow(1, "Registro actualizado de forma exitosa.\n" + DateTime.Now).Show();
                                    this.Close();
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                new PopUpWindow(0, "No se pudo realizar la actualizacion.").Show();
                            }
                        } else
                        {
                            error = "Error, la contraseña debe tener un minimo de 10 caracteres de longitud.";
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
