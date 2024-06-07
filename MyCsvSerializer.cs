using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Globalization;

namespace HW_Reflection
{
    public class MyCsvSerializer: ICsvSerializer
    {
        public char defaultDelimeter;

        public MyCsvSerializer() 
        {
            defaultDelimeter = CultureInfo.CurrentCulture.TextInfo.ListSeparator[0];
        }
        public void SerializeToCsv<T>(List<T> data, string filePath)
        {
            if (data == null || data.Count == 0)
            {
                throw new ArgumentException("Data list is empty or null.");
            }

            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(string.Join(defaultDelimeter, properties.Select(p => p.Name)));
                foreach (T item in data)
                {
                    List<string> propertyValues = new List<string>();
                    foreach (PropertyInfo property in properties)
                    {
                        object value = property.GetValue(item);
                        propertyValues.Add(value != null ? value.ToString() : "");
                    }
                    writer.WriteLine(string.Join(defaultDelimeter, propertyValues));
                }
            }
        }

        public List<T> DeserializeFromCsv<T>(string filePath)
        {
            List<T> data = new List<T>();
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string headerLine = reader.ReadLine();
                string[] headers = headerLine.Split(defaultDelimeter);
                while (!reader.EndOfStream)
                {
                    string[] values = reader.ReadLine().Split(defaultDelimeter);
                    T item = Activator.CreateInstance<T>();

                    for (int i = 0; i < headers.Length; i++)
                    {
                        PropertyInfo property = properties.FirstOrDefault(p => p.Name == headers[i]);
                        if (property != null)
                        {
                            object value = Convert.ChangeType(values[i], property.PropertyType);
                            property.SetValue(item, value);
                        }
                    }

                    data.Add(item);
                }
            }

            return data;
        }
    }
}
