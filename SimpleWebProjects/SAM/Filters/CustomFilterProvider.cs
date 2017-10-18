using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAM.Filters
{
    public class CustomActionFilterProvider : IFilterProvider
    {
        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            return new[] { new Filter(new CustomActionFilterAttribute(), FilterScope.Global, 0) };
        }
    }
}