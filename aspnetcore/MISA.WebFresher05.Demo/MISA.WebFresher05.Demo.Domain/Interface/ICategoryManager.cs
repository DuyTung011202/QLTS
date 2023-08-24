using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Demo.Domain.Interface
{
    public interface ICategoryManager
    {
        Task CheckCategoryExistByCode(string code);
    }
}
