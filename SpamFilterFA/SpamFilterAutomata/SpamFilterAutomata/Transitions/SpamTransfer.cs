using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpamFilterAutomata.Transitions
{
    public class SpamTransfer
    {
        public string DocId { get; set; }

        public List<string> Valid { get; protected set; }

        public List<string> Expected { get; protected set; }
        //public int CurrentIndex { get; protected set; }

        public bool Complete { get; set; }

        private string built = "";

        public SpamTransfer()
        {
            Expected = new List<string>()
            {
                "free access",
                "free software",
                "free vaction",
                "free trials",
                "win",
                "wins",
                "winner",
                "winners",
                "winning",
                "winnings",
            };
            Valid = new List<string>(Expected);
        }

        public Status ReadNext(char character)
        {
            if(character == '\r' || character == '\n')
                return Status.Running;

            if (character == ' ')
            {
                if (Valid.Count < 1)
                {
                    Reset();
                }
                else if (built != "free") //complete word
                {
                    Complete = true;
                    return Status.Success;
                }
                else
                {
                    if(built.Length > 0) { 
                        built += character; //add spaces if in middle
                    }
                }

                return Status.Running;

            }

            //if (Valid.Count < 1)
            //    Reset();

            built += character;

            List<int> removeIndexes = new List<int>();
            for (int cnt = 0; cnt < Valid.Count; cnt++)
            {
                if (Valid[cnt].Contains(built) == false || Valid[cnt][0] != built[0])
                {
                    removeIndexes.Add(cnt);
                    continue;
                }

            }

            for (int cnt = removeIndexes.Count - 1; cnt >= 0; cnt--)
            {
                Valid.RemoveAt(removeIndexes[cnt]);
            }

           // CurrentIndex++;
            if (Valid.Count < 1)
            {
                return Status.Running;
            }

            //for (int cnt = 0; cnt < Valid.Count; cnt++)
            //{
            //    if (Valid[cnt].Length == CurrentIndex)
            //    {
            //        //found one!
            //        Complete = true;
            //        return Status.Success;
            //    }

            //}
            return Status.Running;
        }

        public void Reset()
        {
            //CurrentIndex = 0;
            Valid = new List<string>(Expected);
            built = "";
        }
    }
}
