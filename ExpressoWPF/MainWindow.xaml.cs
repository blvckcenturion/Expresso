using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Expresso.Implementation;
using ExpressoWPF.Pages;

namespace ExpressoWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            fContainer.Navigate(new System.Uri("Pages/Home.xaml", UriKind.RelativeOrAbsolute));
        }
        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void onMouseEnter(Button btn, string text)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btn;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = text;
            }
        }

        private void btnHome_MouseEnter(object sender, MouseEventArgs e)
        {
            onMouseEnter(btnHome, "Inicio");
        }

        private void btnPointOfSale_MouseEnter(object sender, MouseEventArgs e)
        {
            onMouseEnter(btnPointOfSale, "Nueva Orden");
        }

        private void btnPreviousSales_MouseEnter(object sender, MouseEventArgs e)
        {
            onMouseEnter(btnPreviousSales, "Ordenes Previas");
        }

        private void btnProducts_MouseEnter(object sender, MouseEventArgs e)
        {
            onMouseEnter(btnProducts, "Productos");
        }

        private void btnLocations_MouseEnter(object sender, MouseEventArgs e)
        {
            onMouseEnter(btnLocations, "Ubicaciones");
        }

        private void btnProductCategories_MouseEnter(object sender, MouseEventArgs e)
        {
            onMouseEnter(btnProductCategories, "Categorias de Producto");
        }

        private void btnUsers_MouseEnter(object sender, MouseEventArgs e)
        {
            onMouseEnter(btnUsers, "Usuarios");
        }

        private void btnSettings_MouseEnter(object sender, MouseEventArgs e)
        {
            onMouseEnter(btnSettings, "Configuracion");
        }

        private void btnClients_MouseEnter(object sender, MouseEventArgs e)
        {
            onMouseEnter(btnClients, "Clientes");
        }

        // End: MenuLeft PopupButton //

        // Start: Button Close | Restore | Minimize 
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        // End: Button Close | Restore | Minimize
        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/UserPages/Main.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/Home.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnPointOfSale_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/NewOrder.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnPreviousSales_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/PreviousSales.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnLocations_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/LocationPages/Main.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/ProductPages/Main.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {

            fContainer.Navigate(new Pages.SettingsPages.Main(this));
        }

        private void btnProductCategories_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/ProductCategoryPages/Main.xaml", UriKind.RelativeOrAbsolute));
        }

        // Window Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtName.Text = SessionClass.sessionFirstName + " " + SessionClass.sessionLastName + (SessionClass.sessionSecondLastName != "" ? " " + SessionClass.sessionSecondLastName : "");
            txtRole.Text = SessionClass.sessionRole;
            profileImg.ImageSource = new BitmapImage(new Uri(ConfigClass.pathPhotoEmployee + SessionClass.sessionPhoto + ".jpg"));
            switch (SessionClass.sessionRole)
            {
                case "Cajero":
                    btnClients.Visibility = Visibility.Collapsed;
                    btnProducts.Visibility = Visibility.Collapsed;
                    btnProductCategories.Visibility = Visibility.Collapsed;
                    btnLocations.Visibility = Visibility.Collapsed;
                    btnUsers.Visibility = Visibility.Collapsed;
                    btnClients.Visibility = Visibility.Collapsed;
                    break;
                case "Gerente":
                    btnUsers.Visibility = Visibility.Collapsed;
                    btnClients.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        // Mouse Leave

        public void OnMouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
    }
}
