using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Motonet.Extensions
{
    public static class Extensions
    {

        public static string AbsoluteContent(this UrlHelper url, string contentPath)
        {
            var requestUrl = url.RequestContext.HttpContext.Request.Url;
            return string.Format(
                "{0}{1}",
                requestUrl.GetLeftPart(UriPartial.Authority),
                url.Content(contentPath)
            );
        }

    }
}