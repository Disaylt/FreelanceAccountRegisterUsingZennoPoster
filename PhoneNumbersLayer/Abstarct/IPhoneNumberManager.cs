using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneNumbersLayer.Abstarct
{
    public interface IPhoneNumberManager
    {
        string GetNumber();
        string GetMessage();
        void Complete();
    }
}
