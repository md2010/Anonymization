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
using System.Collections.ObjectModel;

namespace Anonyimization
{
    public partial class KAnonymityWindow : Window
    {      
        private KAnonymity kAnonymity;
        public KAnonymityWindow()
        {
            InitializeComponent();
            kAnonymity = new KAnonymity();
            kAnonymity.populateCheckBoxList();
            dataGrid.ItemsSource = kAnonymity.dataTable.DefaultView;
            DataContext = kAnonymity;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            kAnonymity.setQuasiIdentifiers();
            if (string.IsNullOrEmpty(kTextBox.Text) || !int.TryParse(kTextBox.Text, out _))
            {
                kTextBox.Text = "Unesite broj!";
            }
            kAnonymity.k = int.Parse(kTextBox.Text);
            kAnonymity.anonymize();
            dataGrid.ItemsSource = kAnonymity.anonymizedTable.DefaultView;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            kAnonymity.save();
            Close();
        }
    }
}
