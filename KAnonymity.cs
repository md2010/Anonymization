using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data;

namespace Anonyimization
{
    class KAnonymity : MeasureOfAnonymity
    {       
        public DataTable anonymizedTable;
        public int k;

        public KAnonymity()
        {
            data = Data.GetInstance();
            Attributes = new List<string>(data.getAttributes());
            QuasiIdentifiers = new List<string>();
            dataTable = data.data.Copy();
        }
       
        public void anonymize()
        {
            dataTable = data.data.Copy();
            anonymizedTable = new DataTable();
            anonymizedTable = dataTable.Clone();           
            DataTable equivalenceClass = dataTable.Clone();

            while (dataTable.Rows.Count > 0)
            {
                equivalenceClass.Rows.Clear();
                int counter = 0;
                equivalenceClass.ImportRow(dataTable.Rows[0]);
                counter++;

                for (int j = 1; j < dataTable.Rows.Count; j++)
                {
                    if (isEqual(dataTable.Rows[0], dataTable.Rows[j]))
                    {
                        equivalenceClass.ImportRow(dataTable.Rows[j]);
                        counter++;
                    }
                }
                if (equivalenceClass.Rows.Count > k)
                {
                    addToAnonymizedTable(equivalenceClass);
                    removeSurplus(counter);
                }
                else if (equivalenceClass.Rows.Count == k)
                {
                    addToAnonymizedTable(equivalenceClass); 
                }
                removeEquals(counter);
            }
        }
        
        private void addToAnonymizedTable(DataTable equivalenceClass)
        {
            foreach(DataRow row in equivalenceClass.Rows)
            {
                anonymizedTable.ImportRow(row);
            }
        }
        private void removeSurplus(int addedRows)
        {
            int surplus = addedRows - k;          
            int i = anonymizedTable.Rows.Count - 1;
            int counter = 0;
            while (counter < surplus)
            {
                anonymizedTable.Rows.Remove(anonymizedTable.Rows[i]);
                i--;
                counter++;
            }
        }       

        public void save()
        {
            data.data = anonymizedTable.Copy();
        }

    }
}
