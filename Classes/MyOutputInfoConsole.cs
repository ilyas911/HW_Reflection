using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW_Reflection.Interfaces;

namespace HW_Reflection.Classes
{
    public class MyOutputInfoConsole : IOutputInfo
    {
        public void OutputResearchInfo(object timeToSerialize, 
                                       object timeToSerialize_fromLibrary, 
                                       object timeToDeserialize, 
                                       object timeToDeserialize_fromLibrary, 
                                       string MethodFromLibrary)
        {
            Console.WriteLine("Время на сериализацию = " + timeToSerialize.ToString());
            Console.WriteLine("Время на десериализацию = " + timeToDeserialize.ToString());
            Console.WriteLine("Стандартный механизм (" + MethodFromLibrary + "):");
            Console.WriteLine("Время на сериализацию = " + timeToSerialize_fromLibrary.ToString());
            Console.WriteLine("Время на десериализацию = " + timeToDeserialize_fromLibrary.ToString());
        }
    }
}
