using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IMutantLogic
    {
        /// <summary> 
        /// Validate if is a mutant's chain 
        /// </summary>
        /// <param name="dna">dna list</param>
        /// <returns>Is a mutant</returns>
        bool IsMutant(List<string> dna);
    }
}
