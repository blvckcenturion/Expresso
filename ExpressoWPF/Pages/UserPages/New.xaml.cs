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
using System.Data;
using Expresso.Implementation;
using ExpressoWPF.Controls;
using Expresso.Model;

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
                try
                {
                    employee = vu.Employee;

                } catch (Exception ex)
                {
                    new PopUpWindow(1, "No se pudo completar la acción\nComuniquese con el Adm de Sistemas.\n" + ex.Message).Show();
                }
                
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SelectTowns();
        }
    }
}
