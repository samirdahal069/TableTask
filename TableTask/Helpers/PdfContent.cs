using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TableTask.Helpers
{
   
    public class PdfContent : ActionResult
    {
        public MemoryStream MemoryStream { get; set; }
        public string FileName { get; set; }
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            var response = context.HttpContext.Response;
            response.ContentType = "pdf/application";
            response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".pdf");
            response.OutputStream.Write(MemoryStream.GetBuffer(), 0, MemoryStream.GetBuffer().Length);
        }
    }

}