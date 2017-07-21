using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpamFilterAutomata
{
    public class StateTransfer
    {
        public State NewState { get; set; }
        public State PreviousState { get; set; }


        public virtual bool CanMove()
        {
            return true;
        }

        public virtual Status ReadNext(char character)
        {
            return Status.Success;
        }

        public virtual void Reset()
        {
            
        }
    }
}
