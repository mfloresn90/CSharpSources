using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpamFilterAutomata.Transitions
{
    public class DocIdEndTransfer :StateTransfer
    {
        public string DocId { get; set; }

        public string Expected { get; protected set; }
        public int CurrentIndex { get; protected set; }

        public DocIdEndTransfer()
        {
            Expected = "</DOCID>";
        }

        public override Status ReadNext(char character)
        {
            if (Expected[CurrentIndex] != character)
            {
                if (CurrentIndex > 0)
                {
                    return Status.Failure;

                }
                DocId += character;

                return Status.Running;
            }

            CurrentIndex++;

            if (Expected.Length > CurrentIndex)
            {
                return Status.Running;
            }
            Console.WriteLine($"Current Doc:{DocId}");

            return Status.Success;
        }

        public override void Reset()
        {
            CurrentIndex = 0;
            DocId = "";
        }
    }
}
