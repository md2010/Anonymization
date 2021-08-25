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
    public partial class RecordSuppressionWindow : Window
    {
        private AnonymizationTechniquesManager manager;
        private RecordSuppression recrodSuppression;
        public RecordSuppressionWindow()
        {
            InitializeComponent();
            manager = new AnonymizationTechniquesManager();
            recrodSuppression = new RecordSuppression(manager);
            DataContext = manager;
            dataGrid.ItemsSource = manager.workingVersionData.DefaultView;
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            int selectedItemsCount = dataGrid.SelectedItems.Count;
            for (int i = 0; i < selectedItemsCount; i++)
            {
                recrodSuppression.selectedRowIndex.Add(dataGrid.Items.IndexOf(dataGrid.SelectedItems[i]));
            }           
            recrodSuppression.apply();
            dataGrid.ItemsSource = manager.workingVersionData.DefaultView;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            manager.updateData();
            recrodSuppression.selectedRowIndex.Clear();
        }
    }
}
