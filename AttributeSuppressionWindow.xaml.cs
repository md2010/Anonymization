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
    public partial class AttributeSuppressionWindow : Window
    {
        private AttributeSuppression attributeSuppression;
        private AnonymizationTechniquesManager manager;
        public AttributeSuppressionWindow()
        {
            InitializeComponent();
            manager = new AnonymizationTechniquesManager();
            attributeSuppression = new AttributeSuppression(manager);
            DataContext = manager;
            dataGrid.ItemsSource = manager.workingVersionData.DefaultView;
        }

        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            if (manager.SelectedAttribute == null)
            {
                comboBox.Text = "Odaberite atribut!";
            }
            attributeSuppression.apply();
            dataGrid.ItemsSource = manager.workingVersionData.DefaultView;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            manager.updateData();
            manager.permanentlyDeleteAttribute();
            Close();
        }
    }
}
