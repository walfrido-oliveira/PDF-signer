using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PDFSigner
{
    /// <summary>
    /// Lógica interna para Config.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {

        public Model.Config Config { get; set; }

        public ConfigWindow()
        {
            InitializeComponent();
            Config = new Model.Config();

            SetCombobox();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadConfig((Model.Config)cboConfig.SelectedItem);
        }

        private void SetCombobox()
        {
            cboConfig.Items.Clear();
            Config.ListAll().ForEach(item => cboConfig.Items.Add(item));
            if (cboConfig.Items.Count > 0) cboConfig.SelectedIndex = 0;
            else LoadConfig(null);
        }

        private void LoadConfig(Model.Config config)
        {
            Config = config;
            if (config != null)
            {
                rbOverplap.IsChecked = config.Overlap;
                rbNewFile.IsChecked = !config.Overlap;
                txtOutput.Text = config.OutputFolder;
                txtTextSigner.Text = config.SignerText;
                rbSignerInsible.IsChecked = !config.SignerVisible;
                rbSignerVisible.IsChecked = config.SignerVisible;
                chkSignerTextVisible.IsChecked = config.TextSignerVisible;
                rbFirstPage.IsChecked = config.FirstPage;
                rbLastPage.IsChecked = config.LastPage;
                rbNumberPage.IsChecked = !rbFirstPage.IsChecked.Value && !rbLastPage.IsChecked.Value;
                txtNumberPage.Value = config.NumberPage;
                txtSizeImg.Value = (decimal)config.SizeImg;
                txtFontSize.Value = (decimal)config.FontSize;
                txtX.Value = (decimal)config.X;
                txtY.Value = (decimal)config.Y;
                SetImage(config.Img);
            } else
            {
                txtOutput.Text = "";
                txtTextSigner.Text = "";
                txtNumberPage.Value = 0;
                txtSizeImg.Value = 0;
                txtFontSize.Value = 0;
                txtX.Value = 0;
                txtY.Value = 0;
                image.Source = null;
 
                Config = new Model.Config();
            }
        }

        private void RbOverplap_Checked(object sender, RoutedEventArgs e)
        {
            SetNewFileFields((bool)!rbOverplap.IsChecked);
        }

        private void RbNewFile_Checked(object sender, RoutedEventArgs e)
        {
            SetNewFileFields((bool)rbNewFile.IsChecked);
        }

        private void SetNewFileFields(bool enabled)
        {
            txtOutput.IsEnabled = enabled;
            btnSearchOutput.IsEnabled = enabled;
            Config.Overlap = !enabled;
            Config.InsertOrUpdate();
        }

        private void BtnSearchOutput_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() ==  System.Windows.Forms.DialogResult.OK)
            {
                txtOutput.Text = fb.SelectedPath;
                Config.OutputFolder = fb.SelectedPath;
                Config.InsertOrUpdate();
            }
        }

        private void TxtTextSigner_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTextSigner.Text) && Config != null)
            {
                Config.SignerText = txtTextSigner.Text;
                Config.InsertOrUpdate();
            }
        }

        private void RbSignerInsible_Checked(object sender, RoutedEventArgs e)
        {
            Config.SignerVisible = !rbSignerInsible.IsChecked.Value;
            Config.InsertOrUpdate();
        }

        private void RbSignerVisible_Checked(object sender, RoutedEventArgs e)
        {
            Config.SignerVisible = rbSignerVisible.IsChecked.Value;
            Config.InsertOrUpdate();
        }

        private void ChkSignerTextVisible_Checked(object sender, RoutedEventArgs e)
        {
            Config.TextSignerVisible = true;
            Config.InsertOrUpdate();
        }

        private void ChkSignerTextVisible_Unchecked(object sender, RoutedEventArgs e)
        {
            Config.TextSignerVisible = false;
            Config.InsertOrUpdate();
        }

        private void RbFirstPage_Checked(object sender, RoutedEventArgs e)
        {
            Config.FirstPage = rbFirstPage.IsChecked.Value;
            Config.InsertOrUpdate();
        }

        private void RbLastPage_Checked(object sender, RoutedEventArgs e)
        {
            Config.LastPage = rbLastPage.IsChecked.Value;
            Config.InsertOrUpdate();
        }

        private void RbNumberPage_Checked(object sender, RoutedEventArgs e)
        {
            Config.FirstPage = false;
            Config.LastPage = false;
            Config.InsertOrUpdate();
        }

        private void TxtNumberPage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Config.NumberPage = txtNumberPage.Value.Value;
            Config.InsertOrUpdate();
        }

        private void TxtSizeImg_TextChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (int.TryParse(txtSizeImg.Text, out int result) && Config != null)
            {
                Config.SizeImg = result;
                Config.InsertOrUpdate();
            }
        }

        private void TxtFontSize_TextChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (int.TryParse(txtFontSize.Text, out int result) && Config != null)
            {
                Config.FontSize = result;
                Config.InsertOrUpdate();
            }
        }

       
        private void BtnSearchImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog  fd = new OpenFileDialog
            {
                Filter = "Imagens (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            };
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {
                SetImage(fd.FileName);
            }
        }

        private void SetImage(string source)
        {
            if (File.Exists(source))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(source);
                bitmap.EndInit();
                image.Source = bitmap;
                Config.Img = source;
                Config.InsertOrUpdate();
            } 
            else
            {
                image.Source = null;
            }
        }

        private void TxtX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (float.TryParse(txtX.Text, out float result) && Config != null)
            {
                Config.X = result;
                Config.InsertOrUpdate();
            }
        }

        private void TxtY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (float.TryParse(txtY.Text, out float result) && Config != null)
            {
                Config.Y = result;
                Config.InsertOrUpdate();
            }
        }

       
        private void CboConfig_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Model.Config config = (Model.Config)cboConfig.SelectedItem;
            if (config != null)
            {
                LoadConfig(config);
            }
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            Model.Config config = new Model.Config();

            ModalWindow modalWindow = new ModalWindow();
            modalWindow.ShowDialog();
            config.Name = ModalWindow.Value;

            config.Insert();
            SetCombobox();
            cboConfig.SelectedIndex = cboConfig.Items.Count - 1;
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            if (Config != null) Config.Delete();
            SetCombobox();
        }
    }
}
