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
    public partial class PerturbationWindow : Window
    {
        private AnonymizationTechniquesManager manager;
        private Perturbation perturbation;

        public PerturbationWindow()
        {
            InitializeComponent();
            manager = new AnonymizationTechniquesManager();
            perturbation = new Perturbation(manager);
            dataGrid.ItemsSource = manager.workingVersionData.DefaultView;
            DataContext = manager;
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            perturbation.apply();
            dataGrid.ItemsSource = manager.workingVersionData.DefaultView;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cbBase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)cbBase.SelectedItem;
            perturbation.Base = double.Parse(typeItem.Content.ToString());
        }
    }
}
