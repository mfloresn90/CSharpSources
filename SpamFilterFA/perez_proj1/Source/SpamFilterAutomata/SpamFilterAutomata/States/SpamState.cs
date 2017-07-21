using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpamFilterAutomata.Transitions;

namespace SpamFilterAutomata.States
{
    public class SpamState : State
    {

        private SpamTransfer spamFilter;
        private DocIdEndTransfer _docEndTransfer;
        private StateMachine _stateMachine;
        public SpamState(DocIdEndTransfer docEndTransfer, StateMachine stateMachine)
        {
            _docEndTransfer = docEndTransfer;
            StateName = "SpamState";
            spamFilter = new SpamTransfer();
            _stateMachine = stateMachine;
        }

        public override Status ReadNext(char character)
        {
            if (!spamFilter.Complete)
            {
                if (spamFilter.ReadNext(character) == Status.Success)
                {
                    _stateMachine.AddSpam(_docEndTransfer.DocId, spamFilter);
                }
            }

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

        public override void OnReset()
        {
            spamFilter.Complete = false;
            spamFilter.Reset();
        }
    }
}
