using System.Data.Entity.ModelConfiguration;
using UIT.iDeal.Domain.Model;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Context
{
    public class DataContextConfiguration : EntityTypeConfiguration<DataContext>
    {
        public DataContextConfiguration()
        {
          
        }
    }
}