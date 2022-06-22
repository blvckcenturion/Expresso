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
using Expresso.Model;
using System.Data;
using ExpressoWPF.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using AForge.Video.DirectShow;
using AForge.Video;
using System.Threading;
using System.Drawing.Imaging;
using Microsoft.Win32;

namespace ExpressoWPF.Pages.ProductPages
{
    /// <summary> ya vengo joven garcia
    /// Lógica de interacción para New.xaml
    /// </summary>
    /// 

    static class BitmapHelpers
    {
        public static BitmapImage ToBitmapImage(this Bitmap bitmap)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            bi.StreamSource = ms;
            bi.EndInit();
            return bi;
        }
    }

    public partial class New : Page
    {
        ProductCategoryImpl productCategoryType;
        ProductImpl productType;
        Main Main;
        List<ProductSize> productSizes = new List<ProductSize>();
        ImageSource frame;
        string fileName;
        int index = -1;

        public New(Main main)
        {
            InitializeComponent();
            Main = main;
            dgvData.ItemsSource = productSizes;

        }


        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            productType = new ProductImpl();
            productCategoryType = new ProductCategoryImpl();

            ValidatedProduct vp = Main.ValidateProduct(txtProductName.Text, txtProductDescription.Text, cbCategories.Text, true, productSizes.Count);
            if (vp.isValidated)
            {
                if(fileName != null || frame != null)
                {
                    try
                    {
                        Product p = new Product(vp.ProductName, vp.ProductDescription, vp.ProductCategory);
                        p.Photo = DateTime.Now.ToFileTime().ToString();
                        
                        int n = productType.Insert(p, productSizes);
                        if (n > 0)
                        {
                            if (fileName != null && frame == null)
                            {
                                var imagePath = System.IO.Path.Combine(ConfigClass.pathPhotoProduct + p.Photo + ".jpg");
                                File.Copy(fileName, imagePath);
                            }
                            else if (fileName == null && frame != null)
                            {
                                BitmapSource bms = frame as BitmapSource;
                                using (var fileStream = new FileStream(ConfigClass.pathPhotoProduct + p.Photo + ".jpg", FileMode.Create))
                                {
                                    BitmapEncoder encoder = new JpegBitmapEncoder();
                                    encoder.Frames.Add(BitmapFrame.Create(bms as BitmapSource));
                                    encoder.Save(fileStream);
                                }
                            }
                            new PopUpWindow(1, "Insercion de producto realizada de forma exitosa.\n" + DateTime.Now).Show();
                            Main.SwitchTabs(0);
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
                    new PopUpWindow(0, "El producto necesita tener una imagen valida.").Show();
                }
                
            }
        }

        void SelectCategories()
        {
            DataTable categories = new DataTable();
            productCategoryType = new ProductCategoryImpl();
            try
            {
                categories = productCategoryType.Select();
                cbCategories.Items.Clear();
                foreach (DataRow row in categories.Rows)
                {
                    cbCategories.Items.Add(row["Nombre de la Categoria"].ToString());
                }
            }
            catch (Exception ex)
            {
                new PopUpWindow(0, "No se pudo completar la acción\nComuniquese con el Adm de Sistemas.\n" + ex.Message).Show();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SelectCategories();
            this.DataContext = this;
            GetVideoDevices();
        }

        private void btnAddVariation_Click(object sender, RoutedEventArgs e)
        {
            string name = txtProductSizeName.Text.Trim();
            string error = "";
            float price;
            bool isValid = float.TryParse(txtProductSizePrice.Text, out price);
            if (name != String.Empty)
            {
                if (isValid && price > 0)
                {
                    if (index >= 0)
                    {
                        if (productSizes.FindIndex(x => x.Name.ToLower() == name.ToLower()) == -1 || productSizes[index].Name == name)
                        {
                            productSizes[index] = new ProductSize(name, price);
                            txtProductSizeName.Text = String.Empty;
                            txtProductSizePrice.Text = String.Empty;
                            updateList();
                            index = -1;
                            toggleStack();
                            return;
                        } else
                        {
                            error = "Ya existe una variacion con el nombre indicado.";
                        }

                    } else
                    {
                        if (productSizes.FindIndex(x => x.Name.ToLower() == name.ToLower()) == -1)
                        {
                            productSizes.Add(new ProductSize(name, price));
                            txtProductSizeName.Text = String.Empty;
                            txtProductSizePrice.Text = String.Empty;
                            updateList();
                            return;
                        }
                        else
                        {
                            error = "Ya existe una variacion con el nombre indicado.";
                        }
                    }
                } else
                {
                    error = "La variacion tiene un precio invalido.";
                }
            } else
            {
                error = "La variacion no puede tener un nombre nulo.";
            }
            new PopUpWindow(0, error).Show();

        }
        private void updateList()
        {
            dgvData.ItemsSource = null;
            dgvData.ItemsSource = productSizes;
        }

        private void dgvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvData.SelectedItem != null && dgvData.Items.Count > 0)
            {
                ProductSize p = (ProductSize)dgvData.SelectedItem;
                if (p != null)
                {
                    index = productSizes.FindIndex(x => x.Name.ToLower() == p.Name.ToLower());
                    txtProductSizeName.Text = p.Name;
                    txtProductSizePrice.Text = p.BasePrice.ToString();
                    btnAddVariation.Content = "Editar Variacion.";
                    btnDeleteVariation.Visibility = Visibility.Visible;
                    stack.Height = 100;
                }
            }
        }

        private void toggleStack()
        {
            btnAddVariation.Content = "Agregar Variacion.";
            btnDeleteVariation.Visibility = Visibility.Collapsed;
            txtProductSizePrice.Text = String.Empty;
            txtProductSizeName.Text = String.Empty;
            stack.Height = 50;
        }

        private void btnDeleteVariation_Click(object sender, RoutedEventArgs e)
        {
            if (index >= 0)
            {
                productSizes.RemoveAt(index);
                updateList();
                index = -1;
                toggleStack();
            }
        }

        private void camera_Click(object sender, RoutedEventArgs e)
        {
            StartUseCamera();
        }

        private void upload_Click(object sender, RoutedEventArgs e)
        {
            StartUseUpload();
        }

        private void StartUseCamera()
        {
            stackOptions.Visibility = Visibility.Collapsed;
            fileName = null;
            imgCapture.Source = null;
            stackCamera.Visibility = Visibility.Visible;
            stackUpload.Visibility = Visibility.Collapsed;
        }

        private void StartUseUpload()
        {
            stackOptions.Visibility = Visibility.Collapsed;
            txtImg.Text = "Seleccionar...";
            frame = null;
            pickedImg.Source = null;
            stackUpload.Visibility = Visibility.Visible;
            stackCamera.Visibility = Visibility.Collapsed;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            StartCamera();
        }

        #region CAMERA IMPLEMENTATION

        #region Public properties

        public ObservableCollection<FilterInfo> VideoDevices { get; set; }

        public FilterInfo CurrentDevice
        {
            get { return _currentDevice; }
            set { _currentDevice = value; this.OnPropertyChanged("CurrentDevice"); }
        }
        private FilterInfo _currentDevice;

        #endregion

        #region Private fields

        private IVideoSource _videoSource;

        #endregion

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StopCamera();
        }

        private void video_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            try
            {
                BitmapImage bi;
                using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                {
                    bi = bitmap.ToBitmapImage();
                }
                bi.Freeze(); // avoid cross thread operations and prevents leaks
                Dispatcher.BeginInvoke(new ThreadStart(delegate { imgCamera.Source = bi; }));
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error on _videoSource_NewFrame:\n" + exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                StopCamera();
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            StopCamera();
        }

        private void GetVideoDevices()
        {
            VideoDevices = new ObservableCollection<FilterInfo>();
            foreach (FilterInfo filterInfo in new FilterInfoCollection(FilterCategory.VideoInputDevice))
            {
                VideoDevices.Add(filterInfo);
            }
            if (VideoDevices.Any())
            {
                CurrentDevice = VideoDevices[0];
            }
            else
            {
                MessageBox.Show("No video sources found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StartCamera()
        {
            if (CurrentDevice != null)
            {
                imgCapture.Visibility = Visibility.Collapsed;
                imgCamera.Visibility = Visibility.Visible;
                _videoSource = new VideoCaptureDevice(CurrentDevice.MonikerString);
                _videoSource.NewFrame += video_NewFrame;
                _videoSource.Start();
            }
        }

        private void StopCamera()
        {
            if (_videoSource != null && _videoSource.IsRunning)
            {
                _videoSource.SignalToStop();
                _videoSource.NewFrame -= new NewFrameEventHandler(video_NewFrame);
            }
        }

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion

        private void btnCapture_Click(object sender, RoutedEventArgs e)
        {
            if (_videoSource != null && _videoSource.IsRunning)
            {
                frame = imgCamera.Source;
                imgCapture.Source = frame;
                imgCamera.Visibility = Visibility.Collapsed;
                imgCapture.Visibility = Visibility.Visible;
                StopCamera();
            }
        }

        #endregion

        private void btnSelectImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Archivos de Imagen|*.jpg";
            if (fd.ShowDialog() == true)
            {
                pickedImg.Source = new BitmapImage(new Uri(fd.FileName));
                txtImg.Text = fd.FileName;
                fileName = fd.FileName;
            }
        }

        private void btnUseCamera_Click(object sender, RoutedEventArgs e)
        {
            StartUseCamera();
        }

        private void btnUseUpload_Click(object sender, RoutedEventArgs e)
        {
            StopCamera();
            StartUseUpload();
        }
    }



}
