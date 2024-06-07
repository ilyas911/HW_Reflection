using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Reflection.Interfaces
{
    public interface ICsvSerializer
    {
        string SerializeToCsv<T>(List<T> objects);
        List<T> DeserializeFromCsv<T>(string objects);
    }
}
