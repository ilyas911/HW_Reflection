using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Globalization;
using HW_Reflection.Interfaces;

namespace HW_Reflection.Classes
{
    public class MyCsvSerializer : ICsvSerializer
    {
        public char defaultDelimeter;

        public MyCsvSerializer()
        {
            defaultDelimeter = CultureInfo.CurrentCulture.TextInfo.ListSeparator[0];
        }
        public string SerializeToCsv<T>(List<T> data)
        {
            string toReturn = string.Empty;
            if (data == null || data.Count == 0)
            {
                throw new ArgumentException("Data list is empty or null.");
            }

            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields();
            PropertyInfo[] properties = type.GetProperties();
            toReturn += string.Join(defaultDelimeter, fields.Select(p => p.Name)) + string.Join(defaultDelimeter, properties.Select(p => p.Name)) + "\n";
            foreach (T item in data)
            {
                List<string> propertyAndFieldValues = new List<string>();
                foreach (FieldInfo field in fields)
                {
                    object value = field.GetValue(item);
                    propertyAndFieldValues.Add(value != null ? value.ToString() : "");
                }
                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(item);
                    propertyAndFieldValues.Add(value != null ? value.ToString() : "");
                }
                toReturn += string.Join(defaultDelimeter, propertyAndFieldValues) + "\n";
            }
            toReturn = toReturn.Substring(0, toReturn.Length - 1);
            return toReturn;
        }

        public List<T> DeserializeFromCsv<T>(string datatoDeserialize)
        {
            List<T> data = new List<T>();
            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields();
            PropertyInfo[] properties = type.GetProperties();

            string[] lines = datatoDeserialize.Split('\n'); 
            string headerLine = lines[0];
            string[] headers = headerLine.Split(defaultDelimeter);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(defaultDelimeter);
                T item = Activator.CreateInstance<T>();

                for (int j = 0; j < headers.Length; j++)
                {
                    PropertyInfo property = properties.FirstOrDefault(p => p.Name == headers[j]);
                    if (property != null)
                    {
                        object value = Convert.ChangeType(values[j], property.PropertyType);
                        property.SetValue(item, value);
                    }
                    FieldInfo field = fields.FirstOrDefault(p => p.Name == headers[j]);
                    if (field != null)
                    {
                        object value = Convert.ChangeType(values[j], field.FieldType);
                        field.SetValue(item, value);
                    }
                }
                data.Add(item);
            }
            return data;
        }
    }
}
