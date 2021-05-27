﻿using System.Collections.Generic;

namespace LibraryApp.DAL.DTO
{
    public class Pager<TData>
    {
        public Pager(IReadOnlyCollection<TData> data, long totalCount)
        {
            Data = data;
            TotalCount = totalCount;
        }

        public IReadOnlyCollection<TData> Data { get; }

        public long TotalCount { get; }
    }
}