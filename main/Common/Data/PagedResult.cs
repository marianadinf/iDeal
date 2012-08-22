using System.Collections.Generic;

namespace UIT.iDeal.Common.Data
{
    public class PagedResult<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> PageOfItems { get; set; }
    }
}