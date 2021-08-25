using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data;

namespace Anonyimization
{
    class KAnonymity
    {
        public List<string> Attributes { get; set; }
        public ObservableCollection<CheckBoxAttribute> CheckBoxList { get; set; }
        public List<string> QuasiIdentifiers { get; set; }
        public DataTable dataTable;
        private Data data;
        public DataTable anonymizedTable;
        public int k;

        public KAnonymity()
        {
            data = Data.GetInstance();
            Attributes = new List<string>(data.getAttributes());
            QuasiIdentifiers = new List<string>();
            dataTable = data.data.Copy();
        }

        public void populateCheckBoxList()
        {
            CheckBoxList = new ObservableCollection<CheckBoxAttribute>();
            foreach (string name in Attributes)
            {
                CheckBoxList.Add(new CheckBoxAttribute { IsSelected = false, Attribute = name });
            }
        }

        public void setQuasiIdentifiers()
        {
            foreach (CheckBoxAttribute attribute in CheckBoxList)
            {
                if (attribute.IsSelected)
                {
                    QuasiIdentifiers.Add(attribute.Attribute);
                }
            }
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

        private void removeEquals(int equalRows)
        {
            for (int i = 0; i < equalRows; i++)
            {
                dataTable.Rows.Remove(dataTable.Rows[0]);              
            }
        }

        private bool isEqual(DataRow wantedRow, DataRow rowValue)
        {
            int counter = 0;
            foreach (string q in QuasiIdentifiers)
            {
                if (rowValue[q].Equals(wantedRow[q]))
                {
                    counter++;
                }
            }
            return counter == QuasiIdentifiers.Count;
        }

        public void save()
        {
            data.data = anonymizedTable.Copy();
        }

    }
}
