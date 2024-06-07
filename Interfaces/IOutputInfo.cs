using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Reflection.Interfaces
{
    public interface IOutputInfo
    {
        void OutputResearchInfo(object timeToSerialize, 
                                object timeToSerialize_fromLibrary, 
                                object timeToDeserialize, 
                                object timeToDeserialize_fromLibrary, 
                                string MethodFromLibrary);
    }
}
