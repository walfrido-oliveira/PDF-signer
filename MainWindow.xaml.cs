using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PDFSigner.Signer;

namespace PDFSigner
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ConfigWindow cw = new ConfigWindow();
            cw.ShowDialog();
        }

        private readonly List<string> files = new List<string>();
        private void Image_DragOver(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData("FileDrop", false);
                this.files.Clear();
                foreach (string file in files)
                {
                    if (System.IO.Path.GetExtension(file) == ".pdf")
                    {
                        this.files.Add(file);
                    }
                }
               
            }

        }

        private void Image_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                e.Effects = System.Windows.DragDropEffects.None;
            }
        }

        private void Image_DragLeave(object sender, System.Windows.DragEventArgs e)
        {
            
        }

        private void Image_Drop(object sender, System.Windows.DragEventArgs e)
        {
            SigerFiles(this.files);
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
         
            OpenFileDialog fd = new OpenFileDialog
            {
                Filter = "PDF | *.pdf",
                Multiselect = true
            };
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SigerFiles(fd.FileNames);
            }

        }

        private void SigerFiles(List<string> files)
        {
            try
            {
                SignerCollection items = new SignerCollection();
                Config.Config config = new Config.Config();

                wait.Visibility = Visibility.Visible;
                foreach (string item in files)
                {
                    Signer.Signer siger = new Signer.Signer(item, config);
                    items.Items.Add(siger);
                }

                if (items.Items.Count > 0)
                {
                    items.SigninColletion();

                    if (config.GetOverlap())
                    {
                        string output = items.Items[0].Output;
                        output = output.Substring(0, output.LastIndexOf("\\"));
                        System.Diagnostics.Process.Start(output);
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(config.GetOutputFolder());
                    }
                }
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
            finally
            {
                wait.Visibility = Visibility.Hidden;
            }
        }

        private void SigerFiles(string[] files)
        {
            try
            {
                SignerCollection items = new SignerCollection();
                Config.Config config = new Config.Config();

                wait.Visibility = Visibility.Visible;
                foreach (string item in files)
                {
                    Signer.Signer siger = new Signer.Signer(item, config);
                    items.Items.Add(siger);
                }

                if (items.Items.Count > 0)
                {
                    items.SigninColletion();

                    if (config.GetOverlap())
                    {
                        string output = items.Items[0].Output;
                        output = output.Substring(0, output.LastIndexOf("\\"));
                        System.Diagnostics.Process.Start(output);
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(config.GetOutputFolder());
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
            finally
            {
                wait.Visibility = Visibility.Hidden;
            }
        }

        
    }
}
