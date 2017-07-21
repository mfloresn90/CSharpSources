using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpamFilterAutomata
{
    public class State
    {

        public string StateName { get; set; }

        public Dictionary<State, StateTransfer> Transfers { get; set; }

        public State()
        {
            StateName = "BadState";
            Transfers = new Dictionary<State, StateTransfer>();
        }

        public virtual Status ReadNext(char character)
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


            if (allFail)
                return Status.Failure;

            if (NextState == null)
            {
                return Status.Running;
            }

            return Status.Success;
        }

        public State NextState { get; protected set; }

        public void Reset()
        {
            NextState = null;
            foreach (var transfer in Transfers)
            {
                transfer.Value.Reset();
            }

            OnReset();
        }

        public virtual void OnReset()
        {
            
        }

    }


    public enum Status
    {
        Running,
        Success,
        Failure
    }
}
