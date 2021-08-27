using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data;

namespace Anonyimization
{
    class AnonymizationTechniquesManager
    {
        public ObservableCollection<string> Attributes { get; set; }
        public string SelectedAttribute { get; set; }
        private Data data;
        public DataTable workingVersionData;
        public AnonymizationTechniquesManager()
        {
            data = Data.GetInstance();
            workingVersionData = data.data.Copy();
        }

        public void setNumericAttributes()
        {
            Attributes = new ObservableCollection<string>(data.getNumericAttributes());
        }

        public void setCategoricalAttributes()
        {
            Attributes = new ObservableCollection<string>(data.getCategoricalAttributes());
        }

        public void setAllAttributes()
        {
            Attributes = new ObservableCollection<string>(data.getAttributes());
        }

        public string[] getAttributeValues()
        {
            var attributeValues = data.data.Rows.OfType<DataRow>()
                     .Select(row => row[SelectedAttribute].ToString())
                     .ToArray();
            return attributeValues;
        }   
        
        private string[] getOriginalAttributeValues()
        {
            var attributeValues = data.originalData.Rows.OfType<DataRow>()
                     .Select(row => row[SelectedAttribute].ToString())
                     .ToArray();
            return attributeValues;
        }

        public void deleteAttribute()
        {
            Attributes.Remove(SelectedAttribute);
        }

        public void permanentlyDeleteAttribute()
        {           
            data.attributes.Remove(SelectedAttribute);
            deleteAttribute();
        }

        public DataTable getData()
        {
            return data.data.Copy();
        }

        public void updateData()
        {           
            data.data = workingVersionData.Copy();
        }

        public void retrieveColumn()
        {
            var values = getOriginalAttributeValues();
            for (int i = 0; i < workingVersionData.Rows.Count; i++)
            {
                workingVersionData.Rows[i][SelectedAttribute] = values[i];
            }
        }

    }
}
