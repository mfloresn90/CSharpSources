using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpamFilterAutomata.Transitions;

namespace SpamFilterAutomata
{
    public class StateMachine
    {

        public State CurrentState { get; set; }
        public State StartState { get; set; }

        public List<SpamMessage> Spams { get; set; }

        public StateMachine()
        {
            Spams = new List<SpamMessage>();
        }

        public void Input(char character)
        {
            if (CurrentState == null)
            {
                Console.WriteLine("No State To Move To");
                return;
            }

            var curState = CurrentState;
            var status = curState.ReadNext(character);
            if (status == Status.Success)
            {
                CurrentState = curState.NextState;

                if (CurrentState == null)
                {
                    //done!
                    return;
                }
                else
                {
                    //setup
                    CurrentState.Reset();
                }
            }

            Console.WriteLine($"σ({curState.StateName},{character}) -> {CurrentState.StateName}");

        }

        public void AddSpam(string docId, SpamTransfer spamFilter)
        {
            Spams.Add(new SpamMessage(docId, spamFilter.Valid.First()));
        }

        public void PrintSpam()
        {
            Console.WriteLine("Found Spam:");
            for (int cnt = 0; cnt < Spams.Count; cnt++)
            {
                Console.WriteLine($"{Spams[cnt].MsgId}, Keyword: {Spams[cnt].SpamKeyword}");
            }
        }
    }

    public class SpamMessage
    {
        public string MsgId { get; set; }
        public string SpamKeyword { get; set; }

        public SpamMessage(string id, string spam)
        {
            MsgId = id;
            SpamKeyword = spam;
        }

    }
}
