using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpamFilterAutomata.Transitions
{
    public class DocEndTransfer :StateTransfer
    {
        public string Expected { get; protected set; }
        public int CurrentIndex { get; protected set; }

        public DocEndTransfer()
        {
            Expected = "</DOC>";
        }

        public override Status ReadNext(char character)
        {
            if (Expected[CurrentIndex] != character)
                return Status.Failure;

            CurrentIndex++;

            if (Expected.Length > CurrentIndex)
            {
                return Status.Running;
            }

            return Status.Success;
        }

        public override void Reset()
        {
            CurrentIndex = 0;
        }
    }
}
