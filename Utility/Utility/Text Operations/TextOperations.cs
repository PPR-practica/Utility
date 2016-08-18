using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Text_Operations
{
    public static class TextOperations
    {
        public static List<string> Uniquify(List<string> inputStrings)
        {
            HashSet<string> uniqueStrings = new HashSet<string>();

            foreach(string piece in inputStrings)
            {
                uniqueStrings.Add(piece);
            }
            return uniqueStrings.ToList<string>();
        }

        public static List<string> SplitToList(string input, string delimiter)
        {
            return input.Split(delimiter.ToCharArray()).ToList();
        }
    }
}
