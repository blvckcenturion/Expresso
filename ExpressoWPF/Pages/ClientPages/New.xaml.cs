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
using Expresso.Model;
using System.Data;

namespace ExpressoWPF.Pages.ClientPages
{
    /// <summary>
    /// Lógica de interacción para New.xaml
    /// </summary>
    public partial class New : Page
    {
        Main main;
        ClientImpl clientImpl = new ClientImpl();
        TownImpl townImpl = new TownImpl();
        
        public New(Main main)
        {
            this.main = main;
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            string error = "";
            string name = txtClientName.Text.Trim();
            string nit = txtClientID.Text.Trim();
            string town = cbTown.Text.Trim();

            if(name != string.Empty && nit != string.Empty && town != string.Empty)
            {
                if (!clientImpl.Exists(nit))
                {
                    try
                    {
                        int n = clientImpl.Insert(new Client(name, nit, town));
                        if(n > 0)
                        {
                            main.SwitchTabs(0);
                            new PopUpWindow(1, "Insercion de cliente realizada de forma exitosa.\n" + DateTime.Now).Show();
                            return;
                        }
                    } catch(Exception ex )
                    {
                        new PopUpWindow(0, "No se pudo completar la acción\nComuniquese con el Adm de Sistemas.\n" + ex.Message).Show();
                    }
                } else
                {
                    error = "Existe un cliente con el mismo NIT.";
                }
            } else
            {
                error = "Existen campos en blanco que son requeridos.";
            }
            new PopUpWindow(0, error).Show();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SelectTowns();
        }

        private void SelectTowns()
        {
            DataTable categories = new DataTable();
            
            try
            {
                categories = townImpl.Select();
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
    }
}
