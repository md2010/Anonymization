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
    public partial class AnonymizationTechniquesWindow1 : Window
    {
        public AnonymizationTechniquesWindow1()
        {
            InitializeComponent();
        }

        private void ButtonGeneralization_Click(object sender, RoutedEventArgs e)
        {
            GeneralizationWindow window2 = new GeneralizationWindow();
            window2.Show();
        }

        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window2 = new MainWindow();
            window2.Show();
        }

        private void ButtonAttributeSuppression_Click(object sender, RoutedEventArgs e)
        {
            AttributeSuppressionWindow window2 = new AttributeSuppressionWindow();
            window2.Show();
        }

        private void ButtonMasking_Click(object sender, RoutedEventArgs e)
        {
            CharacterMaskingWindow window2 = new CharacterMaskingWindow();
            window2.Show();
        }

        private void ButtonRecordSuppression_Click(object sender, RoutedEventArgs e)
        {
            RecordSuppressionWindow window2 = new RecordSuppressionWindow();
            window2.Show();
        }

        private void ButtonKAnonyimity_Click(object sender, RoutedEventArgs e)
        {
            KAnonymityWindow window2 = new KAnonymityWindow();
            window2.Show();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            FinishWindow window2 = new FinishWindow();
            window2.Show();
        }

        private void ButtonPermutation_Click(object sender, RoutedEventArgs e)
        {
            PermutationWindow window2 = new PermutationWindow();
            window2.Show();
        }

        private void ButtonPseudonymization_Click(object sender, RoutedEventArgs e)
        {
            PseudonymizationWindow window2 = new PseudonymizationWindow();
            window2.Show();
        }
    }
}
