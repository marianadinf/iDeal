using System.Data.Entity;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.Common.Interfaces.Data;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Repositories
{
    public class TasksQuery : Query<Task>, ITasksQuery
    {
        public TasksQuery(DataContext context) : base(context)
        {
        }
    }
}