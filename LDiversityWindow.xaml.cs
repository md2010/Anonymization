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
    public partial class LDiversityWindow : Window
    {
        private LDiversity lDiversity;
        public LDiversityWindow()
        {
            InitializeComponent();
            lDiversity = new LDiversity();
            lDiversity.populateCheckBoxList();
            dataGrid.ItemsSource = lDiversity.dataTable.DefaultView;
            DataContext = lDiversity;
            cbSensitiveAttribute.IsEnabled = true;
            lDiversity.setSensitiveAttributes();
        }

        private void checkLDiversity_Click(object sender, RoutedEventArgs e)
        {
            lDiversity.setQuasiIdentifiers();
            if (string.IsNullOrEmpty(lTextBox.Text) || !int.TryParse(lTextBox.Text, out _))
            {
                lTextBox.Text = "Unesite broj!";
            }
            lDiversity.l = int.Parse(lTextBox.Text);
            if (lDiversity.checkDiversity())
            {
                MessageBox.Show("Podaci zadovoljavaju l-raznolikost za odabrani l!");
            } 
            else
            {
                MessageBox.Show("Podaci ne zadovoljavaju l-raznolikost za odabrani l!");
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
