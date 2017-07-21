using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpamFilterAutomata.Transitions;

namespace SpamFilterAutomata.States
{
    public class DocIdState : State
    {

        public DocIdState()
        {
            StateName = "DocIdState";
            var nextState = new DocIdEndState();
            //Transfers.Add(nextState, new DocIdTransfer() {NewState = nextState, PreviousState = this});
        }
    }
}
