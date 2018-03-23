using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TableTask.Helpers
{
    public static class HtmlExtensions
    {
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
          where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { Id = e, Name = e.ToString() };
            return new SelectList(values, "Id", "Name", enumObj);
        }

        #region Pagination
        public static string PagerQueryLink(this HtmlHelper helper, string pagedNoKey, int pageNo)
        {
            var request = helper.ViewContext.HttpContext.Request;
            RouteValueDictionary routeValues = request.QueryString.ToRouteDic();
            routeValues[pagedNoKey] = pageNo;
            return UrlHelper.GenerateUrl(null, null, null, routeValues, helper.RouteCollection, request.RequestContext, true);
        }

        public static string PagerQueryLink(this HtmlHelper helper, int pageNo)
        {
            return helper.PagerQueryLink("page", pageNo);
        }
        public static RouteValueDictionary ToRouteDic(this NameValueCollection collection)
        {
            return collection.ToRouteDic(new RouteValueDictionary());
        }

        public static RouteValueDictionary ToRouteDic(this NameValueCollection collection, RouteValueDictionary routeDic)
        {
            foreach (string key in collection.Keys)
            {
                if (!routeDic.ContainsKey(key))
                    routeDic.Add(key, collection[key]);
            }
            return routeDic;
        }
        #endregion
    }
}