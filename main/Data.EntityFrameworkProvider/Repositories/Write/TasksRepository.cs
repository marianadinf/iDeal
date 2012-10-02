using UIT.iDeal.Common.Interfaces.Data.Repositories.Write;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Domain.Model;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Repositories.Write
{
    public class TasksRepository : Repository<Task>, ITasksRepository
    {
        public TasksRepository(DataContext context) : base(context)
        { }
    }
}