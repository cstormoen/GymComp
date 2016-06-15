using System.Web;

namespace GymComp.Web.App_Start
{
    public class RedirectHandler : IHttpHandler
    {
        private string _newUrl;

        public RedirectHandler(string newUrl)
        {
            _newUrl = newUrl;
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Redirect(_newUrl);
        }
    }
}
