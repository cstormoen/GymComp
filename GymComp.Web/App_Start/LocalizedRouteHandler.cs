using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GymComp.Web.App_Start
{
    public class LocalizedRouteHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var urlLocale = requestContext.RouteData.Values["culture"] as string;
            var cultureName = urlLocale ?? "";

            if (cultureName == "")
            {
                return GetDefaultLocaleRedirectHandler(requestContext);
            }

            try
            {
                var culture = CultureInfo.GetCultureInfo(cultureName);

                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            catch (CultureNotFoundException)
            {
                // if CultureInfo.GetCultureInfo throws exception
                // we should redirect with default locale
                return GetDefaultLocaleRedirectHandler(requestContext);
            }

            return base.GetHttpHandler(requestContext);
        }

        private static IHttpHandler GetDefaultLocaleRedirectHandler(RequestContext requestContext)
        {
            var uiCulture = CultureInfo.CurrentUICulture;
            var routeValues = requestContext.RouteData.Values;
            routeValues["culture"] = uiCulture.Name;
            return new RedirectHandler(new UrlHelper(requestContext).RouteUrl(routeValues));
        }
    }
}
