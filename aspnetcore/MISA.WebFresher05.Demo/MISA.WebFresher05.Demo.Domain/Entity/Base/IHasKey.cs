using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Demo.Domain.Entity.Base
{
    public interface IHasKey
    {
        Guid GetKey();
        void SetKey(Guid id);
    }
}
