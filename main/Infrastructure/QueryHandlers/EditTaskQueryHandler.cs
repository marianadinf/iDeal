using System;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.ViewModel.Tasks;

namespace UIT.iDeal.Infrastructure.QueryHandlers
{
    public class EditTaskQueryHandler : QueryHandler
    {
        readonly ITasksQuery _query;

        public EditTaskQueryHandler(ITasksQuery query)
        {
            _query = query;
            
        }

        public override object BuildViewModel()
        {
            var id = GetFirstArgumentOfType<Guid>();

            var task = _query.Get(id);

            

            return MappingEngine.Map<Task,EditTaskForm>(task);
        }
    }
}