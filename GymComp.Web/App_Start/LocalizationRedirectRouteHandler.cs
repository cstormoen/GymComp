using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GymComp.Web.App_Start
{
    class LocalizationRedirectRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var routeValues = requestContext.RouteData.Values;

            routeValues["culture"] = CultureInfo.CurrentCulture.Name;

            return new RedirectHandler(new UrlHelper(requestContext).RouteUrl(routeValues));
        }
    }
}
