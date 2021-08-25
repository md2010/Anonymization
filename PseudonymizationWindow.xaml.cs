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
    public partial class PseudonymizationWindow : Window
    {
        private AnonymizationTechniquesManager manager;
        private Pseudonymization pseudoanonymization;
        public PseudonymizationWindow()
        {
            InitializeComponent();
            manager = new AnonymizationTechniquesManager();
            pseudoanonymization = new Pseudonymization(manager);
            dataGrid.ItemsSource = manager.workingVersionData.DefaultView;
            DataContext = manager;
        }

        private void ButtonScramble_Click(object sender, RoutedEventArgs e)
        {
            pseudoanonymization.Technique = "scramble";
            pseudoanonymization.apply();
            dataGrid.ItemsSource = manager.workingVersionData.DefaultView;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEncryption.IsEnabled = true;
            btnRNG.IsEnabled = true;
            btnScramble.IsEnabled = true;
            btnHushFunction.IsEnabled = true;
        }

        private void btnRNG_Click(object sender, RoutedEventArgs e)
        {
            pseudoanonymization.Technique = "randomNumberGenerator";
            pseudoanonymization.apply();
            dataGrid.ItemsSource = manager.workingVersionData.DefaultView;
        }

        private void btnEncryption_Click(object sender, RoutedEventArgs e)
        {
            pseudoanonymization.Technique = "encryption";
            pseudoanonymization.apply();
            dataGrid.ItemsSource = manager.workingVersionData.DefaultView;
        }

        private void btnHushFunction_Click(object sender, RoutedEventArgs e)
        {
            pseudoanonymization.Technique = "hushFunction";
            pseudoanonymization.apply();
            dataGrid.ItemsSource = manager.workingVersionData.DefaultView;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            manager.updateData();
            Close();
        }
    }
}
