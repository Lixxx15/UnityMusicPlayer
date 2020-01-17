using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.UIInterface
{
    public interface IUIItemSet<T>
    {
        void SetUI(int index, T t);
    }
}
