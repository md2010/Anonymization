using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data;

namespace Anonyimization
{
    class Generalization : AnonymizationTechnique
    {
        private AnonymizationTechniquesManager manager;
        private int[] values;
        private int[,] ranges;
        private int max;
        private int min;
        public int Step { get; set; }

        public Generalization (AnonymizationTechniquesManager manager)
        {
            this.manager = manager;
            manager.setNumericAttributes();           
        }

        public void apply()
        {
            var arr = manager.getAttributeValues();
            values = Array.ConvertAll(arr, int.Parse); 
            min = values.Min();
            max = values.Max();
            generateSteps();
            updateData();
        }

        public void updateData()
        {
            DataTable data = manager.getData();
            for(int i = 0; i < data.Rows.Count; i++)
            {
                int cellValue = int.Parse(data.Rows[i][manager.SelectedAttribute].ToString());
                for (int j = 0; j < ranges.GetLength(0); j++)
                {
                    if (cellValue >= ranges[j,0] && cellValue <= ranges[j,1])
                    {
                        data.Rows[i][manager.SelectedAttribute] = ranges[j,0] + " - " + ranges[j, 1];
                    }
                }              
            }
            manager.workingVersionData = data.Copy();
        }

        private void generateSteps()
        {
            int chunkNumber = (max - min) / Step;
            ranges = new int[chunkNumber,2];       
            for (int i = 0; i < chunkNumber-1; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    ranges[i,j] = min;
                    if (j == 0) 
                        min += Step;
                }
                min += 1;
            }
            if (min != max)
            {
                ranges[ranges.GetLength(0) - 1, 0] = min;
                ranges[ranges.GetLength(0) - 1, 1] = max;
            }
            return;
        }


        //preview ranges
        public DataTable convertArrayToDataTable(int[,] arr)
        {
            DataTable dt = new DataTable();         
            dt.Columns.Add("lower bound");
            dt.Columns.Add("upper bound");
            for (int j = 0; j < arr.GetLength(0); j++)
            {
                DataRow row = dt.NewRow();
                for (int i = 0; i < 2; i++)
                {                 
                    row[i] = arr[j, i];                  
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

    }
}
