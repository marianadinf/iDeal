using UIT.iDeal.Common.Interfaces.Data.Repositories.Read;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Domain.Model;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Repositories.Read
{
    public class TasksQuery : Query<Task>, ITasksQuery
    {
        public TasksQuery(DataContext context) : base(context)
        {
        }
    }
}