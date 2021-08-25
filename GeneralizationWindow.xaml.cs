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
using System.Data;

namespace Anonyimization
{
    public partial class GeneralizationWindow : Window
    {
        private Generalization generalization;
        private AnonymizationTechniquesManager manager;
        public GeneralizationWindow()
        {
            InitializeComponent();
            manager = new AnonymizationTechniquesManager();
            generalization = new Generalization(manager);
            DataContext = manager;
            dataGrid.ItemsSource = manager.workingVersionData.DefaultView;
        }

        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbStep.Text) || !int.TryParse(tbStep.Text, out _))
            {
                tbStep.Text = "Unesite broj!";
            }
            else if (manager.SelectedAttribute == null)
            {
                tbStep.Text = "Odaberite atribut!";
            }
            else
            {
                generalization.Step = int.Parse(tbStep.Text);
                generalization.apply();
                dataGrid.ItemsSource = manager.workingVersionData.DefaultView;
                tbStep.Clear();
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            manager.updateData();
            manager.deleteAttribute();
            Close();
        }

    }
}
