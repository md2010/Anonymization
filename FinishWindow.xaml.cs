using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace Anonyimization
{
    public partial class FinishWindow : Window
    {
        private Data data;
        public FinishWindow()
        {
            InitializeComponent();
            data = Data.GetInstance();
            dataGrid.ItemsSource = data.data.DefaultView;
            DataContext = data;
        }

        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.ShowDialog();
            string filePath = fileDialog.FileName;
            data.exportCSV(filePath);
        }
    }
}
