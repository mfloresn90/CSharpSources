using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpamFilterAutomata.Transitions;

namespace SpamFilterAutomata.States
{
    public class DocIdEndState : State
    {

        public DocIdEndState()
        {
            StateName = "DocIdEndState";
            //var nextState = new SpamState();
            //Transfers.Add(nextState, new DocIdEndTransfer() {NewState = nextState, PreviousState = this});
        }
    }
}
