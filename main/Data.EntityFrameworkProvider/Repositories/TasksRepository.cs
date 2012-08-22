using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Domain.Model;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Repositories
{
    public class TasksRepository : Repository<Task>, ITasksRepository
    {
        public TasksRepository(DataContext context) : base(context)
        { }
    }
}