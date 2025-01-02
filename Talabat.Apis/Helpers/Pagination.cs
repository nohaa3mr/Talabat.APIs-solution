﻿namespace Talabat.Apis.Helpers
{
    public class Pagination<T>
    {
        public Pagination(int pageSize, int pageIndex, int count, IReadOnlyList<T> data )
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
               Count = count;
                Data = data;
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public IReadOnlyList<T> Data { get; set; }
        public int Count { get; set; }
    }
}
