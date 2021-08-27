using System;
using System.Collections.Generic;
using System.Text;

namespace Anonyimization
{
    class Perturbation : IAnonymizationTechnique
    {
        private AnonymizationTechniquesManager manager;
        public double Base { get; set; }
        private int[] values;
        private double[] perturbatedValues;

        public Perturbation(AnonymizationTechniquesManager manager)
        {
            this.manager = manager;
            manager.setNumericAttributes();
        }

        public void apply()
        {
            values = Array.ConvertAll(manager.getAttributeValues(), int.Parse);
            perturbatedValues = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                int multiplier = (int)(values[i] / Base);
                double modulo = values[i] % Base;
                if (modulo > (Base / 2))
                {
                    perturbatedValues[i] = Base * (multiplier + 1);
                }
                else
                {
                    perturbatedValues[i] = Base * multiplier;
                }                
            }
            updateData();
        }

        private void updateData()
        {
            for (int i = 0; i < manager.workingVersionData.Rows.Count; i++)
            {
                manager.workingVersionData.Rows[i][manager.SelectedAttribute] = perturbatedValues[i];
            }           
        }
    }
}
