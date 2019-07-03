using System.Collections.Generic;

namespace VR.Web.Helpers
{
    public class PagedResult<T>
    {
        public List<T> List { get; set; }
        public long TotalRecords { get; set; }
    }
}