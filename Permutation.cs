using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace Anonyimization
{
    class Permutation : AnonymizationTechnique
    {
        private AnonymizationTechniquesManager manager;
        private string[] permutatedValues;
        public Permutation(AnonymizationTechniquesManager manager)
        {
            this.manager = manager;
            manager.setAllAttributes();
        }
        public void apply()
        {
            string[] values = manager.getAttributeValues();
            Random rnd = new Random();
            permutatedValues = values.OrderBy(x => rnd.Next()).ToArray();
            updateData();        
        }

        private void updateData()
        {
            DataTable data = manager.getData();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                data.Rows[i][manager.SelectedAttribute] = permutatedValues[i];               
            }
            manager.workingVersionData = data.Copy();
        }
    }
}
