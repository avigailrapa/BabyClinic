using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Service.Interfaces
{
    public interface IsExist<T>
    {
        public T Exist(Login login);

    }
}
