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

namespace Anonyimization
{

    public partial class RetrieveColumnWindow : Window
    {
        private AnonymizationTechniquesManager manager;
        public RetrieveColumnWindow()
        {
            InitializeComponent();
            manager = new AnonymizationTechniquesManager();
            manager.setAllAttributes();
            dataGrid.ItemsSource = manager.workingVersionData.DefaultView;
            DataContext = manager;
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            manager.retrieveColumn();
            dataGrid.ItemsSource = manager.workingVersionData.DefaultView;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            manager.updateData();
            Close();
        }
    }
}
