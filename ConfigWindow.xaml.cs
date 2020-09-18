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

        public Config.Config Config { get; set; }

        public ConfigWindow()
        {
            InitializeComponent();
            Config = new Config.Config();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadConfig();
        }

        private void LoadConfig()
        {
            rbOverplap.IsChecked = Config.GetOverlap();
            rbNewFile.IsChecked = !Config.GetOverlap();
            txtOutput.Text = Config.GetOutputFolder();
            txtTextSigner.Text = Config.GetSignerText();
            rbSignerInsible.IsChecked = !Config.GetSignerVisible(); 
            rbSignerVisible.IsChecked = Config.GetSignerVisible();
            chkSignerTextVisible.IsChecked = Config.GetTextSignerVisible();
            rbFirstPage.IsChecked = Config.GetFirstPage();
            rbLastPage.IsChecked = Config.GetLastPage();
            rbNumberPage.IsChecked = !rbFirstPage.IsChecked.Value && !rbLastPage.IsChecked.Value;
            txtNumberPage.Value = Config.GetNumberPage();
            txtSizeImg.Value = (decimal) Config.GetSizeImg();
            txtFontSize.Value = (decimal)Config.GetFontSize();
            txtX.Value = (decimal)Config.GetX();
            txtY.Value = (decimal)Config.GetY();
            SetImage(Config.GetImg());
        }

        private void RbOverplap_Checked(object sender, RoutedEventArgs e)
        {
            Config.SetOverlap(rbOverplap.IsChecked.Value);
            setNewFileFields((bool)!rbOverplap.IsChecked);
        }

        private void RbNewFile_Checked(object sender, RoutedEventArgs e)
        {
            setNewFileFields((bool)rbNewFile.IsChecked);
        }

        private void setNewFileFields(bool enabled)
        {
            txtOutput.IsEnabled = enabled;
            btnSearchOutput.IsEnabled = enabled;
            Config.SetOverlap(!enabled);
        }

        private void btnSearchOutput_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() ==  System.Windows.Forms.DialogResult.OK)
            {
                txtOutput.Text = fb.SelectedPath;
                Config.SetOutputFolder(fb.SelectedPath);
            }
        }

        private void TxtTextSigner_TextChanged(object sender, TextChangedEventArgs e)
        {
            Config.SetSignerText(txtTextSigner.Text);
        }

        private void RbSignerInsible_Checked(object sender, RoutedEventArgs e)
        {
            Config.SetSignerVisible(!rbSignerInsible.IsChecked.Value);
        }

        private void RbSignerVisible_Checked(object sender, RoutedEventArgs e)
        {
            Config.SetSignerVisible(rbSignerVisible.IsChecked.Value);
        }

        private void ChkSignerTextVisible_Checked(object sender, RoutedEventArgs e)
        {
            Config.SetTextSignerVisible(chkSignerTextVisible.IsChecked.Value);
        }

        private void RbFirstPage_Checked(object sender, RoutedEventArgs e)
        {
            Config.SetFirstPage(rbFirstPage.IsChecked.Value);
        }

        private void RbLastPage_Checked(object sender, RoutedEventArgs e)
        {
            Config.SetLastPage(rbLastPage.IsChecked.Value);
        }

        private void RbNumberPage_Checked(object sender, RoutedEventArgs e)
        {
            Config.SetFirstPage(false);
            Config.SetLastPage(false);
        }

        private void TxtNumberPage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Config.SetNumberPage(txtNumberPage.Value.Value);
        }

        private void TxtSizeImg_TextChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Config.SetSizeImg((float) txtSizeImg.Value.Value );
        }

        private void TxtFontSize_TextChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Config.SetFontSize((float)txtFontSize.Value.Value);
        }

       
        private void BtnSearchImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog  fd = new OpenFileDialog
            {
                Filter = "Imagens (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            };
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {
                Config.SetImg(fd.FileName);
                SetImage(fd.FileName);
            }
        }

        private void SetImage(string source)
        {
            if (File.Exists(source))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new System.Uri(source);
                bitmap.EndInit();
                image.Source = bitmap;
            }
        }

        private void TxtX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Config.SetX((float)txtX.Value.Value);
        }

        private void TxtY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Config.SetY((float)txtY.Value.Value);
        }
    }
}
