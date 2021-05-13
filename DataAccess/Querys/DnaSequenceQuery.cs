using DataAccess.Entities;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class DnaSequenceQuery
    {
        private readonly CoreContext CoreContext;

        public DnaSequenceQuery(CoreContext coreContext)
        {
            CoreContext = coreContext;
        }

        public async Task InsertDnaSequence()
        {
            var entity = new DnaSequence()
            {
                PersonDna = "",
                IsMutant = false
            };

            CoreContext.DnaSequences.Add(entity);
            await CoreContext.SaveChangesAsync();
        }
    }
}
