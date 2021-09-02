using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections.ObjectModel;
using System.Linq;

namespace Anonyimization
{
    class LDiversity : MeasureOfAnonymity
    {    
        public int l;
        public ObservableCollection<string> SensitiveAttributes { get; set; }
        public string SensitiveAttribute { get; set; }

        public LDiversity()
        {
            data = Data.GetInstance();
            Attributes = new List<string>(data.getAttributes());
            QuasiIdentifiers = new List<string>();
            SensitiveAttributes = new ObservableCollection<string>();
            dataTable = data.data.Copy();
        }

        public void setSensitiveAttributes()
        {
            foreach (string attribute in Attributes)
            {             
                 SensitiveAttributes.Add(attribute);               
            }
        }

        public bool checkDiversity()
        {
            dataTable = data.data.Copy();            
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
                if (!isDiverse(equivalenceClass))
                {
                    return false;
                }
                removeEquals(counter);
            }
            return true;
        }

        private bool isDiverse(DataTable equivalenceClass)
        {
            List<string> sensitiveValues = new List<string>();
            for (int i = 0; i < equivalenceClass.Rows.Count; i++)
            {
                sensitiveValues.Add(equivalenceClass.Rows[i][SensitiveAttribute].ToString());
            }
            return sensitiveValues.Distinct().Count() >= l;
        }
    }
}
