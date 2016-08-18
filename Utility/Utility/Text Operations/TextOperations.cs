using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Text_Operations
{
    public static class TextOperations
    {
        /// <summary>
        /// method...
        /// </summary>
        /// <param name="inputStrings"></param>
        /// <returns></returns>
        public static List<string> Uniquify(List<string> inputStrings)
        {
            // TODO: 18.08.2016
            //STUDY: https://msdn.microsoft.com/en-us/library/bb353005(v=vs.110).aspx
            //in sectiunea de remarks, aveti: If Count is less than the capacity of the internal array, this method is an O(1) operation. If the HashSet<T> object must be resized, this method becomes an O(n) operation, where n is Count.
            //CE INSEAMNA ASTA?
            //AS ALLWAYS, nu ma intere ce folositi, HASET, LIST, LINQ sau ce mai vreti voi, da pls, inainte de a lucra cu o clasa pe care nu ati mai folosit-o veci, studiati un pic clasa aia.
            //Recomandations: LINQ.Distinct
            //NOTE TO SELF: find a reason to ask IComparable implementation.
            HashSet<string> uniqueStrings = new HashSet<string>();

            foreach(string piece in inputStrings)
            {
                uniqueStrings.Add(piece);
            }
            return uniqueStrings.ToList<string>();
        }
        
        // TODO: 18.08.2016: de ce nu vine delimitatoru ca si charArray?. 
        //SRP violation, avem 2 operatiuni aci... mai mult sau mai putin, dar mna...
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static List<string> SplitToList(string input, string delimiter)
        {
            return input.Split(delimiter.ToCharArray()).ToList();
        }
    }
}
