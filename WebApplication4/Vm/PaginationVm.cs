using System.Collections.Generic;

namespace WebApplication4.Vm
{
    public class PaginationVm<T>
    {
        public List<T> Items { get; set; }
        public int PageCount { get; set; }
        public int ActivPage { get; set; }
    }
}
