using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Reflection
{
    public interface ICsvSerializer
    {
        void SerializeToCsv<T>(List<T> objects, string filePath);
        List<T> DeserializeFromCsv<T>(string filePath);
    }
}
