using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessLayer.BusinessLogic
{
    public class MutantLogic : IMutantLogic
    {
        public bool IsMutant(List<string> dna)
        {
            string regexFormat = "^[ATCG]+$";
            Regex regex = new Regex(regexFormat);

            foreach (string chain in dna)
            {
                MatchCollection matchDNA = regex.Matches(chain);
                if (matchDNA.Count <= 0)
                    return false;
            }

            return true;
        }
    }
}
