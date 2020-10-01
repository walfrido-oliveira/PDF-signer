using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
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
            SetCombobox();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ConfigWindow cw = new ConfigWindow();
            if ((bool)cw.ShowDialog())
            {
                SetCombobox();
            }
        }

        private void SetCombobox()
        {
            comboBox.Items.Clear();
            new Model.Config().ListAll().ForEach(item => comboBox.Items.Add(item));
            if (comboBox.Items.Count > 0) comboBox.SelectedIndex = 0;
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
                if (comboBox.SelectedIndex == -1 )
                {
                    System.Windows.MessageBox.Show("Selecione um configuração!");
                    return;
                }

                SignerCollection items = new SignerCollection();
                Model.Config config = (Model.Config)comboBox.SelectedItem;
           
                wait.Visibility = Visibility.Visible;
                foreach (string item in files)
                {
                    Signer.Signer siger = new Signer.Signer(item, config);
                    items.Add(siger);
                }

                if (items.Count > 0)
                {
                    items.Signin();

                    if (config.Overlap)
                    {
                        string output = items[0].Output;
                        output = output.Substring(0, output.LastIndexOf("\\"));
                        System.Diagnostics.Process.Start(output);
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(config.OutputFolder);
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
                if (comboBox.SelectedIndex == -1)
                {
                    System.Windows.MessageBox.Show("Selecione um configuração!");
                    return;
                }

                SignerCollection items = new SignerCollection();
                Model.Config config = (Model.Config)comboBox.SelectedItem;

                wait.Visibility = Visibility.Visible;
                foreach (string item in files)
                {
                    Signer.Signer siger = new Signer.Signer(item, config);
                    items.Add(siger);
                }

                if (items.Count > 0)
                {
                    items.Signin();

                    if (config.Overlap)
                    {
                        string output = items[0].Output;
                        output = output.Substring(0, output.LastIndexOf("\\"));
                        System.Diagnostics.Process.Start(output);
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(config.OutputFolder);
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
