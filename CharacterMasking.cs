using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Anonyimization
{
    class CharacterMasking : IAnonymizationTechnique
    {
        private AnonymizationTechniquesManager manager;
        public string maskingChar;
        public int maskingLength;
        public string startingMaskingPosition;
        private string maskingValue;
        private string[] maskedValues;
        public CharacterMasking(AnonymizationTechniquesManager manager)
        {
            this.manager = manager;
            manager.setAllAttributes();
        }
        public void apply()
        {
            var arr = manager.getAttributeValues();
            maskedValues = new string[arr.Length];
            setMaskingValue();
            if (startingMaskingPosition == "start")
            {
                maskValuesFromStart(arr);
            } else
            {
                maskValuesFromEnd(arr);
            }
            DataTable data = manager.getData();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                data.Rows[i][manager.SelectedAttribute] = maskedValues[i];
            }
            manager.workingVersionData = data.Copy();
        }

        private void maskValuesFromStart(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                StringBuilder sb = new StringBuilder();
                if (arr[i].Length < maskingLength)
                {
                    maskedValues[i] = setMaskingValue(arr[i].Length);
                } else
                {
                    sb.Append(maskingValue);
                    sb.Append(arr[i].Substring(maskingLength));
                    maskedValues[i] = sb.ToString();
                }                
            }
        }

        private void maskValuesFromEnd(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                StringBuilder sb = new StringBuilder();
                if (arr[i].Length < maskingLength)
                {
                    maskedValues[i] = setMaskingValue(arr[i].Length);
                } else
                {
                    sb.Append(arr[i].Substring(0, arr[i].Length - maskingLength));
                    sb.Append(maskingValue);
                    maskedValues[i] = sb.ToString();
                }              
            }
        }

        private void setMaskingValue()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < maskingLength; i++)
            {
                sb.Append(maskingChar);
            }
            maskingValue = sb.ToString();
        }

        private string setMaskingValue(int length) 
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(maskingChar);
            }
            return sb.ToString();
        }
    }
}
