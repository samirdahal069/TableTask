using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace TableTask.Helpers
{
    public static class Dic1Converter
    {


        public static DataTable DicTo1DataTable(this IEnumerable<Dictionary<string, object>> input)
        {
            DataTable table = new DataTable();
            foreach (IDictionary<string, object> row in input)
            {
                foreach (KeyValuePair<string, object> entry in row)
                {
                    if (!table.Columns.Contains(entry.Key.ToString()))
                    {
                        table.Columns.Add(entry.Key);
                    }
                }
                table.Rows.Add(row.Values.ToArray());
            }
            return table;

        } 


        public static List<object> DatatableToList(this DataTable dt)
        {
             List<object> list = new List<object>();
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    list.Add(row[col]);
                }
            }
            return list;

        }
       
    }
}