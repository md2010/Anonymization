using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using System.Data;
using System.Security.Cryptography;

namespace Anonyimization
{
    class Pseudonymization : IAnonymizationTechnique
    {
        private AnonymizationTechniquesManager manager;
        private string[] values;
        private string[] pseudonymizedValues;
        public string Technique { get; set; }
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        public Pseudonymization(AnonymizationTechniquesManager manager)
        {
            this.manager = manager;
            manager.setCategoricalAttributes();
        }

        public void apply()
        {
            values = manager.getAttributeValues();
            pseudonymizedValues = new string[values.Length];
            MethodInfo mi = this.GetType().GetMethod(Technique);            
            mi.Invoke(this, null);
        }

        public void scramble()  //calling with Reflection method must be public
        {
            Random rnd = new Random();
            for(int i = 0; i < values.Length; i++)
            {
                string scrambled = new string(values[i].ToCharArray().OrderBy(s => (rnd.Next(2) % 2) == 0).ToArray());
                pseudonymizedValues[i] = scrambled;
            }
            updateData();          
        }

        public void randomNumberGenerator()
        {
            for (int i = 0; i < values.Length; i++)
            {
                byte[] randomNumber = new byte[1];
                rngCsp.GetBytes(randomNumber);         
                pseudonymizedValues[i] = randomNumber[0].ToString(); 
            }
            updateData();
        }

        public void encryption()
        {
            for (int i = 0; i < values.Length; i++)
            {
                byte[] asciiBytes = Encoding.ASCII.GetBytes(values[i]);
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < asciiBytes.Length; j++)
                {
                    sb.Append(Convert.ToChar(asciiBytes[j] + 2));
                }
                pseudonymizedValues[i] = sb.ToString();
            }
            updateData();
        }

        public void hushFunction()
        {
            for (int i = 0; i < values.Length; i++)
            {
                byte[] asciiBytes = Encoding.ASCII.GetBytes(values[i]);
                int sum = 0;
                for (int j = 0; j < asciiBytes.Length; j++)
                {
                    sum += asciiBytes[j];
                }
                pseudonymizedValues[i] = sum.ToString();
            }
            updateData();
        }

        private void updateData()
        {           
            DataTable data = manager.getData();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                data.Rows[i][manager.SelectedAttribute] = pseudonymizedValues[i];
            }
            manager.workingVersionData = data.Copy();        
        }

    }
}
