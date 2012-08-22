using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Database.Initialisation
{
    public interface IDataContextReferenceDataInitialiser : IReferenceDataInititaliser<DataContext>
    { }
}