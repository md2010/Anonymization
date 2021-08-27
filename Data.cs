using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Data;
using System.Linq;
using System.Diagnostics;

namespace Anonyimization
{
    class Data
    {
        public Dictionary<string, string> attributes = new Dictionary<string, string>();
        public List<string> quasiIdentifiers = new List<string>();
        public DataTable data = new DataTable();
        public DataTable originalData = new DataTable();
        private Data() {}
        private static Data instance;
        public string Delimiter { get; set; }

        public static Data GetInstance()
        {
            if (instance == null)
            {
                instance = new Data();
            }
            return instance;
        }

        public DataTable convertCSV(string filePath) 
        {         
            var arr = File.ReadLines(filePath).Select(x => x.Split(Delimiter)).ToArray();          
            populateDataTable(arr);
            checkDataTypes(arr[1]);
            return data;
        }

        public void exportCSV(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = data.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(Delimiter, columnNames));

            foreach (DataRow row in data.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(Delimiter, fields));
            }

            File.WriteAllText(filePath, sb.ToString());
        }

        private void populateDataTable(string[][] arr)
        {          
            for (int j = 0; j < arr[0].Length; j++)
            {
                data.Columns.Add(new DataColumn(arr[0][j].ToString()));
                this.attributes.Add(arr[0][j], "string");
            }
            for (int i = 1; i < arr.GetLength(0); i++)
            {
                var newRow = data.NewRow();
                for (int j = 0; j < arr[i].Length; j++)
                    newRow[arr[0][j].ToString()] = arr[i][j];
                data.Rows.Add(newRow);
            }
            originalData = data.Copy();
            return;     
        }

        private void checkDataTypes(string[] row)
        {
            for (int i = 0; i < row.Length; i++)
            {
                if (int.TryParse(row[i], out _))
                {
                    this.attributes[attributes.ElementAt(i).Key] = "integer";
                }                                      
            }
        }

        public List<string> getNumericAttributes()
        {
            List<string> numericAttributes = new List<string>();
            foreach(KeyValuePair<string, string> item in attributes)
            {
                if(item.Value == "integer")
                {
                    numericAttributes.Add(item.Key);
                }
            }           
            return numericAttributes;
        }

        public List<string> getCategoricalAttributes()
        {
            List<string> categoricalAttributes = new List<string>();
            foreach (KeyValuePair<string, string> item in attributes)
            {
                if (item.Value == "string")
                {
                    categoricalAttributes.Add(item.Key);
                }
            }
            return categoricalAttributes;
        }

        public List<string> getAttributes()
        {
            List<string> attributes = new List<string>();
            foreach (KeyValuePair<string, string> item in this.attributes)
            {
                attributes.Add(item.Key);              
            }
            return attributes;
        }

        public void addQuasiIdentifier(string attribute)
        {
            quasiIdentifiers.Add(attribute);
        }

    }
}
