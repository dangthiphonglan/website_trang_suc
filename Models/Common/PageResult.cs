using System;
using System.Collections.Generic;

namespace TreasureL.Models.Common
{
    public class PageResult<T>
    {
        public List<T> Items { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public int PageCount
        {
            get
            {
                var pageCount = (double)TotalRecords / PageSize;
                // Round a number upward to its nearest interger
                // print (math.ceil(1,4)) => 2
                return (int)Math.Ceiling(pageCount);
            }
        }
    }
}
