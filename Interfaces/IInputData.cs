using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Reflection.Interfaces
{
    public interface IInputData
    {
        public void InitializeInput_IterationsCount();
        public int InputIterationsCount_WithCheck();
    }
}
