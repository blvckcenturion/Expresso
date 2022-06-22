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
using ExpressoWPF.Controls;
using System.Data;
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
    /// <summary>
    /// Lógica de interacción para List.xaml
    /// </summary>
    public partial class List : Page
    {
        ProductImpl productType;
        ProductCategoryImpl productCategoryType;
        ProductSizeImpl productSizeType = new ProductSizeImpl();
        Product product;
        ProductSize productSize;
        string fileName;
        ImageSource frame;
        public List()
        {
            InitializeComponent();
        }

        private void dgvData_Loaded(object sender, RoutedEventArgs e)
        {
            SelectProducts();
        }

        void SelectCategories()
        {
            DataTable categories = new DataTable();
            productCategoryType = new ProductCategoryImpl();
            cbCategories.Items.Clear();
            try
            {
                categories = productCategoryType.Select();
                
                foreach (DataRow row in categories.Rows)
                {
                    cbCategories.Items.Add(row["Nombre de la Categoria"].ToString());
                }
            } catch(Exception ex)
            {
                showException(ex);
            }
        }

        void SelectProducts()
        {
            productType = new ProductImpl();
            dgvData.ItemsSource = null;

            try
            {
                DataTable dt = productType.Select();
                foreach(DataRow row in dt.Rows)
                {
                    row["photo"] = ConfigClass.pathPhotoProduct + row["photo"] + ".jpg";
                }
                dgvData.ItemsSource = dt.DefaultView;
                txtInfo.Text = dgvData.Items.Count + " Registros Encontrados.";
                dgvData.Columns[2].Visibility = Visibility.Collapsed;
                dgvData.Columns[1].Visibility = Visibility.Collapsed;
                dgvData.Columns[4].Visibility = Visibility.Collapsed;

            }
            catch (Exception ex)
            {
                showException(ex);
            }
        }

        private void scaleDown(bool select)
        {
            MainContent.SetValue(Grid.ColumnSpanProperty, 1);
            dgvData.FontSize = 14;
            if (select)
            {
                dgvData.ItemsSource = null;
                SelectProducts();
            }
            OptionsContent.Visibility = Visibility.Visible;
        }

        private void scaleUp(bool select)
        {
            MainContent.SetValue(Grid.ColumnSpanProperty, 2);
            dgvData.FontSize = 20;
            dgvData.ItemsSource = null;
            OptionsContent.Visibility = Visibility.Collapsed;
            if (select) SelectProducts();
        }

        private void dgvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgvData.SelectedItem != null && dgvData.Items.Count > 0)
            {
                SelectCategories();
                hidePhotoStack();
                try
                {
                    DataRowView d = (DataRowView)dgvData.SelectedItem;
                    byte id = byte.Parse(d.Row.ItemArray[1].ToString());
                    productType = new ProductImpl();
                    product = productType.Get(id);

                    if (product != null)
                    {
                        SelectProductSizes(id);
                        productSize = null;
                        txtProductSizeName.Text = String.Empty;
                        txtProductSizePrice.Text = String.Empty;
                        txtProductName.Text = product.ProductName;
                        txtProductDescription.Text = product.ProductDescription;
                        cbCategories.SelectedIndex = cbCategories.Items.IndexOf(product.ProductCategoryName);
                        currentImg.Source = new BitmapImage(new Uri(ConfigClass.pathPhotoProduct + product.Photo + ".jpg"));
                        scaleDown(false);
                    }
                } catch(Exception ex)
                {
                    showException(ex);
                }
            } 
        }

        private void SelectProductSizes(byte id)
        {
            DataTable productSizes = productSizeType.Select(id);
            dgvDataUpdate.ItemsSource = null;
            dgvDataUpdate.ItemsSource = productSizes.DefaultView;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (product != null)
            {
                productType = new ProductImpl();
                try
                {
                    int n = productType.Delete(product);
                    if (n > 0)
                    {
                        hidePhotoStack();
                        new PopUpWindow(1, "Registro eliminado de forma exitosa.\n" + DateTime.Now).Show();
                        scaleUp(true);
                    } else
                    {
                        new PopUpWindow(0, "No se realizo la eliminacion.\n" + DateTime.Now).Show();
                    }
                } catch(Exception ex)
                {
                    showException(ex);
                }
            }
        }

        void showException(Exception ex)
        {
            new PopUpWindow(0, "No se pudo completar la acción\nComuniquese con el Adm de Sistemas.\n" + ex.Message).Show();
        }

        void hidePhotoStack()
        {
            frame = null;
            fileName = null;
            stackOptions.Visibility = Visibility.Visible;
            stackCamera.Visibility = Visibility.Collapsed;
            stackUpload.Visibility = Visibility.Collapsed;
            pickedImg.Source = null;
            imgCapture.Source = null;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(product != null)
            {
                productType = new ProductImpl();
                bool validateName = product.ProductName.ToLower() != txtProductName.Text.ToLower() ? true : false;
                ValidatedProduct vp = Main.ValidateProduct(txtProductName.Text, txtProductDescription.Text, cbCategories.Text, validateName, dgvDataUpdate.Items.Count);
                if(vp.isValidated)
                {
                    try
                    {
                        product.ProductName = vp.ProductName;
                        product.ProductDescription = vp.ProductDescription;
                        product.ProductCategoryName = vp.ProductCategory;
                        if (fileName != null || frame != null)
                        {
                            product.Photo = DateTime.Now.ToFileTime().ToString();
                            if (fileName != null && frame == null)
                            {
                                var imagePath = System.IO.Path.Combine(ConfigClass.pathPhotoProduct + product.Photo + ".jpg");
                                File.Copy(fileName, imagePath);
                            }
                            else if (fileName == null && frame != null)
                            {
                                BitmapSource bms = frame as BitmapSource;
                                using (var fileStream = new FileStream(ConfigClass.pathPhotoProduct + product.Photo + ".jpg", FileMode.Create))
                                {
                                    BitmapEncoder encoder = new JpegBitmapEncoder();
                                    encoder.Frames.Add(BitmapFrame.Create(bms as BitmapSource));
                                    encoder.Save(fileStream);
                                }
                            }
                        }
                        int n = productType.Update(product);
                        if (n > 0)
                        {
                            new PopUpWindow(1, "Registro actualizado de forma exitosa.\n" + DateTime.Now).Show();
                            scaleUp(true);
                            hidePhotoStack();
                        }
                        else
                        {
                            new PopUpWindow(0, "No se pudo realizar la actualizacion.\n" + DateTime.Now).Show();
                        }
                    } catch(Exception ex)
                    {
                        showException(ex);
                    }
                }
                
            }
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            string filter = txtFilter.Text.Trim();
            if(filter != string.Empty)
            {
                productType = new ProductImpl();
                try
                {
                    DataTable dt = productType.Select(filter);
                    foreach (DataRow row in dt.Rows)
                    {
                        row["photo"] = ConfigClass.pathPhotoProduct + row["photo"] + ".jpg";
                    }
                    scaleUp(false);
                    dgvData.ItemsSource = dt.DefaultView;
                    txtInfo.Text = dgvData.Items.Count + " Registros Encontrados.";
                    dgvData.Columns[2].Visibility = Visibility.Collapsed;
                    dgvData.Columns[1].Visibility = Visibility.Collapsed;
                    dgvData.Columns[4].Visibility = Visibility.Collapsed;
                    btnFilter.SetValue(Grid.ColumnSpanProperty, 1);
                    btnShowAll.Visibility = Visibility.Visible;
                    txtFilter.Text = string.Empty;

                }
                catch (Exception ex)
                {
                    showException(ex);
                }
            }
        }

        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            txtFilter.Text = string.Empty;
            scaleUp(true);
            btnFilter.SetValue(Grid.ColumnSpanProperty, 2);
            btnShowAll.Visibility = Visibility.Collapsed;
        }

        private void btnAddVariation_Click(object sender, RoutedEventArgs e)
        {
            string name = txtProductSizeName.Text.Trim();
            string error = "";
            float price;
            bool isValid = float.TryParse(txtProductSizePrice.Text, out price);
            if (name != String.Empty)
            {
                if(isValid && price > 0)
                {
                    if(productSize != null)
                    {
                        if(!productSizeType.Exists(name, product.Id) || productSize.Name.ToLower() == name.ToLower())
                        {
                            try
                            {
                                productSize.Name = name;
                                productSize.BasePrice = price;
                                int n = productSizeType.Update(productSize);
                                if(n > 0)
                                {
                                    txtProductSizeName.Text = String.Empty;
                                    txtProductSizePrice.Text = String.Empty;
                                    SelectProductSizes(product.Id);
                                    new PopUpWindow(1, "Insercion de variacion de producto realizada de forma exitosa.\n" + DateTime.Now).Show();
                                    return;
                                } else
                                {
                                    error = "No se realizarion inserciones\n" + DateTime.Now;
                                }
                            } catch(Exception ex)
                            {
                                new PopUpWindow(0, "No se pudo completar la acción\nComuniquese con el Adm de Sistemas.\n" + ex.Message).Show();
                            }
                        } else
                        {
                            error = "Ya existe una variacion con el nombre indicado.";
                        }
                    } else
                    {
                        if(!productSizeType.Exists(name, product.Id))
                        {
                            try
                            {
                                int n = productSizeType.Insert(new ProductSize(product.Id,name, price));
                                if (n > 0)
                                {
                                    txtProductSizeName.Text = String.Empty;
                                    txtProductSizePrice.Text = String.Empty;
                                    SelectProductSizes(product.Id);
                                    toggleStack();
                                    new PopUpWindow(1, "Insercion de variacion de producto realizada de forma exitosa.\n" + DateTime.Now).Show();
                                    return;
                                } else
                                {
                                    error = "No se realizarion inserciones\n" + DateTime.Now;
                                }
                            } catch (Exception ex)
                            {
                                new PopUpWindow(0, "No se pudo completar la acción\nComuniquese con el Adm de Sistemas.\n" + ex.Message).Show();
                            }
                        } else
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

        private void btnDeleteVariation_Click(object sender, RoutedEventArgs e)
        {
            if(productSize != null)
            {
                try
                {
                    int n = productSizeType.Delete(productSize);
                    if (n > 0)
                    {
                        productSize = null;
                        SelectProductSizes(product.Id);
                        toggleStack();
                        new PopUpWindow(1, "Registro eliminado de forma exitosa.\n" + DateTime.Now).Show();
                    }
                    else
                    {
                        new PopUpWindow(0, "No se realizo la eliminacion.\n" + DateTime.Now).Show();
                    }
                } catch(Exception ex)
                {
                    new PopUpWindow(0, "No se pudo completar la acción\nComuniquese con el Adm de Sistemas.\n" + ex.Message).Show();
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
            StopCamera();
        }

        private void dgvDataUpdate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgvDataUpdate.SelectedItem != null && dgvData.Items.Count > 0)
            {
                DataRowView d = (DataRowView)dgvDataUpdate.SelectedItem;
                productSize = new ProductSize(byte.Parse(d.Row.ItemArray[0].ToString()), d.Row.ItemArray[1].ToString(), float.Parse(d.Row.ItemArray[2].ToString()), product.Id);
                if(productSize != null)
                {
                    txtProductSizeName.Text = productSize.Name;
                    txtProductSizePrice.Text = productSize.BasePrice.ToString();
                    btnAddVariation.Content = "Editar Variacion.";
                    btnDeleteVariation.Visibility = Visibility.Visible;
                    stack.Height = 100;
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            productSize = null;
            product = null;
            toggleStack();
            scaleUp(true);
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
        private void camera_Click(object sender, RoutedEventArgs e)
        {
            StartUseCamera();
        }

        private void upload_Click(object sender, RoutedEventArgs e)
        {
            StartUseUpload();
        }

        private void btnUseUpload_Click(object sender, RoutedEventArgs e)
        {
            StopCamera();
            StartUseUpload();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            StartCamera();
        }

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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
            GetVideoDevices();
        }
    }
}
