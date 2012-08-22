using UIT.iDeal.Common.Builders.Entities.Framework;
using UIT.iDeal.Domain.Model;

namespace UIT.iDeal.Common.Builders.Entities
{
    public class TaskBuilder : EntityBuilder<Task>
    {
        public TaskBuilder(int listSize) : base(listSize)
        {
            
        }

        public TaskBuilder() 
        {
            
        }
      
    }
}
