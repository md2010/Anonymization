using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Anonyimization
{
    class AttributeSuppression : IAnonymizationTechnique
    {
        private AnonymizationTechniquesManager manager;
        public AttributeSuppression(AnonymizationTechniquesManager manager)
        {
            this.manager = manager;
            manager.setAllAttributes();
        }

        public void apply()
        {
            DataTable data = manager.getData();
            data.Columns.Remove(manager.SelectedAttribute);
            manager.workingVersionData = data.Copy();
            return;
        }
        
    }
}
