using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpamFilterAutomata.States;
using SpamFilterAutomata.Transitions;

namespace SpamFilterAutomata
{
    class Program
    {
        static void Main(string[] args)
        {

            StateMachine machine = new StateMachine();
            machine.StartState = SetupStates(machine);
            machine.CurrentState = machine.StartState;
           // IState state = new DocState(new HeaderState(new HeaderEndState(new ReadBodyState(null, stateMachine), stateMachine), stateMachine), stateMachine);

            using (var textStream = File.OpenText("messagefile.txt"))
            {
                while (!textStream.EndOfStream)
                {
                    var read = (char) textStream.Read();
                    machine.Input(read);


                }
            }

            machine.PrintSpam();

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static DocState SetupStates(StateMachine machine)
        {
            var docState = new DocState();
            var docIdState = new DocIdState();
            docState.Transfers.Add(docIdState, new DocStateTransfer() {NewState = docIdState, PreviousState = docState});

            var docIdEndState = new DocIdEndState();
            docIdState.Transfers.Add(docIdEndState, new DocIdTransfer() { NewState = docIdEndState, PreviousState = docIdState });

            var docIdEndTransfer = new DocIdEndTransfer() {PreviousState = docIdEndState};
            var spamState = new SpamState(docIdEndTransfer, machine);
            docIdEndTransfer.NewState = spamState;
            docIdEndState.Transfers.Add(spamState, docIdEndTransfer);

            spamState.Transfers.Add(docState, new DocEndTransfer() {NewState = docState, PreviousState = spamState});

            return docState;
        }
    }
}
