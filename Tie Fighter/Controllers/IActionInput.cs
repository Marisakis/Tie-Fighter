using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Fighter.Controllers
{
    public interface IActionInput<T> 
    {
        void MoveTo(T x, T y);
        void UpdatePosition(T x, T y);
    }
}
