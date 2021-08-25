using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace Anonyimization
{
    public partial class MainWindow : Window
    {
        private string filePath;
        private Data data;
        public MainWindow()
        {
            InitializeComponent();
            btnUpload.IsEnabled = false;
            data = Data.GetInstance();
            DataContext = data;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnUpload.IsEnabled = true;
            ComboBoxItem typeItem = (ComboBoxItem)cbDelimiter.SelectedItem;
            data.Delimiter = typeItem.Content.ToString();
        }
        private void BtnUploadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".csv"; // Required file extension 
            fileDialog.Filter = "CSV documents (.csv)|*.csv"; // Optional file extensions       
            fileDialog.ShowDialog();
            filePath = fileDialog.FileName;
            showData();
        }

        private void showData()
        {
            DataTable dt = data.convertCSV(filePath);
            dataGrid.ItemsSource = dt.DefaultView;
            btnNext.IsEnabled = true;
        }

        private void ButtonNext1_Click(object sender, RoutedEventArgs e)
        {
            AnonymizationTechniquesWindow1 window2 = new AnonymizationTechniquesWindow1();
            window2.Show();
            Close();
        }

    }
}

