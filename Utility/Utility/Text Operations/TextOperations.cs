using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Text_Operations
{
    public class TextOperations
    {
        public static List<string> SplitAndUniquify(string inputString, string delimiter)
        {
            //List<string> uniqueStrings = new List<string>();
            HashSet<string> uniqueStrings = new HashSet<string>();

            string[] splittedInput = inputString.Split(delimiter.ToCharArray());

            foreach(string piece in splittedInput)
            {
                uniqueStrings.Add(piece);
            }
            return uniqueStrings.ToList<string>();
        }
    }
}
