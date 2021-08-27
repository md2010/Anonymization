using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Data;

namespace Anonyimization
{
    class MeasureOfAnonymity
    {
        public List<string> Attributes { get; set; }
        public ObservableCollection<CheckBoxAttribute> CheckBoxList { get; set; }
        public List<string> QuasiIdentifiers { get; set; }
        public DataTable dataTable;
        protected Data data;

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

        protected bool isEqual(DataRow wantedRow, DataRow rowValue)
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

        protected void removeEquals(int equalRows)
        {
            for (int i = 0; i < equalRows; i++)
            {
                dataTable.Rows.Remove(dataTable.Rows[0]);
            }
        }

    }
}
