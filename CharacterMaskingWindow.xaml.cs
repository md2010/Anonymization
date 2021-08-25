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
using System.Linq;

namespace Anonyimization
{
    public partial class CharacterMaskingWindow : Window
    {
        private CharacterMasking characterMasking;
        private AnonymizationTechniquesManager manager;
        public CharacterMaskingWindow()
        {
            InitializeComponent();
            manager = new AnonymizationTechniquesManager();
            characterMasking = new CharacterMasking(manager);
            DataContext = manager;
            dataGrid.ItemsSource = manager.workingVersionData.DefaultView;
        }

        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            string comboBoxMaskingCharName = ((ComboBoxItem)comboBoxMaskingChar.SelectedItem).Name;
            if(comboBoxMaskingCharName == "asteriks")
            {
                characterMasking.maskingChar = "*";
            } else
            {
                characterMasking.maskingChar = "#";
            }
            characterMasking.startingMaskingPosition = ((ComboBoxItem)comboBoxMaskingPosition.SelectedItem).Name;
            if (string.IsNullOrEmpty(tbMaskingLength.Text) || !int.TryParse(tbMaskingLength.Text, out _))
            {
                tbMaskingLength.Text = "Duljina mora biti veća od nula!";
            }
            else if (manager.SelectedAttribute == null)
            {
                tbMaskingLength.Text = "Odaberite atribut!";
            }
            else if (int.Parse(tbMaskingLength.Text) > manager.getAttributeValues().Max(l => l.Length))
            {
                tbMaskingLength.Text = "Duljina maskiranja duža od najduže riječi!";
            }
            else
            {
                characterMasking.maskingLength = int.Parse(tbMaskingLength.Text);
                characterMasking.apply();
                dataGrid.ItemsSource = manager.workingVersionData.DefaultView;
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
