using System;
using System.Collections.Generic;
using System.Linq;

namespace Utility.Text_Operations
{
    public static class TextOperations
    {
        public static List<string> Uniquify(List<string> inputStrings)
        {
            IEnumerable<string> uniqueStrings = inputStrings.Distinct();
            return uniqueStrings.ToList<string>();
        }

        public static List<string> SplitToList(string input, char[] delimiter)
        {
            input = input.Replace(Environment.NewLine, "");            //  remove newline to avoid duplicates
            return input.Split(delimiter).ToList();
        }

        public static List<string> AddSuffix(List<string> input, string suffix)
        {
            foreach(string part in input)
            {
                input.Remove(part);
                input.Add(part + suffix);
            }
            return input;
        }
    }
}
