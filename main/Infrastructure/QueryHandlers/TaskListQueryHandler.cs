using System.Collections.Generic;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.ViewModel.Tasks;

namespace UIT.iDeal.Infrastructure.QueryHandlers
{
    public class TaskListQueryHandler : QueryHandler
    {
        readonly ITasksQuery _query;

        public TaskListQueryHandler(ITasksQuery query)
        {
            _query = query;
        }

        public override object BuildViewModel()
        {
            var allTasks = _query.GetAll();

            return MappingEngine.Map<IEnumerable<Task>, IEnumerable<TaskItemViewModel>>(allTasks);

        }
    }
}