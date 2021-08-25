using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Anonyimization
{
    class RecordSuppression
    {
        private AnonymizationTechniquesManager manager;
        public List<int> selectedRowIndex;
        public RecordSuppression(AnonymizationTechniquesManager manager)
        {
            this.manager = manager;
            selectedRowIndex = new List<int>();
        }

        public void apply()
        {
            DataTable data = manager.getData();
            foreach(int i in selectedRowIndex)
            {
                data.Rows[i].Delete();
            }         
            manager.workingVersionData = data.Copy();        
        }
    }
}
