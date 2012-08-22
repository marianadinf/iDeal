using System;
using UIT.iDeal.Domain.Model.Base;

namespace UIT.iDeal.Domain.Model
{
    public class Task : AggregateRoot
    {
        public virtual string Description { get; protected set; }
        public virtual bool IsDone { get; protected set; }
        public virtual DateTime ToBeCompletedByDate { get; protected set; }

        public static Task Create(string description, DateTime toBeCompletedByDate, bool isDone = false)
        {
            return new Task
                       {
                           Description = description,
                           ToBeCompletedByDate = toBeCompletedByDate,
                           IsDone = isDone
                       };
        }

       // Business Rule sh
        public virtual void Edit(string description, bool isDone, DateTime toBeCompletedByDate)
        {
            Description = description;
            IsDone = isDone;
            ToBeCompletedByDate = toBeCompletedByDate;

        }
    }
}