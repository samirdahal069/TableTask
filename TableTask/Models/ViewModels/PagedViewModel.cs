using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TableTask.Models.ViewModels
{
    public class PagedViewModel<T>
    {
        public string DefaultSortColumn { get; set; }
        public int? Page { get; set; }
        public IPagedList<T> PagedList { get; set; }

        public int? PageSize { get; set; }
        public IEnumerable<T> Query { get; set; }


        public PagedViewModel<T> Setup()
        {
            PagedList = Query.ToPagedList(Page ?? 1, PageSize ?? 5);
            return this;
        }
    }
}