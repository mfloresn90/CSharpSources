using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpamFilterAutomata.States;
using SpamFilterAutomata.Transitions;

namespace SpamFilterAutomata
{
    public class DocState : State
    {


        public DocState()
        {
            StateName = "DocState";
            var docIdState = new DocIdState();
            //Transfers.Add(docIdState, new DocStateTransfer() {NewState = docIdState, PreviousState = this});
        }


        public override Status ReadNext(char character)
        {
            bool allFail = true;
            foreach (var transfer in Transfers)
            {
                var status = transfer.Value.ReadNext(character);
                if (status != Status.Failure)
                    allFail = false;

                if (status == Status.Success)
                {
                    NextState = transfer.Value.NewState;
                    break;
                }
            }


            if(allFail)
                return Status.Failure;

            if (NextState == null)
            {
                return Status.Running;
            }

            return Status.Success;
        }
    }
}
