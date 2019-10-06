using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Fighter.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Event"></typeparam> Action event.
    /// <typeparam name="T"> Should be a numeral class (such as Int(16,32,64,128), Float, Double, etc) </typeparam>
    public abstract class GeneralController<Event, T>
    {
        public IActionInput<T> actionInput { get; set; }
        public GeneralController(IActionInput<T> actionInput)
        {
            this.actionInput = actionInput;
        }   // Should implement interface later, to bind actions to specific input.

        public abstract void Action(Event eventData);
    }
}
