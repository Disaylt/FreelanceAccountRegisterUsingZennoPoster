using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptchaLayer.Abstract
{
    public interface ICaptchaService<in T, out CaptchaResultT, ServiceResultT>
    {
        ServiceResultT Send(T captchaData);
        CaptchaResultT Get(ServiceResultT serviceResult);
    }
}
