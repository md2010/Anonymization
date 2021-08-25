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
    public partial class PermutationWindow : Window
    {
        private Permutation permutation;
        private AnonymizationTechniquesManager manager;
        public PermutationWindow()
        {
            InitializeComponent();
            manager = new AnonymizationTechniquesManager();
            permutation = new Permutation(manager);
            DataContext = manager;
            dataGrid.ItemsSource = manager.workingVersionData.DefaultView;
        }

        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            if (manager.SelectedAttribute == null)
            {
                comboBox.Text = "Odaberite atribut!";
            }
            permutation.apply();
            dataGrid.ItemsSource = manager.workingVersionData.DefaultView;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            manager.updateData();
            Close();
        }
    }
}
